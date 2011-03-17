using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Businfo.Globe;
using ESRI.ArcGIS.Geodatabase;
using System.Data.OleDb;

namespace Businfo
{
    public partial class frmRoadRecover : Form
    {
        public frmRoadRecover()
        {
            InitializeComponent();
        }

        private void frmRoadRecover_Load(object sender, EventArgs e)
        {
             checkedListBox1.Items.Clear();
            List<IFeature> pCurFeatureList = EngineFuntions.GetSeartchFeatures(EngineFuntions.m_Layer_BackRoad, "OBJECTID > -1");
            foreach (IFeature pfea in pCurFeatureList)
            {
                checkedListBox1.Items.Add(new BusStation(pfea.get_Value(pfea.Fields.FindField("RoadName")).ToString(), pfea.get_Value(pfea.Fields.FindField("RoadTravel")).ToString(), (int)pfea.get_Value(pfea.Fields.FindField("OBJECTID"))));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // If so, loop through all checked items and print results.
            foreach (BusStation pBusStation in checkedListBox1.CheckedItems)
            {
                IFeature pBusRoad = EngineFuntions.GetOneSeartchFeature(EngineFuntions.m_Layer_BusRoad, string.Format("Roadname = '{0}' AND RoadTravel = '{1}'", pBusStation.StationName,pBusStation.Direct));
                if (pBusRoad != null)
                {
                    MessageBox.Show("公交线路图层已经存在该线路！\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                pBusRoad = EngineFuntions.GetOneSeartchFeature(EngineFuntions.m_Layer_BackRoad , "OBJECTID = " + pBusStation.ID);
                IFeature pFeature = EngineFuntions.CopyFeature(EngineFuntions.m_Layer_BusRoad, pBusRoad);
                String sConn = "Provider=sqloledb;Data Source = 172.16.34.120;Initial Catalog=sde;User Id = sa;Password = sa";
                //String sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + ForBusInfo.GetProfileString("Businfo", "DataPos", Application.StartupPath + "\\Businfo.ini") + "\\data\\公交.mdb";
                OleDbConnection mycon = new OleDbConnection(sConn);
                mycon.Open();
                try
                {
                    OleDbDataAdapter da = ForBusInfo.CreateCustomerAdapter(mycon, String.Format("select * from  sde.BackRAndS where RoadID = {0}", pBusRoad.get_Value(pBusRoad.Fields.FindField("OBJECTID"))),
                       "", String.Format("delete from  sde.BackRAndS where RoadID = {0}", pBusRoad.get_Value(pBusRoad.Fields.FindField("OBJECTID"))));
                    da.SelectCommand.ExecuteNonQuery();
                    DataSet ds = new DataSet();
                    int nQueryCount = da.Fill(ds);
                    foreach (DataRow eDataRow in ds.Tables[0].Rows)
                    {
                        da.InsertCommand.CommandText = String.Format("insert into sde.RoadAndStation(RoadID,StationID,StationOrder,BufferLength) values({0},{1},{2},{3})"
                       , pFeature.get_Value(pFeature.Fields.FindField("OBJECTID")), eDataRow[2], eDataRow[3], eDataRow[4]);
                        da.InsertCommand.ExecuteNonQuery();
                    }
                    da.DeleteCommand.ExecuteNonQuery();//删除备份站线关联站点数据
                    ForBusInfo.Add_Log(ForBusInfo.Login_name, "恢复站线", pFeature.get_Value(pFeature.Fields.FindField("RoadName")).ToString(), "");
                    mycon.Close();
                    pBusRoad.Delete();//删除备份站线
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("恢复站线时出错\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                EngineFuntions.PartialRefresh(EngineFuntions.m_Layer_BackRoad);
            }
            for (int i = checkedListBox1.CheckedItems.Count; i > 0; i--)
            {
                checkedListBox1.Items.Remove(checkedListBox1.CheckedItems[i - 1]);
            }
            ForBusInfo.Frm_Main.m_frmRoadPane.RefreshGrid();
         }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (BusStation pBusStation in checkedListBox1.CheckedItems)
            {
                IFeature pBackRoad = EngineFuntions.GetOneSeartchFeature(EngineFuntions.m_Layer_BackRoad, "OBJECTID = " + pBusStation.ID);
                String sConn = "Provider=sqloledb;Data Source = 172.16.34.120;Initial Catalog=sde;User Id = sa;Password = sa";
                //String sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + ForBusInfo.GetProfileString("Businfo", "DataPos", Application.StartupPath + "\\Businfo.ini") + "\\data\\公交.mdb";
                OleDbConnection mycon = new OleDbConnection(sConn);
                mycon.Open();
                try
                {
                    OleDbDataAdapter da = ForBusInfo.CreateCustomerAdapter(mycon,"",
                       "", String.Format("delete from sde.BackRAndS where RoadID = {0}", pBackRoad.get_Value(pBackRoad.Fields.FindField("OBJECTID"))));
                    da.DeleteCommand.ExecuteNonQuery();//删除备份站线关联站点数据
                    ForBusInfo.Add_Log(ForBusInfo.Login_name, "删除备份站线", pBackRoad.get_Value(pBackRoad.Fields.FindField("RoadName")).ToString(), "");
                    mycon.Close();
                    pBackRoad.Delete();//删除备份站线
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("删除备份站线时出错\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                EngineFuntions.PartialRefresh(EngineFuntions.m_Layer_BackRoad);
            }
            for (int i = checkedListBox1.CheckedItems.Count; i > 0; i--)
            {
                checkedListBox1.Items.Remove(checkedListBox1.CheckedItems[i - 1]);
            }
        }
    }
}
