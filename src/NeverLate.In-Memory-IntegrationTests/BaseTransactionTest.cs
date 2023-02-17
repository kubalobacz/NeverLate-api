using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NeverLate_api.Persistence.Database;
using NUnit.Framework;

namespace NeverLate.IntegrationTests;

[TestFixture]
public abstract class BaseTransactionTest
{
    private WebApplicationFactory<Program> _webApplicationFactory = null!;
    private NeverLateContext _neverLateContext = null!;

    protected HttpClient Client = null!;

    [SetUp]
    public virtual void SetUp()
    {
        _webApplicationFactory = new CustomWebApplicationFactory<Program>();

        Client = _webApplicationFactory.CreateClient();
        _neverLateContext = (NeverLateContext) _webApplicationFactory.Services.GetService(typeof(NeverLateContext))!;
    }

    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureTestServices(services =>
            {
                var descriptor2 = services.SingleOrDefault(d => d.ServiceType == typeof(NeverLateContext));

                if (descriptor2 != null)
                {
                    services.Remove(descriptor2);
                }

                services.AddDbContext<NeverLateContext, TestNeverLateContext>();
            });
        }
    }

    [TearDown]
    public virtual Task TearDown()
    {
        return _neverLateContext.Database.EnsureDeletedAsync();
    }
}