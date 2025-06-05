using BookStorage.Application.Commands.Customer.Update;
using FluentValidation;

namespace BookStorage.Application.Validators.Customer
{

	public class UpdateCustomerModelValidator : AbstractValidator<UpdateCustomerModel>
	{
		public UpdateCustomerModelValidator()
		{
			RuleFor(c => c.Email)
				.NotEmpty().WithMessage("Email is required.")
				.EmailAddress().WithMessage("Invalid email format.");

			RuleFor(c => c.FirstName)
				.NotEmpty().WithMessage("First name is required.")
				.MinimumLength(2).WithMessage("First name must be at least 2 characters.");

			RuleFor(c => c.PhoneNumber)
				.NotEmpty().WithMessage("Phone number is required.")
				.Matches(@"^\+?\d{10,15}$").WithMessage("Invalid phone number format.");

			RuleFor(c => c.PurchaseDate)
				.LessThanOrEqualTo(DateTime.Now).WithMessage("Purchase date cannot be in the future.");
		}
	}

	public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
	{
		public UpdateCustomerCommandValidator()
		{
			RuleFor(x => x.Customer).SetValidator(new UpdateCustomerModelValidator());
		}
	}
}
