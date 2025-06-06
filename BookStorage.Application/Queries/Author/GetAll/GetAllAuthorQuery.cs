using MediatR;

namespace BookStorage.Application.Queries.Author.GetAll
{
	public record GetAllAuthorQuery() : IRequest<IEnumerable<GetAllAuthorModel>>;
}
