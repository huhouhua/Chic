using System;
using System.Collections.Generic;
using System.Text;

namespace Chic.Domain.Abstractions
{
    public interface IEntity
    {
        object[] GetKeys();

    }


    public interface IEntity<Tkey> : IEntity
    {

        Tkey Id { get; }

    }
}
