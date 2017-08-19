using System;

namespace ShooterMcGavinBot.Common
{
    public class BotGeneraicException : Exception
    {
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