using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class RegisterFailedException : Exception
    {
        public RegisterFailedException() : base("An unexpected error was encountered while creating a user!")
        {
        }

        public RegisterFailedException(string? message) : base(message)
        {
        }

        public RegisterFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

    }
}
