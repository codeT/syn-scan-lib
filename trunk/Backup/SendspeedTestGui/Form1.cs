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

namespace SendspeedTestGui
{
    partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SYNSender send = new SYNSender(IPAddress.Parse("172.28.160.251"));
            while (true)
                send.SendSYN(new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text)));

        }
    }
}