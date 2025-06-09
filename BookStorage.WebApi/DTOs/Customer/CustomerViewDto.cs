namespace BookStorage.WebApi.DTOs.Customer
{
	public record CustomerViewDto(
	Guid Id,
	string Email,
	string FirstName,
	string PhoneNumber,
	DateTime PurchaseDate
);
}
