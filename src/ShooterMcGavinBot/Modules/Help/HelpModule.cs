using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using ShooterMcGavinBot.Services;

namespace ShooterMcGavinBot.Modules
{
    // Create a module with no prefix
    [Group("help"), Summary("Gives a list of all commands available in this bot.")]
    public class HelpModule : ModuleBase
    {       
        protected IHelpService _helpService;

        public HelpModule(IHelpService helpService)
        {
            _helpService = helpService;
        }
        
        [Command]
        public async Task Default()
        {  
            Embed helpEmbed = _helpService.help(this.GetType());
            //send the message
            await Context.Channel.SendMessageAsync("", embed: helpEmbed);
        }
    }
}
