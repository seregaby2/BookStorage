using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;
using MediatR;

namespace BookStorage.Application.Mediatr.OrderMediatr.UpdateOrder
{
	public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Order>
	{
		private readonly IOrderService _orderService;

		public UpdateOrderCommandHandler(IOrderService orderService)
		{
			_orderService = orderService;
		}

		public async Task<Order?> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
		{
			return await _orderService.Update(request.id, request.Order);
		}
	}
}
