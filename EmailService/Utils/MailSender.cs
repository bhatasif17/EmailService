/* ------------------------------------------------------------------
 * The GNU General Public License v3.0
 * Copyright (C) 2007 Free Software Foundation, Inc. <https://fsf.org/>
 * Asif Bhat
   ------------------------------------------------------------------ */
using EmailService.Models;
using FluentEmail.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EmailService.Utils
{
    public class MailSender : IMailSender
    {
        #region constructor
        private readonly IServiceProvider _serviceProvider;
        public MailSender(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        #endregion ctor

        #region gmail
        public async Task SendPlaintextGmail(EmailViewModel model)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mailer = scope.ServiceProvider.GetRequiredService<IFluentEmail>();
                var email = mailer
                    .To(model.RecipientEmail, model.RecipientName)
                    .Subject("Hello there from DI in Class!")
                    .Body("This is a plain text message using Gmail");

                await email.SendAsync();
            }
        }

        public async Task SendHtmlGmail(EmailViewModel model)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mailer = scope.ServiceProvider.GetRequiredService<IFluentEmail>();
                var email = mailer
                    .To(model.RecipientEmail, model.RecipientName)
                    .Subject(model.Subject)
                    .UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}/wwwroot/emails/sample.cshtml",
                    new
                    {
                        Name = model.RecipientName,
                    });
                var d = await email.SendAsync();
                ;
            }
        }

        public async Task SendHtmlWithAttachmentGmail(EmailViewModel model)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mailer = scope.ServiceProvider.GetRequiredService<IFluentEmail>();
                var email = mailer
                    .To(model.RecipientEmail, model.RecipientName)
                    .Subject(model.Subject)
                    .AttachFromFilename($"{Directory.GetCurrentDirectory()}/wwwroot/files/sample.pdf", "application/pdf", "Application Form")
                    .UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}/wwwroot/emails/sample.cshtml",
                    new
                    {
                        Name = model.RecipientName,
                    });

                await email.SendAsync();
            }
        }
        #endregion gmail

        #region sendGrid
        public async Task SendPlainTextSendgrid(EmailViewModel model)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mailer = scope.ServiceProvider.GetRequiredService<IFluentEmail>();
                var email = mailer
                    .To(model.RecipientEmail, model.RecipientName)
                    .Subject("Hello Plaintext from Sendrid")
                    .Body("Hello there " + model.RecipientName + ", How are you doing today");

                await email.SendAsync();
            }
        }

        public async Task SendHtmlSendgrid(EmailViewModel model)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mailer = scope.ServiceProvider.GetRequiredService<IFluentEmail>();
                var email = mailer
                    .To(model.RecipientEmail, model.RecipientName)
                    .Subject("Hello there Sendgrid HTML")
                    .UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}/wwwroot/emails/sample.cshtml",
                    new
                    {
                        Name = model.RecipientName,
                    });

                await email.SendAsync();
            }
        }

        public async Task SendHtmlWithAttachmentSendgrid(EmailViewModel model)
        {
            var path = $"{Directory.GetCurrentDirectory()}/wwwroot/files/Invoice.pdf";
            await File.WriteAllBytesAsync(path, model.Attachment);
            using (var scope = _serviceProvider.CreateScope())
            {
                var mailer = scope.ServiceProvider.GetRequiredService<IFluentEmail>();
                var email = mailer
                    .To(model.RecipientEmail, model.RecipientName)
                    .Subject("Hello there Sendgrid With Attachment")
                    .AttachFromFilename(path, "application/pdf", "Application Form")
                    .UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}/wwwroot/emails/sample.cshtml",
                    new
                    {
                        Name = model.RecipientName,
                    });

                await email.SendAsync();
            }
        }

        public async Task SendSendgridBulk(EmailViewModel model)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var factory = scope.ServiceProvider.GetRequiredService<IFluentEmailFactory>();
                foreach (var recipient in model.Recipients)
                {
                    var email = factory
                        .Create()
                        .To(recipient)
                        .Subject(model.Subject)
                        .UsingTemplateFromFile($"{Directory.GetCurrentDirectory()}/wwwroot/emails/sample.cshtml",
                        new
                        {
                            Name = model.RecipientName,
                        });
                    await email.SendAsync();
                }
            }
        }
        #endregion sendGrid
    }
}
