namespace Businfo
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
            this.CheckBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.oBJECTIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stationNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stationNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stationAliasDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.directDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mainSymbolDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stationCharacterDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gPSLongtitudeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gPSLatitudeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gPSHighDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rodMaterialFirstDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rodStyleFirstDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stationMaterialDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stationStyleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chairDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stationTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.busShelterDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.constructorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.constructionTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stationLandDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trafficVolumeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureFirstDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureSecondDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureThirdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stationAreaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serviceAreaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dayTrafficVolumeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.passSumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.passRodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hourMassDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hourEvacuateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dayMassDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dayEvacuateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.routeSumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.moveTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rebuildTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.removeTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stationLongDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rodMaterialSecondDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rodMaterialThirdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rodStyleSecondDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rodStyleThirdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dispatchCompanyFirstDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dispatchRouteFirstDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dispatchStationFirstDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dispatchCompanySecondDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dispatchRouteSecondDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dispatchStationSecondDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dispatchCompanyThirdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dispatchRouteThirdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dispatchStationThirdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.classifyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.DataGridView1.AutoGenerateColumns = false;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckBox,
            this.oBJECTIDDataGridViewTextBoxColumn,
            this.stationNoDataGridViewTextBoxColumn,
            this.stationNameDataGridViewTextBoxColumn,
            this.stationAliasDataGridViewTextBoxColumn,
            this.directDataGridViewTextBoxColumn,
            this.mainSymbolDataGridViewTextBoxColumn,
            this.stationCharacterDataGridViewTextBoxColumn,
            this.gPSLongtitudeDataGridViewTextBoxColumn,
            this.gPSLatitudeDataGridViewTextBoxColumn,
            this.gPSHighDataGridViewTextBoxColumn,
            this.rodMaterialFirstDataGridViewTextBoxColumn,
            this.rodStyleFirstDataGridViewTextBoxColumn,
            this.stationMaterialDataGridViewTextBoxColumn,
            this.stationStyleDataGridViewTextBoxColumn,
            this.chairDataGridViewTextBoxColumn,
            this.stationTypeDataGridViewTextBoxColumn,
            this.busShelterDataGridViewTextBoxColumn,
            this.constructorDataGridViewTextBoxColumn,
            this.constructionTimeDataGridViewTextBoxColumn,
            this.stationLandDataGridViewTextBoxColumn,
            this.trafficVolumeDataGridViewTextBoxColumn,
            this.pictureFirstDataGridViewTextBoxColumn,
            this.pictureSecondDataGridViewTextBoxColumn,
            this.pictureThirdDataGridViewTextBoxColumn,
            this.stationAreaDataGridViewTextBoxColumn,
            this.serviceAreaDataGridViewTextBoxColumn,
            this.dayTrafficVolumeDataGridViewTextBoxColumn,
            this.passSumDataGridViewTextBoxColumn,
            this.passRodeDataGridViewTextBoxColumn,
            this.hourMassDataGridViewTextBoxColumn,
            this.hourEvacuateDataGridViewTextBoxColumn,
            this.dayMassDataGridViewTextBoxColumn,
            this.dayEvacuateDataGridViewTextBoxColumn,
            this.routeSumDataGridViewTextBoxColumn,
            this.moveTimeDataGridViewTextBoxColumn,
            this.rebuildTimeDataGridViewTextBoxColumn,
            this.removeTimeDataGridViewTextBoxColumn,
            this.stationLongDataGridViewTextBoxColumn,
            this.rodMaterialSecondDataGridViewTextBoxColumn,
            this.rodMaterialThirdDataGridViewTextBoxColumn,
            this.rodStyleSecondDataGridViewTextBoxColumn,
            this.rodStyleThirdDataGridViewTextBoxColumn,
            this.dispatchCompanyFirstDataGridViewTextBoxColumn,
            this.dispatchRouteFirstDataGridViewTextBoxColumn,
            this.dispatchStationFirstDataGridViewTextBoxColumn,
            this.dispatchCompanySecondDataGridViewTextBoxColumn,
            this.dispatchRouteSecondDataGridViewTextBoxColumn,
            this.dispatchStationSecondDataGridViewTextBoxColumn,
            this.dispatchCompanyThirdDataGridViewTextBoxColumn,
            this.dispatchRouteThirdDataGridViewTextBoxColumn,
            this.dispatchStationThirdDataGridViewTextBoxColumn,
            this.classifyDataGridViewTextBoxColumn});
            this.DataGridView1.DataSource = this.公交站点BindingSource;
            this.DataGridView1.Location = new System.Drawing.Point(5, 48);
            this.DataGridView1.MultiSelect = false;
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.RowHeadersWidth = 20;
            this.DataGridView1.RowTemplate.Height = 23;
            this.DataGridView1.Size = new System.Drawing.Size(244, 430);
            this.DataGridView1.TabIndex = 3;
            this.DataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView1_CellMouseDown);
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
            this.ContextMenuStrip1.Size = new System.Drawing.Size(123, 92);
            // 
            // 定位到ToolStripMenuItem
            // 
            this.定位到ToolStripMenuItem.Name = "定位到ToolStripMenuItem";
            this.定位到ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.定位到ToolStripMenuItem.Text = "定位到";
            this.定位到ToolStripMenuItem.Click += new System.EventHandler(this.定位到ToolStripMenuItem_Click);
            // 
            // 删除站点ToolStripMenuItem
            // 
            this.删除站点ToolStripMenuItem.Name = "删除站点ToolStripMenuItem";
            this.删除站点ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.删除站点ToolStripMenuItem.Text = "删除站点";
            this.删除站点ToolStripMenuItem.Click += new System.EventHandler(this.删除站点ToolStripMenuItem_Click);
            // 
            // 编辑属性ToolStripMenuItem
            // 
            this.编辑属性ToolStripMenuItem.Name = "编辑属性ToolStripMenuItem";
            this.编辑属性ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.编辑属性ToolStripMenuItem.Text = "编辑属性";
            this.编辑属性ToolStripMenuItem.Click += new System.EventHandler(this.编辑属性ToolStripMenuItem_Click);
            // 
            // 全景浏览ToolStripMenuItem
            // 
            this.全景浏览ToolStripMenuItem.Name = "全景浏览ToolStripMenuItem";
            this.全景浏览ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
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
            this.stationNameToolStripLabel.Size = new System.Drawing.Size(72, 22);
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
            this.fillByStationNameToolStripButton.Size = new System.Drawing.Size(96, 17);
            this.fillByStationNameToolStripButton.Text = "FillByStationName";
            this.fillByStationNameToolStripButton.Click += new System.EventHandler(this.fillByStationNameToolStripButton_Click_1);
            // 
            // 公交站点TableAdapter
            // 
            this.公交站点TableAdapter.ClearBeforeFill = true;
            // 
            // CheckBox
            // 
            this.CheckBox.Frozen = true;
            this.CheckBox.HeaderText = "CheckBox";
            this.CheckBox.Name = "CheckBox";
            this.CheckBox.Width = 35;
            // 
            // oBJECTIDDataGridViewTextBoxColumn
            // 
            this.oBJECTIDDataGridViewTextBoxColumn.DataPropertyName = "OBJECTID";
            this.oBJECTIDDataGridViewTextBoxColumn.HeaderText = "OBJECTID";
            this.oBJECTIDDataGridViewTextBoxColumn.Name = "oBJECTIDDataGridViewTextBoxColumn";
            this.oBJECTIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // stationNoDataGridViewTextBoxColumn
            // 
            this.stationNoDataGridViewTextBoxColumn.DataPropertyName = "StationNo";
            this.stationNoDataGridViewTextBoxColumn.HeaderText = "StationNo";
            this.stationNoDataGridViewTextBoxColumn.Name = "stationNoDataGridViewTextBoxColumn";
            this.stationNoDataGridViewTextBoxColumn.Visible = false;
            // 
            // stationNameDataGridViewTextBoxColumn
            // 
            this.stationNameDataGridViewTextBoxColumn.DataPropertyName = "StationName";
            this.stationNameDataGridViewTextBoxColumn.HeaderText = "站点名称";
            this.stationNameDataGridViewTextBoxColumn.Name = "stationNameDataGridViewTextBoxColumn";
            this.stationNameDataGridViewTextBoxColumn.Width = 78;
            // 
            // stationAliasDataGridViewTextBoxColumn
            // 
            this.stationAliasDataGridViewTextBoxColumn.DataPropertyName = "StationAlias";
            this.stationAliasDataGridViewTextBoxColumn.HeaderText = "StationAlias";
            this.stationAliasDataGridViewTextBoxColumn.Name = "stationAliasDataGridViewTextBoxColumn";
            this.stationAliasDataGridViewTextBoxColumn.Visible = false;
            // 
            // directDataGridViewTextBoxColumn
            // 
            this.directDataGridViewTextBoxColumn.DataPropertyName = "Direct";
            this.directDataGridViewTextBoxColumn.HeaderText = "行向";
            this.directDataGridViewTextBoxColumn.Name = "directDataGridViewTextBoxColumn";
            this.directDataGridViewTextBoxColumn.Width = 55;
            // 
            // mainSymbolDataGridViewTextBoxColumn
            // 
            this.mainSymbolDataGridViewTextBoxColumn.DataPropertyName = "MainSymbol";
            this.mainSymbolDataGridViewTextBoxColumn.HeaderText = "MainSymbol";
            this.mainSymbolDataGridViewTextBoxColumn.Name = "mainSymbolDataGridViewTextBoxColumn";
            this.mainSymbolDataGridViewTextBoxColumn.Visible = false;
            // 
            // stationCharacterDataGridViewTextBoxColumn
            // 
            this.stationCharacterDataGridViewTextBoxColumn.DataPropertyName = "StationCharacter";
            this.stationCharacterDataGridViewTextBoxColumn.HeaderText = "StationCharacter";
            this.stationCharacterDataGridViewTextBoxColumn.Name = "stationCharacterDataGridViewTextBoxColumn";
            this.stationCharacterDataGridViewTextBoxColumn.Visible = false;
            // 
            // gPSLongtitudeDataGridViewTextBoxColumn
            // 
            this.gPSLongtitudeDataGridViewTextBoxColumn.DataPropertyName = "GPSLongtitude";
            this.gPSLongtitudeDataGridViewTextBoxColumn.HeaderText = "GPSLongtitude";
            this.gPSLongtitudeDataGridViewTextBoxColumn.Name = "gPSLongtitudeDataGridViewTextBoxColumn";
            this.gPSLongtitudeDataGridViewTextBoxColumn.Visible = false;
            // 
            // gPSLatitudeDataGridViewTextBoxColumn
            // 
            this.gPSLatitudeDataGridViewTextBoxColumn.DataPropertyName = "GPSLatitude";
            this.gPSLatitudeDataGridViewTextBoxColumn.HeaderText = "GPSLatitude";
            this.gPSLatitudeDataGridViewTextBoxColumn.Name = "gPSLatitudeDataGridViewTextBoxColumn";
            this.gPSLatitudeDataGridViewTextBoxColumn.Visible = false;
            // 
            // gPSHighDataGridViewTextBoxColumn
            // 
            this.gPSHighDataGridViewTextBoxColumn.DataPropertyName = "GPSHigh";
            this.gPSHighDataGridViewTextBoxColumn.HeaderText = "GPSHigh";
            this.gPSHighDataGridViewTextBoxColumn.Name = "gPSHighDataGridViewTextBoxColumn";
            this.gPSHighDataGridViewTextBoxColumn.Visible = false;
            // 
            // rodMaterialFirstDataGridViewTextBoxColumn
            // 
            this.rodMaterialFirstDataGridViewTextBoxColumn.DataPropertyName = "RodMaterialFirst";
            this.rodMaterialFirstDataGridViewTextBoxColumn.HeaderText = "RodMaterialFirst";
            this.rodMaterialFirstDataGridViewTextBoxColumn.Name = "rodMaterialFirstDataGridViewTextBoxColumn";
            this.rodMaterialFirstDataGridViewTextBoxColumn.Visible = false;
            // 
            // rodStyleFirstDataGridViewTextBoxColumn
            // 
            this.rodStyleFirstDataGridViewTextBoxColumn.DataPropertyName = "RodStyleFirst";
            this.rodStyleFirstDataGridViewTextBoxColumn.HeaderText = "RodStyleFirst";
            this.rodStyleFirstDataGridViewTextBoxColumn.Name = "rodStyleFirstDataGridViewTextBoxColumn";
            this.rodStyleFirstDataGridViewTextBoxColumn.Visible = false;
            // 
            // stationMaterialDataGridViewTextBoxColumn
            // 
            this.stationMaterialDataGridViewTextBoxColumn.DataPropertyName = "StationMaterial";
            this.stationMaterialDataGridViewTextBoxColumn.HeaderText = "StationMaterial";
            this.stationMaterialDataGridViewTextBoxColumn.Name = "stationMaterialDataGridViewTextBoxColumn";
            this.stationMaterialDataGridViewTextBoxColumn.Visible = false;
            // 
            // stationStyleDataGridViewTextBoxColumn
            // 
            this.stationStyleDataGridViewTextBoxColumn.DataPropertyName = "StationStyle";
            this.stationStyleDataGridViewTextBoxColumn.HeaderText = "StationStyle";
            this.stationStyleDataGridViewTextBoxColumn.Name = "stationStyleDataGridViewTextBoxColumn";
            this.stationStyleDataGridViewTextBoxColumn.Visible = false;
            // 
            // chairDataGridViewTextBoxColumn
            // 
            this.chairDataGridViewTextBoxColumn.DataPropertyName = "Chair";
            this.chairDataGridViewTextBoxColumn.HeaderText = "Chair";
            this.chairDataGridViewTextBoxColumn.Name = "chairDataGridViewTextBoxColumn";
            this.chairDataGridViewTextBoxColumn.Visible = false;
            // 
            // stationTypeDataGridViewTextBoxColumn
            // 
            this.stationTypeDataGridViewTextBoxColumn.DataPropertyName = "StationType";
            this.stationTypeDataGridViewTextBoxColumn.HeaderText = "StationType";
            this.stationTypeDataGridViewTextBoxColumn.Name = "stationTypeDataGridViewTextBoxColumn";
            this.stationTypeDataGridViewTextBoxColumn.Visible = false;
            // 
            // busShelterDataGridViewTextBoxColumn
            // 
            this.busShelterDataGridViewTextBoxColumn.DataPropertyName = "BusShelter";
            this.busShelterDataGridViewTextBoxColumn.HeaderText = "BusShelter";
            this.busShelterDataGridViewTextBoxColumn.Name = "busShelterDataGridViewTextBoxColumn";
            this.busShelterDataGridViewTextBoxColumn.Visible = false;
            // 
            // constructorDataGridViewTextBoxColumn
            // 
            this.constructorDataGridViewTextBoxColumn.DataPropertyName = "Constructor";
            this.constructorDataGridViewTextBoxColumn.HeaderText = "Constructor";
            this.constructorDataGridViewTextBoxColumn.Name = "constructorDataGridViewTextBoxColumn";
            this.constructorDataGridViewTextBoxColumn.Visible = false;
            // 
            // constructionTimeDataGridViewTextBoxColumn
            // 
            this.constructionTimeDataGridViewTextBoxColumn.DataPropertyName = "ConstructionTime";
            this.constructionTimeDataGridViewTextBoxColumn.HeaderText = "ConstructionTime";
            this.constructionTimeDataGridViewTextBoxColumn.Name = "constructionTimeDataGridViewTextBoxColumn";
            this.constructionTimeDataGridViewTextBoxColumn.Visible = false;
            // 
            // stationLandDataGridViewTextBoxColumn
            // 
            this.stationLandDataGridViewTextBoxColumn.DataPropertyName = "StationLand";
            this.stationLandDataGridViewTextBoxColumn.HeaderText = "StationLand";
            this.stationLandDataGridViewTextBoxColumn.Name = "stationLandDataGridViewTextBoxColumn";
            this.stationLandDataGridViewTextBoxColumn.Visible = false;
            // 
            // trafficVolumeDataGridViewTextBoxColumn
            // 
            this.trafficVolumeDataGridViewTextBoxColumn.DataPropertyName = "TrafficVolume";
            this.trafficVolumeDataGridViewTextBoxColumn.HeaderText = "TrafficVolume";
            this.trafficVolumeDataGridViewTextBoxColumn.Name = "trafficVolumeDataGridViewTextBoxColumn";
            this.trafficVolumeDataGridViewTextBoxColumn.Visible = false;
            // 
            // pictureFirstDataGridViewTextBoxColumn
            // 
            this.pictureFirstDataGridViewTextBoxColumn.DataPropertyName = "PictureFirst";
            this.pictureFirstDataGridViewTextBoxColumn.HeaderText = "PictureFirst";
            this.pictureFirstDataGridViewTextBoxColumn.Name = "pictureFirstDataGridViewTextBoxColumn";
            this.pictureFirstDataGridViewTextBoxColumn.Visible = false;
            // 
            // pictureSecondDataGridViewTextBoxColumn
            // 
            this.pictureSecondDataGridViewTextBoxColumn.DataPropertyName = "PictureSecond";
            this.pictureSecondDataGridViewTextBoxColumn.HeaderText = "PictureSecond";
            this.pictureSecondDataGridViewTextBoxColumn.Name = "pictureSecondDataGridViewTextBoxColumn";
            this.pictureSecondDataGridViewTextBoxColumn.Visible = false;
            // 
            // pictureThirdDataGridViewTextBoxColumn
            // 
            this.pictureThirdDataGridViewTextBoxColumn.DataPropertyName = "PictureThird";
            this.pictureThirdDataGridViewTextBoxColumn.HeaderText = "PictureThird";
            this.pictureThirdDataGridViewTextBoxColumn.Name = "pictureThirdDataGridViewTextBoxColumn";
            this.pictureThirdDataGridViewTextBoxColumn.Visible = false;
            // 
            // stationAreaDataGridViewTextBoxColumn
            // 
            this.stationAreaDataGridViewTextBoxColumn.DataPropertyName = "StationArea";
            this.stationAreaDataGridViewTextBoxColumn.HeaderText = "StationArea";
            this.stationAreaDataGridViewTextBoxColumn.Name = "stationAreaDataGridViewTextBoxColumn";
            this.stationAreaDataGridViewTextBoxColumn.Visible = false;
            // 
            // serviceAreaDataGridViewTextBoxColumn
            // 
            this.serviceAreaDataGridViewTextBoxColumn.DataPropertyName = "ServiceArea";
            this.serviceAreaDataGridViewTextBoxColumn.HeaderText = "ServiceArea";
            this.serviceAreaDataGridViewTextBoxColumn.Name = "serviceAreaDataGridViewTextBoxColumn";
            this.serviceAreaDataGridViewTextBoxColumn.Visible = false;
            // 
            // dayTrafficVolumeDataGridViewTextBoxColumn
            // 
            this.dayTrafficVolumeDataGridViewTextBoxColumn.DataPropertyName = "DayTrafficVolume";
            this.dayTrafficVolumeDataGridViewTextBoxColumn.HeaderText = "DayTrafficVolume";
            this.dayTrafficVolumeDataGridViewTextBoxColumn.Name = "dayTrafficVolumeDataGridViewTextBoxColumn";
            this.dayTrafficVolumeDataGridViewTextBoxColumn.Visible = false;
            // 
            // passSumDataGridViewTextBoxColumn
            // 
            this.passSumDataGridViewTextBoxColumn.DataPropertyName = "PassSum";
            this.passSumDataGridViewTextBoxColumn.HeaderText = "PassSum";
            this.passSumDataGridViewTextBoxColumn.Name = "passSumDataGridViewTextBoxColumn";
            this.passSumDataGridViewTextBoxColumn.Visible = false;
            // 
            // passRodeDataGridViewTextBoxColumn
            // 
            this.passRodeDataGridViewTextBoxColumn.DataPropertyName = "PassRode";
            this.passRodeDataGridViewTextBoxColumn.HeaderText = "PassRode";
            this.passRodeDataGridViewTextBoxColumn.Name = "passRodeDataGridViewTextBoxColumn";
            this.passRodeDataGridViewTextBoxColumn.Visible = false;
            // 
            // hourMassDataGridViewTextBoxColumn
            // 
            this.hourMassDataGridViewTextBoxColumn.DataPropertyName = "HourMass";
            this.hourMassDataGridViewTextBoxColumn.HeaderText = "HourMass";
            this.hourMassDataGridViewTextBoxColumn.Name = "hourMassDataGridViewTextBoxColumn";
            this.hourMassDataGridViewTextBoxColumn.Visible = false;
            // 
            // hourEvacuateDataGridViewTextBoxColumn
            // 
            this.hourEvacuateDataGridViewTextBoxColumn.DataPropertyName = "HourEvacuate";
            this.hourEvacuateDataGridViewTextBoxColumn.HeaderText = "HourEvacuate";
            this.hourEvacuateDataGridViewTextBoxColumn.Name = "hourEvacuateDataGridViewTextBoxColumn";
            this.hourEvacuateDataGridViewTextBoxColumn.Visible = false;
            // 
            // dayMassDataGridViewTextBoxColumn
            // 
            this.dayMassDataGridViewTextBoxColumn.DataPropertyName = "DayMass";
            this.dayMassDataGridViewTextBoxColumn.HeaderText = "DayMass";
            this.dayMassDataGridViewTextBoxColumn.Name = "dayMassDataGridViewTextBoxColumn";
            this.dayMassDataGridViewTextBoxColumn.Visible = false;
            // 
            // dayEvacuateDataGridViewTextBoxColumn
            // 
            this.dayEvacuateDataGridViewTextBoxColumn.DataPropertyName = "DayEvacuate";
            this.dayEvacuateDataGridViewTextBoxColumn.HeaderText = "DayEvacuate";
            this.dayEvacuateDataGridViewTextBoxColumn.Name = "dayEvacuateDataGridViewTextBoxColumn";
            this.dayEvacuateDataGridViewTextBoxColumn.Visible = false;
            // 
            // routeSumDataGridViewTextBoxColumn
            // 
            this.routeSumDataGridViewTextBoxColumn.DataPropertyName = "RouteSum";
            this.routeSumDataGridViewTextBoxColumn.HeaderText = "RouteSum";
            this.routeSumDataGridViewTextBoxColumn.Name = "routeSumDataGridViewTextBoxColumn";
            this.routeSumDataGridViewTextBoxColumn.Visible = false;
            // 
            // moveTimeDataGridViewTextBoxColumn
            // 
            this.moveTimeDataGridViewTextBoxColumn.DataPropertyName = "MoveTime";
            this.moveTimeDataGridViewTextBoxColumn.HeaderText = "MoveTime";
            this.moveTimeDataGridViewTextBoxColumn.Name = "moveTimeDataGridViewTextBoxColumn";
            this.moveTimeDataGridViewTextBoxColumn.Visible = false;
            // 
            // rebuildTimeDataGridViewTextBoxColumn
            // 
            this.rebuildTimeDataGridViewTextBoxColumn.DataPropertyName = "RebuildTime";
            this.rebuildTimeDataGridViewTextBoxColumn.HeaderText = "RebuildTime";
            this.rebuildTimeDataGridViewTextBoxColumn.Name = "rebuildTimeDataGridViewTextBoxColumn";
            this.rebuildTimeDataGridViewTextBoxColumn.Visible = false;
            // 
            // removeTimeDataGridViewTextBoxColumn
            // 
            this.removeTimeDataGridViewTextBoxColumn.DataPropertyName = "RemoveTime";
            this.removeTimeDataGridViewTextBoxColumn.HeaderText = "RemoveTime";
            this.removeTimeDataGridViewTextBoxColumn.Name = "removeTimeDataGridViewTextBoxColumn";
            this.removeTimeDataGridViewTextBoxColumn.Visible = false;
            // 
            // stationLongDataGridViewTextBoxColumn
            // 
            this.stationLongDataGridViewTextBoxColumn.DataPropertyName = "StationLong";
            this.stationLongDataGridViewTextBoxColumn.HeaderText = "StationLong";
            this.stationLongDataGridViewTextBoxColumn.Name = "stationLongDataGridViewTextBoxColumn";
            this.stationLongDataGridViewTextBoxColumn.Visible = false;
            // 
            // rodMaterialSecondDataGridViewTextBoxColumn
            // 
            this.rodMaterialSecondDataGridViewTextBoxColumn.DataPropertyName = "RodMaterialSecond";
            this.rodMaterialSecondDataGridViewTextBoxColumn.HeaderText = "RodMaterialSecond";
            this.rodMaterialSecondDataGridViewTextBoxColumn.Name = "rodMaterialSecondDataGridViewTextBoxColumn";
            this.rodMaterialSecondDataGridViewTextBoxColumn.Visible = false;
            // 
            // rodMaterialThirdDataGridViewTextBoxColumn
            // 
            this.rodMaterialThirdDataGridViewTextBoxColumn.DataPropertyName = "RodMaterialThird";
            this.rodMaterialThirdDataGridViewTextBoxColumn.HeaderText = "RodMaterialThird";
            this.rodMaterialThirdDataGridViewTextBoxColumn.Name = "rodMaterialThirdDataGridViewTextBoxColumn";
            this.rodMaterialThirdDataGridViewTextBoxColumn.Visible = false;
            // 
            // rodStyleSecondDataGridViewTextBoxColumn
            // 
            this.rodStyleSecondDataGridViewTextBoxColumn.DataPropertyName = "RodStyleSecond";
            this.rodStyleSecondDataGridViewTextBoxColumn.HeaderText = "RodStyleSecond";
            this.rodStyleSecondDataGridViewTextBoxColumn.Name = "rodStyleSecondDataGridViewTextBoxColumn";
            this.rodStyleSecondDataGridViewTextBoxColumn.Visible = false;
            // 
            // rodStyleThirdDataGridViewTextBoxColumn
            // 
            this.rodStyleThirdDataGridViewTextBoxColumn.DataPropertyName = "RodStyleThird";
            this.rodStyleThirdDataGridViewTextBoxColumn.HeaderText = "RodStyleThird";
            this.rodStyleThirdDataGridViewTextBoxColumn.Name = "rodStyleThirdDataGridViewTextBoxColumn";
            this.rodStyleThirdDataGridViewTextBoxColumn.Visible = false;
            // 
            // dispatchCompanyFirstDataGridViewTextBoxColumn
            // 
            this.dispatchCompanyFirstDataGridViewTextBoxColumn.DataPropertyName = "DispatchCompanyFirst";
            this.dispatchCompanyFirstDataGridViewTextBoxColumn.HeaderText = "DispatchCompanyFirst";
            this.dispatchCompanyFirstDataGridViewTextBoxColumn.Name = "dispatchCompanyFirstDataGridViewTextBoxColumn";
            this.dispatchCompanyFirstDataGridViewTextBoxColumn.Visible = false;
            // 
            // dispatchRouteFirstDataGridViewTextBoxColumn
            // 
            this.dispatchRouteFirstDataGridViewTextBoxColumn.DataPropertyName = "DispatchRouteFirst";
            this.dispatchRouteFirstDataGridViewTextBoxColumn.HeaderText = "DispatchRouteFirst";
            this.dispatchRouteFirstDataGridViewTextBoxColumn.Name = "dispatchRouteFirstDataGridViewTextBoxColumn";
            this.dispatchRouteFirstDataGridViewTextBoxColumn.Visible = false;
            // 
            // dispatchStationFirstDataGridViewTextBoxColumn
            // 
            this.dispatchStationFirstDataGridViewTextBoxColumn.DataPropertyName = "DispatchStationFirst";
            this.dispatchStationFirstDataGridViewTextBoxColumn.HeaderText = "DispatchStationFirst";
            this.dispatchStationFirstDataGridViewTextBoxColumn.Name = "dispatchStationFirstDataGridViewTextBoxColumn";
            this.dispatchStationFirstDataGridViewTextBoxColumn.Visible = false;
            // 
            // dispatchCompanySecondDataGridViewTextBoxColumn
            // 
            this.dispatchCompanySecondDataGridViewTextBoxColumn.DataPropertyName = "DispatchCompanySecond";
            this.dispatchCompanySecondDataGridViewTextBoxColumn.HeaderText = "DispatchCompanySecond";
            this.dispatchCompanySecondDataGridViewTextBoxColumn.Name = "dispatchCompanySecondDataGridViewTextBoxColumn";
            this.dispatchCompanySecondDataGridViewTextBoxColumn.Visible = false;
            // 
            // dispatchRouteSecondDataGridViewTextBoxColumn
            // 
            this.dispatchRouteSecondDataGridViewTextBoxColumn.DataPropertyName = "DispatchRouteSecond";
            this.dispatchRouteSecondDataGridViewTextBoxColumn.HeaderText = "DispatchRouteSecond";
            this.dispatchRouteSecondDataGridViewTextBoxColumn.Name = "dispatchRouteSecondDataGridViewTextBoxColumn";
            this.dispatchRouteSecondDataGridViewTextBoxColumn.Visible = false;
            // 
            // dispatchStationSecondDataGridViewTextBoxColumn
            // 
            this.dispatchStationSecondDataGridViewTextBoxColumn.DataPropertyName = "DispatchStationSecond";
            this.dispatchStationSecondDataGridViewTextBoxColumn.HeaderText = "DispatchStationSecond";
            this.dispatchStationSecondDataGridViewTextBoxColumn.Name = "dispatchStationSecondDataGridViewTextBoxColumn";
            this.dispatchStationSecondDataGridViewTextBoxColumn.Visible = false;
            // 
            // dispatchCompanyThirdDataGridViewTextBoxColumn
            // 
            this.dispatchCompanyThirdDataGridViewTextBoxColumn.DataPropertyName = "DispatchCompanyThird";
            this.dispatchCompanyThirdDataGridViewTextBoxColumn.HeaderText = "DispatchCompanyThird";
            this.dispatchCompanyThirdDataGridViewTextBoxColumn.Name = "dispatchCompanyThirdDataGridViewTextBoxColumn";
            this.dispatchCompanyThirdDataGridViewTextBoxColumn.Visible = false;
            // 
            // dispatchRouteThirdDataGridViewTextBoxColumn
            // 
            this.dispatchRouteThirdDataGridViewTextBoxColumn.DataPropertyName = "DispatchRouteThird";
            this.dispatchRouteThirdDataGridViewTextBoxColumn.HeaderText = "DispatchRouteThird";
            this.dispatchRouteThirdDataGridViewTextBoxColumn.Name = "dispatchRouteThirdDataGridViewTextBoxColumn";
            this.dispatchRouteThirdDataGridViewTextBoxColumn.Visible = false;
            // 
            // dispatchStationThirdDataGridViewTextBoxColumn
            // 
            this.dispatchStationThirdDataGridViewTextBoxColumn.DataPropertyName = "DispatchStationThird";
            this.dispatchStationThirdDataGridViewTextBoxColumn.HeaderText = "DispatchStationThird";
            this.dispatchStationThirdDataGridViewTextBoxColumn.Name = "dispatchStationThirdDataGridViewTextBoxColumn";
            this.dispatchStationThirdDataGridViewTextBoxColumn.Visible = false;
            // 
            // classifyDataGridViewTextBoxColumn
            // 
            this.classifyDataGridViewTextBoxColumn.DataPropertyName = "Classify";
            this.classifyDataGridViewTextBoxColumn.HeaderText = "Classify";
            this.classifyDataGridViewTextBoxColumn.Name = "classifyDataGridViewTextBoxColumn";
            this.classifyDataGridViewTextBoxColumn.Visible = false;
            // 
            // frmStationPane
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
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
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn oBJECTIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationAliasDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn directDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mainSymbolDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationCharacterDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gPSLongtitudeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gPSLatitudeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gPSHighDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rodMaterialFirstDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rodStyleFirstDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationMaterialDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationStyleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn chairDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn busShelterDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn constructorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn constructionTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationLandDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trafficVolumeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pictureFirstDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pictureSecondDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pictureThirdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationAreaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serviceAreaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dayTrafficVolumeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn passSumDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn passRodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hourMassDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hourEvacuateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dayMassDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dayEvacuateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn routeSumDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn moveTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rebuildTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn removeTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationLongDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rodMaterialSecondDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rodMaterialThirdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rodStyleSecondDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rodStyleThirdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dispatchCompanyFirstDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dispatchRouteFirstDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dispatchStationFirstDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dispatchCompanySecondDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dispatchRouteSecondDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dispatchStationSecondDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dispatchCompanyThirdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dispatchRouteThirdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dispatchStationThirdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn classifyDataGridViewTextBoxColumn;
    }
}
