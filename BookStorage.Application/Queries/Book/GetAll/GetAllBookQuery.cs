using MediatR;

namespace BookStorage.Application.Queries.Book.GetAll
{
	public record GetAllBooksQuery() : IRequest<IEnumerable<GetAllBooksModel>>;
}
