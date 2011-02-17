using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using Businfo.Globe;

namespace Businfo
{
    public partial class frmlayerToc : UserControl
    {
        ITOCControl m_pTOCControl;
        public frmlayerToc()
        {
            InitializeComponent();
        }

        private void frmlayerToc_Resize(object sender, EventArgs e)
        {
            TOCControl.SetBounds(1, 1, this.ClientRectangle.Width - 2, this.ClientRectangle.Height - this.MapHawkEye.Height);
            MapHawkEye.SetBounds(1, this.TOCControl.Height, this.ClientRectangle.Width - 2, this.ClientRectangle.Height - this.TOCControl.Height);
        }

        private void frmlayerToc_Load(object sender, EventArgs e)
        {
            m_pTOCControl = (ITOCControl)TOCControl.Object;
            TOCControl.EnableLayerDragDrop = true;
        }

        private void MapHawkEye_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            IEnvelope pEnvelope = MapHawkEye.TrackRectangle();
            MapHawkEye.ActiveView.Refresh();
            EngineFuntions.m_AxMapControl.ActiveView.Extent = pEnvelope;
            EngineFuntions.m_AxMapControl.ActiveView.Refresh();
        }

    }
}
