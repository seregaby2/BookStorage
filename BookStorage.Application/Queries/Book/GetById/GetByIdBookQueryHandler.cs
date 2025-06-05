using AutoMapper;
using BookStorage.Infrastructure.Interfaces;
using MediatR;

namespace BookStorage.Application.Queries.Book.GetById
{
	public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, GetByIdBookModel?>
	{
		private readonly IBookRepository _bookRepository;
		private readonly IMapper _mapper;

		public GetBookByIdQueryHandler(IBookRepository bookRepository, IMapper mapper)
		{
			_bookRepository = bookRepository;
			_mapper = mapper;
		}

		public async Task<GetByIdBookModel?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
		{
			var book = await _bookRepository.GetByIdAsync(request.Id);
			var bookDto = _mapper.Map<GetByIdBookModel>(book);
			return bookDto;
		}
	}
}
