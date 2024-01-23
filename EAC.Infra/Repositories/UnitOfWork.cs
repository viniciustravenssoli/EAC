using EAC.Domain.Interfaces;
using EAC.Infra.Configuration;
using EAC.Infra.Context;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAC.Infra.Repositories
{
    internal class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _dbContext;
        private IDbContextTransaction _dbTransaction;
        private readonly IMediator _mediator;

        public UnitOfWork(
            AppDbContext dbContext,
            IApplicationUserRepository user,
            IAddressRepository address,
            IMediator mediator)
        {
            _dbContext = dbContext;
            Address = address;
            User = user;
            _mediator = mediator;
        }

        public IAddressRepository Address   { get; }
        public IApplicationUserRepository User  { get; }

        public async Task BeginTransactionAsync()
        {
            _dbTransaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _dbTransaction.CommitAsync();
            }
            catch (Exception)
            {
                await _dbTransaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(_dbContext);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
