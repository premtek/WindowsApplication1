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

    /// <summary>[秤重位置校正]</summary>
    /// <remarks></remarks>
    public class CWeightCalibration
    {
        /// <summary>[CCD在秤重位置]</summary>
        /// <remarks></remarks>
        public decimal[] CCDPosX = new decimal[(int)enmValve.Max + 1];
        /// <summary>[CCD在秤重位置]</summary>
        /// <remarks></remarks>
        public decimal[] CCDPosY = new decimal[(int)enmValve.Max + 1];
        /// <summary>[CCD在秤重位置]</summary>
        /// <remarks></remarks>
        public decimal[] CCDPosZ = new decimal[(int)enmValve.Max + 1];
        /// <summary>[CCD在秤重特徵定位點1位置]</summary>
        /// <remarks></remarks>
        public decimal[] CCDAlign1PosX = new decimal[(int)enmValve.Max + 1];
        /// <summary>[CCD在秤重特徵定位點1位置]</summary>
        /// <remarks></remarks>
        public decimal[] CCDAlign1PosY = new decimal[(int)enmValve.Max + 1];
        /// <summary>[CCD在秤重特徵定位點1位置]</summary>
        /// <remarks></remarks>
        public decimal[] CCDAlign1PosZ = new decimal[(int)enmValve.Max + 1];
        /// <summary>[CCD在秤重特徵定位點2位置]</summary>
        /// <remarks></remarks>
        public decimal[] CCDAlign2PosX = new decimal[(int)enmValve.Max + 1];
        /// <summary>[CCD在秤重特徵定位點2位置]</summary>
        /// <remarks></remarks>
        public decimal[] CCDAlign2PosY = new decimal[(int)enmValve.Max + 1];
        /// <summary>[CCD在秤重特徵定位點2位置]</summary>
        /// <remarks></remarks>
        public decimal[] CCDAlign2PosZ = new decimal[(int)enmValve.Max + 1];
        /// <summary>[CCD在秤重特徵定位點3位置]</summary>
        /// <remarks></remarks>
        public decimal[] CCDAlign3PosX = new decimal[(int)enmValve.Max + 1];
        /// <summary>[CCD在秤重特徵定位點3位置]</summary>
        /// <remarks></remarks>
        public decimal[] CCDAlign3PosY = new decimal[(int)enmValve.Max + 1];
        /// <summary>[CCD在秤重特徵定位點3位置]</summary>
        /// <remarks></remarks>
        public decimal[] CCDAlign3PosZ = new decimal[(int)enmValve.Max + 1];
        /// <summary>[閥秤重位置]</summary>
        /// <remarks></remarks>
        public decimal[] ValvePosX = new decimal[(int)enmValve.Max + 1];
        /// <summary>[閥秤重位置]</summary>
        /// <remarks></remarks>
        public decimal[] ValvePosY = new decimal[(int)enmValve.Max + 1];
        /// <summary>[閥秤重位置]</summary>
        /// <remarks></remarks>
        public decimal[] ValvePosZ = new decimal[(int)enmValve.Max + 1];
        /// <summary>[閥秤重位置]</summary>
        /// <remarks></remarks>
        public decimal[] ValvePosB = new decimal[(int)enmValve.Max + 1];
        /// <summary>[閥秤重位置]</summary>
        /// <remarks></remarks>
        public decimal[] ValvePosC = new decimal[(int)enmValve.Max + 1];
        /// <summary>[秤重的安全位置]</summary>
        /// <remarks></remarks>
        public double[] SafePosX = new double[(int)enmValve.Max + 1];
        /// <summary>[秤重的安全位置]</summary>
        /// <remarks></remarks>
        public double[] SafePosY = new double[(int)enmValve.Max + 1];
        /// <summary>[秤重的安全位置]</summary>
        /// <remarks></remarks>
        public double[] SafePosZ = new double[(int)enmValve.Max + 1];
        /// <summary>[秤重的安全位置]</summary>
        /// <remarks></remarks>
        public double[] SafePosB = new double[(int)enmValve.Max + 1];
        /// <summary>[秤重的安全位置]</summary>
        /// <remarks></remarks>

        public double[] SafePosC = new double[(int)enmValve.Max + 1];
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
                    CIni.SaveIniString(strSection, "WeightCalibration_CCDPos" + (mValveNo + 1).ToString() + "X", this.CCDPosX[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "WeightCalibration_CCDPos" + (mValveNo + 1).ToString() + "Y", this.CCDPosY[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "WeightCalibration_CCDPos" + (mValveNo + 1).ToString() + "Z", this.CCDPosZ[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "WeightCalibration_CCDAlign1Pos" + (mValveNo + 1).ToString() + "X", this.CCDAlign1PosX[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "WeightCalibration_CCDAlign1Pos" + (mValveNo + 1).ToString() + "Y", this.CCDAlign1PosY[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "WeightCalibration_CCDAlign1Pos" + (mValveNo + 1).ToString() + "Z", this.CCDAlign1PosZ[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "WeightCalibration_CCDAlign2Pos" + (mValveNo + 1).ToString() + "X", this.CCDAlign2PosX[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "WeightCalibration_CCDAlign2Pos" + (mValveNo + 1).ToString() + "Y", this.CCDAlign2PosY[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "WeightCalibration_CCDAlign2Pos" + (mValveNo + 1).ToString() + "Z", this.CCDAlign2PosZ[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "WeightCalibration_CCDAlign3Pos" + (mValveNo + 1).ToString() + "X", this.CCDAlign3PosX[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "WeightCalibration_CCDAlign3Pos" + (mValveNo + 1).ToString() + "Y", this.CCDAlign3PosY[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "WeightCalibration_CCDAlign3Pos" + (mValveNo + 1).ToString() + "Z", this.CCDAlign3PosZ[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "WeightCalibration_ValvePos" + (mValveNo + 1).ToString() + "X", this.ValvePosX[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "WeightCalibration_ValvePos" + (mValveNo + 1).ToString() + "Y", this.ValvePosY[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "WeightCalibration_ValvePos" + (mValveNo + 1).ToString() + "Z", this.ValvePosZ[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "WeightCalibration_ValvePos" + (mValveNo + 1).ToString() + "B", this.ValvePosB[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "WeightCalibration_ValvePos" + (mValveNo + 1).ToString() + "C", this.ValvePosC[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "WeightCalibration_SafePos" + (mValveNo + 1).ToString() + "X", this.SafePosX[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "WeightCalibration_SafePos" + (mValveNo + 1).ToString() + "Y", this.SafePosY[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "WeightCalibration_SafePos" + (mValveNo + 1).ToString() + "Z", this.SafePosZ[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "WeightCalibration_SafePos" + (mValveNo + 1).ToString() + "B", this.SafePosB[mValveNo].ToString(), fileName);
                    CIni.SaveIniString(strSection, "WeightCalibration_SafePos" + (mValveNo + 1).ToString() + "C", this.SafePosC[mValveNo].ToString(), fileName);
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
                    decimal.TryParse(CIni.ReadIniString(strSection, "WeightCalibration_CCDPos" + (mValveNo + 1).ToString() + "X", fileName, 0), out this.CCDPosX[mValveNo]);
                    decimal.TryParse(CIni.ReadIniString(strSection, "WeightCalibration_CCDPos" + (mValveNo + 1).ToString() + "Y", fileName, 0), out this.CCDPosY[mValveNo]);
                    decimal.TryParse(CIni.ReadIniString(strSection, "WeightCalibration_CCDPos" + (mValveNo + 1).ToString() + "Z", fileName, 0), out this.CCDPosZ[mValveNo]);

                    decimal.TryParse(CIni.ReadIniString(strSection, "WeightCalibration_CCDAlign1Pos" + (mValveNo + 1).ToString() + "X", fileName, 0), out this.CCDAlign1PosX[mValveNo]);
                    decimal.TryParse(CIni.ReadIniString(strSection, "WeightCalibration_CCDAlign1Pos" + (mValveNo + 1).ToString() + "Y", fileName, 0), out this.CCDAlign1PosY[mValveNo]);
                    decimal.TryParse(CIni.ReadIniString(strSection, "WeightCalibration_CCDAlign1Pos" + (mValveNo + 1).ToString() + "Z", fileName, 0), out this.CCDAlign1PosZ[mValveNo]);

                    decimal.TryParse(CIni.ReadIniString(strSection, "WeightCalibration_CCDAlign2Pos" + (mValveNo + 1).ToString() + "X", fileName, 0), out this.CCDAlign2PosX[mValveNo]);
                    decimal.TryParse(CIni.ReadIniString(strSection, "WeightCalibration_CCDAlign2Pos" + (mValveNo + 1).ToString() + "Y", fileName, 0), out this.CCDAlign2PosY[mValveNo]);
                    decimal.TryParse(CIni.ReadIniString(strSection, "WeightCalibration_CCDAlign2Pos" + (mValveNo + 1).ToString() + "Z", fileName, 0), out this.CCDAlign2PosZ[mValveNo]);

                    decimal.TryParse(CIni.ReadIniString(strSection, "WeightCalibration_CCDAlign3Pos" + (mValveNo + 1).ToString() + "X", fileName, 0), out this.CCDAlign3PosX[mValveNo]);
                    decimal.TryParse(CIni.ReadIniString(strSection, "WeightCalibration_CCDAlign3Pos" + (mValveNo + 1).ToString() + "Y", fileName, 0), out this.CCDAlign3PosY[mValveNo]);
                    decimal.TryParse(CIni.ReadIniString(strSection, "WeightCalibration_CCDAlign3Pos" + (mValveNo + 1).ToString() + "Z", fileName, 0), out this.CCDAlign3PosZ[mValveNo]);

                    decimal.TryParse(CIni.ReadIniString(strSection, "WeightCalibration_ValvePos" + (mValveNo + 1).ToString() + "X", fileName, 0), out this.ValvePosX[mValveNo]);
                    decimal.TryParse(CIni.ReadIniString(strSection, "WeightCalibration_ValvePos" + (mValveNo + 1).ToString() + "Y", fileName, 0), out this.ValvePosY[mValveNo]);
                    decimal.TryParse(CIni.ReadIniString(strSection, "WeightCalibration_ValvePos" + (mValveNo + 1).ToString() + "Z", fileName, 0), out this.ValvePosZ[mValveNo]);
                    decimal.TryParse(CIni.ReadIniString(strSection, "WeightCalibration_ValvePos" + (mValveNo + 1).ToString() + "B", fileName, 0), out this.ValvePosB[mValveNo]);
                    decimal.TryParse(CIni.ReadIniString(strSection, "WeightCalibration_ValvePos" + (mValveNo + 1).ToString() + "C", fileName, 0), out this.ValvePosC[mValveNo]);

                    double.TryParse(CIni.ReadIniString(strSection, "WeightCalibration_SafePos" + (mValveNo + 1).ToString() + "X", fileName, 0), out this.SafePosX[mValveNo]);
                    double.TryParse(CIni.ReadIniString(strSection, "WeightCalibration_SafePos" + (mValveNo + 1).ToString() + "Y", fileName, 0), out this.SafePosY[mValveNo]);
                    double.TryParse(CIni.ReadIniString(strSection, "WeightCalibration_SafePos" + (mValveNo + 1).ToString() + "Z", fileName, 0), out this.SafePosZ[mValveNo]);
                    double.TryParse(CIni.ReadIniString(strSection, "WeightCalibration_SafePos" + (mValveNo + 1).ToString() + "B", fileName, 0), out this.SafePosB[mValveNo]);
                    double.TryParse(CIni.ReadIniString(strSection, "WeightCalibration_SafePos" + (mValveNo + 1).ToString() + "C", fileName, 0), out this.SafePosC[mValveNo]);

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
