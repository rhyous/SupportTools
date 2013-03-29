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
using System.Windows.Forms;

using SupportTools.ContextMenuXml;

namespace SupportTools
{
    class RightClickMenuItem : LANDesk.ManagementSuite.WinConsole.WinConsoleMenuItem
    {
        #region Member variables
        private ContextMenuItem _MenuItem;
        #endregion

        #region Constructors
        public RightClickMenuItem(MenuAction inMenuAction, EventHandler inEventHandler, bool inMultiSelect)
            : base("@" + inMenuAction.Name, inEventHandler, inMultiSelect, true)
        {
            _MenuItem = inMenuAction;
        }

        public RightClickMenuItem(MenuGroup inMenuGroup, MenuItem[] inMenuItemArray)
            : base("@" + inMenuGroup.Name, inMenuItemArray)
        {
            _MenuItem = inMenuGroup;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Track if this was executed already as sometimes right-click tries to execute more than once.
        /// </summary>
        public bool HasExecuted { get; set; }

        public String Command
        {
            get
            {
                if (_MenuItem is MenuAction)
                {
                    MenuAction action = (MenuAction)_MenuItem;
                    return action.Command;
                }
                else
                {
                    return null;
                }
            }
        }

        public String Parameters
        {
            get
            {
                if (_MenuItem is MenuAction)
                {
                    MenuAction action = (MenuAction)_MenuItem;
                    return action.Parameters;
                }
                return null;
            }
            set
            {
                if (_MenuItem is MenuAction)
                {
                    MenuAction action = (MenuAction)_MenuItem;
                    action.Parameters = value;
                }
            }

        }


        public String ExecutionLocation
        {
            get
            {
                if (_MenuItem is MenuAction)
                {
                    MenuAction action = (MenuAction)_MenuItem;
                    return action.ExecutionLocation;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion
    }
}
