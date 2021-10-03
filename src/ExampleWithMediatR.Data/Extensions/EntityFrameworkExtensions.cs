using ExampleWithMediatR.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace ExampleWithMediatR.Data.Extensions
{
    public static class EntityFrameworkExtensions
    {
        public static void AddAuditInfo(this DbContext dbContext)
        {
            var entries = dbContext.ChangeTracker.Entries().Where(e =>
                e.Entity is EntityBase && (e.State is EntityState.Added || e.State is EntityState.Modified));

            foreach (var entry in entries)
            {
                if (entry.State is EntityState.Added)
                {
                    ((EntityBase)entry.Entity).CreatedAt = DateTime.UtcNow;
                }

                ((EntityBase)entry.Entity).UpdatedAt = DateTime.UtcNow;
            }
        }

        public static void ApplyAllConfigurations<TDbContext>(this ModelBuilder modelBuilder) where TDbContext : DbContext
        {
            var applyConfigurationMethodInfo = modelBuilder
                .GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .First(m => m.Name.Equals("ApplyConfiguration", StringComparison.OrdinalIgnoreCase));

            _ = typeof(TDbContext).Assembly
                .GetTypes()
                .Select(t => (t,
                    i: t.GetInterfaces().FirstOrDefault(i => i.Name.Equals(typeof(IEntityTypeConfiguration<>).Name, StringComparison.Ordinal))))
                .Where(it => it.i != null)
                .Select(it => (et: it.i.GetGenericArguments()[0], cfgObj: Activator.CreateInstance(it.t)))
                .Select(it => applyConfigurationMethodInfo.MakeGenericMethod(it.et).Invoke(modelBuilder, new[] { it.cfgObj }))
                .ToList();
        }
    }
}
