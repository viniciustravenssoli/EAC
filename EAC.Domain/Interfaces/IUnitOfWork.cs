using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAC.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IAddressRepository Address { get; }
        IApplicationUserRepository User { get; }
        ITransferRepository Transfer { get; }

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
