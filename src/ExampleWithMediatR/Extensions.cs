using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleWithMediatR
{
    public static class Extensions
    {
        public static IApplicationBuilder UseAutoMigrationDatabase<TDBContext>(this IApplicationBuilder builder) where TDBContext : DbContext
        {
            using (var serviceScope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<TDBContext>().Database.Migrate();
            }

            return builder;
        }
    }
}
