using AutoMapper;
using BookStorage.Infrastructure.Interfaces;
using MediatR;
using DomainCustomer = BookStorage.Domain.Models.Customer;

namespace BookStorage.Application.Commands.Customer.Update
{
	public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, UpdateCustomerModel>
	{
		private readonly ICustomerRepository _customerRepository;
		private readonly IMapper _mapper;

		public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
		{
			_customerRepository = customerRepository;
			_mapper = mapper;
		}

		public async Task<UpdateCustomerModel?> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
		{
			var customerToDelete = _customerRepository.GetByIdAsync(request.id);
			if (customerToDelete == null)
				return null;

			var domainCustomer = _mapper.Map<DomainCustomer>(request.Customer);

			var updatedCustomer = await _customerRepository.UpdateAsync(request.id, domainCustomer);

			var result = _mapper.Map<UpdateCustomerModel>(updatedCustomer);
			return result;

		}
	}
}
