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
            m_strLog = textBox1.Text + comboBox2.Text;
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("վ�����Ʋ���Ϊ�գ�\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            nIndex = fields.FindField("StationCharacter");//�ĳ���վ�����ڵ�·
            m_pFeature.set_Value(nIndex, comboBox1.Text);

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
            //û����ʾ�����Ǳ�����
            //nIndex = fields.FindField("StationMaterial");
            //m_pFeature.set_Value(nIndex, textBox7.Text);
            nIndex = fields.FindField("StationStyle");//ΪStationCharacter��StationMaterial��StationStyle�������
            m_pFeature.set_Value(nIndex, comboBox3.Text);
            nIndex = fields.FindField("StationAlias");
            m_pFeature.set_Value(nIndex, textBox9.Text);
            m_pFeature.Store();

            EngineFuntions.m_AxMapControl.Map.ClearSelection();
            EngineFuntions.m_AxMapControl.ActiveView.GraphicsContainer.DeleteAllElements();
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
                MessageBox.Show("վ�����·̫Զ��\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            int nlen;
            int.TryParse(ForBusInfo.GetProfileString("StationDirect", "Num", Application.StartupPath + "\\Businfo.ini"), out nlen);
            for (int i = 0; i < nlen;i++)
            {
                comboBox2.Items.Add(ForBusInfo.GetProfileString("StationDirect", string.Format("���{0}", i + 1), Application.StartupPath + "\\Businfo.ini"));
            }
            comboBox2.SelectedIndex = 0;
            int.TryParse(ForBusInfo.GetProfileString("StationMaterial", "Num", Application.StartupPath + "\\Businfo.ini"), out nlen);
            for (int i = 0; i < nlen; i++)
            {
                comboBox3.Items.Add(ForBusInfo.GetProfileString("StationMaterial", string.Format("���{0}", i + 1), Application.StartupPath + "\\Businfo.ini"));
            }
            comboBox3.SelectedIndex = 0;
        }
    }
}