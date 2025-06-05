using AutoMapper;
using BookStorage.Application.Commands.Customer.Create;
using BookStorage.Application.Commands.Customer.Update;
using BookStorage.Application.Queries.Customer.GetAll;
using BookStorage.Application.Queries.Customer.GetById;
using BookStorage.Domain.Models;

namespace BookStorage.Application.MappingProfiles
{
	public class CustomerProfile : Profile
	{
		public CustomerProfile()
		{
			CreateMap<CreateCustomerModel, Customer>();
			CreateMap<Customer, CreateCustomerModel>();

			CreateMap<UpdateCustomerModel, Customer>();
			CreateMap<Customer, UpdateCustomerModel>();

			CreateMap<Customer, GetAllCustomerModel>();

			CreateMap<Customer, GetByIdCustomerModel>();
		}
	}
}
