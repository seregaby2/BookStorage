using MediatR;

namespace BookStorage.Application.Commands.Author.Create
{
	public record CreateAuthorCommand(CreateAuthorModel Author) : IRequest<CreateAuthorModel>;
}
