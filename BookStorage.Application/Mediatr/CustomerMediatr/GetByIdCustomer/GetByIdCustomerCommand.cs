using BookStorage.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStorage.Application.Mediatr.CustomerMediatr.GetByIdCustomer
{
	public record GetCustomerByIdQuery(Guid Id) : IRequest<Customer?>;
}
