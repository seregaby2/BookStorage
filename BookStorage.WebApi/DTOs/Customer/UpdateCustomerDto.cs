namespace BookStorage.WebApi.DTOs.Customer
{
	public record UpdateCustomerDto(
		string Email,
		string FirstName,
		string PhoneNumber,
		DateTime PurchaseDate
	);
}
