using Core.Security.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Abstract
{
    public interface ISendOtpServiceFactory
    {
        IOneTimePasswordService CreateSendOtpService(OneTimePasswordChannel channel);
    }
}
