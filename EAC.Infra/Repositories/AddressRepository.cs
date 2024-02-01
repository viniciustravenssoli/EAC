using EAC.Domain.Entities;
using EAC.Domain.Interfaces;
using EAC.Infra.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAC.Infra.Repositories
{
    public class AddressRepository : BaseRepositorie, IAddressRepository
    {
        public AddressRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> CreateAddressAsync(Address address)
        {
            await _dbContext.Addresses.AddAsync(address);
            return address.Id;
        }

        public async Task<Address> GetAddressByIdAsync(int id)
        {
            return await _dbContext.Addresses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Address>> GetAllAsync(int top, int skip)
        {
            throw new NotImplementedException();
        }

        public Task<List<Address>> GetAllByUserId(int top, int skip, string userId)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteAddressAsync(Address addressToRemove)
        {
            _dbContext.Addresses.Remove(addressToRemove);
            await _dbContext.SaveChangesAsync();

        }
    }
}
