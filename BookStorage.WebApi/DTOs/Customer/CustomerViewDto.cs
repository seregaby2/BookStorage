namespace BookStorage.WebApi.DTOs.Customer
{
	public class CustomerViewDto
	{
		public Guid Id { get; set; }
		public string Email { get; set; } = null!;
		public string FirstName { get; set; } = null!;
		public string PhoneNumber { get; set; } = null!;
		public DateTime PurchasehDate { get; set; }
	}
}
