/*
 * http://developer.yahoo.com/dotnet/howto-xml_cs.html
 */

using System;
using System.IO;
using System.Net;

namespace Rhyous.Agent.Ping.Business
{
    class WebHelper
    {
        public static string GetPageAsString(Uri address)
        {
            string result = "";

            // Create the web request  
            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

            // Get response  
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // Get the response stream  
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    // Read the whole contents and return as a string  
                    result = reader.ReadToEnd();
                }
            }

            return result;
        }
    }
}
