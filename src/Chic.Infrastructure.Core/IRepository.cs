using Chic.Core.Messaging;
using Chic.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chic.Infrastructure.Core
{
    public interface IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        TEntity Create(TEntity entity);

        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

        TEntity Update(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        bool Remove(TEntity entity);

        Task<bool> RemoveAsync(TEntity entity);
    }


    public interface IRepository<TEntity, TKey> : IRepository<TEntity> where TEntity : Entity<TKey>, IAggregateRoot
    {

        long LongCount();

        Task<long> LongCountAsync(CancellationToken cancellationToken = default);

        long LongCount(Expression<Func<TEntity, bool>> predicate);

        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);





        bool Delete(TKey id);

        bool Delete(Expression<Func<TEntity, bool>> predicate);

        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(TKey id, CancellationToken cancellationToken = default);



        TEntity GetById(TKey id);

        List<TEntity> GetAllList();

        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        List<TEntity> GetAllListByAsc(Expression<Func<TEntity, TKey>> orderBy);

        List<TEntity> GetAllListByAsc(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy);

        List<TEntity> GetAllListByDesc(Expression<Func<TEntity, TKey>> orderBy);

        List<TEntity> GetAllListByDesc(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy);




        Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetAllListAsync(CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetAllListByAscAsync(Expression<Func<TEntity, TKey>> orderBy, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetAllListByAscAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetAllListByDescAsync(Expression<Func<TEntity, TKey>> orderBy, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetAllListByDescAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, CancellationToken cancellationToken = default);

     

        List<TEntity> GetListByPage(IPagedListRequest page);

        List<TEntity> GetListByPage(Expression<Func<TEntity, bool>> predicate, IPagedListRequest page);

        List<TEntity> GetListByPageAsc(Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest page);

        List<TEntity> GetListByPageAsc(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest page);

        List<TEntity> GetListByPageDesc(Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest page);

        List<TEntity> GetListByPageDesc(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest page);



        List<TEntity> GetListByPageAsync(IPagedListRequest page, CancellationToken cancellationToken = default);

        List<TEntity> GetListByPageAsync(Expression<Func<TEntity, bool>> predicate, IPagedListRequest page, CancellationToken cancellationToken = default);

        List<TEntity> GetListByPageAscAsync(Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest page, CancellationToken cancellationToken = default);

        List<TEntity> GetListByPageAscAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest page, CancellationToken cancellationToken = default);

        List<TEntity> GetListByPageDescAsync(Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest page, CancellationToken cancellationToken = default);

        List<TEntity> GetListByPageDescAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest page, CancellationToken cancellationToken = default);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
