
namespace TouchConsulting.GestorInventario.Application.Dto
{
    public class RoleDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<UserRoleDto> UserRoles { get; set; } = new List<UserRoleDto>();
    }
}
