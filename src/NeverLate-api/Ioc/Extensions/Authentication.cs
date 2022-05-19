using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace NeverLate_api.Ioc.Extensions;

public static class Authentication
{
    public static void RegisterIdentity(this ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType<UserManager<IdentityUser>>()
            .As<IUserStore<IdentityUser>>()
            .InstancePerLifetimeScope();
    }
}