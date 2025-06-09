namespace BookStorage.WebApi.DTOs.Customer
{
	public record CreateCustomerDto(
		string Email,
		string FirstName,
		string PhoneNumber,
		DateTime PurchaseDate
	);
}

/*Refactor this class to use records 
 benefits:
	- default immutable
	- support Equal, GetHasCode
	- compact and readable code.
 */
