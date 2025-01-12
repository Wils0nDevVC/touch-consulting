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
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

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
