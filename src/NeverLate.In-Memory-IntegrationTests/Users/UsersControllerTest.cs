using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using NeverLate_api.Users.CreateUser;
using NUnit.Framework;

namespace NeverLate.IntegrationTests.Users;

public class UsersControllerTest : BaseTransactionTest
{
    [Test]
    public async Task CreateUser_ReturnsSuccessResponse_ForValidRequestModel()
    {
        Client.BaseAddress = new Uri("http://localhost/users");
        var createUserRequest = new CreateUserRequest("testuser@test.pl", "fooPassword123");
        var response = await Client.PostAsJsonAsync("", createUserRequest);

        response.Should().HaveStatusCode(HttpStatusCode.OK);
    }

    [Test]
    public async Task CreateUser_ReturnsBadRequest_IfUserWithSameEmailFound()
    {
        Client.BaseAddress = new Uri("http://localhost/users");
        var createUserRequest = new CreateUserRequest("testuser@test.pl", "fooPassword123");
        await Client.PostAsJsonAsync("", createUserRequest);

        var response = await Client.PostAsJsonAsync("", createUserRequest);

        response.Should().HaveStatusCode(HttpStatusCode.BadRequest);
    }
}