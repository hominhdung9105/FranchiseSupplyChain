using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using UserProfile.Application.Interfaces;
using UserProfile.Application.Services;
using UserProfile.Infrastructure.Repositories;
using UserProfile.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add DbContext
builder.Services.AddDbContext<UserProfileDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserProfileDatabase")));

builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
