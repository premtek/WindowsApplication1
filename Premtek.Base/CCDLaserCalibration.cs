using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ProjectCore;

namespace Premtek.Base
{

    /// <summary>CCD對雷射校正</summary>
    /// <remarks></remarks>
    public class CCDLaserCalibration
    {
        /// <summary>外部接入系統參數
        /// </summary>
        public int StageUseValveCount = 1;
        #region "CCD Laser XY校正"
        /// <summary>CCD測平台校正X(for Laser)</summary>
        /// <remarks></remarks>
        public decimal[] CCDCalibPosX = new decimal[(int)enmValve.Max + 1];
        /// <summary>CCD測平台校正Y(for Laser)</summary>
        /// <remarks></remarks>
        public decimal[] CCDCalibPosY = new decimal[(int)enmValve.Max + 1];
        /// <summary>CCD測平台校正Z(for Laser)</summary>
        /// <remarks></remarks>

        public decimal[] CCDCalibPosZ = new decimal[(int)enmValve.Max + 1];
        /// <summary>[雷射測平台X]</summary>
        /// <remarks></remarks>
        public decimal[] LaserCalibPosX = new decimal[(int)enmValve.Max + 1];
        /// <summary>[雷射測平台Y]</summary>
        /// <remarks></remarks>
        public decimal[] LaserCalibPosY = new decimal[(int)enmValve.Max + 1];
        /// <summary>[雷射測平台Z]</summary>
        /// <remarks></remarks>

        public decimal[] LaserCalibPosZ = new decimal[(int)enmValve.Max + 1];
        /// <summary>
        /// [平台內CCD與雷射偏移量X]
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal CCDLaserOffsetX(int valveNo)
        {
            return CCDCalibPosX[valveNo] - LaserCalibPosX[valveNo];
        }

        /// <summary>
        /// [平台內CCD與雷射偏移量Y]
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal CCDLaserOffsetY(int valveNo)
        {
            return CCDCalibPosY[valveNo] - LaserCalibPosY[valveNo];
        }

        /// <summary>
        /// [平台內CCD與雷射偏移量Y]
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal CCDLaserOffsetZ(int valveNo)
        {
            return CCDCalibPosZ[valveNo] - LaserCalibPosZ[valveNo];
        }

        #endregion

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
                    CIni.SaveIniString(strSection, "LaserCalibPos" + (mValveNo + 1).ToString() + "X", this.LaserCalibPosX[mValveNo], fileName);
                    CIni.SaveIniString(strSection, "LaserCalibPos" + (mValveNo + 1).ToString() + "Y", this.LaserCalibPosY[mValveNo], fileName);
                    CIni.SaveIniString(strSection, "LaserCalibPos" + (mValveNo + 1).ToString() + "Z", this.LaserCalibPosZ[mValveNo], fileName);
                    CIni.SaveIniString(strSection, "CCDLaserCalibPos" + (mValveNo + 1).ToString() + "X", this.CCDCalibPosX[mValveNo], fileName);
                    CIni.SaveIniString(strSection, "CCDLaserCalibPos" + (mValveNo + 1).ToString() + "Y", this.CCDCalibPosY[mValveNo], fileName);
                    CIni.SaveIniString(strSection, "CCDLaserCalibPos" + (mValveNo + 1).ToString() + "Z", this.CCDCalibPosZ[mValveNo], fileName);
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
            try
            {
                string strSection = null;

                for (int mValveNo = (int)enmValve.No1; mValveNo <= StageUseValveCount - 1; mValveNo++)
                {
                    strSection = "Valve" + (mValveNo + 1).ToString();
                    this.LaserCalibPosX[mValveNo] = Convert.ToDecimal(CIni.ReadIniString(strSection, "LaserCalibPos" + (mValveNo + 1).ToString() + "X", fileName, 0));
                    this.LaserCalibPosY[mValveNo] = Convert.ToDecimal(CIni.ReadIniString(strSection, "LaserCalibPos" + (mValveNo + 1).ToString() + "Y", fileName, 0));
                    this.LaserCalibPosZ[mValveNo] = Convert.ToDecimal(CIni.ReadIniString(strSection, "LaserCalibPos" + (mValveNo + 1).ToString() + "Z", fileName, 0));
                    this.CCDCalibPosX[mValveNo] = Convert.ToDecimal(CIni.ReadIniString(strSection, "CCDLaserCalibPos" + (mValveNo + 1).ToString() + "X", fileName, 0));
                    this.CCDCalibPosY[mValveNo] = Convert.ToDecimal(CIni.ReadIniString(strSection, "CCDLaserCalibPos" + (mValveNo + 1).ToString() + "Y", fileName, 0));
                    this.CCDCalibPosZ[mValveNo] = Convert.ToDecimal(CIni.ReadIniString(strSection, "CCDLaserCalibPos" + (mValveNo + 1).ToString() + "Z", fileName, 0));
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
