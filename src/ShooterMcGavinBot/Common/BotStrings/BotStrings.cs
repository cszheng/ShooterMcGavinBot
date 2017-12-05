using System.IO;
using System.Collections.Generic;
using System.Collections.Immutable;
using Newtonsoft.Json;

namespace ShooterMcGavinBot.Common
{
    public class BotStrings : IBotStrings
    {
        //public properties
        public Dictionary<string, string> Container { get; private set; }
        
        //constructors
        public BotStrings(string jsonFilePath)
        {
            if(!File.Exists(jsonFilePath)) 
            {
                throw new BotGeneraicException("Json file not found");
            }
            string jsonString = File.ReadAllText(jsonFilePath);
            Container = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
            Container.ToImmutableDictionary();
        }

        //public functions
        public string getString(string key)
        {
            if (!Container.ContainsKey(key)) 
            {
                throw new BotGeneraicException($"{key} botstring not found");
            }            
            return Container[key];
        }
    }
}