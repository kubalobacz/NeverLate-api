using System.Transactions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NeverLate_api.Mediator;
using NeverLate_api.Persistence.Database;

namespace NeverLate_api.Users.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Unit, CreateUserErrorReasonEnum>>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly NeverLateContext _context;

    public CreateUserCommandHandler(UserManager<IdentityUser> userManager, NeverLateContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    
    public async Task<Result<Unit, CreateUserErrorReasonEnum>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        //using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var userExists = await _userManager.FindByEmailAsync(request.Email);
        if (userExists is not null)
        {
            return Result<Unit, CreateUserErrorReasonEnum>.Failure(CreateUserErrorReasonEnum.UserWithSameEmailFound);
        }

        var identityUser = new IdentityUser
        {
            Email = request.Email,
            UserName = request.Email
        };
        var createResult = await _userManager.CreateAsync(identityUser, request.Password);
        if (!createResult.Succeeded)
        {
            return Result<Unit, CreateUserErrorReasonEnum>.Failure(CreateUserErrorReasonEnum.Other);
        }
        //transaction.Complete();
        return Result<Unit, CreateUserErrorReasonEnum>.Success(Unit.Value);
    }
}