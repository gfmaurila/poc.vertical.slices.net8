﻿namespace API.Admin.Domain.User.Events;

/// <summary>
/// Evento que represente um nov1 entidade.
/// </summary>
public class UserCreatedEvent : UserBaseEvent
{
    public UserCreatedEvent(Guid id, string firstName, string lastName, string email, string phone, string password, DateTime dateOfBirth) :
        base(id, firstName, lastName, email, phone, password, dateOfBirth)
    {
    }
}