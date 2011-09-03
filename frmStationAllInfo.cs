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
        public string m_strField = "";
        public DataSet m_ds = null;

        public frmStationAllInfo()
        {
            InitializeComponent();
        }

        private void frmStationAllInfo_Load(object sender, EventArgs e)
        {
            // TODO: ���д��뽫���ݼ��ص���stationDataSet.����վ�㡱�С������Ը�����Ҫ�ƶ����Ƴ�����
            if(button2.Visible == false)
            {
                m_strField = "all";
            }
            if (m_ds != null)
            {
                
            }
            else if (m_featureCollection.Count < 1)
                ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Station_FillAll, "", new string[] { m_strField });
            else
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

        private void DataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
#region ȡ����˫���޸ģ��ĳɰ�ť����ȫ���༭ģʽ
            //if (e.ColumnIndex == -1 && e.RowIndex > -1)
            //{
            //    if (!m_bEdit)
            //    {
            //        if (MessageBox.Show("�Ƿ�Ҫ�޸����ԣ�", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //        {
            //            foreach (DataGridViewCell eCell in DataGridView1.Rows[e.RowIndex].Cells)
            //            {
            //                eCell.ReadOnly = false;
            //            }
            //            if (ForBusInfo.Login_name == "��ʩ����")
            //            {
            //                DataGridView1.Columns["StationName"].ReadOnly = true;
            //                DataGridView1.Columns["Direct"].ReadOnly = true;
            //                DataGridView1.Columns["StationAlias"].ReadOnly = true;
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
            //        if (MessageBox.Show("ȡ���޸ģ�", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //        {
            //            DataGridView1.EndEdit();
            //            m_nObjectId = (int)DataGridView1.Rows[m_nCurRowIndex].Cells[1].Value;
            //            m_pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusStation, "OBJECTID", m_nObjectId.ToString());
            //            if (m_pCurFeature != null)
            //            {
            //                for (int i = 3; i < m_pCurFeature.Fields.FieldCount; i++)
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

        private void DataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
#region ȡ�����л��б������ݣ��û������ťȫ������.ʵ����cellchangʱ���档
            //if (e.RowIndex != m_nCurRowIndex && m_bEdit)
            //{
            //    foreach (DataGridViewCell eCell in DataGridView1.Rows[m_nCurRowIndex].Cells)
            //    {
            //        eCell.ReadOnly = true;
            //    }
            //    DataGridView1.EndEdit();
            //    m_nObjectId = (int)DataGridView1.Rows[m_nCurRowIndex].Cells[1].Value;
            //    m_pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusStation, "OBJECTID", m_nObjectId.ToString());
            //    if (m_pCurFeature != null)
            //    {
            //        for (int i = 3; i < m_pCurFeature.Fields.FieldCount; i++)
            //        {
            //            if (DataGridView1.Rows[m_nCurRowIndex].Cells[i].Visible)
            //            {
            //                m_pCurFeature.set_Value(i, DataGridView1.Rows[m_nCurRowIndex].Cells[i].Value);
            //            }
            //        }
            //        m_pCurFeature.Store();
            //    }
            //    string strName = m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("StationName")).ToString();
            //    ForBusInfo.Add_Log(ForBusInfo.Login_name, "�޸�վ������", strName, "");
            //    m_bEdit = false;
            //}
#endregion
            
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
                ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Station_FillByOBJECTID, string.Format(" WHERE (OBJECTID IN ({0}))", strInPara.Substring(0, strInPara.Length - 1)), new string[] { m_strField });
                //this.����վ��TableAdapter.FillByINOBJECTID(this.stationDataSet.����վ��, strInPara);
            }
        }

        public void RefreshStationGrid(string strInPara)
        {
            //DataGridView1.DataSource = null;
            //DataGridView1.DataSource = m_ds;
            //DataGridView1.DataMember = "Station";
            //DataGridView1.Columns["CheckBox1"].Visible = false;
            //ForBusInfo.SetGridHeard(DataGridView1, ForBusInfo.GridSetType.Station_FillByOBJECTID, new string[] { m_strField });
            //ForBusInfo.SetColSortMode(DataGridView1, DataGridViewColumnSortMode.NotSortable);
            //ForBusInfo.SetRowNo(DataGridView1);
            ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Station_FillByOBJECTID, string.Format(" WHERE (OBJECTID IN ({0}))", strInPara.Substring(0, strInPara.Length - 1)), new string[] { m_strField });
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

        public void SetSortColumn(int nSortColumn)
        {
            DataGridView1.Sort(DataGridView1.Columns[nSortColumn], System.ComponentModel.ListSortDirection.Ascending);
            ForBusInfo.SetRowNo(DataGridView1);
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

        //�޸ı���վ������
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
                //m_nObjectId = (int)DataGridView1.Rows[m_nCurRowIndex].Cells["OBJECTID"].Value;
                //m_pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusStation, "OBJECTID", m_nObjectId.ToString());
                //if (m_pCurFeature != null)
                //{
                //    for (int i = 3; i < m_pCurFeature.Fields.FieldCount; i++)
                //    {
                //        if (DataGridView1.Rows[m_nCurRowIndex].Cells[i].Visible)
                //        {
                //            m_pCurFeature.set_Value(i , DataGridView1.Rows[m_nCurRowIndex].Cells[i].Value);
                //        }
                //    }
                //    m_pCurFeature.Store();
                //}
                //string strName = m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("StationName")).ToString();
                //ForBusInfo.Add_Log(ForBusInfo.Login_name, "�޸�վ������", strName, "");
                m_bEdit = false;
                button2.Text = "��ʼ�༭";
            }
            else
            {
                if (MessageBox.Show("�Ƿ�Ҫ�޸����ԣ�", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    button2.Text = "�����޸�";
                    foreach (DataGridViewColumn eColumn in DataGridView1.Columns)
                    {
                        eColumn.ReadOnly = false;
                    }
                    if (ForBusInfo.Login_name == "��ʩ����")
                    {
                        DataGridView1.Columns["StationName"].ReadOnly = true;
                        DataGridView1.Columns["Direct"].ReadOnly = true;
                        DataGridView1.Columns["StationAlias"].ReadOnly = true;
                        DataGridView1.Columns["DispatchStationThird"].ReadOnly = true;
                    }
                    m_bEdit = true;
                }
                else
                {
                    m_bEdit = false;
                }
            }
        }

        //��ѯ
        private void button1_Click(object sender, EventArgs e)
        {
            if (m_bEdit)
            {
                MessageBox.Show("���ȱ����޸ģ�\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (String.IsNullOrEmpty(TextBox1.Text))
            {
                ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Station_FillAll, "", new string[] { m_strField });
            }
            else if (TextBox1.Text.Contains("-"))
            {
                string[] strColu = TextBox1.Text.Split('-');
                ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Station_FillByStationName, string.Format(" WHERE (OBJECTID > {0} and OBJECTID < {1})", strColu[0], strColu[1]), new string[] { m_strField });
            }
            else
            {
                ForBusInfo.StationFill(DataGridView1, ForBusInfo.GridSetType.Station_FillByStationName, string.Format(" WHERE (StationName LIKE '%{0}%')", TextBox1.Text), new string[] { m_strField });
            }

           
            foreach (DataGridViewColumn eColumn in DataGridView1.Columns)
            {
                eColumn.ReadOnly = true;
            }
            DataGridView1.Columns[0].ReadOnly = false;
        }

        //��ť��Ϊ���ɼ���
        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < DataGridView1.Columns.Count;i++ )
            {
                DataGridView1.Columns[i].Visible = true;
            }
        }

        //�����excel
        private void button4_Click(object sender, EventArgs e)
        {
            ForBusInfo.DataGridView2Excel(DataGridView1, "վ��",true,1);//0��checkbox
        }
        
        //�޸���cell�ͱ�������
        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (m_bEdit)
            {
                m_nCurRowIndex = e.RowIndex;
                //DataGridView1.EndEdit();
                m_nObjectId = (int)DataGridView1.Rows[m_nCurRowIndex].Cells["OBJECTID"].Value;
                m_pCurFeature = EngineFuntions.GetFeatureByFieldAndValue(EngineFuntions.m_Layer_BusStation, "OBJECTID", m_nObjectId.ToString());
                if (m_pCurFeature != null)
                {
                    int nField = m_pCurFeature.Fields.FindField(DataGridView1.Columns[e.ColumnIndex].Name);
                    m_pCurFeature.set_Value(nField, DataGridView1.Rows[m_nCurRowIndex].Cells[e.ColumnIndex].Value);

                    double B, L, H;
                    if (double.TryParse(DataGridView1.Rows[m_nCurRowIndex].Cells["GPSLatitude"].Value.ToString(), out  B)//�о�γ�ȵ�ʱ��Ҫ��վ��ƽ�ƹ�ȥ��
                        && double.TryParse(DataGridView1.Rows[m_nCurRowIndex].Cells["GPSLongtitude"].Value.ToString(), out  L))
                    //H = DataGridView1.Rows[m_nCurRowIndex].Cells["GPSHigh"].Value;
                    {
                        if (B > 30 && L > 114)
                        {
                            double x, y, z;
                            x = y = z = 0;
                            CoordTrans Coord = new CoordTrans(162.8998, 216.8504, 133.8944, 0.72814164, 2.73301875, -5.38285723, -9.06757729, 114, 3);
                            Coord.BLHto84XYZ(B, L, 0, ref y, ref x, ref z);
                            IPoint outPoint = new PointClass();
                            outPoint.PutCoords(x, y - 3000000);
                            m_pCurFeature.Shape = outPoint;
                        }
                    }
                    m_pCurFeature.Store();
                }
                string strName = m_pCurFeature.get_Value(m_pCurFeature.Fields.FindField("StationName")).ToString();
                if (e.ColumnIndex > 0)
                ForBusInfo.Add_Log(ForBusInfo.Login_name, "�޸�վ������", strName, DataGridView1.Columns[e.ColumnIndex].HeaderText);
            }
        }

        //����gridview�����ʽ����
        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void SetButtonVisable()
        {
            button2.Visible = false;
        }

        private void DataGridView1_Sorted(object sender, EventArgs e)
        {
            ForBusInfo.SetRowNo(DataGridView1);
        }
    }
}