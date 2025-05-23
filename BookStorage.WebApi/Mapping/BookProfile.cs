using AutoMapper;
using BookStorage.WebApi.DTOs.Book;
using BookStorage.Domain.Models;

namespace BookStorage.WebApi.Mapping
{
    public class BookProfile: Profile
    {
        public BookProfile() {
            CreateMap<Book, BookViewDto>();
            CreateMap<CreateBookDto, BookViewDto>();
            CreateMap<UpdateBookDto, BookViewDto>();
        }
    }
}
