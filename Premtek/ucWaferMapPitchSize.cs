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
    public partial class ucWaferMapPitchSize : UserControl
    {
        /// <summary>外部配接Sys
        /// </summary>
        public MSystemParameter.sSysParam Sys;
        /// <summary>外部配接軸卡
        /// </summary>
        public Premtek.Base.CMotionCollection Motion;
        /// <summary>位置變更事件
        /// </summary>
        public event EventHandler PosChanged;
        /// <summary>位置移動開始
        /// </summary>
        public event EventHandler MoveStart;

        public ucWaferMapPitchSize()
        {
            InitializeComponent();
        }


        /// <summary>Wafer間距X
        /// </summary>
        public decimal PitchX
        {
            get
            {
                return mPitchX;
            }
        }

        /// <summary>Wafer間距Y
        /// </summary>
        public decimal PitchY
        {
            get
            {
                return mPitchY;
            }
        }
        /// <summary>Wafer外緣決定的中心點座標
        /// </summary>
        public decimal WaferCenterX { get; set; }
        /// <summary>Wafer外緣決定的中心點座標
        /// </summary>
        public decimal WaferCenterY { get; set; }
        /// <summary>Wafer外緣決定的中心點座標
        /// </summary>
        public decimal WaferCenterZ { get; set; }

        /// <summary>Wafer有效圓內邊Die決定的中心點座標
        /// </summary>
        public decimal WaferCenterX2 { get; set; }
        /// <summary>Wafer有效圓內邊Die決定的中心點座標
        /// </summary>
        public decimal WaferCenterY2 { get; set; }
        /// <summary>Wafer有效圓內邊Die決定的中心點座標
        /// </summary>
        public decimal WaferCenterZ2 { get; set; }
        /// <summary>同列最左Die左下角位置
        /// </summary>
        public decimal LeftDiePosX { get; set; }
        /// <summary>同列最左Die左下角位置
        /// </summary>
        public decimal LeftDiePosY { get; set; }
        /// <summary>同列最左Die左下角位置
        /// </summary>
        public decimal LeftDiePosZ { get; set; }
        /// <summary>同列最右Die左下角位置
        /// </summary>
        public decimal RightDiePosX { get; set; }
        /// <summary>同列最右Die左下角位置
        /// </summary>
        public decimal RightDiePosY { get; set; }
        /// <summary>同列最右Die左下角位置
        /// </summary>
        public decimal RightDiePosZ { get; set; }
        /// <summary>同行最上Die左下角位置
        /// </summary>
        public decimal TopDiePosX { get; set; }
        /// <summary>同行最上Die左下角位置
        /// </summary>
        public decimal TopDiePosY { get; set; }
        /// <summary>同行最上Die左下角位置
        /// </summary>
        public decimal TopDiePosZ { get; set; }
        /// <summary>同行最下Die左下角位置
        /// </summary>
        public decimal BottomDiePosX { get; set; }
        /// <summary>同行最下Die左下角位置
        /// </summary>
        public decimal BottomDiePosY { get; set; }
        /// <summary>同行最下Die左下角位置
        /// </summary>
        public decimal BottomDiePosZ { get; set; }

        #region "內部變數"

        /// <summary>Wafer間距X
        /// </summary>
        decimal mPitchX;
        /// <summary>Wafer間距Y
        /// </summary>
        decimal mPitchY;
        /// <summary>是否介面更新中
        /// </summary>
        bool mIsUpdating = false;
        #endregion

        #region "公開屬性"

        /// <summary>Wafer傾斜角度
        /// </summary>
        public decimal WaferAngle { get; set; }
        /// <summary>元件左下角座標
        /// </summary>
        public decimal PosX1 { get; set; }
        /// <summary>元件左下角座標
        /// </summary>
        public decimal PosY1 { get; set; }
        /// <summary>元件左下角座標
        /// </summary>
        public decimal PosZ1 { get; set; }
        /// <summary>元件右上角座標
        /// </summary>
        public decimal PosX2 { get; set; }
        /// <summary>元件右上角座標
        /// </summary>
        public decimal PosY2 { get; set; }
        /// <summary>元件右上角座標
        /// </summary>
        public decimal PosZ2 { get; set; }
        /// <summary>元件尺寸X
        /// </summary>
        public decimal DieSizeX { get; set; }
        /// <summary>元件尺寸Y
        /// </summary>
        public decimal DieSizeY { get; set; }
        /// <summary>A方向臨近元件位置座標
        /// </summary>
        public decimal ADirectionX { get; set; }
        /// <summary>A方向臨近元件位置座標
        /// </summary>
        public decimal ADirectionY { get; set; }
        /// <summary>A方向臨近元件位置座標
        /// </summary>
        public decimal ADirectionZ { get; set; }
        /// <summary>B方向臨近元件位置座標
        /// </summary>
        public decimal BDirectionX { get; set; }
        /// <summary>B方向臨近元件位置座標
        /// </summary>
        public decimal BDirectionY { get; set; }
        /// <summary>B方向臨近元件位置座標
        /// </summary>
        public decimal BDirectionZ { get; set; }
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

        public void ShowProperty()
        {
            mIsUpdating = true;
            UpdateMaxMin();
            CMotionMisc.Motion = Motion;
            _IsCorrect = true;
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
            if (ControlMisc.SetNumericValue(ref nmuADirectionX, ADirectionX) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuADirectionY, ADirectionY) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuADirectionZ, ADirectionZ) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuBDirectionX, BDirectionX) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuBDirectionY, BDirectionY) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuBDirectionZ, BDirectionZ) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuPitchX, mPitchX) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuPitchY, mPitchY) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            txtCenterX.Text = WaferCenterX.ToString();
            txtCenterY.Text = WaferCenterY.ToString();
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

            nmuPosX1.Minimum = Motion.AxisParameter[Sys.AxisX].Limit.NegativeLimit;
            nmuPosX2.Minimum = Motion.AxisParameter[Sys.AxisX].Limit.NegativeLimit;

            nmuPosY1.Maximum = Motion.AxisParameter[Sys.AxisY].Limit.PosivtiveLimit;
            nmuPosY2.Maximum = Motion.AxisParameter[Sys.AxisY].Limit.PosivtiveLimit;

            nmuPosY1.Minimum = Motion.AxisParameter[Sys.AxisY].Limit.NegativeLimit;
            nmuPosY2.Minimum = Motion.AxisParameter[Sys.AxisY].Limit.NegativeLimit;

            nmuPosZ1.Maximum = Motion.AxisParameter[Sys.AxisZ].Limit.PosivtiveLimit;
            nmuPosZ2.Maximum = Motion.AxisParameter[Sys.AxisZ].Limit.PosivtiveLimit;

            nmuPosZ1.Minimum = Motion.AxisParameter[Sys.AxisZ].Limit.NegativeLimit;
            nmuPosZ2.Minimum = Motion.AxisParameter[Sys.AxisZ].Limit.NegativeLimit;

            nmuADirectionX.Maximum = Motion.AxisParameter[Sys.AxisX].Limit.PosivtiveLimit;
            nmuADirectionY.Maximum = Motion.AxisParameter[Sys.AxisY].Limit.PosivtiveLimit;
            nmuADirectionZ.Maximum = Motion.AxisParameter[Sys.AxisZ].Limit.PosivtiveLimit;
            nmuADirectionX.Minimum = Motion.AxisParameter[Sys.AxisX].Limit.NegativeLimit;
            nmuADirectionY.Minimum = Motion.AxisParameter[Sys.AxisY].Limit.NegativeLimit;
            nmuADirectionZ.Minimum = Motion.AxisParameter[Sys.AxisZ].Limit.NegativeLimit;

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
            _IsCorrect = true;
            PosX1 = nmuPosX1.Value;
            PosY1 = nmuPosY1.Value;
            PosZ1 = nmuPosZ1.Value;
            PosX2 = nmuPosX2.Value;
            PosY2 = nmuPosY2.Value;
            PosZ2 = nmuPosZ2.Value;

            ADirectionX = nmuADirectionX.Value;
            ADirectionY = nmuADirectionY.Value;
            ADirectionZ = nmuADirectionZ.Value;
            BDirectionX = nmuBDirectionX.Value;
            BDirectionY = nmuBDirectionY.Value;
            BDirectionZ = nmuBDirectionZ.Value;

            mPitchX = CMath.GetDistance(PosX1, PosY1, ADirectionX, ADirectionY);
            mPitchY = CMath.GetDistance(PosX1, PosY1, BDirectionX, BDirectionY);

            ControlMisc.SetNumericValue(ref nmuPitchX, mPitchX);
            ControlMisc.SetNumericValue(ref nmuPitchY, mPitchY);

            decimal mSlope = Convert.ToDecimal(Math.Tan(Convert.ToDouble(WaferAngle) * Math.PI / 180));//斜率 =角度取Tan 對邊/底邊
            //左上角點座標
            decimal mPosX3 = 0;
            decimal mPosY3 = 0;
            CMath.GetFootPoint(PosX1, PosY1, mSlope, PosX2, PosY2, out mPosX3, out mPosY3);//垂足公式
            //右下角點座標
            decimal mPosX4 = 0;
            decimal mPosY4 = 0;
            CMath.GetFootPoint(PosX2, PosY2, mSlope, PosX1, PosY1, out mPosX4, out mPosY4);//垂足公式
            DieSizeX = CMath.GetDistance(PosX1, PosY1, mPosX4, mPosY4);//左下-右下距離
            DieSizeY = CMath.GetDistance(PosX1, PosY1, mPosX3, mPosY3);//左下-左上距離

            WaferCenterX2 = (LeftDiePosX + RightDiePosX + DieSizeX) / 2;
            WaferCenterY2 = (TopDiePosY + BottomDiePosY + DieSizeY) / 2;
            txtWaferCenterX2.Text = WaferCenterX2.ToString("#.000");
            txtWaferCenterY2.Text = WaferCenterY2.ToString("#.000");
            mIsUpdating = false;


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

        private void btnSetADirection_Click(object sender, EventArgs e)
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
            nmuADirectionX.Value = Convert.ToDecimal(Motion.GetPositionValue(Sys.AxisX));
            nmuADirectionY.Value = Convert.ToDecimal(Motion.GetPositionValue(Sys.AxisY));
            nmuADirectionZ.Value = Convert.ToDecimal(Motion.GetPositionValue(Sys.AxisZ));
        }

        private void btnSetBDirection_Click(object sender, EventArgs e)
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
            nmuBDirectionX.Value = Convert.ToDecimal(Motion.GetPositionValue(Sys.AxisX));
            nmuBDirectionY.Value = Convert.ToDecimal(Motion.GetPositionValue(Sys.AxisY));
            nmuBDirectionZ.Value = Convert.ToDecimal(Motion.GetPositionValue(Sys.AxisZ));
        }

        private void nmuADirectionX_ValueChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        void ButtonLock(bool isEnabled)
        {
            btnSetPos1.Enabled = isEnabled;
            btnGoPos1.Enabled = isEnabled;
            btnSetPos2.Enabled = isEnabled;
            btnGoPos2.Enabled = isEnabled;
            btnSetADirection.Enabled = isEnabled;
            btnGoADirection.Enabled = isEnabled;
            btnSetBDirection.Enabled = isEnabled;
            btnGoBDirection.Enabled = isEnabled;
            btnAutoFocus.Enabled = isEnabled;
            btnAutoPitch.Enabled = isEnabled;
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

            Task.Run(new Action(() =>
            {
                if (MoveStart != null)
                {
                    MoveStart(sender, e);
                }
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

        private void btnGoADirection_Click(object sender, EventArgs e)
        {
            decimal[] _Pos = new decimal[] { nmuADirectionX.Value, nmuADirectionY.Value, nmuADirectionZ.Value, 0, 0, 0 };
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
                btnGoADirection.BeginInvoke(new Action(() =>
                {
                    btnGoADirection.BackColor = Color.Yellow;
                }));
                ErrorCode ret = ErrorCode.Running;
                do
                {
                    ret = CMotionMisc.Move3DPos(_Sys, _Pos);
                } while (ret == ErrorCode.Running);
                switch (_Sys.RunStatus)
                {
                    case MSystemParameter.enmRunStatus.Finish:
                        btnGoADirection.BeginInvoke(new Action(() =>
                        {
                            btnGoADirection.BackColor = SystemColors.Control;
                            btnGoADirection.UseVisualStyleBackColor = true;
                        }));
                        break;
                    case MSystemParameter.enmRunStatus.Alarm:
                        btnGoADirection.BeginInvoke(new Action(() =>
                        {
                            btnGoADirection.BackColor = Color.Red;
                            btnGoADirection.UseVisualStyleBackColor = true;
                        }));
                        break;
                }
                if (PosChanged != null)
                {
                    PosChanged(sender, e);
                }
            }));

        }

        private void btnGoBDirection_Click(object sender, EventArgs e)
        {
            decimal[] _Pos = new decimal[] { nmuBDirectionX.Value, nmuBDirectionY.Value, nmuBDirectionZ.Value, 0, 0, 0 };
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
                btnGoBDirection.BeginInvoke(new Action(() =>
                {
                    btnGoBDirection.BackColor = Color.Yellow;
                }));
                ErrorCode ret = ErrorCode.Running;
                do
                {
                    ret = CMotionMisc.Move3DPos(_Sys, _Pos);
                } while (ret == ErrorCode.Running);

                switch (_Sys.RunStatus)
                {
                    case MSystemParameter.enmRunStatus.Finish:
                        btnGoBDirection.BeginInvoke(new Action(() =>
                        {
                            btnGoBDirection.BackColor = SystemColors.Control;
                            btnGoBDirection.UseVisualStyleBackColor = true;
                        }));
                        break;
                    case MSystemParameter.enmRunStatus.Alarm:
                        btnGoBDirection.BeginInvoke(new Action(() =>
                        {
                            btnGoBDirection.BackColor = Color.Red;
                            btnGoBDirection.UseVisualStyleBackColor = true;
                        }));
                        break;
                }
                if (PosChanged != null)
                {
                    PosChanged(sender, e);
                }
            }));

        }

        private void btnGoCenter_Click(object sender, EventArgs e)
        {
            decimal[] _Pos = new decimal[] { WaferCenterX, WaferCenterY, WaferCenterZ, 0, 0, 0 };

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
                btnGoCenter.BeginInvoke(new Action(() =>
                {
                    btnGoCenter.BackColor = Color.Yellow;
                }));
                ErrorCode ret = ErrorCode.Running;
                do
                {
                    ret = CMotionMisc.Move3DPos(_Sys, _Pos);
                } while (ret == ErrorCode.Running);
                switch (_Sys.RunStatus)
                {
                    case MSystemParameter.enmRunStatus.Finish:
                        btnGoCenter.BeginInvoke(new Action(() =>
                        {
                            btnGoCenter.BackColor = SystemColors.Control;
                            btnGoCenter.UseVisualStyleBackColor = true;
                        }));
                        break;
                    case MSystemParameter.enmRunStatus.Alarm:
                        btnGoCenter.BeginInvoke(new Action(() =>
                        {
                            btnGoCenter.BackColor = Color.Red;
                            btnGoCenter.UseVisualStyleBackColor = true;
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
