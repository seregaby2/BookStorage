using BookStorage.Infrastructure.Interfaces;
using MediatR;

namespace BookStorage.Application.Commands.Customer.Delete
{
	public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
	{
		private readonly ICustomerRepository _customerRepository;

		public DeleteCustomerCommandHandler(ICustomerRepository orderRepository)
		{
			_customerRepository = orderRepository;
		}

		public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
		{
			var customerToDelete = _customerRepository.GetByIdAsync(request.Id);
			if (customerToDelete == null)
				return false;

			return await _customerRepository.DeleteAsync(request.Id);
		}
	}
}
