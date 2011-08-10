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
                ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Road_FillByStationName, string.Format(" WHERE (RoadName LIKE '%{0}%')", TextBox1.Text), new string[] { "" });
                //this.����վ��TableAdapter.FillByRoadName(this.roadDataSet.����վ��, "%" + TextBox1.Text + "%");
            }
            int nNum = 1;
            foreach (DataGridViewRow eRow in DataGridView1.Rows)
            {
                eRow.HeaderCell.Value = nNum++.ToString();
            }
        }

        private void frmRoadPane_Load(object sender, EventArgs e)
        {
            RefreshGrid();
            switch (ForBusInfo.Login_name)
            {
                case "վ�����":
                    contextMenuStrip1.Items.Find("ɾ����·ToolStripMenuItem", false)[0].Visible = false;
                    contextMenuStrip1.Items.Find("���Ա༭ToolStripMenuItem", false)[0].Visible = false;
                    contextMenuStrip1.Items.Find("������·ToolStripMenuItem", false)[0].Visible = false;
                    contextMenuStrip1.Items.Find("���ɷ�����·ToolStripMenuItem", false)[0].Visible = false;
                    contextMenuStrip1.Items.Find("����վ��ToolStripMenuItem", false)[0].Visible = true;
                    contextMenuStrip1.Items.Find("��ʾվ��ToolStripMenuItem", false)[0].Visible = true;
                    contextMenuStrip1.Items.Find("������ToolStripMenuItem", false)[0].Visible = true;
                    break;
                case "admin":
                    contextMenuStrip1.Items.Find("����վ��ToolStripMenuItem", false)[0].Visible = true;
                    contextMenuStrip1.Items.Find("��ʾվ��ToolStripMenuItem", false)[0].Visible = true;
                    contextMenuStrip1.Items.Find("������ToolStripMenuItem", false)[0].Visible = true;
                    break;
            }
            int nNum = 1;
            foreach (DataGridViewRow eRow in DataGridView1.Rows)
            {
                eRow.HeaderCell.Value = nNum++.ToString();
            }
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
                ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Road_FillByOBJECTID, string.Format(" WHERE (OBJECTID IN ({0}))", strInPara.Substring(0, strInPara.Length - 1)), new string[] { "" });
                //this.����վ��TableAdapter.FillByINOBJECTID(this.roadDataSet.����վ��, strInPara);
            }
        }

        public void RefreshGrid()
        {
            ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Road_FillPan, "", new string[] { "" });
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
                EngineFuntions.MapRefresh();
                EngineFuntions.m_AxMapControl.Map.ClearSelection();
                EngineFuntions.m_AxMapControl.ActiveView.GraphicsContainer.DeleteAllElements();
                EngineFuntions.m_AxMapControl.Map.SelectFeature(EngineFuntions.m_Layer_BusRoad, m_pCurFeature);
                EngineFuntions.MapRefresh();
                
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
                        OleDbConnection mycon = new OleDbConnection(ForBusInfo.Connect_Sql);
                        mycon.Open();
                        OleDbDataAdapter da;
                        if(ForBusInfo.Connect_Type == 1)
                            da = ForBusInfo.CreateCustomerAdapter(mycon, "",
                           "", String.Format("delete from  sde.RoadAndStation where RoadID = {0}", DataGridView1.Rows[i].Cells[1].Value.ToString()));
                        else
                            da = ForBusInfo.CreateCustomerAdapter(mycon, "",
                           "", String.Format("delete from  RoadAndStation where RoadID = {0}", DataGridView1.Rows[i].Cells[1].Value.ToString()));
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
                    OleDbConnection mycon = new OleDbConnection(ForBusInfo.Connect_Sql);
                    mycon.Open();
                    OleDbDataAdapter da;
                    if (ForBusInfo.Connect_Type == 1)
                        da = ForBusInfo.CreateCustomerAdapter(mycon, "",
                           "", String.Format("delete from  sde.RoadAndStation where RoadID = {0}", DataGridView1.Rows[m_nCurRowIndex].Cells[1].Value.ToString()));
                    else
                        da = ForBusInfo.CreateCustomerAdapter(mycon, "",
                       "", String.Format("delete from  RoadAndStation where RoadID = {0}", DataGridView1.Rows[m_nCurRowIndex].Cells[1].Value.ToString()));
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
                //IFeature FFFF = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusRoad, "OBJECTID","1809");
                ///////////////////////////////////�ֶ� �ı�path///////////////////////////////////////
                IPolyline pPolyl = null;
                IPolyline pPLine = m_pCurFeature.ShapeCopy as IPolyline;
                object Missing1 = Type.Missing;
                IGeometryCollection pGeometryCollection = (IGeometryCollection)pPLine;//���ཻ�ĺϲ��������ཻ����һ�ζε�path��������count�ж��ˡ�
                if (pGeometryCollection.GeometryCount > 1)
                {
                    if (MessageBox.Show("��������ִ�еģ�Ҫע�⣡\n", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                    {

                       //IPolyline pPLine1 = FFFF.ShapeCopy as IPolyline;
                       //IGeometryCollection pGeometryCollection1 = (IGeometryCollection)pPLine1;
                        
                        IGeometryCollection pGeometryCol = new PolylineClass();//�õ�������polyline�������Ϳ������ཻ��
                        IPath pPath = pGeometryCollection.get_Geometry(0) as IPath;//���ǵõ�polyline path�ķ�����
                        IPointCollection pPtColl = new PolylineClass();
                        pPath.ReverseOrientation();
                        //pPtColl.RemovePoints(10, 1);
                        
                        //pPath.FromPoint = pPtColl.get_Point(6);
                        //pPath.ToPoint = pPtColl.get_Point(6);
                        pGeometryCol.AddGeometry(pPath as IGeometry, ref Missing1, ref Missing1);


                        IPath pPath1 = pGeometryCollection.get_Geometry(1) as IPath;
                        //pPath1.ReverseOrientation();
                        pGeometryCol.AddGeometry(pPath1 as IGeometry, ref Missing1, ref Missing1);


                        IPath pPath2 = pGeometryCollection.get_Geometry(2) as IPath;
                        //pPath2.ReverseOrientation();
                        pGeometryCol.AddGeometry(pPath2 as IGeometry, ref Missing1, ref Missing1);


                        //pGeometryCol.AddGeometry(pGeometryCollection.get_Geometry(0), ref Missing1, ref Missing1);
                        //pGeometryCol.AddGeometry(pGeometryCollection.get_Geometry(1), ref Missing1, ref Missing1);

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
                        frmEditRoadAndStation frmPopup = new frmEditRoadAndStation();
                        frmPopup.m_bNew = false;
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
                    else//��һ��ѡ���У�û�й�����վ��ġ�
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
                            //frmRoadAndStation frmPopup = new frmRoadAndStation();//RoadAndStation���ڣ���ʹ���ˣ�ͬ���EditRoadAndStation����m_bNew�����ж��ǲ��ǵ�һ�����
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
                    MessageBox.Show("���ɹ��������\n" + ex.ToString(), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ��ʾվ��ToolStripMenuItem_Click(object sender, EventArgs e)
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
            string strPath = DateTime.Now.ToLongTimeString();
            strPath = strPath.Replace(":", "-");
            strPath = string.Format("D:\\������\\{0}", strPath);
            System.IO.Directory.CreateDirectory(strPath);

            Excelapp app = new Excelapp();
            if (app == null)
            {
                MessageBox.Show("����Excel����ʧ��!\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            app.Visible = true;
            app.DisplayAlerts = false;
            Workbooks workbooks = app.Workbooks;
            _Workbook workbook = workbooks.Open(Winapp.StartupPath + "\\data\\������.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Sheets sheets = workbook.Worksheets;
            _Worksheet worksheet = (_Worksheet)sheets.get_Item(1);

            m_featureCollection.Clear();
            DataGridView1.EndEdit();
            List<IFeature> pCurFeatureList;
            bool bCheck = false;

            List<string> pRoadNames = new List<string>();

            foreach (DataGridViewRow eRow in DataGridView1.Rows)////�ж��Ƿ�򹳽��ж�ѡ��  ɾ���ظ�����·����ȥ�л���ֻ��һ�Σ�
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
            foreach (string eRoadName in pRoadNames)//��ȥ���в��ظ���·����
            {
                pCurFeatureList = EngineFuntions.GetSeartchFeatures(EngineFuntions.m_Layer_BusRoad, string.Format("RoadName = '{0}'", eRoadName));
                    int i = 0, j = 0;

                    SetRoadTableTitle(app, worksheet, pCurFeatureList[0], false);
                    //��ʼ����վ��,ȥ�С�����վ��ֱ��ȡ�б�ȥ�С����п��ܲ�һ����
                    OleDbConnection mycon = new OleDbConnection(ForBusInfo.Connect_Sql);
                    mycon.Open();
                    try
                    {
                        foreach (IFeature pCurFeature in pCurFeatureList)
                        {
                            if (pCurFeature.get_Value(pCurFeature.Fields.FindField("RoadTravel")).ToString() == "ȥ��")
                            {
                                i = SetRoadTableQu(mycon, worksheet, pCurFeature);
                            }
                            if (pCurFeature.get_Value(pCurFeature.Fields.FindField("RoadTravel")).ToString() == "����")
                            {
                                j = SetRoadTableHui(mycon, worksheet, pCurFeature);
                            }
                            if (i > j)//���ô�ӡ����
                            {
                                worksheet.PageSetup.PrintArea = string.Format("$A$1:$K${0}", i * 2 + 7);
                            }
                            else
                            {
                                worksheet.PageSetup.PrintArea = string.Format("$A$1:$K${0}", j * 2 + 7);
                            }
                            // string vFileName = "D:\\������\\" + pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("RoadName")) + ".xls";

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
                        MessageBox.Show("���ɹ��������\n" + ex.ToString(), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    bCheck = true;
            }
           

            if (bCheck)
            {
                System.Diagnostics.Process.Start(strPath);
            }
            else//�Ҽ�ֱ��ѡ�������һ��һ����
            {
                if (m_pCurFeature != null)
                {
                    pCurFeatureList = EngineFuntions.GetSeartchFeatures(EngineFuntions.m_Layer_BusRoad, string.Format("RoadName = '{0}'", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadName"))));
                    int i = 0, j = 0;

                    SetRoadTableTitle(app, worksheet, pCurFeatureList[0], true);
                    //��ʼ����վ��,ȥ�С�����վ��ֱ��ȡ�б�ȥ�С����п��ܲ�һ����
                    OleDbConnection mycon = new OleDbConnection(ForBusInfo.Connect_Sql);
                    mycon.Open();
                    try
                    {
                        foreach (IFeature pCurFeature in pCurFeatureList)
                        {
                            if (pCurFeature.get_Value(pCurFeature.Fields.FindField("RoadTravel")).ToString() == "ȥ��")
                            {
                                i = SetRoadTableQu(mycon, worksheet, pCurFeature);
                            }
                            if (pCurFeature.get_Value(pCurFeature.Fields.FindField("RoadTravel")).ToString() == "����")
                            {

                                j = SetRoadTableHui(mycon, worksheet, pCurFeature);
                            }
                            if (i > j)
                            {
                                worksheet.PageSetup.PrintArea = string.Format("$A$1:$K${0}", i * 2 + 7);
                            }
                            else
                            {
                                worksheet.PageSetup.PrintArea = string.Format("$A$1:$K${0}", j * 2 + 7);
                            }
                            workbook.SaveAs("D:\\������\\" + pCurFeatureList[0].get_Value(pCurFeatureList[0].Fields.FindField("RoadName")), Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, null);
                        }

                        mycon.Close();
                    }
                        
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("���ɹ��������\n" + ex.ToString(), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            app.Quit();
        }

        private int SetRoadTableQu(OleDbConnection mycon, _Worksheet worksheet, IFeature pCurFeature)
        {
            OleDbDataAdapter da;
            if (ForBusInfo.Connect_Type == 1)
                da = new OleDbDataAdapter(String.Format("select a.* from sde.����վ�� a inner join sde.RoadAndStation b on (a.OBJECTID = b.StationID and b.RoadID = {0}) Order by b.StationOrder", pCurFeature.get_Value(pCurFeature.Fields.FindField("OBJECTID"))), mycon);
            else
                da = new OleDbDataAdapter(String.Format("select a.* from ����վ�� a inner join RoadAndStation b on (a.OBJECTID = b.StationID and b.RoadID = {0}) Order by b.StationOrder", pCurFeature.get_Value(pCurFeature.Fields.FindField("OBJECTID"))), mycon);
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
                    range1 = worksheet.get_Range(string.Format("B{0}", 8 + (2 * i)), string.Format("B{0}", 8 + (2 * i)));
                    range1.Value2 = eTableRow["StationName"];
                    range1 = worksheet.get_Range(string.Format("B{0}", 9 + (2 * i)), string.Format("B{0}", 9 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchStationThird"];//վ��˵��
                    range1 = worksheet.get_Range(string.Format("C{0}", 8 + (2 * i)), string.Format("C{0}", 8 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchCompanyFirst"];//��·�Ʋ���
                    range1 = worksheet.get_Range(string.Format("D{0}", 8 + (2 * i)), string.Format("D{0}", 8 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchRouteFirst"];// ��·�Ƴߴ�
                    range1 = worksheet.get_Range(string.Format("E{0}", 8 + (2 * i)), string.Format("E{0}", 8 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchStationFirst"];//��·��������λ
                    range1 = worksheet.get_Range(string.Format("C{0}", 9 + (2 * i)), string.Format("C{0}", 9 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchStationSecond"];//��·�Ʋ���2
                    range1 = worksheet.get_Range(string.Format("D{0}", 9 + (2 * i)), string.Format("D{0}", 9 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchCompanyThird"];//��·�Ƴߴ�2
                    range1 = worksheet.get_Range(string.Format("E{0}", 9 + (2 * i)), string.Format("E{0}", 9 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchRouteThird"];//��·��������λ2
                    range1 = worksheet.get_Range(string.Format("F{0}", 8 + (2 * i)), string.Format("F{0}", 8 + (2 * i)));
                    range1.Value2 = eTableRow["StationCharacter"];//վ�����ڵ�·
                    range1 = worksheet.get_Range(string.Format("F{0}", 9 + (2 * i)), string.Format("F{0}", 9 + (2 * i++)));
                    range1.Value2 = eTableRow["StationAlias"];//��վ��
                }

            }
            return nQueryCount;
        }

        private int SetRoadTableHui(OleDbConnection mycon, _Worksheet worksheet, IFeature pCurFeature)
        {
            OleDbDataAdapter da;
            if (ForBusInfo.Connect_Type == 1)
                da = new OleDbDataAdapter(String.Format("select a.* from sde.����վ�� a inner join sde.RoadAndStation b on (a.OBJECTID = b.StationID and b.RoadID = {0}) Order by b.StationOrder", pCurFeature.get_Value(pCurFeature.Fields.FindField("OBJECTID"))), mycon);
            else
                da = new OleDbDataAdapter(String.Format("select a.* from ����վ�� a inner join RoadAndStation b on (a.OBJECTID = b.StationID and b.RoadID = {0}) Order by b.StationOrder", pCurFeature.get_Value(pCurFeature.Fields.FindField("OBJECTID"))), mycon);
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
                    range1 = worksheet.get_Range(string.Format("G{0}", 8 + (2 * i)), string.Format("G{0}", 8 + (2 * i)));
                    range1.Value2 = eTableRow["StationName"];
                    range1 = worksheet.get_Range(string.Format("G{0}", 9 + (2 * i)), string.Format("G{0}", 9 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchStationThird"];//վ��˵��
                    range1 = worksheet.get_Range(string.Format("H{0}", 8 + (2 * i)), string.Format("H{0}", 8 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchCompanyFirst"];//��·�Ʋ���
                    range1 = worksheet.get_Range(string.Format("I{0}", 8 + (2 * i)), string.Format("I{0}", 8 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchRouteFirst"];// ��·�Ƴߴ�
                    range1 = worksheet.get_Range(string.Format("J{0}", 8 + (2 * i)), string.Format("J{0}", 8 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchStationFirst"];//��·��������λ
                    range1 = worksheet.get_Range(string.Format("H{0}", 9 + (2 * i)), string.Format("H{0}", 9 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchStationSecond"];//��·�Ʋ���2
                    range1 = worksheet.get_Range(string.Format("I{0}", 9 + (2 * i)), string.Format("I{0}", 9 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchCompanyThird"];//��·�Ƴߴ�2
                    range1 = worksheet.get_Range(string.Format("J{0}", 9 + (2 * i)), string.Format("J{0}", 9 + (2 * i)));
                    range1.Value2 = eTableRow["DispatchRouteThird"];//��·��������λ2
                    range1 = worksheet.get_Range(string.Format("K{0}", 8 + (2 * i)), string.Format("K{0}", 8 + (2 * i)));
                    range1.Value2 = eTableRow["StationCharacter"];//վ�����ڵ�·
                    range1 = worksheet.get_Range(string.Format("k{0}", 9 + (2 * i)), string.Format("K{0}", 9 + (2 * i++)));
                    range1.Value2 = eTableRow["StationAlias"];//��վ��
                }

            }
            return nQueryCount;
        }

        private void SetRoadTableTitle(Excelapp app, _Worksheet worksheet, IFeature pCurFeature,bool bVisable)
        {
            app.Visible = bVisable;
            Range range1 = worksheet.get_Range("B8", "K127");
            range1.Cells.ClearContents();//����ǹ̶�������
            range1 = worksheet.get_Range("A1", "I3");
            if (range1 == null)
            {
                MessageBox.Show("��������ʧ��!\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            range1.Value2 = string.Format("������{0}· {1}", pCurFeature.get_Value(pCurFeature.Fields.FindField("RoadName")), pCurFeature.get_Value(pCurFeature.Fields.FindField("Company")));
            range1 = worksheet.get_Range("A4", "A5");
            range1.Value2 = pCurFeature.get_Value(pCurFeature.Fields.FindField("RoadType"));
            range1 = worksheet.get_Range("B4", "C5");
            range1.Value2 = string.Format("Ʊ�ۣ�{0:0.00} {1:0.00} {2:0.00} {3}", pCurFeature.get_Value(pCurFeature.Fields.FindField("TicketPrice1")), pCurFeature.get_Value(pCurFeature.Fields.FindField("TicketPrice2")), pCurFeature.get_Value(pCurFeature.Fields.FindField("TicketPrice3")), pCurFeature.get_Value(pCurFeature.Fields.FindField("Picture5")));
            range1 = worksheet.get_Range("H4", "H4");//��վ����
            range1.Value2 = pCurFeature.get_Value(pCurFeature.Fields.FindField("FirstStartTime"));
            range1 = worksheet.get_Range("K4", "K4");//��վ�հ�
            range1.Value2 = pCurFeature.get_Value(pCurFeature.Fields.FindField("FirstCloseTime"));
            range1 = worksheet.get_Range("H5", "H5");//ĩվ����
            range1.Value2 = pCurFeature.get_Value(pCurFeature.Fields.FindField("EndStartTime"));
            range1 = worksheet.get_Range("K5", "K5");//ĩվ�հ�
            range1.Value2 = pCurFeature.get_Value(pCurFeature.Fields.FindField("EndCloseTim"));
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
                                da.InsertCommand.CommandText = String.Format("insert into sde.BackRAndS(RoadID,StationID,StationOrder,BufferLength) values({0},{1},{2},{3})"
                               , pFeature.get_Value(pFeature.Fields.FindField("OBJECTID")), eDataRow[2], eDataRow[3], eDataRow[4]);
                            else
                                da.InsertCommand.CommandText = String.Format("insert into BackRAndS(RoadID,StationID,StationOrder,BufferLength) values({0},{1},{2},{3})"
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
                                da.InsertCommand.CommandText = String.Format("insert into sde.BackRAndS(RoadID,StationID,StationOrder,BufferLength) values({0},{1},{2},{3})"
                                    , pFeature.get_Value(pFeature.Fields.FindField("OBJECTID")), eRow[2], eRow[3], eRow[4]);
                            else
                                da.InsertCommand.CommandText = String.Format("insert into BackRAndS(RoadID,StationID,StationOrder,BufferLength) values({0},{1},{2},{3})"
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
