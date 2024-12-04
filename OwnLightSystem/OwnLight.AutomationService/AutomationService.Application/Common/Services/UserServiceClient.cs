using System.Text;
using AutomationService.Application.Common.Services.Interfaces;
using AutomationService.Application.Contracts;
using Newtonsoft.Json;

namespace AutomationService.Application.Common.Services;

public class UserServiceClient(HttpClient httpClient) : IUserServiceClient
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<TokenResponse> RefreshAccessTokenAsync(Guid userId)
    {
        var requestUri = $"http://localhost:5008/api/Auth/refresh_token/{userId}";

        var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
        {
            Content = new StringContent(
                "",
                Encoding.UTF8,
                "application/json"
            ) // Corpo vazio
            ,
        };

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Failed to refresh access token.");

        var result =
            JsonConvert.DeserializeObject<TokenResponse>(await response.Content.ReadAsStringAsync())
            ?? throw new Exception("Failed to deserialize token response.");

        return result;
    }
}
