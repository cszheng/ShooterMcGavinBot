using System.Threading.Tasks;
using Discord;

namespace ShooterMcGavinBot.Common
{
    public interface ILogger
    {
        Task Log(LogMessage message);
        void Log(string message);
    }
}