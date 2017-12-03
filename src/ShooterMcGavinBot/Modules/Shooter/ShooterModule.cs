using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using ShooterMcGavinBot.Services;

namespace ShooterMcGavinBot.Modules
{
    // Create a module with no prefix
    [Group("shooter"), Summary("Your one and only Shooter McGavin.")]
    public class ShooterModule : ModuleBase
    {
        protected IShooterService _shooterService;

        public ShooterModule(IShooterService shooterService)
        {
            _shooterService = shooterService;
        }
        
        [Command()]
        public async Task Default()
        {
            await help(); 
        }                
        
        [Command("help"), Summary("Shows options of shooter command.")]
        public async Task help()
        {
            await Context.Channel.SendMessageAsync("", embed: _shooterService.help(this.GetType()));
        }        
        
        [Command("roast"), Summary("Shooter will roast someone.")]
        public async Task roast([Summary("(optional) The person that Shooter will roast. [@mention]")] IUser user = null)
        {   
            if (user != null) 
            {               
                string roastMessage = _shooterService.roast(user.Mention);
                await Context.Channel.SendMessageAsync(roastMessage);
            }
            else 
            {   string roastMessage = _shooterService.roast();
                await Context.Channel.SendMessageAsync(roastMessage);
            }
        }
    }
}
