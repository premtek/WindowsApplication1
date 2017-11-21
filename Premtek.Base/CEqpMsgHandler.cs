using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectCore;
using System.Data.SqlClient;

namespace Premtek.Base
{

    /// <summary>
    /// 錯誤訊息歷史紀錄
    /// </summary>
    /// <remarks></remarks>
    public struct sAlarmHistory
    {
        /// <summary>[發生時間]</summary>
        /// <remarks></remarks>
        public System.DateTime DateTime;
        /// <summary>[發生函式]</summary>
        /// <remarks></remarks>
        public string FunctionName;
        /// <summary>[發生步驟]</summary>
        /// <remarks></remarks>
        public string SysNum;
        /// <summary>[錯誤代碼]</summary>
        /// <remarks></remarks>
        public string ALID;
        /// <summary>[錯誤訊息]</summary>
        /// <remarks></remarks>
        public string AlarmString;
    }

    /// <summary>
    /// 設備訊息處理
    /// </summary>
    /// <remarks></remarks>
    public class CEqpMsgHandler
    {

        /// <summary>最新狀態(以最嚴重為主)</summary>
        /// <remarks></remarks>

        public eMessageLevel MsgLevel;
        /// <summary>[錯誤訊息列表]</summary>
        /// <remarks></remarks>

        public List<sAlarmHistory> AlarmList = new List<sAlarmHistory>();
        public event OnAlarmCanPauseEventHandler OnAlarmCanPause;
        public delegate void OnAlarmCanPauseEventHandler(object sender, EventArgs e);

        /// <summary>Alarm顯示在DataGridView中,如有相同代碼將不顯示</summary>
        /// <param name="funcName"></param>
        /// <param name="alid"></param>
        /// <param name="msgLevel"></param>
        /// <param name="sysNum"></param>
        /// <remarks></remarks>
        public void Add(string funcName, int alid, eMessageLevel msgLevel, string sysNum = "0000")
        {
            AddHistoryAlarm(alid.ToString(), funcName, sysNum, MDateLog.gMsgHandler.GetMessage(alid), msgLevel);
        }

        /// <summary>訊息顯示與記錄</summary>
        /// <param name="strAlarmCode">ALID</param>
        /// <param name="LoopName">發生函式名稱</param>
        /// <param name="Sys_Num">系統步驟編號</param>
        /// <param name="strLog">要記錄的Log</param>
        /// <param name="msgLevel">訊息等級</param>
        /// <remarks></remarks>

        public void AddHistoryAlarm(string strAlarmCode, string LoopName, string Sys_Num = "0000", string strLog = "", eMessageLevel msgLevel = eMessageLevel.Alarm)
        {
            bool mIsAlarmExist = false;
            sAlarmHistory tmp = default(sAlarmHistory);
            string mAlamID = null;

            if (strAlarmCode == null) strAlarmCode = "0000";
            

            if (strAlarmCode.Contains("_"))
            {
                mAlamID = strAlarmCode.Split(new string[] { "_" }, StringSplitOptions.None)[1];
            }
            else
            {
                mAlamID = strAlarmCode;
            }

            //[說明]:同一異常不要一直顯示
            //[說明]:先判斷有無相同的Alarm
            if (AlarmList.Count > 0)
            {
                foreach (sAlarmHistory p in AlarmList)
                {
                    //[Note]:檢查List 有無相同 AlarmCode
                    if (p.FunctionName == LoopName & p.SysNum == Sys_Num & p.ALID == mAlamID & p.AlarmString == strLog)
                    {
                        mIsAlarmExist = true;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
            }
            else
            {
                mIsAlarmExist = false;
            }

            if (mIsAlarmExist == false)
            {
                switch (msgLevel)
                {
                    //Soni + 2016.09.09 訊息等級(for燈號..etc)
                    case eMessageLevel.Error:
                        this.MsgLevel = eMessageLevel.Error;
                        //最高異警
                        break;

                    case eMessageLevel.Alarm:
                        if (this.MsgLevel == eMessageLevel.Error)
                        {
                        }
                        else
                        {
                            this.MsgLevel = eMessageLevel.Alarm;
                            //次高異警
                        }

                        break;
                    case eMessageLevel.Warning:
                        if (this.MsgLevel == eMessageLevel.Error)
                        {
                        }
                        else if (this.MsgLevel == eMessageLevel.Alarm)
                        {
                        }
                        else
                        {
                            this.MsgLevel = eMessageLevel.Warning;
                        }

                        break;
                    case eMessageLevel.Information:
                        //訊息提示不呈現
                        this.MsgLevel = eMessageLevel.Information;
                        break;
                    case eMessageLevel.Running:
                        this.MsgLevel = eMessageLevel.Running;

                        break;
                    case eMessageLevel.Idle:
                        this.MsgLevel = eMessageLevel.Idle;

                        break;
                }

                //[Note]:記錄跳出的訊息
                MDateLog.gSyslog.Save(strLog, strAlarmCode, msgLevel);

                //[Note]:加入Alarm Code
                tmp.DateTime = DateTime.Now;
                tmp.FunctionName = LoopName;
                tmp.SysNum = Sys_Num;
                tmp.ALID = mAlamID;
                tmp.AlarmString = strLog;
                AlarmList.Add(tmp);
            }

            //[說明]:只要一有Alarm，就必須要能暫停，終止流程
            if (OnAlarmCanPause != null)
            {
                OnAlarmCanPause(this, null);
            }

        }

        /// <summary>
        /// 顯示警告訊息
        /// </summary>
        /// <param name="cboAlarmMessage"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool ShowAlarm(ref ComboBox cboAlarmMessage)
        {

            sAlarmHistory sAlarm = default(sAlarmHistory);
            string strGetString = null;

            //[說明]:只要有新的Alarm進來就更新
            if (AlarmList.Count > 0)
            {
                if (cboAlarmMessage.Items.Count == AlarmList.Count)
                {
                    //if ((enmRunStatus)MSystemParameter.gSYS[MCommonDefine.eSys.OverAll].RunStatus == enmRunStatus.Running & (eSysCommand)MSystemParameter.gSYS[MCommonDefine.eSys.OverAll].ExecuteCommand == eSysCommand.Home)
                    //{
                    //    AlarmList.Clear();
                    //    cboAlarmMessage.Items.Clear();
                    //    cboAlarmMessage.Text = "";
                    //}
                }
                else
                {
                    cboAlarmMessage.Items.Clear();
                    for (int i = 0; i <= AlarmList.Count - 1; i++)
                    {
                        sAlarm = AlarmList[i];
                        strGetString = " [" + sAlarm.FunctionName + "] " + " [" + sAlarm.SysNum + "] " + " [" + sAlarm.ALID + "] " + " [" + sAlarm.AlarmString + "] ";
                        //加入Alarm Code
                        cboAlarmMessage.Items.Add(strGetString);
                        cboAlarmMessage.Text = strGetString;
                    }
                    if (OnAlarmCanPause != null)
                    {
                        OnAlarmCanPause(this, null);
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 清除警告訊息重複比對表
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool ClearAlarmCmpTable(ref ComboBox cboAlarmMessage, bool cleanMessage = false)
        {
            AlarmList.Clear();
            //20171114
            if (cleanMessage == false)
            {
                cboAlarmMessage.Items.Clear();
                cboAlarmMessage.Text = "";
            };
            this.MsgLevel = eMessageLevel.Idle;
            return true;
        }

    }
}
