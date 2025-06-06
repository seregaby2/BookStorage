using AutoMapper;
using BookStorage.Infrastructure.Interfaces;
using MediatR;

namespace BookStorage.Application.Queries.Author.GetById
{
	public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, GetByIdAuthorModel?>
	{
		private readonly IAuthorRepository _authorRepository;
		private readonly IMapper _mapper;

		public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository, IMapper mapper)
		{
			_authorRepository = authorRepository;
			_mapper = mapper;
		}

		public async Task<GetByIdAuthorModel?> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
		{
			var author = await _authorRepository.GetByIdAsync(request.Id);
			var authorDto = _mapper.Map<GetByIdAuthorModel>(author);
			return authorDto;
		}
	}
}
