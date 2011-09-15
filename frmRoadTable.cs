using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Businfo.Globe;
using ESRI.ArcGIS.Controls;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;
using Excelapp = Microsoft.Office.Interop.Excel.Application;

namespace Businfo
{
    public partial class frmRoadTable : Form
    {
        public List<IFeature> m_featureCollection = new List<IFeature>();  //得到所有选中的feature
        public frmRoadTable()
        {
            InitializeComponent();
        }

        private void frmRoadTable_Load(object sender, EventArgs e)
        {
            String strInPara = "";
            if (m_featureCollection.Count > 0)
            {
                foreach (IFeature pFeature in m_featureCollection)
                {
                    strInPara = String.Format("{0}{1},", strInPara, pFeature.get_Value(pFeature.Fields.FindField("OBJECTID")).ToString());
                }
                ForBusInfo.StationFill(dataGridView1, ForBusInfo.GridSetType.Road_FillByOBJECTID, string.Format(" WHERE (OBJECTID IN ({0}))", strInPara.Substring(0, strInPara.Length - 1)), new string[] { "" });
            }
            dataGridView1.RowHeadersWidth = 60;
            //int nNum = 1;
            //foreach (DataGridViewRow eRow in dataGridView1.Rows)
            //{
            //    eRow.HeaderCell.Value = nNum++.ToString();
            //}
            foreach (DataGridViewColumn eColumn in dataGridView1.Columns)
            {
                eColumn.ReadOnly = true;
            }
            dataGridView1.Columns[0].ReadOnly = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                foreach (DataGridViewRow eRow in dataGridView1.Rows)
                {
                    eRow.Cells[0].Value = true;
                }
            }
            else
            {
                foreach (DataGridViewRow eRow in dataGridView1.Rows)
                {
                    eRow.Cells[0].Value = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            if (String.IsNullOrEmpty(comboBox1.Text))
            {
                if (String.IsNullOrEmpty(textBox1.Text))
                {
                    ForBusInfo.StationFill(dataGridView1, ForBusInfo.GridSetType.Road_FillByOBJECTID, " WHERE (OBJECTID > 0)", new string[] { "" });
                }
                else
                {
                    ForBusInfo.StationFill(dataGridView1, ForBusInfo.GridSetType.Road_FillByOBJECTID, string.Format(" WHERE (RoadName LIKE '%{0}%')", textBox1.Text), new string[] { "" });
                }
                
            }
            else
            {
                ForBusInfo.StationFill(dataGridView1, ForBusInfo.GridSetType.Road_FillByOBJECTID, string.Format(" WHERE (Company = '{0}')", comboBox1.Text), new string[] { "" });
            }
            //int nNum = 1;
            //foreach (DataGridViewRow eRow in dataGridView1.Rows)
            //{
            //    eRow.HeaderCell.Value = nNum++.ToString();
            //}
            dataGridView1.Columns[0].ReadOnly = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strPath = DateTime.Now.ToLongTimeString();
            strPath = strPath.Replace(":", "-");
            strPath = string.Format("D:\\制作单\\{0}", strPath); 
            System.IO.Directory.CreateDirectory(strPath);

            Excelapp app = new Excelapp();
            if (app == null)
            {
                MessageBox.Show("创建Excel服务失败!\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            app.DisplayAlerts = false;
            Workbooks workbooks = app.Workbooks;
            _Workbook workbook = workbooks.Open(System.Windows.Forms.Application.StartupPath + "\\data\\制作单.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Sheets sheets = workbook.Worksheets;
            _Worksheet worksheet = (_Worksheet)sheets.get_Item(1);

            m_featureCollection.Clear();
            List<IFeature> pCurFeatureList;

            List<string> pRoadNames = new List<string>();
            bool bHave;
            foreach (DataGridViewRow eRow in dataGridView1.Rows)////判断是否打钩进行多选择  删除重复的线路名（去行回行只记一次）
            {
                if (eRow.Cells[0].Value != null && (bool)eRow.Cells[0].Value == true)
                {
                    bHave = false;
                    foreach (string eRoadName in pRoadNames)
                    {
                        if(eRow.Cells["RoadName"].Value.ToString() == eRoadName)
                        {
                            bHave = true;
                            break;
                        }
                    }

                    if (bHave == false)
                    {
                        pRoadNames.Add(eRow.Cells["RoadName"].Value.ToString());
                    }
                }
            }
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
                                worksheet.PageSetup.PrintArea = string.Format("$A$1:$K${0}", i * 2 + 7);
                            }
                            else
                            {
                                worksheet.PageSetup.PrintArea = string.Format("$A$1:$K${0}", j * 2 + 7);
                            }

                            workbook.SaveAs(strPath + "\\" + eRoadName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, null);
                        }
                        mycon.Close();
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("生成关联表出错\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
            }
            app.Quit();
            System.Diagnostics.Process.Start(strPath);
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
                foreach (DataRow eTableRow in ds.Tables[0].Rows)
                {
                    range1 = worksheet.get_Range(string.Format("B{0}", 8 + (2 * i)), string.Format("B{0}", 8 + (2 * i)));
                    range1.Value2 = eTableRow["StationName"];
                    range1 = worksheet.get_Range(string.Format("B{0}", 9 + (2 * i)), string.Format("B{0}", 9 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchStationThird"];//站点说明
                    range1 = worksheet.get_Range(string.Format("C{0}", 8 + (2 * i)), string.Format("C{0}", 8 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchCompanyFirst"];//线路牌材质
                    range1 = worksheet.get_Range(string.Format("D{0}", 8 + (2 * i)), string.Format("D{0}", 8 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchRouteFirst"];// 线路牌尺寸
                    range1 = worksheet.get_Range(string.Format("E{0}", 8 + (2 * i)), string.Format("E{0}", 8 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchStationFirst"];//线路牌制作单位
                    range1 = worksheet.get_Range(string.Format("C{0}", 9 + (2 * i)), string.Format("C{0}", 9 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchStationSecond"];//线路牌材质2
                    range1 = worksheet.get_Range(string.Format("D{0}", 9 + (2 * i)), string.Format("D{0}", 9 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchCompanyThird"];//线路牌尺寸2
                    range1 = worksheet.get_Range(string.Format("E{0}", 9 + (2 * i)), string.Format("E{0}", 9 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchRouteThird"];//线路牌制作单位2
                    range1 = worksheet.get_Range(string.Format("F{0}", 8 + (2 * i)), string.Format("F{0}", 8 + (2 * i)));
                    range1.Value2 = eTableRow["StationCharacter"];//站点所在道路
                    range1 = worksheet.get_Range(string.Format("F{0}", 9 + (2 * i)), string.Format("F{0}", 9 + (2 * i++)));
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
                foreach (DataRow eTableRow in ds.Tables[0].Rows)
                {
                    range1 = worksheet.get_Range(string.Format("G{0}", 8 + (2 * i)), string.Format("G{0}", 8 + (2 * i)));
                    range1.Value2 = eTableRow["StationName"];
                    range1 = worksheet.get_Range(string.Format("G{0}", 9 + (2 * i)), string.Format("G{0}", 9 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchStationThird"];//站点说明
                    range1 = worksheet.get_Range(string.Format("H{0}", 8 + (2 * i)), string.Format("H{0}", 8 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchCompanyFirst"];//线路牌材质
                    range1 = worksheet.get_Range(string.Format("I{0}", 8 + (2 * i)), string.Format("I{0}", 8 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchRouteFirst"];// 线路牌尺寸
                    range1 = worksheet.get_Range(string.Format("J{0}", 8 + (2 * i)), string.Format("J{0}", 8 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchStationFirst"];//线路牌制作单位
                    range1 = worksheet.get_Range(string.Format("H{0}", 9 + (2 * i)), string.Format("H{0}", 9 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchStationSecond"];//线路牌材质2
                    range1 = worksheet.get_Range(string.Format("I{0}", 9 + (2 * i)), string.Format("I{0}", 9 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchCompanyThird"];//线路牌尺寸2
                    range1 = worksheet.get_Range(string.Format("J{0}", 9 + (2 * i)), string.Format("J{0}", 9 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchRouteThird"];//线路牌制作单位2
                    range1 = worksheet.get_Range(string.Format("K{0}", 8 + (2 * i)), string.Format("K{0}", 8 + (2 * i)));
                    range1.Value2 = eTableRow["StationCharacter"];//站点所在道路
                    range1 = worksheet.get_Range(string.Format("k{0}", 9 + (2 * i)), string.Format("K{0}", 9 + (2 * i++)));
                    range1.Value2 = eTableRow["StationAlias"];//副站名
                }

            }
            return nQueryCount;
        }

        private void SetRoadTableTitle(Excelapp app, _Worksheet worksheet, IFeature pCurFeature, bool bVisable)
        {
            app.Visible = bVisable;
            Range range1 = worksheet.get_Range("B8", "K127");
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
            range1 = worksheet.get_Range("H4", "H4");//首站开班
            range1.Value2 = pCurFeature.get_Value(pCurFeature.Fields.FindField("FirstStartTime"));
            range1 = worksheet.get_Range("K4", "K4");//首站收班
            range1.Value2 = pCurFeature.get_Value(pCurFeature.Fields.FindField("FirstCloseTime"));
            range1 = worksheet.get_Range("H5", "H5");//末站开班
            range1.Value2 = pCurFeature.get_Value(pCurFeature.Fields.FindField("EndStartTime"));
            range1 = worksheet.get_Range("K5", "K5");//末站收班
            range1.Value2 = pCurFeature.get_Value(pCurFeature.Fields.FindField("EndCloseTim"));
        }
    }
}
