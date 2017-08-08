using System;
using System.Threading.Tasks;
using Discord;

namespace ShooterMcGavinBot.Common
{
    public class Logger: ILogger
    {
        public Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}