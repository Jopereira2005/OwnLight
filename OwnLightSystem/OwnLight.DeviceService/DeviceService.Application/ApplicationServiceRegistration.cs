using System.Reflection;
using DeviceService.Application.Common.Services;
using DeviceService.Application.Common.Services.Interfaces;
using DeviceService.Application.Common.Validations.Device;
using DeviceService.Application.Common.Validations.DeviceAction;
using DeviceService.Application.Common.Validations.DeviceType;
using DeviceService.Application.Features.Device.Commands;
using DeviceService.Application.Features.DeviceAction.Commands;
using DeviceService.Application.Features.DeviceType.Commands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Service = DeviceService.Application.Common.Services;

namespace DeviceService.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddScoped<IDeviceActionService, DeviceActionService>();
        services.AddScoped<IDeviceService, Service.DeviceService>();


        // Validators for Device Entity
        services.AddTransient<IValidator<CreateDeviceCommand>, CreateDeviceValidator>();
        services.AddTransient<IValidator<UpdateDeviceCommand>, UpdateDeviceValidator>();
        services.AddTransient<IValidator<UpdateDeviceRoomCommand>, UpdateDeviceRoomValidator>();
        services.AddTransient<IValidator<UpdateDeviceGroupCommand>, UpdateDeviceGroupValidator>();

        // Validators for DeviceAction Entity
        services.AddTransient<IValidator<ControlAllUserDevicesCommand>, ControlAllUserDevicesValidator>();
        services.AddTransient<IValidator<ControlDeviceCommand>, ControlDeviceValidator>();
        services.AddTransient<IValidator<ControlGroupCommand>, ControlGroupValidator>();
        services.AddTransient<IValidator<ControlRoomCommand>, ControlRoomValidator>();
        services.AddTransient<IValidator<DimmerizeDeviceCommand>, DimmerizeDeviceValidator>();
        services.AddTransient<IValidator<DimmerizeGroupCommand>, DimmerizeGroupValidator>();
        services.AddTransient<IValidator<DimmerizeRoomCommand>, DimmerizeRoomValidator>();

        // Validators for DeviceType Entity
        services.AddTransient<IValidator<CreateDeviceTypeCommand>, CreateDeviceTypeValidator>();
        services.AddTransient<IValidator<UpdateDeviceTypeCommand>, UpdateDeviceTypeValidator>();

        return services;
    }
}
