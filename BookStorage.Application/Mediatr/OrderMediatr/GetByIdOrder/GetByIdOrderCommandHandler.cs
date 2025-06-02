using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;
using MediatR;

namespace BookStorage.Application.Mediatr.OrderMediatr.GetByIdOrder
{
	public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Order?>
	{
		private readonly IOrderService _orderService;

		public GetOrderByIdQueryHandler(IOrderService orderService)
		{
			_orderService = orderService;
		}

		public async Task<Order?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
		{
			return await _orderService.GetById(request.Id);
		}
	}
}

