using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using ShooterMcGavinBot.Common;
using ShooterMcGavinBot.Modules;
using ShooterMcGavinBot.Services;

namespace ShooterMcGavinBot.Main
{
    class Program
    {
        private IConfiguration _config;
        private DiscordSocketConfig _clientConfig;
        private Assembly _moduleAssembly;        
        private DiscordSocketClient _client;
        private CommandService _commandService; 

        private Program()
        {
            //load configurations 
            _config = BuildConfig();
            _clientConfig = BuildClientConfig();
            //load modules assembly
            _moduleAssembly = Assembly.GetEntryAssembly();
            //initialize private members to use in DI later            
            _client = new DiscordSocketClient(_clientConfig);
            _commandService = new CommandService();
        }

        public static void Main(string[] args)
        {   
            Console.WriteLine("Starting Shooter McGavin Bot.");
            Console.WriteLine("Press CTRL+C to exit.");
            try 
            {
                new Program().MainAsync()
                             .GetAwaiter()
                             .GetResult();
            }
            catch (BotGeneraicException e)
            {
                Console.WriteLine(e.Message);
                return;                   
            }                     
        }

        public async Task MainAsync() 
        {
            var services = ConfigureServices();
            await services.GetRequiredService<ICommandHandler>().Start();
        }

        private string GetEnvironment()
        {
            var env = Environment.GetEnvironmentVariable("DOTNETCORE_ENVIRONMENT");
            if(String.IsNullOrWhiteSpace(env)){
                env = "Development";
            }
            return env;
        }

        private IServiceProvider ConfigureServices()
        {
            //add the services
            var services = new ServiceCollection();
            //add the objects initialized in the constructor
            services.AddSingleton<IConfiguration>(_config);
            services.AddSingleton<Assembly>(_moduleAssembly);
            services.AddSingleton<DiscordSocketClient>(_client);
            services.AddSingleton<CommandService>(_commandService);  
            //main shooter bot for handling commands and any single instance objects
            services.AddSingleton<ICommandHandler, CommandHandler>();
            services.AddSingleton<ILogger, Logger>();
            services.AddSingleton<IBotStringsContainer, BotStringsContainer>();
            //add service assemblies
            services.AddTransient<IShooterService, ShooterService>();
            services.AddTransient<IHelpService, HelpService>();
            return services.BuildServiceProvider();
        }

        private IConfiguration BuildConfig()
        {
            var env = GetEnvironment();
            var config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("config.json")
                            .AddJsonFile($"config.{env}.json")
                            .Build();
            return config;
        }

        private DiscordSocketConfig BuildClientConfig() 
        {
            var logLevel = _config["log_level"];
            var clientConfig = new DiscordSocketConfig();
            switch(logLevel) 
            {
                case "Critical":
                    clientConfig.LogLevel = LogSeverity.Critical;
                    break;
                case "Debug":
                    clientConfig.LogLevel = LogSeverity.Debug;
                    break;
                case "Error":
                    clientConfig.LogLevel = LogSeverity.Error;
                    break;
                case "Info":
                    clientConfig.LogLevel = LogSeverity.Info;
                    break;
                case "Verbose":
                    clientConfig.LogLevel = LogSeverity.Verbose;
                    break;
                case "Warning":
                    clientConfig.LogLevel = LogSeverity.Warning;
                    break;
                default:
                    clientConfig.LogLevel = LogSeverity.Info;
                    break;
            }
            return clientConfig;
        }
    }
}
