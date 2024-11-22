namespace API.Admin.Domain.User.Events.Auth;

public class AuthResetEvent : UserBaseEvent
{
    public AuthResetEvent(Guid id, string firstName, string lastName, string email, string phone, string password, DateTime dateOfBirth) :
                       base(id, firstName, lastName, email, phone, password, dateOfBirth)
    {
    }
}
