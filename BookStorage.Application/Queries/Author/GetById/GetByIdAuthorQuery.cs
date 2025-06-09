using MediatR;

namespace BookStorage.Application.Queries.Author.GetById
{
	public record GetAuthorByIdQuery(Guid Id) : IRequest<GetByIdAuthorModel?>;
}
