namespace Businfo
{
    partial class frmRoadAndStation
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
            this.Lab_Name = new System.Windows.Forms.Label();
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.BtnTrue = new System.Windows.Forms.Button();
            this.ListBox1 = new System.Windows.Forms.ListBox();
            this.BtnUP = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnDele = new System.Windows.Forms.Button();
            this.BtnDOWN = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Lab_Name
            // 
            this.Lab_Name.AutoSize = true;
            this.Lab_Name.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lab_Name.Location = new System.Drawing.Point(12, 6);
            this.Lab_Name.Name = "Lab_Name";
            this.Lab_Name.Size = new System.Drawing.Size(93, 16);
            this.Lab_Name.TabIndex = 8;
            this.Lab_Name.Text = "线路名称：";
            // 
            // CheckBox1
            // 
            this.CheckBox1.AutoSize = true;
            this.CheckBox1.Location = new System.Drawing.Point(9, 18);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(91, 20);
            this.CheckBox1.TabIndex = 2;
            this.CheckBox1.Text = "换边选择";
            this.CheckBox1.UseVisualStyleBackColor = true;
            this.CheckBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // BtnTrue
            // 
            this.BtnTrue.Location = new System.Drawing.Point(234, 324);
            this.BtnTrue.Name = "BtnTrue";
            this.BtnTrue.Size = new System.Drawing.Size(61, 44);
            this.BtnTrue.TabIndex = 1;
            this.BtnTrue.Text = "确定";
            this.BtnTrue.UseVisualStyleBackColor = true;
            this.BtnTrue.Click += new System.EventHandler(this.BtnTrue_Click);
            // 
            // ListBox1
            // 
            this.ListBox1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ListBox1.FormattingEnabled = true;
            this.ListBox1.ItemHeight = 20;
            this.ListBox1.Items.AddRange(new object[] {
            "11111",
            "22222",
            "33333",
            "44444",
            "55555"});
            this.ListBox1.Location = new System.Drawing.Point(7, 42);
            this.ListBox1.Name = "ListBox1";
            this.ListBox1.Size = new System.Drawing.Size(222, 324);
            this.ListBox1.TabIndex = 0;
            // 
            // BtnUP
            // 
            this.BtnUP.Location = new System.Drawing.Point(235, 44);
            this.BtnUP.Name = "BtnUP";
            this.BtnUP.Size = new System.Drawing.Size(61, 46);
            this.BtnUP.TabIndex = 1;
            this.BtnUP.Text = "上移";
            this.BtnUP.UseVisualStyleBackColor = true;
            this.BtnUP.Click += new System.EventHandler(this.BtnUP_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.CheckBox1);
            this.GroupBox1.Controls.Add(this.BtnTrue);
            this.GroupBox1.Controls.Add(this.ListBox1);
            this.GroupBox1.Controls.Add(this.BtnUP);
            this.GroupBox1.Controls.Add(this.BtnDele);
            this.GroupBox1.Controls.Add(this.BtnDOWN);
            this.GroupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.GroupBox1.Location = new System.Drawing.Point(8, 40);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(305, 379);
            this.GroupBox1.TabIndex = 6;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "线路站点列表";
            // 
            // BtnDele
            // 
            this.BtnDele.Location = new System.Drawing.Point(234, 180);
            this.BtnDele.Name = "BtnDele";
            this.BtnDele.Size = new System.Drawing.Size(61, 46);
            this.BtnDele.TabIndex = 1;
            this.BtnDele.Text = "删除";
            this.BtnDele.UseVisualStyleBackColor = true;
            this.BtnDele.Click += new System.EventHandler(this.BtnDele_Click);
            // 
            // BtnDOWN
            // 
            this.BtnDOWN.Location = new System.Drawing.Point(234, 109);
            this.BtnDOWN.Name = "BtnDOWN";
            this.BtnDOWN.Size = new System.Drawing.Size(61, 46);
            this.BtnDOWN.TabIndex = 1;
            this.BtnDOWN.Text = "下移";
            this.BtnDOWN.UseVisualStyleBackColor = true;
            this.BtnDOWN.Click += new System.EventHandler(this.BtnDOWN_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label1.Location = new System.Drawing.Point(105, 6);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(0, 16);
            this.Label1.TabIndex = 7;
            // 
            // frmRoadAndStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 423);
            this.Controls.Add(this.Lab_Name);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.Label1);
            this.Name = "frmRoadAndStation";
            this.Text = "关联站点";
            this.Load += new System.EventHandler(this.frmRoadAndStation_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Lab_Name;
        internal System.Windows.Forms.CheckBox CheckBox1;
        internal System.Windows.Forms.Button BtnTrue;
        internal System.Windows.Forms.ListBox ListBox1;
        internal System.Windows.Forms.Button BtnUP;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Button BtnDele;
        internal System.Windows.Forms.Button BtnDOWN;
        internal System.Windows.Forms.Label Label1;
    }
}