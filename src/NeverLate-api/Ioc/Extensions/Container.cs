using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace NeverLate_api.Ioc.Extensions;

public static class Container
{
    public static void RegisterServices(this ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterMediatR(typeof(Program).Assembly);
        containerBuilder.RegisterIdentity();
    }
}