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
using System.Xml.Serialization;

namespace SupportTools.ContextMenuXml
{
    [Serializable]
    public class InventoryFilter
    {
        #region Member Variables
        String _InventoryProperty;
        List<String> _BlockedValues = new List<string>();
        List<String> _AllowedValues = new List<string>();
        #endregion

        #region Constructors

        /*
		 * The default constructor
 		 */
        public InventoryFilter()
        {
        }

        public InventoryFilter(String inInventoryProperty)
        {
            _InventoryProperty = inInventoryProperty;
        }

        public InventoryFilter(String inInventoryProperty, List<String> inAllowedValues, List<String> inBlockedValues)
        {
            _InventoryProperty = inInventoryProperty;
            _AllowedValues = inAllowedValues;
            _BlockedValues = inBlockedValues;
        }

        #endregion

        #region Properties
        public String InventoryProperty
        {
            get { return _InventoryProperty; }
            set { _InventoryProperty = value; }
        }

        [XmlElement("BlockedValue")]
        public List<String> BlockedValues
        {
            get { return _BlockedValues; }
            set { _BlockedValues = value; }
        }

        [XmlElement("AllowedValue")]
        public List<String> AllowedValues
        {
            get { return _AllowedValues; }
            set { _AllowedValues = value; }
        }
        #endregion

        #region Functions
        #endregion

        #region Enums
        #endregion
    }
}
