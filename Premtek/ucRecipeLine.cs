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
    public partial class ucRecipeLine : UserControl
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
            if (ControlMisc.SetNumericValue(ref nmuStartX, this._StepEdit.Line.Start.X) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuStartY, this._StepEdit.Line.Start.Y) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuStartZ, this._StepEdit.Line.Start.Z) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuEndX, this._StepEdit.Line.End.X) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuEndY, this._StepEdit.Line.End.Y) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuEndZ, this._StepEdit.Line.End.Z) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuDotCount, this._StepEdit.Line.DotCount) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuVelocity, this._StepEdit.Line.Velocity) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuWeight, this._StepEdit.Line.Weight) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetComboBox(ref cmbType, this._StepEdit.Type, "Default") != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            txtRemark.Text = this._StepEdit.Remark;
            this._parentForm = parentForm;
            RefreshUI();
            this._IsLoaded = true;
            return ErrorCode.Success;
        }

        public ucRecipeLine(ProjectCore.MSystemParameter.sSysParam sys)
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

        /// <summary>三點/圓心設定介面切換
        /// </summary>
        void RefreshUI()
        {
            this._StepEdit.IsCorrect = true;
            decimal _length = CMath.GetDistance(nmuStartX.Value, nmuStartY.Value, nmuEndX.Value, nmuEndY.Value);
            if (_length == 0)
            {
                nmuStartX.BackColor = Color.Red;
                nmuStartY.BackColor = Color.Red;
                nmuEndX.BackColor = Color.Red;
                nmuEndY.BackColor = Color.Red;
                this._StepEdit.IsCorrect = false;
            }
            else
            {
                nmuStartX.BackColor = SystemColors.Window;
                nmuStartY.BackColor = SystemColors.Window;
                nmuEndX.BackColor = SystemColors.Window;
                nmuEndY.BackColor = SystemColors.Window;
            }
            
            decimal _acc = 9800;
            decimal _cycleTime = 0.002M;
            decimal _avgWeight = 0.01M;
            int dotmin, dotmax;
            if (CRecipeStepLineLimit.GetDotLimit(_length, _acc, _cycleTime, out dotmin, out dotmax) == ErrorCode.Success)
            {
                nmuDotCount.Minimum = dotmin;
                nmuDotCount.Maximum = dotmax;
            }
            decimal velmin, velmax;
            if (CRecipeStepLineLimit.GetVelLimit(_length,  _acc, _cycleTime, out velmin, out velmax) == ErrorCode.Success)
            {
                nmuVelocity.Minimum = velmin;
                nmuVelocity.Maximum = velmax;
            }
           
            decimal wmin, wmax;
            if (CRecipeStepLineLimit.GetWeightLimit(_length, _acc, _cycleTime, _avgWeight, out wmin, out wmax) == ErrorCode.Success)
            {
                nmuWeight.Minimum = wmin;
                nmuWeight.Maximum = wmax;
                txtAvgWeight.BackColor = SystemColors.Window;
            }
            else
            {
                if (_avgWeight <= 0)
                {
                    txtAvgWeight.BackColor = Color.Red;
                }
            }
            if (nmuDotCount.Value > 1)
            {
                txtPitch.Text = (_length / (nmuDotCount.Value - 1)).ToString("0.000");//間距顯示
            }
            
        }
        #region "資料變更"
        private void nmuStartX_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Line.Start.X = this.nmuStartX.Value;
            RefreshUI();
        }

        private void nmuStartY_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Line.Start.Y = this.nmuStartY.Value;
            RefreshUI();
        }

        private void nmuStartZ_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Line.Start.Z = this.nmuStartZ.Value;
        }

        private void nmuEndX_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Line.End.X = this.nmuEndX.Value;
            RefreshUI();
        }

        private void nmuEndY_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Line.End.Y = this.nmuEndY.Value;
            RefreshUI();
        }

        private void nmuEndZ_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Line.End.Z = this.nmuEndZ.Value;
        }

        private void txtRemark_TextChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Remark = this.txtRemark.Text;
        }

        private void nmuVelocity_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Line.Velocity = this.nmuVelocity.Value;
            RefreshUI();
        }

        private void nmuDotCount_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Line.DotCount =(Int32) this.nmuDotCount.Value;
            RefreshUI();
        }

        private void nmuWeight_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Line.Weight = this.nmuWeight.Value;
            RefreshUI();
        }
        #endregion

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ((frmRecipe)this._parentForm).ShowStepPrameter(this.StepGroup);
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Type = cmbType.SelectedItem.ToString();
        }
    }
}
