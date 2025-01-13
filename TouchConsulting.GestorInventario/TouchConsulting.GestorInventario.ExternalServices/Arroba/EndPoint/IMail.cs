using TouchConsulting.GestorInventario.ExternalServices.Arroba.Models;

namespace TouchConsulting.GestorInventario.ExternalServices.Arroba.EndPoint
{
    public interface IMail
    {
        Task<ApiGenericResponse<MailResponse>> SendEmail(MailRequest mailRequest);
    }
}
