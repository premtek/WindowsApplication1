namespace Premtek
{
    partial class ucRecipeLine
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucRecipeLine));
            this.grpStep = new System.Windows.Forms.GroupBox();
            this.txtAvgWeight = new System.Windows.Forms.TextBox();
            this.lblAvgWeight = new System.Windows.Forms.Label();
            this.txtPitch = new System.Windows.Forms.TextBox();
            this.lblPitch = new System.Windows.Forms.Label();
            this.nmuWeight = new System.Windows.Forms.NumericUpDown();
            this.lblWeight = new System.Windows.Forms.Label();
            this.nmuDotCount = new System.Windows.Forms.NumericUpDown();
            this.lblDotCount = new System.Windows.Forms.Label();
            this.nmuVelocity = new System.Windows.Forms.NumericUpDown();
            this.lblVelocity = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.grpPos = new System.Windows.Forms.GroupBox();
            this.grpArcEnd = new System.Windows.Forms.GroupBox();
            this.btnSetEnd = new System.Windows.Forms.Button();
            this.nmuEndZ = new System.Windows.Forms.NumericUpDown();
            this.nmuEndY = new System.Windows.Forms.NumericUpDown();
            this.nmuEndX = new System.Windows.Forms.NumericUpDown();
            this.lblEndZ = new System.Windows.Forms.Label();
            this.lblEndY = new System.Windows.Forms.Label();
            this.lblEndX = new System.Windows.Forms.Label();
            this.btnGoEnd = new System.Windows.Forms.Button();
            this.grpStart = new System.Windows.Forms.GroupBox();
            this.btnSetStart = new System.Windows.Forms.Button();
            this.nmuStartZ = new System.Windows.Forms.NumericUpDown();
            this.nmuStartY = new System.Windows.Forms.NumericUpDown();
            this.nmuStartX = new System.Windows.Forms.NumericUpDown();
            this.lblStartZ = new System.Windows.Forms.Label();
            this.lblStartY = new System.Windows.Forms.Label();
            this.lblStartX = new System.Windows.Forms.Label();
            this.btnGoStart = new System.Windows.Forms.Button();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grpStep.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuDotCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuVelocity)).BeginInit();
            this.grpPos.SuspendLayout();
            this.grpArcEnd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuEndZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuEndY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuEndX)).BeginInit();
            this.grpStart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartX)).BeginInit();
            this.SuspendLayout();
            // 
            // grpStep
            // 
            this.grpStep.Controls.Add(this.txtAvgWeight);
            this.grpStep.Controls.Add(this.lblAvgWeight);
            this.grpStep.Controls.Add(this.txtPitch);
            this.grpStep.Controls.Add(this.lblPitch);
            this.grpStep.Controls.Add(this.nmuWeight);
            this.grpStep.Controls.Add(this.lblWeight);
            this.grpStep.Controls.Add(this.nmuDotCount);
            this.grpStep.Controls.Add(this.lblDotCount);
            this.grpStep.Controls.Add(this.nmuVelocity);
            this.grpStep.Controls.Add(this.lblVelocity);
            this.grpStep.Controls.Add(this.btnEdit);
            this.grpStep.Controls.Add(this.cmbType);
            this.grpStep.Controls.Add(this.lblType);
            this.grpStep.Controls.Add(this.grpPos);
            this.grpStep.Controls.Add(this.txtRemark);
            this.grpStep.Controls.Add(this.lblRemark);
            this.grpStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpStep.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpStep.Location = new System.Drawing.Point(0, 0);
            this.grpStep.Name = "grpStep";
            this.grpStep.Size = new System.Drawing.Size(990, 600);
            this.grpStep.TabIndex = 0;
            this.grpStep.TabStop = false;
            this.grpStep.Text = "線";
            // 
            // txtAvgWeight
            // 
            this.txtAvgWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAvgWeight.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtAvgWeight.Location = new System.Drawing.Point(656, 280);
            this.txtAvgWeight.Name = "txtAvgWeight";
            this.txtAvgWeight.Size = new System.Drawing.Size(94, 33);
            this.txtAvgWeight.TabIndex = 30;
            // 
            // lblAvgWeight
            // 
            this.lblAvgWeight.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblAvgWeight.Location = new System.Drawing.Point(513, 281);
            this.lblAvgWeight.Name = "lblAvgWeight";
            this.lblAvgWeight.Size = new System.Drawing.Size(140, 30);
            this.lblAvgWeight.TabIndex = 29;
            this.lblAvgWeight.Text = "單點均重(mg)";
            this.lblAvgWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPitch
            // 
            this.txtPitch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPitch.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtPitch.Location = new System.Drawing.Point(656, 230);
            this.txtPitch.Name = "txtPitch";
            this.txtPitch.Size = new System.Drawing.Size(94, 33);
            this.txtPitch.TabIndex = 28;
            // 
            // lblPitch
            // 
            this.lblPitch.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPitch.Location = new System.Drawing.Point(513, 231);
            this.lblPitch.Name = "lblPitch";
            this.lblPitch.Size = new System.Drawing.Size(140, 30);
            this.lblPitch.TabIndex = 27;
            this.lblPitch.Text = "打點間距(mm)";
            this.lblPitch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmuWeight
            // 
            this.nmuWeight.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.nmuWeight.Location = new System.Drawing.Point(656, 180);
            this.nmuWeight.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nmuWeight.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.nmuWeight.Name = "nmuWeight";
            this.nmuWeight.Size = new System.Drawing.Size(94, 33);
            this.nmuWeight.TabIndex = 26;
            this.nmuWeight.ValueChanged += new System.EventHandler(this.nmuWeight_ValueChanged);
            // 
            // lblWeight
            // 
            this.lblWeight.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblWeight.Location = new System.Drawing.Point(513, 180);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(140, 30);
            this.lblWeight.TabIndex = 25;
            this.lblWeight.Text = "膠重(mg)";
            this.lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmuDotCount
            // 
            this.nmuDotCount.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.nmuDotCount.Location = new System.Drawing.Point(656, 130);
            this.nmuDotCount.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nmuDotCount.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.nmuDotCount.Name = "nmuDotCount";
            this.nmuDotCount.Size = new System.Drawing.Size(94, 33);
            this.nmuDotCount.TabIndex = 24;
            this.nmuDotCount.ValueChanged += new System.EventHandler(this.nmuDotCount_ValueChanged);
            // 
            // lblDotCount
            // 
            this.lblDotCount.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblDotCount.Location = new System.Drawing.Point(553, 130);
            this.lblDotCount.Name = "lblDotCount";
            this.lblDotCount.Size = new System.Drawing.Size(100, 30);
            this.lblDotCount.TabIndex = 23;
            this.lblDotCount.Text = "打點數";
            this.lblDotCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmuVelocity
            // 
            this.nmuVelocity.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.nmuVelocity.Location = new System.Drawing.Point(656, 80);
            this.nmuVelocity.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nmuVelocity.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.nmuVelocity.Name = "nmuVelocity";
            this.nmuVelocity.Size = new System.Drawing.Size(94, 33);
            this.nmuVelocity.TabIndex = 22;
            this.nmuVelocity.ValueChanged += new System.EventHandler(this.nmuVelocity_ValueChanged);
            // 
            // lblVelocity
            // 
            this.lblVelocity.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblVelocity.Location = new System.Drawing.Point(513, 80);
            this.lblVelocity.Name = "lblVelocity";
            this.lblVelocity.Size = new System.Drawing.Size(140, 30);
            this.lblVelocity.TabIndex = 21;
            this.lblVelocity.Text = "點膠速度(mm/s)";
            this.lblVelocity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(755, 26);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(40, 40);
            this.btnEdit.TabIndex = 8;
            this.btnEdit.Text = "..";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "Default"});
            this.cmbType.Location = new System.Drawing.Point(656, 30);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(94, 32);
            this.cmbType.TabIndex = 7;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // lblType
            // 
            this.lblType.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblType.Location = new System.Drawing.Point(553, 30);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(100, 30);
            this.lblType.TabIndex = 6;
            this.lblType.Text = "型態";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpPos
            // 
            this.grpPos.Controls.Add(this.grpArcEnd);
            this.grpPos.Controls.Add(this.grpStart);
            this.grpPos.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpPos.Location = new System.Drawing.Point(6, 80);
            this.grpPos.Name = "grpPos";
            this.grpPos.Size = new System.Drawing.Size(500, 500);
            this.grpPos.TabIndex = 2;
            this.grpPos.TabStop = false;
            this.grpPos.Text = "位置";
            // 
            // grpArcEnd
            // 
            this.grpArcEnd.Controls.Add(this.btnSetEnd);
            this.grpArcEnd.Controls.Add(this.nmuEndZ);
            this.grpArcEnd.Controls.Add(this.nmuEndY);
            this.grpArcEnd.Controls.Add(this.nmuEndX);
            this.grpArcEnd.Controls.Add(this.lblEndZ);
            this.grpArcEnd.Controls.Add(this.lblEndY);
            this.grpArcEnd.Controls.Add(this.lblEndX);
            this.grpArcEnd.Controls.Add(this.btnGoEnd);
            this.grpArcEnd.Location = new System.Drawing.Point(4, 196);
            this.grpArcEnd.Name = "grpArcEnd";
            this.grpArcEnd.Size = new System.Drawing.Size(230, 160);
            this.grpArcEnd.TabIndex = 8;
            this.grpArcEnd.TabStop = false;
            this.grpArcEnd.Text = "線終點";
            // 
            // btnSetEnd
            // 
            this.btnSetEnd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSetEnd.BackgroundImage")));
            this.btnSetEnd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSetEnd.Location = new System.Drawing.Point(168, 91);
            this.btnSetEnd.Name = "btnSetEnd";
            this.btnSetEnd.Size = new System.Drawing.Size(50, 50);
            this.btnSetEnd.TabIndex = 14;
            this.toolTip1.SetToolTip(this.btnSetEnd, "Set");
            this.btnSetEnd.UseVisualStyleBackColor = true;
            // 
            // nmuEndZ
            // 
            this.nmuEndZ.DecimalPlaces = 3;
            this.nmuEndZ.Location = new System.Drawing.Point(74, 112);
            this.nmuEndZ.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuEndZ.Name = "nmuEndZ";
            this.nmuEndZ.Size = new System.Drawing.Size(88, 29);
            this.nmuEndZ.TabIndex = 13;
            this.nmuEndZ.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuEndZ.ValueChanged += new System.EventHandler(this.nmuEndZ_ValueChanged);
            // 
            // nmuEndY
            // 
            this.nmuEndY.DecimalPlaces = 3;
            this.nmuEndY.Location = new System.Drawing.Point(74, 72);
            this.nmuEndY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuEndY.Name = "nmuEndY";
            this.nmuEndY.Size = new System.Drawing.Size(88, 29);
            this.nmuEndY.TabIndex = 12;
            this.nmuEndY.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuEndY.ValueChanged += new System.EventHandler(this.nmuEndY_ValueChanged);
            // 
            // nmuEndX
            // 
            this.nmuEndX.DecimalPlaces = 3;
            this.nmuEndX.Location = new System.Drawing.Point(74, 32);
            this.nmuEndX.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuEndX.Name = "nmuEndX";
            this.nmuEndX.Size = new System.Drawing.Size(88, 29);
            this.nmuEndX.TabIndex = 11;
            this.nmuEndX.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuEndX.ValueChanged += new System.EventHandler(this.nmuEndX_ValueChanged);
            // 
            // lblEndZ
            // 
            this.lblEndZ.Location = new System.Drawing.Point(6, 110);
            this.lblEndZ.Name = "lblEndZ";
            this.lblEndZ.Size = new System.Drawing.Size(60, 30);
            this.lblEndZ.TabIndex = 8;
            this.lblEndZ.Text = "Z(mm)";
            this.lblEndZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEndY
            // 
            this.lblEndY.Location = new System.Drawing.Point(6, 70);
            this.lblEndY.Name = "lblEndY";
            this.lblEndY.Size = new System.Drawing.Size(60, 30);
            this.lblEndY.TabIndex = 9;
            this.lblEndY.Text = "Y(mm)";
            this.lblEndY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEndX
            // 
            this.lblEndX.Location = new System.Drawing.Point(6, 30);
            this.lblEndX.Name = "lblEndX";
            this.lblEndX.Size = new System.Drawing.Size(60, 30);
            this.lblEndX.TabIndex = 10;
            this.lblEndX.Text = "X(mm)";
            this.lblEndX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnGoEnd
            // 
            this.btnGoEnd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGoEnd.BackgroundImage")));
            this.btnGoEnd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGoEnd.Location = new System.Drawing.Point(168, 28);
            this.btnGoEnd.Name = "btnGoEnd";
            this.btnGoEnd.Size = new System.Drawing.Size(50, 50);
            this.btnGoEnd.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnGoEnd, "Go");
            this.btnGoEnd.UseVisualStyleBackColor = true;
            // 
            // grpStart
            // 
            this.grpStart.Controls.Add(this.btnSetStart);
            this.grpStart.Controls.Add(this.nmuStartZ);
            this.grpStart.Controls.Add(this.nmuStartY);
            this.grpStart.Controls.Add(this.nmuStartX);
            this.grpStart.Controls.Add(this.lblStartZ);
            this.grpStart.Controls.Add(this.lblStartY);
            this.grpStart.Controls.Add(this.lblStartX);
            this.grpStart.Controls.Add(this.btnGoStart);
            this.grpStart.Location = new System.Drawing.Point(6, 30);
            this.grpStart.Name = "grpStart";
            this.grpStart.Size = new System.Drawing.Size(230, 160);
            this.grpStart.TabIndex = 7;
            this.grpStart.TabStop = false;
            this.grpStart.Text = "線起點";
            // 
            // btnSetStart
            // 
            this.btnSetStart.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSetStart.BackgroundImage")));
            this.btnSetStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSetStart.Location = new System.Drawing.Point(168, 91);
            this.btnSetStart.Name = "btnSetStart";
            this.btnSetStart.Size = new System.Drawing.Size(50, 50);
            this.btnSetStart.TabIndex = 14;
            this.toolTip1.SetToolTip(this.btnSetStart, "Set");
            this.btnSetStart.UseVisualStyleBackColor = true;
            // 
            // nmuStartZ
            // 
            this.nmuStartZ.DecimalPlaces = 3;
            this.nmuStartZ.Location = new System.Drawing.Point(74, 112);
            this.nmuStartZ.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuStartZ.Name = "nmuStartZ";
            this.nmuStartZ.Size = new System.Drawing.Size(88, 29);
            this.nmuStartZ.TabIndex = 13;
            this.nmuStartZ.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuStartZ.ValueChanged += new System.EventHandler(this.nmuStartZ_ValueChanged);
            // 
            // nmuStartY
            // 
            this.nmuStartY.DecimalPlaces = 3;
            this.nmuStartY.Location = new System.Drawing.Point(74, 72);
            this.nmuStartY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuStartY.Name = "nmuStartY";
            this.nmuStartY.Size = new System.Drawing.Size(88, 29);
            this.nmuStartY.TabIndex = 12;
            this.nmuStartY.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuStartY.ValueChanged += new System.EventHandler(this.nmuStartY_ValueChanged);
            // 
            // nmuStartX
            // 
            this.nmuStartX.DecimalPlaces = 3;
            this.nmuStartX.Location = new System.Drawing.Point(74, 32);
            this.nmuStartX.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuStartX.Name = "nmuStartX";
            this.nmuStartX.Size = new System.Drawing.Size(88, 29);
            this.nmuStartX.TabIndex = 11;
            this.nmuStartX.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuStartX.ValueChanged += new System.EventHandler(this.nmuStartX_ValueChanged);
            // 
            // lblStartZ
            // 
            this.lblStartZ.Location = new System.Drawing.Point(6, 110);
            this.lblStartZ.Name = "lblStartZ";
            this.lblStartZ.Size = new System.Drawing.Size(60, 30);
            this.lblStartZ.TabIndex = 8;
            this.lblStartZ.Text = "Z(mm)";
            this.lblStartZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStartY
            // 
            this.lblStartY.Location = new System.Drawing.Point(6, 70);
            this.lblStartY.Name = "lblStartY";
            this.lblStartY.Size = new System.Drawing.Size(60, 30);
            this.lblStartY.TabIndex = 9;
            this.lblStartY.Text = "Y(mm)";
            this.lblStartY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStartX
            // 
            this.lblStartX.Location = new System.Drawing.Point(6, 30);
            this.lblStartX.Name = "lblStartX";
            this.lblStartX.Size = new System.Drawing.Size(60, 30);
            this.lblStartX.TabIndex = 10;
            this.lblStartX.Text = "X(mm)";
            this.lblStartX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnGoStart
            // 
            this.btnGoStart.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGoStart.BackgroundImage")));
            this.btnGoStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGoStart.Location = new System.Drawing.Point(168, 28);
            this.btnGoStart.Name = "btnGoStart";
            this.btnGoStart.Size = new System.Drawing.Size(50, 50);
            this.btnGoStart.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnGoStart, "Go");
            this.btnGoStart.UseVisualStyleBackColor = true;
            // 
            // txtRemark
            // 
            this.txtRemark.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtRemark.Location = new System.Drawing.Point(110, 30);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(376, 33);
            this.txtRemark.TabIndex = 1;
            this.txtRemark.TextChanged += new System.EventHandler(this.txtRemark_TextChanged);
            // 
            // lblRemark
            // 
            this.lblRemark.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblRemark.Location = new System.Drawing.Point(6, 30);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(100, 30);
            this.lblRemark.TabIndex = 0;
            this.lblRemark.Text = "備註";
            this.lblRemark.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ucRecipeLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.grpStep);
            this.Name = "ucRecipeLine";
            this.Size = new System.Drawing.Size(990, 600);
            this.grpStep.ResumeLayout(false);
            this.grpStep.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuDotCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuVelocity)).EndInit();
            this.grpPos.ResumeLayout(false);
            this.grpArcEnd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmuEndZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuEndY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuEndX)).EndInit();
            this.grpStart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpStep;
        private System.Windows.Forms.GroupBox grpPos;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.GroupBox grpArcEnd;
        private System.Windows.Forms.Button btnSetEnd;
        private System.Windows.Forms.NumericUpDown nmuEndZ;
        private System.Windows.Forms.NumericUpDown nmuEndY;
        private System.Windows.Forms.NumericUpDown nmuEndX;
        private System.Windows.Forms.Label lblEndZ;
        private System.Windows.Forms.Label lblEndY;
        private System.Windows.Forms.Label lblEndX;
        private System.Windows.Forms.Button btnGoEnd;
        private System.Windows.Forms.GroupBox grpStart;
        private System.Windows.Forms.Button btnSetStart;
        private System.Windows.Forms.NumericUpDown nmuStartZ;
        private System.Windows.Forms.NumericUpDown nmuStartY;
        private System.Windows.Forms.NumericUpDown nmuStartX;
        private System.Windows.Forms.Label lblStartZ;
        private System.Windows.Forms.Label lblStartY;
        private System.Windows.Forms.Label lblStartX;
        private System.Windows.Forms.Button btnGoStart;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.TextBox txtAvgWeight;
        private System.Windows.Forms.Label lblAvgWeight;
        private System.Windows.Forms.TextBox txtPitch;
        private System.Windows.Forms.Label lblPitch;
        private System.Windows.Forms.NumericUpDown nmuWeight;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.NumericUpDown nmuDotCount;
        private System.Windows.Forms.Label lblDotCount;
        private System.Windows.Forms.NumericUpDown nmuVelocity;
        private System.Windows.Forms.Label lblVelocity;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
