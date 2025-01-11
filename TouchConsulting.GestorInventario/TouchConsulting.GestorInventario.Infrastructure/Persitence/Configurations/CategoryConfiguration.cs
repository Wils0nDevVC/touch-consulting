using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TouchConsulting.GestorInventario.Domain.Entities;

namespace TouchConsulting.GestorInventario.Infrastructure.Persitence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(t => t.Id);
        }
    }
}
