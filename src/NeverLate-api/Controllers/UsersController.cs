using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeverLate_api.Mediator;
using NeverLate_api.Users.CreateUser;

namespace NeverLate_api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserRequest request)
    {
        var result = await _mediator.Send(new CreateUserCommand(request.Email, request.Password));

        return result switch
        {
            {ErrorReason: CreateUserErrorReasonEnum.UserWithSameEmailFound} => BadRequest(),
            {IsSuccess: false} => StatusCode(500, "Creating user failed."),
            _ => Ok("User created.")
        };
    }
}