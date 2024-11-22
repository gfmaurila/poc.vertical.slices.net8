using Common.Net8;

namespace API.Admin.Feature.Users.UpdateEmail;

public class UpdateEmailUserResponse : BaseResponse
{
    public UpdateEmailUserResponse(Guid id) => Id = id;
    public Guid Id { get; }
}
