using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LANDesk.ManagementSuite.WinConsole;

namespace UnmanagedToManaged
{
    public abstract class BasicNode 
    {
        protected String mDeviceName = "",
                       mIPAddress = "",
                       mIPAddressPadded = "",
                       mSubnetMask = "",
                       mOSDescription = "",
                       mMacAddress = "";


        public BasicNode()
        {
        }

        public BasicNode(String inDeviceName, String inIPAddress, String inSubnetMask, String inOSDescription, String inMacAddress)
        {
            DeviceName = inDeviceName;
            IPAddress = RemoveIPAddressPadding(inIPAddress);;
            IPAddressPadded = PadIPAddress(inIPAddress);
            SubnetMask = inSubnetMask;
            OSDescription = inOSDescription;
            MacAddress = inMacAddress;
        }

        public string DeviceName
        {
            get { return this.mDeviceName; }
            set { this.mDeviceName = value; }
        }

        public string IPAddress
        {
            get { return this.mIPAddress; }
            set { this.mIPAddress = value; }
        }

        public string IPAddressPadded
        {
            get { return this.mIPAddressPadded; }
            set { this.mIPAddressPadded = value; }
        }

        new public string SubnetMask
        {
            get { return this.mSubnetMask; }
            set { this.mSubnetMask = value; }
        }

        public string OSDescription
        {
            get { return this.mOSDescription; }
            set { this.mOSDescription = value; }
        }

        public string MacAddress
        {
            get { return this.mMacAddress; }
            set { this.mMacAddress = value; }
        }


        protected String PadIPAddress(String inIPAddress)
        {
            String RetVal = inIPAddress.Trim();
            String[] IPOctet = inIPAddress.Split('.');
            for (int i = 0; i < IPOctet.Length; i++)
            {
                if (IPOctet[i].Length == 3)
                {
                    continue;
                }
                else if (IPOctet[i].Length == 2)
                {
                    IPOctet[i] = "0" + IPOctet[i];
                }
                else if (IPOctet[i].Length == 1)
                {
                    IPOctet[i] = "00" + IPOctet[i];
                }
            }
            RetVal = IPOctet[0] + "." + IPOctet[1] + "." + IPOctet[2] + "." + IPOctet[3];
            return RetVal;
        }

        protected String RemoveIPAddressPadding(String inIPAddress)
        {
            String RetVal = inIPAddress.Trim();
            String[] IPOctet = inIPAddress.Split('.');
            for (int i = 0; i < IPOctet.Length; i++)
            {
                while (IPOctet[i].Length > 1 && IPOctet[i].StartsWith("0"))
                {
                    IPOctet[i] = IPOctet[i].Substring(1, (IPOctet[i].Length - 1));
                }
            }
            RetVal = IPOctet[0] + "." + IPOctet[1] + "." + IPOctet[2] + "." + IPOctet[3];
            return RetVal;
        }

    }



}
