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
    partial class EditActionControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanelLeft = new System.Windows.Forms.TableLayoutPanel();
            this.labelFilter = new System.Windows.Forms.Label();
            this.labelLocation = new System.Windows.Forms.Label();
            this.labelParameters = new System.Windows.Forms.Label();
            this.labelCommand = new System.Windows.Forms.Label();
            this.labelIsMultiselect = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.tableLayoutPanelRight = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxLocation = new System.Windows.Forms.ComboBox();
            this.textBoxParameters = new System.Windows.Forms.TextBox();
            this.textBoxCommand = new System.Windows.Forms.TextBox();
            this.checkBoxIsMultiselect = new System.Windows.Forms.CheckBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanelFilterLayout = new System.Windows.Forms.TableLayoutPanel();
            this.checkedListBox2 = new System.Windows.Forms.CheckedListBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelAllowedOSTypes = new System.Windows.Forms.Label();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tableLayoutPanelLeft.SuspendLayout();
            this.tableLayoutPanelRight.SuspendLayout();
            this.tableLayoutPanelFilterLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.tableLayoutPanelLeft);
            this.splitContainer.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.tableLayoutPanelRight);
            this.splitContainer.Size = new System.Drawing.Size(665, 275);
            this.splitContainer.SplitterDistance = 116;
            this.splitContainer.TabIndex = 7;
            // 
            // tableLayoutPanelLeft
            // 
            this.tableLayoutPanelLeft.AutoSize = true;
            this.tableLayoutPanelLeft.ColumnCount = 1;
            this.tableLayoutPanelLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLeft.Controls.Add(this.labelFilter, 0, 5);
            this.tableLayoutPanelLeft.Controls.Add(this.labelLocation, 0, 4);
            this.tableLayoutPanelLeft.Controls.Add(this.labelParameters, 0, 3);
            this.tableLayoutPanelLeft.Controls.Add(this.labelCommand, 0, 2);
            this.tableLayoutPanelLeft.Controls.Add(this.labelIsMultiselect, 0, 1);
            this.tableLayoutPanelLeft.Controls.Add(this.labelName, 0, 0);
            this.tableLayoutPanelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelLeft.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelLeft.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelLeft.Name = "tableLayoutPanelLeft";
            this.tableLayoutPanelLeft.RowCount = 6;
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelLeft.Size = new System.Drawing.Size(116, 275);
            this.tableLayoutPanelLeft.TabIndex = 0;
            // 
            // labelFilter
            // 
            this.labelFilter.AutoSize = true;
            this.labelFilter.Location = new System.Drawing.Point(70, 153);
            this.labelFilter.Margin = new System.Windows.Forms.Padding(3);
            this.labelFilter.Name = "labelFilter";
            this.labelFilter.Padding = new System.Windows.Forms.Padding(2);
            this.labelFilter.Size = new System.Drawing.Size(43, 21);
            this.labelFilter.TabIndex = 15;
            this.labelFilter.Text = "Filter";
            this.labelFilter.Visible = false;
            // 
            // labelLocation
            // 
            this.labelLocation.AutoSize = true;
            this.labelLocation.Location = new System.Drawing.Point(13, 123);
            this.labelLocation.Margin = new System.Windows.Forms.Padding(3);
            this.labelLocation.Name = "labelLocation";
            this.labelLocation.Padding = new System.Windows.Forms.Padding(2);
            this.labelLocation.Size = new System.Drawing.Size(100, 21);
            this.labelLocation.TabIndex = 14;
            this.labelLocation.Text = "Exec Location";
            // 
            // labelParameters
            // 
            this.labelParameters.AutoSize = true;
            this.labelParameters.Location = new System.Drawing.Point(28, 93);
            this.labelParameters.Margin = new System.Windows.Forms.Padding(3);
            this.labelParameters.Name = "labelParameters";
            this.labelParameters.Padding = new System.Windows.Forms.Padding(2);
            this.labelParameters.Size = new System.Drawing.Size(85, 21);
            this.labelParameters.TabIndex = 13;
            this.labelParameters.Text = "Parameters";
            // 
            // labelCommand
            // 
            this.labelCommand.AutoSize = true;
            this.labelCommand.Location = new System.Drawing.Point(38, 63);
            this.labelCommand.Margin = new System.Windows.Forms.Padding(3);
            this.labelCommand.Name = "labelCommand";
            this.labelCommand.Padding = new System.Windows.Forms.Padding(2);
            this.labelCommand.Size = new System.Drawing.Size(75, 21);
            this.labelCommand.TabIndex = 12;
            this.labelCommand.Text = "Command";
            // 
            // labelIsMultiselect
            // 
            this.labelIsMultiselect.AutoSize = true;
            this.labelIsMultiselect.Location = new System.Drawing.Point(35, 33);
            this.labelIsMultiselect.Margin = new System.Windows.Forms.Padding(3);
            this.labelIsMultiselect.Name = "labelIsMultiselect";
            this.labelIsMultiselect.Padding = new System.Windows.Forms.Padding(2);
            this.labelIsMultiselect.Size = new System.Drawing.Size(78, 21);
            this.labelIsMultiselect.TabIndex = 11;
            this.labelIsMultiselect.Text = "Multiselect";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(68, 3);
            this.labelName.Margin = new System.Windows.Forms.Padding(3);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(45, 17);
            this.labelName.TabIndex = 10;
            this.labelName.Text = "Name";
            // 
            // tableLayoutPanelRight
            // 
            this.tableLayoutPanelRight.AutoSize = true;
            this.tableLayoutPanelRight.ColumnCount = 1;
            this.tableLayoutPanelRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelRight.Controls.Add(this.comboBoxLocation, 0, 4);
            this.tableLayoutPanelRight.Controls.Add(this.textBoxParameters, 0, 3);
            this.tableLayoutPanelRight.Controls.Add(this.textBoxCommand, 0, 2);
            this.tableLayoutPanelRight.Controls.Add(this.checkBoxIsMultiselect, 0, 1);
            this.tableLayoutPanelRight.Controls.Add(this.textBoxName, 0, 0);
            this.tableLayoutPanelRight.Controls.Add(this.tableLayoutPanelFilterLayout, 0, 5);
            this.tableLayoutPanelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelRight.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelRight.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            this.tableLayoutPanelRight.RowCount = 6;
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelRight.Size = new System.Drawing.Size(545, 275);
            this.tableLayoutPanelRight.TabIndex = 0;
            // 
            // comboBoxLocation
            // 
            this.comboBoxLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLocation.FormattingEnabled = true;
            this.comboBoxLocation.Items.AddRange(new object[] {
            "Client",
            "Console"});
            this.comboBoxLocation.Location = new System.Drawing.Point(3, 123);
            this.comboBoxLocation.Name = "comboBoxLocation";
            this.comboBoxLocation.Size = new System.Drawing.Size(539, 24);
            this.comboBoxLocation.TabIndex = 18;
            this.comboBoxLocation.SelectedIndexChanged += new System.EventHandler(this.comboBoxLocation_SelectedIndexChanged);
            // 
            // textBoxParameters
            // 
            this.textBoxParameters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxParameters.Location = new System.Drawing.Point(3, 93);
            this.textBoxParameters.Name = "textBoxParameters";
            this.textBoxParameters.Size = new System.Drawing.Size(539, 22);
            this.textBoxParameters.TabIndex = 17;
            this.textBoxParameters.TextChanged += new System.EventHandler(this.textBoxParameters_TextChanged);
            // 
            // textBoxCommand
            // 
            this.textBoxCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCommand.Location = new System.Drawing.Point(3, 63);
            this.textBoxCommand.Name = "textBoxCommand";
            this.textBoxCommand.Size = new System.Drawing.Size(539, 22);
            this.textBoxCommand.TabIndex = 16;
            this.textBoxCommand.TextChanged += new System.EventHandler(this.textBoxCommand_TextChanged);
            // 
            // checkBoxIsMultiselect
            // 
            this.checkBoxIsMultiselect.AutoSize = true;
            this.checkBoxIsMultiselect.Location = new System.Drawing.Point(3, 33);
            this.checkBoxIsMultiselect.Name = "checkBoxIsMultiselect";
            this.checkBoxIsMultiselect.Padding = new System.Windows.Forms.Padding(2);
            this.checkBoxIsMultiselect.Size = new System.Drawing.Size(22, 21);
            this.checkBoxIsMultiselect.TabIndex = 15;
            this.checkBoxIsMultiselect.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.checkBoxIsMultiselect.UseVisualStyleBackColor = true;
            this.checkBoxIsMultiselect.CheckedChanged += new System.EventHandler(this.checkBoxIsMultiselect_CheckedChanged);
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(3, 3);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(539, 22);
            this.textBoxName.TabIndex = 14;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // tableLayoutPanelFilterLayout
            // 
            this.tableLayoutPanelFilterLayout.ColumnCount = 2;
            this.tableLayoutPanelFilterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelFilterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelFilterLayout.Controls.Add(this.checkedListBox2, 0, 1);
            this.tableLayoutPanelFilterLayout.Controls.Add(this.checkedListBox1, 0, 1);
            this.tableLayoutPanelFilterLayout.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanelFilterLayout.Controls.Add(this.labelAllowedOSTypes, 0, 0);
            this.tableLayoutPanelFilterLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelFilterLayout.Location = new System.Drawing.Point(0, 150);
            this.tableLayoutPanelFilterLayout.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelFilterLayout.Name = "tableLayoutPanelFilterLayout";
            this.tableLayoutPanelFilterLayout.RowCount = 2;
            this.tableLayoutPanelFilterLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanelFilterLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelFilterLayout.Size = new System.Drawing.Size(545, 125);
            this.tableLayoutPanelFilterLayout.TabIndex = 19;
            // 
            // checkedListBox2
            // 
            this.checkedListBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox2.FormattingEnabled = true;
            this.checkedListBox2.Location = new System.Drawing.Point(3, 30);
            this.checkedListBox2.Name = "checkedListBox2";
            this.checkedListBox2.Size = new System.Drawing.Size(266, 89);
            this.checkedListBox2.TabIndex = 4;
            this.checkedListBox2.Visible = false;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(275, 30);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(267, 89);
            this.checkedListBox1.TabIndex = 3;
            this.checkedListBox1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(275, 3);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(2);
            this.label2.Size = new System.Drawing.Size(146, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Disallowed OS Types";
            this.label2.Visible = false;
            // 
            // labelAllowedOSTypes
            // 
            this.labelAllowedOSTypes.AutoSize = true;
            this.labelAllowedOSTypes.Location = new System.Drawing.Point(3, 3);
            this.labelAllowedOSTypes.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.labelAllowedOSTypes.Name = "labelAllowedOSTypes";
            this.labelAllowedOSTypes.Padding = new System.Windows.Forms.Padding(2);
            this.labelAllowedOSTypes.Size = new System.Drawing.Size(127, 21);
            this.labelAllowedOSTypes.TabIndex = 0;
            this.labelAllowedOSTypes.Text = "Allowed OS Types";
            this.labelAllowedOSTypes.Visible = false;
            // 
            // EditActionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "EditActionControl";
            this.Size = new System.Drawing.Size(665, 275);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            this.splitContainer.ResumeLayout(false);
            this.tableLayoutPanelLeft.ResumeLayout(false);
            this.tableLayoutPanelLeft.PerformLayout();
            this.tableLayoutPanelRight.ResumeLayout(false);
            this.tableLayoutPanelRight.PerformLayout();
            this.tableLayoutPanelFilterLayout.ResumeLayout(false);
            this.tableLayoutPanelFilterLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLeft;
        private System.Windows.Forms.Label labelIsMultiselect;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRight;
        private System.Windows.Forms.CheckBox checkBoxIsMultiselect;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelCommand;
        private System.Windows.Forms.TextBox textBoxCommand;
        private System.Windows.Forms.Label labelLocation;
        private System.Windows.Forms.Label labelParameters;
        private System.Windows.Forms.TextBox textBoxParameters;
        private System.Windows.Forms.Label labelFilter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFilterLayout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox checkedListBox2;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.ComboBox comboBoxLocation;
        private System.Windows.Forms.Label labelAllowedOSTypes;
    }
}
