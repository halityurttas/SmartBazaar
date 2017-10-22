using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartBazaar.Web.Components.Converters
{
    public static class DynamicCasting
    {
        public static dynamic Cast(Type toType, object fromObj)
        {
            if (toType == typeof(bool))
            {
                return Convert.ToBoolean(fromObj);
            }
            else if (toType == typeof(decimal))
            {
                return Convert.ToDecimal(fromObj);
            }
            else if (toType == typeof(double))
            {
                return Convert.ToDouble(fromObj);
            }
            else if (toType == typeof(float))
            {
                return Convert.ToSingle(fromObj);
            }
            else if (toType == typeof(int))
            {
                return Convert.ToInt32(fromObj);
            }
            else if (toType == typeof(long))
            {
                return Convert.ToInt64(fromObj);
            }
            else if (toType == typeof(short))
            {
                return Convert.ToInt16(fromObj);
            }
            else if (toType == typeof(DateTime))
            {
                return Convert.ToDateTime(fromObj);
            }
            if (toType == typeof(bool?))
            {
                if (fromObj == null) return null;
                return Convert.ToBoolean(fromObj);
            }
            else if (toType == typeof(decimal?))
            {
                if (fromObj == null) return null;
                return Convert.ToDecimal(fromObj);
            }
            else if (toType == typeof(double?))
            {
                if (fromObj == null) return null;
                return Convert.ToDouble(fromObj);
            }
            else if (toType == typeof(float?))
            {
                if (fromObj == null) return null;
                return Convert.ToSingle(fromObj);
            }
            else if (toType == typeof(int?))
            {
                if (fromObj == null) return null;
                return Convert.ToInt32(fromObj);
            }
            else if (toType == typeof(long?))
            {
                if (fromObj == null) return null;
                return Convert.ToInt64(fromObj);
            }
            else if (toType == typeof(short?))
            {
                if (fromObj == null) return null;
                return Convert.ToInt16(fromObj);
            }
            else if (toType == typeof(DateTime?))
            {
                if (fromObj == null) return null;
                return Convert.ToDateTime(fromObj);
            }
            else
            {
                if (fromObj == null) return null;
                return fromObj.ToString();
            }
            
        }
    }
}