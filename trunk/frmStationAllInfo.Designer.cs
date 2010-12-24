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
            this.Button1 = new System.Windows.Forms.Button();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
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
            this.Checkbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
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
            this.fillByOBJECTIDToolStrip.SuspendLayout();
            this.fillByINOBJECTIDToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(12, 1);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(75, 23);
            this.Button1.TabIndex = 4;
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
            this.DataGridView1.AutoGenerateColumns = false;
            this.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Checkbox,
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
            this.DataGridView1.GridColor = System.Drawing.SystemColors.Desktop;
            this.DataGridView1.Location = new System.Drawing.Point(12, 27);
            this.DataGridView1.MultiSelect = false;
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.RowTemplate.Height = 23;
            this.DataGridView1.Size = new System.Drawing.Size(462, 277);
            this.DataGridView1.TabIndex = 3;
            this.DataGridView1.Sorted += new System.EventHandler(this.DataGridView1_Sorted);
            this.DataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView1_CellMouseDown);
            this.DataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView1_CellMouseDoubleClick);
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
            this.oBJECTIDToolStripLabel.Size = new System.Drawing.Size(60, 22);
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
            this.fillByOBJECTIDToolStripButton.Size = new System.Drawing.Size(84, 22);
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
            this.fillByINOBJECTIDToolStripButton.Size = new System.Drawing.Size(95, 22);
            this.fillByINOBJECTIDToolStripButton.Text = "FillByINOBJECTID";
            this.fillByINOBJECTIDToolStripButton.Click += new System.EventHandler(this.fillByINOBJECTIDToolStripButton_Click);
            // 
            // 公交站点TableAdapter
            // 
            this.公交站点TableAdapter.ClearBeforeFill = true;
            // 
            // Checkbox
            // 
            this.Checkbox.Frozen = true;
            this.Checkbox.HeaderText = "Checkbox";
            this.Checkbox.Name = "Checkbox";
            this.Checkbox.Width = 59;
            // 
            // oBJECTIDDataGridViewTextBoxColumn
            // 
            this.oBJECTIDDataGridViewTextBoxColumn.DataPropertyName = "OBJECTID";
            this.oBJECTIDDataGridViewTextBoxColumn.HeaderText = "OBJECTID";
            this.oBJECTIDDataGridViewTextBoxColumn.Name = "oBJECTIDDataGridViewTextBoxColumn";
            this.oBJECTIDDataGridViewTextBoxColumn.Visible = false;
            this.oBJECTIDDataGridViewTextBoxColumn.Width = 78;
            // 
            // stationNoDataGridViewTextBoxColumn
            // 
            this.stationNoDataGridViewTextBoxColumn.DataPropertyName = "StationNo";
            this.stationNoDataGridViewTextBoxColumn.HeaderText = "StationNo";
            this.stationNoDataGridViewTextBoxColumn.Name = "stationNoDataGridViewTextBoxColumn";
            this.stationNoDataGridViewTextBoxColumn.Visible = false;
            this.stationNoDataGridViewTextBoxColumn.Width = 84;
            // 
            // stationNameDataGridViewTextBoxColumn
            // 
            this.stationNameDataGridViewTextBoxColumn.DataPropertyName = "StationName";
            this.stationNameDataGridViewTextBoxColumn.Frozen = true;
            this.stationNameDataGridViewTextBoxColumn.HeaderText = "站点名称";
            this.stationNameDataGridViewTextBoxColumn.Name = "stationNameDataGridViewTextBoxColumn";
            this.stationNameDataGridViewTextBoxColumn.Width = 96;
            // 
            // stationAliasDataGridViewTextBoxColumn
            // 
            this.stationAliasDataGridViewTextBoxColumn.DataPropertyName = "StationAlias";
            this.stationAliasDataGridViewTextBoxColumn.HeaderText = "副站名";
            this.stationAliasDataGridViewTextBoxColumn.Name = "stationAliasDataGridViewTextBoxColumn";
            this.stationAliasDataGridViewTextBoxColumn.Width = 102;
            // 
            // directDataGridViewTextBoxColumn
            // 
            this.directDataGridViewTextBoxColumn.DataPropertyName = "Direct";
            this.directDataGridViewTextBoxColumn.HeaderText = "行向";
            this.directDataGridViewTextBoxColumn.Name = "directDataGridViewTextBoxColumn";
            this.directDataGridViewTextBoxColumn.Width = 66;
            // 
            // mainSymbolDataGridViewTextBoxColumn
            // 
            this.mainSymbolDataGridViewTextBoxColumn.DataPropertyName = "MainSymbol";
            this.mainSymbolDataGridViewTextBoxColumn.HeaderText = "主要标识物";
            this.mainSymbolDataGridViewTextBoxColumn.Name = "mainSymbolDataGridViewTextBoxColumn";
            this.mainSymbolDataGridViewTextBoxColumn.Width = 90;
            // 
            // stationCharacterDataGridViewTextBoxColumn
            // 
            this.stationCharacterDataGridViewTextBoxColumn.DataPropertyName = "StationCharacter";
            this.stationCharacterDataGridViewTextBoxColumn.HeaderText = "站点性质";
            this.stationCharacterDataGridViewTextBoxColumn.Name = "stationCharacterDataGridViewTextBoxColumn";
            this.stationCharacterDataGridViewTextBoxColumn.Width = 126;
            // 
            // gPSLongtitudeDataGridViewTextBoxColumn
            // 
            this.gPSLongtitudeDataGridViewTextBoxColumn.DataPropertyName = "GPSLongtitude";
            this.gPSLongtitudeDataGridViewTextBoxColumn.HeaderText = "GPS经度";
            this.gPSLongtitudeDataGridViewTextBoxColumn.Name = "gPSLongtitudeDataGridViewTextBoxColumn";
            this.gPSLongtitudeDataGridViewTextBoxColumn.Width = 108;
            // 
            // gPSLatitudeDataGridViewTextBoxColumn
            // 
            this.gPSLatitudeDataGridViewTextBoxColumn.DataPropertyName = "GPSLatitude";
            this.gPSLatitudeDataGridViewTextBoxColumn.HeaderText = "GPS纬度";
            this.gPSLatitudeDataGridViewTextBoxColumn.Name = "gPSLatitudeDataGridViewTextBoxColumn";
            this.gPSLatitudeDataGridViewTextBoxColumn.Width = 96;
            // 
            // gPSHighDataGridViewTextBoxColumn
            // 
            this.gPSHighDataGridViewTextBoxColumn.DataPropertyName = "GPSHigh";
            this.gPSHighDataGridViewTextBoxColumn.HeaderText = "GPS高度";
            this.gPSHighDataGridViewTextBoxColumn.Name = "gPSHighDataGridViewTextBoxColumn";
            this.gPSHighDataGridViewTextBoxColumn.Width = 72;
            // 
            // rodMaterialFirstDataGridViewTextBoxColumn
            // 
            this.rodMaterialFirstDataGridViewTextBoxColumn.DataPropertyName = "RodMaterialFirst";
            this.rodMaterialFirstDataGridViewTextBoxColumn.HeaderText = "站杆材质";
            this.rodMaterialFirstDataGridViewTextBoxColumn.Name = "rodMaterialFirstDataGridViewTextBoxColumn";
            this.rodMaterialFirstDataGridViewTextBoxColumn.Width = 126;
            // 
            // rodStyleFirstDataGridViewTextBoxColumn
            // 
            this.rodStyleFirstDataGridViewTextBoxColumn.DataPropertyName = "RodStyleFirst";
            this.rodStyleFirstDataGridViewTextBoxColumn.HeaderText = "站杆式样";
            this.rodStyleFirstDataGridViewTextBoxColumn.Name = "rodStyleFirstDataGridViewTextBoxColumn";
            this.rodStyleFirstDataGridViewTextBoxColumn.Width = 108;
            // 
            // stationMaterialDataGridViewTextBoxColumn
            // 
            this.stationMaterialDataGridViewTextBoxColumn.DataPropertyName = "StationMaterial";
            this.stationMaterialDataGridViewTextBoxColumn.HeaderText = "站牌材质";
            this.stationMaterialDataGridViewTextBoxColumn.Name = "stationMaterialDataGridViewTextBoxColumn";
            this.stationMaterialDataGridViewTextBoxColumn.Width = 120;
            // 
            // stationStyleDataGridViewTextBoxColumn
            // 
            this.stationStyleDataGridViewTextBoxColumn.DataPropertyName = "StationStyle";
            this.stationStyleDataGridViewTextBoxColumn.HeaderText = "站牌规格";
            this.stationStyleDataGridViewTextBoxColumn.Name = "stationStyleDataGridViewTextBoxColumn";
            this.stationStyleDataGridViewTextBoxColumn.Width = 102;
            // 
            // chairDataGridViewTextBoxColumn
            // 
            this.chairDataGridViewTextBoxColumn.DataPropertyName = "Chair";
            this.chairDataGridViewTextBoxColumn.HeaderText = "有无板凳";
            this.chairDataGridViewTextBoxColumn.Name = "chairDataGridViewTextBoxColumn";
            this.chairDataGridViewTextBoxColumn.Width = 60;
            // 
            // stationTypeDataGridViewTextBoxColumn
            // 
            this.stationTypeDataGridViewTextBoxColumn.DataPropertyName = "StationType";
            this.stationTypeDataGridViewTextBoxColumn.HeaderText = "站点类型";
            this.stationTypeDataGridViewTextBoxColumn.Name = "stationTypeDataGridViewTextBoxColumn";
            this.stationTypeDataGridViewTextBoxColumn.Width = 96;
            // 
            // busShelterDataGridViewTextBoxColumn
            // 
            this.busShelterDataGridViewTextBoxColumn.DataPropertyName = "BusShelter";
            this.busShelterDataGridViewTextBoxColumn.HeaderText = "候车亭样式";
            this.busShelterDataGridViewTextBoxColumn.Name = "busShelterDataGridViewTextBoxColumn";
            this.busShelterDataGridViewTextBoxColumn.Width = 90;
            // 
            // constructorDataGridViewTextBoxColumn
            // 
            this.constructorDataGridViewTextBoxColumn.DataPropertyName = "Constructor";
            this.constructorDataGridViewTextBoxColumn.HeaderText = "建设商";
            this.constructorDataGridViewTextBoxColumn.Name = "constructorDataGridViewTextBoxColumn";
            this.constructorDataGridViewTextBoxColumn.Width = 96;
            // 
            // constructionTimeDataGridViewTextBoxColumn
            // 
            this.constructionTimeDataGridViewTextBoxColumn.DataPropertyName = "ConstructionTime";
            this.constructionTimeDataGridViewTextBoxColumn.HeaderText = "建设时间";
            this.constructionTimeDataGridViewTextBoxColumn.Name = "constructionTimeDataGridViewTextBoxColumn";
            this.constructionTimeDataGridViewTextBoxColumn.Width = 126;
            // 
            // stationLandDataGridViewTextBoxColumn
            // 
            this.stationLandDataGridViewTextBoxColumn.DataPropertyName = "StationLand";
            this.stationLandDataGridViewTextBoxColumn.HeaderText = "站点用地";
            this.stationLandDataGridViewTextBoxColumn.Name = "stationLandDataGridViewTextBoxColumn";
            this.stationLandDataGridViewTextBoxColumn.Width = 96;
            // 
            // trafficVolumeDataGridViewTextBoxColumn
            // 
            this.trafficVolumeDataGridViewTextBoxColumn.DataPropertyName = "TrafficVolume";
            this.trafficVolumeDataGridViewTextBoxColumn.HeaderText = "集散量高峰";
            this.trafficVolumeDataGridViewTextBoxColumn.Name = "trafficVolumeDataGridViewTextBoxColumn";
            this.trafficVolumeDataGridViewTextBoxColumn.Width = 108;
            // 
            // pictureFirstDataGridViewTextBoxColumn
            // 
            this.pictureFirstDataGridViewTextBoxColumn.DataPropertyName = "PictureFirst";
            this.pictureFirstDataGridViewTextBoxColumn.HeaderText = "图片一";
            this.pictureFirstDataGridViewTextBoxColumn.Name = "pictureFirstDataGridViewTextBoxColumn";
            this.pictureFirstDataGridViewTextBoxColumn.Width = 102;
            // 
            // pictureSecondDataGridViewTextBoxColumn
            // 
            this.pictureSecondDataGridViewTextBoxColumn.DataPropertyName = "PictureSecond";
            this.pictureSecondDataGridViewTextBoxColumn.HeaderText = "图片二";
            this.pictureSecondDataGridViewTextBoxColumn.Name = "pictureSecondDataGridViewTextBoxColumn";
            this.pictureSecondDataGridViewTextBoxColumn.Width = 108;
            // 
            // pictureThirdDataGridViewTextBoxColumn
            // 
            this.pictureThirdDataGridViewTextBoxColumn.DataPropertyName = "PictureThird";
            this.pictureThirdDataGridViewTextBoxColumn.HeaderText = "图片三";
            this.pictureThirdDataGridViewTextBoxColumn.Name = "pictureThirdDataGridViewTextBoxColumn";
            this.pictureThirdDataGridViewTextBoxColumn.Width = 102;
            // 
            // stationAreaDataGridViewTextBoxColumn
            // 
            this.stationAreaDataGridViewTextBoxColumn.DataPropertyName = "StationArea";
            this.stationAreaDataGridViewTextBoxColumn.HeaderText = "站点面积";
            this.stationAreaDataGridViewTextBoxColumn.Name = "stationAreaDataGridViewTextBoxColumn";
            this.stationAreaDataGridViewTextBoxColumn.Width = 96;
            // 
            // serviceAreaDataGridViewTextBoxColumn
            // 
            this.serviceAreaDataGridViewTextBoxColumn.DataPropertyName = "ServiceArea";
            this.serviceAreaDataGridViewTextBoxColumn.HeaderText = "站服务面积";
            this.serviceAreaDataGridViewTextBoxColumn.Name = "serviceAreaDataGridViewTextBoxColumn";
            this.serviceAreaDataGridViewTextBoxColumn.Width = 96;
            // 
            // dayTrafficVolumeDataGridViewTextBoxColumn
            // 
            this.dayTrafficVolumeDataGridViewTextBoxColumn.DataPropertyName = "DayTrafficVolume";
            this.dayTrafficVolumeDataGridViewTextBoxColumn.HeaderText = "天集散量高峰";
            this.dayTrafficVolumeDataGridViewTextBoxColumn.Name = "dayTrafficVolumeDataGridViewTextBoxColumn";
            this.dayTrafficVolumeDataGridViewTextBoxColumn.Width = 126;
            // 
            // passSumDataGridViewTextBoxColumn
            // 
            this.passSumDataGridViewTextBoxColumn.DataPropertyName = "PassSum";
            this.passSumDataGridViewTextBoxColumn.HeaderText = "通过线路数";
            this.passSumDataGridViewTextBoxColumn.Name = "passSumDataGridViewTextBoxColumn";
            this.passSumDataGridViewTextBoxColumn.Width = 72;
            // 
            // passRodeDataGridViewTextBoxColumn
            // 
            this.passRodeDataGridViewTextBoxColumn.DataPropertyName = "PassRode";
            this.passRodeDataGridViewTextBoxColumn.HeaderText = "通道条数";
            this.passRodeDataGridViewTextBoxColumn.Name = "passRodeDataGridViewTextBoxColumn";
            this.passRodeDataGridViewTextBoxColumn.Width = 78;
            // 
            // hourMassDataGridViewTextBoxColumn
            // 
            this.hourMassDataGridViewTextBoxColumn.DataPropertyName = "HourMass";
            this.hourMassDataGridViewTextBoxColumn.HeaderText = "小时集结量";
            this.hourMassDataGridViewTextBoxColumn.Name = "hourMassDataGridViewTextBoxColumn";
            this.hourMassDataGridViewTextBoxColumn.Width = 78;
            // 
            // hourEvacuateDataGridViewTextBoxColumn
            // 
            this.hourEvacuateDataGridViewTextBoxColumn.DataPropertyName = "HourEvacuate";
            this.hourEvacuateDataGridViewTextBoxColumn.HeaderText = "小时疏散量";
            this.hourEvacuateDataGridViewTextBoxColumn.Name = "hourEvacuateDataGridViewTextBoxColumn";
            this.hourEvacuateDataGridViewTextBoxColumn.Width = 102;
            // 
            // dayMassDataGridViewTextBoxColumn
            // 
            this.dayMassDataGridViewTextBoxColumn.DataPropertyName = "DayMass";
            this.dayMassDataGridViewTextBoxColumn.HeaderText = "全天集结量";
            this.dayMassDataGridViewTextBoxColumn.Name = "dayMassDataGridViewTextBoxColumn";
            this.dayMassDataGridViewTextBoxColumn.Width = 72;
            // 
            // dayEvacuateDataGridViewTextBoxColumn
            // 
            this.dayEvacuateDataGridViewTextBoxColumn.DataPropertyName = "DayEvacuate";
            this.dayEvacuateDataGridViewTextBoxColumn.HeaderText = "全天疏散量";
            this.dayEvacuateDataGridViewTextBoxColumn.Name = "dayEvacuateDataGridViewTextBoxColumn";
            this.dayEvacuateDataGridViewTextBoxColumn.Width = 96;
            // 
            // routeSumDataGridViewTextBoxColumn
            // 
            this.routeSumDataGridViewTextBoxColumn.DataPropertyName = "RouteSum";
            this.routeSumDataGridViewTextBoxColumn.HeaderText = "线路数";
            this.routeSumDataGridViewTextBoxColumn.Name = "routeSumDataGridViewTextBoxColumn";
            this.routeSumDataGridViewTextBoxColumn.Width = 78;
            // 
            // moveTimeDataGridViewTextBoxColumn
            // 
            this.moveTimeDataGridViewTextBoxColumn.DataPropertyName = "MoveTime";
            this.moveTimeDataGridViewTextBoxColumn.HeaderText = "迁移安装时间";
            this.moveTimeDataGridViewTextBoxColumn.Name = "moveTimeDataGridViewTextBoxColumn";
            this.moveTimeDataGridViewTextBoxColumn.Width = 78;
            // 
            // rebuildTimeDataGridViewTextBoxColumn
            // 
            this.rebuildTimeDataGridViewTextBoxColumn.DataPropertyName = "RebuildTime";
            this.rebuildTimeDataGridViewTextBoxColumn.HeaderText = "改建时间";
            this.rebuildTimeDataGridViewTextBoxColumn.Name = "rebuildTimeDataGridViewTextBoxColumn";
            this.rebuildTimeDataGridViewTextBoxColumn.Width = 96;
            // 
            // removeTimeDataGridViewTextBoxColumn
            // 
            this.removeTimeDataGridViewTextBoxColumn.DataPropertyName = "RemoveTime";
            this.removeTimeDataGridViewTextBoxColumn.HeaderText = "拆除时间";
            this.removeTimeDataGridViewTextBoxColumn.Name = "removeTimeDataGridViewTextBoxColumn";
            this.removeTimeDataGridViewTextBoxColumn.Width = 90;
            // 
            // stationLongDataGridViewTextBoxColumn
            // 
            this.stationLongDataGridViewTextBoxColumn.DataPropertyName = "StationLong";
            this.stationLongDataGridViewTextBoxColumn.HeaderText = "候车亭长度";
            this.stationLongDataGridViewTextBoxColumn.Name = "stationLongDataGridViewTextBoxColumn";
            this.stationLongDataGridViewTextBoxColumn.Width = 96;
            // 
            // rodMaterialSecondDataGridViewTextBoxColumn
            // 
            this.rodMaterialSecondDataGridViewTextBoxColumn.DataPropertyName = "RodMaterialSecond";
            this.rodMaterialSecondDataGridViewTextBoxColumn.HeaderText = "站杆材质2";
            this.rodMaterialSecondDataGridViewTextBoxColumn.Name = "rodMaterialSecondDataGridViewTextBoxColumn";
            this.rodMaterialSecondDataGridViewTextBoxColumn.Width = 132;
            // 
            // rodMaterialThirdDataGridViewTextBoxColumn
            // 
            this.rodMaterialThirdDataGridViewTextBoxColumn.DataPropertyName = "RodMaterialThird";
            this.rodMaterialThirdDataGridViewTextBoxColumn.HeaderText = "站杆材质3";
            this.rodMaterialThirdDataGridViewTextBoxColumn.Name = "rodMaterialThirdDataGridViewTextBoxColumn";
            this.rodMaterialThirdDataGridViewTextBoxColumn.Width = 126;
            // 
            // rodStyleSecondDataGridViewTextBoxColumn
            // 
            this.rodStyleSecondDataGridViewTextBoxColumn.DataPropertyName = "RodStyleSecond";
            this.rodStyleSecondDataGridViewTextBoxColumn.HeaderText = "站杆样式2";
            this.rodStyleSecondDataGridViewTextBoxColumn.Name = "rodStyleSecondDataGridViewTextBoxColumn";
            this.rodStyleSecondDataGridViewTextBoxColumn.Width = 114;
            // 
            // rodStyleThirdDataGridViewTextBoxColumn
            // 
            this.rodStyleThirdDataGridViewTextBoxColumn.DataPropertyName = "RodStyleThird";
            this.rodStyleThirdDataGridViewTextBoxColumn.HeaderText = "站杆样式3";
            this.rodStyleThirdDataGridViewTextBoxColumn.Name = "rodStyleThirdDataGridViewTextBoxColumn";
            this.rodStyleThirdDataGridViewTextBoxColumn.Width = 108;
            // 
            // dispatchCompanyFirstDataGridViewTextBoxColumn
            // 
            this.dispatchCompanyFirstDataGridViewTextBoxColumn.DataPropertyName = "DispatchCompanyFirst";
            this.dispatchCompanyFirstDataGridViewTextBoxColumn.HeaderText = "调度公司1";
            this.dispatchCompanyFirstDataGridViewTextBoxColumn.Name = "dispatchCompanyFirstDataGridViewTextBoxColumn";
            this.dispatchCompanyFirstDataGridViewTextBoxColumn.Width = 150;
            // 
            // dispatchRouteFirstDataGridViewTextBoxColumn
            // 
            this.dispatchRouteFirstDataGridViewTextBoxColumn.DataPropertyName = "DispatchRouteFirst";
            this.dispatchRouteFirstDataGridViewTextBoxColumn.HeaderText = "调度线路1";
            this.dispatchRouteFirstDataGridViewTextBoxColumn.Name = "dispatchRouteFirstDataGridViewTextBoxColumn";
            this.dispatchRouteFirstDataGridViewTextBoxColumn.Width = 138;
            // 
            // dispatchStationFirstDataGridViewTextBoxColumn
            // 
            this.dispatchStationFirstDataGridViewTextBoxColumn.DataPropertyName = "DispatchStationFirst";
            this.dispatchStationFirstDataGridViewTextBoxColumn.HeaderText = "调度站道1";
            this.dispatchStationFirstDataGridViewTextBoxColumn.Name = "dispatchStationFirstDataGridViewTextBoxColumn";
            this.dispatchStationFirstDataGridViewTextBoxColumn.Width = 150;
            // 
            // dispatchCompanySecondDataGridViewTextBoxColumn
            // 
            this.dispatchCompanySecondDataGridViewTextBoxColumn.DataPropertyName = "DispatchCompanySecond";
            this.dispatchCompanySecondDataGridViewTextBoxColumn.HeaderText = "调度公司2";
            this.dispatchCompanySecondDataGridViewTextBoxColumn.Name = "dispatchCompanySecondDataGridViewTextBoxColumn";
            this.dispatchCompanySecondDataGridViewTextBoxColumn.Width = 156;
            // 
            // dispatchRouteSecondDataGridViewTextBoxColumn
            // 
            this.dispatchRouteSecondDataGridViewTextBoxColumn.DataPropertyName = "DispatchRouteSecond";
            this.dispatchRouteSecondDataGridViewTextBoxColumn.HeaderText = "调度线路2";
            this.dispatchRouteSecondDataGridViewTextBoxColumn.Name = "dispatchRouteSecondDataGridViewTextBoxColumn";
            this.dispatchRouteSecondDataGridViewTextBoxColumn.Width = 144;
            // 
            // dispatchStationSecondDataGridViewTextBoxColumn
            // 
            this.dispatchStationSecondDataGridViewTextBoxColumn.DataPropertyName = "DispatchStationSecond";
            this.dispatchStationSecondDataGridViewTextBoxColumn.HeaderText = "调度站道2";
            this.dispatchStationSecondDataGridViewTextBoxColumn.Name = "dispatchStationSecondDataGridViewTextBoxColumn";
            this.dispatchStationSecondDataGridViewTextBoxColumn.Width = 156;
            // 
            // dispatchCompanyThirdDataGridViewTextBoxColumn
            // 
            this.dispatchCompanyThirdDataGridViewTextBoxColumn.DataPropertyName = "DispatchCompanyThird";
            this.dispatchCompanyThirdDataGridViewTextBoxColumn.HeaderText = "调度公司3";
            this.dispatchCompanyThirdDataGridViewTextBoxColumn.Name = "dispatchCompanyThirdDataGridViewTextBoxColumn";
            this.dispatchCompanyThirdDataGridViewTextBoxColumn.Width = 150;
            // 
            // dispatchRouteThirdDataGridViewTextBoxColumn
            // 
            this.dispatchRouteThirdDataGridViewTextBoxColumn.DataPropertyName = "DispatchRouteThird";
            this.dispatchRouteThirdDataGridViewTextBoxColumn.HeaderText = "调度线路3";
            this.dispatchRouteThirdDataGridViewTextBoxColumn.Name = "dispatchRouteThirdDataGridViewTextBoxColumn";
            this.dispatchRouteThirdDataGridViewTextBoxColumn.Width = 138;
            // 
            // dispatchStationThirdDataGridViewTextBoxColumn
            // 
            this.dispatchStationThirdDataGridViewTextBoxColumn.DataPropertyName = "DispatchStationThird";
            this.dispatchStationThirdDataGridViewTextBoxColumn.HeaderText = "调度站道3";
            this.dispatchStationThirdDataGridViewTextBoxColumn.Name = "dispatchStationThirdDataGridViewTextBoxColumn";
            this.dispatchStationThirdDataGridViewTextBoxColumn.Width = 150;
            // 
            // classifyDataGridViewTextBoxColumn
            // 
            this.classifyDataGridViewTextBoxColumn.DataPropertyName = "Classify";
            this.classifyDataGridViewTextBoxColumn.HeaderText = "站点类别";
            this.classifyDataGridViewTextBoxColumn.Name = "classifyDataGridViewTextBoxColumn";
            this.classifyDataGridViewTextBoxColumn.Width = 78;
            // 
            // frmStationAllInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 324);
            this.Controls.Add(this.fillByOBJECTIDToolStrip);
            this.Controls.Add(this.fillByINOBJECTIDToolStrip);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.DataGridView1);
            this.Name = "frmStationAllInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "站点信息";
            this.Load += new System.EventHandler(this.frmStationAllInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.公交站点BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stationDataSet)).EndInit();
            this.fillByOBJECTIDToolStrip.ResumeLayout(false);
            this.fillByOBJECTIDToolStrip.PerformLayout();
            this.fillByINOBJECTIDToolStrip.ResumeLayout(false);
            this.fillByINOBJECTIDToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button Button1;
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