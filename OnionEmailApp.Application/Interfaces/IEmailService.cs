using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading.Tasks;

namespace OnionEmailApp.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string subject, string body);
    }
}

