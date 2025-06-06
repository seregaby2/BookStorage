namespace BookStorage.Application.Queries.Customer.GetAll
{
	public record GetAllCustomerModel(
		Guid id,
		string Email,
		string FirstName,
		string PhoneNumber,
		DateTime PurchaseDate
	);
}
