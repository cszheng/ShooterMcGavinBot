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
            if (!Containers.ContainsKey(containerKey)) 
            {
                throw new BotGeneraicException($"{containerKey} container not found");
            }
            return Containers[containerKey];
        }

        public string getString(string containerKey, string stringKey)
        {        
            return getContainer(containerKey).getString(stringKey);
        }
        
        private Dictionary<string, string> getFilePaths()
        {
            var filePaths = new Dictionary<string, string>();
            var botStrDir =  _config["bot_strings_path"];
            if(!Directory.Exists(botStrDir)) 
            {
                throw new BotGeneraicException("Directory not found");
            }
            filePaths = Directory.GetFiles(botStrDir)
                                 .Where(t => t.EndsWith(".json"))
                                 .Select(t => new KeyValuePair<string, string>(Path.GetFileName(t).Replace(".json", ""), t))
                                 .ToDictionary(t => t.Key, t => t.Value);
            return filePaths;
        }

        private Dictionary<string, BotStrings> buildContainer()
        {
            var filePaths = getFilePaths();
            var botStringsDict = new Dictionary<string, BotStrings>();
            if(filePaths.Count == 0)
            {
                throw new BotGeneraicException("No json files in directory");
            }
            botStringsDict = filePaths.ToDictionary(t => t.Key, t => new BotStrings(t.Value));            
            return botStringsDict;
        }
    }
}