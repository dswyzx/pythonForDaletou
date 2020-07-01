using System;
using System.Text;

namespace DotNetCoreTest
{
    public class HTTPHelper
    {
        public static string GetHTMLByURL(string url)
        {
            try
            {
                System.Net.WebRequest wRequest = System.Net.WebRequest.Create(url);
                wRequest.ContentType = "text/html; charset=utf-8";
                wRequest.Method = "get";
                wRequest.UseDefaultCredentials = true;
                // Get the response instance.
                var task = wRequest.GetResponseAsync();
                System.Net.WebResponse wResp = task.Result;
                System.IO.Stream respStream = wResp.GetResponseStream();
                using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding("utf-8")))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("GetHTMLByURL Exception", ex, new { Url = url });
                return string.Empty;
            }
        }



    }


}