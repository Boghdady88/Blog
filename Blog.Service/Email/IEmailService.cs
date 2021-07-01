using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Email
{
    public interface IEmailService
    {
        Task SendEmail(string email, string subject, string message);
    }
}
