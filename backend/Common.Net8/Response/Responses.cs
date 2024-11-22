namespace Common.Net8.Response;

public static class Responses
{
    public static ResponseDefault ApplicationErrorMessage()
    {
        return new ResponseDefault
        {
            Message = "Ocorreu algum erro interno na aplicação, por favor tente novamente.",
            Success = false,
            Data = null
        };
    }

    public static ResponseDefault DomainErrorMessage(string message)
    {
        return new ResponseDefault
        {
            Message = message,
            Success = false,
            Data = null
        };
    }

    public static ResponseDefault DomainErrorMessage(string message, IReadOnlyCollection<string> errors)
    {
        return new ResponseDefault
        {
            Message = message,
            Success = false,
            Data = errors
        };
    }

    public static ResponseDefault UnauthorizedErrorMessage()
    {
        return new ResponseDefault
        {
            Message = "A combinação de login e senha está incorreta!",
            Success = false,
            Data = null
        };
    }
}
