using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Windows.Forms;
using ProjectCore;

namespace Premtek.Base
{
    // 摘要:
    //     [Language Type]
    public enum enmLanguageType
    {
        // 摘要:
        //     [英文]
        eEnglish = 0,
        //
        // 摘要:
        //     [繁中]
        eTraditionalChinese = 1,
        //
        // 摘要:
        //     [簡中]
        eSimplifiedChinese = 2,
    }

    /// <summary>
    /// 訊息結構
    /// </summary>
    /// <remarks></remarks>
    public struct MessageStructure
    {
        /// <summary>
        /// 程式內部名稱
        /// </summary>
        /// <remarks></remarks>
        public string ProgramID;
        /// <summary>
        /// 錯誤代碼
        /// </summary>
        /// <remarks></remarks>
        public string ALID;
        /// <summary>
        /// ITRI參數???
        /// </summary>
        /// <remarks></remarks>
        public string CD;
        /// <summary>
        /// 是否使用
        /// </summary>
        /// <remarks></remarks>
        public string Enabled;
        /// <summary>語系訊息1</summary>
        /// <remarks></remarks>
        public string Msg1;
        /// <summary>語系訊息2</summary>
        /// <remarks></remarks>
        public string Msg2;
        /// <summary>語系訊息3</summary>
        /// <remarks></remarks>
        public string Msg3;
        // ''' <summary>
        // ''' 警報層級
        // ''' </summary>
        // ''' <remarks></remarks>
        //Public Level As String
    }

    /// <summary>多語系處理</summary>
    /// <remarks></remarks>
    public class CMsgHandler : IDisposable
    {

        /// <summary>文字檔讀取工具</summary>
        /// <remarks></remarks>
        System.IO.StreamReader mStreamReader;
        /// <summary>
        /// 統整訊息字典
        /// </summary>
        /// <remarks></remarks>

        public Dictionary<string, MessageStructure> MsgDictionary = new Dictionary<string, MessageStructure>();
        /// <summary>選擇語系(預設英文)</summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public enmLanguageType SelectedLanguage { get; set; }

        // ''' <summary>
        // ''' 讀取訊息層級設定
        // ''' </summary>
        // ''' <param name="fileName"></param>
        // ''' <remarks></remarks>
        //Public Function LoadLevel(ByVal fileName As String) As Boolean
        //    Try
        //        If Not System.IO.File.Exists(fileName) Then
        //            MsgBox("Msg Level File Lost...")
        //            Return False
        //        End If
        //        Dim mStreamReader As New System.IO.StreamReader(fileName, System.Text.Encoding.Unicode)
        //        Dim mData As String = ""
        //        Do
        //            mData = mStreamReader.ReadLine
        //            If mData Is Nothing Then
        //                MsgBox("Msg Level File is Empty...")
        //                Return False
        //            End If
        //            If Not mData.StartsWith("//") Then '不是標頭
        //                Dim pattern2 As String = "(?:^|,)(?=[^""]|("")?)""?((?(1)[^""]*|[^,""]*))""?(?=,|$)"
        //                Dim mRegEx As New Regex(pattern2) 'Compiled速度會更慢..., System.Text.RegularExpressions.RegexOptions.Compiled + RegexOptions.Singleline + RegexOptions.IgnoreCase)
        //                Dim matches As MatchCollection = mRegEx.Matches(mData)
        //                If matches.Count > 1 Then
        //                    Dim msgID As String = matches(0).Value.TrimStart(",")
        //                    Dim level As String = matches(1).Value.TrimStart(",")
        //                    If MsgDictionary.ContainsKey(msgID) Then
        //                        Dim tmp As MessageStructure = MsgDictionary[msgID]
        //                        tmp.Level = level
        //                        MsgDictionary[msgID] = tmp
        //                    End If

        //                End If
        //            End If
        //        Loop Until mStreamReader.EndOfStream
        //        mStreamReader.Close()
        //        Return True
        //    Catch ex As Exception
        //        MsgBox(ex.Message, vbOKOnly)
        //        Return False
        //    End Try
        //End Function

        // ''' <summary>
        // ''' 寫入訊息層級設定
        // ''' </summary>
        // ''' <param name="fileName"></param>
        // ''' <returns></returns>
        // ''' <remarks></remarks>
        //Public Function SaveLevel(ByVal fileName As String) As Boolean
        //    Try
        //        Dim mStreamWriter As New System.IO.StreamWriter(fileName, False, System.Text.Encoding.Unicode)
        //        Dim mData As String = ""
        //        For mRow As Integer = 0 To MsgDictionary.Keys.Count - 1
        //            Dim ALID As String = MsgDictionary.Keys(mRow)
        //            Dim level As String = ""
        //            Select Case MsgDictionary(ALID).Level
        //                Case "Heavy", "Alarm"
        //                    level = "Alarm"
        //                Case "Light", "Warn"
        //                    level = "Warn"
        //            End Select
        //            mData += ALID & "," & MsgDictionary(ALID).Level & vbCrLf
        //        Next
        //        mStreamWriter.WriteLine(mData) '整批寫入
        //        mStreamWriter.Close()
        //        Return True
        //    Catch ex As Exception
        //        MsgBox(ex.Message, vbOKOnly)
        //        Return False
        //    End Try
        //End Function

        /// <summary>讀取訊息語系設定</summary>
        /// <param name="fileName"></param>
        /// <remarks></remarks>
        public bool Load(string fileName)
	{
		string data = "";

		try {
			if (!System.IO.File.Exists(fileName)) {
				MessageBox.Show("Multi-Language File Lost...");
				return false;
			}
			//CSV檔案開啟只支援ANSI,Big5格式
			//StreamReader開ANSI會變亂碼.
			mStreamReader = new System.IO.StreamReader(fileName, System.Text.Encoding.Unicode);


			do {
				data = mStreamReader.ReadLine();
				if (data == null) {
				//不是標頭
				} else if (!data.StartsWith("//")) {
					//string pattern = "[^\",]+|\"(?:[^\"]|\")*";
					string pattern2 = "(?:^|,)(?=[^\"]|(\")?)\"?((?(1)[^\"]*|[^,\"]*))\"?(?=,|$)";
					Regex mRegEx = new Regex(pattern2);
					//Compiled速度會更慢..., System.Text.RegularExpressions.RegexOptions.Compiled + RegexOptions.Singleline + RegexOptions.IgnoreCase)
					MatchCollection matches = mRegEx.Matches(data);
					//Dim splitedData() As String = data.Split(",")
					string progID = matches[0].Value.TrimStart(',');
					string msgID = matches[1].Value.TrimStart(',');
					//CD
					string enabled = matches[3].Value.TrimStart(',');
					string msg1 = "";
					string msg2 = "";
					string msg3 = "";
					if (matches.Count > 4) {
						msg1 = matches[4].Value.TrimStart(',');
					}
					if (matches.Count > 5) {
						msg2 = matches[5].Value.TrimStart(',');
					}
					if (matches.Count > 6) {
						msg3 = matches[6].Value.TrimStart(',');
					}
					if (MsgDictionary.ContainsKey(msgID)) {
						MessageStructure tmp = MsgDictionary[msgID];
						tmp.ProgramID = progID;
						tmp.ALID = msgID;
						tmp.Enabled = enabled;
						tmp.Msg1 = msg1;
						tmp.Msg2 = msg2;
						tmp.Msg3 = msg3;
						MsgDictionary[msgID] = tmp;
					} else {
						MessageStructure tmp = new MessageStructure();
						tmp.ProgramID = progID;
						tmp.ALID = msgID;
						tmp.Enabled = enabled;
						tmp.Msg1 = msg1;
						tmp.Msg2 = msg2;
						tmp.Msg3 = msg3;
						MsgDictionary.Add(msgID, tmp);
					}

				}

			} while (!(mStreamReader.EndOfStream));
			
			mStreamReader.Close();
			return true;
		} catch (Exception ex) {
		MessageBox.Show(data + "\r\n" + ex.Message);
		MDateLog.	gSyslog.Save("CMsgHandler. Exception Message:" + ex.Message, "", eMessageLevel.Alarm);
			return false;
		}

	}

        /// <summary>儲存語系設定檔</summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool Save(string fileName)
        {
            try
            {
                System.IO.StreamWriter mStreamWriter = new System.IO.StreamWriter(fileName, false, System.Text.Encoding.Unicode);
                string mData = "// Name,ID,CD,Enabled,Text,Cht,,";
                //& vbCrLf
                mStreamWriter.WriteLine(mData);
                //寫入
                for (int mRow = 0; mRow <= MsgDictionary.Keys.Count - 1; mRow++)
                {
                    string ALID = MsgDictionary.Keys.ToList()[mRow];
                  
                    mData = MsgDictionary[ALID].ProgramID + "," + ALID + ",," + MsgDictionary[ALID].Enabled + "," + MsgDictionary[ALID].Msg1 + "," + MsgDictionary[ALID].Msg2 + "," + MsgDictionary[ALID].Msg3 + ",";
                    mStreamWriter.WriteLine(mData);
                    //寫入
                }

                mStreamWriter.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show (ex.Message,"",MessageBoxButtons.OK);
                return false;
            }
        }

        /// <summary>取得訊息</summary>
        /// <param name="msgId">訊息代碼</param>
        /// <param name="arg">參數, 如果有的話</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string GetMessage(string msgId, params string[] arg)
        {
            switch (SelectedLanguage)
            {
                case enmLanguageType.eEnglish:
                    if (MsgDictionary.ContainsKey(msgId))
                    {
                        return string.Format(MsgDictionary[msgId].Msg1, arg);
                    }
                    else
                    {
                        return "";
                    }

                case enmLanguageType.eTraditionalChinese:
                    if (MsgDictionary.ContainsKey(msgId))
                    {
                        return string.Format(MsgDictionary[msgId].Msg2, arg);
                    }
                    else
                    {
                        return "";
                    }

                case enmLanguageType.eSimplifiedChinese:
                    if (MsgDictionary.ContainsKey(msgId))
                    {
                        return string.Format(MsgDictionary[msgId].Msg3, arg);
                    }
                    else
                    {
                        return "";
                    }
              
            }
            return "";
        }

        //Eason 20170228
        public CMsgHandler()
        {
            System.Threading.ThreadPool.SetMinThreads(150, 150);
            //Eason 20170228
        }

        #region "IDisposable Support"
        // 偵測多餘的呼叫
        private bool disposedValue;

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    mStreamReader.Dispose();
                }
            }
            this.disposedValue = true;
        }

        // 由 Visual Basic 新增此程式碼以正確實作可處置的模式。
        public void Dispose()
        {
            // 請勿變更此程式碼。在以上的 Dispose 置入清除程式碼 (視為布林值處置)。
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
