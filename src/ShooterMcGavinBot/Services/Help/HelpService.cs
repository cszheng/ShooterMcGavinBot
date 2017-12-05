using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Discord;
using Discord.Commands;
using ShooterMcGavinBot.Common;

namespace ShooterMcGavinBot.Services 
{
    public class HelpService: BotService, IHelpService
    {   
        //private members
        protected Assembly _assembly;        

        //constructors
        public HelpService(Assembly assembly, 
                           IBotStringsContainer botStrings) 
        : base(botStrings)
        {   
            _assembly = assembly;
        }
        
        //private functions
        protected override Embed BuildHelpEmbed(Type type)
        {
            //filter type by namespace
            Type[] typeList = _assembly.ExportedTypes
                                       .Where(t => t.Namespace == type.Namespace && 
                                                   t.IsSubclassOf(typeof(ModuleBase)))
                                       .ToArray();
            if (typeList.Length == 0)
            {
                throw new BotGeneraicException("No command modules in assembly or namespace");
            }                                    
            StringBuilder responseBuilder = new StringBuilder();
            if(typeList.Length > 0) 
            {
                string commandHeader = _botStrings.getString("common", "command_header");
                responseBuilder.Append((commandHeader));
                responseBuilder.AppendLine();
            }
            foreach (Type typeObj in typeList)
            {   
                //build command description
                responseBuilder.Append(BuildCommandDescription(typeObj.GetTypeInfo()));
                responseBuilder.AppendLine();
            }
            //create the embed
            Embed embeded = new EmbedBuilder().WithDescription(responseBuilder.ToString());
            return embeded;
        }
    }
}