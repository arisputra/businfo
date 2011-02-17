﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Businfo.Globe;

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
            String sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + Application.StartupPath + "\\data\\公交.mdb";
            OleDbConnection mycon = new OleDbConnection(sConn);
            mycon.Open();
            OleDbDataAdapter da = ForBusInfo.CreateCustomerAdapter(mycon, "select * from  OperationLog","","");
            da.SelectCommand.ExecuteNonQuery();
            DataSet ds = new DataSet();
            int nQueryCount = da.Fill(ds);
            foreach (DataRow eDataRow in ds.Tables[0].Rows)
            {
                listBox1.Items.Add(string.Format("{0}与{1}进行了{2}操作：{3}", eDataRow[1], eDataRow[2], eDataRow[4], eDataRow[3]));
            }
            maskedTextBox1.Text = DateTime.Now.AddMonths(-1).ToShortDateString() ;
            maskedTextBox2.Text = DateTime.Now.ToShortDateString();
            //Sunisoft.IrisSkin.SkinEngine se = null;
            //se = new Sunisoft.IrisSkin.SkinEngine();
            //se.SkinFile = Application.StartupPath + @"\Data\Diamond\DiamondBlue.ssk";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + Application.StartupPath + "\\data\\公交.mdb";
            OleDbConnection mycon = new OleDbConnection(sConn);
            mycon.Open();
            OleDbDataAdapter da = ForBusInfo.CreateCustomerAdapter(mycon, string.Format("select * from  OperationLog where (name = '{0}' and LogTime > '{1}' and LogTime < '{2}')", textBox1.Text, maskedTextBox1.Text + " 00:00:00", maskedTextBox2.Text + " 24:00:00"), "", "");
            da.SelectCommand.ExecuteNonQuery();
            DataSet ds = new DataSet();
            int nQueryCount = da.Fill(ds);
            listBox1.Items.Clear();
            foreach (DataRow eDataRow in ds.Tables[0].Rows)
            {
                listBox1.Items.Add(string.Format("{0}与{1}进行了{2}操作：{3}", eDataRow[1], eDataRow[2], eDataRow[4], eDataRow[3]));
            }
        }
    }
}