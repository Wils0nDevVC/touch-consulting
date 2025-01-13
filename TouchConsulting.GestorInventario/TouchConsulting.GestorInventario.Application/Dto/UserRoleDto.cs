using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchConsulting.GestorInventario.Domain.Entities;

namespace TouchConsulting.GestorInventario.Application.Dto
{
    public class UserRoleDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime AssignedDate { get; set; }
    }
}
