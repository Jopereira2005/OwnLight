namespace AutomationService.Application.Contracts;

public class DeviceServiceResult
{
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}
