using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Businfo.Globe;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using System.Data.OleDb;

namespace Businfo
{
    public partial class frmRoadPane : UserControl
    {
        public int m_nCurRowIndex;
        public IFeature m_pCurFeature;
        public List<IFeature> m_featureCollection = new List<IFeature>();  //得到所有选中的feature
        public frmRoadPane()
        {
            InitializeComponent();
        }

        private void fillByRoadNameToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.公交站线TableAdapter.FillByRoadName(this.roadDataSet.公交站线, "%" + roadNameToolStripTextBox.Text + "%");
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
                this.公交站线TableAdapter.Fill(this.roadDataSet.公交站线);
            }
            else
            {
                this.公交站线TableAdapter.FillByRoadName(this.roadDataSet.公交站线, "%" + TextBox1.Text + "%");
            }
        }

        private void frmRoadPane_Load(object sender, EventArgs e)
        {
            this.公交站线TableAdapter.Fill(this.roadDataSet.公交站线);
        }

        private void 定位到ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_pCurFeature != null)
            {
                IEnvelope pEnvelope;
                pEnvelope = m_pCurFeature.Extent;
                pEnvelope.Expand(2, 2, true);
                EngineFuntions.m_AxMapControl.ActiveView.Extent = pEnvelope;
                EngineFuntions.m_AxMapControl.ActiveView.ScreenDisplay.Invalidate(null, true, (short)esriScreenCache.esriAllScreenCaches);
            }
        }

        private void 删除线路ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView1.EndEdit();
            IFeature pCurFeature; 
            bool bCheck = false;
             for (int i = DataGridView1.Rows.Count - 1; i >= 0 ; i--)
             {
                 if (DataGridView1.Rows[i].Cells[0].Value != null && (bool)DataGridView1.Rows[i].Cells[0].Value == true)
         	    {
                    pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusRoad, "OBJECTID", DataGridView1.Rows[i].Cells[1].Value.ToString());
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
          EngineFuntions.PartialRefresh(EngineFuntions.m_Layer_BusRoad);
        }

        private void 属性编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_featureCollection.Clear();
            DataGridView1.EndEdit();
            IFeature pCurFeature; 
            bool bCheck = false;
            foreach (DataGridViewRow eRow in DataGridView1.Rows)
            {
                if (eRow.Cells[0].Value != null && (bool)eRow.Cells[0].Value == true)
                {
                    pCurFeature = EngineFuntions.GetOneSeartchFeature(EngineFuntions.m_Layer_BusRoad, "OBJECTID = " + eRow.Cells[1].Value.ToString()); //EngineEditOperations.GetFeatureByFieldAndValue(EngineEditOperations.m_Layer_BusRoad, "OBJECTID", eRow.Cells[1].Value.ToString());
                    m_featureCollection.Add(pCurFeature);
                    bCheck = true;
                }
            }
            frmRoadAllInfo frmPopup = new frmRoadAllInfo();
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
            }
            
        }

        private void 关联站点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_pCurFeature != null)
            {
                EngineFuntions.m_AxMapControl.ActiveView.GraphicsContainer.DeleteAllElements();
                object Missing = Type.Missing;
                IConstructCurve mycurve = new PolylineClass();
                mycurve.ConstructOffset((IPolycurve)m_pCurFeature.Shape, 35, ref Missing, ref Missing);
                IPolygon pPolygon;
                EngineFuntions.m_Layer_BusStation.Selectable = true;
                pPolygon = (IPolygon)EngineFuntions.ClickSel((IGeometry)mycurve , false, false, 35);
                EngineFuntions.AddPolygonElement(pPolygon);
                if (EngineFuntions.GetSeledFeatures(EngineFuntions.m_Layer_BusStation,ref m_featureCollection))
                {
                    frmRoadAndStation frmPopup = new frmRoadAndStation();
                    frmPopup.m_featureCollection = m_featureCollection;
                    frmPopup.m_pCurFeature = m_pCurFeature;
                    frmPopup.ShowDialog();
                }
            }
        }

        public void RefreshSelectGrid()
        {
            String strInPara = "";
            if (m_featureCollection.Count > 0)
            {
                foreach (IFeature pFeature in m_featureCollection)
                {
                    strInPara = String.Format("{0}{1},", strInPara, pFeature.get_Value(pFeature.Fields.FindField("OBJECTID")).ToString());
                }
                this.公交站线TableAdapter.FillByINOBJECTID(this.roadDataSet.公交站线, strInPara);
            }
        }

        public void RefreshGrid()
        {
            this.公交站线TableAdapter.Fill(this.roadDataSet.公交站线);
        }

        private void DataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            m_pCurFeature = null;
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.RowIndex <= DataGridView1.Rows.Count)
            {
                m_nCurRowIndex = e.RowIndex;
                DataGridView1.Rows[m_nCurRowIndex].Selected = true;
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                m_pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusRoad, "OBJECTID", DataGridView1.Rows[m_nCurRowIndex].Cells[1].Value.ToString());
            }
        }

        private void 显示站点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_pCurFeature != null)
            {
                String sConn ;
                OleDbConnection mycon;
                sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + Application.StartupPath + "\\data\\公交.mdb";
                mycon = new OleDbConnection(sConn);
                mycon.Open();
                try
                {
                    //sConn = String.Format("select a.* from 公交站点 a inner join RoadAndStation b on (a.OBJECTID = b.StationID and b.RoadID = {0})", m_pCurFeature.Value(m_pCurFeature.Fields.FindField("OBJECTID")))
                    sConn = String.Format("select StationID,StationOrder from  RoadAndStation where RoadID = {0} Order by StationOrder",m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID")));
                    OleDbDataAdapter da =  new OleDbDataAdapter(sConn, mycon);
                    DataSet ds =  new DataSet();
                    int nQueryCount = da.Fill(ds);
                    string strInPara = "";
                    if (nQueryCount > 0)
                    {
                        List<String> nList = new List<string>();
                        frmStationAllInfo frmPopup = new frmStationAllInfo();
                        foreach (DataRow eRow in ds.Tables[0].Rows)
                        {
                            strInPara = String.Format("{0}{1},", strInPara, eRow[0]);
                            nList.Add(eRow[0].ToString());             
                        }
                        frmPopup.Show();
                        frmPopup.RefreshStationGrid(strInPara); //填充完以后Gridview的值才是要排序的
                        frmPopup.SetStationOrderCell(nList); //填写排序值，使用的站点编号字段
                        frmPopup.SetSortColumn(2); //按站点编号列排序
                    }
                    mycon.Close();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("生成关联表出错\n" + ex.ToString() , "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
