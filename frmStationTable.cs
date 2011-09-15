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
using Microsoft.Office.Interop.Excel;

namespace Businfo
{
    public partial class frmStationTable : Form
    {
        public List<IFeature> m_featureCollection = new List<IFeature>();  //得到所有选中的feature
        public frmStationTable()
        {
            InitializeComponent();
        }

        private void frmStationTable_Load(object sender, EventArgs e)
        {
            String strInPara = "";
            if (m_featureCollection.Count > 0)
            {
                foreach (IFeature pFeature in m_featureCollection)
                {
                    strInPara = String.Format("{0}{1},", strInPara, pFeature.get_Value(pFeature.Fields.FindField("OBJECTID")).ToString());
                }
                ForBusInfo.StationFill(dataGridView1, ForBusInfo.GridSetType.Station_FillByOBJECTID, string.Format(" WHERE (OBJECTID IN ({0}))", strInPara.Substring(0, strInPara.Length - 1)), new string[] { "" });
            }
            dataGridView1.RowHeadersWidth = 60;
            dataGridView1.Columns[0].ReadOnly = false;
            dataGridView1.Columns[4].Frozen = false;
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
                if (String.IsNullOrEmpty(textBox1.Text))
                {
                    ForBusInfo.StationFill(dataGridView1, ForBusInfo.GridSetType.Station_FillByOBJECTID, " WHERE (OBJECTID > 0)", new string[] { "" });
                }
                else
                {
                    ForBusInfo.StationFill(dataGridView1, ForBusInfo.GridSetType.Station_FillByOBJECTID, string.Format(" WHERE (StationName LIKE '%{0}%')", textBox1.Text), new string[] { "" });
                }
            dataGridView1.Columns[0].ReadOnly = false;
            dataGridView1.Columns[4].Frozen = false;
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            OleDbConnection mycon = new OleDbConnection(ForBusInfo.Connect_Sql);
            mycon.Open();
            OleDbDataAdapter da;
            if (ForBusInfo.Connect_Type == 1)
                da = new OleDbDataAdapter(String.Format("select a.* from sde.公交站线 a inner join sde.RoadAndStation b on (a.OBJECTID = b.RoadID and b.StationID = {0})", dataGridView1.Rows[e.RowIndex].Cells["OBJECTID"].Value), mycon);
            else
                da = new OleDbDataAdapter(String.Format("select a.* from 公交站线 a inner join RoadAndStation b on (a.OBJECTID = b.RoadID and b.StationID = {0})", dataGridView1.Rows[e.RowIndex].Cells["OBJECTID"].Value), mycon);
            DataSet ds = new DataSet();
            int nQueryCount = da.Fill(ds, "Road");
            if (nQueryCount > 0)
            {
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = ds;
                dataGridView2.DataMember = "Road";
                //string strRoad = "";
                //foreach (DataRow eDataRow in ds.Tables["Road"].Rows)
                //{
                //    strRoad = strRoad + eDataRow["OBJECTID"].ToString() + ",";
                //}
                //ForBusInfo.StationFill(dataGridView2, ForBusInfo.GridSetType.Road_FillByOBJECTID, string.Format(" WHERE (OBJECTID IN ({0}))", strRoad.Substring(0, strRoad.Length - 1)), new string[] { "" });
                dataGridView2.Columns["CheckBox2"].Visible = false;
                ForBusInfo.SetGridHeard(dataGridView2, ForBusInfo.GridSetType.Road_FillByOBJECTID, new string[] { "" });
                ForBusInfo.SetColSortMode(dataGridView2, DataGridViewColumnSortMode.NotSortable);
                ForBusInfo.SetRowNo(dataGridView2);
            }
            mycon.Close();
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            ForBusInfo.SetRowNo(dataGridView1);
            ForBusInfo.SetRowNo(dataGridView2);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ForBusInfo.Excel_app == null)
            {
                MessageBox.Show("创建Excel服务失败!\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ForBusInfo.Excel_app.Visible = true;
            Workbooks workbooks = ForBusInfo.Excel_app.Workbooks;
            _Workbook workbook = workbooks.Open(System.Windows.Forms.Application.StartupPath + "\\data\\路单.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Sheets sheets = workbook.Worksheets;
            _Worksheet worksheet = (_Worksheet)sheets.get_Item(1);

            m_featureCollection.Clear();
            int nTotal = 0;
            foreach (DataGridViewRow eRow in dataGridView1.Rows)//判断是否打钩进行多选择
            {
                if (eRow.Cells[0].Value != null && (bool)eRow.Cells[0].Value == true)
                {
                    string strAB;
                    if (nTotal % 2 == 0)
                    {
                        strAB = "A";
                    }
                    else
                    {
                        strAB = "B";
                    }
                    Range range1 = worksheet.get_Range(string.Format("{0}{1}", strAB, 1 + (nTotal / 2) * 8), string.Format("{0}{1}", strAB, 1 + (nTotal / 2) * 8));
                    range1.Value2 = string.Format("{0}({1})({2}:{3})", eRow.Cells["StationName"].Value, eRow.Cells["DispatchStationThird"].Value, eRow.Cells["Direct"].Value, eRow.Cells["MainSymbol"].Value);
                    range1 = worksheet.get_Range(string.Format("{0}{1}", strAB, 2 + (nTotal / 2) * 8), string.Format("{0}{1}", strAB, 2 + (nTotal / 2) * 8));
                    range1.Value2 = string.Format("线路牌1:{0}:{1}:{2}      线路牌2：{3}:{4}:{5}", eRow.Cells["DispatchCompanyFirst"].Value, eRow.Cells["DispatchRouteFirst"].Value, eRow.Cells["DispatchStationFirst"].Value, eRow.Cells["DispatchStationSecond"].Value, eRow.Cells["DispatchCompanyThird"].Value, eRow.Cells["DispatchRouteThird"].Value);
                    range1 = worksheet.get_Range(string.Format("{0}{1}", strAB, 3 + (nTotal / 2) * 8), string.Format("{0}{1}", strAB, 3 + (nTotal / 2) * 8));
                    range1.Value2 = eRow.Cells["BusShelter"].Value;


                    OleDbConnection mycon = new OleDbConnection(ForBusInfo.Connect_Sql);
                    mycon.Open();
                    OleDbDataAdapter da;
                    if (ForBusInfo.Connect_Type == 1)
                        da = new OleDbDataAdapter(String.Format("select a.RoadName from sde.公交站线 a inner join sde.RoadAndStation b on (a.OBJECTID = b.RoadID and b.StationID = {0})", eRow.Cells["OBJECTID"].Value), mycon);
                    else
                        da = new OleDbDataAdapter(String.Format("select a.RoadName from 公交站线 a inner join RoadAndStation b on (a.OBJECTID = b.RoadID and b.StationID = {0})", eRow.Cells["OBJECTID"].Value), mycon);
                    DataSet ds = new DataSet();
                    int nQueryCount = da.Fill(ds, "Road");
                    if (nQueryCount > 0)
                    {
                        string strRoad = "";
                        foreach (DataRow eDataRow in ds.Tables["Road"].Rows)
                        {
                            strRoad = strRoad + eDataRow["RoadName"].ToString() + "、";
                        }
                        range1 = worksheet.get_Range(string.Format("{0}{1}", strAB, 4 + (nTotal / 2) * 8), string.Format("{0}{1}", strAB, 4 + (nTotal / 2) * 8));
                        range1.Value2 = string.Format("总经过线路({0}条)：{1}", nQueryCount, strRoad);
                    }

                    worksheet.PageSetup.PrintArea = string.Format("$A$1:$B${0}", 8 + (nTotal / 2) * 8);
                    nTotal++;
                    mycon.Close();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
