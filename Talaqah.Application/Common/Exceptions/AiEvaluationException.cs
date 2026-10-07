namespace Talaqah.Application.Common.Exceptions;

public class AiEvaluationException : Exception
{
    public AiEvaluationException(string message) : base(message) { }

    public AiEvaluationException(string message, Exception innerException) : base(message, innerException) { }
}
