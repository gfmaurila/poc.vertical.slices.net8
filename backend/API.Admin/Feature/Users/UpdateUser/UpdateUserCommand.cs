using Common.Net8.Enumerado;
using Common.Net8.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace API.Admin.Feature.Users.UpdateUser;

public class UpdateUserCommand : IRequest<ApiResult<UpdateUserResponse>>
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    [DataType(DataType.Text)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    [DataType(DataType.Text)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(20)]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
}
