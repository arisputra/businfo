using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;

namespace Businfo.Globe
{
    class ForBusInfo
    {
        #region "��������"
        public const int Pan_Layer = 1; //ͼ���ͣ�����
        public const int Pan_Station = 10; //վ���ͣ�����
        public const int Pan_Road = 11; //վ�߿�ͣ�����


        public const int Bus_Add = 1041; //վ�����
        public const int Bus_Supervise = 1042; //վ�����Ŀ¼��
        public const int Bus_Dele = 1043; //վ��ɾ��
        public const int Bus_Query = 1044; //վ���ѯ
        public const int Bus_Move = 1045; //վ���ƶ�
        public const int Bus_Edit = 1046; //���Ա༭
        public const int Bus_BackUp = 1047; //վ�㱸��
        public const int Bus_Recover = 1048; //վ��ָ�
        public const int Bus_Pano = 1049; //վ��ȫ��
        public const int Road_Supervise = 1050; //��·����Ŀ¼��
        public const int Road_Add = 1051; //��·���
        public const int Road_Dele = 1052; //��·ɾ��
        public const int Road_Query = 1053; //��·��ѯ
        public const int Road_Edit = 1054; //���Ա༭
        public const int Road_BackUp = 1055; //��·����
        public const int Road_Recover = 1056; //��·�ָ�
        public const int Road_Associate = 1057; //����վ��
        public const int Table_Supervise = 1058; //�������Ŀ¼��
        public const int BusInfo_Layer = 1059; //ͼ���л�
        public const int Table_RoadExport = 1061; //�������
        public const int Table_Operation = 1062; //�鿴��¼
        public const int Road_End = 1063; //�����·���
        public const int Road_Reversed = 1064; //��·����

        public const int Map3D_ZoomIn = 10211; //�Ŵ�
        public const int Map3D_ZoomOut = 10212; //��С
        public const int Map3D_Full = 10213; //ȫͼ
        public const int Map3D_Pan = 10214; //����
        public const int Map3D_Vector = 10215; //����ʸ��
        public const int Map3D_OpenMode = 10216; //
        public const int Map3D_PreView = 10217; //��һ��
        public const int Map3D_NextView = 10218; //��һ��
        public const int Map3D_Reflash = 10219; //ˢ��
        public const int Map3D_Distance = 10222; //����
        public const int Map3D_Area = 10224; //���

        public static string Login_name = "admin"; //��¼��
        public static frmMainNew Frm_Main; //��������

        #endregion

        public static void SetRowColor_Alternation(DataGridViewRowCollection RowCollection)
        {
            DataGridViewCellStyle RowColor = new DataGridViewCellStyle();
            RowColor.BackColor = Color.Aqua;
            int nRowCount = 0;
            foreach (DataGridViewRow eRow in RowCollection)
            {
                if (0 == nRowCount)
                {
                    eRow.DefaultCellStyle = RowColor;
                    nRowCount = 1;
                }
                else
                {
                    nRowCount = 0;
                }
            }
        }

        public static void Add_Log(string strName,string strOperation,string strField,string Description)
        {
            OleDbConnection mycon;
            String sConn;
            sConn = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + Application.StartupPath + "\\data\\����.mdb";
            mycon = new OleDbConnection(sConn);
            mycon.Open();
            try
            {
                OleDbCommand pCom;
                sConn = String.Format("insert into OperationLog(Name,LogTime,Field,Operation,LogScribe) values('{0}','{1}','{2}','{3}','{4}')"
                      , strName, DateTime.Now.ToString(), strField,strOperation, Description);
                pCom = new OleDbCommand(sConn, mycon);
                pCom.ExecuteNonQuery();         
                mycon.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("�����־�ļ�����\n" + ex.ToString(), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static OleDbDataAdapter CreateCustomerAdapter(OleDbConnection connection, string strSELECT, string strINSERT, string strDele)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            OleDbCommand command;

            // Create the SelectCommand.
            command = new OleDbCommand(strSELECT, connection);
            adapter.SelectCommand = command;

            // Create the InsertCommand.
            command = new OleDbCommand(strINSERT, connection);
            adapter.InsertCommand = command;

            command = new OleDbCommand(strDele, connection);
            adapter.DeleteCommand = command;

            return adapter;
        }


    }

    public class BusStation : IComparable<BusStation>

    {
        public int ID;
        public string StationName, Direct;
        public double rLength;//�������Ͼ����ľ���

        public BusStation(string StationName,string Direct,int ID)
        {
            this.StationName = StationName;
            this.Direct = Direct;
            this.ID = ID;
        }

        public BusStation(string StationName, string Direct, int ID, double rLength)
        {
            this.StationName = StationName;
            this.Direct = Direct;
            this.ID = ID;
            this.rLength = rLength;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}" ,StationName , Direct);
        }

        #region IComparable<BusStation> ��Ա

        public int CompareTo(BusStation other)
         {
             return this.rLength.CompareTo(other.rLength);
         }
         #endregion

    }
}
