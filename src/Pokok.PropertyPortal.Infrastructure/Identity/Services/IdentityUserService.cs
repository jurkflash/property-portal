using Microsoft.Extensions.Options;
using Pokok.PropertyPortal.Application.Services;
using System.Net.Http.Headers;

namespace Pokok.PropertyPortal.Infrastructure.Identity.Services
{
    public sealed class IdentityUserService : IIdentityService
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

            throw new NotImplementedException();
        }
    }
}
