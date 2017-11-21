namespace Premtek
{
    partial class ucRecipePattern
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
            this.cmbPattern = new System.Windows.Forms.ComboBox();
            this.lblGroup = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.grpPos = new System.Windows.Forms.GroupBox();
            this.grpStart = new System.Windows.Forms.GroupBox();
            this.nmuPosZ = new System.Windows.Forms.NumericUpDown();
            this.nmuPosY = new System.Windows.Forms.NumericUpDown();
            this.nmuPosX = new System.Windows.Forms.NumericUpDown();
            this.lblStartZ = new System.Windows.Forms.Label();
            this.lblStartY = new System.Windows.Forms.Label();
            this.lblStartX = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnSetStart = new System.Windows.Forms.Button();
            this.btnGoStart = new System.Windows.Forms.Button();
            this.grpStep.SuspendLayout();
            this.grpPos.SuspendLayout();
            this.grpStart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuPosZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuPosX)).BeginInit();
            this.SuspendLayout();
            // 
            // grpStep
            // 
            this.grpStep.Controls.Add(this.cmbPattern);
            this.grpStep.Controls.Add(this.lblGroup);
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
            this.grpStep.Text = "呼叫副程式";
            // 
            // cmbPattern
            // 
            this.cmbPattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPattern.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbPattern.FormattingEnabled = true;
            this.cmbPattern.Items.AddRange(new object[] {
            "Default"});
            this.cmbPattern.Location = new System.Drawing.Point(656, 80);
            this.cmbPattern.Name = "cmbPattern";
            this.cmbPattern.Size = new System.Drawing.Size(94, 32);
            this.cmbPattern.TabIndex = 7;
            this.cmbPattern.SelectedIndexChanged += new System.EventHandler(this.cmbGroup_SelectedIndexChanged);
            // 
            // lblGroup
            // 
            this.lblGroup.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblGroup.Location = new System.Drawing.Point(553, 80);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(100, 30);
            this.lblGroup.TabIndex = 6;
            this.lblGroup.Text = "副程式";
            this.lblGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(755, 26);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(40, 40);
            this.btnEdit.TabIndex = 5;
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
            this.cmbType.TabIndex = 4;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // lblType
            // 
            this.lblType.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblType.Location = new System.Drawing.Point(553, 30);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(100, 30);
            this.lblType.TabIndex = 3;
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
            this.grpStart.Controls.Add(this.btnSetStart);
            this.grpStart.Controls.Add(this.nmuPosZ);
            this.grpStart.Controls.Add(this.nmuPosY);
            this.grpStart.Controls.Add(this.nmuPosX);
            this.grpStart.Controls.Add(this.lblStartZ);
            this.grpStart.Controls.Add(this.lblStartY);
            this.grpStart.Controls.Add(this.lblStartX);
            this.grpStart.Controls.Add(this.btnGoStart);
            this.grpStart.Location = new System.Drawing.Point(6, 30);
            this.grpStart.Name = "grpStart";
            this.grpStart.Size = new System.Drawing.Size(230, 160);
            this.grpStart.TabIndex = 1;
            this.grpStart.TabStop = false;
            this.grpStart.Text = "原點";
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
            // txtRemark
            // 
            this.txtRemark.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtRemark.Location = new System.Drawing.Point(110, 30);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(382, 33);
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
            // btnSetStart
            // 
            this.btnSetStart.BackgroundImage = global::Premtek.Properties.Resources.setup;
            this.btnSetStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSetStart.Location = new System.Drawing.Point(168, 91);
            this.btnSetStart.Name = "btnSetStart";
            this.btnSetStart.Size = new System.Drawing.Size(50, 50);
            this.btnSetStart.TabIndex = 14;
            this.toolTip1.SetToolTip(this.btnSetStart, "Set");
            this.btnSetStart.UseVisualStyleBackColor = true;
            this.btnSetStart.Click += new System.EventHandler(this.btnSetStart_Click);
            // 
            // btnGoStart
            // 
            this.btnGoStart.BackgroundImage = global::Premtek.Properties.Resources.goPos;
            this.btnGoStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGoStart.Location = new System.Drawing.Point(168, 28);
            this.btnGoStart.Name = "btnGoStart";
            this.btnGoStart.Size = new System.Drawing.Size(50, 50);
            this.btnGoStart.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnGoStart, "Go");
            this.btnGoStart.UseVisualStyleBackColor = true;
            this.btnGoStart.Click += new System.EventHandler(this.btnGoStart_Click);
            // 
            // ucRecipeAlign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.grpStep);
            this.Name = "ucRecipeAlign";
            this.Size = new System.Drawing.Size(990, 600);
            this.grpStep.ResumeLayout(false);
            this.grpStep.PerformLayout();
            this.grpPos.ResumeLayout(false);
            this.grpStart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmuPosZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuPosX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpStep;
        private System.Windows.Forms.ComboBox cmbPattern;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.GroupBox grpPos;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.GroupBox grpStart;
        private System.Windows.Forms.Button btnSetStart;
        private System.Windows.Forms.NumericUpDown nmuPosZ;
        private System.Windows.Forms.NumericUpDown nmuPosY;
        private System.Windows.Forms.NumericUpDown nmuPosX;
        private System.Windows.Forms.Label lblStartZ;
        private System.Windows.Forms.Label lblStartY;
        private System.Windows.Forms.Label lblStartX;
        private System.Windows.Forms.Button btnGoStart;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
