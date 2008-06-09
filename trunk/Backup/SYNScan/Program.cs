#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using y97523.net;
using System.Threading;

#endregion

namespace SYNScan
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress startIP =null, endIP =null ;
            int port = 0, IPInterfaceID = -1;

            if (!(args.Length == 3 || args.Length == 4)) useage();
            try
            {
                startIP = IPAddress.Parse(args[0]);
                endIP = IPAddress.Parse(args[1]);
                port = int.Parse(args[2]);
                if (args.Length == 4)
                    IPInterfaceID = int.Parse(args[3]);
                else
                    IPInterfaceID = -1;
            }
            catch 
            {
                useage();
            }

            IPAddress localhost;
            if (IPInterfaceID == -1)
                localhost = GetlocalIP();
            else
                localhost = Dns.GetHostByName(Dns.GetHostName()).AddressList[IPInterfaceID];
            SYNScaner scaner = new SYNScaner(localhost);
            scaner.OnFind +=new _onFind(scaner_OnFind);
            scaner.Scan(startIP, endIP, port);
        }

        static void useage()
        {
            Console.WriteLine("y97523's SYN Scaner");
            Console.WriteLine("useage:");
            Console.WriteLine("  SYNScan startIP endIP port [IPInterfaceID]");
        }
        private static IPAddress GetlocalIP()
        {
            IPAddress[] localhosts = Dns.GetHostByName(Dns.GetHostName()).AddressList;
            for (int i = 0; i < localhosts.Length; i++)
                Console.WriteLine(i.ToString() + ":" + localhosts[i].ToString());
            int x = -1;
            while (!(x >= 0 && x < localhosts.Length))
            {
                Console.Write("Please select the IP interface:");
                x = int.Parse(Console.ReadLine());
            }
            IPAddress localhost = localhosts[x];
            return localhost;
        }

        static void scaner_OnFind(IPAddress address)
        {
            Console.WriteLine(address.ToString());
        }
    }
}
