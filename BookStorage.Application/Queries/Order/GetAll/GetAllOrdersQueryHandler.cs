using AutoMapper;
using BookStorage.Infrastructure.Interfaces;
using MediatR;


namespace BookStorage.Application.Queries.Order.GetAll
{

	public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<GetAllOrderModel>>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;

		public GetAllOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<GetAllOrderModel>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
		{
			var orders = await _orderRepository.GetAllAsync();
			var ordersDto = _mapper.Map<List<GetAllOrderModel>>(orders);
			return ordersDto;
		}
	}
}
