using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace ShooterMcGavinBot.Services 
{
    public interface IShooterService : IBotService
    {
        Task roast(ICommandContext commandContext, IUser user);
        Task pewpew(ICommandContext commandContext);
    }
}