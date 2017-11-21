using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectCore;

namespace Premtek
{

    /// <summary>DI卡型號</summary>
    /// <remarks></remarks>
    public enum enmDICardType
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

    /// <summary>DI卡參數</summary>
    /// <remarks></remarks>
    public struct sDICardParameter
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
            int.TryParse(CIni.ReadIniString(strSection, "DICard-" + cardNo + "-CardID", fileName, ""), out CardID);
            Enum.TryParse<enmDICardType>(CIni.ReadIniString(strSection, "DICard-" + cardNo + "-CardType", fileName, ((int)enmDICardType.PCI_1756).ToString()), out CardType);
            DeviceDescreiption = CIni.ReadIniString(strSection, "DICard-" + cardNo + "-Description", fileName, "");
           MDateLog. gSyslog.Save("DI-Card" + cardNo + ":\t"  + "CardID: " + CardID + " CardType: " + CardType.ToString() + " Desc: " + DeviceDescreiption);
            return true;
        }
        public bool Save(int cardNo, string fileName)
        {
            string strSection = null;
            strSection = "Configuration";
            CIni.SaveIniString(strSection, "DICard-" + cardNo + "-CardID", CardID.ToString(), fileName);
            CIni.SaveIniString(strSection, "DICard-" + cardNo + "-CardType", ((int)CardType).ToString(), fileName);
            CIni.SaveIniString(strSection, "DICard-" + cardNo + "-Description", DeviceDescreiption, fileName);
            return true;
        }

    }

    public class CDICards
    {
        /// <summary>DI卡數量</summary>
        /// <remarks></remarks>
        int DICardCount;
        /// <summary>實際使用DI接點數量</summary>
        /// <remarks></remarks>
        int DIChannelCount = 32;
        /// <summary>DI卡參數</summary>
        /// <remarks></remarks>
        public List<sDICardParameter> DICardParameter = new List<sDICardParameter>();
        public bool Load(string strFileName)
        {
            string strSection = null;
            strSection = "Configuration";
            DICardCount = Convert.ToInt32(CIni.ReadIniString(strSection, "DICardCount", strFileName, "1"));
            DIChannelCount = Convert.ToInt32(CIni.ReadIniString(strSection, "DIChannelCount", strFileName, "32"));
            DICardParameter.Clear();
            MDateLog.gSyslog.Save("DI-Card:" + DICardCount + " PCS Channel: " + DIChannelCount);

            //[說明]:對每一張DI卡,讀取參數
            for (int mCardNo = 0; mCardNo <= DICardCount - 1; mCardNo++)
            {
                sDICardParameter mCard = new sDICardParameter();
                mCard.Load(mCardNo, strFileName);
                DICardParameter.Add(mCard);
            }

            return true;
        }
        public bool Save(string strFileName)
        {
            string strSection = null;
            strSection = "Configuration";
           CIni.SaveIniString(strSection, "DICardCount", DICardCount.ToString(), strFileName);
           CIni.SaveIniString(strSection, "DIChannelCount", DIChannelCount.ToString(), strFileName);
            //[說明]:對每一張DI卡,儲存參數
            for (int mCardNo = 0; mCardNo <= DICardCount - 1; mCardNo++)
            {
                DICardParameter[mCardNo].Save(mCardNo, strFileName);
            }
            return true;
        }

    }

}



//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
