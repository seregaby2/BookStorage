namespace BookStorage.DTOs.Customer
{
    public class CustomerViewDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime PurchasehDate { get; set; }
    }
}
