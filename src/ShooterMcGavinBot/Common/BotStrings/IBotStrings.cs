using System.Collections.Generic;

namespace ShooterMcGavinBot.Common
{
    public interface IBotStrings
    {
        Dictionary<string, string> Container { get; }
        string getString(string key);
    }
}