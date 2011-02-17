using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Businfo.Globe;

namespace Businfo
{
    
    public partial class frmFlash : Form
    {
        public frmFlash()
        {
            InitializeComponent();
        }

        [STAThread]
        static void Main()
        {
            Application.Run(new frmMainNew());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0)
            {
                this.Opacity = this.Opacity - 0.02;
            }
            else
            {
                this.Hide() ;
                timer1.Stop();
                frmMainNew frmmain = new frmMainNew();
                frmmain.m_frmFlash = this;
                frmmain.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text.ToLower() == "admin")
            {
                ForBusInfo.Login_name = textBox1.Text;
                timer1.Start();
            }
            else
            {
                MessageBox.Show("密码错误，请重新输入\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}