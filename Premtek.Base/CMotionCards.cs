using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek.Base
{
    using ProjectCore;

    #region "運動控制連線參數"
    /// <summary>PCI-1245/85連線設定</summary>
    /// <remarks></remarks>
    public struct sPCIAdvantechConnect
    {
        /// <summary>實際初始化卡號</summary>
        /// <remarks></remarks>
        public int CardNo;
    }

    /// <summary>MODBUS連線設定</summary>
    /// <remarks></remarks>
    public struct sMODBUSConnect
    {
        /// <summary>COM通訊埠</summary>
        /// <remarks></remarks>
        public string PortName;
        /// <summary>通訊埠</summary>
        /// <remarks></remarks>
        public System.IO.Ports.SerialPort Port;
        /// <summary>對Motion註冊票券</summary>
        /// <remarks></remarks>
        public int ItemNo;
        /// <summary>
        /// 軸數/站數
        /// </summary>
        /// <remarks></remarks>
        public int AxisCount;
        /// <summary>
        /// 各站馬達類型
        /// </summary>
        /// <remarks></remarks>
        public List<eMotorType> MotorType;
        /// <summary>
        /// 各站軸通訊類型
        /// </summary>
        /// <remarks></remarks>

        public List<enmAxisType> AxisType;
        public void SetItemNo(int value)
        {
            this.ItemNo = value;
        }
    }

    /// <summary>運動控制卡連線參數</summary>
    /// <remarks></remarks>
    public struct SMotionConnectParameter
    {
        /// <summary>卡片型號</summary>
        /// <remarks></remarks>

        public enmMotionCardType CardType;
        /// <summary>PCI-1245連線設定</summary>
        /// <remarks></remarks>
        public sPCIAdvantechConnect PCI_1245;
        /// <summary>
        /// PCI-1285連線設定
        /// </summary>
        /// <remarks></remarks>

        public sPCIAdvantechConnect PCI_1285;
        /// <summary>MODBUS連線設定</summary>
        /// <remarks></remarks>

        public sMODBUSConnect MODBUS;
        /// <summary>取得卡片名稱</summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public string GetCardTypeName()
        {
            switch (CardType)
            {
                case enmMotionCardType.None:
                    return "None";
                case enmMotionCardType.ModBus:
                    return "ModBus";
                case enmMotionCardType.PCI_1245:
                    return "PCI_1245";
                case enmMotionCardType.PCI_1285:
                    return "PCI_1285";
                default:
                    return "Undefined";
            }
        }

        public bool Save(int cardNo, string fileName)
        {
            string strSection = null;
            strSection = "Configuration";

            CIni.SaveIniString(strSection, "MotionCard-" + cardNo + "-CardType", CardType, fileName);
            switch (CardType)
            {
                case enmMotionCardType.PCI_1245:
                    CIni.SaveIniString(strSection, "MotionCard-" + cardNo + "-CardNo", PCI_1245.CardNo, fileName);
                    break;
                case enmMotionCardType.PCI_1285:
                    CIni.SaveIniString(strSection, "MotionCard-" + cardNo + "-CardNo", PCI_1285.CardNo, fileName);
                    break;
                case enmMotionCardType.ModBus:
                    CIni.SaveIniString(strSection, "MotionCard-" + cardNo + "-PortName", MODBUS.PortName, fileName);
                    CIni.SaveIniString(strSection, "MotionCard-" + cardNo + "-BaudRate", MODBUS.Port.BaudRate, fileName);
                    CIni.SaveIniString(strSection, "MotionCard-" + cardNo + "-StationCount", MODBUS.AxisCount, fileName);
                    for (int mAxisNo = 0; mAxisNo <= MODBUS.AxisType.Count - 1; mAxisNo++)
                    {
                        CIni.SaveIniString(strSection, "MotionCard-" + cardNo + "-Station" + mAxisNo + "AxisType", Convert.ToInt32(MODBUS.AxisType[mAxisNo]), fileName);
                    }

                    for (int mAxisNo = 0; mAxisNo <= MODBUS.MotorType.Count - 1; mAxisNo++)
                    {
                        CIni.SaveIniString(strSection, "MotionCard-" + cardNo + "-Station" + mAxisNo + "MotorType", Convert.ToInt32(MODBUS.MotorType[mAxisNo]), fileName);
                    }


                    break;
                case enmMotionCardType.None:
                    break;
            }
            return true;
        }

        public bool Load(int cardNo, string fileName)
        {
            string strSection = null;
            strSection = "Configuration";

             Enum.TryParse<enmMotionCardType>(CIni.ReadIniString(strSection, "MotionCard-" + cardNo + "-CardType", fileName, 0), out this.CardType);
            switch (CardType)
            {
                case enmMotionCardType.PCI_1245:
                    PCI_1245.CardNo = Convert.ToInt32(CIni.ReadIniString(strSection, "MotionCard-" + cardNo + "-CardNo", fileName, 0));
                    break;
                case enmMotionCardType.PCI_1285:
                    PCI_1285.CardNo = Convert.ToInt32(CIni.ReadIniString(strSection, "MotionCard-" + cardNo + "-CardNo", fileName, 0));
                    break;
                case enmMotionCardType.ModBus:
                    MODBUS.PortName = CIni.ReadIniString(strSection, "MotionCard-" + cardNo + "-PortName", fileName, "COM1");
                    MODBUS.Port = new System.IO.Ports.SerialPort();
                    MODBUS.Port.PortName = MODBUS.PortName;
                    MODBUS.Port.BaudRate = Convert.ToInt32(CIni.ReadIniString(strSection, "MotionCard-" + cardNo + "-BaudRate", fileName, 9600));
                    int mAxisCount = Convert.ToInt32(CIni.ReadIniString(strSection, "MotionCard-" + cardNo + "-StationCount", fileName, 0));
                    MODBUS.AxisType = new List<enmAxisType>();
                    for (int mAxisNo = 0; mAxisNo <= MODBUS.AxisType.Count - 1; mAxisNo++)
                    {
                        enmAxisType item;
                        Enum.TryParse<enmAxisType>(CIni.ReadIniString(strSection, "MotionCard-" + cardNo + "-Station" + mAxisNo + "AxisType", fileName, 0), out item);
                        MODBUS.AxisType.Add(item);
                    }

                    MODBUS.MotorType = new List<eMotorType>();
                    for (int mAxisNo = 0; mAxisNo <= MODBUS.MotorType.Count - 1; mAxisNo++)
                    {
                        eMotorType item;
                        Enum.TryParse<eMotorType>(CIni.ReadIniString(strSection, "MotionCard-" + cardNo + "-Station" + mAxisNo + "MotorType", fileName, 0), out item);
                        MODBUS.MotorType.Add(item);
                    }

                    break;
                case enmMotionCardType.None:
                    break;
            }
            return true;
        }

    }
    #endregion

    /// <summary>軸卡連線參數</summary>
    /// <remarks></remarks>
    public class CMotionCards
    {
        /// <summary>軸卡連線參數</summary>
        /// <remarks></remarks>
        public List<SMotionConnectParameter> MotionCardConnectParameter = new List<SMotionConnectParameter>();
        /// <summary>[讀取軸卡資料]</summary>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool Load(string strFileName)
        {
            try
            {
                int mTotoalMotionCardCount = 0;
                //所有軸卡之數量
                string strSection = null;
                strSection = "Configuration";

                //[說明]:讀取卡片數量
                mTotoalMotionCardCount = Convert.ToInt32(CIni.ReadIniString(strSection, "TotalMotionCardCount", strFileName, 2));
                MDateLog.gSyslog.Save("Motion-Card:" + mTotoalMotionCardCount);

                MotionCardConnectParameter.Clear();
                //[說明]:對每一張Motion卡,讀取參數
                for (int mCardNo = 0; mCardNo <= mTotoalMotionCardCount - 1; mCardNo++)
                {
                    SMotionConnectParameter mCard = new SMotionConnectParameter();
                    mCard.Load(mCardNo, strFileName);
                    MotionCardConnectParameter.Add(mCard);
                }

                return true;
            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1002010), "Error_1002010", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "MMotion@ReadMotionCard", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }

        /// <summary>[存取軸卡資料]</summary>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool Save(string strFileName)
        {

            try
            {
                string strSection = null;
                strSection = "Configuration";
                //[說明]:儲存卡片數量
                CIni.SaveIniString(strSection, "TotalMotionCardCount", MotionCardConnectParameter.Count, strFileName);

                //[說明]:對每一張Motion卡,儲存參數
                for (int mCardNo = 0; mCardNo <= MotionCardConnectParameter.Count - 1; mCardNo++)
                {
                    this.MotionCardConnectParameter[mCardNo].Save(mCardNo, strFileName);
                }

                return true;
            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1002011), "Error_1002011", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
             System.Windows.Forms.MessageBox.Show(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1002011) + ex.Message, "MMotion@SaveMotionCard", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }
    }

}
