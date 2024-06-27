using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Json.Serialization;
using WAGym.AccessControl.DependencyModule;
using WAGym.Data.Data;
using WAGym.Domain.Extension;
using WAGym.Domain.Middleware;
using WAGym.Domain.Validation;

var builder = WebApplication.CreateBuilder(args);
string accessControlDb = builder.Configuration.GetValue<string>("AccessControlDb")!;

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Services.AddControllers()
    .AddFluentValidation(x =>
    {
        x.ValidatorOptions.LanguageManager.Culture = CultureInfo.GetCultureInfo("pt-BR");
        x.RegisterValidatorsFromAssemblyContaining<UserValidator>();
    })
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddDbContext<AppDbContext>(config => config.UseSqlServer(accessControlDb));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.AddTokenConfiguration();

builder.Services.AddSingleton<AppDbContext>();
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule<AccessControlDependencyModule>();
});
var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseMiddleware<BusinessExceptionMiddleware>();
app.UseMiddleware<ValidationExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseSwaggerConfig();

app.UseSession();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();