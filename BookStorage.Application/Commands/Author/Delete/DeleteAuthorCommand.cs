using MediatR;

namespace BookStorage.Application.Commands.Author.Delete
{
	public record DeleteAuthorCommand(Guid Id) : IRequest<bool>;
}
