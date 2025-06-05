using AutoMapper;
using BookStorage.Infrastructure.Interfaces;
using MediatR;

namespace BookStorage.Application.Queries.Book.GetAll
{
	public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<GetAllBooksModel>>
	{
		private readonly IBookRepository _bookRepository;
		private readonly IMapper _mapper;

		public GetAllBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
		{
			_bookRepository = bookRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<GetAllBooksModel>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
		{
			var books = await _bookRepository.GetAllAsync();
			var booksDto = _mapper.Map<List<GetAllBooksModel>>(books);
			return booksDto;
		}
	}
}
