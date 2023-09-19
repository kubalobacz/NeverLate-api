using MediatR;
using NeverLate_api.Mediator;

namespace NeverLate_api.Users.CreateUser;

public record CreateUserCommand : IRequest<Result<Unit, CreateUserErrorReasonEnum>>;