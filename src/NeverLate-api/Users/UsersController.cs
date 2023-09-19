using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeverLate_api.Users.CreateUser;

namespace NeverLate_api.Users;

[ApiController]
[Authorize]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUser()
    {
        var result = await _mediator.Send(new CreateUserCommand());

        return result switch
        {
            {IsSuccess: false, ErrorReason: CreateUserErrorReasonEnum.UserWithSameEmailFound} => BadRequest(),
            {IsSuccess: false} => StatusCode(500, "Creating user failed."),
            _ => Ok("User created.")
        };
    }
}