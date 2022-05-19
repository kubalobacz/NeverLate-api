using MediatR;
using Microsoft.AspNetCore.Identity;
using NeverLate_api.Mediator;

namespace NeverLate_api.Users.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Unit, CreateUserErrorReasonEnum>>
{
    private readonly UserManager<IdentityUser> _userManager;

    public CreateUserCommandHandler(UserManager<IdentityUser> userManager )
    {
        _userManager = userManager;
    }
    
    public async Task<Result<Unit, CreateUserErrorReasonEnum>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _userManager.FindByEmailAsync(request.Email);  
    }
}