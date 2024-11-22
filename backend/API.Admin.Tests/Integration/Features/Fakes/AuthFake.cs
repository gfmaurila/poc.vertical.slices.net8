using API.Admin.Feature.Auth.Login;
using Bogus;

namespace API.Admin.Tests.Integration.Features.Fakes;
public static class AuthFake
{
    public static AuthCommand GetAuthAsync()
    {
        var faker = new Faker("pt_BR");

        var command = new AuthCommand()
        {
            Email = "auth@auth.com.br",
            Password = "Test123$"
        };
        return command;
    }

    public static AuthCommand AuthExistingCommand()
    {
        var faker = new Faker("pt_BR");

        var command = new AuthCommand()
        {
            Email = "emailteste-2@teste.com.br",
            Password = "Test123$"
        };
        return command;
    }

    public static AuthCommand AuthCommand()
    {
        var faker = new Faker("pt_BR");

        var command = new AuthCommand()
        {
            Email = "auth1@auth.com.br",
            Password = "1Test123$"
        };
        return command;
    }

    public static AuthCommand AuthInvalidCommand()
    {
        var faker = new Faker("pt_BR");

        var command = new AuthCommand()
        {
            Email = "testedeleteteste.com.br",
            Password = "Test123$"
        };
        return command;
    }
}
