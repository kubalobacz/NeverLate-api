using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NeverLate_api.Authentication;
using NeverLate_api.Persistence.Database;

namespace NeverLate_api.DependencyInjection.Extensions;

public static class Container
{
    public static void RegisterServicesWithAutofac(this ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterMediatR(typeof(Program).Assembly);
        containerBuilder.RegisterType<NeverLateContext>().As<DbContext>().InstancePerLifetimeScope();
        containerBuilder.RegisterType<UserManager<IdentityUser>>().InstancePerLifetimeScope();
        containerBuilder.RegisterType<UserStore<IdentityUser>>().As<IUserStore<IdentityUser>>().InstancePerLifetimeScope();
    }

    public static void RegisterConfigurations(this IServiceCollection serviceCollection, WebApplicationBuilder builder)
    {
        serviceCollection.Configure<PasswordRulesProvider>(builder.Configuration.GetSection("PasswordRules"));
    }
}