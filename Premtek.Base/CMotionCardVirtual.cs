using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek.Base
{
    /// <summary>虛擬運動控制卡</summary>
    /// <remarks></remarks>
    public class CMotionCardVirtual : IMotionCard
    {



        /// <summary>命令發送狀態</summary>
        /// <remarks></remarks>
        public CommandStatus cmdStatus { get; set; }

        public CommandStatus AbsMove(int axis, decimal absPos)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus AxisResetError(int axis)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus CheckMotorStatus(int axis, ref IOStatus IOStatus)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus Close()
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus EmgStop(int axis)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus GetAxisState(int axis, ref uint status)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus GetMotionState(int axis, ref int status)
        {
            return CommandStatus.Sucessed;
        }


        public CommandStatus GetCurrentVel(int axis, ref decimal velocity)
        {
            return CommandStatus.Sucessed;
        }

        public string GetErrorMessage(int axisNo)
        {
            return "";
        }

        public CommandStatus GetLatchFlag(int axis, ref bool latch)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus GetLatchPosition(int axis, enmPositionType type, ref decimal pos)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus HomeFinish(int axis)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus Initial( SMotionConnectParameter cardNo)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus VelMove(int axis, eDirection dir)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus MoveFinish(int axis)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus RelMove(int axis, decimal Dist)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus ResetLatch(int axis)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus Servo(int axis, enmONOFF state)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetAcc(int axis, decimal acc)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetALM(int axis, enmAlarmEnable enable, enmAlarmLogic logic, enmAlarmStopMode stopMode)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetDec(int axis, decimal dec)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetEMG(int axis, enmEmgLogic logic)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetERC(int axis, enmErcEnableMode enableMode, enmErcLogic logic)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetEZ(int axis, enmEZLogic logic)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetGantry(int masterAxis, int slaveAxis)
        {
            return 0;
        }

        public CommandStatus SetHEL(int axis, enmLimitEnable enable, enmLimitLogic logic, enmLimitStopMode stopMode)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetHomeAcc(int axis, decimal acc)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetHomeCrossDistance(int axis, decimal homeCrossDistance)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetHomeDec(int axis, decimal dec)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetHomeExSwitchMode(int axis, enmHomeExSwitchMode homeExSwitchMode)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetHomeReset(int axis, enmHomeReset enable)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetHomeVelHigh(int axis, decimal velHigh)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetHomeVelLow(int axis, decimal velLow)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SETIN1Stop(int axis, enmTriggerStopEnable enable, enmTriggerStopMode mode, enmTriggerStopLogic logic)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SETIN2Stop(int axis, enmTriggerStopEnable enable, enmTriggerStopMode mode, enmTriggerStopLogic logic)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SETIN4Stop(int axis, enmTriggerStopEnable enable, enmTriggerStopMode mode, enmTriggerStopLogic logic)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SETIN5Stop(int axis, enmTriggerStopEnable enable, enmTriggerStopMode mode, enmTriggerStopLogic logic)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetINP(int axis, enmINPEnable enable, enmINPLogic logic)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetLatch(int axis, enmLatchEnable enable, enmLatchPLogic logic)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetMaxPulseFrequency(int axis, enmEncodePulseInFrequency frequency)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetORG(int axis, enmOrgLogic logic)
        {
            return CommandStatus.Sucessed;
        }


        public CommandStatus SetPosition(int axis, decimal pos)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetPPU(int axis, decimal ppu)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetPulseIn(int axis, enmPulseInMode mode, enmPulseInLogic logic)
        {
            return CommandStatus.Sucessed;
        }


        public CommandStatus SetScale(int axis, decimal scale)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetVelHigh(int axis, decimal velHigh)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetVelLow(int axis, decimal velHigh)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus GpAddMovePath(ref CSyncParameter SyncParameter, bool IsEndPath = false)
        {
            return CommandStatus.Sucessed;
        }
        CommandStatus IMotionCard.GpAddDotPath(ref CSyncParameter SyncParameter, bool IsEndPath = false)
        {
            return GpAddMovePath(ref SyncParameter, IsEndPath);
        }

        public int AxisCount { get; set; }


        public uint AxisNum { get; set; }

        public CommandStatus GpClearMovePath(ref CSyncParameter SyncParameter)
        {
            return CommandStatus.Sucessed;
        }

        public double[] EndArray { get; set; }
        public double[] CenArray { get; set; }

        public CommandStatus GetCommandValue(int axisNo, ref decimal pos)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus GetCompareValue(int axisNo, ref decimal Pos)
        {
            return CommandStatus.Sucessed;
        }

        public string GetPositionValue(int axisNo)
        {
            return "0.0";
        }

        public CommandStatus GpAddAxis(ref CSyncParameter SyncParameter, List<int> AxisNoList)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus Home(int axisNo, uint homeMode, uint homeDirection)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus IOSet(int axisNo)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus GpMoveFinish(ref CSyncParameter SyncParameter)
        {
            return CommandStatus.Sucessed;
        }
        CommandStatus IMotionCard.GpMoveDone(ref CSyncParameter SyncParameter)
        {
            return GpMoveFinish(ref SyncParameter);
        }

        public CommandStatus GpMoving(ref CSyncParameter SyncParameter)
        {
            return CommandStatus.Sucessed;
        }
        CommandStatus IMotionCard.GpMovePath(ref CSyncParameter SyncParameter)
        {
            return GpMoving(ref SyncParameter);
        }


        public CommandStatus GpPauseMovePath(ref CSyncParameter SyncParameter)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus GpSetAcc(ref CSyncParameter SyncParameter, decimal Acc)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetBacklash1(int axisNo, enmBacklashEnable Enable)
        {
            return CommandStatus.Sucessed;
        }
        CommandStatus IMotionCard.SetBacklash(int axisNo, enmBacklashEnable Enable)
        {
            return SetBacklash1(axisNo, Enable);
        }

        public CommandStatus GpSetBlendingTime(ref CSyncParameter SyncParameter, int Time)
        {
            return CommandStatus.Sucessed;
        }



        public CommandStatus SetCampareData(int axisNo, decimal startPos, decimal endPos, int interval)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetCompare(int axisNo, enmCompareEnable Enable, enmCompareSource Type, enmCompareMethod Method)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus GpSetDec(ref CSyncParameter SyncParameter, decimal Dec)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetEMG1(enmEmgLogic Logic)
        {
            return CommandStatus.Sucessed;
        }
        CommandStatus IMotionCard.SetEMG(enmEmgLogic Logic)
        {
            return SetEMG1(Logic);
        }

        public CommandStatus SetExternalDrive(int axisNo, enmExternalDrive Drive, enmExternalDriveEnable Enable, enmExternalDrivePulseInMode Mode)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetPulseOutMode(int axisNo, enmPulseOutMode Mode)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus GpSetSpeedForward(ref CSyncParameter SyncParameter, enmSFEnable Enable)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus GpSetVelHigh(ref CSyncParameter SyncParameter, decimal VelHigh)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus GpSetVelLow(ref CSyncParameter SyncParameter, decimal VelLow)
        {
            return CommandStatus.Sucessed;
        }
        //===20170821========================================================================================================================================
        public CommandStatus SetSNEL(int axisNo, decimal negativeLimit)
        {
            return CommandStatus.Sucessed;
        }
        public CommandStatus SetSPEL(int axisNo, decimal positiveLimit)
        {
            return CommandStatus.Sucessed;
        }
        public CommandStatus GpSetPelReact(int axisNo, bool PelReact)
        {
            return CommandStatus.Sucessed;
        }
        CommandStatus IMotionCard.SetSPELReact(int axisNo, bool PelReact)
        {
            return GpSetPelReact(axisNo, PelReact);
        }
        public CommandStatus GpSetMelReact(int axisNo, bool MelReact)
        {
            return CommandStatus.Sucessed;
        }
        CommandStatus IMotionCard.SetSNELReact(int axisNo, bool MelReact)
        {
            return GpSetMelReact(axisNo, MelReact);
        }
        //===================================================================================================================================================
        //jimmy 20170823
        public CommandStatus GpSetSwMelEnable(int axisNo, bool SwMelEnable)
        {
            return CommandStatus.Sucessed;
        }
        public CommandStatus GpSetSwPelEnable(int axisNo, bool SwPelEnable)
        {
            return CommandStatus.Sucessed;
        }
        public CommandStatus SlowStop(int axisNo, decimal dec)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus WaitTableStop(int axisNo, decimal TimeOut = 1000M)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetHeaterSV(int axisNo, int valve)
        {
            return CommandStatus.Sucessed;
        }

        public string ReadHeaterPV(int axisNo)
        {
            return "0";
        }


        public CommandStatus DOOutput(int AxisNo, ushort DOChannel, enmCardIOONOFF OnOFF = enmCardIOONOFF.eOFF)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus GpAddArcPath(ref CSyncParameter SyncParameter, bool IsEndPath = false)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus GpAddDwell(ref CSyncParameter SyncParameter)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetCurve(int axisNo, eCurveMode curveMode)
        {
           System.Diagnostics. Debug.Print("Set Curve is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        /// <summary></summary>
        /// <param name="axisNo"></param>
        /// <param name="homeOffset"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetHomeOffset(int axisNo, decimal homeOffset)
        {
            return CommandStatus.Sucessed;
        }

        /// <summary>設定參考平面</summary>
        /// <param name="SyncParameter"></param>
        /// <param name="plane"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public object GpSetRefPlane(ref CSyncParameter SyncParameter, ePlane plane)
        {
            return CommandStatus.Sucessed;
        }

        /// <summary>設定運行模式</summary>
        /// <param name="syncParameter"></param>
        /// <param name="runMode"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GPSetRunMode(ref CSyncParameter syncParameter, eRunMode runMode, int blendingTime = 1024)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus GpSetCurve(ref CSyncParameter SyncParameter, eCurveMode curveMode)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetPulseOutReverse(int axisNo, enmPulseOutReverse pulseOutReverse)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus GpGetPathStatus(ref CSyncParameter SyncParameter, ref long remainCount)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus GpResetPath(ref CSyncParameter SyncParameter)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetMaxAcc(int axis, decimal maxAcc)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetMaxDec(int axis, decimal maxDec)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetMaxVel(int axis, decimal maxVel)
        {
            return CommandStatus.Sucessed;
        }

        //Eason 20170313 
        /// <summary>
        /// 表格補正
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="axisNo2"></param>
        /// <param name="originPosX"></param>
        /// <param name="originPosY"></param>
        /// <param name="pitchX"></param>
        /// <param name="pitchY"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus Dev2DCompensateTable(int axisNo, int axisNo2, decimal originPosX, decimal originPosY, decimal pitchX, decimal pitchY, ref decimal[,] offsetX, ref decimal[,] offsetY)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus Dev2DCompensateTableEnable(bool Enable)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus GetCompensatePosition(int axisNo, ref double OffsetValue)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus GpMoveLinearAbsXYZ(ref CSyncParameter SyncParameter)
        {
            return CommandStatus.Sucessed;
        }

    }
}
