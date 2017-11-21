using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Premtek.Base
{
    /// <summary>通訊結果傳回結構
    /// </summary>
    public struct sReceiveStatus
    {
        /// <summary>[是否接收完成]
        /// </summary>
        public bool Status;
        /// <summary>[原始字串]
        /// </summary>
        public string STR;
        /// <summary>[結果(處理完的資料內容)]
        /// </summary>
        public string Value;
    }

/// <summary>系統命令</summary>
    /// <remarks></remarks>
    public enum eSysCommand
    {
        /// <summary>無命令</summary>
        /// <remarks></remarks>
        None = 0,
        /// <summary>復歸</summary>
        /// <remarks></remarks>
        Home = 1,
        /// <summary>自動生產</summary>
        /// <remarks></remarks>
        AutoRun = 2,
        /// <summary>擦膠</summary>
        /// <remarks></remarks>
        ClearGlue = 3,
        /// <summary>排膠</summary>
        /// <remarks></remarks>
        Purge = 4,
        /// <summary>CCD閥自動校正</summary>
        /// <remarks></remarks>
        CCDValveAutoCalibrationXY = 5,
        /// <summary>單邊微量天平秤重</summary>
        /// <remarks></remarks>
        WeightUnit = 6,
        /// <summary>單邊手動微量天平秤重</summary>
        /// <remarks></remarks>
        ManuallyWeightUnit = 7,
        /// <summary>單邊換膠</summary>
        /// <remarks></remarks>
        ChangeGlue = 8,
        /// <summary>單邊閥自動測高</summary>
        /// <remarks></remarks>
        DispenserAutoSearch = 9,
        /// <summary>單邊雷射測高</summary>
        /// <remarks></remarks>
        LaserReader = 10,
        /// <summary>單邊CCD定位</summary>
        /// <remarks></remarks>
        CCDFix = 11,
        /// <summary>單邊CCD後檢</summary>
        /// <remarks></remarks>
        CCDSCanGlue = 12,
        /// <summary>單邊點膠</summary>
        /// <remarks></remarks>
        Dispensing = 13,
        /// <summary>產品資料載入</summary>
        /// <remarks></remarks>
        ProductLoading = 14,
        /// <summary>退料</summary>
        /// <remarks></remarks>
        ProductUnload = 15,
        /// <summary>進料</summary>
        /// <remarks></remarks>
        Loading = 16,
        /// <summary>A機進料</summary>
        /// <remarks></remarks>
        LoadA = 17,
        /// <summary>B機進料</summary>
        /// <remarks></remarks>
        LoadB = 18,
        /// <summary>B機退料 </summary>
        /// <remarks></remarks>
        UnloadB = 19,
        /// <summary>A機退料</summary>
        /// <remarks></remarks>
        UnloadA = 20,
        /// <summary>點膠前動作(秤重、Purge)</summary>
        /// <remarks></remarks>
        PrevDispense = 21,
        /// <summary>CCD-閥全自動校正(XYZ)</summary>
        /// <remarks></remarks>
        CCDValveFullAutoCalibration = 22,
        /// <summary>手動生產模式</summary>
        /// <remarks></remarks>
        ManualProduceMode = 23,
        // ''' <summary>單動模式[可單獨執行特定動作，例如：測重、清膠等等]</summary>
        // ''' <remarks></remarks>
        //SingleAction = 24
        /// <summary>[接續流程(接續之前沒做完的生產流程)]</summary>
        /// <remarks></remarks>
        ContinueLastRun = 25,
        /// <summary>[強制退料]</summary>
        /// <remarks></remarks>
        AbnormalUnload = 26,
        /// <summary>A機復歸</summary>
        /// <remarks></remarks>
        HomeA = 27,
        /// <summary>B機復歸</summary>
        /// <remarks></remarks>
        HomeB = 28,
        /// <summary>安全</summary>
        /// <remarks></remarks>
        Safe = 29,
        /// <summary>[監控]</summary>
        /// <remarks></remarks>
        Monitor = 30

    }

    /// <summary>
    /// 記錄狀態(AutoRunAction、Home)
    /// </summary>
    /// <remarks></remarks>
    public enum enmRunStatus
    {
        /// <summary>[尚未執行] </summary>
        /// <remarks></remarks>
        None = 0,
        /// <summary>[Runing] </summary>
        /// <remarks></remarks>
        Running = 1,
        /// <summary>停止生產, 下一步必須復歸</summary>
        /// <remarks></remarks>
        Stop = 2,
        // ''' <summary>[Pause] </summary>
        // ''' <remarks></remarks>
        //Pause = 2
        /// <summary>[Alarm]</summary>
        /// <remarks></remarks>
        Alarm = 3,
        /// <summary>[完成] </summary>
        /// <remarks></remarks>
        Finish = 4,
        /// <summary>[等待] </summary>
        /// <remarks></remarks>
        Waiting = 5
        // ''' <summary>暫停開門</summary>
        // ''' <remarks></remarks>
        //PauseDoorOpen = 6
        // ''' <summary>暫停開門再繼續</summary>
        // ''' <remarks></remarks>
        //PauseResume = 7
    }

    /// <summary>平台索引</summary>
    /// <remarks></remarks>
    public enum enmStage
    {
        /// <summary>第一組</summary>
        /// <remarks></remarks>
        No1 = 0,
        /// <summary>第二組</summary>
        /// <remarks></remarks>
        No2 = 1,
        /// <summary>第三組</summary>
        /// <remarks></remarks>
        No3 = 2,
        /// <summary>第四組</summary>
        /// <remarks></remarks>
        No4 = 3,
        /// <summary>最大數量</summary>
        /// <remarks></remarks>
        Max = No4
    }

    /// <summary>
    /// 原enmDsipenserNo
    /// </summary>
    /// <remarks></remarks>
    public enum enmValve
    {
        /// <summary>第一組</summary>
        /// <remarks></remarks>
        No1 = 0,
        /// <summary>第二組</summary>
        /// <remarks></remarks>
        No2 = 1,
        /// <summary>第三組</summary>
        /// <remarks></remarks>
        No3 = 2,
        /// <summary>第四組</summary>
        /// <remarks></remarks>
        No4 = 3,
        /// <summary>最大數量</summary>
        /// <remarks></remarks>
        Max = No4
    }

    /// <summary>單平台作業閥No.</summary>
    /// <remarks></remarks>
    public enum eValveWorkMode
    {
        None = -1,
        Valve1 = 0,
        Valve2 = 1,
        /// <summary>[多閥同動點膠]</summary>
        /// <remarks></remarks>
        MultiValve = 99,
        MaxValve = Valve2
        //Valve3 = 4
        //Valve4 = 8
        //預留參數，暫不開放
        //ComboValve12 = 3
        //ComboValve13 = 5
        //ComboValve14 = 9
        //ComboValve23 = 6
        //ComboValve24 = 10
        //ComboValve34 = 12
        //ComboValve123 = 7
        //ComboValve124 = 11
        //ComboValve134 = 13
        //ComboValve234 = 14
        //ComboValve1234 = 15
    }

    /// <summary>微量天平組數</summary>
    /// <remarks></remarks>SS
    public enum enmBalance
    {
        /// <summary>第一組天平</summary>
        /// <remarks></remarks>
        No1 = 0,
        /// <summary>第二組天平</summary>
        /// <remarks></remarks>
        No2 = 1,
        Max = No2
    }

    /// <summary>
    /// [機台索引值]
    /// </summary>
    /// <remarks></remarks>
    public enum enmMachineStation
    {
        /// <summary>機台A</summary>
        /// <remarks></remarks>
        MachineA = 0,
        /// <summary>機台B</summary>
        /// <remarks></remarks>
        MachineB = 1,
        /// <summary>總部的機台數量</summary>
        /// <remarks></remarks>
        MaxMachine = 1
    }

    /// <summary>[Conveyor索引值]</summary>
    /// <remarks></remarks>
    public enum eConveyor
    {
        /// <summary>[第一組Conveyor]</summary>
        /// <remarks></remarks>
        ConveyorNo1 = 0,
        /// <summary>第二組Conveyor</summary>
        /// <remarks></remarks>
        ConveyorNo2 = 1,
        Max = ConveyorNo2
    }
    /// <summary>動作基本參數</summary>
    /// <remarks></remarks>
    public struct sActParam
    {
        /// <summary>動作執行狀態</summary>
        /// <remarks></remarks>

        public enmRunStatus RunStatus;
    }


    /// <summary>動作</summary>
    /// <remarks></remarks>
    public struct eAct
    {
        /// <summary>微量天平秤重</summary>
        /// <remarks></remarks>
        public static int WeightUnit = 2;
        /// <summary>機台復歸</summary>
        /// <remarks></remarks>
        public static int Home = 3;
        /// <summary>自動生產</summary>
        /// <remarks></remarks>
        public static int AutoRun = 4;
        /// <summary>真空除膠</summary>
        /// <remarks></remarks>
        public static int Purge = 5;
        /// <summary>換膠</summary>
        /// <remarks></remarks>
        public static int ChangeGlue = 6;
        /// <summary>擦膠</summary>
        /// <remarks></remarks>
        public static int ClearGlue = 7;
        /// <summary>閥自動測高</summary>
        /// <remarks></remarks>
        public static int DispenserAutoSearch = 8;
        /// <summary>雷射測高</summary>
        /// <remarks></remarks>
        public static int LaserReader = 9;
        /// <summary>CCD定位</summary>
        /// <remarks></remarks>
        public static int CCDSCanFix = 10;
        /// <summary>CCD後檢</summary>
        /// <remarks></remarks>
        public static int CCDSCanGlue = 11;
        /// <summary>點膠</summary>
        /// <remarks></remarks>
        public static int Dispensing = 12;
        /// <summary>產品資料載入</summary>
        /// <remarks></remarks>
        public static int ProductLoading = 13;
        /// <summary>退料</summary>
        /// <remarks></remarks>
        public static int ProductUnload = 14;
        /// <summary>進料</summary>
        /// <remarks></remarks>

        public static int Loading = 15;
        /// <summary>閥自動校正</summary>
        /// <remarks></remarks>
        public static int AutoValveCalibration = 16;
        /// <summary>A機進料</summary>
        /// <remarks></remarks>
        public static int LoadA = 17;
        /// <summary>B機進料</summary>
        /// <remarks></remarks>
        public static int LoadB = 18;
        /// <summary>A機退料</summary>
        /// <remarks></remarks>
        public static int UnloadA = 19;
        /// <summary>B機退料</summary>
        /// <remarks></remarks>
        public static int UnloadB = 20;
        /// <summary>手動流程</summary>
        /// <remarks></remarks>
        public static int ManualAction = 21;
        /// <summary>Rerun流程</summary>
        /// <remarks></remarks>
        public static int Rerun = 22;
        /// <summary>[強制退料]</summary>
        /// <remarks></remarks>
        public static int AbnormalUnload = 23;
        /// <summary>[點膠前動作]</summary>
        /// <remarks></remarks>
        public static int PrevDispense = 24;
        /// <summary>最大值</summary>
        /// <remarks></remarks>
        public static int Max = 24;
    }


    /// <summary>
    /// 動作基本參數
    /// </summary>
    /// <remarks></remarks>
    public class sSysParam
    {
        /// <summary>[紀錄ESYS號碼]</summary>
        /// <remarks></remarks>
        public int EsysNum;
        /// <summary>接收命令</summary>
        /// <remarks></remarks>
        public eSysCommand Command;
        /// <summary>執行命令</summary>
        /// <remarks></remarks>
        public eSysCommand ExecuteCommand;
        /// <summary>系統執行步驟</summary>
        /// <remarks></remarks>
        public int SysNum;
        /// <summary>命令執行狀態</summary>
        /// <remarks></remarks>
        public enmRunStatus RunStatus;
        /// <summary>起始步驟</summary>
        /// <remarks></remarks>
        public const int SysLoopStart = 1000;
        /// <summary>搭配平台索引</summary>
        /// <remarks></remarks>
        public enmStage StageNo;
        /// <summary>設定閥件索引陣列</summary>
        /// <remarks></remarks>
        public enmValve[] ValveNo = new enmValve[(int)enmValve.Max + 1];
        private eValveWorkMode _ValveWorkMode;
        /// <summary>
        /// [Stage的閥體工作模式]
        /// </summary>
        /// <remarks></remarks>
        public eValveWorkMode SelectValve
        {
            get { return _ValveWorkMode; }
            set
            {
                switch (value)
                {
                    case eValveWorkMode.Valve1:
                        _ValveWorkMode = eValveWorkMode.Valve1;
                        break;
                    case eValveWorkMode.Valve2:
                        _ValveWorkMode = eValveWorkMode.Valve2;
                        break;
                }
            }
        }
        /// <summary>搭配天平索引</summary>
        /// <remarks></remarks>
        public enmBalance BalanceNo;
        /// <summary>搭配CCD索引</summary>
        /// <remarks></remarks>
        public int CCDNo;
        /// <summary>搭配測高索引</summary>
        /// <remarks></remarks>
        public int PinNo;
        /// <summary>搭配FMCS索引</summary>
        /// <remarks></remarks>
        public int FMCSNo;
        /// <summary>搭配清膠機構索引</summary>
        /// <remarks></remarks>
        public int ClearNo;
        /// <summary>搭配測高機構索引</summary>
        /// <remarks></remarks>
        public int LaserNo;
        /// <summary>[電空閥索引]</summary>
        /// <remarks></remarks>
        public int EPVNo;
        /// <summary>[搭配ValveControllerNo1機構索引]</summary>
        /// <remarks></remarks>
        public int ValveControllerNo1;
        /// <summary>搭配ValveControllerNo2機構索引</summary>
        /// <remarks></remarks>
        public int ValveControllerNo2;
        /// <summary>搭配Conveyor索引</summary>
        /// <remarks></remarks>
        public eConveyor ConveyorNo;
        /// <summary>搭配的Machine索引</summary>
        /// <remarks></remarks>
        public enmMachineStation MachineNo;
        /// <summary>等效X軸</summary>
        /// <remarks></remarks>
        public int AxisX;
        /// <summary>等效Y軸</summary>
        /// <remarks></remarks>
        public int AxisY;
        /// <summary>等效Z軸</summary>
        /// <remarks></remarks>
        public int AxisZ;
        /// <summary>等效A軸</summary>
        /// <remarks></remarks>
        public int AxisA;
        /// <summary>等效B軸</summary>
        /// <remarks></remarks>
        public int AxisB;
        /// <summary>等效C軸</summary>
        /// <remarks></remarks>
        public int AxisC;
        /// <summary>[等效Converter軸]</summary>
        /// <remarks></remarks>
        public int AxisConverter;
        /// <summary>未定義暫存用標記</summary>
        /// <remarks></remarks>
        public object Tag;
        /// <summary>產品進入的ID</summary>
        /// <remarks></remarks>
        public string WaferID;
        /// <summary>動作運行狀態 用於流程判定與保護 因系統動作無法自保持,因此另開記錄區間</summary>
        /// <remarks></remarks>
        public sActParam[] Act = new sActParam[eAct.Max + 1];
        public Stopwatch Timer = new Stopwatch();
        /// <summary>外部要求暫停</summary>
        /// <remarks></remarks>
        public bool ExternalPause;
        /// <summary>由流程決定外部是否可暫停</summary>
        /// <remarks></remarks>
        public bool IsCanPause = true;
    }

    public class CDefine
    {

    }
}
