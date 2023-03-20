using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entityTypeBuilder)
        {
            entityTypeBuilder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            entityTypeBuilder.Property(x => x.Number)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            entityTypeBuilder.Property(x => x.Date)
                .IsRequired()
                .HasColumnType("datetime2(7)");

            entityTypeBuilder.Property(x => x.ProviderId)
                .IsRequired();

            entityTypeBuilder.HasMany(x => x.OrderItems)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .IsRequired();
        }
    }
}