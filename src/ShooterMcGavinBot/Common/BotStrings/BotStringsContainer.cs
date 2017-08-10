using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ShooterMcGavinBot.Common
{
    public class BotStringsContainer: IBotStringsContainer
    {
        protected IConfiguration _config;

        public Dictionary<string, BotStrings> Containers { get; private set; }
        
        public BotStringsContainer(IConfiguration config)
        {
            _config = config;
            Containers = buildContainer();
            Containers.ToImmutableDictionary();
        }

        public BotStrings getContainer(string containerKey)
        {
            return Containers[containerKey];
        }

        public string getString(string containerKey, string stringKey)
        {
            if (Containers.ContainsKey(containerKey)) {
                return Containers[containerKey].getString(stringKey);
            }            
            return null;
        }
        
        private Dictionary<string, string> getFilePaths()
        {
            var filePaths = new Dictionary<string, string>();
            //var botStringsDir = Path.Combine(Directory.GetCurrentDirectory(), _config["bot_strings_path"]);
            var botStrDir =  _config["bot_strings_path"];
            if(Directory.Exists(botStrDir))
            {
                filePaths = Directory.GetFiles(botStrDir)
                                     .Where(t => t.EndsWith(".json"))
                                     .Select(t => new KeyValuePair<string, string>(Path.GetFileName(t).Replace(".json", ""), t))
                                     .ToDictionary(t => t.Key, t => t.Value);
            }
            return filePaths;
        }

        private Dictionary<string, BotStrings> buildContainer()
        {
            var filePaths = getFilePaths();
            var botStringsDict = new Dictionary<string, BotStrings>();
            if(filePaths.Count > 0)
            {
                botStringsDict = filePaths.ToDictionary(t => t.Key, t => new BotStrings(t.Value));
            }
            return botStringsDict;
        }
    }
}