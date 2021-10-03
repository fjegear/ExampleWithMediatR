using ExampleWithMediatR.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExampleWithMediatR.Data.EntityConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("customers");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("id").HasDefaultValueSql("newId()");

            builder.Property(c => c.Name).HasColumnName("name").HasMaxLength(255).IsRequired();

            builder.Property(c => c.Age).HasColumnName("age").IsRequired();

            builder.Property(c => c.Address).HasColumnName("address").HasMaxLength(255).IsRequired();

            builder.Property(c => c.Email).HasColumnName("email").HasMaxLength(255).IsRequired();

            builder.Property(c => c.PhoneNumber).HasColumnName("phonenumber").HasMaxLength(20).IsRequired();

            builder.Property(c => c.CreatedAt).HasColumnName("createdat").IsRequired();

            builder.Property(c => c.UpdatedAt).HasColumnName("updatedat").IsRequired();
        }
    }
}
