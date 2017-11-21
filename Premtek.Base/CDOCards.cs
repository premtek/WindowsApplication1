using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectCore;

namespace Premtek
{

    /// <summary>
    /// DO卡型號
    /// </summary>
    /// 
    public enum enmDOCardType
    {
        /// <summary>無</summary>
        /// <remarks></remarks>
        None = 0,
        /// <summary>32點數位輸入</summary>
        /// <remarks></remarks>
        PCI_1756 = 1,
        /// <summary>16點數位輸入</summary>
        /// <remarks></remarks>
        PCI_1710 = 2,
        /// <summary>64點數位輸入</summary>
        /// <remarks></remarks>
        PCI_1758 = 3
    }
    /// <summary>
    /// DO卡參數
    /// </summary>
    public struct sDOCardParameter
    {
        /// <summary> 卡型號 </summary>
        public enmDOCardType CardType;
        /// <summary> 敘述 </summary>
        public string DeviceDescreiption;
        /// <summary> 卡號 </summary>
        public int CardID;

        public bool Load(int cardNo, string fileName)
        {
            string strSection = null;
            strSection = "Configuration";
            int.TryParse(CIni.ReadIniString(strSection, "DOCard-" + cardNo + "-CardID", fileName, ""), out CardID);
            Enum.TryParse<enmDOCardType>(CIni.ReadIniString(strSection, "DOCard-" + cardNo + "-CardType", fileName, ((int)enmDOCardType.PCI_1756).ToString()), out CardType);
            DeviceDescreiption = CIni.ReadIniString(strSection, "DOCard-" + cardNo + "-Description", fileName, "");
            MDateLog.gSyslog.Save("DO-Card" + cardNo + ":\t" + "CardID: " + CardID + " CardType: " + CardType.ToString() + " Desc: " + DeviceDescreiption);
            return true;
        }

        public bool Save(int cardNo, string fileName)
        {
            string strSection = null;
            strSection = "Configuration";
            CIni.SaveIniString(strSection, "DOCard-" + cardNo + "-CardID", CardID.ToString(), fileName);
            CIni.SaveIniString(strSection, "DOCard-" + cardNo + "-CardType", ((int)CardType).ToString(), fileName);
            CIni.SaveIniString(strSection, "DOCard-" + cardNo + "-Description", DeviceDescreiption, fileName);
            return true;
        }

    }
    public class CDOCards
    {
        /// <summary> DO卡數量 </summary>
        int DOCardCount;
        /// <summary>實際使用DO接點數量</summary>
        /// <remarks></remarks>
        int DOChannelCount = 32;
        /// <summary>DO卡參數</summary>
        /// <remarks></remarks>
        public List<sDOCardParameter> DOCardParameter = new List<sDOCardParameter>();

        public bool Load(string strFileName)
        {
            string strSection = null;
            strSection = "Configuration";
            DOCardCount = Convert.ToInt32(CIni.ReadIniString(strSection, "DOCardCount", strFileName, "1"));
            DOChannelCount = Convert.ToInt32(CIni.ReadIniString(strSection, "DOChannelCount", strFileName, "32"));
            DOCardParameter.Clear();
            MDateLog.gSyslog.Save("DO-Card:" + DOCardCount + " PCS Channel: " + DOChannelCount);

            //[說明]:對每一張DO卡,讀取參數
            for (int mCardNo = 0; mCardNo <= DOCardCount - 1; mCardNo++)
            {
                sDOCardParameter mCard = new sDOCardParameter();
                mCard.Load(mCardNo, strFileName);
                DOCardParameter.Add(mCard);
            }

            return true;
        }

        public bool Save(string strFileName)
        {
            string strSection = null;
            strSection = "Configuration";
            CIni.SaveIniString(strSection, "DOCardCount", DOCardCount.ToString(), strFileName);
            CIni.SaveIniString(strSection, "DOChannelCount", DOChannelCount.ToString(), strFileName);
            //[說明]:對每一張DO卡,儲存參數
            for (int mCardNo = 0; mCardNo <= DOCardCount - 1; mCardNo++)
            {
                DOCardParameter[mCardNo].Save(mCardNo, strFileName);
            }
            return true;
        }

    }
}
