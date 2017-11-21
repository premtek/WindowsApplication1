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

    public class CValveData
    {
        /// <summary>外部配接系統參數
        /// </summary>
        public int StageUseValveCount = 1;
        /// <summary>[閥件類型]</summary>
        /// <remarks></remarks>
        public MSystemParameter.enmValveType[] ValveType = new MSystemParameter.enmValveType[(int)enmValve.Max + 1];
        /// <summary>[噴射閥類型]</summary>
        /// <remarks></remarks>
        public MSystemParameter.eValveModel[] JetValve = new MSystemParameter.eValveModel[(int)enmValve.Max + 1];
        /// <summary>[Purge時間(Jet Valve)]</summary>
        /// <remarks></remarks>
        public decimal[] PurgeTime = new decimal[(int)enmValve.Max + 1];
        /// <summary>閥膠管膠量檢測致能</summary>
        /// <remarks></remarks>
        public bool[] EnableDetectPaste = new bool[(int)enmValve.Max + 1];
        /// <summary>[螺桿閥電流保護值]</summary>
        /// <remarks></remarks>
        public decimal[] CTThreshold = new decimal[(int)enmValve.Max + 1];
        /// <summary>Lucas建議增加的開機預壓參數</summary>
        /// <remarks></remarks>
        public decimal[] PrePressure = new decimal[(int)enmValve.Max + 1];
        /// <summary>[清膠每次的間隔]</summary>
        /// <remarks></remarks>
        public decimal[] CleanPastePitch = new decimal[(int)enmValve.Max + 1];
        /// <summary>[紀錄已經清膠幾次]</summary>
        /// <remarks></remarks>
        public int[] CleanPasteNum = new int[(int)enmValve.Max + 1];
        /// <summary>[使用清膠的上限次數]</summary>
        /// <remarks></remarks>
        public int[] CleanPasteNumLimit = new int[(int)enmValve.Max + 1];
        /// <summary>[清膠時Z軸移動的距離]</summary>
        /// <remarks></remarks>
        public decimal[] CleanPasteDistanceZ = new decimal[(int)enmValve.Max + 1];
        /// <summary>[清膠後出壓力之時間]</summary>
        /// <remarks></remarks>
        public decimal[] CleanPastePressureTime = new decimal[(int)enmValve.Max + 1];
        /// <summary>[清膠平台之工作長度]</summary>
        /// <remarks></remarks>
        public decimal[] CleanPasteTableLength = new decimal[(int)enmValve.Max + 1];
        /// <summary>[清膠擦拭移動距離Y移動距離 ]</summary>
        /// <remarks></remarks>
        public decimal[] CleanPasteOffset = new decimal[(int)enmValve.Max + 1];
        /// <summary>[清膠擦拭移動速度]</summary>
        /// <remarks></remarks>
        public decimal[] CleanPasteSpeed = new decimal[(int)enmValve.Max + 1];
        /// <summary>[清膠馬達旋轉方向]</summary>
        /// <remarks></remarks>

        public bool[] CleanPasteDir = new bool[(int)enmValve.Max + 1];
        /// <summary>儲存平台內所有閥</summary>
        /// <param name="fileName"></param>
        /// <remarks></remarks>
        public void Save(string fileName)
        {
            try
            {

                string strSection = null;
                for (int mValveNo = (int)enmValve.No1; mValveNo <= StageUseValveCount - 1; mValveNo++)
                {
                    strSection = "Valve" + (mValveNo + 1).ToString();
                    CIni.SaveIniString(strSection, "ValveData_ValveType" + (mValveNo + 1).ToString(),Convert.ToInt32( this.ValveType[mValveNo]).ToString(), fileName);
                    CIni.SaveIniString(strSection, "ValveData_JetValve" + (mValveNo + 1).ToString(), Convert.ToInt32(this.JetValve[mValveNo]).ToString(), fileName);
                    CIni.SaveIniString(strSection, "ValveData_PurgeTime" + (mValveNo + 1).ToString(), this.PurgeTime[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "ValveData_EnableDetectPaste" + (mValveNo + 1).ToString(), this.EnableDetectPaste[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "ValveData_CTThreshold" + (mValveNo + 1).ToString(), this.CTThreshold[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "ValveData_PrePressure" + (mValveNo + 1).ToString(), this.PrePressure[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "ValveData_CleanPastePitch" + (mValveNo + 1).ToString(), this.CleanPastePitch[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "ValveData_CleanPasteNum" + (mValveNo + 1).ToString(), this.CleanPasteNum[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "ValveData_CleanPasteNumLimit" + (mValveNo + 1).ToString(), this.CleanPasteNumLimit[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "ValveData_CleanPasteDistanceZ" + (mValveNo + 1).ToString(), this.CleanPasteDistanceZ[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "ValveData_CleanPastePressureTime" + (mValveNo + 1).ToString(), this.CleanPastePressureTime[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "ValveData_CleanPasteTableLength" + (mValveNo + 1).ToString(), this.CleanPasteTableLength[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "ValveData_CleanPasteOffset" + (mValveNo + 1).ToString(), this.CleanPasteOffset[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "ValveData_CleanPasteSpeed" + (mValveNo + 1).ToString(), this.CleanPasteSpeed[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "ValveData_CleanPasteDir" + (mValveNo + 1).ToString(), this.CleanPasteDir[mValveNo].ToString(), fileName);
                }

            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1002029), "Error_1002029", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                MessageBox.Show(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1002029) + ex.Message);
            }
        }
        /// <summary>讀取平台內所有閥</summary>
        /// <param name="fileName"></param>
        /// <remarks></remarks>
        public void Load(string fileName)
        {
            try
            {
                string strSection = null;
              
                for (int mValveNo = (int)enmValve.No1; mValveNo <= StageUseValveCount - 1; mValveNo++)
                {
                    strSection = "Valve" + (mValveNo + 1).ToString();
                    Enum.TryParse<MSystemParameter.enmValveType>(CIni.ReadIniString(strSection, "ValveData_ValveType" + (mValveNo + 1).ToString(), fileName, 0), out this.ValveType[mValveNo]);
                    Enum.TryParse<MSystemParameter.eValveModel>(CIni.ReadIniString(strSection, "ValveData_JetValve" + (mValveNo + 1).ToString(), fileName, 0), out this.JetValve[mValveNo]);
                    decimal.TryParse(CIni.ReadIniString(strSection, "ValveData_PurgeTime" + (mValveNo + 1).ToString(), fileName, 0), out this.PurgeTime[mValveNo]);
                    //TODO:待確認是否放在CPurgeParameter
                    bool.TryParse(CIni.ReadIniString(strSection, "ValveData_EnableDetectPaste" + (mValveNo + 1).ToString(), fileName, 0), out this.EnableDetectPaste[mValveNo]);

                    decimal.TryParse(CIni.ReadIniString(strSection, "ValveData_CTThreshold" + (mValveNo + 1).ToString(), fileName, 0), out this.CTThreshold[mValveNo]);
                    decimal.TryParse(CIni.ReadIniString(strSection, "ValveData_PrePressure" + (mValveNo + 1).ToString(), fileName, 0), out this.PrePressure[mValveNo]);
                    decimal.TryParse(CIni.ReadIniString(strSection, "ValveData_CleanPastePitch" + (mValveNo + 1).ToString(), fileName, 0), out this.CleanPastePitch[mValveNo]);
                    int.TryParse(CIni.ReadIniString(strSection, "ValveData_CleanPasteNum" + (mValveNo + 1).ToString(), fileName, 0), out  this.CleanPasteNum[mValveNo]);
                    int.TryParse(CIni.ReadIniString(strSection, "ValveData_CleanPasteNumLimit" + (mValveNo + 1).ToString(), fileName, 0), out  this.CleanPasteNumLimit[mValveNo]);

                    decimal.TryParse(CIni.ReadIniString(strSection, "ValveData_CleanPasteDistanceZ" + (mValveNo + 1).ToString(), fileName, 0), out this.CleanPasteDistanceZ[mValveNo]);
                    decimal.TryParse(CIni.ReadIniString(strSection, "ValveData_CleanPastePressureTime" + (mValveNo + 1).ToString(), fileName, 0), out this.CleanPastePressureTime[mValveNo]);
                    decimal.TryParse(CIni.ReadIniString(strSection, "ValveData_CleanPasteTableLength" + (mValveNo + 1).ToString(), fileName, 0), out this.CleanPasteTableLength[mValveNo]);
                    decimal.TryParse(CIni.ReadIniString(strSection, "ValveData_CleanPasteOffset" + (mValveNo + 1).ToString(), fileName, 0), out this.CleanPasteOffset[mValveNo]);
                    decimal.TryParse(CIni.ReadIniString(strSection, "ValveData_CleanPasteSpeed" + (mValveNo + 1).ToString(), fileName, 0), out this.CleanPasteSpeed[mValveNo]);
                    bool.TryParse(CIni.ReadIniString(strSection, "ValveData_CleanPasteDir" + (mValveNo + 1).ToString(), fileName, 0), out this.CleanPasteDir[mValveNo]);

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
