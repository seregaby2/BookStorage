using BookStorage.Application.Interfaces.Services;
using MediatR;

namespace BookStorage.Application.Mediatr.OrderMediatr.DeleteOrder
{
	public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
	{
		private readonly IOrderService _orderService;

		public DeleteOrderCommandHandler(IOrderService orderService)
		{
			_orderService = orderService;
		}

		public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
		{
			return await _orderService.Delete(request.Id);
		}
	}
}
