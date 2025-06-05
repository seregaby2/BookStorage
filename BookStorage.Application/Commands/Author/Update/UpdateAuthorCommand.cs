using MediatR;

namespace BookStorage.Application.Commands.Author.Update
{
	public record UpdateAuthorCommand(Guid id, UpdateAuthorModel Author) : IRequest<UpdateAuthorModel>;
}
