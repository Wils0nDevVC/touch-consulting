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
        public int UserId { get; set; }
        public UserDto User { get; set; }

        public int RoleId { get; set; }
        public RoleDto Role { get; set; }

        public DateTime AssignedDate { get; set; }
    }
}
