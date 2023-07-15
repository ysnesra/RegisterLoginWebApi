using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Models
{
    public class EmailVerifyModel
    {
        public string SmtpServer { get; set; }
        public string SmtpPort { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SenderEmail { get; set; }
      
    }
}
