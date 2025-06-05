using AutoMapper;
using BookStorage.Infrastructure.Interfaces;
using MediatR;

namespace BookStorage.Application.Queries.Customer.GetAll
{
	public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<GetAllCustomerModel>>
	{
		private readonly ICustomerRepository _customerRepository;
		private readonly IMapper _mapper;

		public GetAllCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
		{
			_customerRepository = customerRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<GetAllCustomerModel>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
		{
			var customers = await _customerRepository.GetAllAsync();
			var customersDto = _mapper.Map<List<GetAllCustomerModel>>(customers);
			return customersDto;
		}
	}
}
