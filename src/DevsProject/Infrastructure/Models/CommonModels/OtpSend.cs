using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.CommonModels
{
    public class OtpSend
    {
        public string Message { get; set; }

        public string To { get; set; }

        public string Type { get; set; } //Channel'a göre düzenlenecek

        public string Encode { get; set; }

        public int Timeout { get; set; }

        public int Length { get; set; }

        public int Header { get; set; }

        public string? Title { get; set; }

        public int? Sender { get; set; }

    }

}
