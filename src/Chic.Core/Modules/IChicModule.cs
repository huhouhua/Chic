using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chic.Core.Modules
{
    public interface IChicModule
    {
        void PreInitialize();

        void Initialize();

        void Shutdown();
    }
}
