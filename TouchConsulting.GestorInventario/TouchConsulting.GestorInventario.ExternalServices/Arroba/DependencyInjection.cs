using TouchConsulting.GestorInventario.ExternalServices.Arroba.Base;
using TouchConsulting.GestorInventario.ExternalServices.Arroba.EndPoint;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using TouchConsulting.GestorInventario.ExternalServices.Arroba.Models;

namespace TouchConsulting.GestorInventario.ExternalServices.Arroba
{
    public static class Extensions
    {
        /// <summary>
        /// Agregamos las conexiones para las apis de se de Antamina.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        /// <returns></returns>
        public static IHttpClientBuilder AddArroba(this IServiceCollection services, Func<MailSettings> options)
        {

            var mailSettings = options.Invoke();

            // Registrar las configuraciones de MailSettings como un singleton
            services.AddSingleton(mailSettings);

            // Registrar el servicio IApiArroba
            services.AddScoped<IApiArroba, ApiArroba>();


            services.AddSingleton<IMail, EndPoint.Mail>();

            return services.AddHttpClient<IApiArroba, ApiArroba>();
        }
    }
}
