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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using LANDesk.ManagementSuite.WinConsole.Tools;
using Microsoft.Win32;

using LANDesk.ManagementSuite.UserManagement.Business;
using LANDesk.ManagementSuite.WinConsole;
using SupportTools.ContextMenuXml;
using System.Web;

namespace SupportTools
{
    public class SupportTools : ToolForm
    {
        static bool _IsHandlerAdded;
        RightClickMenuItem[] _MenuItems;
        readonly String _LDMainPath;
        readonly String _XmlFileName;
        readonly EventHandler _EventHandler;

        public SupportTools()
        {
            _LDMainPath = GetLDMainPathFromReg();
            _XmlFileName = _LDMainPath + @"SupportTools\SupportTools.xml";
            if (_IsHandlerAdded == false)
            {
                Computer.AddingContextMenus += Computer_AddSupportToolsMenuHandler;
                _IsHandlerAdded = true;
            }
            _EventHandler = HandleRightClickSelectMenuItem;
        }

        public BnfResolver BnfResolver
        {
            get { return _BnfResolver ?? (_BnfResolver = new BnfResolver()); }
            set { _BnfResolver = value; }
        } private BnfResolver _BnfResolver;


        private void Computer_AddSupportToolsMenuHandler(object inSender, WinConsoleContextMenu inMenu)
        {
            var contextMenu = Serializer.DeserializeFromXML<ComputerContextMenu>(_XmlFileName);
            var milfx = new MenuItemList(contextMenu, (Computer)inSender, _EventHandler);
            _MenuItems = milfx.GetSupportToolsMenuItemArray();
            foreach (var t in _MenuItems)
            {
                inMenu.Add(WinConsoleMenuItem.Separator);
                inMenu.Add(t);
            }
            inMenu.Add(WinConsoleMenuItem.Separator);

        }

        private bool UserHasRemoteControlRights()
        {
            var bRet = false;
            try
            {
                if (Global.MainForm.NTUser.CheckRights(LDMSRights.ExecuteProgramsEdit))
                {
                    Trace.WriteLine(Process.GetCurrentProcess().StartInfo.UserName + "has RC Rights");
                    bRet = true;
                }
            }
            catch (Exception)
            {
                Trace.WriteLine("Exception on the get db call");
                bRet = false;
            }

            Trace.WriteLine("GetUserRights Return " + bRet + " for " + Process.GetCurrentProcess().StartInfo.UserName);
            return bRet;

        }

        private void HandleRightClickSelectMenuItem(object inSender, EventArgs inEventArgs)
        {
            var stMenuItem = inSender as RightClickMenuItem;
            if (stMenuItem == null) return;

            // When executing a multi-select action, this even fires twices with the same object.
            // This fixes that.
            if (stMenuItem.HasExecuted)
                return;

            stMenuItem.Parameters = CheckForPrompt(stMenuItem.Parameters);
            if (stMenuItem.Parameters.Equals("Cancel action!"))
            {
                return;
            }
            var args = inEventArgs as WinconsoleEventArgs;
            if (args == null) return;
            foreach (Computer c in args.Nodes)
            {
                if (stMenuItem.ExecutionLocation.ToLower().Equals("core"))
                {
                    // Should always execute on the Core server, even if launched from a
                    // Remote Console, but it is not implemented yet.
                    // Currently this value acts the same as console.
                    ExecuteOnCore(stMenuItem, c);
                }
                else if (stMenuItem.ExecutionLocation.ToLower().Equals("console"))
                {
                    // Executes on the Local Console
                    ExecuteOnConsole(stMenuItem, c);

                }
                else if (stMenuItem.ExecutionLocation.ToLower().Equals("client"))
                {
                    // Executes remotely on the client
                    if (UserHasRemoteControlRights())
                    {
                        ExecuteOnClient(stMenuItem, c);
                    }
                    else
                    {
                        var userLoggedIntoTheConsole = Global.MainForm.User.UserName;
                        var consoleProcessUser = GetConsoleExeProcessUser();

                        MessageBox.Show(@"You must have 'Remote Control' rights to execute commands remotely on this device."
                                            + Environment.NewLine + Environment.NewLine
                                            + @"User logged into console: " + userLoggedIntoTheConsole
                                            + Environment.NewLine
                                            + @"User running the console.exe process: " + consoleProcessUser);
                    }
                }
            }
            // Mark the object as having been executed.
            stMenuItem.HasExecuted = true;
        }

        private String GetConsoleExeProcessUser()
        {
            var username = "";
            var userdomain = "";
            var sd = Process.GetCurrentProcess().StartInfo.EnvironmentVariables;
            foreach (DictionaryEntry de in sd)
            {
                if (de.Key.ToString().ToLower().Equals("username"))
                {
                    username = de.Value.ToString();
                }
                if (de.Key.ToString().ToLower().Equals("userdomain"))
                {
                    userdomain = de.Value.ToString();
                }
            }
            if (userdomain.Equals(""))
            {
                return username;
            }
            return userdomain + @"\" + username;
        }

        private String GetLDMainPathFromReg()
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

        private void ExecuteOnCore(RightClickMenuItem inMenuItem, Computer inComputer)
        {
            // Change this when we add the ability to execute on Core from a remote console
            ExecuteOnConsole(inMenuItem, inComputer);
        }

        private void ExecuteOnConsole(RightClickMenuItem inMenuItem, Computer inComputer)
        {
            var parameters = CheckForPrompt(inMenuItem.Parameters);
            if (parameters.Equals("Cancel action!"))
            {
                return;
            }
            ExecuteItem(BnfResolver.ResolveBnf(ResolveCommandPathVars(inMenuItem.Command), inComputer.ID), BnfResolver.ResolveBnf(parameters, inComputer.ID));
        }


        private void ExecuteOnClient(RightClickMenuItem inMenuItem, Computer inComputer)
        {
            ShutdownRebootForm.Go(inComputer, ShutdownReboot.CommandType.remoteexec, ResolveCommandPathVars(inMenuItem.Command), BnfResolver.ResolveBnf(inMenuItem.Parameters, inComputer.ID));
        }

        private void ExecuteItem(string inCommand, string inParameter)
        {
            var startInfo = new ProcessStartInfo(inCommand, inParameter)
            {
                WorkingDirectory = _LDMainPath,
                CreateNoWindow = true
            };
            Process.Start(startInfo);
            //Process.Start(command, parameter);
        }

        private string CheckForPrompt(String inParameter)
        {
            if (String.IsNullOrEmpty(inParameter))
                return string.Empty;

            if (inParameter.ToLower().Contains("%prompt"))
            {
                // Make sure that %prompt:Name of value to get% is replaced regardless of whether it is upper case
                // or lower case.  We do this by remove %Promp% and in any case and replacing
                // it with %prompt% all lowercase, without affecting the case of other parameters,
                // because some commands have case sensitive parameters.
                var allPrompts = new List<string>();
                var matchposition = new List<int>();


                // Create a new Regex object and define the regular expression.
                var r = new Regex("%prompt:([^%]*%)");
                // Use the Matches method to find all matches in the input string.
                var mc = r.Matches(inParameter);
                // Loop through the match collection to retrieve all 
                // matches and positions.
                for (var i = 0; i < mc.Count; i++)
                {
                    // Add the match string to the string array.   
                    allPrompts.Add(mc[i].Value.Substring("%prompt:".Length, mc[i].Length - "%prompt:".Length - 1));
                    // Record the character position where the match was found.
                    matchposition.Add(mc[i].Index);

                }
                var myForm = new DataPrompt(allPrompts);
                myForm.ShowDialog();
                var tempStrList = myForm.getMessageText();

                if (tempStrList != null)
                {
                    if (tempStrList.Any(String.IsNullOrEmpty))
                    {
                        return "Cancel action!";
                    }
                    //Replace each prompt in the parameter with the userinput value.
                    var szNewParameter = inParameter;
                    for (var i = 0; i < allPrompts.Count; i++)
                    {
                        szNewParameter = szNewParameter.Replace(mc[i].Value, "\"" + HttpUtility.HtmlEncode(tempStrList[i]) + "\"");
                    }
                    return szNewParameter;
                }
            }
            return inParameter;
        }

        /*
         *  This is not for environment variables, this is for special variables such as
         *  %DTMDIR% or %ldmain%, which both should resolve to the ManagementSuite directory.
         */
        private string ResolveCommandPathVars(String inCommand)
        {
            // Make it all lowercase cause I don't want to worry about case
            // when checking if the command path contains these variables.
            inCommand = inCommand.ToLower();

            // I choose %ldmain% because that is more current and recognized
            // terminology for the managemementsuite folder.
            if (inCommand.Contains("%ldmain%"))
            {
                return inCommand.Replace("%ldmain%", _LDMainPath);
            }

            // %dtmdir% is what custom scripts uses so I added it too but it no longer
            // makes sense as dtmdir is old terminology.
            if (inCommand.Contains("%dtmdir%"))
            {
                return inCommand.Replace("%dtmdir%", _LDMainPath);
            }
            return inCommand;
        }
    }
}
