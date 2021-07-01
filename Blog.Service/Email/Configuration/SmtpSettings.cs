using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Service.Email.Configuration
{
    public class SmtpSettings
    {
        public string From { get; set; }
        public string Server { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
