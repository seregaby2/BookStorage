using BookStorage.Application.Commands.Customer.Create;
using FluentValidation;

namespace BookStorage.Application.Validators.Customer
{
	public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
	{
		public CreateCustomerCommandValidator()
		{
			RuleFor(c => c.Customer.Email)
				.NotEmpty().WithMessage("Email is required.")
				.EmailAddress().WithMessage("Invalid email format.");

			RuleFor(c => c.Customer.FirstName)
				.NotEmpty().WithMessage("First name is required.")
				.MinimumLength(2).WithMessage("First name must be at least 2 characters.");

			RuleFor(c => c.Customer.PhoneNumber)
				.NotEmpty().WithMessage("Phone number is required.")
				.Matches(@"^\+?\d{10,15}$").WithMessage("Invalid phone number format.");

			RuleFor(c => c.Customer.PurchaseDate)
				.LessThanOrEqualTo(DateTime.Now).WithMessage("Purchase date cannot be in the future.");
		}
	}
}
