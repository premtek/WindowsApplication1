using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectCore;
using System.Diagnostics;
using Premtek.Base;
using ProjectAOI;

namespace Premtek
{
    public partial class frmRecipe : Form
    {
        ProjectCore.MSystemParameter.sSysParam _Sys;
        /// <summary>外部引入系統參數
        /// </summary>
        public ProjectCore.MSystemParameter.sSysParam sys
        {
            get
            {
                return _Sys;
            }
            set
            {
                _Sys = value;
                if (value != null)
                {
                    ucJoyStick1.AxisX = value.AxisX;
                    ucJoyStick1.AxisY = value.AxisY;
                    ucJoyStick1.AxisZ = value.AxisZ;
                    ucJoyStick1.AxisA = value.AxisA;
                    ucJoyStick1.AxisB = value.AxisB;
                    ucJoyStick1.AxisC = value.AxisC;
                }
            }

        }
        /// <summary>外部軸卡物件
        /// </summary>
        public Premtek.Base.CMotionCollection Motion
        {
            get { return ucJoyStick1.Motion; }
            set { ucJoyStick1.Motion = value; }
        }
        /// <summary>外部配接Log物件
        /// </summary>
        public CSystemLog Syslog
        {
            get { return ucJoyStick1.Syslog; }
            set { ucJoyStick1.Syslog = value; }
        }
        /// <summary>外部配接MsgHandler物件
        /// </summary>
        public CEqpMsgHandler EqpMsg
        {
            get { return ucJoyStick1.EqpMsg; }
            set { ucJoyStick1.EqpMsg = value; }
        }
        public CAOICollection AOI
        {
            get { return ucVision1.AOI; }
            set { ucVision1.AOI = value; }
        }
        /// <summary>Recipe內單機數
        /// </summary>
        int MachineCount;
        /// <summary>Recipe內單機的平台數
        /// </summary>
        int MachineStageCount;
        /// <summary>Recipe內平台的閥數
        /// </summary>
        int StageValveCount;
        /// <summary>Recipe內軌道數
        /// </summary>
        int ConveyorCount;
        /// <summary>編輯用Recipe
        /// </summary>
        CRecipe gRecipeEdit;
        /// <summary>生產用Recipe
        /// </summary>
        CRecipe gRecipeUse;
        /// <summary>生產用Map
        /// </summary>
        CRecipe gMapUse;

        /// <summary>步驟編輯介面-點
        /// </summary>
        ucRecipeDot _StepDot;
        /// <summary>步驟編輯介面-線
        /// </summary>
        ucRecipeLine _StepLine;
        /// <summary>步驟編輯介面-弧
        /// </summary>
        ucRecipeArc _StepArc;
        /// <summary>步驟編輯介面-圓
        /// </summary>
        ucRecipeCircle _StepCircle;
        /// <summary>步驟編輯介面-矩形
        /// </summary>
        ucRecipeRectangle _StepRectangle;
        /// <summary>步驟編輯介面-陣列
        /// </summary>
        ucRecipeArray _StepArray;
        /// <summary>步驟編輯介面-巨集
        /// </summary>
        ucRecipePattern _StepMacro;
        /// <summary>步驟編輯介面-測高
        /// </summary>
        ucRecipeFindHeight _StepFindHeight;
        /// <summary>步驟編輯介面-Delay
        /// </summary>
        ucRecipeDelay _StepDelay;
        /// <summary>步驟編輯介面-排膠
        /// </summary>
        ucRecipePurge _StepPurge;
        /// <summary>步驟編輯介面-流量量測
        /// </summary>
        ucRecipeWeight _StepWeight;
        /// <summary>步驟編輯介面-弧中點
        /// </summary>
        ucRecipeArcMid _StepArcMid;
        /// <summary>步驟編輯介面-線結束
        /// </summary>
        ucRecipeContiEnd _StepContiEnd;
        /// <summary>步驟編輯介面-線開始
        /// </summary>
        ucRecipeContiStart _StepContiStart;
        /// <summary>步驟編輯介面-線中點
        /// </summary>
        ucRecipeLineMid _StepLineMid;
        /// <summary>步驟編輯介面-計時開始
        /// </summary>
        ucRecipeTimerStart _StepTimerStart;
        /// <summary>步驟編輯介面-計時限制
        /// </summary>
        ucRecipeTimesUp _StepTimesUp;
        /// <summary>Pattern定位編輯介面
        /// </summary>
        ucRecipePatternGroup _PatternAlign;
        /// <summary>步驟參數編輯介面
        /// </summary>
        ucRecipeStepParameter _StepParam;
        private void frmRecipe_Load(object sender, EventArgs e)
        {
            _StepParam = new ucRecipeStepParameter(MachineCount, MachineStageCount, StageValveCount, ConveyorCount);

            _StepParam.Name = "ucRecipeStepParameter";
            _StepParam.Width = palMain.Width;
            _StepParam.Height = palMain.Height;
            _StepParam.Visible = false;

            _PatternAlign = new ucRecipePatternGroup(MachineCount, MachineStageCount, StageValveCount, ConveyorCount);
            _PatternAlign.Name = "ucRecipePatternGroup";
            _PatternAlign.Width = palMain.Width;
            _PatternAlign.Height = palMain.Height;
            _PatternAlign.Visible = false;
            palMain.Controls.Add(_PatternAlign);
            palMain.Controls.Add(_StepParam);

            _StepDot = new ucRecipeDot(sys);
            _StepDot.Name = "ucRecipeDot";
            _StepDot.Width = palStep.Width;
            _StepDot.Height = palStep.Height;
            _StepDot.Visible = false;
            _StepDot.Motion = this.Motion;

            _StepLine = new ucRecipeLine(sys);
            _StepLine.Name = "ucRecipeLine";
            _StepLine.Width = palStep.Width;
            _StepLine.Height = palStep.Height;
            _StepLine.Visible = false;
            _StepLine.Motion = this.Motion;

            _StepArc = new ucRecipeArc(sys);
            _StepArc.Name = "ucRecipeArc";
            _StepArc.Width = palStep.Width;
            _StepArc.Height = palStep.Height;
            _StepArc.Visible = false;
            _StepArc.Motion = this.Motion;

            _StepCircle = new ucRecipeCircle(sys);
            _StepCircle.Name = "ucRecipeCircle";
            _StepCircle.Width = palStep.Width;
            _StepCircle.Height = palStep.Height;
            _StepCircle.Visible = false;
            _StepCircle.Motion = this.Motion;

            _StepRectangle = new ucRecipeRectangle(sys);
            _StepRectangle.Name = "ucRecipeRectangle";
            _StepRectangle.Width = palStep.Width;
            _StepRectangle.Height = palStep.Height;
            _StepRectangle.Visible = false;
            _StepRectangle.Motion = this.Motion;

            _StepArray = new ucRecipeArray(sys);
            _StepArray.Name = "ucRecipeArray";
            _StepArray.Width = palStep.Width;
            _StepArray.Height = palStep.Height;
            _StepArray.Visible = false;
            _StepArray.Motion = this.Motion;

            _StepMacro = new ucRecipePattern(sys);
            _StepMacro.Name = "ucRecipePattern";
            _StepMacro.Width = palStep.Width;
            _StepMacro.Height = palStep.Height;
            _StepMacro.Visible = false;
            _StepMacro.Motion = this.Motion;

            _StepFindHeight = new ucRecipeFindHeight(sys, MachineCount, MachineStageCount, StageValveCount, ConveyorCount);
            _StepFindHeight.Name = "ucRecipeFindHeight";
            _StepFindHeight.Width = palStep.Width;
            _StepFindHeight.Height = palStep.Height;
            _StepFindHeight.Visible = false;
            _StepFindHeight.Motion = this.Motion;

            _StepDelay = new ucRecipeDelay(sys);
            _StepDelay.Name = "ucRecipeDelay";
            _StepDelay.Width = palStep.Width;
            _StepDelay.Height = palStep.Height;
            _StepDelay.Visible = false;

            _StepPurge = new ucRecipePurge(sys);
            _StepPurge.Name = "ucRecipePurge";
            _StepPurge.Width = palStep.Width;
            _StepPurge.Height = palStep.Height;
            _StepPurge.Visible = false;
            _StepPurge.Motion = this.Motion;

            _StepWeight = new ucRecipeWeight(sys);
            _StepWeight.Name = "ucRecipeWeight";
            _StepWeight.Width = palStep.Width;
            _StepWeight.Height = palStep.Height;
            _StepWeight.Visible = false;
            _StepWeight.Motion = this.Motion;

            _StepArcMid = new ucRecipeArcMid(sys);
            _StepArcMid.Name = "ucRecipeArcMid";
            _StepArcMid.Width = palStep.Width;
            _StepArcMid.Height = palStep.Height;
            _StepArcMid.Visible = false;
            _StepArcMid.Motion = this.Motion;

            _StepContiEnd = new ucRecipeContiEnd(sys);
            _StepContiEnd.Name = "ucRecipeContiEnd";
            _StepContiEnd.Width = palStep.Width;
            _StepContiEnd.Height = palStep.Height;
            _StepContiEnd.Visible = false;
            _StepContiEnd.Motion = this.Motion;

            _StepContiStart = new ucRecipeContiStart(sys);
            _StepContiStart.Name = "ucRecipeContiStart";
            _StepContiStart.Width = palStep.Width;
            _StepContiStart.Height = palStep.Height;
            _StepContiStart.Visible = false;
            _StepContiStart.Motion = this.Motion;

            _StepLineMid = new ucRecipeLineMid(sys);
            _StepLineMid.Name = "ucRecipeLineMid";
            _StepLineMid.Width = palStep.Width;
            _StepLineMid.Height = _StepLineMid.Height;
            _StepLineMid.Visible = false;
            _StepLineMid.Motion = this.Motion;

            _StepTimerStart = new ucRecipeTimerStart(sys);
            _StepTimerStart.Name = "ucRecipeTimerStart";
            _StepTimerStart.Width = palStep.Width;
            _StepTimerStart.Height = palStep.Height;
            _StepTimerStart.Visible = false;
            _StepTimerStart.Motion = this.Motion;

            _StepTimesUp = new ucRecipeTimesUp(sys);
            _StepTimesUp.Name = "ucRecipeTimesUp";
            _StepTimesUp.Width = palStep.Width;
            _StepTimesUp.Height = palStep.Height;
            _StepTimesUp.Visible = false;
            _StepTimesUp.Motion = this.Motion;

            palStep.Controls.Clear();
            palStep.Controls.Add(_StepDot);
            palStep.Controls.Add(_StepLine);
            palStep.Controls.Add(_StepArc);
            palStep.Controls.Add(_StepCircle);
            palStep.Controls.Add(_StepRectangle);
            palStep.Controls.Add(_StepArray);
            palStep.Controls.Add(_StepMacro);
            palStep.Controls.Add(_StepFindHeight);
            palStep.Controls.Add(_StepDelay);
            palStep.Controls.Add(_StepPurge);
            palStep.Controls.Add(_StepWeight);
            palStep.Controls.Add(_StepArcMid);
            palStep.Controls.Add(_StepContiEnd);
            palStep.Controls.Add(_StepContiStart);
            palStep.Controls.Add(_StepLineMid);
            palStep.Controls.Add(_StepTimerStart);
            palStep.Controls.Add(_StepTimesUp);
        }


        public frmRecipe(int machineCount, int machineStageCount, int stageValveCount, int conveyorCount)
        {
            this.MachineCount = machineCount;
            this.MachineStageCount = machineStageCount;
            this.StageValveCount = stageValveCount;
            this.ConveyorCount = conveyorCount;
            gRecipeEdit = new CRecipe(MachineCount);
            InitializeComponent();
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            CreateStepColumn();

            UpdateMenuBar();


        }
        #region "步驟編輯"
        /// <summary>編輯用步驟資料
        /// </summary>
        CRecipeStep _StepEdit = null;

        /// <summary>建立步驟資料副本或新建步驟資料
        /// </summary>
        /// <param name="workType">作業形式</param>
        /// <returns></returns>
        CRecipeStep CreateOrLoadStep(eStepWorkType workType)
        {
            CRecipeStep _Temp = null;
            if (dgvStep.SelectedCells.Count > 0)//如果有選, 從既有資料撈取
            {
                if (dgvStep.SelectedCells[0].OwningRow.Cells[0].Value != null)//選取欄位非空
                {
                    string _patternID = cmbPattern.SelectedItem.ToString();
                    int _stepNo = dgvStep.SelectedCells[0].RowIndex;
                    if (gRecipeEdit.Pattern.ContainsKey(_patternID))
                    {
                        if ((0 <= _stepNo) && (_stepNo < gRecipeEdit.Pattern[_patternID].Step.Count))
                        {
                            _Temp = gRecipeEdit.Pattern[_patternID].Step[_stepNo].Clone();
                        }
                    }
                    if (_Temp == null) _Temp = new CRecipeStep(gRecipeEdit.Pattern[_patternID]);
                }
            }
            if (_Temp == null) _Temp = new CRecipeStep(null);
            _Temp.WorkType = workType;
            return _Temp;
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            _StepEdit = CreateOrLoadStep(eStepWorkType.Dot);
            _StepDot.StepGroup = gRecipeEdit.StepGroup;
            _StepDot.SetValue(_StepEdit, _StepEdit.Parent, this);
            _StepDot.BringToFront();
            _StepDot.Visible = true;
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            _StepEdit = CreateOrLoadStep(eStepWorkType.Line);
            _StepLine.StepGroup = gRecipeEdit.StepGroup;
            _StepLine.SetValue(_StepEdit, _StepEdit.Parent, this);
            _StepLine.BringToFront();
            _StepLine.Visible = true;
        }

        private void btnArc_Click(object sender, EventArgs e)
        {
            _StepEdit = CreateOrLoadStep(eStepWorkType.Arc);
            _StepArc.StepGroup = gRecipeEdit.StepGroup;
            _StepArc.SetValue(_StepEdit, _StepEdit.Parent, this);
            _StepArc.BringToFront();
            _StepArc.Visible = true;
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            _StepEdit = CreateOrLoadStep(eStepWorkType.Circle);
            _StepCircle.StepGroup = gRecipeEdit.StepGroup;
            _StepCircle.SetValue(_StepEdit, _StepEdit.Parent, this);
            _StepCircle.BringToFront();
            _StepCircle.Visible = true;
        }

        private void btnRectangle_Click(object sender, EventArgs e)
        {
            _StepEdit = CreateOrLoadStep(eStepWorkType.Rectangle);
            _StepRectangle.StepGroup = gRecipeEdit.StepGroup;
            _StepRectangle.SetValue(_StepEdit, _StepEdit.Parent, this);
            _StepRectangle.BringToFront();
            _StepRectangle.Visible = true;
        }

        private void btnArray_Click(object sender, EventArgs e)
        {
            _StepEdit = CreateOrLoadStep(eStepWorkType.Array);
            _StepArray.StepGroup = gRecipeEdit.StepGroup;
            _StepArray.SetValue(_StepEdit, _StepEdit.Parent, this);
            _StepArray.BringToFront();
            _StepArray.Visible = true;
        }

        private void btnAlign_Click(object sender, EventArgs e)
        {
            _StepEdit = CreateOrLoadStep(eStepWorkType.Pattern);
            _StepMacro.StepGroup = gRecipeEdit.StepGroup;
            _StepMacro.SetValue(_StepEdit, _StepEdit.Parent, this);
            _StepMacro.BringToFront();
            _StepMacro.Visible = true;
        }

        private void btnFindHeight_Click(object sender, EventArgs e)
        {
            _StepEdit = CreateOrLoadStep(eStepWorkType.FindHeight);
            _StepFindHeight.StepGroup = gRecipeEdit.StepGroup;
            _StepFindHeight.SetValue(_StepEdit, _StepEdit.Parent, this);
            _StepFindHeight.BringToFront();
            _StepFindHeight.Visible = true;
        }

        private void btnDelay_Click(object sender, EventArgs e)
        {
            _StepEdit = CreateOrLoadStep(eStepWorkType.Delay);
            _StepDelay.StepGroup = gRecipeEdit.StepGroup;
            _StepDelay.SetValue(_StepEdit, _StepEdit.Parent, this);
            _StepDelay.BringToFront();
            _StepDelay.Visible = true;
        }

        private void btnWeight_Click(object sender, EventArgs e)
        {
            _StepEdit = CreateOrLoadStep(eStepWorkType.Weight);
            _StepWeight.StepGroup = gRecipeEdit.StepGroup;
            _StepWeight.SetValue(_StepEdit, _StepEdit.Parent, this);
            _StepWeight.BringToFront();
            _StepWeight.Visible = true;
        }

        private void btnPurge_Click(object sender, EventArgs e)
        {
            _StepEdit = CreateOrLoadStep(eStepWorkType.Purge);
            _StepPurge.StepGroup = gRecipeEdit.StepGroup;
            _StepPurge.SetValue(_StepEdit, _StepEdit.Parent, this);
            _StepPurge.BringToFront();
            _StepPurge.Visible = true;
        }

        private void btnUFArray_Click(object sender, EventArgs e)
        {
            _StepEdit = CreateOrLoadStep(eStepWorkType.UFArray);
            _StepArray.StepGroup = gRecipeEdit.StepGroup;
            _StepArray.SetValue(_StepEdit, _StepEdit.Parent, this);
            _StepArray.BringToFront();
            _StepArray.Visible = true;
        }

        private void btnTimerStart_Click(object sender, EventArgs e)
        {
            _StepEdit = CreateOrLoadStep(eStepWorkType.TimerStart);
            _StepTimerStart.StepGroup = gRecipeEdit.StepGroup;
            _StepTimerStart.SetValue(_StepEdit, _StepEdit.Parent, this);
            _StepTimerStart.BringToFront();
            _StepTimerStart.Visible = true;
        }

        private void btnTimesUp_Click(object sender, EventArgs e)
        {
            _StepEdit = CreateOrLoadStep(eStepWorkType.TimesUp);
            _StepTimesUp.StepGroup = gRecipeEdit.StepGroup;
            _StepTimesUp.SetValue(_StepEdit, _StepEdit.Parent, this);
            _StepTimesUp.BringToFront();
            _StepTimesUp.Visible = true;
        }

        private void btnContiStart_Click(object sender, EventArgs e)
        {
            _StepEdit = CreateOrLoadStep(eStepWorkType.ContiStart);
            _StepContiStart.StepGroup = gRecipeEdit.StepGroup;
            _StepContiStart.SetValue(_StepEdit, _StepEdit.Parent, this);
            _StepContiStart.BringToFront();
            _StepContiStart.Visible = true;
        }

        private void btnContiEnd_Click(object sender, EventArgs e)
        {
            _StepEdit = CreateOrLoadStep(eStepWorkType.ContiEnd);
            _StepContiEnd.StepGroup = gRecipeEdit.StepGroup;
            _StepContiEnd.SetValue(_StepEdit, _StepEdit.Parent, this);
            _StepContiEnd.BringToFront();
            _StepContiEnd.Visible = true;
        }

        private void btnLineMid_Click(object sender, EventArgs e)
        {
            _StepEdit = CreateOrLoadStep(eStepWorkType.LineMid);
            _StepLineMid.StepGroup = gRecipeEdit.StepGroup;
            _StepLineMid.SetValue(_StepEdit, _StepEdit.Parent, this);
            _StepLineMid.BringToFront();
            _StepLineMid.Visible = true;
        }

        private void btnArcMid_Click(object sender, EventArgs e)
        {
            _StepEdit = CreateOrLoadStep(eStepWorkType.ArcMid);
            _StepArcMid.StepGroup = gRecipeEdit.StepGroup;
            _StepArcMid.SetValue(_StepEdit, _StepEdit.Parent, this);
            _StepArcMid.BringToFront();
            _StepArcMid.Visible = true;
        }
        #endregion

        #region "介面更新"

        /// <summary>更新選單
        /// </summary>
        private void UpdateMenuBar()
        {
            if (_FileExist)
            {
                mnuSaveAs.Enabled = true;
                mnuSaveFile.Enabled = true;
            }
            else
            {
                mnuSaveAs.Enabled = false;
                mnuSaveFile.Enabled = false;
            }
        }

        private void cmbPattern_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (cmbPattern.SelectedItem.ToString())
            {

                case "Main2":
                    sys = MSystemParameter.gSYS[MCommonDefine.eSys.DispStage2];
                    break;
                case "Main3":
                    sys = MSystemParameter.gSYS[MCommonDefine.eSys.DispStage3];
                    break;
                case "Main4":
                    sys = MSystemParameter.gSYS[MCommonDefine.eSys.DispStage4];
                    break;
                case "Main":
                default:
                    sys = MSystemParameter.gSYS[MCommonDefine.eSys.DispStage1];
                    break;
            }
            RefreshStep(0);//更新步驟顯示

            ucJoyStick1.AxisX = sys.AxisX;
            ucJoyStick1.AxisY = sys.AxisY;
            ucJoyStick1.AxisZ = sys.AxisZ;
            ucJoyStick1.AxisA = sys.AxisA;
            ucJoyStick1.AxisB = sys.AxisB;
            ucJoyStick1.AxisC = sys.AxisC;

        }

        /// <summary>建立步驟欄位
        /// </summary>
        void CreateStepColumn()
        {
            this.dgvStep.Columns.Clear();

            this.dgvStep.Columns.Add("Work", "Work");
            this.dgvStep.Columns.Add("Type", "Type");
            this.dgvStep.Columns.Add("Remark", "Remark");
            for (int i = 0; i < this.dgvStep.Columns.Count; i++)
            {
                this.dgvStep.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }
        /// <summary>更新Pattern與指定Sys
        /// </summary>
        void RefreshPattern()
        {
            cmbPattern.Items.Clear();
            foreach (string key in gRecipeEdit.Pattern.Keys)
            {
                cmbPattern.Items.Add(key);
            }
            if (cmbPattern.Items.Count > 0)
            {
                cmbPattern.SelectedIndex = 0;
            }


        }

        /// <summary>更新相應步驟顯示
        /// </summary>
        void RefreshStep(int stepNo)
        {
            this.dgvStep.Rows.Clear();
            string mPattern = cmbPattern.SelectedItem.ToString();
            for (int i = 0; i < gRecipeEdit.Pattern[mPattern].Step.Count; i++)
            {
                CRecipeStep _Step = gRecipeEdit.Pattern[mPattern].Step[i];
                this.dgvStep.Rows.Add(_Step.WorkType.ToString(), _Step.Type, _Step.Remark);
            }
            if (this.dgvStep.Rows.Count > 1)
            {
                if (stepNo < 0)
                {
                    //沒選
                }
                else if (stepNo < this.dgvStep.Rows.Count)
                {
                    this.dgvStep.Rows[stepNo].Selected = true;
                }
                else if (stepNo == this.dgvStep.Rows.Count)
                {
                    this.dgvStep.Rows[stepNo - 1].Selected = true;
                }
            }
            this.dgvStep.CurrentCell = dgvStep.Rows[stepNo].Cells[0];
            this.dgvStep.FirstDisplayedScrollingRowIndex = stepNo;

        }

        /// <summary>顯示步驟參數
        /// </summary>
        /// <param name="stepGroup"></param>
        public void ShowStepPrameter(Dictionary<string, CRecipeParameterStepGroup> stepGroup)
        {
            _StepParam.SetValue(stepGroup);
            _StepParam.BringToFront();
            _StepParam.Visible = true;
        }
        #endregion

        #region "檔案操作"
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        OpenFileDialog openFileDialog = new OpenFileDialog();
        bool _FileExist = false;
        /// <summary>開新檔案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCreateFile_Click(object sender, EventArgs e)
        {
            gRecipeEdit = new CRecipe(MachineCount);//開新檔案
            openFileDialog.FileName = "";
            saveFileDialog.FileName = "";
            _FileExist = true;
            RefreshPattern();//介面更新
            UpdateMenuBar();
        }

        /// <summary>開啟舊檔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuOpenFile_Click(object sender, EventArgs e)
        {

            openFileDialog.InitialDirectory = Application.StartupPath + @"\Recipe\";
            openFileDialog.Filter = "*.rcp|*.rcp";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                gRecipeEdit.Load(openFileDialog.FileName);
                gRecipeUse = gRecipeEdit.Clone();//讀檔時,設定生產檔
                gMapUse = gRecipeUse.PreProcess();
                saveFileDialog.FileName = openFileDialog.FileName;
                _FileExist = true;
            }
            RefreshPattern();//介面更新
            UpdateMenuBar();
        }

        /// <summary>儲存檔案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSaveFile_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.FileName == "")
            {
                saveFileDialog.InitialDirectory = Application.StartupPath + @"\Recipe\";
                saveFileDialog.Filter = "*.rcp|*.rcp";
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    gRecipeEdit.Save(saveFileDialog.FileName);
                    gRecipeUse = gRecipeEdit.Clone();//存檔時, 更新生產檔
                    gMapUse = gRecipeUse.PreProcess();
                    MessageBox.Show("Save OK!");
                }
            }
            else
            {
                if (System.IO.File.Exists(saveFileDialog.FileName))
                {
                    if (MessageBox.Show("File: " + saveFileDialog.FileName + " Already Existed, Overwrite it?") == System.Windows.Forms.DialogResult.OK)
                    {
                        gRecipeEdit.Save(saveFileDialog.FileName);
                        gRecipeUse = gRecipeEdit.Clone();//存檔時, 更新生產檔
                        gMapUse = gRecipeUse.PreProcess();
                        MessageBox.Show("Save OK!");
                    }
                    else
                    {
                        MessageBox.Show("Save Cancel!!");
                    }
                }
                else
                {
                    gRecipeEdit.Save(saveFileDialog.FileName);
                    gRecipeUse = gRecipeEdit.Clone();//存檔時, 更新生產檔
                    gMapUse = gRecipeUse.PreProcess();
                    MessageBox.Show("Save OK!");
                }

            }

        }

        /// <summary>另存新檔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSaveAs_Click(object sender, EventArgs e)
        {

            saveFileDialog.InitialDirectory = Application.StartupPath + @"\Recipe\";
            saveFileDialog.Filter = "*.rcp|*.rcp";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                gRecipeEdit.Save(saveFileDialog.FileName);
                gRecipeUse = gRecipeEdit.Clone();//存檔時, 更新生產檔
                gMapUse = gRecipeUse.PreProcess();
                MessageBox.Show("Save OK!");
            }
        }

        #endregion

        /// <summary>插入步驟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStepInsert_Click(object sender, EventArgs e)
        {
            if (_StepEdit == null) return;//沒資料不能插入
            if (dgvStep.SelectedCells.Count == 0) return;//沒選不能插入
            int _stepNo = dgvStep.SelectedCells[0].RowIndex;//插入點索引索引
            if (!_StepEdit.IsCorrect)
            {
                MessageBox.Show("參數不正確, 步驟插入失敗!!");
                return;
            }
            string _PatternID = cmbPattern.SelectedItem.ToString();
            if (!gRecipeEdit.Pattern.ContainsKey(_PatternID))
            {
                Debug.Assert(false);
            }
            gRecipeEdit.Pattern[_PatternID].Step.Insert(_stepNo, _StepEdit.Clone());//資料插入
            RefreshStep(_stepNo);

        }
        /// <summary>更新步驟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStepUpdate_Click(object sender, EventArgs e)
        {
            if (_StepEdit == null) return;//沒資料不能更新
            if (dgvStep.SelectedCells.Count == 0) return;//沒選不能更新
            int _stepNo = dgvStep.SelectedCells[0].RowIndex;//更新點索引索引
            if (!_StepEdit.IsCorrect)
            {
                MessageBox.Show("參數不正確, 步驟更新失敗!");
                return;
            }
            string _PatternID = cmbPattern.SelectedItem.ToString();
            if (!gRecipeEdit.Pattern.ContainsKey(_PatternID))
            {
                Debug.Assert(false);
            }
            if (_stepNo >= gRecipeEdit.Pattern[_PatternID].Step.Count)//無此步驟
            {
                return;
            }

            gRecipeEdit.Pattern[_PatternID].Step[_stepNo] = _StepEdit.Clone();//資料覆蓋

            RefreshStep(_stepNo);
        }
        /// <summary>刪除步驟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStepDelete_Click(object sender, EventArgs e)
        {
            if (dgvStep.SelectedCells.Count == 0) return;//沒選不能更新
            int _stepNo = dgvStep.SelectedCells[0].RowIndex;//更新點索引索引
            string _PatternID = cmbPattern.SelectedItem.ToString();
            if (!gRecipeEdit.Pattern.ContainsKey(_PatternID))
            {
                Debug.Assert(false);
            }
            if (_stepNo >= gRecipeEdit.Pattern[_PatternID].Step.Count)//無此步驟
            {
                return;
            }

            gRecipeEdit.Pattern[_PatternID].Step.RemoveAt(_stepNo);
            RefreshStep(_stepNo);
        }

        /// <summary>選取步驟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvStep_Click(object sender, EventArgs e)
        {
            if (cmbPattern.SelectedIndex < 0) return;//沒選Pattern
            
            string _patternID = cmbPattern.SelectedItem.ToString();
            if (!gRecipeEdit.Pattern.ContainsKey(_patternID))//Pattern不存在
            {
                foreach (Control obj in palStep.Controls)
                {
                    obj.Visible = false;
                }
                return;
            }
            if (dgvStep.SelectedCells.Count == 0)//DataGrid沒有資料
            {
                foreach (Control obj in palStep.Controls)
                {
                    obj.Visible = false;
                }
                return;
            }
            int _stepNo = dgvStep.SelectedCells[0].RowIndex;
            if (_stepNo >= gRecipeEdit.Pattern[_patternID].Step.Count)//選取索引超過步驟上限
            {
                foreach (Control obj in palStep.Controls)
                {
                    obj.Visible = false;
                }
                return;
            }
            switch (gRecipeEdit.Pattern[_patternID].Step[_stepNo].WorkType)//選什麼,點什麼
            {
                case eStepWorkType.Pattern:
                    btnAlign.PerformClick();
                    break;
                case eStepWorkType.Arc:
                    btnArc.PerformClick();
                    break;
                case eStepWorkType.Array:
                    btnArray.PerformClick();
                    break;
                case eStepWorkType.Circle:
                    btnCircle.PerformClick();
                    break;
                case eStepWorkType.Delay:
                    btnDelay.PerformClick();
                    break;
                case eStepWorkType.Dot:
                    btnDot.PerformClick();
                    break;
                case eStepWorkType.FindHeight:
                    btnFindHeight.PerformClick();
                    break;
                case eStepWorkType.Line:
                    btnLine.PerformClick();
                    break;
                case eStepWorkType.Purge:
                    btnPurge.PerformClick();
                    break;
                case eStepWorkType.Rectangle:
                    btnRectangle.PerformClick();
                    break;
                case eStepWorkType.UFArray:
                    btnUFArray.PerformClick();
                    break;
                case eStepWorkType.TimerStart:
                    btnTimerStart.PerformClick();
                    break;
                case eStepWorkType.TimesUp:
                    btnTimesUp.PerformClick();
                    break;
                case eStepWorkType.Weight:
                    btnWeight.PerformClick();
                    break;
                case eStepWorkType.ContiStart:
                    btnContiStart.PerformClick();
                    break;
                case eStepWorkType.ContiEnd:
                    btnContiEnd.PerformClick();
                    break;
                case eStepWorkType.LineMid:
                    btnLineMid.PerformClick();
                    break;
                case eStepWorkType.ArcMid:
                    btnArcMid.PerformClick();
                    break;
                default:
                    foreach (Control obj in palStep.Controls)
                    {
                        obj.Visible = false;
                    }
                    break;
            }
        }

        private void dgvStep_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                    dgvStep_Click(sender, e);
                    break;
            }
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPatternEdit_Click(object sender, EventArgs e)
        {
            _PatternAlign.BringToFront();
            _PatternAlign.Visible = true;
            _PatternAlign.SetValue(gRecipeEdit, cmbPattern.SelectedItem.ToString(), this);
        }

        private void mnuMAP_Click(object sender, EventArgs e)
        {
            frmMapSplit _frmMapSplit = new frmMapSplit();
            _frmMapSplit.MapList = gRecipeEdit.Map;
            _frmMapSplit.ShowDialog();

        }
















    }
}
