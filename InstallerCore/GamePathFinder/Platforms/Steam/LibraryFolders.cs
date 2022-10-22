using System.Collections.Generic;

namespace Timbermod_installer.GamePathFinder.Platforms
{
    public class LibraryFolders<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public string Contentstatsid { get; set; }
    }
}