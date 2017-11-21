namespace Premtek
{
    partial class ucRecipeFindHeight
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
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.grpStep = new System.Windows.Forms.GroupBox();
            this.btnC2S2 = new System.Windows.Forms.Button();
            this.btnC1S2 = new System.Windows.Forms.Button();
            this.btnC2S1 = new System.Windows.Forms.Button();
            this.btnC1S1 = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.grpPos = new System.Windows.Forms.GroupBox();
            this.grpStart = new System.Windows.Forms.GroupBox();
            this.btnSetPos = new System.Windows.Forms.Button();
            this.nmuPosZ = new System.Windows.Forms.NumericUpDown();
            this.nmuPosY = new System.Windows.Forms.NumericUpDown();
            this.nmuPosX = new System.Windows.Forms.NumericUpDown();
            this.lblPosZ = new System.Windows.Forms.Label();
            this.lblPosY = new System.Windows.Forms.Label();
            this.lblPosX = new System.Windows.Forms.Label();
            this.btnGoPos = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.nmuDotCount = new System.Windows.Forms.NumericUpDown();
            this.lblDotCount = new System.Windows.Forms.Label();
            this.grpStep.SuspendLayout();
            this.grpPos.SuspendLayout();
            this.grpStart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuPosZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuPosX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuDotCount)).BeginInit();
            this.SuspendLayout();
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
            // txtRemark
            // 
            this.txtRemark.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtRemark.Location = new System.Drawing.Point(110, 30);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(376, 33);
            this.txtRemark.TabIndex = 1;
            this.txtRemark.TextChanged += new System.EventHandler(this.txtRemark_TextChanged);
            // 
            // grpStep
            // 
            this.grpStep.Controls.Add(this.nmuDotCount);
            this.grpStep.Controls.Add(this.lblDotCount);
            this.grpStep.Controls.Add(this.btnC2S2);
            this.grpStep.Controls.Add(this.btnC1S2);
            this.grpStep.Controls.Add(this.btnC2S1);
            this.grpStep.Controls.Add(this.btnC1S1);
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
            this.grpStep.TabIndex = 1;
            this.grpStep.TabStop = false;
            this.grpStep.Text = "測高";
            // 
            // btnC2S2
            // 
            this.btnC2S2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnC2S2.Location = new System.Drawing.Point(564, 81);
            this.btnC2S2.Name = "btnC2S2";
            this.btnC2S2.Size = new System.Drawing.Size(180, 40);
            this.btnC2S2.TabIndex = 26;
            this.btnC2S2.Text = "Conveyor2 Stage2";
            this.btnC2S2.UseVisualStyleBackColor = true;
            this.btnC2S2.Click += new System.EventHandler(this.btnC2S2_Click);
            // 
            // btnC1S2
            // 
            this.btnC1S2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnC1S2.Location = new System.Drawing.Point(379, 80);
            this.btnC1S2.Name = "btnC1S2";
            this.btnC1S2.Size = new System.Drawing.Size(180, 40);
            this.btnC1S2.TabIndex = 24;
            this.btnC1S2.Text = "Conveyor1 Stage2";
            this.btnC1S2.UseVisualStyleBackColor = true;
            this.btnC1S2.Click += new System.EventHandler(this.btnC1S2_Click);
            // 
            // btnC2S1
            // 
            this.btnC2S1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnC2S1.Location = new System.Drawing.Point(194, 80);
            this.btnC2S1.Name = "btnC2S1";
            this.btnC2S1.Size = new System.Drawing.Size(180, 40);
            this.btnC2S1.TabIndex = 25;
            this.btnC2S1.Text = "Conveyor2 Stage1";
            this.btnC2S1.UseVisualStyleBackColor = true;
            this.btnC2S1.Click += new System.EventHandler(this.btnC2S1_Click);
            // 
            // btnC1S1
            // 
            this.btnC1S1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnC1S1.Location = new System.Drawing.Point(9, 80);
            this.btnC1S1.Name = "btnC1S1";
            this.btnC1S1.Size = new System.Drawing.Size(180, 40);
            this.btnC1S1.TabIndex = 23;
            this.btnC1S1.Text = "Conveyor1 Stage1";
            this.btnC1S1.UseVisualStyleBackColor = true;
            this.btnC1S1.Click += new System.EventHandler(this.btnC1S1_Click);
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
            this.grpPos.Location = new System.Drawing.Point(6, 130);
            this.grpPos.Name = "grpPos";
            this.grpPos.Size = new System.Drawing.Size(480, 378);
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
            this.grpStart.Controls.Add(this.lblPosZ);
            this.grpStart.Controls.Add(this.lblPosY);
            this.grpStart.Controls.Add(this.lblPosX);
            this.grpStart.Controls.Add(this.btnGoPos);
            this.grpStart.Location = new System.Drawing.Point(6, 30);
            this.grpStart.Name = "grpStart";
            this.grpStart.Size = new System.Drawing.Size(230, 160);
            this.grpStart.TabIndex = 6;
            this.grpStart.TabStop = false;
            this.grpStart.Text = "測高點";
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
            // lblPosZ
            // 
            this.lblPosZ.Location = new System.Drawing.Point(6, 110);
            this.lblPosZ.Name = "lblPosZ";
            this.lblPosZ.Size = new System.Drawing.Size(60, 30);
            this.lblPosZ.TabIndex = 8;
            this.lblPosZ.Text = "Z(mm)";
            this.lblPosZ.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPosY
            // 
            this.lblPosY.Location = new System.Drawing.Point(6, 70);
            this.lblPosY.Name = "lblPosY";
            this.lblPosY.Size = new System.Drawing.Size(60, 30);
            this.lblPosY.TabIndex = 9;
            this.lblPosY.Text = "Y(mm)";
            this.lblPosY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPosX
            // 
            this.lblPosX.Location = new System.Drawing.Point(6, 30);
            this.lblPosX.Name = "lblPosX";
            this.lblPosX.Size = new System.Drawing.Size(60, 30);
            this.lblPosX.TabIndex = 10;
            this.lblPosX.Text = "X(mm)";
            this.lblPosX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // nmuDotCount
            // 
            this.nmuDotCount.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.nmuDotCount.Location = new System.Drawing.Point(701, 130);
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
            this.nmuDotCount.TabIndex = 34;
            this.nmuDotCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblDotCount
            // 
            this.lblDotCount.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblDotCount.Location = new System.Drawing.Point(598, 130);
            this.lblDotCount.Name = "lblDotCount";
            this.lblDotCount.Size = new System.Drawing.Size(100, 30);
            this.lblDotCount.TabIndex = 33;
            this.lblDotCount.Text = "定點量測數";
            this.lblDotCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ucRecipeFindHeight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.grpStep);
            this.Name = "ucRecipeFindHeight";
            this.Size = new System.Drawing.Size(990, 600);
            this.grpStep.ResumeLayout(false);
            this.grpStep.PerformLayout();
            this.grpPos.ResumeLayout(false);
            this.grpStart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmuPosZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuPosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuDotCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.GroupBox grpStep;
        private System.Windows.Forms.GroupBox grpPos;
        private System.Windows.Forms.GroupBox grpStart;
        private System.Windows.Forms.Button btnSetPos;
        private System.Windows.Forms.NumericUpDown nmuPosZ;
        private System.Windows.Forms.NumericUpDown nmuPosY;
        private System.Windows.Forms.NumericUpDown nmuPosX;
        private System.Windows.Forms.Label lblPosZ;
        private System.Windows.Forms.Label lblPosY;
        private System.Windows.Forms.Label lblPosX;
        private System.Windows.Forms.Button btnGoPos;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnC2S2;
        private System.Windows.Forms.Button btnC1S2;
        private System.Windows.Forms.Button btnC2S1;
        private System.Windows.Forms.Button btnC1S1;
        private System.Windows.Forms.NumericUpDown nmuDotCount;
        private System.Windows.Forms.Label lblDotCount;
    }
}
