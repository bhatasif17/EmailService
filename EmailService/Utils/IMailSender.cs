using EmailService.Models;
using System.Threading.Tasks;

namespace EmailService.Utils
{
    public interface IMailSender
    {
        Task SendPlaintextGmail(EmailViewModel model);
        Task SendHtmlGmail(EmailViewModel model);
        Task SendHtmlWithAttachmentGmail(EmailViewModel model);

        Task SendPlainTextSendgrid(EmailViewModel model);
        Task SendHtmlSendgrid(EmailViewModel model);
        Task SendHtmlWithAttachmentSendgrid(EmailViewModel model);
        Task SendSendgridBulk(EmailViewModel model);
    }
}
