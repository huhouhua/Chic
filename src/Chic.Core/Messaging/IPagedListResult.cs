using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Chic.Core.Messaging
{
    public interface IPagedListResult : IResult
    {
        int PageIndex { get; set; }
        int PageSize { get; set; }
        int PageCount { get; }
        int TotalItemCount { get; set; }
    }

    public interface IPagedListResult<T> : IPagedListResult, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
        
    }

}
