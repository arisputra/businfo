using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Businfo.Globe;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.SystemUI;

using XtremeDockingPane;
using System.Globalization;
using Microsoft.Office.Interop.Excel;
using Winapp = System.Windows.Forms.Application;
using GISPoint = ESRI.ArcGIS.Geometry.IPoint;
using System.Data.OleDb;
using System.IO;

namespace Businfo
{

    public partial class frmMainNew : Form
    {
        #region 变量声明
        IMapDocument m_pMapDocument;
        public frmFlash m_frmFlash;
        public frmlayerToc m_frmlayerToc = new frmlayerToc();
        public frmStationPane m_frmStationPane = new frmStationPane();
        public frmRoadPane m_frmRoadPane = new frmRoadPane();
        public frmFacilitiesPane m_frmFacilityPane = new frmFacilitiesPane();
        public int m_ToolStatus;
        public GISPoint m_mapPoint;//鼠标点击查询处得坐标
        public GISPoint m_FormPoint;//添加线路的顺序节点
        public IFeatureLayer m_CurFeatureLayer ;//当前FeatureLayer
        public IFeature m_CurFeature ;  //当前feature
        public List<IFeature> m_featureCollection = new List<IFeature>();   //得到所有选中的feature
        public List<IFeature> m_featureColByOrder = new List<IFeature>();   //选择大于100时fea不是按照顺序排列的，这个人工排序。
        public List<IPolyline> m_PolylineCollection = new List<IPolyline>();   //得到所有选中的IPolyline
        public MovePointFeedbackClass m_FeedBack;
        public bool m_bShowLayer;
        public int m_nPLineNum = 1;//线路每次包含总数
        AoInitialize m_pAoInitialize = new AoInitialize();//判断版本
        #endregion

        public frmMainNew()
        {
            InitializeComponent();
        }

        private void frmMainNew_FormClosed(object sender, FormClosedEventArgs e)
        {
            ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();
            if (m_frmFlash != null)
            {
                m_frmFlash.Close();
            }
            if (ForBusInfo.Excel_app != null)
            {
                ForBusInfo.Excel_app.Quit();
                int nGeneration = System.GC.GetGeneration(ForBusInfo.Excel_app);
                ForBusInfo.Excel_app = null;
                //虽然用了_xlApp.Quit()，但由于是COM，并不能清除驻留在内存在的进程，每实例一次Excel则Excell进程多一个。 
                //因此用垃圾回收，建议不要用进程的KILL()方法，否则可能会错杀无辜啊:)。 
                System.GC.Collect(nGeneration); 
                
            }

        }

        private void frmMainNew_Load(object sender, EventArgs e)
        {
            
            ////Create a new AoInitialize object
            //if (m_pAoInitialize == null)
            //{
            //    this.Close();
            //}
            ////Determine if the product is available
            //if (m_pAoInitialize.IsProductCodeAvailable(esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB) == esriLicenseStatus.esriLicenseAvailable)
            //{
            //    if (m_pAoInitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngine) != esriLicenseStatus.esriLicenseCheckedOut)
            //    {
            //        this.Close();
            //    }
            //}
            //else
            //{
            //    this.Close();
            //}

            EngineFuntions.m_AxMapControl = axMapControl1;//传递Map控件

            axCommandBars1.LoadDesignerBars(null, null);
            axCommandBars1.ActiveMenuBar.Delete();
            axCommandBars1.Options.UseDisabledIcons = true;
            UInt32 pColor;
            pColor = System.Convert.ToUInt32(ColorTranslator.ToOle(Color.FromArgb(255, 255, 255)).ToString());
            axCommandBars1.SetSpecialColor((XtremeCommandBars.XTPColorManagerColor)15, pColor);

            m_pMapDocument = new MapDocumentClass();
            m_pMapDocument.Open(ForBusInfo.Mxd_Name, string.Empty);
            axMapControl1.Map = m_pMapDocument.get_Map(0);
            axMapControl1.Map.Name = "查询";
            //axMapControl1.Extent = axMapControl1.FullExtent;

            List<IFeatureLayer> colLayers;
            colLayers = EngineFuntions.GetAllValidFeatureLayers(axMapControl1.Map);
            //设置所有图层不能选择
            EngineFuntions.SetCanSelLay("");//无图层名即所有不可选

            EngineFuntions.m_Layer_BusStation = EngineFuntions.GetLayerByName("公交站点", colLayers);
            EngineFuntions.m_Layer_BusRoad = EngineFuntions.GetLayerByName("公交站线", colLayers);
            EngineFuntions.m_Layer_BackRoad = EngineFuntions.GetLayerByName("站线备份", colLayers);


            //Determine if Alpha Context is supported, if it is, then enable it
            axDockingPane1.Options.AlphaDockingContext = true;
            //Determine if Docking Stickers is supported, if they are, then enable them
            axDockingPane1.Options.ShowDockingContextStickers = true;

            axDockingPane1.Options.ThemedFloatingFrames = true;
            axDockingPane1.TabPaintManager.Position = XTPTabPosition.xtpTabPositionTop;
            axDockingPane1.TabPaintManager.Appearance = XtremeDockingPane.XTPTabAppearanceStyle.xtpTabAppearanceVisualStudio;

            XtremeDockingPane.Pane ThePane = axDockingPane1.CreatePane(ForBusInfo.Pan_Layer, 260, 200, DockingDirection.DockLeftOf, null);
            ThePane.Title = "图 层";
            axDockingPane1.FindPane(ForBusInfo.Pan_Layer).Handle = m_frmlayerToc.Handle.ToInt32();

            ThePane = axDockingPane1.CreatePane(ForBusInfo.Pan_Station, 260, 200, DockingDirection.DockLeftOf, null);
            ThePane.Title = "站 点";
            axDockingPane1.FindPane(ForBusInfo.Pan_Station).Handle = m_frmStationPane.Handle.ToInt32();

            ThePane = axDockingPane1.CreatePane(ForBusInfo.Pan_Road, 260, 200, DockingDirection.DockLeftOf, null);
            ThePane.Title = "线 路";
            axDockingPane1.FindPane(ForBusInfo.Pan_Road).Handle = m_frmRoadPane.Handle.ToInt32();

            ThePane = axDockingPane1.CreatePane(ForBusInfo.Pan_Facility, 260, 200, DockingDirection.DockLeftOf, null);
            ThePane.Title = "设 施";
            axDockingPane1.FindPane(ForBusInfo.Pan_Facility).Handle = m_frmFacilityPane.Handle.ToInt32();

            axDockingPane1.AttachPane(axDockingPane1.FindPane(ForBusInfo.Pan_Road), axDockingPane1.FindPane(ForBusInfo.Pan_Station));
            axDockingPane1.AttachPane(axDockingPane1.FindPane(ForBusInfo.Pan_Layer), axDockingPane1.FindPane(ForBusInfo.Pan_Station));
            axDockingPane1.AttachPane(axDockingPane1.FindPane(ForBusInfo.Pan_Facility), axDockingPane1.FindPane(ForBusInfo.Pan_Station));
            axDockingPane1.FindPane(ForBusInfo.Pan_Layer).Select();
            axDockingPane1.FindPane(ForBusInfo.Pan_Road).Options = PaneOptions.PaneNoCloseable;
            axDockingPane1.FindPane(ForBusInfo.Pan_Layer).Options = PaneOptions.PaneNoCloseable;
            axDockingPane1.FindPane(ForBusInfo.Pan_Facility).Options = PaneOptions.PaneNoCloseable;
            axDockingPane1.FindPane(ForBusInfo.Pan_Station).Options = PaneOptions.PaneNoCloseable;
            switch (ForBusInfo.Login_name)
            {
            case "站点管理":
                axDockingPane1.FindPane(ForBusInfo.Pan_Facility).Closed = true;
            	break;
            case "线路管理":
                axDockingPane1.FindPane(ForBusInfo.Pan_Station).Closed = true;
                axDockingPane1.FindPane(ForBusInfo.Pan_Facility).Closed = true;
                break;
            case "设施管理":
                axDockingPane1.FindPane(ForBusInfo.Pan_Road).Closed = true;
                axDockingPane1.FindPane(ForBusInfo.Pan_Station).Closed = true;
                break;
            case "浏览":
                axDockingPane1.FindPane(ForBusInfo.Pan_Road).Closed = true;
                axDockingPane1.FindPane(ForBusInfo.Pan_Facility).Closed = true;
                axDockingPane1.FindPane(ForBusInfo.Pan_Station).Closed = true;
                break;
            }

            
            //'鹰眼图：
            String sHawkEyeFileName;
            sHawkEyeFileName = ForBusInfo.Mxd_Name;
            m_frmlayerToc.MapHawkEye.LoadMxFile(sHawkEyeFileName);
            m_frmlayerToc.MapHawkEye.Extent = m_frmlayerToc.MapHawkEye.FullExtent;
            //m_frmlayerToc.m_MapControl = axMapControl1.Object;
            m_frmlayerToc.TOCControl.SetBuddyControl(this.axMapControl1.Object);

            axDockingPane1.SetCommandBars(axCommandBars1.GetDispatch());
            this.WindowState = FormWindowState.Maximized;
            m_bShowLayer = false;
            ForBusInfo.Frm_Main = this;

            if(ForBusInfo.Login_Operation != "")//设置用户权限对应禁止的操作
            {
                string[] strColu = ForBusInfo.Login_Operation.Split('；');
                int nCol;
                foreach (string eStrRow in strColu)
                {
                    nCol = Convert.ToInt32(eStrRow[0].ToString());
                    string strRow = eStrRow.Substring(2);
                    string[] strRows = strRow.Split('、');
                    foreach (string eStrRows in strRows)
                    {
                        axCommandBars1[nCol].Controls[Convert.ToInt32(eStrRows)].Enabled = false;
                        
                    }
                }
            }

            // Set what the Help file will be for the HelpProvider.
            this.helpProvider2.HelpNamespace = Winapp.StartupPath + @"\用户手册.doc";
        }

        private void axCommandBars1_Execute(object sender, AxXtremeCommandBars._DCommandBarsEvents_ExecuteEvent e)
        {
            EngineFuntions.SetToolNull();
            if (m_ToolStatus != e.control.Id)
            {
                foreach (XtremeCommandBars.CommandBarControl eControl in axCommandBars1[2].Controls)
                {
                    eControl.Checked = false;
                }
            }
            switch (e.control.Id)
            {
                case ForBusInfo.BusInfo_Help:
                    System.Diagnostics.Process.Start(Winapp.StartupPath + @"\用户手册.doc");
                    break;
                case ForBusInfo.BusInfo_ParaSet:
                    System.Diagnostics.Process.Start(Winapp.StartupPath + @"\Businfo.ini");
                    break;
                case ForBusInfo.Map3D_ZoomIn:
                    m_ToolStatus = ForBusInfo.Map3D_ZoomIn;
                    if(axMapControl1.Visible == true)
                    {
                        ICommand pCommand; 
                        pCommand = new ControlsMapZoomInTool();
                        pCommand.OnCreate(axMapControl1.Object);
                        axMapControl1.CurrentTool = (ITool)pCommand;
                    }
                    break;
                case ForBusInfo.Map3D_ZoomOut:
                    m_ToolStatus = ForBusInfo.Map3D_ZoomOut;
                    if (axMapControl1.Visible == true)
                    {
                        ICommand pCommand; 
                        pCommand = new ControlsMapZoomOutTool();
                        pCommand.OnCreate(axMapControl1.Object);
                        axMapControl1.CurrentTool = (ITool)pCommand;
                    }
                    break;
                case ForBusInfo.Map3D_Pan:
                    m_ToolStatus = ForBusInfo.Map3D_Pan;
                    if (axMapControl1.Visible == true)
                    {
                        ICommand pCommand; 
                        pCommand = new ControlsMapPanTool();
                        pCommand.OnCreate(axMapControl1.Object);
                        axMapControl1.CurrentTool = (ITool)pCommand;
                    }
                    break;
                case ForBusInfo.Map3D_Reflash:
                    m_ToolStatus = ForBusInfo.Map3D_Reflash;
                    if (axMapControl1.Visible == true)
                    {
                       EngineFuntions.MapRefresh();
                       axMapControl1.Map.ClearSelection();
                       axMapControl1.ActiveView.GraphicsContainer.DeleteAllElements();
                       EngineFuntions.MapRefresh();
                    }
                    break;
                //前一屏
                case ForBusInfo.Map3D_PreView:
                    m_ToolStatus = ForBusInfo.Map3D_PreView;
                    if (axMapControl1.Visible == true)
                    {
                       EngineFuntions.GoBack();
                    }
                    break;
                //后一屏
                case ForBusInfo.Map3D_NextView:
                    m_ToolStatus = ForBusInfo.Map3D_NextView;
                    if (axMapControl1.Visible == true)
                    {
                       EngineFuntions.GoNext();
                    }
                    break;
                case ForBusInfo.Map3D_Distance://计算长度
                    break;

                case ForBusInfo.Map3D_Area://计算面积
                    break;
                
                case ForBusInfo.Map3D_Select ://拉框选择
                    m_ToolStatus = ForBusInfo.Map3D_Select;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerPencil;
                    
                    break;

                case ForBusInfo.Map3D_Full:
                    m_ToolStatus = ForBusInfo.Map3D_Full;
                    if (axMapControl1.Visible == true)
                    {
                       axMapControl1.Extent = axMapControl1.FullExtent;
                    }
                    break;
                case ForBusInfo.Bus_Add:
                    if(e.control.Checked)
                    {
                        e.control.Checked = false;
                    }
                    else
                    {
                        e.control.Checked = true;
                    }

                    m_ToolStatus = ForBusInfo.Bus_Add;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerPencil;
                    break;
                case ForBusInfo.Bus_BackUp://站点不用备份


                case ForBusInfo.Bus_Dele:
                    if (e.control.Checked)
                    {
                        e.control.Checked = false;
                    }
                    else
                    {
                        e.control.Checked = true;
                    }
                    m_ToolStatus = ForBusInfo.Bus_Dele;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerIdentify;
                    break;

                case ForBusInfo.Bus_Edit:
                    if (e.control.Checked)
                    {
                        e.control.Checked = false;
                    }
                    else
                    {
                        e.control.Checked = true;
                    }
                    m_ToolStatus = ForBusInfo.Bus_Edit;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerHotLink;
                    break;

                case ForBusInfo.Bus_Move:
                    if (e.control.Checked)
                    {
                        e.control.Checked = false;
                    }
                    else
                    {
                        e.control.Checked = true;
                    }
                    m_ToolStatus = ForBusInfo.Bus_Move;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerIdentify;
                    break;

                case ForBusInfo.Bus_Pano:
                    if (e.control.Checked)
                    {
                        e.control.Checked = false;
                    }
                    else
                    {
                        e.control.Checked = true;
                    }
                    m_ToolStatus = ForBusInfo.Bus_Pano;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerIdentify;
                    break;

                case ForBusInfo.Bus_Query:
                    m_ToolStatus = ForBusInfo.Bus_Query;
                    axDockingPane1.FindPane(ForBusInfo.Pan_Station).Select();
                    break;

                case ForBusInfo.Bus_Recover://站点不用恢复


                case ForBusInfo.Road_Add:
                    m_ToolStatus = ForBusInfo.Road_Add;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerIdentify;
                    break;

                case ForBusInfo.Road_Associate:
                    m_ToolStatus = ForBusInfo.Road_Associate;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerPencil;
                    break;
                case ForBusInfo.Road_BackUp://站线备份
                    m_ToolStatus = ForBusInfo.Road_BackUp;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerIdentify;
                    break;
                case ForBusInfo.Road_Dele:
                    m_ToolStatus = ForBusInfo.Road_Dele;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerIdentify;
                    break;
                case ForBusInfo.Road_Edit:
                    m_ToolStatus = ForBusInfo.Road_Edit;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerIdentify;
                    break;
                case ForBusInfo.Road_Query:
                    m_ToolStatus = ForBusInfo.Road_Query;
                    axDockingPane1.FindPane(ForBusInfo.Pan_Road).Select();
                    break;
                case ForBusInfo.Road_Recover://站线恢复
                    m_ToolStatus = ForBusInfo.Road_Recover;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerIdentify;
                    frmRoadRecover frmPopup = new frmRoadRecover();
                    frmPopup.ShowDialog();
                    break;
                case ForBusInfo.BusInfo_Layer://开关图层
                    List<string> colLayerName = new List<string>();
                    colLayerName.Add("公交站点");
                    colLayerName.Add("道路中心线");
                    if (m_bShowLayer)
                    {
                        colLayerName.Add("背景底图");
                        colLayerName.Add("公交站线");
                        EngineFuntions.SetLayerVisble(colLayerName);
                        axMapControl1.ActiveView.Refresh();
                        m_bShowLayer = false;
                    }
                    else
                    {
                        EngineFuntions.SetLayerVisble(colLayerName);
                        axMapControl1.ActiveView.Refresh();
                        m_bShowLayer = true;
                    }
                    break;
                case ForBusInfo.Table_RoadExport://制作单报表
                    m_ToolStatus = ForBusInfo.Table_RoadExport;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerPencil;
                    //axDockingPane1.FindPane(ForBusInfo.Pan_Road).Select();
                    break;
                case ForBusInfo.Table_Operation://查看操作
                    m_ToolStatus = ForBusInfo.Table_Operation;
                    //axDockingPane1.FindPane(ForBusInfo.Pan_Road).Select();
                    frmOperation frmPopup1 = new frmOperation();
                    frmPopup1.ShowDialog();
                    break;
                case ForBusInfo.Table_StationTable://路单输出
                    m_ToolStatus = ForBusInfo.Table_StationTable;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerPencil;
                    //axDockingPane1.FindPane(ForBusInfo.Pan_Station).Select();
                    break;
                case ForBusInfo.Road_Pause://保存临时的线路
                    m_ToolStatus = ForBusInfo.Road_Pause;
                    m_CurFeature = null;
                    if (MessageBox.Show("是否临时保存？\n", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        if (EngineFuntions.GetSeledFeatures(m_CurFeatureLayer, ref m_featureCollection))
                        {
                            string strLineID = "";
                            if (m_featureCollection.Count < 100)
                            {
                                foreach (IFeature pfea in m_featureCollection)
                                {
                                    strLineID = string.Format("{0},{1}", strLineID, pfea.OID);
                                }
                            }
                            else
                            {
                                foreach (IFeature pfea in m_featureColByOrder)
                                {
                                    strLineID = string.Format("{0},{1}", strLineID, pfea.OID);
                                }
                            }
                            ForBusInfo.WritePrivateProfileString("LineId", "IDNumber", strLineID, Winapp.StartupPath + "\\Businfo.ini");
                            ForBusInfo.WritePrivateProfileString("LineId", "FormPoint",string.Format("{0},{1}",m_FormPoint.X.ToString(),m_FormPoint.Y.ToString()), Winapp.StartupPath + "\\Businfo.ini");
                        }
                        else
                        {
                            MessageBox.Show("提示：请在道路中心线路中选中路段！\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        EngineFuntions.m_AxMapControl.Map.ClearSelection();
                        EngineFuntions.m_AxMapControl.Refresh();
                    }
                    break;

                case ForBusInfo.Road_Resume://继续临时线路进行编辑

                    string strLID = ForBusInfo.GetProfileString("LineId", "IDNumber", Winapp.StartupPath + "\\Businfo.ini");
                    strLID = strLID.Substring(1);
                    string[] strLIDs = strLID.Split(',');
                         
                    if(strLIDs.Length>2)
                    {
                        m_CurFeatureLayer = EngineFuntions.SetCanSelLay("道路中心线");
                        EngineFuntions.m_AxMapControl.Map.ClearSelection();
                        m_featureCollection.Clear();
                        m_featureCollection = EngineFuntions.GetSeartchFeatures(m_CurFeatureLayer, string.Format("OBJECTID IN ({0})", strLID));
                        m_featureColByOrder.Clear();
                        m_featureColByOrder.AddRange(m_featureCollection); 
                        EngineFuntions.MapRefresh();
                        m_nPLineNum = strLIDs.Length;
                        strLID = ForBusInfo.GetProfileString("LineId", "FormPoint", Winapp.StartupPath + "\\Businfo.ini");
                        string[] strPoints = strLID.Split(',');
                        IPolyline pline = m_featureCollection[m_featureCollection.Count - 1].Shape as IPolyline;
                        m_FormPoint = pline.FromPoint;
                        m_FormPoint.PutCoords(Convert.ToDouble(strPoints[0]), Convert.ToDouble(strPoints[1]));
                        EngineFuntions.ZoomPoint(m_FormPoint, 1500);
                        
                        //m_ToolStatus = ForBusInfo.Road_Add;
                        //axMapControl1.MousePointer = esriControlsMousePointer.esriPointerIdentify;
                    }

                    break;

                case ForBusInfo.Road_End://完成线路添加
                    m_ToolStatus = ForBusInfo.Road_End;
                    m_CurFeature = null;
                    if (MessageBox.Show("是否选择完成？\n", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        IPolyline pPolyline = null;
                        m_nPLineNum = 1;
                        if (EngineFuntions.GetSeledFeatures(m_CurFeatureLayer, ref m_featureCollection))
                        {
                            if (EngineFuntions.MergeLinesByTopo(m_featureCollection, ref pPolyline))
                            {
                                axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                                frmRoadPara frmRoadPara = new frmRoadPara();
                                frmRoadPara.m_pPolyline = pPolyline;
                                if (frmRoadPara.ShowDialog() == DialogResult.OK)
                                {
                                    EngineFuntions.PartialRefresh(EngineFuntions.m_Layer_BusRoad);
                                    m_CurFeature = frmRoadPara.m_pFeature;
                                    frmRoadPara.Close();
                                    System.Threading.Thread.Sleep(1000);
                                    m_frmRoadPane.RefreshGrid();
                                    axMapControl1.Map.ClearSelection();
                                }
                            }
                            else
                            {
                                MessageBox.Show("提示：选择的线路不是连续的！\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("提示：请在公交线路中选中路段\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        m_PolylineCollection.Clear();
                        EngineFuntions.m_AxMapControl.Map.ClearSelection();
                        EngineFuntions.m_AxMapControl.Refresh();
                    }
                    break;

                case ForBusInfo.Road_Reversed://线路反向
                    //m_ToolStatus = ForBusInfo.Road_Reversed;
                    if (m_ToolStatus == ForBusInfo.Road_End && m_CurFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                    {
                        string strDirect;
                        if (m_CurFeature.get_Value(m_CurFeature.Fields.FindField("RoadTravel")).ToString() == "去行")
                            strDirect = "回行";
                        else
                            strDirect = "去行";
                        IFeature pFeature = EngineFuntions.GetOneSeartchFeature(EngineFuntions.m_Layer_BusRoad, string.Format("Roadname = '{0}' AND RoadTravel = '{1}'", m_CurFeature.get_Value(m_CurFeature.Fields.FindField("Roadname")).ToString(), strDirect));
                        if (pFeature != null)
                        {
                            MessageBox.Show("线路图层已经存在反向线路\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        IPolyline pPLine = m_CurFeature.ShapeCopy as IPolyline;
                        pPLine.ReverseOrientation();
                        m_CurFeature.Shape = pPLine;
                        m_CurFeature.set_Value(m_CurFeature.Fields.FindField("RoadTravel"), strDirect);
                        EngineFuntions.CopyFeature(EngineFuntions.m_Layer_BusRoad, m_CurFeature);
                        System.Threading.Thread.Sleep(1000);
                        m_frmRoadPane.RefreshGrid();
                        MessageBox.Show("反向线路添加完成，请关联站点！\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    break;
                default :
                    break;
            }
        }

        private void axCommandBars1_ResizeEvent(object sender, EventArgs e)
        {
            int left, top, right, bottom;
            axCommandBars1.GetClientRect(out left, out top, out right, out bottom);
            axMapControl1.SetBounds(left, top, right - left, bottom - top);
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            m_mapPoint = axMapControl1.ToMapPoint(e.x, e.y);
            IActiveView pActiveView = axMapControl1.ActiveView;
            if (e.button == 1)
             {
                 switch (m_ToolStatus)
                 {
                     case ForBusInfo.Map3D_Select:
                         if (axMapControl1.Visible == true)
                         {
                            IGeometry pGeo;
                            pGeo = axMapControl1.TrackPolygon();
                            List<IFeature> pSelFea = EngineFuntions.GetSeartchFeatures(EngineFuntions.m_Layer_BusStation,pGeo);
                            if (pSelFea.Count > 0)
                            {
                               
                                frmStationAllInfo frmPopup = new frmStationAllInfo();
                                frmPopup.m_featureCollection = pSelFea;
                                frmPopup.SetButtonVisable();
                                frmPopup.ShowDialog();
                                
                            }
                            pSelFea.Clear();
                            pSelFea = EngineFuntions.GetSeartchFeatures(EngineFuntions.m_Layer_BusRoad, pGeo);
                            if (pSelFea.Count > 0)
                            {
                                frmRoadAllInfo frmPopup = new frmRoadAllInfo();
                                frmPopup.m_featureCollection = pSelFea;
                                frmPopup.SetButtonVisable();
                                frmPopup.ShowDialog();

                            }
                         }
                         break;
                     case ForBusInfo.Bus_Add:
                         {
                             frmStationPara frmPopup = new frmStationPara();
                             frmPopup.m_mapPoint = m_mapPoint;
                             m_CurFeatureLayer = EngineFuntions.SetCanSelLay("道路中心线");
                             EngineFuntions.ClickSel(m_mapPoint, false, false, 24);
                             if (EngineFuntions.GetSeledFeatures(m_CurFeatureLayer, ref  m_featureCollection))
                             {
                                 foreach (IFeature pfea in m_featureCollection)
                                 {
                                     frmPopup.m_ListRoadName.Add(pfea.get_Value(pfea.Fields.FindField("道路名称")) as string);
                                 }
                             }
                            
                             if (frmPopup.ShowDialog() == DialogResult.OK)
                             {
                                 EngineFuntions.PartialRefresh(EngineFuntions.m_Layer_BusStation);
                                 ForBusInfo.Add_Log(ForBusInfo.Login_name, "添加站点", frmPopup.m_strLog, "");
                                 frmPopup.Close();
                                 System.Threading.Thread.Sleep(1000);
                                 m_frmStationPane.RefreshGrid();
                             }
                             axMapControl1.Map.ClearSelection();
                             axMapControl1.ActiveView.GraphicsContainer.DeleteAllElements();
                             axMapControl1.Refresh();
                             //m_ToolStatus = -1;
                             //axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                             break;
                         }
                     case ForBusInfo.Bus_Dele:
                        m_CurFeatureLayer = EngineFuntions.SetCanSelLay("公交站点");
                        EngineFuntions.ClickSel(m_mapPoint, false, false, 6);
                        if (EngineFuntions.GetSeledFeatures(m_CurFeatureLayer, ref  m_featureCollection))
                         {
                             m_CurFeature = m_featureCollection[0];
                             
                             if (m_CurFeature != null)
                             {
                                 string strName = m_CurFeature.get_Value(m_CurFeature.Fields.FindField("StationName")).ToString();
                                 if (MessageBox.Show(string.Format("确认删除站点：{0}!", strName), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                                 {
                                     m_CurFeature.Delete();
                                     EngineFuntions.m_AxMapControl.ActiveView.Refresh();
                                     System.Threading.Thread.Sleep(1000);
                                     m_frmStationPane.RefreshGrid();
                                     ForBusInfo.Add_Log(ForBusInfo.Login_name, "删除站点", strName, "");

                                 }
                             }
                         }
                        //m_ToolStatus = -1;
                        //axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                        break;
                    case ForBusInfo.Bus_Move:
                        {
                            m_CurFeatureLayer = EngineFuntions.SetCanSelLay("公交站点");
                            EngineFuntions.ClickSel(m_mapPoint, false, false, 6);
                            if (EngineFuntions.GetSeledFeatures(m_CurFeatureLayer, ref  m_featureCollection))
                             {
                                 m_CurFeature = m_featureCollection[0];
                                 if (m_CurFeature != null)
                                 {
                                     m_FeedBack = new MovePointFeedbackClass();
                                     m_FeedBack.Display = pActiveView.ScreenDisplay;
                                     IMovePointFeedback pointMoveFeedback = m_FeedBack as IMovePointFeedback;
                                     pointMoveFeedback.Start(m_CurFeature.Shape as GISPoint, m_mapPoint);
                                     string strName = m_CurFeature.get_Value(m_CurFeature.Fields.FindField("StationName")).ToString();
                                     ForBusInfo.Add_Log(ForBusInfo.Login_name, "移动站点", strName, "");
                                 }
                             }
                             break;
                        }
                    case ForBusInfo.Bus_Pano:
                         {
                             m_CurFeatureLayer = EngineFuntions.SetCanSelLay("公交站点");
                             EngineFuntions.ClickSel(m_mapPoint, false, false, 6);
                             if (EngineFuntions.GetSeledFeatures(m_CurFeatureLayer, ref m_featureCollection))
                             {
                                 m_CurFeature = m_featureCollection[0];
                                 if (m_CurFeature != null)
                                 {
                                     frmPano frmPopup = new frmPano();
                                     frmPopup.m_strURL =Winapp.StartupPath + "\\Data\\A01\\pano1.html";
                                     frmPopup.Show();
                                 }
                             }
                             //m_ToolStatus = -1;
                             //axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                             break;
                         }
                     case ForBusInfo.Bus_Edit:
                         {
                             //GetSeartchFeatures可以替代SetCanSelLay，ClickSel，GetSeledFeatures三个步骤。
                             //m_featureCollection = EngineEditOperations.GetSeartchFeatures(EngineEditOperations.m_Layer_BusStation, m_mapPoint);
                             m_CurFeatureLayer = EngineFuntions.SetCanSelLay("公交站点");
                             EngineFuntions.ClickSel(m_mapPoint, false, false, 6);
                             if (EngineFuntions.GetSeledFeatures(m_CurFeatureLayer, ref  m_featureCollection))
                             {
                                 //m_CurFeature = m_featureCollection[0];
                                 if (m_featureCollection.Count > 0)
                                 {
                                     frmStationAllInfo frmPopup = new frmStationAllInfo();
                                     //frmPopup.m_nObjectId = (int)m_CurFeature.get_Value(m_CurFeature.Fields.FindField("OBJECTID"));
                                     frmPopup.m_featureCollection = m_featureCollection;
                                     frmPopup.ShowDialog();
                                 }
                             }
                             //m_ToolStatus = -1;54
                             //axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                             break;
                         }
                     case ForBusInfo.Road_Dele:
                         {
                             m_CurFeatureLayer = EngineFuntions.SetCanSelLay("公交站线");
                             EngineFuntions.ClickSel(m_mapPoint, false, false, 10);
                             if (EngineFuntions.GetSeledFeatures(m_CurFeatureLayer, ref  m_featureCollection))
                             {
                                 if (m_featureCollection.Count > 0)
                                 {
                                     m_frmRoadPane.m_featureCollection = m_featureCollection;
                                     m_frmRoadPane.RefreshSelectGrid();
                                     axDockingPane1.FindPane(ForBusInfo.Pan_Road).Select();
                                 }
                             }
                             m_ToolStatus = -1;
                             axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                             break;
                         }
                     case ForBusInfo.Road_Edit:
                         {
                             //GetSeartchFeatures可以替代SetCanSelLay，ClickSel，GetSeledFeatures三个步骤。
                             //m_featureCollection = EngineEditOperations.GetSeartchFeatures(EngineEditOperations.m_Layer_BusRoad, m_mapPoint);
                             m_CurFeatureLayer = EngineFuntions.SetCanSelLay("公交站线");
                             EngineFuntions.ClickSel(m_mapPoint, false, false, 10);
                             if (EngineFuntions.GetSeledFeatures(m_CurFeatureLayer, ref  m_featureCollection))
                             {
                                 if (m_featureCollection.Count > 0)
                                 {
                                      frmRoadAllInfo frmPopup = new frmRoadAllInfo();
                                      frmPopup.m_featureCollection = m_featureCollection;
                                      frmPopup.ShowDialog();
                                 }
                             }
                             m_ToolStatus = -1;
                             axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                             break;
                         }
                     case ForBusInfo.Road_Add:
                         {
                             bool bHave = false;
                             GISPoint PointBefore1, PointBefore2, PointAfter1, PointAfter2;
                             m_CurFeatureLayer = EngineFuntions.SetCanSelLay("道路中心线");
                             //if (1 == e.shift)
                            EngineFuntions.ClickSel(m_mapPoint, true, true, 6);
                            if (EngineFuntions.GetSeledFeatures(m_CurFeatureLayer, ref  m_featureCollection))
                            {
                                if (m_featureCollection.Count == 2)
                                {
                                    IPolyline pline = m_featureCollection[m_featureCollection.Count - 2].Shape as IPolyline;
                                    m_PolylineCollection.Add(pline);
                                    if (!EngineFuntions.GetLinkPoint(pline, m_featureCollection[m_featureCollection.Count - 1].Shape as IPolyline, ref m_FormPoint))
                                     {
                                         EngineFuntions.ClickSel(m_mapPoint, true, true, 6);//不连续再点击选择一次，取消不连续的线
                                     }
                                     else
                                     {
                                         m_PolylineCollection.Add(m_featureCollection[m_featureCollection.Count - 1].Shape as IPolyline);
                                     }
                                     m_nPLineNum = 2;
                                }
                                else if (m_nPLineNum < m_featureCollection.Count)
                                {
                                    IPolyline pline = m_featureCollection[m_featureCollection.Count - 1].Shape as IPolyline;
                                    
                                    if (m_featureCollection.Count == 99)//超过100时选择集就不是按照选择先后顺序列出了，必须人工判断
                                    {
                                        m_featureColByOrder.Clear();
                                        m_featureColByOrder.AddRange(m_featureCollection);
                                    }
                                    else if (m_featureCollection.Count > 99)
                                    {
                                        foreach (IFeature pfea in m_featureCollection)
                                        {
                                            bHave = false;
                                            foreach (IFeature pfeature in m_featureColByOrder)
                                            {
                                                if (pfea.OID == pfeature.OID)
                                                {
                                                    bHave = true;
                                                    break;
                                                }
                                            }
                                            if(bHave == false)
                                            {
                                                m_featureColByOrder.Add(pfea);
                                                pline = pfea.Shape as IPolyline;
                                                break;
                                            }
                                        }

                                    }

                                    
                                    PointAfter1 = pline.FromPoint;
                                    PointAfter2 = pline.ToPoint;
                                    //System.IO.FileStream fst = new System.IO.FileStream("C:\\222.txt", FileMode.Append);//追加模式
                                    //StreamWriter stw = new StreamWriter(fst, System.Text.Encoding.GetEncoding("utf-8"));//指定编码.否则将出错!
                                    ////stw.WriteLine(string.Format("{0}:{1}-{2};;;;{3}-{4}", m_featureCollection.Count - 1,PointAfter1.X,PointAfter1.Y,PointAfter2.X,PointAfter2.Y));

                                    //foreach (IFeature pfea in m_featureCollection)
                                    //{
                                    //    stw.WriteLine(pfea.get_Value(pfea.Fields.FindField("OBJECTID")).ToString());
                                    //}
                                    //stw.WriteLine("");
                                    //stw.Close();
                                    //fst.Close();

                                    if (m_FormPoint.Compare(PointAfter1) == 0)
                                     {
                                         m_FormPoint = PointAfter2;
                                         m_PolylineCollection.Add(pline);
                                         m_nPLineNum = m_featureCollection.Count;
                                     }
                                     else if (m_FormPoint.Compare(PointAfter2) == 0)
                                     {
                                         m_FormPoint = PointAfter1;
                                         m_PolylineCollection.Add(pline);
                                         m_nPLineNum = m_featureCollection.Count;
                                     }
                                     else
                                     {
                                         if (m_featureColByOrder.Count>99)
                                         m_featureColByOrder.RemoveAt(m_featureColByOrder.Count-1);
                                         MessageBox.Show("线路不连续！\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                         EngineFuntions.ClickSel(m_mapPoint, true, true, 6);//不连续再点击选择一次，取消不连续的线
                                     }
                                }
                                else if (2 < m_featureCollection.Count)//反向点选了线，就是删除了线。
                                {
                                    m_bShowLayer = true;
                                    foreach (IFeature pfea in m_featureCollection)
                                    {
                                        IPolyline pline = pfea.ShapeCopy as IPolyline;
                                        PointAfter1 = pline.FromPoint;
                                        PointAfter2 = pline.ToPoint;
                                        if (PointAfter1.Compare(m_PolylineCollection[m_PolylineCollection.Count - 1].FromPoint) == 0 && PointAfter2.Compare(m_PolylineCollection[m_PolylineCollection.Count - 1].ToPoint) == 0)
                                        {
                                            m_bShowLayer = false;
                                            MessageBox.Show("线路不连续\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            EngineFuntions.ClickSel(m_mapPoint, true, true, 6);//点击的不是最后的线，再点击一次重新选择加上
                                            break;
                                        }
                                    }
                                    if (m_bShowLayer)
                                    {
                                        m_nPLineNum = m_featureCollection.Count;
                                        if (!EngineFuntions.GetLinkPoint(m_PolylineCollection[m_PolylineCollection.Count - 3], m_PolylineCollection[m_PolylineCollection.Count - 2], ref m_FormPoint))
                                        {
                                            EngineFuntions.ClickSel(m_mapPoint, true, true, 6);//不连续再点击选择一次，取消不连续的线
                                        }
                                        else
                                        {
                                            m_PolylineCollection.RemoveAt(m_PolylineCollection.Count - 1);//是最后一条线，删除掉。
                                            if (m_featureColByOrder.Count>99)
                                                m_featureColByOrder.RemoveAt(m_featureColByOrder.Count - 1);
                                        }
                                    }
                                }
                                else
                                {
                                    m_FormPoint = m_mapPoint;
                                }
                                
                                EngineFuntions.PanPoint(m_FormPoint);
                             }
                             break;
                         }
                     case ForBusInfo.Road_Associate :
                         {
                            m_CurFeatureLayer = EngineFuntions.SetCanSelLay("公交站线");
                            EngineFuntions.ClickSel(m_mapPoint, false, false, 6);
                            if (EngineFuntions.GetSeledFeatures(m_CurFeatureLayer, ref m_featureCollection))
                            {
                                m_frmRoadPane.m_featureCollection = m_featureCollection;
                                m_frmRoadPane.RefreshSelectGrid();
                                axDockingPane1.FindPane(ForBusInfo.Pan_Road).Select();
                            }
                            m_ToolStatus = -1;
                            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                             break;
                         }
                     case ForBusInfo.Road_BackUp:
                         {
                             m_CurFeatureLayer = EngineFuntions.SetCanSelLay("公交站线");
                             EngineFuntions.ClickSel(m_mapPoint, false, false, 6);
                             if (EngineFuntions.GetSeledFeatures(m_CurFeatureLayer, ref m_featureCollection))
                             {
                                 m_frmRoadPane.m_featureCollection = m_featureCollection;
                                 m_frmRoadPane.RefreshSelectGrid();
                                 axDockingPane1.FindPane(ForBusInfo.Pan_Road).Select();
                             }
                             m_ToolStatus = -1;
                             axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                             break;
                         }
                     case ForBusInfo.Table_RoadExport://制作单报表
                         {
                             IGeometry pGeo;
                             pGeo = axMapControl1.TrackPolygon();
                             List<IFeature> pSelFea = EngineFuntions.GetSeartchFeatures(EngineFuntions.m_Layer_BusRoad, pGeo);
                             if (pSelFea.Count > 0)
                             {
                                 frmRoadTable frmPopup = new frmRoadTable();
                                 frmPopup.m_featureCollection = pSelFea;
                                 frmPopup.ShowDialog();
                             }
                             m_ToolStatus = -1;
                             axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                             break;
                         }
                     case ForBusInfo.Table_StationTable://路单报表
                         {
                             IGeometry pGeo;
                             pGeo = axMapControl1.TrackPolygon();
                             List<IFeature> pSelFea = EngineFuntions.GetSeartchFeatures(EngineFuntions.m_Layer_BusStation, pGeo);
                             if (pSelFea.Count > 0)
                             {

                                 frmStationTable frmPopup = new frmStationTable();
                                 frmPopup.m_featureCollection = pSelFea;
                                 frmPopup.ShowDialog();

                             }
                             m_ToolStatus = -1;
                             axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                             break;

                         }
                     default :
           	                break;
               }
             }
           if (2 == e.button)
           {
               EngineFuntions.PanPoint(m_mapPoint); 
           }
         
        }

        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (e.button == 1)
            {
                switch (m_ToolStatus)
                {
                    case ForBusInfo.Bus_Move:
                        m_mapPoint = axMapControl1.ToMapPoint(e.x, e.y);
                        m_FeedBack.MoveTo(m_mapPoint);
                        break;
                    default:
                        break;
                }
            }
        }

        private void axMapControl1_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            if (e.button == 1)
            {
                switch (m_ToolStatus)
                {
                    case ForBusInfo.Bus_Move:
                        if (m_FeedBack != null)
                        {
                            IMovePointFeedback pointMoveFeedback = m_FeedBack;
                            if (m_CurFeature != null)
                            {
                                  m_CurFeature.Shape = pointMoveFeedback.Stop();
                                  m_CurFeature.Store();
                                EngineFuntions.PartialRefresh(EngineFuntions.m_Layer_BusStation);
                            }
                            m_FeedBack = null;
                        }
                       
                    //m_ToolStatus = -1;
                    //axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                        break;
                    default:
                        break;
                }
            }
        }

        private void frmMainNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Release COM objects and shut down the AoInitilaize object
            //m_pAoInitialize.Shutdown();
            axMapControl1.Map.ClearSelection();
            axMapControl1.ActiveView.GraphicsContainer.DeleteAllElements();
            m_pMapDocument.Save(m_pMapDocument.UsesRelativePaths, false);

        }

        private void axMapControl1_OnAfterScreenDraw(object sender, IMapControlEvents2_OnAfterScreenDrawEvent e)
        {
           
        }

        private void axLicenseControl1_Enter(object sender, EventArgs e)
        {

        }

    }
}