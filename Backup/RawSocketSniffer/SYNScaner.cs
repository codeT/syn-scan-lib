#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;

#endregion

namespace y97523.net
{
        public delegate void _onFind(IPAddress address);
    public class SYNScaner
    {
        public event _onFind OnFind;
            SYNSender sender;
            RawSocketSniffer recever;
        long start, end;
        int port;
        IPAddress localhost;

        public SYNScaner(IPAddress localhost)
        {
            this.localhost = localhost;
            sender = new SYNSender(localhost);
            recever = new RawSocketSniffer();
            recever.OnPackage +=new _onPackage(recever_OnPackage);
        }

        public void Scan(IPAddress startIP, IPAddress endIP, int port)
        {
            this.port = port;
            this.start = IPAddress.NetworkToHostOrder((int)startIP.Address);
            this.end = IPAddress.NetworkToHostOrder((int)endIP.Address);
            if (start > end)
                throw new Exception("开始IP必须大于结束IP!");
            if (port < 1 || port > 65535)
                throw new Exception("端口号应在1-65535之间");

            recever.start(localhost);
            Thread.CurrentThread.IsBackground = true;
            Thread.CurrentThread.Priority = ThreadPriority.Lowest;
            send();
        }

        void send()
        {
            for (long i = start; i <= end; i++)
            {
                if (((i % 256) == 0) || ((i % 256) == 255))
                    continue;
                sender.SendSYN(new IPEndPoint(((long)IPAddress.HostToNetworkOrder((int)i)) & 0xffffffff,port));
            }
            Thread.Sleep(1000);
            recever.stop();
        }

        void recever_OnPackage(byte[] bytes)
        {
            IPEndPoint ipEndpoint;
            if (SYNPackage.MatchSYNACKPackage(bytes, out ipEndpoint))
                if (port == ipEndpoint.Port)
                    if (OnFind != null)
                        OnFind(ipEndpoint.Address);
        }
    }
}
