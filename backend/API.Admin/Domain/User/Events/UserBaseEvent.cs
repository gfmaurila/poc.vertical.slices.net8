using Common.Net8.Events;

namespace API.Admin.Domain.User.Events;


public abstract class UserBaseEvent : Event
{
    protected UserBaseEvent(Guid id, string firstName, string lastName, string email, string phone, string password, DateTime dateOfBirth)
    {
        Id = id;
        AggregateId = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Password = password;
        DateOfBirth = dateOfBirth;
    }

    public Guid Id { get; private init; }
    public string FirstName { get; private init; }

    public string LastName { get; private init; }

    public DateTime DateOfBirth { get; private init; }

    public string Email { get; private set; }
    public string Phone { get; private set; }

    public string Password { get; private set; }
}