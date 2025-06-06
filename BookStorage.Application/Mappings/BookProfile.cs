using AutoMapper;
using BookStorage.Application.Commands.Book.Create;
using BookStorage.Application.Commands.Book.Update;
using BookStorage.Application.Queries.Book.GetAll;
using BookStorage.Application.Queries.Book.GetById;
using BookStorage.Domain.Models;

namespace BookStorage.Application.Mappings
{
	public class BookProfile : Profile
	{
		public BookProfile()
		{
			CreateMap<CreateBookModel, Book>();
			CreateMap<Book, CreateBookModel>();

			CreateMap<UpdateBookModel, Book>();
			CreateMap<Book, UpdateBookModel>();

			CreateMap<Book, GetAllBooksModel>();

			CreateMap<Book, GetByIdBookModel>();
		}
	}
}
