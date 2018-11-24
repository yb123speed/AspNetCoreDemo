using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OidcSample.Models
{
    public class ProcessConsentResult
    {
        public string ReturnUrl { get; set; }
        public bool IsRedirct => ReturnUrl != null;

        public string ValidationError { get; set; }

        public ConsentViewModel ViewModel { get; set; }
    }
}
