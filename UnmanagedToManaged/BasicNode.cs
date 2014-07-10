using System;

namespace UnmanagedToManaged
{
    public abstract class BasicNode
    {
        private string _DeviceName = "",
                       _IPAddress = "",
                       _IPAddressPadded = "",
                       _SubnetMask = "",
                       _OSDescription = "",
                       _MacAddress = "";


        protected BasicNode()
        {
        }

        protected BasicNode(string inDeviceName, string inIPAddress, string inSubnetMask, string inOSDescription, string inMacAddress)
        {
            DeviceName = inDeviceName;
            IPAddress = RemoveIPAddressPadding(inIPAddress);
            IPAddressPadded = PadIPAddress(inIPAddress);
            SubnetMask = inSubnetMask;
            OSDescription = inOSDescription;
            MacAddress = inMacAddress;
        }

        public string DeviceName
        {
            get { return _DeviceName; }
            set { _DeviceName = value; }
        }

        public string IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        }

        public string IPAddressPadded
        {
            get { return _IPAddressPadded; }
            set { _IPAddressPadded = value; }
        }

        public string SubnetMask
        {
            get { return _SubnetMask; }
            set { _SubnetMask = value; }
        }

        public string OSDescription
        {
            get { return _OSDescription; }
            set { _OSDescription = value; }
        }

        public string MacAddress
        {
            get { return _MacAddress; }
            set { _MacAddress = value; }
        }


        protected string PadIPAddress(string inIPAddress)
        {
            var ipOctet = inIPAddress.Split('.');
            for (var i = 0; i < ipOctet.Length; i++)
            {
                if (ipOctet[i].Length == 3)
                {
                }
                if (ipOctet[i].Length == 2)
                {
                    ipOctet[i] = "0" + ipOctet[i];
                }
                else if (ipOctet[i].Length == 1)
                {
                    ipOctet[i] = "00" + ipOctet[i];
                }
            }
            var retVal = ipOctet[0] + "." + ipOctet[1] + "." + ipOctet[2] + "." + ipOctet[3];
            return retVal;
        }

        protected string RemoveIPAddressPadding(string inIPAddress)
        {
            var ipOctet = inIPAddress.Split('.');
            for (var i = 0; i < ipOctet.Length; i++)
            {
                while (ipOctet[i].Length > 1 && ipOctet[i].StartsWith("0"))
                {
                    ipOctet[i] = ipOctet[i].Substring(1, (ipOctet[i].Length - 1));
                }
            }
            var retVal = ipOctet[0] + "." + ipOctet[1] + "." + ipOctet[2] + "." + ipOctet[3];
            return retVal;
        }
    }
}
