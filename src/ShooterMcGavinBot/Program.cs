﻿using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using ShooterMcGavinBot.Common;
using ShooterMcGavinBot.Services;

namespace ShooterMcGavinBot.Main
{
    public class Program
    {
        //private members
        private IConfiguration _config;
        private DiscordSocketConfig _clientConfig;
        private Assembly _moduleAssembly;        
        private DiscordSocketClient _client;
        private CommandService _commandService; 

        //constructors
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

        //public functions
        public static int Main(string[] args)
        {   
            try 
            {
                Console.WriteLine("Starting Shooter McGavin Bot.");
                Console.WriteLine("Press CTRL+C to exit.");
                new Program().MainAsync()
                             .GetAwaiter()
                             .GetResult();
                Console.WriteLine("Stopping Shooter McGavin Bot.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured, terminating program. Error: ${e.Message}");
                return 1;                   
            }
            return 0;                     
        }

        public async Task MainAsync() 
        {
            IServiceProvider services = ConfigureServices();
            await services.GetRequiredService<ICommandHandler>().Start();
            await services.GetRequiredService<ICommandHandler>().WaitForStop();
        }

        //private functions
        private string GetEnvironment()
        {
            string env = Environment.GetEnvironmentVariable("DOTNETCORE_ENVIRONMENT");
            if(String.IsNullOrWhiteSpace(env)){
                env = "Development";
            }
            return env;
        }

        private IServiceProvider ConfigureServices()
        {
            //add the services
            ServiceCollection services = new ServiceCollection();
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
            string env = GetEnvironment();
            IConfiguration config = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("config.json")
                                        .AddJsonFile($"config.{env}.json")
                                        .Build();
            return config;
        }

        private DiscordSocketConfig BuildClientConfig() 
        {
            string logLevel = _config["log_level"];
            DiscordSocketConfig clientConfig = new DiscordSocketConfig();
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
