using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Xml;
using System.Runtime.InteropServices;

namespace Businfo.Globe
{
    class ForBusInfo
    {
        #region "��������"
        public const int Pan_Layer = 1; //ͼ���ͣ�����
        public const int Pan_Station = 10; //վ���ͣ�����
        public const int Pan_Road = 11; //��·��ͣ�����
        public const int Pan_Facility = 12; //վ����ʩ��ͣ�����


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
        public const int Map3D_Select = 1065; //����ѡ��
        public const int BusInfo_Help = 1066;//����
        public const int Bus_Select = 1067;//�����ѡ��
        public const int BusInfo_ParaSet = 1068;//��������
        public const int Road_Pause = 1070;//������ʱ����·
        public const int Road_Resume = 1071;//������ʱ��·���б༭
        public const int Table_StationTable = 1069;//·������
        public const int Table_RoadInfoEx = 1073;//һ������
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
        public static string Login_Operation = "" ;//�������
        public static frmMainNew Frm_Main; //��������
        public static Microsoft.Office.Interop.Excel.Application Excel_app = new Microsoft.Office.Interop.Excel.Application();//����������Excel���̹ز��ˣ�ȫ��һ����
        public enum GridSetType {Station_FillPan = 1, Station_FillAll, Station_FillByOBJECTID, Station_FillByStationName, Road_FillPan, Road_FillAll, Road_FillByOBJECTID, Road_FillByStationName};
        public static string Connect_Sql = "";//�������ݿ��ַ���
        public static string Mxd_Name = "";//����mxd
        public static int Connect_Type = 1;//�������ݿ�����1sde,2����

        //�ж��ļ��Ƿ��
        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);
        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);
        public const int OF_READWRITE = 2;
        public const int OF_SHARE_DENY_NONE = 0x40;
        public static readonly IntPtr HFILE_ERROR = new IntPtr(-1);

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
            OleDbConnection mycon = new OleDbConnection(Connect_Sql);
            mycon.Open();
            try
            {
                OleDbCommand pCom;
                if(Connect_Type == 1)
                    pCom = new OleDbCommand(String.Format("insert into sde.OperationLog(Name,LogTime,Field,Operation,LogScribe) values('{0}','{1}','{2}','{3}','{4}')"
                          , strName, DateTime.Now.ToString(), strField, strOperation, Description), mycon);
                else
                    pCom = new OleDbCommand(String.Format("insert into OperationLog(Name,LogTime,Field,Operation,LogScribe) values('{0}','{1}','{2}','{3}','{4}')"
                      , strName, DateTime.Now.ToString(), strField, strOperation, Description), mycon);
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

        public static void StationFill(DataGridView grid, GridSetType emunType, string strQuery, string[] strShow)
        {
            OleDbConnection mycon = new OleDbConnection(Connect_Sql);
            
            mycon.Open();
            string strStationSQL, strRoadSQL;
            if (Connect_Type == 1)
            {
                strStationSQL = @"SELECT  OBJECTID,StationNo,StationName,Direct,DispatchStationThird,StationAlias,StationCharacter,GPSLongtitude,GPSLatitude,GPSHigh,MainSymbol,
                      StationMaterial,DayMass, DayEvacuate, DispatchCompanyFirst, DispatchRouteFirst, DispatchStationFirst,DispatchStationSecond, DispatchCompanyThird, DispatchRouteThird,
                      Constructor, ConstructionTime, StationLand, RodStyleFirst, HourMass,RodMaterialFirst,  RodStyleSecond,  HourEvacuate, RodMaterialSecond,DispatchCompanySecond,  BusShelter, DispatchRouteSecond, 
                      MoveTime, StationStyle, Chair, StationType,TrafficVolume, PictureFirst, PictureSecond, PictureThird, StationArea, ServiceArea, DayTrafficVolume, PassSum, PassRode, 
                       RouteSum, RebuildTime, RemoveTime, StationLong,RodMaterialThird,RodStyleThird,Classify FROM sde.����վ��";//sde.����վ��";

                strRoadSQL = @"SELECT OBJECTID,RoadID,RoadName,Unit,RoadTravel, Company,  RoadType,FirstStartTime, FirstCloseTime, EndStartTime, EndCloseTim, TicketPrice1, 
                      TicketPrice2, TicketPrice3, Picture5, RoadNo, Length, AverageLoadFactor, BusNumber, 
                      Capacity, PassengerSum, PassengerWorkSum, AverageSpeed, NulineCoefficient, 
                      NulineCoefficient2, Picture1, Picture2, Picture3, Picture4,  ServeArea, 
                      AverageLength, HigeLoadFactor, RoadLoad, DirectImbalance, AlternatelyCoefficient, 
                      TimeCoefficient, DayCoefficient, HighHourSect, HighHourArea, HighHourMass, 
                      HighPassengerMass FROM sde.����վ��";//sde.����վ��";
            }
            else
            {
                strStationSQL = @"SELECT  OBJECTID,StationNo,StationName,Direct,DispatchStationThird,StationAlias,StationCharacter,GPSLongtitude,GPSLatitude,GPSHigh,MainSymbol,
                      StationMaterial,DayMass, DayEvacuate, DispatchCompanyFirst, DispatchRouteFirst, DispatchStationFirst,DispatchStationSecond, DispatchCompanyThird, DispatchRouteThird,
                      Constructor, ConstructionTime, StationLand, RodStyleFirst, HourMass,RodMaterialFirst,  RodStyleSecond,  HourEvacuate, RodMaterialSecond,DispatchCompanySecond,  BusShelter, DispatchRouteSecond, 
                      MoveTime, StationStyle, Chair, StationType,TrafficVolume, PictureFirst, PictureSecond, PictureThird, StationArea, ServiceArea, DayTrafficVolume, PassSum, PassRode, 
                       RouteSum, RebuildTime, RemoveTime, StationLong,RodMaterialThird,RodStyleThird,Classify FROM ����վ��";//sde.����վ��";

                strRoadSQL = @"SELECT OBJECTID,RoadID,RoadName, Unit, RoadTravel, Company,  RoadType,FirstStartTime, FirstCloseTime, EndStartTime, EndCloseTim, TicketPrice1, 
                      TicketPrice2, TicketPrice3, Picture5, RoadNo, Length, AverageLoadFactor, BusNumber, 
                      Capacity, PassengerSum, PassengerWorkSum, AverageSpeed, NulineCoefficient, 
                      NulineCoefficient2, Picture1, Picture2, Picture3, Picture4, ServeArea, 
                      AverageLength, HigeLoadFactor, RoadLoad, DirectImbalance, AlternatelyCoefficient, 
                      TimeCoefficient, DayCoefficient, HighHourSect, HighHourArea, HighHourMass, 
                      HighPassengerMass FROM ����վ��";//sde.����վ��";
            }
            try
            {
               switch (emunType)
               {
                   case GridSetType.Station_FillPan:
                       OleDbDataAdapter da = ForBusInfo.CreateCustomerAdapter(mycon, strStationSQL, "", "");
                       da.SelectCommand.ExecuteNonQuery();
                       DataSet ds = new DataSet();
                       int nQueryCount = da.Fill(ds, "Station");
                       if (nQueryCount > 0)
                       {
                           grid.DataSource = ds;
                           grid.DataMember = "Station";
                           foreach (DataGridViewColumn eCol in grid.Columns)
                           {
                               eCol.Visible = false;
                               eCol.ReadOnly = true;
                               eCol.Resizable = DataGridViewTriState.False;
                           }
                           grid.Columns[0].Visible = true;
                           grid.Columns[0].ReadOnly = false;
                           grid.Columns[0].HeaderText = "";
                           grid.Columns[0].Width = 25;
                           grid.Columns[3].Visible = true;
                           grid.Columns[3].HeaderText = "վ������";
                           grid.Columns[3].Width = 150;
                           grid.Columns[4].Visible = true;
                           grid.Columns[4].HeaderText = "����";
                           grid.Columns[4].Width = 55;
                           grid.Columns[5].Visible = true;
                           grid.Columns[5].HeaderText = "վ��˵��";
                           grid.Columns[5].Width = 55;
                       }
                       SetColSortMode(grid, DataGridViewColumnSortMode.NotSortable);
                       SetRowNo(grid);
                       break;
                   case GridSetType.Station_FillAll:
                       da = ForBusInfo.CreateCustomerAdapter(mycon, strStationSQL, "", "");
                       da.SelectCommand.ExecuteNonQuery();
                       ds = new DataSet();
                       nQueryCount = da.Fill(ds, "Station");
                       if (nQueryCount > 0)
                       {
                           grid.DataSource = ds;
                           grid.DataMember = "Station";
                           SetGridHeard(grid, GridSetType.Station_FillAll, strShow);
                           SetColSortMode(grid, DataGridViewColumnSortMode.NotSortable);
                           SetRowNo(grid);
                        }
                       break;
                   case GridSetType.Station_FillByOBJECTID:
                       da = ForBusInfo.CreateCustomerAdapter(mycon, strStationSQL + strQuery, "", "");
                       da.SelectCommand.ExecuteNonQuery();
                    ds = new DataSet();
                    nQueryCount = da.Fill(ds, "Station");
                    if (nQueryCount > 0)
                    {
                        grid.DataSource = ds;
                        grid.DataMember = "Station";
                        SetGridHeard(grid, GridSetType.Station_FillByOBJECTID, strShow);
                        SetColSortMode(grid, DataGridViewColumnSortMode.NotSortable);
                        SetRowNo(grid);

                    }
                    break;
                   case GridSetType.Station_FillByStationName:
                    da = ForBusInfo.CreateCustomerAdapter(mycon, strStationSQL + strQuery, "", "");
                    da.SelectCommand.ExecuteNonQuery();
                    ds = new DataSet();
                    nQueryCount = da.Fill(ds, "Station");
                    if (nQueryCount > 0)
                    {
                        grid.DataSource = ds;
                        grid.DataMember = "Station";
                        foreach (DataGridViewColumn eCol in grid.Columns)
                        {
                            eCol.ReadOnly = true;
                            eCol.Resizable = DataGridViewTriState.False;
                        }
                        grid.Columns[0].ReadOnly = false;
                    }
                    SetGridHeard(grid, GridSetType.Station_FillByOBJECTID, strShow);
                    SetColSortMode(grid, DataGridViewColumnSortMode.NotSortable);
                    SetRowNo(grid);
                    break;
                   case GridSetType.Road_FillPan:
                    da = ForBusInfo.CreateCustomerAdapter(mycon, strRoadSQL, "", "");
                    da.SelectCommand.ExecuteNonQuery();
                    ds = new DataSet();
                    nQueryCount = da.Fill(ds, "Road");
                    if (nQueryCount > 0)
                    {
                        grid.DataSource = ds;
                        grid.DataMember = "Road";
                        foreach (DataGridViewColumn eCol in grid.Columns)
                        {
                            eCol.Visible = false;
                            eCol.ReadOnly = true;
                            eCol.Resizable = DataGridViewTriState.False;
                        }
                        grid.Columns[0].Visible = true;
                        grid.Columns[0].ReadOnly = false;
                        grid.Columns[0].HeaderText = "";
                        grid.Columns[0].Width = 35;
                        grid.Columns[3].Visible = true;
                        grid.Columns[3].HeaderText = "��·����";
                        grid.Columns[3].Width = 40;
                        grid.Columns[4].Visible = true;
                        grid.Columns[4].HeaderText = "��·����";
                        grid.Columns[4].Width = 40;
                        grid.Columns[5].Visible = true;
                        grid.Columns[5].HeaderText = "����";
                        grid.Columns[5].Width = 60;
                    }
                    SetColSortMode(grid, DataGridViewColumnSortMode.NotSortable);
                    SetRowNo(grid);
                    break;
                   case GridSetType.Road_FillAll:
                    da = ForBusInfo.CreateCustomerAdapter(mycon, strRoadSQL, "", "");
                    da.SelectCommand.ExecuteNonQuery();
                    ds = new DataSet();
                    nQueryCount = da.Fill(ds, "Road");
                    if (nQueryCount > 0)
                    {
                        grid.DataSource = ds;
                        grid.DataMember = "Road";
                        SetGridHeard(grid, GridSetType.Road_FillAll, strShow);
                        SetColSortMode(grid, DataGridViewColumnSortMode.NotSortable);
                        SetRowNo(grid);
                    }
                    break;
                   case GridSetType.Road_FillByOBJECTID:
                    da = ForBusInfo.CreateCustomerAdapter(mycon, strRoadSQL + strQuery, "", "");
                    da.SelectCommand.ExecuteNonQuery();
                    ds = new DataSet();
                    nQueryCount = da.Fill(ds, "Road");
                    if (nQueryCount > 0)
                    {
                        grid.DataSource = ds;
                        grid.DataMember = "Road";
                        SetGridHeard(grid, GridSetType.Road_FillByOBJECTID, strShow);
                        SetColSortMode(grid, DataGridViewColumnSortMode.NotSortable);
                        SetRowNo(grid);
                    }
                    break;
                   case GridSetType.Road_FillByStationName:
                    da = ForBusInfo.CreateCustomerAdapter(mycon, strRoadSQL + strQuery, "", "");
                    da.SelectCommand.ExecuteNonQuery();
                    ds = new DataSet();
                    nQueryCount = da.Fill(ds, "Road");
                    if (nQueryCount > 0)
                    {
                        grid.DataSource = ds;
                        grid.DataMember = "Road";
                        foreach (DataGridViewColumn eCol in grid.Columns)
                        {
                            eCol.ReadOnly = true;
                            eCol.Resizable = DataGridViewTriState.False;
                        }
                        grid.Columns[0].ReadOnly = false;
                    }
                    SetColSortMode(grid, DataGridViewColumnSortMode.NotSortable);
                    SetRowNo(grid);
                    break;
               }
               mycon.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("StationFill ��������\n" + ex.ToString(), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void SetGridHeard(DataGridView grid, GridSetType emunType, string[] strShow)
        {
            switch (emunType)
            {
                case GridSetType.Station_FillAll:
                case GridSetType.Station_FillByOBJECTID:
                    foreach (DataGridViewColumn eCol in grid.Columns)
                    {
                        eCol.ReadOnly = true;
                        eCol.Resizable = DataGridViewTriState.False;
                        eCol.Visible = false;
                    }
                    grid.Columns[0].Visible = true;
                    grid.Columns[0].ReadOnly = false;
                    grid.Columns[0].HeaderText = "";

                    if (strShow[0] == "")
                    {
                        //for (int i = 3; i < 53; i++)//��ʱȫ���ſ�
                        for (int i = 3; i < 15; i++)
                        {
                            grid.Columns[i].Visible = true;
                        }
                      
                    }
                    else
                    {
                        grid.Columns[3].Visible = true;
                        grid.Columns[4].Visible = true;
                        grid.Columns[5].Visible = true;
                        grid.Columns[6].Visible = true;
                        for (int i = 15; i < 53; i++)
                        {
                            grid.Columns[i].Visible = true;
                        }
                    }
                    grid.Columns[3].HeaderText = "վ������";
                    grid.Columns[4].HeaderText = "����";
                    grid.Columns[5].HeaderText = "վ��˵��";//ԭ���ǵ���վ��
                    grid.Columns[6].HeaderText = "��վ��";
                    grid.Columns[6].Frozen = true;
                    grid.Columns[7].HeaderText = "վ�����ڵ�·";
                    grid.Columns[8].HeaderText = "GPS����";
                    grid.Columns[9].HeaderText = "GPSγ��";
                    grid.Columns[10].HeaderText = "GPS�߶�";
                    grid.Columns[11].HeaderText = "��Ҫ��ʶ��";//վ�˲���
                    grid.Columns[12].HeaderText = "��Ҫ��ʶ��2";//վ�Ʋ���
                    grid.Columns[13].HeaderText = "������·��";//ȫ�켯����
                    grid.Columns[14].HeaderText = "������·";//ȫ����ɢ��
                    grid.Columns[15].HeaderText = "��·�Ʋ���";//ԭ���ǵ��ȹ�˾
                    grid.Columns[16].HeaderText = "��·�Ƴߴ�";//ԭ���ǵ�����·
                    grid.Columns[17].HeaderText = "��·��������λ";//ԭ���ǵ���վ��
                    grid.Columns[18].HeaderText = "��·�Ʋ���2";//ԭ���ǵ���վ��
                    grid.Columns[19].HeaderText = "��·�Ƴߴ�2";//ԭ���ǵ��ȹ�˾
                    grid.Columns[20].HeaderText = "��·��������λ2";//ԭ���ǵ�����·
                    grid.Columns[21].HeaderText = "��·�Ʋ���3";//ԭ����"������";
                    grid.Columns[22].HeaderText = "��·�Ƴߴ�3";//ԭ����"����ʱ��";
                    grid.Columns[23].HeaderText = "��·��������λ3";//ԭ��"վ���õ�";
                    grid.Columns[24].HeaderText = "վ����ʽ";
                    grid.Columns[25].HeaderText = "վ�˳ߴ�";//Сʱ������
                    grid.Columns[26].HeaderText = "վ��ά����λ";//վ�˲���
                    grid.Columns[27].HeaderText = "վ����ʽ2";
                    grid.Columns[28].HeaderText = "վ�˳ߴ�2";//Сʱ��ɢ��
                    grid.Columns[29].HeaderText = "վ��ά����λ2";//ԭ���� վ�˲���2
                    grid.Columns[30].HeaderText = "��ͤ�����";//ԭ���� ���ȹ�˾2
                    grid.Columns[31].HeaderText = "��ͤС����";
                    grid.Columns[32].HeaderText = "��ͤά����λ";//ԭ���� ������·2
                    grid.Columns[33].HeaderText = "��ͤ����ʱ��";//ԭ���� Ǩ�ư�װʱ��

                    grid.Columns[34].HeaderText = "վ������";
                    grid.Columns[35].HeaderText = "���ް��";
                    grid.Columns[36].HeaderText = "վ������";
                    grid.Columns[37].HeaderText = "��ɢ���߷�";
                    grid.Columns[38].HeaderText = "ͼƬһ";
                    grid.Columns[39].HeaderText = "ͼƬ��";
                    grid.Columns[40].HeaderText = "ͼƬ��";
                    grid.Columns[41].HeaderText = "վ�����";
                    grid.Columns[42].HeaderText = "վ�������";
                    grid.Columns[43].HeaderText = "�켯ɢ���߷�";
                    grid.Columns[44].HeaderText = "ͨ����·��";
                    grid.Columns[45].HeaderText = "ͨ������";
                    grid.Columns[46].HeaderText = "��·��";
                    grid.Columns[47].HeaderText = "�Ľ�ʱ��";
                    grid.Columns[48].HeaderText = "���ʱ��";
                    grid.Columns[49].HeaderText = "��ͤ����";
                    grid.Columns[50].HeaderText = "վ�˲���3";
                    grid.Columns[51].HeaderText = "վ����ʽ3";
                    grid.Columns[52].HeaderText = "վ�����";
                    break;
                case GridSetType.Road_FillAll:
                case GridSetType.Road_FillByOBJECTID:
                    foreach (DataGridViewColumn eCol in grid.Columns)
                    {
                        eCol.ReadOnly = true;
                        eCol.Resizable = DataGridViewTriState.False;
                    }
                    grid.Columns[0].ReadOnly = false;
                    grid.Columns[0].HeaderText = "";
                    grid.Columns[1].Visible = false;
                    grid.Columns[2].Visible = false;
                    grid.Columns[3].HeaderText = "��·����";
                    grid.Columns[4].HeaderText = "��·����";
                    grid.Columns[5].HeaderText = "��·�г�";
                    grid.Columns[5].Frozen = true;
                    grid.Columns[6].HeaderText = "������˾";
                    grid.Columns[7].HeaderText = "��·����";
                    grid.Columns[8].HeaderText = "��վ����ʱ��";
                    grid.Columns[9].HeaderText = "��վ�հ�ʱ��";
                    grid.Columns[10].HeaderText = "ĩվ����ʱ��";
                    grid.Columns[11].HeaderText = "ĩվ�հ�ʱ��";
                    grid.Columns[12].HeaderText = "Ʊ��1";
                    grid.Columns[13].HeaderText = "Ʊ��2";
                    grid.Columns[14].HeaderText = "Ʊ��3";
                    grid.Columns[15].HeaderText = "Ʊ��4";//ԭ���� ͼƬ5
                    grid.Columns[16].HeaderText = "��·���";
                    grid.Columns[17].HeaderText = "����";
                    grid.Columns[18].HeaderText = "ƽ��������";
                    grid.Columns[19].HeaderText = "��Ӫ����";
                    grid.Columns[20].HeaderText = "�����䱸";
                    grid.Columns[21].HeaderText = "������";
                    grid.Columns[22].HeaderText = "���˹�����";
                    grid.Columns[23].HeaderText = "ƽ������";
                    grid.Columns[24].HeaderText = "��ֱ��ϵ��";
                    grid.Columns[25].HeaderText = "��ֱ��ϵ��2";
                    grid.Columns[26].HeaderText = "ͼƬ1";
                    grid.Columns[27].HeaderText = "ͼƬ2";
                    grid.Columns[28].HeaderText = "ͼƬ3";
                    grid.Columns[29].HeaderText = "ͼƬ4";
                    grid.Columns[30].HeaderText = "�������";
                    grid.Columns[31].HeaderText = "ƽ���˾�";
                    grid.Columns[32].HeaderText = "�߷�������";
                    grid.Columns[33].HeaderText = "��·����";
                    grid.Columns[34].HeaderText = "���򲻾���";
                    grid.Columns[35].HeaderText = "����ϵ��";
                    grid.Columns[36].HeaderText = "ʱ����ϵ��";
                    grid.Columns[37].HeaderText = "�첻��ϵ��";
                    grid.Columns[38].HeaderText = "�߷�Сʱ��";
                    grid.Columns[39].HeaderText = "�߷�Сʱ��";
                    grid.Columns[40].HeaderText = "�߷�Сʱ��";
                    grid.Columns[41].HeaderText = "�߷������";
                    break;
            }
        }

        //����������ģʽ
        public static void SetColSortMode(DataGridView grid, DataGridViewColumnSortMode ColSortMode)
        {
            foreach (DataGridViewColumn eColumn in grid.Columns)
            {

                eColumn.SortMode = ColSortMode;
            }
        }
        //��rowheard������
        public static void SetRowNo(DataGridView grid)
        {
            int nNum = 1;
            foreach (DataGridViewRow eRow in grid.Rows)
            {
                eRow.HeaderCell.Value = nNum++.ToString(); 
            }
        }

        public static void SaveXml(Form myForm) //���洰��textbox����Ϊxml 
        {
             string FileName = Application.ExecutablePath;
             string xmlFileName = Path.ChangeExtension(FileName, ".xml");
             if (File.Exists(xmlFileName))
                 File.Delete(xmlFileName);
             XmlTextWriter tw = new XmlTextWriter(xmlFileName, null);
             tw.Formatting = Formatting.Indented;
             tw.WriteStartDocument();
             tw.WriteStartElement("savetextconent");
             tw.WriteAttributeString("Text", "SaveText");
             foreach (object obj in myForm.Controls)
             {
                 TextBox myextBox;
                 if (obj is TextBox)
                 {
                     myextBox = (TextBox)obj;
                     tw.WriteElementString(myextBox.Name, myextBox.Text);
                 }
                 else
                 {
                     if (obj is Form)//�ݹ鱣�洰���ڵ�xml
                         SaveXml((Form)obj);
                 }
             }
                tw.WriteEndElement();
             tw.WriteEndDocument();
             tw.Flush();
             tw.Close();
        }

        public static void loadXml(Form myForm) //�����ϴα����xml������textbox 
         {
             string FileName = Application.ExecutablePath;
             string xmlFileName = Path.ChangeExtension(FileName, ".xml");
             if (!File.Exists(xmlFileName)) return;
             XmlDocument xmldoc = new XmlDocument();
             xmldoc.Load(xmlFileName);
             XmlElement root = xmldoc.DocumentElement;
             XmlNodeList xnl = root.ChildNodes;
             for (int i = 0; i < xnl.Count; i++)
             {
                 object textobj = myForm.GetType().GetField(xnl.Item(i).Name,
                 System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(myForm);
                 if (textobj != null)
                 {
                        TextBox textBox = textobj as TextBox;
                     textBox.Text = xnl.Item(i).InnerText;
                 }
             }
         }

        //������дINI�ļ���API����
        [DllImport("kernel32")]
        public static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);

        public static string GetProfileString(string section, string key, string filePath)
        {
            Byte[] Buffer = new Byte[65535];
            int bufLen = GetPrivateProfileString(section, key, "", Buffer, Buffer.GetUpperBound(0), filePath);
            //�����趨0��ϵͳĬ�ϵĴ���ҳ���ı��뷽ʽ�������޷�֧������
            string s = Encoding.GetEncoding(0).GetString(Buffer);
            s = s.Substring(0, bufLen);

            return s.Trim();
        }

        #region ��DataGridView�е����ݵ��뵽Excel�У�DataGridView���������Դ
        /// <summary>
        /// ��DataGridView�е����ݵ��뵽Excel�У�DataGridView���������Դ
        /// </summary>
        /// <param name="datagridview">DataGridView</param>
        /// <param name="SheetName">Excel sheet title</param>
        /// <param name="bVisble">�Ƿ�������ɼ���</param>
        /// <param name="nBegin">׼���������ʼ�кţ����㿪ʼ����</param>
        public static void DataGridView2Excel(System.Windows.Forms.DataGridView datagridview, string SheetName,bool bVisble,int nBegin)
        {
            int iRows = 0;
            int iCols = 0;
            int iTrueCols = 0;
            int nStarCol = 0;
            if (Excel_app == null)
            {
                Excel_app = new Microsoft.Office.Interop.Excel.Application();
            }
            Microsoft.Office.Interop.Excel.Workbook wb = Excel_app.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet ws = null;
            if (wb.Worksheets.Count > 0)
            {
                ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets.get_Item(1);
            }
            else
            {
                wb.Worksheets.Add(System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
            System.Reflection.Missing.Value);
                ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets.get_Item(1);
            }
            if (ws != null)
            {
                if (SheetName.Trim() != "")
                {
                    ws.Name = SheetName;
                }
                iRows = datagridview.Rows.Count + 1;������//������ͷ��
                iTrueCols = datagridview.Columns.Count;�� //�������ص��У�һ���ж�����
                //��������ʡ��Visible = false����
                for (int i = 0; i < datagridview.Columns.Count; i++)
                {
                    if (bVisble && datagridview.Columns[i].Visible == false)
                        continue;
                    iCols++;
                    if (iCols == nBegin)
                    {
                        nStarCol = i + 1;//�жϵ�bVisble==trueʱ��׼������Ŀɼ�����ʼ���кţ�Ҫ��һ���ų���ǰ�ɼ��С�
                    }
                }
                iCols = iCols - nBegin;//�������������
                //string[,] dimArray = new string[iRows + 1, iCols];    // ��Ҫ�޸�string[iRows + 1, iCols]Ϊstring[iRows, iCols]
                string[,] dimArray = new string[iRows, iCols];// �޸ĺ�

                for (int j = nStarCol, k = 0; j < iTrueCols; j++)
                {
                    //�ж�ʡ��Visible = false����
                    if (bVisble && datagridview.Columns[j].Visible == false)
                        continue;

                    dimArray[0, k] = datagridview.Columns[j].HeaderText;//�õ�����
                    k++;

                }
               
                               
                // for (int i = 0; i < iRows; i++) �޸�ǰ
                for (int i = 0; i < iRows - 1; i++) // �޸ĺ�
                {
                    for (int j = nStarCol, k = 0; j < iTrueCols; j++)
                    {
                        //ʡ��Visible = false����
                        if (bVisble && datagridview.Columns[j].Visible == false)
                            continue;

                            dimArray[i + 1, k] = datagridview.Rows[i].Cells[j].Value.ToString();//�õ���������
                            k++;
                    }
                }
                /* �޸�ǰ
                ws.get_Range(ws.Cells[1, 1], ws.Cells[iRows + 1, iCols]).Value2 = dimArray;
                ws.get_Range(ws.Cells[1, 1], ws.Cells[1, iCols]).Font.Bold = true;
                ws.get_Range(ws.Cells[1, 1], ws.Cells[iRows + 1, iCols]).Font.Size = 10.0;
                ws.get_Range(ws.Cells[1, 1], ws.Cells[iRows + 1, iCols]).RowHeight = 14.25;
                 * */
                ws.get_Range(ws.Cells[1, 1], ws.Cells[iRows, iCols]).Value2 = dimArray;//ȫ�����ݸ�ֵ
                ws.get_Range(ws.Cells[1, 1], ws.Cells[1, iCols]).Font.Bold = true;//��ͷ
                ws.get_Range(ws.Cells[1, 1], ws.Cells[iRows, iCols]).Font.Size = 10.0;
                ws.get_Range(ws.Cells[1, 1], ws.Cells[iRows, iCols]).RowHeight = 14.25;
                //for (int j = 0, k = 0; j < iTrueCols; j++)
                //{
                //    //ʡ��Visible = false����
                //    if (datagridview.Columns[j].Visible)
                //    {
                //        ws.get_Range(ws.Cells[1, k + 1], ws.Cells[1, k + 1]).ColumnWidth =
                //            (datagridview.Columns[j].Width / 8.4) > 255 ? 255 : (datagridview.Columns[j].Width / 8.4);
                //        k++;
                //    }
                //}
            }
            Excel_app.Visible = true;
        }
        #endregion

        public static void AppIni(int nType)
        {
            Connect_Type = nType;
            string strType = Application.StartupPath;//GetProfileString("Businfo", "DataPos", Application.StartupPath + "\\Businfo.ini");
            if (Connect_Type == 1)
            {
                Connect_Sql = "Provider=sqloledb;Data Source = 192.168.133.182;Initial Catalog=sde;User Id = sa;Password = 123";
                //Connect_Sql = "Provider=sqloledb;Data Source = 172.16.34.120;Initial Catalog=sde;User Id = sa;Password = sa";
                Mxd_Name = strType + "\\data\\DataSDE.mxd";
                    
            }
            else
            {
                Connect_Sql = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + strType + "\\data\\����.mdb";
                Mxd_Name = strType + "\\data\\Data.mxd";
            }
        }
    }

    public class BusStation : IComparable<BusStation>

    {
        public int ID;
        public string StationName, Direct,StationExplain;//ԭ���� ����վ��3��������վ��˵��
        public double rLength;//�������Ͼ����ľ���
        public string StationCharacter;//վ�����ڵ�·

        public BusStation(string StationName,string Direct,int ID)
        {
            this.StationName = StationName;
            this.Direct = Direct;
            this.ID = ID;
        }

        public BusStation(string StationName, string Direct, int ID, double rLength,string StationExplain,string StationCharacter)
        {
            this.StationName = StationName;
            this.Direct = Direct;
            this.ID = ID;
            this.rLength = rLength;
            this.StationExplain = StationExplain;
            this.StationCharacter = StationCharacter;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}", StationName, Direct, StationExplain);
        }

        #region IComparable<BusStation> ��Ա

        public int CompareTo(BusStation other)
         {
             return this.rLength.CompareTo(other.rLength);
         }
         #endregion

    }

  

    // public class IniFiles
    //{
    //    public string FileName; //INI�ļ���
    //    //string path   =   System.IO.Path.Combine(Application.StartupPath,"pos.ini");

    //    //������дINI�ļ���API����
    //    [DllImport("kernel32")]
    //    private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
    //    [DllImport("kernel32")]
    //    private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);

    //    //��Ĺ��캯��������INI�ļ���
    //    public IniFiles(string AFileName)
    //    {
    //        // �ж��ļ��Ƿ����
    //        FileInfo fileInfo = new FileInfo(AFileName);
    //        //Todo:����ö�ٵ��÷�
    //        if ((!fileInfo.Exists))
    //        { //|| (FileAttributes.Directory in fileInfo.Attributes))
    //            //�ļ������ڣ������ļ�
    //            System.IO.StreamWriter sw = new System.IO.StreamWriter(AFileName, false, System.Text.Encoding.Default);
    //            try
    //            {
    //                sw.Write("#������õ���");
    //                sw.Close();
    //            }
    //            catch
    //            {
    //                throw (new ApplicationException("Ini�ļ�������"));
    //            }
    //        }
    //        //��������ȫ·�������������·��
    //        FileName = fileInfo.FullName;
    //    }

    //    //дINI�ļ�
    //    public void WriteString(string Section, string Ident, string Value)
    //    {
    //        if (!WritePrivateProfileString(Section, Ident, Value, FileName))
    //        {

    //            throw (new ApplicationException("дIni�ļ�����"));
    //        }
    //    }

    //    //��ȡINI�ļ�ָ��
    //    public string ReadString(string Section, string Ident, string Default)
    //    {
    //        Byte[] Buffer = new Byte[65535];
    //        int bufLen = GetPrivateProfileString(Section, Ident, Default, Buffer, Buffer.GetUpperBound(0), FileName);
    //        //�����趨0��ϵͳĬ�ϵĴ���ҳ���ı��뷽ʽ�������޷�֧������
    //        string s = Encoding.GetEncoding(0).GetString(Buffer);
    //        s = s.Substring(0, bufLen);
    //        return s.Trim();
    //    }

    //    //������
    //    public int ReadInteger(string Section, string Ident, int Default)
    //    {
    //        string intStr = ReadString(Section, Ident, Convert.ToString(Default));
    //        try
    //        {
    //            return Convert.ToInt32(intStr);
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine(ex.Message);
    //            return Default;
    //        }
    //    }

    //    //д����
    //    public void WriteInteger(string Section, string Ident, int Value)
    //    {
    //        WriteString(Section, Ident, Value.ToString());
    //    }

    //    //������
    //    public bool ReadBool(string Section, string Ident, bool Default)
    //    {
    //        try
    //        {
    //            return Convert.ToBoolean(ReadString(Section, Ident, Convert.ToString(Default)));
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine(ex.Message);
    //            return Default;
    //        }
    //    }

    //    //дBool
    //    public void WriteBool(string Section, string Ident, bool Value)
    //    {
    //        WriteString(Section, Ident, Convert.ToString(Value));
    //    }

    //    //��Ini�ļ��У���ָ����Section�����е�����Ident��ӵ��б���
    //    public void ReadSection(string Section, StringCollection Idents)
    //    {
    //        Byte[] Buffer = new Byte[16384];
    //        //Idents.Clear();

    //        int bufLen = GetPrivateProfileString(Section, null, null, Buffer, Buffer.GetUpperBound(0), FileName);
    //        //��Section���н���
    //        GetStringsFromBuffer(Buffer, bufLen, Idents);
    //    }

    //    private void GetStringsFromBuffer(Byte[] Buffer, int bufLen, StringCollection Strings)
    //    {
    //        Strings.Clear();
    //        if (bufLen != 0)
    //        {
    //            int start = 0;
    //            for (int i = 0; i < bufLen; i++)
    //            {
    //                if ((Buffer[i] == 0) && ((i - start) > 0))
    //                {
    //                    String s = Encoding.GetEncoding(0).GetString(Buffer, start, i - start);
    //                    Strings.Add(s);
    //                    start = i + 1;
    //                }
    //            }
    //        }
    //    }

    //    //��Ini�ļ��У���ȡ���е�Sections������
    //    public void ReadSections(StringCollection SectionList)
    //    {
    //        //Note:�������Bytes��ʵ�֣�StringBuilderֻ��ȡ����һ��Section
    //        byte[] Buffer = new byte[65535];
    //        int bufLen = 0;
    //        bufLen = GetPrivateProfileString(null, null, null, Buffer,
    //        Buffer.GetUpperBound(0), FileName);
    //        GetStringsFromBuffer(Buffer, bufLen, SectionList);
    //    }

    //    //��ȡָ����Section������Value���б���
    //    public void ReadSectionValues(string Section, NameValueCollection Values)
    //    {
    //        StringCollection KeyList = new StringCollection();
    //        ReadSection(Section, KeyList);
    //        Values.Clear();
    //        foreach (string key in KeyList)
    //        {
    //            Values.Add(key, ReadString(Section, key, ""));
    //        }
    //    }

    //    /**/
    //    ////��ȡָ����Section������Value���б��У�
    //    //public void ReadSectionValues(string Section, NameValueCollection Values,char splitString)
    //    //{�� string sectionValue;
    //    //����string[] sectionValueSplit;
    //    //����StringCollection KeyList = new StringCollection();
    //    //����ReadSection(Section, KeyList);
    //    //����Values.Clear();
    //    //����foreach (string key in KeyList)
    //    //����{
    //    //��������sectionValue=ReadString(Section, key, "");
    //    //��������sectionValueSplit=sectionValue.Split(splitString);
    //    //��������Values.Add(key, sectionValueSplit[0].ToString(),sectionValueSplit[1].ToString());
    //    //����}
    //    //}

    //    //���ĳ��Section
    //    public void EraseSection(string Section)
    //    {
    //        if (!WritePrivateProfileString(Section, null, null, FileName))
    //        {
    //            throw (new ApplicationException("�޷����Ini�ļ��е�Section"));
    //        }
    //    }

    //    //ɾ��ĳ��Section�µļ�
    //    public void DeleteKey(string Section, string Ident)
    //    {
    //        WritePrivateProfileString(Section, Ident, null, FileName);
    //    }

    //    //Note:����Win9X����˵��Ҫʵ��UpdateFile�����������е�����д���ļ�
    //    //��Win NT, 2000��XP�ϣ�����ֱ��д�ļ���û�л��壬���ԣ�����ʵ��UpdateFile
    //    //ִ�����Ini�ļ����޸�֮��Ӧ�õ��ñ��������»�������
    //    public void UpdateFile()
    //    {
    //        WritePrivateProfileString(null, null, null, FileName);
    //    }

    //    //���ĳ��Section�µ�ĳ����ֵ�Ƿ����
    //    public bool ValueExists(string Section, string Ident)
    //    {
    //        StringCollection Idents = new StringCollection();
    //        ReadSection(Section, Idents);
    //        return Idents.IndexOf(Ident) > -1;
    //    }

    //    //ȷ����Դ���ͷ�
    //    ~IniFiles()
    //    {
    //        UpdateFile();
    //    }
    //}
}

