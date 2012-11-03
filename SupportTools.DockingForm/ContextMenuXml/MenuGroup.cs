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
using System.Linq;
using System.Xml.Serialization;

namespace SupportTools.ContextMenuXml
{
    [Serializable]
    public class MenuGroup : ContextMenuItem
    {
        #region Member Variables
        private List<ContextMenuItem> _MenuItems = new List<ContextMenuItem>();
        #endregion

        #region Constructors
        /*
		 * The default constructor
 		 */
        public MenuGroup()
        {
        }


        public MenuGroup(String inName, ContextMenuItem inParent)
            : base(inName, inParent)
        {
        }

        public MenuGroup(String inName, MenuGroup inParent)
            : base(inName, inParent)
        {
        }

        #endregion

        #region Properties
        [XmlElement("MenuItem")]
        public List<ContextMenuItem> MenuItems
        {
            get { return _MenuItems; }
            set { _MenuItems = value; }
        }

        #endregion

        #region Functions
        public void OrderByType()
        {
            List<ContextMenuItem> items = new List<ContextMenuItem>(_MenuItems.Count);
            foreach (MenuGroup group in _MenuItems.OfType<MenuGroup>())
            {
                items.Add(group);
                group.OrderByType();
            }
            foreach (MenuAction action in _MenuItems.OfType<MenuAction>())
            {
                items.Add(action);
            } 
            _MenuItems = items;
        }
        #endregion

        #region Enums
        #endregion
    }
}
