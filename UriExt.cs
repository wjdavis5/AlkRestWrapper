using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace ALK.Core
{
    public static class UriExt
    {
        public static Uri AttachParameters(this Uri uri, NameValueCollection parameters)
        {
            var stringBuilder = new StringBuilder();
            string str = "?";
            for (int index = 0; index < parameters.Count; ++index)
            {
                
                stringBuilder.Append(str + parameters.AllKeys[index] + "=" + WebUtility.UrlEncode(parameters[index]));
                str = "&";
            }
            return new Uri(uri + stringBuilder.ToString());
        }
    }
}