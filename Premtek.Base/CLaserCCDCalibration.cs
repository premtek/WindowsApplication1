using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ProjectCore;

namespace Premtek.Base
{

    /// <summary>
    /// "Laser CCD 校正"
    /// </summary>
    /// <remarks></remarks>
    public struct SLserCCDCalibration
    {
  

        /// <summary>CCD測平台校正X(for Laser)</summary>
        /// <remarks></remarks>
        public decimal CCDCalibPosX;
        /// <summary>CCD測平台校正Y(for Laser)</summary>
        /// <remarks></remarks>
        public decimal CCDCalibPosY;
        /// <summary>CCD測平台校正Z(for Laser)</summary>
        /// <remarks></remarks>
        public decimal CCDCalibPosZ;
        /// <summary>[雷射測平台X]</summary>
        /// <remarks></remarks>
        public decimal LaserCalibPosX;
        /// <summary>[雷射測平台Y]</summary>
        /// <remarks></remarks>
        public decimal LaserCalibPosY;
        /// <summary>[雷射測平台Z]</summary>
        /// <remarks></remarks>
        public decimal LaserCalibPosZ;
    }

    public class CLaserCCDCalibration
    {
        private Dictionary<decimal, SLserCCDCalibration>[] DicLaserCCD = new Dictionary<decimal, SLserCCDCalibration>[(int)enmValve.Max + 1];
        /// <summary>外部接入系統參數
        /// </summary>
        public int StageUseValveCount = 1;
        //Set(ByVal value)
        //    mDicLaserCCD(enmValve.Max) = value
        //End Set

        //Public DicLaserCCD(enmValve.Max) As Dictionary(Of Decimal, SLserCCDCalibration)

        /// <summary>
        /// [平台內CCD與 Laser修正量X:(CCDCalibPosY-LaserCalibPosY]
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal LaserCCDOffsetX(int eValve, decimal angle)
        {
            SLserCCDCalibration TiltTemp = new SLserCCDCalibration();
            TiltTemp = DicLaserCCD[eValve][angle];
            return TiltTemp.CCDCalibPosX - TiltTemp.LaserCalibPosX;

        }

        /// <summary>
        /// [平台內CCD與 Laser修正量Y:(CCDCalibPosY-LaserCalibPosY]
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal LaserCCDOffsetY(int eValve, decimal angle)
        {

            SLserCCDCalibration TiltTemp = new SLserCCDCalibration();
            TiltTemp = DicLaserCCD[eValve][angle];
            return TiltTemp.CCDCalibPosY - TiltTemp.LaserCalibPosY;

        }


        /// <summary>
        /// [平台內CCD與 Laser修正量Z:(CCDCalibPosZ-LaserCalibPosZ)-ValvePinPosZ] 重點是在新竹可能
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal LaserCCDOffsetZ(int eValve, decimal angle)
        {
            SLserCCDCalibration TiltTemp = new SLserCCDCalibration();
            TiltTemp = DicLaserCCD[eValve][angle];
            return TiltTemp.CCDCalibPosZ - TiltTemp.LaserCalibPosZ;
        }
        public CLaserCCDCalibration()
        {
            for (int index = 0; index <= (int)enmValve.Max; index++)
            {
                DicLaserCCD[index] = new Dictionary<decimal, SLserCCDCalibration>();
            }
        }
        /// <summary>
        /// 加入新項目
        /// </summary>
        /// <param name="eValve"></param>
        /// <param name="angle"></param>
        /// <param name="sLaserCCD"></param>
        /// <remarks></remarks>
        public void ADDLaserCCD(int eValve, decimal angle, SLserCCDCalibration sLaserCCD)
        {
            if (DicLaserCCD[eValve].ContainsKey(angle) == false)
            {
                DicLaserCCD[eValve].Add(angle, sLaserCCD);
            }
            else
            {
                DicLaserCCD[eValve][angle] = sLaserCCD;
            }
        }

        /// <summary>
        /// 移除項目
        /// </summary>
        /// <param name="eValve"></param>
        /// <param name="angle"></param>
        /// <param name="sLaserTiltvalve"></param>
        /// <remarks></remarks>
        public void ReMoveLaserCCD(int eValve, decimal angle, SLserCCDCalibration sLaserTiltvalve)
        {
            if (DicLaserCCD[eValve].ContainsKey(angle) == true)
            {
                DicLaserCCD[eValve].Remove(angle);
            }
        }

        /// <summary>
        /// 取得對應閥裡的所有key值
        /// </summary>
        /// <param name="eValve"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<decimal> GetAllKeyName(int eValve)
        {
            List<decimal> lstKeyName = new List<decimal>();
            foreach (KeyValuePair<decimal, SLserCCDCalibration> pair in DicLaserCCD[eValve])
            {
                lstKeyName.Add(pair.Key);
            }
            return lstKeyName;
        }

        /// <summary>儲存校正檔</summary>
        /// <param name="fileName"></param>
        /// <remarks></remarks>
        public void Save(string fileName)
        {
            try
            {
                string strSection = null;
                int iCount = 0;

                for (int mValveNo = (int)enmValve.No1; mValveNo <= StageUseValveCount - 1; mValveNo++)
                {
                    foreach (KeyValuePair<decimal, SLserCCDCalibration> pair in DicLaserCCD[mValveNo])
                    {
                        strSection = "Valve" + (mValveNo + 1).ToString() + "_LaserCCD_" + pair.Key.ToString();
                        SLserCCDCalibration sTemp1 = pair.Value;
                        CIni.SaveIniString(strSection, "CCDCalibPosX_" + iCount.ToString(), sTemp1.CCDCalibPosX, fileName);
                        CIni.SaveIniString(strSection, "CCDCalibPosY_" + iCount.ToString(), sTemp1.CCDCalibPosY, fileName);
                        CIni.SaveIniString(strSection, "CCDCalibPosZ_" + iCount.ToString(), sTemp1.CCDCalibPosZ, fileName);
                        CIni.SaveIniString(strSection, "LaserCalibPosX_" + iCount.ToString(), sTemp1.LaserCalibPosX, fileName);
                        CIni.SaveIniString(strSection, "LaserCalibPosY_" + iCount.ToString(), sTemp1.LaserCalibPosY, fileName);
                        CIni.SaveIniString(strSection, "LaserCalibPosZ_" + iCount.ToString(), sTemp1.LaserCalibPosZ, fileName);
                        CIni.SaveIniString("Valve" + mValveNo.ToString() + 1 + "_Key", "KeyName" + iCount.ToString(), pair.Key.ToString(), fileName);
                        iCount = iCount + 1;
                    }
                    CIni.SaveIniString("Valve" + (mValveNo + 1).ToString() + "_Key", "KeyCount", iCount, fileName);
                    iCount = 0;
                }

            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1002029), "Error_1002029", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                System.Windows.Forms.MessageBox.Show(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1002029) + ex.Message);

            }
        }

        /// <summary>讀取校正檔-平台內所有閥</summary>
        /// <param name="fileName"></param>
        /// <remarks></remarks>
        public void Load(string fileName)
        {
            for (int mValveNo = (int)enmValve.No1; mValveNo <= StageUseValveCount - 1; mValveNo++)
            {
                DicLaserCCD[mValveNo].Clear();
            }

            try
            {
                SLserCCDCalibration sTemp1 = new SLserCCDCalibration();
                string kTemp = "";
                int iTemp = 0;

                for (int mValveNo = (int)enmValve.No1; mValveNo <= StageUseValveCount - 1; mValveNo++)
                {
                    sTemp1 = new SLserCCDCalibration();
                    iTemp =Convert.ToInt32( CIni.ReadIniString("Valve" + (mValveNo + 1).ToString() + "_Key", "KeyCount", fileName, 0));

                    for (int index = 0; index <= iTemp - 1; index++)
                    {
                        kTemp = CIni.ReadIniString("Valve1_Key", "KeyName" + index.ToString(), fileName, 0);
                        sTemp1.CCDCalibPosX =Convert.ToDecimal( CIni.ReadIniString("Valve" + mValveNo + 1 + "_LaserCCD_" + kTemp, "CCDCalibPosX" + index.ToString(), fileName, 0));
                        sTemp1.CCDCalibPosY = Convert.ToDecimal(CIni.ReadIniString("Valve" + mValveNo + 1 + "_LaserCCD_" + kTemp, "CCDCalibPosY" + index.ToString(), fileName, 0));
                        sTemp1.CCDCalibPosZ = Convert.ToDecimal( CIni.ReadIniString("Valve" + mValveNo + 1 + "_LaserCCD_" + kTemp, "CCDCalibPosZ" + index.ToString(), fileName, 0));
                        sTemp1.LaserCalibPosX = Convert.ToDecimal( CIni.ReadIniString("Valve" + mValveNo + 1 + "_LaserCCD_" + kTemp, "LaserCalibPosX" + index.ToString(), fileName, 0));
                        sTemp1.LaserCalibPosY = Convert.ToDecimal( CIni.ReadIniString("Valve" + mValveNo + 1 + "_LaserCCD_" + kTemp, "LaserCalibPosY" + index.ToString(), fileName, 0));
                        sTemp1.LaserCalibPosZ = Convert.ToDecimal( CIni.ReadIniString("Valve" + mValveNo + 1 + "_LaserCCD_" + kTemp, "LaserCalibPosZ" + index.ToString(), fileName, 0));
                        DicLaserCCD[mValveNo].Add(Convert.ToDecimal(kTemp), sTemp1);
                    }
                }

            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1002028), "Error_1002028", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                System.Windows.Forms.MessageBox.Show(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1002028) + ex.Message);

            }
        }
    }

}
