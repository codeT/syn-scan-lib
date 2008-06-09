#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

#endregion

namespace y97523.net
{
    /// <summary>
    /// 发送自定义IP包
    /// </summary>
    public class IPSender:IDisposable
    {
        Socket sock1;

        public IPSender()
        {
            sock1 = CreateRowSocket();
        }

         /// <summary>
         /// 创建row socket
         /// </summary>
         /// <param name="localhost">目标地址</param>
         /// <returns></returns>
        private Socket CreateRowSocket()
        {
            Socket soc = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Raw,
                ProtocolType.IP);
            soc.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);

            return soc;
        }

        /// <summary>
        /// 销毁socket对象
        /// </summary>
        public void Close()
        {
            this.Dispose();
        }

        public void SendTo(byte[] package,IPEndPoint remoteEndPoint)
        {
//            sock1.SendTo(package, remoteEndPoint);
            sock1.SendTo(new byte[] { 0x45,0x00,0x00,0x30,0xa9,0x45,0x40,0x00,0x68,0x06,0xa8,0x4f,0xac,0xbb,0xf5,0xc7,0xda,0x5c,0x44,0x53,0xda,0x34,0x12,0x36,0x53,0xce,0xa0,0x28,0x00,0x00,0x00,0x00,0x70,0x02,0x40,0x00,0xa1,0xee,0x00,0x00,0x02,0x04,0x05,0x50,0x01,0x01,0x04,0x02 }, new IPEndPoint(IPAddress.Parse("218.92.68.83"),4662));
        }

        #region IDisposable Members

        public void Dispose()
        {
            if(sock1 != null)
                sock1.Close();
        }

        #endregion
    }

   /// <summary>
   /// 发送SYN包到指定IP的指定端口
   /// </summary>
    public class SYNSender
    {
        IPSender ipSender1;
        SYNPackage synPackage1;

        public SYNSender(IPAddress localhost)
        {
            ipSender1 = new IPSender();
            synPackage1 = new SYNPackage();
            synPackage1.SourceIP = localhost;
        }

        /// <summary>
        /// 发送SYN包
        /// </summary>
        /// <param name="remoteEndpoint">目标地址</param>
        public void SendSYN(IPEndPoint remoteEndpoint)
        {
            synPackage1.DestPort = remoteEndpoint.Port;
            synPackage1.DestIP = remoteEndpoint.Address;
            ipSender1.SendTo(synPackage1.ToBytes(), remoteEndpoint);
        }

        /// <summary>
        /// 销毁socket对象
        /// </summary>
        public void Close()
        {
            ipSender1.Close();
        }
    }
}
