using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.BDaq;
using ProjectCore;

namespace Premtek
{

    public class CDI_PCI_1710 : IDIInterface
    {
        /// <summary>埠數/卡</summary>
        /// <remarks></remarks>
        public int PortPerCard { get; set; }


        /// <summary>點數/埠</summary>
        /// <remarks></remarks>
        public int BitsPerPort { get; set; }
        

        internal Automation.BDaq.InstantDiCtrl InstantDiCtrl1 = new Automation.BDaq.InstantDiCtrl();
        public CDI_PCI_1710()
        {
            this.PortPerCard = 2;
            this.BitsPerPort = 8;
        }

        /// <summary>y資源釋放</summary>
        /// <remarks></remarks>
        public void Close()
        {
            InstantDiCtrl1.Dispose();
        }
        /// <summary>初始化</summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool Initial(string deviceDescription)
        {
            try
            {
                InstantDiCtrl1.SelectedDevice = new DeviceInformation(deviceDescription);
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

        /// <summary>資料批次讀取</summary>
        /// <param name="port"></param>
        /// <param name="bytDiData"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int Read(int port, ref byte bytDiData)
        {
            byte _Data;
            Automation.BDaq.ErrorCode errorCode = InstantDiCtrl1.Read(port, out _Data);
            bytDiData = _Data;
            return (int)errorCode;
        }
    }

}
