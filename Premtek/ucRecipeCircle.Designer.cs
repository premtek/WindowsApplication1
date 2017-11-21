namespace Premtek
{
    partial class ucRecipeCircle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucRecipeCircle));
            this.cmbDirection = new System.Windows.Forms.ComboBox();
            this.lblDirection = new System.Windows.Forms.Label();
            this.cmbMethod = new System.Windows.Forms.ComboBox();
            this.lblMethod = new System.Windows.Forms.Label();
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
            this.grpArcCenter = new System.Windows.Forms.GroupBox();
            this.btnSetCenter = new System.Windows.Forms.Button();
            this.nmuCenterZ = new System.Windows.Forms.NumericUpDown();
            this.nmuCenterY = new System.Windows.Forms.NumericUpDown();
            this.nmuCenterX = new System.Windows.Forms.NumericUpDown();
            this.lblCenterZ = new System.Windows.Forms.Label();
            this.lblCenterY = new System.Windows.Forms.Label();
            this.lblCenterX = new System.Windows.Forms.Label();
            this.btnGoCenter = new System.Windows.Forms.Button();
            this.grpArcEnd = new System.Windows.Forms.GroupBox();
            this.btnSetEnd = new System.Windows.Forms.Button();
            this.nmuEndZ = new System.Windows.Forms.NumericUpDown();
            this.nmuEndY = new System.Windows.Forms.NumericUpDown();
            this.nmuEndX = new System.Windows.Forms.NumericUpDown();
            this.lblBSideZ = new System.Windows.Forms.Label();
            this.lblBSideY = new System.Windows.Forms.Label();
            this.lblBSideX = new System.Windows.Forms.Label();
            this.btnGoEnd = new System.Windows.Forms.Button();
            this.grpArcMiddle = new System.Windows.Forms.GroupBox();
            this.btnSetMiddle = new System.Windows.Forms.Button();
            this.nmuMiddleZ = new System.Windows.Forms.NumericUpDown();
            this.nmuMiddleY = new System.Windows.Forms.NumericUpDown();
            this.nmuMiddleX = new System.Windows.Forms.NumericUpDown();
            this.lblASideZ = new System.Windows.Forms.Label();
            this.lblASideY = new System.Windows.Forms.Label();
            this.lblASideX = new System.Windows.Forms.Label();
            this.btnGoMiddle = new System.Windows.Forms.Button();
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
            this.grpArcCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuCenterZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuCenterY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuCenterX)).BeginInit();
            this.grpArcEnd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuEndZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuEndY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuEndX)).BeginInit();
            this.grpArcMiddle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuMiddleZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuMiddleY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuMiddleX)).BeginInit();
            this.grpStart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartX)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbDirection
            // 
            this.cmbDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDirection.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbDirection.FormattingEnabled = true;
            this.cmbDirection.Items.AddRange(new object[] {
            "Default"});
            this.cmbDirection.Location = new System.Drawing.Point(656, 130);
            this.cmbDirection.Name = "cmbDirection";
            this.cmbDirection.Size = new System.Drawing.Size(94, 32);
            this.cmbDirection.TabIndex = 7;
            this.cmbDirection.SelectedIndexChanged += new System.EventHandler(this.cmbDirection_SelectedIndexChanged);
            // 
            // lblDirection
            // 
            this.lblDirection.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblDirection.Location = new System.Drawing.Point(553, 132);
            this.lblDirection.Name = "lblDirection";
            this.lblDirection.Size = new System.Drawing.Size(100, 30);
            this.lblDirection.TabIndex = 6;
            this.lblDirection.Text = "方向";
            this.lblDirection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbMethod
            // 
            this.cmbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMethod.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbMethod.FormattingEnabled = true;
            this.cmbMethod.Items.AddRange(new object[] {
            "Default"});
            this.cmbMethod.Location = new System.Drawing.Point(656, 80);
            this.cmbMethod.Name = "cmbMethod";
            this.cmbMethod.Size = new System.Drawing.Size(94, 32);
            this.cmbMethod.TabIndex = 7;
            this.cmbMethod.SelectedIndexChanged += new System.EventHandler(this.cmbMethod_SelectedIndexChanged);
            // 
            // lblMethod
            // 
            this.lblMethod.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblMethod.Location = new System.Drawing.Point(553, 80);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Size = new System.Drawing.Size(100, 30);
            this.lblMethod.TabIndex = 6;
            this.lblMethod.Text = "方法";
            this.lblMethod.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.grpStep.Controls.Add(this.cmbDirection);
            this.grpStep.Controls.Add(this.lblDirection);
            this.grpStep.Controls.Add(this.cmbMethod);
            this.grpStep.Controls.Add(this.lblMethod);
            this.grpStep.Controls.Add(this.txtRemark);
            this.grpStep.Controls.Add(this.lblRemark);
            this.grpStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpStep.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpStep.Location = new System.Drawing.Point(0, 0);
            this.grpStep.Name = "grpStep";
            this.grpStep.Size = new System.Drawing.Size(990, 600);
            this.grpStep.TabIndex = 2;
            this.grpStep.TabStop = false;
            this.grpStep.Text = "圓";
            // 
            // txtAvgWeight
            // 
            this.txtAvgWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAvgWeight.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtAvgWeight.Location = new System.Drawing.Point(656, 380);
            this.txtAvgWeight.Name = "txtAvgWeight";
            this.txtAvgWeight.Size = new System.Drawing.Size(94, 33);
            this.txtAvgWeight.TabIndex = 30;
            // 
            // lblAvgWeight
            // 
            this.lblAvgWeight.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblAvgWeight.Location = new System.Drawing.Point(513, 381);
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
            this.txtPitch.Location = new System.Drawing.Point(656, 330);
            this.txtPitch.Name = "txtPitch";
            this.txtPitch.Size = new System.Drawing.Size(94, 33);
            this.txtPitch.TabIndex = 28;
            // 
            // lblPitch
            // 
            this.lblPitch.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPitch.Location = new System.Drawing.Point(513, 331);
            this.lblPitch.Name = "lblPitch";
            this.lblPitch.Size = new System.Drawing.Size(140, 30);
            this.lblPitch.TabIndex = 27;
            this.lblPitch.Text = "打點間距(mm)";
            this.lblPitch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmuWeight
            // 
            this.nmuWeight.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.nmuWeight.Location = new System.Drawing.Point(656, 280);
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
            this.lblWeight.Location = new System.Drawing.Point(513, 280);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(140, 30);
            this.lblWeight.TabIndex = 25;
            this.lblWeight.Text = "膠重(mg)";
            this.lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmuDotCount
            // 
            this.nmuDotCount.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.nmuDotCount.Location = new System.Drawing.Point(656, 230);
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
            this.lblDotCount.Location = new System.Drawing.Point(553, 230);
            this.lblDotCount.Name = "lblDotCount";
            this.lblDotCount.Size = new System.Drawing.Size(100, 30);
            this.lblDotCount.TabIndex = 23;
            this.lblDotCount.Text = "打點數";
            this.lblDotCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmuVelocity
            // 
            this.nmuVelocity.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.nmuVelocity.Location = new System.Drawing.Point(656, 180);
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
            this.lblVelocity.Location = new System.Drawing.Point(513, 180);
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
            this.btnEdit.TabIndex = 11;
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
            this.cmbType.TabIndex = 10;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // lblType
            // 
            this.lblType.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblType.Location = new System.Drawing.Point(553, 30);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(100, 30);
            this.lblType.TabIndex = 9;
            this.lblType.Text = "型態";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpPos
            // 
            this.grpPos.Controls.Add(this.grpArcCenter);
            this.grpPos.Controls.Add(this.grpArcEnd);
            this.grpPos.Controls.Add(this.grpArcMiddle);
            this.grpPos.Controls.Add(this.grpStart);
            this.grpPos.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpPos.Location = new System.Drawing.Point(6, 80);
            this.grpPos.Name = "grpPos";
            this.grpPos.Size = new System.Drawing.Size(500, 500);
            this.grpPos.TabIndex = 8;
            this.grpPos.TabStop = false;
            this.grpPos.Text = "位置";
            // 
            // grpArcCenter
            // 
            this.grpArcCenter.Controls.Add(this.btnSetCenter);
            this.grpArcCenter.Controls.Add(this.nmuCenterZ);
            this.grpArcCenter.Controls.Add(this.nmuCenterY);
            this.grpArcCenter.Controls.Add(this.nmuCenterX);
            this.grpArcCenter.Controls.Add(this.lblCenterZ);
            this.grpArcCenter.Controls.Add(this.lblCenterY);
            this.grpArcCenter.Controls.Add(this.lblCenterX);
            this.grpArcCenter.Controls.Add(this.btnGoCenter);
            this.grpArcCenter.Location = new System.Drawing.Point(248, 196);
            this.grpArcCenter.Name = "grpArcCenter";
            this.grpArcCenter.Size = new System.Drawing.Size(230, 160);
            this.grpArcCenter.TabIndex = 7;
            this.grpArcCenter.TabStop = false;
            this.grpArcCenter.Text = "圓心點";
            // 
            // btnSetCenter
            // 
            this.btnSetCenter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSetCenter.BackgroundImage")));
            this.btnSetCenter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSetCenter.Location = new System.Drawing.Point(168, 91);
            this.btnSetCenter.Name = "btnSetCenter";
            this.btnSetCenter.Size = new System.Drawing.Size(50, 50);
            this.btnSetCenter.TabIndex = 14;
            this.toolTip1.SetToolTip(this.btnSetCenter, "Set");
            this.btnSetCenter.UseVisualStyleBackColor = true;
            this.btnSetCenter.Click += new System.EventHandler(this.btnSetCenter_Click);
            // 
            // nmuCenterZ
            // 
            this.nmuCenterZ.DecimalPlaces = 3;
            this.nmuCenterZ.Location = new System.Drawing.Point(74, 112);
            this.nmuCenterZ.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuCenterZ.Name = "nmuCenterZ";
            this.nmuCenterZ.Size = new System.Drawing.Size(88, 29);
            this.nmuCenterZ.TabIndex = 13;
            this.nmuCenterZ.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuCenterZ.ValueChanged += new System.EventHandler(this.nmuCenterZ_ValueChanged);
            // 
            // nmuCenterY
            // 
            this.nmuCenterY.DecimalPlaces = 3;
            this.nmuCenterY.Location = new System.Drawing.Point(74, 72);
            this.nmuCenterY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuCenterY.Name = "nmuCenterY";
            this.nmuCenterY.Size = new System.Drawing.Size(88, 29);
            this.nmuCenterY.TabIndex = 12;
            this.nmuCenterY.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuCenterY.ValueChanged += new System.EventHandler(this.nmuCenterY_ValueChanged);
            // 
            // nmuCenterX
            // 
            this.nmuCenterX.DecimalPlaces = 3;
            this.nmuCenterX.Location = new System.Drawing.Point(74, 32);
            this.nmuCenterX.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuCenterX.Name = "nmuCenterX";
            this.nmuCenterX.Size = new System.Drawing.Size(88, 29);
            this.nmuCenterX.TabIndex = 11;
            this.nmuCenterX.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuCenterX.ValueChanged += new System.EventHandler(this.nmuCenterX_ValueChanged);
            // 
            // lblCenterZ
            // 
            this.lblCenterZ.Location = new System.Drawing.Point(6, 110);
            this.lblCenterZ.Name = "lblCenterZ";
            this.lblCenterZ.Size = new System.Drawing.Size(60, 30);
            this.lblCenterZ.TabIndex = 8;
            this.lblCenterZ.Text = "Z(mm)";
            this.lblCenterZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCenterY
            // 
            this.lblCenterY.Location = new System.Drawing.Point(6, 70);
            this.lblCenterY.Name = "lblCenterY";
            this.lblCenterY.Size = new System.Drawing.Size(60, 30);
            this.lblCenterY.TabIndex = 9;
            this.lblCenterY.Text = "Y(mm)";
            this.lblCenterY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCenterX
            // 
            this.lblCenterX.Location = new System.Drawing.Point(6, 30);
            this.lblCenterX.Name = "lblCenterX";
            this.lblCenterX.Size = new System.Drawing.Size(60, 30);
            this.lblCenterX.TabIndex = 10;
            this.lblCenterX.Text = "X(mm)";
            this.lblCenterX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnGoCenter
            // 
            this.btnGoCenter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGoCenter.BackgroundImage")));
            this.btnGoCenter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGoCenter.Location = new System.Drawing.Point(168, 28);
            this.btnGoCenter.Name = "btnGoCenter";
            this.btnGoCenter.Size = new System.Drawing.Size(50, 50);
            this.btnGoCenter.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnGoCenter, "Go");
            this.btnGoCenter.UseVisualStyleBackColor = true;
            this.btnGoCenter.Click += new System.EventHandler(this.btnGoCenter_Click);
            // 
            // grpArcEnd
            // 
            this.grpArcEnd.Controls.Add(this.btnSetEnd);
            this.grpArcEnd.Controls.Add(this.nmuEndZ);
            this.grpArcEnd.Controls.Add(this.nmuEndY);
            this.grpArcEnd.Controls.Add(this.nmuEndX);
            this.grpArcEnd.Controls.Add(this.lblBSideZ);
            this.grpArcEnd.Controls.Add(this.lblBSideY);
            this.grpArcEnd.Controls.Add(this.lblBSideX);
            this.grpArcEnd.Controls.Add(this.btnGoEnd);
            this.grpArcEnd.Location = new System.Drawing.Point(4, 196);
            this.grpArcEnd.Name = "grpArcEnd";
            this.grpArcEnd.Size = new System.Drawing.Size(230, 160);
            this.grpArcEnd.TabIndex = 6;
            this.grpArcEnd.TabStop = false;
            this.grpArcEnd.Text = "圓中二";
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
            this.btnSetEnd.Click += new System.EventHandler(this.btnSetEnd_Click);
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
            // lblBSideZ
            // 
            this.lblBSideZ.Location = new System.Drawing.Point(6, 110);
            this.lblBSideZ.Name = "lblBSideZ";
            this.lblBSideZ.Size = new System.Drawing.Size(60, 30);
            this.lblBSideZ.TabIndex = 8;
            this.lblBSideZ.Text = "Z(mm)";
            this.lblBSideZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBSideY
            // 
            this.lblBSideY.Location = new System.Drawing.Point(6, 70);
            this.lblBSideY.Name = "lblBSideY";
            this.lblBSideY.Size = new System.Drawing.Size(60, 30);
            this.lblBSideY.TabIndex = 9;
            this.lblBSideY.Text = "Y(mm)";
            this.lblBSideY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBSideX
            // 
            this.lblBSideX.Location = new System.Drawing.Point(6, 30);
            this.lblBSideX.Name = "lblBSideX";
            this.lblBSideX.Size = new System.Drawing.Size(60, 30);
            this.lblBSideX.TabIndex = 10;
            this.lblBSideX.Text = "X(mm)";
            this.lblBSideX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.btnGoEnd.Click += new System.EventHandler(this.btnGoEnd_Click);
            // 
            // grpArcMiddle
            // 
            this.grpArcMiddle.Controls.Add(this.btnSetMiddle);
            this.grpArcMiddle.Controls.Add(this.nmuMiddleZ);
            this.grpArcMiddle.Controls.Add(this.nmuMiddleY);
            this.grpArcMiddle.Controls.Add(this.nmuMiddleX);
            this.grpArcMiddle.Controls.Add(this.lblASideZ);
            this.grpArcMiddle.Controls.Add(this.lblASideY);
            this.grpArcMiddle.Controls.Add(this.lblASideX);
            this.grpArcMiddle.Controls.Add(this.btnGoMiddle);
            this.grpArcMiddle.Location = new System.Drawing.Point(250, 30);
            this.grpArcMiddle.Name = "grpArcMiddle";
            this.grpArcMiddle.Size = new System.Drawing.Size(230, 160);
            this.grpArcMiddle.TabIndex = 5;
            this.grpArcMiddle.TabStop = false;
            this.grpArcMiddle.Text = "圓中一";
            // 
            // btnSetMiddle
            // 
            this.btnSetMiddle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSetMiddle.BackgroundImage")));
            this.btnSetMiddle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSetMiddle.Location = new System.Drawing.Point(168, 91);
            this.btnSetMiddle.Name = "btnSetMiddle";
            this.btnSetMiddle.Size = new System.Drawing.Size(50, 50);
            this.btnSetMiddle.TabIndex = 14;
            this.toolTip1.SetToolTip(this.btnSetMiddle, "Set");
            this.btnSetMiddle.UseVisualStyleBackColor = true;
            this.btnSetMiddle.Click += new System.EventHandler(this.btnSetMiddle_Click);
            // 
            // nmuMiddleZ
            // 
            this.nmuMiddleZ.DecimalPlaces = 3;
            this.nmuMiddleZ.Location = new System.Drawing.Point(74, 112);
            this.nmuMiddleZ.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuMiddleZ.Name = "nmuMiddleZ";
            this.nmuMiddleZ.Size = new System.Drawing.Size(88, 29);
            this.nmuMiddleZ.TabIndex = 13;
            this.nmuMiddleZ.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuMiddleZ.ValueChanged += new System.EventHandler(this.nmuMiddleZ_ValueChanged);
            // 
            // nmuMiddleY
            // 
            this.nmuMiddleY.DecimalPlaces = 3;
            this.nmuMiddleY.Location = new System.Drawing.Point(74, 72);
            this.nmuMiddleY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuMiddleY.Name = "nmuMiddleY";
            this.nmuMiddleY.Size = new System.Drawing.Size(88, 29);
            this.nmuMiddleY.TabIndex = 12;
            this.nmuMiddleY.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuMiddleY.ValueChanged += new System.EventHandler(this.nmuMiddleY_ValueChanged);
            // 
            // nmuMiddleX
            // 
            this.nmuMiddleX.DecimalPlaces = 3;
            this.nmuMiddleX.Location = new System.Drawing.Point(74, 32);
            this.nmuMiddleX.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuMiddleX.Name = "nmuMiddleX";
            this.nmuMiddleX.Size = new System.Drawing.Size(88, 29);
            this.nmuMiddleX.TabIndex = 11;
            this.nmuMiddleX.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuMiddleX.ValueChanged += new System.EventHandler(this.nmuMiddleX_ValueChanged);
            // 
            // lblASideZ
            // 
            this.lblASideZ.Location = new System.Drawing.Point(6, 110);
            this.lblASideZ.Name = "lblASideZ";
            this.lblASideZ.Size = new System.Drawing.Size(60, 30);
            this.lblASideZ.TabIndex = 8;
            this.lblASideZ.Text = "Z(mm)";
            this.lblASideZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblASideY
            // 
            this.lblASideY.Location = new System.Drawing.Point(6, 70);
            this.lblASideY.Name = "lblASideY";
            this.lblASideY.Size = new System.Drawing.Size(60, 30);
            this.lblASideY.TabIndex = 9;
            this.lblASideY.Text = "Y(mm)";
            this.lblASideY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblASideX
            // 
            this.lblASideX.Location = new System.Drawing.Point(6, 30);
            this.lblASideX.Name = "lblASideX";
            this.lblASideX.Size = new System.Drawing.Size(60, 30);
            this.lblASideX.TabIndex = 10;
            this.lblASideX.Text = "X(mm)";
            this.lblASideX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnGoMiddle
            // 
            this.btnGoMiddle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGoMiddle.BackgroundImage")));
            this.btnGoMiddle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGoMiddle.Location = new System.Drawing.Point(168, 28);
            this.btnGoMiddle.Name = "btnGoMiddle";
            this.btnGoMiddle.Size = new System.Drawing.Size(50, 50);
            this.btnGoMiddle.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnGoMiddle, "Go");
            this.btnGoMiddle.UseVisualStyleBackColor = true;
            this.btnGoMiddle.Click += new System.EventHandler(this.btnGoMiddle_Click);
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
            this.grpStart.TabIndex = 4;
            this.grpStart.TabStop = false;
            this.grpStart.Text = "圓起點";
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
            this.btnSetStart.Click += new System.EventHandler(this.btnSetStart_Click);
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
            this.btnGoStart.Click += new System.EventHandler(this.btnGoStart_Click);
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
            // ucRecipeCircle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.grpStep);
            this.Name = "ucRecipeCircle";
            this.Size = new System.Drawing.Size(990, 600);
            this.grpStep.ResumeLayout(false);
            this.grpStep.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuDotCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuVelocity)).EndInit();
            this.grpPos.ResumeLayout(false);
            this.grpArcCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmuCenterZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuCenterY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuCenterX)).EndInit();
            this.grpArcEnd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmuEndZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuEndY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuEndX)).EndInit();
            this.grpArcMiddle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmuMiddleZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuMiddleY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuMiddleX)).EndInit();
            this.grpStart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDirection;
        private System.Windows.Forms.Label lblDirection;
        private System.Windows.Forms.ComboBox cmbMethod;
        private System.Windows.Forms.Label lblMethod;
        private System.Windows.Forms.GroupBox grpStep;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.GroupBox grpPos;
        private System.Windows.Forms.GroupBox grpArcCenter;
        private System.Windows.Forms.Button btnSetCenter;
        private System.Windows.Forms.NumericUpDown nmuCenterZ;
        private System.Windows.Forms.NumericUpDown nmuCenterY;
        private System.Windows.Forms.NumericUpDown nmuCenterX;
        private System.Windows.Forms.Label lblCenterZ;
        private System.Windows.Forms.Label lblCenterY;
        private System.Windows.Forms.Label lblCenterX;
        private System.Windows.Forms.Button btnGoCenter;
        private System.Windows.Forms.GroupBox grpArcEnd;
        private System.Windows.Forms.Button btnSetEnd;
        private System.Windows.Forms.NumericUpDown nmuEndZ;
        private System.Windows.Forms.NumericUpDown nmuEndY;
        private System.Windows.Forms.NumericUpDown nmuEndX;
        private System.Windows.Forms.Label lblBSideZ;
        private System.Windows.Forms.Label lblBSideY;
        private System.Windows.Forms.Label lblBSideX;
        private System.Windows.Forms.Button btnGoEnd;
        private System.Windows.Forms.GroupBox grpArcMiddle;
        private System.Windows.Forms.Button btnSetMiddle;
        private System.Windows.Forms.NumericUpDown nmuMiddleZ;
        private System.Windows.Forms.NumericUpDown nmuMiddleY;
        private System.Windows.Forms.NumericUpDown nmuMiddleX;
        private System.Windows.Forms.Label lblASideZ;
        private System.Windows.Forms.Label lblASideY;
        private System.Windows.Forms.Label lblASideX;
        private System.Windows.Forms.Button btnGoMiddle;
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
