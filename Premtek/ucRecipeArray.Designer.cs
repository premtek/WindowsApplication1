namespace Premtek
{
    partial class ucRecipeArray
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
            this.cmbRoute = new System.Windows.Forms.ComboBox();
            this.lblRoute = new System.Windows.Forms.Label();
            this.grpStep = new System.Windows.Forms.GroupBox();
            this.cmbMAP = new System.Windows.Forms.ComboBox();
            this.lblMAP = new System.Windows.Forms.Label();
            this.cmbPattern = new System.Windows.Forms.ComboBox();
            this.lblPattern = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.grpStep.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbRoute
            // 
            this.cmbRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoute.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbRoute.FormattingEnabled = true;
            this.cmbRoute.Items.AddRange(new object[] {
            "Default"});
            this.cmbRoute.Location = new System.Drawing.Point(110, 120);
            this.cmbRoute.Name = "cmbRoute";
            this.cmbRoute.Size = new System.Drawing.Size(94, 32);
            this.cmbRoute.TabIndex = 7;
            this.cmbRoute.SelectedIndexChanged += new System.EventHandler(this.cmbRoute_SelectedIndexChanged);
            // 
            // lblRoute
            // 
            this.lblRoute.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblRoute.Location = new System.Drawing.Point(10, 120);
            this.lblRoute.Name = "lblRoute";
            this.lblRoute.Size = new System.Drawing.Size(100, 30);
            this.lblRoute.TabIndex = 6;
            this.lblRoute.Text = "點膠路線";
            this.lblRoute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpStep
            // 
            this.grpStep.Controls.Add(this.btnPrev);
            this.grpStep.Controls.Add(this.btnNext);
            this.grpStep.Controls.Add(this.cmbMAP);
            this.grpStep.Controls.Add(this.lblMAP);
            this.grpStep.Controls.Add(this.cmbPattern);
            this.grpStep.Controls.Add(this.lblPattern);
            this.grpStep.Controls.Add(this.btnEdit);
            this.grpStep.Controls.Add(this.cmbType);
            this.grpStep.Controls.Add(this.lblType);
            this.grpStep.Controls.Add(this.txtRemark);
            this.grpStep.Controls.Add(this.lblRemark);
            this.grpStep.Controls.Add(this.cmbRoute);
            this.grpStep.Controls.Add(this.lblRoute);
            this.grpStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpStep.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpStep.Location = new System.Drawing.Point(0, 0);
            this.grpStep.Name = "grpStep";
            this.grpStep.Size = new System.Drawing.Size(970, 600);
            this.grpStep.TabIndex = 3;
            this.grpStep.TabStop = false;
            this.grpStep.Text = "陣列";
            // 
            // cmbMAP
            // 
            this.cmbMAP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMAP.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbMAP.FormattingEnabled = true;
            this.cmbMAP.Items.AddRange(new object[] {
            "None"});
            this.cmbMAP.Location = new System.Drawing.Point(110, 78);
            this.cmbMAP.Name = "cmbMAP";
            this.cmbMAP.Size = new System.Drawing.Size(94, 32);
            this.cmbMAP.TabIndex = 18;
            this.cmbMAP.SelectedIndexChanged += new System.EventHandler(this.cmbMAP_SelectedIndexChanged);
            // 
            // lblMAP
            // 
            this.lblMAP.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblMAP.Location = new System.Drawing.Point(10, 79);
            this.lblMAP.Name = "lblMAP";
            this.lblMAP.Size = new System.Drawing.Size(100, 30);
            this.lblMAP.TabIndex = 17;
            this.lblMAP.Text = "MAP";
            this.lblMAP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPattern
            // 
            this.cmbPattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPattern.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbPattern.FormattingEnabled = true;
            this.cmbPattern.Items.AddRange(new object[] {
            "Default"});
            this.cmbPattern.Location = new System.Drawing.Point(110, 160);
            this.cmbPattern.Name = "cmbPattern";
            this.cmbPattern.Size = new System.Drawing.Size(94, 32);
            this.cmbPattern.TabIndex = 16;
            this.cmbPattern.SelectedIndexChanged += new System.EventHandler(this.cmbPattern_SelectedIndexChanged);
            // 
            // lblPattern
            // 
            this.lblPattern.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblPattern.Location = new System.Drawing.Point(10, 160);
            this.lblPattern.Name = "lblPattern";
            this.lblPattern.Size = new System.Drawing.Size(100, 30);
            this.lblPattern.TabIndex = 15;
            this.lblPattern.Text = "副程式";
            this.lblPattern.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(911, 26);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(40, 40);
            this.btnEdit.TabIndex = 14;
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
            this.cmbType.Location = new System.Drawing.Point(811, 30);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(94, 32);
            this.cmbType.TabIndex = 13;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.cmbType_SelectedIndexChanged);
            // 
            // lblType
            // 
            this.lblType.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblType.Location = new System.Drawing.Point(711, 30);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(100, 30);
            this.lblType.TabIndex = 12;
            this.lblType.Text = "型態";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRemark
            // 
            this.txtRemark.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtRemark.Location = new System.Drawing.Point(110, 30);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(376, 33);
            this.txtRemark.TabIndex = 10;
            this.txtRemark.TextChanged += new System.EventHandler(this.txtRemark_TextChanged);
            // 
            // lblRemark
            // 
            this.lblRemark.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblRemark.Location = new System.Drawing.Point(6, 30);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(100, 30);
            this.lblRemark.TabIndex = 9;
            this.lblRemark.Text = "備註";
            this.lblRemark.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnNext.Location = new System.Drawing.Point(811, 554);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(112, 40);
            this.btnNext.TabIndex = 21;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnPrev.Location = new System.Drawing.Point(811, 508);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(112, 40);
            this.btnPrev.TabIndex = 22;
            this.btnPrev.Text = "Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // ucRecipeArray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.grpStep);
            this.Name = "ucRecipeArray";
            this.Size = new System.Drawing.Size(970, 600);
            this.grpStep.ResumeLayout(false);
            this.grpStep.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbRoute;
        private System.Windows.Forms.Label lblRoute;
        private System.Windows.Forms.GroupBox grpStep;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbPattern;
        private System.Windows.Forms.Label lblPattern;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cmbMAP;
        private System.Windows.Forms.Label lblMAP;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
    }
}
