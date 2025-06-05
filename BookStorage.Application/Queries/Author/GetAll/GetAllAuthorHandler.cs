using AutoMapper;
using BookStorage.Infrastructure.Interfaces;
using MediatR;

namespace BookStorage.Application.Queries.Author.GetAll
{
	public class GetAllAuthorQueryHandler : IRequestHandler<GetAllAuthorQuery, IEnumerable<GetAllAuthorModel>>
	{
		private readonly IAuthorRepository _authorRepository;
		private readonly IMapper _mapper;

		public GetAllAuthorQueryHandler(IAuthorRepository authorRepository, IMapper mapper)
		{
			_authorRepository = authorRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<GetAllAuthorModel>> Handle(GetAllAuthorQuery request, CancellationToken cancellationToken)
		{
			var authors = await _authorRepository.GetAllAsync();
			var authorsDto = _mapper.Map<List<GetAllAuthorModel>>(authors);
			return authorsDto;
		}
	}
}
