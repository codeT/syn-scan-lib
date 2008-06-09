#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

#endregion

namespace y97523.net
{

    public  delegate void _onPackage(byte[] bytes);
    public interface ISniffer
    {
        event _onPackage OnPackage;//收到数据包
        void start(IPAddress alocalhost);//开始接收
        void stop();//停止接收
    }

    public class RawSocketSniffer : ISniffer
    {
        #region 私有变量

        IPAddress localhost;
        bool stopFlag;
        Socket socket1;

        #endregion

        #region 公开的接口

        /// <summary>
        /// 收到数据包
        /// </summary>
        public event _onPackage OnPackage;

        /// <summary>
        /// 开始接收
        /// </summary>
        /// <param name="alocalhost"></param>
        public void start(IPAddress alocalhost)
        {
            localhost = alocalhost;
            Thread thd = new Thread(new ThreadStart(start1));
            thd.IsBackground = true;
            thd.Start();
        }

        /// <summary>
        /// 停止接收
        /// </summary>
        public void stop()
        {
            stopFlag = true;
        }

        /// <summary>
        /// 获得本机IP地址表
        /// </summary>
        /// <returns></returns>
        public IPAddress[] GetlocalIPs()
        {
            return Dns.GetHostByName(Dns.GetHostName()).AddressList;
        }

        #endregion

        #region 私有函数

        /// <summary>
        /// 建立row socket
        /// </summary>
        /// <param name="localhost">本机ip</param>
        /// <returns></returns>
        private Socket CreateSnifferSocket(IPAddress localhost)
        {
            Socket soc = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Raw,
                ProtocolType.IP);
            soc.Bind(new IPEndPoint(localhost, 0));

            soc.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);

            byte[] outbyte = new byte[4];
            soc.IOControl(IOControlCode.ReceiveAll,
                new byte[4] { 1, 0, 0, 0 },
                outbyte);
            if (BitConverter.ToInt32(outbyte, 0) != 0)
                throw new SocketException();

            return soc;
        }



        /// <summary>
        /// 接收数据包线程
        /// </summary>
        private void start1()
        {
            //prepaie
            using (socket1 = CreateSnifferSocket(localhost))
            {
                //start
                byte[] buffer = new byte[1500];
                try
                {
                    stopFlag = false;
                    while (!stopFlag)
                    {
                        int size = socket1.Receive(buffer);

                        byte[] data = new byte[size];
                        Buffer.BlockCopy(buffer, 0, data, 0, size);
                        if (OnPackage != null)
                        {
                            OnPackage(data);
                        }
                    }
                }
                catch (SocketException e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        #endregion

    }
}
