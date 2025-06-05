using AutoMapper;
using BookStorage.Infrastructure.Interfaces;
using MediatR;

namespace BookStorage.Application.Queries.Order.GetById
{
	public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, GetByIdOrderModel?>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;

		public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
		{
			_mapper = mapper;
			_orderRepository = orderRepository;
		}

		public async Task<GetByIdOrderModel?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
		{
			var order = await _orderRepository.GetByIdAsync(request.Id);
			var orderDto = _mapper.Map<GetByIdOrderModel>(order);
			return orderDto;
		}
	}
}

