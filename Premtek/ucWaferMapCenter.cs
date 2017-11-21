using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectCore;

namespace Premtek
{
    public partial class ucWaferMapCenter : UserControl
    {
        /// <summary>外部配接Sys
        /// </summary>
        public MSystemParameter.sSysParam Sys;
        /// <summary>外部配接軸卡
        /// </summary>
        public Premtek.Base.CMotionCollection Motion;
        /// <summary>位置移動開始
        /// </summary>
        public event EventHandler MoveStart;

        public ucWaferMapCenter()
        {
            InitializeComponent();

        }

        /// <summary>位置變更事件
        /// </summary>
        public event EventHandler PosChanged;

        #region "內部變數"

        /// <summary>Wafer圓心
        /// </summary>
        decimal mCenterX;
        /// <summary>Wafer圓心
        /// </summary>
        decimal mCenterY;

        /// <summary>Wafer邊緣半徑
        /// </summary>
        decimal mWaferRadius;
        /// <summary>Wafer有效半徑
        /// </summary>
        decimal mWaferEffetiveRadius;

        /// <summary>是否介面更新中
        /// </summary>
        bool mIsUpdating = false;
        #endregion

        #region "公開屬性"

        /// <summary>Wafer邊緣 第一點
        /// </summary>
        public decimal PosX1 { get; set; }
        /// <summary>Wafer邊緣 第一點
        /// </summary>
        public decimal PosY1 { get; set; }
        /// <summary>Wafer邊緣 第一點
        /// </summary>
        public decimal PosZ1 { get; set; }
        /// <summary>Wafer邊緣 第二點
        /// </summary>
        public decimal PosX2 { get; set; }
        /// <summary>Wafer邊緣 第二點
        /// </summary>
        public decimal PosY2 { get; set; }
        /// <summary>Wafer邊緣 第二點
        /// </summary>
        public decimal PosZ2 { get; set; }
        /// <summary>Wafer邊緣 第三點
        /// </summary>
        public decimal PosX3 { get; set; }
        /// <summary>Wafer邊緣 第三點
        /// </summary>
        public decimal PosY3 { get; set; }
        /// <summary>Wafer邊緣 第三點
        /// </summary>
        public decimal PosZ3 { get; set; }
        /// <summary>邊框寬度
        /// </summary>
        public decimal Boundary { get; set; }
        /// <summary>圓心
        /// </summary>
        public decimal CenterX
        {
            get
            {
                return mCenterX;
            }
        }
        /// <summary>圓心
        /// </summary>
        public decimal CenterY
        {
            get
            {
                return mCenterY;
            }
        }
        /// <summary>Wafer外緣半徑
        /// </summary>
        public decimal WaferRadius
        {
            get
            {
                return mWaferRadius;
            }
        }
        /// <summary>Wafer有效半徑
        /// </summary>
        public decimal WaferEffectiveRadius
        {
            get
            {
                return mWaferEffetiveRadius;
            }
        }
        bool _IsCorrect;
        /// <summary>資料正確
        /// </summary>
        public bool IsCorrect
        {
            get
            {
                return _IsCorrect;
            }
        }
        #endregion

        /// <summary>將屬性套用至介面
        /// </summary>
        public void ShowProperty()
        {
            mIsUpdating = true;
            UpdateMaxMin();
            CMotionMisc.Motion = Motion;
            _IsCorrect = true;//預設正確, 開始檢查
            if (ControlMisc.SetNumericValue(ref nmuPosX1, PosX1) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuPosY1, PosY1) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuPosZ1, PosZ1) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuPosX2, PosX2) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuPosY2, PosY2) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuPosZ2, PosZ2) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuPosX3, PosX3) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuPosY3, PosY3) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuPosZ3, PosZ3) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuBoundary, Boundary) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            mIsUpdating = false;
        }
        void UpdateMaxMin()
        {
            if (Sys == null)
            { return; }
            if (Motion == null)
            {
                return;
            }
            nmuPosX1.Maximum = Motion.AxisParameter[Sys.AxisX].Limit.PosivtiveLimit;
            nmuPosX2.Maximum = Motion.AxisParameter[Sys.AxisX].Limit.PosivtiveLimit;
            nmuPosX3.Maximum = Motion.AxisParameter[Sys.AxisX].Limit.PosivtiveLimit;
            nmuPosX1.Minimum = Motion.AxisParameter[Sys.AxisX].Limit.NegativeLimit;
            nmuPosX2.Minimum = Motion.AxisParameter[Sys.AxisX].Limit.NegativeLimit;
            nmuPosX3.Minimum = Motion.AxisParameter[Sys.AxisX].Limit.NegativeLimit;
            nmuPosY1.Maximum = Motion.AxisParameter[Sys.AxisY].Limit.PosivtiveLimit;
            nmuPosY2.Maximum = Motion.AxisParameter[Sys.AxisY].Limit.PosivtiveLimit;
            nmuPosY3.Maximum = Motion.AxisParameter[Sys.AxisY].Limit.PosivtiveLimit;
            nmuPosY1.Minimum = Motion.AxisParameter[Sys.AxisY].Limit.NegativeLimit;
            nmuPosY2.Minimum = Motion.AxisParameter[Sys.AxisY].Limit.NegativeLimit;
            nmuPosY3.Minimum = Motion.AxisParameter[Sys.AxisY].Limit.NegativeLimit;
            nmuPosZ1.Maximum = Motion.AxisParameter[Sys.AxisZ].Limit.PosivtiveLimit;
            nmuPosZ2.Maximum = Motion.AxisParameter[Sys.AxisZ].Limit.PosivtiveLimit;
            nmuPosZ3.Maximum = Motion.AxisParameter[Sys.AxisZ].Limit.PosivtiveLimit;
            nmuPosZ1.Minimum = Motion.AxisParameter[Sys.AxisZ].Limit.NegativeLimit;
            nmuPosZ2.Minimum = Motion.AxisParameter[Sys.AxisZ].Limit.NegativeLimit;
            nmuPosZ3.Minimum = Motion.AxisParameter[Sys.AxisZ].Limit.NegativeLimit;
        }
        /// <summary>介面更新
        /// </summary>
        public void UpdateUI()
        {
            if (mIsUpdating)
            {
                return;
            }
            mIsUpdating = true;
            UpdateMaxMin();
            PosX1 = nmuPosX1.Value;
            PosY1 = nmuPosY1.Value;
            PosZ1 = nmuPosZ1.Value;
            PosX2 = nmuPosX2.Value;
            PosY2 = nmuPosY2.Value;
            PosZ2 = nmuPosZ2.Value;
            PosX3 = nmuPosX3.Value;
            PosY3 = nmuPosY3.Value;
            PosZ3 = nmuPosZ3.Value;
            Boundary = nmuBoundary.Value;
            _IsCorrect = true;
            if (CMath.GetCircumCenter(PosX1, PosY1, PosX2, PosY2, PosX3, PosY3, out mCenterX, out mCenterY) != ErrorCode.Success)
            {
                txtCenterX.BackColor = Color.Red;
                txtCenterY.BackColor = Color.Red;
                mIsUpdating = false;
                _IsCorrect = false;
                return;
            }
            txtCenterX.BackColor = Color.White;
            txtCenterY.BackColor = Color.White;
            txtCenterX.Text = mCenterX.ToString("0.###");
            txtCenterY.Text = mCenterY.ToString("0.###");
            mWaferRadius = Premtek.CMath.GetDistance(PosX1, PosY1, mCenterX, mCenterY);

            if (mWaferRadius < nmuBoundary.Value)
            {
                nmuBoundary.BackColor = Color.Red;
                _IsCorrect = false;
                mIsUpdating = false;
                return;
            }

            mWaferEffetiveRadius = mWaferRadius - nmuBoundary.Value;
            lblWaferRadius.Text = "R= " + mWaferRadius.ToString("0.###") + " mm";
            lblWaferEffectiveRadius.Text = "R'= " + mWaferEffetiveRadius.ToString("0.###") + " mm";
            mIsUpdating = false;
        }

        void ButtonLock(bool isEnabled)
        {
            btnSetPos1.Enabled = isEnabled;
            btnGoPos1.Enabled = isEnabled;
            btnSetPos2.Enabled = isEnabled;
            btnGoPos2.Enabled = isEnabled;
            btnSetPos3.Enabled = isEnabled;
            btnGoPos3.Enabled = isEnabled;
            btnAutoFocus.Enabled = isEnabled;
        }


        private void btnSetPos1_Click(object sender, EventArgs e)
        {
            if (Sys == null)
            {
                System.Diagnostics.Debug.Assert(false);
                return;
            }
            if (Motion == null)
            {
                System.Diagnostics.Debug.Assert(false);
                return;
            }
            nmuPosX1.Value = Convert.ToDecimal(Motion.GetPositionValue(Sys.AxisX));
            nmuPosY1.Value = Convert.ToDecimal(Motion.GetPositionValue(Sys.AxisY));
            nmuPosZ1.Value = Convert.ToDecimal(Motion.GetPositionValue(Sys.AxisZ));

        }

        private void btnSetPos2_Click(object sender, EventArgs e)
        {
            if (Sys == null)
            {
                System.Diagnostics.Debug.Assert(false);
                return;
            }
            if (Motion == null)
            {
                System.Diagnostics.Debug.Assert(false);
                return;
            }
            nmuPosX2.Value = Convert.ToDecimal(Motion.GetPositionValue(Sys.AxisX));
            nmuPosY2.Value = Convert.ToDecimal(Motion.GetPositionValue(Sys.AxisY));
            nmuPosZ2.Value = Convert.ToDecimal(Motion.GetPositionValue(Sys.AxisZ));
        }

        private void btnSetPos3_Click(object sender, EventArgs e)
        {
            if (Sys == null)
            {
                System.Diagnostics.Debug.Assert(false);
                return;
            }
            if (Motion == null)
            {
                System.Diagnostics.Debug.Assert(false);
                return;
            }
            nmuPosX3.Value = Convert.ToDecimal(Motion.GetPositionValue(Sys.AxisX));
            nmuPosY3.Value = Convert.ToDecimal(Motion.GetPositionValue(Sys.AxisY));
            nmuPosZ3.Value = Convert.ToDecimal(Motion.GetPositionValue(Sys.AxisZ));
        }

        private void nmuPosX1_ValueChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        public void SetCenterStatus(bool value)
        {
            if (value)
            {
                txtCenterX.BackColor = Color.White;
                txtCenterY.BackColor = Color.White;
            }
            else
            {
                txtCenterX.BackColor = Color.Red;
                txtCenterY.BackColor = Color.Red;
            }
        }

        private void btnGoPos1_Click(object sender, EventArgs e)
        {
            decimal[] _Pos = new decimal[] { nmuPosX1.Value, nmuPosY1.Value, nmuPosZ1.Value, 0, 0, 0 };

            MSystemParameter.sSysParam _Sys = new MSystemParameter.sSysParam();
            _Sys.AxisX = Sys.AxisX;
            _Sys.AxisY = Sys.AxisY;
            _Sys.AxisZ = Sys.AxisZ;
            _Sys.AxisA = Sys.AxisA;
            _Sys.AxisB = Sys.AxisB;
            _Sys.AxisC = Sys.AxisC;

            _Sys.SysNum = 1000;
            _Sys.RunStatus = MSystemParameter.enmRunStatus.Running;
            if (MoveStart != null)
            {
                MoveStart(sender, e);
            }
            Task.Run(new Action(() =>
            {
                btnGoPos1.BeginInvoke(new Action(() =>
                {
                    btnGoPos1.BackColor = Color.Yellow;
                    ButtonLock(false);
                }));
                ErrorCode ret = ErrorCode.Running;
                do
                {
                    ret = CMotionMisc.Move3DPos(_Sys, _Pos);
                } while (ret == ErrorCode.Running);
                switch (_Sys.RunStatus)
                {
                    case MSystemParameter.enmRunStatus.Finish:
                        btnGoPos1.BeginInvoke(new Action(() =>
                        {
                            btnGoPos1.BackColor = SystemColors.Control;
                            btnGoPos1.UseVisualStyleBackColor = true;
                            ButtonLock(true);
                        }));
                        break;
                    case MSystemParameter.enmRunStatus.Alarm:
                        btnGoPos1.BeginInvoke(new Action(() =>
                        {
                            btnGoPos1.BackColor = Color.Red;
                            btnGoPos1.UseVisualStyleBackColor = true;
                            ButtonLock(true);
                        }));
                        break;
                }
                if (PosChanged != null)
                {
                    PosChanged(sender, e);
                }
            }));

        }

        private void btnGoPos2_Click(object sender, EventArgs e)
        {
            decimal[] _Pos = new decimal[] { nmuPosX2.Value, nmuPosY2.Value, nmuPosZ2.Value, 0, 0, 0 };
            MSystemParameter.sSysParam _Sys = new MSystemParameter.sSysParam();
            _Sys.AxisX = Sys.AxisX;
            _Sys.AxisY = Sys.AxisY;
            _Sys.AxisZ = Sys.AxisZ;
            _Sys.AxisA = Sys.AxisA;
            _Sys.AxisB = Sys.AxisB;
            _Sys.AxisC = Sys.AxisC;

            _Sys.SysNum = 1000;
            _Sys.RunStatus = MSystemParameter.enmRunStatus.Running;
            Task.Run(new Action(() =>
            {
                if (MoveStart != null)
                {
                    MoveStart(sender, e);
                }
                btnGoPos2.BeginInvoke(new Action(() =>
                {
                    btnGoPos2.BackColor = Color.Yellow;
                }));
                ErrorCode ret = ErrorCode.Running;
                do
                {
                    ret = CMotionMisc.Move3DPos(_Sys, _Pos);
                } while (ret == ErrorCode.Running);
                switch (_Sys.RunStatus)
                {
                    case MSystemParameter.enmRunStatus.Finish:
                        btnGoPos2.BeginInvoke(new Action(() =>
                        {
                            btnGoPos2.BackColor = SystemColors.Control;
                            btnGoPos2.UseVisualStyleBackColor = true;
                        }));
                        break;
                    case MSystemParameter.enmRunStatus.Alarm:
                        btnGoPos2.BeginInvoke(new Action(() =>
                        {
                            btnGoPos2.BackColor = Color.Red;
                            btnGoPos2.UseVisualStyleBackColor = true;
                        }));
                        break;
                }
                if (PosChanged != null)
                {
                    PosChanged(sender, e);
                }
            }));

        }

        private void btnGoPos3_Click(object sender, EventArgs e)
        {
            decimal[] _Pos = new decimal[] { nmuPosX3.Value, nmuPosY3.Value, nmuPosZ3.Value, 0, 0, 0 };
            MSystemParameter.sSysParam _Sys = new MSystemParameter.sSysParam();
            _Sys.AxisX = Sys.AxisX;
            _Sys.AxisY = Sys.AxisY;
            _Sys.AxisZ = Sys.AxisZ;
            _Sys.AxisA = Sys.AxisA;
            _Sys.AxisB = Sys.AxisB;
            _Sys.AxisC = Sys.AxisC;

            _Sys.SysNum = 1000;
            _Sys.RunStatus = MSystemParameter.enmRunStatus.Running;
            
            Task.Run(new Action(() =>
            {
                if (MoveStart != null)
                {
                    MoveStart(sender, e);
                }
                btnGoPos3.BeginInvoke(new Action(() =>
                {
                    btnGoPos3.BackColor = Color.Yellow;
                }));
                ErrorCode ret = ErrorCode.Running;
                do
                {
                    ret = CMotionMisc.Move3DPos(_Sys, _Pos);
                } while (ret == ErrorCode.Running);
                switch (_Sys.RunStatus)
                {
                    case MSystemParameter.enmRunStatus.Finish:
                        btnGoPos3.BeginInvoke(new Action(() =>
                        {
                            btnGoPos3.BackColor = SystemColors.Control;
                            btnGoPos3.UseVisualStyleBackColor = true;
                        }));
                        break;
                    case MSystemParameter.enmRunStatus.Alarm:
                        btnGoPos3.BeginInvoke(new Action(() =>
                        {
                            btnGoPos3.BackColor = Color.Red;
                            btnGoPos3.UseVisualStyleBackColor = true;
                        }));
                        break;
                }
                if (PosChanged != null)
                {
                    PosChanged(sender, e);
                }
            }));
           
        }

    }

}
