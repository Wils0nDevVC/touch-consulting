using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchConsulting.GestorInventario.Domain;
using TouchConsulting.GestorInventario.Domain.Interfaces;
using TouchConsulting.GestorInventario.Infrastructure.Persitence;

namespace TouchConsulting.GestorInventario.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
                              where TEntity : BaseEntity
    {
        private readonly BaseDbContext _context;
        private readonly IMapper _mapper;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(BaseDbContext context, IMapper mapper, DbSet<TEntity> dbSet)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = dbSet;
        }

        public IQueryable<TEntity> AsNoTracking()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public void Create(TEntity entity)
        {
            entity.createAt = DateTime.Now;
            _dbSet.Add(entity);
        }

        public async Task CreateAsync(TEntity entity)
        {
            entity.createAt = DateTime.Now;
            await _dbSet.AddAsync(entity);
        }

        public void CreateRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                this.Create(entity);
        }

        public async Task CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                await this.CreateAsync(entity);
        }

        public void Delete(params object[] id)
        {
            TEntity entity = this.Find(id);
            if (entity != null)
                this.Delete(entity);
        }

        public void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);

            _dbSet.Remove(entity);
        }

        public async Task DeleteAsync(params object[] id)
        {
            TEntity entity = await this.FindAsync(id);
            if (entity != null)
                this.Delete(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                this.Delete(entity);
        }

        public TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }

        public void Update(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            entity.updateAt = DateTime.Now; 
            _context.Entry(entity).State = EntityState.Modified; 
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        //public async Task<TEntity> UpdateAsync(TEntity entity)
        //{
        //    TEntity destination = entity.RecoverKey();
        //    _dbSet.Attach(destination);
        //    _context.Entry(destination).State = EntityState.Modified;
        //    _mapper.Map(entity, destination);
        //    destination.updateAt = DateTime.Now;
        //    var entry = _context.Entry(destination);
        //    return destination;
        //}
    }
}
