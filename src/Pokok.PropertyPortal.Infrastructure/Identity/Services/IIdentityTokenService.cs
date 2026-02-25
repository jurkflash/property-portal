namespace Pokok.PropertyPortal.Infrastructure.Identity.Services
{
    internal interface IIdentityTokenService
    {
        Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default);
    }
}
