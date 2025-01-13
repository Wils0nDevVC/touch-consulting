using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using TouchConsulting.GestorInventario.ExternalServices.Arroba.Base;
using TouchConsulting.GestorInventario.ExternalServices.Arroba.Models;
using TouchConsulting.GestorInventario.ExternalServices;

public class ApiArroba : IApiArroba
{
    private readonly MailSettings _mailSettings;
    private readonly HttpClient _httpClient;
    private readonly IServiceProvider _serviceProvider;

    public ApiArroba(IOptions<MailSettings> mailSettings, HttpClient httpClient, IServiceProvider serviceProvider)
    {
        _mailSettings = mailSettings.Value;
        _httpClient = httpClient;
        _serviceProvider = serviceProvider;
    }

    public async Task<ApiGenericResponse<MailResponse>> SendEmail(MailRequest mailRequest)
    {
        var apiResponse = new ApiGenericResponse<MailResponse>();

        try
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(mailRequest.MailFrom, mailRequest.MailFromAlias ?? ""),
                Subject = mailRequest.MailSubject,
                Body = mailRequest.MailBodyHtml ?? "",
                IsBodyHtml = true
            };

            mailMessage.To.Add(mailRequest.MailTo);

            if (!string.IsNullOrEmpty(mailRequest.MailCc))
                mailMessage.CC.Add(mailRequest.MailCc);

            if (!string.IsNullOrEmpty(mailRequest.MailBcc))
                mailMessage.Bcc.Add(mailRequest.MailBcc);

            foreach (var file in mailRequest.FileAttached ?? Enumerable.Empty<KeyValuePair<string, byte[]>>())
            {
                var attachment = new Attachment(new MemoryStream(file.Value), file.Key);
                mailMessage.Attachments.Add(attachment);
            }

            using (var smtpClient = new SmtpClient("smtp.gmail.com"))
            {
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(_mailSettings.MailFrom, _mailSettings.MailPassword);

                await smtpClient.SendMailAsync(mailMessage);

                apiResponse.IsSuccess = true;
                apiResponse.Message = "Correo enviado correctamente.";
            }
        }
        catch (Exception ex)
        {
            apiResponse.IsSuccess = false;
            apiResponse.Message = $"Error al enviar correo: {ex.Message}";
        }

        return apiResponse;
    }
}
