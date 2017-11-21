namespace Premtek
{
    partial class ucRecipeAlign
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.nmuStartZ = new System.Windows.Forms.NumericUpDown();
            this.nmuStartY = new System.Windows.Forms.NumericUpDown();
            this.nmuStartX = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGoStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.grpStep = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.grpPos = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartX)).BeginInit();
            this.grpStep.SuspendLayout();
            this.grpPos.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nmuStartZ);
            this.panel1.Controls.Add(this.nmuStartY);
            this.panel1.Controls.Add(this.nmuStartX);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnGoStart);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(7, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(433, 75);
            this.panel1.TabIndex = 0;
            // 
            // nmuStartZ
            // 
            this.nmuStartZ.DecimalPlaces = 3;
            this.nmuStartZ.Location = new System.Drawing.Point(316, 31);
            this.nmuStartZ.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuStartZ.Name = "nmuStartZ";
            this.nmuStartZ.Size = new System.Drawing.Size(88, 29);
            this.nmuStartZ.TabIndex = 5;
            this.nmuStartZ.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            // 
            // nmuStartY
            // 
            this.nmuStartY.DecimalPlaces = 3;
            this.nmuStartY.Location = new System.Drawing.Point(197, 31);
            this.nmuStartY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuStartY.Name = "nmuStartY";
            this.nmuStartY.Size = new System.Drawing.Size(88, 29);
            this.nmuStartY.TabIndex = 4;
            this.nmuStartY.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            // 
            // nmuStartX
            // 
            this.nmuStartX.DecimalPlaces = 3;
            this.nmuStartX.Location = new System.Drawing.Point(78, 31);
            this.nmuStartX.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nmuStartX.Name = "nmuStartX";
            this.nmuStartX.Size = new System.Drawing.Size(88, 29);
            this.nmuStartX.TabIndex = 3;
            this.nmuStartX.Value = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(291, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "Z";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(172, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "X";
            // 
            // btnGoStart
            // 
            this.btnGoStart.Location = new System.Drawing.Point(7, 23);
            this.btnGoStart.Name = "btnGoStart";
            this.btnGoStart.Size = new System.Drawing.Size(40, 40);
            this.btnGoStart.TabIndex = 1;
            this.btnGoStart.Text = "Go";
            this.btnGoStart.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "原點";
            // 
            // grpStep
            // 
            this.grpStep.Controls.Add(this.comboBox1);
            this.grpStep.Controls.Add(this.label17);
            this.grpStep.Controls.Add(this.btnEdit);
            this.grpStep.Controls.Add(this.cmbType);
            this.grpStep.Controls.Add(this.lblType);
            this.grpStep.Controls.Add(this.grpPos);
            this.grpStep.Controls.Add(this.textBox1);
            this.grpStep.Controls.Add(this.lblRemark);
            this.grpStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpStep.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpStep.Location = new System.Drawing.Point(0, 0);
            this.grpStep.Name = "grpStep";
            this.grpStep.Size = new System.Drawing.Size(800, 600);
            this.grpStep.TabIndex = 2;
            this.grpStep.TabStop = false;
            this.grpStep.Text = "定位";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Default"});
            this.comboBox1.Location = new System.Drawing.Point(556, 85);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(94, 32);
            this.comboBox1.TabIndex = 7;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label17.Location = new System.Drawing.Point(480, 91);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(73, 20);
            this.label17.TabIndex = 6;
            this.label17.Text = "定位群組";
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(654, 19);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(40, 40);
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Text = "..";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "Default"});
            this.cmbType.Location = new System.Drawing.Point(556, 23);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(94, 32);
            this.cmbType.TabIndex = 4;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblType.Location = new System.Drawing.Point(509, 29);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(41, 20);
            this.lblType.TabIndex = 3;
            this.lblType.Text = "型態";
            // 
            // grpPos
            // 
            this.grpPos.Controls.Add(this.panel1);
            this.grpPos.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpPos.Location = new System.Drawing.Point(34, 63);
            this.grpPos.Name = "grpPos";
            this.grpPos.Size = new System.Drawing.Size(446, 370);
            this.grpPos.TabIndex = 2;
            this.grpPos.TabStop = false;
            this.grpPos.Text = "位置";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(77, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(300, 33);
            this.textBox1.TabIndex = 1;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblRemark.Location = new System.Drawing.Point(30, 30);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(41, 20);
            this.lblRemark.TabIndex = 0;
            this.lblRemark.Text = "備註";
            // 
            // ucRecipeAlign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpStep);
            this.Name = "ucRecipeAlign";
            this.Size = new System.Drawing.Size(800, 600);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuStartX)).EndInit();
            this.grpStep.ResumeLayout(false);
            this.grpStep.PerformLayout();
            this.grpPos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown nmuStartZ;
        private System.Windows.Forms.NumericUpDown nmuStartY;
        private System.Windows.Forms.NumericUpDown nmuStartX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGoStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpStep;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.GroupBox grpPos;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblRemark;
    }
}
