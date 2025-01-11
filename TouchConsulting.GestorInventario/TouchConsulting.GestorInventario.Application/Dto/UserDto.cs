

namespace TouchConsulting.GestorInventario.Application.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        public ICollection<UserRoleDto> UserRoles { get; set; } = new List<UserRoleDto>();
    }
}
