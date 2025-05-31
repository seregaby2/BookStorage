namespace BookStorage.WebApi.DTOs.Customer
{
	public class CreateCustomerDto
	{
		public required string Email { get; set; }
		public required string FirstName { get; set; }
		public required string PhoneNumber { get; set; }
		public DateTime PurchaseDate { get; set; }
	}
}
