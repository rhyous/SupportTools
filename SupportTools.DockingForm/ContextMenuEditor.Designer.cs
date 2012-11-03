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

namespace SupportTools.DockingForm
{
    partial class ContextMenuEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContextMenuEditor));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAddGroup = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAddAction = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonWrite = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelChanges = new System.Windows.Forms.ToolStripLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewContextMenu = new System.Windows.Forms.TreeView();
            this.groupBoxEditMenuItem = new System.Windows.Forms.GroupBox();
            this.toolStrip.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 286);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip.Size = new System.Drawing.Size(662, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAddGroup,
            this.toolStripButtonAddAction,
            this.toolStripButtonDelete,
            this.toolStripButtonRefresh,
            this.toolStripButtonWrite,
            this.toolStripLabelChanges});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(662, 31);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripButtonAddGroup
            // 
            this.toolStripButtonAddGroup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddGroup.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddGroup.Image")));
            this.toolStripButtonAddGroup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddGroup.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.toolStripButtonAddGroup.Name = "toolStripButtonAddGroup";
            this.toolStripButtonAddGroup.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonAddGroup.Text = "Add Group";
            this.toolStripButtonAddGroup.ToolTipText = "Add context menu group.";
            this.toolStripButtonAddGroup.Click += new System.EventHandler(this.toolStripButtonAddGroup_Click);
            // 
            // toolStripButtonAddAction
            // 
            this.toolStripButtonAddAction.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAddAction.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddAction.Image")));
            this.toolStripButtonAddAction.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddAction.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.toolStripButtonAddAction.Name = "toolStripButtonAddAction";
            this.toolStripButtonAddAction.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonAddAction.Text = "Add Action";
            this.toolStripButtonAddAction.ToolTipText = "Add a context menu action.";
            this.toolStripButtonAddAction.Click += new System.EventHandler(this.toolStripButtonAddAction_Click);
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDelete.Image")));
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonDelete.Text = "Delete item.";
            this.toolStripButtonDelete.ToolTipText = "Delete context menu item.";
            this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonRemoveItem_Click);
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRefresh.Image")));
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonRefresh.Text = "Revert changes.";
            this.toolStripButtonRefresh.ToolTipText = "Revert changes since last write.";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // toolStripButtonWrite
            // 
            this.toolStripButtonWrite.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonWrite.Enabled = false;
            this.toolStripButtonWrite.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonWrite.Image")));
            this.toolStripButtonWrite.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonWrite.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.toolStripButtonWrite.Name = "toolStripButtonWrite";
            this.toolStripButtonWrite.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonWrite.Text = "Apply and write changes.";
            this.toolStripButtonWrite.Click += new System.EventHandler(this.toolStripButtonWrite_Click);
            // 
            // toolStripLabelChanges
            // 
            this.toolStripLabelChanges.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabelChanges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.toolStripLabelChanges.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.toolStripLabelChanges.Name = "toolStripLabelChanges";
            this.toolStripLabelChanges.Size = new System.Drawing.Size(204, 28);
            this.toolStripLabelChanges.Text = "Changes have not yet been applied!";
            this.toolStripLabelChanges.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 31);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewContextMenu);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBoxEditMenuItem);
            this.splitContainer1.Size = new System.Drawing.Size(662, 255);
            this.splitContainer1.SplitterDistance = 219;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 5;
            // 
            // treeViewContextMenu
            // 
            this.treeViewContextMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewContextMenu.ItemHeight = 24;
            this.treeViewContextMenu.Location = new System.Drawing.Point(0, 0);
            this.treeViewContextMenu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.treeViewContextMenu.Name = "treeViewContextMenu";
            this.treeViewContextMenu.Size = new System.Drawing.Size(219, 255);
            this.treeViewContextMenu.TabIndex = 4;
            this.treeViewContextMenu.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // groupBoxEditMenuItem
            // 
            this.groupBoxEditMenuItem.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxEditMenuItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxEditMenuItem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBoxEditMenuItem.Location = new System.Drawing.Point(0, 0);
            this.groupBoxEditMenuItem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBoxEditMenuItem.Name = "groupBoxEditMenuItem";
            this.groupBoxEditMenuItem.Padding = new System.Windows.Forms.Padding(4, 8, 4, 4);
            this.groupBoxEditMenuItem.Size = new System.Drawing.Size(440, 255);
            this.groupBoxEditMenuItem.TabIndex = 5;
            this.groupBoxEditMenuItem.TabStop = false;
            this.groupBoxEditMenuItem.Text = "Edit Menu Item";
            // 
            // ContextMenuEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 308);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(647, 287);
            this.Name = "ContextMenuEditor";
            this.Text = "Computer Right-Click Menu Editor";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddGroup;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddAction;
        private System.Windows.Forms.ToolStripButton toolStripButtonWrite;
        private System.Windows.Forms.ToolStripLabel toolStripLabelChanges;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewContextMenu;
        private System.Windows.Forms.GroupBox groupBoxEditMenuItem;
    }
}

