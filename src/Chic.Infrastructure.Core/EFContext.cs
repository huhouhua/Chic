using Chic.Infrastructure.Core.Extensions;
using DotNetCore.CAP;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chic.Infrastructure.Core
{
    public class EFContext : DbContext, IUnitOfWork, ITransaction
    {
        protected IMediator _mediator;
        private ICapPublisher _capBus;

        protected EFContext(DbContextOptions options, IMediator mediator, ICapPublisher capBus) : base(options)
        {
            _mediator = mediator;
            _capBus = capBus;
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            await _mediator.DispatchDomainEventsAsync(this);

            return true;
        }

        private IDbContextTransaction _contextTransaction;

        public IDbContextTransaction GetCurrentTransaction()
        {
            return _contextTransaction;
        }

        public bool HasActiveTransaction => _contextTransaction != null;

        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_contextTransaction != null)
            {
                return null;
            }
            else
            {
                _contextTransaction = Database.BeginTransaction(_capBus, false);
            }

            return Task.FromResult(_contextTransaction);
        }


        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }
            if (transaction != _contextTransaction)
            {
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
            }
            try
            {
                await SaveChangesAsync();

                await transaction.CommitAsync();

            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_contextTransaction != null)
                {
                    await _contextTransaction.DisposeAsync();

                    _contextTransaction = null;
                }


            }

        }


        public async Task RollbackTransactionAsync()
        {
            try
            {
                await _contextTransaction?.RollbackAsync();
            }
            finally
            {
                if (_contextTransaction != null)
                {
                    await _contextTransaction.DisposeAsync();

                    _contextTransaction = null;
                }
            }
        }




    }
}
