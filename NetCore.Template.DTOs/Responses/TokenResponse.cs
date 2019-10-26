using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Template.DTOs.Responses
{
    public class TokenResponse
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
    }
}
