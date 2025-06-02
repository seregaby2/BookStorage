using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;
using MediatR;

namespace BookStorage.Application.Mediatr.OrderMediatr.CreateOrder
{
	public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
	{
		private readonly IOrderService _orderService;

		public CreateOrderCommandHandler(IOrderService orderService)
		{
			_orderService = orderService;
		}

		public async Task<Order?> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			return await _orderService.Create(request.Order);
		}
	}
}

