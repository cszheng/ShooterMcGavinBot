using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            //init roasts
            _roasts = _botStrings.getContainer("shooter")
                                 .Container
                                 .Where(t => new Regex(@"quote_\d+").Match(t.Key).Success)
                                 .Select(t => t.Value)
                                 .ToArray();
        }

        public string roast(string userMention = null)
        {
            int randIndx = new Random().Next(0, _roasts.Length);
            string retQuote = _roasts[randIndx];
            if(userMention != null)
            {
                return $"{userMention} {retQuote}";
            }
            return $"@here {retQuote}";
        }
    }
}