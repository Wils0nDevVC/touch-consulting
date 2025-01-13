using TouchConsulting.GestorInventario.Domain;
using TouchConsulting.GestorInventario.Domain.Entities;
using EFCore.BulkExtensions;
using System.Data.Common;
using TouchConsulting.GestorInventario.Domain.Interfaces;


namespace TouchConsulting.GestorInventario.Application.Interfaces
{
    public interface IUnitOfWork
    {
        #region Entidades mapeadas al contexto

        IGenericRepository<Product> Products { get; }
        IGenericRepository<User> Users { get; }



        #endregion




            #region Funciones y metodos públicos
        void BulkInsert<TEntity>(IList<TEntity> entities, BulkConfig bulkConfig = null) where TEntity : class, IGenerateEntity<TEntity>;
        Task BulkInsertAsync<TEntity>(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken cancellationToken = default) where TEntity : class, IGenerateEntity<TEntity>;
        void BulkUpdate<TEntity>(IList<TEntity> entities, BulkConfig bulkConfig = null) where TEntity : class, IGenerateEntity<TEntity>;
        Task BulkUpdateAsync<TEntity>(IList<TEntity> entities, BulkConfig bulkConfig = null, CancellationToken cancellationToken = default) where TEntity : class, IGenerateEntity<TEntity>;
        void Truncate<TEntity>(Type type = null) where TEntity : class, IGenerateEntity<TEntity>;
        Task TruncateAsync<TEntity>(Type type = null, CancellationToken cancellationToken = default) where TEntity : class, IGenerateEntity<TEntity>;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbTransaction Transaction();
        Task<DbTransaction> TransactionAsync(CancellationToken cancellationToken = default);
        void UseTransaction(DbTransaction dbTransaction);
        Task UseTransactionAsync(DbTransaction dbTransaction, CancellationToken cancellationToken = default);


        #endregion
    }
}
