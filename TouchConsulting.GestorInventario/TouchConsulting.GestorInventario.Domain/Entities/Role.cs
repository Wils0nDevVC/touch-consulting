using System;
using System.Collections.Generic;
using System.Text;

namespace TouchConsulting.GestorInventario.Domain.Entities
{
    public class Role : BaseEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
