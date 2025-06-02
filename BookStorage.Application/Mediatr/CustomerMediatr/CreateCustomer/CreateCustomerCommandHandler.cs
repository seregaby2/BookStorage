using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;
using MediatR;

namespace BookStorage.Application.Mediatr.CustomerMediatr.CreateCustomer
{
	public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
	{
		private readonly ICustomerService _customerService;

		public CreateCustomerCommandHandler(ICustomerService orderService)
		{
			_customerService = orderService;
		}

		public async Task<Customer?> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
		{
			return await _customerService.Create(request.customer);
		}
	}
}
