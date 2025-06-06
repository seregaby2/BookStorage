using MediatR;

namespace BookStorage.Application.Commands.Book.Delete
{
	public record DeleteBookCommand(Guid Id) : IRequest<bool>;
}
