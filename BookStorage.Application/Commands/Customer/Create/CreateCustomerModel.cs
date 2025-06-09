namespace BookStorage.Application.Commands.Customer.Create
{
	public record CreateCustomerModel(
		string Email,
		string FirstName,
		string PhoneNumber,
		DateTime PurchaseDate
	);
}
