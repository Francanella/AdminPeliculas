using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Helpers
{
    public class UriHelper
    {
        public static string GetHost()
        {
            Uri myuri = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
            string pathQuery = myuri.PathAndQuery;

            string hostName = hostName = myuri.ToString();
            if (pathQuery != "/")
                hostName = hostName.Replace(pathQuery, "");
            else if (hostName.Last() == '/')
                hostName = hostName.Substring(0, hostName.Length - 1);

            return hostName;
        }
    }
}