namespace Premtek
{
    partial class frmRecipe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.mnuFile = new System.Windows.Forms.MenuItem();
            this.mnuCreateFile = new System.Windows.Forms.MenuItem();
            this.mnuOpenFile = new System.Windows.Forms.MenuItem();
            this.mnuSaveFile = new System.Windows.Forms.MenuItem();
            this.mnuSaveAs = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mnuImport = new System.Windows.Forms.MenuItem();
            this.mnuImportDXF = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mnuExit = new System.Windows.Forms.MenuItem();
            this.mnuTool = new System.Windows.Forms.MenuItem();
            this.mnuValveController = new System.Windows.Forms.MenuItem();
            this.mnuCalibration = new System.Windows.Forms.MenuItem();
            this.mnuCCD = new System.Windows.Forms.MenuItem();
            this.mnuMAP = new System.Windows.Forms.MenuItem();
            this.mnuLUL = new System.Windows.Forms.MenuItem();
            this.scrollableControl1 = new System.Windows.Forms.ScrollableControl();
            this.btnTimesUp = new System.Windows.Forms.Button();
            this.btnTimerStart = new System.Windows.Forms.Button();
            this.btnContiEnd = new System.Windows.Forms.Button();
            this.btnArcMid = new System.Windows.Forms.Button();
            this.btnLineMid = new System.Windows.Forms.Button();
            this.btnContiStart = new System.Windows.Forms.Button();
            this.btnUFArray = new System.Windows.Forms.Button();
            this.btnDelay = new System.Windows.Forms.Button();
            this.btnWeight = new System.Windows.Forms.Button();
            this.btnPurge = new System.Windows.Forms.Button();
            this.btnFindHeight = new System.Windows.Forms.Button();
            this.btnAlign = new System.Windows.Forms.Button();
            this.btnArray = new System.Windows.Forms.Button();
            this.btnRectangle = new System.Windows.Forms.Button();
            this.btnCircle = new System.Windows.Forms.Button();
            this.btnArc = new System.Windows.Forms.Button();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnDot = new System.Windows.Forms.Button();
            this.dgvStep = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStepInsert = new System.Windows.Forms.Button();
            this.btnStepUpdate = new System.Windows.Forms.Button();
            this.btnStepDelete = new System.Windows.Forms.Button();
            this.btnPatternEdit = new System.Windows.Forms.Button();
            this.palStep = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmbPattern = new System.Windows.Forms.ComboBox();
            this.palMain = new System.Windows.Forms.Panel();
            this.ucDisplay2 = new ProjectAOI.ucDisplay();
            this.ucVision1 = new Premtek.ucVision();
            this.ucJoyStick1 = new Premtek.ucJoyStick();
            this.scrollableControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStep)).BeginInit();
            this.palMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFile,
            this.mnuTool,
            this.mnuLUL});
            // 
            // mnuFile
            // 
            this.mnuFile.Index = 0;
            this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuCreateFile,
            this.mnuOpenFile,
            this.mnuSaveFile,
            this.mnuSaveAs,
            this.menuItem1,
            this.mnuImport,
            this.menuItem2,
            this.mnuExit});
            this.mnuFile.Text = "檔案";
            // 
            // mnuCreateFile
            // 
            this.mnuCreateFile.Index = 0;
            this.mnuCreateFile.Text = "開新檔案";
            this.mnuCreateFile.Click += new System.EventHandler(this.mnuCreateFile_Click);
            // 
            // mnuOpenFile
            // 
            this.mnuOpenFile.Index = 1;
            this.mnuOpenFile.Text = "開啟舊檔";
            this.mnuOpenFile.Click += new System.EventHandler(this.mnuOpenFile_Click);
            // 
            // mnuSaveFile
            // 
            this.mnuSaveFile.Index = 2;
            this.mnuSaveFile.Text = "儲存檔案";
            this.mnuSaveFile.Click += new System.EventHandler(this.mnuSaveFile_Click);
            // 
            // mnuSaveAs
            // 
            this.mnuSaveAs.Index = 3;
            this.mnuSaveAs.Text = "另存新檔";
            this.mnuSaveAs.Click += new System.EventHandler(this.mnuSaveAs_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 4;
            this.menuItem1.Text = "-";
            // 
            // mnuImport
            // 
            this.mnuImport.Index = 5;
            this.mnuImport.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuImportDXF});
            this.mnuImport.Text = "匯入";
            // 
            // mnuImportDXF
            // 
            this.mnuImportDXF.Index = 0;
            this.mnuImportDXF.Text = "DXF匯入";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 6;
            this.menuItem2.Text = "-";
            // 
            // mnuExit
            // 
            this.mnuExit.Index = 7;
            this.mnuExit.Text = "離開";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuTool
            // 
            this.mnuTool.Index = 1;
            this.mnuTool.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuValveController,
            this.mnuCalibration,
            this.mnuCCD,
            this.mnuMAP});
            this.mnuTool.Text = "工具";
            // 
            // mnuValveController
            // 
            this.mnuValveController.Index = 0;
            this.mnuValveController.Text = "閥控器參數";
            // 
            // mnuCalibration
            // 
            this.mnuCalibration.Index = 1;
            this.mnuCalibration.Text = "校正";
            // 
            // mnuCCD
            // 
            this.mnuCCD.Index = 2;
            this.mnuCCD.Text = "相機";
            // 
            // mnuMAP
            // 
            this.mnuMAP.Index = 3;
            this.mnuMAP.Text = "MAP";
            this.mnuMAP.Click += new System.EventHandler(this.mnuMAP_Click);
            // 
            // mnuLUL
            // 
            this.mnuLUL.Index = 2;
            this.mnuLUL.Text = "上下料";
            // 
            // scrollableControl1
            // 
            this.scrollableControl1.AutoScroll = true;
            this.scrollableControl1.Controls.Add(this.btnTimesUp);
            this.scrollableControl1.Controls.Add(this.btnTimerStart);
            this.scrollableControl1.Controls.Add(this.btnContiEnd);
            this.scrollableControl1.Controls.Add(this.btnArcMid);
            this.scrollableControl1.Controls.Add(this.btnLineMid);
            this.scrollableControl1.Controls.Add(this.btnContiStart);
            this.scrollableControl1.Controls.Add(this.btnUFArray);
            this.scrollableControl1.Controls.Add(this.btnDelay);
            this.scrollableControl1.Controls.Add(this.btnWeight);
            this.scrollableControl1.Controls.Add(this.btnPurge);
            this.scrollableControl1.Controls.Add(this.btnFindHeight);
            this.scrollableControl1.Controls.Add(this.btnAlign);
            this.scrollableControl1.Controls.Add(this.btnArray);
            this.scrollableControl1.Controls.Add(this.btnRectangle);
            this.scrollableControl1.Controls.Add(this.btnCircle);
            this.scrollableControl1.Controls.Add(this.btnArc);
            this.scrollableControl1.Controls.Add(this.btnLine);
            this.scrollableControl1.Controls.Add(this.btnDot);
            this.scrollableControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.scrollableControl1.Location = new System.Drawing.Point(975, 0);
            this.scrollableControl1.Name = "scrollableControl1";
            this.scrollableControl1.Size = new System.Drawing.Size(120, 981);
            this.scrollableControl1.TabIndex = 1;
            this.scrollableControl1.Text = "scrollableControl1";
            // 
            // btnTimesUp
            // 
            this.btnTimesUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTimesUp.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnTimesUp.Location = new System.Drawing.Point(60, 480);
            this.btnTimesUp.Name = "btnTimesUp";
            this.btnTimesUp.Size = new System.Drawing.Size(60, 60);
            this.btnTimesUp.TabIndex = 19;
            this.btnTimesUp.Text = "計時條件";
            this.toolTip1.SetToolTip(this.btnTimesUp, "Array");
            this.btnTimesUp.UseVisualStyleBackColor = true;
            this.btnTimesUp.Click += new System.EventHandler(this.btnTimesUp_Click);
            // 
            // btnTimerStart
            // 
            this.btnTimerStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTimerStart.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnTimerStart.Location = new System.Drawing.Point(0, 480);
            this.btnTimerStart.Name = "btnTimerStart";
            this.btnTimerStart.Size = new System.Drawing.Size(60, 60);
            this.btnTimerStart.TabIndex = 18;
            this.btnTimerStart.Text = "計時開始";
            this.toolTip1.SetToolTip(this.btnTimerStart, "Array");
            this.btnTimerStart.UseVisualStyleBackColor = true;
            this.btnTimerStart.Click += new System.EventHandler(this.btnTimerStart_Click);
            // 
            // btnContiEnd
            // 
            this.btnContiEnd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnContiEnd.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnContiEnd.Location = new System.Drawing.Point(60, 360);
            this.btnContiEnd.Name = "btnContiEnd";
            this.btnContiEnd.Size = new System.Drawing.Size(60, 60);
            this.btnContiEnd.TabIndex = 17;
            this.btnContiEnd.Text = "線結束";
            this.toolTip1.SetToolTip(this.btnContiEnd, "Array");
            this.btnContiEnd.UseVisualStyleBackColor = true;
            this.btnContiEnd.Click += new System.EventHandler(this.btnContiEnd_Click);
            // 
            // btnArcMid
            // 
            this.btnArcMid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnArcMid.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnArcMid.Location = new System.Drawing.Point(60, 420);
            this.btnArcMid.Name = "btnArcMid";
            this.btnArcMid.Size = new System.Drawing.Size(60, 60);
            this.btnArcMid.TabIndex = 16;
            this.btnArcMid.Text = "弧中點";
            this.toolTip1.SetToolTip(this.btnArcMid, "Array");
            this.btnArcMid.UseVisualStyleBackColor = true;
            this.btnArcMid.Click += new System.EventHandler(this.btnArcMid_Click);
            // 
            // btnLineMid
            // 
            this.btnLineMid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLineMid.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnLineMid.Location = new System.Drawing.Point(0, 420);
            this.btnLineMid.Name = "btnLineMid";
            this.btnLineMid.Size = new System.Drawing.Size(60, 60);
            this.btnLineMid.TabIndex = 15;
            this.btnLineMid.Text = "線中點";
            this.toolTip1.SetToolTip(this.btnLineMid, "Array");
            this.btnLineMid.UseVisualStyleBackColor = true;
            this.btnLineMid.Click += new System.EventHandler(this.btnLineMid_Click);
            // 
            // btnContiStart
            // 
            this.btnContiStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnContiStart.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnContiStart.Location = new System.Drawing.Point(0, 360);
            this.btnContiStart.Name = "btnContiStart";
            this.btnContiStart.Size = new System.Drawing.Size(60, 60);
            this.btnContiStart.TabIndex = 14;
            this.btnContiStart.Text = "線開始";
            this.toolTip1.SetToolTip(this.btnContiStart, "Array");
            this.btnContiStart.UseVisualStyleBackColor = true;
            this.btnContiStart.Click += new System.EventHandler(this.btnContiStart_Click);
            // 
            // btnUFArray
            // 
            this.btnUFArray.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUFArray.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnUFArray.Location = new System.Drawing.Point(60, 120);
            this.btnUFArray.Name = "btnUFArray";
            this.btnUFArray.Size = new System.Drawing.Size(60, 60);
            this.btnUFArray.TabIndex = 13;
            this.btnUFArray.Text = "UF  陣列";
            this.toolTip1.SetToolTip(this.btnUFArray, "Array");
            this.btnUFArray.UseVisualStyleBackColor = true;
            this.btnUFArray.Click += new System.EventHandler(this.btnUFArray_Click);
            // 
            // btnDelay
            // 
            this.btnDelay.BackgroundImage = global::Premtek.Properties.Resources.Wait;
            this.btnDelay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelay.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnDelay.Location = new System.Drawing.Point(0, 300);
            this.btnDelay.Name = "btnDelay";
            this.btnDelay.Size = new System.Drawing.Size(60, 60);
            this.btnDelay.TabIndex = 12;
            this.toolTip1.SetToolTip(this.btnDelay, "Delay");
            this.btnDelay.UseVisualStyleBackColor = true;
            this.btnDelay.Click += new System.EventHandler(this.btnDelay_Click);
            // 
            // btnWeight
            // 
            this.btnWeight.BackgroundImage = global::Premtek.Properties.Resources.scale;
            this.btnWeight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnWeight.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnWeight.Location = new System.Drawing.Point(60, 240);
            this.btnWeight.Name = "btnWeight";
            this.btnWeight.Size = new System.Drawing.Size(60, 60);
            this.btnWeight.TabIndex = 11;
            this.toolTip1.SetToolTip(this.btnWeight, "Weight");
            this.btnWeight.UseVisualStyleBackColor = true;
            this.btnWeight.Click += new System.EventHandler(this.btnWeight_Click);
            // 
            // btnPurge
            // 
            this.btnPurge.BackgroundImage = global::Premtek.Properties.Resources.Purge;
            this.btnPurge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPurge.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnPurge.Location = new System.Drawing.Point(0, 240);
            this.btnPurge.Name = "btnPurge";
            this.btnPurge.Size = new System.Drawing.Size(60, 60);
            this.btnPurge.TabIndex = 10;
            this.toolTip1.SetToolTip(this.btnPurge, "Purge");
            this.btnPurge.UseVisualStyleBackColor = true;
            this.btnPurge.Click += new System.EventHandler(this.btnPurge_Click);
            // 
            // btnFindHeight
            // 
            this.btnFindHeight.BackgroundImage = global::Premtek.Properties.Resources.LaserReader;
            this.btnFindHeight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFindHeight.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnFindHeight.Location = new System.Drawing.Point(0, 180);
            this.btnFindHeight.Name = "btnFindHeight";
            this.btnFindHeight.Size = new System.Drawing.Size(60, 60);
            this.btnFindHeight.TabIndex = 9;
            this.toolTip1.SetToolTip(this.btnFindHeight, "FindHeight");
            this.btnFindHeight.UseVisualStyleBackColor = true;
            this.btnFindHeight.Click += new System.EventHandler(this.btnFindHeight_Click);
            // 
            // btnAlign
            // 
            this.btnAlign.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAlign.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAlign.Location = new System.Drawing.Point(60, 300);
            this.btnAlign.Name = "btnAlign";
            this.btnAlign.Size = new System.Drawing.Size(60, 60);
            this.btnAlign.TabIndex = 8;
            this.btnAlign.Text = "MACRO";
            this.toolTip1.SetToolTip(this.btnAlign, "Align");
            this.btnAlign.UseVisualStyleBackColor = true;
            this.btnAlign.Click += new System.EventHandler(this.btnAlign_Click);
            // 
            // btnArray
            // 
            this.btnArray.BackgroundImage = global::Premtek.Properties.Resources.Array;
            this.btnArray.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnArray.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnArray.Location = new System.Drawing.Point(0, 120);
            this.btnArray.Name = "btnArray";
            this.btnArray.Size = new System.Drawing.Size(60, 60);
            this.btnArray.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnArray, "Array");
            this.btnArray.UseVisualStyleBackColor = true;
            this.btnArray.Click += new System.EventHandler(this.btnArray_Click);
            // 
            // btnRectangle
            // 
            this.btnRectangle.Font = new System.Drawing.Font("微軟正黑體", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnRectangle.Location = new System.Drawing.Point(60, 180);
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.Size = new System.Drawing.Size(60, 60);
            this.btnRectangle.TabIndex = 6;
            this.btnRectangle.Text = "□";
            this.btnRectangle.UseVisualStyleBackColor = true;
            this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);
            // 
            // btnCircle
            // 
            this.btnCircle.BackgroundImage = global::Premtek.Properties.Resources.Circle;
            this.btnCircle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCircle.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCircle.Location = new System.Drawing.Point(60, 60);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(60, 60);
            this.btnCircle.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnCircle, "Circle");
            this.btnCircle.UseVisualStyleBackColor = true;
            this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
            // 
            // btnArc
            // 
            this.btnArc.BackgroundImage = global::Premtek.Properties.Resources.Arc;
            this.btnArc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnArc.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnArc.Location = new System.Drawing.Point(60, 0);
            this.btnArc.Name = "btnArc";
            this.btnArc.Size = new System.Drawing.Size(60, 60);
            this.btnArc.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnArc, "Arc");
            this.btnArc.UseVisualStyleBackColor = true;
            this.btnArc.Click += new System.EventHandler(this.btnArc_Click);
            // 
            // btnLine
            // 
            this.btnLine.BackgroundImage = global::Premtek.Properties.Resources.Line;
            this.btnLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLine.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnLine.Location = new System.Drawing.Point(0, 60);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(60, 60);
            this.btnLine.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnLine, "Line");
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnDot
            // 
            this.btnDot.BackgroundImage = global::Premtek.Properties.Resources.Dot;
            this.btnDot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDot.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnDot.Location = new System.Drawing.Point(0, 0);
            this.btnDot.Name = "btnDot";
            this.btnDot.Size = new System.Drawing.Size(60, 60);
            this.btnDot.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnDot, "Dot");
            this.btnDot.UseVisualStyleBackColor = true;
            this.btnDot.Click += new System.EventHandler(this.btnDot_Click);
            // 
            // dgvStep
            // 
            this.dgvStep.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStep.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvStep.Location = new System.Drawing.Point(3, 43);
            this.dgvStep.MultiSelect = false;
            this.dgvStep.Name = "dgvStep";
            this.dgvStep.ReadOnly = true;
            this.dgvStep.RowTemplate.Height = 24;
            this.dgvStep.Size = new System.Drawing.Size(970, 260);
            this.dgvStep.TabIndex = 2;
            this.dgvStep.Click += new System.EventHandler(this.dgvStep_Click);
            this.dgvStep.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvStep_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "副程式";
            // 
            // btnStepInsert
            // 
            this.btnStepInsert.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnStepInsert.Location = new System.Drawing.Point(752, 911);
            this.btnStepInsert.Name = "btnStepInsert";
            this.btnStepInsert.Size = new System.Drawing.Size(70, 70);
            this.btnStepInsert.TabIndex = 14;
            this.btnStepInsert.Text = "插入";
            this.btnStepInsert.UseVisualStyleBackColor = true;
            this.btnStepInsert.Click += new System.EventHandler(this.btnStepInsert_Click);
            // 
            // btnStepUpdate
            // 
            this.btnStepUpdate.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnStepUpdate.Location = new System.Drawing.Point(828, 911);
            this.btnStepUpdate.Name = "btnStepUpdate";
            this.btnStepUpdate.Size = new System.Drawing.Size(70, 70);
            this.btnStepUpdate.TabIndex = 15;
            this.btnStepUpdate.Text = "更新";
            this.btnStepUpdate.UseVisualStyleBackColor = true;
            this.btnStepUpdate.Click += new System.EventHandler(this.btnStepUpdate_Click);
            // 
            // btnStepDelete
            // 
            this.btnStepDelete.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnStepDelete.Location = new System.Drawing.Point(904, 911);
            this.btnStepDelete.Name = "btnStepDelete";
            this.btnStepDelete.Size = new System.Drawing.Size(70, 70);
            this.btnStepDelete.TabIndex = 16;
            this.btnStepDelete.Text = "刪除";
            this.btnStepDelete.UseVisualStyleBackColor = true;
            this.btnStepDelete.Click += new System.EventHandler(this.btnStepDelete_Click);
            // 
            // btnPatternEdit
            // 
            this.btnPatternEdit.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnPatternEdit.Location = new System.Drawing.Point(213, 3);
            this.btnPatternEdit.Name = "btnPatternEdit";
            this.btnPatternEdit.Size = new System.Drawing.Size(70, 34);
            this.btnPatternEdit.TabIndex = 17;
            this.btnPatternEdit.Text = "Edit";
            this.btnPatternEdit.UseVisualStyleBackColor = true;
            this.btnPatternEdit.Click += new System.EventHandler(this.btnPatternEdit_Click);
            // 
            // palStep
            // 
            this.palStep.Location = new System.Drawing.Point(3, 309);
            this.palStep.Name = "palStep";
            this.palStep.Size = new System.Drawing.Size(970, 600);
            this.palStep.TabIndex = 18;
            // 
            // cmbPattern
            // 
            this.cmbPattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPattern.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cmbPattern.FormattingEnabled = true;
            this.cmbPattern.Location = new System.Drawing.Point(86, 3);
            this.cmbPattern.Name = "cmbPattern";
            this.cmbPattern.Size = new System.Drawing.Size(121, 32);
            this.cmbPattern.TabIndex = 4;
            this.cmbPattern.SelectedIndexChanged += new System.EventHandler(this.cmbPattern_SelectedIndexChanged);
            // 
            // palMain
            // 
            this.palMain.Controls.Add(this.label1);
            this.palMain.Controls.Add(this.dgvStep);
            this.palMain.Controls.Add(this.palStep);
            this.palMain.Controls.Add(this.cmbPattern);
            this.palMain.Controls.Add(this.scrollableControl1);
            this.palMain.Controls.Add(this.btnPatternEdit);
            this.palMain.Controls.Add(this.btnStepInsert);
            this.palMain.Controls.Add(this.btnStepDelete);
            this.palMain.Controls.Add(this.btnStepUpdate);
            this.palMain.Dock = System.Windows.Forms.DockStyle.Right;
            this.palMain.Location = new System.Drawing.Point(809, 0);
            this.palMain.Name = "palMain";
            this.palMain.Size = new System.Drawing.Size(1095, 981);
            this.palMain.TabIndex = 20;
            // 
            // ucDisplay2
            // 
            this.ucDisplay2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ucDisplay2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucDisplay2.Location = new System.Drawing.Point(6, 6);
            this.ucDisplay2.Name = "ucDisplay2";
            this.ucDisplay2.Size = new System.Drawing.Size(800, 600);
            this.ucDisplay2.TabIndex = 19;
            // 
            // ucVision1
            // 
            this.ucVision1.AOI = null;
            this.ucVision1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ucVision1.Location = new System.Drawing.Point(6, 611);
            this.ucVision1.Name = "ucVision1";
            this.ucVision1.Size = new System.Drawing.Size(175, 350);
            this.ucVision1.TabIndex = 22;
            // 
            // ucJoyStick1
            // 
            this.ucJoyStick1.AxisA = 0;
            this.ucJoyStick1.AxisB = 0;
            this.ucJoyStick1.AxisC = 0;
            this.ucJoyStick1.AxisX = 0;
            this.ucJoyStick1.AxisY = 0;
            this.ucJoyStick1.AxisZ = 0;
            this.ucJoyStick1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ucJoyStick1.EqpMsg = null;
            this.ucJoyStick1.Location = new System.Drawing.Point(406, 611);
            this.ucJoyStick1.Motion = null;
            this.ucJoyStick1.Name = "ucJoyStick1";
            this.ucJoyStick1.Size = new System.Drawing.Size(400, 350);
            this.ucJoyStick1.Syslog = null;
            this.ucJoyStick1.TabIndex = 21;
            // 
            // frmRecipe
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1904, 981);
            this.Controls.Add(this.ucVision1);
            this.Controls.Add(this.ucJoyStick1);
            this.Controls.Add(this.palMain);
            this.Controls.Add(this.ucDisplay2);
            this.Menu = this.mainMenu1;
            this.Name = "frmRecipe";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Recipe 設定介面";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmRecipe_Load);
            this.scrollableControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStep)).EndInit();
            this.palMain.ResumeLayout(false);
            this.palMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem mnuFile;
        private System.Windows.Forms.MenuItem mnuCreateFile;
        private System.Windows.Forms.MenuItem mnuOpenFile;
        private System.Windows.Forms.MenuItem mnuSaveFile;
        private System.Windows.Forms.MenuItem mnuSaveAs;
        private System.Windows.Forms.MenuItem mnuTool;
        private System.Windows.Forms.MenuItem mnuValveController;
        private System.Windows.Forms.MenuItem mnuCalibration;
        private System.Windows.Forms.MenuItem mnuCCD;
        private System.Windows.Forms.MenuItem mnuLUL;
        private ProjectAOI.ucDisplay ucDisplay1;
        private System.Windows.Forms.ScrollableControl scrollableControl1;
        private System.Windows.Forms.Button btnWeight;
        private System.Windows.Forms.Button btnPurge;
        private System.Windows.Forms.Button btnFindHeight;
        private System.Windows.Forms.Button btnAlign;
        private System.Windows.Forms.Button btnArray;
        private System.Windows.Forms.Button btnRectangle;
        private System.Windows.Forms.Button btnCircle;
        private System.Windows.Forms.Button btnArc;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnDot;
        private System.Windows.Forms.DataGridView dgvStep;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStepInsert;
        private System.Windows.Forms.Button btnStepUpdate;
        private System.Windows.Forms.Button btnStepDelete;
        private System.Windows.Forms.Button btnPatternEdit;
        private System.Windows.Forms.Panel palStep;
        private System.Windows.Forms.Button btnDelay;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem mnuImport;
        private System.Windows.Forms.MenuItem mnuImportDXF;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem mnuExit;
        private System.Windows.Forms.ToolTip toolTip1;
        private ProjectAOI.ucDisplay ucDisplay2;
        private System.Windows.Forms.Panel palMain;
        private ucJoyStick ucJoyStick1;
        private ucVision ucVision1;
        public System.Windows.Forms.ComboBox cmbPattern;
        private System.Windows.Forms.Button btnUFArray;
        private System.Windows.Forms.Button btnContiEnd;
        private System.Windows.Forms.Button btnArcMid;
        private System.Windows.Forms.Button btnLineMid;
        private System.Windows.Forms.Button btnContiStart;
        private System.Windows.Forms.MenuItem mnuMAP;
        private System.Windows.Forms.Button btnTimesUp;
        private System.Windows.Forms.Button btnTimerStart;
    }
}