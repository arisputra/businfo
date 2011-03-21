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
        public List<IFeature> m_featureCollection = new List<IFeature>();  //�õ�����ѡ�е�feature
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
                //this.����վ��TableAdapter.Fill(this.roadDataSet.����վ��);
            }
            else
            {
                ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Road_FillByStationName, string.Format(" WHERE (RoadName LIKE '%{0}%')", TextBox1.Text));
                //this.����վ��TableAdapter.FillByRoadName(this.roadDataSet.����վ��, "%" + TextBox1.Text + "%");
            }
        }

        private void frmRoadPane_Load(object sender, EventArgs e)
        {
            RefreshGrid();
            //this.����վ��TableAdapter.Fill(this.roadDataSet.����վ��);
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
                ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Road_FillByOBJECTID, string.Format(" WHERE (OBJECTID IN ({0}))", strInPara.Substring(0, strInPara.Length - 1)));
                //this.����վ��TableAdapter.FillByINOBJECTID(this.roadDataSet.����վ��, strInPara);
            }
        }

        public void RefreshGrid()
        {
            ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Road_FillPan, "");
            //this.����վ��TableAdapter.Fill(this.roadDataSet.����վ��);
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
                m_pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusRoad, "OBJECTID", DataGridView1.Rows[m_nCurRowIndex].Cells[1].Value.ToString());
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

        private void ��λ��ToolStripMenuItem_Click(object sender, EventArgs e)
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
            }
        }

        private void ɾ����·ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridView1.EndEdit();
            IFeature pCurFeature;
            bool bCheck = false;
            for (int i = DataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                if (DataGridView1.Rows[i].Cells[0].Value != null && (bool)DataGridView1.Rows[i].Cells[0].Value == true)
                {
                    if (MessageBox.Show(string.Format("ȷ��ɾ����·��{0}!", DataGridView1.Rows[i].Cells[3].Value.ToString()), "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        String sConn = "Provider=sqloledb;Data Source = 172.16.34.120;Initial Catalog=sde;User Id = sa;Password = sa";
                        //String sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + ForBusInfo.GetProfileString("Businfo", "DataPos", Winapp.StartupPath + "\\Businfo.ini") + "\\data\\����.mdb";
                        OleDbConnection mycon = new OleDbConnection(sConn);
                        mycon.Open();
                        OleDbDataAdapter da = ForBusInfo.CreateCustomerAdapter(mycon, "",
                           "", String.Format("delete from  sde.RoadAndStation where RoadID = {0}", DataGridView1.Rows[i].Cells[1].Value.ToString()));
                        da.DeleteCommand.ExecuteNonQuery();//ɾ������վ��
                        pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusRoad, "OBJECTID", DataGridView1.Rows[i].Cells[1].Value.ToString());
                        pCurFeature.Delete();//ɾ����·����
                        ForBusInfo.Add_Log(ForBusInfo.Login_name, "ɾ����·", DataGridView1.Rows[i].Cells[3].Value.ToString(), "");
                        DataGridView1.Rows.RemoveAt(DataGridView1.Rows[i].Index);//ɾ������е���ʾ
                        mycon.Close();
                    }
                    bCheck = true;
                }
            }
            if (!bCheck & m_pCurFeature != null)
            {
                if (MessageBox.Show(string.Format("ȷ��ɾ����·��{0}!", DataGridView1.Rows[m_nCurRowIndex].Cells[3].Value.ToString()), "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    String sConn = "Provider=sqloledb;Data Source = 172.16.34.120;Initial Catalog=sde;User Id = sa;Password = sa";
                    //String sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + ForBusInfo.GetProfileString("Businfo", "DataPos", Winapp.StartupPath + "\\Businfo.ini") + "\\data\\����.mdb";
                    OleDbConnection mycon = new OleDbConnection(sConn);
                    mycon.Open();
                    OleDbDataAdapter da = ForBusInfo.CreateCustomerAdapter(mycon, "",
                       "", String.Format("delete from  sde.RoadAndStation where RoadID = {0}", DataGridView1.Rows[m_nCurRowIndex].Cells[1].Value.ToString()));
                    da.DeleteCommand.ExecuteNonQuery();
                    m_pCurFeature.Delete();
                    ForBusInfo.Add_Log(ForBusInfo.Login_name, "ɾ����·", DataGridView1.Rows[m_nCurRowIndex].Cells[3].Value.ToString(), "");
                    DataGridView1.Rows.RemoveAt(m_nCurRowIndex);
                    mycon.Close();
                }
            }
            EngineFuntions.m_AxMapControl.ActiveView.Refresh();
        }

        private void ���Ա༭ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void ����վ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_pCurFeature != null)
            {
                String sConn = "Provider=sqloledb;Data Source = 172.16.34.120;Initial Catalog=sde;User Id = sa;Password = sa";
                //String sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + ForBusInfo.GetProfileString("Businfo", "DataPos", Winapp.StartupPath + "\\Businfo.ini") + "\\data\\����.mdb";
                OleDbConnection mycon = new OleDbConnection(sConn);
                mycon.Open();
                try
                {
                    sConn = String.Format("select StationID,StationOrder,BufferLength from  sde.RoadAndStation where RoadID = {0} Order by StationOrder", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID")));
                    OleDbDataAdapter da = new OleDbDataAdapter(sConn, mycon);
                    DataSet ds = new DataSet();
                    int nQueryCount = da.Fill(ds);
                    if (nQueryCount > 0)
                    {
                        frmEditRoadAndStation frmPopup = new frmEditRoadAndStation();
                        foreach (DataRow eRow in ds.Tables[0].Rows)
                        {
                            frmPopup.m_CurStationList.Add(EngineFuntions.GetOneSeartchFeature(EngineFuntions.m_Layer_BusStation, "OBJECTID = " + eRow[0].ToString()));
                            frmPopup.n_nBufferLength = (int)eRow[2];
                        }
                        frmPopup.m_pCurFeature = m_pCurFeature;
                        EngineFuntions.m_AxMapControl.ActiveView.GraphicsContainer.DeleteAllElements();
                        object Missing = Type.Missing;
                        IConstructCurve mycurve = new PolylineClass();
                        mycurve.ConstructOffset((IPolycurve)m_pCurFeature.Shape, frmPopup.n_nBufferLength, ref Missing, ref Missing);
                        IPolygon pPolygon;
                        EngineFuntions.m_Layer_BusStation.Selectable = true;
                        pPolygon = (IPolygon)EngineFuntions.ClickSel((IGeometry)mycurve, false, false, 35);
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
                    else
                    {
                        EngineFuntions.m_AxMapControl.ActiveView.GraphicsContainer.DeleteAllElements();
                        object Missing = Type.Missing;
                        IConstructCurve mycurve = new PolylineClass();
                        mycurve.ConstructOffset((IPolycurve)m_pCurFeature.Shape, 35, ref Missing, ref Missing);
                        IPolygon pPolygon;
                        EngineFuntions.m_Layer_BusStation.Selectable = true;
                        pPolygon = (IPolygon)EngineFuntions.ClickSel((IGeometry)mycurve, false, false, 35);
                        EngineFuntions.AddPolygonElement(pPolygon);
                        if (EngineFuntions.GetSeledFeatures(EngineFuntions.m_Layer_BusStation, ref m_featureCollection))
                        {
                            frmRoadAndStation frmPopup = new frmRoadAndStation();
                            frmPopup.m_featureCollection = m_featureCollection;
                            frmPopup.m_pCurFeature = m_pCurFeature;
                            frmPopup.Show();
                        }
                    }
                    mycon.Close();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("���ɹ��������\n" + ex.ToString(), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ��ʾվ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_pCurFeature != null)
            {
                String sConn = "Provider=sqloledb;Data Source = 172.16.34.120;Initial Catalog=sde;User Id = sa;Password = sa";
                //String sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + ForBusInfo.GetProfileString("Businfo", "DataPos", Winapp.StartupPath + "\\Businfo.ini") + "\\data\\����.mdb";
                OleDbConnection mycon = new OleDbConnection(sConn);
                mycon.Open();
                try
                {
                    //sConn = String.Format("select a.* from ����վ�� a inner join RoadAndStation b on (a.OBJECTID = b.StationID and b.RoadID = {0})", m_pCurFeature.Value(m_pCurFeature.Fields.FindField("OBJECTID")))
                    sConn = String.Format("select StationID,StationOrder from  sde.RoadAndStation where RoadID = {0} Order by StationOrder", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID")));
                    OleDbDataAdapter da = new OleDbDataAdapter(sConn, mycon);
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
                        frmPopup.Show();
                        frmPopup.RefreshStationGrid(strInPara); //������Ժ�Gridview��ֵ����Ҫ�����
                        frmPopup.SetStationOrderCell(nList); //��д����ֵ��ʹ�õ�վ�����ֶ�
                        frmPopup.SetSortColumn(2); //��վ����������
                    }
                    mycon.Close();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("���ɹ��������\n" + ex.ToString(), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_featureCollection.Clear();
            DataGridView1.EndEdit();
            List<IFeature> pCurFeatureList;
            bool bCheck = false;
            foreach (DataGridViewRow eRow in DataGridView1.Rows)//�ж��Ƿ�򹳽��ж�ѡ��
            {
                if (eRow.Cells[0].Value != null && (bool)eRow.Cells[0].Value == true)
                {
                    pCurFeatureList = EngineFuntions.GetSeartchFeatures(EngineFuntions.m_Layer_BusRoad, string.Format("RoadName = '{0}'", eRow.Cells[3].Value));
                    int i = 0, j = 0;
                    Excelapp app = new Excelapp();
                    if (app == null)
                    {
                        MessageBox.Show("����Excel����ʧ��!\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    app.Visible = true;

                    Workbooks workbooks = app.Workbooks;
                    _Workbook workbook = workbooks.Open(ForBusInfo.GetProfileString("Businfo", "DataPos", Winapp.StartupPath + "\\Businfo.ini") + "\\data\\������.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    Sheets sheets = workbook.Worksheets;
                    _Worksheet worksheet = (_Worksheet)sheets.get_Item(1);
                    Range range1 = worksheet.get_Range("A1", "G3");
                    if (range1 == null)
                    {
                        MessageBox.Show("��������ʧ��!\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    range1.Value2 = string.Format("������{0}·{1}", pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("RoadName")), pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("Company")));
                    range1 = worksheet.get_Range("A4", "A5");
                    range1.Value2 = pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("RoadType"));
                    range1 = worksheet.get_Range("E4", "E4");
                    range1.Value2 = "��վ���ࣺ" + pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("FirstStartTime"));
                    range1 = worksheet.get_Range("G4", "G4");
                    range1.Value2 = "��վ�հࣺ" + pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("FirstCloseTime"));
                    range1 = worksheet.get_Range("E5", "E5");
                    range1.Value2 = "ĩվ���ࣺ" + pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("EndStartTime"));
                    range1 = worksheet.get_Range("G5", "G5");
                    range1.Value2 = "ĩվ�հࣺ" + pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("EndCloseTim"));
                    //��ʼ����վ��,ȥ�С�����վ��ֱ��ȡ�б�ȥ�С����п��ܲ�һ����
                    String sConn = "Provider=sqloledb;Data Source = 172.16.34.120;Initial Catalog=sde;User Id = sa;Password = sa";
                    //String sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + ForBusInfo.GetProfileString("Businfo", "DataPos", Winapp.StartupPath + "\\Businfo.ini") + "\\data\\����.mdb";
                    OleDbConnection mycon = new OleDbConnection(sConn);
                    mycon.Open();
                    try
                    {
                        foreach (IFeature pCurFeature in pCurFeatureList)
                        {
                            if (pCurFeature.get_Value(pCurFeature.Fields.FindField("RoadTravel")).ToString() == "ȥ��")
                            {
                                sConn = String.Format("select a.* from sde.����վ�� a inner join sde.RoadAndStation b on (a.OBJECTID = b.StationID and b.RoadID = {0}) Order by b.StationOrder", pCurFeature.get_Value(pCurFeature.Fields.FindField("OBJECTID")));
                                OleDbDataAdapter da = new OleDbDataAdapter(sConn, mycon);
                                DataSet ds = new DataSet();
                                int nQueryCount = da.Fill(ds);
                                if (nQueryCount > 0)
                                {
                                    i = 0;
                                    range1 = worksheet.get_Range("D4", "D4");//������վվ��
                                    range1.Value2 = ds.Tables[0].Rows[0][2];
                                    foreach (DataRow eTableRow in ds.Tables[0].Rows)
                                    {
                                        range1 = worksheet.get_Range(string.Format("B{0}", 8 + (2 * i)), string.Format("B{0}", 8 + (2 * i)));
                                        range1.Value2 = eTableRow[2];
                                        range1 = worksheet.get_Range(string.Format("B{0}", 9 + (2 * i)), string.Format("B{0}", 9 + (2 * i)));
                                        range1.Value2 = eTableRow[4];
                                        range1 = worksheet.get_Range(string.Format("C{0}", 8 + (2 * i)), string.Format("C{0}", 9 + (2 * i)));
                                        range1.Value2 = eTableRow[13];
                                        range1 = worksheet.get_Range(string.Format("D{0}", 8 + (2 * i)), string.Format("D{0}", 9 + (2 * i++)));
                                        range1.Value2 = eTableRow[6];
                                    }

                                }
                            }
                            if (pCurFeature.get_Value(pCurFeature.Fields.FindField("RoadTravel")).ToString() == "����")
                            {
                                sConn = String.Format("select a.* from sde.����վ�� a inner join sde.RoadAndStation b on (a.OBJECTID = b.StationID and b.RoadID = {0}) Order by b.StationOrder", pCurFeature.get_Value(pCurFeature.Fields.FindField("OBJECTID")));
                                OleDbDataAdapter da = new OleDbDataAdapter(sConn, mycon);
                                DataSet ds = new DataSet();
                                int nQueryCount = da.Fill(ds);
                                if (nQueryCount > 0)
                                {
                                    j = 0;
                                    range1 = worksheet.get_Range("D5", "D5");//����ĩվվ��
                                    range1.Value2 = ds.Tables[0].Rows[0][2];
                                    foreach (DataRow eTableRow in ds.Tables[0].Rows)
                                    {
                                        range1 = worksheet.get_Range(string.Format("E{0}", 8 + (2 * j)), string.Format("E{0}", 8 + (2 * j)));
                                        range1.Value2 = eTableRow[2];
                                        range1 = worksheet.get_Range(string.Format("E{0}", 9 + (2 * j)), string.Format("E{0}", 9 + (2 * j)));
                                        range1.Value2 = eTableRow[4];
                                        range1 = worksheet.get_Range(string.Format("F{0}", 8 + (2 * j)), string.Format("F{0}", 9 + (2 * j)));
                                        range1.Value2 = eTableRow[13];
                                        range1 = worksheet.get_Range(string.Format("G{0}", 8 + (2 * j)), string.Format("G{0}", 9 + (2 * j++)));
                                        range1.Value2 = eTableRow[6];
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
                        MessageBox.Show("���ɹ��������\n" + ex.ToString(), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    bCheck = true;
                }
            }

            if (!bCheck)//�Ҽ�ֱ��ѡ�������һ��һ����
            {
                if (m_pCurFeature != null)
                {
                    pCurFeatureList = EngineFuntions.GetSeartchFeatures(EngineFuntions.m_Layer_BusRoad, string.Format("RoadName = '{0}'", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadName"))));
                    int i = 0, j = 0;
                    Excelapp app = new Excelapp();
                    if (app == null)
                    {
                        MessageBox.Show("����Excel����ʧ��!\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    app.Visible = true;

                    Workbooks workbooks = app.Workbooks;
                    _Workbook workbook = workbooks.Open(ForBusInfo.GetProfileString("Businfo", "DataPos", Winapp.StartupPath + "\\Businfo.ini") + "\\data\\������.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    Sheets sheets = workbook.Worksheets;
                    _Worksheet worksheet = (_Worksheet)sheets.get_Item(1);
                    Range range1 = worksheet.get_Range("A1", "G3");
                    if (range1 == null)
                    {
                        MessageBox.Show("��������ʧ��!\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    range1.Value2 = string.Format("������{0}·{1}", pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("RoadName")), pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("Company")));
                    range1 = worksheet.get_Range("A4", "A5");
                    range1.Value2 = pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("RoadType"));
                    range1 = worksheet.get_Range("E4", "E4");
                    range1.Value2 = "��վ���ࣺ" + pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("FirstStartTime"));
                    range1 = worksheet.get_Range("G4", "G4");
                    range1.Value2 = "��վ�հࣺ" + pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("FirstCloseTime"));
                    range1 = worksheet.get_Range("E5", "E5");
                    range1.Value2 = "ĩվ���ࣺ" + pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("EndStartTime"));
                    range1 = worksheet.get_Range("G5", "G5");
                    range1.Value2 = "ĩվ�հࣺ" + pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("EndCloseTim"));
                    //��ʼ����վ��,ȥ�С�����վ��ֱ��ȡ�б�ȥ�С����п��ܲ�һ����
                    String sConn = "Provider=sqloledb;Data Source = 172.16.34.120;Initial Catalog=sde;User Id = sa;Password = sa";
                    OleDbConnection mycon = new OleDbConnection(sConn);
                    //sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + ForBusInfo.GetProfileString("Businfo", "DataPos", Winapp.StartupPath + "\\Businfo.ini") + "\\data\\����.mdb";
                    mycon.Open();
                    try
                    {
                        foreach (IFeature pCurFeature in pCurFeatureList)
                        {
                            if (pCurFeature.get_Value(pCurFeature.Fields.FindField("RoadTravel")).ToString() == "ȥ��")
                            {
                                sConn = String.Format("select a.* from sde.����վ�� a inner join sde.RoadAndStation b on (a.OBJECTID = b.StationID and b.RoadID = {0}) Order by b.StationOrder", pCurFeature.get_Value(pCurFeature.Fields.FindField("OBJECTID")));
                                OleDbDataAdapter da = new OleDbDataAdapter(sConn, mycon);
                                DataSet ds = new DataSet();
                                int nQueryCount = da.Fill(ds);
                                if (nQueryCount > 0)
                                {
                                    i = 0;
                                    range1 = worksheet.get_Range("D4", "D4");
                                    range1.Value2 = ds.Tables[0].Rows[0][2];
                                    range1 = worksheet.get_Range("D5", "D5");
                                    range1.Value2 = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][2];
                                    foreach (DataRow eTableRow in ds.Tables[0].Rows)
                                    {
                                        range1 = worksheet.get_Range(string.Format("B{0}", 8 + (2 * i)), string.Format("B{0}", 8 + (2 * i)));
                                        range1.Value2 = eTableRow[2];
                                        range1 = worksheet.get_Range(string.Format("B{0}", 9 + (2 * i)), string.Format("B{0}", 9 + (2 * i)));
                                        range1.Value2 = eTableRow[4];
                                        range1 = worksheet.get_Range(string.Format("C{0}", 8 + (2 * i)), string.Format("C{0}", 9 + (2 * i)));
                                        range1.Value2 = eTableRow[13];
                                        range1 = worksheet.get_Range(string.Format("D{0}", 8 + (2 * i)), string.Format("D{0}", 9 + (2 * i++)));
                                        range1.Value2 = eTableRow[6];
                                    }

                                }
                            }
                            if (pCurFeature.get_Value(pCurFeature.Fields.FindField("RoadTravel")).ToString() == "����")
                            {
                                sConn = String.Format("select a.* from sde.����վ�� a inner join sde.RoadAndStation b on (a.OBJECTID = b.StationID and b.RoadID = {0}) Order by b.StationOrder", pCurFeature.get_Value(pCurFeature.Fields.FindField("OBJECTID")));
                                OleDbDataAdapter da = new OleDbDataAdapter(sConn, mycon);
                                DataSet ds = new DataSet();
                                int nQueryCount = da.Fill(ds);
                                if (nQueryCount > 0)
                                {
                                    j = 0;
                                    range1 = worksheet.get_Range("D4", "D4");
                                    range1.Value2 = ds.Tables[0].Rows[0][2];
                                    range1 = worksheet.get_Range("D5", "D5");
                                    range1.Value2 = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][2];
                                    foreach (DataRow eTableRow in ds.Tables[0].Rows)
                                    {
                                        range1 = worksheet.get_Range(string.Format("E{0}", 8 + (2 * j)), string.Format("E{0}", 8 + (2 * j)));
                                        range1.Value2 = eTableRow[2];
                                        range1 = worksheet.get_Range(string.Format("E{0}", 9 + (2 * j)), string.Format("E{0}", 9 + (2 * j)));
                                        range1.Value2 = eTableRow[4];
                                        range1 = worksheet.get_Range(string.Format("F{0}", 8 + (2 * j)), string.Format("F{0}", 9 + (2 * j)));
                                        range1.Value2 = eTableRow[13];
                                        range1 = worksheet.get_Range(string.Format("G{0}", 8 + (2 * j)), string.Format("G{0}", 9 + (2 * j++)));
                                        range1.Value2 = eTableRow[6];
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
                        MessageBox.Show("���ɹ��������\n" + ex.ToString(), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void ������·ToolStripMenuItem_Click(object sender, EventArgs e)
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
                        MessageBox.Show("��ʱͼ���Ѿ����ڸ���·\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                    m_pCurFeature = EngineFuntions.GetOneSeartchFeature(EngineFuntions.m_Layer_BusRoad, "OBJECTID = " + eRow.Cells[1].Value.ToString());
                    IFeature pFeature = EngineFuntions.CopyFeature(EngineFuntions.m_Layer_BackRoad, m_pCurFeature);
                    String sConn = "Provider=sqloledb;Data Source = 172.16.34.120;Initial Catalog=sde;User Id = sa;Password = sa";
                    //String sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + ForBusInfo.GetProfileString("Businfo", "DataPos", Winapp.StartupPath + "\\Businfo.ini") + "\\data\\����.mdb";
                    OleDbConnection mycon = new OleDbConnection(sConn);
                    mycon.Open();
                    try
                    {
                        OleDbDataAdapter da = ForBusInfo.CreateCustomerAdapter(mycon, String.Format("select * from  sde.RoadAndStation where RoadID = {0}", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"))),
                           "", String.Format("delete from  sde.RoadAndStation where RoadID = {0}", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"))));
                        da.SelectCommand.ExecuteNonQuery();
                        DataSet ds = new DataSet();
                        int nQueryCount = da.Fill(ds);
                        foreach (DataRow eDataRow in ds.Tables[0].Rows)
                        {
                            da.InsertCommand.CommandText = String.Format("insert into sde.BackRAndS(RoadID,StationID,StationOrder,BufferLength) values({0},{1},{2},{3})"
                           , pFeature.get_Value(pFeature.Fields.FindField("OBJECTID")), eDataRow[2], eDataRow[3], eDataRow[4]);
                            da.InsertCommand.ExecuteNonQuery();
                        }
                        da.DeleteCommand.ExecuteNonQuery();//ɾ��ԭʼվ�߹���վ������
                        ForBusInfo.Add_Log(ForBusInfo.Login_name, "������·", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadName")).ToString(), "");
                        mycon.Close();
                        //m_pCurFeature.Delete();//ɾ��ԭʼվ��
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("���ɹ��������\n" + ex.ToString(), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    EngineFuntions.PartialRefresh(EngineFuntions.m_Layer_BackRoad);
                }
            }
            //for (int i = DataGridView1.Rows.Count - 1; i >= 0; i--)//�б���ɾ���Ѿ����ݵ�վ�ߵ���ʾ��
            //{
            //    if (DataGridView1.Rows[i].Cells[0].Value != null && (bool)DataGridView1.Rows[i].Cells[0].Value == true)
            //    {
            //       DataGridView1.Rows.RemoveAt(DataGridView1.Rows[i].Index);
            //    }
            //}
            if (!bCheck)//û��check״̬
            {
                if (m_pCurFeature != null)
                {
                    IFeature pFeature = EngineFuntions.GetOneSeartchFeature(EngineFuntions.m_Layer_BackRoad, string.Format("Roadname = '{0}' AND RoadTravel = '{1}'", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("Roadname")), m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadTravel"))));
                    if (pFeature != null)
                    {
                        MessageBox.Show("��ʱͼ���Ѿ����ڸ���·\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    pFeature = EngineFuntions.CopyFeature(EngineFuntions.m_Layer_BackRoad, m_pCurFeature);//����ͼ�κ����Ե�����ͼ��
                    //��ʼ�ƶ���·��Ӧ��վ������
                    String sConn = "Provider=sqloledb;Data Source = 172.16.34.120;Initial Catalog=sde;User Id = sa;Password = sa";
                    //String sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + ForBusInfo.GetProfileString("Businfo", "DataPos", Winapp.StartupPath + "\\Businfo.ini") + "\\data\\����.mdb";
                    OleDbConnection mycon = new OleDbConnection(sConn);
                    mycon.Open();
                    try
                    {
                        OleDbDataAdapter da = ForBusInfo.CreateCustomerAdapter(mycon, String.Format("select * from  sde.RoadAndStation where RoadID = {0}", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"))),
                            "", String.Format("delete from  sde.RoadAndStation where RoadID = {0}", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"))));
                        da.SelectCommand.ExecuteNonQuery();
                        DataSet ds = new DataSet();
                        int nQueryCount = da.Fill(ds);
                        foreach (DataRow eRow in ds.Tables[0].Rows)
                        {
                            da.InsertCommand.CommandText = String.Format("insert into sde.BackRAndS(RoadID,StationID,StationOrder,BufferLength) values({0},{1},{2},{3})"
                                , pFeature.get_Value(pFeature.Fields.FindField("OBJECTID")), eRow[2], eRow[3], eRow[4]);
                            da.InsertCommand.ExecuteNonQuery();
                        }
                        da.DeleteCommand.ExecuteNonQuery();//ɾ��ԭʼվ�߹���վ������
                        ForBusInfo.Add_Log(ForBusInfo.Login_name, "����վ��", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadName")).ToString(), "");
                        mycon.Close();
                        //m_pCurFeature.Delete();//ɾ��ԭʼվ��
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("���ɹ��������\n" + ex.ToString(), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    EngineFuntions.PartialRefresh(EngineFuntions.m_Layer_BackRoad);
                    //DataGridView1.Rows.RemoveAt(m_nCurRowIndex);
                }
            }
        }

        private void ���ɷ�����·ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool bCheck = false;
            string strDirect;
            foreach (DataGridViewRow eRow in DataGridView1.Rows)
            {
                if (eRow.Cells[0].Value != null && (bool)eRow.Cells[0].Value == true)
                {
                    bCheck = true;
                    if (eRow.Cells[6].Value.ToString() == "ȥ��")
                        strDirect = "����";
                    else
                        strDirect = "ȥ��";

                    m_pCurFeature = EngineFuntions.GetOneSeartchFeature(EngineFuntions.m_Layer_BusRoad, string.Format("Roadname = '{0}' AND RoadTravel = '{1}'", eRow.Cells[4].Value.ToString(), strDirect));
                    if (m_pCurFeature != null)
                    {
                        MessageBox.Show("��·ͼ���Ѿ����ڷ�����·\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                    m_pCurFeature = EngineFuntions.GetOneSeartchFeature(EngineFuntions.m_Layer_BusRoad, string.Format("Roadname = '{0}' AND RoadTravel = '{1}'", eRow.Cells[4].Value.ToString(), eRow.Cells[6].Value.ToString()));
                    IPolyline pPLine = m_pCurFeature.ShapeCopy as IPolyline;
                    pPLine.ReverseOrientation();
                    m_pCurFeature.Shape = pPLine;
                    m_pCurFeature.set_Value(m_pCurFeature.Fields.FindField("RoadTravel"), strDirect);
                    EngineFuntions.CopyFeature(EngineFuntions.m_Layer_BusRoad, m_pCurFeature);
                    System.Threading.Thread.Sleep(1000);
                    MessageBox.Show("������·�����ɣ������վ�㣡\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (!bCheck)//û��check״̬
            {
                if (m_pCurFeature != null)
                {
                    if (m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadTravel")).ToString() == "ȥ��")
                        strDirect = "����";
                    else
                        strDirect = "ȥ��";
                    IFeature pFeature = EngineFuntions.GetOneSeartchFeature(EngineFuntions.m_Layer_BusRoad, string.Format("Roadname = '{0}' AND RoadTravel = '{1}'", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("Roadname")).ToString(), strDirect));
                    if (pFeature != null)
                    {
                        MessageBox.Show("��·ͼ���Ѿ����ڷ�����·\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    IPolyline pPLine = m_pCurFeature.ShapeCopy as IPolyline;
                    pPLine.ReverseOrientation();
                    m_pCurFeature.Shape = pPLine;
                    m_pCurFeature.set_Value(m_pCurFeature.Fields.FindField("RoadTravel"), strDirect);
                    EngineFuntions.CopyFeature(EngineFuntions.m_Layer_BusRoad, m_pCurFeature);
                    System.Threading.Thread.Sleep(1000);
                    MessageBox.Show("������·�����ɣ������վ�㣡\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            RefreshGrid();
            EngineFuntions.PartialRefresh(EngineFuntions.m_Layer_BackRoad);
        }
    }
}
