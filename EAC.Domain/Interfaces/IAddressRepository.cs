using EAC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAC.Domain.Interfaces
{
    public interface IAddressRepository
    {
        Task<int> CreateAddressAsync(Address address);
        Task<Address> GetAddressByIdAsync(int id);
        Task DeleteAddressAsync(Address address);
        Task<List<Address>> GetAllAsync(int top, int skip);
        Task<List<Address>> GetAllByUserId(int top, int skip, string userId);
    }
}
