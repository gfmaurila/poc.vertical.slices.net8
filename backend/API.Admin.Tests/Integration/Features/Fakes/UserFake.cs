using API.Admin.Domain.User;
using API.Admin.Feature.Users.CreateUser;
using API.Admin.Feature.Users.UpdateEmail;
using API.Admin.Feature.Users.UpdatePassword;
using API.Admin.Feature.Users.UpdateUser;
using API.Admin.Infrastructure.Database;
using API.Admin.Tests.Integration.Utilities;
using Bogus;
using Common.Net8.Enumerado;
using Common.Net8.Extensions;
using Common.Net8.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace API.Admin.Tests.Integration.Features.Fakes;

public static class UserFake
{
    public static async Task<Guid> GetUserById(DatabaseSQLServerFixture _fixture)
    {
        using var scope = _fixture.Factory().Services.CreateScope();
        var provider = scope.ServiceProvider;
        using var dbContext = provider.GetRequiredService<EFSqlServerContext>();
        await dbContext.Database.EnsureCreatedAsync();
        var user = await dbContext.User.AddAsync(CreateUser("testedelete@teste.com.br", "Test123$"));
        await dbContext.SaveChangesAsync();
        return user.Entity.Id;
    }

    public static async Task CreateUserAuth(DatabaseSQLServerFixture _fixture, UserEntity entity)
    {
        using var scope = _fixture.Factory().Services.CreateScope();
        var provider = scope.ServiceProvider;
        using var dbContext = provider.GetRequiredService<EFSqlServerContext>();
        await dbContext.Database.EnsureCreatedAsync();
        var user = await dbContext.User.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public static async Task Delete(DatabaseSQLServerFixture _fixture, HttpClient _client, Guid id)
    {
        var token = await _fixture.GetAuthAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
        var response = await _client.DeleteAsync($"/api/v1/user/{id}");
        response.EnsureSuccessStatusCode();
    }

    public static UserEntity CreateUser(string email, string password)
    {
        var faker = new Faker("pt_BR");

        var phone = new PhoneNumber(faker.Person.Phone);

        var newUser = new UserEntity(
            faker.Person.FirstName,
            faker.Person.LastName,
            new Email(email),
            phone,
            Password.ComputeSha256Hash(password),
            new DateTime(1990, 1, 1));

        return newUser;
    }

    public static UpdateEmailUserCommand UpdateEmailUserInvalidCommand(Guid id)
    {
        var faker = new Faker("pt_BR");

        var command = new UpdateEmailUserCommand()
        {
            Id = id,
            Email = "teste"
        };
        return command;
    }

    public static UpdateEmailUserCommand UpdateEmailUserCommand(Guid id)
    {
        var faker = new Faker("pt_BR");

        var command = new UpdateEmailUserCommand()
        {
            Id = id,
            Email = faker.Person.Email
        };
        return command;
    }


    public static UpdatePasswordUserCommand UpdatePasswordUserInvalidCommand(Guid id)
    {
        var faker = new Faker("pt_BR");

        var command = new UpdatePasswordUserCommand()
        {
            Id = id,
            Password = "@G18u03i198",
            ConfirmPassword = "@G18u03i1986"
        };
        return command;
    }

    public static UpdatePasswordUserCommand UpdatePasswordUserCommand(Guid id)
    {
        var faker = new Faker("pt_BR");

        var command = new UpdatePasswordUserCommand()
        {
            Id = id,
            Password = "@G18u03i1986",
            ConfirmPassword = "@G18u03i1986"
        };
        return command;
    }

    public static UpdateUserCommand UpdateUserInvalidCommand(Guid id)
    {
        var faker = new Faker("pt_BR");

        var command = new UpdateUserCommand()
        {
            Id = id,
            FirstName = "",
            LastName = "",
            DateOfBirth = new DateTime(1990, 1, 1),
            Phone = ""
        };
        return command;
    }

    public static UpdateUserCommand UpdateUserCommand(Guid id)
    {
        var faker = new Faker("pt_BR");

        var command = new UpdateUserCommand()
        {
            Id = id,
            FirstName = faker.Person.FirstName,
            LastName = faker.Person.LastName,
            DateOfBirth = new DateTime(1990, 1, 1),
            Phone = faker.Person.Phone
        };
        return command;
    }

    public static CreateUserCommand CreateUserCommand()
    {
        var faker = new Faker("pt_BR");

        var command = new CreateUserCommand()
        {
            FirstName = faker.Person.FirstName,
            LastName = faker.Person.LastName,
            Password = "@G21r03a1985",
            ConfirmPassword = "@G21r03a1985",
            DateOfBirth = new DateTime(1990, 1, 1),
            Email = faker.Person.Email,
            Phone = faker.Person.Phone
        };
        return command;
    }

    public static CreateUserCommand CreateUserInvalidCommand()
    {
        var faker = new Faker("pt_BR");

        var command = new CreateUserCommand()
        {
            FirstName = "",
            LastName = "",
            Password = "",
            ConfirmPassword = "",
            DateOfBirth = new DateTime(1990, 1, 1),
            Email = "",
            Phone = ""
        };
        return command;
    }

    public static CreateUserCommand CreateUserExistingDataCommand()
    {
        var faker = new Faker("pt_BR");

        var command = new CreateUserCommand()
        {
            FirstName = faker.Person.FirstName,
            LastName = faker.Person.LastName,
            Password = "@G21r03a1985",
            ConfirmPassword = "@G21r03a1985",
            DateOfBirth = new DateTime(1990, 1, 1),
            Email = "emailteste-8@teste.com.br",
            Phone = faker.Person.Phone
        };
        return command;
    }
}
