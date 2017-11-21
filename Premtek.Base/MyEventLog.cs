using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Timers;

namespace Premtek.Base
{
    public class MyEventLog : System.Threading.SynchronizationContext
    {
        #region "[Private] Variable"

        private Task mEventSendTask;
        private CancellationTokenSource mEventSendTaskTokenSource;
        private CancellationToken mEventSendTaskToken;
        private System.Timers.Timer AutoRecordInterval = new System.Timers.Timer();
        private readonly Queue<string> messages = new Queue<string>();

        private readonly object syncHandle = new object();
        private bool isRunning = true;
        private bool isTimerTrigger = false;
        private int WriteMessageCount = 100;
        private int FileMessageCount = 1000;
        private int AutoRecordIntervalTime = 5000;

        private string strSaveLogPath = string.Empty;

        public bool IsEnable = true;
        #endregion


        public MyEventLog(string SaveLogPath)
        {
            if ((!Directory.Exists(SaveLogPath)))
            {
                Directory.CreateDirectory(SaveLogPath);
            }
            strSaveLogPath = SaveLogPath;

            AutoRecordInterval.Interval = AutoRecordIntervalTime;


            AutoRecordInterval.Elapsed += new ElapsedEventHandler((obj, tar) =>
            {
                lock ((syncHandle))
                {
                    isTimerTrigger = true;
                    SignalContinue();
                }


            });

            mEventSendTaskTokenSource = new CancellationTokenSource();
            mEventSendTaskToken = mEventSendTaskTokenSource.Token;
            mEventSendTask = new Task(() => { MyEventSendThread(mEventSendTaskToken); }, mEventSendTaskToken);
            mEventSendTask.Start();
            AutoRecordInterval.Start();
        }


        public void ManualDispose()
        {
            Cancel();
            AutoRecordInterval.Enabled = false;
            mEventSendTaskTokenSource.Cancel();

        }


        public void Log(string strMessage)
        {
            if ((IsEnable))
            {
                lock ((syncHandle))
                {
                    string TotalString = string.Format("{0} , {1}", System.DateTime.Now.ToString("yyyy/MM/dd , HH:mm:ss:fff "), strMessage);
                    messages.Enqueue(TotalString);
                    SignalContinue();
                }
            }
        }

        private void SignalContinue()
        {
            Monitor.Pulse(syncHandle);
        }

        private bool CanContinue()
        {
            lock ((syncHandle))
            {
                return isRunning;
            }
        }

        public void Cancel()
        {
            lock ((syncHandle))
            {
                isRunning = false;
                SignalContinue();
            }
        }


        private void MyEventSendThread(CancellationToken _Token)
        {
            while ((!_Token.IsCancellationRequested))
            {
                RunMessagePump();
            }

            if ((_Token.IsCancellationRequested))
            {
            }
        }

        private Queue<string> GrabItem()
        {

            lock ((syncHandle))
            {

                while ((CanContinue() & messages.Count < WriteMessageCount))
                {
                    Monitor.Wait(syncHandle);
                    if ((isTimerTrigger))
                    {
                        break; // TODO: might not be correct. Was : Exit While
                    }

                }

                AutoRecordInterval.Stop();
                isTimerTrigger = false;
                AutoRecordInterval.Start();

                Queue<string> qMessageBuffer = new Queue<string>();

                if ((messages.Count == 0))
                {
                    qMessageBuffer = null;
                }
                else
                {
                    while ((messages.Count > 0))
                    {
                        qMessageBuffer.Enqueue(messages.Dequeue());
                    }
                }
                return qMessageBuffer;
            }

        }

        private void RunMessagePump()
	{

		while ((CanContinue())) {
			Queue<string> vectorMessage = GrabItem();

			if (((vectorMessage != null) && vectorMessage.Count > 0)) {
				string[] FileGroup = Directory.GetFiles(strSaveLogPath);
				long MaxCount = 0;

				foreach (string item in FileGroup)
                {
					string strGetFile = Path.GetFileNameWithoutExtension(item);
					if ((System.Text.RegularExpressions.Regex.IsMatch(strGetFile, "^Log_\\d{1,5}$"))) {
						string GetNumString = strGetFile.Replace("Log_", "");
						long inum = 0;
						if ((Int64.TryParse(GetNumString, out inum))) {
							if ((inum > MaxCount)) {
								MaxCount = inum;
							}
						}
					}
				}

				if ((MaxCount == 0)) {
					MaxCount = 1;
				}
				string SaveLogFileName = string.Format("{0}\\\\Log_{1}.txt", strSaveLogPath, MaxCount.ToString());
				int FileRecordCount = 0;


				while ((vectorMessage.Count > 0)) {

					if ((File.Exists(SaveLogFileName))) {
						using (StreamReader Open = new StreamReader(SaveLogFileName)) {
							while (((Open.ReadLine() != null))) {
								FileRecordCount = FileRecordCount + 1;
							}
							Open.Close();
						}
					}

					int iNeedRecordMessageCount = FileMessageCount - FileRecordCount;


					if ((iNeedRecordMessageCount > 0)) {
					}

					using (StreamWriter Write = File.AppendText(SaveLogFileName)) {
						while ((iNeedRecordMessageCount > 0 & vectorMessage.Count > 0)) {
							iNeedRecordMessageCount = iNeedRecordMessageCount - 1;
							Write.WriteLine(vectorMessage.Dequeue());
						}
						Write.Close();
					}
					FileRecordCount = 0;
					MaxCount = MaxCount + 1;
					SaveLogFileName = string.Format("{0}\\\\Log_{1}.txt", strSaveLogPath, MaxCount.ToString());

				}

			}


		}
	}

    }
}



//Eason Ticket Define:
//100010 , Add Pattern Copy Function
//100011 , Bug fix

//100030 , Memory Free
//100031 , Memory Free Part2
//100032 , Memory Free Part3
//100033 , Memory Free Part4
//
//100050 , Add Jet Time
//100060 , Memory Log
//100070 , Operator form frash
//100080 , Add Arc Type Parameter

//100090 , System Update Crash
//100100 , XY Offset from CSV File
