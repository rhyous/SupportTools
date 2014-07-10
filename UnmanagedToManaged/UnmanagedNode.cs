using System;
using LANDesk.ManagementSuite.Database;

namespace UnmanagedToManaged
{
    class UnmanagedNode : BasicNode
    {
        private int _UnmanagednodesIdn,
                    _DevicegroupinfoIdn;

        private string _GroupName,
                       _TopGroupName;

        public enum UnmanagedNodeDataType
        {
            Nodename,
            IPAddress,
            PhysAddress
        }

        public UnmanagedNode(string[] inArgs)
            : base(inArgs[0], inArgs[1], inArgs[2], inArgs[3], inArgs[4])
        {
        }

        public UnmanagedNode(string inData, UnmanagedNodeDataType inDataType)
        {

            if (inDataType == UnmanagedNodeDataType.IPAddress)
            {
                IPAddressPadded = PadIPAddress(inData);
                GetUnmanagedDataFromDb(IPAddressPadded, inDataType);
            }
            else
            {
                GetUnmanagedDataFromDb(inData, inDataType);
            }
            FixBlankValues();
        }

        private void FixBlankValues()
        {
            if (DeviceName.Equals(""))
            {
                if (!IPAddress.Equals(""))
                {
                    DeviceName = IPAddress;
                }
                else if (_TopGroupName.Equals("Printers") || _TopGroupName.Equals("Wireless Access Points"))
                {
                    DeviceName = _TopGroupName.Remove(_TopGroupName.Length - 1);
                }
                else if (!MacAddress.Equals(""))
                {
                    DeviceName = MacAddress;
                }

            }
        }

        private void GetUnmanagedDataFromDb(string inData, UnmanagedNodeDataType inDataType)
        {
            var database = LanDeskDatabase.Get();
            if (!LanDeskDatabase.IsOpen)
                return;
            try
            {
                var data = inData;
                var dataType = inDataType.ToString();
                var sqltext = string.Format(@"Select UNMANAGEDNODES_IDN, UnmanagedNodes.DEVICEGROUPINFO_IDN, NODENAME, IPADDRESS, SUBNETMASK, PHYSADDRESS, OSNAME, GroupName from UnmanagedNodes, DeviceGroupInfo where UnmanagedNodes.DEVICEGROUPINFO_IDN = DeviceGroupInfo.DEVICEGROUPINFO_IDN AND {0}='{1}'", dataType, data);
                var row = database.ExecuteRow(sqltext);
                _UnmanagednodesIdn = (int)row["UNMANAGEDNODES_IDN"];
                _DevicegroupinfoIdn = (int)row["DEVICEGROUPINFO_IDN"];
                DeviceName = (string)row["NODENAME"];
                IPAddressPadded = (string)row["IPADDRESS"];
                IPAddress = RemoveIPAddressPadding(IPAddressPadded);
                SubnetMask = (string)row["SUBNETMASK"];
                MacAddress = (string)row["PHYSADDRESS"];
                OSDescription = (string)row["OSNAME"];
                _GroupName = (string)row["GROUPNAME"];
                _TopGroupName = GetTopGroupName(_DevicegroupinfoIdn);
            }
            catch
            {
                // Do nothing for now.
            }
        }

        public void DeleteFromDb()
        {
            var database = LanDeskDatabase.Get();
            try
            {
                string sqltext;
                if (_UnmanagednodesIdn != 0)
                {
                    sqltext = string.Format(@"Delete from UnmanagedNodes WHERE UNMANAGEDNODES_IDN=" + _UnmanagednodesIdn);
                }
                else
                {
                    sqltext = string.Format(@"Delete from UnmanagedNodes WHERE {0}='{1}' and {2}='{3}' and {4}='{5}'",
                                               UnmanagedNodeDataType.IPAddress, IPAddress,
                                               UnmanagedNodeDataType.Nodename, DeviceName,
                                               UnmanagedNodeDataType.PhysAddress, MacAddress);
                }
                database.ExecuteRow(sqltext);
            }
            catch
            {
                // Do nothing for now.
            }
        }



        private string GetTopGroupName(int deviceGroupInfoIdn)
        {
            var retVal = "";
            var database = LanDeskDatabase.Get();
            var sqltext = @"SELECT GROUPID,GROUPNAME FROM DEVICEGROUPINFO WHERE DEVICEGROUPINFO_IDN=" + deviceGroupInfoIdn;
            try
            {
                var row = database.ExecuteRow(sqltext);
                var tmpStr = (string)row["GROUPID"];
                retVal = (string)row["GROUPNAME"];
                if (tmpStr.Length > 6)
                {
                    tmpStr = tmpStr.Substring(0, tmpStr.Length - 3);
                    sqltext = string.Format(@"SELECT DEVICEGROUPINFO_IDN FROM DEVICEGROUPINFO WHERE GROUPID='{0}'", tmpStr);
                    row = database.ExecuteRow(sqltext);
                    var tmpInt = (int)row["DEVICEGROUPINFO_IDN"];
                    retVal = GetTopGroupName(tmpInt);
                }
            }
            catch (Exception)
            {
                // Do nothing for now.
            }
            return retVal;
        }

        public string GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }
        public string TopGroupName
        {
            get { return _TopGroupName; }
            set { _TopGroupName = value; }
        }
    }
}
