using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using ProjectCore;
//using ProjectIO;
namespace Premtek.Base
{

	public class CLaserReader_KeyenceILS065Voltage : ILaserReader
	{


		public bool ChangeProgram(int ProgramID)
		{
            MDateLog.gSyslog.Save("ChangeProgram Function Not Supported.");
			return true;
		}

		public bool Close()
		{
            MDateLog.gSyslog.Save("ChangeProgram Function Not Supported.");
			return true;
		}


		public bool EthernetOpen(string IP, int Port)
		{
            MDateLog.gSyslog.Save("EthernetOpen Function Not Supported.");
			return true;
		}

		public bool GetValue(string Mode, ref string value, int aiIndex = 0, bool waitReturn = false)
		{
            //TODO: 專案相依性異常 合併後再調整
            //value = MCommonIO.gAICollection.Value[aiIndex].ToString(); 
			return true;
		}

		public bool GetVersion(ref string Version)
		{
            MDateLog.gSyslog.Save("GetVersion Function Not Supported.");
			return true;
		}

		public bool RebootController()
		{
            MDateLog.gSyslog.Save("RebootController Function Not Supported.");
			return true;
		}

		public bool IsTimeOut {
			get { return false; }
		}

		public bool PortIsOpen {
			get { return true; }
		}

		public int TimeoutTimer { get; set; }

		public bool IsBusy {
			get { return false; }
		}

        bool ILaserReader.IsTimeOut
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        int ILaserReader.TimeoutTimer
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        bool ILaserReader.PortIsOpen
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool ILaserReader.IsBusy
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        MCommonDefine.sReceiveStatus ILaserReader.Result
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool Initial(string PortName, string BaudRate)
		{
			MDateLog.gSyslog.Save("Initial Function is Not Supported at CLaserReader_KeyenceILS065Voltage.");
			return false;
		}

        //Public Sub ResetState() Implements ILaserReader.ResetState

        //End Sub
        /// <summary>接收資料結果</summary>
        /// <remarks></remarks>
        MCommonDefine.sReceiveStatus[] mResult = new MCommonDefine.sReceiveStatus[4];
		public MCommonDefine.sReceiveStatus Result(int channelNo) {
			 return mResult[channelNo];
		}

        bool ILaserReader.EthernetOpen(string IP, int Port)
        {
            throw new NotImplementedException();
        }

        bool ILaserReader.Initial(string PortName, string BaudRate)
        {
            throw new NotImplementedException();
        }

        bool ILaserReader.Close()
        {
            throw new NotImplementedException();
        }

        bool ILaserReader.GetValue(string Mode, ref string data, int aiIndex, bool waitReturn)
        {
            throw new NotImplementedException();
        }

        bool ILaserReader.GetVersion(ref string Version)
        {
            throw new NotImplementedException();
        }

        bool ILaserReader.RebootController()
        {
            throw new NotImplementedException();
        }

        bool ILaserReader.ChangeProgram(int ProgramID)
        {
            throw new NotImplementedException();
        }
    }
}
