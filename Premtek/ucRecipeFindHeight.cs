using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Premtek
{
    public partial class ucRecipeFindHeight : UserControl
    {
        /// <summary>外部引入系統參數
        /// </summary>
        private ProjectCore.MSystemParameter.sSysParam _sys;
        /// <summary>外部引入Step參數
        /// </summary>
        public Dictionary<string, CRecipeParameterStepGroup> StepGroup;
        /// <summary>外部配接軸卡物件
        /// </summary>
        public Premtek.Base.CMotionCollection Motion { get; set; }
        /// <summary>編輯用步驟
        /// </summary>
        private CRecipeStep _StepEdit ;
        /// <summary>是否介面已載入
        /// </summary>
        /// <remarks>載入前不能引發ValueChanged等事件</remarks>
        private bool _IsLoaded = false;
        /// <summary>所屬表單
        /// </summary>
        private Form _parentForm;

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
        /// 
        int ConveyorCount;
        /// <summary>選擇的軌道編號
        /// </summary>
        int conveyorNo;
        /// <summary>選擇的Stage編號
        /// </summary>
        int stageNo;

        /// <summary>設定數值
        /// </summary>
        /// <param name="data">待顯示資料</param>
        /// <returns></returns>
        public ErrorCode SetValue(CRecipeStep data, CRecipePattern parent, Form parentForm)
        {
            this._IsLoaded = false;
            if (data != null)
            {
                this._StepEdit = data;
            }
            else
            {
                this._StepEdit = new CRecipeStep(parent);
            }
            this._StepEdit.IsCorrect = true;
            if (ControlMisc.SetNumericValue(ref this.nmuPosX, this._StepEdit.FindHeight.Height[conveyorNo][stageNo].Pos.X) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this.nmuPosY, this._StepEdit.FindHeight.Height[conveyorNo][stageNo].Pos.Y) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this.nmuPosZ, this._StepEdit.FindHeight.Height[conveyorNo][stageNo].Pos.Z) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetComboBox(ref cmbType, this._StepEdit.Type, "Default") != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            txtRemark.Text = this._StepEdit.Remark;
            this._parentForm = parentForm;
            this._IsLoaded = true;
            return ErrorCode.Success;
        }
        public ucRecipeFindHeight(ProjectCore.MSystemParameter.sSysParam sys, int machineCount, int machineStageCount, int stageValveCount, int conveyorCount)
        {
            this._IsLoaded = false;
            this._sys = sys;
            InitializeComponent();
            this.MachineCount = machineCount;
            this.MachineStageCount = machineStageCount;
            this.StageValveCount = stageValveCount;
            this.ConveyorCount = conveyorCount;
            cmbType.Items.Clear();
            cmbType.Items.Add("Default");
            if (StepGroup != null)
            {
                foreach (string key in StepGroup.Keys)
                {
                    if (!cmbType.Items.Contains(key))
                    {
                        cmbType.Items.Add(key);
                    }
                }
            }
            switch (ConveyorCount)
            {
                case 2:
                    switch (MachineStageCount)
                    {
                        case 2:
                            btnC1S1.Visible = true;
                            btnC1S2.Visible = true;
                            btnC2S1.Visible = true;
                            btnC2S2.Visible = true;
                            break;
                        default:
                            btnC1S1.Visible = true;
                            btnC1S2.Visible = true;
                            btnC2S1.Visible = true;
                            btnC2S2.Visible = true;
                            break;
                    }

                    break;

                default:
                    switch (MachineStageCount)
                    {
                        case 2:
                            btnC1S1.Visible = true;
                            btnC1S2.Visible = true;
                            btnC2S1.Visible = false;
                            btnC2S2.Visible = false;
                            break;
                        default:
                            btnC1S1.Visible = true;
                            btnC1S2.Visible = false;
                            btnC2S1.Visible = false;
                            btnC2S2.Visible = false;
                            break;
                    }
                    break;
            }
            this._IsLoaded = true;
        }


        #region "資料變更"

        private void txtRemark_TextChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Remark = txtRemark.Text;
        }

        private void nmuPosX_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.FindHeight.Height[conveyorNo][stageNo].Pos.X = nmuPosX.Value;
        }

        private void nmuPosY_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.FindHeight.Height[conveyorNo][stageNo].Pos.Y = nmuPosY.Value;
        }

        private void nmuPosZ_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.FindHeight.Height[conveyorNo][stageNo].Pos.Z = nmuPosZ.Value;
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Type= cmbType.SelectedItem.ToString();
        }

        #endregion

        private void btnGoPos_Click(object sender, EventArgs e)
        {
            decimal[] _pos = new decimal[] { nmuPosX.Value, nmuPosY.Value, nmuPosZ.Value, 0, 0, 0 };
            _sys.EsysNum = 1000;
            do
            {
                if (CMotionMisc.SafeMovePos(_sys, _pos) == ErrorCode.Success)
                    break;
            } while (true);
        }

        private void btnSetPos_Click(object sender, EventArgs e)
        {
            decimal _PosX, _PosY, _PosZ;
            decimal.TryParse(Motion.GetPositionValue(_sys.AxisX), out _PosX);
            decimal.TryParse(Motion.GetPositionValue(_sys.AxisY), out _PosY);
            decimal.TryParse(Motion.GetPositionValue(_sys.AxisZ), out _PosZ);
            nmuPosX.Value = _PosX;
            nmuPosY.Value = _PosY;
            nmuPosZ.Value = _PosZ;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ((frmRecipe)this._parentForm).ShowStepPrameter(this.StepGroup);
        }

        private void btnC1S1_Click(object sender, EventArgs e)
        {
            conveyorNo = 0;
            stageNo = 0;
            UpdateuI();
        }
       
        private void btnC2S1_Click(object sender, EventArgs e)
        {
            conveyorNo = 1;
            stageNo = 0;
            UpdateuI();
        }

        private void btnC1S2_Click(object sender, EventArgs e)
        {
            conveyorNo = 0;
            stageNo = 1;
            UpdateuI();
        }

        private void btnC2S2_Click(object sender, EventArgs e)
        {
            conveyorNo = 1;
            stageNo = 1;
            UpdateuI();
        }
        void UpdateuI()
        {
            switch (conveyorNo)
            {
                case 0:
                    switch (stageNo)
                    {
                        case 0:
                            btnC1S1.BackColor = Color.Blue;
                            btnC1S2.BackColor = SystemColors.Control;
                            btnC1S2.UseVisualStyleBackColor = true;
                            btnC2S1.BackColor = SystemColors.Control;
                            btnC2S1.UseVisualStyleBackColor = true;
                            btnC2S2.BackColor = SystemColors.Control;
                            btnC2S2.UseVisualStyleBackColor = true;
                            break;
                        case 1:
                            btnC1S1.BackColor = SystemColors.Control;
                            btnC1S1.UseVisualStyleBackColor = true;
                            btnC1S2.BackColor = Color.Blue;
                            btnC2S1.BackColor = SystemColors.Control;
                            btnC2S1.UseVisualStyleBackColor = true;
                            btnC2S2.BackColor = SystemColors.Control;
                            btnC2S2.UseVisualStyleBackColor = true;
                            break;
                    }
                    break;
                case 1:
                    switch (stageNo)
                    {
                        case 0:
                            btnC1S1.BackColor = SystemColors.Control;
                            btnC1S1.UseVisualStyleBackColor = true;
                            btnC1S2.UseVisualStyleBackColor = true;
                            btnC2S1.BackColor = Color.Blue;
                            btnC2S2.BackColor = SystemColors.Control;
                            btnC2S2.UseVisualStyleBackColor = true;
                            break;
                        case 1:
                            btnC1S1.BackColor = SystemColors.Control;
                            btnC1S1.UseVisualStyleBackColor = true;
                            btnC1S2.UseVisualStyleBackColor = true;
                            btnC2S1.BackColor = SystemColors.Control;
                            btnC2S1.UseVisualStyleBackColor = true;
                            btnC2S2.BackColor = Color.Blue;
                            break;
                    }
                    break;
            }
            ControlMisc.SetNumericValue(ref nmuPosX, this._StepEdit.FindHeight.Height[conveyorNo][stageNo].Pos.X);
            ControlMisc.SetNumericValue(ref nmuPosY, this._StepEdit.FindHeight.Height[conveyorNo][stageNo].Pos.Y);
            ControlMisc.SetNumericValue(ref nmuPosZ, this._StepEdit.FindHeight.Height[conveyorNo][stageNo].Pos.Z);
        }
    }
}
