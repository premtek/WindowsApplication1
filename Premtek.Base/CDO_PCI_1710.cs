using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.BDaq;
using ProjectCore;

namespace Premtek
{
    public class CDO_PCI_1710 : IDOInterface
    {
        /// <summary>埠數/卡</summary>
        /// <remarks></remarks>
        public int PortPerCard { get; set; }

        /// <summary>點數/埠</summary>
        /// <remarks></remarks>
        public int BitsPerPort { get; set; }

        public bool IsInitialOK { get; set; }

        internal Automation.BDaq.InstantDoCtrl InstantDoCtrl1 = new Automation.BDaq.InstantDoCtrl();
        public CDO_PCI_1710()
        {
            this.PortPerCard = 8;
            this.BitsPerPort = 8;
        }

        /// <summary> 資源釋放 </summary>
        public void Close()
        {
            InstantDoCtrl1.Dispose();
        }

        /// <summary> 初始化 </summary>
        /// <param name="deviceDescription"></param>
        /// <returns></returns>
        public bool Initial(string deviceDescription)
        {
            try
            {
                InstantDoCtrl1.SelectedDevice = new DeviceInformation(deviceDescription);
                IsInitialOK = true;
                return true;
            }
            catch (Exception ex)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1004000), "Error_1004000", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + ex.Message, "", eMessageLevel.Error);
                System.Windows.Forms.MessageBox.Show(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1004000) + ex.Message, "PCI-1710", System.Windows.Forms.MessageBoxButtons.OK);
                return false;
            }
        }


        /// <summary> 資料寫入卡片 </summary>
        /// <param name="port"></param>
        /// <param name="bytDOData"></param>
        /// <returns></returns>
        public int Write(int port, ref byte bytDOData)
        {
            Automation.BDaq.ErrorCode errorCode = InstantDoCtrl1.Write(port, bytDOData);
            if (errorCode != ErrorCode.Success)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1005003), "Error_1005003", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + errorCode, "", eMessageLevel.Error); //wenda待確認 errorCode怎麼轉16進位
            }
            return (int)errorCode;

        }

        /// <summary> 單點資料寫入/輸出 </summary>
        /// <param name="port"></param>
        /// <param name="bit"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool DOOutput(int port, int bit, bool value)
        {
            Byte data;
            //取出當前資料
            InstantDoCtrl1.Read(port, out data);

            int mtempData;
            if (value == true)
            {
                //data = data | 2 ^ bit; //取聯集,確保該位為1
                mtempData = Convert.ToInt32(data) | 2 ^ bit;
                data = Convert.ToByte(mtempData);
            }
            else
            {
                //data = data & Byte.MaxValue - 2 ^ bit; //取遮罩, 確保該位為0
                mtempData = data & Byte.MaxValue - 2 ^ bit;
                data = Convert.ToByte(mtempData);
            }

            Automation.BDaq.ErrorCode errorCode = InstantDoCtrl1.Write(port, data);
            if (errorCode != ErrorCode.Success)
            {
                MDateLog.gSyslog.Save(MDateLog.gMsgHandler.GetMessage(EqpID.Error_1005003), "Error_1005003", eMessageLevel.Error);
                MDateLog.gSyslog.Save("Exception Message: " + errorCode, "", eMessageLevel.Error); //wenda待確認 errorCode怎麼轉16進位
            }

            return true;
        }
    }
}
