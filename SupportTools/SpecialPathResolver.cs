/* 
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

using Microsoft.Win32;

namespace SupportTools
{
    public class SpecialPathResolver
    {
        public static SpecialPathResolver Instance
        {
            get { return _Instance ?? (_Instance = new SpecialPathResolver()); }
        } private static SpecialPathResolver _Instance;

        private SpecialPathResolver()
        {
        }

        public string LDMainPath
        {
            get { return _LDMainPath ?? (_LDMainPath = GetLDMainPathFromReg()); }
        } private string _LDMainPath;

        /*
         *  This is not for environment variables, this is for special variables such as
         *  %DTMDIR% or %ldmain%, which both should resolve to the ManagementSuite directory.
         */
        public string ResolveCommandPathVars(string inCommand)
        {
            // Make it all lowercase cause I don't want to worry about case
            // when checking if the command path contains these variables.
            inCommand = inCommand.ToLower();

            // I choose %ldmain% because that is more current and recognized
            // terminology for the managemementsuite folder.
            if (inCommand.Contains("%ldmain%"))
            {
                return inCommand.Replace("%ldmain%", LDMainPath);
            }

            // %dtmdir% is what custom scripts uses so I added it too but it no longer
            // makes sense as dtmdir is old terminology.
            if (inCommand.Contains("%dtmdir%"))
            {
                return inCommand.Replace("%dtmdir%", LDMainPath);
            }
            return inCommand;
        }

        private string GetLDMainPathFromReg()
        {
            const string defaultFilePath = @"C:\Program Files\LANDesk\ManagementSuite\";

            // Try 32 bit registry
            var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\LANDesk\ManagementSuite\Setup");
            if (key != null)
                return key.GetValue("LdmainPath", defaultFilePath).ToString();

            // Try 64 bit registry
            var localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            key = localKey.OpenSubKey(@"SOFTWARE\LANDesk\ManagementSuite\Setup");
            if (key != null)
                return key.GetValue("LdmainPath", defaultFilePath).ToString();

            return defaultFilePath;
        }

    }
}
