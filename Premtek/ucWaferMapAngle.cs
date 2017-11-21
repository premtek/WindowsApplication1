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
    public partial class ucWaferMapAngle : UserControl
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
        /// <summary>位置變更事件
        /// </summary>
        public event EventHandler PosChanged;

        public ucWaferMapAngle()
        {
            InitializeComponent();
        }
      

        #region "內部變數"

        /// <summary>X方向 Wafer傾斜角度
        /// </summary>
        decimal mWaferAngle;
        /// <summary>Y方向 Wafer傾斜角度
        /// </summary>
        decimal mWaferAngleY;
        /// <summary>是否介面更新中
        /// </summary>
        bool mIsUpdating = false;
        #endregion

        #region "公開屬性"

        /// <summary>Wafer極左元件左下角座標
        /// </summary>
        public decimal PosX1 { get; set; }
        /// <summary>Wafer極左元件左下角座標
        /// </summary>
        public decimal PosY1 { get; set; }
        /// <summary>Wafer極左元件左下角座標
        /// </summary>
        public decimal PosZ1 { get; set; }
        /// <summary>Wafer極右元件左下角座標
        /// </summary>
        public decimal PosX2 { get; set; }
        /// <summary>Wafer極右元件左下角座標
        /// </summary>
        public decimal PosY2 { get; set; }
        /// <summary>Wafer極右元件左下角座標
        /// </summary>
        public decimal PosZ2 { get; set; }

        /// <summary>Wafer極上元件左下角座標
        /// </summary>
        public decimal PosX3 { get; set; }
        /// <summary>Wafer極上元件左下角座標
        /// </summary>
        public decimal PosY3 { get; set; }
        /// <summary>Wafer極上元件左下角座標
        /// </summary>
        public decimal PosZ3 { get; set; }
        /// <summary>Wafer極下元件左下角座標
        /// </summary>
        public decimal PosX4 { get; set; }
        /// <summary>Wafer極下元件左下角座標
        /// </summary>
        public decimal PosY4 { get; set; }
        /// <summary>Wafer極下元件左下角座標
        /// </summary>
        public decimal PosZ4 { get; set; }

        /// <summary>X數量
        /// </summary>
        public decimal CountX { get; set; }
        /// <summary>Y數量
        /// </summary>
        public decimal CountY { get; set; }
        /// <summary>X方向間距
        /// </summary>
        public decimal PitchX { get; set; }
        /// <summary>Y方向間距
        /// </summary>
        public decimal PitchY { get; set; }

        /// <summary>中心點
        /// </summary>
        public decimal WaferCenterX { get; set; }
        /// <summary>中心點
        /// </summary>
        public decimal WaferCenterY { get; set; }
        /// <summary>中心點
        /// </summary>
        public decimal WaferCenterZ { get; set; }
        /// <summary>有效半徑
        /// </summary>
        public decimal WaferEffectiveRadius { get; set; }
       
        /// <summary>X方向 Wafer傾斜角度
        /// </summary>
        public decimal WaferAngle
        {
            get
            {
                return mWaferAngle;
            }
        }
        /// <summary>Y方向 Wafer傾斜角度
        /// </summary>
        public decimal WaferAngleY
        {
            get
            {
                return mWaferAngleY;
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
            if (ControlMisc.SetNumericValue(ref nmuPosX4, PosX4) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuPosY4, PosY4) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuPosZ4, PosZ4) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuCountX, CountX) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuCountY, CountY) != ErrorCode.Success)
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
            nmuPosX4.Maximum = Motion.AxisParameter[Sys.AxisX].Limit.PosivtiveLimit;

            nmuPosX1.Minimum = Motion.AxisParameter[Sys.AxisX].Limit.NegativeLimit;
            nmuPosX2.Minimum = Motion.AxisParameter[Sys.AxisX].Limit.NegativeLimit;
            nmuPosX3.Minimum = Motion.AxisParameter[Sys.AxisX].Limit.NegativeLimit;
            nmuPosX4.Minimum = Motion.AxisParameter[Sys.AxisX].Limit.NegativeLimit;

            nmuPosY1.Maximum = Motion.AxisParameter[Sys.AxisY].Limit.PosivtiveLimit;
            nmuPosY2.Maximum = Motion.AxisParameter[Sys.AxisY].Limit.PosivtiveLimit;
            nmuPosY3.Maximum = Motion.AxisParameter[Sys.AxisY].Limit.PosivtiveLimit;
            nmuPosY4.Maximum = Motion.AxisParameter[Sys.AxisY].Limit.PosivtiveLimit;

            nmuPosY1.Minimum = Motion.AxisParameter[Sys.AxisY].Limit.NegativeLimit;
            nmuPosY2.Minimum = Motion.AxisParameter[Sys.AxisY].Limit.NegativeLimit;
            nmuPosY3.Minimum = Motion.AxisParameter[Sys.AxisY].Limit.NegativeLimit;
            nmuPosY4.Minimum = Motion.AxisParameter[Sys.AxisY].Limit.NegativeLimit;

            nmuPosZ1.Maximum = Motion.AxisParameter[Sys.AxisZ].Limit.PosivtiveLimit;
            nmuPosZ2.Maximum = Motion.AxisParameter[Sys.AxisZ].Limit.PosivtiveLimit;
            nmuPosZ3.Maximum = Motion.AxisParameter[Sys.AxisZ].Limit.PosivtiveLimit;
            nmuPosZ4.Maximum = Motion.AxisParameter[Sys.AxisZ].Limit.PosivtiveLimit;

            nmuPosZ1.Minimum = Motion.AxisParameter[Sys.AxisZ].Limit.NegativeLimit;
            nmuPosZ2.Minimum = Motion.AxisParameter[Sys.AxisZ].Limit.NegativeLimit;
            nmuPosZ3.Minimum = Motion.AxisParameter[Sys.AxisZ].Limit.NegativeLimit;
            nmuPosZ4.Minimum = Motion.AxisParameter[Sys.AxisZ].Limit.NegativeLimit;
            
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
            PosX3 = nmuPosX3.Value;
            PosY3 = nmuPosY3.Value;
            PosZ3 = nmuPosZ3.Value;
            PosX4 = nmuPosX4.Value;
            PosY4 = nmuPosY4.Value;
            PosZ4 = nmuPosZ4.Value;
            CountX = nmuCountX.Value;
            CountY = nmuCountY.Value;

            mWaferAngle = (decimal)Math.Atan2((double)(PosY2 - PosY1), (double)(PosX2 - PosX1));
            mWaferAngleY = (decimal)Math.Atan2((double)(PosX4 - PosX3), (double)(PosY4 - PosY3));
            //lblWaferAngle.Text = "θ= " + mWaferAngle.ToString("0.###") + "°";
            nmuAngle.Value = mWaferAngle;
            if (CountX > 1)
            {
                PitchX = CMath.GetDistance(PosX1, PosY1, PosX2, PosY2) / (CountX - 1);
            }
            else
            {
                PitchX = 0;
            }
            if (CountY > 1)
            {
                PitchY = CMath.GetDistance(PosX3, PosY3, PosX4, PosY4) / (CountY - 1);
            }
            else
            {
                PitchY = 0;
            }
            ControlMisc.SetNumericValue(ref nmuPitchX, PitchX);
            ControlMisc.SetNumericValue(ref nmuPitchY, PitchY);

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

        private void nmuPosX1_ValueChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        /// <summary>按鍵鎖定開關
        /// </summary>
        /// <param name="isEnabled"></param>
        void ButtonLock(bool isEnabled)
        {
            btnSetPos1.Enabled = isEnabled;
            btnSetPos2.Enabled = isEnabled;
            btnSetPos3.Enabled = isEnabled;
            btnSetPos4.Enabled = isEnabled;
            btnGoPos1.Enabled = isEnabled;
            btnGoPos2.Enabled = isEnabled;
            btnGoPos3.Enabled = isEnabled;
            btnGoPos4.Enabled = isEnabled;
            btnGoLeftEdge.Enabled = isEnabled;
            btnGoRightEdge.Enabled = isEnabled;
            btnGoTopEdge.Enabled = isEnabled;
            btnGoBottomEdge.Enabled = isEnabled;
            btnAutoEdge.Enabled = isEnabled;
            btnAutoFocus.Enabled = isEnabled;

        }

        private void btnGoPos1_Click(object sender, EventArgs e)
        {
            decimal[] _Pos = new decimal[] {nmuPosX1.Value, nmuPosY1.Value, nmuPosZ1.Value, 0,0,0};
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
                        btnGoPos2.BeginInvoke(new Action(() =>
                        {
                            btnGoPos2.BackColor = SystemColors.Control;
                            btnGoPos2.UseVisualStyleBackColor = true;
                            ButtonLock(true);
                        }));
                        break;
                    case MSystemParameter.enmRunStatus.Alarm:
                        btnGoPos2.BeginInvoke(new Action(() =>
                        {
                            btnGoPos2.BackColor = Color.Red;
                            btnGoPos2.UseVisualStyleBackColor = true;
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

        private void btnSetPos4_Click(object sender, EventArgs e)
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
            nmuPosX4.Value = Convert.ToDecimal(Motion.GetPositionValue(Sys.AxisX));
            nmuPosY4.Value = Convert.ToDecimal(Motion.GetPositionValue(Sys.AxisY));
            nmuPosZ4.Value = Convert.ToDecimal(Motion.GetPositionValue(Sys.AxisZ));
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
                        btnGoPos3.BeginInvoke(new Action(() =>
                        {
                            btnGoPos3.BackColor = SystemColors.Control;
                            btnGoPos3.UseVisualStyleBackColor = true;
                            ButtonLock(true);
                        }));
                        break;
                    case MSystemParameter.enmRunStatus.Alarm:
                        btnGoPos3.BeginInvoke(new Action(() =>
                        {
                            btnGoPos3.BackColor = Color.Red;
                            btnGoPos3.UseVisualStyleBackColor = true;
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

        private void btnGoPos4_Click(object sender, EventArgs e)
        {
            decimal[] _Pos = new decimal[] { nmuPosX4.Value, nmuPosY4.Value, nmuPosZ4.Value, 0, 0, 0 };
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
                btnGoPos4.BeginInvoke(new Action(() =>
                {
                    btnGoPos4.BackColor = Color.Yellow;
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
                        btnGoPos4.BeginInvoke(new Action(() =>
                        {
                            btnGoPos4.BackColor = SystemColors.Control;
                            btnGoPos4.UseVisualStyleBackColor = true;
                            ButtonLock(true);
                        }));
                        break;
                    case MSystemParameter.enmRunStatus.Alarm:
                        btnGoPos4.BeginInvoke(new Action(() =>
                        {
                            btnGoPos4.BackColor = Color.Red;
                            btnGoPos4.UseVisualStyleBackColor = true;
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

        private void btnGoLeftEdge_Click(object sender, EventArgs e)
        {
            decimal[] _Pos = new decimal[] { WaferCenterX - WaferEffectiveRadius, WaferCenterY, WaferCenterZ, 0, 0, 0 };
            MSystemParameter.sSysParam _Sys = new MSystemParameter.sSysParam();
            _Sys.AxisX = Sys.AxisX;
            _Sys.AxisY = Sys.AxisY;
            _Sys.AxisZ = Sys.AxisZ;
            _Sys.AxisA = Sys.AxisA;
            _Sys.AxisB = Sys.AxisB;
            _Sys.AxisC = Sys.AxisC;

            _Sys.SysNum = 1000;
            _Sys.RunStatus = MSystemParameter.enmRunStatus.Running;
            _Sys.ExecuteCommand = MCommonDefine.eSysCommand.None;
            _Sys.Command = MCommonDefine.eSysCommand.None;
            if (MoveStart != null)
            {
                MoveStart(sender, e);
            }
            Task.Run(new Action(() =>
            {
                btnGoLeftEdge.BeginInvoke(new Action(() =>
                {
                    btnGoLeftEdge.BackColor = Color.Yellow;
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
                        btnGoLeftEdge.BeginInvoke(new Action(() =>
                        {
                            btnGoLeftEdge.BackColor = SystemColors.Control;
                            btnGoLeftEdge.UseVisualStyleBackColor = true;
                            ButtonLock(true);
                        }));
                        break;
                    case MSystemParameter.enmRunStatus.Alarm:
                        btnGoLeftEdge.BeginInvoke(new Action(() =>
                        {
                            btnGoLeftEdge.BackColor = Color.Red;
                            btnGoLeftEdge.UseVisualStyleBackColor = true;
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

        private void btnGoRightEdge_Click(object sender, EventArgs e)
        {
            decimal[] _Pos = new decimal[] { WaferCenterX + WaferEffectiveRadius, WaferCenterY, WaferCenterZ, 0, 0, 0 };
            MSystemParameter.sSysParam _Sys = new MSystemParameter.sSysParam();
            _Sys.AxisX = Sys.AxisX;
            _Sys.AxisY = Sys.AxisY;
            _Sys.AxisZ = Sys.AxisZ;
            _Sys.AxisA = Sys.AxisA;
            _Sys.AxisB = Sys.AxisB;
            _Sys.AxisC = Sys.AxisC;

            _Sys.SysNum = 1000;
            _Sys.RunStatus = MSystemParameter.enmRunStatus.Running;
            _Sys.ExecuteCommand = MCommonDefine.eSysCommand.None;
            _Sys.Command = MCommonDefine.eSysCommand.None;
            if (MoveStart != null)
            {
                MoveStart(sender, e);
            }
            Task.Run(new Action(() =>
            {
                btnGoRightEdge.BeginInvoke(new Action(() =>
                {
                    btnGoRightEdge.BackColor = Color.Yellow;
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
                        btnGoRightEdge.BeginInvoke(new Action(() =>
                        {
                            btnGoRightEdge.BackColor = SystemColors.Control;
                            btnGoRightEdge.UseVisualStyleBackColor = true;
                            ButtonLock(true);
                        }));
                        break;
                    case MSystemParameter.enmRunStatus.Alarm:
                        btnGoRightEdge.BeginInvoke(new Action(() =>
                        {
                            btnGoRightEdge.BackColor = Color.Red;
                            btnGoRightEdge.UseVisualStyleBackColor = true;
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

        private void btnGoTopEdge_Click(object sender, EventArgs e)
        {
            decimal[] _Pos = new decimal[] { WaferCenterX, WaferCenterY + WaferEffectiveRadius, WaferCenterZ, 0, 0, 0 };
            MSystemParameter.sSysParam _Sys = new MSystemParameter.sSysParam();
            _Sys.AxisX = Sys.AxisX;
            _Sys.AxisY = Sys.AxisY;
            _Sys.AxisZ = Sys.AxisZ;
            _Sys.AxisA = Sys.AxisA;
            _Sys.AxisB = Sys.AxisB;
            _Sys.AxisC = Sys.AxisC;

            _Sys.SysNum = 1000;
            _Sys.RunStatus = MSystemParameter.enmRunStatus.Running;
            _Sys.ExecuteCommand = MCommonDefine.eSysCommand.None;
            _Sys.Command = MCommonDefine.eSysCommand.None;
            if (MoveStart != null)
            {
                MoveStart(sender, e);
            }
            Task.Run(new Action(() =>
            {
                btnGoTopEdge.BeginInvoke(new Action(() =>
                {
                    btnGoTopEdge.BackColor = Color.Yellow;
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
                        btnGoTopEdge.BeginInvoke(new Action(() =>
                        {
                            btnGoTopEdge.BackColor = SystemColors.Control;
                            btnGoTopEdge.UseVisualStyleBackColor = true;
                            ButtonLock(true);
                        }));
                        break;
                    case MSystemParameter.enmRunStatus.Alarm:
                        btnGoTopEdge.BeginInvoke(new Action(() =>
                        {
                            btnGoTopEdge.BackColor = Color.Red;
                            btnGoTopEdge.UseVisualStyleBackColor = true;
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

        private void btnGoBottomEdge_Click(object sender, EventArgs e)
        {
            decimal[] _Pos = new decimal[] { WaferCenterX, WaferCenterY - WaferEffectiveRadius, WaferCenterZ, 0, 0, 0 };
            MSystemParameter.sSysParam _Sys = new MSystemParameter.sSysParam();
            _Sys.AxisX = Sys.AxisX;
            _Sys.AxisY = Sys.AxisY;
            _Sys.AxisZ = Sys.AxisZ;
            _Sys.AxisA = Sys.AxisA;
            _Sys.AxisB = Sys.AxisB;
            _Sys.AxisC = Sys.AxisC;

            _Sys.SysNum = 1000;
            _Sys.RunStatus = MSystemParameter.enmRunStatus.Running;
            _Sys.ExecuteCommand = MCommonDefine.eSysCommand.None;
            _Sys.Command = MCommonDefine.eSysCommand.None;
            if (MoveStart != null)
            {
                MoveStart(sender, e);
            }
            Task.Run(new Action(() =>
            {
                btnGoBottomEdge.BeginInvoke(new Action(() =>
                {
                    btnGoBottomEdge.BackColor = Color.Yellow;
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
                        btnGoBottomEdge.BeginInvoke(new Action(() =>
                        {
                            btnGoBottomEdge.BackColor = SystemColors.Control;
                            btnGoBottomEdge.UseVisualStyleBackColor = true;
                            ButtonLock(true);
                        }));
                        break;
                    case MSystemParameter.enmRunStatus.Alarm:
                        btnGoBottomEdge.BeginInvoke(new Action(() =>
                        {
                            btnGoBottomEdge.BackColor = Color.Red;
                            btnGoBottomEdge.UseVisualStyleBackColor = true;
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

        private void nmuAngle_ValueChanged(object sender, EventArgs e)
        {
            mWaferAngle = nmuAngle.Value;
        }


    }
}
