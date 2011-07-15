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
        public string m_strLog;
        public List<string> m_ListRoadName = new List<string>();
        public frmStationPara()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double B, L, H;
            m_strLog = textBox1.Text + comboBox2.Text;
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("站点名称不能为空！\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.None;
                return;
            }
            m_pFeature = EngineFuntions.m_Layer_BusStation.FeatureClass.CreateFeature();
            m_pFeature.Shape = m_mapPoint;

            IFields fields = m_pFeature.Fields;
            int nIndex = fields.FindField("StationName");
            m_pFeature.set_Value(nIndex, textBox1.Text);
            nIndex = fields.FindField("Direct");
            m_pFeature.set_Value(nIndex, comboBox2.Text);
            nIndex = fields.FindField("StationCharacter");//改成了站点所在道路
            m_pFeature.set_Value(nIndex, comboBox1.Text);

            nIndex = fields.FindField("GPSLongtitude");
            if (double.TryParse(textBox4.Text, out L))
                m_pFeature.set_Value(nIndex, L);
            nIndex = fields.FindField("GPSLatitude");
            if (double.TryParse(textBox5.Text, out  B))
                m_pFeature.set_Value(nIndex, B);
            nIndex = fields.FindField("GPSHigh");
            if (double.TryParse(textBox6.Text, out  H))
                m_pFeature.set_Value(nIndex, H);
            nIndex = fields.FindField("StationMaterial");//改成了邻近标识物
            m_pFeature.set_Value(nIndex, textBox2.Text);
            nIndex = fields.FindField("StationStyle");//为StationCharacter、StationMaterial、StationStyle结合内容
            m_pFeature.set_Value(nIndex, comboBox3.Text);
            nIndex = fields.FindField("StationAlias");
            m_pFeature.set_Value(nIndex, textBox9.Text);

            if (B > 30 && L > 114)
            {
                double x, y, z;
                x = y = z = 0;
                CoordTrans Coord = new CoordTrans(162.8998, 216.8504, 133.8944, 0.72814164, 2.73301875, -5.38285723, -9.06757729, 114, 3);
                Coord.BLHto84XYZ(B, L, 0, ref y, ref x, ref z);
                IPoint outPoint = new PointClass();
                outPoint.PutCoords(x, y-3000000);
                m_pFeature.Shape = outPoint;
            }
            m_pFeature.Store();

            
        }

        private void frmStationPara_Load(object sender, EventArgs e)
        {
            foreach (string strRoadname in m_ListRoadName)
            {
                comboBox1.Items.Add(strRoadname);
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("站点离道路太远！\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            int nlen;
            int.TryParse(ForBusInfo.GetProfileString("StationDirect", "Num", Application.StartupPath + "\\Businfo.ini"), out nlen);
            for (int i = 0; i < nlen;i++)
            {
                comboBox2.Items.Add(ForBusInfo.GetProfileString("StationDirect", string.Format("编号{0}", i + 1), Application.StartupPath + "\\Businfo.ini"));
            }
            comboBox2.SelectedIndex = 0;
            int.TryParse(ForBusInfo.GetProfileString("StationMaterial", "Num", Application.StartupPath + "\\Businfo.ini"), out nlen);
            for (int i = 0; i < nlen; i++)
            {
                comboBox3.Items.Add(ForBusInfo.GetProfileString("StationMaterial", string.Format("编号{0}", i + 1), Application.StartupPath + "\\Businfo.ini"));
            }
            comboBox3.SelectedIndex = 0;
        }
    }
}