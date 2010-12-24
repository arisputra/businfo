using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Businfo.Globe
{
    class ForBusInfo
    {
        #region "常量定义"
        public const int Pan_Layer = 1; //图层可停靠面板
        public const int Pan_Station = 10; //站点可停靠面板
        public const int Pan_Road = 11; //站线可停靠面板

        public const int Bus_Add = 1041; //站点添加
        public const int Bus_Supervise = 1042; //站点管理
        public const int Bus_Dele = 1043; //站点删除
        public const int Bus_Query = 1044; //站点查询
        public const int Bus_Move = 1045; //站点移动
        public const int Bus_Edit = 1046; //属性编辑
        public const int Bus_BackUp = 1047; //站点备份
        public const int Bus_Recover = 1048; //站点恢复
        public const int Bus_Pano = 1049; //站点全景
        public const int Road_Supervise = 1050; //线路管理
        public const int Road_Add = 1051; //线路添加
        public const int Road_Dele = 1052; //线路删除
        public const int Road_Query = 1053; //线路查询
        public const int Road_Edit = 1054; //属性编辑
        public const int Road_BackUp = 1055; //线路备份
        public const int Road_Recover = 1056; //线路恢复
        public const int Road_Associate = 1057; //关联站点
        public const int Table_Supervise = 1058; //报表输出

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

        public const int BusInfo_Layer = 1059; //图层切换
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
