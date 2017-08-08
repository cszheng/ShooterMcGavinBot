using System;
using System.Reflection;
using Discord;

public interface IBotService 
{
    Embed help(Type type);
}