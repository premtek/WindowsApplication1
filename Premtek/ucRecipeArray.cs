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
    public partial class ucRecipeArray : UserControl
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
            if (ControlMisc.SetNumericValue(ref this._TeachPitchSize.nmuPosX1, this._StepEdit.Array.Origin.X) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this._TeachPitchSize.nmuPosY1, this._StepEdit.Array.Origin.Y) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this._TeachPitchSize.nmuPosZ1, this._StepEdit.Array.Origin.Z) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this._TeachPitchSize.nmuADirectionX, this._StepEdit.Array.ASide.X) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this._TeachPitchSize.nmuADirectionY, this._StepEdit.Array.ASide.Y) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this._TeachPitchSize.nmuADirectionZ, this._StepEdit.Array.ASide.Z) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this._TeachPitchSize.nmuBDirectionX, this._StepEdit.Array.BSide.X) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this._TeachPitchSize.nmuBDirectionY, this._StepEdit.Array.BSide.Y) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this._TeachPitchSize.nmuBDirectionZ, this._StepEdit.Array.BSide.Z) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this._TeachAdjust.nmuCountX, this._StepEdit.Array.ACount) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this._TeachAdjust.nmuCountX, this._StepEdit.Array.BCount) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetComboBox(ref cmbType, this._StepEdit.Type, "Default") != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetComboBox(ref cmbRoute, (int)this._StepEdit.Array.Method, 0) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }

            txtRemark.Text = this._StepEdit.Remark;
            if (_StepEdit.WorkType == eStepWorkType.UFArray)
            {
                grpStep.Text = "UF陣列";
            }
            else
            {
                grpStep.Text = "陣列";
            }

            cmbPattern.Items.Clear();
            if ((parent != null) && (parent.Parent != null) && (parent.Parent.Pattern != null))
            {
                if (parent.Parent.Pattern.Count > 0)
                {
                    foreach (string key in parent.Parent.Pattern.Keys)
                    {
                        if (key != parent.Name)
                        {
                            cmbPattern.Items.Add(key);
                        }
                    }
                }
            }

            ControlMisc.SetComboBox(ref cmbPattern, this._StepEdit.Array.Pattern, "Default");
            this._parentForm = parentForm;
            this._IsLoaded = true;
            return ErrorCode.Success;
        }

        ucWaferMapCenter _TeachCenter = new ucWaferMapCenter();
        ucWaferMapAngle _TeachAngle = new ucWaferMapAngle();
        ucWaferMapPitchSize _TeachPitchSize = new ucWaferMapPitchSize();
        ucWaferMapAdjust _TeachAdjust = new ucWaferMapAdjust();

        public ucRecipeArray(ProjectCore.MSystemParameter.sSysParam sys)
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
            cmbRoute.Items.Clear();
            cmbRoute.Items.AddRange(new object[] { "S-Type", "Z-Type", "S-Rev", "Z-Rev" });

            this.Controls.Add(_TeachCenter);
            this.Controls.Add(_TeachAngle);
            this.Controls.Add(_TeachPitchSize);
            this.Controls.Add(_TeachAdjust);
            _TeachAdjust.ValueChanged += _TeachAdjust_ValueChanged;
            btnCenter_Click(null, null);
            this._IsLoaded = true;
        }

        #region "資料變更"
        private void _TeachAdjust_ValueChanged(object sender, EventArgs e)
        {
            this._StepEdit.Array.ACount = (int)_TeachAdjust.nmuCountX.Value;
            this._StepEdit.Array.BCount = (int)_TeachAdjust.nmuCountY.Value;
        }
        private void txtRemark_TextChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Remark = txtRemark.Text;
        }

      

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Type = cmbType.SelectedItem.ToString();
        }

        private void cmbRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Array.Method = (RouteMethod)cmbRoute.SelectedIndex;
        }

        private void cmbPattern_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Array.Pattern = cmbPattern.SelectedItem.ToString();
        }

        #endregion


        private void btnEdit_Click(object sender, EventArgs e)
        {
            ((frmRecipe)this._parentForm).ShowStepPrameter(this.StepGroup);
        }

        int _PageNo = 0;

        private void btnCenter_Click(object sender, EventArgs e)
        {
            _TeachCenter.Sys = this._sys;
            _TeachCenter.Motion = this.Motion;
            _TeachCenter.Width = 800;
            _TeachCenter.Height = 600;
            _TeachCenter.Location = new Point(6, 0);
            _TeachCenter.Visible = true;
            _TeachCenter.BringToFront();

            _TeachCenter.ShowProperty();
            _TeachCenter.UpdateUI();
            _PageNo = 0;
            btnPrev.Visible = false;
            btnNext.Visible = true;
        }

        private void btnAngle_Click(object sender, EventArgs e)
        {
            _TeachAngle.Sys = this._sys;
            _TeachAngle.Motion = this.Motion;
            _TeachAngle.Width = 800;
            _TeachAngle.Height = 600;
            _TeachAngle.Location = new Point(6, 0);
            _TeachAngle.Visible = true;
            _TeachAngle.BringToFront();

            _TeachAngle.ShowProperty();
            _TeachAngle.UpdateUI();
            _PageNo = 1;
            btnPrev.Visible = true;
            btnNext.Visible = true;
        }

        private void btnPitch_Click(object sender, EventArgs e)
        {
            _TeachPitchSize.Sys = this._sys;
            _TeachPitchSize.Motion = this.Motion;
            _TeachPitchSize.Width = 800;
            _TeachPitchSize.Height = 600;
            _TeachPitchSize.Location = new Point(6, 0);
            _TeachPitchSize.Visible = true;
            _TeachPitchSize.BringToFront();

            _TeachPitchSize.WaferAngle = _TeachAngle.WaferAngle;

            _TeachPitchSize.ShowProperty();
            _TeachPitchSize.UpdateUI();
            _PageNo = 2;
            btnPrev.Visible = true;
            btnNext.Visible = true;
        }

        private void btnCount_Click(object sender, EventArgs e)
        {
            //_TeachAdjust
            _TeachAdjust.Width = 800;
            _TeachAdjust.Height = 600;
            _TeachAdjust.Location = new Point(6, 0);
            _TeachAdjust.Visible = true;
            _TeachAdjust.BringToFront();
            _TeachAdjust.PitchX = _TeachPitchSize.PitchX;
            _TeachAdjust.PitchY = _TeachPitchSize.PitchY;
            _TeachAdjust.ShowProperty();
            _TeachAdjust.UpdateUI();
            _PageNo = 3;
            btnPrev.Visible = true;
            btnNext.Visible = true;
        }
       
        private void btnOther_Click(object sender, EventArgs e)
        {
            _TeachAngle.Visible = false;
            _TeachCenter.Visible = false;
            _TeachPitchSize.Visible = false;
            _TeachAdjust.Visible = false;
            _PageNo = 4;
            btnPrev.Visible = true;
            btnNext.Visible = false;
        }

        private void cmbMAP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            //this._StepEdit.Array.Method = cmbMAP.SelectedItem.ToString();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            switch (_PageNo)
            {
                case 1:
                    btnCenter_Click(sender, e);
                    break;
                case 2:
                    btnAngle_Click(sender, e);
                    break;
                case 3:
                    btnPitch_Click(sender, e);
                    break;
                case 4:
                    btnCount_Click(sender, e);
                    break;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (_PageNo)
            {
                case 0:
                    btnAngle_Click(sender, e);
                    break;
                case 1:
                    btnPitch_Click(sender, e);
                    break;
                case 2:
                    btnCount_Click(sender, e);
                    break;
                case 3:
                    btnOther_Click(sender, e);
                    break;
            }
        }

    }
}
