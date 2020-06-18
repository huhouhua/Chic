using System;
using System.Collections.Generic;
using System.Text;

namespace Chic.Core.Messaging
{
    public interface IPagedListRequest : IRequest
    {
        int PageIndex { get; set; }
        int PageSize { get; set; }
        int Skip { get; }
    }
}
