using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;
using MediatR;

namespace BookStorage.Application.Mediatr.CustomerMediatr.GetByIdCustomer
{
	public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer?>
	{
		private readonly ICustomerService _customerService;

		public GetCustomerByIdQueryHandler(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		public async Task<Customer?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
		{
			return await _customerService.GetById(request.Id);
		}
	}
}
