using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Template.Configuration
{
    public class SecurityConfiguration
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int MinutesToExpiration { get; set; }
        public string SigningKey { get; set; }
    }
}
