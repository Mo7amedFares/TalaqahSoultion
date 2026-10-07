namespace Talaqah.Infrastructure.Ai.Providers;

public sealed class AiProviderException : Exception
{
    public string ProviderName { get; }

    public AiProviderException(string providerName, string message)
        : base(message)
    {
        ProviderName = providerName;
    }

    public AiProviderException(string providerName, string message, Exception innerException)
        : base(message, innerException)
    {
        ProviderName = providerName;
    }
}
