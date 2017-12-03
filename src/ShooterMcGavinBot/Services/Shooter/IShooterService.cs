namespace ShooterMcGavinBot.Services 
{
    public interface IShooterService : IBotService
    {
        string roast(string userMention = null); 
    }
}