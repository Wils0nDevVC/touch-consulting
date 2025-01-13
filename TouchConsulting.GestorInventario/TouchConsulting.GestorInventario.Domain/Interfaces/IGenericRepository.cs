using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouchConsulting.GestorInventario.Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> AsNoTracking();
        IQueryable<TEntity> AsQueryable();

        void Create(TEntity entity);
        Task CreateAsync(TEntity entity);
        void CreateRange(IEnumerable<TEntity> entities);
        Task CreateRangeAsync(IEnumerable<TEntity> entities);
        void Delete(params object[] id);
        void Delete(TEntity entity);
        Task DeleteAsync(params object[] id);
        void DeleteRange(IEnumerable<TEntity> entities);
        TEntity Find(params object[] keyValues);
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<TEntity> UpdateAsync(TEntity entity);
        IQueryable<TEntity> GetQuery(Func<IQueryable<TEntity>, IQueryable<TEntity>> queryFunc);

    }
}
