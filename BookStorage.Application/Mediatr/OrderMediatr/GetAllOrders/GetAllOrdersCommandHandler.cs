using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;
using MediatR;


namespace BookStorage.Application.Mediatr.OrderMediatr.GetAllOrders
{

	public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<Order>>
	{
		private readonly IOrderService _orderService;

		public GetAllOrdersQueryHandler(IOrderService orderService)
		{
			_orderService = orderService;
		}

		public async Task<IEnumerable<Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
		{
			return await _orderService.GetAll();
		}
	}
}
