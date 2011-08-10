using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Businfo.Globe;
using System.Data.OleDb;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;

using Excelapp = Microsoft.Office.Interop.Excel.Application;
using ExcelPoint = Microsoft.Office.Interop.Excel.IPoint;
using Microsoft.Office.Interop.Excel;

namespace Businfo
{
    public partial class frmRoadAndStation : Form//不使用这个对话框了。
    {
        public List<IFeature> m_featureCollection;//站点
        public int m_nRoadID;
        public IFeature m_pCurFeature;
        public List<BusStation> m_BusStationList = new List<BusStation>();

        public frmRoadAndStation()
        {
            InitializeComponent();
        }

        private void BtnUP_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedIndices.Contains(0))
            {
                return;
            }

            for (int i = ListBox1.SelectedIndices[0] - 1; i < ListBox1.SelectedIndices[ListBox1.SelectedIndices.Count - 1]; i++)
            {
                if (ListBox1.SelectedIndices.Contains(i + 1))
                {
                    Change(i, i + 1);
                }
            }
        }

        private void BtnDOWN_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedIndices.Contains(ListBox1.Items.Count - 1))
            {
                return;
            }

            for (int i = ListBox1.SelectedIndices[ListBox1.SelectedIndices.Count - 1] + 1; i > ListBox1.SelectedIndices[0]; i--)
            {
                if (ListBox1.SelectedIndices.Contains(i - 1))
                {
                    Change(i, i - 1);
                }
            }
        }

        private void BtnDele_Click(object sender, EventArgs e)
        {
            for (int i = ListBox1.SelectedIndices.Count ; i > 0 ; i--)
            {
                ListBox1.Items.RemoveAt(ListBox1.SelectedIndices[i-1]);
            }
        }

        private void BtnTrue_Click(object sender, EventArgs e)
        {
            int nDirect;
            if (CheckBox1.Checked)
               nDirect = -25;
            else
               nDirect = 25;
            OleDbConnection mycon = new OleDbConnection(ForBusInfo.Connect_Sql);;
            mycon.Open();
            try
            {
                int nOrder = 0;
                string pStrSQL;
                OleDbCommand pCom;
                if(ForBusInfo.Connect_Type == 1)
                    pStrSQL = String.Format("delete from sde.RoadAndStation where RoadID = {0}", m_nRoadID);
                else
                    pStrSQL = String.Format("delete from RoadAndStation where RoadID = {0}", m_nRoadID);
                pCom = new OleDbCommand(pStrSQL, mycon);
                pCom.ExecuteNonQuery();
                foreach (BusStation pBusStation in ListBox1.Items)
                {
                    if (ForBusInfo.Connect_Type == 1)
                        pStrSQL = String.Format("insert into sde.RoadAndStation(RoadID,StationID,StationOrder,BufferLength) values({0},{1},{2},{3})"
                        , m_nRoadID, pBusStation.ID, nOrder++,nDirect);
                    else
                        pStrSQL = String.Format("insert into RoadAndStation(RoadID,StationID,StationOrder,BufferLength) values({0},{1},{2},{3})"
                        , m_nRoadID, pBusStation.ID, nOrder++, nDirect);
                    pCom = new OleDbCommand(pStrSQL, mycon);
                    pCom.ExecuteNonQuery();         
                }
                ForBusInfo.Add_Log(ForBusInfo.Login_name, "线路关联站点", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadName")).ToString(), "");
                mycon.Close();
                this.Close();
                EngineFuntions.m_AxMapControl.Map.ClearSelection();
                EngineFuntions.m_AxMapControl.ActiveView.Refresh();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("生成关联表出错\n" + ex.ToString() , "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            m_featureCollection.Clear();
            EngineFuntions.m_AxMapControl.ActiveView.GraphicsContainer.DeleteAllElements();
            //平移
            IConstructCurve mycurve = new PolylineClass();
            IPolygon pPolygon;
            EngineFuntions.m_Layer_BusStation.Selectable = true;
            object Missing = Type.Missing;

            if (CheckBox1.Checked)
            {
                mycurve.ConstructOffset((IPolycurve)m_pCurFeature.Shape, -25, ref Missing, ref Missing);
                pPolygon = (IPolygon)EngineFuntions.ClickSel((IGeometry)mycurve , false, false, 25);
            } 
            else
            {
                mycurve.ConstructOffset((IPolycurve)m_pCurFeature.Shape, 25, ref Missing, ref Missing);
                pPolygon = (IPolygon)EngineFuntions.ClickSel((IGeometry)mycurve, false, false, 25);
            }
            EngineFuntions.AddPolygonElement(pPolygon);
            if (EngineFuntions.GetSeledFeatures(EngineFuntions.m_Layer_BusStation,ref m_featureCollection))
            {
                ListBox1.Items.Clear();
                m_BusStationList.Clear();
                IPolyline pPLine = m_pCurFeature.ShapeCopy as IPolyline;
                ESRI.ArcGIS.Geometry.IPoint outPoint = new PointClass();
                double distanceAlongCurve = 0;//该点在曲线上最近的点距曲线起点的距离
                double distanceFromCurve = 0;//该点到曲线的直线距离
                bool bRightSide = false;//点在线的左边还是右边
                bool asRatio = false;  //asRatio：byval方式，bool类型，表示上面两个参数给定的长度是以绝对距离的方式给出还是以占曲线总长度的比例的方式给出
                foreach (IFeature pfeat in m_featureCollection)
                {
                    pPLine.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pfeat.ShapeCopy as ESRI.ArcGIS.Geometry.IPoint, asRatio, outPoint, ref distanceAlongCurve, ref distanceFromCurve, ref bRightSide);
                    m_BusStationList.Add(new BusStation(pfeat.get_Value(pfeat.Fields.FindField("StationName")).ToString(), pfeat.get_Value(pfeat.Fields.FindField("Direct")).ToString(), (int)pfeat.get_Value(pfeat.Fields.FindField("OBJECTID")), distanceAlongCurve, pfeat.get_Value(pfeat.Fields.FindField("DispatchStationThird")).ToString(), pfeat.get_Value(pfeat.Fields.FindField("StationCharacter")).ToString()));
                    EngineFuntions.AddTextElement(pfeat.Shape, pfeat.get_Value(pfeat.Fields.FindField("StationName")).ToString());
                }
                m_BusStationList.Sort();
                foreach (BusStation pItem in m_BusStationList)
                {
                    ListBox1.Items.Add(pItem);
                }
            }
        }

        private void frmRoadAndStation_Load(object sender, EventArgs e)
        {
            ListBox1.Items.Clear();
            m_BusStationList.Clear();

          
            IPolyline pPLine = m_pCurFeature.ShapeCopy as IPolyline;
          
            ESRI.ArcGIS.Geometry.IPoint outPoint = new PointClass();
            double distanceAlongCurve = 0;//该点在曲线上最近的点距曲线起点的距离
            double distanceFromCurve = 0;//该点到曲线的直线距离
            bool bRightSide = false;//点在线的左边还是右边
            bool asRatio = false;  //asRatio：byval方式，bool类型，表示上面两个参数给定的长度是以绝对距离的方式给出还是以占曲线总长度的比例的方式给出
            foreach (IFeature pfeat in m_featureCollection)
            {
                pPLine.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pfeat.ShapeCopy as ESRI.ArcGIS.Geometry.IPoint, asRatio, outPoint, ref distanceAlongCurve, ref distanceFromCurve, ref bRightSide);
                m_BusStationList.Add(new BusStation(pfeat.get_Value(pfeat.Fields.FindField("StationName")).ToString(), pfeat.get_Value(pfeat.Fields.FindField("Direct")).ToString(), (int)pfeat.get_Value(pfeat.Fields.FindField("OBJECTID")), distanceAlongCurve, pfeat.get_Value(pfeat.Fields.FindField("DispatchStationThird")).ToString(), pfeat.get_Value(pfeat.Fields.FindField("StationCharacter")).ToString()));
                EngineFuntions.AddTextElement(pfeat.Shape, pfeat.get_Value(pfeat.Fields.FindField("StationName")).ToString());
            }
            m_BusStationList.Sort();
            foreach (BusStation pItem in m_BusStationList)
            {
                ListBox1.Items.Add(pItem);
            }
            Label1.Text = m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadName")).ToString() + m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadTravel")).ToString();
            m_nRoadID = (int)m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"));
            //ListBox1.SelectedIndex = 0;
        }

        private void Change(int nForm,int nTo)
        {
            object Temp;
            Temp = ListBox1.Items[nForm];
            ListBox1.Items[nForm] = ListBox1.Items[nTo];
            ListBox1.Items[nTo] = Temp;
            ListBox1.SelectedIndices.Remove(nTo);
            ListBox1.SelectedIndices.Add(nForm);
        }

        private void frmRoadAndStation_FormClosing(object sender, FormClosingEventArgs e)
        {
            EngineFuntions.m_AxMapControl.ActiveView.GraphicsContainer.DeleteAllElements();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            ListBox1.Items.Clear();
            m_BusStationList.Reverse();
            foreach (BusStation pItem in m_BusStationList)
            {
                ListBox1.Items.Add(pItem);
            }
        }

        private void ListBox1_Click(object sender, EventArgs e)//站点闪烁
        {
            //if (ListBox1.SelectedIndex >= 0)
            //{
            //    BusStation pBusStation = (BusStation)ListBox1.SelectedItem;
            //    IFeature pFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusStation, "OBJECTID", pBusStation.ID.ToString());
            //    Application.DoEvents();
            //    EngineFuntions.FlashShape(pFeature.ShapeCopy);
            //}
        }

        private void ListBox1_DoubleClick(object sender, EventArgs e)//双击闪烁及站点定位
        {
            //if (ListBox1.SelectedIndex >= 0)
            //{
            //    BusStation pBusStation = (BusStation)ListBox1.SelectedItem;
            //    IFeature pFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusStation, "OBJECTID", pBusStation.ID.ToString());
            //    IPoint pPoint;
            //    IEnvelope pEnvelope;
            //    IDisplayTransformation pDisplayTransformation;
            //    pDisplayTransformation = EngineFuntions.m_AxMapControl.ActiveView.ScreenDisplay.DisplayTransformation;
            //    pEnvelope = pFeature.Extent;
            //    pPoint = pEnvelope.UpperLeft;
            //    pEnvelope = pDisplayTransformation.VisibleBounds;
            //    pEnvelope.CenterAt(pPoint);
            //    pDisplayTransformation.VisibleBounds = pEnvelope;
            //    EngineFuntions.m_AxMapControl.Map.MapScale = 2000;
            //    pDisplayTransformation.VisibleBounds = EngineFuntions.m_AxMapControl.ActiveView.Extent;
            //    EngineFuntions.m_AxMapControl.ActiveView.ScreenDisplay.Invalidate(null, true, (short)esriScreenCache.esriAllScreenCaches);
            //    Application.DoEvents();
            //    EngineFuntions.FlashShape(pFeature.ShapeCopy);
            //}
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)//临时添加的，导出线路缓冲区所有的站点到excel
        {
            Excelapp app = new Excelapp();
            if (app == null)
            {
                MessageBox.Show("创建Excel服务失败!\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            app.Visible = false;//不打开excel
            app.DisplayAlerts = false;
            Workbooks workbooks = app.Workbooks;
          


                List<IFeature> featureCollection = EngineFuntions.GetSeartchFeatures(EngineFuntions.m_Layer_BusRoad, "OBJECTID > -1");
                for (int j = 0; j < featureCollection.Count; j++)
                {
                    IFeature pFea = featureCollection[j];
                    if (pFea.get_Value(pFea.Fields.FindField("RoadName")).ToString() == "703" && pFea.get_Value(pFea.Fields.FindField("RoadTravel")).ToString() == "去行")
                    {
                        continue;
                    }
                    if (pFea.get_Value(pFea.Fields.FindField("RoadName")).ToString() == "703")
                    //if (pFea.get_Value(pFea.Fields.FindField("Company")).ToString().Contains("三公司"))
                    {
                        EngineFuntions.m_AxMapControl.Map.ClearSelection();
                        object Missing = Type.Missing;
                        IConstructCurve mycurve = new PolylineClass();
                        mycurve.ConstructOffset((IPolycurve)pFea.Shape, 25, ref Missing, ref Missing);
                        EngineFuntions.ClickSel((IGeometry)mycurve, false, false, 25);
                        if (EngineFuntions.GetSeledFeatures(EngineFuntions.m_Layer_BusStation, ref m_featureCollection))
                        {
                            m_BusStationList.Clear();
                            IPolyline pPLine = pFea.ShapeCopy as IPolyline;
                            ESRI.ArcGIS.Geometry.IPoint outPoint = new PointClass();
                            double distanceAlongCurve = 0;//该点在曲线上最近的点距曲线起点的距离
                            double distanceFromCurve = 0;//该点到曲线的直线距离
                            bool bRightSide = false;//点在线的左边还是右边
                            bool asRatio = false;  //asRatio：byval方式，bool类型，表示上面两个参数给定的长度是以绝对距离的方式给出还是以占曲线总长度的比例的方式给出
                            foreach (IFeature pfeat in m_featureCollection)
                            {
                                pPLine.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pfeat.ShapeCopy as ESRI.ArcGIS.Geometry.IPoint, asRatio, outPoint, ref distanceAlongCurve, ref distanceFromCurve, ref bRightSide);
                                m_BusStationList.Add(new BusStation(pfeat.get_Value(pfeat.Fields.FindField("StationName")).ToString(), pfeat.get_Value(pfeat.Fields.FindField("Direct")).ToString(), (int)pfeat.get_Value(pfeat.Fields.FindField("OBJECTID")), distanceAlongCurve, pfeat.get_Value(pfeat.Fields.FindField("DispatchStationThird")).ToString(), pfeat.get_Value(pfeat.Fields.FindField("StationCharacter")).ToString()));
                                //EngineFuntions.AddTextElement(pfeat.Shape, pfeat.get_Value(pfeat.Fields.FindField("StationName")).ToString());
                            }
                            m_BusStationList.Sort();

                            //////////////////////////////////////////////////////
                            _Workbook workbook = workbooks.Open("C:\\1.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                            Sheets sheets = workbook.Worksheets;
                            _Worksheet worksheet = (_Worksheet)sheets.get_Item(1);
                            Range range1;
                            range1 = worksheet.get_Range("A1", "A1");
                            range1.Value2 = pFea.get_Value(pFea.Fields.FindField("RoadName")).ToString();
                            range1 = worksheet.get_Range("B1", "B1");
                            range1.Value2 = pFea.get_Value(pFea.Fields.FindField("RoadTravel")).ToString();
                            range1 = worksheet.get_Range("C1", "C1");
                            range1.Value2 = pFea.get_Value(pFea.Fields.FindField("OBJECTID")).ToString();
                            for (int i = 0; i < m_BusStationList.Count; i++)
                            {
                                BusStation eTableRow = m_BusStationList[i];
                                range1 = worksheet.get_Range(string.Format("A{0}", 3 + i), string.Format("A{0}", 3 + i));
                                range1.Value2 = eTableRow.StationName;
                                range1 = worksheet.get_Range(string.Format("B{0}", 3 + i), string.Format("B{0}", 3 + i));
                                range1.Value2 = eTableRow.StationExplain;
                                range1 = worksheet.get_Range(string.Format("C{0}", 3 + i), string.Format("C{0}", 3 + i));
                                range1.Value2 = eTableRow.Direct;
                                range1 = worksheet.get_Range(string.Format("D{0}", 3 + i), string.Format("D{0}", 3 + i));
                                range1.Value2 = eTableRow.StationCharacter;
                                range1 = worksheet.get_Range(string.Format("E{0}", 3 + i), string.Format("E{0}", 3 + i));
                                range1.Value2 = eTableRow.ID;
                            }
                            EngineFuntions.m_AxMapControl.Map.ClearSelection();
                            IConstructCurve mycurve1 = new PolylineClass();
                            mycurve1.ConstructOffset((IPolycurve)pFea.Shape, -25, ref Missing, ref Missing);
                            EngineFuntions.ClickSel((IGeometry)mycurve1, false, false, 25);
                            if (EngineFuntions.GetSeledFeatures(EngineFuntions.m_Layer_BusStation, ref m_featureCollection))
                            {
                                m_BusStationList.Clear();
                                IPolyline pPLine1 = pFea.ShapeCopy as IPolyline;
                                ESRI.ArcGIS.Geometry.IPoint outPoint1 = new PointClass();
                                foreach (IFeature pfeat in m_featureCollection)
                                {
                                    pPLine1.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pfeat.ShapeCopy as ESRI.ArcGIS.Geometry.IPoint, asRatio, outPoint, ref distanceAlongCurve, ref distanceFromCurve, ref bRightSide);
                                    m_BusStationList.Add(new BusStation(pfeat.get_Value(pfeat.Fields.FindField("StationName")).ToString(), pfeat.get_Value(pfeat.Fields.FindField("Direct")).ToString(), (int)pfeat.get_Value(pfeat.Fields.FindField("OBJECTID")), distanceAlongCurve, pfeat.get_Value(pfeat.Fields.FindField("DispatchStationThird")).ToString(), pfeat.get_Value(pfeat.Fields.FindField("StationCharacter")).ToString()));
                                    // EngineFuntions.AddTextElement(pfeat.Shape, pfeat.get_Value(pfeat.Fields.FindField("StationName")).ToString());
                                }
                                m_BusStationList.Sort();
                                m_BusStationList.Reverse();
                                _Worksheet worksheet1 = (_Worksheet)sheets.get_Item(2);
                                range1 = worksheet1.get_Range("A1", "A1");
                                range1.Value2 = pFea.get_Value(pFea.Fields.FindField("RoadName")).ToString();
                                range1 = worksheet1.get_Range("B1", "B1");
                                range1.Value2 = pFea.get_Value(pFea.Fields.FindField("RoadTravel")).ToString();
                                range1 = worksheet1.get_Range("C1", "C1");
                                range1.Value2 = pFea.get_Value(pFea.Fields.FindField("OBJECTID")).ToString();
                                for (int i = 0; i < m_BusStationList.Count; i++)
                                {
                                    BusStation eTableRow = m_BusStationList[i];
                                    range1 = worksheet1.get_Range(string.Format("A{0}", 3 + i), string.Format("A{0}", 3 + i));
                                    range1.Value2 = eTableRow.StationName;
                                    range1 = worksheet1.get_Range(string.Format("B{0}", 3 + i), string.Format("B{0}", 3 + i));
                                    range1.Value2 = eTableRow.StationExplain;
                                    range1 = worksheet1.get_Range(string.Format("C{0}", 3 + i), string.Format("C{0}", 3 + i));
                                    range1.Value2 = eTableRow.Direct;
                                    range1 = worksheet1.get_Range(string.Format("D{0}", 3 + i), string.Format("D{0}", 3 + i));
                                    range1.Value2 = eTableRow.StationCharacter;
                                    range1 = worksheet1.get_Range(string.Format("E{0}", 3 + i), string.Format("E{0}", 3 + i));
                                    range1.Value2 = eTableRow.ID;
                                }
                            }
                            workbook.SaveAs(string.Format("E:\\{0}-{1}({2})", pFea.get_Value(pFea.Fields.FindField("RoadName")), pFea.get_Value(pFea.Fields.FindField("RoadTravel")), pFea.get_Value(pFea.Fields.FindField("Company"))), Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, null);
                            workbooks.Close();
                    }
                    
                    }
                
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Excelapp app = new Excelapp();
                if (app == null)
                {
                    MessageBox.Show("创建Excel服务失败!\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                app.Visible = false;//不打开excel
                app.DisplayAlerts = false;
                Workbooks workbooks = app.Workbooks;
                OleDbConnection mycon = new OleDbConnection(ForBusInfo.Connect_Sql);
                mycon.Open();

                foreach (string folderName in openFileDialog1.FileNames)
                {
                    _Workbook workbook = workbooks.Open(folderName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    Sheets sheets = workbook.Worksheets;
                    _Worksheet worksheet = (_Worksheet)sheets.get_Item(1);
                    Range range1;
                    range1 = worksheet.get_Range("C1","C1");
                    m_nRoadID = Convert.ToInt32(range1.Value2.ToString());

                    range1 = worksheet.get_Range("A1","A1");
                    string RoadName = range1.Value2.ToString();
                    range1 = worksheet.get_Range("B1", "B1");
                    RoadName = RoadName + "-" + range1.Value2.ToString();

                    string StationName,StationID;

                    int nBufferLength;
                    if(worksheet.Name.ToString() == "表一")
                    {
                        nBufferLength = 25;
                    }
                    else
                        nBufferLength = -25;
                    
                    try
                    {
                        string pStrSQL;
                        OleDbCommand pCom;
                        if (ForBusInfo.Connect_Type == 1)
                            pStrSQL = String.Format("delete from  sde.RoadAndStation where RoadID = {0}", m_nRoadID);
                        else
                            pStrSQL = String.Format("delete from  RoadAndStation where RoadID = {0}", m_nRoadID);
                        pCom = new OleDbCommand(pStrSQL, mycon);
                        pCom.ExecuteNonQuery();
                        for (int i = 3; i < 60; i++ )
                        {
                            range1 = worksheet.get_Range(string.Format("A{0}", i), string.Format("A{0}",i));
                            if (range1.Value2 == null)
                            {
                                break;
                            }
                            StationName = range1.Value2.ToString();
                            range1 = worksheet.get_Range(string.Format("C{0}", i), string.Format("C{0}", i));
                            StationName = StationName + "-" + range1.Value2.ToString();

                            range1 = worksheet.get_Range(string.Format("E{0}", i), string.Format("E{0}", i));
                            StationID = range1.Value2.ToString();

                            if (ForBusInfo.Connect_Type == 1)
                                pStrSQL = String.Format("insert into sde.RoadAndStation(RoadID,StationID,StationOrder,BufferLength,Roadinfo ,StationInfo) values({0},{1},{2},{3},'{4}','{5}')"
                                , m_nRoadID, StationID, i - 3, nBufferLength
                                , RoadName
                                , StationName);
                            else
                                pStrSQL = String.Format("insert into RoadAndStation(RoadID,StationID,StationOrder,BufferLength,Roadinfo ,StationInfo) values({0},{1},{2},{3},'{4}','{5}')"
                                , m_nRoadID, StationID, i - 3, nBufferLength
                                 , RoadName
                                , StationName);
                            pCom = new OleDbCommand(pStrSQL, mycon);
                            pCom.ExecuteNonQuery();
                        }

                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("生成关联表出错\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                mycon.Close();
                app.Quit();
            }
        }
    }
}