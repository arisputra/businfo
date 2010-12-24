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
using ESRI.ArcGIS.Carto;


namespace Businfo
{
    public partial class frmStationPane : UserControl
    {
        public Int32 m_nCurRowIndex;
        public IFeature m_pCurFeature;
        public List<IFeature> m_featureCollection = new List<IFeature>();  //得到所有选中的feature
        public frmStationPane()
        {
            InitializeComponent();
        }

        private void fillByStationNameToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.公交站点TableAdapter.FillByStationName(this.stationDataSet.公交站点, stationNameToolStripTextBox.Text);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(TextBox1.Text))
            {
                this.公交站点TableAdapter.Fill(this.stationDataSet.公交站点);
            } 
            else
            {
                this.公交站点TableAdapter.FillByStationName(this.stationDataSet.公交站点, "%" + TextBox1.Text + "%");
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
            }
        }

        private void 删除站点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView1.EndEdit();
            IFeature pCurFeature;
            bool bCheck = false;
            for (int i = DataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                if (DataGridView1.Rows[i].Cells[0].Value != null && (bool)DataGridView1.Rows[i].Cells[0].Value == true)
                {
                    pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusStation, "OBJECTID", DataGridView1.Rows[i].Cells[1].Value.ToString());
                    pCurFeature.Delete();
                    DataGridView1.Rows.RemoveAt(DataGridView1.Rows[i].Index);
                    bCheck = true;
                }
            }
            if (!bCheck & m_pCurFeature != null)
            {
                m_pCurFeature.Delete();
                DataGridView1.Rows.RemoveAt(m_nCurRowIndex);
            }
            EngineFuntions.PartialRefresh(EngineFuntions.m_Layer_BusStation);
        }

        private void 编辑属性ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_featureCollection.Clear();
            DataGridView1.EndEdit();
            IFeature pCurFeature;
            bool bCheck = false;
            foreach (DataGridViewRow eRow in DataGridView1.Rows)
            {
                if (eRow.Cells[0].Value != null && (bool)eRow.Cells[0].Value == true)
                {
                    pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusStation, "OBJECTID", eRow.Cells[1].Value.ToString());
                    m_featureCollection.Add(pCurFeature);
                    bCheck = true;
                }
            }
            frmStationAllInfo frmPopup = new frmStationAllInfo();
            if (!bCheck)
            {
                if (m_pCurFeature != null)
                {
                    m_featureCollection.Add(m_pCurFeature);
                    frmPopup.m_featureCollection = m_featureCollection;
                    frmPopup.Show();
                }
            }
            else
            {
                frmPopup.m_featureCollection = m_featureCollection;
                frmPopup.Show();
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

        private void frmStationPane_Load(object sender, EventArgs e)
        {
           this.公交站点TableAdapter.Fill(this.stationDataSet.公交站点);
        }

        private void DataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            m_pCurFeature = null;
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.RowIndex <= DataGridView1.Rows.Count)
            {
                m_nCurRowIndex = e.RowIndex;
                DataGridView1.Rows[m_nCurRowIndex].Selected = true;
                ContextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                m_pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusStation , "OBJECTID", DataGridView1.Rows[m_nCurRowIndex].Cells[1].Value.ToString());
            }
        }

        public void RefreshGrid()
        {
            this.公交站点TableAdapter.Fill(this.stationDataSet.公交站点);
        }

        private void fillByStationNameToolStripButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.公交站点TableAdapter.FillByStationName(this.stationDataSet.公交站点, stationNameToolStripTextBox.Text);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}
