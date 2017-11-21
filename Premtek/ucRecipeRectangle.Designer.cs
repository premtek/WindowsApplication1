namespace Premtek
{
    partial class ucRecipeRectangle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucRecipeRectangle));
            this.txtRemark = new System.Windows.Forms.TextBox();
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
            this.lblRemark = new System.Windows.Forms.Label();
            this.grpStep = new System.Windows.Forms.GroupBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.nmuAngle = new System.Windows.Forms.NumericUpDown();
            this.lblAngle = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grpPos.SuspendLayout();
            this.grpArcEnd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuEndZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuEndY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuEndX)).BeginInit();
            this.grpStart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartX)).BeginInit();
            this.grpStep.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuAngle)).BeginInit();
            this.SuspendLayout();
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
            this.grpArcEnd.TabIndex = 10;
            this.grpArcEnd.TabStop = false;
            this.grpArcEnd.Text = "結束點";
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
            this.btnGoEnd.Click += new System.EventHandler(this.btnGoEnd_Click);
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
            this.grpStart.TabIndex = 9;
            this.grpStart.TabStop = false;
            this.grpStart.Text = "開始點";
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
            // grpStep
            // 
            this.grpStep.Controls.Add(this.btnEdit);
            this.grpStep.Controls.Add(this.cmbType);
            this.grpStep.Controls.Add(this.lblType);
            this.grpStep.Controls.Add(this.nmuAngle);
            this.grpStep.Controls.Add(this.lblAngle);
            this.grpStep.Controls.Add(this.grpPos);
            this.grpStep.Controls.Add(this.txtRemark);
            this.grpStep.Controls.Add(this.lblRemark);
            this.grpStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpStep.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpStep.Location = new System.Drawing.Point(0, 0);
            this.grpStep.Name = "grpStep";
            this.grpStep.Size = new System.Drawing.Size(990, 600);
            this.grpStep.TabIndex = 3;
            this.grpStep.TabStop = false;
            this.grpStep.Text = "矩形";
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(755, 26);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(40, 40);
            this.btnEdit.TabIndex = 13;
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
            this.cmbType.TabIndex = 12;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // lblType
            // 
            this.lblType.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblType.Location = new System.Drawing.Point(553, 30);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(100, 30);
            this.lblType.TabIndex = 11;
            this.lblType.Text = "型態";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmuAngle
            // 
            this.nmuAngle.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.nmuAngle.Location = new System.Drawing.Point(656, 80);
            this.nmuAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nmuAngle.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.nmuAngle.Name = "nmuAngle";
            this.nmuAngle.Size = new System.Drawing.Size(94, 33);
            this.nmuAngle.TabIndex = 10;
            this.nmuAngle.ValueChanged += new System.EventHandler(this.nmuAngle_ValueChanged);
            // 
            // lblAngle
            // 
            this.lblAngle.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblAngle.Location = new System.Drawing.Point(553, 80);
            this.lblAngle.Name = "lblAngle";
            this.lblAngle.Size = new System.Drawing.Size(100, 30);
            this.lblAngle.TabIndex = 9;
            this.lblAngle.Text = "水平角度";
            this.lblAngle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ucRecipeRectangle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.grpStep);
            this.Name = "ucRecipeRectangle";
            this.Size = new System.Drawing.Size(990, 600);
            this.grpPos.ResumeLayout(false);
            this.grpArcEnd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmuEndZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuEndY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuEndX)).EndInit();
            this.grpStart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartX)).EndInit();
            this.grpStep.ResumeLayout(false);
            this.grpStep.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuAngle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.GroupBox grpPos;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.GroupBox grpStep;
        private System.Windows.Forms.NumericUpDown nmuAngle;
        private System.Windows.Forms.Label lblAngle;
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
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
