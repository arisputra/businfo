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
using Microsoft.Office.Interop.Excel;
using Winapp = System.Windows.Forms.Application;
using Excelapp = Microsoft.Office.Interop.Excel.Application;
using ExcelPoint = Microsoft.Office.Interop.Excel.IPoint;

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
            checkBox1.Checked = false;
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
                    if (MessageBox.Show(string.Format("确认删除线路：{0}!", DataGridView1.Rows[i].Cells[4].Value.ToString()), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        String sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + Winapp.StartupPath + "\\data\\公交.mdb";
                        OleDbConnection mycon = new OleDbConnection(sConn);
                        mycon.Open();
                        OleDbDataAdapter da = ForBusInfo.CreateCustomerAdapter(mycon, "",
                           "", String.Format("delete from  RoadAndStation where RoadID = {0}", DataGridView1.Rows[i].Cells[1].Value.ToString()));
                        da.DeleteCommand.ExecuteNonQuery();//删除关联站点
                        pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusRoad, "OBJECTID", DataGridView1.Rows[i].Cells[1].Value.ToString());
                        pCurFeature.Delete();//删除线路对象
                        ForBusInfo.Add_Log(ForBusInfo.Login_name, "删除线路", DataGridView1.Rows[i].Cells[4].Value.ToString(), "");
                        DataGridView1.Rows.RemoveAt(DataGridView1.Rows[i].Index);//删除表格中的显示
                    }
                    bCheck = true;
         	    }
             }
          if (!bCheck & m_pCurFeature != null)
          {
              if (MessageBox.Show(string.Format("确认删除线路：{0}!", DataGridView1.Rows[m_nCurRowIndex].Cells[4].Value.ToString()), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
              {
                  String sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + Winapp.StartupPath + "\\data\\公交.mdb";
                  OleDbConnection mycon = new OleDbConnection(sConn);
                  mycon.Open();
                  OleDbDataAdapter da = ForBusInfo.CreateCustomerAdapter(mycon, "",
                     "", String.Format("delete from  RoadAndStation where RoadID = {0}", DataGridView1.Rows[m_nCurRowIndex].Cells[1].Value.ToString()));
                  da.DeleteCommand.ExecuteNonQuery();
                  m_pCurFeature.Delete();
                  ForBusInfo.Add_Log(ForBusInfo.Login_name, "删除线路", DataGridView1.Rows[m_nCurRowIndex].Cells[4].Value.ToString(), "");
                  DataGridView1.Rows.RemoveAt(m_nCurRowIndex);
              }
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
                    frmPopup.ShowDialog();
                }
            }
            else
            {
                frmPopup.m_featureCollection = m_featureCollection;
                frmPopup.ShowDialog();
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
                    frmPopup.Show();
                    //frmPopup.ShowDialog();
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
                String sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + Winapp.StartupPath + "\\data\\公交.mdb";
                OleDbConnection mycon = new OleDbConnection(sConn);
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

        private void 制作单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_featureCollection.Clear();
            DataGridView1.EndEdit();
            List<IFeature> pCurFeatureList;
            bool bCheck = false;
            foreach (DataGridViewRow eRow in DataGridView1.Rows)//判断是否打钩进行多选择
            {
                if (eRow.Cells[0].Value != null && (bool)eRow.Cells[0].Value == true)
                {
                    pCurFeatureList = EngineFuntions.GetSeartchFeatures(EngineFuntions.m_Layer_BusRoad, string.Format("RoadName = '{0}'", eRow.Cells[4].Value));
                    int i = 0,j = 0;
                    Excelapp app = new Excelapp();
                    if (app == null)
                    {
                        MessageBox.Show("创建Excel服务失败!\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    app.Visible = true;

                    Workbooks workbooks = app.Workbooks;
                    _Workbook workbook = workbooks.Open(Winapp.StartupPath + "\\data\\制作单.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    Sheets sheets = workbook.Worksheets;
                    _Worksheet worksheet = (_Worksheet)sheets.get_Item(1);
                    Range range1 = worksheet.get_Range("A1", "G3");
                    if (range1 == null)
                    {
                        MessageBox.Show("标题设置失败!\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    } 
                    range1.Value2 = string.Format("制作单{0}路{1}", pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("RoadName")), pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("Company")));
                    range1 = worksheet.get_Range("A4", "A5");
                    range1.Value2 = pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("RoadType"));
                    range1 = worksheet.get_Range("E4", "E4");
                    range1.Value2 = "首站开班：" + pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("FirstStartTime"));
                    range1 = worksheet.get_Range("G4", "G4");
                    range1.Value2 = "首站收班：" + pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("FirstCloseTime"));
                    range1 = worksheet.get_Range("E5", "E5");
                    range1.Value2 = "末站开班：" + pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("EndStartTime"));
                    range1 = worksheet.get_Range("G5", "G5");
                    range1.Value2 = "末站收班：" + pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("EndCloseTim"));
                    //开始遍历站点,去行、回行站点分别读取列表，去行、回行可能不一样。
                    String sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + Winapp.StartupPath + "\\data\\公交.mdb";
                    OleDbConnection mycon = new OleDbConnection(sConn);
                    mycon.Open();
                    try
                    {
                        foreach (IFeature pCurFeature in pCurFeatureList)
                        {
                            if (pCurFeature.get_Value(pCurFeature.Fields.FindField("RoadTravel")).ToString() == "去行")
                            {
                                sConn = String.Format("select a.* from 公交站点 a inner join RoadAndStation b on (a.OBJECTID = b.StationID and b.RoadID = {0}) Order by b.StationOrder", pCurFeature.get_Value(pCurFeature.Fields.FindField("OBJECTID")));
                                OleDbDataAdapter da = new OleDbDataAdapter(sConn, mycon);
                                DataSet ds = new DataSet();
                                int nQueryCount = da.Fill(ds);
                                if (nQueryCount > 0)
                                {
                                    i = 0;
                                    range1 = worksheet.get_Range("D4", "D4");//设置首站站名
                                    range1.Value2 = ds.Tables[0].Rows[0][3];
                                    foreach (DataRow eTableRow in ds.Tables[0].Rows)
                                    {
                                        range1 = worksheet.get_Range(string.Format("B{0}", 8 + (2 * i)), string.Format("B{0}", 8 + (2 * i)));
                                        range1.Value2 = eTableRow[3];
                                        range1 = worksheet.get_Range(string.Format("B{0}", 9 + (2 * i)), string.Format("B{0}", 9 + (2 * i)));
                                        range1.Value2 = eTableRow[4];
                                        range1 = worksheet.get_Range(string.Format("C{0}", 8 + (2 * i)), string.Format("C{0}", 9 + (2 * i)));
                                        range1.Value2 = eTableRow[14];
                                        range1 = worksheet.get_Range(string.Format("D{0}", 8 + (2 * i)), string.Format("D{0}", 9 + (2 * i++)));
                                        range1.Value2 = eTableRow[7];
                                    }
                                    
                                }
                            }
                            if (pCurFeature.get_Value(pCurFeature.Fields.FindField("RoadTravel")).ToString() == "回行")
                            {
                                sConn = String.Format("select a.* from 公交站点 a inner join RoadAndStation b on (a.OBJECTID = b.StationID and b.RoadID = {0}) Order by b.StationOrder", pCurFeature.get_Value(pCurFeature.Fields.FindField("OBJECTID")));
                                OleDbDataAdapter da = new OleDbDataAdapter(sConn, mycon);
                                DataSet ds = new DataSet();
                                int nQueryCount = da.Fill(ds);
                                if (nQueryCount > 0)
                                {
                                    j = 0;
                                    range1 = worksheet.get_Range("D5", "D5");//设置末站站名
                                    range1.Value2 = ds.Tables[0].Rows[0][3];
                                    foreach (DataRow eTableRow in ds.Tables[0].Rows)
                                    {
                                        range1 = worksheet.get_Range(string.Format("E{0}", 8 + (2 * j)), string.Format("E{0}", 8 + (2 * j)));
                                        range1.Value2 = eTableRow[3];
                                        range1 = worksheet.get_Range(string.Format("E{0}", 9 + (2 * j)), string.Format("E{0}", 9 + (2 * j)));
                                        range1.Value2 = eTableRow[4];
                                        range1 = worksheet.get_Range(string.Format("F{0}", 8 + (2 * j)), string.Format("F{0}", 9 + (2 * j)));
                                        range1.Value2 = eTableRow[14];
                                        range1 = worksheet.get_Range(string.Format("G{0}", 8 + (2 * j)), string.Format("G{0}", 9 + (2 * j++)));
                                        range1.Value2 = eTableRow[7];
                                    }
                                    
                                }
                            }
                            if (i>j)
                            {
                                worksheet.PageSetup.PrintArea = string.Format("$A$1:$G${0}", i * 2 + 7);
                            } 
                            else
                            {
                                worksheet.PageSetup.PrintArea = string.Format("$A$1:$G${0}", j * 2 + 7);
                            }
                        }
                        mycon.Close();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("生成关联表出错\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    bCheck = true;
                }
            }
 
            if (!bCheck)//右键直接选择出表，单一的一个。
            {
                if (m_pCurFeature != null)
                {
                    pCurFeatureList = EngineFuntions.GetSeartchFeatures(EngineFuntions.m_Layer_BusRoad, string.Format("RoadName = '{0}'", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadName"))));
                    int i = 0, j = 0;
                    Excelapp app = new Excelapp();
                    if (app == null)
                    {
                        MessageBox.Show("创建Excel服务失败!\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    app.Visible = true;

                    Workbooks workbooks = app.Workbooks;
                    _Workbook workbook = workbooks.Open(Winapp.StartupPath + "\\data\\制作单.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    Sheets sheets = workbook.Worksheets;
                    _Worksheet worksheet = (_Worksheet)sheets.get_Item(1);
                    Range range1 = worksheet.get_Range("A1", "G3");
                    if (range1 == null)
                    {
                        MessageBox.Show("标题设置失败!\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    range1.Value2 = string.Format("制作单{0}路{1}", pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("RoadName")), pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("Company")));
                    range1 = worksheet.get_Range("A4", "A5");
                    range1.Value2 = pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("RoadType"));
                    range1 = worksheet.get_Range("E4", "E4");
                    range1.Value2 = "首站开班：" + pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("FirstStartTime"));
                    range1 = worksheet.get_Range("G4", "G4");
                    range1.Value2 = "首站收班：" + pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("FirstCloseTime"));
                    range1 = worksheet.get_Range("E5", "E5");
                    range1.Value2 = "末站开班：" + pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("EndStartTime"));
                    range1 = worksheet.get_Range("G5", "G5");
                    range1.Value2 = "末站收班：" + pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("EndCloseTim"));
                    //开始遍历站点,去行、回行站点分别读取列表，去行、回行可能不一样。
                    String sConn;
                    OleDbConnection mycon;
                    sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + Winapp.StartupPath + "\\data\\公交.mdb";
                    mycon = new OleDbConnection(sConn);
                    mycon.Open();
                    try
                    {
                        foreach (IFeature pCurFeature in pCurFeatureList)
                        {
                            if (pCurFeature.get_Value(pCurFeature.Fields.FindField("RoadTravel")).ToString() == "去行")
                            {
                                sConn = String.Format("select a.* from 公交站点 a inner join RoadAndStation b on (a.OBJECTID = b.StationID and b.RoadID = {0}) Order by b.StationOrder", pCurFeature.get_Value(pCurFeature.Fields.FindField("OBJECTID")));
                                OleDbDataAdapter da = new OleDbDataAdapter(sConn, mycon);
                                DataSet ds = new DataSet();
                                int nQueryCount = da.Fill(ds);
                                if (nQueryCount > 0)
                                {
                                    i = 0;
                                    range1 = worksheet.get_Range("D4", "D4");
                                    range1.Value2 = ds.Tables[0].Rows[0][3];
                                    range1 = worksheet.get_Range("D5", "D5");
                                    range1.Value2 = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][3];
                                    foreach (DataRow eTableRow in ds.Tables[0].Rows)
                                    {
                                        range1 = worksheet.get_Range(string.Format("B{0}", 8 + (2 * i)), string.Format("B{0}", 8 + (2 * i)));
                                        range1.Value2 = eTableRow[3];
                                        range1 = worksheet.get_Range(string.Format("B{0}", 9 + (2 * i)), string.Format("B{0}", 9 + (2 * i)));
                                        range1.Value2 = eTableRow[4];
                                        range1 = worksheet.get_Range(string.Format("C{0}", 8 + (2 * i)), string.Format("C{0}", 9 + (2 * i)));
                                        range1.Value2 = eTableRow[14];
                                        range1 = worksheet.get_Range(string.Format("D{0}", 8 + (2 * i)), string.Format("D{0}", 9 + (2 * i++)));
                                        range1.Value2 = eTableRow[7];
                                    }

                                }
                            }
                            if (pCurFeature.get_Value(pCurFeature.Fields.FindField("RoadTravel")).ToString() == "回行")
                            {
                                sConn = String.Format("select a.* from 公交站点 a inner join RoadAndStation b on (a.OBJECTID = b.StationID and b.RoadID = {0}) Order by b.StationOrder", pCurFeature.get_Value(pCurFeature.Fields.FindField("OBJECTID")));
                                OleDbDataAdapter da = new OleDbDataAdapter(sConn, mycon);
                                DataSet ds = new DataSet();
                                int nQueryCount = da.Fill(ds);
                                if (nQueryCount > 0)
                                {
                                    j = 0;
                                    range1 = worksheet.get_Range("D4", "D4");
                                    range1.Value2 = ds.Tables[0].Rows[0][3];
                                    range1 = worksheet.get_Range("D5", "D5");
                                    range1.Value2 = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][3];
                                    foreach (DataRow eTableRow in ds.Tables[0].Rows)
                                    {
                                        range1 = worksheet.get_Range(string.Format("E{0}", 8 + (2 * j)), string.Format("E{0}", 8 + (2 * j)));
                                        range1.Value2 = eTableRow[3];
                                        range1 = worksheet.get_Range(string.Format("E{0}", 9 + (2 * j)), string.Format("E{0}", 9 + (2 * j)));
                                        range1.Value2 = eTableRow[4];
                                        range1 = worksheet.get_Range(string.Format("F{0}", 8 + (2 * j)), string.Format("F{0}", 9 + (2 * j)));
                                        range1.Value2 = eTableRow[14];
                                        range1 = worksheet.get_Range(string.Format("G{0}", 8 + (2 * j)), string.Format("G{0}", 9 + (2 * j++)));
                                        range1.Value2 = eTableRow[7];
                                    }

                                }
                            }
                            if (i > j)
                            {
                                worksheet.PageSetup.PrintArea = string.Format("$A$1:$G${0}", i * 2 + 7);
                            }
                            else
                            {
                                worksheet.PageSetup.PrintArea = string.Format("$A$1:$G${0}", j * 2 + 7);
                            }
                        }

                        mycon.Close();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("生成关联表出错\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void 备份站点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool bCheck = false;
            foreach (DataGridViewRow eRow in DataGridView1.Rows)
            {
                if (eRow.Cells[0].Value != null && (bool)eRow.Cells[0].Value == true)
                {
                    bCheck = true;
                    m_pCurFeature = EngineFuntions.GetOneSeartchFeature(EngineFuntions.m_Layer_BackRoad, string.Format("Roadname = '{0}' AND RoadTravel = '{1}'", eRow.Cells[4].Value.ToString(), eRow.Cells[6].Value.ToString()));
                    if (m_pCurFeature != null)
                    {
                        MessageBox.Show("临时图层已经存在该线路\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                    m_pCurFeature = EngineFuntions.GetOneSeartchFeature(EngineFuntions.m_Layer_BusRoad, "OBJECTID = " + eRow.Cells[1].Value.ToString());
                    IFeature pFeature = EngineFuntions.CopyFeature(EngineFuntions.m_Layer_BackRoad, m_pCurFeature);

                    String sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + Winapp.StartupPath + "\\data\\公交.mdb";
                    OleDbConnection mycon = new OleDbConnection(sConn);
                    mycon.Open();
                    try
                    {
                        OleDbDataAdapter da = ForBusInfo.CreateCustomerAdapter(mycon, String.Format("select * from  RoadAndStation where RoadID = {0}", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"))),
                           "", String.Format("delete from  RoadAndStation where RoadID = {0}", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"))));
                        da.SelectCommand.ExecuteNonQuery();
                        DataSet ds = new DataSet();
                        int nQueryCount = da.Fill(ds);
                        foreach (DataRow eDataRow in ds.Tables[0].Rows)
                        {
                            da.InsertCommand.CommandText = String.Format("insert into BackRAndS(RoadID,StationID,StationOrder) values({0},{1},{2})"
                           , pFeature.get_Value(pFeature.Fields.FindField("OBJECTID")), eDataRow[2], eDataRow[3]);
                            da.InsertCommand.ExecuteNonQuery();
                        }
                        da.DeleteCommand.ExecuteNonQuery();//删除原始站线关联站点数据
                        ForBusInfo.Add_Log(ForBusInfo.Login_name, "备份线路", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadName")).ToString(), "");
                        mycon.Close();
                        //m_pCurFeature.Delete();//删除原始站线
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("生成关联表出错\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    EngineFuntions.PartialRefresh(EngineFuntions.m_Layer_BackRoad);
                }
            }
            for (int i = DataGridView1.Rows.Count - 1; i >= 0; i--)//列表中删除已经备份的站线的显示行
            {
                if (DataGridView1.Rows[i].Cells[0].Value != null && (bool)DataGridView1.Rows[i].Cells[0].Value == true)
                {
                   DataGridView1.Rows.RemoveAt(DataGridView1.Rows[i].Index);
                }
            }
            if (!bCheck)//没有check状态
            {
                if (m_pCurFeature != null)
                {
                    IFeature pFeature = EngineFuntions.GetOneSeartchFeature(EngineFuntions.m_Layer_BackRoad, string.Format("Roadname = '{0}' AND RoadTravel = '{1}'", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("Roadname")), m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadTravel"))));
                    if (pFeature != null)
                    {
                        MessageBox.Show("临时图层已经存在该线路\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    pFeature = EngineFuntions.CopyFeature(EngineFuntions.m_Layer_BackRoad, m_pCurFeature);//拷贝图形和属性到备份图层
                    //开始移动线路对应的站点数据
                    String sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + Winapp.StartupPath + "\\data\\公交.mdb";
                    OleDbConnection mycon = new OleDbConnection(sConn);
                    mycon.Open();
                    try
                    {
                        OleDbDataAdapter da = ForBusInfo.CreateCustomerAdapter(mycon, String.Format("select * from  RoadAndStation where RoadID = {0}", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"))),
                            "", String.Format("delete from  RoadAndStation where RoadID = {0}", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"))));
                        da.SelectCommand.ExecuteNonQuery();
                        DataSet ds = new DataSet();
                        int nQueryCount = da.Fill(ds);
                        foreach (DataRow eRow in ds.Tables[0].Rows)
                        {
                            da.InsertCommand.CommandText = String.Format("insert into BackRAndS(RoadID,StationID,StationOrder) values({0},{1},{2})"
                                , pFeature.get_Value(pFeature.Fields.FindField("OBJECTID")), eRow[2], eRow[3]);
                            da.InsertCommand.ExecuteNonQuery();
                        }
                        da.DeleteCommand.ExecuteNonQuery();//删除原始站线关联站点数据
                        ForBusInfo.Add_Log(ForBusInfo.Login_name, "备份站线", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadName")).ToString(), "");
                        mycon.Close();
                        //m_pCurFeature.Delete();//删除原始站线
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("生成关联表出错\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    EngineFuntions.PartialRefresh(EngineFuntions.m_Layer_BackRoad);
                    DataGridView1.Rows.RemoveAt(m_nCurRowIndex);
                }
            }
        }

        private void DataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView1.EndEdit();
            }
        }
    }
}
