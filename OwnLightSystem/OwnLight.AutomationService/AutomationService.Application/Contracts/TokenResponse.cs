namespace AutomationService.Application.Contracts;

public class TokenResponse
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required string Type { get; set; }
    public int StatusCode { get; set; }
    public required string AccessToken { get; set; }
}
