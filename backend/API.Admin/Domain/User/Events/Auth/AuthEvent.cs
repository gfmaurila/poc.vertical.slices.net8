using Common.Net8.Events;

namespace API.Admin.Domain.User.Events.Auth;

public class AuthEvent : Event
{
    public AuthEvent(string id, string nome, string email, string token, DateTime createdAt)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Token = token;
        CreatedAt = createdAt;
    }

    public string Id { get; private init; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Token { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

}
