namespace BookStorage.Application.Interfaces.Services
{
	public interface IAuthorBookService
	{
		bool DeleteAuthorAndBooks(Guid authorId);
	}
}
