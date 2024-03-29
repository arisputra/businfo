﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Businfo.Globe;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;

namespace Businfo
{
    public partial class frmOperation : Form
    {
        public frmOperation()
        {
            InitializeComponent();
        }

        private void frmOperation_Load(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection mycon = new OleDbConnection(ForBusInfo.Connect_Sql);
                mycon.Open();
                OleDbDataAdapter da;
                if(ForBusInfo.Connect_Type == 1)
                    da = ForBusInfo.CreateCustomerAdapter(mycon, "select * from sde.OPERATIONLOG ORDER BY OBJECTID DESC", "", "");
                else
                    da = ForBusInfo.CreateCustomerAdapter(mycon, "select * from OPERATIONLOG ORDER BY OBJECTID DESC", "", "");
                da.SelectCommand.ExecuteNonQuery();
                DataSet ds = new DataSet();
                int nQueryCount = da.Fill(ds);
                foreach (DataRow eDataRow in ds.Tables[0].Rows)
                {
                    listBox1.Items.Add(string.Format("{0}于{1}进行了{2}操作：{3}", eDataRow[1], eDataRow[2], eDataRow[4], eDataRow[3]));
                }
                maskedTextBox1.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
                maskedTextBox2.Text = DateTime.Now.ToShortDateString();
                mycon.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //Sunisoft.IrisSkin.SkinEngine se = null;
            //se = new Sunisoft.IrisSkin.SkinEngine();
            //se.SkinFile = Application.StartupPath + @"\Data\Diamond\DiamondBlue.ssk";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection mycon = new OleDbConnection(ForBusInfo.Connect_Sql);
            mycon.Open();
            OleDbDataAdapter da;
            if(ForBusInfo.Connect_Type == 1)
                da = ForBusInfo.CreateCustomerAdapter(mycon, string.Format("select * from  sde.OperationLog where (name = '{0}' and LogTime > '{1}' and LogTime < '{2}') ORDER BY OBJECTID DESC", textBox1.Text, maskedTextBox1.Text + " 00:00:00", maskedTextBox2.Text + " 24:00:00"), "", "");
            else
                da = ForBusInfo.CreateCustomerAdapter(mycon, string.Format("select * from  OperationLog where (name = '{0}' and LogTime > '{1}' and LogTime < '{2}') ORDER BY OBJECTID DESC", textBox1.Text, maskedTextBox1.Text + " 00:00:00", maskedTextBox2.Text + " 24:00:00"), "", "");
            da.SelectCommand.ExecuteNonQuery();
            DataSet ds = new DataSet();
            int nQueryCount = da.Fill(ds);
            listBox1.Items.Clear();
            foreach (DataRow eDataRow in ds.Tables[0].Rows)
            {
                listBox1.Items.Add(string.Format("{0}于{1}进行了{2}操作：{3}", eDataRow[1], eDataRow[2], eDataRow[4], eDataRow[3]));
            }
            mycon.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<IFeature> pFeatureList = new List<IFeature>();
            List<IFeature> pCurFeatureList = EngineFuntions.GetSeartchFeatures(EngineFuntions.m_Layer_BusStation,"OBJECTID > -1");
            foreach (IFeature pfeature in pCurFeatureList)
            {
                IFeatureLayer CurFeatureLayer = EngineFuntions.SetCanSelLay("道路中心线");
                EngineFuntions.ClickSel(pfeature.ShapeCopy, false, true, 26);

                if (EngineFuntions.GetSeledFeatures(CurFeatureLayer, ref  pFeatureList))
                {
                    foreach (IFeature pfea in pFeatureList)
                    {
                        int nIndex = pfeature.Fields.FindField("StationCharacter");
                        pfeature.set_Value(nIndex, pfea.get_Value(pfea.Fields.FindField("道路名称")) as string);
                    }
                }
                pfeature.Store();
            }
            MessageBox.Show("xxxxxx\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
