namespace Premtek
{
    partial class ucRecipeDot
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
            this.grpStep = new System.Windows.Forms.GroupBox();
            this.txtAvgWeight = new System.Windows.Forms.TextBox();
            this.lblAvgWeight = new System.Windows.Forms.Label();
            this.nmuWeight = new System.Windows.Forms.NumericUpDown();
            this.lblWeight = new System.Windows.Forms.Label();
            this.nmuDotCount = new System.Windows.Forms.NumericUpDown();
            this.lblDotCount = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.grpPos = new System.Windows.Forms.GroupBox();
            this.grpStart = new System.Windows.Forms.GroupBox();
            this.btnSetPos = new System.Windows.Forms.Button();
            this.nmuPosZ = new System.Windows.Forms.NumericUpDown();
            this.nmuPosY = new System.Windows.Forms.NumericUpDown();
            this.nmuPosX = new System.Windows.Forms.NumericUpDown();
            this.lblStartZ = new System.Windows.Forms.Label();
            this.lblStartY = new System.Windows.Forms.Label();
            this.lblStartX = new System.Windows.Forms.Label();
            this.btnGoPos = new System.Windows.Forms.Button();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grpStep.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuDotCount)).BeginInit();
            this.grpPos.SuspendLayout();
            this.grpStart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuPosZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuPosX)).BeginInit();
            this.SuspendLayout();
            // 
            // grpStep
            // 
            this.grpStep.Controls.Add(this.txtAvgWeight);
            this.grpStep.Controls.Add(this.lblAvgWeight);
            this.grpStep.Controls.Add(this.nmuWeight);
            this.grpStep.Controls.Add(this.lblWeight);
            this.grpStep.Controls.Add(this.nmuDotCount);
            this.grpStep.Controls.Add(this.lblDotCount);
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
            this.grpStep.TabIndex = 2;
            this.grpStep.TabStop = false;
            this.grpStep.Text = "點";
            // 
            // txtAvgWeight
            // 
            this.txtAvgWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAvgWeight.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtAvgWeight.Location = new System.Drawing.Point(656, 180);
            this.txtAvgWeight.Name = "txtAvgWeight";
            this.txtAvgWeight.Size = new System.Drawing.Size(94, 33);
            this.txtAvgWeight.TabIndex = 36;
            // 
            // lblAvgWeight
            // 
            this.lblAvgWeight.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblAvgWeight.Location = new System.Drawing.Point(513, 181);
            this.lblAvgWeight.Name = "lblAvgWeight";
            this.lblAvgWeight.Size = new System.Drawing.Size(140, 30);
            this.lblAvgWeight.TabIndex = 35;
            this.lblAvgWeight.Text = "單點均重(mg)";
            this.lblAvgWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmuWeight
            // 
            this.nmuWeight.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.nmuWeight.Location = new System.Drawing.Point(656, 130);
            this.nmuWeight.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nmuWeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nmuWeight.Name = "nmuWeight";
            this.nmuWeight.Size = new System.Drawing.Size(94, 33);
            this.nmuWeight.TabIndex = 34;
            this.nmuWeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nmuWeight.ValueChanged += new System.EventHandler(this.nmuWeight_ValueChanged);
            // 
            // lblWeight
            // 
            this.lblWeight.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblWeight.Location = new System.Drawing.Point(513, 130);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(140, 30);
            this.lblWeight.TabIndex = 33;
            this.lblWeight.Text = "膠重(mg)";
            this.lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmuDotCount
            // 
            this.nmuDotCount.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.nmuDotCount.Location = new System.Drawing.Point(656, 80);
            this.nmuDotCount.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nmuDotCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmuDotCount.Name = "nmuDotCount";
            this.nmuDotCount.Size = new System.Drawing.Size(94, 33);
            this.nmuDotCount.TabIndex = 32;
            this.nmuDotCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmuDotCount.ValueChanged += new System.EventHandler(this.nmuDotCount_ValueChanged);
            // 
            // lblDotCount
            // 
            this.lblDotCount.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblDotCount.Location = new System.Drawing.Point(553, 80);
            this.lblDotCount.Name = "lblDotCount";
            this.lblDotCount.Size = new System.Drawing.Size(100, 30);
            this.lblDotCount.TabIndex = 31;
            this.lblDotCount.Text = "打點數";
            this.lblDotCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.grpPos.Controls.Add(this.grpStart);
            this.grpPos.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpPos.Location = new System.Drawing.Point(6, 80);
            this.grpPos.Name = "grpPos";
            this.grpPos.Size = new System.Drawing.Size(500, 500);
            this.grpPos.TabIndex = 2;
            this.grpPos.TabStop = false;
            this.grpPos.Text = "位置";
            // 
            // grpStart
            // 
            this.grpStart.Controls.Add(this.btnSetPos);
            this.grpStart.Controls.Add(this.nmuPosZ);
            this.grpStart.Controls.Add(this.nmuPosY);
            this.grpStart.Controls.Add(this.nmuPosX);
            this.grpStart.Controls.Add(this.lblStartZ);
            this.grpStart.Controls.Add(this.lblStartY);
            this.grpStart.Controls.Add(this.lblStartX);
            this.grpStart.Controls.Add(this.btnGoPos);
            this.grpStart.Location = new System.Drawing.Point(6, 30);
            this.grpStart.Name = "grpStart";
            this.grpStart.Size = new System.Drawing.Size(230, 160);
            this.grpStart.TabIndex = 5;
            this.grpStart.TabStop = false;
            this.grpStart.Text = "目標點";
            // 
            // btnSetPos
            // 
            this.btnSetPos.BackgroundImage = global::Premtek.Properties.Resources.setup;
            this.btnSetPos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSetPos.Location = new System.Drawing.Point(168, 91);
            this.btnSetPos.Name = "btnSetPos";
            this.btnSetPos.Size = new System.Drawing.Size(50, 50);
            this.btnSetPos.TabIndex = 14;
            this.toolTip1.SetToolTip(this.btnSetPos, "Set");
            this.btnSetPos.UseVisualStyleBackColor = true;
            this.btnSetPos.Click += new System.EventHandler(this.btnSetPos_Click);
            // 
            // nmuPosZ
            // 
            this.nmuPosZ.DecimalPlaces = 3;
            this.nmuPosZ.Location = new System.Drawing.Point(74, 112);
            this.nmuPosZ.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuPosZ.Name = "nmuPosZ";
            this.nmuPosZ.Size = new System.Drawing.Size(88, 29);
            this.nmuPosZ.TabIndex = 13;
            this.nmuPosZ.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuPosZ.ValueChanged += new System.EventHandler(this.nmuPosZ_ValueChanged);
            // 
            // nmuPosY
            // 
            this.nmuPosY.DecimalPlaces = 3;
            this.nmuPosY.Location = new System.Drawing.Point(74, 72);
            this.nmuPosY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuPosY.Name = "nmuPosY";
            this.nmuPosY.Size = new System.Drawing.Size(88, 29);
            this.nmuPosY.TabIndex = 12;
            this.nmuPosY.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuPosY.ValueChanged += new System.EventHandler(this.nmuPosY_ValueChanged);
            // 
            // nmuPosX
            // 
            this.nmuPosX.DecimalPlaces = 3;
            this.nmuPosX.Location = new System.Drawing.Point(74, 32);
            this.nmuPosX.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuPosX.Name = "nmuPosX";
            this.nmuPosX.Size = new System.Drawing.Size(88, 29);
            this.nmuPosX.TabIndex = 11;
            this.nmuPosX.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuPosX.ValueChanged += new System.EventHandler(this.nmuPosX_ValueChanged);
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
            // btnGoPos
            // 
            this.btnGoPos.BackgroundImage = global::Premtek.Properties.Resources.goPos;
            this.btnGoPos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGoPos.Location = new System.Drawing.Point(168, 28);
            this.btnGoPos.Name = "btnGoPos";
            this.btnGoPos.Size = new System.Drawing.Size(50, 50);
            this.btnGoPos.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnGoPos, "Go");
            this.btnGoPos.UseVisualStyleBackColor = true;
            this.btnGoPos.Click += new System.EventHandler(this.btnGoPos_Click);
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
            // ucRecipeDot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.grpStep);
            this.Name = "ucRecipeDot";
            this.Size = new System.Drawing.Size(990, 600);
            this.grpStep.ResumeLayout(false);
            this.grpStep.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuDotCount)).EndInit();
            this.grpPos.ResumeLayout(false);
            this.grpStart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmuPosZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuPosX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpStep;
        private System.Windows.Forms.GroupBox grpPos;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.GroupBox grpStart;
        private System.Windows.Forms.Button btnSetPos;
        private System.Windows.Forms.NumericUpDown nmuPosZ;
        private System.Windows.Forms.NumericUpDown nmuPosY;
        private System.Windows.Forms.NumericUpDown nmuPosX;
        private System.Windows.Forms.Label lblStartZ;
        private System.Windows.Forms.Label lblStartY;
        private System.Windows.Forms.Label lblStartX;
        private System.Windows.Forms.Button btnGoPos;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.TextBox txtAvgWeight;
        private System.Windows.Forms.Label lblAvgWeight;
        private System.Windows.Forms.NumericUpDown nmuWeight;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.NumericUpDown nmuDotCount;
        private System.Windows.Forms.Label lblDotCount;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
