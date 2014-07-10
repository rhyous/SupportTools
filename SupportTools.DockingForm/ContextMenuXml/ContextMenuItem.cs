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
    [XmlInclude(typeof(MenuAction))]
    [XmlInclude(typeof(MenuGroup))]
    [XmlInclude(typeof(ComputerContextMenu))]
    abstract public class ContextMenuItem
    {
        //<!-- All sub-items should be <MenuItems> or <SubMenu> -->
        //<MenuItem>
        //  <!-- There can be the following items:
        //    <Text> - Name displayed in the context menu.
        //    <Command> - Executable name, including full path if not in the PATH or in ManagementSuite.
        //    <Parameter> - Optional. The parameters to pass to the executable.  If this does not exist, no parameters are passed.
        //    <Multiselect> - Optional. Values are true or false.  Default is false.If set to true you can highlight multiple devices 
        //                    and see the item in the context menu and run the command. Otherwise, it only shows up when right-clicking
        //                    a single device.
        //    <OSType> 
        //    or 
        //    <OSType> - Optional. This is the Type value in inventory under Computer - Type.
        //               Windows Values are combined in 9.0 to just these: Workstation, Server
        //               Linux Value: Linux
        //               Unix Value: Unix
        //               Mac Value: MAC
        //               You can have multiple entries separated by a comma.  If you put a ! before an OS it
        //               is excluded.  Right now you cannot mix exclusions and inclusions so hopefully that will change.
        //    <ExecLocation> - Optional. Values are Console or Client.  Default is Console.  This means the command will either run on the Core
        //                     a Remote Execute will occur to run these on the client.
        //  -->
        //  <Text>Send Message</Text>
        //  <Command>msg.exe</Command>
        //  <OSType>Workstation, Server</OSType>
        //  <Parameter> Console /TIME:%prompt:Time in seconds to show message on screen.:4% "%prompt:Enter Message. (Currently only one line is supported.):512%"</Parameter>
        //  <MultiSelect>True</MultiSelect>
        //  <ExecLocation>Client</ExecLocation>
        //</MenuItem>

        #region Member Variables
        private string _Name;
        private InventoryFilter _Filter;
        private ContextMenuItem _Parent;
        private bool _IsModified;
        #endregion

        #region Constructors

        /// <summary>
        /// The default constructor
        /// </summary>
        public ContextMenuItem()
        {
        }

        public ContextMenuItem(ContextMenuItem inParent)
        {
        }

        public ContextMenuItem(string inName, ContextMenuItem inParent)
        {
            _Name = inName;
            _Parent = inParent;
        }

        public ContextMenuItem(string inName, InventoryFilter inFilter, ContextMenuItem inParent)
        {
            _Name = inName;
            _Filter = inFilter;
            _Parent = inParent;
        }
        #endregion

        #region Properties
        [XmlAttribute]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public InventoryFilter Filter
        {
            get { return _Filter; }
            set { _Filter = value; }
        }

        [XmlIgnore]
        public ContextMenuItem Parent
        {
            get { return _Parent; }
            set { _Parent = value; }
        }

        [XmlIgnore]
        public bool IsModified
        {
            get { return _IsModified; }
            set 
            { 
                _IsModified = value;
                if (null != _Parent)
                {
                    _Parent.IsModified = value;
                }
            }
        }

        #endregion

        #region Functions
        #endregion

        #region Enums
        #endregion
    }
}
