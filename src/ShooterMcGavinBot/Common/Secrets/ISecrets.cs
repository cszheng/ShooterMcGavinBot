namespace ShooterMcGavinBot.Common
{
    public interface ISecrets
    {
        string GetSecret(string secretKey);
    }
}