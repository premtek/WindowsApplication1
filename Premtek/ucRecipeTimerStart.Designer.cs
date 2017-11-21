namespace Premtek
{
    partial class ucRecipeTimerStart
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
            this.grpStep = new System.Windows.Forms.GroupBox();
            this.cmbTimer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.grpStep.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpStep
            // 
            this.grpStep.Controls.Add(this.cmbTimer);
            this.grpStep.Controls.Add(this.label1);
            this.grpStep.Controls.Add(this.btnEdit);
            this.grpStep.Controls.Add(this.cmbType);
            this.grpStep.Controls.Add(this.lblType);
            this.grpStep.Controls.Add(this.txtRemark);
            this.grpStep.Controls.Add(this.lblRemark);
            this.grpStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpStep.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpStep.Location = new System.Drawing.Point(0, 0);
            this.grpStep.Name = "grpStep";
            this.grpStep.Size = new System.Drawing.Size(800, 600);
            this.grpStep.TabIndex = 2;
            this.grpStep.TabStop = false;
            this.grpStep.Text = "計時器開始";
            // 
            // cmbTimer
            // 
            this.cmbTimer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimer.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbTimer.FormattingEnabled = true;
            this.cmbTimer.Items.AddRange(new object[] {
            "T0",
            "T1",
            "T2",
            "T3",
            "T4",
            "T5",
            "T6",
            "T7",
            "T8",
            "T9"});
            this.cmbTimer.Location = new System.Drawing.Point(110, 80);
            this.cmbTimer.Name = "cmbTimer";
            this.cmbTimer.Size = new System.Drawing.Size(94, 32);
            this.cmbTimer.TabIndex = 19;
            this.cmbTimer.SelectedIndexChanged += new System.EventHandler(this.cmbTimer_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(7, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 30);
            this.label1.TabIndex = 18;
            this.label1.Text = "計時器";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(755, 26);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(40, 40);
            this.btnEdit.TabIndex = 15;
            this.btnEdit.Text = "..";
            this.btnEdit.UseVisualStyleBackColor = true;
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
            this.cmbType.TabIndex = 14;
            // 
            // lblType
            // 
            this.lblType.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblType.Location = new System.Drawing.Point(553, 30);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(100, 30);
            this.lblType.TabIndex = 13;
            this.lblType.Text = "型態";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRemark
            // 
            this.txtRemark.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtRemark.Location = new System.Drawing.Point(110, 30);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(376, 33);
            this.txtRemark.TabIndex = 12;
            // 
            // lblRemark
            // 
            this.lblRemark.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblRemark.Location = new System.Drawing.Point(6, 30);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(100, 30);
            this.lblRemark.TabIndex = 11;
            this.lblRemark.Text = "備註";
            this.lblRemark.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ucRecipeTimerStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.grpStep);
            this.Name = "ucRecipeTimerStart";
            this.Size = new System.Drawing.Size(800, 600);
            this.grpStep.ResumeLayout(false);
            this.grpStep.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpStep;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.ComboBox cmbTimer;
        private System.Windows.Forms.Label label1;
    }
}
