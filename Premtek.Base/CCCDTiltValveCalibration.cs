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
    /// "CCD閥 XY校正"
    /// </summary>
    /// <remarks></remarks>
    public struct SCCDTiltValveCalibration
    {
        /// <summary>
        /// [平台內校正第n組閥的CCD拍照定位位置]
        /// </summary> 
        /// <remarks></remarks>
        public decimal CCDCalibPosX;
        /// <summary>
        /// [平台內校正第n組閥的CCD拍照定位位置]
        /// </summary>
        /// <remarks></remarks>
        public decimal CCDCalibPosY;
        /// <summary>
        /// [平台內校正第n組閥的CCD拍照高度]
        /// </summary>
        /// <remarks></remarks>
        public decimal CCDCalibPosZ;
        /// <summary>
        /// [平台內校正第n組閥的點膠實際位置X]
        /// </summary>
        /// <remarks></remarks>
        public decimal ValveCalibPosX;
        /// <summary>
        /// [平台內校正第n組閥的點膠實際位置Y]
        /// </summary>
        /// <remarks></remarks>
        public decimal ValveCalibPosY;
        /// <summary>
        /// [平台內校正第n組閥的點膠實際高度Z]
        /// </summary>
        /// <remarks></remarks>

        public decimal ValveCalibPosZ;
        /// <summary>
        /// [點膠閥至最低點上移多少至點膠位置]
        /// </summary>
        /// <remarks></remarks>

        public decimal ValvePinZHight;
        // ''' <summary>是否以CCD跑點膠/CCD對閥修正量為0</summary>
        // ''' <remarks></remarks>
        //Public IsVideoRun As Boolean 'Soni + 2016.09.20 VideoRun計算條件

        /// <summary>自動校正影像計算結果X</summary>
        /// <remarks></remarks>
        public decimal AutoCalibResultX;
        /// <summary>自動校正影像計算結果Y</summary>
        /// <remarks></remarks>

        public decimal AutoCalibResultY;

        /// <summary>[CCD校正場景名稱]</summary>
        /// <remarks></remarks>
        public string CCDValveCalibrationSceneName;
        /// <summary>[CCD Valve 校正的次數]</summary>
        /// <remarks></remarks>
        public int CCDValveCalibrationCount;
        /// <summary>[CCD Valve 校正後之容許誤差]</summary>
        /// <remarks></remarks>
        public decimal CCDValveCalibrationThreshold;
        /// <summary>自動打點間隔</summary>
        /// <remarks></remarks>

        public decimal Pitch;
        /// <summary>氣壓量</summary>
        /// <remarks></remarks>

        public decimal AirPressure;
        //20170520
        /// <summary>CCD穩定時間</summary>
        /// <remarks></remarks>

        public decimal CCDStableTime;

    }
    public class CCCDTiltValveCalibration
    {
        public Dictionary<decimal, SCCDTiltValveCalibration>[] DicCCDTiltValveCalib = new Dictionary<decimal, SCCDTiltValveCalibration>[(int)enmValve.Max + 1];

        public bool IsVideoRun { get; set; }

        /// <summary>外部接入SystemParameter參數
        /// </summary>
        public int StageUseValveCount;

        // ''' <summary>
        // ''' CCD 與 Valve
        // ''' </summary>
        // ''' <remarks></remarks>
        //  Public DicCCDTiltValveCalib(enmValve.Max) As Dictionary(Of Decimal, SCCDTiltValveCalibration)
        public CCCDTiltValveCalibration()
        {
            for (int index = 0; index <= (int)enmValve.Max; index++)
            {
                DicCCDTiltValveCalib[index] = new Dictionary<decimal, SCCDTiltValveCalibration>();
            }
        }
        /// <summary>
        /// 加入新項目
        /// </summary>
        /// <param name="eValve"></param>
        /// <param name="angle"></param>
        /// <param name="sCCDTiltValve"></param>
        /// <remarks></remarks>
        public void AddCCDTiltCCDValve(enmValve eValve, decimal angle, SCCDTiltValveCalibration sCCDTiltValve)
        {
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle) == false)
            {
                DicCCDTiltValveCalib[(int)eValve].Add(angle, sCCDTiltValve);
            }
            else
            {
                DicCCDTiltValveCalib[(int)eValve][angle] = sCCDTiltValve;
            }
        }
        /// <summary>
        /// 移除項目
        /// </summary>
        /// <param name="eValve"></param>
        /// <param name="angle"></param>
        /// <remarks></remarks>
        //, sLaserTiltvalve As SCCDTiltValveCalibration)
        public void ReMoveCCDTiltValve(enmValve eValve, decimal angle)
        {
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle) == true)
            {
                DicCCDTiltValveCalib[(int)eValve].Remove(angle);
            }
        }
        /// <summary>
        /// 取得對應閥裡的所有key值
        /// </summary>
        /// <param name="eValve"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<decimal> GetAllKeyName(enmValve eValve)
        {
            List<decimal> lstKeyName = new List<decimal>();
            foreach (KeyValuePair<decimal, SCCDTiltValveCalibration> pair in DicCCDTiltValveCalib[(int)eValve])
            {
                lstKeyName.Add(pair.Key);
            }
            return lstKeyName;
        }

        #region "CCD 與 Valve Offset"
        /// <summary>
        /// [平台內CCD與閥偏移量X]
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal CCDTiltValveOffsetX(enmValve eValve, decimal angle)
        {

            //Soni + 2016.09.20 VideoRun判定條件
            if (IsVideoRun == true)
            {
                return 0;
            }
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
            return TiltTemp.CCDCalibPosX - TiltTemp.ValveCalibPosX;
        }
        /// <summary>
        /// [平台內CCD與閥偏移量Y]
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal CCDTiltValveOffsetY(enmValve eValve, decimal angle)
        {

            //Soni + 2016.09.20 VideoRun判定條件
            if (IsVideoRun == true)
            {
                return 0;
            }
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
            return TiltTemp.CCDCalibPosY - TiltTemp.ValveCalibPosY;

        }
        /// <summary>
        /// [平台內CCD與閥偏移量Z]
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public object CCDTiltValveOffsetZ(enmValve eValve, decimal angle)
        {
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
            return TiltTemp.CCDCalibPosZ - TiltTemp.ValveCalibPosZ;
        }
        #endregion

        public decimal GetValveX(enmValve eValve, decimal angle)
        {
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
            }
            else
            {
                return 0;
            }
            return TiltTemp.ValveCalibPosX;
        }
        public void SetValveX(enmValve eValve, decimal angle, decimal value)
        {
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
                TiltTemp.ValveCalibPosX = value;
                AddCCDTiltCCDValve(eValve, angle, TiltTemp);
            }
        }

        public decimal GetValveY(enmValve eValve, decimal angle)
        {
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
            }
            else
            {
                return 0;
            }
            return TiltTemp.ValveCalibPosY;
        }
        public void SetValveY(enmValve eValve, decimal angle, decimal value)
        {
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
                TiltTemp.ValveCalibPosY = value;
                AddCCDTiltCCDValve(eValve, angle, TiltTemp);
            }
        }

        public decimal GetValveZ(enmValve eValve, decimal angle)
        {

            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
            }
            else
            {
                return 0;
            }

            return TiltTemp.ValveCalibPosZ;

        }
        public void SetValveZ(enmValve eValve, decimal angle, decimal value)
        {
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
                TiltTemp.ValveCalibPosZ = value;
                AddCCDTiltCCDValve(eValve, angle, TiltTemp);
            }
        }

        public decimal GetCCDX(enmValve eValve, decimal angle)
        {

            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
            }
            else
            {
                return 0;
            }

            return TiltTemp.CCDCalibPosX;

        }
        public void SetCCDX(enmValve eValve, decimal angle, decimal value)
        {
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
                TiltTemp.CCDCalibPosX = value;
                AddCCDTiltCCDValve(eValve, angle, TiltTemp);
            }
        }

        public decimal GetCCDY(enmValve eValve, decimal angle)
        {

            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
            }
            else
            {
                return 0;
            }

            return TiltTemp.CCDCalibPosY;

        }
        public void SetCCDY(enmValve eValve, decimal angle, decimal value)
        {
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
                TiltTemp.CCDCalibPosY = value;
                AddCCDTiltCCDValve(eValve, angle, TiltTemp);
            }
        }

        public decimal GetCCDZ(enmValve eValve, decimal angle)
        {

            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
            }
            else
            {
                return 0;
            }
            return TiltTemp.CCDCalibPosZ;

        }
        public void SetCCDZ(enmValve eValve, decimal angle, decimal value)
        {
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
                TiltTemp.CCDCalibPosZ = value;
                AddCCDTiltCCDValve(eValve, angle, TiltTemp);
            }
        }

        public decimal GetValvePinZHight(enmValve eValve, decimal angle)
        {

            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
            }
            else
            {
                return 0;
            }
            return TiltTemp.ValvePinZHight;

        }
        public void SetValvePinZHight(enmValve eValve, decimal angle, decimal value)
        {
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
                TiltTemp.ValvePinZHight = value;
                AddCCDTiltCCDValve(eValve, angle, TiltTemp);
            }
        }

        public decimal SetAutoResultX(enmValve eValve, decimal angle)
        {
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
            }
            else
            {
                return 0;
            }
            return TiltTemp.AutoCalibResultX;

        }
        public void SetAutoResultX(enmValve eValve, decimal angle, decimal value)
        {
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
                TiltTemp.AutoCalibResultX = value;
                AddCCDTiltCCDValve(eValve, angle, TiltTemp);
            }
        }

        public decimal GetAutoResultY(enmValve eValve, decimal angle)
        {

            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
            }
            else
            {
                return 0;
            }
            return TiltTemp.AutoCalibResultY;

        }
        public void SetAutoResultY(enmValve eValve, decimal angle, decimal value)
        {
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
                TiltTemp.AutoCalibResultY = value;
                AddCCDTiltCCDValve(eValve, angle, TiltTemp);
            }
        }

        public string GetdecCCDValveCalibrationSceneName(enmValve eValve, decimal angle)
        {
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
            }
            else
            {
                return "0";
            }
            return TiltTemp.CCDValveCalibrationSceneName;
        }
        public void SetdecCCDValveCalibrationSceneName(enmValve eValve, decimal angle, string value)
        {

            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
                TiltTemp.CCDValveCalibrationSceneName = value;
                AddCCDTiltCCDValve(eValve, angle, TiltTemp);
            }

        }

        public int GetiCCDValveCount(enmValve eValve, decimal angle)
        {
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
            }
            else
            {
                return 0;
            }
            return TiltTemp.CCDValveCalibrationCount;
        }
        public void SetiCCDValveCount(enmValve eValve, decimal angle, int value)
        {

            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
                TiltTemp.CCDValveCalibrationCount = value;
                AddCCDTiltCCDValve(eValve, angle, TiltTemp);
            }

        }

        public decimal GetdCCDValveThreadshold(enmValve eValve, decimal angle)
        {
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
            }
            else
            {
                return 0;
            }
            return TiltTemp.CCDValveCalibrationThreshold;
        }
        public void SetdCCDValveThreadshold(enmValve eValve, decimal angle, decimal value)
        {
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
                TiltTemp.CCDValveCalibrationThreshold = value;
                AddCCDTiltCCDValve(eValve, angle, TiltTemp);
            }
        }

        public decimal GetdecAirPressure(enmValve eValve, decimal angle)
        {
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
            }
            else
            {
                return 0;
            }
            return TiltTemp.AirPressure;
        }
        //AirPressure
        //20161122
        public void SetdecAirPressure(enmValve eValve, decimal angle, decimal value)
        {
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
                TiltTemp.AirPressure = value;
                AddCCDTiltCCDValve(eValve, angle, TiltTemp);
            }
        }

        public decimal GetdecPitch(enmValve eValve, decimal angle)
        {
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
            }
            else
            {
                return 0;
            }
            return TiltTemp.Pitch;
        }
        public void SetdecPitch(enmValve eValve, decimal angle, decimal value)
        {

            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
                TiltTemp.Pitch = value;
                AddCCDTiltCCDValve(eValve, angle, TiltTemp);
            }

        }

        public decimal GetdecCCDStableTime(enmValve eValve, decimal angle)
        {
            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
            }
            else
            {
                return 0;
            }
            return TiltTemp.CCDStableTime;
        }

        //20170520
        //CCDStableTime
        public void SetdecCCDStableTime(enmValve eValve, decimal angle, decimal value)
        {

            SCCDTiltValveCalibration TiltTemp = new SCCDTiltValveCalibration();
            if (DicCCDTiltValveCalib[(int)eValve].ContainsKey(angle))
            {
                TiltTemp = DicCCDTiltValveCalib[(int)eValve][angle];
                TiltTemp.CCDStableTime = value;
                AddCCDTiltCCDValve(eValve, angle, TiltTemp);
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
                //Dim sCountSection As String = "AngleCount"

                for (enmValve mValveNo = enmValve.No1; (int)mValveNo <= StageUseValveCount - 1; mValveNo++)
                {
                    foreach (KeyValuePair<decimal, SCCDTiltValveCalibration> pair in DicCCDTiltValveCalib[(int)mValveNo])
                    {
                        strSection = "CCDValve" + (mValveNo + 1).ToString() + "_TiltWorkAngle_" + pair.Key.ToString();
                        SCCDTiltValveCalibration sTemp1 = pair.Value;
                        CIni.SaveIniString(strSection, "ValveCalibPosX_" + iCount.ToString(), sTemp1.ValveCalibPosX, fileName);
                        CIni.SaveIniString(strSection, "ValveCalibPosY_" + iCount.ToString(), sTemp1.ValveCalibPosY, fileName);
                        CIni.SaveIniString(strSection, "ValveCalibPosZ_" + iCount.ToString(), sTemp1.ValveCalibPosZ, fileName);
                        CIni.SaveIniString(strSection, "CCDCalibPosX_" + iCount.ToString(), sTemp1.CCDCalibPosX, fileName);
                        CIni.SaveIniString(strSection, "CCDCalibPosY_" + iCount.ToString(), sTemp1.CCDCalibPosY, fileName);
                        CIni.SaveIniString(strSection, "CCDCalibPosZ_" + iCount.ToString(), sTemp1.CCDCalibPosZ, fileName);
                        CIni.SaveIniString(strSection, "CCDValveCalibrationSceneName_" + iCount.ToString(), sTemp1.CCDValveCalibrationSceneName, fileName);
                        CIni.SaveIniString(strSection, "CCDValveCalibrationCount_" + iCount.ToString(), sTemp1.CCDValveCalibrationCount, fileName);
                        CIni.SaveIniString(strSection, "CCDValveCalibrationThreshold_" + iCount.ToString(), sTemp1.CCDValveCalibrationThreshold, fileName);
                        CIni.SaveIniString(strSection, "AirPressure_" + iCount.ToString(), sTemp1.AirPressure, fileName);
                        CIni.SaveIniString(strSection, "Pitch_" + iCount.ToString(), sTemp1.Pitch, fileName);

                        //20170520
                        CIni.SaveIniString(strSection, "CCDStableTime_" + iCount.ToString(), sTemp1.CCDStableTime, fileName);

                        CIni.SaveIniString("CCDValve" + (mValveNo + 1).ToString() + "_Key", "KeyName" + iCount.ToString(), pair.Key.ToString(), fileName);
                        iCount = iCount + 1;
                    }
                    CIni.SaveIniString("CCDValve" + (mValveNo + 1).ToString() + "_Key", "KeyCount", iCount, fileName);
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
            for (enmValve mValveNo = enmValve.No1; (int)mValveNo <= StageUseValveCount - 1; mValveNo++)
            {
                DicCCDTiltValveCalib[(int)mValveNo].Clear();
            }

            try
            {
                SCCDTiltValveCalibration sTemp1 = new SCCDTiltValveCalibration();
                string kTemp = "";
                int iTemp = 0;

                for (int mValveNo = (int)enmValve.No1; mValveNo <= StageUseValveCount - 1; mValveNo++)
                {
                    sTemp1 = new SCCDTiltValveCalibration();
                    iTemp = Convert.ToInt32(CIni.ReadIniString("CCDValve" + (mValveNo + 1).ToString() + "_Key", "KeyCount", fileName, 1));
                    for (int index = 0; index <= iTemp - 1; index++)
                    {
                        kTemp = CIni.ReadIniString("CCDValve" + (mValveNo + 1).ToString() + "_Key", "KeyName" + index.ToString(), fileName, 0);
                        sTemp1.ValveCalibPosX = Convert.ToDecimal(CIni.ReadIniString("CCDValve" + mValveNo + 1 + "_TiltWorkAngle_" + kTemp, "ValveCalibPosX_" + index.ToString(), fileName, 0));
                        sTemp1.ValveCalibPosY = Convert.ToDecimal(CIni.ReadIniString("CCDValve" + mValveNo + 1 + "_TiltWorkAngle_" + kTemp, "ValveCalibPosY_" + index.ToString(), fileName, 0));
                        sTemp1.ValveCalibPosZ = Convert.ToDecimal(CIni.ReadIniString("CCDValve" + mValveNo + 1 + "_TiltWorkAngle_" + kTemp, "ValveCalibPosZ_" + index.ToString(), fileName, 0));
                        sTemp1.CCDCalibPosX = Convert.ToDecimal(CIni.ReadIniString("CCDValve" + mValveNo + 1 + "_TiltWorkAngle_" + kTemp, "CCDCalibPosX_" + index.ToString(), fileName, 0));
                        sTemp1.CCDCalibPosY = Convert.ToDecimal(CIni.ReadIniString("CCDValve" + mValveNo + 1 + "_TiltWorkAngle_" + kTemp, "CCDCalibPosY_" + index.ToString(), fileName, 0));
                        sTemp1.CCDCalibPosZ = Convert.ToDecimal(CIni.ReadIniString("CCDValve" + mValveNo + 1 + "_TiltWorkAngle_" + kTemp, "CCDCalibPosZ_" + index.ToString(), fileName, 0));
                        sTemp1.CCDValveCalibrationSceneName = CIni.ReadIniString("CCDValve" + mValveNo + 1 + "_TiltWorkAngle_" + kTemp, "CCDValveCalibrationSceneName_" + index.ToString(), fileName, 0);
                        sTemp1.CCDValveCalibrationCount = Convert.ToInt32(CIni.ReadIniString("CCDValve" + mValveNo + 1 + "_TiltWorkAngle_" + kTemp, "CCDValveCalibrationCount_" + index.ToString(), fileName, 0));
                        sTemp1.CCDValveCalibrationThreshold = Convert.ToDecimal(CIni.ReadIniString("CCDValve" + mValveNo + 1 + "_TiltWorkAngle_" + kTemp, "CCDValveCalibrationThreshold_" + index.ToString(), fileName, 5));
                        sTemp1.AirPressure = Convert.ToDecimal(CIni.ReadIniString("CCDValve" + mValveNo + 1 + "_TiltWorkAngle_" + kTemp, "AirPressure_" + index.ToString(), fileName, 0));
                        sTemp1.Pitch = Convert.ToDecimal(CIni.ReadIniString("CCDValve" + mValveNo + 1 + "_TiltWorkAngle_" + kTemp, "Pitch_" + index.ToString(), fileName, 0));

                        //20170520
                        sTemp1.CCDStableTime = Convert.ToDecimal(CIni.ReadIniString("CCDValve" + mValveNo + 1 + "_TiltWorkAngle_" + kTemp, "CCDStableTime_" + index.ToString(), fileName, 1200));

                        DicCCDTiltValveCalib[(int)mValveNo].Add(Convert.ToDecimal(kTemp), sTemp1);
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
