using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Businfo.Globe
{
    class ForBusInfo
    {
        #region "��������"
        public const int Pan_Layer = 1; //ͼ���ͣ�����
        public const int Pan_Station = 10; //վ���ͣ�����
        public const int Pan_Road = 11; //վ�߿�ͣ�����

        public const int Bus_Add = 1041; //վ�����
        public const int Bus_Supervise = 1042; //վ�����
        public const int Bus_Dele = 1043; //վ��ɾ��
        public const int Bus_Query = 1044; //վ���ѯ
        public const int Bus_Move = 1045; //վ���ƶ�
        public const int Bus_Edit = 1046; //���Ա༭
        public const int Bus_BackUp = 1047; //վ�㱸��
        public const int Bus_Recover = 1048; //վ��ָ�
        public const int Bus_Pano = 1049; //վ��ȫ��
        public const int Road_Supervise = 1050; //��·����
        public const int Road_Add = 1051; //��·���
        public const int Road_Dele = 1052; //��·ɾ��
        public const int Road_Query = 1053; //��·��ѯ
        public const int Road_Edit = 1054; //���Ա༭
        public const int Road_BackUp = 1055; //��·����
        public const int Road_Recover = 1056; //��·�ָ�
        public const int Road_Associate = 1057; //����վ��
        public const int Table_Supervise = 1058; //�������

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

        public const int BusInfo_Layer = 1059; //ͼ���л�
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
    }

    class BusStation
    {
        public int ID;
        string StationName,Direct;

        public BusStation(string StationName,string Direct,int ID)
        {
            this.StationName = StationName;
            this.Direct = Direct;
            this.ID = ID;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}" ,StationName , Direct);
        }
    }
}
