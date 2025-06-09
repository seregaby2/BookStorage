using AutoMapper;
using BookStorage.Infrastructure.Interfaces;
using MediatR;
using DomainBook = BookStorage.Domain.Models.Book;

namespace BookStorage.Application.Commands.Book.Update
{
	public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, UpdateBookModel>
	{
		private readonly IBookRepository _bookRepository;
		private readonly IAuthorRepository _authorRepository;
		private readonly IMapper _mapper;

		public UpdateBookCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository, IMapper mapper)
		{
			_bookRepository = bookRepository;
			_authorRepository = authorRepository;
			_mapper = mapper;
		}

		public async Task<UpdateBookModel?> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
		{
			var author = await _authorRepository.GetByIdAsync(request.Book.AuthorId);
			if (author == null)
				return null;

			var existingBook = await _bookRepository.GetByIdAsync(request.id);
			if (existingBook == null)
				return null;

			var domainBook = _mapper.Map<DomainBook>(request.Book);

			var updatedBook = await _bookRepository.UpdateAsync(request.id, domainBook);

			var result = _mapper.Map<UpdateBookModel>(updatedBook);
			return result;

		}
	}
}
