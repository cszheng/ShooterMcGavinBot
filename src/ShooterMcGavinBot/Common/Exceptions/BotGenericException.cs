using System;

namespace ShooterMcGavinBot.Common
{
    public class BotGeneraicException : Exception
    {
        //constructors
        public BotGeneraicException(string message) 
        : base(message)
        {
        }

        public BotGeneraicException(string message, Exception inner) 
        : base(message, inner)
        {
        }
    }
}