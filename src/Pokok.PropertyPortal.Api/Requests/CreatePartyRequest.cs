using Pokok.PropertyPortal.Domain.Parties.Enums;

namespace Pokok.PropertyPortal.Api.Requests
{
    public class CreatePartyRequest
    {
        public string Name { get; set; } = string.Empty;
        public PartyType PartyType { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
