using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO.Ports;

namespace Premtek.Base
{



    /// <summary>ModBus通訊馬達</summary>
    /// <remarks></remarks>
    public class CMotion_ModBus : IDisposable, IMotionCard
    {

        public struct Status
        {
            /// <summary>Z相狀態</summary>
            /// <remarks></remarks>
            public bool ZSG;
            /// <summary>馬達激磁狀態</summary>
            /// <remarks></remarks>
            public bool TIM;
            /// <summary>顯示資料No.</summary>
            /// <remarks></remarks>
            public bool M5_R;
            /// <summary>顯示資料No.</summary>
            /// <remarks></remarks>
            public bool M4_R;
            /// <summary>顯示資料No.</summary>
            /// <remarks></remarks>
            public bool M3_R;
            /// <summary>顯示資料No.</summary>
            /// <remarks></remarks>
            public bool M2_R;
            /// <summary>顯示資料No.</summary>
            /// <remarks></remarks>
            public bool M1_R;
            /// <summary></summary>
            /// <remarks></remarks>
            public bool M0_R;
            /// <summary>顯示START狀態</summary>
            /// <remarks></remarks>
            public bool START_R;
            /// <summary>過熱警告狀態</summary>
            /// <remarks></remarks>
            public bool OH;
            /// <summary>輸入狀態</summary>
            /// <remarks></remarks>
            public bool HOMES;
            /// <summary>SLIT輸入狀態</summary>
            /// <remarks></remarks>
            public bool SLIT;
            /// <summary>負極限</summary>
            /// <remarks></remarks>
            public bool NEL;
            /// <summary>正極限</summary>
            /// <remarks></remarks>
            public bool PEL;
            /// <summary>偏差異常狀態</summary>
            /// <remarks></remarks>
            public bool STEPOUT;
            /// <summary>驅動器警告狀態</summary>
            /// <remarks></remarks>
            public bool WNG;
            /// <summary>馬達激磁狀態</summary>
            /// <remarks></remarks>
            public bool ENABLE;
            /// <summary>驅動器的ALARM狀態</summary>
            /// <remarks></remarks>
            public bool ALM;
            /// <summary>顯示馬達位於區域範圍內</summary>
            /// <remarks></remarks>
            public bool AREA;
            /// <summary>顯示依RS-485通訊的內部處理狀態</summary>
            /// <remarks></remarks>
            public bool SBSY;
            /// <summary>顯示驅動器的運轉準備完成</summary>
            /// <remarks></remarks>
            public bool READY;
            /// <summary>顯示馬達位於原點</summary>
            /// <remarks></remarks>
            public bool HOME_P;
            /// <summary>顯示馬達的運轉狀態</summary>
            /// <remarks></remarks>
            public bool MOVE;
            /// <summary>反轉</summary>
            /// <remarks></remarks>
            public bool RVS;
            /// <summary>順轉</summary>
            /// <remarks></remarks>
            public bool FWD;
            public bool OUT4;
            public bool OUT3;
            public bool OUT2;
            public bool OUT1;
            public bool STOP;
            public bool AWO;
            public bool ALM_RST;
            public bool START;
            public string AlarmCode { get; set; }
            public bool HOME;
            public bool M5;
            public bool M4;
            public bool M3;
            public bool M2;
            public bool M1;
            public bool M0;
        }


        /// <summary>軸數</summary>
        /// <remarks></remarks>
        public int AxisCount { get; set; }

        public uint AxisNum { get; set; }

        public double[] EndArray { get; set; }
        public double[] CenArray { get; set; }

        #region "基底物件"


        /// <summary>序列埠物件</summary>
        /// <remarks></remarks>
        private SerialPort withEventsField_Subject = new SerialPort();
        public SerialPort Subject
        {
            get { return withEventsField_Subject; }
            set
            {
                if (withEventsField_Subject != null)
                {
                    withEventsField_Subject.DataReceived -= Subject_DataReceived;
                }
                withEventsField_Subject = value;
                if (withEventsField_Subject != null)
                {
                    withEventsField_Subject.DataReceived += Subject_DataReceived;
                }
            }
        }
        public CMotion_ModBus()
        {
            threadStart = new System.Threading.ThreadStart(ModBus_Sender);
            thread = new System.Threading.Thread(threadStart);
            CmdPos = new decimal[AxisCount + 1];
            feedbackPos = new decimal[AxisCount + 1];
            IOState = new int[AxisCount + 1];
            IOStatus = new Status[AxisCount + 1];
        }
        /// <summary>執行緒事件通知</summary>
        /// <remarks></remarks>
        System.Threading.AutoResetEvent mAutoWait = new System.Threading.AutoResetEvent(false);
        /// <summary>Queue的執行緒</summary>
        /// <remarks></remarks>
        System.Threading.ThreadStart threadStart;
        /// <summary>命令發送Queue的執行緒</summary>
        /// <remarks></remarks>
        System.Threading.Thread thread;
        /// <summary>內部計時器</summary>
        /// <remarks></remarks>
        Stopwatch mStopWatch = new Stopwatch();
        /// <summary>命令佇列(FIFO)</summary>
        /// <remarks></remarks>

        public Queue<string> CmdQueue = new Queue<string>();
        /// <summary>命令逾時(ms)</summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int TimeOut { get; set; }
        /// <summary>命令發送狀態</summary>
        /// <remarks></remarks>
        public CommandStatus cmdStatus { get; set; }

        /// <summary>命令位置</summary>
        /// <remarks></remarks>
        decimal[] CmdPos;
        /// <summary>回授位置</summary>
        /// <remarks></remarks>
        decimal[] feedbackPos;
        ///' <summary>驅動器傳回信號</summary>
        ///' <remarks></remarks>
        //Dim IOStatus(AxisCount) As Integer
        /// <summary>直接IO訊號 00D4h</summary>
        /// <remarks></remarks>

        int[] IOState;
        /// <summary>接收字串</summary>
        /// <remarks></remarks>
        string RecievedData;
        /// <summary>送出命令</summary>
        /// <remarks></remarks>
        string CommandSend;
        /// <summary>從站位置</summary>
        /// <remarks></remarks>
        const int StationByte = 0;
        /// <summary>功能碼所在位置</summary>
        /// <remarks></remarks>
        const int CmdByte = 1;
        /// <summary>暫存器 資料位置為Hex轉數值</summary>
        /// <remarks></remarks>
        public Dictionary<int, int> Register = new Dictionary<int, int>();

        Stopwatch stopWatch = new Stopwatch();

        #endregion

        /// <summary>卡上各軸型號對照表</summary>
        /// <remarks></remarks>
        enmAxisType[] mAxisType;
        /// <summary>
        /// 卡上各軸IO狀態 索引為實際軸號
        /// </summary>
        /// <remarks></remarks>
        IOStatus[] mAxisIOStatus;
        /// <summary>
        /// 卡上各軸馬達狀態
        /// </summary>
        /// <remarks></remarks>

        eMotorType[] mAxisMotor;
        #region "內部功能"
        // ''' <summary>單軸馬達參數</summary>
        // ''' <remarks></remarks>
        //Dim AxisParam(AxisCount) As SMotor
        /// <summary>取得暫存器上下位合併值</summary>
        /// <param name="addressU"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int GetRegisterULValue(int addressU)
        {
            return Register[addressU] * (byte.MaxValue + 1) + Register[addressU + 1];
        }
        /// <summary>暫存器數值轉帶符號整數</summary>
        /// <param name="addressU"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public long RegisterULValueToSignedInteger(int addressU)
        {
            //判定最高位元為負
            if ((Register[addressU] & 0x8000) != 0)
            {
                //Return 4294967294 - (Register(addressU) * 65536 + Register(addressU + 1))
                Int64 iReg = Register[addressU];
                Int64 iReg2 = 65536;
                Int64 iReg3 = iReg * iReg2;
                Int64 iReg4 = Register[addressU + 1];
                Int64 i = iReg3 + iReg4;
                return i - 4294967294L;
                //正數
            }
            else
            {
                return Register[addressU] * (byte.MaxValue + 1) + Register[addressU + 1];
            }
        }
        /// <summary>Hex轉Byte數值</summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public byte HexToByte(string hex)
        {
            //已經是直接轉
            if (hex.StartsWith("&H"))
            {
                return Convert.ToByte(hex);
            }
            return Convert.ToByte("&H" + hex);
        }
        /// <summary>傳回值檢查</summary>
        /// <param name="Cmd"></param>
        /// <param name="retVal"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool CheckAck(ref string Cmd, ref string retVal)
        {
            string[] cmdData = Cmd.TrimEnd(' ').Split(' ');
            string[] retData = retVal.TrimEnd(' ').Split(' ');
            //查無檢查碼
            if (cmdData.Length <= 2)
            {
                return false;
            }
            //站號不同
            if (cmdData[StationByte] != retData[StationByte])
            {
                return false;
            }
            if (cmdData[CmdByte] != retData[CmdByte])
            {
                return false;
            }
            switch (cmdData[CmdByte])
            {
                //功能碼
                case "03":
                    //讀取
                    int RegisterStart = 0;
                    //起始暫存器(Hex已轉數值)
                    byte cmdStartByte = 0;
                    byte cmdEndByte = 0;
                    cmdStartByte = HexToByte(cmdData[2]);
                    cmdEndByte = HexToByte(cmdData[3]);
                    RegisterStart = BigEndianToInteger(cmdStartByte, cmdEndByte);
                    int registerCount = 0;
                    //寄存器數量
                    cmdStartByte = HexToByte(cmdData[4]);
                    cmdEndByte = HexToByte(cmdData[5]);
                    registerCount = BigEndianToInteger(cmdStartByte, cmdEndByte);

                    //詢問數量與傳回數量不符
                    if (Convert.ToInt32(retData[2]) != registerCount * 2)
                    {
                        return false;
                    }
                    for (int i = 0; i <= registerCount - 1; i++)
                    {
                        cmdStartByte = HexToByte(retData[3 + 2 * i]);
                        cmdEndByte = HexToByte(retData[4 + 2 * i]);
                        int retInt = BigEndianToInteger(cmdStartByte, cmdEndByte);
                        Register[RegisterStart + i] = retInt;
                    }

                    switch (mAxisType[Convert.ToInt32(cmdData[StationByte])])
                    {
                        case enmAxisType.WT404:
                            string TempData = "";
                            Int32 returnValve = 0;

                            TempData = retData[3] + retData[4];
                            returnValve = Convert.ToInt32(TempData, 16);
                            feedbackPos[Convert.ToInt32(retData[StationByte])] = returnValve;

                            break;

                        case enmAxisType.HM60163C:
                            switch (RegisterStart)
                            {
                                case 32:
                                    //20h 驅動器輸出指令 Revised HM
                                    int iosts = Register[RegisterStart];
                                    int _No = Convert.ToInt32(cmdData[StationByte]);

                                    IOStatus[_No].M0_R = Convert.ToBoolean(iosts & 0x1);
                                    IOStatus[_No].M1_R = Convert.ToBoolean(iosts & 0x2);
                                    IOStatus[_No].M2_R = Convert.ToBoolean(iosts & 0x4);
                                    IOStatus[_No].M3_R = Convert.ToBoolean(iosts & 0x8);
                                    IOStatus[_No].M4_R = Convert.ToBoolean(iosts & 0x10);
                                    IOStatus[_No].M5_R = Convert.ToBoolean(iosts & 0x20);
                                    IOStatus[_No].WNG = Convert.ToBoolean(iosts & 0x40);
                                    IOStatus[_No].ALM = Convert.ToBoolean(iosts & 0x80);
                                    mAxisIOStatus[Convert.ToInt32(cmdData[StationByte])].blnALM = Convert.ToBoolean(iosts & 0x80);
                                    IOStatus[_No].START_R = Convert.ToBoolean(iosts & 0x100);
                                    IOStatus[_No].STEPOUT = Convert.ToBoolean(iosts & 0x200);
                                    IOStatus[_No].MOVE = Convert.ToBoolean(iosts & 0x400);
                                    IOStatus[_No].HOME_P = Convert.ToBoolean(iosts & 0x800);
                                    IOStatus[_No].READY = Convert.ToBoolean(iosts & 0x2000);
                                    mAxisIOStatus[Convert.ToInt32(cmdData[StationByte])].blnINP = Convert.ToBoolean(iosts & 0x2000);
                                    IOStatus[_No].AREA = Convert.ToBoolean(iosts & 0x8000);
                                    iosts = Register[RegisterStart + 1];
                                    IOStatus[_No].ZSG = Convert.ToBoolean(iosts & 0x10);
                                    mAxisIOStatus[Convert.ToInt32(cmdData[StationByte])].blnEZ = Convert.ToBoolean(iosts & 0x10);

                                    IOStatus[_No].TIM = Convert.ToBoolean(iosts & 0x8);
                                    IOStatus[_No].OH = Convert.ToBoolean(iosts & 0x4);
                                    IOStatus[_No].ENABLE = Convert.ToBoolean(iosts & 0x2);
                                    IOStatus[_No].SBSY = Convert.ToBoolean(iosts & 0x1);

                                    break;
                                case 33:
                                    //21H
                                    _No = Convert.ToInt32(cmdData[StationByte]);
                                    iosts = Register[RegisterStart];
                                    IOStatus[_No].ZSG = Convert.ToBoolean(iosts & 0x10);
                                    mAxisIOStatus[Convert.ToInt32(cmdData[StationByte])].blnEZ = Convert.ToBoolean(iosts & 0x10);
                                    IOStatus[_No].TIM = Convert.ToBoolean(iosts & 0x8);
                                    IOStatus[_No].OH = Convert.ToBoolean(iosts & 0x4);
                                    IOStatus[_No].ENABLE = Convert.ToBoolean(iosts & 0x2);
                                    IOStatus[_No].SBSY = Convert.ToBoolean(iosts & 0x2);
                                    break;
                                case 256:
                                    //100h Alarm Code
                                    IOStatus[Convert.ToInt32(cmdData[StationByte])].AlarmCode = Register[RegisterStart].ToString();
                                    Debug.Print("Alarm Code:" + IOStatus[Convert.ToInt32(cmdData[StationByte])].AlarmCode);
                                    break;
                                case 280:
                                    //118h 指令位置
                                    CmdPos[Convert.ToInt32(cmdData[StationByte])] = RegisterULValueToSignedInteger(RegisterStart);
                                    if (!(mAxisMotor[Convert.ToInt32(cmdData[StationByte])] == eMotorType.ServoMotor))
                                    {
                                        feedbackPos[Convert.ToInt32(cmdData[StationByte])] = CmdPos[Convert.ToInt32(cmdData[StationByte])];
                                        Debug.Print("01 18 h:" + feedbackPos[Convert.ToInt32(cmdData[StationByte])]);
                                    }
                                    else
                                    {
                                        Debug.Print("01 18 h:" + CmdPos[Convert.ToInt32(cmdData[StationByte])]);
                                    }

                                    break;
                                case 286:
                                    //11Eh'回授位置
                                    if (mAxisMotor[Convert.ToInt32(cmdData[StationByte])] == eMotorType.ServoMotor)
                                    {
                                        feedbackPos[Convert.ToInt32(cmdData[StationByte])] = RegisterULValueToSignedInteger(RegisterStart);
                                    }
                                    Debug.Print("01 1E h:" + feedbackPos[Convert.ToInt32(cmdData[StationByte])]);
                                    break;
                                case 294:
                                    //126h
                                    _No = Convert.ToInt32(cmdData[StationByte]);
                                    iosts = Register[RegisterStart];
                                    IOStatus[_No].OUT4 = Convert.ToBoolean(iosts & 0x2000);
                                    IOStatus[_No].OUT3 = Convert.ToBoolean(iosts & 0x1000);
                                    IOStatus[_No].OUT2 = Convert.ToBoolean(iosts & 0x800);
                                    IOStatus[_No].OUT1 = Convert.ToBoolean(iosts & 0x400);
                                    IOStatus[_No].ALM = Convert.ToBoolean(iosts & 0x200);
                                    mAxisIOStatus[Convert.ToInt32(cmdData[StationByte])].blnALM = Convert.ToBoolean(iosts & 0x200);
                                    IOStatus[_No].MOVE = Convert.ToBoolean(iosts & 0x100);
                                    IOStatus[_No].SLIT = Convert.ToBoolean(iosts & 0x8);
                                    IOStatus[_No].HOMES = Convert.ToBoolean(iosts & 0x4);
                                    mAxisIOStatus[Convert.ToInt32(cmdData[StationByte])].blnORG = Convert.ToBoolean(iosts & 0x4);
                                    IOStatus[_No].NEL = Convert.ToBoolean(iosts & 0x2);
                                    IOStatus[_No].PEL = Convert.ToBoolean(iosts & 0x1);
                                    iosts = Register[RegisterStart + 1];
                                    IOStatus[_No].RVS = Convert.ToBoolean(iosts & 0x4000);
                                    IOStatus[_No].FWD = Convert.ToBoolean(iosts & 0x2000);
                                    IOStatus[_No].HOME = Convert.ToBoolean(iosts & 0x1000);
                                    IOStatus[_No].STOP = Convert.ToBoolean(iosts & 0x800);
                                    IOStatus[_No].AWO = Convert.ToBoolean(iosts & 0x400);
                                    IOStatus[_No].ALM_RST = Convert.ToBoolean(iosts & 0x200);
                                    IOStatus[_No].START = Convert.ToBoolean(iosts & 0x100);
                                    IOStatus[_No].M5 = Convert.ToBoolean(iosts & 0x20);
                                    IOStatus[_No].M4 = Convert.ToBoolean(iosts & 0x10);
                                    IOStatus[_No].M3 = Convert.ToBoolean(iosts & 0x8);
                                    IOStatus[_No].M2 = Convert.ToBoolean(iosts & 0x4);
                                    IOStatus[_No].M1 = Convert.ToBoolean(iosts & 0x2);
                                    IOStatus[_No].M0 = Convert.ToBoolean(iosts & 0x1);

                                    break;
                                case 307:
                                    //133H
                                    iosts = Register[RegisterStart];
                                    IOStatus[Convert.ToInt32(cmdData[StationByte])].AlarmCode = (iosts % 256).ToString();
                                    _No = Convert.ToInt32(cmdData[StationByte]);
                                    IOStatus[_No].ZSG = Convert.ToBoolean(iosts & 0x80);
                                    mAxisIOStatus[Convert.ToInt32(cmdData[StationByte])].blnEZ = Convert.ToBoolean(iosts & 0x80);
                                    IOStatus[_No].TIM = Convert.ToBoolean(iosts & 0x40);
                                    IOStatus[_No].M5_R = Convert.ToBoolean(iosts & 0x20);
                                    IOStatus[_No].M4_R = Convert.ToBoolean(iosts & 0x10);
                                    IOStatus[_No].M3_R = Convert.ToBoolean(iosts & 0x8);
                                    IOStatus[_No].M2_R = Convert.ToBoolean(iosts & 0x4);
                                    IOStatus[_No].M1_R = Convert.ToBoolean(iosts & 0x2);
                                    IOStatus[_No].M0_R = Convert.ToBoolean(iosts & 0x1);

                                    iosts = Register[RegisterStart + 1];
                                    IOStatus[_No].START_R = Convert.ToBoolean(iosts & 0x8000);
                                    IOStatus[_No].OH = Convert.ToBoolean(iosts & 0x4000);
                                    IOStatus[_No].HOMES = Convert.ToBoolean(iosts & 0x2000);
                                    mAxisIOStatus[Convert.ToInt32(cmdData[StationByte])].blnORG = Convert.ToBoolean(iosts & 0x2000);
                                    IOStatus[_No].SLIT = Convert.ToBoolean(iosts & 0x1000);
                                    IOStatus[_No].NEL = Convert.ToBoolean(iosts & 0x800);
                                    mAxisIOStatus[Convert.ToInt32(cmdData[StationByte])].blnNEL = Convert.ToBoolean(iosts & 0x800);
                                    IOStatus[_No].PEL = Convert.ToBoolean(iosts & 0x400);
                                    mAxisIOStatus[Convert.ToInt32(cmdData[StationByte])].blnPEL = Convert.ToBoolean(iosts & 0x400);
                                    IOStatus[_No].STEPOUT = Convert.ToBoolean(iosts & 0x200);
                                    IOStatus[_No].WNG = Convert.ToBoolean(iosts & 0x100);
                                    IOStatus[_No].ENABLE = Convert.ToBoolean(iosts & 0x80);
                                    IOStatus[_No].ALM = Convert.ToBoolean(iosts & 0x40);
                                    mAxisIOStatus[Convert.ToInt32(cmdData[StationByte])].blnALM = Convert.ToBoolean(iosts & 0x40);
                                    IOStatus[_No].AREA = Convert.ToBoolean(iosts & 0x20);
                                    IOStatus[_No].SBSY = Convert.ToBoolean(iosts & 0x10);
                                    IOStatus[_No].READY = Convert.ToBoolean(iosts & 0x8);
                                    IOStatus[_No].HOME_P = Convert.ToBoolean(iosts & 0x4);
                                    IOStatus[_No].MOVE = Convert.ToBoolean(iosts & 0x1);
                                    break;
                            }
                            break;
                        case enmAxisType.RK2:
                            switch (RegisterStart)
                            {
                                case 127:
                                    //7F 驅動器輸出指令
                                    int iosts = GetRegisterULValue(RegisterStart);
                                    int _No = Convert.ToInt32(cmdData[StationByte]);
                                    IOStatus[_No].M0_R = Convert.ToBoolean(iosts & 0x1);
                                    IOStatus[_No].M1_R = Convert.ToBoolean(iosts & 0x2);
                                    IOStatus[_No].M2_R = Convert.ToBoolean(iosts & 0x4);
                                    IOStatus[_No].START_R = Convert.ToBoolean(iosts & 0x8);
                                    IOStatus[_No].HOME_P = Convert.ToBoolean(iosts & 0x10);
                                    IOStatus[_No].READY = Convert.ToBoolean(iosts & 0x20);
                                    IOStatus[_No].WNG = Convert.ToBoolean(iosts & 0x40);
                                    IOStatus[_No].ALM = Convert.ToBoolean(iosts & 0x80);
                                    IOStatus[_No].SBSY = Convert.ToBoolean(iosts & 0x100);
                                    IOStatus[_No].AREA = Convert.ToBoolean(iosts & 0x200);
                                    IOStatus[_No].TIM = Convert.ToBoolean(iosts & 0x1000);
                                    IOStatus[_No].MOVE = Convert.ToBoolean(iosts & 0x2000);
                                    IOStatus[_No].STEPOUT = Convert.ToBoolean(iosts & 0x8000);
                                    break;
                                case 198:
                                    //C6 指令位置
                                    CmdPos[Convert.ToInt32(cmdData[StationByte])] = GetRegisterULValue(RegisterStart);
                                    if (!(mAxisMotor[Convert.ToInt32(cmdData[StationByte])] == eMotorType.ServoMotor))
                                    {
                                        feedbackPos[Convert.ToInt32(cmdData[StationByte])] = CmdPos[Convert.ToInt32(cmdData[StationByte])];
                                    }
                                    break;
                                case 204:
                                    //CC 反饋位置
                                    feedbackPos[Convert.ToInt32(cmdData[StationByte])] = GetRegisterULValue(RegisterStart);
                                    break;
                                case 212:
                                    //D4 直接IO
                                    iosts = GetRegisterULValue(RegisterStart);
                                    _No = Convert.ToInt32(cmdData[StationByte]);

                                    IOStatus[_No].OUT1 = bool.Parse((iosts & 0x2).ToString());
                                    IOStatus[_No].OUT2 = bool.Parse((iosts & 0x4).ToString());
                                    IOStatus[_No].OUT3 = bool.Parse((iosts & 0x8).ToString());
                                    IOStatus[_No].OUT4 = bool.Parse((iosts & 0x10).ToString());
                                    iosts = GetRegisterULValue(RegisterStart + 1);
                                    IOStatus[_No].PEL = bool.Parse((iosts & 0x10).ToString());
                                    mAxisIOStatus[Convert.ToInt32(cmdData[StationByte])].blnPEL = bool.Parse((iosts & 0x10).ToString());
                                    IOStatus[_No].NEL = bool.Parse((iosts & 0x20).ToString());
                                    mAxisIOStatus[Convert.ToInt32(cmdData[StationByte])].blnNEL = bool.Parse((iosts & 0x20).ToString());
                                    IOStatus[_No].HOMES = bool.Parse((iosts & 0x40).ToString());
                                    mAxisIOStatus[Convert.ToInt32(cmdData[StationByte])].blnORG = bool.Parse((iosts & 0x40).ToString());
                                    IOStatus[_No].SLIT = bool.Parse((iosts & 0x80).ToString());
                                    break;
                            }
                            break;
                    }

                    return true;
                case "06":
                    //寫入
                    for (int i = 0; i <= cmdData.GetUpperBound(0); i++)
                    {
                        if (cmdData[i] != retData[i])
                        {
                            return false;
                        }
                    }

                    //傳回值驗證OK
                    return true;
                case "08":
                    //診斷
                    if (cmdData[2] != "0")
                    {
                        return false;
                        //命令錯誤
                    }
                    if (cmdData[3] != "0")
                    {
                        return false;
                        //命令錯誤
                    }
                    for (int i = 0; i <= cmdData.GetUpperBound(0); i++)
                    {
                        if (cmdData[i] != retData[i])
                        {
                            return false;
                        }
                    }

                    //傳回值驗證OK
                    return true;
                case "10":
                    //批次寫入
                    for (int i = 0; i <= 5; i++)
                    {
                        if (cmdData[i] != retData[i])
                        {
                            return false;
                        }
                    }

                    //傳回值驗證OK
                    return true;
            }
            return false;
        }

        public byte[] StringToByte(string strData, ref int count)
        {
            string[] SplitedData = strData.TrimEnd(' ').Split(' ');

            byte[] bytData = new byte[SplitedData.GetUpperBound(0) + 1];
            for (int i = 0; i <= SplitedData.GetUpperBound(0); i++)
            {
                bytData[i] = Convert.ToByte("&H" + SplitedData[i]);
            }
            count = bytData.GetUpperBound(0) + 1;
            return bytData;
        }
        /// <summary>逾時計時</summary>
        /// <remarks></remarks>
        System.Threading.Timer ModBusTimer;
        /// <summary>MODBUS逾時</summary>
        /// <remarks></remarks>
        bool mIsModBusTimeOut;
        /// <summary>逾時重試次數</summary>
        /// <remarks></remarks>
        int mRetryCount = 3;
        public void ModBusTimeOut(object state)
        {
            mAutoWait.Set();
            mIsModBusTimeOut = true;
        }

        /// <summary>命令發送端</summary>
        /// <remarks></remarks>
        public void ModBus_Sender()
        {
            byte[] bytData = null;
            int count = 0;
            do
            {
                //有命令
                if (CmdQueue.Count != 0)
                {
                    cmdStatus = CommandStatus.Sending;
                    //Debug.Print("cmdStatus = CommandStatus.Sending")
                    if (Subject.IsOpen == false)
                    {
                        Debug.Print("Port Is Close...ModBus Closed");
                        break; // TODO: might not be correct. Was : Exit Do
                    }
                    //無須讀取
                    if (Subject.BytesToRead == 0)
                    {
                        RecievedData = "";
                        CommandSend = CmdQueue.Dequeue();
                        bytData = StringToByte(CommandSend, ref count);
                        //轉為Byte資料
                        mIsModBusTimeOut = false;
                        for (int mRetryNo = 0; mRetryNo <= mRetryCount - 1; mRetryNo++)
                        {
                            Subject.Write(bytData, 0, count);
                            ModBusTimer = new System.Threading.Timer(ModBusTimeOut, null, 0, 3000);
                            //Debug.Print("Subject.Write:" & CommandSend)
                            mAutoWait.WaitOne();
                            //等待接收完成
                            //寫入成功, 離開迴圈
                            if (mIsModBusTimeOut == false)
                            {
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }

                        //Debug.Print("CheckAck:" & RecievedData)
                        if (CheckAck(ref CommandSend, ref RecievedData) & mIsModBusTimeOut == false)
                        {
                            //Debug.Print("通過CheckAck檢查")
                        }
                        else
                        {
                            cmdStatus = CommandStatus.Alarm;
                            Debug.Print("cmdStatus = CommandStatus.Alarm");
                        }

                    }
                    else
                    {
                        Debug.Print("Subject.BytesToRead: " + Subject.BytesToRead);
                        mAutoWait.WaitOne();
                        //資料處理中,等待
                    }
                }
                else
                {
                    cmdStatus = CommandStatus.Sucessed;
                    //Debug.Print("cmdStatus = CommandStatus.Sucessed")
                    mAutoWait.WaitOne();
                    //無命令,等待
                }
            } while (true);
        }

        /// <summary>給定上下位,傳回值</summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public int BigEndianToInteger(byte bytDataU, byte bytDataL)
        {
            return bytDataU * (byte.MaxValue + 1) + bytDataL;
        }

        /// <summary>數值拆為上下位Hex</summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string[] IntegerToBigEndian(int value)
        {
            string[] strData = new string[4];
            //正數
            if (value >= 0)
            {
                dynamic data = Convert.ToString(value, 16).PadLeft(8, '0');
                for (int i = 0; i <= data.Length / 2 - 1; i++)
                {
                    strData[data.Length / 2 - 1 - i] = data.Substring(data.Length - 2 - 2 * i, 2);
                }
                //負數
            }
            else
            {
                dynamic data = Convert.ToString(value, 16);
                for (int i = 0; i <= data.Length / 2 - 1; i++)
                {
                    strData[data.Length / 2 - 1 - i] = data.Substring(data.Length - 2 - 2 * i, 2);
                }
            }
            return strData;
        }

        /// <summary>取得命令的CRC</summary>
        /// <param name="strCmd"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string GetCmdCRC(string strCmd)
        {
            string[] strData = strCmd.TrimEnd(' ').Split(' ');
            byte[] bytData = new byte[strData.GetUpperBound(0) + 3];

            for (int i = 0; i <= strData.GetUpperBound(0); i++)
            {
                bytData[i] = Convert.ToByte("&H" + strData[i]);
            }

            byte[] tmpData = CRC16(bytData, strData.GetUpperBound(0));

            bytData[bytData.GetUpperBound(0) - 1] = tmpData[1];
            bytData[bytData.GetUpperBound(0)] = tmpData[0];

            string CRCData = "";
            for (int i = bytData.GetUpperBound(0) - 1; i <= bytData.GetUpperBound(0); i++)
            {
                CRCData += Convert.ToString(bytData[i], 16).PadLeft(2, '0') + " ";
            }
            return CRCData.TrimEnd(' ');
        }

        /// <summary>補上檢查碼</summary>
        /// <param name="strCmd"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string PadCmdCRC(string strCmd)
        {
            string[] strData = strCmd.TrimEnd(' ').Split(' ');
            byte[] bytData = new byte[strData.GetUpperBound(0) + 3];

            for (int i = 0; i <= strData.GetUpperBound(0); i++)
            {
                bytData[i] = Convert.ToByte("&H" + strData[i]);
            }

            byte[] tmpData = CRC16(bytData, strData.GetUpperBound(0));

            bytData[bytData.GetUpperBound(0) - 1] = tmpData[1];
            bytData[bytData.GetUpperBound(0)] = tmpData[0];

            string FullCmd = "";
            for (int i = 0; i <= bytData.GetUpperBound(0); i++)
            {
                FullCmd += Convert.ToString(bytData[i], 16).PadLeft(2, '0') + " ";
            }
            return FullCmd;
        }


        public string[] SplitHexData(int value)
        {
            string[] strData = new string[4];
            //正數
            if (value >= 0)
            {
                string data = Convert.ToString(value, 16).PadLeft(8, '0');
                for (int i = 0; i <= data.Length / 2 - 1; i++)
                {
                    strData[data.Length / 2 - 1 - i] = data.Substring(data.Length - 2 - 2 * i, 2);
                }
                //負數
            }
            else
            {
                string data = Convert.ToString(value, 16);
                for (int i = 0; i <= data.Length / 2 - 1; i++)
                {
                    strData[data.Length / 2 - 1 - i] = data.Substring(data.Length - 2 - 2 * i, 2);
                }
            }
            return strData;
        }

        public byte[] CRC16(byte[] data, int iNum)
        {
            byte CRC16Lo = 0;
            byte CRC16Hi = 0;
            //CRC寄存器
            byte CL = 0;
            byte CH = 0;
            //多項式&HA001
            byte SaveHi = 0;
            byte SaveLo = 0;
            int I = 0;
            int Flag = 0;

            CRC16Lo = 0xff;
            CRC16Hi = 0xff;
            CL = 0x1;
            CH = 0xa0;
            for (I = 0; I <= iNum; I++)
            {
                CRC16Lo = (byte)(CRC16Lo ^ data[I]);
                for (Flag = 0; Flag <= 7; Flag++)
                {
                    SaveHi = CRC16Hi;
                    SaveLo = CRC16Lo;
                    CRC16Hi = (byte)(CRC16Hi / 2);
                    //
                    CRC16Lo = (byte)(CRC16Lo / 2);
                    //
                    //
                    if (((SaveHi & 0x1) == 0x1))
                    {
                        CRC16Lo = (byte)(CRC16Lo | 0x80);
                        //
                    }
                    //
                    //
                    if (((SaveLo & 0x1) == 0x1))
                    {
                        CRC16Hi = (byte)(CRC16Hi ^ CH);
                        CRC16Lo = (byte)(CRC16Lo ^ CL);
                    }
                }
            }

            byte[] ReturnData = new byte[2];
            ReturnData[0] = CRC16Hi;
            //CRC高位
            ReturnData[1] = CRC16Lo;
            //CRC低位
            return ReturnData;
        }

        private void Subject_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int byteUBound = 0;
            byteUBound = Subject.BytesToRead - 1;
            byte[] data = new byte[byteUBound + 1];
            for (int i = 0; i <= byteUBound; i++)
            {
                data[i] = (byte)Subject.ReadByte();
                RecievedData += Convert.ToString(data[i], 16).PadLeft(2, '0') + " ";
            }

            string[] splitedData = RecievedData.Trim(' ').Split(' ');
            if (splitedData.GetUpperBound(0) < 3)
            {
                //Debug.Print("DataRecieved資料長度不足" & RecievedData)
                return;
            }

            string rawdata = "";
            for (int i = 0; i <= splitedData.GetUpperBound(0) - 2; i++)
            {
                rawdata += splitedData[i] + " ";
            }
            rawdata = rawdata.Trim(' ');

            string crcdata = "";
            for (int i = splitedData.GetUpperBound(0) - 1; i <= splitedData.GetUpperBound(0); i++)
            {
                crcdata += splitedData[i] + " ";
            }
            crcdata = crcdata.Trim(' ');
            //RecievedData.Substring(RecievedData.Length - 2)
            //檢查通過
            if (GetCmdCRC(rawdata) == crcdata)
            {
                ModBusTimer.Dispose();
                //接收資料完成, 逾時計時清除
                //RaiseEvent OnDataReceived(sender, e, RecievedData)
                //Debug.Print("DataRecieved CRC檢查通過" & RecievedData)
                mAutoWait.Set();
            }
            else
            {
                //Debug.Print("DataRecieved CRC檢查失敗" & RecievedData)
            }
        }

        public void EnqueueBatchCmd(int axisNo, string register, decimal value)
        {
            string axisCmd = axisNo.ToString().PadLeft(2, '0');

            string CmdVel = null;
            string[] strData = IntegerToBigEndian((int)value);
            //01 從站位置
            //10 批次寫入
            //register Register起點
            //00 02 Register數量
            //04 Register數量x2
            CmdVel = axisNo.ToString().PadLeft(2, '0') + " 10 " + register + " 00 02 04 ";
            for (int i = 0; i <= strData.GetUpperBound(0); i++)
            {
                CmdVel += strData[i] + " ";
            }

            CmdVel = PadCmdCRC(CmdVel);
            CmdQueue.Enqueue(CmdVel);
            //壓入設定命令
        }

        /// <summary>簡易批次命令 由某Register位置寫Value值入兩個Register</summary>
        /// <param name="axisNo"></param>
        /// <param name="register"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus EasyBatchCmd(int axisNo, string register, decimal value)
        {
            string axisCmd = axisNo.ToString().PadLeft(2, '0');
            //Dim Cmd As String = axisCmd & " 03 01 00 00 02"

            //--- 不是可發命令狀態時,傳回前一值 ---
            if (cmdStatus != CommandStatus.Sucessed)
            {
                return cmdStatus;
            }
            //--- 不是可發命令狀態時,傳回前一值 ---

            string CmdVel = null;
            string[] strData = IntegerToBigEndian((int)value);
            //01 從站位置
            //10 批次寫入
            //register Register起點
            //00 02 Register數量
            //04 Register數量x2
            CmdVel = axisNo.ToString().PadLeft(2, '0') + " 10 " + register + " 00 02 04 ";
            for (int i = 0; i <= strData.GetUpperBound(0); i++)
            {
                CmdVel += strData[i] + " ";
            }

            CmdVel = PadCmdCRC(CmdVel);
            CmdQueue.Enqueue(CmdVel);
            //壓入設定命令
            mAutoWait.Set();
            cmdStatus = CommandStatus.Sending;
            return cmdStatus;
        }

        #endregion

        /// <summary>IO狀態</summary>
        /// <remarks></remarks>
        Status[] IOStatus;
        /// <summary>通訊埠是否開啟</summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsOpen()
        {
            return Subject.IsOpen;
        }
        /// <summary>鮑率(Baud Rate)</summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int BaudRate
        {
            get { return Subject.BaudRate; }
            set { Subject.BaudRate = value; }
        }
        /// <summary>同位檢查位元</summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public System.IO.Ports.Parity Parity
        {
            get { return Subject.Parity; }
            set { Subject.Parity = value; }
        }

        /// <summary>自製命令發送</summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus Write(string str)
        {
            Subject.Write(str);
            mAutoWait.Set();
            return CommandStatus.Sucessed;
        }
        public CommandStatus Write(ref byte[] buffer, int offset, int count)
        {
            Subject.Write(buffer, offset, count);
            return CommandStatus.Sucessed;
        }

        // ''' <summary>資料接收事件</summary>
        // ''' <param name="recievedData"></param>
        // ''' <remarks></remarks>
        //Public Event OnDataReceived(sender As Object, e As SerialDataReceivedEventArgs, ByVal recievedData As String)

        #region "已處理動作"

        //Public Function VMove(ByVal axisNo As Integer, ByVal vel As Integer, ByVal direction As Integer) As CommandStatus
        public CommandStatus JogPlus(int axisNo, eDirection direction)
        {
            //--- 不是可發命令狀態時,傳回前一值 ---
            if (cmdStatus != CommandStatus.Sucessed)
            {
                return cmdStatus;
            }
            //--- 不是可發命令狀態時,傳回前一值 ---
            //"01 06 00 7D 40 00"
            //Dim CmdVel As String = ""
            string CmdRun = "";
            //Dim strData() As String = IntegerToBigEndian(vel)
            //01 從站位置
            //10 批次寫入
            //04 80 Register起點
            //00 02 Register數量
            //04 Register數量x2
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    //CmdVel = axisNo.ToString().PadLeft(2, "0") + " 10 02 30 00 02 04 "
                    //For i As Integer = 0 To strData.GetUpperBound(0)
                    //    CmdVel += strData(i) & " "
                    //Next

                    //正轉
                    if (direction == 0)
                    {
                        CmdRun = axisNo.ToString().PadLeft(2, '0') + " 06 00 1E 22 01";
                        //反轉
                    }
                    else
                    {
                        CmdRun = axisNo.ToString().PadLeft(2, '0') + " 06 00 1E 24 01";
                    }
                    break;
                case enmAxisType.RK2:
                    //CmdVel = axisNo.ToString().PadLeft(2, "0") + " 10 04 80 00 02 04 "
                    //For i As Integer = 0 To strData.GetUpperBound(0)
                    //    CmdVel += strData(i) & " "
                    //Next

                    //正轉
                    if (direction == 0)
                    {
                        CmdRun = axisNo.ToString().PadLeft(2, '0') + " 06 00 7D 40 00";
                        //反轉
                    }
                    else
                    {
                        CmdRun = axisNo.ToString().PadLeft(2, '0') + " 06 00 7D 80 00";
                    }

                    break;
            }
            //        CmdVel = PadCmdCRC(CmdVel)
            CmdRun = PadCmdCRC(CmdRun);
            //CmdQueue.Enqueue(CmdVel) '存入速度設定命令
            CmdQueue.Enqueue(CmdRun);
            //存入運行命令
            //Debug.Print("CmdQueue:" & CmdVel)
            Debug.Print("CmdQueue:" + CmdRun);
            mAutoWait.Set();
            cmdStatus = CommandStatus.Sending;
            Debug.Print("JogPlus");
            return cmdStatus;

        }
        CommandStatus IMotionCard.VelMove(int axisNo, eDirection direction)
        {
            return JogPlus(axisNo, direction);
        }
        /// <summary>緊急停止</summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus Emgtop(int axisNo)
        {
            //--- 不是可發命令狀態時,傳回前一值 ---
            if (cmdStatus != CommandStatus.Sucessed)
            {
                return cmdStatus;
            }
            //--- 不是可發命令狀態時,傳回前一值 ---
            string Cmd = "";
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    EnqueueBatchCmd(axisNo, "02 02", 0);
                    //立即停止
                    Cmd = axisNo.ToString().PadLeft(2, '0') + " 06 00 1E 30 00";
                    break;
                case enmAxisType.RK2:
                    EnqueueBatchCmd(axisNo, "02 00", 0);
                    //立即停止
                    Cmd = axisNo.ToString().PadLeft(2, '0') + " 06 00 7D 00 20";
                    break;
            }

            Cmd = PadCmdCRC(Cmd);
            CmdQueue.Enqueue(Cmd);
            mAutoWait.Set();
            Debug.Print("EmgStop");
            return cmdStatus;
        }
        CommandStatus IMotionCard.EmgStop(int axisNo)
        {
            return Emgtop(axisNo);
        }

        /// <summary>減速停止</summary>
        /// <param name="axisNo"></param>
        /// <param name="dec"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SlowStop(int axisNo, decimal dec)
        {
            //--- 不是可發命令狀態時,傳回前一值 ---
            if (cmdStatus != CommandStatus.Sucessed)
            {
                return cmdStatus;
            }
            //--- 不是可發命令狀態時,傳回前一值 ---
            string Cmd = "";
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    EnqueueBatchCmd(axisNo, "02 02", 1);
                    //減速停止
                    Cmd = axisNo.ToString().PadLeft(2, '0') + " 06 00 1E 21 00";
                    break;
                case enmAxisType.RK2:
                    EnqueueBatchCmd(axisNo, "02 00", 1);
                    //減速停止
                    Cmd = axisNo.ToString().PadLeft(2, '0') + " 06 00 7D 00 20";
                    break;
            }

            Cmd = PadCmdCRC(Cmd);
            CmdQueue.Enqueue(Cmd);
            mAutoWait.Set();
            Debug.Print("SlowStop");
            return CommandStatus.Sucessed;
        }

        /// <summary>激磁</summary>
        /// <param name="axisNo"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus Servo(int axisNo, enmONOFF state)
        {
            //--- 不是可發命令狀態時,傳回前一值 ---
            if (cmdStatus != CommandStatus.Sucessed)
            {
                return cmdStatus;
            }
            //--- 不是可發命令狀態時,傳回前一值 ---

            string ServoCmd = "";
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    //On
                    if (state == enmONOFF.eON)
                    {
                        ServoCmd = axisNo.ToString().PadLeft(2, '0') + " 06 00 1E 20 00";
                        //Off
                    }
                    else
                    {
                        ServoCmd = axisNo.ToString().PadLeft(2, '0') + " 06 00 1E 00 00";
                    }
                    break;
                case enmAxisType.RK2:
                    //On
                    if (state == enmONOFF.eON)
                    {
                        ServoCmd = axisNo.ToString().PadLeft(2, '0') + " 06 00 7D 00 00";
                        //Off
                    }
                    else
                    {
                        ServoCmd = axisNo.ToString().PadLeft(2, '0') + " 06 00 7D 00 40";
                    }
                    break;
            }

            ServoCmd = PadCmdCRC(ServoCmd);
            CmdQueue.Enqueue(ServoCmd);
            mAutoWait.Set();
            cmdStatus = CommandStatus.Sending;
            //Debug.Print("Servo")
            return cmdStatus;
        }
        /// <summary>相對移動</summary>
        /// <param name="axisNo"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus RelMove(int axisNo, decimal offset)
        {
            //--- 不是可發命令狀態時,傳回前一值 ---
            if (cmdStatus != CommandStatus.Sucessed)
            {
                return cmdStatus;
            }
            //--- 不是可發命令狀態時,傳回前一值 ---
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    string setModeCmd = null;
                    //模式設定
                    string axisCmd = axisNo.ToString().PadLeft(2, '0');
                    setModeCmd = axisCmd + " 06 00 10 00 00 ";
                    //增量式
                    setModeCmd = PadCmdCRC(setModeCmd);
                    CmdQueue.Enqueue(setModeCmd);

                    string SetPosCmd = null;
                    //位置設定命令
                    SetPosCmd = axisCmd + " 10 04 02 00 02 04 ";
                    string[] strData = SplitHexData((int)offset);
                    for (int i = 0; i <= strData.GetUpperBound(0); i++)
                    {
                        SetPosCmd += strData[i] + " ";
                    }

                    SetPosCmd = PadCmdCRC(SetPosCmd);
                    CmdQueue.Enqueue(SetPosCmd);

                    string SelectMCmd = null;
                    //運轉資料選擇M0
                    SelectMCmd = axisCmd + " 06 00 1E 01 00";
                    SelectMCmd = PadCmdCRC(SelectMCmd);
                    CmdQueue.Enqueue(SelectMCmd);
                    string RelMoveCmd = null;
                    RelMoveCmd = axisCmd + " 06 00 1E 01 00";
                    RelMoveCmd = PadCmdCRC(RelMoveCmd);
                    CmdQueue.Enqueue(RelMoveCmd);
                    //移動命令
                    break;
                case enmAxisType.RK2:

                    setModeCmd = null;
                    //模式設定 
                    axisCmd = axisNo.ToString().PadLeft(2, '0');
                    setModeCmd = axisCmd + " 06 05 01 00 00 ";
                    //相對 = 0
                    setModeCmd = PadCmdCRC(setModeCmd);
                    CmdQueue.Enqueue(setModeCmd);

                    SetPosCmd = null;
                    //位置設定命令

                    SetPosCmd = axisCmd + " 10 04 00 00 02 04 ";
                    strData = SplitHexData((int)offset);
                    for (int i = 0; i <= strData.GetUpperBound(0); i++)
                    {
                        SetPosCmd += strData[i] + " ";
                    }

                    SetPosCmd = PadCmdCRC(SetPosCmd);
                    CmdQueue.Enqueue(SetPosCmd);

                    SelectMCmd = null;
                    //運轉資料選擇M0
                    SelectMCmd = axisCmd + " 06 00 7D 00 00";
                    SelectMCmd = PadCmdCRC(SelectMCmd);
                    CmdQueue.Enqueue(SelectMCmd);
                    RelMoveCmd = null;
                    RelMoveCmd = axisCmd + " 06 00 7D 00 08";
                    RelMoveCmd = PadCmdCRC(RelMoveCmd);
                    CmdQueue.Enqueue(RelMoveCmd);
                    //移動命令
                    break;
            }
            Debug.Print("RelMove");
            mAutoWait.Set();
            cmdStatus = CommandStatus.Sending;
            return cmdStatus;
        }
        /// <summary>絕對移動</summary>
        /// <param name="axisNo">站號</param>
        /// <param name="absPos">目標絕對位置</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus AbsMove(int axisNo, decimal absPos)
        {
            //--- 不是可發命令狀態時,傳回前一值 ---
            if (cmdStatus != CommandStatus.Sucessed)
            {
                return cmdStatus;
            }
            Int32 targetPosInPulse = default(Int32);
            targetPosInPulse = Convert.ToInt32(absPos);
            //--- 不是可發命令狀態時,傳回前一值 ---
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    string setModeCmd = null;
                    //模式設定
                    string axisCmd = axisNo.ToString().PadLeft(2, '0');
                    setModeCmd = axisCmd + " 06 00 10 00 01 ";
                    //絕對式
                    setModeCmd = PadCmdCRC(setModeCmd);
                    CmdQueue.Enqueue(setModeCmd);

                    string SetPosCmd = null;
                    //位置設定命令
                    SetPosCmd = axisCmd + " 10 04 02 00 02 04 ";
                    string[] strData = SplitHexData(targetPosInPulse);
                    for (int i = 0; i <= strData.GetUpperBound(0); i++)
                    {
                        SetPosCmd += strData[i] + " ";
                    }

                    SetPosCmd = PadCmdCRC(SetPosCmd);
                    CmdQueue.Enqueue(SetPosCmd);

                    string SelectMCmd = null;
                    //運轉資料選擇M0
                    SelectMCmd = axisCmd + " 06 00 1E 21 01";
                    SelectMCmd = PadCmdCRC(SelectMCmd);
                    CmdQueue.Enqueue(SelectMCmd);
                    string RelMoveCmd = null;
                    RelMoveCmd = axisCmd + " 06 00 1E 20 01";
                    RelMoveCmd = PadCmdCRC(RelMoveCmd);
                    CmdQueue.Enqueue(RelMoveCmd);
                    //移動命令
                    break;
                case enmAxisType.RK2:
                    setModeCmd = null;
                    //模式設定
                    axisCmd = axisNo.ToString().PadLeft(2, '0');
                    setModeCmd = axisCmd + " 06 05 01 00 01 ";
                    //絕對 =1 
                    setModeCmd = PadCmdCRC(setModeCmd);
                    CmdQueue.Enqueue(setModeCmd);

                    SetPosCmd = null;
                    //位置設定命令

                    SetPosCmd = axisCmd + " 10 04 00 00 02 04 ";
                    strData = SplitHexData(targetPosInPulse);
                    for (int i = 0; i <= strData.GetUpperBound(0); i++)
                    {
                        SetPosCmd += strData[i] + " ";
                    }

                    SetPosCmd = PadCmdCRC(SetPosCmd);
                    CmdQueue.Enqueue(SetPosCmd);

                    SelectMCmd = null;
                    //運轉資料選擇M0
                    SelectMCmd = axisCmd + " 06 00 7D 00 00";
                    SelectMCmd = PadCmdCRC(SelectMCmd);
                    CmdQueue.Enqueue(SelectMCmd);
                    RelMoveCmd = null;
                    RelMoveCmd = axisCmd + " 06 00 7D 00 08";
                    RelMoveCmd = PadCmdCRC(RelMoveCmd);
                    CmdQueue.Enqueue(RelMoveCmd);
                    //移動命令
                    break;
            }
            Debug.Print("AbsMove");
            mAutoWait.Set();
            cmdStatus = CommandStatus.Sending;
            return CommandStatus.Sucessed;
        }

        /// <summary>開啟通訊埠</summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus Initial(SMotionConnectParameter item)
        {
            Subject = item.MODBUS.Port;
            if (Subject == null)
            {
                Subject = new SerialPort();
                Subject.PortName = item.MODBUS.PortName;
                Subject.BaudRate = 19200;
                Subject.Parity = Parity.Even;
                Subject.DataBits = 8;
                Subject.StopBits = StopBits.One;
            }

            if (Subject.IsOpen == false)
            {
                try
                {
                    Subject.Open();
                    Subject.ReadExisting();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "MODBUS Initial", System.Windows.Forms.MessageBoxButtons.OK);
                }

                //Dim curProc As Process = Process.GetCurrentProcess
                //curProc.ProcessorAffinity = CType("0x02", IntPtr)
                //If thread.ThreadState <> Threading.ThreadState.WaitSleepJoin Then
                //    thread.Start()
                //End If
                //Eason 20170221 Ticket:100033 , Memory Free Part4 [S]
                //Threading.ThreadPool.QueueUserWorkItem(New Threading.WaitCallback(AddressOf ModBus_Sender))
                Task.Run(() => { ModBus_Sender(); });
                //Eason 20170221 Ticket:100033 , Memory Free Part4 [E]
                mStopWatch.Restart();
                return CommandStatus.Sucessed;
            }
            return CommandStatus.Alarm;
        }

        /// <summary>關閉通訊埠</summary>
        /// <remarks></remarks>
        public CommandStatus Close()
        {
            Subject.Close();
            return CommandStatus.Sucessed;
        }

        public CommandStatus GetAlarmCode(int axisNo, ref string alarmCode)
        {
            //Select Case cmdStatus
            //    Case CommandStatus.Alarm
            //End Select
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    string axisCmd = axisNo.ToString().PadLeft(2, '0');
                    string Cmd = axisCmd + " 03 01 00 00 02";
                    Cmd = PadCmdCRC(Cmd);
                    CmdQueue.Enqueue(Cmd);
                    break;
                case enmAxisType.RK2:
                    axisCmd = axisNo.ToString().PadLeft(2, '0');
                    Cmd = axisCmd + " 03 00 80 00 02";
                    Cmd = PadCmdCRC(Cmd);
                    CmdQueue.Enqueue(Cmd);
                    break;
            }
            Debug.Print("GetAlarmCode");
            mAutoWait.Set();
            return cmdStatus;
        }

        public CommandStatus Home(int axisNo, uint homeMode, uint homeDirection)
        {
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    string axisCmd = axisNo.ToString().PadLeft(2, '0');
                    string HomePCmd = null;
                    HomePCmd = axisNo.ToString().PadLeft(2, '0') + " 06 00 20 08 00";
                    //Home-P原點復歸完成輸出
                    HomePCmd = PadCmdCRC(HomePCmd);
                    CmdQueue.Enqueue(CmdByte.ToString());

                    HomePCmd = axisNo.ToString().PadLeft(2, '0') + " 06 02 37 00 01";
                    //3檢知器方式復歸
                    HomePCmd = PadCmdCRC(HomePCmd);
                    CmdQueue.Enqueue(CmdByte.ToString());

                    //負
                    if (homeDirection == 1)
                    {
                        HomePCmd = axisNo.ToString().PadLeft(2, '0') + " 06 02 42 00 00";
                        //負方向開始
                        HomePCmd = PadCmdCRC(HomePCmd);
                        CmdQueue.Enqueue(CmdByte.ToString());
                        //正
                    }
                    else
                    {
                        HomePCmd = axisNo.ToString().PadLeft(2, '0') + " 06 02 42 00 01";
                        //正方向開始
                        HomePCmd = PadCmdCRC(HomePCmd);
                        CmdQueue.Enqueue(CmdByte.ToString());
                    }
                    string HomeCmd = null;
                    HomeCmd = axisNo.ToString().PadLeft(2, '0') + " 06 00 7D 00 10";
                    HomeCmd = PadCmdCRC(HomeCmd);
                    CmdQueue.Enqueue(CmdByte.ToString());
                    break;
                case enmAxisType.RK2:
                    axisCmd = axisNo.ToString().PadLeft(2, '0');
                    HomePCmd = null;
                    HomePCmd = axisNo.ToString().PadLeft(2, '0') + " 06 10 0D 00 01";
                    //Home-P原點復歸完成輸出
                    HomePCmd = PadCmdCRC(HomePCmd);
                    CmdQueue.Enqueue(CmdByte.ToString());

                    HomePCmd = axisNo.ToString().PadLeft(2, '0') + " 06 02 C1 00 01";
                    //3檢知器方式復歸
                    HomePCmd = PadCmdCRC(HomePCmd);
                    CmdQueue.Enqueue(CmdByte.ToString());

                    //負
                    if (homeDirection == 1)
                    {
                        HomePCmd = axisNo.ToString().PadLeft(2, '0') + " 06 02 A1 00 00";
                        //負方向開始
                        HomePCmd = PadCmdCRC(HomePCmd);
                        CmdQueue.Enqueue(CmdByte.ToString());
                        //正
                    }
                    else
                    {
                        HomePCmd = axisNo.ToString().PadLeft(2, '0') + " 06 02 A1 00 01";
                        //正方向開始
                        HomePCmd = PadCmdCRC(HomePCmd);
                        CmdQueue.Enqueue(CmdByte.ToString());
                    }
                    HomeCmd = null;
                    HomeCmd = axisNo.ToString().PadLeft(2, '0') + " 06 00 7D 00 10";
                    HomeCmd = PadCmdCRC(HomeCmd);
                    CmdQueue.Enqueue(CmdByte.ToString());
                    break;
            }
            Debug.Print("Home");
            mAutoWait.Set();
            cmdStatus = CommandStatus.Sending;
            return cmdStatus;
        }

        /// <summary>確認馬達停止</summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus MoveFinish(int axisNo)
        {
            string axisCmd = axisNo.ToString().PadLeft(2, '0');
            //Debug.Print("MoveFinish")
            IOStatus mIOStatus = default(IOStatus);
            if (IOStatus[axisNo].READY)
            {
                CheckMotorStatus(axisNo, ref mIOStatus);
                return CommandStatus.Sucessed;
            }
            else
            {
                CheckMotorStatus(axisNo, ref mIOStatus);
                return CommandStatus.Alarm;
            }
            //MoveFinish = IOStatus(axisNo).READY '取得NET-OUT5 READY Bit5
            //Return GetAxisState(axisNo)

        }
        /// <summary>確認馬達停止</summary>
        /// <param name="axisNo"></param>
        /// <param name="TimeOut"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus WaitTableStop(int axisNo, decimal TimeOut = 1000.0M)
        {
            bool functionReturnValue = false;

            string axisCmd = axisNo.ToString().PadLeft(2, '0');

            do
            {
                functionReturnValue = IOStatus[axisNo].READY;
                //取得NET-OUT5 READY Bit5
                //--- 不是可發命令狀態時,傳回前一值 ---
                if (cmdStatus != CommandStatus.Sucessed)
                {
                    //Application.DoEvents();
                }
                else
                {
                    string cmd = null;
                    switch (mAxisType[axisNo])
                    {
                        case enmAxisType.HM60163C:
                            cmd = axisCmd + " 03 00 20 00 02";
                            cmd = PadCmdCRC(cmd);
                            CmdQueue.Enqueue(cmd);
                            //壓入詢問命令
                            break;
                        case enmAxisType.RK2:
                            cmd = axisCmd + " 03 00 7F 00 02";
                            cmd = PadCmdCRC(cmd);
                            CmdQueue.Enqueue(cmd);
                            //壓入詢問命令
                            break;
                    }
                    Debug.Print("WaitTableStop");
                    mAutoWait.Set();
                    cmdStatus = CommandStatus.Sending;
                }
                //--- 不是可發命令狀態時,傳回前一值 ---

            } while (!(functionReturnValue));
            return cmdStatus;

        }

        /// <summary>取得現在位置</summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        /// <remarks>因最快傳回要鎖住23ms,所以改為傳回前一次資料.</remarks>
        public string GetPositionValue(int axisNo)
        {
            string axisCmd = axisNo.ToString().PadLeft(2, '0');
            decimal pos = default(decimal);
            //Dim Cmd As String = axisCmd & " 03 01 00 00 02"
            if (axisNo > feedbackPos.GetUpperBound(0))
            {
                return "NA";
            }
            //pos = feedbackPos(axisNo) '丟出前一次傳回值
            pos = feedbackPos[axisNo];
            //--- 不是可發命令狀態時,傳回前一值 ---
            if (cmdStatus != CommandStatus.Sucessed)
            {
                return pos.ToString();
            }
            //--- 不是可發命令狀態時,傳回前一值 ---
            string Cmd = null;
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    if (mAxisMotor[axisNo] == eMotorType.ServoMotor)
                    {
                        Cmd = axisCmd + " 03 01 1E 00 02";
                    }
                    else
                    {
                        Cmd = axisCmd + " 03 01 18 00 02";
                    }
                    Cmd = PadCmdCRC(Cmd);
                    CmdQueue.Enqueue(Cmd);
                    //壓入詢問命令
                    break;
                case enmAxisType.RK2:
                    if (mAxisMotor[axisNo] == eMotorType.ServoMotor)
                    {
                        Cmd = axisCmd + " 03 00 CC 00 02";
                    }
                    else
                    {
                        Cmd = axisCmd + " 03 00 C6 00 02";
                    }
                    Cmd = PadCmdCRC(Cmd);
                    CmdQueue.Enqueue(Cmd);
                    //壓入詢問命令
                    break;
            }
            Debug.Print("GetPositionValue");
            mAutoWait.Set();
            cmdStatus = CommandStatus.Sending;
            return pos.ToString();
        }

        /// <summary>取得機器狀態</summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GetMachineStatus(int axisNo)
        {
            string axisCmd = axisNo.ToString().PadLeft(2, '0');
            //--- 不是可發命令狀態時,傳回前一值 ---
            if (cmdStatus != CommandStatus.Sucessed)
            {
                return cmdStatus;
            }
            //--- 不是可發命令狀態時,傳回前一值 ---
            string Cmd = null;
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    Cmd = axisCmd + " 03 00 20 00 02";
                    //取得直接IO、電磁剎車狀態 'Revised HM
                    Cmd = PadCmdCRC(Cmd);
                    CmdQueue.Enqueue(Cmd);
                    //壓入詢問命令
                    break;
                case enmAxisType.RK2:
                    Cmd = axisCmd + "03 00 D4 00 02";
                    //取得直接IO、電磁剎車狀態
                    Cmd = PadCmdCRC(Cmd);
                    CmdQueue.Enqueue(Cmd);
                    //壓入詢問命令
                    break;
            }
            Debug.Print("GetMachineStatus");
            mAutoWait.Set();
            cmdStatus = CommandStatus.Sending;
            return cmdStatus;
        }

        /// <summary>啟動速度設定</summary>
        /// <param name="axisNo"></param>
        /// <param name="velHigh"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetVelLow(int axisNo, decimal velHigh)
        {
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    return CommandStatus.Sucessed;
                case enmAxisType.RK2:
                    Debug.Print("SetVelLow");
                    return EasyBatchCmd(axisNo, "02 84", velHigh);
                default:
                    return CommandStatus.Alarm;
            }
        }

        /// <summary>加速度設定</summary>
        /// <param name="axisNo"></param>
        /// <param name="acc"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetAcc(int axisNo, decimal acc)
        {
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    Debug.Print("SetAcc");
                    //Revised HM
                    return EasyBatchCmd(axisNo, "00 18", acc);
                case enmAxisType.RK2:
                    Debug.Print("SetAcc");
                    return EasyBatchCmd(axisNo, "02 80", acc);
                default:
                    return CommandStatus.Alarm;
            }
        }

        /// <summary>單軸運動 原點復歸加速度</summary>
        /// <param name="axisNo"></param>
        /// <param name="acc"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetHomeAcc(int axisNo, decimal acc)
        {
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    Debug.Print("SetHomeAcc");
                    return EasyBatchCmd(axisNo, "02 3C", acc);
                case enmAxisType.RK2:
                    Debug.Print("SetHomeAcc");
                    return EasyBatchCmd(axisNo, "02 C4", acc);
                default:
                    return CommandStatus.Alarm;
            }
        }
        /// <summary>單軸運動 減速度</summary>
        /// <param name="axisNo"></param>
        /// <param name="dec"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetDec(int axisNo, decimal dec)
        {
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    Debug.Print("SetDec");
                    //Revised HM
                    return EasyBatchCmd(axisNo, "00 16", dec);
                case enmAxisType.RK2:
                    Debug.Print("SetDec");
                    return EasyBatchCmd(axisNo, "02 82", dec);
                default:
                    return CommandStatus.Alarm;
            }
        }

        public CommandStatus SetHomeDec(int axisNo, decimal dec)
        {
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    Debug.Print("SetHomeDec");
                    return EasyBatchCmd(axisNo, "02 3C", dec);
                case enmAxisType.RK2:
                    Debug.Print("SetHomeDec");
                    return EasyBatchCmd(axisNo, "02 C4", dec);
                default:
                    return CommandStatus.Alarm;
            }
        }

        public CommandStatus SetHomeVelHigh(int axisNo, decimal VelHigh)
        {
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    //Debug.Print("SetHomeVelHigh")
                    //Revised HM
                    return EasyBatchCmd(axisNo, "02 3A", VelHigh);
                case enmAxisType.RK2:
                    //Debug.Print("SetHomeVelHigh")
                    return EasyBatchCmd(axisNo, "02 C2", VelHigh);
                default:
                    return CommandStatus.Alarm;
            }
        }

        public CommandStatus SetHomeVelLow(int axisNo, decimal velLow)
        {
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    return CommandStatus.Sucessed;
                case enmAxisType.RK2:
                    //Debug.Print("SetHomeVelLow")
                    return EasyBatchCmd(axisNo, "02 C6", velLow);
                default:
                    return CommandStatus.Alarm;
            }
        }

        public CommandStatus SetVelHigh(int axisNo, decimal velHigh)
        {
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    //Debug.Print("SetVelHigh(HM60163C)" & axisNo)
                    //Revised HM
                    return EasyBatchCmd(axisNo, "00 1A", velHigh);
                case enmAxisType.RK2:
                    //Debug.Print("SetVelHigh(RK2)" & axisNo)
                    return EasyBatchCmd(axisNo, "04 80", velHigh);
                default:
                    return CommandStatus.Alarm;
            }
        }

        public CommandStatus SetORG(int axisNo, enmOrgLogic logic)
        {
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    return CommandStatus.Sucessed;
                case enmAxisType.RK2:
                    if (logic == enmOrgLogic.HighActive)
                    {
                        Debug.Print("SetORG");
                        return EasyBatchCmd(axisNo, "02 1A", 1);
                    }
                    else
                    {
                        Debug.Print("SetORG");
                        return EasyBatchCmd(axisNo, "02 1A", 0);
                    }

                default:
                    return CommandStatus.Alarm;
            }
        }

        public CommandStatus AxisResetError(int axisNo)
        {
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    Debug.Print("AxisResetError");
                    EnqueueBatchCmd(axisNo, "00 40", 1);
                    //Revised HM
                    return EasyBatchCmd(axisNo, "00 40", 0);
                case enmAxisType.RK2:
                    Debug.Print("AxisResetError");
                    return EasyBatchCmd(axisNo, "01 80", 1);
                default:
                    return CommandStatus.Alarm;
            }
        }

        /// <summary>取得運動接點狀態</summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus CheckMotorStatus(int axisNo, ref IOStatus IOStatus)
        {
            string axisCmd = axisNo.ToString().PadLeft(2, '0');
            //--- 不是可發命令狀態時,傳回前一值 ---
            if (cmdStatus != CommandStatus.Sucessed)
            {
                return cmdStatus;
            }
            //--- 不是可發命令狀態時,傳回前一值 ---
            string Cmd = null;
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    Cmd = axisCmd + " 03 01 33 00 04";
                    //取得直接IO、電磁剎車狀態 'Revised HM
                    Cmd = PadCmdCRC(Cmd);
                    CmdQueue.Enqueue(Cmd);
                    //壓入詢問命令
                    break;
                case enmAxisType.RK2:
                    Cmd = axisCmd + " 03 00 7F 00 02";
                    //取得驅動器輸出
                    Cmd = PadCmdCRC(Cmd);
                    CmdQueue.Enqueue(Cmd);
                    //壓入詢問命令
                    break;

            }
            //Debug.Print("CheckMotorStatus")
            mAutoWait.Set();
            cmdStatus = CommandStatus.Sending;
            return cmdStatus;

        }
        #endregion

        /// <summary>取得軸狀態</summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GetAxisState(int axisNo, ref uint status)
        {
            Debug.Print("GetAxisState is NOT Supported");
            return CommandStatus.Sucessed;
        }
          //20171013 新增get 軸狀態
          // <summary>取得軸運動狀態</summary>
          // <param name="axisNo"></param>
          // <returns></returns>
          //<remarks></remarks>
        public CommandStatus GetMotionState(int axisNo, ref int status) 
        {
            Debug.Print("GetMotionState is NOT Supported");
            return CommandStatus.Sucessed;
        }

        public CommandStatus AddMovePath(ref CSyncParameter SyncParameter, bool IsEndPath = false)
        {
            Debug.Print("AddMovePath is NOT Supported");
            return CommandStatus.Sucessed;
        }
        CommandStatus IMotionCard.GpAddDotPath(ref CSyncParameter SyncParameter, bool IsEndPath = false)
        {
            return AddMovePath(ref SyncParameter, IsEndPath);
        }

        public CommandStatus ClearMovePath(ref CSyncParameter SyncParameter)
        {
            Debug.Print("ClearMovePath is NOT Supported");
            return CommandStatus.Sucessed;
        }
        CommandStatus IMotionCard.GpClearMovePath(ref CSyncParameter SyncParameter)
        {
            return ClearMovePath(ref SyncParameter);
        }

        public CommandStatus GetCompareValue(int axisNo, ref decimal Pos)
        {
            Debug.Print("GetCompareValue is NOT Supported");
            return CommandStatus.Sucessed;
        }

        public CommandStatus GetCommandValue(int axisNo, ref decimal pos)
        {
            string axisCmd = axisNo.ToString().PadLeft(2, '0');
            pos = feedbackPos[axisNo];
            //丟出前一次傳回值

            //--- 不是可發命令狀態時,傳回前一值 ---
            if (cmdStatus != CommandStatus.Sucessed)
            {
                return cmdStatus;
            }
            //--- 不是可發命令狀態時,傳回前一值 ---

            string Cmd = null;
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    Cmd = axisCmd + " 03 01 18 00 02";
                    Cmd = PadCmdCRC(Cmd);
                    CmdQueue.Enqueue(Cmd);
                    //壓入詢問命令
                    break;
                case enmAxisType.RK2:
                    Cmd = axisCmd + " 03 00 C6 00 02";
                    Cmd = PadCmdCRC(Cmd);
                    CmdQueue.Enqueue(Cmd);
                    //壓入詢問命令
                    break;
            }

            mAutoWait.Set();
            cmdStatus = CommandStatus.Sending;
            return cmdStatus;
        }


        public CommandStatus GetCurrentVel(int axisNo, ref decimal velocity)
        {
            string axisCmd = axisNo.ToString().PadLeft(2, '0');

            //Dim Cmd As String = axisCmd & " 03 01 00 00 02"
            velocity = feedbackPos[axisNo];
            //丟出前一次傳回值

            //--- 不是可發命令狀態時,傳回前一值 ---
            if (cmdStatus != CommandStatus.Sucessed)
            {
                return cmdStatus;
            }
            //--- 不是可發命令狀態時,傳回前一值 ---

            string Cmd = null;
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    return CommandStatus.Sucessed;
                case enmAxisType.RK2:
                    if (mAxisMotor[axisNo] == eMotorType.ServoMotor)
                    {
                        Cmd = axisCmd + " 03 00 CC 00 02";
                    }
                    else
                    {
                        Cmd = axisCmd + " 03 00 C6 00 02";
                    }

                    Cmd = PadCmdCRC(Cmd);
                    CmdQueue.Enqueue(Cmd);
                    //壓入詢問命令
                    break;
            }

            mAutoWait.Set();
            cmdStatus = CommandStatus.Sending;
            return cmdStatus;
        }

        public string GetErrorMessage(int axisNo)
        {
            Debug.Print("GetErrorMessage is NOT Supported.");
            return "GetErrorMessage is NOT Supported.";
        }

        public CommandStatus GetLatchFlag(int axis, ref bool latch)
        {
            Debug.Print("GetLatchFlag is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus GetLatchPosition(int axis, enmPositionType type, ref decimal pos)
        {
            Debug.Print("GetLatchPosition is NOT Supported.");
            return CommandStatus.Sucessed;
        }


        public CommandStatus GroupAddAxes(ref CSyncParameter SyncParameter, List<int> AxisNoList)
        {
            Debug.Print("GroupAddAxes is NOT Supported.");
            return CommandStatus.Sucessed;
        }
        CommandStatus IMotionCard.GpAddAxis(ref CSyncParameter SyncParameter, List<int> AxisNoList)
        {
            return GroupAddAxes(ref SyncParameter, AxisNoList);
        }


        /// <summary>該軸原點復歸完成</summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus HomeFinish(int axisNo)
        {
            bool functionReturnValue = false;
            string axisCmd = axisNo.ToString().PadLeft(2, '0');
            functionReturnValue = IOStatus[axisNo].HOME_P;
            //取得NET-OUT4 HOME-P Bit4at
            IOStatus mIOStatus = default(IOStatus);
            CheckMotorStatus(axisNo, ref mIOStatus);
            if (functionReturnValue)
            {
                return CommandStatus.Sucessed;
            }
            else
            {
                return CommandStatus.Alarm;
            }


        }

        /// <summary>該軸 IO設定值套用</summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus IOSet(int axisNo)
        {
            Debug.Print("IOSet is NOT Supported.");
            return CommandStatus.Sucessed;
        }


        //Public Property m_GpHand As IntPtr Implements IMotionCard.m_GpHand
        /// <summary>該軸移動到位</summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus MoveFinish(ref CSyncParameter SyncParameter)
        {
            Debug.Print("MoveFinish is NOT Supported.");
            return CommandStatus.Sucessed;
        }
        CommandStatus IMotionCard.GpMoveDone(ref CSyncParameter SyncParameter)
        {
            return MoveFinish(ref SyncParameter);
        }

        public CommandStatus Moving(ref CSyncParameter SyncParameter)
        {
            Debug.Print("Moving is NOT Supported.");
            return cmdStatus;
        }
        CommandStatus IMotionCard.GpMovePath(ref CSyncParameter SyncParameter)
        {
            return Moving(ref SyncParameter);
        }

        public CommandStatus PauseMovePath(ref CSyncParameter SyncParameter)
        {
            Debug.Print("PauseMovePath is NOT Supported.");
            return CommandStatus.Sucessed;
        }
        CommandStatus IMotionCard.GpPauseMovePath(ref CSyncParameter SyncParameter)
        {
            return PauseMovePath(ref SyncParameter);
        }

        public CommandStatus ResetLatch(int axis)
        {
            Debug.Print("ResetLatch is NOT Supported.");
            return CommandStatus.Sucessed;
        }


        /// <summary>連續動作群組 加速度設定</summary>
        /// <param name="Acc"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetAcc(ref CSyncParameter SyncParameter, decimal Acc)
        {
            Debug.Print("SetAcc is NOT Supported.");
            return CommandStatus.Sucessed;
        }
        CommandStatus IMotionCard.GpSetAcc(ref CSyncParameter SyncParameter, decimal Acc)
        {
            return SetAcc(ref SyncParameter, Acc);
        }

        public CommandStatus SetALM(int axis, enmAlarmEnable enable, enmAlarmLogic logic, enmAlarmStopMode stopMode)
        {
            Debug.Print("SetALM is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetBacklash(int axisNo, enmBacklashEnable Enable)
        {
            Debug.Print("SetBacklash is NOT Supported.");
            return CommandStatus.Sucessed;
        }


        public CommandStatus SetBlengdingTime(ref CSyncParameter SyncParameter, int Time)
        {
            Debug.Print("SetBlengdingTime is NOT Supported.");
            return CommandStatus.Sucessed;
        }
        CommandStatus IMotionCard.GpSetBlendingTime(ref CSyncParameter SyncParameter, int Time)
        {
            return SetBlengdingTime(ref SyncParameter, Time);
        }

        public CommandStatus SetCampareData(int axisNo, decimal startPos, decimal endPos, int interval)
        {
            Debug.Print("SetCampareData is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetCompare(int axisNo, enmCompareEnable Enable, enmCompareSource Type, enmCompareMethod Method)
        {
            Debug.Print("SetCompare is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus GroupSetDec(ref CSyncParameter SyncParameter, decimal Dec)
        {
            Debug.Print("SetDec is NOT Supported.");
            return CommandStatus.Sucessed;
        }
        CommandStatus IMotionCard.GpSetDec(ref CSyncParameter SyncParameter, decimal Dec)
        {
            return GroupSetDec(ref SyncParameter, Dec);
        }

        public CommandStatus SetEMG(int axis, enmEmgLogic logic)
        {
            Debug.Print("SetEMG is NOT Supported");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetEMG(enmEmgLogic Logic)
        {
            Debug.Print("SetEMG is NOT Supported");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetERC(int axis, enmErcEnableMode enableMode, enmErcLogic logic)
        {
            Debug.Print("SetERC is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetExternalDrive(int axisNo, enmExternalDrive Drive, enmExternalDriveEnable Enable, enmExternalDrivePulseInMode Mode)
        {
            Debug.Print("SetExternalDrive is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetEZ(int axisNo, enmEZLogic logic)
        {
            Debug.Print("SetEZ is NOT Supported to Change logic");
            switch (mAxisType[axisNo])
            {
                case enmAxisType.HM60163C:
                    return CommandStatus.Sucessed;
                case enmAxisType.RK2:
                    //原點復歸TIM信號檢知 使用ZSG
                    return EasyBatchCmd(axisNo, "07 18", 2);
                default:
                    return CommandStatus.Alarm;
            }
        }

        public CommandStatus SetGantry(int masterAxis, int slaveAxis)
        {
            Debug.Print("SetGantry is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetHEL(int axis, enmLimitEnable enable, enmLimitLogic logic, enmLimitStopMode stopMode)
        {
            Debug.Print("SetHEL is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetHomeCrossDistance(int axis, decimal homeCrossDistance)
        {
            Debug.Print("SetHomeCrossDistance is NOT Supported.");
            return CommandStatus.Sucessed;
        }


        public CommandStatus SetHomeExSwitchMode(int axis, enmHomeExSwitchMode homeExSwitchMode)
        {
            Debug.Print("SetHomeExSwitchMode is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetHomeReset(int axis, enmHomeReset enable)
        {
            Debug.Print("SetHomeReset is NOT Supported.");
            return CommandStatus.Sucessed;
        }


        public CommandStatus SETIN1Stop(int axis, enmTriggerStopEnable enable, enmTriggerStopMode mode, enmTriggerStopLogic logic)
        {
            Debug.Print("SETIN1Stop is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SETIN2Stop(int axis, enmTriggerStopEnable enable, enmTriggerStopMode mode, enmTriggerStopLogic logic)
        {
            Debug.Print("SETIN2Stop is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SETIN4Stop(int axis, enmTriggerStopEnable enable, enmTriggerStopMode mode, enmTriggerStopLogic logic)
        {
            Debug.Print("SETIN4Stop is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SETIN5Stop(int axis, enmTriggerStopEnable enable, enmTriggerStopMode mode, enmTriggerStopLogic logic)
        {
            Debug.Print("SETIN5Stop is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetINP(int axis, enmINPEnable enable, enmINPLogic logic)
        {
            Debug.Print("SetINP is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetLatch(int axis, enmLatchEnable enable, enmLatchPLogic logic)
        {
            Debug.Print("SetLatch is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetMaxPulseFrequency(int axis, enmEncodePulseInFrequency frequency)
        {
            Debug.Print("SetMaxPulseFrequency is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetPosition(int axis, decimal pos)
        {
            Debug.Print("SetPosition is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetPPU(int axis, decimal ppu)
        {
            Debug.Print("SetPPU is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetPulseIn(int axis, enmPulseInMode mode, enmPulseInLogic logic)
        {
            Debug.Print("SetPulseIn is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetPulseOutMode(int axisNo, enmPulseOutMode Mode)
        {
            Debug.Print("SetPulseOutMode is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetScale(int axis, decimal scale)
        {
            Debug.Print("SetScale is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetSpeedForward(ref CSyncParameter SyncParameter, enmSFEnable Enable)
        {
            Debug.Print("SetSpeedForward is NOT Supported.");
            return CommandStatus.Sucessed;
        }
        CommandStatus IMotionCard.GpSetSpeedForward(ref CSyncParameter SyncParameter, enmSFEnable Enable)
        {
            return SetSpeedForward(ref SyncParameter, Enable);
        }
        /// <summary>連續動作群組 最大速度</summary>
        /// <param name="VelHigh"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GroupSetVelHigh(ref CSyncParameter SyncParameter, decimal VelHigh)
        {
            Debug.Print("SetVelHigh is NOT Supported");
            return CommandStatus.Sucessed;
        }
        CommandStatus IMotionCard.GpSetVelHigh(ref CSyncParameter SyncParameter, decimal VelHigh)
        {
            return GroupSetVelHigh(ref SyncParameter, VelHigh);
        }
        /// <summary>連續動作群組 起始速度</summary>
        /// <param name="VelLow"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus GroupSetVelLow(ref CSyncParameter SyncParameter, decimal VelLow)
        {
            Debug.Print("SetVelLow is NOT Supported");
            return CommandStatus.Sucessed;
        }
        CommandStatus IMotionCard.GpSetVelLow(ref CSyncParameter SyncParameter, decimal VelLow)
        {
            return GroupSetVelLow(ref SyncParameter, VelLow);
        }

        public CommandStatus GpSetCurve(ref CSyncParameter SyncParameter, eCurveMode curveMode)
        {
            Debug.Print("SetVelLow is NOT Supported");
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

            Debug.Print("SetSwMelEnable is NOT Supported");
            return CommandStatus.Sucessed;
        }
        public CommandStatus GpSetSwPelEnable(int axisNo, bool SwPelEnable)
        {

            Debug.Print("SetSwPelEnable is NOT Supported");
            return CommandStatus.Sucessed;
        }
        /// <summary>
        /// 設定加熱溫度
        /// </summary>
        /// <param name="axisNo">通訊機號</param>
        /// <param name="valve">溫度值</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public CommandStatus SetHeaterSV(int axisNo, int valve)
        {
            string axisCmd = axisNo.ToString().PadLeft(2, '0');
            //--- 不是可發命令狀態時,傳回前一值 ---
            if (cmdStatus != CommandStatus.Sucessed)
            {
                return cmdStatus;
            }
            //--- 不是可發命令狀態時,傳回前一值 ---
            string Cmd = null;

            //Write 
            string stemp = System.Convert.ToString(valve, 16).PadLeft(4, '0');
            //10進制轉16進制後存入字串
            stemp = stemp.Insert(2, " ").ToUpper();
            Cmd = axisCmd + " 06 00 00 " + stemp;
            Cmd = PadCmdCRC(Cmd);
            CmdQueue.Enqueue(Cmd);
            //壓入詢問命令

            mAutoWait.Set();
            cmdStatus = CommandStatus.Sending;
            return cmdStatus;

        }

        /// <summary>
        /// 讀取加熱器溫度
        /// </summary>
        /// <param name="axisNo">通訊機號</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string ReadHeaterPV(int axisNo)
        {
            string axisCmd = axisNo.ToString().PadLeft(2, '0');
            decimal pos = default(decimal);

            pos = feedbackPos[axisNo];
            //丟出前一次傳回值

            //--- 不是可發命令狀態時,傳回前一值 ---
            if (cmdStatus != CommandStatus.Sucessed)
            {
                return pos.ToString();
            }
            //--- 不是可發命令狀態時,傳回前一值 ---

            string Cmd = null;

            Cmd = axisCmd + " 03 00 8A 00 01";
            Cmd = PadCmdCRC(Cmd);
            CmdQueue.Enqueue(Cmd);
            //壓入詢問命令

            mAutoWait.Set();
            cmdStatus = CommandStatus.Sending;
            return pos.ToString();
        }


        public CommandStatus DOOutput(int AxisNo, ushort DOChannel, enmCardIOONOFF OnOFF = enmCardIOONOFF.eOFF)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus GpAddArcPath(ref CSyncParameter synParam, bool IsEndPath = false)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus GpAddDwell(ref CSyncParameter SyncParameter)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetCurve(int axisNo, eCurveMode curveMode)
        {
            Debug.Print("SetCurve is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetHomeOffset(int axisNo, decimal homeOffset)
        {
            Debug.Print("SetHomeOffset is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public object GpSetRefPlane(ref CSyncParameter SyncParameter, ePlane plane)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus GPSetRunMode(ref CSyncParameter syncParameter, eRunMode runMode, int blendingTime = 1024)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetPulseOutReverse(int axisNo, enmPulseOutReverse pulseOutReverse)
        {
            return CommandStatus.Sucessed;
        }

        public CommandStatus GpGetPathStatus(ref CSyncParameter SyncParameter, ref long remainCount)
        {
            Debug.Print("GpGetPathStatus is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetMaxAcc(int axis, decimal maxAcc)
        {
            Debug.Print("SetMaxAcc is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetMaxDec(int axis, decimal maxDec)
        {
            Debug.Print("SetMaxDec is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus SetMaxVel(int axis, decimal maxVel)
        {
            Debug.Print("SetMaxVel is NOT Supported.");
            return CommandStatus.Sucessed;
        }

        public CommandStatus GpResetPath(ref CSyncParameter SyncParameter)
        {
            Debug.Print("GpResetPath is NOT Supported.");
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
        #region "IDisposable Support"
        // 偵測多餘的呼叫
        private bool disposedValue;

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    mAutoWait.Dispose();
                    ModBusTimer.Dispose();
                }

            }
            this.disposedValue = true;
        }

        // 由 Visual Basic 新增此程式碼以正確實作可處置的模式。
        public void Dispose()
        {
            // 請勿變更此程式碼。在以上的 Dispose 置入清除程式碼 (視為布林值處置)。
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion




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
