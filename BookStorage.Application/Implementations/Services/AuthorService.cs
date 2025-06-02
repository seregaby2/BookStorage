using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;
using BookStorage.Infrastructure.Interfaces;

namespace BookStorage.Application.Implementations.Services
{
	public class AuthorService : IAuthorService
	{
		private readonly IAuthorRepository _repository;

		public AuthorService(IAuthorRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<Author>> GetAll()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<Author?> GetById(Guid id)
		{
			return await _repository.GetByIdAsync(id);
		}

		public async Task<Author?> Create(Author author)
		{
			var isExist = await _repository.CheckIfAuthorAlreadyExistsAsync(author.FirstName, author.LastName, author.BirthDate);
			if (isExist)
				return null;

			return await _repository.CreateAsync(author);
		}

		public async Task<Author?> Update(Guid id, Author author)
		{
			var existingAuthor = await _repository.GetByIdAsync(id);
			if (existingAuthor == null)
				return null;

			return await _repository.UpdateAsync(id, author);
		}

		public async Task<bool> Delete(Guid id)
		{
			var existingAuthor = await _repository.GetByIdAsync(id);
			if (existingAuthor == null)
				return false;

			return await _repository.DeleteAsync(id);
		}
	}
}
