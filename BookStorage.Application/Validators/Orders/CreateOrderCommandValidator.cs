using BookStorage.Application.Mediatr.OrderMediatr.CreateOrder;
using FluentValidation;



namespace BookStorage.Application.Validators.Orders
{
	public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
	{
		public CreateOrderCommandValidator()
		{
			RuleFor(x => x.Order.CustomerId)
				.NotEmpty().WithMessage("CustomerId is required.");

			RuleFor(x => x.Order.OrderBooks)
				.NotNull().WithMessage("OrderBooks list must be provided.")
				.Must(books => books.Count > 0)
				.WithMessage("At least one book must be included in the order.");

			RuleFor(x => x.Order.Status)
				.NotEmpty().WithMessage("\r\nThe status can take the following values: Pending, Paid, Shipped, Delivered, Canceled,");
		}
	}
}

