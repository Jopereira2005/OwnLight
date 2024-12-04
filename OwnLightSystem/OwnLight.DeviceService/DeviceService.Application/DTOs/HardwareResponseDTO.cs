namespace DeviceService.Application.DTOs;

public class HardwareResponseDTO
{
    public Guid Id { get; set; }
    public string Status { get; set; } = null!;
    public int? Brightness { get; set; }
}
