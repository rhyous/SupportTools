using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UnmanagedToManaged
{
    class MiniscanBuilder
    {
        string DeviceName = "",
               Type = "",
               NICAddress = "",
               IPAddress = "",
               SubnetMask = "",
               HostName = "",
               OSDecription = "";


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
           DeviceName = inUnmanagedNode.DeviceName;
           NICAddress = inUnmanagedNode.MacAddress;
           IPAddress = inUnmanagedNode.IPAddress;
           SubnetMask = inUnmanagedNode.SubnetMask;
           HostName = inUnmanagedNode.DeviceName;
           OSDecription = inUnmanagedNode.OSDescription;
           if ( inUnmanagedNode.TopGroupName.Equals("Printers" ) || inUnmanagedNode.GroupName.Equals("Wireless Access Points" ) )
           {
               Type = inUnmanagedNode.TopGroupName;  // Leave as "" if not from one of these two groups.
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
        private String GetLDMainPathFromReg()
        {
            var FilePath = @"C:\Program Files\LANDesk\ManagementSuite\";
            var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\LANDesk\ManagementSuite\Setup");
            return key.GetValue("LdmainPath", FilePath).ToString();
        }

        public void WriteMiniscan()
        {
            var path = GetLDMainPathFromReg() + @"ldscan\" + DeviceName + ".tmp";
             
            if (!File.Exists(path))
            {
                // Create a file to write to.
                var sw = File.CreateText(path);

                if (! Type.Equals(""))
                { 
                    sw.WriteLine("Type = {0}", Type); 
                }
                if (! NICAddress.Equals(""))
                {
                    sw.WriteLine("Network - NIC Address = {0}", NICAddress);
                }
                if (! IPAddress.Equals(""))
                {
                    sw.WriteLine("Network - TCPIP - Address = {0}", IPAddress);
                }
                if (! SubnetMask.Equals(""))
                {
                    sw.WriteLine("Network - TCPIP - Subnet Mask = {0}", SubnetMask);
                }
                if (! DeviceName.Equals(""))
                {
                    sw.WriteLine("Device Name = {0}", DeviceName);
                }
                if (!OSDecription.Equals("") && !OSDecription.ToLower().Equals("unknown") && !(null == OSDecription) )
                {
                    sw.WriteLine("OS - Name = {0}", OSDecription);
                }
                if (! HostName.Equals(""))
                {
                    sw.WriteLine("Network - TCPIP - Host Name = {0}", HostName);
                }
                sw.WriteLine("");
                sw.Close();
                var miniscanPath = GetLDMainPathFromReg() + @"ldscan\" + DeviceName + ".ims";
                File.Move(path, miniscanPath);
            }
        }
    }
}
