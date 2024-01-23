using Microsoft.AspNetCore.Identity;

namespace EAC.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public List<Address>? Addresses { get; set; }
    }
}
