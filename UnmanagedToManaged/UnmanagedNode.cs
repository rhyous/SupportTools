using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using LANDesk.ManagementSuite.Database;
using LANDesk.ManagementSuite.WinConsole;
using LANDesk.ManagementSuite.Information.DatabaseInformationWebReference;

namespace UnmanagedToManaged
{
    class UnmanagedNode : BasicNode
    {
        private int UNMANAGEDNODES_IDN, 
                    DEVICEGROUPINFO_IDN;

        private String mGroupName,
                       mTopGroupName;

        public enum UnmanagedNodeDataType
        {
            NODENAME,
            IPADDRESS,
            PHYSADDRESS
        }

        public UnmanagedNode(String[] inArgs)
        : base(inArgs[0], inArgs[1], inArgs[2], inArgs[3], inArgs[4])
        {
        }

        public UnmanagedNode(String inData, UnmanagedNodeDataType inDataType)
        {

            if (inDataType == UnmanagedNodeDataType.IPADDRESS)
            {
                mIPAddressPadded = PadIPAddress(inData);
                GetUnmanagedDataFromDB(mIPAddressPadded, inDataType);
            }
            else
            {
                GetUnmanagedDataFromDB(inData, inDataType);
            }
            FixBlankValues();
        }

        private void FixBlankValues()
        {
            if (DeviceName.Equals(""))
            {
                if (! mIPAddress.Equals("") ) 
                {
                    DeviceName = mIPAddress;
                } 
                else if ( mTopGroupName.Equals("Printers") || mTopGroupName.Equals("Wireless Access Points") )
                {
                    DeviceName = mTopGroupName.Remove(mTopGroupName.Length - 1);
                } 
                else if (! mMacAddress.Equals(""))
                {
                    DeviceName = mMacAddress;
                }

            }
        }

        private void GetUnmanagedDataFromDB(String inData, UnmanagedNodeDataType inDataType)
        {
            LanDeskDatabase database = LanDeskDatabase.Get();;
            if (!LanDeskDatabase.IsOpen)
            {
                
            }
			try
			{
                String Data = inData;
                String DataType = inDataType.ToString();
                string sqltext = string.Format(@"Select UNMANAGEDNODES_IDN, UnmanagedNodes.DEVICEGROUPINFO_IDN, NODENAME, IPADDRESS, SUBNETMASK, PHYSADDRESS, OSNAME, GroupName from UnmanagedNodes, DeviceGroupInfo where UnmanagedNodes.DEVICEGROUPINFO_IDN = DeviceGroupInfo.DEVICEGROUPINFO_IDN AND {0}='{1}'", DataType, Data);
				DataRow row = database.ExecuteRow(sqltext);
                UNMANAGEDNODES_IDN = (int)row["UNMANAGEDNODES_IDN"];
                DEVICEGROUPINFO_IDN = (int)row["DEVICEGROUPINFO_IDN"];
                mDeviceName = (string)row["NODENAME"];
                mIPAddressPadded = (string)row["IPADDRESS"];
                mIPAddress = RemoveIPAddressPadding(mIPAddressPadded);
                mSubnetMask = (string)row["SUBNETMASK"];
                mMacAddress = (string)row["PHYSADDRESS"];
                mOSDescription = (string)row["OSNAME"];
                mGroupName = (string)row["GROUPNAME"];
                mTopGroupName = GetTopGroupName(DEVICEGROUPINFO_IDN);
			}
			catch
			{
                // Do nothing for now.
	    	}
        }

        new public void DeleteFromDB()
        {
            LanDeskDatabase database = LanDeskDatabase.Get();
            try
            {
                string sqltext;
                if ( !(UNMANAGEDNODES_IDN == 0) )
                {
                    sqltext = string.Format(@"Delete from UnmanagedNodes WHERE UNMANAGEDNODES_IDN=" + UNMANAGEDNODES_IDN);
                } 
                else 
                {
                    sqltext = string.Format(@"Delete from UnmanagedNodes WHERE {0}='{1}' and {2}='{3}' and {4}='{5}'", 
                                               UnmanagedNodeDataType.IPADDRESS.ToString(), mIPAddress,
                                               UnmanagedNodeDataType.NODENAME.ToString(), mDeviceName,
                                               UnmanagedNodeDataType.PHYSADDRESS.ToString(), mMacAddress);
                }
                database.ExecuteRow(sqltext);
            }
            catch
            {
                // Do nothing for now.
            }
        }



        private String GetTopGroupName(int DEVICEGROUPINFO_IDN)
        {
            String RetVal = "";
            LanDeskDatabase database = LanDeskDatabase.Get();
            string sqltext = @"SELECT GROUPID,GROUPNAME FROM DEVICEGROUPINFO WHERE DEVICEGROUPINFO_IDN=" + DEVICEGROUPINFO_IDN;
            DataRow row;
            String tmpStr;
            try
            {
                row = database.ExecuteRow(sqltext);
                tmpStr = (string)row["GROUPID"];
                RetVal = (string)row["GROUPNAME"];
                if (tmpStr.Length > 6)
                {
                    tmpStr = tmpStr.Substring(0, tmpStr.Length - 3);
                    sqltext = string.Format(@"SELECT DEVICEGROUPINFO_IDN FROM DEVICEGROUPINFO WHERE GROUPID='{0}'", tmpStr);
                    row = database.ExecuteRow(sqltext);
                    int tmpInt = (int)row["DEVICEGROUPINFO_IDN"];
                    RetVal = GetTopGroupName(tmpInt);
                }
            }
            catch (Exception)
            {
                // Do nothing for now.
            }
            return RetVal;
        }

        public string GroupName
        {
            get { return this.mGroupName; }
            set { this.mGroupName = value; }
        }
        public string TopGroupName
        {
            get { return this.mTopGroupName; }
            set { this.mTopGroupName = value; }
        }



    }
}
