using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using ShooterMcGavinBot.Common;

namespace ShooterMcGavinBot.Services 
{
    public class ShooterService: BotService, IShooterService
    {   
        //private members
        protected string[] _roasts;

        //constructors
        public ShooterService(IBotStringsContainer botString)
        : base(botString) 
        {   
            //init roasts
            _roasts = _botStrings.getContainer("shooter")
                                 .Container
                                 .Where(t => new Regex(@"quote_\d+").Match(t.Key).Success)
                                 .Select(t => t.Value)
                                 .ToArray();
        }

        //public functions
        public async Task roast(ICommandContext cmdCntx, IUser user)
        {
            string quote = getRandomRoast();
            string mention = (user != null) ? user.Mention : "@here"; 
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