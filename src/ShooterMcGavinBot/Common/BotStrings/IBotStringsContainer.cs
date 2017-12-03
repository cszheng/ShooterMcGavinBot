using System.Collections.Generic;

namespace ShooterMcGavinBot.Common
{
    public interface IBotStringsContainer
    {
        Dictionary<string, IBotStrings> Containers { get; }
        string getString(string containerKey, string stringKey);
        IBotStrings getContainer(string containerKey);
    }
}