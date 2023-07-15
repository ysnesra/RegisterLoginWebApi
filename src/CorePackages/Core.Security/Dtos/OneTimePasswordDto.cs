using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Dtos
{
    public class OneTimePasswordDto
    {
        public bool Success { get; set; }
        public long OneTimePasswordId { get; set; }
        public string OneTimePassword { get; set; }
        
    }
}
