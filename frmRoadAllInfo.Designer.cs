namespace Businfo
{
    partial class frmRoadAllInfo
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
            this.Button1 = new System.Windows.Forms.Button();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.Checkbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.公交站线BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.roadDataSet = new Businfo.RoadDataSet();
            this.公交站线TableAdapter = new Businfo.RoadDataSetTableAdapters.公交站线TableAdapter();
            this.fillByINOBJECTIDToolStrip = new System.Windows.Forms.ToolStrip();
            this.param1ToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.param1ToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.fillByINOBJECTIDToolStripButton = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.公交站线BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.roadDataSet)).BeginInit();
            this.fillByINOBJECTIDToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(12, 8);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(75, 23);
            this.Button1.TabIndex = 6;
            this.Button1.Text = "显示全部";
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
            this.DataGridView1.Location = new System.Drawing.Point(12, 34);
            this.DataGridView1.MultiSelect = false;
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.RowTemplate.Height = 23;
            this.DataGridView1.Size = new System.Drawing.Size(470, 290);
            this.DataGridView1.TabIndex = 5;
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
            // 公交站线BindingSource
            // 
            this.公交站线BindingSource.DataMember = "公交站线";
            this.公交站线BindingSource.DataSource = this.roadDataSet;
            // 
            // roadDataSet
            // 
            this.roadDataSet.DataSetName = "RoadDataSet";
            this.roadDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // 公交站线TableAdapter
            // 
            this.公交站线TableAdapter.ClearBeforeFill = true;
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
            this.fillByINOBJECTIDToolStrip.TabIndex = 7;
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
            // frmRoadAllInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 331);
            this.Controls.Add(this.fillByINOBJECTIDToolStrip);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.DataGridView1);
            this.Name = "frmRoadAllInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "线路信息";
            this.Load += new System.EventHandler(this.frmRoadAllInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.公交站线BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.roadDataSet)).EndInit();
            this.fillByINOBJECTIDToolStrip.ResumeLayout(false);
            this.fillByINOBJECTIDToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.DataGridView DataGridView1;
        private RoadDataSet roadDataSet;
        private System.Windows.Forms.BindingSource 公交站线BindingSource;
        private Businfo.RoadDataSetTableAdapters.公交站线TableAdapter 公交站线TableAdapter;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Checkbox;
        private System.Windows.Forms.ToolStrip fillByINOBJECTIDToolStrip;
        private System.Windows.Forms.ToolStripLabel param1ToolStripLabel;
        private System.Windows.Forms.ToolStripTextBox param1ToolStripTextBox;
        private System.Windows.Forms.ToolStripButton fillByINOBJECTIDToolStripButton;
    }
}