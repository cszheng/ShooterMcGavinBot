using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Immutable;
using Newtonsoft.Json;

namespace ShooterMcGavinBot.Common
{
    public class BotStrings
    {
        protected Dictionary<string, string> _botStrings;
        
        public BotStrings(string jsonFilePath)
        {
            var jsonString = File.ReadAllText(jsonFilePath);
            _botStrings = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
            _botStrings.ToImmutableDictionary();
        }

        public string getString(string key)
        {
            if (_botStrings.ContainsKey(key)) {
                return _botStrings[key];
            }            
            return null;
        }
    }
}