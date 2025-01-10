using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TouchConsulting.GestorInventario.Domain.Entities
{
    public class User : BaseEntity
    {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     public int Id { get; set; }
     public required string Name { get; set; }
     public string LastName { get; set; }
     public required string Email { get; set; }
     public required string Password { get; set; }
     public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    }
}
