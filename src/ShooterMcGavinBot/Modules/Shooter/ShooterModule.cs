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
        //private members
        protected IShooterService _shooterService;

        //constructors
        public ShooterModule(IShooterService shooterService)
        {
            _shooterService = shooterService;
        }
        
        //public functions
        [Command()]
        public async Task Default()
        {
            await help(); 
        }                
        
        [Command("help"), Summary("Shows options of shooter command.")]
        public async Task help()
        {
            await _shooterService.help(Context, this.GetType());
        }        
        
        [Command("roast"), Summary("Shooter will roast someone.")]
        public async Task roast([Summary("(optional) The person that Shooter will roast. [@mention]")] IUser user = null)
        {   
            await _shooterService.roast(Context, user);
        }

        [Command("pewpew"), Summary("Pew Pew Pew!")]
        public async Task pewpew()
        {   
            await _shooterService.pewpew(Context);
        }
    }
}
