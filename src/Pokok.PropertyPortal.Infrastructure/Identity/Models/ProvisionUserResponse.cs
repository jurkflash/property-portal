using System.Text.Json.Serialization;

namespace Pokok.PropertyPortal.Infrastructure.Identity.Models
{
    internal sealed class ProvisionUserResponse
    {
        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }
    }
}
