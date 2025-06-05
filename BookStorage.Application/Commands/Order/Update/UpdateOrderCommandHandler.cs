using AutoMapper;
using BookStorage.Application.Interfaces.Services;
using BookStorage.Infrastructure.Interfaces;
using MediatR;
using DomainOrder = BookStorage.Domain.Models.Order;

namespace BookStorage.Application.Commands.Order.Update
{
	public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, UpdateOrderModel>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly ICustomerRepository _customerRepository;
		private readonly IBookRepository _bookRepository;
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public UpdateOrderCommandHandler(
			IOrderRepository orderRepository,
			ICustomerRepository customerRepository,
			IBookRepository bookRepository,
			IOrderService orderService,
			IMapper mapper)
		{
			_orderRepository = orderRepository;
			_customerRepository = customerRepository;
			_bookRepository = bookRepository;
			_orderService = orderService;
			_mapper = mapper;
		}

		public async Task<UpdateOrderModel?> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
		{
			var existingOrder = await _orderRepository.GetByIdAsync(request.id);
			if (existingOrder == null)
				return null;

			var customerExists = await _customerRepository.GetByIdAsync(request.Order.CustomerId);
			if (customerExists == null)
				return null;

			foreach (var ob in request.Order.OrderBook)
			{
				var bookExists = await _bookRepository.GetByIdAsync(ob.BookId);
				if (bookExists == null)
					return null;
			}

			var domainOrder = _mapper.Map<DomainOrder>(request.Order);

			await _orderService.CountTotalAmount(domainOrder);

			var UpdatedOrder = await _orderRepository.UpdateAsync(request.id, domainOrder);

			var result = _mapper.Map<UpdateOrderModel>(UpdatedOrder);
			return result;
		}
	}
}
