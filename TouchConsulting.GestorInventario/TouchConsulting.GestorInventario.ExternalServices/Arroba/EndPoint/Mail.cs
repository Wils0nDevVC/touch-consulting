using TouchConsulting.GestorInventario.ExternalServices.Arroba.Base;
using TouchConsulting.GestorInventario.ExternalServices.Arroba.Models;

namespace TouchConsulting.GestorInventario.ExternalServices.Arroba.EndPoint
{
    public class Mail : IMail
    {
        private readonly IApiArroba _apiMail;

        public Mail(IApiArroba apiMail)
        {
            this._apiMail = apiMail;
        }

        public Task<ApiGenericResponse<MailResponse>> SendEmail(MailRequest mailRequest)
        {
            return _apiMail.SendEmail(mailRequest);
        }
    }
}
