using AutomationService.Application.Contracts;
using AutomationService.Domain.Entities;

namespace AutomationService.Application.Common.Services.Interfaces;

public interface IDeviceServiceClient
{
    Task<DeviceServiceResult> ExecuteActionAsync(Routine routine, string accessToken);
}
