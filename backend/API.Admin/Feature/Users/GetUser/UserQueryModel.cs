using API.Admin.Domain;
using Common.Net8.Enumerado;

namespace API.Admin.Feature.Users.GetUser;

public class UserQueryModel : BaseQueryModel
{
    public string FirstName { get; init; }

    public string LastName { get; init; }

    public DateTime DateOfBirth { get; init; }

    public string Email { get; init; }
    public string Phone { get; init; }
}
