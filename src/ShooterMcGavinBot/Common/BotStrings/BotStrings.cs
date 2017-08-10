using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Immutable;
using Newtonsoft.Json;

namespace ShooterMcGavinBot.Common
{
    public class BotStrings
    {
        public Dictionary<string, string> Container { get; private set; }
        
        public BotStrings(string jsonFilePath)
        {
            var jsonString = File.ReadAllText(jsonFilePath);
            Container = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
            Container.ToImmutableDictionary();
        }

        public string getString(string key)
        {
            if (Container.ContainsKey(key)) {
                return Container[key];
            }            
            return null;
        }
    }
}