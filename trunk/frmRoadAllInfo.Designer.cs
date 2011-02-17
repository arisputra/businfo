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
            this.oBJECTIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roadIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.companyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roadNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roadTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roadTravelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstStartTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstCloseTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endStartTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endCloseTimDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ticketPrice1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ticketPrice2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ticketPrice3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roadNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lengthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.averageLoadFactorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.busNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.capacityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.passengerSumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.passengerWorkSumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.averageSpeedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nulineCoefficientDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nulineCoefficient2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.picture1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.picture2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.picture3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.picture4DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.picture5DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serveAreaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.averageLengthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.higeLoadFactorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roadLoadDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.directImbalanceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alternatelyCoefficientDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeCoefficientDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dayCoefficientDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.highHourSectDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.highHourAreaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.highHourMassDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.highPassengerMassDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sHAPELengthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.DataGridView1.AutoGenerateColumns = false;
            this.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Checkbox,
            this.oBJECTIDDataGridViewTextBoxColumn,
            this.roadIDDataGridViewTextBoxColumn,
            this.companyDataGridViewTextBoxColumn,
            this.roadNameDataGridViewTextBoxColumn,
            this.roadTypeDataGridViewTextBoxColumn,
            this.roadTravelDataGridViewTextBoxColumn,
            this.firstStartTimeDataGridViewTextBoxColumn,
            this.firstCloseTimeDataGridViewTextBoxColumn,
            this.endStartTimeDataGridViewTextBoxColumn,
            this.endCloseTimDataGridViewTextBoxColumn,
            this.ticketPrice1DataGridViewTextBoxColumn,
            this.ticketPrice2DataGridViewTextBoxColumn,
            this.ticketPrice3DataGridViewTextBoxColumn,
            this.roadNoDataGridViewTextBoxColumn,
            this.lengthDataGridViewTextBoxColumn,
            this.averageLoadFactorDataGridViewTextBoxColumn,
            this.busNumberDataGridViewTextBoxColumn,
            this.capacityDataGridViewTextBoxColumn,
            this.passengerSumDataGridViewTextBoxColumn,
            this.passengerWorkSumDataGridViewTextBoxColumn,
            this.averageSpeedDataGridViewTextBoxColumn,
            this.nulineCoefficientDataGridViewTextBoxColumn,
            this.nulineCoefficient2DataGridViewTextBoxColumn,
            this.picture1DataGridViewTextBoxColumn,
            this.picture2DataGridViewTextBoxColumn,
            this.picture3DataGridViewTextBoxColumn,
            this.picture4DataGridViewTextBoxColumn,
            this.picture5DataGridViewTextBoxColumn,
            this.unitDataGridViewTextBoxColumn,
            this.serveAreaDataGridViewTextBoxColumn,
            this.averageLengthDataGridViewTextBoxColumn,
            this.higeLoadFactorDataGridViewTextBoxColumn,
            this.roadLoadDataGridViewTextBoxColumn,
            this.directImbalanceDataGridViewTextBoxColumn,
            this.alternatelyCoefficientDataGridViewTextBoxColumn,
            this.timeCoefficientDataGridViewTextBoxColumn,
            this.dayCoefficientDataGridViewTextBoxColumn,
            this.highHourSectDataGridViewTextBoxColumn,
            this.highHourAreaDataGridViewTextBoxColumn,
            this.highHourMassDataGridViewTextBoxColumn,
            this.highPassengerMassDataGridViewTextBoxColumn,
            this.sHAPELengthDataGridViewTextBoxColumn});
            this.DataGridView1.DataSource = this.公交站线BindingSource;
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
            // roadIDDataGridViewTextBoxColumn
            // 
            this.roadIDDataGridViewTextBoxColumn.DataPropertyName = "RoadID";
            this.roadIDDataGridViewTextBoxColumn.HeaderText = "RoadID";
            this.roadIDDataGridViewTextBoxColumn.Name = "roadIDDataGridViewTextBoxColumn";
            this.roadIDDataGridViewTextBoxColumn.Visible = false;
            this.roadIDDataGridViewTextBoxColumn.Width = 66;
            // 
            // companyDataGridViewTextBoxColumn
            // 
            this.companyDataGridViewTextBoxColumn.DataPropertyName = "Company";
            this.companyDataGridViewTextBoxColumn.Frozen = true;
            this.companyDataGridViewTextBoxColumn.HeaderText = "所属公司";
            this.companyDataGridViewTextBoxColumn.Name = "companyDataGridViewTextBoxColumn";
            this.companyDataGridViewTextBoxColumn.Width = 61;
            // 
            // roadNameDataGridViewTextBoxColumn
            // 
            this.roadNameDataGridViewTextBoxColumn.DataPropertyName = "RoadName";
            this.roadNameDataGridViewTextBoxColumn.Frozen = true;
            this.roadNameDataGridViewTextBoxColumn.HeaderText = "线路名称";
            this.roadNameDataGridViewTextBoxColumn.Name = "roadNameDataGridViewTextBoxColumn";
            this.roadNameDataGridViewTextBoxColumn.Width = 61;
            // 
            // roadTypeDataGridViewTextBoxColumn
            // 
            this.roadTypeDataGridViewTextBoxColumn.DataPropertyName = "RoadType";
            this.roadTypeDataGridViewTextBoxColumn.HeaderText = "线路类型";
            this.roadTypeDataGridViewTextBoxColumn.Name = "roadTypeDataGridViewTextBoxColumn";
            this.roadTypeDataGridViewTextBoxColumn.Width = 61;
            // 
            // roadTravelDataGridViewTextBoxColumn
            // 
            this.roadTravelDataGridViewTextBoxColumn.DataPropertyName = "RoadTravel";
            this.roadTravelDataGridViewTextBoxColumn.HeaderText = "线路行程";
            this.roadTravelDataGridViewTextBoxColumn.Name = "roadTravelDataGridViewTextBoxColumn";
            this.roadTravelDataGridViewTextBoxColumn.Width = 61;
            // 
            // firstStartTimeDataGridViewTextBoxColumn
            // 
            this.firstStartTimeDataGridViewTextBoxColumn.DataPropertyName = "FirstStartTime";
            this.firstStartTimeDataGridViewTextBoxColumn.HeaderText = "首站开班时间";
            this.firstStartTimeDataGridViewTextBoxColumn.Name = "firstStartTimeDataGridViewTextBoxColumn";
            this.firstStartTimeDataGridViewTextBoxColumn.Width = 72;
            // 
            // firstCloseTimeDataGridViewTextBoxColumn
            // 
            this.firstCloseTimeDataGridViewTextBoxColumn.DataPropertyName = "FirstCloseTime";
            this.firstCloseTimeDataGridViewTextBoxColumn.HeaderText = "首站收班时间";
            this.firstCloseTimeDataGridViewTextBoxColumn.Name = "firstCloseTimeDataGridViewTextBoxColumn";
            this.firstCloseTimeDataGridViewTextBoxColumn.Width = 72;
            // 
            // endStartTimeDataGridViewTextBoxColumn
            // 
            this.endStartTimeDataGridViewTextBoxColumn.DataPropertyName = "EndStartTime";
            this.endStartTimeDataGridViewTextBoxColumn.HeaderText = "末站开班时间";
            this.endStartTimeDataGridViewTextBoxColumn.Name = "endStartTimeDataGridViewTextBoxColumn";
            this.endStartTimeDataGridViewTextBoxColumn.Width = 72;
            // 
            // endCloseTimDataGridViewTextBoxColumn
            // 
            this.endCloseTimDataGridViewTextBoxColumn.DataPropertyName = "EndCloseTim";
            this.endCloseTimDataGridViewTextBoxColumn.HeaderText = "末站收班时间";
            this.endCloseTimDataGridViewTextBoxColumn.Name = "endCloseTimDataGridViewTextBoxColumn";
            this.endCloseTimDataGridViewTextBoxColumn.Width = 72;
            // 
            // ticketPrice1DataGridViewTextBoxColumn
            // 
            this.ticketPrice1DataGridViewTextBoxColumn.DataPropertyName = "TicketPrice1";
            this.ticketPrice1DataGridViewTextBoxColumn.HeaderText = "票价1";
            this.ticketPrice1DataGridViewTextBoxColumn.Name = "ticketPrice1DataGridViewTextBoxColumn";
            this.ticketPrice1DataGridViewTextBoxColumn.Width = 51;
            // 
            // ticketPrice2DataGridViewTextBoxColumn
            // 
            this.ticketPrice2DataGridViewTextBoxColumn.DataPropertyName = "TicketPrice2";
            this.ticketPrice2DataGridViewTextBoxColumn.HeaderText = "票价2";
            this.ticketPrice2DataGridViewTextBoxColumn.Name = "ticketPrice2DataGridViewTextBoxColumn";
            this.ticketPrice2DataGridViewTextBoxColumn.Width = 51;
            // 
            // ticketPrice3DataGridViewTextBoxColumn
            // 
            this.ticketPrice3DataGridViewTextBoxColumn.DataPropertyName = "TicketPrice3";
            this.ticketPrice3DataGridViewTextBoxColumn.HeaderText = "票价3";
            this.ticketPrice3DataGridViewTextBoxColumn.Name = "ticketPrice3DataGridViewTextBoxColumn";
            this.ticketPrice3DataGridViewTextBoxColumn.Width = 51;
            // 
            // roadNoDataGridViewTextBoxColumn
            // 
            this.roadNoDataGridViewTextBoxColumn.DataPropertyName = "RoadNo";
            this.roadNoDataGridViewTextBoxColumn.HeaderText = "线路编号";
            this.roadNoDataGridViewTextBoxColumn.Name = "roadNoDataGridViewTextBoxColumn";
            this.roadNoDataGridViewTextBoxColumn.Width = 61;
            // 
            // lengthDataGridViewTextBoxColumn
            // 
            this.lengthDataGridViewTextBoxColumn.DataPropertyName = "Length";
            this.lengthDataGridViewTextBoxColumn.HeaderText = "长度";
            this.lengthDataGridViewTextBoxColumn.Name = "lengthDataGridViewTextBoxColumn";
            this.lengthDataGridViewTextBoxColumn.Width = 51;
            // 
            // averageLoadFactorDataGridViewTextBoxColumn
            // 
            this.averageLoadFactorDataGridViewTextBoxColumn.DataPropertyName = "AverageLoadFactor";
            this.averageLoadFactorDataGridViewTextBoxColumn.HeaderText = "平均满载率";
            this.averageLoadFactorDataGridViewTextBoxColumn.Name = "averageLoadFactorDataGridViewTextBoxColumn";
            this.averageLoadFactorDataGridViewTextBoxColumn.Width = 72;
            // 
            // busNumberDataGridViewTextBoxColumn
            // 
            this.busNumberDataGridViewTextBoxColumn.DataPropertyName = "BusNumber";
            this.busNumberDataGridViewTextBoxColumn.HeaderText = "运营车次";
            this.busNumberDataGridViewTextBoxColumn.Name = "busNumberDataGridViewTextBoxColumn";
            this.busNumberDataGridViewTextBoxColumn.Width = 61;
            // 
            // capacityDataGridViewTextBoxColumn
            // 
            this.capacityDataGridViewTextBoxColumn.DataPropertyName = "Capacity";
            this.capacityDataGridViewTextBoxColumn.HeaderText = "运力配备";
            this.capacityDataGridViewTextBoxColumn.Name = "capacityDataGridViewTextBoxColumn";
            this.capacityDataGridViewTextBoxColumn.Width = 61;
            // 
            // passengerSumDataGridViewTextBoxColumn
            // 
            this.passengerSumDataGridViewTextBoxColumn.DataPropertyName = "PassengerSum";
            this.passengerSumDataGridViewTextBoxColumn.HeaderText = "客运量";
            this.passengerSumDataGridViewTextBoxColumn.Name = "passengerSumDataGridViewTextBoxColumn";
            this.passengerSumDataGridViewTextBoxColumn.Width = 61;
            // 
            // passengerWorkSumDataGridViewTextBoxColumn
            // 
            this.passengerWorkSumDataGridViewTextBoxColumn.DataPropertyName = "PassengerWorkSum";
            this.passengerWorkSumDataGridViewTextBoxColumn.HeaderText = "客运工作量";
            this.passengerWorkSumDataGridViewTextBoxColumn.Name = "passengerWorkSumDataGridViewTextBoxColumn";
            this.passengerWorkSumDataGridViewTextBoxColumn.Width = 72;
            // 
            // averageSpeedDataGridViewTextBoxColumn
            // 
            this.averageSpeedDataGridViewTextBoxColumn.DataPropertyName = "AverageSpeed";
            this.averageSpeedDataGridViewTextBoxColumn.HeaderText = "平均车速";
            this.averageSpeedDataGridViewTextBoxColumn.Name = "averageSpeedDataGridViewTextBoxColumn";
            this.averageSpeedDataGridViewTextBoxColumn.Width = 61;
            // 
            // nulineCoefficientDataGridViewTextBoxColumn
            // 
            this.nulineCoefficientDataGridViewTextBoxColumn.DataPropertyName = "NulineCoefficient";
            this.nulineCoefficientDataGridViewTextBoxColumn.HeaderText = "非直线系数";
            this.nulineCoefficientDataGridViewTextBoxColumn.Name = "nulineCoefficientDataGridViewTextBoxColumn";
            this.nulineCoefficientDataGridViewTextBoxColumn.Width = 72;
            // 
            // nulineCoefficient2DataGridViewTextBoxColumn
            // 
            this.nulineCoefficient2DataGridViewTextBoxColumn.DataPropertyName = "NulineCoefficient2";
            this.nulineCoefficient2DataGridViewTextBoxColumn.HeaderText = "非直线系数2";
            this.nulineCoefficient2DataGridViewTextBoxColumn.Name = "nulineCoefficient2DataGridViewTextBoxColumn";
            this.nulineCoefficient2DataGridViewTextBoxColumn.Width = 72;
            // 
            // picture1DataGridViewTextBoxColumn
            // 
            this.picture1DataGridViewTextBoxColumn.DataPropertyName = "Picture1";
            this.picture1DataGridViewTextBoxColumn.HeaderText = "图片1";
            this.picture1DataGridViewTextBoxColumn.Name = "picture1DataGridViewTextBoxColumn";
            this.picture1DataGridViewTextBoxColumn.Width = 51;
            // 
            // picture2DataGridViewTextBoxColumn
            // 
            this.picture2DataGridViewTextBoxColumn.DataPropertyName = "Picture2";
            this.picture2DataGridViewTextBoxColumn.HeaderText = "图片2";
            this.picture2DataGridViewTextBoxColumn.Name = "picture2DataGridViewTextBoxColumn";
            this.picture2DataGridViewTextBoxColumn.Width = 51;
            // 
            // picture3DataGridViewTextBoxColumn
            // 
            this.picture3DataGridViewTextBoxColumn.DataPropertyName = "Picture3";
            this.picture3DataGridViewTextBoxColumn.HeaderText = "图片3";
            this.picture3DataGridViewTextBoxColumn.Name = "picture3DataGridViewTextBoxColumn";
            this.picture3DataGridViewTextBoxColumn.Width = 51;
            // 
            // picture4DataGridViewTextBoxColumn
            // 
            this.picture4DataGridViewTextBoxColumn.DataPropertyName = "Picture4";
            this.picture4DataGridViewTextBoxColumn.HeaderText = "图片4";
            this.picture4DataGridViewTextBoxColumn.Name = "picture4DataGridViewTextBoxColumn";
            this.picture4DataGridViewTextBoxColumn.Width = 51;
            // 
            // picture5DataGridViewTextBoxColumn
            // 
            this.picture5DataGridViewTextBoxColumn.DataPropertyName = "Picture5";
            this.picture5DataGridViewTextBoxColumn.HeaderText = "图片5";
            this.picture5DataGridViewTextBoxColumn.Name = "picture5DataGridViewTextBoxColumn";
            this.picture5DataGridViewTextBoxColumn.Width = 51;
            // 
            // unitDataGridViewTextBoxColumn
            // 
            this.unitDataGridViewTextBoxColumn.DataPropertyName = "Unit";
            this.unitDataGridViewTextBoxColumn.HeaderText = "所属单位";
            this.unitDataGridViewTextBoxColumn.Name = "unitDataGridViewTextBoxColumn";
            this.unitDataGridViewTextBoxColumn.Width = 61;
            // 
            // serveAreaDataGridViewTextBoxColumn
            // 
            this.serveAreaDataGridViewTextBoxColumn.DataPropertyName = "ServeArea";
            this.serveAreaDataGridViewTextBoxColumn.HeaderText = "服务面积";
            this.serveAreaDataGridViewTextBoxColumn.Name = "serveAreaDataGridViewTextBoxColumn";
            this.serveAreaDataGridViewTextBoxColumn.Width = 61;
            // 
            // averageLengthDataGridViewTextBoxColumn
            // 
            this.averageLengthDataGridViewTextBoxColumn.DataPropertyName = "AverageLength";
            this.averageLengthDataGridViewTextBoxColumn.HeaderText = "平均运距";
            this.averageLengthDataGridViewTextBoxColumn.Name = "averageLengthDataGridViewTextBoxColumn";
            this.averageLengthDataGridViewTextBoxColumn.Width = 61;
            // 
            // higeLoadFactorDataGridViewTextBoxColumn
            // 
            this.higeLoadFactorDataGridViewTextBoxColumn.DataPropertyName = "HigeLoadFactor";
            this.higeLoadFactorDataGridViewTextBoxColumn.HeaderText = "高峰满载率";
            this.higeLoadFactorDataGridViewTextBoxColumn.Name = "higeLoadFactorDataGridViewTextBoxColumn";
            this.higeLoadFactorDataGridViewTextBoxColumn.Width = 72;
            // 
            // roadLoadDataGridViewTextBoxColumn
            // 
            this.roadLoadDataGridViewTextBoxColumn.DataPropertyName = "RoadLoad";
            this.roadLoadDataGridViewTextBoxColumn.HeaderText = "线路负荷";
            this.roadLoadDataGridViewTextBoxColumn.Name = "roadLoadDataGridViewTextBoxColumn";
            this.roadLoadDataGridViewTextBoxColumn.Width = 61;
            // 
            // directImbalanceDataGridViewTextBoxColumn
            // 
            this.directImbalanceDataGridViewTextBoxColumn.DataPropertyName = "DirectImbalance";
            this.directImbalanceDataGridViewTextBoxColumn.HeaderText = "方向不均衡";
            this.directImbalanceDataGridViewTextBoxColumn.Name = "directImbalanceDataGridViewTextBoxColumn";
            this.directImbalanceDataGridViewTextBoxColumn.Width = 72;
            // 
            // alternatelyCoefficientDataGridViewTextBoxColumn
            // 
            this.alternatelyCoefficientDataGridViewTextBoxColumn.DataPropertyName = "AlternatelyCoefficient";
            this.alternatelyCoefficientDataGridViewTextBoxColumn.HeaderText = "交替系数";
            this.alternatelyCoefficientDataGridViewTextBoxColumn.Name = "alternatelyCoefficientDataGridViewTextBoxColumn";
            this.alternatelyCoefficientDataGridViewTextBoxColumn.Width = 61;
            // 
            // timeCoefficientDataGridViewTextBoxColumn
            // 
            this.timeCoefficientDataGridViewTextBoxColumn.DataPropertyName = "TimeCoefficient";
            this.timeCoefficientDataGridViewTextBoxColumn.HeaderText = "时不均系数";
            this.timeCoefficientDataGridViewTextBoxColumn.Name = "timeCoefficientDataGridViewTextBoxColumn";
            this.timeCoefficientDataGridViewTextBoxColumn.Width = 72;
            // 
            // dayCoefficientDataGridViewTextBoxColumn
            // 
            this.dayCoefficientDataGridViewTextBoxColumn.DataPropertyName = "DayCoefficient";
            this.dayCoefficientDataGridViewTextBoxColumn.HeaderText = "天不均系数";
            this.dayCoefficientDataGridViewTextBoxColumn.Name = "dayCoefficientDataGridViewTextBoxColumn";
            this.dayCoefficientDataGridViewTextBoxColumn.Width = 72;
            // 
            // highHourSectDataGridViewTextBoxColumn
            // 
            this.highHourSectDataGridViewTextBoxColumn.DataPropertyName = "HighHourSect";
            this.highHourSectDataGridViewTextBoxColumn.HeaderText = "高峰小时段";
            this.highHourSectDataGridViewTextBoxColumn.Name = "highHourSectDataGridViewTextBoxColumn";
            this.highHourSectDataGridViewTextBoxColumn.Width = 72;
            // 
            // highHourAreaDataGridViewTextBoxColumn
            // 
            this.highHourAreaDataGridViewTextBoxColumn.DataPropertyName = "HighHourArea";
            this.highHourAreaDataGridViewTextBoxColumn.HeaderText = "高峰小时面";
            this.highHourAreaDataGridViewTextBoxColumn.Name = "highHourAreaDataGridViewTextBoxColumn";
            this.highHourAreaDataGridViewTextBoxColumn.Width = 72;
            // 
            // highHourMassDataGridViewTextBoxColumn
            // 
            this.highHourMassDataGridViewTextBoxColumn.DataPropertyName = "HighHourMass";
            this.highHourMassDataGridViewTextBoxColumn.HeaderText = "高峰小时量";
            this.highHourMassDataGridViewTextBoxColumn.Name = "highHourMassDataGridViewTextBoxColumn";
            this.highHourMassDataGridViewTextBoxColumn.Width = 72;
            // 
            // highPassengerMassDataGridViewTextBoxColumn
            // 
            this.highPassengerMassDataGridViewTextBoxColumn.DataPropertyName = "HighPassengerMass";
            this.highPassengerMassDataGridViewTextBoxColumn.HeaderText = "高峰客运量";
            this.highPassengerMassDataGridViewTextBoxColumn.Name = "highPassengerMassDataGridViewTextBoxColumn";
            this.highPassengerMassDataGridViewTextBoxColumn.Width = 72;
            // 
            // sHAPELengthDataGridViewTextBoxColumn
            // 
            this.sHAPELengthDataGridViewTextBoxColumn.DataPropertyName = "SHAPE_Length";
            this.sHAPELengthDataGridViewTextBoxColumn.HeaderText = "线路长度";
            this.sHAPELengthDataGridViewTextBoxColumn.Name = "sHAPELengthDataGridViewTextBoxColumn";
            this.sHAPELengthDataGridViewTextBoxColumn.Width = 61;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn oBJECTIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn roadIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn companyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn roadNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn roadTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn roadTravelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstStartTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstCloseTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn endStartTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn endCloseTimDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ticketPrice1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ticketPrice2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ticketPrice3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn roadNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lengthDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn averageLoadFactorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn busNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn capacityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn passengerSumDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn passengerWorkSumDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn averageSpeedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nulineCoefficientDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nulineCoefficient2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn picture1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn picture2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn picture3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn picture4DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn picture5DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serveAreaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn averageLengthDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn higeLoadFactorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn roadLoadDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn directImbalanceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn alternatelyCoefficientDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeCoefficientDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dayCoefficientDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn highHourSectDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn highHourAreaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn highHourMassDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn highPassengerMassDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sHAPELengthDataGridViewTextBoxColumn;
        private System.Windows.Forms.ToolStrip fillByINOBJECTIDToolStrip;
        private System.Windows.Forms.ToolStripLabel param1ToolStripLabel;
        private System.Windows.Forms.ToolStripTextBox param1ToolStripTextBox;
        private System.Windows.Forms.ToolStripButton fillByINOBJECTIDToolStripButton;
    }
}