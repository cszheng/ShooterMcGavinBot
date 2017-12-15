using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Discord;
using Discord.Commands;
using ShooterMcGavinBot.Common;

namespace ShooterMcGavinBot.Services 
{
    public class ShooterService: BotService, IShooterService
    {   
        //private members
        protected string[] _roasts;
        protected string _creatorUserName;

        //constructors
        public ShooterService(IConfiguration config,
                              IBotStringsContainer botString)
        : base(botString) 
        {   
            //init roasts
            _roasts = _botStrings.getContainer("shooter")
                                 .Container
                                 .Where(t => new Regex(@"quote_\d+").Match(t.Key).Success)
                                 .Select(t => t.Value)
                                 .ToArray();
            //set creator name
            _creatorUserName = config["creator_username"];
        }

        //public functions
        public async Task roast(ICommandContext cmdCntx, IUser user)
        {
            string quote = getRandomRoast();
            IUser userToRoast;            
            //determine who to roast
            if (user == null)
            {
                userToRoast = null; //roast noone
            }
            else 
            {
               if (cmdCntx.User.Username == _creatorUserName)
               {
                   //conditions for when creator is roasting
                   if(user.Id == cmdCntx.Client.CurrentUser.Id) 
                   {
                       //creator cannot roast bot, so roast nothing
                       userToRoast = null;
                   }
                   else 
                   {
                       //creator roast whoever else, including self
                       userToRoast = user;                       
                   }
               }
               else 
               {
                   //regular user roast
                   if(user.Id == cmdCntx.Client.CurrentUser.Id)
                   {
                       //regular user cannot roast bot, roast self isntead...
                       userToRoast = cmdCntx.User;
                   }
                   else if (user.Username == _creatorUserName)
                   {
                       //regular user cannot roast creator, roast self instead
                       userToRoast = cmdCntx.User;
                   }
                   else 
                   {
                       //regular user roast whoever
                       userToRoast = user;
                   }
               }
            }
            string mention = userToRoast != null ? userToRoast.Mention : "@here";
            await cmdCntx.Channel.SendMessageAsync($"{mention} {quote}");            
        }

        public async Task pewpew(ICommandContext cmdCntx)
        {
            Embed pewPewEmbed = buildPewPewEmbed();
            await cmdCntx.Channel.SendMessageAsync("", embed: pewPewEmbed);       
        }

        //private functions
        private string getRandomRoast()
        {
            int randIndx = new Random().Next(0, _roasts.Length);
            string retQuote = _roasts[randIndx];
            return retQuote;
        }

        private Embed buildPewPewEmbed()
        {
            string pewpewtitle = _botStrings.getString("shooter", "pewpewtitle");
            string pewpewurl = _botStrings.getString("shooter", "pewpewurl");
            Embed embeded = new EmbedBuilder().WithTitle(pewpewtitle)
                                              .WithImageUrl(pewpewurl);
            return embeded;
        }
    }
}