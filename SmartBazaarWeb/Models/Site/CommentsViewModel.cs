using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Models.Site
{
    public class CommentsListViewModel
    {
        public string UserTitle { get; set; }
        public short Rating { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
    }

    public class CommentsPostViewModel
    {
        public int ProductId { get; set; }
        public short Rating { get; set; }
        public string Description { get; set; }
    }
}