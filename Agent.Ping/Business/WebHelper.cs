﻿/*
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
            var result = "";

            // Create the web request  
            var request = WebRequest.Create(address) as HttpWebRequest;

            // Get response  
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                // Get the response stream  
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    // Read the whole contents and return as a string  
                    result = reader.ReadToEnd();
                }
            }

            return result;
        }
    }
}
