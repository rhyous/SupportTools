using System;
using System.Diagnostics;
using LANDesk.ManagementSuite.WinConsole;

namespace UnmanagedToManaged
{
    class Program
    {
        private static string ip, name, mac;
        private static UnmanagedNode un;

        static void Main(string[] args)
        {
            CheckArgs(args);
            if ( !(ip.Equals("")) && !(null == ip) )
            {
                un = new UnmanagedNode(ip, UnmanagedNode.UnmanagedNodeDataType.IPAddress);
            }
            else if (!(name.Equals("")) && !(null == name))
            {
                un = new UnmanagedNode(name, UnmanagedNode.UnmanagedNodeDataType.Nodename);
            }
            else if (!(mac.Equals("")) && !(null == mac))
            {
                un = new UnmanagedNode(mac, UnmanagedNode.UnmanagedNodeDataType.PhysAddress);
            }

            // Computer is an object from LANDesk.ManagementSuite.WinConsole
            var c = Computer.Add(un.DeviceName, un.GroupName, un.IPAddress, un.DeviceName);
            var i = c.ID;

            // Todo: Check if the "Managed Printers" and "Managed Wireless Access Points" groups exist.
            // If not, create the appropriate one for the device.
            // Then move the newly added managed node to the group.
            // INSERT INTO CustomGroupComputer (CustomGroup_Idn, Member_Idn) VALUES ({0}, {1})
        }

        private static void CheckArgs(string[] args)
        {
            for (var i = 0; i < args.Length; i++)
            {
                var splitArg = args[i].Trim().ToLower().Split('=');
                try
                {
                    switch (splitArg[0])
                    {
                        case "ip":
                            ip = splitArg[1];
                            break;
                        case "name":
                            name = splitArg[1];
                            break;
                        case "mac":
                            mac = splitArg[1];
                            break;
                        default:
                            PrintArgs(87); // 87 is Microsoft's ERROR_INVALID_PARAMETER
                            break;
                    }
                }
                catch 
                {
                    PrintArgs(87); // 87 is Microsoft's ERROR_INVALID_PARAMETER
                }
            }

            if ( (   ip == "" || null == ip   ) &&
                 ( name == "" || null == name ) && 
                 (  mac == "" || null == mac  ) )
            {
                PrintArgs(87); // 87 is Microsoft's ERROR_INVALID_PARAMETER
            }
        }

        private static void PrintArgs()
        {
            PrintArgs(0);
        }

        private static void PrintArgs(int inExitCode)
        {
            var p = Process.GetCurrentProcess();
            Console.WriteLine("");
            Console.WriteLine("  Usage:");
            Console.WriteLine("  " + p.ProcessName + ".exe ip=<address> name=<Name> mac=<Mac Address>");
            Console.WriteLine("  ");
            Environment.Exit(inExitCode);
        }

    }
}
