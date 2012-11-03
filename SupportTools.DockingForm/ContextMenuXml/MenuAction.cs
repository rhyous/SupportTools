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
using System.Xml.Serialization;

namespace SupportTools.ContextMenuXml
{
    [Serializable]
    public class MenuAction : ContextMenuItem
    {
        #region Member Variables
        private String _Command;
        private String _Parameters;
        private String _ExecutionLocation = "Console"; // Allowed options are Client or Console.
        private bool _IsMultiSelect;
        private readonly String[] _ExecutionLocationOptions = new String[] { "Console", "Client" };
        #endregion

        #region Constructors

        /*
		 * The default constructor
 		 */
        public MenuAction()
        {
        }

        public MenuAction(String inName, ContextMenuItem inParent)
            : base(inName, inParent)
        {

        }

        public MenuAction(String inName, String inCommand, String inParameters, ContextMenuItem inParent)
            : base(inName, inParent)
        {
            _Command = inCommand;
            _Parameters = inParameters;
        }

        #endregion

        #region Properties
        public String Command
        {
            get { return _Command; }
            set { _Command = value; }
        }

        public String Parameters
        {
            get { return _Parameters; }
            set { _Parameters = value; }
        }

        public String ExecutionLocation
        {
            get { return _ExecutionLocation; }
            set 
            {
                value = value.Trim();
                foreach (string execlocation in _ExecutionLocationOptions)
                {
                    if (execlocation.ToLower().Equals(value.ToLower()))
                    {
                        _ExecutionLocation = value;
                        return;
                    }
                }
                if (String.IsNullOrEmpty(value))
                {
                    _ExecutionLocation = value;
                    return;
                }

                // You shouldn't get here
                throw new InvalidExecutionLocation(value);
            }
        }

        [XmlElement("MultiSelect")]
        public bool IsMultiSelect
        {
            get { return _IsMultiSelect; }
            set { _IsMultiSelect = value; }
        }
        #endregion

        #region Functions
        #endregion

        #region Enums
        #endregion
    }
}
