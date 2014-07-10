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

namespace SupportTools.DockingForm
{
    public partial class EditActionControl : UserControl
    {
        MenuItemTreeNode _Node;

        #region Constructor
        public EditActionControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public EditActionControl(ref MenuItemTreeNode inNode)
        {
            _Node = inNode;
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            GetDataFromNode(ref inNode);
        }
        #endregion

        #region Functions
        public void GetDataFromNode(ref MenuItemTreeNode inNode)
        {
            _Node = inNode;
            if (_Node.Item is MenuAction)
            {
                var action = (MenuAction)_Node.Item;
                textBoxName.Text = action.Name;
                checkBoxIsMultiselect.Checked = action.IsMultiSelect;
                textBoxCommand.Text = action.Command;
                textBoxParameters.Text = action.Parameters;
                comboBoxLocation.SelectedIndex = comboBoxLocation.Items.IndexOf(action.ExecutionLocation);
            }
        }
        #endregion

        #region Event functions
        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            if (null != _Node.Text && _Node.Text.Equals(textBoxName.Text))
            {
                return;
            }
            _Node.Text = textBoxName.Text;
            _Node.IsModified = true;
        }

        private void checkBoxIsMultiselect_CheckedChanged(object sender, EventArgs e)
        {
            var action = (MenuAction)_Node.Item;
            if (action.IsMultiSelect == checkBoxIsMultiselect.Checked)
            {
                return;
            }
            action.IsMultiSelect = checkBoxIsMultiselect.Checked;
            _Node.IsModified = true;
        }

        private void textBoxCommand_TextChanged(object sender, EventArgs e)
        {
            var action = (MenuAction)_Node.Item;
            if (null != action.Command && action.Command.Equals(textBoxCommand.Text))
            {
                return;
            } 
            action.Command = textBoxCommand.Text;
            _Node.IsModified = true;
        }

        private void textBoxParameters_TextChanged(object sender, EventArgs e)
        {
            var action = (MenuAction)_Node.Item;
            if (null != action.Parameters && action.Parameters.Equals(textBoxParameters.Text))
            {
                return;
            }
            action.Parameters = textBoxParameters.Text;
            _Node.IsModified = true;
        }

        private void comboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            var action = (MenuAction)_Node.Item;
            if (null != action.ExecutionLocation && action.ExecutionLocation.Equals(comboBoxLocation.SelectedItem.ToString()))
            {
                return;
            }
            action.ExecutionLocation = comboBoxLocation.SelectedItem.ToString();
            _Node.IsModified = true;
        }
        #endregion
    }
}
