using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SmartBazaar.Web.Models.Internal;
using System.Linq.Expressions;

namespace SmartBazaar.Web.Helpers.Extensions
{
    public static class CommonSelectLists
    {
        public static void StatusList(this Controller controller, short selected)
        {
            controller.ViewBag.Status = Statuses.StatusList.Select(item =>
                new SelectListItem { Text = item.Value, Value = item.Key.ToString(), Selected = item.Key == selected }).ToList();
        }

        public static void Pair2List<TModel, TProp, TKey>(this Controller controller, Dictionary<TKey, string> dict, Expression<Func<TModel, TProp>> prop, string selected)
        {
            var expression = (MemberExpression)prop.Body;
            string propName = expression.Member.Name;

            controller.ViewData[propName] = dict.Select(item =>
                new SelectListItem { Text = item.Value, Value = item.Key.ToString(), Selected = item.Key.ToString() == selected }).ToList();
        }
    }
}