using EmailService.Models;
using EmailService.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Controllers
{
    [Route("api/v1/email")]
    public class EmailController : Controller
    {
        private readonly IMailSender _mailSender;

        public EmailController(IMailSender mailSender)
        {
            _mailSender = mailSender;
        }

        //Gmail
        [HttpPost("TextViaGmail")]
        public async Task<IActionResult> SendPlaintextGmail([FromBody] EmailViewModel model)
        {
            await _mailSender.SendPlaintextGmail(model);
            return Ok();
        }
        [HttpPost("HTMLViaGmail")]

        public async Task<IActionResult> SendHtmlGmail([FromBody] EmailViewModel model)
        {
            await _mailSender.SendHtmlGmail(model);
            return Ok();
        }
        [HttpPost("HTMLWithAttachmentViaGmail")]
        public async Task<IActionResult> SendHtmlWithAttachmentGmail([FromBody] EmailViewModel model)
        {
            await _mailSender.SendHtmlWithAttachmentGmail(model);
            return Ok();
        }

        //SendGrid
        [HttpPost("TextViaSendGrid")]
        public async Task<IActionResult> SendPlainTextSendgrid([FromBody] EmailViewModel model)
        {
            await _mailSender.SendPlainTextSendgrid(model);
            return Ok();
        }

        [HttpPost("HTMLViaSendGrid")]
        public async Task<IActionResult> SendHtmlSendgrid([FromBody] EmailViewModel model)
        {
            await _mailSender.SendHtmlSendgrid(model);
            return Ok();
        }
        [HttpPost("HTMLWithAttachmentViaSendGrid")]
        public async Task<IActionResult> SendHtmlWithAttachmentSendgrid([FromBody] EmailViewModel model)
        {
            await _mailSender.SendHtmlWithAttachmentSendgrid(model);
            return Ok();
        }

        [HttpPost("BulkViaSendGrid")]
        public async Task<IActionResult> SendSendgridBulk([FromBody] EmailViewModel model)
        {
            await _mailSender.SendSendgridBulk(model);
            return Ok();
        }

        [HttpPost("GetBytes")]
        public async Task<byte[]> GetBytes(IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                string contentAsString = reader.ReadToEnd();
                byte[] contentAsByteArray = Encoding.UTF8.GetBytes(contentAsString);
                return contentAsByteArray;
            }
        }
    }
}
