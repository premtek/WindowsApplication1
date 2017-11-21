using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ProjectCore;
using System.Windows.Forms;

namespace Premtek.Base
{

    /// <summary>
    /// Tilt Z Height高度校正
    /// </summary>
    /// <remarks></remarks>
    public struct SLaserTiltValveCalibration
    {

        /// <summary>[閥頭測Pin高位置X]</summary>
        /// <remarks></remarks>
        public decimal ValvePinPosX;
        /// <summary>[閥頭測Pin高位置Y]</summary>
        /// <remarks></remarks>
        public decimal ValvePinPosY;
        /// <summary>[閥頭測Pin高位置Z]</summary>
        /// <remarks></remarks>
        public decimal ValvePinPosZ;
        /// <summary>[第一支膠針高度(相對於Latch)之測高找尋上限值]</summary>
        /// <remarks></remarks>
        public decimal ValvePinLimitZ;
        /// <summary>雷射測Pin高位置X</summary>
        /// <remarks></remarks>
        public decimal LaserPinPosX;
        /// <summary>雷射測Pin高位置Y</summary>
        /// <remarks></remarks>
        public decimal LaserPinPosY;
        /// <summary>雷射測Pin高位置Z</summary>
        /// <remarks></remarks>
        public decimal LaserPinPosZ;
        /// <summary>雷射測Pin高讀值</summary>
        /// <remarks></remarks>

        public decimal LaserPinValue;
    }
    public class CLaserTiltValveCalibration
    {
        /// <summary>外部接入系統參數
        /// </summary>
        public int StageUseValveCount = 1;
        private Dictionary<decimal, SLaserTiltValveCalibration>[] DicLaserTiltValve = new Dictionary<decimal, SLaserTiltValveCalibration>[(int)enmValve.Max + 1];

        //Set(ByVal value As Dictionary(Of Decimal, SLaserTiltValveCalibration))
        //    DicLaserTiltValve = value
        //End Set

        //Public DicLaserTiltValve(enmValve.Max) As Dictionary(Of Decimal, SLaserTiltValveCalibration)
        public CLaserTiltValveCalibration()
        {
            for (int index = 0; index <= (int)enmValve.Max; index++)
            {
                DicLaserTiltValve[index] = new Dictionary<decimal, SLaserTiltValveCalibration>();

            }
        }
        /// <summary>
        /// 加入新項目
        /// </summary>
        /// <param name="eValve"></param>
        /// <param name="angle"></param>
        /// <param name="sLaserTiltvalve"></param>
        /// <remarks></remarks>
        public void AddLaserTiltValve(int eValve, decimal angle, SLaserTiltValveCalibration sLaserTiltvalve)
        {
            if (DicLaserTiltValve[eValve].ContainsKey(angle) == false)
            {
                DicLaserTiltValve[eValve].Add(angle, sLaserTiltvalve);
            }
            else
            {
                DicLaserTiltValve[eValve][angle] = sLaserTiltvalve;
            }
        }
        /// <summary>
        /// 移除項目
        /// </summary>
        /// <param name="eValve"></param>
        /// <param name="angle"></param>
        /// <remarks></remarks>
        //, sLaserTiltvalve As SLaserTiltValveCalibration)
        public void ReMoveLaserTiltValve(int eValve, decimal angle)
        {
            if (DicLaserTiltValve[eValve].ContainsKey(angle) == true)
            {
                DicLaserTiltValve[eValve].Remove(angle);
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
            foreach (KeyValuePair<decimal, SLaserTiltValveCalibration> pair in DicLaserTiltValve[eValve])
            {
                lstKeyName.Add(pair.Key);
            }
            return lstKeyName;
        }
        #region "Laser 與 Vavle Offset"
        /// <summary> 
        /// [平台內Laser與閥頭修正量Z:(LaserPinPosZ-LaserPinValue)-ValvePinPosZ]
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal LaserTiltValveOffsetZ(int eValve, decimal angle)
        {
            
                SLaserTiltValveCalibration TiltTemp = new SLaserTiltValveCalibration();
                TiltTemp = DicLaserTiltValve[eValve][angle];
                //Return TiltTemp.LaserPinPosZ - TiltTemp.ValvePinLimitZ

                //[Note]:(LaserPinPosZ-LaserPinValue)-ValvePinPosZ
                return (TiltTemp.LaserPinPosZ - TiltTemp.LaserPinValue) - TiltTemp.ValvePinPosZ;


        }
        #endregion




        public decimal GetValvePinX(int eValve, decimal angle)
        {

            SLaserTiltValveCalibration TiltTemp = new SLaserTiltValveCalibration();
            if (DicLaserTiltValve[eValve].ContainsKey(angle) == true)
            {
                TiltTemp = DicLaserTiltValve[eValve][angle];
            }
            else
            {
                return 0;
            }
            return TiltTemp.ValvePinPosX;

        }
        public void SetValvePinX(int eValve, decimal angle, decimal value)
        {
            SLaserTiltValveCalibration TiltTemp = new SLaserTiltValveCalibration();
            if (DicLaserTiltValve[eValve].ContainsKey(angle) == true)
            {
                TiltTemp = DicLaserTiltValve[eValve][angle];
                TiltTemp.ValvePinPosY = value;
                AddLaserTiltValve(eValve, angle, TiltTemp);
            }
        }
        public decimal GetValvePinY(int eValve, decimal angle)
        {
            SLaserTiltValveCalibration TiltTemp = new SLaserTiltValveCalibration();
            if (DicLaserTiltValve[eValve].ContainsKey(angle) == true)
            {
                TiltTemp = DicLaserTiltValve[eValve][angle];
            }
            else
            {
                return 0;
            }
            return TiltTemp.ValvePinPosY;
        }
        public void SetValvePinY(int eValve, decimal angle, decimal value)
        {

            SLaserTiltValveCalibration TiltTemp = new SLaserTiltValveCalibration();
            if (DicLaserTiltValve[eValve].ContainsKey(angle) == true)
            {
                TiltTemp = DicLaserTiltValve[eValve][angle];
                TiltTemp.ValvePinPosY = value;
                AddLaserTiltValve(eValve, angle, TiltTemp);
            }

        }
        public decimal GetValvePinZ(int eValve, decimal angle)
        {
            SLaserTiltValveCalibration TiltTemp = new SLaserTiltValveCalibration();
            if (DicLaserTiltValve[eValve].ContainsKey(angle) == true)
            {
                TiltTemp = DicLaserTiltValve[eValve][angle];
            }
            else
            {
                return 0;
            }
            return TiltTemp.ValvePinPosZ;
        }
        public void SetValvePinZ(int eValve, decimal angle, decimal value)
        {

            SLaserTiltValveCalibration TiltTemp = new SLaserTiltValveCalibration();
            if (DicLaserTiltValve[eValve].ContainsKey(angle) == true)
            {
                TiltTemp = DicLaserTiltValve[eValve][angle];
                TiltTemp.ValvePinPosZ = value;
                AddLaserTiltValve(eValve, angle, TiltTemp);

            }

        }
        public decimal GetLaserPinX(int eValve, decimal angle)
        {
            SLaserTiltValveCalibration TiltTemp = new SLaserTiltValveCalibration();
            if (DicLaserTiltValve[eValve].ContainsKey(angle) == true)
            {
                TiltTemp = DicLaserTiltValve[eValve][angle];
            }
            else
            {
                return 0;
            }
            return TiltTemp.LaserPinPosX;
        }
        public void SetLaserPinX(int eValve, decimal angle, decimal value)
        {

            SLaserTiltValveCalibration TiltTemp = new SLaserTiltValveCalibration();
            if (DicLaserTiltValve[eValve].ContainsKey(angle) == true)
            {
                TiltTemp = DicLaserTiltValve[eValve][angle];
                TiltTemp.ValvePinPosZ = value;
                AddLaserTiltValve(eValve, angle, TiltTemp);
            }

        }

        public decimal GetLaserPinY(int eValve, decimal angle)
        {
            SLaserTiltValveCalibration TiltTemp = new SLaserTiltValveCalibration();
            if (DicLaserTiltValve[eValve].ContainsKey(angle) == true)
            {
                TiltTemp = DicLaserTiltValve[eValve][angle];
            }
            else
            {
                return 0;
            }
            return TiltTemp.LaserPinPosY;
        }
        public void SetLaserPinY(int eValve, decimal angle, decimal value)
        {
            SLaserTiltValveCalibration TiltTemp = new SLaserTiltValveCalibration();
            if (DicLaserTiltValve[eValve].ContainsKey(angle) == true)
            {
                TiltTemp = DicLaserTiltValve[eValve][angle];
                TiltTemp.ValvePinPosZ = value;
                AddLaserTiltValve(eValve, angle, TiltTemp);
            }
        }
        public decimal GetLaserPinZ(int eValve, decimal angle)
        {
            SLaserTiltValveCalibration TiltTemp = new SLaserTiltValveCalibration();
            if (DicLaserTiltValve[eValve].ContainsKey(angle) == true)
            {
                TiltTemp = DicLaserTiltValve[eValve][angle];
            }
            else
            {
                return 0;
            }
            return TiltTemp.LaserPinPosZ;
        }

        public void SetLaserPinZ(int eValve, decimal angle, decimal value)
        {

            SLaserTiltValveCalibration TiltTemp = new SLaserTiltValveCalibration();
            if (DicLaserTiltValve[eValve].ContainsKey(angle) == true)
            {
                TiltTemp = DicLaserTiltValve[eValve][angle];
                TiltTemp.ValvePinPosZ = value;
                AddLaserTiltValve(eValve, angle, TiltTemp);
            }

        }

        public decimal GetLaserPinLimitZ(int eValve, decimal angle)
        {
            SLaserTiltValveCalibration TiltTemp = new SLaserTiltValveCalibration();
            if (DicLaserTiltValve[eValve].ContainsKey(angle) == true)
            {
                TiltTemp = DicLaserTiltValve[eValve][angle];
            }
            else
            {
                return 0;
            }
            return TiltTemp.ValvePinLimitZ;
        }

        public void SetLaserPinLimitZ(int eValve, decimal angle, decimal value)
        {

            SLaserTiltValveCalibration TiltTemp = new SLaserTiltValveCalibration();
            if (DicLaserTiltValve[eValve].ContainsKey(angle) == true)
            {
                TiltTemp = DicLaserTiltValve[eValve][angle];
                TiltTemp.ValvePinPosZ = value;
                AddLaserTiltValve(eValve, angle, TiltTemp);
            }

        }


        /// <summary>儲存校正檔</summary>
        /// <param name="fileName"></param>
        /// <remarks></remarks>
        public void SaveCalibrationValve(string fileName)
        {
            try
            {
                string strSection = null;
                int iCount = 0;

                for (int mValveNo = (int)enmValve.No1; mValveNo <= StageUseValveCount - 1; mValveNo++)
                {
                    foreach (KeyValuePair<decimal, SLaserTiltValveCalibration> pair in DicLaserTiltValve[mValveNo])
                    {
                        strSection = "LaserValve" + (mValveNo + 1).ToString() + "_TiltWorkAngle_" + pair.Key.ToString();
                        SLaserTiltValveCalibration sTemp1 = pair.Value;
                        CIni.SaveIniString(strSection, "ValvePinPosX_" + iCount.ToString(), sTemp1.ValvePinPosX, fileName);
                        CIni.SaveIniString(strSection, "ValvePinPosY_" + iCount.ToString(), sTemp1.ValvePinPosY, fileName);
                        CIni.SaveIniString(strSection, "ValvePinPosZ_" + iCount.ToString(), sTemp1.ValvePinPosZ, fileName);
                        CIni.SaveIniString(strSection, "ValvePinLimitZ_" + iCount.ToString(), sTemp1.ValvePinLimitZ, fileName);
                        CIni.SaveIniString(strSection, "LaserPinPosX_" + iCount.ToString(), sTemp1.LaserPinPosX, fileName);
                        CIni.SaveIniString(strSection, "LaserPinPosY_" + iCount.ToString(), sTemp1.LaserPinPosY, fileName);
                        CIni.SaveIniString(strSection, "LaserPinPosZ_" + iCount.ToString(), sTemp1.LaserPinPosZ, fileName);
                        CIni.SaveIniString(strSection, "LaserPinValue_" + iCount.ToString(), sTemp1.LaserPinValue, fileName);
                        CIni.SaveIniString("LaserValve" + (mValveNo + 1).ToString() + "_Key", "KeyName" + iCount.ToString(), pair.Key.ToString(), fileName);
                        iCount = iCount + 1;
                    }
                    CIni.SaveIniString("LaserValve" + (mValveNo + 1).ToString() + "_Key", "KeyCount", iCount, fileName);
                    iCount = 0;
                }

            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1002029), "Error_1002029", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                MessageBox.Show(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1002029) + ex.Message);
            }
        }

        /// <summary>讀取校正檔-平台內所有閥</summary>
        /// <param name="fileName"></param>
        /// <remarks></remarks>
        public void Load(string fileName)
        {
            for (int mValveNo = (int)enmValve.No1; mValveNo <= StageUseValveCount - 1; mValveNo++)
            {
                DicLaserTiltValve[mValveNo].Clear();
            }

            try
            {
                SLaserTiltValveCalibration sTemp1 = new SLaserTiltValveCalibration();
                string kTemp = "";
                int iTemp = 0;

                for (int mValveNo = (int)enmValve.No1; mValveNo <= StageUseValveCount - 1; mValveNo++)
                {
                    sTemp1 = new SLaserTiltValveCalibration();
                    iTemp = Convert.ToInt32(CIni.ReadIniString("LaserValve" + (mValveNo + 1).ToString() + "_Key", "KeyCount", fileName, 1));
                    for (int index = 0; index <= iTemp - 1; index++)
                    {
                        kTemp = CIni.ReadIniString("LaserValve" + (mValveNo + 1).ToString() + "_Key", "KeyName" + index.ToString(), fileName, 0);
                        sTemp1.ValvePinPosX = Convert.ToDecimal(CIni.ReadIniString("LaserValve" + mValveNo + 1 + "_TiltWorkAngle_" + kTemp, "ValvePinPosX_" + index.ToString(), fileName, 0));
                        sTemp1.ValvePinPosY = Convert.ToDecimal(CIni.ReadIniString("LaserValve" + mValveNo + 1 + "_TiltWorkAngle_" + kTemp, "ValvePinPosY_" + index.ToString(), fileName, 0));
                        sTemp1.ValvePinPosZ = Convert.ToDecimal(CIni.ReadIniString("LaserValve" + mValveNo + 1 + "_TiltWorkAngle_" + kTemp, "ValvePinPosZ_" + index.ToString(), fileName, 0));
                        sTemp1.ValvePinLimitZ = Convert.ToDecimal(CIni.ReadIniString("LaserValve" + mValveNo + 1 + "_TiltWorkAngle_" + kTemp, "ValvePinLimitZ_" + index.ToString(), fileName, 0));
                        sTemp1.LaserPinPosX = Convert.ToDecimal(CIni.ReadIniString("LaserValve" + mValveNo + 1 + "_TiltWorkAngle_" + kTemp, "LaserPinPosX_" + index.ToString(), fileName, 0));
                        sTemp1.LaserPinPosY = Convert.ToDecimal(CIni.ReadIniString("LaserValve" + mValveNo + 1 + "_TiltWorkAngle_" + kTemp, "LaserPinPosY_" + index.ToString(), fileName, 0));
                        sTemp1.LaserPinPosZ = Convert.ToDecimal(CIni.ReadIniString("LaserValve" + mValveNo + 1 + "_TiltWorkAngle_" + kTemp, "LaserPinPosZ_" + index.ToString(), fileName, 0));
                        sTemp1.LaserPinValue = Convert.ToDecimal(CIni.ReadIniString("LaserValve" + mValveNo + 1 + "_TiltWorkAngle_" + kTemp, "LaserPinValue_" + index.ToString(), fileName, 0));
                        DicLaserTiltValve[mValveNo].Add(Convert.ToDecimal(kTemp), sTemp1);
                    }
                }

            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1002028), "Error_1002028", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                MessageBox.Show(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1002028) + ex.Message);
            }
        }


    }
}
