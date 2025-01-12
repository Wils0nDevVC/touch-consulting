using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchConsulting.GestorInventario.Domain.Entities;

namespace TouchConsulting.GestorInventario.Infrastructure.Persitence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(t => t.Id);

            // Configurar las propiedades del producto
            builder.Property(t => t.Nombre)
                .IsRequired();  // Asegúrate de que 'Nombre' sea obligatorio

            builder.Property(t => t.Descripcion)
                .IsRequired();  // Asegúrate de que 'Descripcion' sea obligatorio

            builder.Property(t => t.Precio)
                .IsRequired()  // Asegúrate de que 'Precio' sea obligatorio
                .HasColumnType("decimal(18,2)");  // Especifica la precisión y escala para el decimal

            builder.Property(t => t.CantidadInventario)
                .IsRequired();  // Asegúrate de que 'CantidadInventario' sea obligatorio

            // Configurar la relación con la entidad Category
            builder.HasOne(t => t.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar las propiedades heredadas de BaseEntity (createAt y updateAt)
            builder.Property(t => t.createAt)
                .HasColumnType("datetimeoffset")  // Usar datetimeoffset para mantener la zona horaria
                .IsRequired();

            builder.Property(t => t.updateAt)
                .HasColumnType("datetimeoffset")  // Usar datetimeoffset para mantener la zona horaria
                .IsRequired(false);  // La propiedad 'updateAt' es opcional


        }
    }
}
