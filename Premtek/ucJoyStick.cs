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
using System.Threading;
using ProjectCore;
using Premtek.Base;

namespace Premtek
{
    public enum CommandType
    {
        /// <summary>左鍵按下</summary>
        /// <remarks></remarks>
        LeftDown,
        /// <summary>左鍵放開</summary>
        /// <remarks></remarks>
        LeftUp,
        /// <summary>右鍵按下
        /// </summary>
        /// <remarks></remarks>
        RightDown,
        /// <summary>右鍵放開
        /// </summary>
        /// <remarks></remarks>
        RightUp,
        /// <summary>上鍵按下
        /// </summary>
        /// <remarks></remarks>
        UpDown,
        /// <summary>上鍵放開
        /// </summary>
        /// <remarks></remarks>
        UpUp,
        /// <summary>下鍵按下
        /// </summary>
        /// <remarks></remarks>
        DownDown,
        /// <summary>下鍵放開
        /// </summary>
        /// <remarks></remarks>
        DownUp,
        /// <summary>前鍵按下
        /// </summary>
        /// <remarks></remarks>
        ForwardUp,
        /// <summary>前鍵放開
        /// </summary>
        /// <remarks></remarks>
        ForwardDown,
        /// <summary>後鍵按下
        /// </summary>
        /// <remarks></remarks>
        BackwardUp,
        /// <summary>後鍵放開
        /// </summary>
        /// <remarks></remarks>
        BackwardDown
    }

    /// <summary>運動方式
    /// </summary>
    public enum eMode
    {
        /// <summary>吋動(等速移動)
        /// </summary>
        Jog,
        /// <summary>單步
        /// </summary>
        Distance,
    }
    /// <summary>座標系
    /// </summary>
    public enum eCoord
    {
        /// <summary>直線運動XYZ
        /// </summary>
        Linear,
        /// <summary>旋轉運動ABC
        /// </summary>
        Rotation,
    }
    /// <summary>速度
    /// </summary>
    public enum SpeedType
    {
        /// <summary>快
        /// </summary>
        Fast,
        /// <summary>中
        /// </summary>
        Medium,
        /// <summary>慢
        /// </summary>
        Slow,
    }
    public partial class ucJoyStick : UserControl
    {

        private CancellationTokenSource mTaskTokenSource = new CancellationTokenSource();
        private CancellationToken mTaskToken;
        bool mIsDisposing;

        public ucJoyStick()
        {
            InitializeComponent();
            SetSpeedType(mSpeedType);
            mTaskTokenSource = new CancellationTokenSource();
            mTaskToken = mTaskTokenSource.Token;
            Task.Run(new Action(Dispatch), mTaskToken);
        }

        public void Dispatch()
        {
            do
            {
                //Eason 20170120 Ticket:100030 , Memory Freed [S]
                if (mIsDisposing)
                {
                    return;
                }
                //Eason 20170120 Ticket:100030 , Memory Freed [E]

                //如果有待處理影像
                if (CmdQueue.Count > 0)
                {
                    CommandType mCmd = CmdQueue.Dequeue();
                    switch (mCmd)
                    {
                        case CommandType.BackwardDown:
                            Back_MouseDown();
                            break;
                        case CommandType.BackwardUp:
                            Back_MouseUp();
                            break;
                        case CommandType.DownDown:
                            Down_MouseDown();
                            break;
                        case CommandType.DownUp:
                            Down_MouseUp();
                            break;
                        case CommandType.ForwardDown:
                            Forward_MouseDown();
                            break;
                        case CommandType.ForwardUp:
                            Forward_MouseUp();
                            break;
                        case CommandType.LeftDown:
                            Left_MouseDown();
                            break;
                        case CommandType.LeftUp:
                            Left_MouseUp();
                            break;
                        case CommandType.RightDown:
                            Right_MouseDown();
                            break;
                        case CommandType.RightUp:
                            Right_MouseUp();
                            break;
                        case CommandType.UpDown:
                            Up_MouseDown();
                            break;
                        case CommandType.UpUp:
                            Up_MouseUp();
                            break;
                    }
                }
                else
                {
                    mAutoWait.WaitOne();
                }

                if (mIsDisposing)
                {
                    return;
                }
            } while (true);
        }

        public void RefreshPosition()
        {
            if (Motion == null)
                return;
             this.BeginInvoke(new Action(()=>{ 
                 txtPosX.Text = Motion.GetPositionValue(AxisX);
                 txtPosY.Text = Motion.GetPositionValue(AxisY);
                 txtPosZ.Text = Motion.GetPositionValue(AxisZ);
             }));


        }

        /// <summary>是否計時可持續運轉</summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsTimerCanRun()
        {

            if (Motion == null)
            {
                return false;
            }
            switch (mCoord)
            {
                case eCoord.Linear:
                    if (AxisX != -1)
                    {
                        switch (Motion.AxisParameter[AxisX].CardParameter.CardType)
                        {
                            case enmMotionCardType.PCI_1245:
                            case enmMotionCardType.PCI_1285:
                                break;
                            default:
                                return false;
                        }
                    }
                    if (AxisY != -1)
                    {
                        switch (Motion.AxisParameter[AxisY].CardParameter.CardType)
                        {
                            case enmMotionCardType.PCI_1245:
                            case enmMotionCardType.PCI_1285:
                                break;
                            default:
                                return false;
                        }
                    }
                    if (AxisZ != -1)
                    {
                        switch (Motion.AxisParameter[AxisZ].CardParameter.CardType)
                        {
                            case enmMotionCardType.PCI_1245:
                            case enmMotionCardType.PCI_1285:
                                break;
                            default:
                                return false;
                        }
                    }

                    return true;

                case eCoord.Rotation:
                    if (AxisA != -1)
                    {
                        switch (Motion.AxisParameter[AxisA].CardParameter.CardType)
                        {
                            case enmMotionCardType.PCI_1245:
                            case enmMotionCardType.PCI_1285:
                                break;
                            default:
                                return false;
                        }
                    }
                    if (AxisB != -1)
                    {
                        switch (Motion.AxisParameter[AxisB].CardParameter.CardType)
                        {
                            case enmMotionCardType.PCI_1245:
                            case enmMotionCardType.PCI_1285:
                                break;
                            default:
                                return false;
                        }
                    }
                    if (AxisC != -1)
                    {
                        switch (Motion.AxisParameter[AxisC].CardParameter.CardType)
                        {
                            case enmMotionCardType.PCI_1245:
                            case enmMotionCardType.PCI_1285:
                                break;
                            default:
                                return false;
                        }
                    }
                    return true;
            }
            return false;
        }


        /// <summary>全速度設定 </summary>
        /// <param name="speed"></param>
        /// <remarks></remarks>
        public void SetSpeedType(SpeedType speed)
        {
            mSpeedType = speed;
            //20170929 Toby_ Add 判斷
            if (((Parent != null)))
            {
                this.BeginInvoke(new Action(() =>
                {
                    switch (mSpeedType)
                    {
                        case SpeedType.Fast:
                            //高速
                            btnSpeed.Image = Premtek.Properties.Resources.SpeedHigh;
                            btnSpeed.BackColor = Color.Orange;
                            break;
                        case SpeedType.Medium:
                            //中速
                            btnSpeed.Image = Premtek.Properties.Resources.SpeedMid;
                            btnSpeed.BackColor = Color.Yellow;
                            break;
                        case SpeedType.Slow:
                            //低速
                            btnSpeed.Image = Premtek.Properties.Resources.SpeedLow;
                            btnSpeed.BackColor = Color.Green;
                            break;
                    }
                }));
            }

            SetMode();

        }


        /// <summary>速度形式設定</summary>
        /// <param name="axisIndex"></param>
        /// <param name="speed"></param>
        /// <remarks></remarks>
        private void SetSpeedType(int axisIndex, SpeedType speed)
        {
            if (Motion == null)
            {
                return;
            }
            switch (speed)
            {
                case SpeedType.Fast:
                    Motion.WaitCmdStatus(axisIndex);
                    Motion.SetVelHigh(axisIndex, 60);
                    Motion.WaitCmdStatus(axisIndex);
                    Motion.SetAcc(axisIndex, 60);
                    Motion.WaitCmdStatus(axisIndex);
                    Motion.SetDec(axisIndex, 60);
                    Motion.WaitCmdStatus(axisIndex);
                    break;
                case SpeedType.Medium:
                    Motion.WaitCmdStatus(axisIndex);
                    Motion.SetVelHigh(axisIndex, 30);
                    Motion.WaitCmdStatus(axisIndex);
                    Motion.SetAcc(axisIndex, 30);
                    Motion.WaitCmdStatus(axisIndex);
                    Motion.SetDec(axisIndex, 30);
                    Motion.WaitCmdStatus(axisIndex);
                    break;
                case SpeedType.Slow:
                    Motion.WaitCmdStatus(axisIndex);
                    Motion.SetVelHigh(axisIndex, 10);
                    Motion.WaitCmdStatus(axisIndex);
                    Motion.SetAcc(axisIndex, 10);
                    Motion.WaitCmdStatus(axisIndex);
                    Motion.SetDec(axisIndex, 10);
                    Motion.WaitCmdStatus(axisIndex);

                    break;
            }
        }


        /// <summary>設定模式.速度.顯示</summary>
        /// <remarks></remarks>
        private void SetMode()
        {
            switch (mMode)
            {
                case eMode.Jog:
                    //等速移動
                    //InvokeButton(btnSpeed, myResource.GetString("btnSpeed.Text"));
                    if(Syslog !=null) Syslog.Save("JoyStick Mode: Speed " + mSpeedType.ToString());
                    switch (mCoord)
                    {
                        case eCoord.Linear:
                            //設定XYZ速度
                            SetSpeedType(AxisX, mSpeedType);
                            SetSpeedType(AxisY, mSpeedType);
                            SetSpeedType(AxisZ, mSpeedType);
                            break;
                        case eCoord.Rotation:
                            //設定ABC速度
                            SetSpeedType(AxisA, mSpeedType);
                            SetSpeedType(AxisB, mSpeedType);
                            SetSpeedType(AxisC, mSpeedType);
                            break;
                    }

                    break;
                case eMode.Distance:
                    //等距離移動
                    switch (mSpeedType)
                    {
                        case SpeedType.Fast:
                            btnSpeed.BeginInvoke(new Action(() => { btnSpeed.Text = "100um"; }));
                            //InvokeButton(btnSpeed, "100um")
                            if(Syslog !=null) Syslog.Save("JoyStick Mode: Distance(100um)");
                            break;
                        case SpeedType.Medium:
                            btnSpeed.BeginInvoke(new Action(() => { btnSpeed.Text = "10um"; }));
                            //InvokeButton(btnSpeed, "10um")
                            if(Syslog !=null) Syslog.Save("JoyStick Mode: Distance(10um)");
                            break;
                        case SpeedType.Slow:
                            btnSpeed.BeginInvoke(new Action(() => { btnSpeed.Text = "1um"; }));
                            //InvokeButton(btnSpeed, "1um")
                            if(Syslog !=null) Syslog.Save("JoyStick Mode: Distance(1um)");
                            break;
                    }

                    break;
            }
        }


        private void Left_MouseDown()
        {
            if(Syslog !=null) Syslog.Save("[ucJoyStick]\t[btnLeft]\tMouseDown");
            if (IsTimerCanRun())
            {
                SetSpeedType(mSpeedType);
                mTimer.Enabled = true;
            }

            //[說明]:按下去移動，放掉就停止
            eDirection dir = default(eDirection);
            //移動方向
            int mAxis = 0;
            switch (mCoord)
            {
                case eCoord.Linear:
                    mAxis = AxisX;
                    break;
                case eCoord.Rotation:
                    mAxis = AxisA;
                    break;
            }
            if (Motion == null)
            {
                if (Syslog != null) Syslog.Save("Click btnLeft@UcJoyStick(Motion == null)", "", eMessageLevel.Error);
                return;
            }
            if (Motion.AxisParameter[mAxis].Parameter.ButtonDirection == eDirection.Negative)
            {
                dir = eDirection.Positive;
            }
            else
            {
                dir = eDirection.Negative;
            }
            switch (mMode)
            {
                case eMode.Distance:
                    //停止才能下
                    if (Motion.MotionDone(mAxis) == Base.CommandStatus.Sucessed)
                    {
                        decimal direction = (dir == eDirection.Negative ? -1 : 1);
                        switch (mSpeedType)
                        {
                            case SpeedType.Slow:
                                Motion.RelMove((int)mAxis, 0.001M * direction);
                                if(Syslog !=null) Syslog.Save("Click btnLeft@UcJoyStick(Step 1 um)");
                                break;
                            case SpeedType.Medium:
                                Motion.RelMove((int)mAxis, 0.01M * direction);
                                if (Syslog != null) Syslog.Save("Click btnLeft@UcJoyStick(Step 10 um)");
                                break;
                            case SpeedType.Fast:
                                Motion.RelMove((int)mAxis, 0.1M * direction);
                                if (Syslog != null) Syslog.Save("Click btnLeft@UcJoyStick(Step 100 um)");
                                break;
                        }
                    }

                    break;
                case eMode.Jog:
                    if(Syslog !=null) Syslog.Save("Click btnRight@UcJoyStick(Jog" + mSpeedType.ToString() + ")");
                    if (Motion.VelMove((int)mAxis, dir) == CommandStatus.Alarm)
                    {
                         if (EqpMsg != null) EqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp btnLeft", "", MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1009006));
                    }

                    break;

            }

            btnLeft.BackColor = Color.Yellow;
            //按鍵顏色

            //switch (Motion.AxisParameter[mAxis].CardParameter.CardType) {
            //    case enmMotionCardType.ModBus:
            //        mTimer.Enabled = true;
            //        break;
            //}
        }


        private void Right_MouseDown()
        {
            if(Syslog !=null) Syslog.Save("[ucJoyStick]\t[btnRight]\tMouseDown");
            if (IsTimerCanRun())
            {
                SetSpeedType(mSpeedType);
                mTimer.Enabled = true;
            }
            //[說明]:按下去移動，放掉就停止
            eDirection dir = default(eDirection);
            int mAxis = 0;
            switch (mCoord)
            {
                case eCoord.Linear:
                    mAxis = AxisX;
                    break;
                case eCoord.Rotation:
                    mAxis = AxisA;
                    break;
            }
            if (Motion == null)
            {
                if (Syslog != null) Syslog.Save("Click btnRight@UcJoyStick(Motion == null)", "", eMessageLevel.Error);
                return;
            }
            if (Motion.AxisParameter[mAxis].Parameter.ButtonDirection == eDirection.Negative)
            {
                dir = eDirection.Negative;
            }
            else
            {
                dir = eDirection.Positive;
            }

            switch (mMode)
            {
                case eMode.Distance:
                    decimal direction = (dir == eDirection.Negative ? -1 : 1);
                    switch (mSpeedType)
                    {
                        case SpeedType.Slow:
                            Motion.RelMove(mAxis, 0.001M * direction);
                            if(Syslog !=null) Syslog.Save("Click btnRight@UcJoyStick(Step 1 um)");
                            break;
                        case SpeedType.Medium:
                            Motion.RelMove(mAxis, 0.01M * direction);
                            if(Syslog !=null) Syslog.Save("Click btnRight@UcJoyStick(Step 10 um)");
                            break;
                        case SpeedType.Fast:
                            Motion.RelMove(mAxis, 0.1M * direction);
                            if(Syslog !=null) Syslog.Save("Click btnRight@UcJoyStick(Step 100 um)");
                            break;
                    }
                    break;
                case eMode.Jog:
                    if(Syslog !=null) Syslog.Save("Click btnRight@UcJoyStick(Jog" + mSpeedType.ToString() + ")");
                    if (Motion.VelMove(mAxis, dir) == CommandStatus.Alarm)
                    {
                         if (EqpMsg != null) EqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp btnRight", "", MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1009006), eMessageLevel.Error);
                    }

                    break;
                //20170712
                //If dir = eDirection.Positive Then
                //    If Motion.AbsMove(mAxis, Motion.AxisParameter[mAxis].Limit.PosivtiveLimit) = False Then
                //        gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Up_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                //    End If
                //ElseIf dir = eDirection.Negative Then
                //    If Motion.AbsMove(mAxis, Motion.AxisParameter[mAxis].Limit.NegativeLimit) = False Then
                //        gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Up_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                //    End If
                //End If
            }

            btnRight.BackColor = Color.Yellow;
            //按鍵顏色
            switch (Motion.AxisParameter[mAxis].CardParameter.CardType)
            {
                case enmMotionCardType.ModBus:
                    mTimer.Enabled = true;
                    break;
            }
        }
        private void Back_MouseDown()
        {
            lock (this)
            {
                Debug.Print("btnBack_MouseDown");
                if(Syslog !=null) Syslog.Save("[ucJoyStick]\t[btnBack]\tMouseDown");

                if (IsTimerCanRun())
                {
                    SetSpeedType(mSpeedType);
                    mTimer.Enabled = true;
                }
                //[說明]:按下去移動，放掉就停止

                eDirection dir = default(eDirection);
                //移動方向
                int mAxis = 0;
                switch (mCoord)
                {
                    case eCoord.Linear:
                        mAxis = AxisY;
                        break;
                    case eCoord.Rotation:
                        mAxis = AxisB;
                        break;
                }
                if (Motion == null)
                {
                    if (Syslog != null) Syslog.Save("Click btnBack@UcJoyStick(Motion == null)", "", eMessageLevel.Error);
                    return;
                }
                if (Motion.AxisParameter[mAxis].Parameter.ButtonDirection == eDirection.Negative)
                {
                    dir = eDirection.Positive;
                }
                else
                {
                    dir = eDirection.Negative;
                }
                switch (mMode)
                {
                    case eMode.Distance:
                        decimal direction = (dir == eDirection.Negative ? -1 : 1);
                        switch (mSpeedType)
                        {
                            case SpeedType.Slow:
                                Motion.RelMove(mAxis, 0.001M * direction);
                                if(Syslog !=null) Syslog.Save("Click btnBack@UcJoyStick(Step 1 um)");
                                break;
                            case SpeedType.Medium:
                                Motion.RelMove(mAxis, 0.01M * direction);
                                if(Syslog !=null) Syslog.Save("Click btnBack@UcJoyStick(Step 10 um)");
                                break;
                            case SpeedType.Fast:
                                Motion.RelMove(mAxis, 0.1M * direction);
                                if(Syslog !=null) Syslog.Save("Click btnBack@UcJoyStick(Step 100 um)");
                                break;
                        }
                        break;
                    case eMode.Jog:
                        if(Syslog !=null) Syslog.Save("Click btnBack@UcJoyStick(Jog" + mSpeedType.ToString() + ")");
                        if (Motion.VelMove(mAxis, dir) == CommandStatus.Alarm)
                        {
                             if (EqpMsg != null) EqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp btnBack", "", MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1009006), eMessageLevel.Error);
                        }

                        break;
                    //20170712
                    //If dir = eDirection.Positive Then
                    //    If Motion.AbsMove(mAxis, Motion.AxisParameter[mAxis].Limit.PosivtiveLimit) = False Then
                    //        gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Up_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                    //    End If
                    //ElseIf dir = eDirection.Negative Then
                    //    If Motion.AbsMove(mAxis, Motion.AxisParameter[mAxis].Limit.NegativeLimit) = False Then
                    //        gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Up_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                    //    End If
                    //End If
                }

                btnBack.BackColor = Color.Yellow;
                //按鍵顏色
                switch (Motion.AxisParameter[mAxis].CardParameter.CardType)
                {
                    case enmMotionCardType.ModBus:
                        mTimer.Enabled = true;
                        break;
                }
            }
        }



        private void Forward_MouseDown()
        {
            if(Syslog !=null) Syslog.Save("[ucJoyStick]\t[btnForward]\tMouseDown");
            if (IsTimerCanRun())
            {
                SetSpeedType(mSpeedType);
                mTimer.Enabled = true;
            }
            //[說明]:按下去移動，放掉就停止
            eDirection dir = default(eDirection);
            int mAxis = 0;
            switch (mCoord)
            {
                case eCoord.Linear:
                    mAxis = AxisY;
                    break;
                case eCoord.Rotation:
                    mAxis = AxisB;
                    break;
            }
            if (Motion == null)
            {
                if (Syslog != null) Syslog.Save("Click btnForward@UcJoyStick(Motion == null)", "", eMessageLevel.Error);
                return;
            }
            if (Motion.AxisParameter[mAxis].Parameter.ButtonDirection == eDirection.Negative)
            {
                dir = eDirection.Negative;
            }
            else
            {
                dir = eDirection.Positive;
            }
            switch (mMode)
            {
                case eMode.Distance:
                    decimal direction = (dir == eDirection.Negative ? -1 : 1);
                    switch (mSpeedType)
                    {
                        case SpeedType.Slow:
                            Motion.RelMove(mAxis, 0.001M * direction);
                            if(Syslog !=null) Syslog.Save("Click btnForward@UcJoyStick(Step 1 um)");
                            break;
                        case SpeedType.Medium:
                            Motion.RelMove(mAxis, 0.01M * direction);
                            if(Syslog !=null) Syslog.Save("Click btnForward@UcJoyStick(Step 10 um)");
                            break;
                        case SpeedType.Fast:
                            Motion.RelMove(mAxis, 0.1M * direction);
                            if(Syslog !=null) Syslog.Save("Click btnForward@UcJoyStick(Step 100 um)");
                            break;
                    }
                    break;
                case eMode.Jog:
                   if(Syslog !=null) Syslog.Save("Click btnLeft@UcJoyStick(Jog" + mSpeedType.ToString() + ")");
                    if (Motion.VelMove((int)mAxis, dir) == CommandStatus.Alarm)
                    {
                         if (EqpMsg != null) EqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp btnForward", "", MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1009006), eMessageLevel.Error);
                    }

                    break;
                //20170712
                //If dir = eDirection.Positive Then
                //    If Motion.AbsMove(mAxis, Motion.AxisParameter[mAxis].Limit.PosivtiveLimit) = False Then
                //        gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Up_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                //    End If
                //ElseIf dir = eDirection.Negative Then
                //    If Motion.AbsMove(mAxis, Motion.AxisParameter[mAxis].Limit.NegativeLimit) = False Then
                //        gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Up_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                //    End If
                //End If
            }

            btnForward.BackColor = Color.Yellow;
            //按鍵顏色
            switch (Motion.AxisParameter[mAxis].CardParameter.CardType)
            {
                case enmMotionCardType.ModBus:
                    mTimer.Enabled = true;
                    break;
            }
        }

        private void Down_MouseDown()
        {
            if(Syslog !=null) Syslog.Save("[ucJoyStick]\t[btnDown]\tMouseDown");
            if (IsTimerCanRun())
            {
                SetSpeedType(mSpeedType);
                mTimer.Enabled = true;
            }
            //[說明]:按下去移動，放掉就停止
            eDirection dir = default(eDirection);
            int mAxis = 0;
            switch (mCoord)
            {
                case eCoord.Linear:
                    mAxis = AxisZ;
                    break;
                case eCoord.Rotation:
                    mAxis = AxisC;
                    break;
            }
            if (Motion == null)
            {
                if (Syslog != null) Syslog.Save("Click btnDown@UcJoyStick(Motion == null)", "", eMessageLevel.Error);
                return;
            }
            if (Motion.AxisParameter[mAxis].Parameter.ButtonDirection == eDirection.Negative)
            {
                dir = eDirection.Negative;
            }
            else
            {
                dir = eDirection.Positive;
            }
            switch (mMode)
            {
                case eMode.Distance:
                    decimal direction = (dir == eDirection.Negative ? -1 : 1);
                    switch (mSpeedType)
                    {
                        case SpeedType.Slow:
                            Motion.RelMove(mAxis, 0.001M * direction);
                            if(Syslog !=null) Syslog.Save("Click btnDown@UcJoyStick(Step 1 um)");
                            break;
                        case SpeedType.Medium:
                            Motion.RelMove(mAxis, 0.01M * direction);
                            if(Syslog !=null) Syslog.Save("Click btnDown@UcJoyStick(Step 10 um)");
                            break;
                        case SpeedType.Fast:
                            Motion.RelMove(mAxis, 0.1M * direction);
                            if(Syslog !=null) Syslog.Save("Click btnDown@UcJoyStick(Step 100 um)");
                            break;
                    }
                    break;
                case eMode.Jog:
                    if(Syslog !=null) Syslog.Save("Click btnDown@UcJoyStick(Jog" + mSpeedType.ToString() + ")");
                    if (Motion.VelMove(mAxis, dir) == CommandStatus.Alarm)
                    {
                         if (EqpMsg != null) EqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp btnDown", "", MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1009006), eMessageLevel.Error);
                    }

                    break;
                //20170712
                //If dir = eDirection.Positive Then
                //    If Motion.AbsMove(mAxis, Motion.AxisParameter[mAxis].Limit.PosivtiveLimit) = False Then
                //        gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Up_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                //    End If
                //ElseIf dir = eDirection.Negative Then
                //    If Motion.AbsMove(mAxis, Motion.AxisParameter[mAxis].Limit.NegativeLimit) = False Then
                //        gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Up_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                //    End If
                //End If
            }

            btnDown.BackColor = Color.Yellow;
            //按鍵顏色
            switch (Motion.AxisParameter[mAxis].CardParameter.CardType)
            {
                case enmMotionCardType.ModBus:
                    mTimer.Enabled = true;
                    break;
            }
        }

        private void Up_MouseDown()
        {
            if(Syslog !=null) Syslog.Save("[ucJoyStick]\t[btnUp]\tMouseDown");
            if (IsTimerCanRun())
            {
                SetSpeedType(mSpeedType);
                mTimer.Enabled = true;
            }
            //[說明]:按下去移動，放掉就停止
            eDirection dir = default(eDirection);
            //移動方向
            int mAxis = 0;
            switch (mCoord)
            {
                case eCoord.Linear:
                    mAxis = AxisZ;
                    break;
                case eCoord.Rotation:
                    mAxis = AxisC;
                    break;
            }
            if (Motion == null)
            {
                if (Syslog != null) Syslog.Save("Click btnUp@UcJoyStick(Motion == null)", "", eMessageLevel.Error);
                return;
            }
            if (Motion.AxisParameter[mAxis].Parameter.ButtonDirection == eDirection.Negative)
            {
                dir = eDirection.Positive;
            }
            else
            {
                dir = eDirection.Negative;
            }
            switch (mMode)
            {
                case eMode.Distance:
                    decimal direction = (dir == eDirection.Negative ? -1 : 1);
                    switch (mSpeedType)
                    {
                        case SpeedType.Slow:
                            if (Motion.RelMove(mAxis, 0.001M * direction) == CommandStatus.Sucessed)
                            {
                                if(Syslog !=null) Syslog.Save("Click btnUp@UcJoyStick(Step 1 um)");
                            }
                            break;
                        case SpeedType.Medium:
                            if (Motion.RelMove(mAxis, 0.01M * direction) == CommandStatus.Sucessed)
                            {
                                if(Syslog !=null) Syslog.Save("Click btnUp@UcJoyStick(Step 10 um)");
                            }

                            break;
                        case SpeedType.Fast:
                            if (Motion.RelMove(mAxis, 0.1M * direction) == CommandStatus.Sucessed)
                            {
                                if(Syslog !=null) Syslog.Save("Click btnUp@UcJoyStick(Step 100 um)");
                            }
                            break;
                    }
                    break;
                case eMode.Jog:
                    if(Syslog !=null) Syslog.Save("Click btnUp@UcJoyStick(Jog" + mSpeedType.ToString() + ")");
                    if (Motion.VelMove(mAxis, dir) == CommandStatus.Alarm)
                    {
                        if (EqpMsg != null) EqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp btnUp", "", MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1009006), eMessageLevel.Error);
                    }

                    break;
                //20170712
                //If dir = eDirection.Positive Then
                //    If Motion.AbsMove(mAxis, Motion.AxisParameter[mAxis].Limit.PosivtiveLimit) = False Then
                //        gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Up_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                //    End If
                //ElseIf dir = eDirection.Negative Then
                //    If Motion.AbsMove(mAxis, Motion.AxisParameter[mAxis].Limit.NegativeLimit) = False Then
                //        gEqpMsg.AddHistoryAlarm("Error_1009006", "frmMotionOp Up_MouseDown", , gMsgHandler.GetMessage(Error_1009006), eMessageLevel.Error)
                //    End If
                //End If
            }

            btnUp.BackColor = Color.Yellow;
            //按鍵顏色
            switch (Motion.AxisParameter[mAxis].CardParameter.CardType)
            {
                case enmMotionCardType.ModBus:
                    mTimer.Enabled = false;
                    break;
            }
        }


        private async void Up_MouseUp()
        {
            await Task.Run(() =>
               {
                   switch (mCoord)
                   {
                       case eCoord.Linear:
                           BtnMouseUp(AxisZ, btnUp, ref txtPosZ);
                           break;
                       case eCoord.Rotation:
                           BtnMouseUp(AxisC, btnUp, ref txtPosZ);
                           break;
                   }
               });

        }



        private async void Down_MouseUp()
        {
            await Task.Run(() =>
            {
                switch (mCoord)
                {
                    case eCoord.Linear:
                        BtnMouseUp(AxisZ, btnDown, ref txtPosZ);
                        break;
                    case eCoord.Rotation:
                        BtnMouseUp(AxisC, btnDown, ref  txtPosZ);
                        break;
                }
            });
        }

        private async void Right_MouseUp()
        {
            await Task.Run(() =>
            {
                switch (mCoord)
                {
                    case eCoord.Linear:
                        BtnMouseUp(AxisX, btnRight, ref txtPosX);
                        break;
                    case eCoord.Rotation:
                        BtnMouseUp(AxisA, btnRight, ref txtPosX);
                        break;
                }
            });

        }

        private async void Left_MouseUp()
        {
            await Task.Run(() =>
            {
                switch (mCoord)
                {
                    case eCoord.Linear:
                        BtnMouseUp(AxisX, btnLeft, ref txtPosX);
                        break;
                    case eCoord.Rotation:
                        BtnMouseUp(AxisA, btnLeft, ref txtPosX);
                        break;
                }
            });

        }

        private async void Back_MouseUp()
        {
            await Task.Run(() =>
            {
                switch (mCoord)
                {
                    case eCoord.Linear:
                        BtnMouseUp(AxisY, btnBack, ref txtPosY);
                        break;
                    case eCoord.Rotation:
                        BtnMouseUp(AxisB, btnBack, ref txtPosY);
                        break;
                }
            });
        }

        private async void Forward_MouseUp()
        {
            await Task.Run(() =>
             {
                 switch (mCoord)
                 {
                     case eCoord.Linear:
                         BtnMouseUp(AxisY, btnForward, ref txtPosY);
                         break;
                     case eCoord.Rotation:
                         BtnMouseUp(AxisB, btnForward, ref txtPosY);
                         break;
                 }
             });

        }

        private void BtnMouseUp(int axisIndex, object sender, ref TextBox lblAxisPosition)
        {
            switch (mMode)
            {
                case eMode.Distance:
                    ((Button)sender).BackColor = SystemColors.Control;
                    //按鍵顏色
                    ((Button)sender).UseVisualStyleBackColor = true;
                    RefreshPosition();
                    //InvokeTextBox(lblAxisPosition, Motion.GetPositionValue(axisIndex));
                    //更新位置
                    return;

            }

            double mDec = 5000;
            //[說明]:按下去移動，放掉就停止
            switch (mSpeedType)
            {
                case SpeedType.Fast:
                    mDec = 5000;
                    break;
                case SpeedType.Medium:
                    mDec = 2500;
                    break;
                case SpeedType.Slow:
                    mDec = 100;
                    break;
            }
            //   Call Motion.SlowStop(axisIndex, mDec)
            //  Call Motion.Emg(axisIndex, mDec)
            Motion.EmgStop(axisIndex);
            if(Syslog !=null) Syslog.Save(Motion.AxisParameter[axisIndex].AxisName + " SlowStop(" + mDec + "mm/s^2)UcJoyStick");

            if ((mTimer != null))
            {
                mTimer.Enabled = false;
            }
            //確保命令發完
            do
            {
                //Application.DoEvents()
            } while (!(Motion.GetCmdStatus(axisIndex) == CommandStatus.Sucessed));
            System.Threading.Thread.Sleep(100);
            //等100ms

            Motion.GetPositionValue(axisIndex);
            do
            {
                //Application.DoEvents()
            } while (!(Motion.GetCmdStatus(axisIndex) == CommandStatus.Sucessed));
            Motion.GetPositionValue(axisIndex);
            do
            {
                //Application.DoEvents()
            } while (!(Motion.GetCmdStatus(axisIndex) == CommandStatus.Sucessed));


            //InvokeTextBox(lblAxisPosition, Motion.GetPositionValue(axisIndex));
            //更新位置

            ((Button)sender).BackColor = SystemColors.Control;
            //按鍵顏色
            ((Button)sender).UseVisualStyleBackColor = true;
        }


        /// <summary>外部軸卡物件
        /// </summary>
        public Premtek.Base.CMotionCollection Motion { get; set; }
        /// <summary>外部配接Log物件
        /// </summary>
        public CSystemLog Syslog { get; set; }
        /// <summary>外部配接MsgHandler物件
        /// </summary>
        public CEqpMsgHandler EqpMsg { get; set; }
        /// <summary>運行模式
        /// </summary>
        private eMode mMode = eMode.Jog;
        /// <summary>座標
        /// </summary>
        private eCoord mCoord= eCoord.Linear;
        /// <summary>速度
        /// </summary>
        private SpeedType mSpeedType = SpeedType.Slow;

        /// <summary>直線運動X軸
        /// </summary>
        public int AxisX { get; set; }
        /// <summary>直線運動Y軸
        /// </summary>
        public int AxisY { get; set; }
        /// <summary>直線運動Z軸
        /// </summary>
        public int AxisZ { get; set; }
        /// <summary>旋轉運動A軸
        /// </summary>
        public int AxisA { get; set; }
        /// <summary>旋轉運動B軸
        /// </summary>
        public int AxisB { get; set; }
        /// <summary>旋轉運動C軸
        /// </summary>
        public int AxisC { get; set; }

        Queue<CommandType> CmdQueue = new Queue<CommandType>();
        System.Threading.AutoResetEvent mAutoWait = new System.Threading.AutoResetEvent(false);

        #region "介面操作"

        /// <summary>模式切換
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMode_Click(object sender, EventArgs e)
        {
            switch (mMode)
            {
                case eMode.Distance:
                    mMode = eMode.Jog;
                    btnMode.Text = "Jog";
                    break;
                case eMode.Jog:
                    mMode = eMode.Distance;
                    btnMode.Text = "Dis";
                    break;
            }

        }

        /// <summary>速度切換
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSpeed_Click(object sender, EventArgs e)
        {
            switch (mSpeedType)
            {
                case SpeedType.Slow:
                    mSpeedType = SpeedType.Medium;
                    break;
                case SpeedType.Medium:
                    mSpeedType = SpeedType.Fast;
                    break;
                case SpeedType.Fast:
                    mSpeedType = SpeedType.Slow;
                    break;
            }
            SetSpeedType(mSpeedType);
        }

        /// <summary>座標切換
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCoordinate_Click(object sender, EventArgs e)
        {
            switch (mCoord)
            {
                case eCoord.Linear:
                    mCoord = eCoord.Rotation;
                    break;
                case eCoord.Rotation:
                    mCoord = eCoord.Linear;
                    break;
            }
        }


        private void btnBack_MouseDown(object sender, MouseEventArgs e)
        {
            CmdQueue.Enqueue(CommandType.BackwardDown);
            mAutoWait.Set();
        }

        private void btnLeft_MouseDown(object sender, MouseEventArgs e)
        {
            CmdQueue.Enqueue(CommandType.LeftDown);
            mAutoWait.Set();
        }

        private void btnRight_MouseDown(object sender, MouseEventArgs e)
        {
            CmdQueue.Enqueue(CommandType.RightDown);
            mAutoWait.Set();
        }

        private void btnForward_MouseDown(object sender, MouseEventArgs e)
        {
            CmdQueue.Enqueue(CommandType.ForwardDown);
            mAutoWait.Set();
        }

        private void btnUp_MouseDown(object sender, MouseEventArgs e)
        {
            CmdQueue.Enqueue(CommandType.UpDown);
            mAutoWait.Set();
        }

        private void btnDown_MouseDown(object sender, MouseEventArgs e)
        {
            CmdQueue.Enqueue(CommandType.DownDown);
            mAutoWait.Set();
        }

        private void btnBack_MouseUp(object sender, MouseEventArgs e)
        {
            CmdQueue.Enqueue(CommandType.BackwardUp);
            mAutoWait.Set();
        }

        private void btnLeft_MouseUp(object sender, MouseEventArgs e)
        {
            CmdQueue.Enqueue(CommandType.LeftUp);
            mAutoWait.Set();
        }

        private void btnRight_MouseUp(object sender, MouseEventArgs e)
        {
            CmdQueue.Enqueue(CommandType.RightUp);
            mAutoWait.Set();
        }

        private void btnForward_MouseUp(object sender, MouseEventArgs e)
        {
            CmdQueue.Enqueue(CommandType.ForwardUp);
            mAutoWait.Set();
        }

        private void btnUp_MouseUp(object sender, MouseEventArgs e)
        {
            CmdQueue.Enqueue(CommandType.UpUp);
            mAutoWait.Set();
        }

        private void btnDown_MouseUp(object sender, MouseEventArgs e)
        {
            CmdQueue.Enqueue(CommandType.DownUp);
            mAutoWait.Set();
        }

        #endregion

        private void mTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            int mAxisX = AxisX;
            int mAxisY = AxisY;
            int mAxisZ = AxisZ;
            if (mCoord == eCoord.Rotation)
            {
                mAxisX = AxisA;
                mAxisY = AxisB;
                mAxisZ = AxisC;
            }
            if (Motion == null)
            {
                return;
            }
            if (Parent != null)
            {
                this.BeginInvoke(new Action(() =>
                {
                    txtPosX.Text = Motion.GetPositionValue(AxisX);
                    txtPosY.Text = Motion.GetPositionValue(AxisY);
                    txtPosZ.Text = Motion.GetPositionValue(AxisZ);
                }));
                if (Motion.MotionDone(mAxisX) == CommandStatus.Sucessed)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        btnLeft.BackColor = SystemColors.Control;
                        btnRight.BackColor = SystemColors.Control;
                        btnLeft.UseVisualStyleBackColor = true;
                        btnRight.UseVisualStyleBackColor = true;
                    }));
                }
                if (Motion.MotionDone(mAxisY) == CommandStatus.Sucessed)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        btnBack.BackColor = SystemColors.Control;
                        btnForward.BackColor = SystemColors.Control;
                        btnBack.UseVisualStyleBackColor = true;
                        btnForward.UseVisualStyleBackColor = true;
                    }));
                }
                if (Motion.MotionDone(mAxisZ) == CommandStatus.Sucessed)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        btnUp.BackColor = SystemColors.Control;
                        btnDown.BackColor = SystemColors.Control;
                        btnUp.UseVisualStyleBackColor = true;
                        btnDown.UseVisualStyleBackColor = true;
                    }));
                }
            }
            if ((Motion.MotionDone(mAxisX) == CommandStatus.Sucessed) && (Motion.MotionDone(mAxisY) == CommandStatus.Sucessed) && (Motion.MotionDone(mAxisZ) == CommandStatus.Sucessed))
            {
                mTimer.Enabled = false;
            }

        }

       
      
    }
}
