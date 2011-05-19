using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Businfo.Globe;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;

namespace Businfo
{
    public partial class frmFacilitiesPane : UserControl
    {
        public Int32 m_nCurRowIndex;
        public IFeature m_pCurFeature;
        public List<IFeature> m_featureCollection = new List<IFeature>();  //得到所有选中的feature
        public frmFacilitiesPane()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            if (String.IsNullOrEmpty(TextBox1.Text))
            {
                RefreshGrid();
            }
            else
            {
                ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Station_FillByStationName, string.Format(" WHERE (StationName LIKE '%{0}%')", TextBox1.Text), new string[] { "" });
            }
            int nNum = 1;
            foreach (DataGridViewRow eRow in DataGridView1.Rows)
            {
                eRow.HeaderCell.Value = nNum++.ToString();
            }
        }

        private void frmFacilitiesPane_Load(object sender, EventArgs e)
        {
            RefreshGrid();
            int nNum = 1;
            foreach (DataGridViewRow eRow in DataGridView1.Rows)
            {
                eRow.HeaderCell.Value = nNum++.ToString();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                foreach (DataGridViewRow eRow in DataGridView1.Rows)
                {
                    eRow.Cells[0].Value = true;
                }
            }
            else
            {
                foreach (DataGridViewRow eRow in DataGridView1.Rows)
                {
                    eRow.Cells[0].Value = false;
                }
            }
        }

        private void 定位到ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_pCurFeature != null)
            {
                IPoint pPoint;
                IEnvelope pEnvelope;
                IDisplayTransformation pDisplayTransformation;
                pDisplayTransformation = EngineFuntions.m_AxMapControl.ActiveView.ScreenDisplay.DisplayTransformation;
                pEnvelope = m_pCurFeature.Extent;
                pPoint = pEnvelope.UpperLeft;
                pEnvelope = pDisplayTransformation.VisibleBounds;
                pEnvelope.CenterAt(pPoint);
                pDisplayTransformation.VisibleBounds = pEnvelope;
                EngineFuntions.m_AxMapControl.Map.MapScale = 2000;
                pDisplayTransformation.VisibleBounds = EngineFuntions.m_AxMapControl.ActiveView.Extent;
                EngineFuntions.m_AxMapControl.ActiveView.ScreenDisplay.Invalidate(null, true, (short)esriScreenCache.esriAllScreenCaches);
                Application.DoEvents();
                EngineFuntions.FlashShape(m_pCurFeature.ShapeCopy);
            }
        }

        private void 编辑属性ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_featureCollection.Clear();
            DataGridView1.EndEdit();
            IFeature pCurFeature;
            bool bCheck = false;
            if (checkBox1.Checked == true && TextBox1.Text == "")
            {
                m_featureCollection = EngineFuntions.GetSeartchFeatures(EngineFuntions.m_Layer_BusStation, "OBJECTID > -1");//.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusStation, "OBJECTID", eRow.Cells["OBJECTID"].Value.ToString());
                //m_featureCollection.Add(pCurFeature);
                bCheck = true;
            }
            else
            {
                foreach (DataGridViewRow eRow in DataGridView1.Rows)
                {
                    if (eRow.Cells[0].Value != null && (bool)eRow.Cells[0].Value == true)
                    {
                        pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusStation, "OBJECTID", eRow.Cells[1].Value.ToString());
                        m_featureCollection.Add(pCurFeature);
                        bCheck = true;
                    }
                }
            }
            
            frmStationAllInfo frmPopup = new frmStationAllInfo();
            if (!bCheck)
            {
                if (m_pCurFeature != null)
                {
                    m_featureCollection.Add(m_pCurFeature);
                    frmPopup.m_featureCollection = m_featureCollection;
                    frmPopup.m_strField = "设备管理";
                    frmPopup.ShowDialog();
                }
            }
            else
            {
                frmPopup.m_featureCollection = m_featureCollection;
                frmPopup.m_strField = "设备管理";
                frmPopup.ShowDialog();
                //frmPopup.RefreshSelectGrid();
            }
        }

        private void 全景浏览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_pCurFeature != null)
            {
                frmPano frmPopup = new frmPano();
                frmPopup.m_strURL = "E:\\Code For Working\\BusInfo\\bin\\Debug\\Data\\A01\\pano1.html";
                frmPopup.Show();
            }
        }

        public void RefreshGrid()
        {
            ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Station_FillPan, "", new string[] { "" });
        }

        private void DataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            m_pCurFeature = null;
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.RowIndex <= DataGridView1.Rows.Count)
            {
                m_nCurRowIndex = e.RowIndex;
                DataGridView1.Rows[m_nCurRowIndex].Selected = true;
                ContextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                m_pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusStation, "OBJECTID", DataGridView1.Rows[m_nCurRowIndex].Cells[1].Value.ToString());
            }
        }

    }
}
