using System;
using System.Collections.Generic;
using System.Text;

namespace Eatigo.Eatilink.DataObjects.Settings
{
    public class MemoryCache
    {
        public string CacheKey { get; set; }
        public int RefreshTimeInSeconds { get; set; }
    }
}
