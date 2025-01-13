using System;
using System.Collections.Generic;
using System.Text;

namespace TouchConsulting.GestorInventario.Common.Interfaces
{
    public interface IProduct
    {
        string Nombre { get; }
        int CantidadInventario { get; }
    }

}
