using ExampleWithMediatR.Domain.Dtos;
using ExampleWithMediatR.Domain.Models;

namespace ExampleWithMediatR.ApplicationServices.Mappers
{
    public interface ICustomerMapping
    {
        CustomerDto MapCustomerDto(Customer customer);
    }
}
