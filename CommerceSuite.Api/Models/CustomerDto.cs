namespace CommerceSuite.Api.Models
{
    public class CustomerDto
    {
        public string Name { get; set; } = "Guest";
        public AddressDto Address { get; set; } = new();
    }
}
