using SmartBazaar.Web.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    public class LangDictionaryListViewModel
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string Key { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FieldRequired")]
        public string Value { get; set; }
    }

    public static class LangDictionaryDefaults
    {
        public static List<LangDictionaryListViewModel> Get(int bookId)
        {
            return Langs.GetKeys().Select(s => new LangDictionaryListViewModel { Key = s, BookId = bookId }).ToList();
        }
    }
}