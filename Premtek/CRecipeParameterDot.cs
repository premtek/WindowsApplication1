using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>打點參數
    /// </summary>
    public class CRecipeParameterDot
    {
        /// <summary>噴射模式
        /// </summary>
        public bool JetMode = true;
        /// <summary>XY到位穩定時間(Sec)
        /// </summary>
        public decimal XYStableTime;
        /// <summary>Z到位穩定時間(Sec)
        /// </summary>
        public decimal ZStableTime;
        /// <summary>Z軸下降速度(mm/s)
        /// </summary>
        public decimal ZDownVelocity;
        /// <summary>Z軸下降加速度(mm/s^2)
        /// </summary>
        public decimal ZDownAcc;

        /// <summary>開閥後等待時間(Sec)
        /// </summary>
        public decimal ValveOnDelayTime;
        /// <summary>斷膠平移方向
        /// </summary>
        public BacktrackDirection BacktrackDirection;
        /// <summary>真空回吸時間(Sec)
        /// </summary>
        public decimal SuckBackTime;
        /// <summary>斷膠等待時間(Sec)
        /// </summary>
        public decimal BacktrackDelayTime;
        /// <summary>斷膠高度(mm)
        /// </summary>
        public decimal BacktrackHeight;
        /// <summary>斷膠平移距離(mm)
        /// </summary>
        public decimal BacktrackLength;
        /// <summary>斷膠移動速度
        /// </summary>
        public decimal BacktrackVelocity;
        /// <summary>上抬高度(閃元件高度)(mm_
        /// </summary>
        public decimal RetractHeight;
        /// <summary>上抬速度(mm/s)
        /// </summary>
        public decimal RetractVelocity;
        /// <summary>上抬加速度(mm/s)
        /// </summary>
        public decimal RetractAcc;


        /// <summary>複製
        /// </summary>
        /// <returns></returns>
        public CRecipeParameterDot Clone()
        {
            CRecipeParameterDot _Temp = new CRecipeParameterDot();
            _Temp.JetMode = this.JetMode;
            _Temp.XYStableTime = this.XYStableTime;
            _Temp.ZStableTime = this.ZStableTime;
            _Temp.ZDownVelocity = this.ZDownVelocity;
            _Temp.ZDownAcc = this.ZDownAcc;
            _Temp.ValveOnDelayTime = this.ValveOnDelayTime;
            _Temp.BacktrackDirection = this.BacktrackDirection;
            _Temp.SuckBackTime = this.SuckBackTime;
            _Temp.BacktrackDelayTime = this.BacktrackDelayTime;
            _Temp.BacktrackHeight = this.BacktrackHeight;
            _Temp.BacktrackLength = this.BacktrackLength;
            _Temp.BacktrackVelocity = this.BacktrackVelocity;
            _Temp.RetractHeight = this.RetractHeight;
            _Temp.RetractVelocity = this.RetractVelocity;
            _Temp.RetractAcc = this.RetractAcc;
            return _Temp;
        }

        /// <summary>儲存參數
        /// </summary>
        /// <param name="typeNo">類型編號</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Save(string key, string fileName)
        {
            string _SectionName = "StepParameter_" + key.ToString();
            string _KeyNameStart = "Dot_";

            CIni.SaveIniString(_SectionName, _KeyNameStart + "JetMode", Convert.ToInt32(this.JetMode).ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "XYStableTime", this.XYStableTime.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "ZStableTime", this.ZStableTime.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "ZDownVelocity", this.ZDownVelocity.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "ZDownAcc", this.ZDownAcc.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "ValveOnDelayTime", this.ValveOnDelayTime.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "BacktrackDirection", Convert.ToInt32(this.BacktrackDirection).ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "SuckBackTime", this.SuckBackTime.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "BacktrackDelayTime", this.BacktrackDelayTime.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "BacktrackHeight", this.BacktrackHeight.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "BacktrackLength", this.BacktrackLength.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "BacktrackVelocity", this.BacktrackVelocity.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "RetractHeight", this.RetractHeight.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "RetractVelocity", this.RetractVelocity.ToString(), fileName);
            CIni.SaveIniString(_SectionName, _KeyNameStart + "RetractAcc", this.RetractAcc.ToString(), fileName);
            return ErrorCode.Success;
        }

        /// <summary>讀取參數
        /// </summary>
        /// <param name="typeNo">類型編號</param>
        /// <param name="fileName">檔案完整路徑</param>
        /// <returns>ErrorCode</returns>
        public ErrorCode Load(string key, string fileName)
        {
            string _SectionName = "StepParameter_" + key.ToString();
            string _KeyNameStart = "Dot_";

            bool.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "JetMode", fileName, 0), out this.JetMode);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "XYStableTime", fileName, 0), out this.XYStableTime);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "ZStableTime", fileName, 0), out this.ZStableTime);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "ZDownVelocity", fileName, 0), out this.ZDownVelocity);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "ZDownAcc", fileName, 0), out this.ZDownAcc);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "ValveOnDelayTime", fileName, 0), out this.ValveOnDelayTime);
            Enum.TryParse<BacktrackDirection>(CIni.ReadIniString(_SectionName, _KeyNameStart + "BacktrackDirection", fileName, 0), out this.BacktrackDirection);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "SuckBackTime", fileName, 0), out this.SuckBackTime);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "BacktrackDelayTime", fileName, 0), out this.BacktrackDelayTime);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "BacktrackHeight", fileName, 0), out this.BacktrackHeight);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "BacktrackLength", fileName, 0), out this.BacktrackLength);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "BacktrackVelocity", fileName, 0), out this.BacktrackVelocity);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "RetractHeight", fileName, 0), out this.RetractHeight);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "RetractVelocity", fileName, 0), out this.RetractVelocity);
            decimal.TryParse(CIni.ReadIniString(_SectionName, _KeyNameStart + "RetractAcc", fileName, 0), out this.RetractAcc);
            return ErrorCode.Success;
        }

    }
}
