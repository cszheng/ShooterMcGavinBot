using System.Collections.Generic;

namespace ShooterMcGavinBot.Common
{
    public interface IBotStringsContainer
    {
        Dictionary<string, BotStrings> Containers { get; }
        string getString(string containerKey, string stringKey);
        BotStrings getContainer(string containerKey);
    }
}