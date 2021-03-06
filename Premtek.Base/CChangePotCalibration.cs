﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using ProjectCore;

namespace Premtek.Base
{

    /// <summary>[換膠管位置校正]</summary>
    /// <remarks></remarks>
    public class CChangePotCalibration
    {
        /// <summary>[換膠的位置] </summary>
        /// <remarks></remarks>
        public decimal[] PosX = new decimal[(int)enmValve.Max + 1];
        /// <summary>[換膠的位置]</summary>
        /// <remarks></remarks>
        public decimal[] PosY = new decimal[(int)enmValve.Max + 1];
        /// <summary>[換膠的位置]</summary>
        /// <remarks></remarks>
        public decimal[] PosZ = new decimal[(int)enmValve.Max + 1];
        /// <summary>[換膠的位置]</summary>
        /// <remarks></remarks>
        public decimal[] PosB = new decimal[(int)enmValve.Max + 1];
        /// <summary>[換膠的位置]</summary>
        /// <remarks></remarks>

        public decimal[] PosC = new decimal[(int)enmValve.Max + 1];
        /// <summary>外部配接系統參數
        /// </summary>
        public int StageUseValveCount = 1;
        /// <summary>儲存校正檔-平台內所有閥</summary>
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
                    CIni.SaveIniString(strSection, "ChangePotCalibration_Pos" + (mValveNo + 1).ToString() + "X", this.PosX[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "ChangePotCalibration_Pos" + (mValveNo + 1).ToString() + "Y", this.PosY[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "ChangePotCalibration_Pos" + (mValveNo + 1).ToString() + "Z", this.PosZ[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "ChangePotCalibration_Pos" + (mValveNo + 1).ToString() + "B", this.PosB[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "ChangePotCalibration_Pos" + (mValveNo + 1).ToString() + "C", this.PosC[mValveNo].ToString(), fileName);
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
            try
            {
                string strSection = null;


                for (int mValveNo = (int)enmValve.No1; mValveNo <= StageUseValveCount - 1; mValveNo++)
                {
                    strSection = "Valve" + (mValveNo + 1).ToString();
                    decimal.TryParse(CIni.ReadIniString(strSection, "PurgeCalibration_ValvePos" + (mValveNo + 1).ToString() + "X", fileName, 0), out this.PosX[mValveNo]);
                    decimal.TryParse(CIni.ReadIniString(strSection, "PurgeCalibration_ValvePos" + (mValveNo + 1).ToString() + "Y", fileName, 0), out this.PosY[mValveNo]);
                    this.PosZ[mValveNo] = 0;
                    this.PosB[mValveNo] = 0;
                    this.PosC[mValveNo] = 0;
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
