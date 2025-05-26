using AutoMapper;
using BookStorage.WebApi.DTOs.Book;
using BookStorage.Domain.Models;

namespace BookStorage.WebApi.Mapping
{
    public class BookProfile: Profile
    {
        public BookProfile() 
        {
            CreateMap<Book, BookViewDto>();
            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();
        }
    }
}
