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
using System.Drawing;
using System.Windows.Forms;

using Microsoft.Win32;

using LANDesk.ManagementSuite.WinConsole.Tools;

using SupportTools.ContextMenuXml;

namespace SupportTools.DockingForm
{
    public partial class ContextMenuEditor : ToolForm
    {
        ComputerContextMenu _ContextMenu;
        String _XmlPath = @"SupportTools\SupportTools.xml";
        String _LDMainPath;

        #region Constructor
        public ContextMenuEditor()
        {
            _LDMainPath = GetLDMainPathFromReg();
            InitializeComponent();
            CreateImageList();
            ReadXml();
            SetAddRemoveGroupButtonStates(true);
            SetDefaultTreeState();
        }
        #endregion

        #region Functions
        private void CreateImageList()
        {   
            treeViewContextMenu.ImageList = new ImageList();
            treeViewContextMenu.ImageList.Images.Add(Image.FromFile(_LDMainPath + @"\SupportTools\Images\Folder.png"));
            treeViewContextMenu.ImageList.Images.Add(Image.FromFile(_LDMainPath + @"\SupportTools\Images\SupportTools.png"));
        }

        private void ReadXml()
        {
            _ContextMenu = Serializer.DeserializeFromXML<ComputerContextMenu>(_LDMainPath + @"\" + _XmlPath);
            _ContextMenu.OrderByType();
            ContextMenuItem item = _ContextMenu;
            item.Name = "Computer context menu";
            var node = new MenuItemTreeNode(ref item);
            treeViewContextMenu.Nodes.Add(node);
            DisplayLabelChanges(false);
        }

        private void WriteXml()
        {
            Serializer.SerializeToXML<MenuGroup>(_ContextMenu, _LDMainPath + @"\" + _XmlPath);
            DisplayLabelChanges(false);
        }

        private void CreateMenuItems(List<ContextMenuItem> inMenuItems, TreeNode inParent)
        {
            for (var i = 0; i < inMenuItems.Count; i++)
            {
                var item = inMenuItems[i];
                var node = new MenuItemTreeNode(ref item);
                inParent.Nodes.Add(node);
            }
        }


        private void SetAddRemoveGroupButtonStates(bool inState)
        {
            toolStripButtonAddGroup.Enabled = inState;
            toolStripButtonAddAction.Enabled = inState;
            toolStripButtonDelete.Enabled = true;
        }

        public void DisplayLabelChanges(bool b)
        {
            toolStripLabelChanges.Visible = b;
            _ContextMenu.IsModified = b;
            toolStripButtonWrite.Enabled = b;
        }

        private void SetDefaultTreeState()
        {
            treeViewContextMenu.SelectedNode = treeViewContextMenu.Nodes[0];
            treeViewContextMenu.Nodes[0].Expand();
        }

        private String GetLDMainPathFromReg()
        {
            var FilePath = @"C:\Program Files\LANDesk\ManagementSuite\";
            var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\LANDesk\ManagementSuite\Setup");
            return key.GetValue("LdmainPath", FilePath).ToString();
        }
        #endregion

        #region Event functions
        private void toolStripButtonAddGroup_Click(object sender, EventArgs e)
        {
            if (treeViewContextMenu.SelectedNode is MenuItemTreeNode)
            {
                var node = (MenuItemTreeNode)treeViewContextMenu.SelectedNode;
                if (node.Item is MenuGroup)
                {
                    // Add to XML object
                    var group = (MenuGroup)node.Item;
                    ContextMenuItem newGroup = new MenuGroup("<Enter name>",group);
                    group.MenuItems.Add(newGroup);

                    // Add to TreeView
                    var newNode = new MenuItemTreeNode(ref newGroup);
                    node.Nodes.Add(newNode);
                    treeViewContextMenu.SelectedNode = newNode;
                }
            }
            DisplayLabelChanges(true);
        }
        
        private void toolStripButtonRemoveItem_Click(object sender, EventArgs e)
        {
            if (treeViewContextMenu.SelectedNode is MenuItemTreeNode)
            {
                // Cast node and parent node
                if (treeViewContextMenu.SelectedNode is MenuItemTreeNode
                    && treeViewContextMenu.SelectedNode is MenuItemTreeNode)
                {
                    var node = (MenuItemTreeNode)treeViewContextMenu.SelectedNode;
                    var parentNode = (MenuItemTreeNode)node.Parent;

                    // Remove from XML object
                    if (parentNode.Item is MenuGroup)
                    {
                        var group = (MenuGroup)parentNode.Item;
                        group.MenuItems.Remove(node.Item);
                    }

                    // Remove from TreeView
                    node.Remove();
                }
            }
            DisplayLabelChanges(true);
        }

        private void toolStripButtonAddAction_Click(object sender, EventArgs e)
        {
            if (treeViewContextMenu.SelectedNode is MenuItemTreeNode)
            {
                var node = (MenuItemTreeNode)treeViewContextMenu.SelectedNode;
                if (node.Item is MenuGroup)
                {
                    // Add to XML object
                    var group = (MenuGroup)node.Item;
                    ContextMenuItem newAction = new MenuAction("<Enter name>", group);
                    group.MenuItems.Add(newAction);
                    // Add to TreeView
                    var newNode = new MenuItemTreeNode(ref newAction);
                    node.Nodes.Add(newNode);
                    treeViewContextMenu.SelectedNode = newNode;
                }
            }
            DisplayLabelChanges(true);
        }

        private void toolStripButtonRemoveAction_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            treeViewContextMenu.Nodes.Clear();
            ReadXml();
            SetDefaultTreeState();
        }

        private void toolStripButtonWrite_Click(object sender, EventArgs e)
        {
            WriteXml();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.groupBoxEditMenuItem.Controls.Clear();
            var tree = (TreeView)sender;

            if (tree.SelectedNode == tree.TopNode)
            {
                toolStripButtonDelete.Enabled = false;
            }
            else if (tree.SelectedNode is MenuItemTreeNode)
            {
                var node = (MenuItemTreeNode)tree.SelectedNode;
                if (node.ItemType == MenuItemTreeNode.MenuItemType.Group)
                {
                    var editGroupControl = new EditGroupControl(ref node);
                    this.groupBoxEditMenuItem.Controls.Add(editGroupControl);
                    SetAddRemoveGroupButtonStates(true);
                }
                if (node.ItemType == MenuItemTreeNode.MenuItemType.Action)
                {
                    var userControl1 = new EditActionControl(ref node);
                    this.groupBoxEditMenuItem.Controls.Add(userControl1);
                    SetAddRemoveGroupButtonStates(false);
                }
            }
        }
        #endregion
    }
}
