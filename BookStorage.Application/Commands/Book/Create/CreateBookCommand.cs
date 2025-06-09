using MediatR;

namespace BookStorage.Application.Commands.Book.Create
{
	public record CreateBookCommand(CreateBookModel Book) : IRequest<CreateBookModel>;
}
