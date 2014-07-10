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
            string result;

            // Create the web request  
            var request = WebRequest.Create(address) as HttpWebRequest;

            // Get response  
            if (request == null) return null;
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                // Get the response stream  
                if (response == null) return null;
                var streamResponse = response.GetResponseStream();
                if (streamResponse == null) return null;
                using (var reader = new StreamReader(streamResponse))
                {
                    // Read the whole contents and return as a string  
                    result = reader.ReadToEnd();
                }
            }
            return result;
        }
    }
}
