using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Chic.Core.Reflection
{
    public static class AssemblyHelper
    {
        public static Assembly[] GetDLLAssemblys(string folderPath, SearchOption searchOption)
        {
            var assemblyFiles = Directory
                .EnumerateFiles(folderPath, "*.*", searchOption)
                .Where(s => s.EndsWith(".dll"));

            return assemblyFiles.Select(Assembly.LoadFile).ToArray();
        }

        public static Assembly[] GetAllAssembliesInFolder(string folderPath, SearchOption searchOption)
        {
            var assemblyFiles = Directory
                .EnumerateFiles(folderPath, "*.*", searchOption)
                .Where(s => s.EndsWith(".dll") || s.EndsWith(".exe"));

            return assemblyFiles.Select(
                Assembly.LoadFile
            ).ToArray();
        }
    }
}
