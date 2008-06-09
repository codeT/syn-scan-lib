#region Using directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using y97523.net;
using System.Net;

#endregion

namespace GuiTest
{
    partial class Form1 : Form
    {
        ISniffer snf1;
        public Form1()
        {
            InitializeComponent();
            snf1 = new RawSocketSniffer();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                snf1.OnPackage += new _onPackage(snf1_OnPackage);
                snf1.start(IPAddress.Parse("0.0.0.0"));
            }
            else
            {
                snf1.stop();
            }
        }

        void snf1_OnPackage(byte[] bytes)
        {
            pak = bytes;
            Invoke(new MethodInvoker(onPackage));
        }

        byte[] pak;
        void onPackage()
        {
            IPEndPoint ipEndpoint;
            if (SYNPackage.MatchSYNACKPackage(pak, out ipEndpoint))
                lsbPackage.Items.Add(ipEndpoint.Address.ToString() + ":" + ipEndpoint.Port.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SYNSender sender1 = new SYNSender(IPAddress.Parse("218.92.68.155"));
//            while(true)
                sender1.SendSYN(new IPEndPoint(IPAddress.Parse(textBox1.Text),int.Parse( textBox2.Text)));
        }
    }
}