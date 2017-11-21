using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectCore;
using Premtek;

namespace Premtek
{

    /// <summary>DI集合</summary>
    /// <remarks></remarks>
    public class CDICollection
    {
        /// <summary>
        /// DI卡連線設定
        /// </summary>
        /// <remarks></remarks>
        public CDICards Cards = new CDICards();
        /// <summary>[卡片初始化狀態]</summary>
        /// <remarks></remarks>
        public bool IsCardIntialOK
        {
            get { return mIsCardIntialOK; }
        }


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

        public List<IDIInterface> Items = new List<IDIInterface>();
        /// <summary>總點數</summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int TotalBits
        {
            get { return mTotalBits; }
            set { mTotalBits = value; }
        }
        /// <summary>總點數</summary>
        /// <remarks></remarks>
        //預設值
        int mTotalBits = 32;

        /// <summary>初始化</summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool Initial(List<sDICardParameter> cards)
        {
            try
            {
                string ErrMessage = "";

                if (mIsSimulationType == true)
                {
                    mIsCardIntialOK = false;
                    return true;
                }
                else
                {
                    mTotalBits = 0;
                    //對每一張卡
                    for (int mCardNo = 0; mCardNo <= cards.Count - 1; mCardNo++)
                    {
                        switch (cards[mCardNo].CardType)
                        {
                            //依卡型號做初始化
                            case enmDICardType.None:
                                //虛擬卡
                                Items.Add(new CDIVirtual());
                                break;
                            case enmDICardType.PCI_1756:
                                Items.Add(new CDI_PCI_1756());
                                if (Items[Items.Count - 1].Initial(cards[mCardNo].DeviceDescreiption) == false)
                                {
                                    ErrMessage += "" + cards[mCardNo].DeviceDescreiption + "初始化失敗.";
                                }
                                break;
                            case enmDICardType.PCI_1758:
                                Items.Add(new CDI_PCI_1758());
                                if (Items[Items.Count - 1].Initial(cards[mCardNo].DeviceDescreiption) == false)
                                {
                                    ErrMessage += "" + cards[mCardNo].DeviceDescreiption + "初始化失敗.";
                                }
                                break;
                            case enmDICardType.PCI_1710:
                                Items.Add(new CDI_PCI_1710());
                                if (Items[Items.Count - 1].Initial(cards[mCardNo].DeviceDescreiption) == false)
                                {
                                    ErrMessage += "" + cards[mCardNo].DeviceDescreiption + "初始化失敗.";
                                }

                                break;
                        }
                        mTotalBits += Items[Items.Count - 1].PortPerCard * Items[Items.Count - 1].BitsPerPort;
                        //計算總點數
                    }
                    gblnDI = new bool[Items.Count, Items[0].PortPerCard + 1, Items[0].BitsPerPort + 1];
                    if (mTotalBits > 0)
                    {
                        gblnDIInput = new bool[mTotalBits];
                        DIParameter = new sDIParameter[mTotalBits];
                    }

                    if (!string.IsNullOrEmpty(ErrMessage))
                    {
                        mIsCardIntialOK = false;
                        MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1004000), "Error_1004000", eMessageLevel.Error);
                        MDateLog.gSyslog.Save("Eror Message: " + ErrMessage, "", eMessageLevel.Error);
                        System.Windows.Forms.MessageBox.Show(ErrMessage, "DI Collection", System.Windows.Forms.MessageBoxButtons.OK);
                        return false;
                    }
                    else
                    {
                        MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.INFO_6004000));
                        mIsCardIntialOK = true;
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                mIsCardIntialOK = false;
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1004000), "Error_1004000", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                System.Windows.Forms.MessageBox.Show("初始化失敗" + ex.Message, "CDICollection@Initial", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }


        /// <summary>資源釋放</summary>
        /// <remarks></remarks>
        public bool Close()
        {

            try
            {
                int mCardNo = 0;

                if (mIsSimulationType == true)
                {
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.INFO_6004001));
                    MDateLog.gSyslog.Save("Simulation");
                    return true;
                }

                if (mIsCardIntialOK == true)
                {
                    //[說明]:卡片關閉
                    for (mCardNo = 0; mCardNo <= Items.Count - 1; mCardNo++)
                    {
                        Items[mCardNo].Close();
                    }
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.INFO_6004001));
                    return true;
                }
                else
                {
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.INFO_6004001));
                    //gSyslog.Save("DI Card Close OK(Initial Failed.).")
                    return true;
                }


            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1004001), EqpID.Error_1004001.ToString(), eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                System.Windows.Forms.MessageBox.Show(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1004001) + ex.Message, "CDICollection@Initial", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }

        }


        /// <summary>DI接點參數</summary>
        /// <remarks></remarks>
        public sDIParameter[] DIParameter = new sDIParameter[256];
        /// <summary>DI原始資料</summary>
        /// <remarks></remarks>
        bool[, ,] gblnDI;
        //--------------------------------------------------------------------
        /// <summary>[DI-Input(陣列)]</summary>
        /// <remarks></remarks>

        bool[] gblnDIInput = new bool[256];
        /// <summary>取得DI輸入值</summary>
        /// <param name="DIIndex"></param>
        /// <param name="defaultStatus"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool GetState(int DIIndex, bool defaultStatus = false)
        {
            if (DIIndex < DIParameter.GetLowerBound(0))
            {
                return defaultStatus;
            }
            if (DIIndex > DIParameter.GetUpperBound(0))
            {
                return defaultStatus;
            }

            try
            {
                //略過,傳回預設值
                if (DIParameter[DIIndex].ByPass == true)
                {
                    return defaultStatus;
                }

                //反向
                if (DIParameter[DIIndex].Toggle == true)
                {
                    return !gblnDIInput[DIIndex];
                    //傳回反向讀取結果
                }

                return gblnDIInput[DIIndex];
                //傳回讀取結果

            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1004002), "Error_1004002", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                return false;
            }
        }

        public bool RefreshDI()
        {

            long mBitNo = 0;
            int mPortNo = 0;
            byte bytDiData = 0;
            //[讀取出來的數值]

            try
            {
                if (mIsSimulationType == true)
                {
                    return true;
                }
                else
                {
                    if (mIsCardIntialOK == true)
                    {
                        //Byte資料轉為卡埠點表
                        for (int mCardNo = 0; mCardNo <= Items.Count - 1; mCardNo++)
                        {
                            for (mPortNo = 0; mPortNo <= Items[mCardNo].PortPerCard - 1; mPortNo++)
                            {
                                Items[mCardNo].Read(mPortNo, ref bytDiData);
                                for (mBitNo = Items[mCardNo].BitsPerPort - 1; mBitNo >= 0; mBitNo += -1)
                                {
                                    gblnDI[mCardNo, mPortNo, mBitNo] = ((bytDiData & (byte)(Math.Pow(2, mBitNo))) != 0 ? true : false);
                                }
                                bytDiData = 0;
                            }
                        }

                        //點位Mapping
                        //目前DI只使用32組
                        for (mBitNo = 0; mBitNo <= mTotalBits - 1; mBitNo++)
                        {
                            gblnDIInput[mBitNo] = gblnDI[DIParameter[mBitNo].IPAddress, DIParameter[mBitNo].Port, DIParameter[mBitNo].Bits];
                        }
                        return true;
                    }
                    else
                    {
                        return true;
                    }
                }


            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1004003), "Error_1004003", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                return false;
            }
        }

        /// <summary>讀取DI設定檔</summary>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool Load(string strFileName)
        {

            try
            {
                int mBitNo = 0;
                string strSection = null;
                string[] str_DIAddressBuffer = null;

                strSection = "Configuration";
                MDateLog.gSyslog.Save("DI Parameter Loaded as below:");

                //讀取
                for (mBitNo = 0; mBitNo <= mTotalBits - 1; mBitNo++)
                {
                    
                    //[Note]:Save Full Name(全語系)，Name是語系切換完之後的內容
                    string mName = CIni.ReadIniString(strSection, "DI-" + mBitNo + "-Name", strFileName, "");
                    string[] mSplitedName = mName.Split(new string[] { "," }, StringSplitOptions.None);
                    switch (ProjectCore.MSystemParameter.gSSystemParameter.LanguageType)
                    {
                        case ProjectCore.MSystemParameter.enmLanguageType.eEnglish:
                            if (mSplitedName.GetUpperBound(0) >= 0)
                            {
                                DIParameter[mBitNo].Name = mSplitedName[0];
                            }
                            break;
                        case ProjectCore.MSystemParameter.enmLanguageType.eTraditionalChinese:
                            if (mSplitedName.GetUpperBound(0) >= 1)
                            {
                                DIParameter[mBitNo].Name = mSplitedName[1];
                            }
                            break;
                        case ProjectCore.MSystemParameter.enmLanguageType.eSimplifiedChinese:
                            if (mSplitedName.GetUpperBound(0) >= 2)
                            {
                                DIParameter[mBitNo].Name = mSplitedName[2];
                            }
                            break;
                    }

                    DIParameter[mBitNo].FullName = mName;
                    Enum.TryParse<Premtek.enmDICardType>(CIni.ReadIniString(strSection, "DI-" + mBitNo + "-CardType", strFileName, ((int)enmDICardType.PCI_1758).ToString()), out DIParameter[mBitNo].CardType);
                    DIParameter[mBitNo].Address = CIni.ReadIniString(strSection, "DI-" + mBitNo + "-Address", strFileName, "");
                   
                    int mByPass, mToggle;
                    int.TryParse(CIni.ReadIniString(strSection, "DI-" + mBitNo + "-ByPass", strFileName, "0"), out mByPass);
                    int.TryParse(CIni.ReadIniString(strSection, "DI-" + mBitNo + "-Toggle", strFileName, "0"), out mToggle);
                    DIParameter[mBitNo].ByPass = Convert.ToBoolean(mByPass);
                    DIParameter[mBitNo].Toggle = Convert.ToBoolean(mToggle);

                    if (DIParameter[mBitNo].Address.Contains("-"))
                    {
                        str_DIAddressBuffer = DIParameter[mBitNo].Address.Split(new string[] { "-" }, StringSplitOptions.None);
                        long.TryParse((str_DIAddressBuffer[0]), out DIParameter[mBitNo].IPAddress);
                        DIParameter[mBitNo].Port = Convert.ToInt16(str_DIAddressBuffer[1].Substring(0, 2), 16) / 8;
                        DIParameter[mBitNo].Bits = Convert.ToInt64(str_DIAddressBuffer[1].Substring(0, 2), 16) % 8;
                    }
                    MDateLog.gSyslog.Save("DI-" + mBitNo.ToString("000") + " Address: " + DIParameter[mBitNo].Address + " CardType: " + DIParameter[mBitNo].CardType.ToString() + " ByPass: " + (DIParameter[mBitNo].ByPass ? 1 : 0) + " Toggle: " + (DIParameter[mBitNo].Toggle ? 1 : 0) + " Name: " + DIParameter[mBitNo].Name);
                }
                return true;

            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1002016), "Error_1002016", eMessageLevel.Error);
                System.Windows.Forms.MessageBox.Show("Excepption Message: " + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 儲存DI設定檔
        /// </summary>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool Save(string strFileName)
	{

		try {
			long mBitNo = 0;
			string strSection = null;

			strSection = "Configuration";
			MDateLog.gSyslog.Save("DI Parameter Saved as below:");
			for (mBitNo = 0; mBitNo <= mTotalBits - 1; mBitNo++) {
				//[Note]:Save Full Name(全語系)，Name是語系切換完之後的內容
                CIni.SaveIniString(strSection, "DI-" + mBitNo + "-Name", DIParameter[mBitNo].FullName , strFileName);
				CIni.SaveIniString(strSection, "DI-" + mBitNo + "-CardType", Convert.ToInt32(DIParameter[mBitNo].CardType).ToString(), strFileName);
				CIni.SaveIniString(strSection, "DI-" + mBitNo + "-Address", DIParameter[mBitNo].Address, strFileName);
				CIni.SaveIniString(strSection, "DI-" + mBitNo + "-ByPass", Convert.ToInt32(DIParameter[mBitNo].ByPass).ToString(), strFileName);
				CIni.SaveIniString(strSection, "DI-" + mBitNo + "-Toggle", Convert.ToInt32(DIParameter[mBitNo].Toggle).ToString(), strFileName);
                MDateLog.gSyslog.Save("DI-" + mBitNo.ToString("000") + " Address: " + DIParameter[mBitNo].Address + " CardType: " + DIParameter[mBitNo].CardType.ToString() + " ByPass: " + (DIParameter[mBitNo].ByPass ? 1 : 0) + " Toggle: " + (DIParameter[mBitNo].Toggle ? 1 : 0) + " Name: " + DIParameter[mBitNo].Name);
			}
			return true;

		} catch (Exception ex) {
			MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1002017), "Error_1002017", eMessageLevel.Error);
			MDateLog.gSyslog.Save("Excepption Message: " + ex.Message, "", eMessageLevel.Error);
			return false;
		}
	}
    }
}


/// <summary>DI卡參數</summary>
/// <remarks></remarks>
public class sDICardParameter
{
    /// <summary>卡型號</summary>
    /// <remarks></remarks>
    public enmDICardType CardType;
    /// <summary>敘述</summary>
    /// <remarks></remarks>
    public string DeviceDescreiption;
    /// <summary>卡號</summary>
    /// <remarks></remarks>
    public int CardID;
    public bool Load(int cardNo, string fileName)
    {
        string strSection = null;
        strSection = "Configuration";
        int.TryParse(Premtek. CIni.ReadIniString(strSection, "DICard-" + cardNo + "-CardID", fileName, ""), out CardID);
        Enum.TryParse<enmDICardType>(Premtek.CIni.ReadIniString(strSection, "DICard-" + cardNo + "-CardType", fileName, ((int)enmDICardType.PCI_1756).ToString()), out CardType);
        DeviceDescreiption = Premtek.CIni.ReadIniString(strSection, "DICard-" + cardNo + "-Description", fileName, "");
       MDateLog. gSyslog.Save("DI-Card" + cardNo + ":\tCardID: " + CardID + " CardType: " + CardType.ToString() + " Desc: " + DeviceDescreiption);
        return true;
    }
    public bool Save(int cardNo, string fileName)
    {
        string strSection = null;
        strSection = "Configuration";
        Premtek.CIni.SaveIniString(strSection, "DICard-" + cardNo + "-CardID", CardID.ToString(), fileName);
        Premtek.CIni.SaveIniString(strSection, "DICard-" + cardNo + "-CardType", CardType.ToString(), fileName);
        Premtek.CIni.SaveIniString(strSection, "DICard-" + cardNo + "-Description", DeviceDescreiption, fileName);
        return true;
    }

}
/// <summary>DI點位參數</summary>
/// <remarks></remarks>
public struct sDIParameter
{
    /// <summary>點位名稱(所有語系)</summary>
    /// <remarks></remarks>
    public string FullName;
    /// <summary>點位名稱</summary>
    /// <remarks></remarks>
    public string Name;
    /// <summary>型號</summary>
    /// <remarks></remarks>
    public enmDICardType CardType;
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
    public long Bits;
    /// <summary>略過</summary>
    /// <remarks></remarks>
    public bool ByPass;
    /// <summary>邏輯反向</summary>
    /// <remarks></remarks>
    public bool Toggle;
    /// <summary>讀值</summary>
    /// <remarks></remarks>
    public bool Value;
}

