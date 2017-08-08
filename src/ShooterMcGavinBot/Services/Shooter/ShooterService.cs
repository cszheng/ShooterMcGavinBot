using System;
using Discord;
using ShooterMcGavinBot.Common;

namespace ShooterMcGavinBot.Services 
{
    public class ShooterService: BotService, IShooterService
    {   
        //public properties
        protected string[] _roasts;

        //constructor
        public ShooterService(IBotStringsContainer botString)
        : base(botString) 
        {   
            //init quotes
            string[] roasts = 
            {
                "You know what *else* could draw a crowd? A golfer with an arm growing out of his ass.",
                "Damn you people. Go back to your shanties.",
                "Damn you people. This is golf. Not a rock concert.",                
                "Ah ah. You lay another finger on me, I burn the house down and piss on the ashes.",
                "You're in big trouble though, pal. I eat pieces of shit like you for breakfast!",
                "Just stay out of my way... or you'll pay! LISTEN to what I say!"
            };          
            //set the properties
            _roasts = roasts;
        }

        public string roast(string userMention = null)
        {
            int randIndx = new Random().Next(0, _roasts.Length);
            string retQuote = _roasts[randIndx];
            if(userMention != null)
            {
                return String.Format("{0} {1}", userMention, retQuote);
            }
            return String.Format("@here {0}", retQuote);
        }
    }
}