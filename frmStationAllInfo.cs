using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using Businfo.Globe;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;

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
            if (m_featureCollection.Count < 1)
                ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Station_FillAll, "");
            else
                RefreshSelectGrid();
            foreach (DataGridViewColumn eColumn in DataGridView1.Columns)
            {
                eColumn.ReadOnly = true;
            }
            DataGridView1.Columns[0].ReadOnly = false;
            m_bEdit = false;
            
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
                            for (int i = 3; i < m_pCurFeature.Fields.FieldCount; i++)
                            {
                                DataGridView1.Rows[m_nCurRowIndex].Cells[i].Value = m_pCurFeature.get_Value(i-1);
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
                     for (int i = 3; i < m_pCurFeature.Fields.FieldCount; i++)
                     {
                         m_pCurFeature.set_Value(i-1,DataGridView1.Rows[m_nCurRowIndex].Cells[i].Value) ;
                     }
                     m_pCurFeature.Store();   
                }
                string strName = m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("StationName")).ToString();
                ForBusInfo.Add_Log(ForBusInfo.Login_name, "�޸�վ������", strName, "");
                 m_bEdit = false;
            }
            //�õ���ǰ�Ҽ�ѡ�е�feature
            m_pCurFeature = null;
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.RowIndex <= DataGridView1.Rows.Count)
            {
                m_nCurRowIndex = e.RowIndex;
                DataGridView1.Rows[m_nCurRowIndex].Selected = true;
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                m_pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusStation, "OBJECTID", DataGridView1.Rows[m_nCurRowIndex].Cells[1].Value.ToString());
            }
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
                ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Station_FillByOBJECTID, string.Format(" WHERE (OBJECTID IN ({0}))", strInPara.Substring(0, strInPara.Length-1)));
                //this.����վ��TableAdapter.FillByINOBJECTID(this.stationDataSet.����վ��, strInPara);
            }
        }

         public void RefreshStationGrid(string strInPara)
        {
            ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Station_FillByOBJECTID, string.Format(" WHERE (OBJECTID IN ({0}))", strInPara.Substring(0, strInPara.Length - 1)));
            //this.����վ��TableAdapter.FillByINOBJECTID(this.stationDataSet.����վ��, strInPara);
        }

        public void SetStationOrderCell(List<String> nRowAndorder)
        {  
            string strTemp;
            for (int i = 0; i < nRowAndorder.Count; i++)
            {
               if (i < 10)
               {
                   strTemp = String.Format("0{0}", i);//������λ��ǰ�油��   
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

        private void ��λ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_pCurFeature != null)
            {
                IPoint pPoint;
                IEnvelope pEnvelope;
                IDisplayTransformation pDisplayTransformation;
                pDisplayTransformation = EngineFuntions.m_AxMapControl.ActiveView.ScreenDisplay.DisplayTransformation;
                pEnvelope = m_pCurFeature.Extent;
                pPoint = pEnvelope.UpperLeft;
                pEnvelope = pDisplayTransformation.VisibleBounds;
                pEnvelope.CenterAt(pPoint);
                pDisplayTransformation.VisibleBounds = pEnvelope;
                EngineFuntions.m_AxMapControl.Map.MapScale = 2000;
                pDisplayTransformation.VisibleBounds = EngineFuntions.m_AxMapControl.ActiveView.Extent;
                EngineFuntions.m_AxMapControl.ActiveView.ScreenDisplay.Invalidate(null, true, (short)esriScreenCache.esriAllScreenCaches);
                Application.DoEvents();
                EngineFuntions.FlashShape(m_pCurFeature.ShapeCopy);
            }
        }

        private void ȫ�����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_pCurFeature != null)
            {
                frmPano frmPopup = new frmPano();
                frmPopup.m_strURL = "E:\\Code For Working\\BusInfo\\bin\\Debug\\Data\\A01\\pano1.html";
                frmPopup.Show();
            }
        }

        //����Ҽ��ͽ���DataGridView1�༭����ȻDataGridView1���༭״̬û�н�����ֵ�����¡�
        private void DataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView1.EndEdit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (m_bEdit)
            {
                foreach (DataGridViewCell eCell in DataGridView1.Rows[m_nCurRowIndex].Cells)
                {
                    eCell.ReadOnly = true;
                }
                DataGridView1.EndEdit();
                m_nObjectId = (int)DataGridView1.Rows[m_nCurRowIndex].Cells[1].Value;
                m_pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusStation, "OBJECTID", m_nObjectId.ToString());
                if (m_pCurFeature != null)
                {
                    for (int i = 3; i < m_pCurFeature.Fields.FieldCount; i++)
                    {
                        m_pCurFeature.set_Value(i - 1, DataGridView1.Rows[m_nCurRowIndex].Cells[i].Value);
                    }
                    m_pCurFeature.Store();
                }
                string strName = m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("StationName")).ToString();
                ForBusInfo.Add_Log(ForBusInfo.Login_name, "�޸�վ������", strName, "");
                m_bEdit = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(TextBox1.Text))
            {
                //this.����վ��TableAdapter.Fill(this.stationDataSet.����վ��);
                ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Station_FillAll, "");
            }
            else
            {
                ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Station_FillByStationName, string.Format(" WHERE (StationName LIKE '%{0}%')", TextBox1.Text));
                //this.����վ��TableAdapter.FillByStationName(this.stationDataSet.����վ��, "%" + TextBox1.Text + "%");
            }

            foreach (DataGridViewColumn eColumn in DataGridView1.Columns)
            {
                eColumn.ReadOnly = true;
            }
            DataGridView1.Columns[0].ReadOnly = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < DataGridView1.Columns.Count;i++ )
            {
                DataGridView1.Columns[i].Visible = true;
            }
        }
    }
}