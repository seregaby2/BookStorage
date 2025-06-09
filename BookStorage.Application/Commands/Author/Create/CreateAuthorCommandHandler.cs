using AutoMapper;
using BookStorage.Infrastructure.Interfaces;
using MediatR;
using DomainAuthor = BookStorage.Domain.Models.Author;

namespace BookStorage.Application.Commands.Author.Create
{
	public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, CreateAuthorModel>
	{
		private readonly IAuthorRepository _authorRepository;
		private readonly IMapper _mapper;

		public CreateAuthorCommandHandler(IAuthorRepository authorRepository, IMapper mapper)
		{
			_authorRepository = authorRepository;
			_mapper = mapper;
		}

		public async Task<CreateAuthorModel?> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
		{
			var isExist = await _authorRepository.CheckIfAuthorAlreadyExistsAsync(request.Author.FirstName, request.Author.LastName, request.Author.BirthDate);
			if (isExist)
				return null;
			var domainAuthor = _mapper.Map<DomainAuthor>(request.Author);
			var createdAuthor = await _authorRepository.CreateAsync(domainAuthor);

			var result = _mapper.Map<CreateAuthorModel>(createdAuthor);
			return result;

		}
	}
}
