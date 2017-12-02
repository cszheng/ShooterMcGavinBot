using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace ShooterMcGavinBot.Common
{
    public class CommandHandler : ICommandHandler
    {
        //private variables
        protected IServiceProvider _provider;
        protected IConfiguration _config;
        protected Assembly _entryAssembly;
        protected DiscordSocketClient _client;
        protected CommandService _commands;
        protected ILogger _logger;
        //constructor
        public CommandHandler(IServiceProvider provider, 
                              IConfiguration config,
                              Assembly entryAssembly,
                              DiscordSocketClient client, 
                              CommandService commands, 
                              ILogger logger)
        {
            _provider = provider;
            _config = config;
            _entryAssembly = entryAssembly;           
            _client = client;
            _commands = commands;
            _logger = logger;
        }

        //public functions
        public async Task Start() 
        {   
            var token = _config["discordbot_token"];
            //command events
            HookEvents();
            //command modules
            await AddCommandModules();
            //start the client          
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }
        
        private void HookEvents()
        {
            //hook the logger
            _client.Log += _logger.Log;
            // Hook the MessageReceived Event into our Command Handler
            _client.MessageReceived += HandleMessages;
        }
        
        private async Task AddCommandModules()
        {
            // Discover all of the commands in this assembly and load them
            await _commands.AddModulesAsync(_entryAssembly);
        }

        private async Task HandleMessages(SocketMessage message)
        {   
            try 
            {
                //handle differnet messages depending the message type
                SocketUserMessage userMessage = message as SocketUserMessage;
                if (userMessage != null)
                {
                    await HandleUserMessage(userMessage);
                }
                //might add other messages later..
            }
            catch (BotGeneraicException e)
            {
                _logger.Log(e.Message);
            }
        }

        private async Task HandleUserMessage(SocketUserMessage userMessage)
        {
             // Create a number to track where the prefix ends and the command begins
            var prefix = _config["command_prefix"].ToCharArray()[0];
            int argPos = 0;
            // Determine if the message is a command, based on if it starts with 'prefix' or a mention prefix
            if (!(userMessage.HasCharPrefix(prefix, ref argPos) || userMessage.HasMentionPrefix(_client.CurrentUser, ref argPos))) 
            {
                return;
            }
            //Create a Command Context
            CommandContext context = new CommandContext(_client, userMessage);
            // Execute the command. (result does not indicate a return value, 
            // rather an object stating if the command executed succesfully)
            var result = await _commands.ExecuteAsync(context, argPos, _provider);
            if (!result.IsSuccess)
            {
                //command execute failed, throw an exception
                throw new BotGeneraicException(result.ErrorReason);
            }                
        }        
    }    
}