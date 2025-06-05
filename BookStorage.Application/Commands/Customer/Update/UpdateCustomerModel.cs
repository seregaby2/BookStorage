namespace BookStorage.Application.Commands.Customer.Update
{
	public record UpdateCustomerModel(
		string Email,
		string FirstName,
		string PhoneNumber,
		DateTime PurchaseDate
	);
}
