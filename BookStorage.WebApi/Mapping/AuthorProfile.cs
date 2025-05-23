using AutoMapper;
using BookStorage.WebApi.DTOs.Author;
using BookStorage.Domain.Models;

namespace BookStorage.WebApi.Mapping
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorViewDto>();
            CreateMap<CreateAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>();
        }
    }
}
