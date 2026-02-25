using Pokok.PropertyPortal.Application.Services;
using Pokok.PropertyPortal.Infrastructure.Identity.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Pokok.PropertyPortal.Infrastructure.Identity.Services
{
    internal sealed class IdentityUserService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly IIdentityTokenService _tokenService;

        public IdentityUserService(HttpClient httpClient, IIdentityTokenService tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
        }

        public async Task<Guid> CreateUserAsync(string email, string name, CancellationToken cancellationToken = default)
        {
            var token = await _tokenService.GetAccessTokenAsync(cancellationToken);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var request = new ProvisionUserRequest
            {
                Email = email,
                DisplayName = name
            };

            var response = await _httpClient.PostAsJsonAsync("api/users", request, cancellationToken);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ProvisionUserResponse>(cancellationToken: cancellationToken);

            return result?.UserId
                ?? throw new InvalidOperationException("Identity server did not return a user ID.");
        }
    }
}
