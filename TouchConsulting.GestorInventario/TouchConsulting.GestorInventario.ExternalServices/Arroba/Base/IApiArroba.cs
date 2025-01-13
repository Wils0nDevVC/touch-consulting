using TouchConsulting.GestorInventario.ExternalServices.Arroba.Models;

namespace TouchConsulting.GestorInventario.ExternalServices.Arroba.Base
{
    public interface IApiArroba
    {
        Task<ApiGenericResponse<MailResponse>> SendEmail(MailRequest mailRequest);
    }
}
