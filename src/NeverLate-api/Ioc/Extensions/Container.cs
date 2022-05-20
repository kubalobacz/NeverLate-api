using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NeverLate_api.Persistence.Database;

namespace NeverLate_api.Ioc.Extensions;

public static class Container
{
    public static void RegisterServices(this ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterMediatR(typeof(Program).Assembly);
        containerBuilder.RegisterType<NeverLateContext>().As<DbContext>().InstancePerLifetimeScope();
        containerBuilder.RegisterType<UserManager<IdentityUser>>().InstancePerLifetimeScope();
        containerBuilder.RegisterType<UserStore<IdentityUser>>().As<IUserStore<IdentityUser>>().InstancePerLifetimeScope();
    }
}