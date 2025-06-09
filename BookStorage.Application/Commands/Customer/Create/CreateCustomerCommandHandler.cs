using AutoMapper;
using BookStorage.Infrastructure.Interfaces;
using MediatR;
using DomainCustomer = BookStorage.Domain.Models.Customer;

namespace BookStorage.Application.Commands.Customer.Create
{
	public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerModel>
	{
		private readonly ICustomerRepository _customerRepository;
		private readonly IMapper _mapper;

		public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
		{
			_customerRepository = customerRepository;
			_mapper = mapper;
		}

		public async Task<CreateCustomerModel?> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
		{
			var customers = await _customerRepository.GetAllAsync();

			bool exists = customers.Any(a =>
				string.Equals(a.Email, request.Customer.Email, StringComparison.OrdinalIgnoreCase));
			if (exists)
				return null;

			var domainCustomer = _mapper.Map<DomainCustomer>(request.Customer);

			var createdCustomer = await _customerRepository.CreateAsync(domainCustomer);

			var result = _mapper.Map<CreateCustomerModel>(createdCustomer);
			return result;

		}
	}
}
