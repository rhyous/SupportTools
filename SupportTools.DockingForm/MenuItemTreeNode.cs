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
using System.Windows.Forms;
using SupportTools.ContextMenuXml;

namespace SupportTools.DockingForm
{
    public class MenuItemTreeNode : TreeNode
    {
        #region Member Variables
        private readonly ContextMenuItem _Item;
        #endregion

        #region Constructors

        /*
		 * The default constructor
 		 */
        public MenuItemTreeNode()
        {
            ImageIndex = SelectedImageIndex = (int)MenuItemType.Group;

        }

        public MenuItemTreeNode(ref ContextMenuItem inItem)
        {
            _Item = inItem;
            Text = _Item.Name;
            if (ItemType == MenuItemType.Group)
            {
                ImageIndex = SelectedImageIndex = (int)MenuItemType.Group;
                CreateMenuItems();
            }
            else if (ItemType == MenuItemType.Action)
            {
                ImageIndex = SelectedImageIndex = (int)MenuItemType.Action;
            }
        }

        #endregion

        #region Properties
        new public string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                _Item.Name = value;
            }
        }


        /// <summary>
        /// We want to use a referenc to set the item, and we can't use a ref here,
        /// so we will have a separate set function.
        /// </summary>
        public ContextMenuItem Item
        {
            get { return _Item; }
        }

        public MenuItemType ItemType
        {
            get
            {
                if (_Item is MenuAction)
                {
                    return MenuItemType.Action;
                }
                if (_Item is MenuGroup)
                {
                    return MenuItemType.Group;
                }
                throw new Exception("Invalid type: " + _Item.GetType());
            }
        }

        public bool IsModified
        {
            get { return _Item.IsModified; }
            set
            {
                _Item.IsModified = value;
                GetContextMenuEditor().DisplayLabelChanges(value);
            }
        }
        #endregion

        #region Functions
        private ContextMenuEditor GetContextMenuEditor()
        {
            var temp = TreeView.Parent;
            while (!(temp is ContextMenuEditor))
            {
                temp = temp.Parent;
            }
            return (ContextMenuEditor)temp;
        }


        private void CreateMenuItems()
        {
            var menuGroup = _Item as MenuGroup;
            if (menuGroup != null)
            {
                var group = menuGroup;
                foreach (var node in @group.MenuItems.Select(item => new MenuItemTreeNode(ref item)))
                {
                    Nodes.Add(node);
                }
            }
        }

        #endregion

        #region Enums
        public enum MenuItemType
        {
            Group,
            Action
        }
        #endregion
    }
}
