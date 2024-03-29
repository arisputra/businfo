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
using System.IO;

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

        private void Button1_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            if (String.IsNullOrEmpty(TextBox1.Text))
            {
                RefreshGrid();
                //this.公交站线TableAdapter.Fill(this.roadDataSet.公交站线);
            }
            else
            {
                ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Road_FillByStationName, string.Format(" WHERE (RoadName LIKE '%{0}%')", TextBox1.Text), new string[] { "" });
                //this.公交站线TableAdapter.FillByRoadName(this.roadDataSet.公交站线, "%" + TextBox1.Text + "%");
            }
            //int nNum = 1;
            //foreach (DataGridViewRow eRow in DataGridView1.Rows)
            //{
            //    eRow.HeaderCell.Value = nNum++.ToString();
            //}
        }

        private void frmRoadPane_Load(object sender, EventArgs e)
        {
            RefreshGrid();
            switch (ForBusInfo.Login_name)
            {
                case "站点管理":
                    contextMenuStrip1.Items.Find("删除线路ToolStripMenuItem", false)[0].Visible = false;
                    contextMenuStrip1.Items.Find("属性编辑ToolStripMenuItem", false)[0].Visible = false;
                    contextMenuStrip1.Items.Find("备份线路ToolStripMenuItem", false)[0].Visible = false;
                    contextMenuStrip1.Items.Find("生成反向线路ToolStripMenuItem", false)[0].Visible = false;
                    contextMenuStrip1.Items.Find("关联站点ToolStripMenuItem", false)[0].Visible = true;
                    contextMenuStrip1.Items.Find("显示站点ToolStripMenuItem", false)[0].Visible = true;
                    contextMenuStrip1.Items.Find("制作单ToolStripMenuItem", false)[0].Visible = true;
                    break;
                case "admin":
                    contextMenuStrip1.Items.Find("关联站点ToolStripMenuItem", false)[0].Visible = true;
                    contextMenuStrip1.Items.Find("显示站点ToolStripMenuItem", false)[0].Visible = true;
                    contextMenuStrip1.Items.Find("制作单ToolStripMenuItem", false)[0].Visible = true;
                    break;
                case "浏览":
                    contextMenuStrip1.Items.Find("定位到ToolStripMenuItem", false)[0].Visible = true;
                    contextMenuStrip1.Items.Find("删除线路ToolStripMenuItem", false)[0].Visible = false;
                    contextMenuStrip1.Items.Find("属性编辑ToolStripMenuItem", false)[0].Visible = false;
                    contextMenuStrip1.Items.Find("备份线路ToolStripMenuItem", false)[0].Visible = false;
                    contextMenuStrip1.Items.Find("生成反向线路ToolStripMenuItem", false)[0].Visible = false;
                    contextMenuStrip1.Items.Find("关联站点ToolStripMenuItem", false)[0].Visible = false;
                    contextMenuStrip1.Items.Find("显示站点ToolStripMenuItem", false)[0].Visible = true;
                    contextMenuStrip1.Items.Find("制作单ToolStripMenuItem", false)[0].Visible = true;
                    break;
            }
            //int nNum = 1;
            //foreach (DataGridViewRow eRow in DataGridView1.Rows)
            //{
            //    eRow.HeaderCell.Value = nNum++.ToString();
            //}
            //this.公交站线TableAdapter.Fill(this.roadDataSet.公交站线);
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
                ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Road_FillByOBJECTID, string.Format(" WHERE (OBJECTID IN ({0}))", strInPara.Substring(0, strInPara.Length - 1)), new string[] { "" });
                //this.公交站线TableAdapter.FillByINOBJECTID(this.roadDataSet.公交站线, strInPara);
            }
        }

        public void RefreshGrid()
        {
            ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Road_FillPan, "", new string[] { "" });
            //this.公交站线TableAdapter.Fill(this.roadDataSet.公交站线);
        }

        private void DataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView1.EndEdit();
            }
        }

        private void DataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            m_pCurFeature = null;
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.RowIndex <= DataGridView1.Rows.Count)
            {
                m_nCurRowIndex = e.RowIndex;
                DataGridView1.Rows[m_nCurRowIndex].Selected = true;
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                m_pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusRoad, "OBJECTID", DataGridView1.Rows[m_nCurRowIndex].Cells["OBJECTID"].Value.ToString());
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
                IEnvelope pEnvelope;
                pEnvelope = m_pCurFeature.Extent;
                pEnvelope.Expand(2, 2, true);
                EngineFuntions.m_AxMapControl.ActiveView.Extent = pEnvelope;
                EngineFuntions.m_AxMapControl.ActiveView.ScreenDisplay.Invalidate(null, true, (short)esriScreenCache.esriAllScreenCaches);
                System.Windows.Forms.Application.DoEvents();
                EngineFuntions.FlashShape(m_pCurFeature.ShapeCopy);
                EngineFuntions.MapRefresh();
                EngineFuntions.m_AxMapControl.Map.ClearSelection();
                EngineFuntions.m_AxMapControl.ActiveView.GraphicsContainer.DeleteAllElements();
                EngineFuntions.m_AxMapControl.Map.SelectFeature(EngineFuntions.m_Layer_BusRoad, m_pCurFeature);
                EngineFuntions.MapRefresh();

            }
        }

        private void 删除线路ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView1.EndEdit();
            IFeature pCurFeature;
            bool bCheck = false;
            for (int i = DataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                if (DataGridView1.Rows[i].Cells[0].Value != null && (bool)DataGridView1.Rows[i].Cells[0].Value == true)
                {
                    if (MessageBox.Show(string.Format("确认删除线路：{0}!", DataGridView1.Rows[i].Cells[3].Value.ToString()), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        OleDbConnection mycon = new OleDbConnection(ForBusInfo.Connect_Sql);
                        mycon.Open();
                        OleDbDataAdapter da;
                        if(ForBusInfo.Connect_Type == 1)
                            da = ForBusInfo.CreateCustomerAdapter(mycon, "",
                           "", String.Format("delete from  sde.RoadAndStation where RoadID = {0}", DataGridView1.Rows[i].Cells[1].Value.ToString()));
                        else
                            da = ForBusInfo.CreateCustomerAdapter(mycon, "",
                           "", String.Format("delete from  RoadAndStation where RoadID = {0}", DataGridView1.Rows[i].Cells[1].Value.ToString()));
                        da.DeleteCommand.ExecuteNonQuery();//删除关联站点
                        pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusRoad, "OBJECTID", DataGridView1.Rows[i].Cells[1].Value.ToString());
                        pCurFeature.Delete();//删除线路对象
                        ForBusInfo.Add_Log(ForBusInfo.Login_name, "删除线路", DataGridView1.Rows[i].Cells[3].Value.ToString(), "");
                        DataGridView1.Rows.RemoveAt(DataGridView1.Rows[i].Index);//删除表格中的显示
                        mycon.Close();
                    }
                    bCheck = true;
                }
            }
            if (!bCheck & m_pCurFeature != null)
            {
                if (MessageBox.Show(string.Format("确认删除线路：{0}!", DataGridView1.Rows[m_nCurRowIndex].Cells[3].Value.ToString()), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    OleDbConnection mycon = new OleDbConnection(ForBusInfo.Connect_Sql);
                    mycon.Open();
                    OleDbDataAdapter da;
                    if (ForBusInfo.Connect_Type == 1)
                        da = ForBusInfo.CreateCustomerAdapter(mycon, "",
                           "", String.Format("delete from  sde.RoadAndStation where RoadID = {0}", DataGridView1.Rows[m_nCurRowIndex].Cells[1].Value.ToString()));
                    else
                        da = ForBusInfo.CreateCustomerAdapter(mycon, "",
                       "", String.Format("delete from  RoadAndStation where RoadID = {0}", DataGridView1.Rows[m_nCurRowIndex].Cells[1].Value.ToString()));
                    da.DeleteCommand.ExecuteNonQuery();//删除关联站点
                    m_pCurFeature.Delete();//删除线路对象
                    ForBusInfo.Add_Log(ForBusInfo.Login_name, "删除线路", DataGridView1.Rows[m_nCurRowIndex].Cells[3].Value.ToString(), "");
                    DataGridView1.Rows.RemoveAt(m_nCurRowIndex);
                    mycon.Close();
                }
            }
            EngineFuntions.m_AxMapControl.ActiveView.Refresh();
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
                //IFeature FFFF = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusRoad, "OBJECTID","1809");
                //////////////////////////////////手动 改变path///////////////////////////////////////
                IPolyline pPolyl = null;
                IPolyline pPLine = m_pCurFeature.ShapeCopy as IPolyline;
                object Missing1 = Type.Missing;
                IGeometryCollection pGeometryCollection = (IGeometryCollection)pPLine;//自相交的合并会在自相交点变成一段段的path，不能用count判断了。
                if (pGeometryCollection.GeometryCount > 1)
                {
                    if (MessageBox.Show("不能连续执行的！要注意！\n", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                    {

                        //IPolyline pPLine1 = FFFF.ShapeCopy as IPolyline;
                        //IGeometryCollection pGeometryCollection1 = (IGeometryCollection)pPLine1;
                        IGeometryCollection pGeometryCol = new PolylineClass();//用点来生成polyline，这样就可以自相交。
                        IPointCollection pPtColl = new PolylineClass();
                        //pPath.FromPoint = pPtColl.get_Point(6);
                        //pPath.ToPoint = pPtColl.get_Point(6);

                        IPath pPath = pGeometryCollection.get_Geometry(2) as IPath;//这是得到polyline path的方法。
                        pPath.ReverseOrientation();
                        pGeometryCol.AddGeometry(pPath as IGeometry, ref Missing1, ref Missing1);

                        IPath pPath1 = pGeometryCollection.get_Geometry(0) as IPath;
                        //pPath1.ReverseOrientation();
                        pGeometryCol.AddGeometry(pPath1 as IGeometry, ref Missing1, ref Missing1);


                        IPath pPath2 = pGeometryCollection.get_Geometry(1) as IPath;
                        //pPath2.ReverseOrientation();
                        pGeometryCol.AddGeometry(pPath2 as IGeometry, ref Missing1, ref Missing1);


                        //pGeometryCol.AddGeometry(pGeometryCollection.get_Geometry(0), ref Missing1, ref Missing1);
                        //pGeometryCol.AddGeometry(pGeometryCollection.get_Geometry(1), ref Missing1, ref Missing1);
                        //ESRI.ArcGIS.Geometry.IPoint p = new PointClass();
                        //object aa = Type.Missing;
                        //p.PutCoords(523212.167, 390209.897);
                        //pPtColl.AddPoint(p, ref aa, ref aa);
                       
                        //pPtColl.RemovePoints(260, 1);
                        //ESRI.ArcGIS.Geometry.IPoint p1 = new PointClass();
                        //p1.PutCoords(523206.248, 390211.263);
                        //pPtColl.AddPoint(p1, ref aa, ref aa);

                        pPtColl.AddPointCollection(pGeometryCol.get_Geometry(0) as IPointCollection);
                        pPtColl.AddPointCollection(pGeometryCol.get_Geometry(1) as IPointCollection);
                        pPtColl.AddPointCollection(pGeometryCol.get_Geometry(2) as IPointCollection);
                        //pPolyl = pGeometryCol as IPolyline;
                        pPolyl = pPtColl as IPolyline;
                        m_pCurFeature.Shape = pPolyl;
                        m_pCurFeature.Store();
                    }
                }
                //////////////////////////////////////////////////////////////////*/
                OleDbConnection mycon = new OleDbConnection(ForBusInfo.Connect_Sql);
                mycon.Open();
                try
                {
                    OleDbDataAdapter da;
                    if (ForBusInfo.Connect_Type == 1)
                        da = new OleDbDataAdapter(String.Format("select StationID,StationOrder,BufferLength from  sde.RoadAndStation where RoadID = {0} Order by StationOrder", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"))), mycon);
                    else
                        da = new OleDbDataAdapter(String.Format("select StationID,StationOrder,BufferLength from  RoadAndStation where RoadID = {0} Order by StationOrder", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"))), mycon);
                    DataSet ds = new DataSet();
                    int nQueryCount = da.Fill(ds);
                    if (nQueryCount > 0)
                    {
                        IFeature pFea;
                        frmEditRoadAndStation frmPopup = new frmEditRoadAndStation();
                        frmPopup.m_bNew = false;
                        foreach (DataRow eRow in ds.Tables[0].Rows)
                        {
                            pFea = EngineFuntions.GetOneSeartchFeature(EngineFuntions.m_Layer_BusStation, "OBJECTID = " + eRow[0].ToString());
                            if (pFea != null)
                                frmPopup.m_CurStationList.Add(pFea);
                            frmPopup.n_nBufferLength = (int)eRow[2];
                        }
                        frmPopup.m_pCurFeature = m_pCurFeature;
                        EngineFuntions.m_AxMapControl.ActiveView.GraphicsContainer.DeleteAllElements();
                        object Missing = Type.Missing;
                        IConstructCurve mycurve = new PolylineClass();
                        mycurve.ConstructOffset((IPolycurve)m_pCurFeature.Shape, frmPopup.n_nBufferLength, ref Missing, ref Missing);
                        IPolygon pPolygon;
                        EngineFuntions.m_Layer_BusStation.Selectable = true;
                        pPolygon = (IPolygon)EngineFuntions.ClickSel((IGeometry)mycurve, false, false, 25);
                        EngineFuntions.AddPolygonElement(pPolygon);
                        if (EngineFuntions.GetSeledFeatures(EngineFuntions.m_Layer_BusStation, ref m_featureCollection))
                        {
                            for (int i = m_featureCollection.Count; i > 0; i--)
                            {
                                foreach (IFeature eFeature in frmPopup.m_CurStationList)
                                {
                                    if (m_featureCollection[i - 1].OID == eFeature.OID)
                                    {
                                        m_featureCollection.Remove(m_featureCollection[i - 1]);
                                        break;
                                    }
                                }
                            }

                            frmPopup.m_SelStationList = m_featureCollection;
                        }
                        mycon.Close();
                        frmPopup.Show();
                    }
                    else//第一次选择中，没有关联过站点的。
                    {
                        EngineFuntions.m_AxMapControl.ActiveView.GraphicsContainer.DeleteAllElements();
                        object Missing = Type.Missing;
                        IConstructCurve mycurve = new PolylineClass();
                        mycurve.ConstructOffset((IPolycurve)m_pCurFeature.Shape, 25, ref Missing, ref Missing);
                        IPolygon pPolygon;
                        EngineFuntions.m_Layer_BusStation.Selectable = true;
                        pPolygon = (IPolygon)EngineFuntions.ClickSel((IGeometry)mycurve, false, false, 25);
                        EngineFuntions.AddPolygonElement(pPolygon);
                        if (EngineFuntions.GetSeledFeatures(EngineFuntions.m_Layer_BusStation, ref m_featureCollection))
                        {
                            //frmRoadAndStation frmPopup = new frmRoadAndStation();//RoadAndStation窗口，不使用了，同意成EditRoadAndStation，在m_bNew参数判断是不是第一次添加
                            //frmPopup.m_featureCollection = m_featureCollection;
                            //frmPopup.m_pCurFeature = m_pCurFeature;
                            //frmPopup.Show();

                            frmEditRoadAndStation frmPopup = new frmEditRoadAndStation();
                            frmPopup.m_bNew = true;
                            frmPopup.n_nBufferLength = 25;
                            frmPopup.m_pCurFeature = m_pCurFeature;
                            frmPopup.m_CurStationList = m_featureCollection;
                            frmPopup.m_SelStationList.Clear();
                            frmPopup.Show();
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

        private void 显示站点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_pCurFeature != null)
            {
                OleDbConnection mycon = new OleDbConnection(ForBusInfo.Connect_Sql);
                mycon.Open();
                try
                {
                    OleDbDataAdapter da;
                    if (ForBusInfo.Connect_Type == 1)
                        da = new OleDbDataAdapter(String.Format("select StationID,StationOrder from  sde.RoadAndStation where RoadID = {0} Order by StationOrder", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"))), mycon);
                    else
                        da = new OleDbDataAdapter(String.Format("select StationID,StationOrder from  RoadAndStation where RoadID = {0} Order by StationOrder", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"))), mycon);
                    DataSet ds = new DataSet();
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
                        frmPopup.m_ds = ds;
                        frmPopup.Show();
                        frmPopup.RefreshStationGrid(strInPara); //填充完以后Gridview的值才是要排序的
                        frmPopup.SetStationOrderCell(nList); //填写排序值，使用的站点编号字段
                        frmPopup.SetSortColumn(2); //按站点编号列排序
                    }
                    mycon.Close();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("生成关联表出错\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void 制作单ToolStripMenuItem_Click(object sender, EventArgs e)//记得更改SetRoadTableTitle里面的清除非固定的内容
        {
            string strPath = DateTime.Now.ToLongTimeString();
            strPath = strPath.Replace(":", "-");
            strPath = string.Format("{0}\\制作单\\{1}", System.Windows.Forms.Application.StartupPath,strPath);
            System.IO.Directory.CreateDirectory(strPath);

            Excelapp app = new Excelapp();
            if (app == null)
            {
                MessageBox.Show("创建Excel服务失败!\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            app.Visible = true;
            app.DisplayAlerts = false;
            Workbooks workbooks = app.Workbooks;
            _Workbook workbook = workbooks.Open(Winapp.StartupPath + "\\data\\制作单.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Sheets sheets = workbook.Worksheets;
            _Worksheet worksheet = (_Worksheet)sheets.get_Item(1);

            m_featureCollection.Clear();
            DataGridView1.EndEdit();
            List<IFeature> pCurFeatureList;
            bool bCheck = false;

            List<string> pRoadNames = new List<string>();

            foreach (DataGridViewRow eRow in DataGridView1.Rows)////判断是否打钩进行多选择  删除重复的线路名（去行回行只记一次）
            {
                if (eRow.Cells[0].Value != null && (bool)eRow.Cells[0].Value == true)
                {
                    bCheck = false;
                    foreach (string eRoadName in pRoadNames)
                    {
                        if (eRow.Cells["RoadName"].Value.ToString() == eRoadName)
                        {
                            bCheck = true;
                            break;
                        }
                    }

                    if (bCheck == false)
                    {
                        pRoadNames.Add(eRow.Cells["RoadName"].Value.ToString());
                    }
                }
            }

            bCheck = false;
            foreach (string eRoadName in pRoadNames)//按去回行不重复线路生成
            {
                pCurFeatureList = EngineFuntions.GetSeartchFeatures(EngineFuntions.m_Layer_BusRoad, string.Format("RoadName = '{0}'", eRoadName));
                    int i = 0, j = 0;

                    SetRoadTableTitle(app, worksheet, pCurFeatureList[0], false);
                    //开始遍历站点,去行、回行站点分别读取列表，去行、回行可能不一样。
                    OleDbConnection mycon = new OleDbConnection(ForBusInfo.Connect_Sql);
                    mycon.Open();
                    try
                    {
                        foreach (IFeature pCurFeature in pCurFeatureList)
                        {
                            if (pCurFeature.get_Value(pCurFeature.Fields.FindField("RoadTravel")).ToString() == "去行")
                            {
                                i = SetRoadTableQu(mycon, worksheet, pCurFeature);
                            }
                            if (pCurFeature.get_Value(pCurFeature.Fields.FindField("RoadTravel")).ToString() == "回行")
                            {
                                j = SetRoadTableHui(mycon, worksheet, pCurFeature);
                            }
                            if (i > j)//设置打印区域
                            {
                                worksheet.PageSetup.PrintArea = string.Format("$A$1:$K${0}", i * 3 + 7);
                            }
                            else
                            {
                                worksheet.PageSetup.PrintArea = string.Format("$A$1:$K${0}", j * 3 + 7);
                            }
                            // string vFileName = "D:\\制作单\\" + pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("RoadName")) + ".xls";

                            //if (File.Exists(vFileName))
                            //{
                            //    IntPtr vHandle = ForBusInfo._lopen(vFileName, ForBusInfo.OF_READWRITE | ForBusInfo.OF_SHARE_DENY_NONE);
                            //    if (vHandle == ForBusInfo.HFILE_ERROR)
                            //    {
                            //        ForBusInfo.CloseHandle(vHandle);
                            //        continue;
                            //    }
                            //    ForBusInfo.CloseHandle(vHandle);
                            //}
                            workbook.SaveAs(strPath + "\\" + eRoadName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, null);
                        }
                        mycon.Close();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("生成关联表出错\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    bCheck = true;
            }
           

            if (bCheck)
            {
                //System.Diagnostics.Process.Start(strPath);
            }
            else//右键直接选择出表，单一的一个。
            {
                if (m_pCurFeature != null)
                {
                    pCurFeatureList = EngineFuntions.GetSeartchFeatures(EngineFuntions.m_Layer_BusRoad, string.Format("RoadName = '{0}'", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadName"))));
                    int i = 0, j = 0;

                    SetRoadTableTitle(app, worksheet, pCurFeatureList[0], true);
                    //开始遍历站点,去行、回行站点分别读取列表，去行、回行可能不一样。
                    OleDbConnection mycon = new OleDbConnection(ForBusInfo.Connect_Sql);
                    mycon.Open();
                    try
                    {
                        foreach (IFeature pCurFeature in pCurFeatureList)
                        {
                            if (pCurFeature.get_Value(pCurFeature.Fields.FindField("RoadTravel")).ToString() == "去行")
                            {
                                i = SetRoadTableQu(mycon, worksheet, pCurFeature);
                            }
                            if (pCurFeature.get_Value(pCurFeature.Fields.FindField("RoadTravel")).ToString() == "回行")
                            {

                                j = SetRoadTableHui(mycon, worksheet, pCurFeature);
                            }
                            if (i > j)
                            {
                                worksheet.PageSetup.PrintArea = string.Format("$A$1:$K${0}", i * 3 + 7);
                            }
                            else
                            {
                                worksheet.PageSetup.PrintArea = string.Format("$A$1:$K${0}", j * 3 + 7);
                            }
                            workbook.SaveAs(strPath + "\\" + pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("RoadName")), Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, null);
                        }

                        mycon.Close();
                    }
                        
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("生成关联表出错\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            System.Diagnostics.Process.Start(strPath);
            app.Quit();
        }

        private int SetRoadTableQu(OleDbConnection mycon, _Worksheet worksheet, IFeature pCurFeature)
        {
            OleDbDataAdapter da;
            if (ForBusInfo.Connect_Type == 1)
                da = new OleDbDataAdapter(String.Format("select a.* from sde.公交站点 a inner join sde.RoadAndStation b on (a.OBJECTID = b.StationID and b.RoadID = {0}) Order by b.StationOrder", pCurFeature.get_Value(pCurFeature.Fields.FindField("OBJECTID"))), mycon);
            else
                da = new OleDbDataAdapter(String.Format("select a.* from 公交站点 a inner join RoadAndStation b on (a.OBJECTID = b.StationID and b.RoadID = {0}) Order by b.StationOrder", pCurFeature.get_Value(pCurFeature.Fields.FindField("OBJECTID"))), mycon);
            DataSet ds = new DataSet();
            int nQueryCount = da.Fill(ds);
            if (nQueryCount > 0)
            {
                int i = 0;
                Range range1 = worksheet.get_Range("D4", "F4");
                range1.Value2 = ds.Tables[0].Rows[0]["StationName"];
                //range1 = worksheet.get_Range("D5", "E5");
                //range1.Value2 = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][2];
                foreach (DataRow eTableRow in ds.Tables[0].Rows)
                {
                    range1 = worksheet.get_Range(string.Format("B{0}", 8 + (3 * i)), string.Format("B{0}", 9 + (3 * i)));
                    range1.Value2 = eTableRow["StationName"];
                    range1 = worksheet.get_Range(string.Format("B{0}", 10 + (3 * i)), string.Format("B{0}", 10 + (3 * i)));
                    range1.Value2 = eTableRow["DispatchStationThird"];//站点说明
                    range1 = worksheet.get_Range(string.Format("C{0}", 8 + (3 * i)), string.Format("C{0}", 8 + (3 * i)));
                    range1.Value2 = eTableRow["DispatchCompanyFirst"];//线路牌材质
                    range1 = worksheet.get_Range(string.Format("D{0}", 8 + (3 * i)), string.Format("D{0}", 8 + (3 * i)));
                    range1.Value2 = eTableRow["DispatchRouteFirst"];// 线路牌尺寸
                    range1 = worksheet.get_Range(string.Format("E{0}", 8 + (3 * i)), string.Format("E{0}", 8 + (3 * i)));
                    range1.Value2 = eTableRow["DispatchStationFirst"];//线路牌制作单位
                    range1 = worksheet.get_Range(string.Format("C{0}", 9 + (3 * i)), string.Format("C{0}", 9 + (3 * i)));
                    range1.Value2 = eTableRow["DispatchStationSecond"];//线路牌材质2
                    range1 = worksheet.get_Range(string.Format("D{0}", 9 + (3 * i)), string.Format("D{0}", 9 + (3 * i)));
                    range1.Value2 = eTableRow["DispatchCompanyThird"];//线路牌尺寸2
                    range1 = worksheet.get_Range(string.Format("E{0}", 9 + (3 * i)), string.Format("E{0}", 9 + (3 * i)));
                    range1.Value2 = eTableRow["DispatchRouteThird"];//线路牌制作单位2
                    range1 = worksheet.get_Range(string.Format("C{0}", 10 + (3 * i)), string.Format("C{0}", 10 + (3 * i)));
                    range1.Value2 = eTableRow["Constructor"];//线路牌材质3
                    range1 = worksheet.get_Range(string.Format("D{0}", 10 + (3 * i)), string.Format("D{0}", 10 + (3 * i)));
                    range1.Value2 = eTableRow["ConstructionTime"];//线路牌尺寸3
                    range1 = worksheet.get_Range(string.Format("E{0}", 10 + (3 * i)), string.Format("E{0}", 10 + (3 * i)));
                    range1.Value2 = eTableRow["StationLand"];//线路牌制作单位3
                    range1 = worksheet.get_Range(string.Format("F{0}", 8 + (3 * i)), string.Format("F{0}", 9 + (3 * i)));
                    range1.Value2 = eTableRow["StationCharacter"];//站点所在道路
                    range1 = worksheet.get_Range(string.Format("F{0}", 10 + (3 * i)), string.Format("F{0}", 10 + (3 * i++)));
                    range1.Value2 = eTableRow["StationAlias"];//副站名
                }

            }
            return nQueryCount;
        }

        private int SetRoadTableHui(OleDbConnection mycon, _Worksheet worksheet, IFeature pCurFeature)
        {
            OleDbDataAdapter da;
            if (ForBusInfo.Connect_Type == 1)
                da = new OleDbDataAdapter(String.Format("select a.* from sde.公交站点 a inner join sde.RoadAndStation b on (a.OBJECTID = b.StationID and b.RoadID = {0}) Order by b.StationOrder", pCurFeature.get_Value(pCurFeature.Fields.FindField("OBJECTID"))), mycon);
            else
                da = new OleDbDataAdapter(String.Format("select a.* from 公交站点 a inner join RoadAndStation b on (a.OBJECTID = b.StationID and b.RoadID = {0}) Order by b.StationOrder", pCurFeature.get_Value(pCurFeature.Fields.FindField("OBJECTID"))), mycon);
            DataSet ds = new DataSet();
            int nQueryCount = da.Fill(ds);
            if (nQueryCount > 0)
            {
                int i = 0;
                Range range1 = worksheet.get_Range("D5", "F5");
                range1.Value2 = ds.Tables[0].Rows[0]["StationName"];
                //range1 = worksheet.get_Range("D5", "E5");
                //range1.Value2 = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][2];
                foreach (DataRow eTableRow in ds.Tables[0].Rows)
                {
                    range1 = worksheet.get_Range(string.Format("G{0}", 8 + (3 * i)), string.Format("G{0}", 9 + (3 * i)));
                    range1.Value2 = eTableRow["StationName"];
                    range1 = worksheet.get_Range(string.Format("G{0}", 10 + (3 * i)), string.Format("G{0}", 10 + (3 * i)));
                    range1.Value2 = eTableRow["DispatchStationThird"];//站点说明
                    range1 = worksheet.get_Range(string.Format("H{0}", 8 + (3 * i)), string.Format("H{0}", 8 + (3 * i)));
                    range1.Value2 = eTableRow["DispatchCompanyFirst"];//线路牌材质
                    range1 = worksheet.get_Range(string.Format("I{0}", 8 + (3 * i)), string.Format("I{0}", 8 + (3 * i)));
                    range1.Value2 = eTableRow["DispatchRouteFirst"];// 线路牌尺寸
                    range1 = worksheet.get_Range(string.Format("J{0}", 8 + (3 * i)), string.Format("J{0}", 8 + (3 * i)));
                    range1.Value2 = eTableRow["DispatchStationFirst"];//线路牌制作单位
                    range1 = worksheet.get_Range(string.Format("H{0}", 9 + (3 * i)), string.Format("H{0}", 9 + (3 * i)));
                    range1.Value2 = eTableRow["DispatchStationSecond"];//线路牌材质2
                    range1 = worksheet.get_Range(string.Format("I{0}", 9 + (3 * i)), string.Format("I{0}", 9 + (3 * i)));
                    range1.Value2 = eTableRow["DispatchCompanyThird"];//线路牌尺寸2
                    range1 = worksheet.get_Range(string.Format("J{0}", 9 + (3 * i)), string.Format("J{0}", 9 + (3 * i)));
                    range1.Value2 = eTableRow["DispatchRouteThird"];//线路牌制作单位2
                    range1 = worksheet.get_Range(string.Format("H{0}", 10 + (3 * i)), string.Format("H{0}", 10 + (3 * i)));
                    range1.Value2 = eTableRow["Constructor"];//线路牌材质3
                    range1 = worksheet.get_Range(string.Format("I{0}", 10 + (3 * i)), string.Format("I{0}", 10 + (3 * i)));
                    range1.Value2 = eTableRow["ConstructionTime"];//线路牌尺寸3
                    range1 = worksheet.get_Range(string.Format("J{0}", 10 + (3 * i)), string.Format("J{0}", 10 + (3 * i)));
                    range1.Value2 = eTableRow["StationLand"];//线路牌制作单位3
                    range1 = worksheet.get_Range(string.Format("K{0}", 8 + (3 * i)), string.Format("K{0}", 9 + (3 * i)));
                    range1.Value2 = eTableRow["StationCharacter"];//站点所在道路
                    range1 = worksheet.get_Range(string.Format("k{0}", 10 + (3 * i)), string.Format("K{0}", 10 + (3 * i++)));
                    range1.Value2 = eTableRow["StationAlias"];//副站名
                }

            }
            return nQueryCount;
        }

        private void SetRoadTableTitle(Excelapp app, _Worksheet worksheet, IFeature pCurFeature,bool bVisable)
        {
            app.Visible = bVisable;
            Range range1 = worksheet.get_Range("B8", "K157");
            range1.Cells.ClearContents();//清除非固定的内容
            range1 = worksheet.get_Range("A1", "I3");
            if (range1 == null)
            {
                MessageBox.Show("标题设置失败!\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            range1.Value2 = string.Format("制作单{0}路 {1}", pCurFeature.get_Value(pCurFeature.Fields.FindField("RoadName")), pCurFeature.get_Value(pCurFeature.Fields.FindField("Company")));
            range1 = worksheet.get_Range("A4", "A5");
            range1.Value2 = pCurFeature.get_Value(pCurFeature.Fields.FindField("RoadType"));
            range1 = worksheet.get_Range("B4", "C5");
            range1.Value2 = string.Format("票价：{0} {1} {2} {3}", pCurFeature.get_Value(pCurFeature.Fields.FindField("TicketPrice1")), pCurFeature.get_Value(pCurFeature.Fields.FindField("TicketPrice2")), pCurFeature.get_Value(pCurFeature.Fields.FindField("TicketPrice3")), pCurFeature.get_Value(pCurFeature.Fields.FindField("Picture5")));
            if (pCurFeature.get_Value(pCurFeature.Fields.FindField("RoadTravel")).ToString() == "去行")
            {
                range1 = worksheet.get_Range("H4", "H4");//首站开班
                range1.Value2 = pCurFeature.get_Value(pCurFeature.Fields.FindField("FirstStartTime"));
                range1 = worksheet.get_Range("K4", "K4");//首站收班
                range1.Value2 = pCurFeature.get_Value(pCurFeature.Fields.FindField("FirstCloseTime"));
                range1 = worksheet.get_Range("H5", "H5");//末站开班
                range1.Value2 = pCurFeature.get_Value(pCurFeature.Fields.FindField("EndStartTime"));
                range1 = worksheet.get_Range("K5", "K5");//末站收班
                range1.Value2 = pCurFeature.get_Value(pCurFeature.Fields.FindField("EndCloseTim"));
            }
            else
            {
                range1 = worksheet.get_Range("H4", "H4");//首站开班
                range1.Value2 = pCurFeature.get_Value(pCurFeature.Fields.FindField("EndStartTime"));
                range1 = worksheet.get_Range("K4", "K4");//首站收班
                range1.Value2 = pCurFeature.get_Value(pCurFeature.Fields.FindField("EndCloseTim"));
                range1 = worksheet.get_Range("H5", "H5");//末站开班
                range1.Value2 = pCurFeature.get_Value(pCurFeature.Fields.FindField("FirstStartTime"));
                range1 = worksheet.get_Range("K5", "K5");//末站收班
                range1.Value2 = pCurFeature.get_Value(pCurFeature.Fields.FindField("FirstCloseTime"));
            }
           
        }

        private void 备份线路ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool bCheck = false;
            foreach (DataGridViewRow eRow in DataGridView1.Rows)
            {
                if (eRow.Cells[0].Value != null && (bool)eRow.Cells[0].Value == true)
                {
                    bCheck = true;
                    m_pCurFeature = EngineFuntions.GetOneSeartchFeature(EngineFuntions.m_Layer_BackRoad, string.Format("Roadname = '{0}' AND RoadTravel = '{1}'", eRow.Cells["Roadname"].Value.ToString(), eRow.Cells["RoadTravel"].Value.ToString()));
                    if (m_pCurFeature != null)
                    {
                        MessageBox.Show("临时图层已经存在该线路\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                    m_pCurFeature = EngineFuntions.GetOneSeartchFeature(EngineFuntions.m_Layer_BusRoad, "OBJECTID = " + eRow.Cells["OBJECTID"].Value.ToString());
                    IFeature pFeature = EngineFuntions.CopyFeature(EngineFuntions.m_Layer_BackRoad, m_pCurFeature);
                    OleDbConnection mycon = new OleDbConnection(ForBusInfo.Connect_Sql);
                    mycon.Open();
                    try
                    {
                        OleDbDataAdapter da;
                        if (ForBusInfo.Connect_Type == 1)
                            da = ForBusInfo.CreateCustomerAdapter(mycon, String.Format("select * from  sde.RoadAndStation where RoadID = {0}", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"))),
                               "", String.Format("delete from  sde.RoadAndStation where RoadID = {0}", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"))));
                        else
                            da = ForBusInfo.CreateCustomerAdapter(mycon, String.Format("select * from  RoadAndStation where RoadID = {0}", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"))),
                           "", String.Format("delete from  RoadAndStation where RoadID = {0}", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"))));
                        da.SelectCommand.ExecuteNonQuery();
                        DataSet ds = new DataSet();
                        int nQueryCount = da.Fill(ds);
                        foreach (DataRow eDataRow in ds.Tables[0].Rows)
                        {
                            if (ForBusInfo.Connect_Type == 1)
                                da.InsertCommand.CommandText = String.Format("insert into sde.RAndSBack(RoadID,StationID,StationOrder,BufferLength,Roadinfo ,StationInfo) values({0},{1},{2},{3},'{4}','{5}')"
                               , pFeature.get_Value(pFeature.Fields.FindField("OBJECTID")), eDataRow["StationID"], eDataRow["StationOrder"], eDataRow["BufferLength"], eDataRow["Roadinfo"], eDataRow["StationInfo"]);
                            else
                                da.InsertCommand.CommandText = String.Format("insert into RAndSBack(RoadID,StationID,StationOrder,BufferLength,Roadinfo ,StationInfo) values({0},{1},{2},{3},'{4}','{5}')"
                               , pFeature.get_Value(pFeature.Fields.FindField("OBJECTID")), eDataRow["StationID"], eDataRow["StationOrder"], eDataRow["BufferLength"], eDataRow["Roadinfo"], eDataRow["StationInfo"]);
                            da.InsertCommand.ExecuteNonQuery();
                        }
                        //da.DeleteCommand.ExecuteNonQuery();//删除原始站线关联站点数据
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
            //for (int i = DataGridView1.Rows.Count - 1; i >= 0; i--)//列表中删除已经备份的站线的显示行
            //{
            //    if (DataGridView1.Rows[i].Cells[0].Value != null && (bool)DataGridView1.Rows[i].Cells[0].Value == true)
            //    {
            //       DataGridView1.Rows.RemoveAt(DataGridView1.Rows[i].Index);
            //    }
            //}
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
                    OleDbConnection mycon = new OleDbConnection(ForBusInfo.Connect_Sql);
                    mycon.Open();
                    try
                    {
                        OleDbDataAdapter da;
                        if (ForBusInfo.Connect_Type == 1)
                            da = ForBusInfo.CreateCustomerAdapter(mycon, String.Format("select * from  sde.RoadAndStation where RoadID = {0}", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"))),
                                "", String.Format("delete from  sde.RoadAndStation where RoadID = {0}", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"))));
                        else
                            da = ForBusInfo.CreateCustomerAdapter(mycon, String.Format("select * from  RoadAndStation where RoadID = {0}", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"))),
                            "", String.Format("delete from  RoadAndStation where RoadID = {0}", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"))));
                        da.SelectCommand.ExecuteNonQuery();
                        DataSet ds = new DataSet();
                        int nQueryCount = da.Fill(ds);
                        foreach (DataRow eRow in ds.Tables[0].Rows)
                        {
                            if (ForBusInfo.Connect_Type == 1)
                                da.InsertCommand.CommandText = String.Format("insert into sde.RAndSBack(RoadID,StationID,StationOrder,BufferLength,Roadinfo ,StationInfo) values({0},{1},{2},{3},'{4}','{5}')"
                               , pFeature.get_Value(pFeature.Fields.FindField("OBJECTID")), eRow["StationID"], eRow["StationOrder"], eRow["BufferLength"], eRow["Roadinfo"], eRow["StationInfo"]);
                            else
                                da.InsertCommand.CommandText = String.Format("insert into RAndSBack(RoadID,StationID,StationOrder,BufferLength,Roadinfo ,StationInfo) values({0},{1},{2},{3},'{4}','{5}')"
                               , pFeature.get_Value(pFeature.Fields.FindField("OBJECTID")), eRow["StationID"], eRow["StationOrder"], eRow["BufferLength"], eRow["Roadinfo"], eRow["StationInfo"]);
                            da.InsertCommand.ExecuteNonQuery();
                        }
                        //da.DeleteCommand.ExecuteNonQuery();//删除原始站线关联站点数据
                        ForBusInfo.Add_Log(ForBusInfo.Login_name, "备份站线", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadName")).ToString(), "");
                        mycon.Close();
                        //m_pCurFeature.Delete();//删除原始站线
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("生成关联表出错\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    EngineFuntions.PartialRefresh(EngineFuntions.m_Layer_BackRoad);
                    //DataGridView1.Rows.RemoveAt(m_nCurRowIndex);
                }
            }
        }

        private void 生成反向线路ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool bCheck = false;
            string strDirect;
            foreach (DataGridViewRow eRow in DataGridView1.Rows)
            {
                if (eRow.Cells[0].Value != null && (bool)eRow.Cells[0].Value == true)
                {
                    bCheck = true;
                    if (eRow.Cells[6].Value.ToString() == "去行")
                        strDirect = "回行";
                    else
                        strDirect = "去行";

                    m_pCurFeature = EngineFuntions.GetOneSeartchFeature(EngineFuntions.m_Layer_BusRoad, string.Format("Roadname = '{0}' AND RoadTravel = '{1}'", eRow.Cells["Roadname"].Value.ToString(), strDirect));
                    if (m_pCurFeature != null)
                    {
                        MessageBox.Show("线路图层已经存在反向线路\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                    m_pCurFeature = EngineFuntions.GetOneSeartchFeature(EngineFuntions.m_Layer_BusRoad, string.Format("Roadname = '{0}' AND RoadTravel = '{1}'", eRow.Cells["Roadname"].Value.ToString(), eRow.Cells["RoadTravel"].Value.ToString()));
                    IPolyline pPLine = m_pCurFeature.ShapeCopy as IPolyline;
                    pPLine.ReverseOrientation();
                    m_pCurFeature.Shape = pPLine;
                    m_pCurFeature.set_Value(m_pCurFeature.Fields.FindField("RoadTravel"), strDirect);
                    EngineFuntions.CopyFeature(EngineFuntions.m_Layer_BusRoad, m_pCurFeature);
                    System.Threading.Thread.Sleep(1000);
                    MessageBox.Show("反向线路添加完成，请关联站点！\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (!bCheck)//没有check状态
            {
                if (m_pCurFeature != null)
                {
                    if (m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadTravel")).ToString() == "去行")
                        strDirect = "回行";
                    else
                        strDirect = "去行";
                    IFeature pFeature = EngineFuntions.GetOneSeartchFeature(EngineFuntions.m_Layer_BusRoad, string.Format("Roadname = '{0}' AND RoadTravel = '{1}'", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("Roadname")).ToString(), strDirect));
                    if (pFeature != null)
                    {
                        MessageBox.Show("线路图层已经存在反向线路\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    IPolyline pPLine = m_pCurFeature.ShapeCopy as IPolyline;
                    pPLine.ReverseOrientation();
                    m_pCurFeature.Shape = pPLine;
                    m_pCurFeature.set_Value(m_pCurFeature.Fields.FindField("RoadTravel"), strDirect);
                    EngineFuntions.CopyFeature(EngineFuntions.m_Layer_BusRoad, m_pCurFeature);
                    System.Threading.Thread.Sleep(1000);
                    MessageBox.Show("反向线路添加完成，请关联站点！\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            RefreshGrid();
            EngineFuntions.PartialRefresh(EngineFuntions.m_Layer_BackRoad);
        }

    }
}
