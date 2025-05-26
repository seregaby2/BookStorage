using AutoMapper;
using BookStorage.Domain.Models;
using BookStorage.WebApi.DTOs.Customer;

namespace BookStorage.WebApi.Mapping
{
    public class CustomerProfile: Profile
    {       
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerViewDto>();
            CreateMap<UpdateCustomerDto, Customer>();
            CreateMap<CreateCustomerDto, Customer>();
        }  
    }
}
