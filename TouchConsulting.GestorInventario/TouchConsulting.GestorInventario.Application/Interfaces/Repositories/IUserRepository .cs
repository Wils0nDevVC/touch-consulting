using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchConsulting.GestorInventario.Application.Dto;
using TouchConsulting.GestorInventario.Domain.Entities;

namespace TouchConsulting.GestorInventario.Application.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserAuth(string email, string password);

        Task<User> FindByEmailAsync(string email);
    }
}
