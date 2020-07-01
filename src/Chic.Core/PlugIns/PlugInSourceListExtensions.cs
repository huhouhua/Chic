using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Chic.Core.PlugIns
{
    public static class PlugInSourceListExtensions
    {
        public static void AddFolder(this PlugInSourceList list, string folder, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            list.Add(new FolderPlugInSource(folder, searchOption));
        }
    }
}
