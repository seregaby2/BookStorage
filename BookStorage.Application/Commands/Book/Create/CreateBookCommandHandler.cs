using AutoMapper;
using BookStorage.Infrastructure.Interfaces;
using MediatR;
using DomainBook = BookStorage.Domain.Models.Book;

namespace BookStorage.Application.Commands.Book.Create
{
	public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, CreateBookModel>
	{
		private readonly IBookRepository _bookRepository;
		private readonly IAuthorRepository _authorRepository;
		private readonly IMapper _mapper;

		public CreateBookCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository, IMapper mapper)
		{
			_bookRepository = bookRepository;
			_authorRepository = authorRepository;
			_mapper = mapper;
		}

		public async Task<CreateBookModel?> Handle(CreateBookCommand request, CancellationToken cancellationToken)
		{
			var author = await _authorRepository.GetByIdAsync(request.Book.AuthorId);
			if (author == null)
				return null;

			var domainBook = _mapper.Map<DomainBook>(request.Book);

			var createdBook = await _bookRepository.CreateAsync(domainBook);

			var result = _mapper.Map<CreateBookModel>(createdBook);
			return result;

		}
	}
}
