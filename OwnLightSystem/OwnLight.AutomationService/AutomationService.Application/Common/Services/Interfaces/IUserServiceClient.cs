using AutomationService.Application.Contracts;

namespace AutomationService.Application.Common.Services.Interfaces;

public interface IUserServiceClient
{
    Task<TokenResponse> RefreshAccessTokenAsync(Guid userId);
}
