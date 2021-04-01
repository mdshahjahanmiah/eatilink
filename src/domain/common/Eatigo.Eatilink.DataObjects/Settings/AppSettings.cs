using System;
using System.Collections.Generic;
using System.Text;

namespace Eatigo.Eatilink.DataObjects.Settings
{
    public class AppSettings
    {
        public DatabaseSettings DatabaseSettings { get; set; }
        public Token JsonWebTokens { get; set; }
    }
}
