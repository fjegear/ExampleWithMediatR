using ExampleWithMediatR.Domain.Models;
using System.Threading.Tasks;

namespace ExampleWithMediatR.Data.RepositoriesAbstractions
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<bool> EmailExistAsync(string email);
    }
}
