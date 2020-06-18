using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chic.Core.Linq
{
    public interface IAsyncQueryableExecuter
    {
        Task<int> CountAsync<T>(IQueryable<T> queryable);

        Task<List<T>> ToListAsync<T>(IQueryable<T> queryable);

        Task<T> FirstOrDefaultAsync<T>(IQueryable<T> queryable);

        Task<bool> AnyAsync<T>(IQueryable<T> queryable);
    }
}
