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
            foreach (DataGridViewColumn eColumn in DataGridView1.Columns)
            {
                eColumn.ReadOnly = true;
            }
            DataGridView1.Columns[0].ReadOnly = false;
            m_bEdit = false;
        }

        private void fillByINOBJECTIDToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.公交站线TableAdapter.FillByINOBJECTID(this.roadDataSet.公交站线, ((string)(System.Convert.ChangeType(param1ToolStripTextBox.Text, typeof(int)))));
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.公交站线TableAdapter.Fill(this.roadDataSet.公交站线);
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
                this.公交站线TableAdapter.FillByINOBJECTID(this.roadDataSet.公交站线, strInPara);
            }
        }

        private void DataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != m_nCurRowIndex && m_bEdit)
            {
                foreach (DataGridViewCell eCell in DataGridView1.Rows[m_nCurRowIndex].Cells)
                {
                    eCell.ReadOnly = true;
                }
                DataGridView1.EndEdit();
                m_nObjectId = (int)DataGridView1.Rows[m_nCurRowIndex].Cells[1].Value;
                m_pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusRoad, "OBJECTID", m_nObjectId.ToString());
                if (m_pCurFeature != null)
                {
                    for (int i = 3; i < m_pCurFeature.Fields.FieldCount-1; i++)
                    {
                        m_pCurFeature.set_Value(i, DataGridView1.Rows[m_nCurRowIndex].Cells[i].Value);
                    }
                    m_pCurFeature.Store();
                    ForBusInfo.Add_Log(ForBusInfo.Login_name, "编辑站线属性", DataGridView1.Rows[m_nCurRowIndex].Cells[4].Value.ToString(), "");
                }
                m_bEdit = false;
            }
        }

        private void DataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == -1 && e.RowIndex > -1)
            {
                if (!m_bEdit)
                {
                    if (MessageBox.Show("是否要修改属性！", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        foreach (DataGridViewCell eCell in DataGridView1.Rows[e.RowIndex].Cells)
                        {
                            eCell.ReadOnly = false;
                        }
                        m_bEdit = true;
                        m_nCurRowIndex = e.RowIndex;
                    }
                    else
                    {
                        m_bEdit = false;
                    }
                }
                else
                {
                    if (MessageBox.Show("取消修改！", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DataGridView1.EndEdit();
                        m_nObjectId = (int)DataGridView1.Rows[m_nCurRowIndex].Cells[1].Value;
                        m_pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusStation, "OBJECTID", m_nObjectId.ToString());
                        if (m_pCurFeature != null)
                        {
                            for (int i = 3; i < m_pCurFeature.Fields.FieldCount; i++)
                            {
                                DataGridView1.Rows[m_nCurRowIndex].Cells[i].Value = m_pCurFeature.get_Value(i);
                            }
                            foreach (DataGridViewCell eCell in DataGridView1.Rows[m_nCurRowIndex].Cells)
                            {
                                eCell.ReadOnly = true;
                            }
                        }
                        m_bEdit = false;
                    }
                    else
                    {
                        m_bEdit = true;
                    }
                }
            }
        }

    }
}