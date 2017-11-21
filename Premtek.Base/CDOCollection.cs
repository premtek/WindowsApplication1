using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectCore;
using Premtek;
using System.Diagnostics;

namespace Premtek
{
    /// <summary>
    /// DI集合
    /// </summary>
    public class CDOCollection
    {
        /// <summary>
        /// DO卡連線設定
        /// </summary>
        public CDOCards Cards = new CDOCards();
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

        public List<IDOInterface> Items = new List<IDOInterface>();

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
        int mTotalBits = 32;//預設值

        /// <summary>DO接點參數</summary>
        /// <remarks></remarks>
        public sDOParameter[] DOParameter = new sDOParameter[256];

        /// <summary> DO原始資料，初始化後定義 </summary>
        public bool[, ,] RawValue;
        

        /// <summary> [DO-Output(陣列)] </summary>
        bool[] mblnDOOutput = new bool[256];


        /// <summary> 取得目前DO設定的狀態 </summary>
        /// <param name="DOIndex"></param>
        /// <returns></returns>
        public bool GetState(int DOIndex, bool defaultStatus = false)
        {
            try
            {
                if (DOIndex < 0)
                { return false; }
                else
                { return mblnDOOutput[DOIndex]; }
            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1005002), "Error_1005002", eMessageLevel.Error);
                MDateLog.gSyslog.Save("DO Index: " + DOIndex, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                return false;
            }
        }

        /// <summary> 設定DO的狀態 </summary>
        /// <param name="DOIndex"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetState(int DOIndex, bool value)
        {
            try
            {
                if (DOIndex < 0)
                { return false; }
                else
                {
                    if (DOParameter[DOIndex].ByPass == true)
                    {
                        return true;
                    }
                    if (DOParameter[DOIndex].Toggle == true)
                    {
                        mblnDOOutput[DOIndex] = !value;
                        return true;
                    }

                    mblnDOOutput[DOIndex] = value;
                    return true;
                }
            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1005003), "Error_1005003", eMessageLevel.Error);
                MDateLog.gSyslog.Save("DO Index: " + DOIndex, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                return false;
            }
        }

        /// <summary> 狀態反向輸出 </summary>
        /// <param name="DOIndex"></param>
        /// <returns></returns>
        public bool ToggleOutput(int DOIndex)
        {
            try
            {
                if (DOIndex > DOParameter.Length)
                { return false; }

                if (DOIndex < 0)
                { return false; }

                if (DOParameter[DOIndex].ByPass == true)
                {
                    return mblnDOOutput[DOIndex];
                }
                else
                {
                    return mblnDOOutput[DOIndex] = !mblnDOOutput[DOIndex];
                }

            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1005004), "Error_1005004", eMessageLevel.Error);
                MDateLog.gSyslog.Save("DO Index: " + DOIndex, "", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                return false;
            }
        }

        //wenda 待確認
        //Public Sub New()
        //    ReDim RawValue(1, 8, 8)
        //End Sub

        /// <summary> 初始化 </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public bool Initial(List<sDOCardParameter> cards)
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
                            case enmDOCardType.None:
                                //虛擬卡
                                Items.Add(new CDOVirtual());
                                break;
                            case enmDOCardType.PCI_1756:
                                Items.Add(new CDO_PCI_1756());
                                if (Items[Items.Count - 1].Initial(cards[mCardNo].DeviceDescreiption) == false)
                                {
                                    ErrMessage += "" + cards[mCardNo].DeviceDescreiption + "初始化失敗.";
                                }
                                break;
                            case enmDOCardType.PCI_1758:
                                Items.Add(new CDO_PCI_1758());
                                if (Items[Items.Count - 1].Initial(cards[mCardNo].DeviceDescreiption) == false)
                                {
                                    ErrMessage += "" + cards[mCardNo].DeviceDescreiption + "初始化失敗.";
                                }
                                break;
                            case enmDOCardType.PCI_1710:
                                Items.Add(new CDO_PCI_1710());
                                if (Items[Items.Count - 1].Initial(cards[mCardNo].DeviceDescreiption) == false)
                                {
                                    ErrMessage += "" + cards[mCardNo].DeviceDescreiption + "初始化失敗.";
                                }

                                break;
                        }
                        mTotalBits += Items[Items.Count - 1].PortPerCard * Items[Items.Count - 1].BitsPerPort;
                        //計算總點數
                    }

                    RawValue= new bool [Items.Count , Items[0].PortPerCard , Items[0].BitsPerPort ];
                    DOParameter= new sDOParameter[mTotalBits];
                    mblnDOOutput= new bool [mTotalBits];
                    
                    if (ErrMessage != "")
                    {
                        mIsCardIntialOK = false;
                        MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1005000), "Error_1005000", eMessageLevel.Error);
                        System.Windows.Forms.MessageBox.Show(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1005000), "DO Collection", System.Windows.Forms.MessageBoxButtons.OK);
                        return false;
                    }
                    else
                    {
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

        /// <summary> 資源釋放 </summary>
        /// <returns></returns>
        public bool Close()
        {
            try
            {
                int mCardNo = 0;

                if (mIsSimulationType == true)
                {
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.INFO_6005001));
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
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.INFO_6005001));
                    return true;
                }
                else
                {
                    MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.INFO_6005001));
                    //gSyslog.Save("DO Card Close OK(Initial Failed.).")
                    return true;
                }


            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1005001), EqpID.Error_1005001.ToString(), eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                System.Windows.Forms.MessageBox.Show(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1005001) + ex.Message, "CDOCollection@Initial", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }


        }

        /// <summary> 寫入卡片DO點 </summary>
        /// <returns></returns>
        public bool RefreshDO()
        {
            int mBitNo = 0;
            int IPBuffer = 0;
            int PortBuffer = 0;
            int PointBuffer = 0;
            //int mPortNo = 0;
            byte bytDoData = 0;
            //[讀取出來的數值]

            try
            {
                if (mIsSimulationType == true)
                {
                    return true;
                }
                else
                {
                    for (mBitNo = 0; mBitNo <= mTotalBits - 1; mBitNo++)
                    {
                        RawValue[DOParameter[mBitNo].IPAddress, DOParameter[mBitNo].Port, DOParameter[mBitNo].Bits] = mblnDOOutput[mBitNo];
                    }

                    for (IPBuffer = 0; (IPBuffer  <= (Items.Count - 1)); IPBuffer++) {
                        if ( Items[IPBuffer].IsInitialOK == true) {
                            for (PortBuffer = 0; PortBuffer  <= (Items[IPBuffer].PortPerCard - 1); PortBuffer++) {
                                bytDoData = 0;
                                for (PointBuffer = 0; (PointBuffer <= (Items[IPBuffer].BitsPerPort - 1)); PointBuffer++) {
                                    if (RawValue[IPBuffer, PortBuffer, PointBuffer] == true) {
                                        //Debug.Print("PointBuffer:" + PointBuffer + "  value:" + (1 << PointBuffer) + "    ");
                                        bytDoData = Convert.ToByte(bytDoData | (1 << PointBuffer));
                                        // TODO: Warning!!! The operator should be an XOR ^ instead of an OR, but not available in CodeDOM
                                    }
                                }
                                Items[IPBuffer].Write(PortBuffer, ref bytDoData);
                            }
        
                        }
    
                    }
                    return true;
                }

            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1005003), "Error_1005003", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                return false;
            }
        }



        /// <summary> 讀取DO設定檔 </summary>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        public bool Load(string strFileName)
        {
            try
            {
                int mBitNo = 0;
                string strSection = null;
                string[] str_DOAddressBuffer = null;

                strSection = "Configuration";
                MDateLog.gSyslog.Save("DO Parameter Loaded as below:");

                //讀取
                for (mBitNo = 0; mBitNo <= mTotalBits - 1; mBitNo++)
                {
                    //[Note]:Save Full Name(全語系)，Name是語系切換完之後的內容
                    string mName = CIni.ReadIniString(strSection, "DO-" + mBitNo + "-Name", strFileName, "");
                    string[] mSplitedName = mName.Split(new string[] { "," }, StringSplitOptions.None);
                    switch (ProjectCore.MSystemParameter.gSSystemParameter.LanguageType)
                    {
                        case ProjectCore.MSystemParameter.enmLanguageType.eEnglish:
                            if (mSplitedName.GetUpperBound(0) >= 0)
                            {
                                DOParameter[mBitNo].Name = mSplitedName[0];
                            }
                            break;
                        case ProjectCore.MSystemParameter.enmLanguageType.eTraditionalChinese:
                            if (mSplitedName.GetUpperBound(0) >= 1)
                            {
                                DOParameter[mBitNo].Name = mSplitedName[1];
                            }
                            break;
                        case ProjectCore.MSystemParameter.enmLanguageType.eSimplifiedChinese:
                            if (mSplitedName.GetUpperBound(0) >= 2)
                            {
                                DOParameter[mBitNo].Name = mSplitedName[2];
                            }
                            break;
                    }

                    DOParameter[mBitNo].FullName = mName;
                    Enum.TryParse<Premtek.enmDOCardType>(CIni.ReadIniString(strSection, "DO-" + mBitNo + "-CardType", strFileName, ((int)enmDOCardType.PCI_1758).ToString()), out DOParameter[mBitNo].CardType);
                    DOParameter[mBitNo].Address = CIni.ReadIniString(strSection, "DO-" + mBitNo + "-Address", strFileName, "");
                    int mByPass, mToggle, InitialValue;
                    int.TryParse(CIni.ReadIniString(strSection, "DO-" + mBitNo + "-ByPass", strFileName, "0"), out mByPass);
                    int.TryParse(CIni.ReadIniString(strSection, "DO-" + mBitNo + "-Toggle", strFileName, "0"), out mToggle);
                    int.TryParse(CIni.ReadIniString(strSection, "DO-" + mBitNo + "-InitialValue", strFileName, "0"), out InitialValue);
                    DOParameter[mBitNo].ByPass = Convert.ToBoolean(mByPass);
                    DOParameter[mBitNo].Toggle = Convert.ToBoolean(mToggle);
                    DOParameter[mBitNo].InitialValue = Convert.ToBoolean(InitialValue);

                    if (DOParameter[mBitNo].Address.Contains("-"))
                    {
                        str_DOAddressBuffer = DOParameter[mBitNo].Address.Split(new string[] { "-" }, StringSplitOptions.None);
                        long.TryParse((str_DOAddressBuffer[0]), out DOParameter[mBitNo].IPAddress);
                        DOParameter[mBitNo].Port = Convert.ToInt16(str_DOAddressBuffer[1].Substring(0, 2), 16) / 8;
                        DOParameter[mBitNo].Bits = Convert.ToInt64(str_DOAddressBuffer[1].Substring(0, 2), 16) % 8;
                    }
                    MDateLog.gSyslog.Save("DO-" + mBitNo.ToString("000") + " Address: " + DOParameter[mBitNo].Address + " CardType: " + DOParameter[mBitNo].CardType.ToString() + " ByPass: " + (DOParameter[mBitNo].ByPass ? 1 : 0) + " Toggle: " + (DOParameter[mBitNo].Toggle ? 1 : 0) + " Name: " + DOParameter[mBitNo].Name);
                }
                return true;

            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1002018), "Error_1002018", eMessageLevel.Error);
                System.Windows.Forms.MessageBox.Show("Excepption Message: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 儲存DO設定檔
        /// </summary>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        public bool Save(string strFileName)
        {

            try
            {
                int mBitNo = 0;
                string strSection = null;

                strSection = "Configuration";
                MDateLog.gSyslog.Save("DO Parameter Saved as below:");
                for (mBitNo = 0; mBitNo <= mTotalBits - 1; mBitNo++)
                {
                    //[Note]:Save Full Name(全語系)，Name是語系切換完之後的內容
                    CIni.SaveIniString(strSection, "DO-" + mBitNo + "-Name", DOParameter[mBitNo].FullName, strFileName);
                    CIni.SaveIniString(strSection, "DO-" + mBitNo + "-CardType", Convert.ToInt32(DOParameter[mBitNo].CardType).ToString(), strFileName);
                    CIni.SaveIniString(strSection, "DO-" + mBitNo + "-Address", DOParameter[mBitNo].Address, strFileName);
                    CIni.SaveIniString(strSection, "DO-" + mBitNo + "-ByPass", Convert.ToInt32(DOParameter[mBitNo].ByPass).ToString(), strFileName);
                    CIni.SaveIniString(strSection, "DO-" + mBitNo + "-Toggle", Convert.ToInt32(DOParameter[mBitNo].Toggle).ToString(), strFileName);
                    CIni.SaveIniString(strSection, "DO-" + mBitNo + "-InitialValue", Convert.ToInt32(DOParameter[mBitNo].InitialValue).ToString(), strFileName);
                    MDateLog.gSyslog.Save("DO-" + mBitNo.ToString("000") + " Address: " + DOParameter[mBitNo].Address + " CardType: " + DOParameter[mBitNo].CardType.ToString() + " ByPass: " + (DOParameter[mBitNo].ByPass ? 1 : 0) + " Toggle: " + (DOParameter[mBitNo].Toggle ? 1 : 0) + " Name: " + DOParameter[mBitNo].Name);
                }
                return true;

            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1002017), "Error_1002017", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Excepption Message: " + ex.Message, "", eMessageLevel.Error);
                return false;
            }
        }

        public bool ReSetDO()
        {
            int mBitNo = 0;
            try
            {
                for (mBitNo = 0; mBitNo <= mTotalBits - 1; mBitNo++)
                {
                    if(DOParameter[mBitNo].ByPassInitialValue == false) 
                    {
                        mblnDOOutput[mBitNo] = DOParameter[mBitNo].InitialValue;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
                return false;
            }
        }




    

    }



}


/// <summary>DO點位參數</summary>
/// <remarks></remarks>
public struct sDOParameter
{
    /// <summary>點位名稱(所有語系)</summary>
    /// <remarks></remarks>
    public string FullName;
    /// <summary>點位名稱</summary>
    /// <remarks></remarks>
    public string Name;
    /// <summary>型號</summary>
    /// <remarks></remarks>
    public enmDOCardType CardType;
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
    /// <summary> 初始值 </summary>
    public bool InitialValue;

    public bool ByPassInitialValue;
}

