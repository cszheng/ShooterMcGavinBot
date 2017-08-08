using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ShooterMcGavinBot.Common
{
    public class BotStringsContainer: IBotStringsContainer
    {
        protected IConfiguration _config;
        protected Dictionary<string, BotStrings> _container;
        
        public BotStringsContainer(IConfiguration config)
        {
            _config = config;
            _container = buildContainer();
            _container.ToImmutableDictionary();
        }

        public string getString(string containerKey, string stringKey)
        {
            if (_container.ContainsKey(containerKey)) {
                return _container[containerKey].getString(stringKey);
            }            
            return null;
        }
        
        private Dictionary<string, string> getFilePaths()
        {
            var i = 0;
            var filePaths = new Dictionary<string, string>();
            var currentDir = Directory.GetCurrentDirectory();
            //loop until no more file paths from config
            do
            {
                var key = _config[$"bot_string_files:{i}:key"];
                var path = _config[$"bot_string_files:{i}:path"];
                filePaths.Add(key, path);
                i++;
            } 
            while(_config[$"bot_string_files:${i}:key"] != null);
            //done gathering file names
            return filePaths;
        }


        private Dictionary<string, BotStrings> buildContainer()
        {
            var filePaths = getFilePaths();
            var botStringsDict = new Dictionary<string, BotStrings>();
            foreach(var entry in  filePaths)
            {
                var key = entry.Key;
                var fullPath = entry.Value;
                botStringsDict.Add(key, new BotStrings(fullPath));
            }
            return botStringsDict;
        }
    }
}