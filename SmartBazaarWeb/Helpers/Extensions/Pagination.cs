using System;
using System.Web.Mvc;
using System.Text;

namespace SmartBazaar.Web.Helpers.Extensions
{
    public static class Pagination
    {
        private const string nav = "<nav>{0}</nav>";
        private const string ul = "<ul class=\"pagination\">{0}</ul>";
        private const string li = "<li>{0}</li>";
        private const string liactive = "<li class=\"active\">{0}</li>";
        private const string pager = "<a href=\"{0}\" aria-label=\"{1}\"><span>{2}</span></a>";
        private const string numeric = "<a href=\"{0}\">{1}</a>";
        public static String Paging(this HtmlHelper helper, string Controller = "", string Action = "", string PageKey = "page", int CurrentPage = 1, int PageSize = AppConfig.PAGE_SIZE, int RecordCount = 0, bool ShowPager = true, bool ShowNumeric = true, Labels Labels = null, Tooltips Tooltips = null)
        {
            int totalPage = (RecordCount - (RecordCount % PageSize)) / PageSize;
            if (RecordCount % PageSize > 0) { totalPage++; }
            string url = "";
            if (!string.IsNullOrWhiteSpace(Controller))
            {
                url = "/" + Controller;
                if (!string.IsNullOrWhiteSpace(Action))
                {
                    url += "/" + Action;
                }
            }
            url = "?" + PageKey + "=";
            System.Collections.Specialized.NameValueCollection qscol = helper.ViewContext.HttpContext.Request.QueryString;
            StringBuilder qses = new StringBuilder();
            for (int i = 0; i < qscol.Count; i++)
            {
                if (qscol.Keys[i] != PageKey)
                {
                    qses.Append("&" + qscol.Keys[i] + "=" + qscol[i]);
                }
            }
            if (Labels == null)
            {
                Labels = new Labels();
            }
            if (Tooltips == null)
            {
                Tooltips = new Tooltips();
            }
            string prevButton = string.Format(pager, url + (CurrentPage == 1 ? 1 : CurrentPage - 1).ToString() + qses.ToString(), Tooltips.Previous, Labels.Previous);
            string nextButton = string.Format(pager, url + (CurrentPage == totalPage ? CurrentPage : CurrentPage + 1) + qses.ToString(), Tooltips.Next, Labels.Next);
            StringBuilder lies = new StringBuilder();
            lies.Append(string.Format(li, prevButton));

            for (int i = 0; i < totalPage; i++)
            {
                if (i == CurrentPage - 1)
                {
                    lies.Append(string.Format(liactive, string.Format(numeric, url + (i + 1).ToString() + qses.ToString(), (i + 1).ToString())));
                }
                else
                {
                    lies.Append(string.Format(li, string.Format(numeric, url + (i + 1).ToString() + qses.ToString(), (i + 1).ToString())));
                }
            }

            lies.Append(string.Format(li, nextButton));
            string pagerdata = string.Format(nav,
                string.Format(ul, lies.ToString())
                );
            if (totalPage == 0)
            {
                pagerdata = "";
            }
            return pagerdata;
        }

    }

    public class Labels
    {
        private string previous = "&laquo;";
        private string next = "&raquo;";

        public string Previous
        {
            get { return previous; }
            set { previous = value; }
        }

        public string Next
        {
            get { return next; }
            set { next = value; }
        }

    }

    public class Tooltips
    {
        private string previous = "Previous";
        private string next = "Next";

        public string Previous
        {
            get { return previous; }
            set { previous = value; }
        }

        public string Next
        {
            get { return next; }
            set { next = value; }
        }

    }
}