namespace Businfo
{
    partial class frmStationAllInfo
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStationAllInfo));
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.Checkbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.公交站点BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.stationDataSet = new Businfo.StationDataSet();
            this.fillByOBJECTIDToolStrip = new System.Windows.Forms.ToolStrip();
            this.oBJECTIDToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.oBJECTIDToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.fillByOBJECTIDToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.fillByINOBJECTIDToolStrip = new System.Windows.Forms.ToolStrip();
            this.param1ToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.param1ToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.fillByINOBJECTIDToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.公交站点TableAdapter = new Businfo.StationDataSetTableAdapters.公交站点TableAdapter();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.定位到ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全景浏览ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.公交站点BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stationDataSet)).BeginInit();
            this.fillByOBJECTIDToolStrip.SuspendLayout();
            this.fillByINOBJECTIDToolStrip.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToAddRows = false;
            this.DataGridView1.AllowUserToDeleteRows = false;
            this.DataGridView1.AllowUserToOrderColumns = true;
            this.DataGridView1.AllowUserToResizeColumns = false;
            this.DataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Checkbox});
            this.DataGridView1.GridColor = System.Drawing.SystemColors.Desktop;
            this.DataGridView1.Location = new System.Drawing.Point(12, 27);
            this.DataGridView1.MultiSelect = false;
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.RowTemplate.Height = 23;
            this.DataGridView1.Size = new System.Drawing.Size(462, 277);
            this.DataGridView1.TabIndex = 3;
            this.DataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataGridView1_MouseDown);
            this.DataGridView1.Sorted += new System.EventHandler(this.DataGridView1_Sorted);
            this.DataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView1_CellMouseDown);
            this.DataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView1_CellMouseDoubleClick);
            this.DataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick);
            // 
            // Checkbox
            // 
            this.Checkbox.Frozen = true;
            this.Checkbox.HeaderText = "Checkbox";
            this.Checkbox.Name = "Checkbox";
            this.Checkbox.Width = 59;
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
            // fillByOBJECTIDToolStrip
            // 
            this.fillByOBJECTIDToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oBJECTIDToolStripLabel,
            this.oBJECTIDToolStripTextBox,
            this.fillByOBJECTIDToolStripButton,
            this.toolStripButton1});
            this.fillByOBJECTIDToolStrip.Location = new System.Drawing.Point(0, 0);
            this.fillByOBJECTIDToolStrip.Name = "fillByOBJECTIDToolStrip";
            this.fillByOBJECTIDToolStrip.Size = new System.Drawing.Size(494, 25);
            this.fillByOBJECTIDToolStrip.TabIndex = 5;
            this.fillByOBJECTIDToolStrip.Text = "fillByOBJECTIDToolStrip";
            this.fillByOBJECTIDToolStrip.Visible = false;
            // 
            // oBJECTIDToolStripLabel
            // 
            this.oBJECTIDToolStripLabel.Name = "oBJECTIDToolStripLabel";
            this.oBJECTIDToolStripLabel.Size = new System.Drawing.Size(59, 22);
            this.oBJECTIDToolStripLabel.Text = "OBJECTID:";
            // 
            // oBJECTIDToolStripTextBox
            // 
            this.oBJECTIDToolStripTextBox.Name = "oBJECTIDToolStripTextBox";
            this.oBJECTIDToolStripTextBox.Size = new System.Drawing.Size(100, 25);
            // 
            // fillByOBJECTIDToolStripButton
            // 
            this.fillByOBJECTIDToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fillByOBJECTIDToolStripButton.Name = "fillByOBJECTIDToolStripButton";
            this.fillByOBJECTIDToolStripButton.Size = new System.Drawing.Size(93, 22);
            this.fillByOBJECTIDToolStripButton.Text = "FillByOBJECTID";
            this.fillByOBJECTIDToolStripButton.Click += new System.EventHandler(this.fillByOBJECTIDToolStripButton_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // fillByINOBJECTIDToolStrip
            // 
            this.fillByINOBJECTIDToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.param1ToolStripLabel,
            this.param1ToolStripTextBox,
            this.fillByINOBJECTIDToolStripButton});
            this.fillByINOBJECTIDToolStrip.Location = new System.Drawing.Point(0, 0);
            this.fillByINOBJECTIDToolStrip.Name = "fillByINOBJECTIDToolStrip";
            this.fillByINOBJECTIDToolStrip.Size = new System.Drawing.Size(494, 25);
            this.fillByINOBJECTIDToolStrip.TabIndex = 6;
            this.fillByINOBJECTIDToolStrip.Text = "fillByINOBJECTIDToolStrip";
            this.fillByINOBJECTIDToolStrip.Visible = false;
            // 
            // param1ToolStripLabel
            // 
            this.param1ToolStripLabel.Name = "param1ToolStripLabel";
            this.param1ToolStripLabel.Size = new System.Drawing.Size(47, 22);
            this.param1ToolStripLabel.Text = "Param1:";
            // 
            // param1ToolStripTextBox
            // 
            this.param1ToolStripTextBox.Name = "param1ToolStripTextBox";
            this.param1ToolStripTextBox.Size = new System.Drawing.Size(100, 25);
            // 
            // fillByINOBJECTIDToolStripButton
            // 
            this.fillByINOBJECTIDToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fillByINOBJECTIDToolStripButton.Name = "fillByINOBJECTIDToolStripButton";
            this.fillByINOBJECTIDToolStripButton.Size = new System.Drawing.Size(105, 22);
            this.fillByINOBJECTIDToolStripButton.Text = "FillByINOBJECTID";
            this.fillByINOBJECTIDToolStripButton.Click += new System.EventHandler(this.fillByINOBJECTIDToolStripButton_Click);
            // 
            // 公交站点TableAdapter
            // 
            this.公交站点TableAdapter.ClearBeforeFill = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.定位到ToolStripMenuItem,
            this.全景浏览ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 48);
            // 
            // 定位到ToolStripMenuItem
            // 
            this.定位到ToolStripMenuItem.Name = "定位到ToolStripMenuItem";
            this.定位到ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.定位到ToolStripMenuItem.Text = "定位到";
            this.定位到ToolStripMenuItem.Click += new System.EventHandler(this.定位到ToolStripMenuItem_Click);
            // 
            // 全景浏览ToolStripMenuItem
            // 
            this.全景浏览ToolStripMenuItem.Name = "全景浏览ToolStripMenuItem";
            this.全景浏览ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.全景浏览ToolStripMenuItem.Text = "全景浏览";
            this.全景浏览ToolStripMenuItem.Click += new System.EventHandler(this.全景浏览ToolStripMenuItem_Click);
            // 
            // TextBox1
            // 
            this.TextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox1.Location = new System.Drawing.Point(277, 3);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(116, 21);
            this.TextBox1.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(399, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "查询";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmStationAllInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 324);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.fillByOBJECTIDToolStrip);
            this.Controls.Add(this.fillByINOBJECTIDToolStrip);
            this.Controls.Add(this.DataGridView1);
            this.Name = "frmStationAllInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "站点信息";
            this.Load += new System.EventHandler(this.frmStationAllInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.公交站点BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stationDataSet)).EndInit();
            this.fillByOBJECTIDToolStrip.ResumeLayout(false);
            this.fillByOBJECTIDToolStrip.PerformLayout();
            this.fillByINOBJECTIDToolStrip.ResumeLayout(false);
            this.fillByINOBJECTIDToolStrip.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DataGridView DataGridView1;
        private StationDataSet stationDataSet;
        private Businfo.StationDataSetTableAdapters.公交站点TableAdapter 公交站点TableAdapter;
        private System.Windows.Forms.ToolStrip fillByOBJECTIDToolStrip;
        private System.Windows.Forms.ToolStripLabel oBJECTIDToolStripLabel;
        private System.Windows.Forms.ToolStripTextBox oBJECTIDToolStripTextBox;
        private System.Windows.Forms.ToolStripButton fillByOBJECTIDToolStripButton;
        private System.Windows.Forms.BindingSource 公交站点BindingSource;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStrip fillByINOBJECTIDToolStrip;
        private System.Windows.Forms.ToolStripLabel param1ToolStripLabel;
        private System.Windows.Forms.ToolStripTextBox param1ToolStripTextBox;
        private System.Windows.Forms.ToolStripButton fillByINOBJECTIDToolStripButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Checkbox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 定位到ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全景浏览ToolStripMenuItem;
        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.Button button2;
    }
}