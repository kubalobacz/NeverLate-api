using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NeverLate_api.Authentication;
using NeverLate_api.DependencyInjection.Extensions;
using NeverLate_api.Persistence.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureAppConfiguration((builderContext, configurationBuilder) =>
{
    configurationBuilder.AddJsonFile("Configuration/identity-rules.json", false, true);
});

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterServicesWithAutofac();
});

builder.Services.RegisterConfigurations(builder);
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
builder.Services.AddOptions();

builder.Services.AddIdentityCore<IdentityUser>(options =>
{
    var passwordRulesProvider = builder.Configuration.GetSection("PasswordRules").Get<PasswordRulesProvider>();
    options.Password.RequireDigit = passwordRulesProvider.RequireDigit;
    options.Password.RequiredLength = passwordRulesProvider.RequiredLength;
    options.Password.RequireLowercase = passwordRulesProvider.RequireLowercase;
    options.Password.RequireUppercase = passwordRulesProvider.RequireUppercase;
    options.Password.RequireNonAlphanumeric = passwordRulesProvider.RequireNonAlphanumeric;
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

public partial class Program { }