using System;
using System.Collections.Generic;
using System.Text;

namespace Chic.Core.Messaging
{
    public class BasePagedListRequest : IPagedListRequest
    {
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

        public int Skip
        {
            get
            {
                return (this.PageIndex - 1) * this.PageSize;
            }
        }
    }
}
