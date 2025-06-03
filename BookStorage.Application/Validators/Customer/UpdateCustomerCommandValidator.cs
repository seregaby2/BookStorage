using BookStorage.Application.Mediatr.CustomerMediatr.UpdateCustomer;
using FluentValidation;

namespace BookStorage.Application.Validators.Customer
{
	public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
	{
		public UpdateCustomerCommandValidator()
		{
			RuleFor(c => c.customer.Email)
				.NotEmpty().WithMessage("Email is required.")
				.EmailAddress().WithMessage("Invalid email format.");

			RuleFor(c => c.customer.FirstName)
				.NotEmpty().WithMessage("First name is required.")
				.MinimumLength(2).WithMessage("First name must be at least 2 characters.");

			RuleFor(c => c.customer.PhoneNumber)
				.NotEmpty().WithMessage("Phone number is required.")
				.Matches(@"^\+?\d{10,15}$").WithMessage("Invalid phone number format.");

			RuleFor(c => c.customer.PurchaseDate)
				.LessThanOrEqualTo(DateTime.Now).WithMessage("Purchase date cannot be in the future.");
		}
	}
}
