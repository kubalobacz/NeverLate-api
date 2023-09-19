using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NeverLate_api.Mediator;
using NeverLate_api.Users;
using NeverLate_api.Users.CreateUser;
using NUnit.Framework;

namespace NeverLate.UnitTests.Users;

[TestFixture]
public class UsersControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private UsersController _usersController;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _usersController = new UsersController(_mediatorMock.Object);
    }

    [Test]
    public async Task CreateUser_ReturnsBadRequest_WhenUserWithSameEmailFound()
    {
        var createUserRequest = new CreateUserRequest("foo@test.com", "zaq1@WSX");
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<Unit, CreateUserErrorReasonEnum>.Failure(CreateUserErrorReasonEnum.UserWithSameEmailFound));
        
        var result = await _usersController.CreateUser();

        result.Should().BeOfType<BadRequestResult>();
    }
}