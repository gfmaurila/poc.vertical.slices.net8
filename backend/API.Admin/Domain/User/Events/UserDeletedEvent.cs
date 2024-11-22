namespace API.Admin.Domain.User.Events;

/// <summary>
/// Evento que representa uma atualização de um entidade.
/// </summary>
public class UserDeletedEvent : UserBaseEvent
{
    public UserDeletedEvent(Guid id, string firstName, string lastName, string email, string phone, string password, DateTime dateOfBirth) :
        base(id, firstName, lastName, email, phone, password, dateOfBirth)
    {
    }
}
