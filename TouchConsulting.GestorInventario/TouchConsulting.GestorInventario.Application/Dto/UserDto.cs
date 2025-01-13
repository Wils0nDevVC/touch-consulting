

using TouchConsulting.GestorInventario.Domain.Entities;

namespace TouchConsulting.GestorInventario.Application.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string LastName { get; set; }
        public required string Email { get; set; }
        public  string Password { get; set; }
        public List<UserRoleDto> UserRoles { get; set; } = new();
    }
}
