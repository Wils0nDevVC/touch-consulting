using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchConsulting.GestorInventario.Domain.Entities;

namespace TouchConsulting.GestorInventario.Application.Interfaces
{
    public interface IAuthService
    {
        string EncriptarSHA256(string texto);
        string GenerarJWT(User user);
    }
}
