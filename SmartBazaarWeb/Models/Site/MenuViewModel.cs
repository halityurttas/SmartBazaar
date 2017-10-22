using System.Collections.Generic;

namespace SmartBazaar.Web.Models.Site
{
    public class MenuViewModel
    {
        public MenuViewModel()
        {
            SubList = new List<MenuViewModel>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public List<MenuViewModel> SubList { get; set; }
    }
}