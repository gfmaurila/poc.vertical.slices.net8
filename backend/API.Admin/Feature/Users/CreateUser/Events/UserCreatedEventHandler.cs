using API.Admin.Domain.User.Events;
using API.Admin.Feature.Users.GetUser;
using API.Admin.Infrastructure.Database.Repositories.Interfaces;
using MediatR;

namespace API.Admin.Feature.Users.CreateUser.Events;


public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly ILogger<UserCreatedEventHandler> _logger;
    private readonly IUserRepository _repo;
    public UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger,
                                   IUserRepository repo)
    {
        _logger = logger;
        _repo = repo;
    }

    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        const string cacheKey = nameof(GetUserQuery);
    }
}