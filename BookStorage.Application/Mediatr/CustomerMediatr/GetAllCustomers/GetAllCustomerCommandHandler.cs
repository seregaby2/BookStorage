using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;
using MediatR;

namespace BookStorage.Application.Mediatr.CustomerMediatr.GetAllCustomers
{
	public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<Customer>>
	{
		private readonly ICustomerService _customerService;

		public GetAllCustomersQueryHandler(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		public async Task<IEnumerable<Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
		{
			return await _customerService.GetAll();
		}
	}
}
