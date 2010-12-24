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
    public partial class frmStationPara : Form
    {
        public IPoint m_mapPoint;
        public IFeature m_pFeature;
        public frmStationPara()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("站点名称不能为空！\n", "应用程序错误", MessageBoxButtons.OK);
                this.DialogResult = DialogResult.None;
                return;
            }
            m_pFeature = EngineFuntions.m_Layer_BusStation.FeatureClass.CreateFeature();
            m_pFeature.Shape = m_mapPoint;
            IFields fields = m_pFeature.Fields;
            int nIndex = fields.FindField("StationName");
            m_pFeature.set_Value(nIndex, textBox1.Text);
            nIndex = fields.FindField("Direct");
            m_pFeature.set_Value(nIndex, textBox2.Text);
            nIndex = fields.FindField("StationCharacter");
            m_pFeature.set_Value(nIndex, textBox3.Text);

           
            double rNumber;
            nIndex = fields.FindField("GPSLongtitude");
            if (double.TryParse(textBox4.Text, out rNumber))
                m_pFeature.set_Value(nIndex, rNumber);
            nIndex = fields.FindField("GPSLatitude");
            if (double.TryParse(textBox5.Text, out  rNumber))
                m_pFeature.set_Value(nIndex, rNumber);
            nIndex = fields.FindField("GPSHigh");
            if (double.TryParse(textBox6.Text, out  rNumber))
                m_pFeature.set_Value(nIndex, rNumber);
            nIndex = fields.FindField("StationMaterial");
            m_pFeature.set_Value(nIndex, textBox7.Text);
            nIndex = fields.FindField("StationStyle");
            m_pFeature.set_Value(nIndex, textBox8.Text);
            nIndex = fields.FindField("StationAlias");
            m_pFeature.set_Value(nIndex, textBox9.Text);
            m_pFeature.Store();
        }
    }
}