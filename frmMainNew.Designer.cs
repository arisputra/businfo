﻿namespace Businfo
{
    partial class frmMainNew
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainNew));
            this.panel1 = new System.Windows.Forms.Panel();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.axCommandBars1 = new AxXtremeCommandBars.AxCommandBars();
            this.axDockingPane1 = new AxXtremeDockingPane.AxDockingPane();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.helpProvider2 = new System.Windows.Forms.HelpProvider();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCommandBars1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axDockingPane1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.axLicenseControl1);
            this.panel1.Controls.Add(this.axMapControl1);
            this.panel1.Controls.Add(this.axCommandBars1);
            this.panel1.Controls.Add(this.axDockingPane1);
            this.panel1.Location = new System.Drawing.Point(0, 86);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(702, 419);
            this.panel1.TabIndex = 1;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(628, 350);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 15;
            this.axLicenseControl1.Enter += new System.EventHandler(this.axLicenseControl1_Enter);
            // 
            // axMapControl1
            // 
            this.axMapControl1.Location = new System.Drawing.Point(162, 6);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(537, 410);
            this.axMapControl1.TabIndex = 14;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl1_OnMouseMove);
            this.axMapControl1.OnMouseUp += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseUpEventHandler(this.axMapControl1_OnMouseUp);
            this.axMapControl1.OnAfterScreenDraw += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnAfterScreenDrawEventHandler(this.axMapControl1_OnAfterScreenDraw);
            // 
            // axCommandBars1
            // 
            this.axCommandBars1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.axCommandBars1.Enabled = true;
            this.axCommandBars1.Location = new System.Drawing.Point(90, 87);
            this.axCommandBars1.Name = "axCommandBars1";
            this.axCommandBars1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCommandBars1.OcxState")));
            this.axCommandBars1.Size = new System.Drawing.Size(24, 24);
            this.axCommandBars1.TabIndex = 16;
            this.axCommandBars1.Execute += new AxXtremeCommandBars._DCommandBarsEvents_ExecuteEventHandler(this.axCommandBars1_Execute);
            this.axCommandBars1.ResizeEvent += new System.EventHandler(this.axCommandBars1_ResizeEvent);
            // 
            // axDockingPane1
            // 
            this.axDockingPane1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.axDockingPane1.Enabled = true;
            this.axDockingPane1.Location = new System.Drawing.Point(60, 87);
            this.axDockingPane1.Name = "axDockingPane1";
            this.axDockingPane1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axDockingPane1.OcxState")));
            this.axDockingPane1.Size = new System.Drawing.Size(24, 24);
            this.axDockingPane1.TabIndex = 17;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1280, 86);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // frmMainNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 503);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.helpProvider2.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.Topic);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMainNew";
            this.helpProvider2.SetShowHelp(this, true);
            this.Text = "武汉市公交业务管理信息系统";
            this.Load += new System.EventHandler(this.frmMainNew_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMainNew_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMainNew_FormClosing);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCommandBars1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axDockingPane1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private AxXtremeDockingPane.AxDockingPane axDockingPane1;
        internal System.Windows.Forms.Panel panel1;
        public  AxXtremeCommandBars.AxCommandBars axCommandBars1;
        internal ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private System.Windows.Forms.HelpProvider helpProvider2;

    }
}