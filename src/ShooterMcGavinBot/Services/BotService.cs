using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Discord;
using Discord.Commands;
using ShooterMcGavinBot.Common;

namespace ShooterMcGavinBot.Services 
{
    public class BotService: IBotService
    {   
        protected IBotStringsContainer _botStrings;    

        public BotService(IBotStringsContainer botStrings)
        {
            _botStrings = botStrings;
        }
        
        protected MethodInfo[] GetCommandMethods(Type typeObj)
        {
            //get methods with the Command attribute
            var mthdLst = typeObj.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                                 .Where((t) => { 
                                     var cmdAttrib = t.GetCustomAttribute(typeof(CommandAttribute)) as CommandAttribute;
                                     return  cmdAttrib != null && 
                                             cmdAttrib.Text != null && 
                                             cmdAttrib.Text != "";
                                 })
                                 .ToArray();
            return mthdLst;
        }

        protected string BuildParameterDescription(System.Reflection.ParameterInfo parameterInfo)
        {
            //get parameter name and attribute
            var paramName = parameterInfo.Name;
            var summAttrib = parameterInfo.GetCustomAttribute(typeof(SummaryAttribute)) as SummaryAttribute;
            //get parameter template
            var paramTmpl = _botStrings.getString("response", "common_parameter_description");
            return String.Format(paramTmpl, paramName, summAttrib.Text);
        }

        protected string BuildMethodDescription(MethodInfo methodInfo)
        {
            //get the method level attributes
            var cmnAttrib = methodInfo.GetCustomAttribute(typeof(CommandAttribute)) as CommandAttribute;
            var sumAttrib = methodInfo.GetCustomAttribute(typeof(SummaryAttribute)) as SummaryAttribute;
            //get method template
            var cmdDescTmpl = _botStrings.getString("response", "common_method_description");
            return String.Format(cmdDescTmpl, cmnAttrib.Text, sumAttrib.Text);
        }
        
        protected string BuildCommandDescription(TypeInfo typeInfo)
        {
            //get the class level attributes
            var grpAttrib = typeInfo.GetCustomAttribute(typeof(GroupAttribute)) as GroupAttribute;
            var sumAttrib = typeInfo.GetCustomAttribute(typeof(SummaryAttribute)) as SummaryAttribute;
            //get description template
            var cmdDescTmpl = _botStrings.getString("response", "common_command_description");
            //build string with tempate
            return String.Format(cmdDescTmpl, grpAttrib.Prefix, sumAttrib.Text);
        }

        public virtual Embed help(Type typeObj)
        {
            var sectBreaks = _botStrings.getString("response", "common_section_breaks");
            var responseBuilder = new StringBuilder();
            //build response with tempates
            // responseBuilder.Append(BuildCommandDescription(typeObj.GetTypeInfo()));
            // responseBuilder.AppendLine();
            // responseBuilder.Append((sectBreaks));
            // responseBuilder.AppendLine();
            //get methods
            var methods = GetCommandMethods(typeObj);
            foreach (var method in methods)
            {
                //build method description
                responseBuilder.Append(BuildMethodDescription(method));
                responseBuilder.AppendLine();
                //get parameters
                var parameters = method.GetParameters();
                foreach (var parameter in parameters)
                {
                     //build parameter description
                     responseBuilder.Append(BuildParameterDescription(parameter));
                     responseBuilder.AppendLine();
                }                
                responseBuilder.Append((sectBreaks));
                responseBuilder.AppendLine();
            }            
            var embeded = new EmbedBuilder().WithDescription(responseBuilder.ToString());
            return embeded;
        }
    }
}