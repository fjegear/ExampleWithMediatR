using Microsoft.EntityFrameworkCore;

namespace ExampleWithMediatR.Data.Context
{
    public class DbContextFactory : DesignTimeDbContextFactory<CustomerDbContext>
    {
        protected override CustomerDbContext CreateNewInstance(DbContextOptions<CustomerDbContext> options)
        {
            return new CustomerDbContext(options);
        }
    }
}
