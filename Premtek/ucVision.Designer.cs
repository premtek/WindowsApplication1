namespace Premtek
{
    partial class ucVision
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
            this.grpVision = new System.Windows.Forms.GroupBox();
            this.chkLight4 = new System.Windows.Forms.CheckBox();
            this.chkLight3 = new System.Windows.Forms.CheckBox();
            this.chkLight2 = new System.Windows.Forms.CheckBox();
            this.chkLight1 = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnTargetPlot = new System.Windows.Forms.Button();
            this.nmuLight4 = new System.Windows.Forms.NumericUpDown();
            this.nmuLight3 = new System.Windows.Forms.NumericUpDown();
            this.nmuLight2 = new System.Windows.Forms.NumericUpDown();
            this.nmuLight1 = new System.Windows.Forms.NumericUpDown();
            this.grpVision.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuLight4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuLight3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuLight2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuLight1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpVision
            // 
            this.grpVision.Controls.Add(this.chkLight4);
            this.grpVision.Controls.Add(this.chkLight3);
            this.grpVision.Controls.Add(this.chkLight2);
            this.grpVision.Controls.Add(this.chkLight1);
            this.grpVision.Controls.Add(this.btnSave);
            this.grpVision.Controls.Add(this.btnTargetPlot);
            this.grpVision.Controls.Add(this.nmuLight4);
            this.grpVision.Controls.Add(this.nmuLight3);
            this.grpVision.Controls.Add(this.nmuLight2);
            this.grpVision.Controls.Add(this.nmuLight1);
            this.grpVision.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpVision.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpVision.Location = new System.Drawing.Point(0, 0);
            this.grpVision.Name = "grpVision";
            this.grpVision.Size = new System.Drawing.Size(400, 350);
            this.grpVision.TabIndex = 0;
            this.grpVision.TabStop = false;
            this.grpVision.Text = "Vision";
            // 
            // chkLight4
            // 
            this.chkLight4.Location = new System.Drawing.Point(11, 150);
            this.chkLight4.Name = "chkLight4";
            this.chkLight4.Size = new System.Drawing.Size(80, 30);
            this.chkLight4.TabIndex = 10;
            this.chkLight4.Text = "Light4";
            this.chkLight4.UseVisualStyleBackColor = true;
            this.chkLight4.CheckedChanged += new System.EventHandler(this.chkLight4_CheckedChanged);
            // 
            // chkLight3
            // 
            this.chkLight3.Location = new System.Drawing.Point(11, 110);
            this.chkLight3.Name = "chkLight3";
            this.chkLight3.Size = new System.Drawing.Size(80, 30);
            this.chkLight3.TabIndex = 10;
            this.chkLight3.Text = "Light3";
            this.chkLight3.UseVisualStyleBackColor = true;
            this.chkLight3.CheckedChanged += new System.EventHandler(this.chkLight3_CheckedChanged);
            // 
            // chkLight2
            // 
            this.chkLight2.Location = new System.Drawing.Point(11, 70);
            this.chkLight2.Name = "chkLight2";
            this.chkLight2.Size = new System.Drawing.Size(80, 30);
            this.chkLight2.TabIndex = 10;
            this.chkLight2.Text = "Light2";
            this.chkLight2.UseVisualStyleBackColor = true;
            this.chkLight2.CheckedChanged += new System.EventHandler(this.chkLight2_CheckedChanged);
            // 
            // chkLight1
            // 
            this.chkLight1.Location = new System.Drawing.Point(11, 30);
            this.chkLight1.Name = "chkLight1";
            this.chkLight1.Size = new System.Drawing.Size(80, 30);
            this.chkLight1.TabIndex = 10;
            this.chkLight1.Text = "Light1";
            this.chkLight1.UseVisualStyleBackColor = true;
            this.chkLight1.CheckedChanged += new System.EventHandler(this.chkLight1_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(46, 226);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 30);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "SaveImage";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnTargetPlot
            // 
            this.btnTargetPlot.Location = new System.Drawing.Point(46, 186);
            this.btnTargetPlot.Name = "btnTargetPlot";
            this.btnTargetPlot.Size = new System.Drawing.Size(110, 30);
            this.btnTargetPlot.TabIndex = 8;
            this.btnTargetPlot.Text = "TargetPlot";
            this.btnTargetPlot.UseVisualStyleBackColor = true;
            // 
            // nmuLight4
            // 
            this.nmuLight4.Location = new System.Drawing.Point(97, 150);
            this.nmuLight4.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nmuLight4.Name = "nmuLight4";
            this.nmuLight4.Size = new System.Drawing.Size(59, 29);
            this.nmuLight4.TabIndex = 7;
            this.nmuLight4.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // nmuLight3
            // 
            this.nmuLight3.Location = new System.Drawing.Point(97, 110);
            this.nmuLight3.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nmuLight3.Name = "nmuLight3";
            this.nmuLight3.Size = new System.Drawing.Size(59, 29);
            this.nmuLight3.TabIndex = 5;
            this.nmuLight3.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // nmuLight2
            // 
            this.nmuLight2.Location = new System.Drawing.Point(97, 70);
            this.nmuLight2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nmuLight2.Name = "nmuLight2";
            this.nmuLight2.Size = new System.Drawing.Size(59, 29);
            this.nmuLight2.TabIndex = 3;
            this.nmuLight2.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // nmuLight1
            // 
            this.nmuLight1.Location = new System.Drawing.Point(97, 30);
            this.nmuLight1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nmuLight1.Name = "nmuLight1";
            this.nmuLight1.Size = new System.Drawing.Size(59, 29);
            this.nmuLight1.TabIndex = 1;
            this.nmuLight1.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // ucVision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.grpVision);
            this.Name = "ucVision";
            this.Size = new System.Drawing.Size(400, 350);
            this.grpVision.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nmuLight4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuLight3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuLight2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuLight1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpVision;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnTargetPlot;
        private System.Windows.Forms.NumericUpDown nmuLight4;
        private System.Windows.Forms.NumericUpDown nmuLight3;
        private System.Windows.Forms.NumericUpDown nmuLight2;
        private System.Windows.Forms.NumericUpDown nmuLight1;
        private System.Windows.Forms.CheckBox chkLight4;
        private System.Windows.Forms.CheckBox chkLight3;
        private System.Windows.Forms.CheckBox chkLight2;
        private System.Windows.Forms.CheckBox chkLight1;
    }
}
