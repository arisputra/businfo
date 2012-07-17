using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Threading;

namespace Businfo
{
    public partial class frmLoading : Form
    {
        public int m_nType;
        public frmLoading()
        {
            InitializeComponent();
        }

        private void frmLoading_Load(object sender, EventArgs e)
        {
            switch (m_nType)
            {
                case 2:
                    label1.Image = Businfo.Properties.Resources.UpLoading ;
                    break;

                default:
                    break;

            }

        }
        
    }
}
