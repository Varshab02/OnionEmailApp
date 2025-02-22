using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using OnionEmailApp.Application.Interfaces;
using OnionEmailApp.Domain.Repositories;

namespace OnionEmailApp.Application.Services
{
    public class SmtpSettings
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class EmailService : IEmailService
    {
        private readonly IUserRepository _userRepository;
        private readonly SmtpSettings _smtpSettings;


        public EmailService(IUserRepository userRepository, IOptions<SmtpSettings> smtpSettings)
        {
            _userRepository = userRepository;
            _smtpSettings = smtpSettings.Value;

            // Debugging output
            Console.WriteLine($"Loaded SMTP Email: {_smtpSettings.Email}");
            Console.WriteLine($"Loaded SMTP Password: {_smtpSettings.Password}");
        }


        public async Task SendEmailAsync(string subject, string body)
        {
            var users = await _userRepository.GetAllUsersAsync();
            var recipientEmails = users.Select(u => u.Email).ToList();

            if (!recipientEmails.Any())
                return;

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Admin", _smtpSettings.Email));
            message.To.AddRange(recipientEmails.Select(email => new MailboxAddress(email, email)));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_smtpSettings.Email, _smtpSettings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
