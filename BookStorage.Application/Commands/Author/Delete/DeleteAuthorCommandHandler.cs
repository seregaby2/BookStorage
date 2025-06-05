using BookStorage.Infrastructure.Interfaces;
using MediatR;

namespace BookStorage.Application.Commands.Author.Delete
{
	public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, bool>
	{
		private readonly IAuthorRepository _authorRepository;

		public DeleteAuthorCommandHandler(IAuthorRepository authorRepository)
		{
			_authorRepository = authorRepository;
		}

		public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
		{
			var authorToDelete = await _authorRepository.GetByIdAsync(request.Id);
			if (authorToDelete == null)
				return false;

			return await _authorRepository.DeleteAsync(request.Id);
		}
	}
}
