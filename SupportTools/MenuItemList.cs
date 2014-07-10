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
using System.Linq;
using LANDesk.ManagementSuite.WinConsole;
using SupportTools.ContextMenuXml;

namespace SupportTools
{
    class MenuItemList
    {
        private readonly RightClickMenuItem[] _MenuItem;
        private readonly EventHandler _EventHandler;

        /*
         * MenuItemListFromXML expects a string containing the full path to an xml document as
         * its only parameter.
         */
        public MenuItemList(ComputerContextMenu inComputerContextMenu, Computer inComputer, EventHandler inEventHandler)
        {
            _EventHandler = inEventHandler;
            _MenuItem = GetSupportToolsMenuItemArrayFromContextMenuItem(inComputerContextMenu, inComputer);
        }

        private RightClickMenuItem CreateSupportToolsMenuItemFromContextMenuItem(ContextMenuItem inItem, Computer inComputer, EventHandler eh)
        {
            var item = inItem as MenuGroup;
            if (item != null)
            {
                return new RightClickMenuItem(item, GetSupportToolsMenuItemArrayFromContextMenuItem(item, inComputer));
            }

            var menuAction = inItem as MenuAction;
            if (menuAction != null)
            {
                var action = menuAction;
                return new RightClickMenuItem(action, eh, action.IsMultiSelect);
            }

            return null;
        }

        private RightClickMenuItem[] GetSupportToolsMenuItemArrayFromContextMenuItem(MenuGroup inGroup, Computer inComputer)
        {
            var preRetVal = inGroup.MenuItems.Select(item => CreateSupportToolsMenuItemFromContextMenuItem(item, inComputer, _EventHandler)).ToList();

            var retVal = new RightClickMenuItem[preRetVal.Count];
            var i = 0;
            foreach (RightClickMenuItem stmi in preRetVal)
            {
                retVal[i++] = stmi;
            }
            return retVal;
        }

        public RightClickMenuItem[] GetSupportToolsMenuItemArray()
        {
            return _MenuItem;
        }

    }
}
