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

namespace Businfo
{
    public partial class frmRoadAndStation : Form
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
            String sConn ;
            OleDbConnection mycon;
            sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + Application.StartupPath + "\\data\\公交.mdb";
            mycon = new OleDbConnection(sConn);
            mycon.Open();

            try
            {
                int nOrder = 0;
                string pStrSQL;
                OleDbCommand pCom;
                pStrSQL = String.Format("delete from  RoadAndStation where RoadID = {0}", m_nRoadID);
                pCom = new OleDbCommand(pStrSQL, mycon);
                pCom.ExecuteNonQuery();
                foreach (BusStation pBusStation in ListBox1.Items)
                {
                    pStrSQL = String.Format("insert into RoadAndStation(RoadID,StationID,StationOrder) values({0},{1},{2})"
                        ,m_nRoadID, pBusStation.ID, nOrder++);
                    pCom = new OleDbCommand(pStrSQL, mycon);
                    pCom.ExecuteNonQuery();         
                }
                ForBusInfo.Add_Log(ForBusInfo.Login_name, "线路关联站点", m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadName")).ToString(), "");
                mycon.Close();
                this.Close();
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
                mycurve.ConstructOffset((IPolycurve)m_pCurFeature.Shape, -35, ref Missing, ref Missing);
                pPolygon = (IPolygon)EngineFuntions.ClickSel((IGeometry)mycurve , false, false, 35);
            } 
            else
            {
                mycurve.ConstructOffset((IPolycurve)m_pCurFeature.Shape, 35, ref Missing, ref Missing);
                pPolygon = (IPolygon)EngineFuntions.ClickSel((IGeometry)mycurve, false, false, 35);
            }
            EngineFuntions.AddPolygonElement(pPolygon);
            if (EngineFuntions.GetSeledFeatures(EngineFuntions.m_Layer_BusStation,ref m_featureCollection))
            {
                ListBox1.Items.Clear();
                m_BusStationList.Clear();
                IPolyline pPLine = m_pCurFeature.ShapeCopy as IPolyline;
                IPoint outPoint = new PointClass();
                double distanceAlongCurve = 0;//该点在曲线上最近的点距曲线起点的距离
                double distanceFromCurve = 0;//该点到曲线的直线距离
                bool bRightSide = false;//点在线的左边还是右边
                bool asRatio = false;  //asRatio：byval方式，bool类型，表示上面两个参数给定的长度是以绝对距离的方式给出还是以占曲线总长度的比例的方式给出
                foreach (IFeature pfeat in m_featureCollection)
                {
                    pPLine.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pfeat.ShapeCopy as IPoint, asRatio, outPoint, ref distanceAlongCurve, ref distanceFromCurve, ref bRightSide);
                    m_BusStationList.Add(new BusStation(pfeat.get_Value(pfeat.Fields.FindField("StationName")).ToString(), pfeat.get_Value(pfeat.Fields.FindField("Direct")).ToString(), (int)pfeat.get_Value(pfeat.Fields.FindField("OBJECTID")), distanceAlongCurve));
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
            IPoint outPoint = new PointClass();
            double distanceAlongCurve = 0;//该点在曲线上最近的点距曲线起点的距离
            double distanceFromCurve = 0;//该点到曲线的直线距离
            bool bRightSide = false;//点在线的左边还是右边
            bool asRatio = false;  //asRatio：byval方式，bool类型，表示上面两个参数给定的长度是以绝对距离的方式给出还是以占曲线总长度的比例的方式给出
            foreach (IFeature pfeat in m_featureCollection)
            {
                pPLine.QueryPointAndDistance(esriSegmentExtension.esriNoExtension, pfeat.ShapeCopy as IPoint, asRatio, outPoint, ref distanceAlongCurve, ref distanceFromCurve, ref bRightSide);
                m_BusStationList.Add(new BusStation(pfeat.get_Value(pfeat.Fields.FindField("StationName")).ToString(), pfeat.get_Value(pfeat.Fields.FindField("Direct")).ToString(), (int)pfeat.get_Value(pfeat.Fields.FindField("OBJECTID")), distanceAlongCurve));
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
    }
}