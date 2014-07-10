using System.IO;
using Microsoft.Win32;

namespace UnmanagedToManaged
{
    class MiniscanBuilder
    {
        private readonly string _DeviceName = string.Empty,
               _Type = string.Empty,
               _NicAddress = string.Empty,
               _IPAddress = string.Empty,
               _SubnetMask = string.Empty,
               _HostName = string.Empty,
               _OSDecription = string.Empty;


        enum DeviceType
        {
            Printer,
            WirelessAccessPoint
        }

        public MiniscanBuilder()
        {

        }

        public MiniscanBuilder(UnmanagedNode inUnmanagedNode)
        {
            _DeviceName = inUnmanagedNode.DeviceName;
            _NicAddress = inUnmanagedNode.MacAddress;
            _IPAddress = inUnmanagedNode.IPAddress;
            _SubnetMask = inUnmanagedNode.SubnetMask;
            _HostName = inUnmanagedNode.DeviceName;
            _OSDecription = inUnmanagedNode.OSDescription;
            if (inUnmanagedNode.TopGroupName.Equals("Printers") || inUnmanagedNode.GroupName.Equals("Wireless Access Points"))
            {
                _Type = inUnmanagedNode.TopGroupName;  // Leave as "" if not from one of these two groups.
            }

        }

        /*
            Device Name = PrinterName
            Type = Printer
            Network - NIC Address = 000C29170420
            Network - TCPIP - Address = 192.168.027.101
            Network - TCPIP - Subnet Mask = 255.255.255.000
            Network - TCPIP - Host Name = PrinterName.domain.com
        */

        /*
            Network - NIC Address = 000C2917042A
            Network - TCPIP - Address = 192.168.027.019
            Network - TCPIP - Subnet Mask = 255.255.255.000
            Network - TCPIP - Subnet Broadcast Address = 192.168.027.255
            Device Name = XP1
            Network - TCPIP - Host Name = XP

        */
        private string GetLDMainPathFromReg()
        {
            const string defaultFilePath = @"C:\Program Files\LANDesk\ManagementSuite\";

            // Try 32 bit registry
            var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\LANDesk\ManagementSuite\Setup");
            if (key != null)
                return key.GetValue("LdmainPath", defaultFilePath).ToString();

            // Try 64 bit registry
            var localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            key = localKey.OpenSubKey(@"SOFTWARE\LANDesk\ManagementSuite\Setup");
            if (key != null)
                return key.GetValue("LdmainPath", defaultFilePath).ToString();

            return defaultFilePath;
        }

        public void WriteMiniscan()
        {
            var path = GetLDMainPathFromReg() + @"ldscan\" + _DeviceName + ".tmp";

            if (!File.Exists(path))
            {
                // Create a file to write to.
                var sw = File.CreateText(path);

                if (!_Type.Equals(""))
                {
                    sw.WriteLine("Type = {0}", _Type);
                }
                if (!_NicAddress.Equals(""))
                {
                    sw.WriteLine("Network - NIC Address = {0}", _NicAddress);
                }
                if (!_IPAddress.Equals(""))
                {
                    sw.WriteLine("Network - TCPIP - Address = {0}", _IPAddress);
                }
                if (!_SubnetMask.Equals(""))
                {
                    sw.WriteLine("Network - TCPIP - Subnet Mask = {0}", _SubnetMask);
                }
                if (!_DeviceName.Equals(""))
                {
                    sw.WriteLine("Device Name = {0}", _DeviceName);
                }
                if (!_OSDecription.Equals("") && !_OSDecription.ToLower().Equals("unknown") && !(null == _OSDecription))
                {
                    sw.WriteLine("OS - Name = {0}", _OSDecription);
                }
                if (!_HostName.Equals(""))
                {
                    sw.WriteLine("Network - TCPIP - Host Name = {0}", _HostName);
                }
                sw.WriteLine("");
                sw.Close();
                var miniscanPath = GetLDMainPathFromReg() + @"ldscan\" + _DeviceName + ".ims";
                File.Move(path, miniscanPath);
            }
        }
    }
}
