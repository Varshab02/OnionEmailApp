using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnionEmailApp.Application.Interfaces;

namespace OnionEmailApp.Web.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public IActionResult Compose()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string subject, string body)
        {
            await _emailService.SendEmailAsync(subject, body);
            ViewBag.Message = "Email sent successfully!";
            return View("Compose");
        }
    }
}
