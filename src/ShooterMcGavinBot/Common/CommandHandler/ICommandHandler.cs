using System.Threading.Tasks;

namespace ShooterMcGavinBot.Common
{
    public interface ICommandHandler
    {
        Task Start();
    }
}