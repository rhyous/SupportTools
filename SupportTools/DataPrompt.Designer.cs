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

namespace SupportTools
{
    partial class DataPrompt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataPrompt));
            this.SuspendLayout();
            // 
            // DataPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(260, 67);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DataPrompt";
            this.Text = "Complete Prompt Fields";
            this.ResumeLayout(false);

        }

        #endregion
        private void CustomInitComponents()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();

            this.PromptValueName = new List<System.Windows.Forms.Label>();
            this.ValueDataTextBox = new List<System.Windows.Forms.TextBox>();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 

            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(458, 69);
            this.flowLayoutPanel1.TabIndex = promptList.Count+2;

            for (int i = 0; i < promptList.Count; i++)
            {
                string[] strArray;

                char[] delmin = { ':' };

                strArray = promptList[i].Split(delmin);
                string strPromptValueName = strArray[0].Trim();
                int valueLength;
                try
                {
                     valueLength = Convert.ToInt32(strArray[1]);
                }
                catch (Exception)
                {
                    valueLength = 0; ;
                }
                // 
                // PromptValueName
                //             
                this.PromptValueName.Add(new System.Windows.Forms.Label());
                this.PromptValueName[i].AutoSize = true;
                this.PromptValueName[i].Location = new System.Drawing.Point(3, 0);
                this.PromptValueName[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.PromptValueName[i].Name = "PromptValueName";
                this.PromptValueName[i].Size = new System.Drawing.Size(strPromptValueName.Length * (int)PromptValueName[i].Font.SizeInPoints, 20);
                this.PromptValueName[i].Text = strPromptValueName;
                this.flowLayoutPanel1.Controls.Add(PromptValueName[i]);
                // 
                // ValueDataTextBox
                // 
                this.ValueDataTextBox.Add(new System.Windows.Forms.TextBox());
                this.ValueDataTextBox[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.ValueDataTextBox[i].Location = new System.Drawing.Point(3, 20);
                this.ValueDataTextBox[i].MaxLength = valueLength;
                this.ValueDataTextBox[i].Name = "ValueDataTextBox";
                this.ValueDataTextBox[i].Size = new System.Drawing.Size(GetLimitedSize(valueLength * (int)ValueDataTextBox[i].Font.SizeInPoints), 20);
                this.ValueDataTextBox[i].TabIndex = i;
                this.flowLayoutPanel1.Controls.Add(ValueDataTextBox[i]);
            }
            // 
            // OkButton
            // 
            this.OkButton = new System.Windows.Forms.Button();
            this.OkButton.Location = new System.Drawing.Point(this.Width - 100, this.Height - 100);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 25);
            this.OkButton.TabIndex = promptList.Count + 1;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            this.AcceptButton = this.OkButton;
            this.flowLayoutPanel1.Controls.Add(this.OkButton);

            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.Controls.Add(flowLayoutPanel1);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private int GetLimitedSize(int i)
        {
            int maxwidth = 450;
            int minwidth = 8;
            if (i > 450)
            {
                return maxwidth;
            }
            else if (i < minwidth)
            {
                return minwidth;
            }
            return i;
        }

    }
}