using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCClient.Models
{
    public class ClaimsViewModel
    {
        public string Header { get; set; }
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public IEnumerable<KeyValuePair<string, string>> Claims { get; set; }

        public ClaimsViewModel() { }

        public ClaimsViewModel(IEnumerable<KeyValuePair<string, string>> claims)
        {
            Claims = claims;
        }
    }
}