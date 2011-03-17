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
        public List<IFeature> m_CurStationList = new List<IFeature>();
        public List<IFeature> m_SelStationList = new List<IFeature>();//站点
        public int m_nRoadID,n_nBufferLength;
        public IFeature m_pCurFeature;
        public bool m_bReverse;//是不是反向的队列
        public List<BusStation> m_BusStationList = new List<BusStation>();
        public List<BusStation> m_SelBusStationList = new List<BusStation>();

        public frmEditRoadAndStation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_BusStationList.Clear();
            for (int i = checkedListBox1.CheckedItems.Count; i > 0; i--)
            {
                m_SelBusStationList.Add(checkedListBox1.CheckedItems[i - 1] as BusStation);
                checkedListBox1.Items.Remove(checkedListBox1.CheckedItems[i - 1]);
            }
            foreach (BusStation pBusStation in checkedListBox1.Items)
            {
                m_BusStationList.Add(pBusStation);
            }
            m_SelBusStationList.Sort();
            if (m_bReverse)
                m_SelBusStationList.Reverse();
            checkedListBox2.Items.Clear();
            foreach (BusStation pItem in m_SelBusStationList)
            {
                checkedListBox2.Items.Add(pItem);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            m_SelBusStationList.Clear();
            for (int i = checkedListBox2.CheckedItems.Count; i > 0; i--)
            {
                m_BusStationList.Add(checkedListBox2.CheckedItems[i - 1] as BusStation);
                checkedListBox2.Items.Remove(checkedListBox2.CheckedItems[i - 1]);
            }
            foreach (BusStation pBusStation in checkedListBox2.Items)
            {
                m_SelBusStationList.Add(pBusStation);
            }
            m_BusStationList.Sort();
            if (m_bReverse)
                m_BusStationList.Reverse();
            checkedListBox1.Items.Clear();
            foreach (BusStation pItem in m_BusStationList)
            {
                checkedListBox1.Items.Add(pItem);
            }
        }

        private void frmEditRoadAndStation_Load(object sender, EventArgs e)
        {
            label3.Text = m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadName")).ToString() + m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadTravel")).ToString();
            checkedListBox1.Items.Clear();
            m_BusStationList.Clear();
            IPolyline pPLine = m_pCurFeature.ShapeCopy as IPolyline;
            IPoint outPoint = new PointClass();
            double distanceAlongCurve = 0;//该点在曲线上最近的点距曲线起点的距离
            double distanceFromCurve = 0;//该点到曲线的直线距离
            bool bRightSide = false;//点在线的左边还是右边
            bool asRatio = false;  //asRatio：byval方式，bool类型，表示上面两个参数给定的长度是以绝对距离的方式给出还是以占曲线总长度的比例的方式给出
            foreach (IFeature pfeat in m_CurStationList)
            {
                pPLine.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pfeat.ShapeCopy as IPoint, asRatio, outPoint, ref distanceAlongCurve, ref distanceFromCurve, ref bRightSide);
                m_BusStationList.Add(new BusStation(pfeat.get_Value(pfeat.Fields.FindField("StationName")).ToString(), pfeat.get_Value(pfeat.Fields.FindField("Direct")).ToString(), (int)pfeat.get_Value(pfeat.Fields.FindField("OBJECTID")), distanceAlongCurve));
                EngineFuntions.AddTextElement(pfeat.Shape, pfeat.get_Value(pfeat.Fields.FindField("StationName")).ToString());
            }
            //m_BusStationList.Sort();
            foreach (BusStation pItem in m_BusStationList)
            {
                checkedListBox1.Items.Add(pItem);
            }
            if (m_BusStationList[0].rLength > m_BusStationList[1].rLength)
                m_bReverse = true;
            else
                m_bReverse = false;

            foreach (IFeature pfeat in m_SelStationList)
            {
                pPLine.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pfeat.ShapeCopy as IPoint, asRatio, outPoint, ref distanceAlongCurve, ref distanceFromCurve, ref bRightSide);
                m_SelBusStationList.Add(new BusStation(pfeat.get_Value(pfeat.Fields.FindField("StationName")).ToString(), pfeat.get_Value(pfeat.Fields.FindField("Direct")).ToString(), (int)pfeat.get_Value(pfeat.Fields.FindField("OBJECTID")), distanceAlongCurve));
                EngineFuntions.AddTextElement(pfeat.Shape, pfeat.get_Value(pfeat.Fields.FindField("StationName")).ToString());
            }
            m_SelBusStationList.Sort();
            if (m_bReverse)//是不是反向的队列
                m_SelBusStationList.Reverse();
            foreach (BusStation pItem in m_SelBusStationList)
            {
                checkedListBox2.Items.Add(pItem);
            }

            m_nRoadID = (int)m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("OBJECTID"));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String sConn = "Provider=sqloledb;Data Source = 172.16.34.120;Initial Catalog=sde;User Id = sa;Password = sa";
            //sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + ForBusInfo.GetProfileString("Businfo", "DataPos", Application.StartupPath + "\\Businfo.ini") + "\\data\\公交.mdb";
            OleDbConnection mycon = new OleDbConnection(sConn);
            mycon.Open();
            try
            {
                int nOrder = 0;
                string pStrSQL;
                OleDbCommand pCom;
                pStrSQL = String.Format("delete from  sde.RoadAndStation where RoadID = {0}", m_nRoadID);
                pCom = new OleDbCommand(pStrSQL, mycon);
                pCom.ExecuteNonQuery();
                foreach (BusStation pBusStation in checkedListBox1.Items)
                {
                    pStrSQL = String.Format("insert into sde.RoadAndStation(RoadID,StationID,StationOrder,BufferLength) values({0},{1},{2},{3})"
                        , m_nRoadID, pBusStation.ID, nOrder++, n_nBufferLength);
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
    }
}
