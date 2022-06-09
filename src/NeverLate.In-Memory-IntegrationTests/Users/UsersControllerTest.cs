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
        var createUserRequest = new CreateUserRequest("zzzz@test.pl", "testtest123");
        var response = await Client.PostAsJsonAsync("", createUserRequest);

        response.Should().HaveStatusCode(HttpStatusCode.OK);
    }
}