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
        //private members
        protected IConfiguration _config;

        //public properties
        public Dictionary<string, IBotStrings> Containers { get; private set; }
        
        //constructors
        public BotStringsContainer(IConfiguration config)
        {
            _config = config;
            Containers = buildContainer();
            Containers.ToImmutableDictionary();
        }

        //public functions
        public IBotStrings getContainer(string containerKey)
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
        
        //private functions
        private Dictionary<string, string> getFilePaths()
        {
            Dictionary<string, string> filePaths = new Dictionary<string, string>();
            string botStrDir =  _config["bot_strings_path"];
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

        private Dictionary<string, IBotStrings> buildContainer()
        {
            Dictionary<string, string> filePaths = getFilePaths();
            Dictionary<string, IBotStrings> botStringsDict = new Dictionary<string, IBotStrings>();
            if(filePaths.Count == 0)
            {
                throw new BotGeneraicException("No json files in directory");
            }
            botStringsDict = filePaths.ToDictionary(t => t.Key, 
                                                    t => {  
                                                        IBotStrings botStr = new BotStrings(t.Value);
                                                        return botStr;
                                                    });            
            return botStringsDict;
        }
    }
}