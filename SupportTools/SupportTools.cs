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
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;

using LANDesk.ManagementSuite.Database;
using LANDesk.ManagementSuite.UserManagement.Business;
using LANDesk.ManagementSuite.WinConsole;
using SupportTools.ContextMenuXml;
using System.Web;

namespace SupportTools
{
    public class SupportTools : LANDesk.ManagementSuite.WinConsole.Tools.ToolForm
    {
        static bool _IsHandlerAdded = false;
        WinConsoleMenuItem[] _MenuItems;
        String _LDMainPath;
        String _XmlFileName;
        EventHandler _EventHandler;

        public SupportTools()
        {
            _LDMainPath = GetLDMainPathFromReg();
            _XmlFileName = _LDMainPath + @"SupportTools\SupportTools.xml";
            if (_IsHandlerAdded == false)
            {
                Computer.AddingContextMenus += new LANDesk.ManagementSuite.WinConsole.Computer.AddContextMenusHandler(Computer_AddSupportToolsMenuHandler);
                _IsHandlerAdded = true;
            }
            _EventHandler = new EventHandler(HandleRightClickSelectMenuItem);
        }

        private void Computer_AddSupportToolsMenuHandler(object inSender, WinConsoleContextMenu inMenu)
        {
            ComputerContextMenu contextMenu = Serializer.DeserializeFromXML<ComputerContextMenu>(_XmlFileName);
            MenuItemList MILFX = new MenuItemList(contextMenu, (Computer)inSender, _EventHandler);
            _MenuItems = MILFX.GetSupportToolsMenuItemArray();
            for (int i = 0; i < _MenuItems.Length; i++)
            {
                inMenu.Add(WinConsoleMenuItem.Separator);
                inMenu.Add(_MenuItems[i]);
            }
            inMenu.Add(WinConsoleMenuItem.Separator);

        }

        private bool UserHasRemoteControlRights()
        {
            bool bRet = false;
            try
            {
                if (Global.MainForm.NTUser.CheckRights(LDMSRights.ExecuteProgramsEdit))
                {
                    Trace.WriteLine(System.Diagnostics.Process.GetCurrentProcess().StartInfo.UserName + "has RC Rights");
                    bRet = true;
                }
            }
            catch (Exception)
            {
                Trace.WriteLine("Exception on the get db call");
                bRet = false;
            }

            Trace.WriteLine("GetUserRights Return " + bRet.ToString() + " for " + System.Diagnostics.Process.GetCurrentProcess().StartInfo.UserName);
            return bRet;

        }

        private void HandleRightClickSelectMenuItem(object inSender, EventArgs inEventArgs)
        {
            RightClickMenuItem stMenuItem = inSender as RightClickMenuItem;

            // When executing a multi-select action, this even fires twices with the same object.
            // This fixes that.
            if (stMenuItem.HasExecuted)
                return;

            stMenuItem.Parameters = checkForPrompt(stMenuItem.Parameters);
            if (stMenuItem.Parameters.Equals("Cancel action!"))
            {
                return;
            }
            WinconsoleEventArgs args = inEventArgs as WinconsoleEventArgs;
            foreach (Computer c in args.Nodes)
            {
                if (stMenuItem.ExecutionLocation.ToLower().Equals("core"))
                {
                    // Should always execute on the Core server, even if launched from a
                    // Remote Console, but it is not implemented yet.
                    // Currently this value acts the same as console.
                    ExecuteOnConsole(stMenuItem, c);
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
                        String UserLoggedIntoTheConsole = Global.MainForm.User.UserName; ;
                        String ConsoleProcessUser = GetConsoleExeProcessUser();

                        MessageBox.Show("You must have 'Remote Control' rights to execute commands remotely on this device.\n\n"
                                        + "User logged into console: "
                                        + UserLoggedIntoTheConsole);
                    }
                }
            }
            // Mark the object as having been executed.
            stMenuItem.HasExecuted = true;
        }

        private String GetConsoleExeProcessUser()
        {
            String username = "";
            String userdomain = "";
            System.Collections.Specialized.StringDictionary sd = Process.GetCurrentProcess().StartInfo.EnvironmentVariables;
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
            else
            {
                return userdomain + @"\" + username;
            }

        }

        private String GetLDMainPathFromReg()
        {
            string FilePath = @"C:\Program Files\LANDesk\ManagementSuite\";
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\LANDesk\ManagementSuite\Setup");
            return key.GetValue("LdmainPath", FilePath).ToString();
        }

        private void ExecuteOnCore(RightClickMenuItem inMenuItem, Computer inComputer)
        {
            // Change this when we add the ability to execute on Core from a remote console
            ExecuteOnConsole(inMenuItem, inComputer);
        }

        private void ExecuteOnConsole(RightClickMenuItem inMenuItem, Computer inComputer)
        {
            String parameters = checkForPrompt(inMenuItem.Parameters);
            if (parameters.Equals("Cancel action!"))
            {
                return;
            }
            ExecuteItem(ResolveBNF(resolveCommandPathVars(inMenuItem.Command), inComputer.ID), ResolveBNF(parameters, inComputer.ID));
        }


        private void ExecuteOnClient(RightClickMenuItem inMenuItem, Computer inComputer)
        {
            ShutdownRebootForm.Go(inComputer, ShutdownReboot.CommandType.remoteexec, resolveCommandPathVars(inMenuItem.Command), ResolveBNF(inMenuItem.Parameters, inComputer.ID));
        }

        private void ExecuteItem(string inCommand, string inParameter)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(inCommand, inParameter);
            startInfo.WorkingDirectory = _LDMainPath;
            startInfo.CreateNoWindow = true;
            Process.Start(startInfo);
            //Process.Start(command, parameter);
            return;
        }

        private string checkForPrompt(String inParameter)
        {
            if (String.IsNullOrEmpty(inParameter))
                return inParameter;

            if (inParameter.ToLower().Contains("%prompt"))
            {
                // Make sure that %prompt:Name of value to get% is replaced regardless of whether it is upper case
                // or lower case.  We do this by remove %Promp% and in any case and replacing
                // it with %prompt% all lowercase, without affecting the case of other parameters,
                // because some commands have case sensitive parameters.
                MatchCollection mc;
                List<string> allPrompts = new List<string>();
                List<int> matchposition = new List<int>();


                // Create a new Regex object and define the regular expression.
                Regex r = new Regex("%prompt:([^%]*%)");
                // Use the Matches method to find all matches in the input string.
                mc = r.Matches(inParameter);
                // Loop through the match collection to retrieve all 
                // matches and positions.
                for (int i = 0; i < mc.Count; i++)
                {
                    // Add the match string to the string array.   
                    allPrompts.Add(mc[i].Value.Substring("%prompt:".Length, mc[i].Length - "%prompt:".Length - 1));
                    // Record the character position where the match was found.
                    matchposition.Add(mc[i].Index);

                }
                DataPrompt myForm = new DataPrompt(allPrompts);
                myForm.ShowDialog();
                List<string> tempStrList = myForm.getMessageText();

                if (tempStrList != null)
                {
                    foreach (string s in tempStrList)
                    {
                        if (String.IsNullOrEmpty(s))
                        {
                            return "Cancel action!";
                        }
                    }
                }
                //Replace each prompt in the parameter with the userinput value.
                string szNewParameter = inParameter;
                for (int i = 0; i < allPrompts.Count; i++)
                {
                    szNewParameter = szNewParameter.Replace(mc[i].Value, "\"" + HttpUtility.HtmlEncode(tempStrList[i]) + "\"");
                }
                return szNewParameter;
            }
            return inParameter;
        }

        /*
 *  This is not for environment variables, this is for special variables such as
 *  %DTMDIR% or %ldmain%, which both should resolve to the ManagementSuite directory.
 */
        private string resolveCommandPathVars(String inCommand)
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

        private string ResolveBNF(string inParameter, int inID)
        {
            int startLoc = 0, loc1, loc2;
            ArrayList bnflist = new ArrayList();

            try
            {
                if ((loc1 = inParameter.IndexOf('%')) == -1)
                {
                    return inParameter;
                }
                if ((loc2 = inParameter.IndexOf('%', loc1 + 1)) == -1)
                {
                    return inParameter;
                }
            }
            catch
            {
                return inParameter;
            }

            bool isBNF = true;
            string temp = inParameter;
            string ret = inParameter;
            string bnf = "";
            int i = 0;

            // Since we have already checked loc1 and loc2, we will always do this one,
            // so we will use 'do' iterator.
            do
            {
                if (temp.Substring(loc1, (loc2 - loc1)).Contains("."))
                {
                    isBNF = true;
                    bnf = temp.Substring(loc1 + 1, loc2 - loc1 - 1);
                    bnflist.Add(bnf);
                    ret = temp.Substring(0, loc1);
                    ret += string.Format("{{{0}}}", i.ToString());
                    ret += temp.Substring(loc2 + 1);
                    temp = ret;
                    startLoc = temp.IndexOf("{" + i++ + "}") + 3;

                }
                else
                {
                    isBNF = false;
                }
                try
                {
                    if (isBNF)
                    {
                        loc1 = temp.IndexOf('%', startLoc);
                    }
                    else
                    {
                        loc1 = temp.IndexOf('%', (startLoc = (loc2 + 1)));
                    }
                    loc2 = temp.IndexOf('%', loc1 + 1);
                }
                catch
                {
                    break;
                }
            } while ((loc1 != -1) && (loc2 != -1));

            ret = GetBNFValue(bnflist, ret, inID);
            return ret;
        }

        private string GetBNFValue(ArrayList inBNFList, string inCommand, int inID)
        {
            object sort = new string[] { "" };
            object group = new string[] { "" };
            inBNFList.TrimToSize();
            object columns = inBNFList.ToArray();
            string Filter = "Computer.ID = ";
            Filter += inID.ToString();

            try
            {
                LanDeskDatabase _database = LanDeskDatabase.Get();

                string sql = _database.IDal.GetSQL(_database.ConnectionString, Filter, ref sort, ref group, ref columns, 0);
                DataRow row = _database.ExecuteRow(sql);
                if (row != null)
                {
                    string val = "";
                    string[] strret = new string[row.ItemArray.Length];
                    for (int k = 0; k < row.ItemArray.Length; k++)
                    {
                        val = row.ItemArray[k].ToString().Trim();
                        val = FixIpAddress(val);
                        strret[k] = val;
                    }

                    inCommand = string.Format(inCommand, strret);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            return inCommand;
        }

        private string FixIpAddress(string inIpAddress)
        {
            string tempString = "";
            string[] bytes = inIpAddress.Split('.');
            if (bytes.Length == 4)
            {
                try
                {
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        byte u = Convert.ToByte(bytes[i]);
                        bytes[i] = u.ToString();
                    }
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        tempString += bytes[i];
                        if (i < bytes.Length - 1)
                            tempString += ".";
                    }
                }
                catch (Exception)
                {
                    tempString = inIpAddress;
                }
            }
            else
                tempString = inIpAddress;
            return tempString;
        }


    }
}
