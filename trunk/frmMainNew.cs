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
        #region ��������
        IMapDocument m_pMapDocument;
        public frmFlash m_frmFlash;
        public frmlayerToc m_frmlayerToc = new frmlayerToc();
        public frmStationPane m_frmStationPane = new frmStationPane();
        public frmRoadPane m_frmRoadPane = new frmRoadPane();
        public frmFacilitiesPane m_frmFacilityPane = new frmFacilitiesPane();
        public int m_ToolStatus;
        public GISPoint m_mapPoint;//�������ѯ��������
        public GISPoint m_FormPoint;//�����·��˳��ڵ�
        public IFeatureLayer m_CurFeatureLayer ;//��ǰFeatureLayer
        public IFeature m_CurFeature ;  //��ǰfeature
        public List<IFeature> m_featureCollection = new List<IFeature>();   //�õ�����ѡ�е�feature
        public List<IFeature> m_featureColByOrder = new List<IFeature>();   //ѡ�����100ʱfea���ǰ���˳�����еģ�����˹�����
        public List<IPolyline> m_PolylineCollection = new List<IPolyline>();   //�õ�����ѡ�е�IPolyline
        public MovePointFeedbackClass m_FeedBack;
        public bool m_bShowLayer;
        public int m_nPLineNum = 1;//��·ÿ�ΰ�������
        AoInitialize m_pAoInitialize = new AoInitialize();//�жϰ汾
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
                //��Ȼ����_xlApp.Quit()����������COM�����������פ�����ڴ��ڵĽ��̣�ÿʵ��һ��Excel��Excell���̶�һ���� 
                //������������գ����鲻Ҫ�ý��̵�KILL()������������ܻ��ɱ�޹���:)�� 
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

            EngineFuntions.m_AxMapControl = axMapControl1;//����Map�ؼ�

            axCommandBars1.LoadDesignerBars(null, null);
            axCommandBars1.ActiveMenuBar.Delete();
            axCommandBars1.Options.UseDisabledIcons = true;
            UInt32 pColor;
            pColor = System.Convert.ToUInt32(ColorTranslator.ToOle(Color.FromArgb(255, 255, 255)).ToString());
            axCommandBars1.SetSpecialColor((XtremeCommandBars.XTPColorManagerColor)15, pColor);

            m_pMapDocument = new MapDocumentClass();
            m_pMapDocument.Open(ForBusInfo.Mxd_Name, string.Empty);
            axMapControl1.Map = m_pMapDocument.get_Map(0);
            axMapControl1.Map.Name = "��ѯ";
            //axMapControl1.Extent = axMapControl1.FullExtent;

            List<IFeatureLayer> colLayers;
            colLayers = EngineFuntions.GetAllValidFeatureLayers(axMapControl1.Map);
            //��������ͼ�㲻��ѡ��
            EngineFuntions.SetCanSelLay("");//��ͼ���������в���ѡ

            EngineFuntions.m_Layer_BusStation = EngineFuntions.GetLayerByName("����վ��", colLayers);
            EngineFuntions.m_Layer_BusRoad = EngineFuntions.GetLayerByName("����վ��", colLayers);
            EngineFuntions.m_Layer_BackRoad = EngineFuntions.GetLayerByName("վ�߱���", colLayers);


            //Determine if Alpha Context is supported, if it is, then enable it
            axDockingPane1.Options.AlphaDockingContext = true;
            //Determine if Docking Stickers is supported, if they are, then enable them
            axDockingPane1.Options.ShowDockingContextStickers = true;

            axDockingPane1.Options.ThemedFloatingFrames = true;
            axDockingPane1.TabPaintManager.Position = XTPTabPosition.xtpTabPositionTop;
            axDockingPane1.TabPaintManager.Appearance = XtremeDockingPane.XTPTabAppearanceStyle.xtpTabAppearanceVisualStudio;

            XtremeDockingPane.Pane ThePane = axDockingPane1.CreatePane(ForBusInfo.Pan_Layer, 260, 200, DockingDirection.DockLeftOf, null);
            ThePane.Title = "ͼ ��";
            axDockingPane1.FindPane(ForBusInfo.Pan_Layer).Handle = m_frmlayerToc.Handle.ToInt32();

            ThePane = axDockingPane1.CreatePane(ForBusInfo.Pan_Station, 260, 200, DockingDirection.DockLeftOf, null);
            ThePane.Title = "վ ��";
            axDockingPane1.FindPane(ForBusInfo.Pan_Station).Handle = m_frmStationPane.Handle.ToInt32();

            ThePane = axDockingPane1.CreatePane(ForBusInfo.Pan_Road, 260, 200, DockingDirection.DockLeftOf, null);
            ThePane.Title = "�� ·";
            axDockingPane1.FindPane(ForBusInfo.Pan_Road).Handle = m_frmRoadPane.Handle.ToInt32();

            ThePane = axDockingPane1.CreatePane(ForBusInfo.Pan_Facility, 260, 200, DockingDirection.DockLeftOf, null);
            ThePane.Title = "�� ʩ";
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
            case "վ�����":
                axDockingPane1.FindPane(ForBusInfo.Pan_Facility).Closed = true;
            	break;
            case "��·����":
                axDockingPane1.FindPane(ForBusInfo.Pan_Station).Closed = true;
                axDockingPane1.FindPane(ForBusInfo.Pan_Facility).Closed = true;
                break;
            case "��ʩ����":
                axDockingPane1.FindPane(ForBusInfo.Pan_Road).Closed = true;
                axDockingPane1.FindPane(ForBusInfo.Pan_Station).Closed = true;
                break;
            case "���":
                axDockingPane1.FindPane(ForBusInfo.Pan_Road).Closed = true;
                axDockingPane1.FindPane(ForBusInfo.Pan_Facility).Closed = true;
                axDockingPane1.FindPane(ForBusInfo.Pan_Station).Closed = true;
                break;
            }

            
            //'ӥ��ͼ��
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

            if(ForBusInfo.Login_Operation != "")//�����û�Ȩ�޶�Ӧ��ֹ�Ĳ���
            {
                string[] strColu = ForBusInfo.Login_Operation.Split('��');
                int nCol;
                foreach (string eStrRow in strColu)
                {
                    nCol = Convert.ToInt32(eStrRow[0].ToString());
                    string strRow = eStrRow.Substring(2);
                    string[] strRows = strRow.Split('��');
                    foreach (string eStrRows in strRows)
                    {
                        axCommandBars1[nCol].Controls[Convert.ToInt32(eStrRows)].Enabled = false;
                        
                    }
                }
            }

            // Set what the Help file will be for the HelpProvider.
            this.helpProvider2.HelpNamespace = Winapp.StartupPath + @"\�û��ֲ�.doc";
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
                    System.Diagnostics.Process.Start(Winapp.StartupPath + @"\�û��ֲ�.doc");
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
                //ǰһ��
                case ForBusInfo.Map3D_PreView:
                    m_ToolStatus = ForBusInfo.Map3D_PreView;
                    if (axMapControl1.Visible == true)
                    {
                       EngineFuntions.GoBack();
                    }
                    break;
                //��һ��
                case ForBusInfo.Map3D_NextView:
                    m_ToolStatus = ForBusInfo.Map3D_NextView;
                    if (axMapControl1.Visible == true)
                    {
                       EngineFuntions.GoNext();
                    }
                    break;
                case ForBusInfo.Map3D_Distance://���㳤��
                    break;

                case ForBusInfo.Map3D_Area://�������
                    break;
                
                case ForBusInfo.Map3D_Select ://����ѡ��
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
                case ForBusInfo.Bus_BackUp://վ�㲻�ñ���


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

                case ForBusInfo.Bus_Recover://վ�㲻�ûָ�


                case ForBusInfo.Road_Add:
                    m_ToolStatus = ForBusInfo.Road_Add;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerIdentify;
                    break;

                case ForBusInfo.Road_Associate:
                    m_ToolStatus = ForBusInfo.Road_Associate;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerPencil;
                    break;
                case ForBusInfo.Road_BackUp://վ�߱���
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
                case ForBusInfo.Road_Recover://վ�߻ָ�
                    m_ToolStatus = ForBusInfo.Road_Recover;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerIdentify;
                    frmRoadRecover frmPopup = new frmRoadRecover();
                    frmPopup.ShowDialog();
                    break;
                case ForBusInfo.BusInfo_Layer://����ͼ��
                    List<string> colLayerName = new List<string>();
                    colLayerName.Add("����վ��");
                    colLayerName.Add("��·������");
                    if (m_bShowLayer)
                    {
                        colLayerName.Add("������ͼ");
                        colLayerName.Add("����վ��");
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
                case ForBusInfo.Table_RoadExport://����������
                    m_ToolStatus = ForBusInfo.Table_RoadExport;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerPencil;
                    //axDockingPane1.FindPane(ForBusInfo.Pan_Road).Select();
                    break;
                case ForBusInfo.Table_Operation://�鿴����
                    m_ToolStatus = ForBusInfo.Table_Operation;
                    //axDockingPane1.FindPane(ForBusInfo.Pan_Road).Select();
                    frmOperation frmPopup1 = new frmOperation();
                    frmPopup1.ShowDialog();
                    break;
                case ForBusInfo.Table_StationTable://·�����
                    m_ToolStatus = ForBusInfo.Table_StationTable;
                    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerPencil;
                    //axDockingPane1.FindPane(ForBusInfo.Pan_Station).Select();
                    break;
                case ForBusInfo.Road_Pause://������ʱ����·
                    m_ToolStatus = ForBusInfo.Road_Pause;
                    m_CurFeature = null;
                    if (MessageBox.Show("�Ƿ���ʱ���棿\n", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
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
                            MessageBox.Show("��ʾ�����ڵ�·������·��ѡ��·�Σ�\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        EngineFuntions.m_AxMapControl.Map.ClearSelection();
                        EngineFuntions.m_AxMapControl.Refresh();
                    }
                    break;

                case ForBusInfo.Road_Resume://������ʱ��·���б༭

                    string strLID = ForBusInfo.GetProfileString("LineId", "IDNumber", Winapp.StartupPath + "\\Businfo.ini");
                    strLID = strLID.Substring(1);
                    string[] strLIDs = strLID.Split(',');
                         
                    if(strLIDs.Length>2)
                    {
                        m_CurFeatureLayer = EngineFuntions.SetCanSelLay("��·������");
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

                case ForBusInfo.Road_End://�����·���
                    m_ToolStatus = ForBusInfo.Road_End;
                    m_CurFeature = null;
                    if (MessageBox.Show("�Ƿ�ѡ����ɣ�\n", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
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
                                MessageBox.Show("��ʾ��ѡ�����·���������ģ�\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("��ʾ�����ڹ�����·��ѡ��·��\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        m_PolylineCollection.Clear();
                        EngineFuntions.m_AxMapControl.Map.ClearSelection();
                        EngineFuntions.m_AxMapControl.Refresh();
                    }
                    break;

                case ForBusInfo.Road_Reversed://��·����
                    //m_ToolStatus = ForBusInfo.Road_Reversed;
                    if (m_ToolStatus == ForBusInfo.Road_End && m_CurFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolyline)
                    {
                        string strDirect;
                        if (m_CurFeature.get_Value(m_CurFeature.Fields.FindField("RoadTravel")).ToString() == "ȥ��")
                            strDirect = "����";
                        else
                            strDirect = "ȥ��";
                        IFeature pFeature = EngineFuntions.GetOneSeartchFeature(EngineFuntions.m_Layer_BusRoad, string.Format("Roadname = '{0}' AND RoadTravel = '{1}'", m_CurFeature.get_Value(m_CurFeature.Fields.FindField("Roadname")).ToString(), strDirect));
                        if (pFeature != null)
                        {
                            MessageBox.Show("��·ͼ���Ѿ����ڷ�����·\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        IPolyline pPLine = m_CurFeature.ShapeCopy as IPolyline;
                        pPLine.ReverseOrientation();
                        m_CurFeature.Shape = pPLine;
                        m_CurFeature.set_Value(m_CurFeature.Fields.FindField("RoadTravel"), strDirect);
                        EngineFuntions.CopyFeature(EngineFuntions.m_Layer_BusRoad, m_CurFeature);
                        System.Threading.Thread.Sleep(1000);
                        m_frmRoadPane.RefreshGrid();
                        MessageBox.Show("������·�����ɣ������վ�㣡\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                             m_CurFeatureLayer = EngineFuntions.SetCanSelLay("��·������");
                             EngineFuntions.ClickSel(m_mapPoint, false, false, 24);
                             if (EngineFuntions.GetSeledFeatures(m_CurFeatureLayer, ref  m_featureCollection))
                             {
                                 foreach (IFeature pfea in m_featureCollection)
                                 {
                                     frmPopup.m_ListRoadName.Add(pfea.get_Value(pfea.Fields.FindField("��·����")) as string);
                                 }
                             }
                            
                             if (frmPopup.ShowDialog() == DialogResult.OK)
                             {
                                 EngineFuntions.PartialRefresh(EngineFuntions.m_Layer_BusStation);
                                 ForBusInfo.Add_Log(ForBusInfo.Login_name, "���վ��", frmPopup.m_strLog, "");
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
                        m_CurFeatureLayer = EngineFuntions.SetCanSelLay("����վ��");
                        EngineFuntions.ClickSel(m_mapPoint, false, false, 6);
                        if (EngineFuntions.GetSeledFeatures(m_CurFeatureLayer, ref  m_featureCollection))
                         {
                             m_CurFeature = m_featureCollection[0];
                             
                             if (m_CurFeature != null)
                             {
                                 string strName = m_CurFeature.get_Value(m_CurFeature.Fields.FindField("StationName")).ToString();
                                 if (MessageBox.Show(string.Format("ȷ��ɾ��վ�㣺{0}!", strName), "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                                 {
                                     m_CurFeature.Delete();
                                     EngineFuntions.m_AxMapControl.ActiveView.Refresh();
                                     System.Threading.Thread.Sleep(1000);
                                     m_frmStationPane.RefreshGrid();
                                     ForBusInfo.Add_Log(ForBusInfo.Login_name, "ɾ��վ��", strName, "");

                                 }
                             }
                         }
                        //m_ToolStatus = -1;
                        //axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                        break;
                    case ForBusInfo.Bus_Move:
                        {
                            m_CurFeatureLayer = EngineFuntions.SetCanSelLay("����վ��");
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
                                     ForBusInfo.Add_Log(ForBusInfo.Login_name, "�ƶ�վ��", strName, "");
                                 }
                             }
                             break;
                        }
                    case ForBusInfo.Bus_Pano:
                         {
                             m_CurFeatureLayer = EngineFuntions.SetCanSelLay("����վ��");
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
                             //GetSeartchFeatures�������SetCanSelLay��ClickSel��GetSeledFeatures�������衣
                             //m_featureCollection = EngineEditOperations.GetSeartchFeatures(EngineEditOperations.m_Layer_BusStation, m_mapPoint);
                             m_CurFeatureLayer = EngineFuntions.SetCanSelLay("����վ��");
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
                             m_CurFeatureLayer = EngineFuntions.SetCanSelLay("����վ��");
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
                             //GetSeartchFeatures�������SetCanSelLay��ClickSel��GetSeledFeatures�������衣
                             //m_featureCollection = EngineEditOperations.GetSeartchFeatures(EngineEditOperations.m_Layer_BusRoad, m_mapPoint);
                             m_CurFeatureLayer = EngineFuntions.SetCanSelLay("����վ��");
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
                             m_CurFeatureLayer = EngineFuntions.SetCanSelLay("��·������");
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
                                         EngineFuntions.ClickSel(m_mapPoint, true, true, 6);//�������ٵ��ѡ��һ�Σ�ȡ������������
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
                                    
                                    if (m_featureCollection.Count == 99)//����100ʱѡ�񼯾Ͳ��ǰ���ѡ���Ⱥ�˳���г��ˣ������˹��ж�
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
                                    //System.IO.FileStream fst = new System.IO.FileStream("C:\\222.txt", FileMode.Append);//׷��ģʽ
                                    //StreamWriter stw = new StreamWriter(fst, System.Text.Encoding.GetEncoding("utf-8"));//ָ������.���򽫳���!
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
                                         MessageBox.Show("��·��������\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                         EngineFuntions.ClickSel(m_mapPoint, true, true, 6);//�������ٵ��ѡ��һ�Σ�ȡ������������
                                     }
                                }
                                else if (2 < m_featureCollection.Count)//�����ѡ���ߣ�����ɾ�����ߡ�
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
                                            MessageBox.Show("��·������\n", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            EngineFuntions.ClickSel(m_mapPoint, true, true, 6);//����Ĳ��������ߣ��ٵ��һ������ѡ�����
                                            break;
                                        }
                                    }
                                    if (m_bShowLayer)
                                    {
                                        m_nPLineNum = m_featureCollection.Count;
                                        if (!EngineFuntions.GetLinkPoint(m_PolylineCollection[m_PolylineCollection.Count - 3], m_PolylineCollection[m_PolylineCollection.Count - 2], ref m_FormPoint))
                                        {
                                            EngineFuntions.ClickSel(m_mapPoint, true, true, 6);//�������ٵ��ѡ��һ�Σ�ȡ������������
                                        }
                                        else
                                        {
                                            m_PolylineCollection.RemoveAt(m_PolylineCollection.Count - 1);//�����һ���ߣ�ɾ������
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
                            m_CurFeatureLayer = EngineFuntions.SetCanSelLay("����վ��");
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
                             m_CurFeatureLayer = EngineFuntions.SetCanSelLay("����վ��");
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
                     case ForBusInfo.Table_RoadExport://����������
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
                     case ForBusInfo.Table_StationTable://·������
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