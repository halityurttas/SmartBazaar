using System;

namespace SmartBazaar.Web.Models.Internal
{
    public class ErrorModel
    {
        public Exception Error { get; set; }
        public string ReturnPage { get; set; }
    }
}