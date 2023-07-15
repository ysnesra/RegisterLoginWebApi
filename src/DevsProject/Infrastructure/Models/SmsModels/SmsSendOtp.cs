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

        public List<int> Groups { get; set; }

        public List<string> Recipients { get; set; }

        public string Commercial { get; set; }

        public int Header { get; set; }

        public string Type { get; set; }

        public bool Appeal { get; set; }

        public bool Otp { get; set; }

        public int Validity { get; set; }

        public DateTime Date { get; set; }
    }
}
