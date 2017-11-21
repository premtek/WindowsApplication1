using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Premtek
{
    public partial class ucRecipeArc : UserControl
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
            if (ControlMisc.SetNumericValue(ref this.nmuStartX, this._StepEdit.Arc.Start.X) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this.nmuStartY, this._StepEdit.Arc.Start.Y) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
           if( ControlMisc.SetNumericValue(ref this.nmuStartZ, this._StepEdit.Arc.Start.Z) != ErrorCode.Success)
           {
               this._StepEdit.IsCorrect = false;
           }
           if( ControlMisc.SetNumericValue(ref this.nmuEndX, this._StepEdit.Arc.End.X)!=    ErrorCode.Success)
           {
               this._StepEdit.IsCorrect = false;
           }
            if (ControlMisc.SetNumericValue(ref this.nmuEndY, this._StepEdit.Arc.End.Y)!=    ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this.nmuEndZ, this._StepEdit.Arc.End.Z)!=   ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this.nmuCenterX, this._StepEdit.Arc.Center.X)!=  ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
           if ( ControlMisc.SetNumericValue(ref this.nmuCenterY, this._StepEdit.Arc.Center.Y)!= ErrorCode.Success)
           {
               this._StepEdit.IsCorrect = false;
           }
            if (ControlMisc.SetNumericValue(ref this.nmuCenterZ, this._StepEdit.Arc.Center.Z)!= ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this.nmuMiddleX, this._StepEdit.Arc.Middle.X)!= ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this.nmuMiddleY, this._StepEdit.Arc.Middle.Y)!= ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this.nmuMiddleZ, this._StepEdit.Arc.Middle.Z)!= ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }

           if ( ControlMisc.SetNumericValue(ref this.nmuAngle, this._StepEdit.Arc.Angle)!= ErrorCode.Success)
           {
               this._StepEdit.IsCorrect = false;
           }
            if (ControlMisc.SetNumericValue(ref this.nmuDotCount, this._StepEdit.Arc.DotCount)!= ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this.nmuVelocity, this._StepEdit.Arc.Velocity)!= ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref this.nmuWeight, this._StepEdit.Arc.Weight)!= ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetComboBox(ref cmbType, this._StepEdit.Type, "Default")!= ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetComboBox(ref cmbMethod, (int)this._StepEdit.Arc.Method, 0) !=   ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            if (ControlMisc.SetComboBox(ref cmbDirection, (int)this._StepEdit.Arc.Direction, 0) != ErrorCode.Success)
            {
                this._StepEdit.IsCorrect = false;
            }
            txtRemark.Text = this._StepEdit.Remark;
            this._parentForm = parentForm;
            RefreshUI();
            this._IsLoaded = true;
            return ErrorCode.Success;
        }

        void UpdateMaxMin()
        {
            if (_sys == null)
            { return; }
            if (Motion == null)
            {
                return;
            }
            nmuStartX.Maximum = Motion.AxisParameter[_sys.AxisX].Limit.PosivtiveLimit;
            nmuMiddleX.Maximum = Motion.AxisParameter[_sys.AxisX].Limit.PosivtiveLimit;
            nmuEndX.Maximum = Motion.AxisParameter[_sys.AxisX].Limit.PosivtiveLimit;
            nmuCenterX.Maximum = Motion.AxisParameter[_sys.AxisX].Limit.PosivtiveLimit;

            nmuStartX.Minimum = Motion.AxisParameter[_sys.AxisX].Limit.NegativeLimit;
            nmuMiddleX.Minimum = Motion.AxisParameter[_sys.AxisX].Limit.NegativeLimit;
            nmuEndX.Minimum = Motion.AxisParameter[_sys.AxisX].Limit.NegativeLimit;
            nmuCenterX.Minimum = Motion.AxisParameter[_sys.AxisX].Limit.NegativeLimit;

            nmuStartY.Maximum = Motion.AxisParameter[_sys.AxisY].Limit.PosivtiveLimit;
            nmuMiddleY.Maximum = Motion.AxisParameter[_sys.AxisY].Limit.PosivtiveLimit;
            nmuEndY.Maximum = Motion.AxisParameter[_sys.AxisY].Limit.PosivtiveLimit;
            nmuCenterY.Maximum = Motion.AxisParameter[_sys.AxisY].Limit.PosivtiveLimit;

            nmuStartY.Minimum = Motion.AxisParameter[_sys.AxisY].Limit.NegativeLimit;
            nmuMiddleY.Minimum = Motion.AxisParameter[_sys.AxisY].Limit.NegativeLimit;
            nmuEndY.Minimum = Motion.AxisParameter[_sys.AxisY].Limit.NegativeLimit;
            nmuCenterY.Minimum = Motion.AxisParameter[_sys.AxisY].Limit.NegativeLimit;

            nmuStartZ.Maximum = Motion.AxisParameter[_sys.AxisZ].Limit.PosivtiveLimit;
            nmuMiddleZ.Maximum = Motion.AxisParameter[_sys.AxisZ].Limit.PosivtiveLimit;
            nmuEndZ.Maximum = Motion.AxisParameter[_sys.AxisZ].Limit.PosivtiveLimit;
            nmuCenterZ.Maximum = Motion.AxisParameter[_sys.AxisZ].Limit.PosivtiveLimit;

            nmuStartZ.Minimum = Motion.AxisParameter[_sys.AxisZ].Limit.NegativeLimit;
            nmuMiddleZ.Minimum = Motion.AxisParameter[_sys.AxisZ].Limit.NegativeLimit;
            nmuEndZ.Minimum = Motion.AxisParameter[_sys.AxisZ].Limit.NegativeLimit;
            nmuCenterZ.Minimum = Motion.AxisParameter[_sys.AxisZ].Limit.NegativeLimit;
        }


        public ucRecipeArc(ProjectCore.MSystemParameter.sSysParam sys)
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
            cmbMethod.Items.Clear();
            cmbMethod.Items.AddRange(new object[] { "3-Points", "Center" });
            cmbDirection.Items.Clear();
            cmbDirection.Items.AddRange(new object[] { "CW", "CCW" });
            this._IsLoaded = true;
        }

        /// <summary>三點/圓心設定介面切換
        /// </summary>
        void RefreshUI()
        {
            double _angle =0;//圓心角
            _StepEdit.IsCorrect = true;
            switch (this._StepEdit.Arc.Method)
            {
                case ArcCircleMethod.By3Pts:
                    grpArcCenter.Enabled = false;
                    grpArcMiddle.Enabled = true;
                    grpArcEnd.Enabled = true;
                    cmbDirection.Enabled = false; //三點定弧, 起終點已知
                    decimal Xc, Yc;
                    if (CMath.GetCircumCenter(nmuStartX.Value, nmuStartY.Value, nmuEndX.Value, nmuEndY.Value, nmuMiddleX.Value, nmuMiddleY.Value, out Xc, out Yc) != ErrorCode.Success)//三點求圓心
                    {
                        nmuStartX.BackColor = Color.Red;
                        nmuStartY.BackColor = Color.Red;
                        nmuEndX.BackColor = Color.Red;
                        nmuEndY.BackColor = Color.Red;
                        nmuCenterX.BackColor = SystemColors.Window;//三點與圓心無關
                        nmuCenterY.BackColor = SystemColors.Window;
                        nmuMiddleX.BackColor = Color.Red;
                        nmuMiddleY.BackColor = Color.Red;
                        _StepEdit.IsCorrect = false;
                        return;//設定異常, 無法繼續推算.
                    }

                    nmuCenterX.Value = Xc;
                    nmuCenterY.Value = Yc;
                    ArcCircleDirection _direction = CMath.GetArcDirection(this.nmuCenterX.Value, this.nmuCenterY.Value, this.nmuStartX.Value, this.nmuStartY.Value, this.nmuEndX.Value, this.nmuEndY.Value);//圓心計算方向
                    cmbDirection.SelectedIndex = (int)_direction;

                    _angle = CMath.GetCentralAngle(nmuCenterX.Value, nmuCenterY.Value, nmuStartX.Value, nmuStartY.Value, nmuEndX.Value, nmuEndY.Value);//圓心角
                    if (double.IsNaN(_angle))
                    {
                        nmuStartX.BackColor = Color.Red;
                        nmuStartY.BackColor = Color.Red;
                        nmuEndX.BackColor = Color.Red;
                        nmuEndY.BackColor = Color.Red;
                        nmuCenterX.BackColor = SystemColors.Window;
                        nmuCenterY.BackColor = SystemColors.Window;
                        nmuMiddleX.BackColor = Color.Red;
                        nmuMiddleY.BackColor = Color.Red;
                        _StepEdit.IsCorrect = false;
                        return;//設定異常, 無法繼續推算.
                    }
                    else
                    {
                        nmuAngle.Value = (decimal)_angle;
                        nmuStartX.BackColor = SystemColors.Window;
                        nmuStartY.BackColor = SystemColors.Window;
                        nmuEndX.BackColor = SystemColors.Window;
                        nmuEndY.BackColor = SystemColors.Window;
                        nmuCenterX.BackColor = SystemColors.Window;
                        nmuCenterY.BackColor = SystemColors.Window;
                        nmuMiddleX.BackColor = SystemColors.Window;
                        nmuMiddleY.BackColor = SystemColors.Window;
                    }

                    break;
                case ArcCircleMethod.ByCenter:
                    grpArcCenter.Enabled = true;
                    grpArcMiddle.Enabled = false;
                    grpArcEnd.Enabled = true;
                    _angle = CMath.GetCentralAngle(nmuCenterX.Value, nmuCenterY.Value, nmuStartX.Value, nmuStartY.Value, nmuEndX.Value, nmuEndY.Value);//圓心角
                    if (double.IsNaN(_angle))
                    {
                        nmuStartX.BackColor = Color.Red;
                        nmuStartY.BackColor = Color.Red;
                        nmuEndX.BackColor = Color.Red;
                        nmuEndY.BackColor = Color.Red;
                        nmuCenterX.BackColor = Color.Red;
                        nmuCenterY.BackColor = Color.Red;
                        nmuMiddleX.BackColor = SystemColors.Window;
                        nmuMiddleY.BackColor = SystemColors.Window;
                        _StepEdit.IsCorrect = false;
                        return;//設定異常, 無法繼續推算.
                    }
                    else
                    {
                        nmuAngle.Value = (decimal)_angle;
                        nmuStartX.BackColor = SystemColors.Window;
                        nmuStartY.BackColor = SystemColors.Window;
                        nmuEndX.BackColor = SystemColors.Window;
                        nmuEndY.BackColor = SystemColors.Window;
                        nmuCenterX.BackColor = SystemColors.Window;
                        nmuCenterY.BackColor = SystemColors.Window;
                        nmuMiddleX.BackColor = SystemColors.Window;
                        nmuMiddleY.BackColor = SystemColors.Window;
                    }

                    break;
            }
            decimal _radius = CMath.GetDistance(nmuStartX.Value, nmuStartY.Value, nmuCenterX.Value, nmuCenterY.Value);
            decimal _length = (decimal)Math.PI * _radius * (decimal)_angle / 180;
            decimal _acc = 9800;
            decimal _cycleTime = 0.002M;
            decimal _avgWeight = 0.01M;
            int dotmin, dotmax;
            if (CRecipeStepArcLimit.GetDotLimit(_length, _radius, _acc, _cycleTime, out dotmin, out dotmax) == ErrorCode.Success)
            {
                nmuDotCount.Minimum = dotmin;
                nmuDotCount.Maximum = dotmax;
            }
            decimal velmin, velmax;
            if (CRecipeStepArcLimit.GetVelLimit(_length, _radius, _acc, _cycleTime, out velmin, out velmax) == ErrorCode.Success)
            {
                nmuVelocity.Minimum = velmin;
                nmuVelocity.Maximum = velmax;
            }
           
            decimal wmin, wmax;
            if (CRecipeStepArcLimit.GetWeightLimit(_length, _radius, _acc, _cycleTime, _avgWeight, out wmin, out wmax) == ErrorCode.Success)
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
                txtPitch.Text = (_length / (nmuDotCount.Value - 1)).ToString("0.000");
            }
            
        }
        #region "資料變更"

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Type = cmbType.SelectedItem.ToString();
        }

        private void cmbMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Arc.Method = (ArcCircleMethod)cmbMethod.SelectedIndex;
            RefreshUI();
        }

        private void cmbDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Arc.Direction = (ArcCircleDirection)cmbDirection.SelectedIndex;
        }

        private void nmuAngle_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Arc.Angle = nmuAngle.Value;
        }

        private void nmuVelocity_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Arc.Velocity = nmuVelocity.Value;
            RefreshUI();
        }

        private void nmuDotCount_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Arc.DotCount = (int)nmuDotCount.Value;
            RefreshUI();
        }

        private void nmuWeight_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Arc.Weight = nmuWeight.Value;
            RefreshUI();
        }

        private void nmuStartX_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Arc.Start.X = nmuStartX.Value;
            RefreshUI();
        }

        private void nmuStartY_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Arc.Start.Y = nmuStartY.Value;
            RefreshUI();
        }

        private void nmuStartZ_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Arc.Start.Z = nmuStartZ.Value;
        }

        private void nmuEndX_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Arc.End.X = nmuEndX.Value;
            RefreshUI();
        }

        private void nmuEndY_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Arc.End.Y = nmuEndY.Value;
            RefreshUI();
        }

        private void nmuEndZ_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Arc.End.Z = nmuEndZ.Value;
        }

        private void nmuMiddleX_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Arc.Middle.X = nmuMiddleX.Value;
            RefreshUI();
        }

        private void nmuMiddleY_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Arc.Middle.Y = nmuMiddleY.Value;
            RefreshUI();
        }

        private void nmuMiddleZ_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Arc.Middle.Z = nmuMiddleZ.Value;
        }

        private void nmuCenterX_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Arc.Center.X = nmuCenterX.Value;
            RefreshUI();
        }

        private void nmuCenterY_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Arc.Center.Y = nmuCenterY.Value;
            RefreshUI();
        }

        private void nmuCenterZ_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Arc.Center.Z = nmuCenterZ.Value;
        }
        private void txtRemark_TextChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            this._StepEdit.Remark = txtRemark.Text;
        }
        #endregion

        private void btnSetStart_Click(object sender, EventArgs e)
        {
            decimal _PosX, _PosY, _PosZ;
            decimal.TryParse(Motion.GetPositionValue(_sys.AxisX), out _PosX);
            decimal.TryParse(Motion.GetPositionValue(_sys.AxisY), out _PosY);
            decimal.TryParse(Motion.GetPositionValue(_sys.AxisZ), out _PosZ);
            nmuStartX.Value = _PosX;
            nmuStartY.Value = _PosY;
            nmuStartZ.Value = _PosZ;

        }

        private void btnSetEnd_Click(object sender, EventArgs e)
        {
            decimal _PosX, _PosY, _PosZ;
            decimal.TryParse(Motion.GetPositionValue(_sys.AxisX), out _PosX);
            decimal.TryParse(Motion.GetPositionValue(_sys.AxisY), out _PosY);
            decimal.TryParse(Motion.GetPositionValue(_sys.AxisZ), out _PosZ);
            nmuEndX.Value = _PosX;
            nmuEndY.Value = _PosY;
            nmuEndZ.Value = _PosZ;

        }

        private void btnSetMiddle_Click(object sender, EventArgs e)
        {
            decimal _PosX, _PosY, _PosZ;
            decimal.TryParse(Motion.GetPositionValue(_sys.AxisX), out _PosX);
            decimal.TryParse(Motion.GetPositionValue(_sys.AxisY), out _PosY);
            decimal.TryParse(Motion.GetPositionValue(_sys.AxisZ), out _PosZ);
            nmuMiddleX.Value = _PosX;
            nmuMiddleY.Value = _PosY;
            nmuMiddleZ.Value = _PosZ;

        }

        private void btnSetCenter_Click(object sender, EventArgs e)
        {
            decimal _PosX, _PosY, _PosZ;
            decimal.TryParse(Motion.GetPositionValue(_sys.AxisX), out _PosX);
            decimal.TryParse(Motion.GetPositionValue(_sys.AxisY), out _PosY);
            decimal.TryParse(Motion.GetPositionValue(_sys.AxisZ), out _PosZ);
            nmuCenterX.Value = _PosX;
            nmuCenterY.Value = _PosY;
            nmuCenterZ.Value = _PosZ;

        }

        private void btnGoStart_Click(object sender, EventArgs e)
        {
            decimal[] _pos = new decimal[] { nmuStartX.Value, nmuStartY.Value, nmuStartZ.Value, 0, 0, 0 };
            _sys.EsysNum = 1000;
            do
            {
                if (CMotionMisc.SafeMovePos(_sys, _pos) == ErrorCode.Success)
                    break;
            } while (true);
        }

        private void btnGoEnd_Click(object sender, EventArgs e)
        {
            decimal[] _pos = new decimal[] { nmuEndX.Value, nmuEndY.Value, nmuEndZ.Value, 0, 0, 0 };
            _sys.EsysNum = 1000;
            do
            {
                if (CMotionMisc.SafeMovePos(_sys, _pos) == ErrorCode.Success)
                    break;
            } while (true);
        }

        private void btnGoMiddle_Click(object sender, EventArgs e)
        {
            decimal[] _pos = new decimal[] { nmuMiddleX.Value, nmuMiddleY.Value, nmuMiddleZ.Value, 0, 0, 0 };
            _sys.EsysNum = 1000;
            do
            {
                if (CMotionMisc.SafeMovePos(_sys, _pos) == ErrorCode.Success)
                    break;
            } while (true);
        }

        private void btnGoCenter_Click(object sender, EventArgs e)
        {
            decimal[] _pos = new decimal[] { nmuCenterX.Value, nmuCenterY.Value, nmuCenterZ.Value, 0, 0, 0 };
            _sys.EsysNum = 1000;
            do
            {
                if (CMotionMisc.SafeMovePos(_sys, _pos) == ErrorCode.Success)
                    break;
            } while (true);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ((frmRecipe)this._parentForm).ShowStepPrameter(this.StepGroup);
        }

    }
}
