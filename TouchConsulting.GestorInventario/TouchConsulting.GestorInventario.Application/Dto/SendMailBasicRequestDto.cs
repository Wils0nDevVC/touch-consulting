using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouchConsulting.GestorInventario.Application.Dto
{
    public class SendMailBasicRequestDto
    {
        public string MailFromAlias { get; set; }
        public string MailSubject { get; set; }
        public string MailTo { get; set; }
        public string MailBodyHtml { get; set; }
    }
}
