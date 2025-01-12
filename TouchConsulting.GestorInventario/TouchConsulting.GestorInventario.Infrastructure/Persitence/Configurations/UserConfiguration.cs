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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.LastName)
                .HasMaxLength(255);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
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
