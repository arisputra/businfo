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
using Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.IO;


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
                //this.公交站点TableAdapter.FillByStationName(this.stationDataSet.公交站点, "%" + TextBox1.Text + "%");
            }
            int nNum = 1;
            foreach (DataGridViewRow eRow in DataGridView1.Rows)
            {
                eRow.HeaderCell.Value = nNum++.ToString();
            }
        }

        private void 定位到ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_pCurFeature != null)
            {
                ESRI.ArcGIS.Geometry.IPoint pPoint;
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
                System.Windows.Forms.Application.DoEvents();
                EngineFuntions.FlashShape(m_pCurFeature.ShapeCopy);
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
                    if (MessageBox.Show(string.Format("确认删除站点：{0}!", DataGridView1.Rows[i].Cells[3].Value.ToString()), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusStation, "OBJECTID", DataGridView1.Rows[i].Cells[1].Value.ToString());
                        pCurFeature.Delete();
                        ForBusInfo.Add_Log(ForBusInfo.Login_name, "删除站点", DataGridView1.Rows[i].Cells[3].Value.ToString(), "");
                        DataGridView1.Rows.RemoveAt(DataGridView1.Rows[i].Index);
                    }
                    bCheck = true;
                }
            }
            if (!bCheck & m_pCurFeature != null)
            {
                if (MessageBox.Show(string.Format("确认删除站点：{0}!", DataGridView1.Rows[m_nCurRowIndex].Cells[3].Value.ToString()), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    m_pCurFeature.Delete();
                    ForBusInfo.Add_Log(ForBusInfo.Login_name, "删除站点", DataGridView1.Rows[m_nCurRowIndex].Cells[3].Value.ToString(), "");
                    DataGridView1.Rows.RemoveAt(m_nCurRowIndex);
                }
            }
            EngineFuntions.PartialRefresh(EngineFuntions.m_Layer_BusStation);
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
                        pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusStation, "OBJECTID", eRow.Cells["OBJECTID"].Value.ToString());
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
                    frmPopup.ShowDialog();
                }
            }
            else
            {
                frmPopup.m_featureCollection = m_featureCollection;
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

        private void frmStationPane_Load(object sender, EventArgs e)
        {
            RefreshGrid();
            int nNum = 1;
            foreach (DataGridViewRow eRow in DataGridView1.Rows)
            {
                eRow.HeaderCell.Value = nNum++.ToString();
            }
           //this.公交站点TableAdapter.Fill(this.stationDataSet.公交站点);
        }

        private void DataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            m_pCurFeature = null;
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.RowIndex <= DataGridView1.Rows.Count)
            {
                m_nCurRowIndex = e.RowIndex;
                DataGridView1.Rows[m_nCurRowIndex].Selected = true;
                ContextMenuStrip1.Show(MousePosition.X,MousePosition.Y);
                m_pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusStation , "OBJECTID", DataGridView1.Rows[m_nCurRowIndex].Cells[1].Value.ToString());
            }
        }

        public void RefreshGrid()
        {
            ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Station_FillPan, "", new string[] { "" });
            //this.公交站点TableAdapter.Fill(this.stationDataSet.公交站点);
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

        //鼠标右键就结束DataGridView1编辑，不然DataGridView1最后编辑状态没有结束，值不更新。
        private void DataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView1.EndEdit();
            }
        }

        private void 站点表单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_featureCollection.Clear();
            DataGridView1.EndEdit();
            List<IFeature> pCurFeatureList;
            bool bCheck = false;
            foreach (DataGridViewRow eRow in DataGridView1.Rows)//判断是否打钩进行多选择
            {
                if (eRow.Cells[0].Value != null && (bool)eRow.Cells[0].Value == true)
                {
                    if (ForBusInfo.Excel_app == null)
                    {
                        MessageBox.Show("创建Excel服务失败!\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    ForBusInfo.Excel_app.Visible = true;
                    //ForBusInfo.Excel_app.DisplayAlerts = false;
                    //Workbooks workbooks = ForBusInfo.Excel_app.Workbooks;
                    _Workbook workbook = ForBusInfo.Excel_app.Workbooks.Add(System.Reflection.Missing.Value);
                    _Worksheet worksheet = null;
                    if (workbook.Worksheets.Count > 0)
                    {
                        worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1);
                    }
                    else
                    {
                        workbook.Worksheets.Add(System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                    System.Reflection.Missing.Value);
                        worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1);
                    }
                    Range range1 = worksheet.get_Range("A1", "A1");
                    range1.Value2 = string.Format("{0}({1})({2}:{3})", eRow.Cells["StationName"].Value, eRow.Cells["StationAlias"].Value, eRow.Cells["MainSymbol"].Value, eRow.Cells["Direct"].Value);
                    range1 = worksheet.get_Range("A2", "A2");
                    range1.Value2 = string.Format("{0}:{1}", eRow.Cells["StationStyle"].Value, eRow.Cells["RodStyleFirst"].Value);
                    range1 = worksheet.get_Range("A3", "A3");
                    range1.Value2 = eRow.Cells["BusShelter"].Value;


                    OleDbConnection mycon = new OleDbConnection(ForBusInfo.Connect_Sql);
                    mycon.Open();
                    OleDbDataAdapter da;
                    if (ForBusInfo.Connect_Type == 1)
                        da = new OleDbDataAdapter(String.Format("select a.RoadName from sde.公交站线 a inner join sde.RoadAndStation b on (a.OBJECTID = b.RoadID and b.StationID = {0})", eRow.Cells["OBJECTID"].Value), mycon);
                    else
                        da = new OleDbDataAdapter(String.Format("select a.RoadName from 公交站线 a inner join RoadAndStation b on (a.OBJECTID = b.RoadID and b.StationID = {0})", eRow.Cells["OBJECTID"].Value), mycon);
                    DataSet ds = new DataSet();
                    int nQueryCount = da.Fill(ds,"Road");
                    if (nQueryCount > 0)
                    {
                        string strRoad = "";
                        foreach (DataRow eDataRow in ds.Tables["Road"].Rows)
                        {
                            strRoad = strRoad + eDataRow["RoadName"].ToString() + "、";
                        }
                        range1 = worksheet.get_Range("A4", "A4");
                        range1.Value2 = strRoad;
                    }
                    range1 = worksheet.get_Range("A5", "A5");
                    range1.Value2 = string.Format("总经过线路：{0}",nQueryCount);

                    mycon.Close();
                    bCheck = true;
                }
            }

            if (!bCheck)//右键直接选择出表，单一的一个。
            {
                if (m_pCurFeature != null)
                {
                    if (ForBusInfo.Excel_app == null)
                    {
                        MessageBox.Show("创建Excel服务失败!\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    ForBusInfo.Excel_app.Visible = true;
                    //ForBusInfo.Excel_app.DisplayAlerts = false;
                    //Workbooks workbooks = ForBusInfo.Excel_app.Workbooks;
                    _Workbook workbook = ForBusInfo.Excel_app.Workbooks.Add(System.Reflection.Missing.Value);
                    _Worksheet worksheet = null;
                    if (workbook.Worksheets.Count > 0)
                    {
                        worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1);
                    }
                    else
                    {
                        workbook.Worksheets.Add(System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                    System.Reflection.Missing.Value);
                        worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(1);
                    }
                    Range range1 = worksheet.get_Range("A1", "A1");
                    range1.Value2 = string.Format("{0}({1})({2}:{3})", DataGridView1.Rows[m_nCurRowIndex].Cells["StationName"].Value, DataGridView1.Rows[m_nCurRowIndex].Cells["StationAlias"].Value, DataGridView1.Rows[m_nCurRowIndex].Cells["MainSymbol"].Value, DataGridView1.Rows[m_nCurRowIndex].Cells["Direct"].Value);
                    range1 = worksheet.get_Range("A2", "A2");
                    range1.Value2 = string.Format("{0}:{1}", DataGridView1.Rows[m_nCurRowIndex].Cells["StationStyle"].Value, DataGridView1.Rows[m_nCurRowIndex].Cells["RodStyleFirst"].Value);
                    range1 = worksheet.get_Range("A3", "A3");
                    range1.Value2 = DataGridView1.Rows[m_nCurRowIndex].Cells["BusShelter"].Value;


                    OleDbConnection mycon = new OleDbConnection(ForBusInfo.Connect_Sql);
                    mycon.Open();
                    OleDbDataAdapter da;
                    if (ForBusInfo.Connect_Type == 1)
                        da = new OleDbDataAdapter(String.Format("select a.RoadName from sde.公交站线 a inner join sde.RoadAndStation b on (a.OBJECTID = b.RoadID and b.StationID = {0})", DataGridView1.Rows[m_nCurRowIndex].Cells["OBJECTID"].Value), mycon);
                    else
                        da = new OleDbDataAdapter(String.Format("select a.RoadName from 公交站线 a inner join RoadAndStation b on (a.OBJECTID = b.RoadID and b.StationID = {0})", DataGridView1.Rows[m_nCurRowIndex].Cells["OBJECTID"].Value), mycon);
                    DataSet ds = new DataSet();
                    int nQueryCount = da.Fill(ds, "Road");
                    if (nQueryCount > 0)
                    {
                        string strRoad = "";
                        foreach (DataRow eDataRow in ds.Tables["Road"].Rows)
                        {
                            strRoad = strRoad + eDataRow["RoadName"].ToString() + "、";
                        }
                        range1 = worksheet.get_Range("A4", "A4");
                        range1.Value2 = strRoad;
                    }
                    range1 = worksheet.get_Range("A5", "A5");
                    range1.Value2 = string.Format("总经过线路：{0}", nQueryCount);

                    mycon.Close();
                }
            }
        }

     }
}
