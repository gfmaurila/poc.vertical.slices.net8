using Common.Net8.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace API.Admin.Feature.Users.UpdatePassword;

public class UpdatePasswordUserCommand : IRequest<ApiResult<UpdatePasswordUserResponse>>
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}
