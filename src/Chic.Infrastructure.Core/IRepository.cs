using Chic.Core.Messaging;
using Chic.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chic.Infrastructure.Core
{
    public interface IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        IQueryable<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate, IDictionary<Expression<Func<TEntity, object>>, bool> dictOrder, int pageIndex, int pageSize);

        IQueryable<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate, IDictionary<Expression<Func<TEntity, object>>, bool> dictOrder, IPagedListRequest request);

        IQueryable<TEntity> GetSkipTakeList(Expression<Func<TEntity, bool>> predicate, IDictionary<Expression<Func<TEntity, object>>, bool> dictOrder, int skip, int take);

        IList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate, IDictionary<Expression<Func<TEntity, object>>, bool> dictOrder, int pageIndex, int pageSize, out int totalCount);

        IList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate, IDictionary<Expression<Func<TEntity, object>>, bool> dictOrder, IPagedListRequest request, out int totalCount);

        IList<TEntity> GetSkipTakeList(Expression<Func<TEntity, bool>> predicate, IDictionary<Expression<Func<TEntity, object>>, bool> dictOrder, int skip, int take, out int totalCount);

        Task<int> GetTotalCount(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        Task<IList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate, IDictionary<Expression<Func<TEntity, object>>, bool> dictOrder, int pageIndex, int pageSize, CancellationToken cancellationToken = default);

        Task<IList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate, IDictionary<Expression<Func<TEntity, object>>, bool> dictOrder, IPagedListRequest request, CancellationToken cancellationToken = default);

        Task<IList<TEntity>> GetSkipTakeListAsync(Expression<Func<TEntity, bool>> predicate, IDictionary<Expression<Func<TEntity, object>>, bool> dictOrder, int skip, int take, CancellationToken cancellationToken = default);

        TEntity Insert(TEntity entity);

        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

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

        List<TEntity> GetAllListByAsc(Expression<Func<TEntity, TKey>> orderby);

        List<TEntity> GetAllListByAsc(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderby);

        List<TEntity> GetAllListByDesc(Expression<Func<TEntity, TKey>> orderby);

        List<TEntity> GetAllListByDesc(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderby);




        Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetAllListAsync(CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetAllListByAscAsync(Expression<Func<TEntity, TKey>> orderby, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetAllListByAscAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderby, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetAllListByDescAsync(Expression<Func<TEntity, TKey>> orderby, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetAllListByDescAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderby, CancellationToken cancellationToken = default);


        List<TEntity> GetListByPaged(IPagedListRequest paged, out int totalCount);

        List<TEntity> GetListByPaged(Expression<Func<TEntity, bool>> predicate, IPagedListRequest paged, out int totalCount);

        List<TEntity> GetListByPagedAsc(Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest paged, out int totalCount);

        List<TEntity> GetListByPagedAsc(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest paged, out int totalCount);

        List<TEntity> GetListByPagedDesc(Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest paged, out int totalCount);

        List<TEntity> GetListByPagedDesc(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest paged, out int totalCount);


        Task<List<TEntity>> GetListByPagedAsync(IPagedListRequest paged, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetListByPagedAsync(Expression<Func<TEntity, bool>> predicate, IPagedListRequest paged, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetListByPagedAscAsync(Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest paged, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetListByPagedAscAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest paged, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetListByPagedDescAsync(Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest paged, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetListByPagedDescAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, IPagedListRequest paged, CancellationToken cancellationToken = default);



        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
