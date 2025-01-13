using Microsoft.EntityFrameworkCore;
using TouchConsulting.GestorInventario.Application.Dto;
using TouchConsulting.GestorInventario.Application.Interfaces.Repository;
using TouchConsulting.GestorInventario.Domain.Entities;
using TouchConsulting.GestorInventario.Infrastructure.Persitence;

namespace TouchConsulting.GestorInventario.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BaseDbContext _context;

        public UserRepository(BaseDbContext context)
        {
            _context = context;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserAuth(string email, string password)
        {
            return await _context.User
               .Where(u => u.Email == email && u.Password == password)
               .Include(u => u.UserRoles)
               .ThenInclude(ur => ur.Role) 
               .FirstOrDefaultAsync();
        }
    }
}
