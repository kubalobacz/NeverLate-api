using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.Storage;
using NeverLate_api.Persistence.Database;
using NUnit.Framework;

namespace NeverLate.IntegrationTests;

[TestFixture]
public abstract class BaseTransactionTest
{
    private WebApplicationFactory<Program> _webApplicationFactory;
    private NeverLateContext _neverLateContext;
    private IDbContextTransaction _transaction;

    protected HttpClient Client;
    
    [SetUp]
    public virtual void SetUp()
    {
        _webApplicationFactory = new WebApplicationFactory<Program>();
        _neverLateContext = (NeverLateContext) _webApplicationFactory.Services.GetService(typeof(NeverLateContext))!;
        _transaction = _neverLateContext.Database.BeginTransaction();
        Client = _webApplicationFactory.CreateClient();
    }

    [TearDown]
    public virtual async Task TearDown()
    {
        await _transaction.RollbackAsync();
        await _transaction.DisposeAsync();
    }
}