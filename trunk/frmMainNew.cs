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
        public int m_ToolStatus;
        public IPoint m_mapPoint;//鼠标点击查询处得坐标
        public IFeatureLayer m_CurFeatureLayer ;//当前FeatureLayer
        public IFeature m_CurFeature ;  //当前feature
        public List<IFeature> m_featureCollection = new List<IFeature>();   //得到所有选中的feature
        public MovePointFeedbackClass m_FeedBack;
        public bool m_bShowLayer;
        #endregion

        public frmMainNew()
        {
            InitializeComponent();
        }

        private void frmMainNew_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_frmFlash.Close();
        }

        private void frmMainNew_Load(object sender, EventArgs e)
        {
            EngineFuntions.m_AxMapControl = axMapControl1;//传递Map控件

            axCommandBars1.LoadDesignerBars(null, null);
            axCommandBars1.ActiveMenuBar.Delete();
            axCommandBars1.Options.UseDisabledIcons = true;
            UInt32 pColor;
            pColor = System.Convert.ToUInt32(ColorTranslator.ToOle(Color.FromArgb(255, 255, 255)).ToString());
            axCommandBars1.SetSpecialColor((XtremeCommandBars.XTPColorManagerColor)15, pColor);


            m_pMapDocument = new MapDocumentClass();
            m_pMapDocument.Open(Application.StartupPath + "\\data\\JianCe.mxd", string.Empty);
            axMapControl1.Map = m_pMapDocument.get_Map(0);
            axMapControl1.Map.Name = "查询";
            axMapControl1.Extent = axMapControl1.FullExtent;

            List<IFeatureLayer> colLayers;
            colLayers = EngineFuntions.GetAllValidFeatureLayers(axMapControl1.Map);
            //设置所有图层不能选择
            EngineFuntions.SetCanSelLay("");//无图层名即所有不可选

            EngineFuntions.m_Layer_BusStation = EngineFuntions.GetLayerByName("公交站点", colLayers);
            EngineFuntions.m_Layer_BusRoad = EngineFuntions.GetLayerByName("公交站线", colLayers);


            //Determine if Alpha Context is supported, if it is, then enable it
            axDockingPane1.Options.AlphaDockingContext = true;
            //Determine if Docking Stickers is supported, if they are, then enable them
            axDockingPane1.Options.ShowDockingContextStickers = true;

            axDockingPane1.Options.ThemedFloatingFrames = true;
            axDockingPane1.TabPaintManager.Position = XTPTabPosition.xtpTabPositionTop;
            axDockingPane1.TabPaintManager.Appearance = XtremeDockingPane.XTPTabAppearanceStyle.xtpTabAppearanceVisualStudio;

            Pane ThePane = axDockingPane1.CreatePane(ForBusInfo.Pan_Layer, 200, 200, DockingDirection.DockLeftOf, null);
            ThePane.Title = "图层配置";
            axDockingPane1.FindPane(ForBusInfo.Pan_Layer).Handle = m_frmlayerToc.Handle.ToInt32();

            ThePane = axDockingPane1.CreatePane(ForBusInfo.Pan_Station, 200, 200, DockingDirection.DockLeftOf, null);
            ThePane.Title = "公交站点";
            axDockingPane1.FindPane(ForBusInfo.Pan_Station).Handle = m_frmStationPane.Handle.ToInt32();

            ThePane = axDockingPane1.CreatePane(ForBusInfo.Pan_Road, 200, 200, DockingDirection.DockLeftOf, null);
            ThePane.Title = "公交线路";
            axDockingPane1.FindPane(ForBusInfo.Pan_Road).Handle = m_frmRoadPane.Handle.ToInt32();

            axDockingPane1.AttachPane(axDockingPane1.FindPane(ForBusInfo.Pan_Road), axDockingPane1.FindPane(ForBusInfo.Pan_Station));
            axDockingPane1.AttachPane(axDockingPane1.FindPane(ForBusInfo.Pan_Layer), axDockingPane1.FindPane(ForBusInfo.Pan_Station));
            axDockingPane1.FindPane(ForBusInfo.Pan_Layer).Select();

            //'鹰眼图：
            String sHawkEyeFileName;
            sHawkEyeFileName = Application.StartupPath + "\\data\\JianCe.mxd";
            m_frmlayerToc.MapHawkEye.LoadMxFile(sHawkEyeFileName);
            m_frmlayerToc.MapHawkEye.Extent = m_frmlayerToc.MapHawkEye.FullExtent;
            //m_frmlayerToc.m_MapControl = axMapControl1.Object;
            m_frmlayerToc.TOCControl.SetBuddyControl(this.axMapControl1.Object);

            axDockingPane1.SetCommandBars(axCommandBars1.GetDispatch());
            this.WindowState = FormWindowState.Maximized;
            m_bShowLayer = false;
        }

        private void axCommandBars1_Execute(object sender, AxXtremeCommandBars._DCommandBarsEvents_ExecuteEvent e)
        {
            EngineFuntions.SetToolNull();
            switch (e.control.Id)
            {
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
                case ForBusInfo.Map3D_Full:
                    m_ToolStatus = ForBusInfo.Map3D_Full;
                    if (axMapControl1.Visible == true)
                    {
                       axMapControl1.Extent = axMapControl1.FullExtent;
                    }
                    break;
                case ForBusInfo.Bus_Add:
                    m_ToolStatus = ForBusInfo.Bus_Add;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerPencil;
                    break;
                case ForBusInfo.Bus_BackUp:


                case ForBusInfo.Bus_Dele:
                    m_ToolStatus = ForBusInfo.Bus_Dele;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerIdentify;
                    break;
                case ForBusInfo.Bus_Edit:
                    m_ToolStatus = ForBusInfo.Bus_Edit;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerHotLink;
                    break;
                case ForBusInfo.Bus_Move:
                    m_ToolStatus = ForBusInfo.Bus_Move;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerIdentify;
                    break;
                case ForBusInfo.Bus_Pano:
                    m_ToolStatus = ForBusInfo.Bus_Pano;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerIdentify;
                    break;
                case ForBusInfo.Bus_Query:
                    m_ToolStatus = ForBusInfo.Bus_Query;
                    axDockingPane1.FindPane(ForBusInfo.Pan_Station).Select();
                    break;
                case ForBusInfo.Bus_Recover:


                case ForBusInfo.Road_Add:
                    m_ToolStatus = ForBusInfo.Road_Add;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerIdentify;
                    break;
                case ForBusInfo.Road_Associate:
                    m_ToolStatus = ForBusInfo.Road_Associate;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerPencil;
                    break;
                case ForBusInfo.Road_BackUp:
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
                    m_frmRoadPane.Select();
                    break;
                case ForBusInfo.Road_Recover:

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
                     case ForBusInfo.Bus_Add:
                         {
                             frmStationPara frmPopup = new frmStationPara();
                             frmPopup.m_mapPoint = m_mapPoint;
                             if (frmPopup.ShowDialog() == DialogResult.OK)
                             {
                                 EngineFuntions.PartialRefresh(EngineFuntions.m_Layer_BusStation);
                                 frmPopup.Close();
                                 System.Threading.Thread.Sleep(1000);
                                 m_frmStationPane.RefreshGrid();
                             }
                             m_ToolStatus = -1;
                             axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
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
                                 m_CurFeature.Delete();
                                 EngineFuntions.PartialRefresh(EngineFuntions.m_Layer_BusStation);
                                 System.Threading.Thread.Sleep(1000);
                                 m_frmStationPane.RefreshGrid();
                             }
                         }
                        m_ToolStatus = -1;
                        axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
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
                                 pointMoveFeedback.Start(m_CurFeature.Shape as IPoint, m_mapPoint);
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
                                     frmPopup.m_strURL = "E:\\Code For Working\\BusInfo\\bin\\Debug\\Data\\A01\\pano1.html";
                                     frmPopup.Show();
                                 }
                             }

                             m_ToolStatus = -1;
                             axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
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
                                 m_CurFeature = m_featureCollection[0];
                                 if (m_CurFeature != null)
                                 {
                                     frmStationAllInfo frmPopup = new frmStationAllInfo();
                                     frmPopup.m_nObjectId = (int)m_CurFeature.get_Value(m_CurFeature.Fields.FindField("OBJECTID"));
                                     frmPopup.Show();
                                 }
                             }

                             m_ToolStatus = -1;
                             axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
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
                                      frmPopup.Show();
                                 }
                             }
                             m_ToolStatus = -1;
                             axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                             break;
                         }
                     case ForBusInfo.Road_Add:
                         {
                             m_CurFeatureLayer = EngineFuntions.SetCanSelLay("道路中心线");
                             if (1 == e.shift)
                             {
                                 EngineFuntions.ClickSel(m_mapPoint, true, true, 6);
                             } 
                             else
                             {
                                 EngineFuntions.ClickSel(m_mapPoint, false, true, 6);
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
	                 default :
	           	            break;
	           }
             }
             if (2 == e.button && m_ToolStatus == ForBusInfo.Road_Add)
           {
               if (MessageBox.Show("是否选择完成？\n", "提示", MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Yes)
               {
                   
                   IPolyline pPolyline = null;
                   if (EngineFuntions.GetSeledFeatures(m_CurFeatureLayer, ref m_featureCollection))
                   {
                       if (EngineFuntions.MergeLines(m_featureCollection, ref pPolyline))
                       {
                           m_ToolStatus = -1;
                           axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                           frmRoadPara frmPopup = new frmRoadPara();
                           frmPopup.m_pPolyline = pPolyline;
                           if (frmPopup.ShowDialog() == DialogResult.OK)
                           {
                               EngineFuntions.PartialRefresh(EngineFuntions.m_Layer_BusRoad);
                               frmPopup.Close();
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
                       MessageBox.Show("提示：请在公交线路中选中路段\n"  , "提示", MessageBoxButtons.OK,MessageBoxIcon.Information);
                   }
               }
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
                default :
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
                       
                    m_ToolStatus = -1;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                        break;
                    default:
                        break;
                }
            }
        }

        private void axMapControl1_OnAfterDraw(object sender, IMapControlEvents2_OnAfterDrawEvent e)
        {

        }
    }
}