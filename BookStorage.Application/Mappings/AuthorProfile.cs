using AutoMapper;
using BookStorage.Application.Commands.Author.Create;
using BookStorage.Application.Commands.Author.Update;
using BookStorage.Application.Queries.Author.GetAll;
using BookStorage.Application.Queries.Author.GetById;
using BookStorage.Domain.Models;

namespace BookStorage.Application.Mappings
{
	public class AuthorProfile : Profile
	{
		public AuthorProfile()
		{
			CreateMap<CreateAuthorModel, Author>();
			CreateMap<Author, CreateAuthorModel>();

			CreateMap<UpdateAuthorModel, Author>();
			CreateMap<Author, UpdateAuthorModel>();

			CreateMap<Author, GetAllAuthorModel>();

			CreateMap<Author, GetByIdAuthorModel>();
		}
	}
}
