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
        //private members
        protected IHelpService _helpService;

        //constructors
        public HelpModule(IHelpService helpService)
        {
            _helpService = helpService;
        }
        
        //public functions
        [Command]
        public async Task Default()
        {  
            await _helpService.help(Context, this.GetType());
        }
    }
}
