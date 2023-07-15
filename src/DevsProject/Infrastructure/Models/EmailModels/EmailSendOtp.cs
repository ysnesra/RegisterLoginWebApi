using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.EmailModels
{
    public class EmailSendOtp
    {
        public int Template { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Recipients { get; set; }

        public string Encode { get; set; }

        public int Timeout { get; set; }

        public int Length { get; set; }
    }
}
