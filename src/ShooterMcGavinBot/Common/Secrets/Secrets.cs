using System;
using System.Collections.Generic;

namespace ShooterMcGavinBot.Common
{
    public class Secrets: ISecrets
    {
        protected Dictionary<string, string> _secrets;
        
        public Secrets()
        {
            _secrets = new Dictionary<string, string>();
            try 
            {
                //grab secrets loaded in environment variables
                LoadEnvironemntSecrets();
                //validate they all exist
                ValidateEnvironmentSecrets();
            }
            catch(Exception e)
            {
                throw new BotGeneraicException("Failed to load secrets", e);
            }
        }

        private void LoadEnvironemntSecrets()
        {
            _secrets.Add("DOTNETCORE_ENVIRONMENT", Environment.GetEnvironmentVariable("DOTNETCORE_ENVIRONMENT"));
            _secrets.Add("DISCORDBOT_TOKEN", Environment.GetEnvironmentVariable("DISCORDBOT_TOKEN"));
        }

        private void ValidateEnvironmentSecrets()
        {
            foreach(var secret in _secrets)
            {
                if (String.IsNullOrEmpty(secret.Value))
                {
                    throw new BotGeneraicException($"{secret.Key} secret is not found");
                }
            }
        }
        
        public string GetSecret(string key)
        {
            if(!_secrets.ContainsKey(key)){
                throw new BotGeneraicException($"{key} secret not found");
            }          
            return _secrets[key];
        }
    }
}