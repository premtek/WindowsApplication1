using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMotion;
using ProjectCore;

namespace Premtek
{
    /// <summary>共用的運動雜項
    /// </summary>
    public static class CMotionMisc
    {
        public static Premtek.Base.CMotionCollection Motion { get; set; }
        /// <summary>修改速度為優化速度</summary>
        /// <param name="axisNo">軸號</param>
        /// <param name="targetPos">目標位置</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Premtek.Base.CommandStatus ReviseVelocity(int axisNo, decimal targetPos, decimal MaxVelLimit)
        {
            if (Motion == null)
            {
                System.Diagnostics.Debug.Assert(false);
            }
            Premtek.Base.SVelocity _vel = Motion.AxisParameter[axisNo].Velocity;
            decimal _NowPos = 0;
            _NowPos = Convert.ToDecimal(Motion.GetPositionValue(axisNo));

            decimal _Distance = Math.Abs(targetPos - _NowPos);
            decimal _Velocity = (decimal)Motion.AxisParameter[axisNo].Velocity.MaxVel;
            CDispensingMath.GetCrossVelocity(MaxVelLimit, _vel.Acc * _vel.AccRatio, _vel.Dec * _vel.DecRatio, _Distance, MSystemParameter.gSSystemParameter.CrossVerticalTime, ref _Velocity);
            if (_Velocity != 0)
            {
                return Motion.SetVelHigh(axisNo, _Velocity);
            }
            else
            {
                return Premtek.Base.CommandStatus.Sucessed;
            }

        }
        static System.Diagnostics.Stopwatch _StopWatch = new System.Diagnostics.Stopwatch();
        /// <summary>安全移動位置
        /// </summary>
        /// <param name="Axis"></param>
        /// <param name="posX"></param>
        /// <returns></returns>
        /// <remarks>Z軸到飛行高度(安全高度),Tilt旋轉角度, XY移動到目標位置, Z下降前詢問, 確認後下降. Axis[0] = X, Axis[1] = Y, Axis[2] = Z, Axis[3] = A, Axis[4] = B, Axis[5] = C</remarks>
        public static ErrorCode SafeMovePos(ProjectCore.MSystemParameter.sSysParam sys, decimal[] pos)
        {
            if (sys == null)
            {
                return ErrorCode.Failed;
            }
            if (pos.GetUpperBound(0) < 5)
            {
                return ErrorCode.Failed;
            }
            if (Motion == null)
            {
                return ErrorCode.Failed;
            }
            decimal posX = pos[0];
            decimal posY = pos[1];
            decimal posZ = pos[2];
            decimal posA = pos[3];
            decimal posB = pos[4];
            decimal posC = pos[5];

            switch (sys.SysNum)
            {
                case 1000://要求軸停止
                    Motion.SlowStop(sys.AxisX, 100);
                    Motion.SlowStop(sys.AxisY, 100);
                    Motion.SlowStop(sys.AxisZ, 100);
                    sys.SysNum = 1020;
                    break;

                case 1020://確認停止後再動作
                    if (Motion.MotionDone(sys.AxisX, false) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        return ErrorCode.Running;
                    }
                    if (Motion.MotionDone(sys.AxisY, false) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        return ErrorCode.Running;
                    }
                    if (Motion.MotionDone(sys.AxisZ, false) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        return ErrorCode.Running;
                    }
                    sys.SysNum = 1040;
                    break;

                case 1040:
                    if (Motion.AbsMove(sys.AxisZ, 0) != Premtek.Base.CommandStatus.Sucessed)//Z軸到飛行高度
                    {
                        return ErrorCode.Failed;
                    }
                    _StopWatch.Restart();
                    sys.SysNum = 1100;
                    break;

                case 1100://Z軸到位
                    if (_StopWatch.ElapsedMilliseconds < 100)//防誤判
                    {
                        return ErrorCode.Running;
                    }
                    if (Motion.MotionDone(sys.AxisZ, false) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        return ErrorCode.Running;
                    }
                    sys.SysNum = 1200;
                    break;

                case 1200:
                    if (sys.AxisA >= 0)
                    {
                        if (Motion.AbsMove(sys.AxisA, posA) != Premtek.Base.CommandStatus.Sucessed)
                        {
                            return ErrorCode.Failed;
                        }
                        _StopWatch.Restart();
                        sys.SysNum = 1300;
                    }
                    else
                    {
                        sys.SysNum = 1400;
                    }
                    break;

                case 1300:
                    if (_StopWatch.ElapsedMilliseconds < 50)//防誤判
                    {
                        return ErrorCode.Running;
                    }
                    if (Motion.MotionDone(sys.AxisA, false) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        return ErrorCode.Running;
                    }
                    sys.SysNum = 1400;
                    break;

                case 1400:
                    if (sys.AxisB >= 0)
                    {
                        if (Motion.AbsMove(sys.AxisB, posB) != Premtek.Base.CommandStatus.Sucessed)
                        {
                            return ErrorCode.Failed;
                        }
                        _StopWatch.Restart();
                        sys.SysNum = 1500;
                    }
                    else
                    {
                        sys.SysNum = 1600;
                    }
                    break;

                case 1500:
                    if (_StopWatch.ElapsedMilliseconds < 50)//防誤判
                    {
                        return ErrorCode.Running;
                    }
                    if (Motion.MotionDone(sys.AxisB, false) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        return ErrorCode.Running;
                    }
                    sys.SysNum = 1600;
                    break;

                case 1600:
                    if (sys.AxisB >= 0)
                    {
                        if (Motion.AbsMove(sys.AxisC, posC) != Premtek.Base.CommandStatus.Sucessed)
                        {
                            return ErrorCode.Failed;
                        }
                        _StopWatch.Restart();
                        sys.SysNum = 1700;
                    }
                    else
                    {
                        sys.SysNum = 1800;
                    }
                    break;

                case 1700:
                    if (_StopWatch.ElapsedMilliseconds < 50)//防誤判
                    {
                        return ErrorCode.Running;
                    }
                    if (Motion.MotionDone(sys.AxisC, false) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        return ErrorCode.Running;
                    }
                    sys.SysNum = 1800;
                    break;

                case 1800://XY軸移動到目標位置
                    if (Motion.AbsMove(sys.AxisX, posX) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        return ErrorCode.Failed;
                    }
                    if (Motion.AbsMove(sys.AxisY, posY) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        return ErrorCode.Failed;
                    }
                    _StopWatch.Restart();
                    sys.SysNum = 1900;
                    break;

                case 1900://XY軸移動到位
                    if (_StopWatch.ElapsedMilliseconds < 50)//防誤判
                    {
                        return ErrorCode.Running;
                    }
                    if (Motion.MotionDone(sys.AxisX, false) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        return ErrorCode.Running;
                    }
                    if (Motion.MotionDone(sys.AxisY, false) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        return ErrorCode.Running;
                    }
                    sys.SysNum = 2000;
                    break;

                case 2000://Z軸到目標位置

                    if (Microsoft.VisualBasic.Interaction.MsgBox("Is Z Down Pos Safety?", Microsoft.VisualBasic.MsgBoxStyle.OkCancel | Microsoft.VisualBasic.MsgBoxStyle.SystemModal | Microsoft.VisualBasic.MsgBoxStyle.MsgBoxSetForeground, "Safety Check") == Microsoft.VisualBasic.MsgBoxResult.Ok)
                    {
                        if (Motion.AbsMove(sys.AxisZ, posZ) != Premtek.Base.CommandStatus.Sucessed)
                        {
                            return ErrorCode.Failed;
                        }
                        _StopWatch.Restart();
                        sys.SysNum = 2100;
                        break;
                    }
                    else
                    {
                        sys.RunStatus = MSystemParameter.enmRunStatus.Finish;
                        return ErrorCode.Success;
                    }

                case 2100://Z軸到位
                    if (_StopWatch.ElapsedMilliseconds < 50)//防誤判
                    {
                        return ErrorCode.Running;
                    }
                    if (Motion.MotionDone(sys.AxisZ, false) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        return ErrorCode.Running;
                    }
                    sys.RunStatus = MSystemParameter.enmRunStatus.Finish;
                    return ErrorCode.Success;
                default:
                    //System.Diagnostics.Debug.Assert(false);
                    sys.SysNum = 1000;
                    break;
            }
            return ErrorCode.Running;

        }

        /// <summary>直接移動位置
        /// </summary>
        /// <param name="Axis"></param>
        /// <param name="posX"></param>
        /// <returns></returns>
        /// <remarks>Z軸到作業高度, XY移動到目標位置. Axis[0] = X, Axis[1] = Y, Axis[2] = Z, Axis[3] = A, Axis[4] = B, Axis[5] = C</remarks>
        public static ErrorCode Move3DPos(ProjectCore.MSystemParameter.sSysParam sys, decimal[] pos)
        {
            if (sys == null)
            {
                return ErrorCode.Failed;
            }
            if (pos.GetUpperBound(0) < 5)
            {
                return ErrorCode.Failed;
            }
            if (Motion == null)
            {
                return ErrorCode.Failed;
            }
            decimal posX = pos[0];
            decimal posY = pos[1];
            decimal posZ = pos[2];
            decimal posA = pos[3];
            decimal posB = pos[4];
            decimal posC = pos[5];

            switch (sys.SysNum)
            {
                case 1000://要求軸停止
                    Motion.SlowStop(sys.AxisX, 100);
                    Motion.SlowStop(sys.AxisY, 100);
                    Motion.SlowStop(sys.AxisZ, 100);
                    sys.SysNum = 1100;
                    break;

                case 1100://確認停止後再動作
                    if (Motion.MotionDone(sys.AxisX, false) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        return ErrorCode.Running;
                    }
                    if (Motion.MotionDone(sys.AxisY, false) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        return ErrorCode.Running;
                    }
                    if (Motion.MotionDone(sys.AxisZ, false) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        return ErrorCode.Running;
                    }

                    sys.SysNum = 1200;
                    break;

                case 1200:
                    if (Motion.AbsMove(sys.AxisZ, posZ) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        return ErrorCode.Failed;
                    }
                    _StopWatch.Restart();
                    sys.SysNum = 1300;
                    break;

                case 1300:
                    if (_StopWatch.ElapsedMilliseconds < 50)//防誤判
                    {
                        return ErrorCode.Running;
                    }
                    if (Motion.MotionDone(sys.AxisZ, false) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        return ErrorCode.Running;
                    }
                    sys.SysNum = 1800;
                    break;

                case 1800://XY軸移動到目標位置
                    if (Motion.AbsMove(sys.AxisX, posX) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        return ErrorCode.Failed;
                    }
                    if (Motion.AbsMove(sys.AxisY, posY) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        return ErrorCode.Failed;
                    }
                    _StopWatch.Restart();
                    sys.SysNum = 1900;
                    break;

                case 1900://XY軸移動到位
                    if (_StopWatch.ElapsedMilliseconds < 50)//防誤判
                    {
                        return ErrorCode.Running;
                    }
                    if (Motion.MotionDone(sys.AxisX, false) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        return ErrorCode.Running;
                    }
                    if (Motion.MotionDone(sys.AxisY, false) != Premtek.Base.CommandStatus.Sucessed)
                    {
                        return ErrorCode.Running;
                    }

                    sys.RunStatus = MSystemParameter.enmRunStatus.Finish;
                    return ErrorCode.Success;

                default:
                    //System.Diagnostics.Debug.Assert(false);
                    sys.SysNum = 1000;
                    break;
            }
            return ErrorCode.Running;

        }

    }
}
