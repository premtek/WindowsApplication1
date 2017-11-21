namespace Premtek
{
    partial class ucJoyStick
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGo1 = new System.Windows.Forms.Button();
            this.cmbModule = new System.Windows.Forms.ComboBox();
            this.btnHome = new System.Windows.Forms.Button();
            this.txtPosZ = new System.Windows.Forms.TextBox();
            this.txtPosY = new System.Windows.Forms.TextBox();
            this.txtPosX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCoordinate = new System.Windows.Forms.Button();
            this.btnMode = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnSpeed = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.mTimer = new System.Timers.Timer();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mTimer)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGo1);
            this.groupBox1.Controls.Add(this.cmbModule);
            this.groupBox1.Controls.Add(this.btnHome);
            this.groupBox1.Controls.Add(this.txtPosZ);
            this.groupBox1.Controls.Add(this.txtPosY);
            this.groupBox1.Controls.Add(this.txtPosX);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnCoordinate);
            this.groupBox1.Controls.Add(this.btnMode);
            this.groupBox1.Controls.Add(this.btnDown);
            this.groupBox1.Controls.Add(this.btnUp);
            this.groupBox1.Controls.Add(this.btnRight);
            this.groupBox1.Controls.Add(this.btnLeft);
            this.groupBox1.Controls.Add(this.btnForward);
            this.groupBox1.Controls.Add(this.btnSpeed);
            this.groupBox1.Controls.Add(this.btnBack);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 350);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Jog Controller";
            // 
            // btnGo1
            // 
            this.btnGo1.BackgroundImage = global::Premtek.Properties.Resources.goPos;
            this.btnGo1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGo1.Location = new System.Drawing.Point(242, 180);
            this.btnGo1.Name = "btnGo1";
            this.btnGo1.Size = new System.Drawing.Size(70, 70);
            this.btnGo1.TabIndex = 13;
            this.btnGo1.UseVisualStyleBackColor = true;
            this.btnGo1.Visible = false;
            // 
            // cmbModule
            // 
            this.cmbModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModule.FormattingEnabled = true;
            this.cmbModule.Location = new System.Drawing.Point(267, 146);
            this.cmbModule.Name = "cmbModule";
            this.cmbModule.Size = new System.Drawing.Size(121, 28);
            this.cmbModule.TabIndex = 12;
            this.cmbModule.Visible = false;
            // 
            // btnHome
            // 
            this.btnHome.BackgroundImage = global::Premtek.Properties.Resources.home;
            this.btnHome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnHome.Location = new System.Drawing.Point(318, 180);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(70, 70);
            this.btnHome.TabIndex = 11;
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Visible = false;
            // 
            // txtPosZ
            // 
            this.txtPosZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPosZ.Location = new System.Drawing.Point(288, 101);
            this.txtPosZ.Name = "txtPosZ";
            this.txtPosZ.Size = new System.Drawing.Size(100, 29);
            this.txtPosZ.TabIndex = 10;
            // 
            // txtPosY
            // 
            this.txtPosY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPosY.Location = new System.Drawing.Point(288, 66);
            this.txtPosY.Name = "txtPosY";
            this.txtPosY.Size = new System.Drawing.Size(100, 29);
            this.txtPosY.TabIndex = 10;
            // 
            // txtPosX
            // 
            this.txtPosX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPosX.Location = new System.Drawing.Point(288, 31);
            this.txtPosX.Name = "txtPosX";
            this.txtPosX.Size = new System.Drawing.Size(100, 29);
            this.txtPosX.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(232, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 30);
            this.label3.TabIndex = 9;
            this.label3.Text = "Z";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(232, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 30);
            this.label2.TabIndex = 9;
            this.label2.Text = "Y";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(232, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 30);
            this.label1.TabIndex = 9;
            this.label1.Text = "X";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCoordinate
            // 
            this.btnCoordinate.Location = new System.Drawing.Point(6, 180);
            this.btnCoordinate.Name = "btnCoordinate";
            this.btnCoordinate.Size = new System.Drawing.Size(70, 70);
            this.btnCoordinate.TabIndex = 8;
            this.btnCoordinate.Text = "R";
            this.btnCoordinate.UseVisualStyleBackColor = true;
            this.btnCoordinate.Click += new System.EventHandler(this.btnCoordinate_Click);
            // 
            // btnMode
            // 
            this.btnMode.Location = new System.Drawing.Point(6, 28);
            this.btnMode.Name = "btnMode";
            this.btnMode.Size = new System.Drawing.Size(70, 70);
            this.btnMode.TabIndex = 7;
            this.btnMode.Text = "Jog";
            this.btnMode.UseVisualStyleBackColor = true;
            this.btnMode.Click += new System.EventHandler(this.btnMode_Click);
            // 
            // btnDown
            // 
            this.btnDown.Image = global::Premtek.Properties.Resources.I33_156;
            this.btnDown.Location = new System.Drawing.Point(156, 180);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(70, 70);
            this.btnDown.TabIndex = 6;
            this.btnDown.Text = "Z";
            this.btnDown.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDown_MouseDown);
            this.btnDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnDown_MouseUp);
            // 
            // btnUp
            // 
            this.btnUp.Image = global::Premtek.Properties.Resources.I33_110;
            this.btnUp.Location = new System.Drawing.Point(156, 28);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(70, 70);
            this.btnUp.TabIndex = 5;
            this.btnUp.Text = "Z";
            this.btnUp.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseDown);
            this.btnUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseUp);
            // 
            // btnRight
            // 
            this.btnRight.Image = global::Premtek.Properties.Resources.I33_135;
            this.btnRight.Location = new System.Drawing.Point(156, 104);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(70, 70);
            this.btnRight.TabIndex = 4;
            this.btnRight.Text = "X";
            this.btnRight.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnRight_MouseDown);
            this.btnRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnRight_MouseUp);
            // 
            // btnLeft
            // 
            this.btnLeft.Image = global::Premtek.Properties.Resources.I33_133;
            this.btnLeft.Location = new System.Drawing.Point(6, 104);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(70, 70);
            this.btnLeft.TabIndex = 3;
            this.btnLeft.Text = "X";
            this.btnLeft.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLeft_MouseDown);
            this.btnLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnLeft_MouseUp);
            // 
            // btnForward
            // 
            this.btnForward.Image = global::Premtek.Properties.Resources.I33_156;
            this.btnForward.Location = new System.Drawing.Point(82, 180);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(70, 70);
            this.btnForward.TabIndex = 2;
            this.btnForward.Text = "Y";
            this.btnForward.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnForward_MouseDown);
            this.btnForward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnForward_MouseUp);
            // 
            // btnSpeed
            // 
            this.btnSpeed.Image = global::Premtek.Properties.Resources.SpeedLow;
            this.btnSpeed.Location = new System.Drawing.Point(82, 104);
            this.btnSpeed.Name = "btnSpeed";
            this.btnSpeed.Size = new System.Drawing.Size(70, 70);
            this.btnSpeed.TabIndex = 1;
            this.btnSpeed.Text = "Spd";
            this.btnSpeed.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnSpeed.UseVisualStyleBackColor = true;
            this.btnSpeed.Click += new System.EventHandler(this.btnSpeed_Click);
            // 
            // btnBack
            // 
            this.btnBack.Image = global::Premtek.Properties.Resources.I33_110;
            this.btnBack.Location = new System.Drawing.Point(80, 29);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(70, 70);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Y";
            this.btnBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnBack_MouseDown);
            this.btnBack.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnBack_MouseUp);
            // 
            // mTimer
            // 
            this.mTimer.Enabled = true;
            this.mTimer.SynchronizingObject = this;
            this.mTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.mTimer_Elapsed);
            // 
            // ucJoyStick
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.groupBox1);
            this.Name = "ucJoyStick";
            this.Size = new System.Drawing.Size(400, 350);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mTimer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPosZ;
        private System.Windows.Forms.TextBox txtPosY;
        private System.Windows.Forms.TextBox txtPosX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCoordinate;
        private System.Windows.Forms.Button btnMode;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnSpeed;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ComboBox cmbModule;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnGo1;
        private System.Timers.Timer mTimer;
    }
}
