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
        #region "常量定义"
        public const int Pan_Layer = 1; //图层可停靠面板
        public const int Pan_Station = 10; //站点可停靠面板
        public const int Pan_Road = 11; //线路可停靠面板
        public const int Pan_Facility = 12; //站点设施可停靠面板


        public const int Bus_Add = 1041; //站点添加
        public const int Bus_Supervise = 1042; //站点管理目录栏
        public const int Bus_Dele = 1043; //站点删除
        public const int Bus_Query = 1044; //站点查询
        public const int Bus_Move = 1045; //站点移动
        public const int Bus_Edit = 1046; //属性编辑
        public const int Bus_BackUp = 1047; //站点备份
        public const int Bus_Recover = 1048; //站点恢复
        public const int Bus_Pano = 1049; //站点全景
        public const int Road_Supervise = 1050; //线路管理目录栏
        public const int Road_Add = 1051; //线路添加
        public const int Road_Dele = 1052; //线路删除
        public const int Road_Query = 1053; //线路查询
        public const int Road_Edit = 1054; //属性编辑
        public const int Road_BackUp = 1055; //线路备份
        public const int Road_Recover = 1056; //线路恢复
        public const int Road_Associate = 1057; //关联站点
        public const int Table_Supervise = 1058; //报表输出目录栏
        public const int BusInfo_Layer = 1059; //图层切换
        public const int Table_RoadExport = 1061; //报表输出
        public const int Table_Operation = 1062; //查看记录
        public const int Road_End = 1063; //完成线路添加
        public const int Road_Reversed = 1064; //线路反向
        public const int Map3D_Select = 1065; //拉框选择
        public const int BusInfo_Help = 1066;//帮助
        public const int Bus_Select = 1067;//多变形选择
        public const int BusInfo_ParaSet = 1068;//参数设置
        public const int Road_Pause = 1070;//保存临时的线路
        public const int Road_Resume = 1071;//继续临时线路进行编辑
        public const int Table_StationTable = 1069;//路单导出
        public const int Table_RoadInfoEx = 1073;//一览表导出
        public const int Map3D_ZoomIn = 10211; //放大
        public const int Map3D_ZoomOut = 10212; //缩小
        public const int Map3D_Full = 10213; //全图
        public const int Map3D_Pan = 10214; //漫游
        public const int Map3D_Vector = 10215; //加载矢量
        public const int Map3D_OpenMode = 10216; //
        public const int Map3D_PreView = 10217; //上一屏
        public const int Map3D_NextView = 10218; //下一屏
        public const int Map3D_Reflash = 10219; //刷新
        public const int Map3D_Distance = 10222; //距离
        public const int Map3D_Area = 10224; //面积

        public static string Login_name = "admin"; //登录名
        public static string Login_Operation = "" ;//允许操作
        public static frmMainNew Frm_Main; //主窗体类
        public static Microsoft.Office.Interop.Excel.Application Excel_app = new Microsoft.Office.Interop.Excel.Application();//由于老是有Excel进程关不了，全局一个。
        public enum GridSetType {Station_FillPan = 1, Station_FillAll, Station_FillByOBJECTID, Station_FillByStationName, Road_FillPan, Road_FillAll, Road_FillByOBJECTID, Road_FillByStationName};
        public static string Connect_Sql = "";//链接数据库字符串
        public static string Mxd_Name = "";//加载mxd
        public static int Connect_Type = 1;//链接数据库类型1sde,2本地

        //判断文件是否打开
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
                MessageBox.Show("添加日志文件出错\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                       RouteSum, RebuildTime, RemoveTime, StationLong,RodMaterialThird,RodStyleThird,Classify FROM sde.公交站点";//sde.公交站点";

                strRoadSQL = @"SELECT OBJECTID,RoadID,RoadName,Unit,RoadTravel, Company,  RoadType,FirstStartTime, FirstCloseTime, EndStartTime, EndCloseTim, TicketPrice1, 
                      TicketPrice2, TicketPrice3, Picture5, RoadNo, Length, AverageLoadFactor, BusNumber, 
                      Capacity, PassengerSum, PassengerWorkSum, AverageSpeed, NulineCoefficient, 
                      NulineCoefficient2, Picture1, Picture2, Picture3, Picture4,  ServeArea, 
                      AverageLength, HigeLoadFactor, RoadLoad, DirectImbalance, AlternatelyCoefficient, 
                      TimeCoefficient, DayCoefficient, HighHourSect, HighHourArea, HighHourMass, 
                      HighPassengerMass FROM sde.公交站线";//sde.公交站线";
            }
            else
            {
                strStationSQL = @"SELECT  OBJECTID,StationNo,StationName,Direct,DispatchStationThird,StationAlias,StationCharacter,GPSLongtitude,GPSLatitude,GPSHigh,MainSymbol,
                      StationMaterial,DayMass, DayEvacuate, DispatchCompanyFirst, DispatchRouteFirst, DispatchStationFirst,DispatchStationSecond, DispatchCompanyThird, DispatchRouteThird,
                      Constructor, ConstructionTime, StationLand, RodStyleFirst, HourMass,RodMaterialFirst,  RodStyleSecond,  HourEvacuate, RodMaterialSecond,DispatchCompanySecond,  BusShelter, DispatchRouteSecond, 
                      MoveTime, StationStyle, Chair, StationType,TrafficVolume, PictureFirst, PictureSecond, PictureThird, StationArea, ServiceArea, DayTrafficVolume, PassSum, PassRode, 
                       RouteSum, RebuildTime, RemoveTime, StationLong,RodMaterialThird,RodStyleThird,Classify FROM 公交站点";//sde.公交站点";

                strRoadSQL = @"SELECT OBJECTID,RoadID,RoadName, Unit, RoadTravel, Company,  RoadType,FirstStartTime, FirstCloseTime, EndStartTime, EndCloseTim, TicketPrice1, 
                      TicketPrice2, TicketPrice3, Picture5, RoadNo, Length, AverageLoadFactor, BusNumber, 
                      Capacity, PassengerSum, PassengerWorkSum, AverageSpeed, NulineCoefficient, 
                      NulineCoefficient2, Picture1, Picture2, Picture3, Picture4, ServeArea, 
                      AverageLength, HigeLoadFactor, RoadLoad, DirectImbalance, AlternatelyCoefficient, 
                      TimeCoefficient, DayCoefficient, HighHourSect, HighHourArea, HighHourMass, 
                      HighPassengerMass FROM 公交站线";//sde.公交站线";
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
                           grid.Columns[3].HeaderText = "站点名称";
                           grid.Columns[3].Width = 150;
                           grid.Columns[4].Visible = true;
                           grid.Columns[4].HeaderText = "行向";
                           grid.Columns[4].Width = 55;
                           grid.Columns[5].Visible = true;
                           grid.Columns[5].HeaderText = "站点说明";
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
                        grid.Columns[3].HeaderText = "线路名称";
                        grid.Columns[3].Width = 40;
                        grid.Columns[4].Visible = true;
                        grid.Columns[4].HeaderText = "线路属性";
                        grid.Columns[4].Width = 40;
                        grid.Columns[5].Visible = true;
                        grid.Columns[5].HeaderText = "行向";
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
                MessageBox.Show("StationFill 函数出错！\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        //for (int i = 3; i < 53; i++)//临时全部放开
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
                    grid.Columns[3].HeaderText = "站点名称";
                    grid.Columns[4].HeaderText = "行向";
                    grid.Columns[5].HeaderText = "站点说明";//原来是调度站道
                    grid.Columns[6].HeaderText = "副站名";
                    grid.Columns[6].Frozen = true;
                    grid.Columns[7].HeaderText = "站点所在道路";
                    grid.Columns[8].HeaderText = "GPS经度";
                    grid.Columns[9].HeaderText = "GPS纬度";
                    grid.Columns[10].HeaderText = "GPS高度";
                    grid.Columns[11].HeaderText = "主要标识物";//站杆材质
                    grid.Columns[12].HeaderText = "主要标识物2";//站牌材质
                    grid.Columns[13].HeaderText = "经过线路数";//全天集结量
                    grid.Columns[14].HeaderText = "经过线路";//全天疏散量
                    grid.Columns[15].HeaderText = "线路牌材质";//原来是调度公司
                    grid.Columns[16].HeaderText = "线路牌尺寸";//原来是调度线路
                    grid.Columns[17].HeaderText = "线路牌制作单位";//原来是调度站道
                    grid.Columns[18].HeaderText = "线路牌材质2";//原来是调度站道
                    grid.Columns[19].HeaderText = "线路牌尺寸2";//原来是调度公司
                    grid.Columns[20].HeaderText = "线路牌制作单位2";//原来是调度线路
                    grid.Columns[21].HeaderText = "线路牌材质3";//原来是"建设商";
                    grid.Columns[22].HeaderText = "线路牌尺寸3";//原来是"建设时间";
                    grid.Columns[23].HeaderText = "线路牌制作单位3";//原来"站点用地";
                    grid.Columns[24].HeaderText = "站杆样式";
                    grid.Columns[25].HeaderText = "站杆尺寸";//小时集结量
                    grid.Columns[26].HeaderText = "站杆维护单位";//站杆材质
                    grid.Columns[27].HeaderText = "站杆样式2";
                    grid.Columns[28].HeaderText = "站杆尺寸2";//小时疏散量
                    grid.Columns[29].HeaderText = "站杆维护单位2";//原来是 站杆材质2
                    grid.Columns[30].HeaderText = "候车亭大板数";//原来是 调度公司2
                    grid.Columns[31].HeaderText = "候车亭小板数";
                    grid.Columns[32].HeaderText = "候车亭维护单位";//原来是 调度线路2
                    grid.Columns[33].HeaderText = "候车亭建设时间";//原来是 迁移安装时间

                    grid.Columns[34].HeaderText = "站点类型";
                    grid.Columns[35].HeaderText = "有无板凳";
                    grid.Columns[36].HeaderText = "站点类型";
                    grid.Columns[37].HeaderText = "集散量高峰";
                    grid.Columns[38].HeaderText = "图片一";
                    grid.Columns[39].HeaderText = "图片二";
                    grid.Columns[40].HeaderText = "图片三";
                    grid.Columns[41].HeaderText = "站点面积";
                    grid.Columns[42].HeaderText = "站服务面积";
                    grid.Columns[43].HeaderText = "天集散量高峰";
                    grid.Columns[44].HeaderText = "通过线路数";
                    grid.Columns[45].HeaderText = "通道条数";
                    grid.Columns[46].HeaderText = "线路数";
                    grid.Columns[47].HeaderText = "改建时间";
                    grid.Columns[48].HeaderText = "拆除时间";
                    grid.Columns[49].HeaderText = "候车亭长度";
                    grid.Columns[50].HeaderText = "站杆材质3";
                    grid.Columns[51].HeaderText = "站杆样式3";
                    grid.Columns[52].HeaderText = "站点类别";
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
                    grid.Columns[3].HeaderText = "线路名称";
                    grid.Columns[4].HeaderText = "线路属性";
                    grid.Columns[5].HeaderText = "线路行程";
                    grid.Columns[5].Frozen = true;
                    grid.Columns[6].HeaderText = "所属公司";
                    grid.Columns[7].HeaderText = "线路类型";
                    grid.Columns[8].HeaderText = "首站开班时间";
                    grid.Columns[9].HeaderText = "首站收班时间";
                    grid.Columns[10].HeaderText = "末站开班时间";
                    grid.Columns[11].HeaderText = "末站收班时间";
                    grid.Columns[12].HeaderText = "票价1";
                    grid.Columns[13].HeaderText = "票价2";
                    grid.Columns[14].HeaderText = "票价3";
                    grid.Columns[15].HeaderText = "票价4";//原来是 图片5
                    grid.Columns[16].HeaderText = "线路编号";
                    grid.Columns[17].HeaderText = "长度";
                    grid.Columns[18].HeaderText = "平均满载率";
                    grid.Columns[19].HeaderText = "运营车次";
                    grid.Columns[20].HeaderText = "运力配备";
                    grid.Columns[21].HeaderText = "客运量";
                    grid.Columns[22].HeaderText = "客运工作量";
                    grid.Columns[23].HeaderText = "平均车速";
                    grid.Columns[24].HeaderText = "非直线系数";
                    grid.Columns[25].HeaderText = "非直线系数2";
                    grid.Columns[26].HeaderText = "图片1";
                    grid.Columns[27].HeaderText = "图片2";
                    grid.Columns[28].HeaderText = "图片3";
                    grid.Columns[29].HeaderText = "图片4";
                    grid.Columns[30].HeaderText = "服务面积";
                    grid.Columns[31].HeaderText = "平均运距";
                    grid.Columns[32].HeaderText = "高峰满载率";
                    grid.Columns[33].HeaderText = "线路负荷";
                    grid.Columns[34].HeaderText = "方向不均衡";
                    grid.Columns[35].HeaderText = "交替系数";
                    grid.Columns[36].HeaderText = "时不均系数";
                    grid.Columns[37].HeaderText = "天不均系数";
                    grid.Columns[38].HeaderText = "高峰小时段";
                    grid.Columns[39].HeaderText = "高峰小时面";
                    grid.Columns[40].HeaderText = "高峰小时量";
                    grid.Columns[41].HeaderText = "高峰客运量";
                    break;
            }
        }

        //控制列排序模式
        public static void SetColSortMode(DataGridView grid, DataGridViewColumnSortMode ColSortMode)
        {
            foreach (DataGridViewColumn eColumn in grid.Columns)
            {

                eColumn.SortMode = ColSortMode;
            }
        }
        //在rowheard添加序号
        public static void SetRowNo(DataGridView grid)
        {
            int nNum = 1;
            foreach (DataGridViewRow eRow in grid.Rows)
            {
                eRow.HeaderCell.Value = nNum++.ToString(); 
            }
        }

        public static void SaveXml(Form myForm) //保存窗口textbox输入为xml 
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
                     if (obj is Form)//递归保存窗口内的xml
                         SaveXml((Form)obj);
                 }
             }
                tw.WriteEndElement();
             tw.WriteEndDocument();
             tw.Flush();
             tw.Close();
        }

        public static void loadXml(Form myForm) //载入上次保存的xml到窗口textbox 
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

        //声明读写INI文件的API函数
        [DllImport("kernel32")]
        public static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);

        public static string GetProfileString(string section, string key, string filePath)
        {
            Byte[] Buffer = new Byte[65535];
            int bufLen = GetPrivateProfileString(section, key, "", Buffer, Buffer.GetUpperBound(0), filePath);
            //必须设定0（系统默认的代码页）的编码方式，否则无法支持中文
            string s = Encoding.GetEncoding(0).GetString(Buffer);
            s = s.Substring(0, bufLen);

            return s.Trim();
        }

        #region 将DataGridView中的数据导入到Excel中，DataGridView无需绑定数据源
        /// <summary>
        /// 将DataGridView中的数据导入到Excel中，DataGridView无需绑定数据源
        /// </summary>
        /// <param name="datagridview">DataGridView</param>
        /// <param name="SheetName">Excel sheet title</param>
        /// <param name="bVisble">是否输出不可见列</param>
        /// <param name="nBegin">准备输出的起始列号，从零开始计数</param>
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
                iRows = datagridview.Rows.Count + 1;　　　//加上列头行
                iTrueCols = datagridview.Columns.Count;　 //包含隐藏的列，一共有多少列
                //求列数，省略Visible = false的列
                for (int i = 0; i < datagridview.Columns.Count; i++)
                {
                    if (bVisble && datagridview.Columns[i].Visible == false)
                        continue;
                    iCols++;
                    if (iCols == nBegin)
                    {
                        nStarCol = i + 1;//判断当bVisble==true时，准备输出的可见列起始的列号，要加一来排除当前可见列。
                    }
                }
                iCols = iCols - nBegin;//真正输出的列数
                //string[,] dimArray = new string[iRows + 1, iCols];    // 需要修改string[iRows + 1, iCols]为string[iRows, iCols]
                string[,] dimArray = new string[iRows, iCols];// 修改后

                for (int j = nStarCol, k = 0; j < iTrueCols; j++)
                {
                    //判断省略Visible = false的列
                    if (bVisble && datagridview.Columns[j].Visible == false)
                        continue;

                    dimArray[0, k] = datagridview.Columns[j].HeaderText;//得到列名
                    k++;

                }
               
                               
                // for (int i = 0; i < iRows; i++) 修改前
                for (int i = 0; i < iRows - 1; i++) // 修改后
                {
                    for (int j = nStarCol, k = 0; j < iTrueCols; j++)
                    {
                        //省略Visible = false的列
                        if (bVisble && datagridview.Columns[j].Visible == false)
                            continue;

                            dimArray[i + 1, k] = datagridview.Rows[i].Cells[j].Value.ToString();//得到所有内容
                            k++;
                    }
                }
                /* 修改前
                ws.get_Range(ws.Cells[1, 1], ws.Cells[iRows + 1, iCols]).Value2 = dimArray;
                ws.get_Range(ws.Cells[1, 1], ws.Cells[1, iCols]).Font.Bold = true;
                ws.get_Range(ws.Cells[1, 1], ws.Cells[iRows + 1, iCols]).Font.Size = 10.0;
                ws.get_Range(ws.Cells[1, 1], ws.Cells[iRows + 1, iCols]).RowHeight = 14.25;
                 * */
                ws.get_Range(ws.Cells[1, 1], ws.Cells[iRows, iCols]).Value2 = dimArray;//全部内容赋值
                ws.get_Range(ws.Cells[1, 1], ws.Cells[1, iCols]).Font.Bold = true;//表头
                ws.get_Range(ws.Cells[1, 1], ws.Cells[iRows, iCols]).Font.Size = 10.0;
                ws.get_Range(ws.Cells[1, 1], ws.Cells[iRows, iCols]).RowHeight = 14.25;
                //for (int j = 0, k = 0; j < iTrueCols; j++)
                //{
                //    //省略Visible = false的列
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
                Connect_Sql = "provider=Microsoft.Jet.OLEDB.4.0;data source=" + strType + "\\data\\公交.mdb";
                Mxd_Name = strType + "\\data\\Data.mxd";
            }
        }
    }

    public class BusStation : IComparable<BusStation>

    {
        public int ID;
        public string StationName, Direct,StationExplain;//原来是 调度站道3，现在是站点说明
        public double rLength;//点在线上距起点的距离
        public string StationCharacter;//站点所在道路

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

        #region IComparable<BusStation> 成员

        public int CompareTo(BusStation other)
         {
             return this.rLength.CompareTo(other.rLength);
         }
         #endregion

    }

  

    // public class IniFiles
    //{
    //    public string FileName; //INI文件名
    //    //string path   =   System.IO.Path.Combine(Application.StartupPath,"pos.ini");

    //    //声明读写INI文件的API函数
    //    [DllImport("kernel32")]
    //    private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
    //    [DllImport("kernel32")]
    //    private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);

    //    //类的构造函数，传递INI文件名
    //    public IniFiles(string AFileName)
    //    {
    //        // 判断文件是否存在
    //        FileInfo fileInfo = new FileInfo(AFileName);
    //        //Todo:搞清枚举的用法
    //        if ((!fileInfo.Exists))
    //        { //|| (FileAttributes.Directory in fileInfo.Attributes))
    //            //文件不存在，建立文件
    //            System.IO.StreamWriter sw = new System.IO.StreamWriter(AFileName, false, System.Text.Encoding.Default);
    //            try
    //            {
    //                sw.Write("#表格配置档案");
    //                sw.Close();
    //            }
    //            catch
    //            {
    //                throw (new ApplicationException("Ini文件不存在"));
    //            }
    //        }
    //        //必须是完全路径，不能是相对路径
    //        FileName = fileInfo.FullName;
    //    }

    //    //写INI文件
    //    public void WriteString(string Section, string Ident, string Value)
    //    {
    //        if (!WritePrivateProfileString(Section, Ident, Value, FileName))
    //        {

    //            throw (new ApplicationException("写Ini文件出错"));
    //        }
    //    }

    //    //读取INI文件指定
    //    public string ReadString(string Section, string Ident, string Default)
    //    {
    //        Byte[] Buffer = new Byte[65535];
    //        int bufLen = GetPrivateProfileString(Section, Ident, Default, Buffer, Buffer.GetUpperBound(0), FileName);
    //        //必须设定0（系统默认的代码页）的编码方式，否则无法支持中文
    //        string s = Encoding.GetEncoding(0).GetString(Buffer);
    //        s = s.Substring(0, bufLen);
    //        return s.Trim();
    //    }

    //    //读整数
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

    //    //写整数
    //    public void WriteInteger(string Section, string Ident, int Value)
    //    {
    //        WriteString(Section, Ident, Value.ToString());
    //    }

    //    //读布尔
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

    //    //写Bool
    //    public void WriteBool(string Section, string Ident, bool Value)
    //    {
    //        WriteString(Section, Ident, Convert.ToString(Value));
    //    }

    //    //从Ini文件中，将指定的Section名称中的所有Ident添加到列表中
    //    public void ReadSection(string Section, StringCollection Idents)
    //    {
    //        Byte[] Buffer = new Byte[16384];
    //        //Idents.Clear();

    //        int bufLen = GetPrivateProfileString(Section, null, null, Buffer, Buffer.GetUpperBound(0), FileName);
    //        //对Section进行解析
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

    //    //从Ini文件中，读取所有的Sections的名称
    //    public void ReadSections(StringCollection SectionList)
    //    {
    //        //Note:必须得用Bytes来实现，StringBuilder只能取到第一个Section
    //        byte[] Buffer = new byte[65535];
    //        int bufLen = 0;
    //        bufLen = GetPrivateProfileString(null, null, null, Buffer,
    //        Buffer.GetUpperBound(0), FileName);
    //        GetStringsFromBuffer(Buffer, bufLen, SectionList);
    //    }

    //    //读取指定的Section的所有Value到列表中
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
    //    ////读取指定的Section的所有Value到列表中，
    //    //public void ReadSectionValues(string Section, NameValueCollection Values,char splitString)
    //    //{　 string sectionValue;
    //    //　　string[] sectionValueSplit;
    //    //　　StringCollection KeyList = new StringCollection();
    //    //　　ReadSection(Section, KeyList);
    //    //　　Values.Clear();
    //    //　　foreach (string key in KeyList)
    //    //　　{
    //    //　　　　sectionValue=ReadString(Section, key, "");
    //    //　　　　sectionValueSplit=sectionValue.Split(splitString);
    //    //　　　　Values.Add(key, sectionValueSplit[0].ToString(),sectionValueSplit[1].ToString());
    //    //　　}
    //    //}

    //    //清除某个Section
    //    public void EraseSection(string Section)
    //    {
    //        if (!WritePrivateProfileString(Section, null, null, FileName))
    //        {
    //            throw (new ApplicationException("无法清除Ini文件中的Section"));
    //        }
    //    }

    //    //删除某个Section下的键
    //    public void DeleteKey(string Section, string Ident)
    //    {
    //        WritePrivateProfileString(Section, Ident, null, FileName);
    //    }

    //    //Note:对于Win9X，来说需要实现UpdateFile方法将缓冲中的数据写入文件
    //    //在Win NT, 2000和XP上，都是直接写文件，没有缓冲，所以，无须实现UpdateFile
    //    //执行完对Ini文件的修改之后，应该调用本方法更新缓冲区。
    //    public void UpdateFile()
    //    {
    //        WritePrivateProfileString(null, null, null, FileName);
    //    }

    //    //检查某个Section下的某个键值是否存在
    //    public bool ValueExists(string Section, string Ident)
    //    {
    //        StringCollection Idents = new StringCollection();
    //        ReadSection(Section, Idents);
    //        return Idents.IndexOf(Ident) > -1;
    //    }

    //    //确保资源的释放
    //    ~IniFiles()
    //    {
    //        UpdateFile();
    //    }
    //}
}

