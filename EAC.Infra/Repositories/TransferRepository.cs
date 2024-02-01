using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EAC.Domain.Entities;
using EAC.Domain.Interfaces;
using EAC.Infra.Context;

namespace EAC.Infra.Repositories
{
    public class TransferRepository : BaseRepositorie, ITransferRepository
    {
        public TransferRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> CreateTransferAsync(Transfer transfer)
        {
            await _dbContext.Transfers.AddAsync(transfer);
            return transfer.Id;
        }
    }
}