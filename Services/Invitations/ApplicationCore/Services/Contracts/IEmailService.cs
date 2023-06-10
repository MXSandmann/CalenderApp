using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services.Contracts
{
    public interface IEmailService
    {
        Task SendEmail(string receiverEmail);
    }
}
