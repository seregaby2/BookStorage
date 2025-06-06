using MediatR;

namespace BookStorage.Application.Commands.Book.Update
{
	public record UpdateBookCommand(Guid id, UpdateBookModel Book) : IRequest<UpdateBookModel>;
}
