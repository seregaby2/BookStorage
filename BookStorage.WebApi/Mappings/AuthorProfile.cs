using AutoMapper;
using BookStorage.Domain.Models;
using BookStorage.WebApi.DTOs.Author;

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
