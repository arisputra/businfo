using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Businfo
{
    
    public partial class frmFlash : Form
    {
        public frmFlash()
        {
            InitializeComponent();
            timer1.Start();
    
        }

        [STAThread]
        static void Main()
        {
            Application.Run(new frmFlash());

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
    }
}