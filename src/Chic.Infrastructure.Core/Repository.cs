using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Chic.Domain.Abstractions;
using System.Linq.Expressions;
using Chic.Core.Messaging;
using System.Linq;

namespace Chic.Infrastructure.Core
{
    public abstract class Repository<TEntity, TDbContext> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot where TDbContext : EFContext
    {
        protected virtual TDbContext DbContext { get; set; }

        public virtual IUnitOfWork UnitOfWork => DbContext;

        public virtual DbSet<TEntity> Table => DbContext.Set<TEntity>();

        public Repository(TDbContext context)
        {
            this.DbContext = context;

        }

        public virtual TEntity Create(TEntity entity)
        {
            return DbContext.Add(entity).Entity;
        }

        public virtual Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Create(entity));
        }

        public virtual TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);

            DbContext.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            AttachIfNot(entity);

            DbContext.Entry(entity).State = EntityState.Modified;

            return Task.FromResult(entity);
        }

        public virtual bool Remove(TEntity entity)
        {
            AttachIfNot(entity);

            Table.Remove(entity);

            return true;
        }

        public virtual Task<bool> RemoveAsync(TEntity entity)
        {
            return Task.FromResult(Remove(entity));
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            if (!Table.Local.Contains(entity))
            {
                Table.Attach(entity);
            }
        }

    }

    public abstract class Repository<TEntity, TKey, TDbContext> : Repository<TEntity, TDbContext>, IRepository<TEntity, TKey> where TEntity : Entity<TKey>, IAggregateRoot where TDbContext : EFContext
    {

        public Repository(TDbContext context) : base(context)
        {

        }

        protected  IQueryable<TEntity> GetAll() 
        {
            return Table.AsTracking();
        }

        private Expression<Func<TEntity, TKey>> DefaultOrder(Expression<Func<TEntity,TKey>> order)
        {
            if (order == null) return order = q => q.Id;

            return order;
        }

        protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");

            var idValue = Convert.ChangeType(id, typeof(TKey));

            Expression<Func<object>> closure = () => idValue;

            var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);

            var lambdaBody = Expression.Equal(leftExpression, rightExpression);

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

        public bool Delete(TKey id)
        {
            var entity = Table.Local.FirstOrDefault(ent => EqualityComparer<TKey>.Default.Equals(ent.Id, id));

            if (entity == null)
            {
                entity = FirstOrDefault(q => q.Id.Equals(id));
                if (entity == null)
                {
                    return false;
                }
            }
            return Remove(entity);
        }

        public bool Delete(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null) return false;

            foreach (var item in Table.Where(predicate))
            {
                Remove(item);
            }

            return true;

        }

        public async Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = await Table.FirstOrDefaultAsync(ent => EqualityComparer<TKey>.Default.Equals(ent.Id, id),cancellationToken);

            if (entity == null)
            {
                entity = FirstOrDefault(q => q.Id.Equals(id));
                if (entity == null)
                {
                    return false;
                }
            }
            return await RemoveAsync(entity);

        }

        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            if (predicate == null) return false;

            var list = Table.Where(predicate);

            await list.ForEachAsync(q =>
            {
                 RemoveAsync(q);
            });

            return true;

        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null) return null;

            return GetAll().FirstOrDefault(predicate);

        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            if (predicate == null) return null;

            return await GetAll().FirstOrDefaultAsync(predicate, cancellationToken);

        }

        public TEntity GetById(TKey id)
        {
            return GetAll().FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public List<TEntity> GetAllList()
        {
            return GetAll().ToList();
        }

        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null) return null;

            return GetAll().Where(predicate).ToList();
        }

        public async Task<List<TEntity>> GetAllListAsync(CancellationToken cancellationToken = default)
        {
            return await GetAll().ToListAsync(cancellationToken);
        }

        public async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            if (predicate == null) return null;

            return await GetAll().Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await GetAll().FirstOrDefaultAsync(CreateEqualityExpressionForId(id),cancellationToken);
        }

        public long LongCount()
        {
            return GetAll().LongCount();
        }

        public long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null) return default(long);

            return GetAll().Where(predicate).LongCount();
        }

        public async Task<long> LongCountAsync(CancellationToken cancellationToken = default)
        {
            return await GetAll().LongCountAsync(cancellationToken);
           
        }

        public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            if (predicate == null) return default(long);

            return await GetAll().LongCountAsync(predicate,cancellationToken);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null) return null;

            return  GetAll().SingleOrDefault(predicate);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            if (predicate == null) return null;

            return await GetAll().SingleOrDefaultAsync(predicate,cancellationToken);
        }

        public List<TEntity> GetAllListByAsc(Expression<Func<TEntity, TKey>> orderBy)
        {
            return GetAll().OrderBy(DefaultOrder(orderBy)).ToList();
        }

        public List<TEntity> GetAllListByAsc(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy)
        {
            if (predicate == null) return null;

            return GetAll().Where(predicate).OrderBy(DefaultOrder(orderBy)).ToList();

        }

        public List<TEntity> GetAllListByDesc(Expression<Func<TEntity, TKey>> orderBy)
        {
            return GetAll().OrderByDescending(DefaultOrder(orderBy)).ToList();
        }

        public List<TEntity> GetAllListByDesc(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy)
        {
            if (predicate == null) return null;

            return GetAll().Where(predicate).OrderByDescending(DefaultOrder(orderBy)).ToList();
        }

        public async Task<List<TEntity>> GetAllListByAscAsync(Expression<Func<TEntity, TKey>> orderBy, CancellationToken cancellationToken = default)
        {
            return await GetAll().OrderBy(DefaultOrder(orderBy)).ToListAsync(cancellationToken);
        }

        public async Task<List<TEntity>> GetAllListByAscAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, CancellationToken cancellationToken = default)
        {
            if (predicate == null) return null;

            return await GetAll().Where(predicate).OrderBy(DefaultOrder(orderBy)).ToListAsync(cancellationToken);
        }

        public async Task<List<TEntity>> GetAllListByDescAsync(Expression<Func<TEntity, TKey>> orderBy,CancellationToken cancellationToken = default)
        {
            return await GetAll().OrderByDescending(DefaultOrder(orderBy)).ToListAsync(cancellationToken);
        }

        public async Task<List<TEntity>> GetAllListByDescAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, CancellationToken cancellationToken = default)
        {
            if (predicate == null) return null;

            return await GetAll().Where(predicate).OrderByDescending(DefaultOrder(orderBy)).ToListAsync(cancellationToken);
        }

        public List<TEntity> GetListByPage(IPagedListRequest page)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetListByPage(Expression<Func<TEntity, bool>> predicate, IPagedListRequest page)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetListByPageAsc(Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest page)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetListByPageAsc(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest page)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetListByPageDesc(Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest page)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetListByPageDesc(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest page)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetListByPageAsync(IPagedListRequest page, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetListByPageAsync(Expression<Func<TEntity, bool>> predicate, IPagedListRequest page, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetListByPageAscAsync(Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest page, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetListByPageAscAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest page, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetListByPageDescAsync(Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest page, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetListByPageDescAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest page, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null) return null;

            return GetAll().LastOrDefault(predicate);
        }

        public async Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null) return null;

            return await GetAll().LastOrDefaultAsync(predicate);
        }
    }
}
