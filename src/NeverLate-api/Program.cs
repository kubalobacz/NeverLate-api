using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NeverLate_api.Authentication;
using NeverLate_api.Ioc.Extensions;
using NeverLate_api.Persistence.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterServices();
});

builder.Services.AddControllers()
    .AddFluentValidation(configuration =>
    {
        configuration.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NeverLateContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("NeverLateDB");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddIdentityCore<IdentityUser>(options =>
{
    options.Password.RequireDigit = PasswordRulesProvider.RequireDigit;
    options.Password.RequiredLength = PasswordRulesProvider.RequiredLength;
    options.Password.RequireLowercase = PasswordRulesProvider.RequireLowercase;
    options.Password.RequireUppercase = PasswordRulesProvider.RequireUppercase;
    options.Password.RequireNonAlphanumeric = PasswordRulesProvider.RequireNonAlphanumeric;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();