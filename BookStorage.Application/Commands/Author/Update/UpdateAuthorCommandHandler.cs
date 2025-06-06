using AutoMapper;
using BookStorage.Infrastructure.Interfaces;
using MediatR;
using DomainAuthor = BookStorage.Domain.Models.Author;

namespace BookStorage.Application.Commands.Author.Update
{
	public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, UpdateAuthorModel>
	{
		private readonly IAuthorRepository _authorRepository;
		private readonly IMapper _mapper;

		public UpdateAuthorCommandHandler(IAuthorRepository authorRepository, IMapper mapper)
		{
			_authorRepository = authorRepository;
			_mapper = mapper;
		}

		public async Task<UpdateAuthorModel?> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
		{
			var author = await _authorRepository.GetByIdAsync(request.id);
			if (author == null)
				return null;

			var domainAuthor = _mapper.Map<DomainAuthor>(request.Author);

			var updatedAuthor = await _authorRepository.UpdateAsync(request.id, domainAuthor);

			var result = _mapper.Map<UpdateAuthorModel>(updatedAuthor);
			return result;

		}
	}
}
