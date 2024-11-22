using API.Admin.Feature.Users.GetUser;
using Common.Net8.Response;
using MediatR;

namespace API.Admin.Feature.Users.GetUserById;


public class GetUserByIdQuery : IRequest<ApiResult<UserQueryModel>>
{
    public GetUserByIdQuery(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; private set; }
}