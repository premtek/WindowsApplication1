namespace Premtek
{
    partial class ucIndexer
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
            this.btnStep = new System.Windows.Forms.Button();
            this.btnGoPos = new System.Windows.Forms.Button();
            this.nmuYno = new System.Windows.Forms.NumericUpDown();
            this.nmuXno = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuYno)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuXno)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Controls.Add(this.btnStep);
            this.groupBox1.Controls.Add(this.btnGoPos);
            this.groupBox1.Controls.Add(this.nmuYno);
            this.groupBox1.Controls.Add(this.nmuXno);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnRight);
            this.groupBox1.Controls.Add(this.btnLeft);
            this.groupBox1.Controls.Add(this.btnDown);
            this.groupBox1.Controls.Add(this.btnUp);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(203, 284);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Indexer";
            // 
            // btnStep
            // 
            this.btnStep.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnStep.Location = new System.Drawing.Point(76, 166);
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(50, 50);
            this.btnStep.TabIndex = 592;
            this.btnStep.Text = "1";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // btnGoPos
            // 
            this.btnGoPos.BackgroundImage = global::Premtek.Properties.Resources.goPos;
            this.btnGoPos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGoPos.Location = new System.Drawing.Point(141, 43);
            this.btnGoPos.Name = "btnGoPos";
            this.btnGoPos.Size = new System.Drawing.Size(50, 50);
            this.btnGoPos.TabIndex = 591;
            this.btnGoPos.Tag = "0";
            this.btnGoPos.UseVisualStyleBackColor = true;
            this.btnGoPos.Click += new System.EventHandler(this.btnGoPos_Click);
            // 
            // nmuYno
            // 
            this.nmuYno.Location = new System.Drawing.Point(75, 72);
            this.nmuYno.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nmuYno.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmuYno.Name = "nmuYno";
            this.nmuYno.Size = new System.Drawing.Size(60, 33);
            this.nmuYno.TabIndex = 590;
            this.nmuYno.Value = new decimal(new int[] {
            999,
            0,
            0,
            0});
            // 
            // nmuXno
            // 
            this.nmuXno.Location = new System.Drawing.Point(75, 33);
            this.nmuXno.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nmuXno.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmuXno.Name = "nmuXno";
            this.nmuXno.Size = new System.Drawing.Size(60, 33);
            this.nmuXno.TabIndex = 589;
            this.nmuXno.Value = new decimal(new int[] {
            999,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 24);
            this.label2.TabIndex = 588;
            this.label2.Text = "Yno:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 24);
            this.label1.TabIndex = 587;
            this.label1.Text = "Xno:";
            // 
            // btnRight
            // 
            this.btnRight.BackgroundImage = global::Premtek.Properties.Resources.I33_135;
            this.btnRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRight.Location = new System.Drawing.Point(132, 166);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(50, 50);
            this.btnRight.TabIndex = 586;
            this.btnRight.Tag = "4";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnGoPos_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.BackgroundImage = global::Premtek.Properties.Resources.I33_133;
            this.btnLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLeft.Location = new System.Drawing.Point(20, 166);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(50, 50);
            this.btnLeft.TabIndex = 585;
            this.btnLeft.Tag = "3";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnGoPos_Click);
            // 
            // btnDown
            // 
            this.btnDown.BackgroundImage = global::Premtek.Properties.Resources.I33_156;
            this.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDown.Location = new System.Drawing.Point(76, 224);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(50, 50);
            this.btnDown.TabIndex = 584;
            this.btnDown.Tag = "2";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnGoPos_Click);
            // 
            // btnUp
            // 
            this.btnUp.BackgroundImage = global::Premtek.Properties.Resources.I33_110;
            this.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUp.Location = new System.Drawing.Point(76, 111);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(50, 50);
            this.btnUp.TabIndex = 583;
            this.btnUp.Tag = "1";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnGoPos_Click);
            // 
            // ucIndexer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ucIndexer";
            this.Size = new System.Drawing.Size(203, 284);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuYno)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmuXno)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nmuYno;
        private System.Windows.Forms.NumericUpDown nmuXno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnGoPos;
        private System.Windows.Forms.Button btnStep;
    }
}
