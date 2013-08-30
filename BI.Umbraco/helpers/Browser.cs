using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BI.Umbraco.helpers
{
    public class Browser
    {
        public static bool IsMobile(System.Web.HttpRequest request)
        {
            var useragent = request.UserAgent;
            if (useragent != null)
            {
                if (IsHandHeld(request) && (useragent.ToLower().Contains("iphone") || useragent.ToLower().Contains("ipod") || useragent.ToLower().Contains("android")))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsHandHeld(System.Web.HttpRequest request)
        {

            if (request.QueryString["handheld"] != null && request.QueryString["handheld"].Equals("true"))
            {
                return true;
            }
            var useragent = request.UserAgent;
            var isAndroid = false;

            if (useragent != null)
            {
                if (useragent.ToLower().Contains("android"))
                {
                    isAndroid = true;
                }
            }

            if (request.Browser.IsMobileDevice || isAndroid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}