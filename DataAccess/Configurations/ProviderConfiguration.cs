using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> entityTypeBuilder)
        {
            entityTypeBuilder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            entityTypeBuilder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            entityTypeBuilder.HasMany(x => x.Orders)
                .WithOne(x => x.Provider)
                .HasForeignKey(x => x.ProviderId)
                .IsRequired();
        }
    }
}