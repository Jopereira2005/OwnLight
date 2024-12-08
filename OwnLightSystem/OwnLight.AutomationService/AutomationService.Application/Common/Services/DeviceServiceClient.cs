using System.Text;
using AutomationService.Application.Common.Services.Interfaces;
using AutomationService.Application.Contracts;
using AutomationService.Domain.Entities;
using AutomationService.Domain.Enums;

namespace AutomationService.Application.Common.Services;

public class DeviceServiceClient(HttpClient httpClient) : IDeviceServiceClient
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<DeviceServiceResult> ExecuteActionAsync(Routine routine, string accessToken)
    {
        if (string.IsNullOrEmpty(accessToken))
            throw new UnauthorizedAccessException("Missing JWT token.");

        var requestUri = GenerateUriForRoutineAction(routine);

        var content = routine.ActionType switch
        {
            RoutineActionType.TurnOn => new StringContent(
                "{\"status\": 0}",
                Encoding.UTF8,
                "application/json"
            ),
            RoutineActionType.TurnOff => new StringContent(
                "{\"status\": 1}",
                Encoding.UTF8,
                "application/json"
            ),
            RoutineActionType.Dim => new StringContent(
                $"{{\"brightness\": {routine.Brightness}}}",
                Encoding.UTF8,
                "application/json"
            ),
            _ => throw new InvalidOperationException("Invalid action type"),
        };

        var request = new HttpRequestMessage(HttpMethod.Post, requestUri) { Content = content };

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
            "Bearer",
            accessToken
        );

        var response = await _httpClient.SendAsync(request);

        return response.IsSuccessStatusCode
            ? new DeviceServiceResult { IsSuccess = true }
            : new DeviceServiceResult
            {
                IsSuccess = false,
                ErrorMessage = await response.Content.ReadAsStringAsync(),
            };
    }

    private static string GenerateUriForRoutineAction(Routine routine)
    {
        if (routine.ActionTarget == ActionTarget.Device)
        {
            return routine.ActionType switch
            {
                RoutineActionType.TurnOn => $"api/DeviceAction/control/{routine.TargetId}",
                RoutineActionType.TurnOff => $"api/DeviceAction/control/{routine.TargetId}",
                RoutineActionType.Dim => $"api/DeviceAction/dim/{routine.TargetId}",
                _ => throw new InvalidOperationException("Invalid action type for device"),
            };
        }
        else if (routine.ActionTarget == ActionTarget.Group)
        {
            return routine.ActionType switch
            {
                RoutineActionType.TurnOn => $"api/DeviceAction/control/group/{routine.TargetId}",
                RoutineActionType.TurnOff => $"api/DeviceAction/control//group/{routine.TargetId}",
                RoutineActionType.Dim => $"api/DeviceAction/dim/group/{routine.TargetId}",
                _ => throw new InvalidOperationException("Invalid action type for group"),
            };
        }
        else if (routine.ActionTarget == ActionTarget.Room)
        {
            return routine.ActionType switch
            {
                RoutineActionType.TurnOn => $"api/DeviceAction/control/room/{routine.TargetId}",
                RoutineActionType.TurnOff => $"api/DeviceAction/control/room/{routine.TargetId}",
                RoutineActionType.Dim => $"api/DeviceAction/dim/room/{routine.TargetId}",
                _ => throw new InvalidOperationException("Invalid action type for room"),
            };
        }
        else if (routine.ActionTarget == ActionTarget.Home)
        {
            return routine.ActionType switch
            {
                RoutineActionType.TurnOn => $"api/DeviceAction/control/all/user_devices",
                RoutineActionType.TurnOff => $"api/DeviceAction/control/all/user_devices",
                RoutineActionType.Dim => throw new InvalidOperationException(
                    "Dim action is not supported for home"
                ),
                _ => throw new InvalidOperationException("Invalid action type for home"),
            };
        }
        else
            throw new InvalidOperationException("Invalid action target");
    }

    public async Task<DeviceServiceResult> DeleteDevicesByRoomIdAsync(
        Guid roomId,
        string accessToken
    )
    {
        if (string.IsNullOrEmpty(accessToken))
            throw new UnauthorizedAccessException("JWT token ausente.");

        var requestUri = $"api/Device/delete_by_room/{roomId}";

        var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
            "Bearer",
            accessToken
        );

        var response = await _httpClient.SendAsync(request);

        return response.IsSuccessStatusCode
            ? new DeviceServiceResult { IsSuccess = true }
            : new DeviceServiceResult
            {
                IsSuccess = false,
                ErrorMessage = await response.Content.ReadAsStringAsync(),
            };
    }
}
