using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Threading.Tasks;
using ProjectCore;
using Premtek.Base;
namespace Premtek.Base
{

	/// <summary>觸發電路板</summary>
	/// <remarks></remarks>
	public static class MCommonTriggerBoard
	{

		#region "Enum 、Structure"

		/// <summary>[點膠圖樣(給Trigger Board)]</summary>
		/// <remarks></remarks>
		public enum eTriggerBoardPathType
		{
			/// <summary>[點]</summary>
			/// <remarks></remarks>
			Dot = 0,
			/// <summary>[線]</summary>
			/// <remarks></remarks>
			Line = 1,
			/// <summary>[弧]</summary>
			/// <remarks></remarks>
			Arc = 2
		}

		/// <summary>[圓弧方向]</summary>
		/// <remarks></remarks>
		public enum eArcDir
		{
			/// <summary>[順時針]</summary>
			/// <remarks></remarks>
			CW = 0,
			/// <summary>[逆時針]</summary>
			/// <remarks></remarks>
			CCW = 1
		}

		public struct sReceiveStatus
		{
			/// <summary>[狀態]</summary>
			/// <remarks></remarks>
			public bool Status;
			/// <summary>[字串]</summary>
			/// <remarks></remarks>
			public string STR;
			/// <summary>[結果(處理完的資料內容)]</summary>
			/// <remarks></remarks>
			public string Value;
		}

		/// <summary>[落點估測參數(供Trigger board做落點分析用)(J Command)]</summary>
		/// <remarks></remarks>
		public struct sTriggerJCmdParam
		{
			/// <summary>[Jet Time(us)]</summary>
			/// <remarks></remarks>
			public decimal JetTime;
			/// <summary>[Tolerance(um)]</summary>
			/// <remarks></remarks>
			public decimal Tolerance;
			/// <summary>[Measure Length(um)]</summary>
			/// <remarks></remarks>
			public decimal MeasureLength;
			/// <summary>[Measure Pitch(um)]</summary>
			/// <remarks></remarks>
			public decimal MeasurePitch;
		}

		/// <summary>[路徑資料與閥體參數 (J Command)]</summary>
		/// <remarks></remarks>
		public struct sTriggerJCmdStep
		{
			/// <summary>[Glue Pressure (*1000)]</summary>
			/// <remarks></remarks>
			public decimal GluePressure;
			/// <summary>[Jet Pressure(*1000)]</summary>
			/// <remarks></remarks>
			public decimal JetPressure;
			/// <summary>[Pulse Time (us)]</summary>
			/// <remarks></remarks>
			public decimal PulseTime;
			/// <summary>[Falling Velocity(0~100%)]</summary>
			/// <remarks></remarks>
			public decimal FallingVelocity;
			/// <summary>[Stroke(0~100%)]</summary>
			/// <remarks></remarks>
			public decimal Stroke;
			/// <summary>[圖形樣式]</summary>
			/// <remarks></remarks>
			public eTriggerBoardPathType Path;
			/// <summary>[起點座標X] </summary>
			/// <remarks></remarks>
			public decimal StartPosX;
			/// <summary>[起點座標Y] </summary>
			/// <remarks></remarks>
			public decimal StartPosY;
			/// <summary>[起點座標Z] </summary>
			/// <remarks></remarks>
			public decimal StartPosZ;
			/// <summary>[終點座標X] </summary>
			/// <remarks></remarks>
			public decimal EndPosX;
			/// <summary>[終點座標Y] </summary>
			/// <remarks></remarks>
			public decimal EndPosY;
			/// <summary>[終點座標Z] </summary>
			/// <remarks></remarks>
			public decimal EndPosZ;
			/// <summary>[圓心座標X] </summary>
			/// <remarks></remarks>
			public decimal CenPosX;
			/// <summary>[圓心座標Y] </summary>
			/// <remarks></remarks>
			public decimal CenPosY;
			/// <summary>[圓心座標Z] </summary>
			/// <remarks></remarks>
			public decimal CenPosZ;
			/// <summary>[方向]</summary>
			/// <remarks></remarks>
			public eArcDir Dir;
			/// <summary>[Dot數量] </summary>
			/// <remarks></remarks>
			public decimal DotCounts;
			/// <summary>[Open Time(us)]</summary>
			/// <remarks></remarks>
			public int OpenTime;
			/// <summary>[Close Time(us)]</summary>
			/// <remarks></remarks>
			public int CloseTime;
			/// <summary>[Close Voltage]</summary>
			/// <remarks></remarks>
			public decimal CloseVoltage;
		}
		/// <summary>[落點估測參數(供Trigger board做落點分析用)(H Command)]</summary>
		/// <remarks></remarks>
		public struct sTriggerLaserCmdParam
		{
			/// <summary>[取像總數]</summary>
			/// <remarks></remarks>
			public decimal TotalPointCounts;
			/// <summary>[助跑座標X(um)]</summary>
			/// <remarks></remarks>
			public decimal ApproachPosX;
			/// <summary>[助跑座標Y(um)]</summary>
			/// <remarks></remarks>
			public decimal ApproachPosY;
			/// <summary>[延遲時間(ms)]</summary>
			/// <remarks></remarks>
			public decimal DelayTime;
		}

		/// <summary>[路徑資料與視覺參數(H Command)]</summary>
		/// <remarks></remarks>
		public struct sTriggerLaserCmdStep
		{
			/// <summary>[圖形樣式]</summary>
			/// <remarks></remarks>
			public eTriggerBoardPathType Path;
			/// <summary>[方向]</summary>
			/// <remarks></remarks>
			public eArcDir Dir;
			/// <summary>[Stage移動速度]</summary>
			/// <remarks></remarks>
			public decimal Velocity;
			/// <summary>[取像數量] </summary>
			/// <remarks></remarks>
			public decimal PointCounts;
			/// <summary>[起點座標X] </summary>
			/// <remarks></remarks>
			public decimal StartPosX;
			/// <summary>[起點座標Y] </summary>
			/// <remarks></remarks>
			public decimal StartPosY;
			/// <summary>[終點座標X] </summary>
			/// <remarks></remarks>
			public decimal EndPosX;
			/// <summary>[終點座標Y] </summary>
			/// <remarks></remarks>
			public decimal EndPosY;
			/// <summary>[圓心座標X] </summary>
			/// <remarks></remarks>
			public decimal CenPosX;
			/// <summary>[圓心座標Y] </summary>
			/// <remarks></remarks>
			public decimal CenPosY;
			/// <summary>[隔多久的時間才會走到下一條線段]</summary>
			/// <remarks></remarks>
			public decimal PathWaitTime;
		}
		/// <summary>[落點估測參數(供Trigger board做落點分析用)(L Command)]</summary>
		/// <remarks></remarks>
		public struct sTriggerVisionCmdParam
		{
			/// <summary>[取像總數]</summary>
			/// <remarks></remarks>
			public decimal TotalPointCounts;
			/// <summary>[助跑座標X(um)]</summary>
			/// <remarks></remarks>
			public decimal ApproachPosX;
			/// <summary>[助跑座標Y(um)]</summary>
			/// <remarks></remarks>
			public decimal ApproachPosY;
			/// <summary>[延遲時間(ms)]</summary>
			/// <remarks></remarks>
			public decimal DelayTime;
		}

		/// <summary>[路徑資料與視覺參數(L Command)]</summary>
		/// <remarks></remarks>
		public struct sTriggerVisionCmdStep
		{
			/// <summary>[圖形樣式]</summary>
			/// <remarks></remarks>
			public eTriggerBoardPathType Path;
			/// <summary>[方向]</summary>
			/// <remarks></remarks>
			public eArcDir Dir;
			/// <summary>[Stage移動速度]</summary>
			/// <remarks></remarks>
			public decimal Velocity;
			/// <summary>[取像數量] </summary>
			/// <remarks></remarks>
			public decimal PointCounts;
			/// <summary>[起點座標X] </summary>
			/// <remarks></remarks>
			public decimal StartPosX;
			/// <summary>[起點座標Y] </summary>
			/// <remarks></remarks>
			public decimal StartPosY;
			/// <summary>[終點座標X] </summary>
			/// <remarks></remarks>
			public decimal EndPosX;
			/// <summary>[終點座標Y] </summary>
			/// <remarks></remarks>
			public decimal EndPosY;
			/// <summary>[圓心座標X] </summary>
			/// <remarks></remarks>
			public decimal CenPosX;
			/// <summary>[圓心座標Y] </summary>
			/// <remarks></remarks>
			public decimal CenPosY;
			/// <summary>[隔多久的時間才會走到下一條線段]</summary>
			/// <remarks></remarks>
			public decimal PathWaitTime;
		}

		/// <summary>[落點估測參數(供Trigger board做落點分析用)(F Command)]</summary>
		/// <remarks></remarks>
		public struct sTriggerFCmdParam
		{
			/// <summary>[打點總數]</summary>
			/// <remarks></remarks>
			public decimal TotalDotCounts;
			/// <summary>[助跑座標X(um)]</summary>
			/// <remarks></remarks>
			public decimal ApproachPosX;
			/// <summary>[助跑座標Y(um)]</summary>
			/// <remarks></remarks>
			public decimal ApproachPosY;
			// ''' <summary>[隔多久的時間才會走到下一條線段(us)]</summary>
			// ''' <remarks></remarks>
			//Public WaitTime As Decimal
		}

		/// <summary>[路徑資料與閥體參數(F Command)]</summary>
		/// <remarks></remarks>
		public struct sTriggerFCmdStep
		{
			/// <summary>[圖形樣式]</summary>
			/// <remarks></remarks>
			public eTriggerBoardPathType Path;
			/// <summary>[方向]</summary>
			/// <remarks></remarks>
			public eArcDir Dir;
			/// <summary>[Stage移動速度]</summary>
			/// <remarks></remarks>
			public decimal Velocity;
			/// <summary>[Dot數量] </summary>
			/// <remarks></remarks>
			public decimal DotCounts;
			/// <summary>[起點座標X] </summary>
			/// <remarks></remarks>
			public decimal StartPosX;
			/// <summary>[起點座標Y] </summary>
			/// <remarks></remarks>
			public decimal StartPosY;
			/// <summary>[終點座標X] </summary>
			/// <remarks></remarks>
			public decimal EndPosX;
			/// <summary>[終點座標Y] </summary>
			/// <remarks></remarks>
			public decimal EndPosY;
			/// <summary>[圓心座標X] </summary>
			/// <remarks></remarks>
			public decimal CenPosX;
			/// <summary>[圓心座標Y] </summary>
			/// <remarks></remarks>
			public decimal CenPosY;
			/// <summary>[隔多久的時間才會走到下一條線段]</summary>
			/// <remarks></remarks>
			public decimal PathWaitTime;
		}

		/// <summary>[閥體參數(供Trigger board做落點分析用)]</summary>
		/// <remarks></remarks>
		public struct sTriggerGCmdParam
		{
			/// <summary>[Head No]</summary>
			/// <remarks></remarks>
			public int HeadNo;
			/// <summary>[Pulse Time (us)]</summary>
			/// <remarks></remarks>
			public int PulseTime;
			/// <summary>[Jet Time(us)]</summary>
			/// <remarks></remarks>
			public int JetTime;
			/// <summary>[Stroke(0~100%)]</summary>
			/// <remarks></remarks>
			public int Stroke;
			/// <summary>[Glue Pressure (*1000)]</summary>
			/// <remarks></remarks>
			public int GluePressure;
			/// <summary>[Tolerance(um)]</summary>
			/// <remarks></remarks>
			public int Tolerance;
			/// <summary>[Measure Length(um)]</summary>
			/// <remarks></remarks>
			public int MeasureLength;
			/// <summary>[Measure Pitch(um)]</summary>
			/// <remarks></remarks>
			public int MeasurePitch;
			/// <summary>[Measure Counts (次)]</summary>
			/// <remarks></remarks>
			public int MeasureCounts;
			/// <summary>[Jet Pressure(*1000)]</summary>
			/// <remarks></remarks>
			public int JetPressure;
			/// <summary>[Open Time(us)]</summary>
			/// <remarks></remarks>
			public int OpenTime;
			/// <summary>[Close Time(us)]</summary>
			/// <remarks></remarks>
			public int CloseTime;
			/// <summary>[Close Voltage(V)]</summary>
			/// <remarks></remarks>
			public int CloseVoltage;
			/// <summary>[Cycle Time(us)]</summary>
			/// <remarks></remarks>
			public int CycleTime;
		}

		/// <summary>[閥體參數(供Trigger board控制Valve)]</summary>
		/// <remarks></remarks>
		public struct sTriggerTPCmdParam
		{
			/// <summary>[Cycle Time(us)]</summary>
			/// <remarks></remarks>
			public int CycleTime;
			/// <summary>[Pitch(um)]</summary>
			/// <remarks></remarks>
			public int Pitch;
			/// <summary>[Dot Counts(ea)]</summary>
			/// <remarks></remarks>
			public int DotCounts;
			/// <summary>[Glue Pressure (*1000)]</summary>
			/// <remarks></remarks>
			public int GluePressure;
			/// <summary>[Jet Pressure (*1000)]</summary>
			/// <remarks></remarks>
			public int JetPressure;
			/// <summary>[Pulse Time (us)]</summary>
			/// <remarks></remarks>
			public int PulseTime;
			/// <summary>[Open Time(us)]</summary>
			/// <remarks></remarks>
			public int OpenTime;
			/// <summary>[Close Time(us)]</summary>
			/// <remarks></remarks>
			public int CloseTime;
			/// <summary>[Close Voltage(V)]</summary>
			/// <remarks></remarks>
			public int CloseVoltage;
			/// <summary>[Stroke(0~100%)]</summary>
			/// <remarks></remarks>
			public int Stroke;
		}

		public enum enmTriggerComdEndType
		{
			/// <summary>[非最後線段]</summary>
			/// <remarks></remarks>
			NonEndLine = 0,
			/// <summary>[此為最後線段] </summary>
			/// <remarks></remarks>
			EedLine = 1
		}

		/// <summary>[Trigger Board Dispensing Type]</summary>
		/// <remarks></remarks>
		public enum enmTriggerDispType
		{
			/// <summary>[J]</summary>
			/// <remarks></remarks>
			JetParamRecipe = 0,
			/// <summary>[I]</summary>
			/// <remarks></remarks>
			NeedleJetParamRecipe = 1,
			/// <summary>[A]</summary>
			/// <remarks></remarks>
			AugerParamRecipe = 2,
			/// <summary>[T(Cycle Time)]</summary>
			/// <remarks></remarks>
			CycleRecipe = 3,
			/// <summary>[P(Pitch)]</summary>
			/// <remarks></remarks>
			PitchRecipe = 4,
			/// <summary>[F(套用在相同的Valve Parameter)]</summary>
			/// <remarks></remarks>
			JetRecipe = 5,
			/// <summary>[Vision Recipe(L Command)]</summary>
			/// <remarks></remarks>
			VisionRecipe = 6
		}
		#endregion

        ///// <summary>[觸發控制器集合]</summary>
        ///// <remarks></remarks>
        //public static Premtek.Base.CTriggerBoardCollection gTriggerBoard = new Premtek.Base.CTriggerBoardCollection();
		/// <summary>[觸發版版本]</summary>
		/// <remarks></remarks>

		public static string[] gTriggerBoardVersion = new string[Convert.ToInt16(MCommonDefine.enmTriggerBoard.Max + 1)];
		/// <summary>[通訊異常後 允取再從送幾次]</summary>
		/// <remarks></remarks>

		public const int gTriggerCmdMaxFailCounts = 3;

        ///// <summary>[觸發板初始化]</summary>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //public static bool Initialize_TriggerBoard()
        //{
        //    gTriggerBoard.Load(Application.StartupPath + "\\System\\" +MCommonDefine.MachineName + "\\CardTriggerBoard.ini");
        //    if (gTriggerBoard.Initial(gTriggerBoard.TBConnectionParameter) == false) {
        //        MDateLog.gSyslog.Save("Initialized Trigger Board Failed!","" , eMessageLevel.Error);
        //        return false;
        //    } else {
        //        return true;
        //    }

        //}

        ///// <summary>[關閉觸發板]</summary>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //public static bool Close_TriggerBoard()
        //{
        //    return gTriggerBoard.Close();
        //}


	}
}
