using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAC.Domain.Result
{
    public record ResultError(string Key, string Description);
}
