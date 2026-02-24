namespace Pokok.PropertyPortal.Infrastructure.Identity
{
    public sealed class IdentityServerOptions
    {
        public const string SectionName = "IdentityServer";

        public string BaseUrl { get; set; } = string.Empty;
        public string TokenEndpoint { get; set; } = "/connect/token";
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string Scope { get; set; } = string.Empty;
        public string TenantId { get; set; } = string.Empty;
    }
}
