using System;
using System.Threading.Tasks;
using Discord.Commands;

public interface IBotService 
{
    Task help(ICommandContext commandContext, Type type);
}