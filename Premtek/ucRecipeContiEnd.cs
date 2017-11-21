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
    public partial class ucRecipeContiEnd : UserControl
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
        private CRecipeStep _StepEdit;

        /// <summary>是否介面已載入
        /// </summary>
        /// <remarks>載入前不能引發ValueChanged等事件</remarks>
        private bool _IsLoaded = false;

        /// <summary>所屬表單
        /// </summary>
        private Form _parentForm;

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
            if (ControlMisc.SetNumericValue(ref this.nmuPosX, this._StepEdit.ContiEnd.Pos.X) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this.nmuPosY, this._StepEdit.ContiEnd.Pos.Y) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this.nmuPosZ, this._StepEdit.ContiEnd.Pos.Z) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this.nmuDotCount, this._StepEdit.ContiEnd.DotCount) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this.nmuWeight, this._StepEdit.ContiEnd.Weight) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this.nmuVelocity, this._StepEdit.ContiEnd.Velocity) != ErrorCode.Success)
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


        public ucRecipeContiEnd(ProjectCore.MSystemParameter.sSysParam sys)
        {
            this._IsLoaded = false;
            this._sys = sys;
            InitializeComponent();
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
            this._IsLoaded = true;
        }

        #region "資料變更"
        //TODO:連續資料位置檢查
        private void nmuPosX_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.ContiEnd.Pos.X = nmuPosX.Value;
        }

        private void nmuPosY_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.ContiEnd.Pos.Y = nmuPosY.Value;
        }

        private void nmuPosZ_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.ContiEnd.Pos.Z = nmuPosZ.Value;
        }

        private void nmuDotCount_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.ContiEnd.DotCount = (int)nmuDotCount.Value;
        }

        private void nmuWeight_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.ContiEnd.Weight = nmuWeight.Value;
        }
        private void txtRemark_TextChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Remark = txtRemark.Text;
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

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Type = cmbType.SelectedItem.ToString();
        }

        private void nmuVelocity_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.ContiEnd.Velocity = nmuVelocity.Value;
        }

    }
}
