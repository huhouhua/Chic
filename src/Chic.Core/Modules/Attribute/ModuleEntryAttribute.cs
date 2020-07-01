using System;
using System.Collections.Generic;
using System.Text;

namespace Chic.Core.Modules
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public  class ModuleEntryAttribute : Attribute
    {
        private Type _type { get; set; }

        public ModuleEntryAttribute(Type type)
        {
            _type = type;
        }

        public Type GetModule()
        {
            return _type;
        }
    }
}
