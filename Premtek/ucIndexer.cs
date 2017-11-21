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
    public partial class ucIndexer : UserControl
    {
        /// <summary>目前索引
        /// </summary>
        public int Xno;
        /// <summary>目前索引
        /// </summary>
        public int Yno;
        /// <summary>元件尺寸
        /// </summary>
        public decimal DieSizeX;
        /// <summary>元件尺寸
        /// </summary>
        public decimal DieSizeY;
        /// <summary>左上角原點(0,0)位置
        /// </summary>
        public decimal LeftUpperPosX;
        /// <summary>左上角原點(0,0)位置
        /// </summary>
        public decimal LeftUpperPosY;
        /// <summary>元件間距
        /// </summary>
        public decimal PitchX;
        /// <summary>元件間距
        /// </summary>
        public decimal PitchY;
        /// <summary>Wafer圓心
        /// </summary>
        public decimal WaferCenterX;
        /// <summary>Wafer圓心
        /// </summary>
        public decimal WaferCenterY;
        /// <summary>Wafer圓心
        /// </summary>
        public decimal WaferCenterZ;
        /// <summary>Wafer角度(X方向)
        /// </summary>
        public decimal WaferAngle;
        /// <summary>Wafer角度Y方向
        /// </summary>
        public decimal WaferAngleY;
        /// <summary>外部配接系統
        /// </summary>
        public MSystemParameter.sSysParam Sys;
        /// <summary>位置移動開始
        /// </summary>
        public event EventHandler MoveStart;
        /// <summary>位置變更事件
        /// </summary>
        public event EventHandler PosChanged;

        /// <summary>每次點擊移動Indexer 步數
        /// </summary>
        int Step = 1;

        public ucIndexer()
        {
            InitializeComponent();
        }

        public void ShowProperty()
        {
            ControlMisc.SetNumericValue(ref nmuXno, Xno);
            ControlMisc.SetNumericValue(ref nmuYno, Yno);
        }

        void ButtonLock(bool isEnabled)
        {
            btnGoPos.Enabled = isEnabled;
            btnUp.Enabled = isEnabled;
            btnDown.Enabled = isEnabled;
            btnLeft.Enabled = isEnabled;
            btnRight.Enabled = isEnabled;
            btnStep.Enabled = isEnabled;
        }

        private void btnGoPos_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (Convert.ToInt32( btn.Tag))
            {
                case 0:
                    break;
                case 1:
                    Xno = (int)nmuXno.Value;
                    Yno = (int)nmuYno.Value - Step;
                    ControlMisc.SetNumericValue(ref nmuXno, Xno);
                    ControlMisc.SetNumericValue(ref nmuYno, Yno);
                    break;
                case 2:
                    Xno = (int)nmuXno.Value;
                    Yno = (int)nmuYno.Value + Step;
                    ControlMisc.SetNumericValue(ref nmuXno, Xno);
                    ControlMisc.SetNumericValue(ref nmuYno, Yno);
                    break;
                case 3:
                    Xno = (int)nmuXno.Value - Step;
                    Yno = (int)nmuYno.Value;
                    ControlMisc.SetNumericValue(ref nmuXno, Xno);
                    ControlMisc.SetNumericValue(ref nmuYno, Yno);
                    break;
                case 4:
                    Xno = (int)nmuXno.Value + Step;
                    Yno = (int)nmuYno.Value;
                    ControlMisc.SetNumericValue(ref nmuXno, Xno);
                    ControlMisc.SetNumericValue(ref nmuYno, Yno);
                    break;
            }
            decimal _FDposX;
            decimal _FDPosY;
            GetIndexerPos(out _FDposX, out _FDPosY);


            decimal[] _Pos = new decimal[] { _FDposX, _FDPosY, WaferCenterZ, 0, 0, 0 };

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
                btn.BeginInvoke(new Action(() =>
                {
                    btn.BackColor = Color.Yellow;
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
                        btn.BeginInvoke(new Action(() =>
                        {
                            btn.BackColor = SystemColors.Control;
                            btn.UseVisualStyleBackColor = true;
                            ButtonLock(true);
                        }));
                        break;
                    case MSystemParameter.enmRunStatus.Alarm:
                        btn.BeginInvoke(new Action(() =>
                        {
                            btn.BackColor = Color.Red;
                            btn.UseVisualStyleBackColor = true;
                            ButtonLock(true);
                        }));
                        break;
                    case MSystemParameter.enmRunStatus.Running:
                        if (ret == ErrorCode.Failed)
                        {
                            btn.BeginInvoke(new Action(() =>
                            {
                                btn.BackColor = Color.Red;
                                btn.UseVisualStyleBackColor = true;
                                ButtonLock(true);
                            }));
                        }
                        else if (ret == ErrorCode.Running)
                        {
                            Microsoft.VisualBasic.Interaction.MsgBox("Unknown", Microsoft.VisualBasic.MsgBoxStyle.OkOnly | Microsoft.VisualBasic.MsgBoxStyle.SystemModal | Microsoft.VisualBasic.MsgBoxStyle.MsgBoxSetForeground);
                        }
                        break;
                }
                if (PosChanged != null)
                {
                    PosChanged(sender, e);
                }
            }));
        }

        private void GetIndexerPos(out decimal _FDposX, out decimal _FDPosY)
        {
            decimal posX = LeftUpperPosX + DieSizeX * 0.5M + PitchX * ((int)nmuXno.Value - 1);
            decimal posY = LeftUpperPosY - DieSizeY * 0.5M - PitchY * ((int)nmuYno.Value - 1);
            decimal _RotatedPosX = 0;
            decimal _RotatedPosY = 0;
            CMath.Rotation(posX - WaferCenterX, posY - WaferCenterY, WaferAngle, WaferAngleY, out _RotatedPosX, out _RotatedPosY);
            _FDposX = _RotatedPosX + WaferCenterX;
            _FDPosY = _RotatedPosY + WaferCenterY;
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            switch (Step)
            {
                case 1:
                    Step = 5;
                    break;
                case 5:
                    Step = 10;
                    break;
                default:
                    Step = 1;
                    break;
            }
            btnStep.Text = Step.ToString();
        }



    }
}
