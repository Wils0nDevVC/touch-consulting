using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchConsulting.GestorInventario.Application.Interfaces;
using TouchConsulting.GestorInventario.Domain.Entities;
using TouchConsulting.GestorInventario.Domain.Interfaces;
using TouchConsulting.GestorInventario.Infrastructure.Persitence;

namespace TouchConsulting.GestorInventario.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BaseDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private bool disposedValue;

        public UnitOfWork(BaseDbContext Context, IServiceProvider serviceProvider)
        {
            this._context = Context;
            this._serviceProvider = serviceProvider;
        }
        public IGenericRepository<Product> Products => this._serviceProvider.GetRequiredService<IGenericRepository<Product>>();

        public IGenericRepository<User> Users => this._serviceProvider.GetRequiredService<IGenericRepository<User>>();



        public int SaveChanges()
        {
            try
            {
                var vResult = this._context.SaveChanges();
                return vResult;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Record does not exist in the database");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var vResult = await this._context.SaveChangesAsync(cancellationToken);
                return vResult;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Record does not exist in the database");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DbTransaction Transaction()
        {
            return _context.Database.BeginTransaction().GetDbTransaction();
        }

        public async Task<DbTransaction> TransactionAsync(CancellationToken cancellationToken = default)
        {
            return (await _context.Database.BeginTransactionAsync(cancellationToken)).GetDbTransaction();
        }

        public void UseTransaction(DbTransaction dbTransaction)
        {
            _context.Database.UseTransaction(dbTransaction);
        }

        public async Task UseTransactionAsync(DbTransaction dbTransaction, CancellationToken cancellationToken = default)
        {
            await _context.Database.UseTransactionAsync(dbTransaction, cancellationToken);
        }

        void IUnitOfWork.BulkInsert<TEntity>(IList<TEntity> entities, BulkConfig bulkConfig)
        {
            this._context.BulkInsert(entities, bulkConfig);
        }

        Task IUnitOfWork.BulkInsertAsync<TEntity>(IList<TEntity> entities, BulkConfig bulkConfig, CancellationToken cancellationToken)
        {
            return this._context.BulkInsertAsync(entities, bulkConfig, cancellationToken: cancellationToken);
        }

        void IUnitOfWork.BulkUpdate<TEntity>(IList<TEntity> entities, BulkConfig bulkConfig)
        {
            this._context.BulkUpdate(entities, bulkConfig);
        }

        Task IUnitOfWork.BulkUpdateAsync<TEntity>(IList<TEntity> entities, BulkConfig bulkConfig, CancellationToken cancellationToken)
        {

            return this._context.BulkUpdateAsync(entities, bulkConfig, cancellationToken: cancellationToken);
        }

        void IUnitOfWork.Truncate<TEntity>(Type type)
        {
            this._context.Truncate<TEntity>(type);
        }

        Task IUnitOfWork.TruncateAsync<TEntity>(Type type, CancellationToken cancellationToken)
        {
            return this._context.TruncateAsync<TEntity>(type, cancellationToken: cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: eliminar el estado administrado (objetos administrados)
                    _context.Dispose();
                }

                // TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
                // TODO: establecer los campos grandes como NULL
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
