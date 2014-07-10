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
using System.Collections.Generic;
using System.Xml;
using LANDesk.ManagementSuite.WinConsole;

using SupportTools.ContextMenuXml;

namespace SupportTools
{
    class MenuItemList
    {
        private ComputerContextMenu _inComputerContextMenu;
        private RightClickMenuItem[] _MenuItem;
        private EventHandler _EventHandler;

        /*
         * MenuItemListFromXML expects a String containing the full path to an xml document as
         * its only parameter.
         */
        public MenuItemList(ComputerContextMenu inComputerContextMenu, Computer inComputer, EventHandler inEventHandler)
        {
            _inComputerContextMenu = inComputerContextMenu;
            _EventHandler = inEventHandler;
            _MenuItem = GetSupportToolsMenuItemArrayFromContextMenuItem(inComputerContextMenu, inComputer);
        }

        private int CountChildNodes(XmlNode inNode, bool bCountComments)
        {
            var retVal = 0;
            foreach (XmlNode cn in inNode.ChildNodes)
            {
                if ((bCountComments == false) && (cn.NodeType == XmlNodeType.Comment))
                {
                    continue;
                }
                retVal++;
            }
            return retVal;
        }

        private int CountChildNodesIgnoreComments(XmlNode inNode)
        {
            return CountChildNodes(inNode, false);
        }

        private RightClickMenuItem CreateSupportToolsMenuItemFromContextMenuItem(ContextMenuItem inItem, Computer inComputer, EventHandler eh)
        {
            if (inItem is MenuGroup)
            {
                return new RightClickMenuItem((MenuGroup)inItem, GetSupportToolsMenuItemArrayFromContextMenuItem((MenuGroup)inItem, inComputer));
            }

            if (inItem is MenuAction)
            {
                var action = (MenuAction)inItem;
                return new RightClickMenuItem(action, eh, action.IsMultiSelect);
            }

            return null;
        }

        private RightClickMenuItem[] GetSupportToolsMenuItemArrayFromContextMenuItem(MenuGroup inGroup, Computer inComputer)
        {
            var ComputerOSType = inComputer.Type;

            var preRetVal = new List<RightClickMenuItem>();

            foreach (ContextMenuItem item in inGroup.MenuItems)
            {                
                preRetVal.Add(CreateSupportToolsMenuItemFromContextMenuItem(item, inComputer, _EventHandler));
            }

            var RetVal = new RightClickMenuItem[preRetVal.Count];
            var i = 0;
            foreach (RightClickMenuItem stmi in preRetVal)
            {
                RetVal[i++] = stmi;
            }
            return RetVal;
        }

        public RightClickMenuItem[] GetSupportToolsMenuItemArray()
        {
            return _MenuItem;
        }

    }
}
