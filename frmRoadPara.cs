using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Utility;
using Businfo.Globe;

namespace Businfo
{
    public partial class frmRoadPara : Form
    {
        public IFeature m_pFeature;
        public IPolyline m_pPolyline;
        public frmRoadPara()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("线路名称不能为空！\n", "提示", MessageBoxButtons.OK);
                this.DialogResult = DialogResult.None;
                return;
            }
            m_pFeature = EngineFuntions.m_Layer_BusRoad.FeatureClass.CreateFeature();
            m_pFeature.Shape = m_pPolyline;
            IFields fields = m_pFeature.Fields;
            int nIndex = fields.FindField("RoadName");
            m_pFeature.set_Value(nIndex, textBox1.Text);
            nIndex = fields.FindField("RoadType");
            m_pFeature.set_Value(nIndex, textBox2.Text);
            nIndex = fields.FindField("RoadTravel");
            m_pFeature.set_Value(nIndex, textBox3.Text);

            double rNumber;
            nIndex = fields.FindField("TicketPrice1");
            if (double.TryParse(textBox4.Text, out rNumber))
                m_pFeature.set_Value(nIndex, rNumber);
            nIndex = fields.FindField("TicketPrice2");
            if (double.TryParse(textBox5.Text, out  rNumber))
                m_pFeature.set_Value(nIndex, rNumber);
            nIndex = fields.FindField("TicketPrice3");
            if (double.TryParse(textBox6.Text, out  rNumber))
                m_pFeature.set_Value(nIndex, rNumber);

            nIndex = fields.FindField("FirstStartTime");
            m_pFeature.set_Value(nIndex, textBox7.Text);
            nIndex = fields.FindField("FirstCloseTime");
            m_pFeature.set_Value(nIndex, textBox8.Text);
            nIndex = fields.FindField("EndStartTime");
            m_pFeature.set_Value(nIndex, textBox9.Text);
            nIndex = fields.FindField("EndCloseTim");
            m_pFeature.set_Value(nIndex, textBox10.Text);
            m_pFeature.Store();
        }
    }
}