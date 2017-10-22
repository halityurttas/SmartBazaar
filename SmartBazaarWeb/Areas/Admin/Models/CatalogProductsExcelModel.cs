using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Areas.Admin.Models
{
    public class CatalogProductsExcelModel
    {
        public string UrunKodu { get; set; }
        public string Tanim { get; set; }
        public string Barkod { get; set; }
        public string Marka { get; set; }
        public string Kategori { get; set; }
        public string Resim { get; set; }
        public bool Satilabilir { get; set; }
        public bool Ozellikli { get; set; }
        public bool UcretsizNakliye { get; set; }
        public bool AnaSayfadaGoster { get; set; }
        public bool Yeni { get; set; }
        public bool NakliyeEdilebilir { get; set; }
        public int MaksimumTaksit { get; set; }
        public string UreticiUrunKodu { get; set; }
        public string StokKodu { get; set; }
        public string AramaTagi { get; set; }
        public int StokMiktari { get; set; }
        public double Dara { get; set; }
        public decimal Fiyat1 { get; set; }
        public decimal? Fiyat2 { get; set; }
        public decimal? Fiyat3 { get; set; }
        public decimal? Fiyat4 { get; set; }
        public decimal? Fiyat5 { get; set; }
        public string VergiGrubu { get; set; }
        public string MetaTitle { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string KisaAciklama { get; set; }
        public string Detay { get; set; }
        public int Durum { get; set; }
    }
}