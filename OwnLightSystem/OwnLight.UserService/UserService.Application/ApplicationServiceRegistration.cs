using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Common.Services.Auth;
using UserService.Application.Common.Services.Messages;
using UserService.Application.Common.Services.Token;
using UserService.Application.Common.Validation;
using UserService.Application.Features.User.Commands;
using UserService.Application.Features.User.Commands.Update;
using UserService.Domain.Entities;

namespace UserService.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
        services.AddSingleton(jwtSettings);

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddHttpContextAccessor();

        services.AddTransient<IValidator<CreateCommand>, CreateCommandValidator>();
        services.AddTransient<IValidator<UpdateCommand>, UpdateCommandValidator>();
        services.AddTransient<IValidator<UpdatePasswordCommand>, UpdatePasswordCommandValidator>();
        services.AddTransient<IValidator<UpdateEmailCommand>, UpdateEmailCommandValidator>();
        services.AddTransient<IValidator<UpdateUsernameCommand>, UpdateUsernameCommandValidator>();
        services.AddTransient<IValidator<UpdateProfilePictureCommand>, UpdateProfilePictureValidator>();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
