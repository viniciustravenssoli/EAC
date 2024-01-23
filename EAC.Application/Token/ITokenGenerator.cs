using EAC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EAC.Application.Token
{
    public interface ITokenGenerator
    {
        Task<string> GenerateJwtToken(ApplicationUser user);

        Task<List<Claim>> GetAllValidClaims(ApplicationUser user);
    }
}
