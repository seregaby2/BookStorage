using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;
using MediatR;

namespace BookStorage.Application.Mediatr.CustomerMediatr.UpdateCustomer
{
	public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
	{
		private readonly ICustomerService _customerService;

		public UpdateCustomerCommandHandler(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		public async Task<Customer?> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
		{
			return await _customerService.Update(request.id, request.customer);
		}
	}
}
