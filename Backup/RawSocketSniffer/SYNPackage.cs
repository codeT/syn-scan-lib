#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

#endregion

namespace y97523.net
{
    public interface IPackageGen
    {
        byte[] ToBytes();
    }

    public class IPPackage : IPackageGen
    {
        #region 字段
        byte[] data = {
            0x45,0x00,0x00,0x30,0x00,0x75,0x40,0x00,0x80,0x06,0xe6,0xf6,0xda,0x5c,0x44,0x1e,0xd3,0x98,0x21,0x49
        }; 
        #endregion

        #region 属性
        const int _protocol = 0x09;
        /// <summary>
        /// 下一层的协议
        /// </summary>
        /// <value>协议代号</value>
        public byte Protocol
        {
            set { data[_protocol] = value; }
        }

        const int _sourceIP = 0x0c;
        /// <summary>
        /// 源IP地址
        /// </summary>
        /// <value></value>
        public IPAddress SourceIP
        {
            set { SetIP(value, _sourceIP); }
        }

        const int _destIP = 0x10;
        /// <summary>
        /// 目标IP地址
        /// </summary>
        /// <value></value>
        public IPAddress DestIP
        {
            set { SetIP(value, _destIP); }
        }

        private void SetIP(IPAddress value, int ipOffset)
        {

            byte[] temp = value.GetAddressBytes();
            Buffer.BlockCopy(temp, 0, data, ipOffset, 4);
        }

        /// <summary>
        /// 返回TCP头计算checksum所需的ip头数据
        /// </summary>
        /// <returns></returns>
        public long GetTcpCheckSumHelp()
        {
            long sum = 0;
            sum += data[_sourceIP] * 0x100 + data[_sourceIP + 1] + 
                data[_sourceIP + 2] * 0x100 + data[_sourceIP + 3];
            sum += data[_destIP] * 0x100 + data[_destIP + 1] + 
                data[_destIP + 2] * 0x100 + data[_destIP + 3];
            sum += data[_protocol];
            return sum;
        }
        #endregion

        #region IPackageGen Members

        public byte[] ToBytes()
        {
            return data;
        }

        #endregion
    }

    public class SYNPackage : IPackageGen
    {
        #region 字段
        byte[] data = {
            0x04,0x1d,0x00,0x50,0xa4,0x72,0xf0,0x7d,0x00,0x00,0x00,0x00,0x70,0x02,0xff,0xff,0xd6,0x79,0x00,0x00,0x02,0x04,0x05,0xa0,0x01,0x01,0x04,0x02
        };
        IPPackage ipPackage1 = new IPPackage(); 
        #endregion

        #region 构造函数
        public SYNPackage()
        {
            ipPackage1.Protocol = 0x06;//TCP
        } 
        #endregion

        #region 属性
        const int _sourcePort = 0x00;
        public int SourcePort
        {
            set { SetPort(value, _sourcePort); }
        }

        const int _destPort = 0x02;
        public int DestPort
        {
            set { SetPort(value, _destPort); }
        }

        void SetPort(int port, int portOffset)
        {
            data[portOffset] = (byte)((port >> 8) & 0xff);
            data[portOffset + 1] = (byte)(port & 0xff);
        }

        public IPAddress  SourceIP
        {
            set { ipPackage1.SourceIP = value; }
        }

        public IPAddress  DestIP
        {
            set { ipPackage1.DestIP = value; }
        }

        #endregion

        #region IPackageGen Members

        /// <summary>
        /// 生成数据包
        /// </summary>
        /// <returns>byte[]格式的数据包</returns>
        public byte[] ToBytes()
        {
            SetCheckSum();

            byte[] ipData = ipPackage1.ToBytes();
            byte[] result = new byte[ipData.Length + data.Length];
            Buffer.BlockCopy(ipData, 0, result, 0, ipData.Length);
            Buffer.BlockCopy(data, 0, result, ipData.Length, data.Length);
            return result;
        }

        #endregion

        #region 功能函数
        /// <summary>
        /// 判断是否为Syn ack数据包
        /// </summary>
        /// <param name="package">数据包</param>
        /// <param name="ipEndPoint">syn ack数据包的远程地址</param>
        /// <returns></returns>
        public static bool MatchSYNACKPackage(byte[] package, out IPEndPoint ipEndPoint)
        {
            if (package[0x09] == 0x06)//TCP
                if (package[0x21] == 0x12)//SYN
                {
                    int port = package[0x14] * 0x100 + package[0x15];

                    byte[] ipByte = new byte[4];
                    Buffer.BlockCopy(package, 0x0c, ipByte, 0, 4);
                    IPAddress ip = new IPAddress(ipByte);

                    ipEndPoint = new IPEndPoint(ip, port);
                    return true;
                }
            ipEndPoint = null;
            return false;

        } 

        const int _checkSum = 0x10;
        /// <summary>
        /// 设置TCP头的效验和
        /// </summary>
        void SetCheckSum()
        {
            data[_checkSum] = 0;
            data[_checkSum + 1] = 0;

            long sum = 0;
            for (int i = 0; i < data.Length ; i += 2)
                sum += data[i + 1] + data[i] * 0x100;

            //加上TCP伪首部,源IP＋目的IP＋协议+TCP头大小
            sum += ipPackage1.GetTcpCheckSumHelp();
            sum += data.Length;

            while ((sum >> 16) > 0)
                sum = (sum & 0xffff) + (sum >> 16);
            sum = ~sum;
            data[_checkSum] = (byte)((sum >> 8) & 0xff);
            data[_checkSum + 1] = (byte)(sum & 0xff);
        }
        #endregion
    }
}

