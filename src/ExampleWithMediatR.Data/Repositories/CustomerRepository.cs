using ExampleWithMediatR.Data.Context;
using ExampleWithMediatR.Data.RepositoriesAbstractions;
using ExampleWithMediatR.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ExampleWithMediatR.Data.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> EmailExistAsync(string email)
        {
            return await EntitySets.AsNoTracking().AnyAsync(e => EF.Functions.Like(e.Email, email));
        }
    }
}
