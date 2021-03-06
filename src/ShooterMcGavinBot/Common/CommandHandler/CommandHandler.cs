using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace ShooterMcGavinBot.Common
{
    public class CommandHandler : ICommandHandler
    {       
        //private members
        protected IServiceProvider _provider;
        protected IConfiguration _config;
        protected Assembly _entryAssembly;
        protected DiscordSocketClient _client;
        protected CommandService _commands;
        protected ILogger _logger;
        private CancellationTokenSource _cancelTokenSoruce;

        //constructors
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
            //create a cancel token to be used later
            _cancelTokenSoruce = new CancellationTokenSource();
        }

        //public functions
        public async Task Start() 
        {        
            //command events
            HookEvents();
            //command modules
            await AddCommandModules();
            //login with token
            string token = _config["discordbot_token"];
            await _client.LoginAsync(TokenType.Bot, token);
            //set game status
            string prefix = _config["command_prefix"];
            await _client.SetGameAsync($"{prefix}help");
            //start client
            await _client.StartAsync();        
        }

        public async Task WaitForStop()
        {
            //hook onto cancel event
            Console.CancelKeyPress += new ConsoleCancelEventHandler(Stop);
            try 
            {    //wait forever until canceled
                await Task.Delay(-1, _cancelTokenSoruce.Token);     
            }
            catch (TaskCanceledException)
            {
                return;
            }
            return;                  
        }        
        
        //private functions
        private void Stop(Object sender, ConsoleCancelEventArgs args)
        {
            //stop the client
            _client.StopAsync().GetAwaiter().GetResult();
            //logout the client
            _client.LogoutAsync().GetAwaiter().GetResult();           
            //cancel the waitforever task
            _cancelTokenSoruce.Cancel();
            //trap the cancel event
            args.Cancel = true;
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
            char prefix = _config["command_prefix"].ToCharArray()[0];
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
            IResult result = await _commands.ExecuteAsync(context, argPos, _provider);
            if (!result.IsSuccess)
            {
                //command execute failed, throw an exception
                throw new BotGeneraicException(result.ErrorReason);
            }                
        }      
    }    
}