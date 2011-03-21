namespace Businfo
{
    partial class frmRoadPane
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.定位到ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除线路ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.属性编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关联站点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示站点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.制作单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.备份线路ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成反向线路ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(5, 10);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(116, 21);
            this.TextBox1.TabIndex = 8;
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(127, 8);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(75, 23);
            this.Button1.TabIndex = 7;
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
            this.DataGridView1.Size = new System.Drawing.Size(259, 431);
            this.DataGridView1.TabIndex = 6;
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
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.定位到ToolStripMenuItem,
            this.删除线路ToolStripMenuItem,
            this.属性编辑ToolStripMenuItem,
            this.关联站点ToolStripMenuItem,
            this.显示站点ToolStripMenuItem,
            this.制作单ToolStripMenuItem,
            this.备份线路ToolStripMenuItem,
            this.生成反向线路ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(143, 180);
            // 
            // 定位到ToolStripMenuItem
            // 
            this.定位到ToolStripMenuItem.Name = "定位到ToolStripMenuItem";
            this.定位到ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.定位到ToolStripMenuItem.Text = "定位到";
            this.定位到ToolStripMenuItem.Click += new System.EventHandler(this.定位到ToolStripMenuItem_Click);
            // 
            // 删除线路ToolStripMenuItem
            // 
            this.删除线路ToolStripMenuItem.Name = "删除线路ToolStripMenuItem";
            this.删除线路ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.删除线路ToolStripMenuItem.Text = "删除线路";
            this.删除线路ToolStripMenuItem.Click += new System.EventHandler(this.删除线路ToolStripMenuItem_Click);
            // 
            // 属性编辑ToolStripMenuItem
            // 
            this.属性编辑ToolStripMenuItem.Name = "属性编辑ToolStripMenuItem";
            this.属性编辑ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.属性编辑ToolStripMenuItem.Text = "属性编辑";
            this.属性编辑ToolStripMenuItem.Click += new System.EventHandler(this.属性编辑ToolStripMenuItem_Click);
            // 
            // 关联站点ToolStripMenuItem
            // 
            this.关联站点ToolStripMenuItem.Name = "关联站点ToolStripMenuItem";
            this.关联站点ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.关联站点ToolStripMenuItem.Text = "关联站点";
            this.关联站点ToolStripMenuItem.Click += new System.EventHandler(this.关联站点ToolStripMenuItem_Click);
            // 
            // 显示站点ToolStripMenuItem
            // 
            this.显示站点ToolStripMenuItem.Name = "显示站点ToolStripMenuItem";
            this.显示站点ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.显示站点ToolStripMenuItem.Text = "显示站点";
            this.显示站点ToolStripMenuItem.Click += new System.EventHandler(this.显示站点ToolStripMenuItem_Click);
            // 
            // 制作单ToolStripMenuItem
            // 
            this.制作单ToolStripMenuItem.Name = "制作单ToolStripMenuItem";
            this.制作单ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.制作单ToolStripMenuItem.Text = "报表输出";
            this.制作单ToolStripMenuItem.Click += new System.EventHandler(this.制作单ToolStripMenuItem_Click);
            // 
            // 备份线路ToolStripMenuItem
            // 
            this.备份线路ToolStripMenuItem.Name = "备份线路ToolStripMenuItem";
            this.备份线路ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.备份线路ToolStripMenuItem.Text = "备份线路";
            this.备份线路ToolStripMenuItem.Click += new System.EventHandler(this.备份线路ToolStripMenuItem_Click);
            // 
            // 生成反向线路ToolStripMenuItem
            // 
            this.生成反向线路ToolStripMenuItem.Name = "生成反向线路ToolStripMenuItem";
            this.生成反向线路ToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.生成反向线路ToolStripMenuItem.Text = "生成反向线路";
            this.生成反向线路ToolStripMenuItem.Click += new System.EventHandler(this.生成反向线路ToolStripMenuItem_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(5, 37);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "全选";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // frmRoadPane
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.DataGridView1);
            this.Name = "frmRoadPane";
            this.Size = new System.Drawing.Size(269, 498);
            this.Load += new System.EventHandler(this.frmRoadPane_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.DataGridView DataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 定位到ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除线路ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 属性编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关联站点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示站点ToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ToolStripMenuItem 制作单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 备份线路ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckBox;
        private System.Windows.Forms.ToolStripMenuItem 生成反向线路ToolStripMenuItem;
    }
}
