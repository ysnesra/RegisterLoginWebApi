using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.SmsModels
{
    public class SmsSendOtp
    {
        public string Message { get; set; }

        public string Recipients { get; set; }

        public int Header { get; set; }

        public string Type { get; set; }

        public string Encode { get; set; }

        public int Timeout { get; set; }

        public int Length { get; set; }
    }
}
