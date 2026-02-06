using IAM.Application.AutoMapper;
using IAM.Application.Interfaces.Repositories;
using IAM.Application.Interfaces.Security;
using IAM.Application.Interfaces.Services;
using IAM.Application.Services;
using IAM.Infrastructure.Options;
using IAM.Infrastructure.Persistence;
using IAM.Infrastructure.Repositories;
using IAM.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//AutoMapper Configuration
builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperProfile).Assembly);

builder.Services.AddDbContext<IAMDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IAMDatabase")));
//builder.Services.AddAuthentication(option )
builder.Services.AddAuthentication(options =>
           {
               options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
           })
           .AddJwtBearer(options =>
           {
               options.RequireHttpsMetadata = true;
               options.SaveToken = true;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidIssuer = builder.Configuration.GetValue<string>("JwtConfig:Issuer"),
                   ValidateAudience = true,
                   ValidAudience = builder.Configuration.GetValue<string>("JwtConfig:Audience"),
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtConfig:SecretKey")!))
               };
           });
builder.Services.AddAuthorization();
// Configure JwtOptions
builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("JwtConfig")
);

// Dependency Injection for Application Services
builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
