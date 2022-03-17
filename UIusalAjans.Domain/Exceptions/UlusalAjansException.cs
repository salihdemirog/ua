namespace UIusalAjans.Domain.Exceptions;

public class UlusalAjansException : Exception
{
    public UlusalAjansException(string message) : base(message)
    {

    }

    public UlusalAjansException(string message, string detail) : base(message)
    {
        Detail = detail;
    }

    public string? Detail { get; }
}
