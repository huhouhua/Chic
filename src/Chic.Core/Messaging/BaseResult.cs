using System;
using System.Collections.Generic;
using System.Text;

namespace Chic.Core.Messaging
{
    public class BaseResult : IResult
    {
        public virtual bool IsSuccess { get; set; }
        public virtual string Message { get; set; }
    }
}
