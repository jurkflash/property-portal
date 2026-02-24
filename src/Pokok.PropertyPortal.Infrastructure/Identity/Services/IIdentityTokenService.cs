namespace Pokok.PropertyPortal.Infrastructure.Identity.Services
{
    public interface IIdentityTokenService
    {
        Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default);
    }
}
