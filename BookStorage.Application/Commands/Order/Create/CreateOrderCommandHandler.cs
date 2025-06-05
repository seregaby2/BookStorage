using AutoMapper;
using BookStorage.Application.Interfaces.Services;
using BookStorage.Infrastructure.Interfaces;
using MediatR;
using DomainOrder = BookStorage.Domain.Models.Order;

namespace BookStorage.Application.Commands.Order.Create
{
	public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderModel>
	{
		private readonly IOrderRepository _orderRepository;
		private readonly ICustomerRepository _customerRepository;
		private readonly IBookRepository _bookRepository;
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public CreateOrderCommandHandler(
			IOrderRepository orderRepository,
			ICustomerRepository customerRepository,
			IBookRepository bookRepository,
			IOrderService orderService,
			IMapper mapper)
		{
			_orderRepository = orderRepository;
			_bookRepository = bookRepository;
			_customerRepository = customerRepository;
			_orderService = orderService;
			_mapper = mapper;
		}

		public async Task<CreateOrderModel?> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
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

			var createdOrder = await _orderRepository.CreateAsync(domainOrder);

			var result = _mapper.Map<CreateOrderModel>(createdOrder);
			return result;

		}
	}
}


