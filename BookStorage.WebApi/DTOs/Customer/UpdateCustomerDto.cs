namespace BookStorage.WebApi.DTOs.Customer
{
    public class UpdateCustomerDto
    {
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public DateTime PurchasehDate { get; set; }
    }
}
