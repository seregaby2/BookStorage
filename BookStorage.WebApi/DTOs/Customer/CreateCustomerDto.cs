namespace BookStorage.WebApi.DTOs.Customer
{
    public class CreateCustomerDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime PurchasehDate { get; set; }
    }
}
