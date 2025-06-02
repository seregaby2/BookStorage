using BookStorage.Application.Interfaces.Services;
using MediatR;

namespace BookStorage.Application.Mediatr.CustomerMediatr.DeleteCustomer
{
	public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
	{
		private readonly ICustomerService _customerService;

		public DeleteCustomerCommandHandler(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
		{
			return await _customerService.Delete(request.Id);
		}
	}
}
