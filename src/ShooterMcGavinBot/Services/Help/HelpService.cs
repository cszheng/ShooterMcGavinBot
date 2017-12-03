using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Discord;
using Discord.Commands;
using ShooterMcGavinBot.Common;
using ShooterMcGavinBot.Modules;

namespace ShooterMcGavinBot.Services 
{
    public class HelpService: BotService, IHelpService
    {   
        protected Assembly _assembly;        

        //constructor
        public HelpService(Assembly assembly, 
                           IBotStringsContainer botStrings) 
        : base(botStrings)
        {   
            _assembly = assembly;
        }
        
        //public functions
        public override Embed help(Type type)
        {
            //filter type by namespace
            var typeList = _assembly.ExportedTypes
                                    .Where(t => t.Namespace == type.Namespace && 
                                                t.IsSubclassOf(typeof(ModuleBase)))
                                    .ToArray();
            if (typeList.Length == 0)
            {
                throw new BotGeneraicException("No command modules in assembly or namespace");
            }                                    
            var responseBuilder = new StringBuilder();
            if(typeList.Length > 0) 
            {
                var commandHeader = _botStrings.getString("common", "command_header");
                responseBuilder.Append((commandHeader));
                responseBuilder.AppendLine();
            }
            foreach (var typeObj in typeList)
            {   
                //build command description
                responseBuilder.Append(BuildCommandDescription(typeObj.GetTypeInfo()));
                responseBuilder.AppendLine();
            }
            //create the embed
            var embeded = new EmbedBuilder().WithDescription(responseBuilder.ToString());
            return embeded;
        }
    }
}