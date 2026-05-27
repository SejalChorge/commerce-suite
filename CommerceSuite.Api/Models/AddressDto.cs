using CommerceSuite.Api.Models.Domain.Enterprise.Shared;

namespace CommerceSuite.Api.Models
{
    public class AddressDto
    {
        public string City { get; set; } = "Mumbai";

        public string Country { get; set; } = "India";

        public AddressMetadataDto Metadata { get; set; } = new();
    }
}
