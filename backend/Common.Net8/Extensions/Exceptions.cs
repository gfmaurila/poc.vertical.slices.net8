namespace Common.Net8.Extensions;

public class Exceptions : Exception
{

    internal List<string> _errors;

    public IReadOnlyCollection<string> Errors => _errors;

    public Exceptions()
    { }

    public Exceptions(string message, List<string> errors) : base(message)
    {
        _errors = errors;
    }

    public Exceptions(string message) : base(message)
    { }

    public Exceptions(string message, Exception innerException) : base(message, innerException)
    { }
}
