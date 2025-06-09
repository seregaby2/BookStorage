namespace BookStorage.Application.Queries.Customer.GetById
{
	public record GetByIdCustomerModel(
		Guid id,
		string Email,
		string FirstName,
		string PhoneNumber,
		DateTime PurchaseDate
	);
}
