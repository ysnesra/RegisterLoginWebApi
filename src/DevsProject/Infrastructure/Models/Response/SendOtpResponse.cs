using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.Response
{
    public class SendOtpResponse
    {
        public bool Result { get; set; }

        public object Data { get; set; }
    }
}
