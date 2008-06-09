namespace GuiTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button2 = new System.Windows.Forms.Button();
            this.lsbPackage = new System.Windows.Forms.ListBox();
            this.lsbIP = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
// 
// button2
// 
            this.button2.Location = new System.Drawing.Point(245, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(35, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Send";
            this.button2.Click += new System.EventHandler(this.button2_Click);
// 
// lsbPackage
// 
            this.lsbPackage.FormattingEnabled = true;
            this.lsbPackage.ItemHeight = 12;
            this.lsbPackage.Location = new System.Drawing.Point(13, 62);
            this.lsbPackage.Name = "lsbPackage";
            this.lsbPackage.Size = new System.Drawing.Size(141, 196);
            this.lsbPackage.TabIndex = 2;
// 
// lsbIP
// 
            this.lsbIP.FormattingEnabled = true;
            this.lsbIP.ItemHeight = 12;
            this.lsbIP.Location = new System.Drawing.Point(161, 62);
            this.lsbIP.Name = "lsbIP";
            this.lsbIP.Size = new System.Drawing.Size(119, 196);
            this.lsbIP.TabIndex = 3;
// 
// label1
// 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "Package";
// 
// label2
// 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "SYNIP";
// 
// textBox1
// 
            this.textBox1.Location = new System.Drawing.Point(95, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(86, 21);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "218.92.68.1";
// 
// textBox2
// 
            this.textBox2.Location = new System.Drawing.Point(188, 13);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(41, 21);
            this.textBox2.TabIndex = 7;
            this.textBox2.Text = "80";
// 
// checkBox1
// 
            this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(13, 11);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(56, 22);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Listion";
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
// 
// Form1
// 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lsbIP);
            this.Controls.Add(this.lsbPackage);
            this.Controls.Add(this.button2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox lsbPackage;
        private System.Windows.Forms.ListBox lsbIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

