﻿namespace Businfo
{
    partial class frmStationPane
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.Button1 = new System.Windows.Forms.Button();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.CheckBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.公交站点BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.stationDataSet = new Businfo.StationDataSet();
            this.ContextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.定位到ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除站点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑属性ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全景浏览ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillByStationNameToolStrip = new System.Windows.Forms.ToolStrip();
            this.stationNameToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.stationNameToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.fillByStationNameToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.公交站点TableAdapter = new Businfo.StationDataSetTableAdapters.公交站点TableAdapter();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.公交站点BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stationDataSet)).BeginInit();
            this.ContextMenuStrip1.SuspendLayout();
            this.fillByStationNameToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(5, 10);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(116, 21);
            this.TextBox1.TabIndex = 5;
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(127, 8);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(75, 23);
            this.Button1.TabIndex = 4;
            this.Button1.Text = "查询";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToAddRows = false;
            this.DataGridView1.AllowUserToDeleteRows = false;
            this.DataGridView1.AllowUserToOrderColumns = true;
            this.DataGridView1.AllowUserToResizeColumns = false;
            this.DataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckBox});
            this.DataGridView1.Location = new System.Drawing.Point(5, 59);
            this.DataGridView1.MultiSelect = false;
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.RowHeadersWidth = 20;
            this.DataGridView1.RowTemplate.Height = 23;
            this.DataGridView1.Size = new System.Drawing.Size(244, 419);
            this.DataGridView1.TabIndex = 3;
            this.DataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataGridView1_MouseDown);
            this.DataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView1_CellMouseDown);
            // 
            // CheckBox
            // 
            this.CheckBox.Frozen = true;
            this.CheckBox.HeaderText = "CheckBox";
            this.CheckBox.Name = "CheckBox";
            this.CheckBox.Width = 35;
            // 
            // 公交站点BindingSource
            // 
            this.公交站点BindingSource.DataMember = "公交站点";
            this.公交站点BindingSource.DataSource = this.stationDataSet;
            // 
            // stationDataSet
            // 
            this.stationDataSet.DataSetName = "StationDataSet";
            this.stationDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ContextMenuStrip1
            // 
            this.ContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.定位到ToolStripMenuItem,
            this.删除站点ToolStripMenuItem,
            this.编辑属性ToolStripMenuItem,
            this.全景浏览ToolStripMenuItem});
            this.ContextMenuStrip1.Name = "ContextMenuStrip1";
            this.ContextMenuStrip1.Size = new System.Drawing.Size(119, 92);
            // 
            // 定位到ToolStripMenuItem
            // 
            this.定位到ToolStripMenuItem.Name = "定位到ToolStripMenuItem";
            this.定位到ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.定位到ToolStripMenuItem.Text = "定位到";
            this.定位到ToolStripMenuItem.Click += new System.EventHandler(this.定位到ToolStripMenuItem_Click);
            // 
            // 删除站点ToolStripMenuItem
            // 
            this.删除站点ToolStripMenuItem.Name = "删除站点ToolStripMenuItem";
            this.删除站点ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.删除站点ToolStripMenuItem.Text = "删除站点";
            this.删除站点ToolStripMenuItem.Click += new System.EventHandler(this.删除站点ToolStripMenuItem_Click);
            // 
            // 编辑属性ToolStripMenuItem
            // 
            this.编辑属性ToolStripMenuItem.Name = "编辑属性ToolStripMenuItem";
            this.编辑属性ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.编辑属性ToolStripMenuItem.Text = "编辑属性";
            this.编辑属性ToolStripMenuItem.Click += new System.EventHandler(this.编辑属性ToolStripMenuItem_Click);
            // 
            // 全景浏览ToolStripMenuItem
            // 
            this.全景浏览ToolStripMenuItem.Name = "全景浏览ToolStripMenuItem";
            this.全景浏览ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.全景浏览ToolStripMenuItem.Text = "全景浏览";
            this.全景浏览ToolStripMenuItem.Click += new System.EventHandler(this.全景浏览ToolStripMenuItem_Click);
            // 
            // fillByStationNameToolStrip
            // 
            this.fillByStationNameToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stationNameToolStripLabel,
            this.stationNameToolStripTextBox,
            this.fillByStationNameToolStripButton});
            this.fillByStationNameToolStrip.Location = new System.Drawing.Point(0, 0);
            this.fillByStationNameToolStrip.Name = "fillByStationNameToolStrip";
            this.fillByStationNameToolStrip.Size = new System.Drawing.Size(254, 25);
            this.fillByStationNameToolStrip.TabIndex = 6;
            this.fillByStationNameToolStrip.Text = "fillByStationNameToolStrip";
            this.fillByStationNameToolStrip.Visible = false;
            // 
            // stationNameToolStripLabel
            // 
            this.stationNameToolStripLabel.Name = "stationNameToolStripLabel";
            this.stationNameToolStripLabel.Size = new System.Drawing.Size(77, 22);
            this.stationNameToolStripLabel.Text = "StationName:";
            // 
            // stationNameToolStripTextBox
            // 
            this.stationNameToolStripTextBox.Name = "stationNameToolStripTextBox";
            this.stationNameToolStripTextBox.Size = new System.Drawing.Size(100, 25);
            // 
            // fillByStationNameToolStripButton
            // 
            this.fillByStationNameToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fillByStationNameToolStripButton.Name = "fillByStationNameToolStripButton";
            this.fillByStationNameToolStripButton.Size = new System.Drawing.Size(111, 16);
            this.fillByStationNameToolStripButton.Text = "FillByStationName";
            this.fillByStationNameToolStripButton.Click += new System.EventHandler(this.fillByStationNameToolStripButton_Click);
            // 
            // 公交站点TableAdapter
            // 
            this.公交站点TableAdapter.ClearBeforeFill = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(5, 37);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "全选";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // frmStationPane
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.fillByStationNameToolStrip);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.DataGridView1);
            this.Name = "frmStationPane";
            this.Size = new System.Drawing.Size(254, 486);
            this.Load += new System.EventHandler(this.frmStationPane_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.公交站点BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stationDataSet)).EndInit();
            this.ContextMenuStrip1.ResumeLayout(false);
            this.fillByStationNameToolStrip.ResumeLayout(false);
            this.fillByStationNameToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.DataGridView DataGridView1;
        internal System.Windows.Forms.ContextMenuStrip ContextMenuStrip1;
        internal System.Windows.Forms.ToolStripMenuItem 定位到ToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem 删除站点ToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem 编辑属性ToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem 全景浏览ToolStripMenuItem;
        private System.Windows.Forms.BindingSource 公交站点BindingSource;
        private StationDataSet stationDataSet;
        private Businfo.StationDataSetTableAdapters.公交站点TableAdapter 公交站点TableAdapter;
        private System.Windows.Forms.ToolStrip fillByStationNameToolStrip;
        private System.Windows.Forms.ToolStripLabel stationNameToolStripLabel;
        private System.Windows.Forms.ToolStripTextBox stationNameToolStripTextBox;
        private System.Windows.Forms.ToolStripButton fillByStationNameToolStripButton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckBox;
    }
}