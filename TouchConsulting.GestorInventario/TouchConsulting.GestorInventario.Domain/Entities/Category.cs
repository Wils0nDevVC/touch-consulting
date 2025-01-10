using System;
using System.Collections.Generic;
using System.Text;

namespace TouchConsulting.GestorInventario.Domain.Entities
{
    public class Category : BaseEntity
    {
       public int Id { get; set; }
       public string Description { get; set; }
       public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
