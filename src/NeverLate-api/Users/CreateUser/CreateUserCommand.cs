using MediatR;
using NeverLate_api.Mediator;

namespace NeverLate_api.Users.CreateUser;

public record CreateUserCommand(string Email, string Password) : IRequest<Result<Unit, CreateUserErrorReasonEnum>>;