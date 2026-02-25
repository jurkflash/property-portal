using Microsoft.Extensions.Options;
using Pokok.PropertyPortal.Infrastructure.Identity.Models;
using System.Net.Http.Json;

namespace Pokok.PropertyPortal.Infrastructure.Identity.Services
{
    internal sealed class IdentityTokenService : IIdentityTokenService
    {
        private readonly HttpClient _httpClient;
        private readonly IdentityServerOptions _options;

        public IdentityTokenService(HttpClient httpClient, IOptions<IdentityServerOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = _options.ClientId,
                ["client_secret"] = _options.ClientSecret,
                ["scope"] = _options.Scope
            };

            var request = new HttpRequestMessage(HttpMethod.Post, _options.TokenEndpoint)
            {
                Content = new FormUrlEncodedContent(parameters)
            };

            var response = await _httpClient.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();

            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken: cancellationToken);

            return tokenResponse?.AccessToken
                ?? throw new InvalidOperationException("Failed to obtain access token from identity server.");
        }
    }
}
