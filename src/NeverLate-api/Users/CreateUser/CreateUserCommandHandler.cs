using MediatR;
using Microsoft.AspNetCore.Identity;
using NeverLate_api.Authentication.Token;
using NeverLate_api.Mediator;

namespace NeverLate_api.Users.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Unit, CreateUserErrorReasonEnum>>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IJwtDecoder _jwtDecoder;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateUserCommandHandler(UserManager<IdentityUser> userManager, IJwtDecoder jwtDecoder, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _jwtDecoder = jwtDecoder;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<Result<Unit, CreateUserErrorReasonEnum>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var tokenClaims = _jwtDecoder.DecodeToken(_httpContextAccessor.HttpContext!.Request);
            var emailClaim = tokenClaims!.First(c => c.Type == TokenClaimsKeys.EmailClaimKey);
            var userExists = await _userManager.FindByEmailAsync(emailClaim.Value);
            if (userExists is not null)
            {
                return Result<Unit, CreateUserErrorReasonEnum>.Failure(CreateUserErrorReasonEnum.UserWithSameEmailFound);
            }

            var identityUser = new IdentityUser
            {
                Email = emailClaim.Value,
                UserName = emailClaim.Value
            };
            var createResult = await _userManager.CreateAsync(identityUser);
            if (!createResult.Succeeded)
            {
                return Result<Unit, CreateUserErrorReasonEnum>.Failure(CreateUserErrorReasonEnum.Other);
            }
        
            return Result<Unit, CreateUserErrorReasonEnum>.Success(Unit.Value);
        }
        catch (Exception)
        {
            //TODO:Log error.
            return Result<Unit, CreateUserErrorReasonEnum>.Failure(CreateUserErrorReasonEnum.Other);
        }
    }
}