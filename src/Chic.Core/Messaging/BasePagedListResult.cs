using System;
using System.Collections.Generic;
using System.Text;

namespace Chic.Core.Messaging
{
    public class BasePagedListResult : BaseResult, IPagedListResult
    {
        public void SetPageIndexAndPageSize(IPagedListRequest request)
        {
            this.PageIndex = request.PageIndex;
            this.PageSize = request.PageSize;
        }
        private int _pageIndex = 1;
        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
            set
            {
                var val = value;
                if (val > 0)
                {
                    _pageIndex = val;
                }
            }
        }

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                var val = value;
                if (val > 0)
                {
                    _pageSize = val;
                }
            }
        }

        public int PageCount
        {
            get
            {
                if (TotalItemCount < 1)
                {
                    return 0;
                }
                if (PageSize <= 1)
                {
                    return TotalItemCount;
                }
                var pageCount = TotalItemCount / PageSize;
                if (TotalItemCount % PageSize > 0)
                {
                    pageCount++;
                }
                return pageCount;
            }
        }

        private int _totalItemCount = 0;
        public int TotalItemCount
        {
            get
            {
                return _totalItemCount;
            }
            set
            {
                var val = value;
                if (val > 0)
                {
                    _totalItemCount = val;
                }
            }
        }
    }
}
