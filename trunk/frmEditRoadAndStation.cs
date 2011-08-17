using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Businfo.Globe;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System.Data.OleDb;

namespace Businfo
{
    public partial class frmEditRoadAndStation : Form
    {
        public List<IFeature> m_CurStationList = new List<IFeature>();//已关联站点
        public List<IFeature> m_SelStationList = new List<IFeature>();//缓冲区内未关联站点
        public int m_nRoadID,n_nBufferLength;
        public IFeature m_pCurFeature;
        public bool m_bReverse;//是不是反向的队列
        public bool m_bNew = true;//是不是数据库中没有关联过站点的。
        public List<BusStation> m_BusStationList = new List<BusStation>();
        public List<BusStation> m_SelBusStationList = new List<BusStation>();

        public frmEditRoadAndStation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_BusStationList.Clear();

            for (int i = dataGridView1.SelectedRows.Count; i > 0; i--)
            {
                m_SelBusStationList.Add(new BusStation(dataGridView1.SelectedRows[i - 1].Cells[2].Value.ToString(), dataGridView1.SelectedRows[i - 1].Cells[4].Value.ToString(), (int)dataGridView1.SelectedRows[i - 1].Cells[0].Value, (double)dataGridView1.SelectedRows[i-1].Cells[1].Value, dataGridView1.SelectedRows[i - 1].Cells[3].Value.ToString(), dataGridView1.SelectedRows[i - 1].Cells[5].Value.ToString()));
                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[i - 1]);
            }
        
            foreach (DataGridViewRow eRow in dataGridView1.Rows)
            {
                m_BusStationList.Add(new BusStation(eRow.Cells[2].Value.ToString(), eRow.Cells[4].Value.ToString(), (int)eRow.Cells[0].Value, (double)eRow.Cells[1].Value, eRow.Cells[3].Value.ToString(), eRow.Cells[5].Value.ToString()));
            }

            m_SelBusStationList.Sort();
            if (m_bReverse)
                m_SelBusStationList.Reverse();

            dataGridView2.Rows.Clear();
            foreach (BusStation pItem in m_SelBusStationList)
            {
                dataGridView2.Rows.Add(pItem.ID, pItem.rLength, pItem.StationName, pItem.StationExplain, pItem.Direct, pItem.StationCharacter);
            }

            ForBusInfo.SetRowNo(dataGridView1);
            ForBusInfo.SetRowNo(dataGridView2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            m_SelBusStationList.Clear();

            for (int i = dataGridView2.SelectedRows.Count; i > 0; i--)
            {
                m_BusStationList.Add(new BusStation(dataGridView2.SelectedRows[i - 1].Cells[2].Value.ToString(), dataGridView2.SelectedRows[i - 1].Cells[4].Value.ToString(), (int)dataGridView2.SelectedRows[i - 1].Cells[0].Value, (double)dataGridView2.SelectedRows[i - 1].Cells[1].Value, dataGridView2.SelectedRows[i - 1].Cells[3].Value.ToString(), dataGridView2.SelectedRows[i - 1].Cells[5].Value.ToString()));
                dataGridView2.Rows.Remove(dataGridView2.SelectedRows[i - 1]);
            }

            foreach (DataGridViewRow eRow in dataGridView2.Rows)
            {
                m_SelBusStationList.Add(new BusStation(eRow.Cells[2].Value.ToString(), eRow.Cells[4].Value.ToString(), (int)eRow.Cells[0].Value, (double)eRow.Cells[1].Value, eRow.Cells[3].Value.ToString(), eRow.Cells[5].Value.ToString()));
            }

            
            m_BusStationList.Sort();
            if (m_bReverse)
                m_BusStationList.Reverse();

            dataGridView1.Rows.Clear();
            foreach (BusStation pItem in m_BusStationList)
            {
                dataGridView1.Rows.Add(pItem.ID, pItem.rLength, pItem.StationName, pItem.StationExplain, pItem.Direct, pItem.StationCharacter);
            }

            ForBusInfo.SetRowNo(dataGridView1);
            ForBusInfo.SetRowNo(dataGridView2);
        }

        private void frmEditRoadAndStation_Load(object sender, EventArgs e)
        {
            label3.Text = m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadName")).ToString() + m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadTravel")).ToString();
            m_BusStationList.Clear();
            m_nRoadID = (int)m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"));
            IPolyline pPLine = m_pCurFeature.ShapeCopy as IPolyline;
            IPoint outPoint = new PointClass();
            double distanceAlongCurve = 0;//该点在曲线上最近的点距曲线起点的距离
            double distanceFromCurve = 0;//该点到曲线的直线距离
            bool bRightSide = false;//点在线的左边还是右边
            bool asRatio = false;  //asRatio：byval方式，bool类型，表示上面两个参数给定的长度是以绝对距离的方式给出还是以占曲线总长度的比例的方式给出
            foreach (IFeature pfeat in m_CurStationList)
            {
                pPLine.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pfeat.ShapeCopy as IPoint, asRatio, outPoint, ref distanceAlongCurve, ref distanceFromCurve, ref bRightSide);
                m_BusStationList.Add(new BusStation(pfeat.get_Value(pfeat.Fields.FindField("StationName")).ToString(), pfeat.get_Value(pfeat.Fields.FindField("Direct")).ToString(), (int)pfeat.get_Value(pfeat.Fields.FindField("OBJECTID")), distanceAlongCurve, pfeat.get_Value(pfeat.Fields.FindField("DispatchStationThird")).ToString(), pfeat.get_Value(pfeat.Fields.FindField("StationCharacter")).ToString()));
                EngineFuntions.AddTextElement(pfeat.Shape, pfeat.get_Value(pfeat.Fields.FindField("StationName")).ToString());
            }
            if(m_bNew)
            m_BusStationList.Sort();
            foreach (BusStation pItem in m_BusStationList)
            {

                dataGridView1.Rows.Add(pItem.ID,pItem.rLength, pItem.StationName, pItem.StationExplain, pItem.Direct, pItem.StationCharacter);

              //  checkedListBox1.Items.Add(pItem);
            }

            if (m_BusStationList[0].rLength > m_BusStationList[1].rLength)
                m_bReverse = true;
            else
                m_bReverse = false;

            foreach (IFeature pfeat in m_SelStationList)
            {
                pPLine.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pfeat.ShapeCopy as IPoint, asRatio, outPoint, ref distanceAlongCurve, ref distanceFromCurve, ref bRightSide);
                m_SelBusStationList.Add(new BusStation(pfeat.get_Value(pfeat.Fields.FindField("StationName")).ToString(), pfeat.get_Value(pfeat.Fields.FindField("Direct")).ToString(), (int)pfeat.get_Value(pfeat.Fields.FindField("OBJECTID")), distanceAlongCurve, pfeat.get_Value(pfeat.Fields.FindField("DispatchStationThird")).ToString(), pfeat.get_Value(pfeat.Fields.FindField("StationCharacter")).ToString()));
                EngineFuntions.AddTextElement(pfeat.Shape, pfeat.get_Value(pfeat.Fields.FindField("StationName")).ToString());
            }
            m_SelBusStationList.Sort();
            if (m_bReverse)//是不是反向的队列
                m_SelBusStationList.Reverse();
            foreach (BusStation pItem in m_SelBusStationList)
            {
                dataGridView2.Rows.Add(pItem.ID, pItem.rLength, pItem.StationName, pItem.StationExplain, pItem.Direct, pItem.StationCharacter);
                checkedListBox2.Items.Add(pItem);
            }
            
            if (m_bNew == false)
            {
                checkBox1.Visible = false;
            }
            ForBusInfo.SetRowNo(dataGridView1);
            ForBusInfo.SetRowNo(dataGridView2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OleDbConnection mycon = new OleDbConnection(ForBusInfo.Connect_Sql);
            mycon.Open();
            try
            {
                int nOrder = 0;
                string pStrSQL;
                OleDbCommand pCom;
                if (ForBusInfo.Connect_Type == 1)
                    pStrSQL = String.Format("delete from  sde.RoadAndStation where RoadID = {0}", m_nRoadID);
                else
                    pStrSQL = String.Format("delete from  RoadAndStation where RoadID = {0}", m_nRoadID);
                pCom = new OleDbCommand(pStrSQL, mycon);
                pCom.ExecuteNonQuery();
                foreach (DataGridViewRow eRow in dataGridView1.Rows)
                {
                    if (ForBusInfo.Connect_Type == 1)
                        pStrSQL = String.Format("insert into sde.RoadAndStation(RoadID,StationID,StationOrder,BufferLength,Roadinfo ,StationInfo) values({0},{1},{2},{3},'{4}','{5}')"
                        , m_nRoadID, eRow.Cells[0].Value, nOrder++, n_nBufferLength
                        , String.Format("{0}-{1}",m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadName")).ToString(),m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadTravel")).ToString())
                        , String.Format("{0}-{1}", eRow.Cells[2].Value, eRow.Cells[4].Value));
                    else
                        pStrSQL = String.Format("insert into RoadAndStation(RoadID,StationID,StationOrder,BufferLength,Roadinfo ,StationInfo) values({0},{1},{2},{3},'{4}','{5}')"
                        , m_nRoadID, eRow.Cells[0].Value, nOrder++, n_nBufferLength
                         , String.Format("{0}-{1}", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadName")).ToString(), m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadTravel")).ToString())
                        , String.Format("{0}-{1}", eRow.Cells[2].Value, eRow.Cells[4].Value));
                    pCom = new OleDbCommand(pStrSQL, mycon);
                    pCom.ExecuteNonQuery();
                }
                ForBusInfo.Add_Log(ForBusInfo.Login_name, "编辑线路关联站点", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadName")).ToString(), "");
                mycon.Close();
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("生成关联表出错\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            m_CurStationList.Clear();
            EngineFuntions.m_AxMapControl.ActiveView.GraphicsContainer.DeleteAllElements();
            //平移
            IConstructCurve mycurve = new PolylineClass();
            IPolygon pPolygon;
            EngineFuntions.m_Layer_BusStation.Selectable = true;
            object Missing = Type.Missing;

            if (checkBox1.Checked)
            {
                mycurve.ConstructOffset((IPolycurve)m_pCurFeature.Shape, -25, ref Missing, ref Missing);
                pPolygon = (IPolygon)EngineFuntions.ClickSel((IGeometry)mycurve, false, false, 25);
                n_nBufferLength = -25;
            }
            else
            {
                mycurve.ConstructOffset((IPolycurve)m_pCurFeature.Shape, 25, ref Missing, ref Missing);
                pPolygon = (IPolygon)EngineFuntions.ClickSel((IGeometry)mycurve, false, false, 25);
                n_nBufferLength = 25;
            }
            EngineFuntions.AddPolygonElement(pPolygon);
            if (EngineFuntions.GetSeledFeatures(EngineFuntions.m_Layer_BusStation, ref m_CurStationList))
            {
                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();
                m_BusStationList.Clear();
                IPolyline pPLine = m_pCurFeature.ShapeCopy as IPolyline;
                ESRI.ArcGIS.Geometry.IPoint outPoint = new PointClass();
                double distanceAlongCurve = 0;//该点在曲线上最近的点距曲线起点的距离
                double distanceFromCurve = 0;//该点到曲线的直线距离
                bool bRightSide = false;//点在线的左边还是右边
                bool asRatio = false;  //asRatio：byval方式，bool类型，表示上面两个参数给定的长度是以绝对距离的方式给出还是以占曲线总长度的比例的方式给出
                foreach (IFeature pfeat in m_CurStationList)
                {
                    pPLine.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pfeat.ShapeCopy as ESRI.ArcGIS.Geometry.IPoint, asRatio, outPoint, ref distanceAlongCurve, ref distanceFromCurve, ref bRightSide);
                    m_BusStationList.Add(new BusStation(pfeat.get_Value(pfeat.Fields.FindField("StationName")).ToString(), pfeat.get_Value(pfeat.Fields.FindField("Direct")).ToString(), (int)pfeat.get_Value(pfeat.Fields.FindField("OBJECTID")), distanceAlongCurve, pfeat.get_Value(pfeat.Fields.FindField("DispatchStationThird")).ToString(), pfeat.get_Value(pfeat.Fields.FindField("StationCharacter")).ToString()));
                    EngineFuntions.AddTextElement(pfeat.Shape, pfeat.get_Value(pfeat.Fields.FindField("StationName")).ToString());
                }
                m_BusStationList.Sort();
                if (checkBox1.Checked)
                {
                    m_BusStationList.Reverse();
                }
                foreach (BusStation pItem in m_BusStationList)
                {
                    dataGridView1.Rows.Add(pItem.ID, pItem.rLength, pItem.StationName, pItem.StationExplain, pItem.Direct, pItem.StationCharacter);
                }

                ForBusInfo.SetRowNo(dataGridView1);
                ForBusInfo.SetRowNo(dataGridView2);
            }
        }

        private void frmEditRoadAndStation_SizeChanged(object sender, EventArgs e)
        {
            groupBox1.SetBounds(groupBox1.Left, groupBox1.Top, button1.Left - groupBox1.Left - 5, groupBox1.Height);
            groupBox2.SetBounds(button1.Right + 5, groupBox2.Top, groupBox1.Width, groupBox1.Height);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OleDbConnection mycon = new OleDbConnection(ForBusInfo.Connect_Sql);
            mycon.Open();
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
                mycon.Close();
                this.Close();
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("清除关联出错\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
