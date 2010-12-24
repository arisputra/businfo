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
    public partial class frmStationAllInfo : Form
    {
        public Int32 m_nObjectId,m_nCurRowIndex;
        Boolean m_bEdit;
        public IFeature m_pCurFeature;
        public List<IFeature> m_featureCollection = new List<IFeature>();

        public frmStationAllInfo()
        {
            InitializeComponent();
        }

        private void frmStationAllInfo_Load(object sender, EventArgs e)
        {
            // TODO: ���д��뽫���ݼ��ص���stationDataSet.����վ�㡱�С������Ը�����Ҫ�ƶ����Ƴ�����
            //this.����վ��TableAdapter.FillByOBJECTID(this.stationDataSet.����վ��, m_nObjectId);
            RefreshSelectGrid();
            foreach (DataGridViewColumn eColumn in DataGridView1.Columns)
            {
                eColumn.ReadOnly = true;
            }
            DataGridView1.Columns[0].ReadOnly = false;
            m_bEdit = false;
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.����վ��TableAdapter.Fill(this.stationDataSet.����վ��);
            foreach (DataGridViewColumn eColumn in DataGridView1.Columns)
            {
                eColumn.ReadOnly = true;
            }
            DataGridView1.Columns[0].ReadOnly = false;

           
        }

        private void fillByOBJECTIDToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.����վ��TableAdapter.FillByOBJECTID(this.stationDataSet.����վ��, ((int)(System.Convert.ChangeType(oBJECTIDToolStripTextBox.Text, typeof(int)))));
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void DataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ( e.ColumnIndex == -1 && e.RowIndex > -1)
            {
                if (!m_bEdit)
                {
                    if(MessageBox.Show("�Ƿ�Ҫ�޸����ԣ�" , "��ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                    if(MessageBox.Show("ȡ���޸ģ�" , "��ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DataGridView1.EndEdit();
                        m_nObjectId = (int)DataGridView1.Rows[m_nCurRowIndex].Cells[1].Value;
                        m_pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusStation, "OBJECTID",m_nObjectId.ToString());
                        if (m_pCurFeature != null)
                        {
                            for (int i = 3; i < m_pCurFeature.Fields.FieldCount ; i++)
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
                m_pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusStation, "OBJECTID",m_nObjectId.ToString());
                if (m_pCurFeature != null)
                {
                     for (int i = 3; i < m_pCurFeature.Fields.FieldCount ; i++)
                     {
                         m_pCurFeature.set_Value(i,DataGridView1.Rows[m_nCurRowIndex].Cells[i].Value) ;
                     }
                     m_pCurFeature.Store();   
                }
                 m_bEdit = false;
            }
            
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
                this.����վ��TableAdapter.FillByINOBJECTID(this.stationDataSet.����վ��, strInPara);
            }
        }

        private void fillByINOBJECTIDToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.����վ��TableAdapter.FillByINOBJECTID(this.stationDataSet.����վ��, ((string)(System.Convert.ChangeType(param1ToolStripTextBox.Text, typeof(int)))));
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        
        public void RefreshStationGrid(string strInPara)
        {
            this.����վ��TableAdapter.FillByINOBJECTID(this.stationDataSet.����վ��, strInPara);
        }

        public void SetStationOrderCell(List<String> nRowAndorder)
        {  
            string strTemp;
            for (int i = 0; i < nRowAndorder.Count; i++)
            {
               if (i < 10)
               {
                   strTemp = String.Format("0{0}", i);
               } 
               else
               {
                   strTemp = i.ToString();
               }
               foreach (DataGridViewRow eRow in DataGridView1.Rows)
                {
                     if (eRow.Cells[1].Value.ToString() == nRowAndorder[i])
                     {
                         eRow.Cells[2].Value = strTemp;
                         break;
                     }
                }             
            }
        }

        public void SetSortColumn(int  nSortColumn)
        {
            DataGridView1.Sort(DataGridView1.Columns[nSortColumn], System.ComponentModel.ListSortDirection.Ascending);
        }
    }
}