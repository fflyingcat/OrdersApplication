using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> entityTypeBuilder)
        {
            entityTypeBuilder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            entityTypeBuilder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            entityTypeBuilder.Property(x => x.Quantity)
                .IsRequired()
                .HasColumnType("decimal(18, 3)");

            entityTypeBuilder.Property(x => x.Unit)
                .IsRequired()
                .HasColumnType("nvarchar(max)");
        }
    }
}