using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAC.Application.Email
{
    public interface IEmailService
    {
        Task SendWelcomeEmail(string body, string emailto);
    }
}
