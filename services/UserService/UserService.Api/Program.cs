using EventBus.Api;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.Application.SeedWork;
using UserService.Domain.Users;
using UserService.Infra.Data;
using UserService.Infra.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<EventEnvelopeDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEventEnvelopeRepository, EventEnvelopeRepository>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// Behaviors.
var assembly = AppDomain.CurrentDomain.Load("UserService.Application");
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FailFastBehavior<,>));
AssemblyScanner .FindValidatorsInAssembly(assembly).ForEach(result => builder.Services.AddScoped(result.InterfaceType, result.ValidatorType));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));


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



