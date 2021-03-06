﻿/* 
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
using System.Windows.Forms;

namespace SupportTools
{
    public partial class DataPrompt : Form
    {
        private readonly List<string> _PromptList;
        //List<int> promptSizeList;
        private List<string> _UserInput;
        private FlowLayoutPanel _FlowLayoutPanel1;
        private List<Label> _PromptValueName;
        private List<TextBox> _ValueDataTextBox;
        private Button _OkButton;

        public DataPrompt(List<string> promptList)
        {
            _PromptList = promptList;
            InitializeComponent();
            CustomInitComponents();
        }

        public List<string> GetMessageText()
        {
            return _UserInput;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _UserInput = new List<string>();
            foreach (TextBox tb in _ValueDataTextBox)
            {
                    _UserInput.Add(tb.Text); 
            }
            Close();
        }
    }
}
