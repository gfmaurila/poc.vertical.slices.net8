using API.Admin.Domain.User.Events;
using API.Admin.Domain.User.Events.Auth;
using API.Admin.Feature.Users.UpdateUser;
using Common.Net8;
using Common.Net8.Abstractions;
using Common.Net8.ValueObjects;

namespace API.Admin.Domain.User;

public class UserEntity : BaseEntity, IAggregateRoot
{
    public UserEntity(string firstName, string lastName, Email email, PhoneNumber phone, string password, DateTime dateOfBirth)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Password = password;
        DateOfBirth = dateOfBirth;

        AddDomainEvent(new UserCreatedEvent(Id, firstName, lastName, email.Address, phone.Phone, password, dateOfBirth));
    }

    public UserEntity() { } // ORM

    /// <summary>
    /// Primeiro Nome.
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// Sobrenome.
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// Data de Nascimento.
    /// </summary>
    public DateTime DateOfBirth { get; private set; }

    /// <summary>
    /// Endereço de e-mail.
    /// </summary>
    public Email Email { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public PhoneNumber Phone { get; private set; }

    /// <summary>
    /// Senha de acesso
    /// </summary>
    public string Password { get; private set; }

    public void ChangeEmail(Email newEmail)
    {
        if (!Email.Equals(newEmail))
        {
            Email = newEmail;
            AddDomainEvent(new UserUpdatedEvent(Id, FirstName, LastName, newEmail.Address, Phone.Phone, Password, DateOfBirth));
        }
    }

    /// <summary>
    /// Altera registros
    /// </summary>
    /// <param name="dto"></param>
    public void Update(UpdateUserCommand dto)
    {
        FirstName = dto.FirstName;
        LastName = dto.LastName;
        Phone = new PhoneNumber(dto.Phone);
        DateOfBirth = dto.DateOfBirth;
        AddDomainEvent(new UserUpdatedEvent(Id, dto.FirstName, dto.LastName, Email.Address, dto.Phone, Password, dto.DateOfBirth));
    }

    /// <summary>
    /// Altera o Senha.
    /// </summary>
    /// <param name="password"></param>
    public void ChangePassword(string password)
    {
        // Só será alterado o e-mail se for diferente do existente.
        if (!Password.Equals(password))
        {
            Password = password;
            // Adicionando a alteração nos eventos de domínio.
            AddDomainEvent(new UserUpdatedEvent(Id, FirstName, LastName, Email.Address, Phone.Phone, Password, DateOfBirth));
        }
    }

    /// <summary>
    /// Adiciona o evento de entidade deletada.
    /// </summary>
    public void Delete()
        => AddDomainEvent(new UserDeletedEvent(Id, FirstName, LastName, Email.Address, Phone.Phone, Password, DateOfBirth));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="lastName"></param>
    /// <param name="email"></param>
    /// <param name="token"></param>
    public void AuthEvent(string id, string lastName, string email, string token)
        => AddDomainEvent(new AuthEvent(id, lastName, email, token, DateTime.Now.AddHours(8)));

    /// <summary>
    /// 
    /// </summary>
    public void AuthResetEvent()
        => AddDomainEvent(new AuthResetEvent(Id, FirstName, LastName, Email.Address, Phone.Phone, Password, DateOfBirth));

}