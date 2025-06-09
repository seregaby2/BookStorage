using BookStorage.Infrastructure.Interfaces;
using MediatR;

namespace BookStorage.Application.Commands.Book.Delete
{
	public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
	{
		private readonly IBookRepository _bookRepository;

		public DeleteBookCommandHandler(IBookRepository bookRepository)
		{
			_bookRepository = bookRepository;
		}

		public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
		{
			var bookToDelete = await _bookRepository.GetByIdAsync(request.Id);
			if (bookToDelete == null)
				return false;

			return await _bookRepository.DeleteAsync(request.Id);
		}
	}
}
