﻿/* 
 * Copyright (c) Rhyous, Jared A. Barneck
 * All rights reserved.
 * Redistribution and use in source and binary forms, with or without modification, are permitted
 * provided that the following conditions are met:
 * 
 *    * Redistributions of source code must retain the above copyright notice, this list of 
 *      conditions and the following disclaimer.
 *      
 *    * Redistributions in binary form must reproduce the above copyright notice, this list
 *      of conditions and the following disclaimer in the documentation and/or other materials
 *      provided with the distribution.
 *    
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR 
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
 * FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR 
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR 
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY 
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR 
 * OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
 * POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using Rhyous.Agent.Ping.Model;

namespace Rhyous.Agent.Ping.Business
{
    class LDPingAction
    {
        #region Member Variables

        private const string LDPingTemplatePath = "http://{0}:9595/allowed/ldping";

        #endregion

        #region Functions
        public static LDPing AgentPing(string inIPAddress)
        {
            var agentPath = string.Format(LDPingTemplatePath, inIPAddress);
            string xml;

            try
            {
                xml = WebHelper.GetPageAsString(new Uri(agentPath));
            }
            catch (Exception)
            {
                return null;
            }


            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }

            try
            {
                return Serializer.DeserializeFromXml<LDPing>(ref xml);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}
