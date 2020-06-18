using System;
using System.Collections.Generic;
using System.Text;

namespace Chic.Core.Messaging
{
    public interface IResult
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }
    }
}
