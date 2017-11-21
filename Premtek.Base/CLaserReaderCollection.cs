using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using ProjectCore;
using Premtek.Base;
using Premtek;

namespace Premtek.Base
{

    #region "連線參數"

    /// <summary>使用DL RS1A序列埠</summary>
    /// <remarks></remarks>
    public struct DLRS1AConnect
    {
        /// <summary>COM1,2,3...etc</summary>
        /// <remarks></remarks>
        public string PortName;
        /// <summary>Baud Rate: 9600...etc</summary>
        /// <remarks></remarks>
        public string BaudRate;
    }


    /// <summary>使用Line Scan主機網路埠</summary>
    /// <remarks></remarks>
    public struct LJV7060TCPConnect
    {
        /// <summary>通訊位置</summary>
        /// <remarks></remarks>
        public string IP;
        /// <summary>通訊埠</summary>
        /// <remarks></remarks>
        public int Port;
    }


    /// <summary>雷射連線參數</summary>
    /// <remarks></remarks>
    public struct sLaserReaderConnectionParameter
    {
        /// <summary>元件類型</summary>
        /// <remarks></remarks>
        public MSystemParameter.enmLaserInterferometerType CardType;
        public string CardTypeToString()
        {
            switch (CardType)
            {
                case MSystemParameter.enmLaserInterferometerType.None:
                    return "None";
                case MSystemParameter.enmLaserInterferometerType.KeyenceILS065Voltage:
                    return "IL-S065";
                case MSystemParameter.enmLaserInterferometerType.KeyenceLJV7060TCP:
                    return "LJ-V7060 TCP";
            }
            return "Unknown";
        }

        /// <summary>LJ-V7060通訊設定</summary>
        /// <remarks></remarks>

        public LJV7060TCPConnect LJV7060TCP;
        /// <summary>DL-RS1A通訊設定</summary>
        /// <remarks></remarks>

        public DLRS1AConnect DLRS1A;
        #region "DI轉接"
        /// <summary>DI接點編號</summary>
        /// <remarks></remarks>

        public int Bit;
        #endregion

        /// <summary>
        /// 複製
        /// </summary>
        /// <param name="item"></param>
        /// <remarks></remarks>
        public void CopyFrom(ref sLaserReaderConnectionParameter item)
        {
            this.CardType = item.CardType;
            this.LJV7060TCP.IP = item.LJV7060TCP.IP;
            this.LJV7060TCP.Port = item.LJV7060TCP.Port;
            this.DLRS1A = new DLRS1AConnect();
            this.DLRS1A.PortName = item.DLRS1A.PortName;
            this.DLRS1A.BaudRate = item.DLRS1A.BaudRate;
        }
    }


    #endregion

    /// <summary>雷射接點參數</summary>
    /// <remarks></remarks>
    public struct sLaserReaderParameter
    {
        /// <summary>點位名稱</summary>
        /// <remarks></remarks>
        public string Name;
        /// <summary>型號</summary>
        /// <remarks></remarks>
        public MSystemParameter.enmLaserInterferometerType CardType;
        /// <summary>原始紀錄位置</summary>
        /// <remarks></remarks>
        public string Address;
        /// <summary>位置</summary>
        /// <remarks></remarks>
        public long IPAddress;
        /// <summary>埠號</summary>
        /// <remarks></remarks>
        public long Port;
        /// <summary>點號</summary>
        /// <remarks></remarks>
        public long AIIndex;
        /// <summary>略過</summary>
        /// <remarks></remarks>
        public bool ByPass;
        /// <summary>讀值</summary>
        /// <remarks></remarks>
        public string Value;
        /// <summary>Item索引/絕對卡號</summary>
        /// <remarks></remarks>
        public int ItemNo;
        public bool Save(int enmLaserNo, string fileName)
        {
            string mSection = "Channel" + (enmLaserNo + 1).ToString();
            CIni.SaveIniString(mSection, "Name", Name, fileName);
            CIni.SaveIniString(mSection, "CardType", Convert.ToInt32(CardType), fileName);
            CIni.SaveIniString(mSection, "Address", Address, fileName);
            CIni.SaveIniString(mSection, "IPAddress", IPAddress, fileName);
            CIni.SaveIniString(mSection, "Port", Port, fileName);
            CIni.SaveIniString(mSection, "AIIndex", AIIndex, fileName);
            CIni.SaveIniString(mSection, "ByPass", ByPass, fileName);
            CIni.SaveIniString(mSection, "Value", Value, fileName);
            CIni.SaveIniString(mSection, "ItemNo", ItemNo, fileName);
            return true;
        }
        public bool Load(int enmLaserNo, string fileName)
        {
            string mSection = "Channel" + (enmLaserNo + 1).ToString();
            Name = CIni.ReadIniString(mSection, "Name", fileName, "0");
            CardType = (MSystemParameter.enmLaserInterferometerType)Enum.Parse(typeof(MSystemParameter.enmLaserInterferometerType), CIni.ReadIniString(mSection, "CardType", fileName, 0));
            Address = CIni.ReadIniString(mSection, "Address", fileName, "0");
            IPAddress = (long)Convert.ToDouble(CIni.ReadIniString(mSection, "IPAddress", fileName, 0));
            Port = (long)Convert.ToDouble(CIni.ReadIniString(mSection, "Port", fileName, 0));
            AIIndex = (long)Convert.ToDouble(CIni.ReadIniString(mSection, "AIIndex", fileName, 0));
            string result = CIni.ReadIniString(mSection, "ByPass", fileName, 0);
            switch (result)
            {
                case "1":
                case "True":
                case "true":
                    ByPass = true;
                    break;
                default:
                    ByPass = false;
                    break;
            }
            //ByPass = Convert.ToBoolean(Convert.ToInt32( CIni.ReadIniString(mSection, "ByPass", fileName, 0)));
            Value = CIni.ReadIniString(mSection, "Value", fileName, "0");
            ItemNo = Convert.ToInt16(CIni.ReadIniString(mSection, "ItemNo", fileName, 0));
            return true;
        }
    }
}
namespace Premtek.Base
{

    /// <summary>非接觸式測高物件類別</summary>
    /// <remarks></remarks>
    public class CLaserReaderCollection
    {
        /// <summary>[卡片初始化狀態]</summary>
        /// <remarks></remarks>
        public bool IsCardIntialOK
        {
            get { return mIsCardIntialOK; }
        }
        /// <summary>雷射讀值的極限值</summary>
        /// <remarks></remarks>
        public decimal ReaderMaxValue = 9;
        /// <summary>
        /// 陣列複製自Array
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool ArrayCopyFrom(ref sLaserReaderConnectionParameter[] array)
        {
            //編輯區資料全部套用
            for (int mCardNo = 0; mCardNo <= Cards.Count - 1; mCardNo++)
            {
                if (mCardNo < array.Count())
                {
                    //gLaserReaderCollection.Cards(mCardNo).CopyFrom(array(mCardNo))
                    Cards[mCardNo] = array[mCardNo];
                }
            }
            return true;
        }
        /// <summary>
        /// 陣列複製到Array
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool ArrayCopyTo(ref sLaserReaderConnectionParameter[] array)
        {
            array = new sLaserReaderConnectionParameter[Cards.Count];
            for (int mCardNo = 0; mCardNo <= Cards.Count - 1; mCardNo++)
            {
                array[mCardNo] = Cards[mCardNo];
            }
            return true;
        }

        /// <summary>連線參數設定</summary>
        /// <remarks></remarks>
        public CLaserReaderCards Cards = new CLaserReaderCards();
        /// <summary>個別參數設定(索引為enmLaserReader)</summary>
        /// <remarks></remarks>
        private sLaserReaderParameter[] Channel = new sLaserReaderParameter[(int)MCommonDefine.enmLaserReader.Max + 1];
        /// <summary>[卡片初始化狀態]</summary>
        /// <remarks></remarks>
        private bool mIsCardIntialOK;
        /// <summary>[是否走Simulation模式]</summary>
        /// <value></value>
        /// <remarks></remarks>
        public bool IsSimulationType
        {
            set { mIsSimulationType = value; }
        }
        /// <summary>[是否走Simulation模式]</summary>
        /// <remarks></remarks>
        private bool mIsSimulationType;
        /// <summary>卡集合</summary>
        /// <remarks></remarks>

        public List<ILaserReader> Items = new List<ILaserReader>();
        /// <summary>
        /// 卡片設定檔檔名(為避免設定值未被覆蓋,請使用時重新設定一次)
        /// </summary>
        /// <remarks></remarks>

        public string CardFileName = Application.StartupPath + "\\System\\" + MCommonDefine.MachineName + "\\CardLaserReader.ini";



        public bool Initial()
        {
            //=== Card等級存取 ===
            this.Load(CardFileName);
            //=== Card等級存取 ===

            //=== Channel等級存取 ===
            //For mChNo As Integer = enmLaserReader.No1 To enmLaserReader.Max

            for (int mChNo = (int)MCommonDefine.enmLaserReader.No1; (int)mChNo <= MSystemParameter.gSSystemParameter.StageCount - 1; mChNo++)
            {
                Channel[mChNo].Load(mChNo, Application.StartupPath + "\\System\\" + MCommonDefine.MachineName + "\\ConfigLaserReader" + (mChNo + 1).ToString() + ".ini");
                switch (MSystemParameter.gSSystemParameter.MachineType)
                {
                    case MCommonDefine.enmMachineType.eDTS300A:
                    case MCommonDefine.enmMachineType.eDTS330A:
                        //enmMachineType.eGN2, enmMachineType.eGN3,
                        Channel[(int)MCommonDefine.enmLaserReader.No1].AIIndex = MCommonDefineAI.enmAI.LaserReader;
                        break;
                }
            }
            //For mChNo As Integer = enmLaserReader.No1 To enmLaserReader.Max
            for (int mChNo = (int)MCommonDefine.enmLaserReader.No1; mChNo <= MSystemParameter.gSSystemParameter.StageCount - 1; mChNo++)
            {
                Channel[mChNo].Save(mChNo, Application.StartupPath + "\\System\\" + MCommonDefine.MachineName + "\\ConfigLaserReader" + (mChNo + 1).ToString() + ".ini");
            }
            //=== Channel等級存取 ===

            if (this.Initial(Cards.Parameters) == false)
            {
                MDateLog.gSyslog.Save("Initialized Laser Reader Failed!", "", eMessageLevel.Error);
                return false;
            }

            return true;

        }

        /// <summary>初始化</summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool Initial(List<sLaserReaderConnectionParameter> cards)
        {
            try
            {
                string ErrMessage = "";

                if (mIsSimulationType == true)
                {
                    mIsCardIntialOK = false;
                    return true;
                }

                //對每一張卡
                for (int mCardNo = 0; mCardNo <= cards.Count - 1; mCardNo++)
                {
                    switch (cards[mCardNo].CardType)
                    {
                        //依卡型號做初始化
                        case MSystemParameter.enmLaserInterferometerType.None:
                            //虛擬卡
                            break;
                        case MSystemParameter.enmLaserInterferometerType.KeyenceILS065Voltage:
                            Items.Add(new CLaserReader_KeyenceILS065Voltage());
                            break;
                        case MSystemParameter.enmLaserInterferometerType.KeyenceLJV7060TCP:
                            Items.Add(new CLaserReader_KeyenceLJV7060_TCP());
                            if (Items[Items.Count - 1].EthernetOpen(cards[mCardNo].LJV7060TCP.IP, cards[mCardNo].LJV7060TCP.Port) == false)
                            {
                                ErrMessage += "" + cards[mCardNo].CardTypeToString() + "(" + cards[mCardNo].LJV7060TCP.IP + "Port:" + cards[mCardNo].LJV7060TCP.Port + ") 初始化失敗.";
                            }
                            break;
                        case MSystemParameter.enmLaserInterferometerType.KeyenceIL065DLRS1A:
                            Items.Add(new CLaserReader_DLRS1A());
                            if (Items[Items.Count - 1].Initial(cards[mCardNo].DLRS1A.PortName, cards[mCardNo].DLRS1A.BaudRate) == false)
                            {
                                ErrMessage += "" + cards[mCardNo].CardTypeToString() + "(" + cards[mCardNo].DLRS1A.PortName + ") 初始化失敗.";
                            }
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(ErrMessage))
                {
                    mIsCardIntialOK = false;
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1014000), "Error_1014000", eMessageLevel.Error);
                    MDateLog.gSyslog.Save("Eror Message: " + ErrMessage, "", eMessageLevel.Error);
                    MessageBox.Show(ErrMessage, "Laser Reader Collection");
                    return false;
                }
                else
                {
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.INFO_6004000));
                    mIsCardIntialOK = true;
                    return true;
                }


            }
            catch (Exception ex)
            {
                mIsCardIntialOK = false;
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1014000), "Error_1014000", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                MessageBox.Show("初始化失敗" + ex.Message, "CDICollection@Initial");
                return false;
            }
        }

        /// <summary>儲存連線參數設定</summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public object Save(string fileName)
        {

            //string mSection = "LaserReader";

            for (int mChNo = (int)MCommonDefine.enmLaserReader.No1; mChNo <= (int)MCommonDefine.enmLaserReader.Max; mChNo++)
            {
                Channel[mChNo].Save(mChNo, Application.StartupPath + "\\System\\" + MCommonDefine.MachineName + "\\ConfigLaserReader" + (mChNo + 1).ToString() + ".ini");
            }
            return true;
        }
        /// <summary>讀取連線參數設定</summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public object Load(string fileName)
        {
            //string mSection = "LaserReader";
            //Dim mCardCount As Integer
            Cards.Load(fileName);
            for (int mChNo = (int)MCommonDefine.enmLaserReader.No1; mChNo <= (int)MCommonDefine.enmLaserReader.Max; mChNo++)
            {
                Channel[mChNo].Load(mChNo, Application.StartupPath + "\\System\\" + MCommonDefine.MachineName + "\\ConfigLaserReader" + (mChNo + 1).ToString() + ".ini");
            }
            return true;
        }


        /// <summary>資源釋放</summary>
        /// <remarks></remarks>
        public bool Close()
        {

            try
            {
                //模擬
                if (mIsSimulationType == true)
                {
                    MDateLog.gSyslog.Save("Laser Close OK.");
                    MDateLog.gSyslog.Save("Simulation");
                    return true;
                }

                //未開卡成功
                if (mIsCardIntialOK == false)
                {
                    MDateLog.gSyslog.Save("Laser Close OK.");
                    return true;
                }

                //[說明]:卡片關閉
                for (int mItemNo = 0; mItemNo <= Items.Count - 1; mItemNo++)
                {
                    Items[mItemNo].Close();
                }
                MDateLog.gSyslog.Save("Laser Close OK.");
                return true;


            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save("Laser Close Failed!", "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                MessageBox.Show(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1004001) + ex.Message, "CDICollection@Initial");
                return false;
            }

        }

        /// <summary>
        /// 是否索引超出範圍
        /// </summary>
        /// <param name="laserNo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsIndexOutOfRange(int laserNo)
        {
            if (laserNo < Channel.GetLowerBound(0))
            {
                return true;
            }
            if (laserNo > Channel.GetUpperBound(0))
            {
                return true;
            }
            if (Channel[laserNo].ItemNo < 0)
            {
                return true;
            }
            if (Channel[laserNo].ItemNo >= Items.Count)
            {
                return true;
            }
            return false;
        }
        // ''' <summary>
        // ''' 狀態重置
        // ''' </summary>
        // ''' <param name="laserNo">虛擬ID</param>
        // ''' <remarks></remarks>
        //Public Sub ResetState(ByVal laserNo As Integer)
        //    If IsIndexOutOfRange(laserNo) Then
        //        Exit Sub
        //    End If
        //    With Channel(laserNo)
        //        Call Items(Channel(laserNo).ItemNo).ResetState()
        //    End With
        //End Sub


        /// <summary>讀取值命令</summary>
        /// <param name="laserNo">enmLaser索引</param>
        /// <param name="value">預設值</param>
        /// <returns>讀取資料,如為Line-Scan,使用|做Split後使用</returns>
        /// <remarks>有waitReturn,value才有效</remarks>
        public bool GetValue(string Mode, int laserNo, ref string value, bool waitReturn = false)
        {
            if (IsIndexOutOfRange(laserNo))
            {
                return false;
            }

            try
            {
                
                //略過,傳回預設值
                if (Channel[laserNo].ByPass == true)
                {
                    return true;
                }
                return Items[Channel[laserNo].ItemNo].GetValue(Mode, ref value, (int)Channel[laserNo].AIIndex, waitReturn);
                //傳回讀取結果


            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1014003), "Error_1014003", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                return false;
            }
        }

        /// <summary>讀取雷射資料</summary>
        /// <param name="laserNo">enmLaser索引</param>
        /// <param name="value">資料值</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool GetLaserValue(int laserNo, ref string value)
        {
            //略過,傳回預設值
            if (Channel[laserNo].ByPass == true)
            {
                return true;
            }
            value = Items[Channel[laserNo].ItemNo].Result.Value;
            return Items[Channel[laserNo].ItemNo].Result.Status;
        }

        /// <summary>選取Recipe</summary>
        /// <param name="laserNo">enmLaser索引</param>
        /// <param name="ProgramID">LaserReader的RecipeName</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool SelectRecipe(int laserNo, int ProgramID)
        {
            if (IsIndexOutOfRange(laserNo))
            {
                return false;
            }

            try
            {
                //略過,傳回預設值
                if (Channel[laserNo].ByPass == true)
                {
                    return true;
                }

                return Items[Channel[laserNo].ItemNo].ChangeProgram(ProgramID);
                //傳回讀取結果

            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(Premtek.Base.EqpID.Error_1014002), "Error_1014002", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                return false;
            }
        }
        /// <summary>
        /// 是否讀取逾時
        /// </summary>
        /// <param name="laserNo"></param>
        /// <returns></returns>
        public bool IsTimeOut(int laserNo)
        {
            if (IsIndexOutOfRange(laserNo))
            {
                return false;
            }
            //略過,傳回預設值
            if (Channel[laserNo].ByPass == true)
            {
                return false;
            }
            return Items[Channel[laserNo].ItemNo].IsTimeOut;
        }

        /// <summary>是否讀取逾時</summary>
        /// <param name="laserNo"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IsBusy(int laserNo)
        {
            if (IsIndexOutOfRange(laserNo))
            {
                return false;
            }
            //略過,傳回預設值
            if (Channel[laserNo].ByPass == true)
            {
                return false;
            }

            return Items[Channel[laserNo].ItemNo].IsBusy;
        }


        /// <summary>
        /// 設定TimeOut時間
        /// </summary>
        /// <param name="laserNo"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int SetTimeoutTimes(int laserNo, int value)
        {
            if (IsIndexOutOfRange(laserNo))
            {
                return 0;
            }
            
            //略過,傳回預設值
            if (Channel[laserNo].ByPass == true)
            {
                return 0;
            }
            Items[Channel[laserNo].ItemNo].TimeoutTimer = value;
            return 1;
        }
        public int GetTimeoutTimes(int laserNo)
        {
            if (IsIndexOutOfRange(laserNo))
            {
                return 0;
            }
            
            //略過,傳回預設值
            if (Channel[laserNo].ByPass == true)
            {
                return 0;
            }
            return Items[Channel[laserNo].ItemNo].TimeoutTimer;
        }


    }
}
