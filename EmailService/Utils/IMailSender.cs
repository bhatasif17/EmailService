/* ------------------------------------------------------------------
 * The GNU General Public License v3.0
 * Copyright (C) 2007 Free Software Foundation, Inc. <https://fsf.org/>
 * Asif Bhat
   ------------------------------------------------------------------ */
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
