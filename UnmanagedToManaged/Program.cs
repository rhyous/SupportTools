using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LANDesk.ManagementSuite.WinConsole;

namespace UnmanagedToManaged
{
    class Program
    {
        private static string _IP, _Name, _Mac;
        private static UnmanagedNode _UnmanagedNode;

        static void Main(string[] args)
        {
            CheckArgs(args);
            if (!(_IP.Equals("")) && null != _IP)
            {
                _UnmanagedNode = new UnmanagedNode(_IP, UnmanagedNode.UnmanagedNodeDataType.IPAddress);
            }
            else if (!(_Name.Equals("")) && null != _Name)
            {
                _UnmanagedNode = new UnmanagedNode(_Name, UnmanagedNode.UnmanagedNodeDataType.Nodename);
            }
            else if (!(_Mac.Equals("")) && null != _Mac)
            {
                _UnmanagedNode = new UnmanagedNode(_Mac, UnmanagedNode.UnmanagedNodeDataType.PhysAddress);
            }

            // Computer is an object from LANDesk.ManagementSuite.WinConsole
            // var c = 
            Computer.Add(_UnmanagedNode.DeviceName, _UnmanagedNode.GroupName, _UnmanagedNode.IPAddress, _UnmanagedNode.DeviceName);
            // var i = c.ID;

            // Todo: Check if the "Managed Printers" and "Managed Wireless Access Points" groups exist.
            // If not, create the appropriate one for the device.
            // Then move the newly added managed node to the group.
            // INSERT INTO CustomGroupComputer (CustomGroup_Idn, Member_Idn) VALUES ({0}, {1})
        }

        private static void CheckArgs(IEnumerable<string> args)
        {
            foreach (var splitArg in args.Select(t => t.Trim().ToLower().Split('=')))
            {
                try
                {
                    switch (splitArg[0])
                    {
                        case "ip":
                            _IP = splitArg[1];
                            break;
                        case "name":
                            _Name = splitArg[1];
                            break;
                        case "mac":
                            _Mac = splitArg[1];
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

            if (string.IsNullOrWhiteSpace(_IP) && string.IsNullOrWhiteSpace(_Name) && string.IsNullOrWhiteSpace(_Mac))
            {
                PrintArgs(87); // 87 is Microsoft's ERROR_INVALID_PARAMETER
            }
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
