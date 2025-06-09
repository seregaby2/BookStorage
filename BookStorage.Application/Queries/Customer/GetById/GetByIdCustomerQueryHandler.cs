using AutoMapper;
using BookStorage.Infrastructure.Interfaces;
using MediatR;

namespace BookStorage.Application.Queries.Customer.GetById
{
	public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, GetByIdCustomerModel?>
	{
		private readonly ICustomerRepository _customerRepository;
		private readonly IMapper _mapper;

		public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
		{
			_customerRepository = customerRepository;
			_mapper = mapper;
		}

		public async Task<GetByIdCustomerModel?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
		{
			var customer = await _customerRepository.GetByIdAsync(request.Id);
			var customerDto = _mapper.Map<GetByIdCustomerModel>(customer);
			return customerDto;
		}
	}
}
