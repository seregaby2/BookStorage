using BookStorage.Infrastructure.Interfaces;
using MediatR;

namespace BookStorage.Application.Commands.Order.Delete
{
	public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
	{
		private readonly IOrderRepository _orderRepository;

		public DeleteOrderCommandHandler(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
		{
			return await _orderRepository.DeleteAsync(request.Id);
		}
	}
}
