using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Businfo.Globe;

namespace Businfo
{
    public partial class frmRoadAllInfo : Form
    {
        public Int32 m_nObjectId, m_nCurRowIndex;
        Boolean m_bEdit;
        public IFeature m_pCurFeature;
        public List<IFeature> m_featureCollection = new List<IFeature>();
        public frmRoadAllInfo()
        {
            InitializeComponent();
        }

        private void frmRoadAllInfo_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“roadDataSet.公交站线”中。您可以根据需要移动或移除它。
            //this.公交站线TableAdapter.Fill(this.roadDataSet.公交站线);
            RefreshSelectGrid();
            DataGridView1.RowHeadersWidth = 60;
            //int nNum = 1;
            //foreach (DataGridViewRow eRow in DataGridView1.Rows)
            //{
            //    eRow.HeaderCell.Value = nNum++.ToString();
            //}
            foreach (DataGridViewColumn eColumn in DataGridView1.Columns)
            {
                eColumn.ReadOnly = true;
            }
            DataGridView1.Columns[0].ReadOnly = false;
            m_bEdit = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Road_FillAll, "", new string[] { "" });
            //this.公交站线TableAdapter.Fill(this.roadDataSet.公交站线);
            //int nNum = 1;
            //foreach (DataGridViewRow eRow in DataGridView1.Rows)
            //{
            //    eRow.HeaderCell.Value = nNum++.ToString();
            //}
            foreach (DataGridViewColumn eColumn in DataGridView1.Columns)
            {
                eColumn.ReadOnly = true;
            }
            DataGridView1.Columns[0].ReadOnly = false;
        }

        private void DataGridView1_Sorted(object sender, EventArgs e)
        {

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
                //this.公交站线TableAdapter.FillByINOBJECTID(this.roadDataSet.公交站线, strInPara);
            }
        }

        private void DataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            #region 取消了切换行保存数据，用户点击按钮全部保存.实际是cellchang时保存。
            //if (e.RowIndex != m_nCurRowIndex && m_bEdit)
            //{
            //    foreach (DataGridViewCell eCell in DataGridView1.Rows[m_nCurRowIndex].Cells)
            //    {
            //        eCell.ReadOnly = true;
            //    }
            //    DataGridView1.EndEdit();
            //    m_nObjectId = (int)DataGridView1.Rows[m_nCurRowIndex].Cells[1].Value;
            //    m_pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusRoad, "OBJECTID", m_nObjectId.ToString());
            //    if (m_pCurFeature != null)
            //    {
            //        for (int i = 3; i < m_pCurFeature.Fields.FieldCount - 1; i++)
            //        {
            //            if (DataGridView1.Rows[m_nCurRowIndex].Cells[i].Visible)
            //            {
            //                m_pCurFeature.set_Value(i - 1, DataGridView1.Rows[m_nCurRowIndex].Cells[i].Value);
            //            }
            //        }
            //        m_pCurFeature.Store();
            //        ForBusInfo.Add_Log(ForBusInfo.Login_name, "编辑站线属性", DataGridView1.Rows[m_nCurRowIndex].Cells[4].Value.ToString(), "");
            //    }
            //    m_bEdit = false;
            //}
            #endregion
            
        }

        private void DataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            #region 取消了双击修改，改成按钮进入全部编辑模式
            //if (e.ColumnIndex == -1 && e.RowIndex > -1)
            //{
            //    if (!m_bEdit)
            //    {
            //        if (MessageBox.Show("是否要修改属性！", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //        {
            //            foreach (DataGridViewCell eCell in DataGridView1.Rows[e.RowIndex].Cells)
            //            {
            //                eCell.ReadOnly = false;
            //            }
            //            m_bEdit = true;
            //            m_nCurRowIndex = e.RowIndex;
            //        }
            //        else
            //        {
            //            m_bEdit = false;
            //        }
            //    }
            //    else
            //    {
            //        if (MessageBox.Show("取消修改！", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //        {
            //            DataGridView1.EndEdit();
            //            m_nObjectId = (int)DataGridView1.Rows[m_nCurRowIndex].Cells[1].Value;
            //            m_pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusRoad, "OBJECTID", m_nObjectId.ToString());
            //            if (m_pCurFeature != null)
            //            {
            //                for (int i = 3; i < m_pCurFeature.Fields.FieldCount - 1; i++)
            //                {
            //                    if (DataGridView1.Rows[m_nCurRowIndex].Cells[i].Visible)
            //                    {
            //                        DataGridView1.Rows[m_nCurRowIndex].Cells[i].Value = m_pCurFeature.get_Value(i - 1);
            //                    }
            //                }
            //                foreach (DataGridViewCell eCell in DataGridView1.Rows[m_nCurRowIndex].Cells)
            //                {
            //                    eCell.ReadOnly = true;
            //                }
            //            }
            //            m_bEdit = false;
            //        }
            //        else
            //        {
            //            m_bEdit = true;
            //        }
            //    }
            //}
            #endregion
            
        }
        //编辑 保存 数据
        private void button2_Click(object sender, EventArgs e)
        {
            if (m_bEdit)
            {
                DataGridView1.EndEdit();
                foreach (DataGridViewColumn eColumn in DataGridView1.Columns)
                {
                    eColumn.ReadOnly = true;
                }
                //DataGridView1.EndEdit();
                //m_nObjectId = (int)DataGridView1.Rows[m_nCurRowIndex].Cells[1].Value;
                //m_pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusRoad, "OBJECTID", m_nObjectId.ToString());
                //if (m_pCurFeature != null)
                //{
                //    for (int i = 3; i < m_pCurFeature.Fields.FieldCount - 1; i++)
                //    {
                //        if (DataGridView1.Rows[m_nCurRowIndex].Cells[i].Visible)
                //        {
                //            m_pCurFeature.set_Value(i , DataGridView1.Rows[m_nCurRowIndex].Cells[i].Value);
                //        }
                //    }
                //    m_pCurFeature.Store();
                //    ForBusInfo.Add_Log(ForBusInfo.Login_name, "编辑站线属性", DataGridView1.Rows[m_nCurRowIndex].Cells[4].Value.ToString(), "");
                //}
                m_bEdit = false;
                button2.Text = "开始编辑";
            }
            else
            {
                if (MessageBox.Show("是否要修改属性！", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    button2.Text = "保存修改";
                    foreach (DataGridViewColumn eColumn in DataGridView1.Columns)
                    {
                        eColumn.ReadOnly = false;
                    }
                    m_bEdit = true;
                }
                else
                {
                    m_bEdit = false;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ForBusInfo.DataGridView2Excel(DataGridView1, "线路",true,1);//0是checkbox，跳过
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (m_bEdit)
            {
                m_nCurRowIndex = e.RowIndex;
                //DataGridView1.EndEdit();
                m_nObjectId = (int)DataGridView1.Rows[m_nCurRowIndex].Cells["OBJECTID"].Value;
                m_pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusRoad, "OBJECTID", m_nObjectId.ToString());
                if (m_pCurFeature != null)
                {
                    int nField = m_pCurFeature.Fields.FindField(DataGridView1.Columns[e.ColumnIndex].Name);
                    m_pCurFeature.set_Value(nField, DataGridView1.Rows[m_nCurRowIndex].Cells[e.ColumnIndex].Value);
                    //for (int i = 3; i < m_pCurFeature.Fields.FieldCount - 1; i++)
                    //{
                    //    if (DataGridView1.Rows[m_nCurRowIndex].Cells[i].Visible)
                    //    {
                    //        m_pCurFeature.set_Value(i-1, DataGridView1.Rows[m_nCurRowIndex].Cells[i].Value);
                    //    }
                    //}
                    m_pCurFeature.Store();
                }
                string strName = m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("RoadName")).ToString();
                if (e.ColumnIndex>0)
                ForBusInfo.Add_Log(ForBusInfo.Login_name, "编辑线路属性", strName, DataGridView1.Columns[e.ColumnIndex].HeaderText);
            }
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void SetButtonVisable()
        {
            button2.Visible = false;
        }

    }
}