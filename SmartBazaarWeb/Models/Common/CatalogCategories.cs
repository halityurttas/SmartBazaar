using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Models.Common
{
    public struct CatalogCategoryFieldNames
    {
        public const string ParentId = "Üst Kategori";
        public const string Title = "Kategori Adı";
        public const string Description = "Detay";
        public const string Pos = "Sıra No";
        public const string Status = "Durum";
        public const string ExternalRef1 = "Referans Kod 1";
        public const string ExternalRef2 = "Referans Kod 2";
        public const string ExternalRef3 = "Referans Kod 3";
        public const string IsDisplayInMenu = "Menüde Göster";
        public const string ImageUrl = "Resim";
        public const string IsDisplayInMainPage = "Ana Sayfada Göster";
    }
}