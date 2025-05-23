namespace BookStorage.WebApi.DTOs.Customer
{
    public class UpdateCustomerDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime PurchasehDate { get; set; }
    }
}
