using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class EmailCanNotBeDuplicated: Exception
    {
        public EmailCanNotBeDuplicated() : base("This email exists. Email cannot be repeated!")
        {
        }

        public EmailCanNotBeDuplicated(string? message) : base(message)
        {
        }

        public EmailCanNotBeDuplicated(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}
