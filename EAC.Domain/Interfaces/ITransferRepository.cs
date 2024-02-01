using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EAC.Domain.Entities;

namespace EAC.Domain.Interfaces
{
    public interface ITransferRepository
    {
        Task<int> CreateTransferAsync(Transfer transfer);
    }
}