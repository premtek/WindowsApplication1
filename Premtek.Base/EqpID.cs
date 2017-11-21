using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Premtek.Base
{
   
public static class EqpID
{
	//'Alarm code 定義規則
	// 若不確定加什麼Alarm訊息，麻煩先用 "Error_1000002" 

	//項目           		Range

	//參數存取       		2000	2999
	//DIO卡	       		3000	3999
	//DI卡	       		4000	4999
	//DO卡	       		5000	5999
	//AIO卡			6000	6999
	//AI卡			7000	7999
	//AO卡			8000	8999
	//動控制卡		9000	9999
	//COM元件			10000	10999
	//網路元件		11000	11999
	//CCD			12000	12999
	//影像擷取卡		13000	13999
	//雷射干涉儀		14000	14999
	//微量天平		15000	15999
	//觸發板			16000	16999
	//FMCS			17000	17999
	//加熱器			18000	18999
	//噴射閥			19000	19999
	//螺桿閥			20000	20999
	//氣壓閥			21000	21999
	//程控光源		22000	22999
	//PLC			23000	23999
	//閥控制器		24000	24999
	//MAP			25000	25999
	//第一組平台X軸		30000	30999
	//第一組平台Y軸		31000	31999
	//第一組平台Z軸		32000	32999
	//第一組平台X2軸		33000	33999
	//第一組平台B軸(Tilt)	34000	34999
	//第一組平台C軸		35000	35999
	//第一組平台運動群組1	36000	36999
	//第一組傳送帶Conveyor1軸	37000	37999
	//第一組傳送帶Conveyor2軸	38000	38999
	//第一組傳送帶S1軸	39000	39999
	//第一組傳送帶S2軸	40000	40999
	//第一組傳送帶S3軸	41000	41999
	//第二組平台U軸(X軸)	42000	42999
	//第二組平台V軸(Y軸)	43000	43999
	//第二組平台W軸(Z軸)	44000	44999
	//第二組平台U2軸(X2軸)	45000	45999
	//第二組平台(B軸)(Tilt)	46000	46999
	//第二組平台F軸(C軸)	47000	47999
	//第二組平台運動群組2	48000	48999
	//第二組傳送帶Conveyor1軸	49000	49999
	//第二組傳送帶Conveyor2軸	50000	50999
	//第二組傳送帶S1軸	51000	51999
	//第二組傳送帶S2軸	52000	52999
	//第二組傳送帶S3軸	53000	53999
	//第三組平台R軸(X軸)	60000	60999
	//第三組平台S軸(Y軸)	61000	61999
	//第三組平台T軸(Z軸)	62000	62999
	//第三組平台R2軸(X2軸)	63000	63999
	//第三組平台(B軸)(Tilt)	64000	64999
	//第三組平台I軸(C軸)	65000	65999
	//第三組平台運動群組1	66000	66999
	//第四組平台O軸(X軸)	67000	67999
	//第四組平台P軸(Y軸)	68000	68999
	//第四組平台Q軸(Z軸)	69000	69999
	//第四組平台O2軸(X2軸)	70000	70999
	//第四組平台(B軸)(Tilt)	71000	71999
	//第四組平台L軸(C軸)	72000	72999
	//第四組平台運動群組1	73000	73999
	//整機			80000	80999
	//A機			81000	81999
	//B機			82000	82999
	//Conveyor1		83000	83999
	//Conveyor2		84000	84999







	/// <summary>Program Start</summary>
	/// <remarks></remarks>
		//Program Start
	public const int EQP_EVENT_4000001 = 4000001;
	/// <summary>Program End</summary>
	/// <remarks></remarks>
		//Program End
	public const  int EQP_EVENT_4000002 = 4000002;
	/// <summary>User Login</summary>
	/// <remarks></remarks>
		//User Login
	public const  int EQP_EVENT_4000003 = 4000003;
	/// <summary>User Logout</summary>
	/// <remarks></remarks>
		//User Logout
	public const  int EQP_EVENT_4000004 = 4000004;
	/// <summary>AutoRun Start</summary>
	/// <remarks></remarks>
		//AutoRun Start
	public const  int EQP_EVENT_4000005 = 4000005;
	/// <summary>AutoRun End</summary>
	/// <remarks></remarks>
		//AutoRun End
	public const  int EQP_EVENT_4000006 = 4000006;
	/// <summary>Calibration Start</summary>
	/// <remarks></remarks>
		//Calibration Start
	public const  int EQP_EVENT_4000007 = 4000007;
	/// <summary>Calibration End</summary>
	/// <remarks></remarks>
		//Calibration End
	public const  int EQP_EVENT_4000008 = 4000008;
	/// <summary>Recipe Edit Start</summary>
	/// <remarks></remarks>
		//Recipe Edit Start
	public const  int EQP_EVENT_4000009 = 4000009;
	/// <summary>Recipe Edit End</summary>
	/// <remarks></remarks>
		//Recipe Edit End
	public const  int EQP_EVENT_4000010 = 4000010;
	/// <summary>Conveyor Load Start</summary>
	/// <remarks></remarks>
		//Conveyor Load Start
	public const  int EQP_EVENT_4049000 = 4049000;
	/// <summary>Conveyor Load End</summary>
	/// <remarks></remarks>
		//Conveyor Load End
	public const  int EQP_EVENT_4049001 = 4049001;
	/// <summary>Conveyor Unload Start</summary>
	/// <remarks></remarks>
		//Conveyor Unload Start
	public const  int EQP_EVENT_4049002 = 4049002;
	/// <summary>Conveyor Unload End</summary>
	/// <remarks></remarks>
		//Conveyor Unload End
	public const  int EQP_EVENT_4049003 = 4049003;
	/// <summary>Station1 Start</summary>
	/// <remarks></remarks>
		//Station1 Start
	public const  int EQP_EVENT_4049004 = 4049004;
	/// <summary>Station1 End</summary>
	/// <remarks></remarks>
		//Station1 End
	public const  int EQP_EVENT_4049005 = 4049005;
	/// <summary>Station2 Start</summary>
	/// <remarks></remarks>
		//Station2 Start
	public const  int EQP_EVENT_4049006 = 4049006;
	/// <summary>Station2 End</summary>
	/// <remarks></remarks>
		//Station2 End
	public const  int EQP_EVENT_4049007 = 4049007;
	/// <summary>Station3 Start</summary>
	/// <remarks></remarks>
		//Station3 Start
	public const  int EQP_EVENT_4049008 = 4049008;
	/// <summary>Station3 End</summary>
	/// <remarks></remarks>
		//Station3 End
	public const  int EQP_EVENT_4049009 = 4049009;
	/// <summary>Loader Start</summary>
	/// <remarks></remarks>
		//Loader Start
	public const  int EQP_EVENT_4054000 = 4054000;
	/// <summary>Loader End</summary>
	/// <remarks></remarks>
		//Loader End
	public const  int EQP_EVENT_4054001 = 4054001;
	/// <summary>Unloader Start</summary>
	/// <remarks></remarks>
		//Unloader Start
	public const  int EQP_EVENT_4054002 = 4054002;
	/// <summary>Unloader End</summary>
	/// <remarks></remarks>
		//Unloader End
	public const  int EQP_EVENT_4054003 = 4054003;
	/// <summary>請確認以系統管理員執行.</summary>
	/// <remarks></remarks>
		//"Please, ensure System Run by Administrator."
	public const  int Error_1000000 = 1000000;
	/// <summary>"軸索引設定值錯誤,系統即將關閉!!!"</summary>
	/// <remarks></remarks>
		//"Axis Index is Error, System will Shutdown!!!"
	public const  int Error_1000001 = 1000001;
	/// <summary>施工中…未完待續.</summary>
	/// <remarks></remarks>
		//Working…To Be Continued.
	public const  int Error_1000002 = 1000002;
	/// <summary>該功能不支援!</summary>
	/// <remarks></remarks>
		//Function is NOT Supported.
	public const  int Error_1000003 = 1000003;
	/// <summary>龍門同動失敗!</summary>
	/// <remarks></remarks>
		//Gantry Failed!
	public const  int Error_1000004 = 1000004;
	/// <summary>頁面開啟失敗!</summary>
	/// <remarks></remarks>
		//Form Open Failed!
	public const  int Error_1000005 = 1000005;
	/// <summary>使用者登入發生嚴重錯誤!</summary>
	/// <remarks></remarks>
		//User Login fatal Error!
	public const  int Error_1000006 = 1000006;
	/// <summary>不明例外.</summary>
	/// <remarks></remarks>
		//Unknown Exception.
	public const  int Error_1000007 = 1000007;
	/// <summary>影像計算逾時!</summary>
	/// <remarks></remarks>
		//Image Calculation Timeout!
	public const  int Error_1000008 = 1000008;
	/// <summary>Recipe Pattern繪圖失敗!</summary>
	/// <remarks></remarks>
		//Draw Recipe Pattern Failed!
	public const  int Error_1000009 = 1000009;
	/// <summary>Recipe Map繪圖失敗!</summary>
	/// <remarks></remarks>
		//Draw Recipe Map Failed!
	public const  int Error_1000010 = 1000010;
	/// <summary>CCD 硬體異常，請確認!</summary>
	/// <remarks></remarks>
		//CCD Hardware Error, Please Check!
	public const  int Error_1000011 = 1000011;
	/// <summary>串接路徑異常!</summary>
	/// <remarks></remarks>
		//Create Working Path Failed!
	public const  int Error_1000012 = 1000012;
	/// <summary>系統失敗!</summary>
	/// <remarks></remarks>
		//System Failed!
	public const  int Error_1000013 = 1000013;
	/// <summary>系統平台錯誤!</summary>
	/// <remarks></remarks>
		//System Stage Error!
	public const  int Error_1000014 = 1000014;
	/// <summary>平台數量錯誤!</summary>
	/// <remarks></remarks>
		//Stage Count Error!
	public const  int Error_1000015 = 1000015;
	/// <summary>軸索引讀取失敗!請確認參數正確!</summary>
	/// <remarks></remarks>
		//Axis Index Load Failed!
	public const  int Error_1002000 = 1002000;
	/// <summary>軸索引儲存失敗!請確認檔案未被佔用!</summary>
	/// <remarks></remarks>
		//Axis Index Save Failed!
	public const  int Error_1002001 = 1002001;
	/// <summary>系統參數讀取失敗!請確認參數正確!</summary>
	/// <remarks></remarks>
		//System Parameter Load Failed!
	public const  int Error_1002002 = 1002002;
	/// <summary>系統參數儲存失敗!請確認檔案未被佔用!</summary>
	/// <remarks></remarks>
		//System Parameter Save Failed!
	public const  int Error_1002003 = 1002003;
	/// <summary>膠材參數讀取失敗!請確認參數正確!</summary>
	/// <remarks></remarks>
		//Paste Parameter Load Failed
	public const  int Error_1002004 = 1002004;
	/// <summary>膠材參數儲存失敗!請確認檔案未被佔用!</summary>
	/// <remarks></remarks>
		//Paste Parameter Save Failed
	public const  int Error_1002005 = 1002005;
	/// <summary>膠閥參數讀取失敗!請確認參數正確!</summary>
	/// <remarks></remarks>
		//Valve Parameter Load Failed!
	public const  int Error_1002006 = 1002006;
	/// <summary>膠閥參數儲存失敗!請確認檔案未被佔用!</summary>
	/// <remarks></remarks>
		//Valve Parameter Save Failed!
	public const  int Error_1002007 = 1002007;
	/// <summary>IO卡參數讀取失敗!請確認參數正確!</summary>
	/// <remarks></remarks>
		//IO Card Load Failed!
	public const  int Error_1002008 = 1002008;
	/// <summary>IO卡參數儲存失敗!請確認檔案未被佔用!</summary>
	/// <remarks></remarks>
		//IO Card Save Failed!
	public const  int Error_1002009 = 1002009;
	/// <summary>運動控制卡參數讀取失敗!請確認參數正確!</summary>
	/// <remarks></remarks>
		//Motion Card Load Failed!
	public const  int Error_1002010 = 1002010;
	/// <summary>運動控制卡參數儲存失敗!請確認檔案未被佔用!</summary>
	/// <remarks></remarks>
		//Motion Card Save Failed!
	public const  int Error_1002011 = 1002011;
	/// <summary>AI卡參數讀取失敗!請確認參數正確!</summary>
	/// <remarks></remarks>
		//AI Card Load Failed!
	public const  int Error_1002012 = 1002012;
	/// <summary>AI卡參數儲存失敗!請確認檔案未被佔用!</summary>
	/// <remarks></remarks>
		//AI Card Save Failed!
	public const  int Error_1002013 = 1002013;
	/// <summary>AO參數讀取失敗!請確認參數正確!</summary>
	/// <remarks></remarks>
		//AO Card Load Failed!
	public const  int Error_1002014 = 1002014;
	/// <summary>AO參數儲存失敗!請確認檔案未被佔用!</summary>
	/// <remarks></remarks>
		//AO Card Save Failed!
	public const  int Error_1002015 = 1002015;
	/// <summary>DI卡參數讀取失敗!請確認參數正確!</summary>
	/// <remarks></remarks>
		//DI Card Load Failed!
	public const  int Error_1002016 = 1002016;
	/// <summary>DI卡參數儲存失敗!請確認檔案未被佔用!</summary>
	/// <remarks></remarks>
		//DI Card Save Failed!
	public const  int Error_1002017 = 1002017;
	/// <summary>DO參數讀取失敗!請確認參數正確!</summary>
	/// <remarks></remarks>
		//DO Card Load Failed!
	public const  int Error_1002018 = 1002018;
	/// <summary>DO參數儲存失敗!請確認檔案未被佔用!</summary>
	/// <remarks></remarks>
		//DO Card Save Failed!
	public const  int Error_1002019 = 1002019;
	/// <summary>Recipe讀檔失敗!</summary>
	/// <remarks></remarks>
		//Recipe Load Failed!
	public const  int Error_1002020 = 1002020;
	/// <summary>Recipe存檔失敗!</summary>
	/// <remarks></remarks>
		//Recipe Save Failed!
	public const  int Error_1002021 = 1002021;
	/// <summary>單步參數讀取失敗!請確認參數正確!</summary>
	/// <remarks></remarks>
		//Step Parameter Load Failed!
	public const  int Error_1002022 = 1002022;
	/// <summary>單步參數儲存失敗!請確認檔案未被佔用!</summary>
	/// <remarks></remarks>
		//Step Parameter Save Failed!
	public const  int Error_1002023 = 1002023;
	/// <summary>設備訊息行為讀檔失敗!</summary>
	/// <remarks></remarks>
		//Message Behavior Load Failed!
	public const  int Error_1002024 = 1002024;
	/// <summary>設備訊息行為存檔失敗!</summary>
	/// <remarks></remarks>
		//Message Behavior Save Failed!
	public const  int Error_1002025 = 1002025;
	/// <summary>天平參數讀檔失敗!</summary>
	/// <remarks></remarks>
		//Scale Parameter Load Failed!
	public const  int Error_1002026 = 1002026;
	/// <summary>天平參數存檔失敗!</summary>
	/// <remarks></remarks>
		//Scale Parameter Save Failed!
	public const  int Error_1002027 = 1002027;
	/// <summary>閥1校正檔讀檔失敗!</summary>
	/// <remarks></remarks>
		//Valve1 Calibration Load Failed!
	public const  int Error_1002028 = 1002028;
	/// <summary>閥1校正檔存檔失敗!</summary>
	/// <remarks></remarks>
		//Valve1 Calibration Save Failed!
	public const  int Error_1002029 = 1002029;
	/// <summary>閥2校正檔讀檔失敗!</summary>
	/// <remarks></remarks>
		//Valve2 Calibration Load Failed!
	public const  int Error_1002030 = 1002030;
	/// <summary>閥2校正檔存檔失敗!</summary>
	/// <remarks></remarks>
		//Valve2 Calibration Save Failed!
	public const  int Error_1002031 = 1002031;
	/// <summary>閥3校正檔讀檔失敗!</summary>
	/// <remarks></remarks>
		//Valve3 Calibration Load Failed!
	public const  int Error_1002032 = 1002032;
	/// <summary>閥3校正檔存檔失敗!</summary>
	/// <remarks></remarks>
		//Valve3 Calibration Save Failed!
	public const  int Error_1002033 = 1002033;
	/// <summary>閥4校正檔讀檔失敗!</summary>
	/// <remarks></remarks>
		//Valve4 Calibration Load Failed!
	public const  int Error_1002034 = 1002034;
	/// <summary>閥4校正檔存檔失敗!</summary>
	/// <remarks></remarks>
		//Valve4 Calibration Save Failed!
	public const  int Error_1002035 = 1002035;
	/// <summary>閥高校正檔讀檔失敗!</summary>
	/// <remarks></remarks>
		//Valve Height Calibration Load Failed!
	public const  int Error_1002036 = 1002036;
	/// <summary>閥高校正檔存檔失敗!</summary>
	/// <remarks></remarks>
		//Valve Height Calibration Save Failed!
	public const  int Error_1002037 = 1002037;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1002038 = 1002038;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1002039 = 1002039;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1002040 = 1002040;
	/// <summary>IO卡初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize IO Card Failed!
	public const  int Error_1003000 = 1003000;
	/// <summary>IO卡關卡失敗!</summary>
	/// <remarks></remarks>
		//Close IO Card Failed!
	public const  int Error_1003001 = 1003001;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1003002 = 1003002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1003003 = 1003003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1003004 = 1003004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1003005 = 1003005;
	/// <summary>DI卡初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize DI Card Failed!
	public const  int Error_1004000 = 1004000;
	/// <summary>DI卡關卡失敗!</summary>
	/// <remarks></remarks>
		//Close DI Card Failed!
	public const  int Error_1004001 = 1004001;
	/// <summary>DI卡取得輸入值失敗!</summary>
	/// <remarks></remarks>
		//DI Card Get State Failed!
	public const  int Error_1004002 = 1004002;
	/// <summary>DI卡更新輸入值失敗!</summary>
	/// <remarks></remarks>
		//DI Card Refresh State Failed!
	public const  int Error_1004003 = 1004003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1004004 = 1004004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1004005 = 1004005;
	/// <summary>DO卡初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize DO Card Failed!
	public const  int Error_1005000 = 1005000;
	/// <summary>DO卡關卡失敗!</summary>
	/// <remarks></remarks>
		//Close DO Card Failed!
	public const  int Error_1005001 = 1005001;
	/// <summary>DO卡取得輸出值失敗!</summary>
	/// <remarks></remarks>
		//DO Card Get State Failed!
	public const  int Error_1005002 = 1005002;
	/// <summary>DO卡輸出值失敗!</summary>
	/// <remarks></remarks>
		//DO Card Output Failed!
	public const  int Error_1005003 = 1005003;
	/// <summary>DO卡反向輸出失敗!</summary>
	/// <remarks></remarks>
		//DO Card Toggle Output Failed!
	public const  int Error_1005004 = 1005004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1005005 = 1005005;
	/// <summary>AIO卡初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize AIO Card Failed!
	public const  int Error_1006000 = 1006000;
	/// <summary>AIO卡關卡失敗!</summary>
	/// <remarks></remarks>
		//Close AIO Card Failed!
	public const  int Error_1006001 = 1006001;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1006002 = 1006002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int  Error_1006003 = 1006003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1006004 = 1006004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1006005 = 1006005;
	/// <summary>AI卡初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize AI Card Failed!
	public const  int Error_1007000 = 1007000;
	/// <summary>AI卡關卡失敗!</summary>
	/// <remarks></remarks>
		//Close AI Card Failed!
	public const  int Error_1007001 = 1007001;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1007002 = 1007002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1007003 = 1007003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1007004 = 1007004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1007005 = 1007005;
	/// <summary>AO卡初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize AO Card Failed!
	public const  int Error_1008000 = 1008000;
	/// <summary>AO卡關卡失敗!</summary>
	/// <remarks></remarks>
		//Close AO Card Failed!
	public const  int Error_1008001 = 1008001;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1008002 = 1008002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1008003 = 1008003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1008004 = 1008004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1008005 = 1008005;
	/// <summary>運動控制卡1初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize Motion Card1 Failed!
	public const  int Error_1009000 = 1009000;
	/// <summary>運動控制卡1關卡失敗!</summary>
	/// <remarks></remarks>
		//Close Motion Card1 Failed!
	public const  int Error_1009001 = 1009001;
	/// <summary>"運動控制卡1初始化失敗,無法啟用卡!"</summary>
	/// <remarks></remarks>
		//Initialize Motion Card1 Failed! Can Not Open Device.
	public const  int Error_1009002 = 1009002;
	/// <summary>運動控制卡1初始化失敗!取得屬性失敗!</summary>
	/// <remarks></remarks>
		//Initialize Motion Card1 Failed! Get Property Error.
	public const  int Error_1009003 = 1009003;
	/// <summary>運動控制卡1初始化失敗!無法啟用軸!</summary>
	/// <remarks></remarks>
		//Initialize Motion Card1 Failed! Open Axis Failed!
	public const  int Error_1009004 = 1009004;
	/// <summary>"運動控制卡1初始化失敗,無法取得可用卡片!"</summary>
	/// <remarks></remarks>
		//Motion Card1 Can Not Get Available Device
	public const  int Error_1009005 = 1009005;
	/// <summary>運動控制卡1命令發送失敗!</summary>
	/// <remarks></remarks>
		//Motion Card1 Send Command Failed!
	public const  int Error_1009006 = 1009006;
	/// <summary>運動控制卡1IO狀態為警報!</summary>
	/// <remarks></remarks>
		//Motion Card1 Motion IO Status is Alarm!
	public const  int Error_1009007 = 1009007;
	/// <summary>{0}移動到位逾時!</summary>
	/// <remarks></remarks>
		//{0} Move Timeout!
	public const  int Error_1009008 = 1009008;
	/// <summary>運動控制卡1復歸第一段速設定失敗!</summary>
	/// <remarks></remarks>
		//Motion Card1 Set Home Velocity Low Failed!
	public const  int Error_1009009 = 1009009;
	/// <summary>運動控制卡1復歸第二段速設定失敗!</summary>
	/// <remarks></remarks>
		//Motion Card1 Set Home Velocity High Failed!
	public const  int Error_1009010 = 1009010;
	/// <summary>運動控制卡1復歸加速度設定失敗!</summary>
	/// <remarks></remarks>
		//Motion Card1 Set Home Acceleration Failed!
	public const  int Error_1009011 = 1009011;
	/// <summary>運動控制卡1復歸減速度設定失敗!</summary>
	/// <remarks></remarks>
		//Motion Card1 Set Home Deceleration Failed!
	public const  int Error_1009012 = 1009012;
	/// <summary>運動控制卡1初速度設定失敗!</summary>
	/// <remarks></remarks>
		//Motion Card1 Set Velocity Low Failed!
	public const  int Error_1009013 = 1009013;
	/// <summary>運動控制卡1最大速度設定失敗!</summary>
	/// <remarks></remarks>
		//Motion Card1 Set Velocity High Failed!
	public const  int Error_1009014 = 1009014;
	/// <summary>運動控制卡1加速度設定失敗!</summary>
	/// <remarks></remarks>
		//Motion Card1 Set Acceleration Failed!
	public const  int Error_1009015 = 1009015;
	/// <summary>運動控制卡1減速度設定失敗!</summary>
	/// <remarks></remarks>
		//Motion Card1 Set Deceleration Failed!
	public const  int Error_1009016 = 1009016;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1009017 = 1009017;
	/// <summary>COM通訊埠開啟失敗!</summary>
	/// <remarks></remarks>
		//Open COM Port Failed!
	public const  int Error_1010000 = 1010000;
	/// <summary>COM通訊埠關閉失敗!</summary>
	/// <remarks></remarks>
		//COM Port Close Failed!
	public const  int Error_1010001 = 1010001;
	/// <summary>COM通訊埠已開啟!</summary>
	/// <remarks></remarks>
		//COM Port Is Opened!
	public const  int Error_1010002 = 1010002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1010003 = 1010003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1010004 = 1010004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1010005 = 1010005;
	/// <summary>"網路連線逾時!請確認IP,Port未被佔用."</summary>
	/// <remarks></remarks>
		//"EtherNET Connection Time Out! Please confirm IP, Port Unoccupied."
	public const  int Error_1011000 = 1011000;
	/// <summary>網路連線斷開失敗!</summary>
	/// <remarks></remarks>
		//EtherNET Close Failed!
	public const  int Error_1011001 = 1011001;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1011002 = 1011002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1011003 = 1011003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1011004 = 1011004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1011005 = 1011005;
	/// <summary>CCD1初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize CCD1 Failed!
	public const  int Error_1012000 = 1012000;
	/// <summary>CCD1關閉失敗!</summary>
	/// <remarks></remarks>
		//Close CCD1 Failed!
	public const  int Error_1012001 = 1012001;
	/// <summary>CCD1通訊逾時!</summary>
	/// <remarks></remarks>
		//CCD1 Communication TimeOut!
	public const  int Error_1012002 = 1012002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1012003 = 1012003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1012004 = 1012004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1012005 = 1012005;
	/// <summary>CCD2初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize CCD2 Failed!
	public const  int Error_1012100 = 1012100;
	/// <summary>CCD2關閉失敗!</summary>
	/// <remarks></remarks>
		//Close CCD2 Failed!
	public const  int Error_1012101 = 1012101;
	/// <summary>CCD2通訊逾時!</summary>
	/// <remarks></remarks>
		//CCD2 Communication TimeOut!
	public const  int Error_1012102 = 1012102;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1012103 = 1012103;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1012104 = 1012104;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1012105 = 1012105;
	/// <summary>CCD3初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize CCD3 Failed!
	public const  int Error_1012200 = 1012200;
	/// <summary>CCD3關閉失敗!</summary>
	/// <remarks></remarks>
		//Close CCD3 Failed!
	public const  int Error_1012201 = 1012201;
	/// <summary>CCD3通訊逾時!</summary>
	/// <remarks></remarks>
		//CCD3 Communication TimeOut!
	public const  int Error_1012202 = 1012202;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1012203 = 1012203;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1012204 = 1012204;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1012205 = 1012205;
	/// <summary>CCD4初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize CCD4 Failed!
	public const  int Error_1012300 = 1012300;
	/// <summary>CCD4關閉失敗!</summary>
	/// <remarks></remarks>
		//Close CCD4 Failed!
	public const  int Error_1012301 = 1012301;
	/// <summary>CCD4通訊逾時!</summary>
	/// <remarks></remarks>
		//CCD4 Communication TimeOut!
	public const  int Error_1012302 = 1012302;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1012303 = 1012303;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1012304 = 1012304;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1012305 = 1012305;
	/// <summary>CCD5初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize CCD5 Failed!
	public const  int Error_1012400 = 1012400;
	/// <summary>CCD5關閉失敗!</summary>
	/// <remarks></remarks>
		//Close CCD5 Failed!
	public const  int Error_1012401 = 1012401;
	/// <summary>CCD5通訊逾時!</summary>
	/// <remarks></remarks>
		//CCD5 Communication TimeOut!
	public const  int Error_1012402 = 1012402;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1012403 = 1012403;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1012404 = 1012404;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1012405 = 1012405;
	/// <summary>影像擷取卡初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize Image Card Failed!
	public const  int Error_1013000 = 1013000;
	/// <summary>影像擷取卡關閉失敗!</summary>
	/// <remarks></remarks>
		//Close Image Card Failed!
	public const  int Error_1013001 = 1013001;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1013002 = 1013002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1013003 = 1013003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1013004 = 1013004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1013005 = 1013005;
	/// <summary>測高儀1初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize Altimeter 1 Failed!
	public const  int Error_1014000 = 1014000;
	/// <summary>測高儀1關閉失敗!</summary>
	/// <remarks></remarks>
		//Close Altimeter 1 Failed!
	public const  int Error_1014001 = 1014001;
	/// <summary>測高儀1選取Recipe失敗!</summary>
	/// <remarks></remarks>
		//Altimeter 1 Select Recipe Failed!
	public const  int Error_1014002 = 1014002;
	/// <summary>測高儀1 IP位置設定錯誤!</summary>
	/// <remarks></remarks>
		//Altimeter 1 IP-Address Error!
	public const  int Error_1014003 = 1014003;
	/// <summary>測高儀1讀值失敗!</summary>
	/// <remarks></remarks>
		//Altimeter 1 Read Value Error!
	public const  int Error_1014004 = 1014004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1014005 = 1014005;
	/// <summary>測高儀2初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize Altimeter 2 Failed!
	public const  int Error_1014100 = 1014100;
	/// <summary>測高儀2關閉失敗!</summary>
	/// <remarks></remarks>
		//Close Altimeter 2 Failed!
	public const  int Error_1014101 = 1014101;
	/// <summary>測高儀2選取Recipe失敗!</summary>
	/// <remarks></remarks>
		//Altimeter 2 Select Recipe Failed!
	public const  int Error_1014102 = 1014102;
	/// <summary>測高儀2 IP位置設定錯誤!</summary>
	/// <remarks></remarks>
		//Altimeter 2 IP-Address Error!
	public const  int Error_1014103 = 1014103;
	/// <summary>測高儀2讀值失敗!</summary>
	/// <remarks></remarks>
		//Altimeter 2 Read Value Error!
	public const  int Error_1014104 = 1014104;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1014105 = 1014105;
	/// <summary>測高儀3初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize Altimeter 3 Failed!
	public const  int Error_1014200 = 1014200;
	/// <summary>測高儀3關閉失敗!</summary>
	/// <remarks></remarks>
		//Close Altimeter 3 Failed!
	public const  int Error_1014201 = 1014201;
	/// <summary>雷射干涉儀3選取Recipe失敗!</summary>
	/// <remarks></remarks>
		//Altimeter 3 Select Recipe Failed!
	public const  int Error_1014202 = 1014202;
	/// <summary>測高儀3 IP位置設定錯誤!</summary>
	/// <remarks></remarks>
		//Altimeter 3 IP-Address Error!
	public const  int Error_1014203 = 1014203;
	/// <summary>測高儀3讀值失敗!</summary>
	/// <remarks></remarks>
		//Altimeter 3 Read Value Error!
	public const  int Error_1014204 = 1014204;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1014205 = 1014205;
	/// <summary>測高儀4初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize Altimeter 4 Failed!
	public const  int Error_1014300 = 1014300;
	/// <summary>測高儀4關閉失敗!</summary>
	/// <remarks></remarks>
		//Close Altimeter 4 Failed!
	public const  int Error_1014301 = 1014301;
	/// <summary>測高儀4選取Recipe失敗!</summary>
	/// <remarks></remarks>
		//Altimeter 4 Select Recipe Failed!
	public const  int Error_1014302 = 1014302;
	/// <summary>測高儀4 IP位置設定錯誤!</summary>
	/// <remarks></remarks>
		//Altimeter 4 IP-Address Error!
	public const  int Error_1014303 = 1014303;
	/// <summary>測高儀4讀值失敗!</summary>
	/// <remarks></remarks>
		//Altimeter 4 Read Value Error!
	public const  int Error_1014304 = 1014304;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1014305 = 1014305;
	/// <summary>微量天平1初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize Scale1 Failed!
	public const  int Error_1015000 = 1015000;
	/// <summary>微量天平1關閉失敗!</summary>
	/// <remarks></remarks>
		//Close Scale1 Failed!
	public const  int Error_1015001 = 1015001;
	/// <summary>微量天平1通訊逾時!</summary>
	/// <remarks></remarks>
		//Scale1 Communication Timeout!
	public const  int Error_1015002 = 1015002;
	/// <summary>微量天平1命令發送失敗!</summary>
	/// <remarks></remarks>
		//Scale1 Send Command Error!
	public const  int Error_1015003 = 1015003;
	/// <summary>微量天平1接收資料失敗!</summary>
	/// <remarks></remarks>
		//Scale1 Get Data Error!
	public const  int Error_1015004 = 1015004;
	/// <summary>微量天平1重量校正失敗!</summary>
	/// <remarks></remarks>
		//Scale1 Calibration Failed!
	public const  int Error_1015005 = 1015005;
	/// <summary>微量天平1重量不穩定!</summary>
	/// <remarks></remarks>
		//Scale1  is instability.
	public const  int Error_1015006 = 1015006;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1015007 = 1015007;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1015008 = 1015008;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1015009 = 1015009;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1015010 = 1015010;
	/// <summary>微量天平2初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize Scale2 Failed!
	public const  int Error_1015100 = 1015100;
	/// <summary>微量天平2關閉失敗!</summary>
	/// <remarks></remarks>
		//Close Scale2 Failed!
	public const  int Error_1015101 = 1015101;
	/// <summary>微量天平2通訊逾時!</summary>
	/// <remarks></remarks>
		//Scale2 Communication Timeout!
	public const  int Error_1015102 = 1015102;
	/// <summary>微量天平2命令發送失敗!</summary>
	/// <remarks></remarks>
		//Scale2 Send Command Error!
	public const  int Error_1015103 = 1015103;
	/// <summary>微量天平2接收資料失敗!</summary>
	/// <remarks></remarks>
		//Scale2 Get Data Error!
	public const  int Error_1015104 = 1015104;
	/// <summary>微量天平2重量校正失敗!</summary>
	/// <remarks></remarks>
		//Scale2 Calibration Failed!
	public const  int Error_1015105 = 1015105;
	/// <summary>微量天平2重量不穩定!</summary>
	/// <remarks></remarks>
		//Scale2  is instability.
	public const  int Error_1015106 = 1015106;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1015107 = 1015107;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1015108 = 1015108;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1015109 = 1015109;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1015110 = 1015110;
	/// <summary>觸發板1初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize Trigger Board1 Failed!
	public const  int Error_1016000 = 1016000;
	/// <summary>觸發板1關閉失敗!</summary>
	/// <remarks></remarks>
		//Close Trigger Board1 Failed!
	public const  int Error_1016001 = 1016001;
	/// <summary>觸發板1通訊逾時!</summary>
	/// <remarks></remarks>
		//Trigger Board1 Communication Timeout!
	public const  int Error_1016002 = 1016002;
	/// <summary>觸發板1命令發送錯誤!</summary>
	/// <remarks></remarks>
		//Trigger Board1 Send Command Error!
	public const  int Error_1016003 = 1016003;
	/// <summary>觸發板1資料接收錯誤!</summary>
	/// <remarks></remarks>
		//Trigger Board1Recieved Data Error!
	public const  int Error_1016004 = 1016004;
	/// <summary>觸發板1讀取版本錯誤!</summary>
	/// <remarks></remarks>
		//Trigger Board1 Get Version Failed!
	public const  int Error_1016005 = 1016005;
	/// <summary>觸發板2初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize Trigger Board2 Failed!
	public const  int Error_1016100 = 1016100;
	/// <summary>觸發板2關閉失敗!</summary>
	/// <remarks></remarks>
		//Close Trigger Board2 Failed!
	public const  int Error_1016101 = 1016101;
	/// <summary>觸發板2通訊逾時!</summary>
	/// <remarks></remarks>
		//Trigger Board2 Communication Timeout!
	public const  int Error_1016102 = 1016102;
	/// <summary>觸發板2命令發送錯誤!</summary>
	/// <remarks></remarks>
		//Trigger Board2 Send Command Error!
	public const  int Error_1016103 = 1016103;
	/// <summary>觸發板2資料接收錯誤!</summary>
	/// <remarks></remarks>
		//Trigger Board2 Recieved Data Error!
	public const  int Error_1016104 = 1016104;
	/// <summary>觸發板2讀取版本錯誤!</summary>
	/// <remarks></remarks>
		//Trigger Board2 Get Version Failed!
	public const  int Error_1016105 = 1016105;
	/// <summary>觸發板3初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize Trigger Board3 Failed!
	public const  int Error_1016200 = 1016200;
	/// <summary>觸發板3關閉失敗!</summary>
	/// <remarks></remarks>
		//Close Trigger Board3 Failed!
	public const  int Error_1016201 = 1016201;
	/// <summary>觸發板3通訊逾時!</summary>
	/// <remarks></remarks>
		//Trigger Board3 Communication Timeout!
	public const  int Error_1016202 = 1016202;
	/// <summary>觸發板3命令發送錯誤!</summary>
	/// <remarks></remarks>
		//Trigger Board3 Send Command Error!
	public const  int Error_1016203 = 1016203;
	/// <summary>觸發板3資料接收錯誤!</summary>
	/// <remarks></remarks>
		//Trigger Board3 Recieved Data Error!
	public const  int Error_1016204 = 1016204;
	/// <summary>觸發板3讀取版本錯誤!</summary>
	/// <remarks></remarks>
		//Trigger Board3 Get Version Failed!
	public const  int Error_1016205 = 1016205;
	/// <summary>觸發板4初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize Trigger Board4 Failed!
	public const  int Error_1016300 = 1016300;
	/// <summary>觸發板4關閉失敗!</summary>
	/// <remarks></remarks>
		//Close Trigger Board4 Failed!
	public const  int Error_1016301 = 1016301;
	/// <summary>觸發板4通訊逾時!</summary>
	/// <remarks></remarks>
		//Trigger Board4 Communication Timeout!
	public const  int Error_1016302 = 1016302;
	/// <summary>觸發板4命令發送錯誤!</summary>
	/// <remarks></remarks>
		//Trigger Board4 Send Command Error!
	public const  int Error_1016303 = 1016303;
	/// <summary>觸發板4資料接收錯誤!</summary>
	/// <remarks></remarks>
		//Trigger Board4 Recieved Data Error!
	public const  int Error_1016304 = 1016304;
	/// <summary>觸發板4讀取版本錯誤!</summary>
	/// <remarks></remarks>
		//Trigger Board4 Get Version Failed!
	public const  int Error_1016305 = 1016305;
	/// <summary>FMCS1初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize FMCS1 Failed!
	public const  int Error_1017000 = 1017000;
	/// <summary>FMCS1關閉失敗!</summary>
	/// <remarks></remarks>
		//Close FMCS1 Failed
	public const  int Error_1017001 = 1017001;
	/// <summary>FMCS1通訊逾時!</summary>
	/// <remarks></remarks>
		//FMCS1 Communication TimeOut!
	public const  int Error_1017002 = 1017002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1017003 = 1017003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1017004 = 1017004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1017005 = 1017005;
	/// <summary>FMCS2初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize FMCS2 Failed!
	public const  int Error_1017100 = 1017100;
	/// <summary>FMCS2關閉失敗!</summary>
	/// <remarks></remarks>
		//Close FMCS2 Failed
	public const  int Error_1017101 = 1017101;
	/// <summary>FMCS2通訊逾時!</summary>
	/// <remarks></remarks>
		//FMCS2 Communication TimeOut!
	public const  int Error_1017102 = 1017102;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1017103 = 1017103;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1017104 = 1017104;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1017105 = 1017105;
	/// <summary>FMCS3初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize FMCS3 Failed!
	public const  int Error_1017200 = 1017200;
	/// <summary>FMCS3關閉失敗!</summary>
	/// <remarks></remarks>
		//Close FMCS3 Failed
	public const  int Error_1017201 = 1017201;
	/// <summary>FMCS3通訊逾時!</summary>
	/// <remarks></remarks>
		//FMCS3 Communication TimeOut!
	public const  int Error_1017202 = 1017202;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1017203 = 1017203;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1017204 = 1017204;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1017205 = 1017205;
	/// <summary>FMCS4初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize FMCS4 Failed!
	public const  int Error_1017300 = 1017300;
	/// <summary>FMCS4關閉失敗!</summary>
	/// <remarks></remarks>
		//Close FMCS4 Failed
	public const  int Error_1017301 = 1017301;
	/// <summary>FMCS4通訊逾時!</summary>
	/// <remarks></remarks>
		//FMCS4 Communication TimeOut!
	public const  int Error_1017302 = 1017302;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1017303 = 1017303;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1017304 = 1017304;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1017305 = 1017305;
	/// <summary>加熱器1初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize Heater1 Failed!
	public const  int Error_1018000 = 1018000;
	/// <summary>加熱器1關閉失敗!</summary>
	/// <remarks></remarks>
		//Close Heater1 Failed!
	public const  int Error_1018001 = 1018001;
	/// <summary>加熱器1通訊逾時!</summary>
	/// <remarks></remarks>
		//Heater1 Communication TimeOut!
	public const  int Error_1018002 = 1018002;
	/// <summary>加熱器1資料錯誤</summary>
	/// <remarks></remarks>
		//Heater1 Temp Date Fail
	public const  int Error_1018003 = 1018003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1018004 = 1018004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1018005 = 1018005;
	/// <summary>加熱器2初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize Heater2 Failed!
	public const  int Error_1018100 = 1018100;
	/// <summary>加熱器2關閉失敗!</summary>
	/// <remarks></remarks>
		//Close Heater2 Failed!
	public const  int Error_1018101 = 1018101;
	/// <summary>加熱器2通訊逾時!</summary>
	/// <remarks></remarks>
		//Heater2 Communication TimeOut!
	public const  int Error_1018102 = 1018102;
	/// <summary>加熱器2資料錯誤</summary>
	/// <remarks></remarks>
		//Heater2 Temp Date Fail
	public const  int Error_1018103 = 1018103;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1018104 = 1018104;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1018105 = 1018105;
	/// <summary>加熱器3初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize Heater3 Failed!
	public const  int Error_1018200 = 1018200;
	/// <summary>加熱器3關閉失敗!</summary>
	/// <remarks></remarks>
		//Close Heater3 Failed!
	public const  int Error_1018201 = 1018201;
	/// <summary>加熱器3通訊逾時!</summary>
	/// <remarks></remarks>
		//Heater3 Communication TimeOut!
	public const  int Error_1018202 = 1018202;
	/// <summary>加熱器3資料錯誤</summary>
	/// <remarks></remarks>
		//Heater3 Temp Date Fail
	public const  int Error_1018203 = 1018203;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1018204 = 1018204;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1018205 = 1018205;
	/// <summary>加熱器4初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize Heater4 Failed!
	public const  int Error_1018300 = 1018300;
	/// <summary>加熱器4關閉失敗!</summary>
	/// <remarks></remarks>
		//Close Heater4 Failed!
	public const  int Error_1018301 = 1018301;
	/// <summary>加熱器4通訊逾時!</summary>
	/// <remarks></remarks>
		//Heater4 Communication TimeOut!
	public const  int Error_1018302 = 1018302;
	/// <summary>加熱器4資料錯誤</summary>
	/// <remarks></remarks>
		//Heater4 Temp Date Fail
	public const  int Error_1018303 = 1018303;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1018304 = 1018304;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1018305 = 1018305;
	/// <summary>噴射閥1初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize Jet Valve1 Failed!
	public const  int Error_1019000 = 1019000;
	/// <summary>噴射閥1關閉失敗!</summary>
	/// <remarks></remarks>
		//Close Jet Valve1 Failed!
	public const  int Error_1019001 = 1019001;
	/// <summary>噴射閥1控制器異常!</summary>
	/// <remarks></remarks>
		//Jet Valve1 Controller Alarm!
	public const  int Error_1019002 = 1019002;
	/// <summary>噴射閥資料異常!</summary>
	/// <remarks></remarks>
		//Jet Valve Database Alarm!
	public const  int Error_1019003 = 1019003;
	/// <summary>"噴射閥控制器讀/寫資料失敗!"</summary>
	/// <remarks></remarks>
		//Jet Valve Controller Read/Write Failed!
	public const  int Error_1019004 = 1019004;
	/// <summary>請選擇噴射閥1</summary>
	/// <remarks></remarks>
		//Select Jet Valve1, Please!
	public const  int Error_1019005 = 1019005;
	/// <summary>請選擇噴射閥2</summary>
	/// <remarks></remarks>
		//Select Jet Valve2, Please!
	public const  int Error_1019006 = 1019006;
	/// <summary>請選擇噴射閥3</summary>
	/// <remarks></remarks>
		//Select Jet Valve3, Please!
	public const  int Error_1019007 = 1019007;
	/// <summary>請選擇噴射閥4</summary>
	/// <remarks></remarks>
		//Select Jet Valve4, Please!
	public const  int Error_1019008 = 1019008;
	/// <summary>螺桿閥1初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize Auger Valve1 Failed!
	public const  int Error_1020000 = 1020000;
	/// <summary>螺桿閥1關閉失敗!</summary>
	/// <remarks></remarks>
		//Close Auger Valve1 Failed!
	public const  int Error_1020001 = 1020001;
	/// <summary>螺桿閥1異常!</summary>
	/// <remarks></remarks>
		//Auger Valve1 Alarm!
	public const  int Error_1020002 = 1020002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1020003 = 1020003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1020004 = 1020004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1020005 = 1020005;
	/// <summary>氣壓閥1初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize Time-pressure Valve1 Failed!
	public const  int Error_1021000 = 1021000;
	/// <summary>氣壓閥1關閉失敗!</summary>
	/// <remarks></remarks>
		//Close Time-pressure Valve1 Failed!
	public const  int Error_1021001 = 1021001;
	/// <summary>氣壓閥1異常!</summary>
	/// <remarks></remarks>
		//Time-pressure Valve1 Alarm!
	public const  int Error_1021002 = 1021002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1021003 = 1021003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1021004 = 1021004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1021005 = 1021005;
	/// <summary>程控光源初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize Program Light Failed!
	public const  int Error_1022000 = 1022000;
	/// <summary>程控光源關閉失敗!</summary>
	/// <remarks></remarks>
		//Close Program Light Failed!
	public const  int Error_1022001 = 1022001;
	/// <summary>程控光源通訊逾時!</summary>
	/// <remarks></remarks>
		//Program Light Communication Timeout!
	public const  int Error_1022002 = 1022002;
	/// <summary>程控光源通訊失敗!</summary>
	/// <remarks></remarks>
		//Program Light Communication Failed!
	public const  int Error_1022003 = 1022003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1022004 = 1022004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1022005 = 1022005;
	/// <summary>PLC1初始化失敗!</summary>
	/// <remarks></remarks>
		//Initialize PLC1 Failed!
	public const  int Error_1023000 = 1023000;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1023001 = 1023001;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1023002 = 1023002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1023003 = 1023003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1023004 = 1023004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1023005 = 1023005;
	/// <summary>Map檔開啟失敗!</summary>
	/// <remarks></remarks>
		//Mapping Data Open Failed!
	public const  int Error_1025000 = 1025000;
	/// <summary>Map檔切割失敗!</summary>
	/// <remarks></remarks>
		//Mapping Data Split Failed!
	public const  int Error_1025001 = 1025001;
	/// <summary>Map檔填入失敗!</summary>
	/// <remarks></remarks>
		//Mapping Data Fill Failed!
	public const  int Error_1025002 = 1025002;
	/// <summary>節點資料連結失敗!</summary>
	/// <remarks></remarks>
		//Node Map Connect Failed!
	public const  int Error_1025003 = 1025003;
	/// <summary>無法載入Map檔,請手動載入Map檔</summary>
	/// <remarks></remarks>
		//Can't load Map, Please click Load Map Manual
	public const  int Error_1025004 = 1025004;
	/// <summary>A_L_DP_X軸移動失敗</summary>
	/// <remarks></remarks>
		//A_L_DP_X Axis  Move Error!
	public const  int Error_1030000 = 1030000;
	/// <summary>A_L_DP_X軸復歸逾時</summary>
	/// <remarks></remarks>
		//A_L_DP_X Axis  wait Home Timeout!
	public const  int Error_1030001 = 1030001;
	/// <summary>A_L_DP_X軸馬達Alarm</summary>
	/// <remarks></remarks>
		//A_L_DP_X Axis  is Alarm!
	public const  int Error_1030002 = 1030002;
	/// <summary>A_L_DP_X軸馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//A_L_DP_X Axis  Get Motor Status Failed!
	public const  int Error_1030003 = 1030003;
	/// <summary>A_L_DP_X軸等待到位逾時</summary>
	/// <remarks></remarks>
		//A_L_DP_X Axis  wait INP Timeout!
	public const  int Error_1030004 = 1030004;
	/// <summary>A_L_DP_X軸移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//A_L_DP_X Axis  Command is Out of SPEL
	public const  int Error_1030005 = 1030005;
	/// <summary>A_L_DP_X軸移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//A_L_DP_X Axis  Command is Out of SNEL
	public const  int Error_1030006 = 1030006;
	/// <summary>A_L_DP_X軸接觸硬體正極限</summary>
	/// <remarks></remarks>
		//A_L_DP_X Axis  Touch HPEL
	public const  int Error_1030007 = 1030007;
	/// <summary>A_L_DP_X軸接觸硬體負極限</summary>
	/// <remarks></remarks>
		//A_L_DP_X Axis  Touch HNEL
	public const  int Error_1030008 = 1030008;
	/// <summary>A_L_DP_X軸錯誤停止</summary>
	/// <remarks></remarks>
		//A_L_DP_X Axis  Error Stop
	public const  int Error_1030009 = 1030009;
	/// <summary>A_L_DP_X軸參數無效</summary>
	/// <remarks></remarks>
		//A_L_DP_X Axis  Invalid Parameter
	public const  int Error_1030010 = 1030010;
	/// <summary>A_L_DP_X軸加速度無效</summary>
	/// <remarks></remarks>
		//A_L_DP_X Axis  Invalid Acc
	public const  int Error_1030011 = 1030011;
	/// <summary>A_L_DP_X軸減速度無效</summary>
	/// <remarks></remarks>
		//A_L_DP_X Axis  Invalid Dec
	public const  int Error_1030012 = 1030012;
	/// <summary>A_L_DP_X軸最大速度無效</summary>
	/// <remarks></remarks>
		//A_L_DP_X Axis  Invalid VelHigh
	public const  int Error_1030013 = 1030013;
	/// <summary>A_L_DP_X軸初速度無效</summary>
	/// <remarks></remarks>
		//A_L_DP_X Axis  Invalid VelLow
	public const  int Error_1030014 = 1030014;
	/// <summary>A_L_DP_X軸觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//A_L_DP_X Axis  Cmp Table Error.
	public const  int Error_1030015 = 1030015;
	/// <summary>A_L_DP_X軸復歸命令異常!</summary>
	/// <remarks></remarks>
		//A_L_DP_X Axis  Command Home Error!
	public const  int Error_1030016 = 1030016;
	/// <summary>A_L_DP_X軸設定速度失敗!</summary>
	/// <remarks></remarks>
		//A_L_DP_X Axis SetSpeed Error!
	public const  int Error_1030017 = 1030017;
	/// <summary>A_L_DP_X軸命令執行失敗!</summary>
	/// <remarks></remarks>
		//A_L_DP_X Axis Command Error!
	public const  int Error_1030018 = 1030018;
	/// <summary>A_L_DP_X軸命令執行逾時!</summary>
	/// <remarks></remarks>
		//A_L_DP_X Axis X Command is TimeOut!
	public const  int Error_1030019 = 1030019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1030020 = 1030020;
	/// <summary>A_L_DP_Y軸 移動失敗</summary>
	/// <remarks></remarks>
		//A_L_DP_Y Axis  Move Error!
	public const  int Error_1031000 = 1031000;
	/// <summary>A_L_DP_Y軸 復歸逾時</summary>
	/// <remarks></remarks>
		//A_L_DP_Y Axis  wait Home Timeout!
	public const  int Error_1031001 = 1031001;
	/// <summary>A_L_DP_Y軸 馬達Alarm</summary>
	/// <remarks></remarks>
		//A_L_DP_Y Axis  is Alarm!
	public const  int Error_1031002 = 1031002;
	/// <summary>A_L_DP_Y軸 馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//A_L_DP_Y Axis  Get Motor Status Failed!
	public const  int Error_1031003 = 1031003;
	/// <summary>A_L_DP_Y軸 等待到位逾時</summary>
	/// <remarks></remarks>
		//A_L_DP_Y Axis  wait INP Timeout!
	public const  int Error_1031004 = 1031004;
	/// <summary>A_L_DP_Y軸 移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//A_L_DP_Y Axis  Command is Out of SPEL
	public const  int Error_1031005 = 1031005;
	/// <summary>A_L_DP_Y軸 移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//A_L_DP_Y Axis  Command is Out of SNEL
	public const  int Error_1031006 = 1031006;
	/// <summary>A_L_DP_Y軸 接觸硬體正極限</summary>
	/// <remarks></remarks>
		//A_L_DP_Y Axis  Touch HPEL
	public const  int Error_1031007 = 1031007;
	/// <summary>A_L_DP_Y軸 接觸硬體負極限</summary>
	/// <remarks></remarks>
		//A_L_DP_Y Axis  Touch HNEL
	public const  int Error_1031008 = 1031008;
	/// <summary>A_L_DP_Y軸 錯誤停止</summary>
	/// <remarks></remarks>
		//A_L_DP_Y Axis  Error Stop
	public const  int Error_1031009 = 1031009;
	/// <summary>A_L_DP_Y軸 參數無效</summary>
	/// <remarks></remarks>
		//A_L_DP_Y Axis  Invalid Parameter
	public const  int Error_1031010 = 1031010;
	/// <summary>A_L_DP_Y軸 加速度無效</summary>
	/// <remarks></remarks>
		//A_L_DP_Y Axis  Invalid Acc
	public const  int Error_1031011 = 1031011;
	/// <summary>A_L_DP_Y軸 減速度無效</summary>
	/// <remarks></remarks>
		//A_L_DP_Y Axis  Invalid Dec
	public const  int Error_1031012 = 1031012;
	/// <summary>A_L_DP_Y軸 最大速度無效</summary>
	/// <remarks></remarks>
		//A_L_DP_Y Axis  Invalid VelHigh
	public const  int Error_1031013 = 1031013;
	/// <summary>A_L_DP_Y軸 初速度無效</summary>
	/// <remarks></remarks>
		//A_L_DP_Y Axis  Invalid VelLow
	public const  int Error_1031014 = 1031014;
	/// <summary>A_L_DP_Y軸 觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//A_L_DP_Y Axis  Cmp Table Error.
	public const  int Error_1031015 = 1031015;
	/// <summary>A_L_DP_Y軸 復歸命令異常!</summary>
	/// <remarks></remarks>
		//A_L_DP_Y Axis  Command Home Error!
	public const  int Error_1031016 = 1031016;
	/// <summary>A_L_DP_Y軸設定速度失敗!</summary>
	/// <remarks></remarks>
		//A_L_DP_Y Axis SetSpeed Error!
	public const  int Error_1031017 = 1031017;
	/// <summary>A_L_DP_Y軸命令執行失敗!</summary>
	/// <remarks></remarks>
		//A_L_DP_Y Axis Command Error!
	public const  int Error_1031018 = 1031018;
	/// <summary>A_L_DP_Y軸命令執行逾時!</summary>
	/// <remarks></remarks>
		//A_L_DP_Y Axis X Command is TimeOut!
	public const  int Error_1031019 = 1031019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1031020 = 1031020;
	/// <summary>A_L_DP_Z軸 移動失敗</summary>
	/// <remarks></remarks>
		//A_L_DP_Z Axis Move Error!
	public const  int Error_1032000 = 1032000;
	/// <summary>A_L_DP_Z軸 復歸逾時</summary>
	/// <remarks></remarks>
		//A_L_DP_Z Axis wait Home Timeout!
	public const  int Error_1032001 = 1032001;
	/// <summary>A_L_DP_Z軸 馬達Alarm</summary>
	/// <remarks></remarks>
		//A_L_DP_Z Axis is Alarm!
	public const  int Error_1032002 = 1032002;
	/// <summary>A_L_DP_Z軸 馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//A_L_DP_Z Axis Get Motor Status Failed!
	public const  int Error_1032003 = 1032003;
	/// <summary>A_L_DP_Z軸 等待到位逾時</summary>
	/// <remarks></remarks>
		//A_L_DP_Z Axis wait INP Timeout!
	public const  int Error_1032004 = 1032004;
	/// <summary>A_L_DP_Z軸 移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//A_L_DP_Z Axis Command is Out of SPEL
	public const  int Error_1032005 = 1032005;
	/// <summary>A_L_DP_Z軸 移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//A_L_DP_Z Axis Command is Out of SNEL
	public const  int Error_1032006 = 1032006;
	/// <summary>A_L_DP_Z軸 接觸硬體正極限</summary>
	/// <remarks></remarks>
		//A_L_DP_Z Axis Touch HPEL
	public const  int Error_1032007 = 1032007;
	/// <summary>A_L_DP_Z軸 接觸硬體負極限</summary>
	/// <remarks></remarks>
		//A_L_DP_Z Axis Touch HNEL
	public const  int Error_1032008 = 1032008;
	/// <summary>A_L_DP_Z軸 錯誤停止</summary>
	/// <remarks></remarks>
		//A_L_DP_Z Axis Error Stop
	public const  int Error_1032009 = 1032009;
	/// <summary>A_L_DP_Z軸 參數無效</summary>
	/// <remarks></remarks>
		//A_L_DP_Z Axis Invalid Parameter
	public const  int Error_1032010 = 1032010;
	/// <summary>A_L_DP_Z軸 加速度無效</summary>
	/// <remarks></remarks>
		//A_L_DP_Z Axis Invalid Acc
	public const  int Error_1032011 = 1032011;
	/// <summary>A_L_DP_Z軸 減速度無效</summary>
	/// <remarks></remarks>
		//A_L_DP_Z Axis Invalid Dec
	public const  int Error_1032012 = 1032012;
	/// <summary>A_L_DP_Z軸 最大速度無效</summary>
	/// <remarks></remarks>
		//A_L_DP_Z Axis Invalid VelHigh
	public const  int Error_1032013 = 1032013;
	/// <summary>A_L_DP_Z軸 初速度無效</summary>
	/// <remarks></remarks>
		//A_L_DP_Z Axis Invalid VelLow
	public const  int Error_1032014 = 1032014;
	/// <summary>A_L_DP_Z軸 觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//A_L_DP_Z Axis Cmp Table Error.
	public const  int Error_1032015 = 1032015;
	/// <summary>A_L_DP_Z軸 復歸命令異常!</summary>
	/// <remarks></remarks>
		//A_L_DP_Z Axis Command Home Error!
	public const  int Error_1032016 = 1032016;
	/// <summary>A_L_DP_Z軸設定速度失敗!</summary>
	/// <remarks></remarks>
		//A_L_DP_Z Axis SetSpeed Error!
	public const  int Error_1032017 = 1032017;
	/// <summary>A_L_DP_Z軸命令執行失敗!</summary>
	/// <remarks></remarks>
		//A_L_DP_Z Axis Command Error!
	public const  int Error_1032018 = 1032018;
	/// <summary>A_L_DP_Z軸命令執行逾時!</summary>
	/// <remarks></remarks>
		//A_L_DP_Z Axis X Command is TimeOut!
	public const  int Error_1032019 = 1032019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1032020 = 1032020;
	/// <summary>Y2軸命令執行失敗</summary>
	/// <remarks></remarks>
		//Y2 Axis Command Error!
	public const  int Error_1033000 = 1033000;
	/// <summary>Y2軸復歸逾時</summary>
	/// <remarks></remarks>
		//Y2 Axis wait Home Timeout!
	public const  int Error_1033001 = 1033001;
	/// <summary>Y2軸馬達Alarm</summary>
	/// <remarks></remarks>
		//Y2 Axis is Alarm!
	public const  int Error_1033002 = 1033002;
	/// <summary>Y2軸馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//Y2 Axis Get Motor Status Failed!
	public const  int Error_1033003 = 1033003;
	/// <summary>Y2軸等待到位逾時</summary>
	/// <remarks></remarks>
		//Y2 Axis wait INP Timeout!
	public const  int Error_1033004 = 1033004;
	/// <summary>Y2軸移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//Y2 Axis Command is Out of SPEL
	public const  int Error_1033005 = 1033005;
	/// <summary>Y2軸移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//Y2 Axis Command is Out of SNEL
	public const  int Error_1033006 = 1033006;
	/// <summary>Y2軸接觸硬體正極限</summary>
	/// <remarks></remarks>
		//Y2 Axis Touch HPEL
	public const  int Error_1033007 = 1033007;
	/// <summary>Y2軸接觸硬體負極限</summary>
	/// <remarks></remarks>
		//Y2 Axis Touch HNEL
	public const  int Error_1033008 = 1033008;
	/// <summary>Y2軸錯誤停止</summary>
	/// <remarks></remarks>
		//Y2 Axis Error Stop
	public const  int Error_1033009 = 1033009;
	/// <summary>Y2軸參數無效</summary>
	/// <remarks></remarks>
		//Y2 Axis Invalid Parameter
	public const  int Error_1033010 = 1033010;
	/// <summary>Y2軸加速度無效</summary>
	/// <remarks></remarks>
		//Y2 Axis Invalid Acc
	public const  int Error_1033011 = 1033011;
	/// <summary>Y2軸減速度無效</summary>
	/// <remarks></remarks>
		//Y2 Axis Invalid Dec
	public const  int Error_1033012 = 1033012;
	/// <summary>Y2軸最大速度無效</summary>
	/// <remarks></remarks>
		//Y2 Axis Invalid VelHigh
	public const  int Error_1033013 = 1033013;
	/// <summary>Y2軸初速度無效</summary>
	/// <remarks></remarks>
		//Y2 Axis Invalid VelLow
	public const  int Error_1033014 = 1033014;
	/// <summary>Y2軸觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//Y2 Axis Cmp Table Error.
	public const  int Error_1033015 = 1033015;
	/// <summary>Y2軸復歸命令異常!</summary>
	/// <remarks></remarks>
		//Y2 Axis Command Home Error!
	public const  int Error_1033016 = 1033016;
	/// <summary>Y2軸安全位置等待逾時!</summary>
	/// <remarks></remarks>
		//
	public const  int Error_1033017 = 1033017;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1033018 = 1033018;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1033019 = 1033019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1033020 = 1033020;
	/// <summary>A_L_DP_Tilt移動失敗</summary>
	/// <remarks></remarks>
		//A_L_DP_Tilt  Move Error!
	public const  int Error_1034000 = 1034000;
	/// <summary>A_L_DP_Tilt復歸逾時</summary>
	/// <remarks></remarks>
		//A_L_DP_Tilt wait Home Timeout!
	public const  int Error_1034001 = 1034001;
	/// <summary>A_L_DP_Tilt馬達Alarm</summary>
	/// <remarks></remarks>
		//A_L_DP_Tilt is Alarm!
	public const  int Error_1034002 = 1034002;
	/// <summary>A_L_DP_Tilt馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//A_L_DP_Tilt Get Motor Status Failed!
	public const  int Error_1034003 = 1034003;
	/// <summary>A_L_DP_Tilt等待到位逾時</summary>
	/// <remarks></remarks>
		//A_L_DP_Tilt wait INP Timeout!
	public const  int Error_1034004 = 1034004;
	/// <summary>A_L_DP_Tilt移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//A_L_DP_Tilt Command is Out of SPEL
	public const  int Error_1034005 = 1034005;
	/// <summary>A_L_DP_Tilt移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//A_L_DP_Tilt Command is Out of SNEL
	public const  int Error_1034006 = 1034006;
	/// <summary>A_L_DP_Tilt接觸硬體正極限</summary>
	/// <remarks></remarks>
		//A_L_DP_Tilt Touch HPEL
	public const  int Error_1034007 = 1034007;
	/// <summary>A_L_DP_Tilt接觸硬體負極限</summary>
	/// <remarks></remarks>
		//A_L_DP_Tilt Touch HNEL
	public const  int Error_1034008 = 1034008;
	/// <summary>A_L_DP_Tilt錯誤停止</summary>
	/// <remarks></remarks>
		//A_L_DP_Tilt Error Stop
	public const  int Error_1034009 = 1034009;
	/// <summary>A_L_DP_Tilt參數無效</summary>
	/// <remarks></remarks>
		//A_L_DP_Tilt Invalid Parameter
	public const  int Error_1034010 = 1034010;
	/// <summary>A_L_DP_Tilt加速度無效</summary>
	/// <remarks></remarks>
		//A_L_DP_Tilt Invalid Acc
	public const  int Error_1034011 = 1034011;
	/// <summary>A_L_DP_Tilt減速度無效</summary>
	/// <remarks></remarks>
		//A_L_DP_Tilt Invalid Dec
	public const  int Error_1034012 = 1034012;
	/// <summary>A_L_DP_Tilt最大速度無效</summary>
	/// <remarks></remarks>
		//A_L_DP_Tilt Invalid VelHigh
	public const  int Error_1034013 = 1034013;
	/// <summary>A_L_DP_Tilt初速度無效</summary>
	/// <remarks></remarks>
		//A_L_DP_Tilt Invalid VelLow
	public const  int Error_1034014 = 1034014;
	/// <summary>A_L_DP_Tilt觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//A_L_DP_Tilt Cmp Table Error.
	public const  int Error_1034015 = 1034015;
	/// <summary>A_L_DP_Tilt復歸命令異常!</summary>
	/// <remarks></remarks>
		//A_L_DP_Tilt Command Home Error!
	public const  int Error_1034016 = 1034016;
	/// <summary>A_L_DP_Tilt軸設定速度失敗!</summary>
	/// <remarks></remarks>
		//A_L_DP_Tilt Axis SetSpeed Error!
	public const  int Error_1034017 = 1034017;
	/// <summary>A_L_DP_Tilt命令執行失敗!</summary>
	/// <remarks></remarks>
		//A_L_DP_Tilt  Command Error!
	public const  int Error_1034018 = 1034018;
	/// <summary>A_L_DP_Tilt命令執行逾時!</summary>
	/// <remarks></remarks>
		//A_L_DP_Tilt Command is TimeOut!
	public const  int Error_1034019 = 1034019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1034020 = 1034020;
	/// <summary>運動群組1移動失敗</summary>
	/// <remarks></remarks>
		//Group1 Move Error!
	public const  int Error_1036000 = 1036000;
	/// <summary>運動群組1復歸逾時</summary>
	/// <remarks></remarks>
		//Group1 wait Home Timeout!
	public const  int Error_1036001 = 1036001;
	/// <summary>運動群組1馬達Alarm</summary>
	/// <remarks></remarks>
		//Group1 is Alarm!
	public const  int Error_1036002 = 1036002;
	/// <summary>運動群組1馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//Group1 Get Motor Status Failed!
	public const  int Error_1036003 = 1036003;
	/// <summary>運動群組1等待到位逾時</summary>
	/// <remarks></remarks>
		//Group1 wait INP Timeout!
	public const  int Error_1036004 = 1036004;
	/// <summary>運動群組1移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//Group1 Command is Out of SPEL
	public const  int Error_1036005 = 1036005;
	/// <summary>運動群組1移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//Group1 Command is Out of SNEL
	public const  int Error_1036006 = 1036006;
	/// <summary>運動群組1接觸硬體正極限</summary>
	/// <remarks></remarks>
		//Group1 Touch HPEL
	public const  int Error_1036007 = 1036007;
	/// <summary>運動群組1接觸硬體負極限</summary>
	/// <remarks></remarks>
		//Group1 Touch HNEL
	public const  int Error_1036008 = 1036008;
	/// <summary>運動群組1錯誤停止</summary>
	/// <remarks></remarks>
		//Group1 Error Stop
	public const  int Error_1036009 = 1036009;
	/// <summary>運動群組1參數無效</summary>
	/// <remarks></remarks>
		//Group1 Invalid Parameter
	public const  int Error_1036010 = 1036010;
	/// <summary>運動群組1加速度無效</summary>
	/// <remarks></remarks>
		//Group1 Invalid Acc
	public const  int Error_1036011 = 1036011;
	/// <summary>運動群組1減速度無效</summary>
	/// <remarks></remarks>
		//Group1 Invalid Dec
	public const  int Error_1036012 = 1036012;
	/// <summary>運動群組1最大速度無效</summary>
	/// <remarks></remarks>
		//Group1 Invalid VelHigh
	public const  int Error_1036013 = 1036013;
	/// <summary>運動群組1初速度無效</summary>
	/// <remarks></remarks>
		//Group1 Invalid VelLow
	public const  int Error_1036014 = 1036014;
	/// <summary>運動群組1觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//Group1 Cmp Table Error.
	public const  int Error_1036015 = 1036015;
	/// <summary>運動群組1復歸命令異常!</summary>
	/// <remarks></remarks>
		//Group1 Command Home Error!
	public const  int Error_1036016 = 1036016;
	/// <summary>運動群組1命令執行失敗!</summary>
	/// <remarks></remarks>
		//Group1 Command Error!
	public const  int Error_1036017 = 1036017;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1036018 = 1036018;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1036019 = 1036019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1036020 = 1036020;
	/// <summary>Conveyor1軸命令執行失敗</summary>
	/// <remarks></remarks>
		//Conveyor1 Axis Command Error!
	public const  int Error_1037000 = 1037000;
	/// <summary>Conveyor1軸復歸逾時</summary>
	/// <remarks></remarks>
		//Conveyor1Axis wait Home Timeout!
	public const  int Error_1037001 = 1037001;
	/// <summary>Conveyor1軸馬達Alarm</summary>
	/// <remarks></remarks>
		//Conveyor1Axis is Alarm!
	public const  int Error_1037002 = 1037002;
	/// <summary>Conveyor1軸馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//Conveyor1Axis Get Motor Status Failed!
	public const  int Error_1037003 = 1037003;
	/// <summary>Conveyor1軸等待到位逾時</summary>
	/// <remarks></remarks>
		//Conveyor1Axis wait INP Timeout!
	public const  int Error_1037004 = 1037004;
	/// <summary>Conveyor1軸移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//Conveyor1Axis Command is Out of SPEL
	public const  int Error_1037005 = 1037005;
	/// <summary>Conveyor1軸移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//Conveyor1Axis Command is Out of SNEL
	public const  int Error_1037006 = 1037006;
	/// <summary>Conveyor1軸接觸硬體正極限</summary>
	/// <remarks></remarks>
		//Conveyor1Axis Touch HPEL
	public const  int Error_1037007 = 1037007;
	/// <summary>Conveyor1軸接觸硬體負極限</summary>
	/// <remarks></remarks>
		//Conveyor1Axis Touch HNEL
	public const  int Error_1037008 = 1037008;
	/// <summary>Conveyor1軸錯誤停止</summary>
	/// <remarks></remarks>
		//Conveyor1Axis Error Stop
	public const  int Error_1037009 = 1037009;
	/// <summary>Conveyor1軸參數無效</summary>
	/// <remarks></remarks>
		//Conveyor1Axis Invalid Parameter
	public const  int Error_1037010 = 1037010;
	/// <summary>Conveyor1軸加速度無效</summary>
	/// <remarks></remarks>
		//Conveyor1Axis Invalid Acc
	public const  int Error_1037011 = 1037011;
	/// <summary>Conveyor1軸減速度無效</summary>
	/// <remarks></remarks>
		//Conveyor1Axis Invalid Dec
	public const  int Error_1037012 = 1037012;
	/// <summary>Conveyor1軸最大速度無效</summary>
	/// <remarks></remarks>
		//Conveyor1Axis Invalid VelHigh
	public const  int Error_1037013 = 1037013;
	/// <summary>Conveyor1軸初速度無效</summary>
	/// <remarks></remarks>
		//Conveyor1Axis Invalid VelLow
	public const  int Error_1037014 = 1037014;
	/// <summary>Conveyor1觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//Conveyor1 Cmp Table Error.
	public const  int Error_1037015 = 1037015;
	/// <summary>Conveyor1復歸命令異常!</summary>
	/// <remarks></remarks>
		//Conveyor1 Command Home Error!
	public const  int Error_1037016 = 1037016;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1037017 = 1037017;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1037018 = 1037018;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1037019 = 1037019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1037020 = 1037020;
	/// <summary>Conveyor2軸命令執行失敗</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Command Error!
	public const  int Error_1038000 = 1038000;
	/// <summary>Conveyor2軸復歸逾時</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis wait Home Timeout!
	public const  int Error_1038001 = 1038001;
	/// <summary>Conveyor2軸馬達Alarm</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis is Alarm!
	public const  int Error_1038002 = 1038002;
	/// <summary>Conveyor2軸馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Get Motor Status Failed!
	public const  int Error_1038003 = 1038003;
	/// <summary>Conveyor2軸等待到位逾時</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis wait INP Timeout!
	public const  int Error_1038004 = 1038004;
	/// <summary>Conveyor2軸移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Command is Out of SPEL
	public const  int Error_1038005 = 1038005;
	/// <summary>Conveyor2軸移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Command is Out of SNEL
	public const  int Error_1038006 = 1038006;
	/// <summary>Conveyor2軸接觸硬體正極限</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Touch HPEL
	public const  int Error_1038007 = 1038007;
	/// <summary>Conveyor2軸接觸硬體負極限</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Touch HNEL
	public const  int Error_1038008 = 1038008;
	/// <summary>Conveyor2軸錯誤停止</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Error Stop
	public const  int Error_1038009 = 1038009;
	/// <summary>Conveyor2軸參數無效</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Invalid Parameter
	public const  int Error_1038010 = 1038010;
	/// <summary>Conveyor2軸加速度無效</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Invalid Acc
	public const  int Error_1038011 = 1038011;
	/// <summary>Conveyor2軸減速度無效</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Invalid Dec
	public const  int Error_1038012 = 1038012;
	/// <summary>Conveyor2軸最大速度無效</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Invalid VelHigh
	public const  int Error_1038013 = 1038013;
	/// <summary>Conveyor2軸初速度無效</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Invalid VelLow
	public const  int Error_1038014 = 1038014;
	/// <summary>Conveyor2觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//Conveyor2 Cmp Table Error.
	public const  int Error_1038015 = 1038015;
	/// <summary>Conveyor2復歸命令異常!</summary>
	/// <remarks></remarks>
		//Conveyor2 Command Home Error!
	public const  int Error_1038016 = 1038016;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1038017 = 1038017;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1038018 = 1038018;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1038019 = 1038019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1038020 = 1038020;
	/// <summary>S1軸命令執行失敗</summary>
	/// <remarks></remarks>
		//S1 Axis Command Error!
	public const  int Error_1039000 = 1039000;
	/// <summary>S1軸復歸逾時</summary>
	/// <remarks></remarks>
		//S1 Axis wait Home Timeout!
	public const  int Error_1039001 = 1039001;
	/// <summary>S1軸馬達Alarm</summary>
	/// <remarks></remarks>
		//S1 Axis is Alarm!
	public const  int Error_1039002 = 1039002;
	/// <summary>S1軸馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//S1 Axis Get Motor Status Failed!
	public const  int Error_1039003 = 1039003;
	/// <summary>S1軸等待到位逾時</summary>
	/// <remarks></remarks>
		//S1 Axis wait INP Timeout!
	public const  int Error_1039004 = 1039004;
	/// <summary>S1軸移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//S1 Axis Command is Out of SPEL
	public const  int Error_1039005 = 1039005;
	/// <summary>S1軸移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//S1 Axis Command is Out of SNEL
	public const  int Error_1039006 = 1039006;
	/// <summary>S1軸接觸硬體正極限</summary>
	/// <remarks></remarks>
		//S1 Axis Touch HPEL
	public const  int Error_1039007 = 1039007;
	/// <summary>S1軸接觸硬體負極限</summary>
	/// <remarks></remarks>
		//S1 Axis Touch HNEL
	public const  int Error_1039008 = 1039008;
	/// <summary>S1軸錯誤停止</summary>
	/// <remarks></remarks>
		//S1 Axis Error Stop
	public const  int Error_1039009 = 1039009;
	/// <summary>S1軸參數無效</summary>
	/// <remarks></remarks>
		//S1 Axis Invalid Parameter
	public const  int Error_1039010 = 1039010;
	/// <summary>S1軸加速度無效</summary>
	/// <remarks></remarks>
		//S1 Axis Invalid Acc
	public const  int Error_1039011 = 1039011;
	/// <summary>S1軸減速度無效</summary>
	/// <remarks></remarks>
		//S1 Axis Invalid Dec
	public const  int Error_1039012 = 1039012;
	/// <summary>S1軸最大速度無效</summary>
	/// <remarks></remarks>
		//S1 Axis Invalid VelHigh
	public const  int Error_1039013 = 1039013;
	/// <summary>S1軸初速度無效</summary>
	/// <remarks></remarks>
		//S1 Axis Invalid VelLow
	public const  int Error_1039014 = 1039014;
	/// <summary>S1軸觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//S1 Axis Cmp Table Error.
	public const  int Error_1039015 = 1039015;
	/// <summary>S1軸復歸命令異常!</summary>
	/// <remarks></remarks>
		//S1 Axis Command Home Error!
	public const  int Error_1039016 = 1039016;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1039017 = 1039017;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1039018 = 1039018;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1039019 = 1039019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1039020 = 1039020;
	/// <summary>S2軸命令執行失敗</summary>
	/// <remarks></remarks>
		//S2 Axis Command Error!
	public const  int Error_1040000 = 1040000;
	/// <summary>S2軸復歸逾時</summary>
	/// <remarks></remarks>
		//S2 Axis wait Home Timeout!
	public const  int Error_1040001 = 1040001;
	/// <summary>S2軸馬達Alarm</summary>
	/// <remarks></remarks>
		//S2 Axis is Alarm!
	public const  int Error_1040002 = 1040002;
	/// <summary>S2軸馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//S2 Axis Get Motor Status Failed!
	public const  int Error_1040003 = 1040003;
	/// <summary>S2軸等待到位逾時</summary>
	/// <remarks></remarks>
		//S2 Axis wait INP Timeout!
	public const  int Error_1040004 = 1040004;
	/// <summary>S2軸移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//S2 Axis Command is Out of SPEL
	public const  int Error_1040005 = 1040005;
	/// <summary>S2軸移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//S2 Axis Command is Out of SNEL
	public const  int Error_1040006 = 1040006;
	/// <summary>S2軸接觸硬體正極限</summary>
	/// <remarks></remarks>
		//S2 Axis Touch HPEL
	public const  int Error_1040007 = 1040007;
	/// <summary>S2軸接觸硬體負極限</summary>
	/// <remarks></remarks>
		//S2 Axis Touch HNEL
	public const  int Error_1040008 = 1040008;
	/// <summary>S2軸錯誤停止</summary>
	/// <remarks></remarks>
		//S2 Axis Error Stop
	public const  int Error_1040009 = 1040009;
	/// <summary>S2軸參數無效</summary>
	/// <remarks></remarks>
		//S2 Axis Invalid Parameter
	public const  int Error_1040010 = 1040010;
	/// <summary>S2軸加速度無效</summary>
	/// <remarks></remarks>
		//S2 Axis Invalid Acc
	public const  int Error_1040011 = 1040011;
	/// <summary>S2軸減速度無效</summary>
	/// <remarks></remarks>
		//S2 Axis Invalid Dec
	public const  int Error_1040012 = 1040012;
	/// <summary>S2軸最大速度無效</summary>
	/// <remarks></remarks>
		//S2 Axis Invalid VelHigh
	public const  int Error_1040013 = 1040013;
	/// <summary>S2軸初速度無效</summary>
	/// <remarks></remarks>
		//S2 Axis Invalid VelLow
	public const  int Error_1040014 = 1040014;
	/// <summary>S2軸觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//S2 Axis Cmp Table Error.
	public const  int Error_1040015 = 1040015;
	/// <summary>S2軸復歸命令異常!</summary>
	/// <remarks></remarks>
		//S2 Axis Command Home Error!
	public const  int Error_1040016 = 1040016;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1040017 = 1040017;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1040018 = 1040018;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1040019 = 1040019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1040020 = 1040020;
	/// <summary>S3軸命令執行失敗</summary>
	/// <remarks></remarks>
		//S3 Axis Command Error!
	public const  int Error_1041000 = 1041000;
	/// <summary>S3軸復歸逾時</summary>
	/// <remarks></remarks>
		//S3 Axis wait Home Timeout!
	public const  int Error_1041001 = 1041001;
	/// <summary>S3軸馬達Alarm</summary>
	/// <remarks></remarks>
		//S3 Axis is Alarm!
	public const  int Error_1041002 = 1041002;
	/// <summary>S3軸馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//S3 Axis Get Motor Status Failed!
	public const  int Error_1041003 = 1041003;
	/// <summary>S3軸等待到位逾時</summary>
	/// <remarks></remarks>
		//S3 Axis wait INP Timeout!
	public const  int Error_1041004 = 1041004;
	/// <summary>S3軸移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//S3 Axis Command is Out of SPEL
	public const  int Error_1041005 = 1041005;
	/// <summary>S3軸移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//S3 Axis Command is Out of SNEL
	public const  int Error_1041006 = 1041006;
	/// <summary>S3軸接觸硬體正極限</summary>
	/// <remarks></remarks>
		//S3 Axis Touch HPEL
	public const  int Error_1041007 = 1041007;
	/// <summary>S3軸接觸硬體負極限</summary>
	/// <remarks></remarks>
		//S3 Axis Touch HNEL
	public const  int Error_1041008 = 1041008;
	/// <summary>S3軸錯誤停止</summary>
	/// <remarks></remarks>
		//S3 Axis Error Stop
	public const  int Error_1041009 = 1041009;
	/// <summary>S3軸參數無效</summary>
	/// <remarks></remarks>
		//S3 Axis Invalid Parameter
	public const  int Error_1041010 = 1041010;
	/// <summary>S3軸加速度無效</summary>
	/// <remarks></remarks>
		//S3 Axis Invalid Acc
	public const  int Error_1041011 = 1041011;
	/// <summary>S3軸減速度無效</summary>
	/// <remarks></remarks>
		//S3 Axis Invalid Dec
	public const  int Error_1041012 = 1041012;
	/// <summary>S3軸最大速度無效</summary>
	/// <remarks></remarks>
		//S3 Axis Invalid VelHigh
	public const  int Error_1041013 = 1041013;
	/// <summary>S3軸初速度無效</summary>
	/// <remarks></remarks>
		//S3 Axis Invalid VelLow
	public const  int Error_1041014 = 1041014;
	/// <summary>S3軸觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//S3 Axis Cmp Table Error.
	public const  int Error_1041015 = 1041015;
	/// <summary>S3軸復歸命令異常!</summary>
	/// <remarks></remarks>
		//S3 Axis Command Home Error!
	public const  int Error_1041016 = 1041016;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1041017 = 1041017;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1041018 = 1041018;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1041019 = 1041019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1041020 = 1041020;
	/// <summary>A_R_DP_X軸移動失敗</summary>
	/// <remarks></remarks>
		//A_R_DP_X Axis Move Error!
	public const  int Error_1042000 = 1042000;
	/// <summary>A_R_DP_X 軸復歸逾時</summary>
	/// <remarks></remarks>
		//A_R_DP_X Axis wait Home Timeout!
	public const  int Error_1042001 = 1042001;
	/// <summary>A_R_DP_X 軸馬達Alarm</summary>
	/// <remarks></remarks>
		//A_R_DP_X Axis is Alarm!
	public const  int Error_1042002 = 1042002;
	/// <summary>A_R_DP_X 軸馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//A_R_DP_X Axis Get Motor Status Failed!
	public const  int Error_1042003 = 1042003;
	/// <summary>A_R_DP_X 軸等待到位逾時</summary>
	/// <remarks></remarks>
		//A_R_DP_X Axis wait INP Timeout!
	public const  int Error_1042004 = 1042004;
	/// <summary>A_R_DP_X 軸移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//A_R_DP_X Axis Command is Out of SPEL
	public const  int Error_1042005 = 1042005;
	/// <summary>A_R_DP_X 軸移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//A_R_DP_X Axis Command is Out of SNEL
	public const  int Error_1042006 = 1042006;
	/// <summary>A_R_DP_X 軸接觸硬體正極限</summary>
	/// <remarks></remarks>
		//A_R_DP_X Axis Touch HPEL
	public const  int Error_1042007 = 1042007;
	/// <summary>A_R_DP_X 軸接觸硬體負極限</summary>
	/// <remarks></remarks>
		//A_R_DP_X Axis Touch HNEL
	public const  int Error_1042008 = 1042008;
	/// <summary>A_R_DP_X 軸錯誤停止</summary>
	/// <remarks></remarks>
		//A_R_DP_X Axis Error Stop
	public const  int Error_1042009 = 1042009;
	/// <summary>A_R_DP_X 軸參數無效</summary>
	/// <remarks></remarks>
		//A_R_DP_X Axis Invalid Parameter
	public const  int Error_1042010 = 1042010;
	/// <summary>A_R_DP_X 軸加速度無效</summary>
	/// <remarks></remarks>
		//A_R_DP_X Axis Invalid Acc
	public const  int Error_1042011 = 1042011;
	/// <summary>A_R_DP_X 軸減速度無效</summary>
	/// <remarks></remarks>
		//A_R_DP_X Axis Invalid Dec
	public const  int Error_1042012 = 1042012;
	/// <summary>A_R_DP_X 軸最大速度無效</summary>
	/// <remarks></remarks>
		//A_R_DP_X Axis Invalid VelHigh
	public const  int Error_1042013 = 1042013;
	/// <summary>A_R_DP_X 軸初速度無效</summary>
	/// <remarks></remarks>
		//A_R_DP_X Axis Invalid VelLow
	public const  int Error_1042014 = 1042014;
	/// <summary>A_R_DP_X 軸觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//A_R_DP_X Axis Cmp Table Error.
	public const  int Error_1042015 = 1042015;
	/// <summary>A_R_DP_X 軸復歸命令異常!</summary>
	/// <remarks></remarks>
		//A_R_DP_X Axis Command Home Error!
	public const  int Error_1042016 = 1042016;
	/// <summary>A_R_DP_X軸設定速度失敗!</summary>
	/// <remarks></remarks>
		//A_R_DP_X Axis SetSpeed Error!
	public const  int Error_1042017 = 1042017;
	/// <summary>A_R_DP_X軸命令執行失敗!</summary>
	/// <remarks></remarks>
		//A_R_DP_X Axis Command Error!
	public const  int Error_1042018 = 1042018;
	/// <summary>A_L_DP_X軸命令執行逾時!</summary>
	/// <remarks></remarks>
		//A_R_DP_X Axis Command is TimeOut!
	public const  int Error_1042019 = 1042019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1042020 = 1042020;
	/// <summary>A_R_DP_Y軸移動失敗</summary>
	/// <remarks></remarks>
		//A_R_DP_Y Axis Move Error!
	public const  int Error_1043000 = 1043000;
	/// <summary>A_R_DP_Y軸復歸逾時</summary>
	/// <remarks></remarks>
		//A_R_DP_Y Axis wait Home Timeout!
	public const  int Error_1043001 = 1043001;
	/// <summary>A_R_DP_Y軸馬達Alarm</summary>
	/// <remarks></remarks>
		//A_R_DP_Y Axis is Alarm!
	public const  int Error_1043002 = 1043002;
	/// <summary>A_R_DP_Y軸馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//A_R_DP_Y Axis Get Motor Status Failed!
	public const  int Error_1043003 = 1043003;
	/// <summary>A_R_DP_Y軸等待到位逾時</summary>
	/// <remarks></remarks>
		//A_R_DP_Y Axis wait INP Timeout!
	public const  int Error_1043004 = 1043004;
	/// <summary>A_R_DP_Y軸移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//A_R_DP_Y Axis Command is Out of SPEL
	public const  int Error_1043005 = 1043005;
	/// <summary>A_R_DP_Y軸移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//A_R_DP_Y Axis Command is Out of SNEL
	public const  int Error_1043006 = 1043006;
	/// <summary>A_R_DP_Y軸接觸硬體正極限</summary>
	/// <remarks></remarks>
		//A_R_DP_Y Axis Touch HPEL
	public const  int Error_1043007 = 1043007;
	/// <summary>A_R_DP_Y軸接觸硬體負極限</summary>
	/// <remarks></remarks>
		//A_R_DP_Y Axis Touch HNEL
	public const  int Error_1043008 = 1043008;
	/// <summary>A_R_DP_Y軸錯誤停止</summary>
	/// <remarks></remarks>
		//A_R_DP_Y Axis Error Stop
	public const  int Error_1043009 = 1043009;
	/// <summary>A_R_DP_Y軸參數無效</summary>
	/// <remarks></remarks>
		//A_R_DP_Y Axis Invalid Parameter
	public const  int Error_1043010 = 1043010;
	/// <summary>A_R_DP_Y軸加速度無效</summary>
	/// <remarks></remarks>
		//A_R_DP_Y Axis Invalid Acc
	public const  int Error_1043011 = 1043011;
	/// <summary>A_R_DP_Y軸減速度無效</summary>
	/// <remarks></remarks>
		//A_R_DP_Y Axis Invalid Dec
	public const  int Error_1043012 = 1043012;
	/// <summary>A_R_DP_Y軸最大速度無效</summary>
	/// <remarks></remarks>
		//A_R_DP_Y Axis Invalid VelHigh
	public const  int Error_1043013 = 1043013;
	/// <summary>A_R_DP_Y軸初速度無效</summary>
	/// <remarks></remarks>
		//A_R_DP_Y Axis Invalid VelLow
	public const  int Error_1043014 = 1043014;
	/// <summary>A_R_DP_Y軸觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//A_R_DP_Y Axis Cmp Table Error.
	public const  int Error_1043015 = 1043015;
	/// <summary>A_R_DP_Y軸復歸命令異常!</summary>
	/// <remarks></remarks>
		//A_R_DP_Y Axis Command Home Error!
	public const  int Error_1043016 = 1043016;
	/// <summary>A_R_DP_Y軸設定速度失敗!</summary>
	/// <remarks></remarks>
		//A_R_DP_Y Axis SetSpeed Error!
	public const  int Error_1043017 = 1043017;
	/// <summary>A_R_DP_Y軸命令執行失敗!</summary>
	/// <remarks></remarks>
		//A_R_DP_Y Axis Command Error!
	public const  int Error_1043018 = 1043018;
	/// <summary>A_L_DP_Y軸命令執行逾時!</summary>
	/// <remarks></remarks>
		//A_R_DP_Y Axis Command is TimeOut!
	public const  int Error_1043019 = 1043019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1043020 = 1043020;
	/// <summary>A_R_DP_Z軸移動失敗</summary>
	/// <remarks></remarks>
		//A_R_DP_Z Axis Move Error!
	public const  int Error_1044000 = 1044000;
	/// <summary>A_R_DP_Z 軸復歸逾時</summary>
	/// <remarks></remarks>
		//A_R_DP_Z Axis wait Home Timeout!
	public const  int Error_1044001 = 1044001;
	/// <summary>A_R_DP_Z 軸馬達Alarm</summary>
	/// <remarks></remarks>
		//A_R_DP_Z Axis is Alarm!
	public const  int Error_1044002 = 1044002;
	/// <summary>A_R_DP_Z 軸馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//A_R_DP_Z Axis Get Motor Status Failed!
	public const  int Error_1044003 = 1044003;
	/// <summary>A_R_DP_Z 軸等待到位逾時</summary>
	/// <remarks></remarks>
		//A_R_DP_Z Axis wait INP Timeout!
	public const  int Error_1044004 = 1044004;
	/// <summary>A_R_DP_Z 軸移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//A_R_DP_Z Axis Command is Out of SPEL
	public const  int Error_1044005 = 1044005;
	/// <summary>A_R_DP_Z 軸移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//A_R_DP_Z Axis Command is Out of SNEL
	public const  int Error_1044006 = 1044006;
	/// <summary>A_R_DP_Z 軸接觸硬體正極限</summary>
	/// <remarks></remarks>
		//A_R_DP_Z Axis Touch HPEL
	public const  int Error_1044007 = 1044007;
	/// <summary>A_R_DP_Z 軸接觸硬體負極限</summary>
	/// <remarks></remarks>
		//A_R_DP_Z Axis Touch HNEL
	public const  int Error_1044008 = 1044008;
	/// <summary>A_R_DP_Z 軸錯誤停止</summary>
	/// <remarks></remarks>
		//A_R_DP_Z Axis Error Stop
	public const  int Error_1044009 = 1044009;
	/// <summary>A_R_DP_Z 軸參數無效</summary>
	/// <remarks></remarks>
		//A_R_DP_Z Axis Invalid Parameter
	public const  int Error_1044010 = 1044010;
	/// <summary>A_R_DP_Z 軸加速度無效</summary>
	/// <remarks></remarks>
		//A_R_DP_Z Axis Invalid Acc
	public const  int Error_1044011 = 1044011;
	/// <summary>A_R_DP_Z 軸減速度無效</summary>
	/// <remarks></remarks>
		//A_R_DP_Z Axis Invalid Dec
	public const  int Error_1044012 = 1044012;
	/// <summary>A_R_DP_Z 軸最大速度無效</summary>
	/// <remarks></remarks>
		//A_R_DP_Z Axis Invalid VelHigh
	public const  int Error_1044013 = 1044013;
	/// <summary>A_R_DP_Z 軸初速度無效</summary>
	/// <remarks></remarks>
		//A_R_DP_Z Axis Invalid VelLow
	public const  int Error_1044014 = 1044014;
	/// <summary>A_R_DP_Z 軸觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//A_R_DP_Z Axis Cmp Table Error.
	public const  int Error_1044015 = 1044015;
	/// <summary>A_R_DP_Z 軸復歸命令異常!</summary>
	/// <remarks></remarks>
		//A_R_DP_Z Axis Command Home Error!
	public const  int Error_1044016 = 1044016;
	/// <summary>A_R_DP_Z軸設定速度失敗!</summary>
	/// <remarks></remarks>
		//A_R_DP_Z Axis SetSpeed Error!
	public const  int Error_1044017 = 1044017;
	/// <summary>A_R_DP_Z軸命令執行失敗!</summary>
	/// <remarks></remarks>
		//A_R_DP_Z Axis Command Error!
	public const  int Error_1044018 = 1044018;
	/// <summary>A_L_DP_Z軸命令執行逾時!</summary>
	/// <remarks></remarks>
		//A_R_DP_Z Axis Command is TimeOut!
	public const  int Error_1044019 = 1044019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1044020 = 1044020;
	/// <summary>V2軸命令執行失敗</summary>
	/// <remarks></remarks>
		//V2 Axis Command Error!
	public const  int Error_1045000 = 1045000;
	/// <summary>V2軸復歸逾時</summary>
	/// <remarks></remarks>
		//V2 Axis wait Home Timeout!
	public const  int Error_1045001 = 1045001;
	/// <summary>V2軸馬達Alarm</summary>
	/// <remarks></remarks>
		//V2 Axis is Alarm!
	public const  int Error_1045002 = 1045002;
	/// <summary>V2軸馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//V2 Axis Get Motor Status Failed!
	public const  int Error_1045003 = 1045003;
	/// <summary>V2軸等待到位逾時</summary>
	/// <remarks></remarks>
		//V2 Axis wait INP Timeout!
	public const  int Error_1045004 = 1045004;
	/// <summary>V2軸移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//V2 Axis Command is Out of SPEL
	public const  int Error_1045005 = 1045005;
	/// <summary>V2軸移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//V2 Axis Command is Out of SNEL
	public const  int Error_1045006 = 1045006;
	/// <summary>V2軸接觸硬體正極限</summary>
	/// <remarks></remarks>
		//V2 Axis Touch HPEL
	public const  int Error_1045007 = 1045007;
	/// <summary>V2軸接觸硬體負極限</summary>
	/// <remarks></remarks>
		//V2 Axis Touch HNEL
	public const  int Error_1045008 = 1045008;
	/// <summary>V2軸錯誤停止</summary>
	/// <remarks></remarks>
		//V2 Axis Error Stop
	public const  int Error_1045009 = 1045009;
	/// <summary>V2軸參數無效</summary>
	/// <remarks></remarks>
		//V2 Axis Invalid Parameter
	public const  int Error_1045010 = 1045010;
	/// <summary>V2軸加速度無效</summary>
	/// <remarks></remarks>
		//V2 Axis Invalid Acc
	public const  int Error_1045011 = 1045011;
	/// <summary>V2軸減速度無效</summary>
	/// <remarks></remarks>
		//V2 Axis Invalid Dec
	public const  int Error_1045012 = 1045012;
	/// <summary>V2軸最大速度無效</summary>
	/// <remarks></remarks>
		//V2 Axis Invalid VelHigh
	public const  int Error_1045013 = 1045013;
	/// <summary>V2軸初速度無效</summary>
	/// <remarks></remarks>
		//V2 Axis Invalid VelLow
	public const  int Error_1045014 = 1045014;
	/// <summary>V2軸觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//V2 Axis Cmp Table Error.
	public const  int Error_1045015 = 1045015;
	/// <summary>V2軸復歸命令異常!</summary>
	/// <remarks></remarks>
		//V2 Axis Command Home Error!
	public const  int Error_1045016 = 1045016;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1045017 = 1045017;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1045018 = 1045018;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1045019 = 1045019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1045020 = 1045020;
	/// <summary>A_R_DP_Tilt移動失敗</summary>
	/// <remarks></remarks>
		//A_R_DP_Tilt  Move Error!
	public const  int Error_1046000 = 1046000;
	/// <summary>A_R_DP_Tilt復歸逾時</summary>
	/// <remarks></remarks>
		//A_R_DP_Tilt wait Home Timeout!
	public const  int Error_1046001 = 1046001;
	/// <summary>A_R_DP_Tilt馬達Alarm</summary>
	/// <remarks></remarks>
		//A_R_DP_Tilt is Alarm!
	public const  int Error_1046002 = 1046002;
	/// <summary>A_R_DP_Tilt馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//A_R_DP_Tilt Get Motor Status Failed!
	public const  int Error_1046003 = 1046003;
	/// <summary>A_R_DP_Tilt等待到位逾時</summary>
	/// <remarks></remarks>
		//A_R_DP_Tilt wait INP Timeout!
	public const  int Error_1046004 = 1046004;
	/// <summary>A_R_DP_Tilt移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//A_R_DP_Tilt Command is Out of SPEL
	public const  int Error_1046005 = 1046005;
	/// <summary>A_R_DP_Tilt移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//A_R_DP_Tilt Command is Out of SNEL
	public const  int Error_1046006 = 1046006;
	/// <summary>A_R_DP_Tilt接觸硬體正極限</summary>
	/// <remarks></remarks>
		//A_R_DP_Tilt Touch HPEL
	public const  int Error_1046007 = 1046007;
	/// <summary>A_R_DP_Tilt接觸硬體負極限</summary>
	/// <remarks></remarks>
		//A_R_DP_Tilt Touch HNEL
	public const  int Error_1046008 = 1046008;
	/// <summary>A_R_DP_Tilt錯誤停止</summary>
	/// <remarks></remarks>
		//A_R_DP_Tilt Error Stop
	public const  int Error_1046009 = 1046009;
	/// <summary>A_R_DP_Tilt參數無效</summary>
	/// <remarks></remarks>
		//A_R_DP_Tilt Invalid Parameter
	public const  int Error_1046010 = 1046010;
	/// <summary>A_R_DP_Tilt加速度無效</summary>
	/// <remarks></remarks>
		//A_R_DP_Tilt Invalid Acc
	public const  int Error_1046011 = 1046011;
	/// <summary>A_R_DP_Tilt減速度無效</summary>
	/// <remarks></remarks>
		//A_R_DP_Tilt Invalid Dec
	public const  int Error_1046012 = 1046012;
	/// <summary>A_R_DP_Tilt最大速度無效</summary>
	/// <remarks></remarks>
		//A_R_DP_Tilt Invalid VelHigh
	public const  int Error_1046013 = 1046013;
	/// <summary>A_R_DP_Tilt初速度無效</summary>
	/// <remarks></remarks>
		//A_R_DP_Tilt Invalid VelLow
	public const  int Error_1046014 = 1046014;
	/// <summary>A_R_DP_Tilt觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//A_R_DP_Tilt Cmp Table Error.
	public const  int Error_1046015 = 1046015;
	/// <summary>A_R_DP_Tilt復歸命令異常!</summary>
	/// <remarks></remarks>
		//A_R_DP_Tilt Command Home Error!
	public const  int Error_1046016 = 1046016;
	/// <summary>A_R_DP_Tilt軸設定速度失敗!</summary>
	/// <remarks></remarks>
		//A_R_DP_Tilt Axis SetSpeed Error!
	public const  int Error_1046017 = 1046017;
	/// <summary>A_R_DP_Tilt命令執行失敗</summary>
	/// <remarks></remarks>
		//A_R_DP_Tilt  Command Error!
	public const  int Error_1046018 = 1046018;
	/// <summary>A_L_DP_Tilt命令執行逾時!</summary>
	/// <remarks></remarks>
		//A_R_DP_Tilt Command is TimeOut!
	public const  int Error_1046019 = 1046019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1046020 = 1046020;
	/// <summary>運動群組2移動失敗</summary>
	/// <remarks></remarks>
		//Group2 Move Error!
	public const  int Error_1048000 = 1048000;
	/// <summary>運動群組2復歸逾時</summary>
	/// <remarks></remarks>
		//Group2 wait Home Timeout!
	public const  int Error_1048001 = 1048001;
	/// <summary>運動群組2馬達Alarm</summary>
	/// <remarks></remarks>
		//Group2 is Alarm!
	public const  int Error_1048002 = 1048002;
	/// <summary>運動群組2馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//Group2 Get Motor Status Failed!
	public const  int Error_1048003 = 1048003;
	/// <summary>運動群組2等待到位逾時</summary>
	/// <remarks></remarks>
		//Group2 wait INP Timeout!
	public const  int Error_1048004 = 1048004;
	/// <summary>運動群組2移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//Group2 Command is Out of SPEL
	public const  int Error_1048005 = 1048005;
	/// <summary>運動群組2移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//Group2 Command is Out of SNEL
	public const  int Error_1048006 = 1048006;
	/// <summary>運動群組2接觸硬體正極限</summary>
	/// <remarks></remarks>
		//Group2 Touch HPEL
	public const  int Error_1048007 = 1048007;
	/// <summary>運動群組2接觸硬體負極限</summary>
	/// <remarks></remarks>
		//Group2 Touch HNEL
	public const  int Error_1048008 = 1048008;
	/// <summary>運動群組2錯誤停止</summary>
	/// <remarks></remarks>
		//Group2 Error Stop
	public const  int Error_1048009 = 1048009;
	/// <summary>運動群組2參數無效</summary>
	/// <remarks></remarks>
		//Group2 Invalid Parameter
	public const  int Error_1048010 = 1048010;
	/// <summary>運動群組2加速度無效</summary>
	/// <remarks></remarks>
		//Group2 Invalid Acc
	public const  int Error_1048011 = 1048011;
	/// <summary>運動群組2減速度無效</summary>
	/// <remarks></remarks>
		//Group2 Invalid Dec
	public const  int Error_1048012 = 1048012;
	/// <summary>運動群組2最大速度無效</summary>
	/// <remarks></remarks>
		//Group2 Invalid VelHigh
	public const  int Error_1048013 = 1048013;
	/// <summary>運動群組2初速度無效</summary>
	/// <remarks></remarks>
		//Group2 Invalid VelLow
	public const  int Error_1048014 = 1048014;
	/// <summary>運動群組2觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//Group2 Cmp Table Error.
	public const  int Error_1048015 = 1048015;
	/// <summary>運動群組2復歸命令異常!</summary>
	/// <remarks></remarks>
		//Group2 Command Home Error!
	public const  int Error_1048016 = 1048016;
	/// <summary>運動群組2命令執行失敗!</summary>
	/// <remarks></remarks>
		//Group2 Command Error!
	public const  int Error_1048017 = 1048017;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1048018 = 1048018;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1048019 = 1048019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1048020 = 1048020;
	/// <summary>Conveyor1軸命令執行失敗</summary>
	/// <remarks></remarks>
		//Conveyor1 Axis Command Error!
	public const  int Error_1049000 = 1049000;
	/// <summary>Conveyor1軸復歸逾時</summary>
	/// <remarks></remarks>
		//Conveyor1Axis wait Home Timeout!
	public const  int Error_1049001 = 1049001;
	/// <summary>Conveyor1軸馬達Alarm</summary>
	/// <remarks></remarks>
		//Conveyor1Axis is Alarm!
	public const  int Error_1049002 = 1049002;
	/// <summary>Conveyor1軸馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//Conveyor1Axis Get Motor Status Failed!
	public const  int Error_1049003 = 1049003;
	/// <summary>Conveyor1軸等待到位逾時</summary>
	/// <remarks></remarks>
		//Conveyor1Axis wait INP Timeout!
	public const  int Error_1049004 = 1049004;
	/// <summary>Conveyor1軸移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//Conveyor1Axis Command is Out of SPEL
	public const  int Error_1049005 = 1049005;
	/// <summary>Conveyor1軸移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//Conveyor1Axis Command is Out of SNEL
	public const  int Error_1049006 = 1049006;
	/// <summary>Conveyor1軸接觸硬體正極限</summary>
	/// <remarks></remarks>
		//Conveyor1Axis Touch HPEL
	public const  int Error_1049007 = 1049007;
	/// <summary>Conveyor1軸接觸硬體負極限</summary>
	/// <remarks></remarks>
		//Conveyor1Axis Touch HNEL
	public const  int Error_1049008 = 1049008;
	/// <summary>Conveyor1軸錯誤停止</summary>
	/// <remarks></remarks>
		//Conveyor1Axis Error Stop
	public const  int Error_1049009 = 1049009;
	/// <summary>Conveyor1軸參數無效</summary>
	/// <remarks></remarks>
		//Conveyor1Axis Invalid Parameter
	public const  int Error_1049010 = 1049010;
	/// <summary>Conveyor1軸加速度無效</summary>
	/// <remarks></remarks>
		//Conveyor1Axis Invalid Acc
	public const  int Error_1049011 = 1049011;
	/// <summary>Conveyor1軸減速度無效</summary>
	/// <remarks></remarks>
		//Conveyor1Axis Invalid Dec
	public const  int Error_1049012 = 1049012;
	/// <summary>Conveyor1軸最大速度無效</summary>
	/// <remarks></remarks>
		//Conveyor1Axis Invalid VelHigh
	public const  int Error_1049013 = 1049013;
	/// <summary>Conveyor1軸初速度無效</summary>
	/// <remarks></remarks>
		//Conveyor1Axis Invalid VelLow
	public const  int Error_1049014 = 1049014;
	/// <summary>Conveyor1觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//Conveyor1 Cmp Table Error.
	public const  int Error_1049015 = 1049015;
	/// <summary>Conveyor1復歸命令異常!</summary>
	/// <remarks></remarks>
		//Conveyor1 Command Home Error!
	public const  int Error_1049016 = 1049016;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1049017 = 1049017;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1049018 = 1049018;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1049019 = 1049019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1049020 = 1049020;
	/// <summary>Conveyor2軸命令執行失敗</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Command Error!
	public const  int Error_1050000 = 1050000;
	/// <summary>Conveyor2軸復歸逾時</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis wait Home Timeout!
	public const  int Error_1050001 = 1050001;
	/// <summary>Conveyor2軸馬達Alarm</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis is Alarm!
	public const  int Error_1050002 = 1050002;
	/// <summary>Conveyor2軸馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Get Motor Status Failed!
	public const  int Error_1050003 = 1050003;
	/// <summary>Conveyor2軸等待到位逾時</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis wait INP Timeout!
	public const  int Error_1050004 = 1050004;
	/// <summary>Conveyor2軸移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Command is Out of SPEL
	public const  int Error_1050005 = 1050005;
	/// <summary>Conveyor2軸移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Command is Out of SNEL
	public const  int Error_1050006 = 1050006;
	/// <summary>Conveyor2軸接觸硬體正極限</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Touch HPEL
	public const  int Error_1050007 = 1050007;
	/// <summary>Conveyor2軸接觸硬體負極限</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Touch HNEL
	public const  int Error_1050008 = 1050008;
	/// <summary>Conveyor2軸錯誤停止</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Error Stop
	public const  int Error_1050009 = 1050009;
	/// <summary>Conveyor2軸參數無效</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Invalid Parameter
	public const  int Error_1050010 = 1050010;
	/// <summary>Conveyor2軸加速度無效</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Invalid Acc
	public const  int Error_1050011 = 1050011;
	/// <summary>Conveyor2軸減速度無效</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Invalid Dec
	public const  int Error_1050012 = 1050012;
	/// <summary>Conveyor2軸最大速度無效</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Invalid VelHigh
	public const  int Error_1050013 = 1050013;
	/// <summary>Conveyor2軸初速度無效</summary>
	/// <remarks></remarks>
		//Conveyor2 Axis Invalid VelLow
	public const  int Error_1050014 = 1050014;
	/// <summary>Conveyor2觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//Conveyor2 Cmp Table Error.
	public const  int Error_1050015 = 1050015;
	/// <summary>Conveyor2復歸命令異常!</summary>
	/// <remarks></remarks>
		//Conveyor2 Command Home Error!
	public const  int Error_1050016 = 1050016;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1050017 = 1050017;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1050018 = 1050018;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1050019 = 1050019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1050020 = 1050020;
	/// <summary>S1軸命令執行失敗</summary>
	/// <remarks></remarks>
		//S1 Axis Command Error!
	public const  int Error_1051000 = 1051000;
	/// <summary>S1軸復歸逾時</summary>
	/// <remarks></remarks>
		//S1 Axis wait Home Timeout!
	public const  int Error_1051001 = 1051001;
	/// <summary>S1軸馬達Alarm</summary>
	/// <remarks></remarks>
		//S1 Axis is Alarm!
	public const  int Error_1051002 = 1051002;
	/// <summary>S1軸馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//S1 Axis Get Motor Status Failed!
	public const  int Error_1051003 = 1051003;
	/// <summary>S1軸等待到位逾時</summary>
	/// <remarks></remarks>
		//S1 Axis wait INP Timeout!
	public const  int Error_1051004 = 1051004;
	/// <summary>S1軸移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//S1 Axis Command is Out of SPEL
	public const  int Error_1051005 = 1051005;
	/// <summary>S1軸移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//S1 Axis Command is Out of SNEL
	public const  int Error_1051006 = 1051006;
	/// <summary>S1軸接觸硬體正極限</summary>
	/// <remarks></remarks>
		//S1 Axis Touch HPEL
	public const  int Error_1051007 = 1051007;
	/// <summary>S1軸接觸硬體負極限</summary>
	/// <remarks></remarks>
		//S1 Axis Touch HNEL
	public const  int Error_1051008 = 1051008;
	/// <summary>S1軸錯誤停止</summary>
	/// <remarks></remarks>
		//S1 Axis Error Stop
	public const  int Error_1051009 = 1051009;
	/// <summary>S1軸參數無效</summary>
	/// <remarks></remarks>
		//S1 Axis Invalid Parameter
	public const  int Error_1051010 = 1051010;
	/// <summary>S1軸加速度無效</summary>
	/// <remarks></remarks>
		//S1 Axis Invalid Acc
	public const  int Error_1051011 = 1051011;
	/// <summary>S1軸減速度無效</summary>
	/// <remarks></remarks>
		//S1 Axis Invalid Dec
	public const  int Error_1051012 = 1051012;
	/// <summary>S1軸最大速度無效</summary>
	/// <remarks></remarks>
		//S1 Axis Invalid VelHigh
	public const  int Error_1051013 = 1051013;
	/// <summary>S1軸初速度無效</summary>
	/// <remarks></remarks>
		//S1 Axis Invalid VelLow
	public const  int Error_1051014 = 1051014;
	/// <summary>S1軸觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//S1 Axis Cmp Table Error.
	public const  int Error_1051015 = 1051015;
	/// <summary>S1軸復歸命令異常!</summary>
	/// <remarks></remarks>
		//S1 Axis Command Home Error!
	public const  int Error_1051016 = 1051016;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1051017 = 1051017;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1051018 = 1051018;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1051019 = 1051019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1051020 = 1051020;
	/// <summary>A_Chuck_Z1軸命令執行失敗</summary>
	/// <remarks></remarks>
		//A_Chuck_Z1 Axis Command Error!
	public const  int Error_1052000 = 1052000;
	/// <summary>A_Chuck_Z1軸復歸逾時</summary>
	/// <remarks></remarks>
		//A_Chuck_Z1 Axis wait Home Timeout!
	public const  int Error_1052001 = 1052001;
	/// <summary>A_Chuck_Z1軸馬達Alarm</summary>
	/// <remarks></remarks>
		//A_Chuck_Z1 Axis is Alarm!
	public const  int Error_1052002 = 1052002;
	/// <summary>A_Chuck_Z1軸馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//A_Chuck_Z1 Axis Get Motor Status Failed!
	public const  int Error_1052003 = 1052003;
	/// <summary>A_Chuck_Z1軸等待到位逾時</summary>
	/// <remarks></remarks>
		//A_Chuck_Z1 Axis wait INP Timeout!
	public const  int Error_1052004 = 1052004;
	/// <summary>A_Chuck_Z1軸移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//A_Chuck_Z1 Axis Command is Out of SPEL
	public const  int Error_1052005 = 1052005;
	/// <summary>A_Chuck_Z1軸移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//A_Chuck_Z1 Axis Command is Out of SNEL
	public const  int Error_1052006 = 1052006;
	/// <summary>A_Chuck_Z1軸接觸硬體正極限</summary>
	/// <remarks></remarks>
		//A_Chuck_Z1 Axis Touch HPEL
	public const  int Error_1052007 = 1052007;
	/// <summary>A_Chuck_Z1軸接觸硬體負極限</summary>
	/// <remarks></remarks>
		//A_Chuck_Z1 Axis Touch HNEL
	public const  int Error_1052008 = 1052008;
	/// <summary>A_Chuck_Z1軸錯誤停止</summary>
	/// <remarks></remarks>
		//A_Chuck_Z1 Axis Error Stop
	public const  int Error_1052009 = 1052009;
	/// <summary>A_Chuck_Z1軸參數無效</summary>
	/// <remarks></remarks>
		//A_Chuck_Z1 Axis Invalid Parameter
	public const  int Error_1052010 = 1052010;
	/// <summary>A_Chuck_Z1軸加速度無效</summary>
	/// <remarks></remarks>
		//A_Chuck_Z1 Axis Invalid Acc
	public const  int Error_1052011 = 1052011;
	/// <summary>A_Chuck_Z1軸減速度無效</summary>
	/// <remarks></remarks>
		//A_Chuck_Z1 Axis Invalid Dec
	public const  int Error_1052012 = 1052012;
	/// <summary>A_Chuck_Z1軸最大速度無效</summary>
	/// <remarks></remarks>
		//A_Chuck_Z1 Axis Invalid VelHigh
	public const  int Error_1052013 = 1052013;
	/// <summary>A_Chuck_Z1軸初速度無效</summary>
	/// <remarks></remarks>
		//A_Chuck_Z1 Axis Invalid VelLow
	public const  int Error_1052014 = 1052014;
	/// <summary>A_Chuck_Z1軸觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//A_Chuck_Z1 Axis Cmp Table Error.
	public const  int Error_1052015 = 1052015;
	/// <summary>A_Chuck_Z1軸復歸命令異常!</summary>
	/// <remarks></remarks>
		//A_Chuck_Z1 Axis Command Home Error!
	public const  int Error_1052016 = 1052016;
	/// <summary>A_Chuck_Z1設定速度異常!</summary>
	/// <remarks></remarks>
		//A_Chuck_Z1 Set Speed Error!
	public const  int Error_1052017 = 1052017;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1052018 = 1052018;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1052019 = 1052019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1052020 = 1052020;
	/// <summary>B_Chuck_Z1軸命令執行失敗</summary>
	/// <remarks></remarks>
		//B_Chuck_Z1 Axis Command Error!
	public const  int Error_1053000 = 1053000;
	/// <summary>B_Chuck_Z1軸復歸逾時</summary>
	/// <remarks></remarks>
		//B_Chuck_Z1 Axis wait Home Timeout!
	public const  int Error_1053001 = 1053001;
	/// <summary>B_Chuck_Z1軸馬達Alarm</summary>
	/// <remarks></remarks>
		//B_Chuck_Z1 Axis is Alarm!
	public const  int Error_1053002 = 1053002;
	/// <summary>B_Chuck_Z1軸馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//B_Chuck_Z1 Axis Get Motor Status Failed!
	public const  int Error_1053003 = 1053003;
	/// <summary>B_Chuck_Z1軸等待到位逾時</summary>
	/// <remarks></remarks>
		//B_Chuck_Z1 Axis wait INP Timeout!
	public const  int Error_1053004 = 1053004;
	/// <summary>B_Chuck_Z1軸移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//B_Chuck_Z1 Axis Command is Out of SPEL
	public const  int Error_1053005 = 1053005;
	/// <summary>B_Chuck_Z1軸移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//B_Chuck_Z1 Axis Command is Out of SNEL
	public const  int Error_1053006 = 1053006;
	/// <summary>B_Chuck_Z1軸接觸硬體正極限</summary>
	/// <remarks></remarks>
		//B_Chuck_Z1 Axis Touch HPEL
	public const  int Error_1053007 = 1053007;
	/// <summary>B_Chuck_Z1軸接觸硬體負極限</summary>
	/// <remarks></remarks>
		//B_Chuck_Z1 Axis Touch HNEL
	public const  int Error_1053008 = 1053008;
	/// <summary>B_Chuck_Z1軸錯誤停止</summary>
	/// <remarks></remarks>
		//B_Chuck_Z1 Axis Error Stop
	public const  int Error_1053009 = 1053009;
	/// <summary>B_Chuck_Z1軸參數無效</summary>
	/// <remarks></remarks>
		//B_Chuck_Z1 Axis Invalid Parameter
	public const  int Error_1053010 = 1053010;
	/// <summary>B_Chuck_Z1軸加速度無效</summary>
	/// <remarks></remarks>
		//B_Chuck_Z1 Axis Invalid Acc
	public const  int Error_1053011 = 1053011;
	/// <summary>B_Chuck_Z1軸減速度無效</summary>
	/// <remarks></remarks>
		//B_Chuck_Z1 Axis Invalid Dec
	public const  int Error_1053012 = 1053012;
	/// <summary>B_Chuck_Z1軸最大速度無效</summary>
	/// <remarks></remarks>
		//B_Chuck_Z1 Axis Invalid VelHigh
	public const  int Error_1053013 = 1053013;
	/// <summary>B_Chuck_Z1軸初速度無效</summary>
	/// <remarks></remarks>
		//B_Chuck_Z1 Axis Invalid VelLow
	public const  int Error_1053014 = 1053014;
	/// <summary>B_Chuck_Z1軸觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//B_Chuck_Z1 Axis Cmp Table Error.
	public const  int Error_1053015 = 1053015;
	/// <summary>B_Chuck_Z1軸復歸命令異常!</summary>
	/// <remarks></remarks>
		//B_Chuck_Z1 Axis Command Home Error!
	public const  int Error_1053016 = 1053016;
	/// <summary>B_Chuck_Z1設定速度異常!</summary>
	/// <remarks></remarks>
		//B_Chuck_Z1 Set Speed Error!
	public const  int Error_1053017 = 1053017;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1053018 = 1053018;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1053019 = 1053019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1053020 = 1053020;
	/// <summary>B_L_DP_X軸移動失敗</summary>
	/// <remarks></remarks>
		//B_L_DP_X Axis Move Error!
	public const  int Error_1060000 = 1060000;
	/// <summary>B_L_DP_X 軸復歸逾時</summary>
	/// <remarks></remarks>
		//B_L_DP_X Axis wait Home Timeout!
	public const  int Error_1060001 = 1060001;
	/// <summary>B_L_DP_X 軸馬達Alarm</summary>
	/// <remarks></remarks>
		//B_L_DP_X Axis is Alarm!
	public const  int Error_1060002 = 1060002;
	/// <summary>B_L_DP_X 軸馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//B_L_DP_X Axis Get Motor Status Failed!
	public const  int Error_1060003 = 1060003;
	/// <summary>B_L_DP_X 軸等待到位逾時</summary>
	/// <remarks></remarks>
		//B_L_DP_X Axis wait INP Timeout!
	public const  int Error_1060004 = 1060004;
	/// <summary>B_L_DP_X 軸移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//B_L_DP_X Axis Command is Out of SPEL
	public const  int Error_1060005 = 1060005;
	/// <summary>B_L_DP_X 軸移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//B_L_DP_X Axis Command is Out of SNEL
	public const  int Error_1060006 = 1060006;
	/// <summary>B_L_DP_X 軸接觸硬體正極限</summary>
	/// <remarks></remarks>
		//B_L_DP_X Axis Touch HPEL
	public const  int Error_1060007 = 1060007;
	/// <summary>B_L_DP_X 軸接觸硬體負極限</summary>
	/// <remarks></remarks>
		//B_L_DP_X Axis Touch HNEL
	public const  int Error_1060008 = 1060008;
	/// <summary>B_L_DP_X 軸錯誤停止</summary>
	/// <remarks></remarks>
		//B_L_DP_X Axis Error Stop
	public const  int Error_1060009 = 1060009;
	/// <summary>B_L_DP_X 軸參數無效</summary>
	/// <remarks></remarks>
		//B_L_DP_X Axis Invalid Parameter
	public const  int Error_1060010 = 1060010;
	/// <summary>B_L_DP_X 軸加速度無效</summary>
	/// <remarks></remarks>
		//B_L_DP_X Axis Invalid Acc
	public const  int Error_1060011 = 1060011;
	/// <summary>B_L_DP_X 軸減速度無效</summary>
	/// <remarks></remarks>
		//B_L_DP_X Axis Invalid Dec
	public const  int Error_1060012 = 1060012;
	/// <summary>B_L_DP_X 軸最大速度無效</summary>
	/// <remarks></remarks>
		//B_L_DP_X Axis Invalid VelHigh
	public const  int Error_1060013 = 1060013;
	/// <summary>B_L_DP_X 軸初速度無效</summary>
	/// <remarks></remarks>
		//B_L_DP_X Axis Invalid VelLow
	public const  int Error_1060014 = 1060014;
	/// <summary>B_L_DP_X 軸觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//B_L_DP_X Axis Cmp Table Error.
	public const  int Error_1060015 = 1060015;
	/// <summary>B_L_DP_X 軸復歸命令異常!</summary>
	/// <remarks></remarks>
		//B_L_DP_X Axis Command Home Error!
	public const  int Error_1060016 = 1060016;
	/// <summary>B_L_DP_X軸設定速度失敗!</summary>
	/// <remarks></remarks>
		//B_L_DP_X Axis SetSpeed Error!
	public const  int Error_1060017 = 1060017;
	/// <summary>B_L_DP_X軸命令執行失敗!</summary>
	/// <remarks></remarks>
		//B_L_DP_X Axis Command Error!
	public const  int Error_1060018 = 1060018;
	/// <summary>B_L_DP_X軸命令執行逾時!</summary>
	/// <remarks></remarks>
		//B_L_DP_X Axis Command is TimeOut!
	public const  int Error_1060019 = 1060019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1060020 = 1060020;
	/// <summary>B_L_DP_Y軸移動失敗</summary>
	/// <remarks></remarks>
		//B_L_DP_Y Axis Move Error!
	public const  int Error_1061000 = 1061000;
	/// <summary>B_L_DP_Y軸復歸逾時</summary>
	/// <remarks></remarks>
		//B_L_DP_Y Axis wait Home Timeout!
	public const  int Error_1061001 = 1061001;
	/// <summary>B_L_DP_Y軸馬達Alarm</summary>
	/// <remarks></remarks>
		//B_L_DP_Y Axis is Alarm!
	public const  int Error_1061002 = 1061002;
	/// <summary>B_L_DP_Y軸馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//B_L_DP_Y Axis Get Motor Status Failed!
	public const  int Error_1061003 = 1061003;
	/// <summary>B_L_DP_Y軸等待到位逾時</summary>
	/// <remarks></remarks>
		//B_L_DP_Y Axis wait INP Timeout!
	public const  int Error_1061004 = 1061004;
	/// <summary>B_L_DP_Y軸移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//B_L_DP_Y Axis Command is Out of SPEL
	public const  int Error_1061005 = 1061005;
	/// <summary>B_L_DP_Y軸移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//B_L_DP_Y Axis Command is Out of SNEL
	public const  int Error_1061006 = 1061006;
	/// <summary>B_L_DP_Y軸接觸硬體正極限</summary>
	/// <remarks></remarks>
		//B_L_DP_Y Axis Touch HPEL
	public const  int Error_1061007 = 1061007;
	/// <summary>B_L_DP_Y軸接觸硬體負極限</summary>
	/// <remarks></remarks>
		//B_L_DP_Y Axis Touch HNEL
	public const  int Error_1061008 = 1061008;
	/// <summary>B_L_DP_Y軸錯誤停止</summary>
	/// <remarks></remarks>
		//B_L_DP_Y Axis Error Stop
	public const  int Error_1061009 = 1061009;
	/// <summary>B_L_DP_Y軸參數無效</summary>
	/// <remarks></remarks>
		//B_L_DP_Y Axis Invalid Parameter
	public const  int Error_1061010 = 1061010;
	/// <summary>B_L_DP_Y軸加速度無效</summary>
	/// <remarks></remarks>
		//B_L_DP_Y Axis Invalid Acc
	public const  int Error_1061011 = 1061011;
	/// <summary>B_L_DP_Y軸減速度無效</summary>
	/// <remarks></remarks>
		//B_L_DP_Y Axis Invalid Dec
	public const  int Error_1061012 = 1061012;
	/// <summary>B_L_DP_Y軸最大速度無效</summary>
	/// <remarks></remarks>
		//B_L_DP_Y Axis Invalid VelHigh
	public const  int Error_1061013 = 1061013;
	/// <summary>B_L_DP_Y軸初速度無效</summary>
	/// <remarks></remarks>
		//B_L_DP_Y Axis Invalid VelLow
	public const  int Error_1061014 = 1061014;
	/// <summary>B_L_DP_Y軸觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//B_L_DP_Y Axis Cmp Table Error.
	public const  int Error_1061015 = 1061015;
	/// <summary>B_L_DP_Y軸復歸命令異常!</summary>
	/// <remarks></remarks>
		//B_L_DP_Y Axis Command Home Error!
	public const  int Error_1061016 = 1061016;
	/// <summary>B_L_DP_Y軸設定速度失敗!</summary>
	/// <remarks></remarks>
		//B_L_DP_Y Axis SetSpeed Error!
	public const  int Error_1061017 = 1061017;
	/// <summary>B_L_DP_Y軸命令執行失敗!</summary>
	/// <remarks></remarks>
		//B_L_DP_Y Axis Command Error!
	public const  int Error_1061018 = 1061018;
	/// <summary>B_L_DP_Y軸命令執行逾時!</summary>
	/// <remarks></remarks>
		//B_L_DP_Y Axis Command is TimeOut!
	public const  int Error_1061019 = 1061019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1061020 = 1061020;
	/// <summary>B_L_DP_Z軸移動失敗</summary>
	/// <remarks></remarks>
		//B_L_DP_Z Axis Move Error!
	public const  int Error_1062000 = 1062000;
	/// <summary>B_L_DP_Z 軸復歸逾時</summary>
	/// <remarks></remarks>
		//B_L_DP_Z Axis wait Home Timeout!
	public const  int Error_1062001 = 1062001;
	/// <summary>B_L_DP_Z 軸馬達Alarm</summary>
	/// <remarks></remarks>
		//B_L_DP_Z Axis is Alarm!
	public const  int Error_1062002 = 1062002;
	/// <summary>B_L_DP_Z 軸馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//B_L_DP_Z Axis Get Motor Status Failed!
	public const  int Error_1062003 = 1062003;
	/// <summary>B_L_DP_Z 軸等待到位逾時</summary>
	/// <remarks></remarks>
		//B_L_DP_Z Axis wait INP Timeout!
	public const  int Error_1062004 = 1062004;
	/// <summary>B_L_DP_Z 軸移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//B_L_DP_Z Axis Command is Out of SPEL
	public const  int Error_1062005 = 1062005;
	/// <summary>B_L_DP_Z 軸移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//B_L_DP_Z Axis Command is Out of SNEL
	public const  int Error_1062006 = 1062006;
	/// <summary>B_L_DP_Z 軸接觸硬體正極限</summary>
	/// <remarks></remarks>
		//B_L_DP_Z Axis Touch HPEL
	public const  int Error_1062007 = 1062007;
	/// <summary>B_L_DP_Z 軸接觸硬體負極限</summary>
	/// <remarks></remarks>
		//B_L_DP_Z Axis Touch HNEL
	public const  int Error_1062008 = 1062008;
	/// <summary>B_L_DP_Z 軸錯誤停止</summary>
	/// <remarks></remarks>
		//B_L_DP_Z Axis Error Stop
	public const  int Error_1062009 = 1062009;
	/// <summary>B_L_DP_Z 軸參數無效</summary>
	/// <remarks></remarks>
		//B_L_DP_Z Axis Invalid Parameter
	public const  int Error_1062010 = 1062010;
	/// <summary>B_L_DP_Z 軸加速度無效</summary>
	/// <remarks></remarks>
		//B_L_DP_Z Axis Invalid Acc
	public const  int Error_1062011 = 1062011;
	/// <summary>B_L_DP_Z 軸減速度無效</summary>
	/// <remarks></remarks>
		//B_L_DP_Z Axis Invalid Dec
	public const  int Error_1062012 = 1062012;
	/// <summary>B_L_DP_Z 軸最大速度無效</summary>
	/// <remarks></remarks>
		//B_L_DP_Z Axis Invalid VelHigh
	public const  int Error_1062013 = 1062013;
	/// <summary>B_L_DP_Z 軸初速度無效</summary>
	/// <remarks></remarks>
		//B_L_DP_Z Axis Invalid VelLow
	public const  int Error_1062014 = 1062014;
	/// <summary>B_L_DP_Z 軸觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//B_L_DP_Z Axis Cmp Table Error.
	public const  int Error_1062015 = 1062015;
	/// <summary>B_L_DP_Z 軸復歸命令異常!</summary>
	/// <remarks></remarks>
		//B_L_DP_Z Axis Command Home Error!
	public const  int Error_1062016 = 1062016;
	/// <summary>B_L_DP_Z軸設定速度失敗!</summary>
	/// <remarks></remarks>
		//B_L_DP_Z Axis SetSpeed Error!
	public const  int Error_1062017 = 1062017;
	/// <summary>B_L_DP_Z軸命令執行失敗!</summary>
	/// <remarks></remarks>
		//B_L_DP_Z Axis Command Error!
	public const  int Error_1062018 = 1062018;
	/// <summary>B_L_DP_Z軸命令執行逾時!</summary>
	/// <remarks></remarks>
		//B_L_DP_Z Axis Command is TimeOut!
	public const  int Error_1062019 = 1062019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1062020 = 1062020;
	/// <summary>B_L_DP_Tilt移動失敗</summary>
	/// <remarks></remarks>
		//B_L_DP_Tilt Move Error!
	public const  int Error_1064000 = 1064000;
	/// <summary>B_L_DP_Tilt復歸逾時</summary>
	/// <remarks></remarks>
		//B_L_DP_Tilt wait Home Timeout!
	public const  int Error_1064001 = 1064001;
	/// <summary>B_L_DP_Tilt馬達Alarm</summary>
	/// <remarks></remarks>
		//B_L_DP_Tilt is Alarm!
	public const  int Error_1064002 = 1064002;
	/// <summary>B_L_DP_Tilt馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//B_L_DP_Tilt Get Motor Status Failed!
	public const  int Error_1064003 = 1064003;
	/// <summary>B_L_DP_Tilt等待到位逾時</summary>
	/// <remarks></remarks>
		//B_L_DP_Tilt wait INP Timeout!
	public const  int Error_1064004 = 1064004;
	/// <summary>B_L_DP_Tilt移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//B_L_DP_Tilt Command is Out of SPEL
	public const  int Error_1064005 = 1064005;
	/// <summary>B_L_DP_Tilt移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//B_L_DP_Tilt Command is Out of SNEL
	public const  int Error_1064006 = 1064006;
	/// <summary>B_L_DP_Tilt接觸硬體正極限</summary>
	/// <remarks></remarks>
		//B_L_DP_Tilt Touch HPEL
	public const  int Error_1064007 = 1064007;
	/// <summary>B_L_DP_Tilt接觸硬體負極限</summary>
	/// <remarks></remarks>
		//B_L_DP_Tilt Touch HNEL
	public const  int Error_1064008 = 1064008;
	/// <summary>B_L_DP_Tilt錯誤停止</summary>
	/// <remarks></remarks>
		//B_L_DP_Tilt Error Stop
	public const  int Error_1064009 = 1064009;
	/// <summary>B_L_DP_Tilt參數無效</summary>
	/// <remarks></remarks>
		//B_L_DP_Tilt Invalid Parameter
	public const  int Error_1064010 = 1064010;
	/// <summary>B_L_DP_Tilt加速度無效</summary>
	/// <remarks></remarks>
		//B_L_DP_Tilt Invalid Acc
	public const  int Error_1064011 = 1064011;
	/// <summary>B_L_DP_Tilt減速度無效</summary>
	/// <remarks></remarks>
		//B_L_DP_Tilt Invalid Dec
	public const  int Error_1064012 = 1064012;
	/// <summary>B_L_DP_Tilt最大速度無效</summary>
	/// <remarks></remarks>
		//B_L_DP_Tilt Invalid VelHigh
	public const  int Error_1064013 = 1064013;
	/// <summary>B_L_DP_Tilt初速度無效</summary>
	/// <remarks></remarks>
		//B_L_DP_Tilt Invalid VelLow
	public const  int Error_1064014 = 1064014;
	/// <summary>B_L_DP_Tilt觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//B_L_DP_Tilt Cmp Table Error.
	public const  int Error_1064015 = 1064015;
	/// <summary>B_L_DP_Tilt復歸命令異常!</summary>
	/// <remarks></remarks>
		//B_L_DP_Tilt Command Home Error!
	public const  int Error_1064016 = 1064016;
	/// <summary>B_L_DP_Tilt軸設定速度失敗!</summary>
	/// <remarks></remarks>
		//B_L_DP_Tilt Axis SetSpeed Error!
	public const  int Error_1064017 = 1064017;
	/// <summary>B_L_DP_Tilt命令執行失敗!</summary>
	/// <remarks></remarks>
		//B_L_DP_Tilt Command Error!
	public const  int Error_1064018 = 1064018;
	/// <summary>B_L_DP_Tilt命令執行逾時!</summary>
	/// <remarks></remarks>
		//B_L_DP_Tilt Command is TimeOut!
	public const  int Error_1064019 = 1064019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1064020 = 1064020;
	/// <summary>運動群組3移動失敗</summary>
	/// <remarks></remarks>
		//Group3 Move Error!
	public const  int Error_1066000 = 1066000;
	/// <summary>運動群組3復歸逾時</summary>
	/// <remarks></remarks>
		//Group3 wait Home Timeout!
	public const  int Error_1066001 = 1066001;
	/// <summary>運動群組3馬達Alarm</summary>
	/// <remarks></remarks>
		//Group3 is Alarm!
	public const  int Error_1066002 = 1066002;
	/// <summary>運動群組3馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//Group3 Get Motor Status Failed!
	public const  int Error_1066003 = 1066003;
	/// <summary>運動群組3等待到位逾時</summary>
	/// <remarks></remarks>
		//Group3 wait INP Timeout!
	public const  int Error_1066004 = 1066004;
	/// <summary>運動群組3移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//Group3 Command is Out of SPEL
	public const  int Error_1066005 = 1066005;
	/// <summary>運動群組3移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//Group3 Command is Out of SNEL
	public const  int Error_1066006 = 1066006;
	/// <summary>運動群組3接觸硬體正極限</summary>
	/// <remarks></remarks>
		//Group3 Touch HPEL
	public const  int Error_1066007 = 1066007;
	/// <summary>運動群組3接觸硬體負極限</summary>
	/// <remarks></remarks>
		//Group3 Touch HNEL
	public const  int Error_1066008 = 1066008;
	/// <summary>運動群組3錯誤停止</summary>
	/// <remarks></remarks>
		//Group3 Error Stop
	public const  int Error_1066009 = 1066009;
	/// <summary>運動群組3參數無效</summary>
	/// <remarks></remarks>
		//Group3 Invalid Parameter
	public const  int Error_1066010 = 1066010;
	/// <summary>運動群組3加速度無效</summary>
	/// <remarks></remarks>
		//Group3 Invalid Acc
	public const  int Error_1066011 = 1066011;
	/// <summary>運動群組3減速度無效</summary>
	/// <remarks></remarks>
		//Group3 Invalid Dec
	public const  int Error_1066012 = 1066012;
	/// <summary>運動群組3最大速度無效</summary>
	/// <remarks></remarks>
		//Group3 Invalid VelHigh
	public const  int Error_1066013 = 1066013;
	/// <summary>運動群組3初速度無效</summary>
	/// <remarks></remarks>
		//Group3 Invalid VelLow
	public const  int Error_1066014 = 1066014;
	/// <summary>運動群組3觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//Group3 Cmp Table Error.
	public const  int Error_1066015 = 1066015;
	/// <summary>運動群組3復歸命令異常!</summary>
	/// <remarks></remarks>
		//Group3 Command Home Error!
	public const  int Error_1066016 = 1066016;
	/// <summary>運動群組3命令執行失敗!</summary>
	/// <remarks></remarks>
		//Group3 Command Error!
	public const  int Error_1066017 = 1066017;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1066018 = 1066018;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1066019 = 1066019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1066020 = 1066020;
	/// <summary>B_R_DP_X軸移動失敗</summary>
	/// <remarks></remarks>
		//B_R_DP_X Axis Move Error!
	public const  int Error_1067000 = 1067000;
	/// <summary>B_R_DP_X 軸復歸逾時</summary>
	/// <remarks></remarks>
		//B_R_DP_X Axis wait Home Timeout!
	public const  int Error_1067001 = 1067001;
	/// <summary>B_R_DP_X 軸馬達Alarm</summary>
	/// <remarks></remarks>
		//B_R_DP_X Axis is Alarm!
	public const  int Error_1067002 = 1067002;
	/// <summary>B_R_DP_X 軸馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//B_R_DP_X Axis Get Motor Status Failed!
	public const  int Error_1067003 = 1067003;
	/// <summary>B_R_DP_X 軸等待到位逾時</summary>
	/// <remarks></remarks>
		//B_R_DP_X Axis wait INP Timeout!
	public const  int Error_1067004 = 1067004;
	/// <summary>B_R_DP_X 軸移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//B_R_DP_X Axis Command is Out of SPEL
	public const  int Error_1067005 = 1067005;
	/// <summary>B_R_DP_X 軸移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//B_R_DP_X Axis Command is Out of SNEL
	public const  int Error_1067006 = 1067006;
	/// <summary>B_R_DP_X 軸接觸硬體正極限</summary>
	/// <remarks></remarks>
		//B_R_DP_X Axis Touch HPEL
	public const  int Error_1067007 = 1067007;
	/// <summary>B_R_DP_X 軸接觸硬體負極限</summary>
	/// <remarks></remarks>
		//B_R_DP_X Axis Touch HNEL
	public const  int Error_1067008 = 1067008;
	/// <summary>B_R_DP_X 軸錯誤停止</summary>
	/// <remarks></remarks>
		//B_R_DP_X Axis Error Stop
	public const  int Error_1067009 = 1067009;
	/// <summary>B_R_DP_X 軸參數無效</summary>
	/// <remarks></remarks>
		//B_R_DP_X Axis Invalid Parameter
	public const  int Error_1067010 = 1067010;
	/// <summary>B_R_DP_X 軸加速度無效</summary>
	/// <remarks></remarks>
		//B_R_DP_X Axis Invalid Acc
	public const  int Error_1067011 = 1067011;
	/// <summary>B_R_DP_X 軸減速度無效</summary>
	/// <remarks></remarks>
		//B_R_DP_X Axis Invalid Dec
	public const  int Error_1067012 = 1067012;
	/// <summary>B_R_DP_X 軸最大速度無效</summary>
	/// <remarks></remarks>
		//B_R_DP_X Axis Invalid VelHigh
	public const  int Error_1067013 = 1067013;
	/// <summary>B_R_DP_X 軸初速度無效</summary>
	/// <remarks></remarks>
		//B_R_DP_X Axis Invalid VelLow
	public const  int Error_1067014 = 1067014;
	/// <summary>B_R_DP_X 軸觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//B_R_DP_X Axis Cmp Table Error.
	public const  int Error_1067015 = 1067015;
	/// <summary>B_R_DP_X 軸復歸命令異常!</summary>
	/// <remarks></remarks>
		//B_R_DP_X Axis Command Home Error!
	public const  int Error_1067016 = 1067016;
	/// <summary>B_R_DP_X軸設定速度失敗!</summary>
	/// <remarks></remarks>
		//B_R_DP_X Axis SetSpeed Error!
	public const  int Error_1067017 = 1067017;
	/// <summary>B_R_DP_X軸命令執行失敗!</summary>
	/// <remarks></remarks>
		//B_R_DP_X Axis Command Error!
	public const  int Error_1067018 = 1067018;
	/// <summary>B_R_DP_X軸命令執行逾時!</summary>
	/// <remarks></remarks>
		//B_R_DP_X Axis Command is TimeOut!
	public const  int Error_1067019 = 1067019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1067020 = 1067020;
	/// <summary>B_R_DP_Y軸移動失敗</summary>
	/// <remarks></remarks>
		//B_R_DP_Y Axis Move Error!
	public const  int Error_1068000 = 1068000;
	/// <summary>B_R_DP_Y軸復歸逾時</summary>
	/// <remarks></remarks>
		//B_R_DP_Y Axis wait Home Timeout!
	public const  int Error_1068001 = 1068001;
	/// <summary>B_R_DP_Y軸馬達Alarm</summary>
	/// <remarks></remarks>
		//B_R_DP_Y Axis is Alarm!
	public const  int Error_1068002 = 1068002;
	/// <summary>B_R_DP_Y軸馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//B_R_DP_Y Axis Get Motor Status Failed!
	public const  int Error_1068003 = 1068003;
	/// <summary>B_R_DP_Y軸等待到位逾時</summary>
	/// <remarks></remarks>
		//B_R_DP_Y Axis wait INP Timeout!
	public const  int Error_1068004 = 1068004;
	/// <summary>B_R_DP_Y軸移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//B_R_DP_Y Axis Command is Out of SPEL
	public const  int Error_1068005 = 1068005;
	/// <summary>B_R_DP_Y軸移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//B_R_DP_Y Axis Command is Out of SNEL
	public const  int Error_1068006 = 1068006;
	/// <summary>B_R_DP_Y軸接觸硬體正極限</summary>
	/// <remarks></remarks>
		//B_R_DP_Y Axis Touch HPEL
	public const  int Error_1068007 = 1068007;
	/// <summary>B_R_DP_Y軸接觸硬體負極限</summary>
	/// <remarks></remarks>
		//B_R_DP_Y Axis Touch HNEL
	public const  int Error_1068008 = 1068008;
	/// <summary>B_R_DP_Y軸錯誤停止</summary>
	/// <remarks></remarks>
		//B_R_DP_Y Axis Error Stop
	public const  int Error_1068009 = 1068009;
	/// <summary>B_R_DP_Y軸參數無效</summary>
	/// <remarks></remarks>
		//B_R_DP_Y Axis Invalid Parameter
	public const  int Error_1068010 = 1068010;
	/// <summary>B_R_DP_Y軸加速度無效</summary>
	/// <remarks></remarks>
		//B_R_DP_Y Axis Invalid Acc
	public const  int Error_1068011 = 1068011;
	/// <summary>B_R_DP_Y軸減速度無效</summary>
	/// <remarks></remarks>
		//B_R_DP_Y Axis Invalid Dec
	public const  int Error_1068012 = 1068012;
	/// <summary>B_R_DP_Y軸最大速度無效</summary>
	/// <remarks></remarks>
		//B_R_DP_Y Axis Invalid VelHigh
	public const  int Error_1068013 = 1068013;
	/// <summary>B_R_DP_Y軸初速度無效</summary>
	/// <remarks></remarks>
		//B_R_DP_Y Axis Invalid VelLow
	public const  int Error_1068014 = 1068014;
	/// <summary>B_R_DP_Y軸觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//B_R_DP_Y Axis Cmp Table Error.
	public const  int Error_1068015 = 1068015;
	/// <summary>B_R_DP_Y軸復歸命令異常!</summary>
	/// <remarks></remarks>
		//B_R_DP_Y Axis Command Home Error!
	public const  int Error_1068016 = 1068016;
	/// <summary>B_R_DP_Y軸設定速度失敗!</summary>
	/// <remarks></remarks>
		//B_R_DP_Y Axis SetSpeed Error!
	public const  int Error_1068017 = 1068017;
	/// <summary>B_R_DP_Y軸命令執行失敗!</summary>
	/// <remarks></remarks>
		//B_R_DP_Y Axis Command Error!
	public const  int Error_1068018 = 1068018;
	/// <summary>B_R_DP_Y軸命令執行逾時!</summary>
	/// <remarks></remarks>
		//B_R_DP_Y Axis Command is TimeOut!
	public const  int Error_1068019 = 1068019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1068020 = 1068020;
	/// <summary>B_R_DP_Z 軸移動失敗</summary>
	/// <remarks></remarks>
		//B_R_DP_Z Axis Move Error!
	public const  int Error_1069000 = 1069000;
	/// <summary>B_R_DP_Z 軸復歸逾時</summary>
	/// <remarks></remarks>
		//B_R_DP_Z Axis wait Home Timeout!
	public const  int Error_1069001 = 1069001;
	/// <summary>B_R_DP_Z 軸馬達Alarm</summary>
	/// <remarks></remarks>
		//B_R_DP_Z Axis is Alarm!
	public const  int Error_1069002 = 1069002;
	/// <summary>B_R_DP_Z 軸馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//B_R_DP_Z Axis Get Motor Status Failed!
	public const  int Error_1069003 = 1069003;
	/// <summary>B_R_DP_Z 軸等待到位逾時</summary>
	/// <remarks></remarks>
		//B_R_DP_Z Axis wait INP Timeout!
	public const  int Error_1069004 = 1069004;
	/// <summary>B_R_DP_Z 軸移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//B_R_DP_Z Axis Command is Out of SPEL
	public const  int Error_1069005 = 1069005;
	/// <summary>B_R_DP_Z 軸移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//B_R_DP_Z Axis Command is Out of SNEL
	public const  int Error_1069006 = 1069006;
	/// <summary>B_R_DP_Z 軸接觸硬體正極限</summary>
	/// <remarks></remarks>
		//B_R_DP_Z Axis Touch HPEL
	public const  int Error_1069007 = 1069007;
	/// <summary>B_R_DP_Z 軸接觸硬體負極限</summary>
	/// <remarks></remarks>
		//B_R_DP_Z Axis Touch HNEL
	public const  int Error_1069008 = 1069008;
	/// <summary>B_R_DP_Z 軸錯誤停止</summary>
	/// <remarks></remarks>
		//B_R_DP_Z Axis Error Stop
	public const  int Error_1069009 = 1069009;
	/// <summary>B_R_DP_Z 軸參數無效</summary>
	/// <remarks></remarks>
		//B_R_DP_Z Axis Invalid Parameter
	public const  int Error_1069010 = 1069010;
	/// <summary>B_R_DP_Z 軸加速度無效</summary>
	/// <remarks></remarks>
		//B_R_DP_Z Axis Invalid Acc
	public const  int Error_1069011 = 1069011;
	/// <summary>B_R_DP_Z 軸減速度無效</summary>
	/// <remarks></remarks>
		//B_R_DP_Z Axis Invalid Dec
	public const  int Error_1069012 = 1069012;
	/// <summary>B_R_DP_Z 軸最大速度無效</summary>
	/// <remarks></remarks>
		//B_R_DP_Z Axis Invalid VelHigh
	public const  int Error_1069013 = 1069013;
	/// <summary>B_R_DP_Z 軸初速度無效</summary>
	/// <remarks></remarks>
		//B_R_DP_Z Axis Invalid VelLow
	public const  int Error_1069014 = 1069014;
	/// <summary>B_R_DP_Z 軸觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//B_R_DP_Z Axis Cmp Table Error.
	public const  int Error_1069015 = 1069015;
	/// <summary>B_R_DP_Z 軸復歸命令異常!</summary>
	/// <remarks></remarks>
		//B_R_DP_Z Axis Command Home Error!
	public const  int Error_1069016 = 1069016;
	/// <summary>B_R_DP_Z軸設定速度失敗!</summary>
	/// <remarks></remarks>
		//B_R_DP_Z Axis SetSpeed Error!
	public const  int Error_1069017 = 1069017;
	/// <summary>B_R_DP_Z軸命令執行失敗!</summary>
	/// <remarks></remarks>
		//B_R_DP_Z Axis Command Error!
	public const  int Error_1069018 = 1069018;
	/// <summary>B_R_DP_Z軸命令執行逾時!</summary>
	/// <remarks></remarks>
		//B_R_DP_Z Axis Command is TimeOut!
	public const  int Error_1069019 = 1069019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1069020 = 1069020;
	/// <summary>B_R_DP_Tilt移動失敗</summary>
	/// <remarks></remarks>
		//B_R_DP_Tilt  Move Error!
	public const  int Error_1071000 = 1071000;
	/// <summary>B_R_DP_Tilt復歸逾時</summary>
	/// <remarks></remarks>
		//B_R_DP_Tilt wait Home Timeout!
	public const  int Error_1071001 = 1071001;
	/// <summary>B_R_DP_Tilt馬達Alarm</summary>
	/// <remarks></remarks>
		//B_R_DP_Tilt is Alarm!
	public const  int Error_1071002 = 1071002;
	/// <summary>B_R_DP_Tilt馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//B_R_DP_Tilt Get Motor Status Failed!
	public const  int Error_1071003 = 1071003;
	/// <summary>B_R_DP_Tilt等待到位逾時</summary>
	/// <remarks></remarks>
		//B_R_DP_Tilt wait INP Timeout!
	public const  int Error_1071004 = 1071004;
	/// <summary>B_R_DP_Tilt移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//B_R_DP_Tilt Command is Out of SPEL
	public const  int Error_1071005 = 1071005;
	/// <summary>B_R_DP_Tilt移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//B_R_DP_Tilt Command is Out of SNEL
	public const  int Error_1071006 = 1071006;
	/// <summary>B_R_DP_Tilt接觸硬體正極限</summary>
	/// <remarks></remarks>
		//B_R_DP_Tilt Touch HPEL
	public const  int Error_1071007 = 1071007;
	/// <summary>B_R_DP_Tilt接觸硬體負極限</summary>
	/// <remarks></remarks>
		//B_R_DP_Tilt Touch HNEL
	public const  int Error_1071008 = 1071008;
	/// <summary>B_R_DP_Tilt錯誤停止</summary>
	/// <remarks></remarks>
		//B_R_DP_Tilt Error Stop
	public const  int Error_1071009 = 1071009;
	/// <summary>B_R_DP_Tilt參數無效</summary>
	/// <remarks></remarks>
		//B_R_DP_Tilt Invalid Parameter
	public const  int Error_1071010 = 1071010;
	/// <summary>B_R_DP_Tilt加速度無效</summary>
	/// <remarks></remarks>
		//B_R_DP_Tilt Invalid Acc
	public const  int Error_1071011 = 1071011;
	/// <summary>B_R_DP_Tilt減速度無效</summary>
	/// <remarks></remarks>
		//B_R_DP_Tilt Invalid Dec
	public const  int Error_1071012 = 1071012;
	/// <summary>B_R_DP_Tilt最大速度無效</summary>
	/// <remarks></remarks>
		//B_R_DP_Tilt Invalid VelHigh
	public const  int Error_1071013 = 1071013;
	/// <summary>B_R_DP_Tilt初速度無效</summary>
	/// <remarks></remarks>
		//B_R_DP_Tilt Invalid VelLow
	public const  int Error_1071014 = 1071014;
	/// <summary>B_R_DP_Tilt觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//B_R_DP_Tilt Cmp Table Error.
	public const  int Error_1071015 = 1071015;
	/// <summary>B_R_DP_Tilt復歸命令異常!</summary>
	/// <remarks></remarks>
		//B_R_DP_Tilt Command Home Error!
	public const  int Error_1071016 = 1071016;
	/// <summary>B_R_DP_Tilt軸設定速度失敗!</summary>
	/// <remarks></remarks>
		//B_R_DP_Tilt Axis SetSpeed Error!
	public const  int Error_1071017 = 1071017;
	/// <summary>B_R_DP_Tilt命令執行失敗!</summary>
	/// <remarks></remarks>
		//B_R_DP_Tilt  Command Error!
	public const  int Error_1071018 = 1071018;
	/// <summary>B_R_DP_Tilt命令執行逾時!</summary>
	/// <remarks></remarks>
		//B_R_DP_Tilt Command is TimeOut!
	public const  int Error_1071019 = 1071019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1071020 = 1071020;
	/// <summary>運動群組4移動失敗</summary>
	/// <remarks></remarks>
		//Group4 Move Error!
	public const  int Error_1073000 = 1073000;
	/// <summary>運動群組4復歸逾時</summary>
	/// <remarks></remarks>
		//Group4 wait Home Timeout!
	public const  int Error_1073001 = 1073001;
	/// <summary>運動群組4馬達Alarm</summary>
	/// <remarks></remarks>
		//Group4 is Alarm!
	public const  int Error_1073002 = 1073002;
	/// <summary>運動群組4馬達狀態取得失敗</summary>
	/// <remarks></remarks>
		//Group4 Get Motor Status Failed!
	public const  int Error_1073003 = 1073003;
	/// <summary>運動群組4等待到位逾時</summary>
	/// <remarks></remarks>
		//Group4 wait INP Timeout!
	public const  int Error_1073004 = 1073004;
	/// <summary>運動群組4移動命令超出軟體正極限</summary>
	/// <remarks></remarks>
		//Group4 Command is Out of SPEL
	public const  int Error_1073005 = 1073005;
	/// <summary>運動群組4移動命令超出軟體負極限</summary>
	/// <remarks></remarks>
		//Group4 Command is Out of SNEL
	public const  int Error_1073006 = 1073006;
	/// <summary>運動群組4接觸硬體正極限</summary>
	/// <remarks></remarks>
		//Group4 Touch HPEL
	public const  int Error_1073007 = 1073007;
	/// <summary>運動群組4接觸硬體負極限</summary>
	/// <remarks></remarks>
		//Group4 Touch HNEL
	public const  int Error_1073008 = 1073008;
	/// <summary>運動群組4錯誤停止</summary>
	/// <remarks></remarks>
		//Group4 Error Stop
	public const  int Error_1073009 = 1073009;
	/// <summary>運動群組4參數無效</summary>
	/// <remarks></remarks>
		//Group4 Invalid Parameter
	public const  int Error_1073010 = 1073010;
	/// <summary>運動群組4加速度無效</summary>
	/// <remarks></remarks>
		//Group4 Invalid Acc
	public const  int Error_1073011 = 1073011;
	/// <summary>運動群組4減速度無效</summary>
	/// <remarks></remarks>
		//Group4 Invalid Dec
	public const  int Error_1073012 = 1073012;
	/// <summary>運動群組4最大速度無效</summary>
	/// <remarks></remarks>
		//Group4 Invalid VelHigh
	public const  int Error_1073013 = 1073013;
	/// <summary>運動群組4初速度無效</summary>
	/// <remarks></remarks>
		//Group4 Invalid VelLow
	public const  int Error_1073014 = 1073014;
	/// <summary>運動群組4觸發比較表錯誤.</summary>
	/// <remarks></remarks>
		//Group4 Cmp Table Error.
	public const  int Error_1073015 = 1073015;
	/// <summary>運動群組4復歸命令異常!</summary>
	/// <remarks></remarks>
		//Group4 Command Home Error!
	public const  int Error_1073016 = 1073016;
	/// <summary>運動群組4命令執行失敗!</summary>
	/// <remarks></remarks>
		//Group4 Command Error!
	public const  int Error_1073017 = 1073017;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1073018 = 1073018;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1073019 = 1073019;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Error_1073020 = 1073020;
	/// <summary>"廠務氣壓不足,無法復歸!"</summary>
	/// <remarks></remarks>
		//CDA Alarm. Can't Home.
	public const  int Alarm_2000000 = 2000000;
	/// <summary>"緊急停止中, 無法復歸!"</summary>
	/// <remarks></remarks>
		//"EMO Alarm, Can't Home."
	public const  int Alarm_2000001 = 2000001;
	/// <summary>"PLC異常,無法復歸!"</summary>
	/// <remarks></remarks>
		//"PLC Alarm, Can't Home."
	public const  int Alarm_2000002 = 2000002;
	/// <summary>"互鎖保護,無法復歸!"</summary>
	/// <remarks></remarks>
		//"Interlock Alarm, Can't Home."
	public const  int Alarm_2000003 = 2000003;
	/// <summary>CCD 場景毀損!</summary>
	/// <remarks></remarks>
		//"CCD Sence destroyed"
	public const  int Alarm_2000004 = 2000004;
	/// <summary>"伺服系統未激磁,無法復歸!"</summary>
	/// <remarks></remarks>
		//"Servo Off, Can't Home."
	public const  int Alarm_2000005 = 2000005;
	/// <summary>取像工具不存在! 請先設定!</summary>
	/// <remarks></remarks>
		//Acquisition Tool Does Not Exist.
	public const  int Alarm_2000006 = 2000006;
	/// <summary>定位工具不存在! 請先設定!</summary>
	/// <remarks></remarks>
		//Alignment Tool Does Not Exist.
	public const  int Alarm_2000007 = 2000007;
	/// <summary>檢測工具不存在! 請先設定!</summary>
	/// <remarks></remarks>
		//Inspection Tool Does Not Exist.
	public const  int Alarm_2000008 = 2000008;
	/// <summary>請先進行影像教導!</summary>
	/// <remarks></remarks>
		//"Train Image, First."
	public const  int Alarm_2000009 = 2000009;
	/// <summary>場景切換失敗!</summary>
	/// <remarks></remarks>
		//Select Scene Failed!
	public const  int Alarm_2000010 = 2000010;
	/// <summary>第一輪第一基準點定位失敗!</summary>
	/// <remarks></remarks>
		//Round1 Alignment Point1 Failed!
	public const  int Alarm_2000011 = 2000011;
	/// <summary>第一輪第二基準點定位失敗!</summary>
	/// <remarks></remarks>
		//Round1 Alignment Point2 Failed!
	public const  int Alarm_2000012 = 2000012;
	/// <summary>第一輪第三基準點定位失敗!</summary>
	/// <remarks></remarks>
		//Round1 Alignment Point3 Failed!
	public const  int Alarm_2000013 = 2000013;
	/// <summary>第一輪第四基準點定位失敗!</summary>
	/// <remarks></remarks>
		//Round1 Alignment Point4 Failed!
	public const  int Alarm_2000014 = 2000014;
	/// <summary>第二輪第一基準點定位失敗!</summary>
	/// <remarks></remarks>
		//Round2 Alignment Point1 Failed!
	public const  int Alarm_2000015 = 2000015;
	/// <summary>第二輪第二基準點定位失敗!</summary>
	/// <remarks></remarks>
		//Round2 Alignment Point2 Failed!
	public const  int Alarm_2000016 = 2000016;
	/// <summary>第二輪第三基準點定位失敗!</summary>
	/// <remarks></remarks>
		//Round2 Alignment Point3 Failed!
	public const  int Alarm_2000017 = 2000017;
	/// <summary>場景 {0} 影像教導失敗!</summary>
	/// <remarks></remarks>
		//Scene {0} Image Train Failed!
	public const  int Alarm_2000018 = 2000018;
	/// <summary>場景 {0} 特徵識別失敗!</summary>
	/// <remarks></remarks>
		//Scene {0} Pattern Recognition Failed!
	public const  int Alarm_2000019 = 2000019;
	/// <summary>顯示物件不存在!</summary>
	/// <remarks></remarks>
		//CogDisplay is Dispose!
	public const  int Alarm_2000020 = 2000020;
	/// <summary>秤重資料庫檔案不存在</summary>
	/// <remarks></remarks>
		//FlowRateDB Name does not exist!
	public const  int Alarm_2000021 = 2000021;
	/// <summary>定位資料不存在</summary>
	/// <remarks></remarks>
		//Alignment Data Not Exists!
	public const  int Alarm_2000022 = 2000022;
	/// <summary>工具不存在</summary>
	/// <remarks></remarks>
		//Tool Subject Not Exists.
	public const  int Alarm_2000023 = 2000023;
	/// <summary>工具輸入不存在</summary>
	/// <remarks></remarks>
		//Tool Subject Input Not Exists.
	public const  int Alarm_2000024 = 2000024;
	/// <summary>工具輸入影像不存在</summary>
	/// <remarks></remarks>
		//Tool Subject Input 'InputImage' Not Exists.
	public const  int Alarm_2000025 = 2000025;
	/// <summary>CCD讀取影像格式錯誤</summary>
	/// <remarks></remarks>
		//CCD Set VideoFormatType Error!
	public const  int Alarm_2000026 = 2000026;
	/// <summary>CCD序號不匹配</summary>
	/// <remarks></remarks>
		//CCD SerialNumber Not Matched!
	public const  int Alarm_2000027 = 2000027;
	/// <summary>軸向異常</summary>
	/// <remarks></remarks>
		//Axis Alarm.
	public const  int Alarm_2000028 = 2000028;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2000029 = 2000029;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2000030 = 2000030;
	/// <summary>汽缸上逾時!</summary>
	/// <remarks></remarks>
		//Cylinder Up Time Out!
	public const  int Alarm_2004000 = 2004000;
	/// <summary>汽缸下逾時!</summary>
	/// <remarks></remarks>
		//Cylinder Down Time Out!
	public const  int Alarm_2004001 = 2004001;
	/// <summary>夾爪閉逾時!</summary>
	/// <remarks></remarks>
		//Clamp On Time Out!
	public const  int Alarm_2004002 = 2004002;
	/// <summary>夾爪開逾時!</summary>
	/// <remarks></remarks>
		//Clamp Off Time Out!
	public const  int Alarm_2004003 = 2004003;
	/// <summary>CCD1取像工具不存在!</summary>
	/// <remarks></remarks>
		//CCD1 Acquisition Tool Not Exists.
	public const  int Alarm_2012000 = 2012000;
	/// <summary>CCD1取像輸出數量為0!</summary>
	/// <remarks></remarks>
		//CCD1 Acquisition Output List Count is 0.
	public const  int Alarm_2012001 = 2012001;
	/// <summary>CCD1取像輸出不存在!</summary>
	/// <remarks></remarks>
		//CCD1 Acquisition Output Not Exists.
	public const  int Alarm_2012002 = 2012002;
	/// <summary>CCD1 取像TimeOut!</summary>
	/// <remarks></remarks>
		//CCD1 Acquisition TimeOut!
	public const  int Alarm_2012003 = 2012003;
	/// <summary>CCD1 影像運算 TimeOut!</summary>
	/// <remarks></remarks>
		//CCD1 Calculation TimeOut!
	public const  int Alarm_2012004 = 2012004;
	/// <summary>CCD1 通訊異常!</summary>
	/// <remarks></remarks>
		//CCD1 Communication Error!
	public const  int Alarm_2012005 = 2012005;
	/// <summary>CCD1定位工具異常!</summary>
	/// <remarks></remarks>
		//CCD1 Alignment Tool Failed!
	public const  int Alarm_2012100 = 2012100;
	/// <summary>CCD1定位工具未教導!</summary>
	/// <remarks></remarks>
		//CCD1 Alignment Tool Untrained!
	public const  int Alarm_2012101 = 2012101;
	/// <summary>CCD1定位有多個符合特徵!</summary>
	/// <remarks></remarks>
		//CCD1 Alignment Multi-Match Pattern!
	public const  int Alarm_2012102 = 2012102;
	/// <summary>CCD1符合特徵未找到!</summary>
	/// <remarks></remarks>
		//CCD1 Match Pattern Not Found
	public const  int Alarm_2012103 = 2012103;
	/// <summary>CCD1輸入影像不存在!</summary>
	/// <remarks></remarks>
		//CCD1 Input Image Not Exists
	public const  int Alarm_2012104 = 2012104;
	/// <summary>CCD1定位結果顯示失敗!</summary>
	/// <remarks></remarks>
		//CCD1 Show Alignment Result Failed!
	public const  int Alarm_2012105 = 2012105;
	/// <summary>CCD1定位結果不存在!</summary>
	/// <remarks></remarks>
		//CCD1 Alignment Result Not Exists.
	public const  int Alarm_2012106 = 2012106;
	/// <summary>CCD1定位場景不存在!</summary>
	/// <remarks></remarks>
		//CCD1 Alignment Scene Not Exists.
	public const  int Alarm_2012107 = 2012107;
	/// <summary>CCD1定位工具不存在!</summary>
	/// <remarks></remarks>
		//CCD1 Alignment Tool Not Exists.
	public const  int Alarm_2012108 = 2012108;
	/// <summary>CCD1定位工具輸入物件不存在!</summary>
	/// <remarks></remarks>
		//CCD1 Alignment Input Not Exists.
	public const  int Alarm_2012109 = 2012109;
	/// <summary>CCD1定位工具輸入數量為0!</summary>
	/// <remarks></remarks>
		//CCD1 Alignment Input List Count is 0.
	public const  int Alarm_2012110 = 2012110;
	/// <summary>CCD1定位結果誤差過大!</summary>
	/// <remarks></remarks>
		//CCD1 Alignment Out of Range!
	public const  int Alarm_2012111 = 2012111;
	/// <summary>CCD1定位超過角度限制!</summary>
	/// <remarks></remarks>
		//CCD1 Alignment Angle Out of Range!
	public const  int Alarm_2012112 = 2012112;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2012113 = 2012113;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2012114 = 2012114;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2012115 = 2012115;
	/// <summary>CCD1檢測工具異常!</summary>
	/// <remarks></remarks>
		//CCD1 Inspection Tool Failed!
	public const  int Alarm_2012200 = 2012200;
	/// <summary>CCD1檢測工具未教導!</summary>
	/// <remarks></remarks>
		//CCD1 Inspection Tool Untrained!
	public const  int Alarm_2012201 = 2012201;
	/// <summary>CCD1檢測工具警報!</summary>
	/// <remarks></remarks>
		//CCD1 Inspection Alarm!
	public const  int Alarm_2012202 = 2012202;
	/// <summary>CCD1輸入影像不存在!</summary>
	/// <remarks></remarks>
		//CCD1 Input Image Not Exists
	public const  int Alarm_2012203 = 2012203;
	/// <summary>CCD1檢測結果顯示失敗!</summary>
	/// <remarks></remarks>
		//CCD1 Show Inspection Result Failed!
	public const  int Alarm_2012204 = 2012204;
	/// <summary>CCD1檢測結果不存在!</summary>
	/// <remarks></remarks>
		//CCD1 Inspection Result Not Exists.
	public const  int Alarm_2012205 = 2012205;
	/// <summary>CCD1檢測場景不存在!</summary>
	/// <remarks></remarks>
		//CCD1 Inspection Scene Not Exists.
	public const  int Alarm_2012206 = 2012206;
	/// <summary>CCD1檢測工具不存在!</summary>
	/// <remarks></remarks>
		//CCD1 Inspection Tool Not Exists.
	public const  int Alarm_2012207 = 2012207;
	/// <summary>CCD1檢測工具輸入物件不存在!</summary>
	/// <remarks></remarks>
		//CCD1 Inspection Input Not Exists.
	public const  int Alarm_2012208 = 2012208;
	/// <summary>CCD1檢測工具輸入數量為0!</summary>
	/// <remarks></remarks>
		//CCD1 Inspection Input List Count is 0.
	public const  int Alarm_2012209 = 2012209;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2012210 = 2012210;
	/// <summary>CCD2取像工具不存在!</summary>
	/// <remarks></remarks>
		//CCD2 Acquisition Tool Not Exists.
	public const  int Alarm_2012300 = 2012300;
	/// <summary>CCD2取像輸出數量為0!</summary>
	/// <remarks></remarks>
		//CCD2 Acquisition Output List Count is 0.
	public const  int Alarm_2012301 = 2012301;
	/// <summary>CCD2取像輸出不存在!</summary>
	/// <remarks></remarks>
		//CCD2 Acquisition Output Not Exists.
	public const  int Alarm_2012302 = 2012302;
	/// <summary>CCD2 取像TimeOut!</summary>
	/// <remarks></remarks>
		//CCD2 Acquisition TimeOut!
	public const  int Alarm_2012303 = 2012303;
	/// <summary>CCD2 影像運算 TimeOut!</summary>
	/// <remarks></remarks>
		//CCD2 Calculation TimeOut!
	public const  int Alarm_2012304 = 2012304;
	/// <summary>CCD2 通訊異常!</summary>
	/// <remarks></remarks>
		//CCD2 Communication Error!
	public const  int Alarm_2012305 = 2012305;
	/// <summary>CCD2定位工具異常!</summary>
	/// <remarks></remarks>
		//CCD2 Alignment Tool Failed!
	public const  int Alarm_2012400 = 2012400;
	/// <summary>CCD2定位工具未教導!</summary>
	/// <remarks></remarks>
		//CCD2 Alignment Tool Untrained!
	public const  int Alarm_2012401 = 2012401;
	/// <summary>CCD2定位有多個符合特徵!</summary>
	/// <remarks></remarks>
		//CCD2 Alignment Multi-Match Pattern!
	public const  int Alarm_2012402 = 2012402;
	/// <summary>CCD2符合特徵未找到!</summary>
	/// <remarks></remarks>
		//CCD2 Match Pattern Not Found
	public const  int Alarm_2012403 = 2012403;
	/// <summary>CCD2輸入影像不存在!</summary>
	/// <remarks></remarks>
		//CCD2 Input Image Not Exists
	public const  int Alarm_2012404 = 2012404;
	/// <summary>CCD2定位結果顯示失敗!</summary>
	/// <remarks></remarks>
		//CCD2 Show Alignment Result Failed!
	public const  int Alarm_2012405 = 2012405;
	/// <summary>CCD2定位結果不存在!</summary>
	/// <remarks></remarks>
		//CCD2 Alignment Result Not Exists.
	public const  int Alarm_2012406 = 2012406;
	/// <summary>CCD2定位場景不存在!</summary>
	/// <remarks></remarks>
		//CCD2 Alignment Scene Not Exists.
	public const  int Alarm_2012407 = 2012407;
	/// <summary>CCD2定位工具不存在!</summary>
	/// <remarks></remarks>
		//CCD2 Alignment Tool Not Exists.
	public const  int Alarm_2012408 = 2012408;
	/// <summary>CCD2定位工具輸入物件不存在!</summary>
	/// <remarks></remarks>
		//CCD2 Alignment Input Not Exists.
	public const  int Alarm_2012409 = 2012409;
	/// <summary>CCD2定位工具輸入數量為0!</summary>
	/// <remarks></remarks>
		//CCD2 Alignment Input List Count is 0.
	public const  int Alarm_2012410 = 2012410;
	/// <summary>CCD2定位結果誤差過大!</summary>
	/// <remarks></remarks>
		//CCD2 Alignment Out of Range!
	public const  int Alarm_2012411 = 2012411;
	/// <summary>CCD2定位超過角度限制!</summary>
	/// <remarks></remarks>
		//CCD2 Alignment Angle Out of Range!
	public const  int Alarm_2012412 = 2012412;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2012413 = 2012413;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2012414 = 2012414;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2012415 = 2012415;
	/// <summary>CCD2檢測工具異常!</summary>
	/// <remarks></remarks>
		//CCD2 Inspection Tool Failed!
	public const  int Alarm_2012500 = 2012500;
	/// <summary>CCD2檢測工具未教導!</summary>
	/// <remarks></remarks>
		//CCD2 Inspection Tool Untrained!
	public const  int Alarm_2012501 = 2012501;
	/// <summary>CCD2檢測工具警報!</summary>
	/// <remarks></remarks>
		//CCD2 Inspection Alarm!
	public const  int Alarm_2012502 = 2012502;
	/// <summary>CCD2輸入影像不存在!</summary>
	/// <remarks></remarks>
		//CCD2 Input Image Not Exists
	public const  int Alarm_2012503 = 2012503;
	/// <summary>CCD2檢測結果顯示失敗!</summary>
	/// <remarks></remarks>
		//CCD2 Show Inspection Result Failed!
	public const  int Alarm_2012504 = 2012504;
	/// <summary>CCD2檢測結果不存在!</summary>
	/// <remarks></remarks>
		//CCD2 Inspection Result Not Exists.
	public const  int Alarm_2012505 = 2012505;
	/// <summary>CCD2檢測場景不存在!</summary>
	/// <remarks></remarks>
		//CCD2 Inspection Scene Not Exists.
	public const  int Alarm_2012506 = 2012506;
	/// <summary>CCD2檢測工具不存在!</summary>
	/// <remarks></remarks>
		//CCD2 Inspection Tool Not Exists.
	public const  int Alarm_2012507 = 2012507;
	/// <summary>CCD2檢測工具輸入物件不存在!</summary>
	/// <remarks></remarks>
		//CCD2 Inspection Input Not Exists.
	public const  int Alarm_2012508 = 2012508;
	/// <summary>CCD2檢測工具輸入數量為0!</summary>
	/// <remarks></remarks>
		//CCD2 Inspection Input List Count is 0.
	public const  int Alarm_2012509 = 2012509;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2012510 = 2012510;
	/// <summary>CCD3取像工具不存在!</summary>
	/// <remarks></remarks>
		//CCD3 Acquisition Tool Not Exists.
	public const  int Alarm_2012600 = 2012600;
	/// <summary>CCD3取像輸出數量為0!</summary>
	/// <remarks></remarks>
		//CCD3 Acquisition Output List Count is 0.
	public const  int Alarm_2012601 = 2012601;
	/// <summary>CCD3取像輸出不存在!</summary>
	/// <remarks></remarks>
		//CCD3 Acquisition Output Not Exists.
	public const  int Alarm_2012602 = 2012602;
	/// <summary>CCD3 取像TimeOut!</summary>
	/// <remarks></remarks>
		//CCD3 Acquisition TimeOut!
	public const  int Alarm_2012603 = 2012603;
	/// <summary>CCD3 影像運算 TimeOut!</summary>
	/// <remarks></remarks>
		//CCD3 Calculation TimeOut!
	public const  int Alarm_2012604 = 2012604;
	/// <summary>CCD3 通訊異常!</summary>
	/// <remarks></remarks>
		//CCD3 Communication Error!
	public const  int Alarm_2012605 = 2012605;
	/// <summary>CCD3定位工具異常!</summary>
	/// <remarks></remarks>
		//CCD3 Alignment Tool Failed!
	public const  int Alarm_2012700 = 2012700;
	/// <summary>CCD3定位工具未教導!</summary>
	/// <remarks></remarks>
		//CCD3 Alignment Tool Untrained!
	public const  int Alarm_2012701 = 2012701;
	/// <summary>CCD3定位有多個符合特徵!</summary>
	/// <remarks></remarks>
		//CCD3 Alignment Multi-Match Pattern!
	public const  int Alarm_2012702 = 2012702;
	/// <summary>CCD3符合特徵未找到!</summary>
	/// <remarks></remarks>
		//CCD3 Match Pattern Not Found
	public const  int Alarm_2012703 = 2012703;
	/// <summary>CCD3輸入影像不存在!</summary>
	/// <remarks></remarks>
		//CCD3 Input Image Not Exists
	public const  int Alarm_2012704 = 2012704;
	/// <summary>CCD3定位結果顯示失敗!</summary>
	/// <remarks></remarks>
		//CCD3 Show Alignment Result Failed!
	public const  int Alarm_2012705 = 2012705;
	/// <summary>CCD3定位結果不存在!</summary>
	/// <remarks></remarks>
		//CCD3 Alignment Result Not Exists.
	public const  int Alarm_2012706 = 2012706;
	/// <summary>CCD3定位場景不存在!</summary>
	/// <remarks></remarks>
		//CCD3 Alignment Scene Not Exists.
	public const  int Alarm_2012707 = 2012707;
	/// <summary>CCD3定位工具不存在!</summary>
	/// <remarks></remarks>
		//CCD3 Alignment Tool Not Exists.
	public const  int Alarm_2012708 = 2012708;
	/// <summary>CCD3定位工具輸入物件不存在!</summary>
	/// <remarks></remarks>
		//CCD3 Alignment Input Not Exists.
	public const  int Alarm_2012709 = 2012709;
	/// <summary>CCD3定位工具輸入數量為0!</summary>
	/// <remarks></remarks>
		//CCD3 Alignment Input List Count is 0.
	public const  int Alarm_2012710 = 2012710;
	/// <summary>CCD3定位結果誤差過大!</summary>
	/// <remarks></remarks>
		//CCD3 Alignment Out of Range!
	public const  int Alarm_2012711 = 2012711;
	/// <summary>CCD3定位超過角度限制!</summary>
	/// <remarks></remarks>
		//CCD3 Alignment Angle Out of Range!
	public const  int Alarm_2012712 = 2012712;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2012713 = 2012713;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2012714 = 2012714;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2012715 = 2012715;
	/// <summary>CCD3檢測工具異常!</summary>
	/// <remarks></remarks>
		//CCD3 Inspection Tool Failed!
	public const  int Alarm_2012800 = 2012800;
	/// <summary>CCD3檢測工具未教導!</summary>
	/// <remarks></remarks>
		//CCD3 Inspection Tool Untrained!
	public const  int Alarm_2012801 = 2012801;
	/// <summary>CCD3檢測工具警報!</summary>
	/// <remarks></remarks>
		//CCD3 Inspection Alarm!
	public const  int Alarm_2012802 = 2012802;
	/// <summary>CCD3輸入影像不存在!</summary>
	/// <remarks></remarks>
		//CCD3 Input Image Not Exists
	public const  int Alarm_2012803 = 2012803;
	/// <summary>CCD3檢測結果顯示失敗!</summary>
	/// <remarks></remarks>
		//CCD3 Show Inspection Result Failed!
	public const  int Alarm_2012804 = 2012804;
	/// <summary>CCD3檢測結果不存在!</summary>
	/// <remarks></remarks>
		//CCD3 Inspection Result Not Exists.
	public const  int Alarm_2012805 = 2012805;
	/// <summary>CCD3檢測場景不存在!</summary>
	/// <remarks></remarks>
		//CCD3 Inspection Scene Not Exists.
	public const  int Alarm_2012806 = 2012806;
	/// <summary>CCD3檢測工具不存在!</summary>
	/// <remarks></remarks>
		//CCD3 Inspection Tool Not Exists.
	public const  int Alarm_2012807 = 2012807;
	/// <summary>CCD3檢測工具輸入物件不存在!</summary>
	/// <remarks></remarks>
		//CCD3 Inspection Input Not Exists.
	public const  int Alarm_2012808 = 2012808;
	/// <summary>CCD3檢測工具輸入數量為0!</summary>
	/// <remarks></remarks>
		//CCD3 Inspection Input List Count is 0.
	public const  int Alarm_2012809 = 2012809;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2012810 = 2012810;
	/// <summary>CCD4取像工具不存在!</summary>
	/// <remarks></remarks>
		//CCD4 Acquisition Tool Not Exists.
	public const  int Alarm_2012900 = 2012900;
	/// <summary>CCD4取像輸出數量為0!</summary>
	/// <remarks></remarks>
		//CCD4 Acquisition Output List Count is 0.
	public const  int Alarm_2012901 = 2012901;
	/// <summary>CCD4取像輸出不存在!</summary>
	/// <remarks></remarks>
		//CCD4 Acquisition Output Not Exists.
	public const  int Alarm_2012902 = 2012902;
	/// <summary>CCD4 取像TimeOut!</summary>
	/// <remarks></remarks>
		//CCD4 Acquisition TimeOut!
	public const  int Alarm_2012903 = 2012903;
	/// <summary>CCD4 影像運算 TimeOut!</summary>
	/// <remarks></remarks>
		//CCD4 Calculation TimeOut!
	public const  int Alarm_2012904 = 2012904;
	/// <summary>CCD4 通訊異常!</summary>
	/// <remarks></remarks>
		//CCD4 Communication Error!
	public const  int Alarm_2012905 = 2012905;
	/// <summary>CCD4定位工具異常!</summary>
	/// <remarks></remarks>
		//CCD4 Alignment Tool Failed!
	public const  int Alarm_2013000 = 2013000;
	/// <summary>CCD4定位工具未教導!</summary>
	/// <remarks></remarks>
		//CCD4 Alignment Tool Untrained!
	public const  int Alarm_2013001 = 2013001;
	/// <summary>CCD4定位有多個符合特徵!</summary>
	/// <remarks></remarks>
		//CCD4 Alignment Multi-Match Pattern!
	public const  int Alarm_2013002 = 2013002;
	/// <summary>CCD4符合特徵未找到!</summary>
	/// <remarks></remarks>
		//CCD4 Match Pattern Not Found
	public const  int Alarm_2013003 = 2013003;
	/// <summary>CCD4輸入影像不存在!</summary>
	/// <remarks></remarks>
		//CCD4 Input Image Not Exists
	public const  int Alarm_2013004 = 2013004;
	/// <summary>CCD4定位結果顯示失敗!</summary>
	/// <remarks></remarks>
		//CCD4 Show Alignment Result Failed!
	public const  int Alarm_2013005 = 2013005;
	/// <summary>CCD4定位結果不存在!</summary>
	/// <remarks></remarks>
		//CCD4 Alignment Result Not Exists.
	public const  int Alarm_2013006 = 2013006;
	/// <summary>CCD4定位場景不存在!</summary>
	/// <remarks></remarks>
		//CCD4 Alignment Scene Not Exists.
	public const  int Alarm_2013007 = 2013007;
	/// <summary>CCD4定位工具不存在!</summary>
	/// <remarks></remarks>
		//CCD4 Alignment Tool Not Exists.
	public const  int Alarm_2013008 = 2013008;
	/// <summary>CCD4定位工具輸入物件不存在!</summary>
	/// <remarks></remarks>
		//CCD4 Alignment Input Not Exists.
	public const  int Alarm_2013009 = 2013009;
	/// <summary>CCD4定位工具輸入數量為0!</summary>
	/// <remarks></remarks>
		//CCD4 Alignment Input List Count is 0.
	public const  int Alarm_2013010 = 2013010;
	/// <summary>CCD4定位結果誤差過大!</summary>
	/// <remarks></remarks>
		//CCD4 Alignment Out of Range!
	public const  int Alarm_2013011 = 2013011;
	/// <summary>CCD4定位超過角度限制!</summary>
	/// <remarks></remarks>
		//CCD4 Alignment Angle Out of Range!
	public const  int Alarm_2013012 = 2013012;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2013013 = 2013013;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2013014 = 2013014;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2013015 = 2013015;
	/// <summary>CCD4檢測工具異常!</summary>
	/// <remarks></remarks>
		//CCD4 Inspection Tool Failed!
	public const  int Alarm_2013100 = 2013100;
	/// <summary>CCD4檢測工具未教導!</summary>
	/// <remarks></remarks>
		//CCD4 Inspection Tool Untrained!
	public const  int Alarm_2013101 = 2013101;
	/// <summary>CCD4檢測工具警報!</summary>
	/// <remarks></remarks>
		//CCD4 Inspection Alarm!
	public const  int Alarm_2013102 = 2013102;
	/// <summary>CCD4輸入影像不存在!</summary>
	/// <remarks></remarks>
		//CCD4 Input Image Not Exists
	public const  int Alarm_2013103 = 2013103;
	/// <summary>CCD4檢測結果顯示失敗!</summary>
	/// <remarks></remarks>
		//CCD4 Show Inspection Result Failed!
	public const  int Alarm_2013104 = 2013104;
	/// <summary>CCD4檢測結果不存在!</summary>
	/// <remarks></remarks>
		//CCD4 Inspection Result Not Exists.
	public const  int Alarm_2013105 = 2013105;
	/// <summary>CCD4檢測場景不存在!</summary>
	/// <remarks></remarks>
		//CCD4 Inspection Scene Not Exists.
	public const  int Alarm_2013106 = 2013106;
	/// <summary>CCD4檢測工具不存在!</summary>
	/// <remarks></remarks>
		//CCD4 Inspection Tool Not Exists.
	public const  int Alarm_2013107 = 2013107;
	/// <summary>CCD4檢測工具輸入物件不存在!</summary>
	/// <remarks></remarks>
		//CCD4 Inspection Input Not Exists.
	public const  int Alarm_2013108 = 2013108;
	/// <summary>CCD4檢測工具輸入數量為0!</summary>
	/// <remarks></remarks>
		//CCD4 Inspection Input List Count is 0.
	public const  int Alarm_2013109 = 2013109;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2013110 = 2013110;
	/// <summary>測高儀1輸入值異常!</summary>
	/// <remarks></remarks>
		//Altimeter 1 Value is Out of Range!
	public const  int Alarm_2014000 = 2014000;
	/// <summary>測高儀1重試失敗!</summary>
	/// <remarks></remarks>
		//Altimeter 1 Retry Failed!
	public const  int Alarm_2014001 = 2014001;
	/// <summary>測高儀1自動校正失敗!</summary>
	/// <remarks></remarks>
		//Altimeter 1 Auto Calibration Failed!
	public const  int Alarm_2014002 = 2014002;
	/// <summary>測高儀1自動測高失敗!</summary>
	/// <remarks></remarks>
		//Altimeter 1 Auto Find Height Failed!
	public const  int Alarm_2014003 = 2014003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2014004 = 2014004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2014005 = 2014005;
	/// <summary>測高儀2輸入值異常!</summary>
	/// <remarks></remarks>
		//Altimeter 2 Value is Out of Range!
	public const  int Alarm_2014100 = 2014100;
	/// <summary>測高儀2重試失敗!</summary>
	/// <remarks></remarks>
		//Altimeter 2 Retry Failed!
	public const  int Alarm_2014101 = 2014101;
	/// <summary>測高儀2自動校正失敗!</summary>
	/// <remarks></remarks>
		//Altimeter 2 Auto Calibration Failed!
	public const  int Alarm_2014102 = 2014102;
	/// <summary>測高儀2自動測高失敗!</summary>
	/// <remarks></remarks>
		//Altimeter 2 Auto Find Height Failed!
	public const  int Alarm_2014103 = 2014103;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2014104 = 2014104;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2014105 = 2014105;
	/// <summary>測高儀3輸入值異常!</summary>
	/// <remarks></remarks>
		//Altimeter 3 Value is Out of Range!
	public const  int Alarm_2014200 = 2014200;
	/// <summary>測高儀3重試失敗!</summary>
	/// <remarks></remarks>
		//Altimeter 3 Retry Failed!
	public const  int Alarm_2014201 = 2014201;
	/// <summary>測高儀3自動校正失敗!</summary>
	/// <remarks></remarks>
		//Altimeter3 Auto Calibration Failed!
	public const  int Alarm_2014202 = 2014202;
	/// <summary>測高儀3自動測高失敗!</summary>
	/// <remarks></remarks>
		//Altimeter 3 Auto Find Height Failed!
	public const  int Alarm_2014203 = 2014203;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2014204 = 2014204;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2014205 = 2014205;
	/// <summary>測高儀4輸入值異常!</summary>
	/// <remarks></remarks>
		//Altimeter 4 Value is Out of Range!
	public const  int Alarm_2014300 = 2014300;
	/// <summary>測高儀4重試失敗!</summary>
	/// <remarks></remarks>
		//Altimeter 4 Retry Failed!
	public const  int Alarm_2014301 = 2014301;
	/// <summary>測高儀4自動校正失敗!</summary>
	/// <remarks></remarks>
		//Altimeter 4 Auto Calibration Failed!
	public const  int Alarm_2014302 = 2014302;
	/// <summary>測高儀4自動測高失敗!</summary>
	/// <remarks></remarks>
		//Altimeter 4 Auto Find Height Failed!
	public const  int Alarm_2014303 = 2014303;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2014304 = 2014304;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2014305 = 2014305;
	/// <summary>微量天平1初始化失敗! 通訊埠被佔用!</summary>
	/// <remarks></remarks>
		//Initialize Scale1 Failed! Port is Occupied!
	public const  int Alarm_2015000 = 2015000;
	/// <summary>微量天平1命令發送失敗!通訊埠未開啟!</summary>
	/// <remarks></remarks>
		//Scale1 Send Command Error! Port is Not Opened!
	public const  int Alarm_2015001 = 2015001;
	/// <summary>"微量天平1 重量補正失敗!"</summary>
	/// <remarks></remarks>
		//Scale1 Weight Correction failed!
	public const  int Alarm_2015002 = 2015002;
	/// <summary>"微量天平1無回應!"</summary>
	/// <remarks></remarks>
		//Scale1 Not Response!
	public const  int Alarm_2015003 = 2015003;
	/// <summary>重量1超出設定範圍</summary>
	/// <remarks></remarks>
		//Weight1 Out of Range
	public const  int Alarm_2015004 = 2015004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2015005 = 2015005;
	/// <summary>微量天平2初始化失敗! 通訊埠被佔用!</summary>
	/// <remarks></remarks>
		//Initialize Scale2 Failed! Port is Occupied!
	public const  int Alarm_2015006 = 2015006;
	/// <summary>微量天平2命令發送失敗!通訊埠未開啟!</summary>
	/// <remarks></remarks>
		//Scale2 Send Command Error! Port is Not Opened!
	public const  int Alarm_2015007 = 2015007;
	/// <summary>"微量天平2 重量補正失敗!"</summary>
	/// <remarks></remarks>
		//Scale2 Weight Correction failed!
	public const  int Alarm_2015008 = 2015008;
	/// <summary>"微量天平2無回應!"</summary>
	/// <remarks></remarks>
		//Scale2 Not Response!
	public const  int Alarm_2015009 = 2015009;
	/// <summary>重量2超出設定範圍</summary>
	/// <remarks></remarks>
		//Weight2 Out of Range
	public const  int Alarm_2015010 = 2015010;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2015011 = 2015011;
	/// <summary>觸發卡1等待觸發逾時</summary>
	/// <remarks></remarks>
		//Trigger Board1 TimeOut!
	public const  int Alarm_2016000 = 2016000;
	/// <summary>觸發卡1重置錯誤</summary>
	/// <remarks></remarks>
		//Trigger Board1 Reset Error!
	public const  int Alarm_2016001 = 2016001;
	/// <summary>Trigger Board1 Too Much or Incorrect Recipe Data, J Mode</summary>
	/// <remarks></remarks>
		//Trigger Board1 Too Much or Incorrect Recipe Data, J Mode
	public const  int Alarm_2016002 = 2016002;
	/// <summary>Trigger Board1 Too Much or Incorrect Recipe Data, T or P Mode</summary>
	/// <remarks></remarks>
		//Trigger Board1 Too Much or Incorrect Recipe Data, T or P Mode
	public const  int Alarm_2016003 = 2016003;
	/// <summary>Trigger Board1 Data Incorrect, X or D Command</summary>
	/// <remarks></remarks>
		//Trigger Board1 Data Incorrect, X or D Command
	public const  int Alarm_2016004 = 2016004;
	/// <summary>Trigger Board1 Data Incorrect, ParaTmp[]</summary>
	/// <remarks></remarks>
		//Trigger Board1 Data Incorrect, ParaTmp[]
	public const  int Alarm_2016005 = 2016005;
	/// <summary>Trigger Board1 Not Enough Memory For Target Points</summary>
	/// <remarks></remarks>
		//Trigger Board1 Not Enough Memory For Target Points
	public const  int Alarm_2016006 = 2016006;
	/// <summary>Trigger Board1 Data Incorrect, Cycle Time or Dots Number</summary>
	/// <remarks></remarks>
		//Trigger Board1 Data Incorrect, Cycle Time or Dots Number
	public const  int Alarm_2016007 = 2016007;
	/// <summary>Trigger Board1 Data Incorrect, Pitch or Dots Number</summary>
	/// <remarks></remarks>
		//Trigger Board1 Data Incorrect, Pitch or Dots Number
	public const  int Alarm_2016008 = 2016008;
	/// <summary>Trigger Board1 Fail To Generate Fire Table</summary>
	/// <remarks></remarks>
		//Trigger Board1 Fail To Generate Fire Table
	public const  int Alarm_2016009 = 2016009;
	/// <summary>Trigger Board1 Not Enough Memory For History Data</summary>
	/// <remarks></remarks>
		//Trigger Board1 Not Enough Memory For History Data
	public const  int Alarm_2016010 = 2016010;
	/// <summary>Trigger Board1 Dummy Run Do Not End Normally</summary>
	/// <remarks></remarks>
		//Trigger Board1 Dummy Run Do Not End Normally
	public const  int Alarm_2016011 = 2016011;
	/// <summary>Trigger Board1 Fail To Generate Trigger Advanced Distance</summary>
	/// <remarks></remarks>
		//Trigger Board1 Fail To Generate Trigger Advanced Distance
	public const  int Alarm_2016012 = 2016012;
	/// <summary>Trigger Board1 UART Receive Data Timeout</summary>
	/// <remarks></remarks>
		//Trigger Board1 UART Receive Data Timeout
	public const  int Alarm_2016013 = 2016013;
	/// <summary>Trigger Board1 Alarm Is Not Cleared</summary>
	/// <remarks></remarks>
		//Trigger Board1 Alarm Is Not Cleared
	public const  int Alarm_2016014 = 2016014;
	/// <summary>Trigger Board1 SPI:Too Many Fails To Talk With Slave</summary>
	/// <remarks></remarks>
		//Trigger Board1 SPI:Too Many Fails To Talk With Slave
	public const  int Alarm_2016015 = 2016015;
	/// <summary>Trigger Board1 Communication Error With Remote Display</summary>
	/// <remarks></remarks>
		//Trigger Board1 Communication Error With Remote Display
	public const  int Alarm_2016016 = 2016016;
	/// <summary>Trigger Board1 Selected PulseTime/Falling/Stroke Is Not Available</summary>
	/// <remarks></remarks>
		//Trigger Board1 Selected PulseTime/Falling/Stroke Is Not Available
	public const  int Alarm_2016017 = 2016017;
	/// <summary>Trigger Board1 Just Reboot</summary>
	/// <remarks></remarks>
		//Trigger Board1 Just Reboot
	public const  int Alarm_2016018 = 2016018;
	/// <summary>Trigger Board1 Fail To Set Parameters To Jetting Driver Board</summary>
	/// <remarks></remarks>
		//Trigger Board1 Fail To Set Parameters To Jetting Driver Board
	public const  int Alarm_2016019 = 2016019;
	/// <summary>Trigger Board1 Too Much or Incorrect Recipe Data, G or F Mode</summary>
	/// <remarks></remarks>
		//Trigger Board1 Too Much or Incorrect Recipe Data, G or F Mode
	public const  int Alarm_2016020 = 2016020;
	/// <summary>Trigger Board1 Failed to Trim The Recipe File Of F Mode</summary>
	/// <remarks></remarks>
		//Trigger Board1 Failed to Trim The Recipe File Of F Mode
	public const  int Alarm_2016021 = 2016021;
	/// <summary>Trigger Board1 Recipe: Point Type Should Not Connected With Line Type</summary>
	/// <remarks></remarks>
		//Trigger Board1 Recipe: Point Type Should Not Connected With Line Type
	public const  int Alarm_2016022 = 2016022;
	/// <summary>Trigger Board1 There Are Untriggered Dots</summary>
	/// <remarks></remarks>
		//Trigger Board1 There Are Untriggered Dots
	public const  int Alarm_2016023 = 2016023;
	/// <summary>Trigger Board1 Hit Too Late</summary>
	/// <remarks></remarks>
		//Trigger Board1 Hit Too Late
	public const  int Alarm_2016024 = 2016024;
	/// <summary>Trigger Board1 Away From Target, Will Never Be Hit</summary>
	/// <remarks></remarks>
		//Trigger Board1 Away From Target, Will Never Be Hit
	public const  int Alarm_2016025 = 2016025;
	/// <summary>Trigger Board1 Fire Table Content Has Zero</summary>
	/// <remarks></remarks>
		//Trigger Board1 Fire Table Content Has Zero
	public const  int Alarm_2016026 = 2016026;
	/// <summary>Trigger Board1 Not Enough Memory For Mapping Data</summary>
	/// <remarks></remarks>
		//Trigger Board1 Not Enough Memory For Mapping Data
	public const  int Alarm_2016027 = 2016027;
	/// <summary>Trigger Board1 Dummy Run: Error In Direction Check Process</summary>
	/// <remarks></remarks>
		//Trigger Board1 Dummy Run: Error In Direction Check Process
	public const  int Alarm_2016028 = 2016028;
	/// <summary>Trigger Board1 Fire: Error In Direction Check Process</summary>
	/// <remarks></remarks>
		//Trigger Board1 Fire: Error In Direction Check Process
	public const  int Alarm_2016029 = 2016029;
	/// <summary>Trigger Board1 HMI Communication Timeout</summary>
	/// <remarks></remarks>
		//Trigger Board1 HMI Communication Timeout
	public const  int Alarm_2016030 = 2016030;
	/// <summary>Trigger Board1 Driver Over Temperature</summary>
	/// <remarks></remarks>
		//Trigger Board1 Driver Over Temperature
	public const  int Alarm_2016031 = 2016031;
	/// <summary>Trigger Board1 Driver has no High Voltage</summary>
	/// <remarks></remarks>
		//Trigger Board1 Driver has no High Voltage
	public const  int Alarm_2016032 = 2016032;
	/// <summary>Trigger Board1 Communication fail with PICO valve</summary>
	/// <remarks></remarks>
		//Trigger Board1 Communication fail with PICO valve
	public const  int Alarm_2016033 = 2016033;
	/// <summary>Trigger Board1 Piezo Over Temperature</summary>
	/// <remarks></remarks>
		//Trigger Board1 Piezo Over Temperature
	public const  int Alarm_2016034 = 2016034;
	/// <summary>Trigger Board1 Driver Board reboot</summary>
	/// <remarks></remarks>
		//Trigger Board1 Driver Board reboot
	public const  int Alarm_2016035 = 2016035;
	/// <summary>觸發卡2等待觸發逾時</summary>
	/// <remarks></remarks>
		//Trigger Board2 TimeOut!
	public const  int Alarm_2016100 = 2016100;
	/// <summary>觸發卡2重置錯誤</summary>
	/// <remarks></remarks>
		//Trigger Board2 Reset Error!
	public const  int Alarm_2016101 = 2016101;
	/// <summary>Trigger Board2 Too Much or Incorrect Recipe Data, J Mode</summary>
	/// <remarks></remarks>
		//Trigger Board2 Too Much or Incorrect Recipe Data, J Mode
	public const  int Alarm_2016102 = 2016102;
	/// <summary>Trigger Board2 Too Much or Incorrect Recipe Data, T or P Mode</summary>
	/// <remarks></remarks>
		//Trigger Board2 Too Much or Incorrect Recipe Data, T or P Mode
	public const  int Alarm_2016103 = 2016103;
	/// <summary>Trigger Board2 Data Incorrect, X or D Command</summary>
	/// <remarks></remarks>
		//Trigger Board2 Data Incorrect, X or D Command
	public const  int Alarm_2016104 = 2016104;
	/// <summary>Trigger Board2 Data Incorrect, ParaTmp[]</summary>
	/// <remarks></remarks>
		//Trigger Board2 Data Incorrect, ParaTmp[]
	public const  int Alarm_2016105 = 2016105;
	/// <summary>Trigger Board2 Not Enough Memory For Target Points</summary>
	/// <remarks></remarks>
		//Trigger Board2 Not Enough Memory For Target Points
	public const  int Alarm_2016106 = 2016106;
	/// <summary>Trigger Board2 Data Incorrect, Cycle Time or Dots Number</summary>
	/// <remarks></remarks>
		//Trigger Board2 Data Incorrect, Cycle Time or Dots Number
	public const  int Alarm_2016107 = 2016107;
	/// <summary>Trigger Board2 Data Incorrect, Pitch or Dots Number</summary>
	/// <remarks></remarks>
		//Trigger Board2 Data Incorrect, Pitch or Dots Number
	public const  int Alarm_2016108 = 2016108;
	/// <summary>Trigger Board2 Fail To Generate Fire Table</summary>
	/// <remarks></remarks>
		//Trigger Board2 Fail To Generate Fire Table
	public const  int Alarm_2016109 = 2016109;
	/// <summary>Trigger Board2 Not Enough Memory For History Data</summary>
	/// <remarks></remarks>
		//Trigger Board2 Not Enough Memory For History Data
	public const  int Alarm_2016110 = 2016110;
	/// <summary>Trigger Board2 Dummy Run Do Not End Normally</summary>
	/// <remarks></remarks>
		//Trigger Board2 Dummy Run Do Not End Normally
	public const  int Alarm_2016111 = 2016111;
	/// <summary>Trigger Board2 Fail To Generate Trigger Advanced Distance</summary>
	/// <remarks></remarks>
		//Trigger Board2 Fail To Generate Trigger Advanced Distance
	public const  int Alarm_2016112 = 2016112;
	/// <summary>Trigger Board2 UART Receive Data Timeout</summary>
	/// <remarks></remarks>
		//Trigger Board2 UART Receive Data Timeout
	public const  int Alarm_2016113 = 2016113;
	/// <summary>Trigger Board2 Alarm Is Not Cleared</summary>
	/// <remarks></remarks>
		//Trigger Board2 Alarm Is Not Cleared
	public const  int Alarm_2016114 = 2016114;
	/// <summary>Trigger Board2 SPI:Too Many Fails To Talk With Slave</summary>
	/// <remarks></remarks>
		//Trigger Board2 SPI:Too Many Fails To Talk With Slave
	public const  int Alarm_2016115 = 2016115;
	/// <summary>Trigger Board2 Communication Error With Remote Display</summary>
	/// <remarks></remarks>
		//Trigger Board2 Communication Error With Remote Display
	public const  int Alarm_2016116 = 2016116;
	/// <summary>Trigger Board2 Selected PulseTime/Falling/Stroke Is Not Available</summary>
	/// <remarks></remarks>
		//Trigger Board2 Selected PulseTime/Falling/Stroke Is Not Available
	public const  int Alarm_2016117 = 2016117;
	/// <summary>Trigger Board2 Just Reboot</summary>
	/// <remarks></remarks>
		//Trigger Board2 Just Reboot
	public const  int Alarm_2016118 = 2016118;
	/// <summary>Trigger Board2 Fail To Set Parameters To Jetting Driver Board</summary>
	/// <remarks></remarks>
		//Trigger Board2 Fail To Set Parameters To Jetting Driver Board
	public const  int Alarm_2016119 = 2016119;
	/// <summary>Trigger Board2 Too Much or Incorrect Recipe Data, G or F Mode</summary>
	/// <remarks></remarks>
		//Trigger Board2 Too Much or Incorrect Recipe Data, G or F Mode
	public const  int Alarm_2016120 = 2016120;
	/// <summary>Trigger Board2 Failed to Trim The Recipe File Of F Mode</summary>
	/// <remarks></remarks>
		//Trigger Board2 Failed to Trim The Recipe File Of F Mode
	public const  int Alarm_2016121 = 2016121;
	/// <summary>Trigger Board2 Recipe: Point Type Should Not Connected With Line Type</summary>
	/// <remarks></remarks>
		//Trigger Board2 Recipe: Point Type Should Not Connected With Line Type
	public const  int Alarm_2016122 = 2016122;
	/// <summary>Trigger Board2 There Are Untriggered Dots</summary>
	/// <remarks></remarks>
		//Trigger Board2 There Are Untriggered Dots
	public const  int Alarm_2016123 = 2016123;
	/// <summary>Trigger Board2 Hit Too Late</summary>
	/// <remarks></remarks>
		//Trigger Board2 Hit Too Late
	public const  int Alarm_2016124 = 2016124;
	/// <summary>Trigger Board2 Away From Target, Will Never Be Hit</summary>
	/// <remarks></remarks>
		//Trigger Board2 Away From Target, Will Never Be Hit
	public const  int Alarm_2016125 = 2016125;
	/// <summary>Trigger Board2 Fire Table Content Has Zero</summary>
	/// <remarks></remarks>
		//Trigger Board2 Fire Table Content Has Zero
	public const  int Alarm_2016126 = 2016126;
	/// <summary>Trigger Board2 Not Enough Memory For Mapping Data</summary>
	/// <remarks></remarks>
		//Trigger Board2 Not Enough Memory For Mapping Data
	public const  int Alarm_2016127 = 2016127;
	/// <summary>Trigger Board2 Dummy Run: Error In Direction Check Process</summary>
	/// <remarks></remarks>
		//Trigger Board2 Dummy Run: Error In Direction Check Process
	public const  int Alarm_2016128 = 2016128;
	/// <summary>Trigger Board2 Fire: Error In Direction Check Process</summary>
	/// <remarks></remarks>
		//Trigger Board2 Fire: Error In Direction Check Process
	public const  int Alarm_2016129 = 2016129;
	/// <summary>Trigger Board2 HMI Communication Timeout</summary>
	/// <remarks></remarks>
		//Trigger Board2 HMI Communication Timeout
	public const  int Alarm_2016130 = 2016130;
	/// <summary>Trigger Board2 Driver Over Temperature</summary>
	/// <remarks></remarks>
		//Trigger Board2 Driver Over Temperature
	public const  int Alarm_2016131 = 2016131;
	/// <summary>Trigger Board2 Driver has no High Voltage</summary>
	/// <remarks></remarks>
		//Trigger Board2 Driver has no High Voltage
	public const  int Alarm_2016132 = 2016132;
	/// <summary>Trigger Board2 Communication fail with PICO valve</summary>
	/// <remarks></remarks>
		//Trigger Board2 Communication fail with PICO valve
	public const  int Alarm_2016133 = 2016133;
	/// <summary>Trigger Board2 Piezo Over Temperature</summary>
	/// <remarks></remarks>
		//Trigger Board1 Piezo Over Temperature
	public const  int Alarm_2016134 = 2016134;
	/// <summary>Trigger Board2 Driver Board reboot</summary>
	/// <remarks></remarks>
		//Trigger Board2 Driver Board reboot
	public const  int Alarm_2016135 = 2016135;
	/// <summary>"觸發卡3等待觸發逾時"</summary>
	/// <remarks></remarks>
		//Trigger Board3 TimeOut!
	public const  int Alarm_2016200 = 2016200;
	/// <summary>觸發卡3重置錯誤</summary>
	/// <remarks></remarks>
		//Trigger Board3 Reset Error!
	public const  int Alarm_2016201 = 2016201;
	/// <summary>Trigger Board3 Too Much or Incorrect Recipe Data, J Mode</summary>
	/// <remarks></remarks>
		//Trigger Board3 Too Much or Incorrect Recipe Data, J Mode
	public const  int Alarm_2016202 = 2016202;
	/// <summary>Trigger Board3 Too Much or Incorrect Recipe Data, T or P Mode</summary>
	/// <remarks></remarks>
		//Trigger Board3 Too Much or Incorrect Recipe Data, T or P Mode
	public const  int Alarm_2016203 = 2016203;
	/// <summary>Trigger Board3 Data Incorrect, X or D Command</summary>
	/// <remarks></remarks>
		//Trigger Board3 Data Incorrect, X or D Command
	public const  int Alarm_2016204 = 2016204;
	/// <summary>Trigger Board3 Data Incorrect, ParaTmp[]</summary>
	/// <remarks></remarks>
		//Trigger Board3 Data Incorrect, ParaTmp[]
	public const  int Alarm_2016205 = 2016205;
	/// <summary>Trigger Board3 Not Enough Memory For Target Points</summary>
	/// <remarks></remarks>
		//Trigger Board3 Not Enough Memory For Target Points
	public const  int Alarm_2016206 = 2016206;
	/// <summary>Trigger Board3 Data Incorrect, Cycle Time or Dots Number</summary>
	/// <remarks></remarks>
		//Trigger Board3 Data Incorrect, Cycle Time or Dots Number
	public const  int Alarm_2016207 = 2016207;
	/// <summary>Trigger Board3 Data Incorrect, Pitch or Dots Number</summary>
	/// <remarks></remarks>
		//Trigger Board3 Data Incorrect, Pitch or Dots Number
	public const  int Alarm_2016208 = 2016208;
	/// <summary>Trigger Board3 Fail To Generate Fire Table</summary>
	/// <remarks></remarks>
		//Trigger Board3 Fail To Generate Fire Table
	public const  int Alarm_2016209 = 2016209;
	/// <summary>Trigger Board3 Not Enough Memory For History Data</summary>
	/// <remarks></remarks>
		//Trigger Board3 Not Enough Memory For History Data
	public const  int Alarm_2016210 = 2016210;
	/// <summary>Trigger Board3 Dummy Run Do Not End Normally</summary>
	/// <remarks></remarks>
		//Trigger Board3 Dummy Run Do Not End Normally
	public const  int Alarm_2016211 = 2016211;
	/// <summary>Trigger Board3 Fail To Generate Trigger Advanced Distance</summary>
	/// <remarks></remarks>
		//Trigger Board3 Fail To Generate Trigger Advanced Distance
	public const  int Alarm_2016212 = 2016212;
	/// <summary>Trigger Board3 UART Receive Data Timeout</summary>
	/// <remarks></remarks>
		//Trigger Board3 UART Receive Data Timeout
	public const  int Alarm_2016213 = 2016213;
	/// <summary>Trigger Board3 Alarm Is Not Cleared</summary>
	/// <remarks></remarks>
		//Trigger Board3 Alarm Is Not Cleared
	public const  int Alarm_2016214 = 2016214;
	/// <summary>Trigger Board3 SPI:Too Many Fails To Talk With Slave</summary>
	/// <remarks></remarks>
		//Trigger Board3 SPI:Too Many Fails To Talk With Slave
	public const  int Alarm_2016215 = 2016215;
	/// <summary>Trigger Board3 Communication Error With Remote Display</summary>
	/// <remarks></remarks>
		//Trigger Board3 Communication Error With Remote Display
	public const  int Alarm_2016216 = 2016216;
	/// <summary>Trigger Board3 Selected PulseTime/Falling/Stroke Is Not Available</summary>
	/// <remarks></remarks>
		//Trigger Board3 Selected PulseTime/Falling/Stroke Is Not Available
	public const  int Alarm_2016217 = 2016217;
	/// <summary>Trigger Board3 Just Reboot</summary>
	/// <remarks></remarks>
		//Trigger Board3 Just Reboot
	public const  int Alarm_2016218 = 2016218;
	/// <summary>Trigger Board3 Fail To Set Parameters To Jetting Driver Board</summary>
	/// <remarks></remarks>
		//Trigger Board3 Fail To Set Parameters To Jetting Driver Board
	public const  int Alarm_2016219 = 2016219;
	/// <summary>Trigger Board3 Too Much or Incorrect Recipe Data, G or F Mode</summary>
	/// <remarks></remarks>
		//Trigger Board3 Too Much or Incorrect Recipe Data, G or F Mode
	public const  int Alarm_2016220 = 2016220;
	/// <summary>Trigger Board3 Failed to Trim The Recipe File Of F Mode</summary>
	/// <remarks></remarks>
		//Trigger Board3 Failed to Trim The Recipe File Of F Mode
	public const  int Alarm_2016221 = 2016221;
	/// <summary>Trigger Board3 Recipe: Point Type Should Not Connected With Line Type</summary>
	/// <remarks></remarks>
		//Trigger Board3 Recipe: Point Type Should Not Connected With Line Type
	public const  int Alarm_2016222 = 2016222;
	/// <summary>Trigger Board3 There Are Untriggered Dots</summary>
	/// <remarks></remarks>
		//Trigger Board3 There Are Untriggered Dots
	public const  int Alarm_2016223 = 2016223;
	/// <summary>Trigger Board3 Hit Too Late</summary>
	/// <remarks></remarks>
		//Trigger Board3 Hit Too Late
	public const  int Alarm_2016224 = 2016224;
	/// <summary>Trigger Board3 Away From Target, Will Never Be Hit</summary>
	/// <remarks></remarks>
		//Trigger Board3 Away From Target, Will Never Be Hit
	public const  int Alarm_2016225 = 2016225;
	/// <summary>Trigger Board3 Fire Table Content Has Zero</summary>
	/// <remarks></remarks>
		//Trigger Board3 Fire Table Content Has Zero
	public const  int Alarm_2016226 = 2016226;
	/// <summary>Trigger Board3 Not Enough Memory For Mapping Data</summary>
	/// <remarks></remarks>
		//Trigger Board3 Not Enough Memory For Mapping Data
	public const  int Alarm_2016227 = 2016227;
	/// <summary>Trigger Board3 Dummy Run: Error In Direction Check Process</summary>
	/// <remarks></remarks>
		//Trigger Board3 Dummy Run: Error In Direction Check Process
	public const  int Alarm_2016228 = 2016228;
	/// <summary>Trigger Board3 Fire: Error In Direction Check Process</summary>
	/// <remarks></remarks>
		//Trigger Board3 Fire: Error In Direction Check Process
	public const  int Alarm_2016229 = 2016229;
	/// <summary>Trigger Board3 HMI Communication Timeout</summary>
	/// <remarks></remarks>
		//Trigger Board3 HMI Communication Timeout
	public const  int Alarm_2016230 = 2016230;
	/// <summary>Trigger Board3 Driver Over Temperature</summary>
	/// <remarks></remarks>
		//Trigger Board3 Driver Over Temperature
	public const  int Alarm_2016231 = 2016231;
	/// <summary>Trigger Board3 Driver has no High Voltage</summary>
	/// <remarks></remarks>
		//Trigger Board3 Driver has no High Voltage
	public const  int Alarm_2016232 = 2016232;
	/// <summary>Trigger Board3 Communication fail with PICO valve</summary>
	/// <remarks></remarks>
		//Trigger Board3 Communication fail with PICO valve
	public const  int Alarm_2016233 = 2016233;
	/// <summary>Trigger Board3 Piezo Over Temperature</summary>
	/// <remarks></remarks>
		//Trigger Board3 Piezo Over Temperature
	public const  int Alarm_2016234 = 2016234;
	/// <summary>Trigger Board3 Driver Board reboot</summary>
	/// <remarks></remarks>
		//Trigger Board3 Driver Board reboot
	public const  int Alarm_2016235 = 2016235;
	/// <summary>"觸發卡4等待觸發逾時"</summary>
	/// <remarks></remarks>
		//Trigger Board4 TimeOut!
	public const  int Alarm_2016300 = 2016300;
	/// <summary>觸發卡4重置錯誤</summary>
	/// <remarks></remarks>
		//Trigger Board4 Reset Error!
	public const  int Alarm_2016301 = 2016301;
	/// <summary>Trigger Board4 Too Much or Incorrect Recipe Data, J Mode</summary>
	/// <remarks></remarks>
		//Trigger Board4 Too Much or Incorrect Recipe Data, J Mode
	public const  int Alarm_2016302 = 2016302;
	/// <summary>Trigger Board4 Too Much or Incorrect Recipe Data, T or P Mode</summary>
	/// <remarks></remarks>
		//Trigger Board4 Too Much or Incorrect Recipe Data, T or P Mode
	public const  int Alarm_2016303 = 2016303;
	/// <summary>Trigger Board4 Data Incorrect, X or D Command</summary>
	/// <remarks></remarks>
		//Trigger Board4 Data Incorrect, X or D Command
	public const  int Alarm_2016304 = 2016304;
	/// <summary>Trigger Board4 Data Incorrect, ParaTmp[]</summary>
	/// <remarks></remarks>
		//Trigger Board4 Data Incorrect, ParaTmp[]
	public const  int Alarm_2016305 = 2016305;
	/// <summary>Trigger Board4 Not Enough Memory For Target Points</summary>
	/// <remarks></remarks>
		//Trigger Board4 Not Enough Memory For Target Points
	public const  int Alarm_2016306 = 2016306;
	/// <summary>Trigger Board4 Data Incorrect, Cycle Time or Dots Number</summary>
	/// <remarks></remarks>
		//Trigger Board4 Data Incorrect, Cycle Time or Dots Number
	public const  int Alarm_2016307 = 2016307;
	/// <summary>Trigger Board4 Data Incorrect, Pitch or Dots Number</summary>
	/// <remarks></remarks>
		//Trigger Board4 Data Incorrect, Pitch or Dots Number
	public const  int Alarm_2016308 = 2016308;
	/// <summary>Trigger Board4 Fail To Generate Fire Table</summary>
	/// <remarks></remarks>
		//Trigger Board4 Fail To Generate Fire Table
	public const  int Alarm_2016309 = 2016309;
	/// <summary>Trigger Board4 Not Enough Memory For History Data</summary>
	/// <remarks></remarks>
		//Trigger Board4 Not Enough Memory For History Data
	public const  int Alarm_2016310 = 2016310;
	/// <summary>Trigger Board4 Dummy Run Do Not End Normally</summary>
	/// <remarks></remarks>
		//Trigger Board4 Dummy Run Do Not End Normally
	public const  int Alarm_2016311 = 2016311;
	/// <summary>Trigger Board4 Fail To Generate Trigger Advanced Distance</summary>
	/// <remarks></remarks>
		//Trigger Board4 Fail To Generate Trigger Advanced Distance
	public const  int Alarm_2016312 = 2016312;
	/// <summary>Trigger Board4 UART Receive Data Timeout</summary>
	/// <remarks></remarks>
		//Trigger Board4 UART Receive Data Timeout
	public const  int Alarm_2016313 = 2016313;
	/// <summary>Trigger Board4 Alarm Is Not Cleared</summary>
	/// <remarks></remarks>
		//Trigger Board4 Alarm Is Not Cleared
	public const  int Alarm_2016314 = 2016314;
	/// <summary>Trigger Board4 SPI:Too Many Fails To Talk With Slave</summary>
	/// <remarks></remarks>
		//Trigger Board4 SPI:Too Many Fails To Talk With Slave
	public const  int Alarm_2016315 = 2016315;
	/// <summary>Trigger Board4 Communication Error With Remote Display</summary>
	/// <remarks></remarks>
		//Trigger Board4 Communication Error With Remote Display
	public const  int Alarm_2016316 = 2016316;
	/// <summary>Trigger Board4 Selected PulseTime/Falling/Stroke Is Not Available</summary>
	/// <remarks></remarks>
		//Trigger Board4 Selected PulseTime/Falling/Stroke Is Not Available
	public const  int Alarm_2016317 = 2016317;
	/// <summary>Trigger Board4 Just Reboot</summary>
	/// <remarks></remarks>
		//Trigger Board4 Just Reboot
	public const  int Alarm_2016318 = 2016318;
	/// <summary>Trigger Board4 Fail To Set Parameters To Jetting Driver Board</summary>
	/// <remarks></remarks>
		//Trigger Board4 Fail To Set Parameters To Jetting Driver Board
	public const  int Alarm_2016319 = 2016319;
	/// <summary>Trigger Board4 Too Much or Incorrect Recipe Data, G or F Mode</summary>
	/// <remarks></remarks>
		//Trigger Board4 Too Much or Incorrect Recipe Data, G or F Mode
	public const  int Alarm_2016320 = 2016320;
	/// <summary>Trigger Board4 Failed to Trim The Recipe File Of F Mode</summary>
	/// <remarks></remarks>
		//Trigger Board4 Failed to Trim The Recipe File Of F Mode
	public const  int Alarm_2016321 = 2016321;
	/// <summary>Trigger Board4 Recipe: Point Type Should Not Connected With Line Type</summary>
	/// <remarks></remarks>
		//Trigger Board4 Recipe: Point Type Should Not Connected With Line Type
	public const  int Alarm_2016322 = 2016322;
	/// <summary>Trigger Board4 There Are Untriggered Dots</summary>
	/// <remarks></remarks>
		//Trigger Board4 There Are Untriggered Dots
	public const  int Alarm_2016323 = 2016323;
	/// <summary>Trigger Board4 Hit Too Late</summary>
	/// <remarks></remarks>
		//Trigger Board4 Hit Too Late
	public const  int Alarm_2016324 = 2016324;
	/// <summary>Trigger Board4 Away From Target, Will Never Be Hit</summary>
	/// <remarks></remarks>
		//Trigger Board4 Away From Target, Will Never Be Hit
	public const  int Alarm_2016325 = 2016325;
	/// <summary>Trigger Board4 Fire Table Content Has Zero</summary>
	/// <remarks></remarks>
		//Trigger Board4 Fire Table Content Has Zero
	public const  int Alarm_2016326 = 2016326;
	/// <summary>Trigger Board4 Not Enough Memory For Mapping Data</summary>
	/// <remarks></remarks>
		//Trigger Board4 Not Enough Memory For Mapping Data
	public const  int Alarm_2016327 = 2016327;
	/// <summary>Trigger Board4 Dummy Run: Error In Direction Check Process</summary>
	/// <remarks></remarks>
		//Trigger Board4 Dummy Run: Error In Direction Check Process
	public const  int Alarm_2016328 = 2016328;
	/// <summary>Trigger Board4 Fire: Error In Direction Check Process</summary>
	/// <remarks></remarks>
		//Trigger Board4 Fire: Error In Direction Check Process
	public const  int Alarm_2016329 = 2016329;
	/// <summary>Trigger Board4 HMI Communication Timeout</summary>
	/// <remarks></remarks>
		//Trigger Board4 HMI Communication Timeout
	public const  int Alarm_2016330 = 2016330;
	/// <summary>Trigger Board4 Driver Over Temperature</summary>
	/// <remarks></remarks>
		//Trigger Board4 Driver Over Temperature
	public const  int Alarm_2016331 = 2016331;
	/// <summary>Trigger Board4 Driver has no High Voltage</summary>
	/// <remarks></remarks>
		//Trigger Board4 Driver has no High Voltage
	public const  int Alarm_2016332 = 2016332;
	/// <summary>Trigger Board4 Communication fail with PICO valve</summary>
	/// <remarks></remarks>
		//Trigger Board4 Communication fail with PICO valve
	public const  int Alarm_2016333 = 2016333;
	/// <summary>Trigger Board4 Piezo Over Temperature</summary>
	/// <remarks></remarks>
		//Trigger Board4 Piezo Over Temperature
	public const  int Alarm_2016334 = 2016334;
	/// <summary>Trigger Board4 Driver Board reboot</summary>
	/// <remarks></remarks>
		//Trigger Board4 Driver Board reboot
	public const  int Alarm_2016335 = 2016335;
	/// <summary>FMCS1初始化失敗! 通訊埠被佔用!</summary>
	/// <remarks></remarks>
		//Initialize FMCS1 Failed! Port is Occupied!
	public const  int Alarm_2017000 = 2017000;
	/// <summary>FMCS1資料超過規格上界!</summary>
	/// <remarks></remarks>
		//FMCS1 Data is Out of USL
	public const  int Alarm_2017001 = 2017001;
	/// <summary>FMCS1資料低於規格下界!</summary>
	/// <remarks></remarks>
		//FMCS1 Data is Out of LSL
	public const  int Alarm_2017002 = 2017002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2017003 = 2017003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2017004 = 2017004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2017005 = 2017005;
	/// <summary>FMCS2初始化失敗! 通訊埠被佔用!</summary>
	/// <remarks></remarks>
		//Initialize FMCS2 Failed! Port is Occupied!
	public const  int Alarm_2017100 = 2017100;
	/// <summary>FMCS2資料超過規格上界!</summary>
	/// <remarks></remarks>
		//FMCS2 Data is Out of USL
	public const  int Alarm_2017101 = 2017101;
	/// <summary>FMCS2資料低於規格下界!</summary>
	/// <remarks></remarks>
		//FMCS2 Data is Out of LSL
	public const  int Alarm_2017102 = 2017102;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2017103 = 2017103;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2017104 = 2017104;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2017105 = 2017105;
	/// <summary>FMCS3初始化失敗! 通訊埠被佔用!</summary>
	/// <remarks></remarks>
		//Initialize FMCS3 Failed! Port is Occupied!
	public const  int Alarm_2017200 = 2017200;
	/// <summary>FMCS3資料超過規格上界!</summary>
	/// <remarks></remarks>
		//FMCS3 Data is Out of USL
	public const  int Alarm_2017201 = 2017201;
	/// <summary>FMCS3資料低於規格下界!</summary>
	/// <remarks></remarks>
		//FMCS3 Data is Out of LSL
	public const  int Alarm_2017202 = 2017202;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2017203 = 2017203;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2017204 = 2017204;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2017205 = 2017205;
	/// <summary>FMCS4初始化失敗! 通訊埠被佔用!</summary>
	/// <remarks></remarks>
		//Initialize FMCS4 Failed! Port is Occupied!
	public const  int Alarm_2017300 = 2017300;
	/// <summary>FMCS4資料超過規格上界!</summary>
	/// <remarks></remarks>
		//FMCS4 Data is Out of USL
	public const  int Alarm_2017301 = 2017301;
	/// <summary>FMCS4資料低於規格下界!</summary>
	/// <remarks></remarks>
		//FMCS4 Data is Out of LSL
	public const  int Alarm_2017302 = 2017302;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2017303 = 2017303;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2017304 = 2017304;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2017305 = 2017305;
	/// <summary>閥1膠管氣壓建立異常!</summary>
	/// <remarks></remarks>
		//Valve1 Syringe Air Pressure Alarm!
	public const  int Alarm_2019000 = 2019000;
	/// <summary>閥1Purge真空建立失敗!</summary>
	/// <remarks></remarks>
		//Valve1 Purge Vacuum Alarm
	public const  int Alarm_2019001 = 2019001;
	/// <summary>閥1控制器異常!</summary>
	/// <remarks></remarks>
		//Valve1 Controller Alarm!
	public const  int Alarm_2019002 = 2019002;
	/// <summary>閥1膠重判定異常!</summary>
	/// <remarks></remarks>
		//Valve1 Weight is Out of Range!
	public const  int Alarm_2019003 = 2019003;
	/// <summary>閥1膠量判定異常!</summary>
	/// <remarks></remarks>
		//Valve1 Volume is Out of Range!
	public const  int Alarm_2019004 = 2019004;
	/// <summary>閥1自動校正失敗!</summary>
	/// <remarks></remarks>
		//Valve1 Auto Calibration Failed!
	public const  int Alarm_2019005 = 2019005;
	/// <summary>閥1自動測高失敗!</summary>
	/// <remarks></remarks>
		//Valve1 Auto Find Height Failed!
	public const  int Alarm_2019006 = 2019006;
	/// <summary>閥1膠閥清潔失敗!</summary>
	/// <remarks></remarks>
		//Valve1 Clear Failed!
	public const  int Alarm_2019007 = 2019007;
	/// <summary>閥1閥座溫度判定異常!</summary>
	/// <remarks></remarks>
		//Valve1 ValveSet Temperature is Out of Range
	public const  int Alarm_2019008 = 2019008;
	/// <summary>閥1閥體溫度判定異常!</summary>
	/// <remarks></remarks>
		//Valve1 ValveBody Temperature is Out of Range
	public const  int Alarm_2019009 = 2019009;
	/// <summary>閥1膠管溫度判定異常!</summary>
	/// <remarks></remarks>
		//Valve1 Syringe Temperature is Out of Range
	public const  int Alarm_2019010 = 2019010;
	/// <summary>閥1換膠動作異常!</summary>
	/// <remarks></remarks>
		//Valve1 Change Action Failed!
	public const  int Alarm_2019011 = 2019011;
	/// <summary>閥1Purge動作異常!</summary>
	/// <remarks></remarks>
		//Valve1 Purge Action Failed!
	public const  int Alarm_2019012 = 2019012;
	/// <summary>閥1秤重動作異常!</summary>
	/// <remarks></remarks>
		//Valve1 Weighing Action Failed!
	public const  int Alarm_2019013 = 2019013;
	/// <summary>閥1Purge參數讀取失敗!</summary>
	/// <remarks></remarks>
		//Valve1 Purge Parameter Reading Failed!
	public const  int Alarm_2019014 = 2019014;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2019015 = 2019015;
	/// <summary>閥2膠管氣壓建立異常!</summary>
	/// <remarks></remarks>
		//Valve2 Syringe Air Pressure Alarm!
	public const  int Alarm_2019100 = 2019100;
	/// <summary>閥2Purge真空建立失敗!</summary>
	/// <remarks></remarks>
		//Valve2 Purge Vacuum Alarm
	public const  int Alarm_2019101 = 2019101;
	/// <summary>閥2控制器異常!</summary>
	/// <remarks></remarks>
		//Valve2 Controller Alarm!
	public const  int Alarm_2019102 = 2019102;
	/// <summary>閥2膠重判定異常!</summary>
	/// <remarks></remarks>
		//Valve2 Weight is Out of Range!
	public const  int Alarm_2019103 = 2019103;
	/// <summary>閥2膠量判定異常!</summary>
	/// <remarks></remarks>
		//Valve2 Volume is Out of Range!
	public const  int Alarm_2019104 = 2019104;
	/// <summary>閥2自動校正失敗!</summary>
	/// <remarks></remarks>
		//Valve2 Auto Calibration Failed!
	public const  int Alarm_2019105 = 2019105;
	/// <summary>閥2自動測高失敗!</summary>
	/// <remarks></remarks>
		//Valve2 Auto Find Height Failed!
	public const  int Alarm_2019106 = 2019106;
	/// <summary>閥2膠閥清潔失敗!</summary>
	/// <remarks></remarks>
		//Valve2 Clear Failed!
	public const  int Alarm_2019107 = 2019107;
	/// <summary>閥2閥座溫度判定異常!</summary>
	/// <remarks></remarks>
		//Valve2 ValveSet Temperature is Out of Range
	public const  int Alarm_2019108 = 2019108;
	/// <summary>閥2閥體溫度判定異常!</summary>
	/// <remarks></remarks>
		//Valve2 ValveBody Temperature is Out of Range
	public const  int Alarm_2019109 = 2019109;
	/// <summary>閥2膠管溫度判定異常!</summary>
	/// <remarks></remarks>
		//Valve2 Syringe Temperature is Out of Range
	public const  int Alarm_2019110 = 2019110;
	/// <summary>閥2換膠動作異常!</summary>
	/// <remarks></remarks>
		//Valve2 Change Action Failed!
	public const  int Alarm_2019111 = 2019111;
	/// <summary>閥2Purge動作異常!</summary>
	/// <remarks></remarks>
		//Valve2 Purge Action Failed!
	public const  int Alarm_2019112 = 2019112;
	/// <summary>閥2秤重動作異常!</summary>
	/// <remarks></remarks>
		//Valve2 Weighing Action Failed!
	public const  int Alarm_2019113 = 2019113;
	/// <summary>閥2Purge參數讀取失敗!</summary>
	/// <remarks></remarks>
		//Valve2 Purge Parameter Reading Failed!
	public const  int Alarm_2019114 = 2019114;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2019115 = 2019115;
	/// <summary>閥3膠管氣壓建立異常!</summary>
	/// <remarks></remarks>
		//Valve3 Syringe Air Pressure Alarm!
	public const  int Alarm_2019200 = 2019200;
	/// <summary>閥3Purge真空建立失敗!</summary>
	/// <remarks></remarks>
		//Valve3 Purge Vacuum Alarm
	public const  int Alarm_2019201 = 2019201;
	/// <summary>閥3控制器異常!</summary>
	/// <remarks></remarks>
		//Valve3 Controller Alarm!
	public const  int Alarm_2019202 = 2019202;
	/// <summary>閥3膠重判定異常!</summary>
	/// <remarks></remarks>
		//Valve3 Weight is Out of Range!
	public const  int Alarm_2019203 = 2019203;
	/// <summary>閥3膠量判定異常!</summary>
	/// <remarks></remarks>
		//Valve3 Volume is Out of Range!
	public const  int Alarm_2019204 = 2019204;
	/// <summary>閥3自動校正失敗!</summary>
	/// <remarks></remarks>
		//Valve3 Auto Calibration Failed!
	public const  int Alarm_2019205 = 2019205;
	/// <summary>閥3自動測高失敗!</summary>
	/// <remarks></remarks>
		//Valve3 Auto Find Height Failed!
	public const  int Alarm_2019206 = 2019206;
	/// <summary>閥3膠閥清潔失敗!</summary>
	/// <remarks></remarks>
		//Valve3 Clear Failed!
	public const  int Alarm_2019207 = 2019207;
	/// <summary>閥3閥座溫度判定異常!</summary>
	/// <remarks></remarks>
		//Valve3 ValveSet Temperature is Out of Range
	public const  int Alarm_2019208 = 2019208;
	/// <summary>閥3閥體溫度判定異常!</summary>
	/// <remarks></remarks>
		//Valve3 ValveBody Temperature is Out of Range
	public const  int Alarm_2019209 = 2019209;
	/// <summary>閥3膠管溫度判定異常!</summary>
	/// <remarks></remarks>
		//Valve3 Syringe Temperature is Out of Range
	public const  int Alarm_2019210 = 2019210;
	/// <summary>閥3換膠動作異常!</summary>
	/// <remarks></remarks>
		//Valve3 Change Action Failed!
	public const  int Alarm_2019211 = 2019211;
	/// <summary>閥3Purge動作異常!</summary>
	/// <remarks></remarks>
		//Valve3 Purge Action Failed!
	public const  int Alarm_2019212 = 2019212;
	/// <summary>閥3秤重動作異常!</summary>
	/// <remarks></remarks>
		//Valve3 Weighing Action Failed!
	public const  int Alarm_2019213 = 2019213;
	/// <summary>閥3Purge參數讀取失敗!</summary>
	/// <remarks></remarks>
		//Valve3 Purge Parameter Reading Failed!
	public const  int Alarm_2019214 = 2019214;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2019215 = 2019215;
	/// <summary>閥4膠管氣壓建立異常!</summary>
	/// <remarks></remarks>
		//Valve4 Syringe Air Pressure Alarm!
	public const  int Alarm_2019300 = 2019300;
	/// <summary>閥4Purge真空建立失敗!</summary>
	/// <remarks></remarks>
		//Valve4 Purge Vacuum Alarm
	public const  int Alarm_2019301 = 2019301;
	/// <summary>閥4控制器異常!</summary>
	/// <remarks></remarks>
		//Valve4 Controller Alarm!
	public const  int Alarm_2019302 = 2019302;
	/// <summary>閥4膠重判定異常!</summary>
	/// <remarks></remarks>
		//Valve4 Weight is Out of Range!
	public const  int Alarm_2019303 = 2019303;
	/// <summary>閥4膠量判定異常!</summary>
	/// <remarks></remarks>
		//Valve4 Volume is Out of Range!
	public const  int Alarm_2019304 = 2019304;
	/// <summary>閥4自動校正失敗!</summary>
	/// <remarks></remarks>
		//Valve4 Auto Calibration Failed!
	public const  int Alarm_2019305 = 2019305;
	/// <summary>閥4自動測高失敗!</summary>
	/// <remarks></remarks>
		//Valve4 Auto Find Height Failed!
	public const  int Alarm_2019306 = 2019306;
	/// <summary>閥4膠閥清潔失敗!</summary>
	/// <remarks></remarks>
		//Valve4 Clear Failed!
	public const  int Alarm_2019307 = 2019307;
	/// <summary>閥4閥座溫度判定異常!</summary>
	/// <remarks></remarks>
		//Valve4 ValveSet Temperature is Out of Range
	public const  int Alarm_2019308 = 2019308;
	/// <summary>閥4閥體溫度判定異常!</summary>
	/// <remarks></remarks>
		//Valve4 ValveBody Temperature is Out of Range
	public const  int Alarm_2019309 = 2019309;
	/// <summary>閥4膠管溫度判定異常!</summary>
	/// <remarks></remarks>
		//Valve4 Syringe Temperature is Out of Range
	public const  int Alarm_2019310 = 2019310;
	/// <summary>閥4換膠動作異常!</summary>
	/// <remarks></remarks>
		//Valve4 Change Action Failed!
	public const  int Alarm_2019311 = 2019311;
	/// <summary>閥4Purge動作異常!</summary>
	/// <remarks></remarks>
		//Valve4 Purge Action Failed!
	public const  int Alarm_2019312 = 2019312;
	/// <summary>閥4秤重動作異常!</summary>
	/// <remarks></remarks>
		//Valve4 Weighing Action Failed!
	public const  int Alarm_2019313 = 2019313;
	/// <summary>閥4Purge參數讀取失敗!</summary>
	/// <remarks></remarks>
		//Valve4 Purge Parameter Reading Failed!
	public const  int Alarm_2019314 = 2019314;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2019315 = 2019315;
	/// <summary>螺桿閥1過電流警報!</summary>
	/// <remarks></remarks>
		//Auger Valve1 CT Alarm!
	public const  int Alarm_2020000 = 2020000;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2020001 = 2020001;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2020002 = 2020002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2020003 = 2020003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2020004 = 2020004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2020005 = 2020005;
	/// <summary>螺桿閥2過電流警報!</summary>
	/// <remarks></remarks>
		//Auger Valve2 CT Alarm!
	public const  int Alarm_2020100 = 2020100;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2020101 = 2020101;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2020102 = 2020102;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2020103 = 2020103;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2020104 = 2020104;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2020105 = 2020105;
	/// <summary>螺桿閥3過電流警報!</summary>
	/// <remarks></remarks>
		//Auger Valve3 CT Alarm!
	public const  int Alarm_2020200 = 2020200;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2020201 = 2020201;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2020202 = 2020202;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2020203 = 2020203;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2020204 = 2020204;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2020205 = 2020205;
	/// <summary>螺桿閥4過電流警報!</summary>
	/// <remarks></remarks>
		//Auger Valve4 CT Alarm!
	public const  int Alarm_2020300 = 2020300;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2020301 = 2020301;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2020302 = 2020302;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2020303 = 2020303;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2020304 = 2020304;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2020305 = 2020305;
	/// <summary>閥控制器讀取資料失敗</summary>
	/// <remarks></remarks>
		//Valve Controller1 Read Data Failed!
	public const  int Alarm_2024000 = 2024000;
	/// <summary>"閥控制器寫資料失敗"</summary>
	/// <remarks></remarks>
		//Valve Controller1 Write Data Failed!
	public const  int Alarm_2024001 = 2024001;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2024002 = 2024002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2024003 = 2024003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2024004 = 2024004;
	/// <summary>平台1CCD定位動作異常</summary>
	/// <remarks></remarks>
		//Stage1 CCD Alignment Failed!
	public const  int Alarm_2036000 = 2036000;
	/// <summary>平台1雷射測高動作異常</summary>
	/// <remarks></remarks>
		//Stage1 Z-Measurement Failed!
	public const  int Alarm_2036001 = 2036001;
	/// <summary>平台1CCD檢測動作異常</summary>
	/// <remarks></remarks>
		//Stage1 CCD Inspection Failed!
	public const  int Alarm_2036002 = 2036002;
	/// <summary>平台1同步換膠動作異常</summary>
	/// <remarks></remarks>
		//Stage1 Synchronized Exchange Failed!
	public const  int Alarm_2036003 = 2036003;
	/// <summary>平台1同步清膠動作異常</summary>
	/// <remarks></remarks>
		//Stage1 Synchronized Clean Failed!
	public const  int Alarm_2036004 = 2036004;
	/// <summary>平台1同步Purge動作異常</summary>
	/// <remarks></remarks>
		//Stage1 Synchronized Purge Failed!
	public const  int Alarm_2036005 = 2036005;
	/// <summary>平台1順序秤重動作異常</summary>
	/// <remarks></remarks>
		//Stage1 Sequential Weighing Failed!
	public const  int Alarm_2036006 = 2036006;
	/// <summary>平台1同步自動測高異常</summary>
	/// <remarks></remarks>
		//Stage1 Synchronized Auto Z-Measurement Failed!
	public const  int Alarm_2036007 = 2036007;
	/// <summary>平台1同步自動校正異常</summary>
	/// <remarks></remarks>
		//Stage1 Synchronized Auto Calibration Failed!
	public const  int Alarm_2036008 = 2036008;
	/// <summary>平台1復歸逾時!</summary>
	/// <remarks></remarks>
		//Stage1 Home Timeout!
	public const  int Alarm_2036009 = 2036009;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2036010 = 2036010;
	/// <summary>(Station 1)阻擋氣缸未下降，請檢查!!</summary>
	/// <remarks></remarks>
		//Station1 Stopper Down Failed!
	public const  int Alarm_2037000 = 2037000;
	/// <summary>(Station 1)阻擋氣缸未上升，請檢查!!</summary>
	/// <remarks></remarks>
		//Station1 Stopper Up Failed!
	public const  int Alarm_2037001 = 2037001;
	/// <summary>(Station 1)頂升氣缸未下降，請檢查!!</summary>
	/// <remarks></remarks>
		//Station1 Chuck Down Failed!
	public const  int Alarm_2037002 = 2037002;
	/// <summary>(Station 1)頂升氣缸未上升，請檢查!!</summary>
	/// <remarks></remarks>
		//Station1 Chuck Up Failed!
	public const  int Alarm_2037003 = 2037003;
	/// <summary>(Station 1)料盤感測器偵測到Tray盤，請拿走!!</summary>
	/// <remarks></remarks>
		//Station1 Tray on Conveyor! Remove It!
	public const  int Alarm_2037004 = 2037004;
	/// <summary>(Station 1)料盤感測器偵測Tray盤超時，請檢查!!</summary>
	/// <remarks></remarks>
		//Station1 Tray Sensor Timeout!
	public const  int Alarm_2037005 = 2037005;
	/// <summary>(Station 1)夾具真空未建立，請檢查!!</summary>
	/// <remarks></remarks>
		//Station1 Chuck Vacuum build Failed!
	public const  int Alarm_2037006 = 2037006;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2037007 = 2037007;
	/// <summary>(Station 2)阻擋氣缸未下降，請檢查!!</summary>
	/// <remarks></remarks>
		//Station2 Stopper Down Failed!
	public const  int Alarm_2037100 = 2037100;
	/// <summary>(Station 2)阻擋氣缸未上升，請檢查!!</summary>
	/// <remarks></remarks>
		//Station2 Stopper Up Failed!
	public const  int Alarm_2037101 = 2037101;
	/// <summary>(Station 2)頂升氣缸未下降，請檢查!!</summary>
	/// <remarks></remarks>
		//Station2 Chuck Down Failed!
	public const  int Alarm_2037102 = 2037102;
	/// <summary>(Station 2)頂升氣缸未上升，請檢查!!</summary>
	/// <remarks></remarks>
		//Station2 Chuck Up Failed!
	public const  int Alarm_2037103 = 2037103;
	/// <summary>(Station 2)料盤感測器偵測到Tray盤，請拿走!!</summary>
	/// <remarks></remarks>
		//Station2 Tray on Conveyor! Remove It!
	public const  int Alarm_2037104 = 2037104;
	/// <summary>(Station 2)料盤感測器偵測Tray盤超時，請檢查!!</summary>
	/// <remarks></remarks>
		//Station2 Tray Sensor Timeout!
	public const  int Alarm_2037105 = 2037105;
	/// <summary>(Station 2)夾具真空未建立，請檢查!!</summary>
	/// <remarks></remarks>
		//Station2 Chuck Vacuum build Failed!
	public const  int Alarm_2037106 = 2037106;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2037107 = 2037107;
	/// <summary>(Station 2)料盤氣缸夾持未關閉，請檢查!!</summary>
	/// <remarks></remarks>
		//Station2 Tray Clamp Close Failed!
	public const  int Alarm_2037108 = 2037108;
	/// <summary>(Station 2)料盤氣缸夾持未打開，請檢查!!</summary>
	/// <remarks></remarks>
		//Station2 Tray Clamp Open Failed!
	public const  int Alarm_2037109 = 2037109;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2037110 = 2037110;
	/// <summary>(Station 3)阻擋氣缸未下降，請檢查!!</summary>
	/// <remarks></remarks>
		//Station3 Stopper Down Failed!
	public const  int Alarm_2037200 = 2037200;
	/// <summary>(Station 3)阻擋氣缸未上升，請檢查!!</summary>
	/// <remarks></remarks>
		//Station3 Stopper Up Failed!
	public const  int Alarm_2037201 = 2037201;
	/// <summary>(Station 3)頂升氣缸未下降，請檢查!!</summary>
	/// <remarks></remarks>
		//Station3 Chuck Down Failed!
	public const  int Alarm_2037202 = 2037202;
	/// <summary>(Station 3)頂升氣缸未上升，請檢查!!</summary>
	/// <remarks></remarks>
		//Station3 Chuck Up Failed!
	public const  int Alarm_2037203 = 2037203;
	/// <summary>(Station 3)料盤感測器偵測到Tray盤，請拿走!!</summary>
	/// <remarks></remarks>
		//Station3 Tray on Conveyor! Remove It!
	public const  int Alarm_2037204 = 2037204;
	/// <summary>(Station 3)料盤感測器偵測Tray盤超時，請檢查!!</summary>
	/// <remarks></remarks>
		//Station3 Tray Sensor Timeout!
	public const  int Alarm_2037205 = 2037205;
	/// <summary>(Station 3)夾具真空未建立，請檢查!!</summary>
	/// <remarks></remarks>
		//Station3 Chuck Vacuum build Failed!
	public const  int Alarm_2037206 = 2037206;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2037207 = 2037207;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2037300 = 2037300;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2037301 = 2037301;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2037302 = 2037302;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2037303 = 2037303;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2037304 = 2037304;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2037305 = 2037305;
	/// <summary>平台2CCD定位動作異常</summary>
	/// <remarks></remarks>
		//Stage2 CCD Alignment Failed!
	public const  int Alarm_2048000 = 2048000;
	/// <summary>平台2雷射測高動作異常</summary>
	/// <remarks></remarks>
		//Stage2 Z-Measurement Failed!
	public const  int Alarm_2048001 = 2048001;
	/// <summary>平台2CCD檢測動作異常</summary>
	/// <remarks></remarks>
		//Stage2 CCD Inspection Failed!
	public const  int Alarm_2048002 = 2048002;
	/// <summary>平台2同步換膠動作異常</summary>
	/// <remarks></remarks>
		//Stage2 Synchronized Exchange Failed!
	public const  int Alarm_2048003 = 2048003;
	/// <summary>平台2同步清膠動作異常</summary>
	/// <remarks></remarks>
		//Stage2 Synchronized Clean Failed!
	public const  int Alarm_2048004 = 2048004;
	/// <summary>平台2同步Purge動作異常</summary>
	/// <remarks></remarks>
		//Stage2 Synchronized Purge Failed!
	public const  int Alarm_2048005 = 2048005;
	/// <summary>平台2順序秤重動作異常</summary>
	/// <remarks></remarks>
		//Stage2 Sequential Weighing Failed!
	public const  int Alarm_2048006 = 2048006;
	/// <summary>平台2同步自動測高異常</summary>
	/// <remarks></remarks>
		//Stage2 Synchronized Auto Z-Measurement Failed!
	public const  int Alarm_2048007 = 2048007;
	/// <summary>平台2同步自動校正異常</summary>
	/// <remarks></remarks>
		//Stage2 Synchronized Auto Calibration Failed!
	public const  int Alarm_2048008 = 2048008;
	/// <summary>平台2復歸逾時!</summary>
	/// <remarks></remarks>
		//Stage2 Home Timeout!
	public const  int Alarm_2048009 = 2048009;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2048010 = 2048010;
	/// <summary>進料端通訊逾時!</summary>
	/// <remarks></remarks>
		//Loader Communication Timeout!
	public const  int Alarm_2054000 = 2054000;
	/// <summary>退料端通訊逾時!</summary>
	/// <remarks></remarks>
		//Unloader Communication Timeout!
	public const  int Alarm_2054001 = 2054001;
	/// <summary>進料端溫控設定失敗</summary>
	/// <remarks></remarks>
		//The Loader Temperature is setting failed!!!
	public const  int Alarm_2054002 = 2054002;
	/// <summary>退料端溫控設定失敗</summary>
	/// <remarks></remarks>
		//The Unloader Temperature is setting failed!!!
	public const  int Alarm_2054003 = 2054003;
	/// <summary>進料端溫控設定為0</summary>
	/// <remarks></remarks>
		//The Loader Temperature is setting 0!!!
	public const  int Alarm_2054004 = 2054004;
	/// <summary>退料端溫控設定為0</summary>
	/// <remarks></remarks>
		//The Unloader Temperature is setting 0!!!
	public const  int Alarm_2054005 = 2054005;
	/// <summary>Loader PLC Timeout : Get Machine Status.</summary>
	/// <remarks></remarks>
		//Loader PLC Timeout : Get Machine Status. 
	public const  int Alarm_2054006 = 2054006;
	/// <summary>Loader PLC Timeout : Get Pass Model.</summary>
	/// <remarks></remarks>
		// Loader PLC Timeout : Get Pass Model.
	public const  int Alarm_2054007 = 2054007;
	/// <summary>Loader PLC Timeout : Set Pass Model.</summary>
	/// <remarks></remarks>
		// Loader PLC Timeout : Set Pass Model.
	public const  int Alarm_2054008 = 2054008;
	/// <summary>Loader PLC Timeout : Get Alarm Code.</summary>
	/// <remarks></remarks>
		// Loader PLC Timeout : Get Alarm Code.
	public const  int Alarm_2054009 = 2054009;
	/// <summary>Loader PLC Timeout : Set Product Type.</summary>
	/// <remarks></remarks>
		// Loader PLC Timeout : Set Product Type.
	public const  int Alarm_2054010 = 2054010;
	/// <summary>Loader PLC Timeout : Get Product Type.</summary>
	/// <remarks></remarks>
		// Loader PLC Timeout : Get Product Type.
	public const  int Alarm_2054011 = 2054011;
	/// <summary>Loader PLC Timeout : Get Hot Plate Temperatures.</summary>
	/// <remarks></remarks>
		// Loader PLC Timeout : Get Hot Plate Temperatures.
	public const  int Alarm_2054012 = 2054012;
	/// <summary>Loader PLC Timeout : Get Target Temperature.</summary>
	/// <remarks></remarks>
		// Loader PLC Timeout : Get Target Temperature.
	public const  int Alarm_2054013 = 2054013;
	/// <summary>Loader PLC Timeout : Set Target Temperature.</summary>
	/// <remarks></remarks>
		// Loader PLC Timeout : Set Target Temperature.
	public const  int Alarm_2054014 = 2054014;
	/// <summary>Loader PLC Timeout : Get Product Number.</summary>
	/// <remarks></remarks>
		// Loader PLC Timeout : Get Product Number.
	public const  int Alarm_2054015 = 2054015;
	/// <summary>Loader PLC Timeout : Set Product Number.</summary>
	/// <remarks></remarks>
		// Loader PLC Timeout : Set Product Number.
	public const  int Alarm_2054016 = 2054016;
	/// <summary>Loader PLC Timeout : Get Cassette Data.</summary>
	/// <remarks></remarks>
		// Loader PLC Timeout : Get Cassette Data.
	public const  int Alarm_2054017 = 2054017;
	/// <summary>Loader PLC Timeout : Get Cassette Barcode.</summary>
	/// <remarks></remarks>
		// Loader PLC Timeout : Get Cassette Barcode.
	public const  int Alarm_2054018 = 2054018;
	/// <summary>Loader PLC Timeout : Set Cassette Barcode.</summary>
	/// <remarks></remarks>
		// Loader PLC Timeout : Set Cassette Barcode.
	public const  int Alarm_2054019 = 2054019;
	/// <summary>Loader PLC Timeout : Cassette Abort.</summary>
	/// <remarks></remarks>
		// Loader PLC Timeout : Cassette Abort.
	public const  int Alarm_2054020 = 2054020;
	/// <summary>Loader PLC ExMessage</summary>
	/// <remarks></remarks>
		// Loader PLC ExMessage
	public const  int Alarm_2054021 = 2054021;
	/// <summary>Unloader PLC Timeout : Get Machine Status.</summary>
	/// <remarks></remarks>
		// Unloader PLC Timeout : Get Machine Status.
	public const  int Alarm_2054022 = 2054022;
	/// <summary>Unloader PLC Timeout : Get Pass Model.</summary>
	/// <remarks></remarks>
		// Unloader PLC Timeout : Get Pass Model.
	public const  int Alarm_2054023 = 2054023;
	/// <summary>Unloader PLC Timeout : Set Pass Model.</summary>
	/// <remarks></remarks>
		// Unloader PLC Timeout : Set Pass Model.
	public const  int Alarm_2054024 = 2054024;
	/// <summary>Unloader PLC Timeout : Get Alarm Code.</summary>
	/// <remarks></remarks>
		// Unloader PLC Timeout : Get Alarm Code.
	public const  int Alarm_2054025 = 2054025;
	/// <summary>Unloader PLC Timeout : Set Product Type.</summary>
	/// <remarks></remarks>
		// Unloader PLC Timeout : Set Product Type.
	public const  int Alarm_2054026 = 2054026;
	/// <summary>Unloader PLC Timeout : Get Product Type.</summary>
	/// <remarks></remarks>
		// Unloader PLC Timeout : Get Product Type.
	public const  int Alarm_2054027 = 2054027;
	/// <summary>Unloader PLC Timeout : Get Hot Plate Temperatures.</summary>
	/// <remarks></remarks>
		// Unloader PLC Timeout : Get Hot Plate Temperatures.
	public const  int Alarm_2054028 = 2054028;
	/// <summary>Unloader PLC Timeout : Set Target Temperature.</summary>
	/// <remarks></remarks>
		// Unloader PLC Timeout : Set Target Temperature.
	public const  int Alarm_2054029 = 2054029;
	/// <summary>Unloader PLC Timeout : Get Target Temperature.</summary>
	/// <remarks></remarks>
		// Unloader PLC Timeout : Get Target Temperature.
	public const  int Alarm_2054030 = 2054030;
	/// <summary>Unloader PLC Timeout : Get Product Number.</summary>
	/// <remarks></remarks>
		//Unloader PLC Timeout : Get Product Number.
	public const  int Alarm_2054031 = 2054031;
	/// <summary>Unloader PLC Timeout : Set Product Number.</summary>
	/// <remarks></remarks>
		// Unloader PLC Timeout : Set Product Number.
	public const  int Alarm_2054032 = 2054032;
	/// <summary>Unloader PLC Timeout : Get Cassette Data.</summary>
	/// <remarks></remarks>
		//Unloader PLC Timeout : Get Cassette Data.
	public const  int Alarm_2054033 = 2054033;
	/// <summary>Unloader PLC Timeout : Cassette Abort.</summary>
	/// <remarks></remarks>
		// Unloader PLC Timeout : Cassette Abort.
	public const  int Alarm_2054034 = 2054034;
	/// <summary>Unloader PLC Timeout : Set Last Product Number.</summary>
	/// <remarks></remarks>
		// Unloader PLC Timeout : Set Last Product Number.
	public const  int Alarm_2054035 = 2054035;
	/// <summary>Unloader PLC ExMessage</summary>
	/// <remarks></remarks>
		// Unloader PLC ExMessage
	public const  int Alarm_2054036 = 2054036;
	/// <summary>Loader PLC連結失敗</summary>
	/// <remarks></remarks>
		// Loader PLC connect fail.
	public const  int Alarm_2054037 = 2054037;
	/// <summary>Unloader PLC連結失敗</summary>
	/// <remarks></remarks>
		// Unloader PLC connect fail.
	public const  int Alarm_2054038 = 2054038;
	/// <summary>平台3CCD定位動作異常</summary>
	/// <remarks></remarks>
		//Stage3 CCD Alignment Failed!
	public const  int Alarm_2066000 = 2066000;
	/// <summary>平台3雷射測高動作異常</summary>
	/// <remarks></remarks>
		//Stage3 Z-Measurement Failed!
	public const  int Alarm_2066001 = 2066001;
	/// <summary>平台3CCD檢測動作異常</summary>
	/// <remarks></remarks>
		//Stage3 CCD Inspection Failed!
	public const  int Alarm_2066002 = 2066002;
	/// <summary>平台3同步換膠動作異常</summary>
	/// <remarks></remarks>
		//Stage3 Synchronized Exchange Failed!
	public const  int Alarm_2066003 = 2066003;
	/// <summary>平台3同步清膠動作異常</summary>
	/// <remarks></remarks>
		//Stage3 Synchronized Clean Failed!
	public const  int Alarm_2066004 = 2066004;
	/// <summary>平台3同步Purge動作異常</summary>
	/// <remarks></remarks>
		//Stage3 Synchronized Purge Failed!
	public const  int Alarm_2066005 = 2066005;
	/// <summary>平台3順序秤重動作異常</summary>
	/// <remarks></remarks>
		//Stage3 Sequential Weighing Failed!
	public const  int Alarm_2066006 = 2066006;
	/// <summary>平台3同步自動測高異常</summary>
	/// <remarks></remarks>
		//Stage3 Synchronized Auto Z-Measurement Failed!
	public const  int Alarm_2066007 = 2066007;
	/// <summary>平台3同步自動校正異常</summary>
	/// <remarks></remarks>
		//Stage3 Synchronized Auto Calibration Failed!
	public const  int Alarm_2066008 = 2066008;
	/// <summary>平台3復歸逾時!</summary>
	/// <remarks></remarks>
		//Stage3 Home Timeout!
	public const  int Alarm_2066009 = 2066009;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2066010 = 2066010;
	/// <summary>平台4CCD定位動作異常</summary>
	/// <remarks></remarks>
		//Stage4 CCD Alignment Failed!
	public const  int Alarm_2073000 = 2073000;
	/// <summary>平台4雷射測高動作異常</summary>
	/// <remarks></remarks>
		//Stage4 Z-Measurement Failed!
	public const  int Alarm_2073001 = 2073001;
	/// <summary>平台4CCD檢測動作異常</summary>
	/// <remarks></remarks>
		//Stage4 CCD Inspection Failed!
	public const  int Alarm_2073002 = 2073002;
	/// <summary>平台4同步換膠動作異常</summary>
	/// <remarks></remarks>
		//Stage4 Synchronized Exchange Failed!
	public const  int Alarm_2073003 = 2073003;
	/// <summary>平台4同步清膠動作異常</summary>
	/// <remarks></remarks>
		//Stage4 Synchronized Clean Failed!
	public const  int Alarm_2073004 = 2073004;
	/// <summary>平台4同步Purge動作異常</summary>
	/// <remarks></remarks>
		//Stage4 Synchronized Purge Failed!
	public const  int Alarm_2073005 = 2073005;
	/// <summary>平台4順序秤重動作異常</summary>
	/// <remarks></remarks>
		//Stage4 Sequential Weighing Failed!
	public const  int Alarm_2073006 = 2073006;
	/// <summary>平台4同步自動測高異常</summary>
	/// <remarks></remarks>
		//Stage4 Synchronized Auto Z-Measurement Failed!
	public const  int Alarm_2073007 = 2073007;
	/// <summary>平台4同步自動校正異常</summary>
	/// <remarks></remarks>
		//Stage4 Synchronized Auto Calibration Failed!
	public const  int Alarm_2073008 = 2073008;
	/// <summary>平台4復歸逾時!</summary>
	/// <remarks></remarks>
		//Stage4 Home Timeout!
	public const  int Alarm_2073009 = 2073009;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2073010 = 2073010;
	/// <summary>整機狀態異常</summary>
	/// <remarks></remarks>
		//Over-all Status Alarm!
	public const  int Alarm_2080000 = 2080000;
	/// <summary>整機自動生產動作異常</summary>
	/// <remarks></remarks>
		//Over-all Auto Run Failed!
	public const  int Alarm_2080001 = 2080001;
	/// <summary>整機復歸動作異常</summary>
	/// <remarks></remarks>
		//Over-all Home Failed!
	public const  int Alarm_2080002 = 2080002;
	/// <summary>整機EMO停機</summary>
	/// <remarks></remarks>
		//Over-all EMO Stop!
	public const  int Alarm_2080003 = 2080003;
	/// <summary>Recipe與MappingData種類不同</summary>
	/// <remarks></remarks>
		//Recipe and MappingData Type is Not Matched!
	public const  int Alarm_2080004 = 2080004;
	/// <summary>Recipe與MappingData大小不同</summary>
	/// <remarks></remarks>
		//Recipe and MappingData Size is Not Matched!
	public const  int Alarm_2080005 = 2080005;
	/// <summary>X軸移動失敗</summary>
	/// <remarks></remarks>
		//Axis X Move Error
	public const  int Alarm_2080006 = 2080006;
	/// <summary>Y軸移動失敗</summary>
	/// <remarks></remarks>
		// Axis Y Move Error
	public const  int Alarm_2080007 = 2080007;
	/// <summary>Z軸移動失敗</summary>
	/// <remarks></remarks>
		//Axis Z Move Error
	public const  int Alarm_2080008 = 2080008;
	/// <summary>Tilt軸移動失敗</summary>
	/// <remarks></remarks>
		//Axis Tilt Move Error
	public const  int Alarm_2080009 = 2080009;
	/// <summary>X.Y軸移動逾時</summary>
	/// <remarks></remarks>
		//Axis X,Y Wait Inposition Time Out!!!
	public const  int Alarm_2080010 = 2080010;
	/// <summary>SMEMA 載入準備逾時</summary>
	/// <remarks></remarks>
		//SMEMA Loader Ready Timeout.
	public const  int Alarm_2080011 = 2080011;
	/// <summary>SMEMA 載入預備關閉逾時</summary>
	/// <remarks></remarks>
		//SMEMA Loader Ready OFF Timeout.
	public const  int Alarm_2080012 = 2080012;
	/// <summary>SMEMA 載出預備逾時</summary>
	/// <remarks></remarks>
		//SMEMA Unloader Ready Timeout.
	public const  int Alarm_2080013 = 2080013;
	/// <summary>A/B機氣壓缸復歸逾時</summary>
	/// <remarks></remarks>
		//Machine A/B Electric Cylinder 'Home' Timeout.
	public const  int Alarm_2080014 = 2080014;
	/// <summary>A/B機阻擋板下降逾時</summary>
	/// <remarks></remarks>
		//Machine A/B Stoper 'Down' Timeout.
	public const  int Alarm_2080015 = 2080015;
	/// <summary>SMEMA 載入預備逾時或感測器不在安全位置</summary>
	/// <remarks></remarks>
		//SMEMA Loader Ready Timeout Or Sensor Not Safe.
	public const  int Alarm_2080016 = 2080016;
	/// <summary>A/B機氣壓缸復歸逾時或A機阻擋板下降逾時</summary>
	/// <remarks></remarks>
		//Machine A/B Electric Cylinder 'Home' Timeout or Machine A Stoper 'Down' Timeout.
	public const  int Alarm_2080017 = 2080017;
	/// <summary>SMEMA 載出預備逾時或感測器不在安全位置</summary>
	/// <remarks></remarks>
		//SMEMA Unloader Ready Timeout Or Sensor Not Safe.
	public const  int Alarm_2080018 = 2080018;
	/// <summary>SMEMA 載出預備關閉逾時</summary>
	/// <remarks></remarks>
		//SMEMA Unloader Ready OFF Timeout.
	public const  int Alarm_2080019 = 2080019;
	/// <summary>工作區確認狀態錯誤</summary>
	/// <remarks></remarks>
		//Work-Station Check State is False!!!
	public const  int Alarm_2080020 = 2080020;
	/// <summary>工作區確認狀態正確</summary>
	/// <remarks></remarks>
		//Work-Station CheckState is True!!!
	public const  int Alarm_2080021 = 2080021;
	/// <summary>沒有Tilt軸</summary>
	/// <remarks></remarks>
		//None Tilt Axis
	public const  int Alarm_2080022 = 2080022;
	/// <summary>請先停止 Auto Run</summary>
	/// <remarks></remarks>
		//Stop Auto Run, Please.
	public const  int Alarm_2080023 = 2080023;
	/// <summary>Tilt 位置錯誤!</summary>
	/// <remarks></remarks>
		//Tilt Pos Error!
	public const  int Alarm_2080024 = 2080024;
	/// <summary>A機狀態異常</summary>
	/// <remarks></remarks>
		//Machine-A Status Alarm!
	public const  int Alarm_2081000 = 2081000;
	/// <summary>A機自動生產動作異常</summary>
	/// <remarks></remarks>
		//Machine-A Auto Run Failed!
	public const  int Alarm_2081001 = 2081001;
	/// <summary>A機復歸動作異常</summary>
	/// <remarks></remarks>
		//Machine-A Home Failed!
	public const  int Alarm_2081002 = 2081002;
	/// <summary>A機進料異常</summary>
	/// <remarks></remarks>
		//Machine-A Load Failed!
	public const  int Alarm_2081003 = 2081003;
	/// <summary>A機退料異常</summary>
	/// <remarks></remarks>
		//Machine-A Unload Failed!
	public const  int Alarm_2081004 = 2081004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2081005 = 2081005;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2081006 = 2081006;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2081007 = 2081007;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2081008 = 2081008;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2081009 = 2081009;
	/// <summary>A機EMS停機</summary>
	/// <remarks></remarks>
		//Machine-A EMS Stop!
	public const  int Alarm_2081100 = 2081100;
	/// <summary>A機CDA異常</summary>
	/// <remarks></remarks>
		//Machine-A CDA Alarm!
	public const  int Alarm_2081101 = 2081101;
	/// <summary>A機門鎖未關</summary>
	/// <remarks></remarks>
		//Machine-A Door ajar!
	public const  int Alarm_2081102 = 2081102;
	/// <summary>A機馬達電力異常</summary>
	/// <remarks></remarks>
		//Machine-A Motor Power Alarm!
	public const  int Alarm_2081103 = 2081103;
	/// <summary>A機溫控電力異常</summary>
	/// <remarks></remarks>
		//Machine-A Temp Power Alarm!
	public const  int Alarm_2081104 = 2081104;
	/// <summary>A機門鎖未鎖</summary>
	/// <remarks></remarks>
		//Machine-A Door Interlock!
	public const  int Alarm_2081105 = 2081105;
	/// <summary>ERROR A : DataOutputAseMap</summary>
	/// <remarks></remarks>
		//ERROR A : DataOutputAseMap
	public const  int Alarm_2081106 = 2081106;
	/// <summary>ERROR A : CoverMapData</summary>
	/// <remarks></remarks>
		//ERROR A : CoverMapData
	public const  int Alarm_2081107 = 2081107;
	/// <summary>B機狀態異常</summary>
	/// <remarks></remarks>
		//Machine-B Status Alarm!
	public const  int Alarm_2082000 = 2082000;
	/// <summary>B機自動生產動作異常</summary>
	/// <remarks></remarks>
		//Machine-B Auto Run Failed!
	public const  int Alarm_2082001 = 2082001;
	/// <summary>B機復歸動作異常</summary>
	/// <remarks></remarks>
		//Machine-B Home Failed!
	public const  int Alarm_2082002 = 2082002;
	/// <summary>B機進料異常</summary>
	/// <remarks></remarks>
		//Machine-B Load Failed!
	public const  int Alarm_2082003 = 2082003;
	/// <summary>B機退料異常</summary>
	/// <remarks></remarks>
		//Machine-B Unload Failed!
	public const  int Alarm_2082004 = 2082004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Alarm_2082005 = 2082005;
	/// <summary>B機EMS停機</summary>
	/// <remarks></remarks>
		//Machine-B EMS Stop!
	public const  int Alarm_2082100 = 2082100;
	/// <summary>B機CDA異常</summary>
	/// <remarks></remarks>
		//Machine-B CDA Alarm!
	public const  int Alarm_2082101 = 2082101;
	/// <summary>B機門鎖未關</summary>
	/// <remarks></remarks>
		//Machine-B Door ajar!
	public const  int Alarm_2082102 = 2082102;
	/// <summary>B機馬達電力異常</summary>
	/// <remarks></remarks>
		//Machine-B Motor Power Alarm!
	public const  int Alarm_2082103 = 2082103;
	/// <summary>B機溫控電力異常</summary>
	/// <remarks></remarks>
		//Machine-B Temp Power Alarm!
	public const  int Alarm_2082104 = 2082104;
	/// <summary>B機門鎖未鎖</summary>
	/// <remarks></remarks>
		//Machine-B Door Interlock!
	public const  int Alarm_2082105 = 2082105;
	/// <summary>ERROR B : DataOutputAseMap</summary>
	/// <remarks></remarks>
		//ERROR B : DataOutputAseMap
	public const  int Alarm_2082106 = 2082106;
	/// <summary>ERROR B : CoverMapData</summary>
	/// <remarks></remarks>
		//ERROR B : CoverMapData
	public const  int Alarm_2082107 = 2082107;
	/// <summary>Conveyor1狀態異常</summary>
	/// <remarks></remarks>
		//Conveyor1 Status Alarm!
	public const  int Alarm_2083000 = 2083000;
	/// <summary>Conveyor1自動生產動作異常</summary>
	/// <remarks></remarks>
		//Conveyor1 Auto Run Failed!
	public const  int Alarm_2083001 = 2083001;
	/// <summary>Conveyor1復歸動作異常</summary>
	/// <remarks></remarks>
		//Conveyor1 Home Failed!
	public const  int Alarm_2083002 = 2083002;
	/// <summary>Conveyor1 A機進料異常</summary>
	/// <remarks></remarks>
		//Conveyor1 Load to Machine-A Failed!
	public const  int Alarm_2083003 = 2083003;
	/// <summary>Conveyor1 A機退料異常</summary>
	/// <remarks></remarks>
		//Conveyor1 Unload from Machine-A Failed!
	public const  int Alarm_2083004 = 2083004;
	/// <summary>Conveyor1 B機進料異常</summary>
	/// <remarks></remarks>
		//Conveyor1 Load to Machine-B Failed!
	public const  int Alarm_2083005 = 2083005;
	/// <summary>Conveyor1 B機退料異常</summary>
	/// <remarks></remarks>
		//Conveyor1 Unload from Machine-B Failed!
	public const  int Alarm_2083006 = 2083006;
	/// <summary>Conveyor1 進料異常</summary>
	/// <remarks></remarks>
		//Conveyor1 Load Failed!
	public const  int Alarm_2083007 = 2083007;
	/// <summary>Conveyor1 退料異常</summary>
	/// <remarks></remarks>
		//Conveyor1 Unload Failed!
	public const  int Alarm_2083008 = 2083008;
	/// <summary>Conveyor1 EMS停機</summary>
	/// <remarks></remarks>
		//Conveyor1 EMS Stop!
	public const  int Alarm_2083100 = 2083100;
	/// <summary>Conveyor1 CDA異常</summary>
	/// <remarks></remarks>
		//Conveyor1 CDA Alarm!
	public const  int Alarm_2083101 = 2083101;
	/// <summary>Conveyor1 門鎖未關</summary>
	/// <remarks></remarks>
		//Conveyor1 Door ajar!
	public const  int Alarm_2083102 = 2083102;
	/// <summary>Conveyor1 馬達電力異常</summary>
	/// <remarks></remarks>
		//Conveyor1 Motor Power Alarm!
	public const  int Alarm_2083103 = 2083103;
	/// <summary>Conveyor1 溫控電力異常</summary>
	/// <remarks></remarks>
		//Conveyor1 Temp Power Alarm!
	public const  int Alarm_2083104 = 2083104;
	/// <summary>Conveyor1 馬達異常</summary>
	/// <remarks></remarks>
		//Conveyor1 Motor Alarm!
	public const  int Alarm_2083105 = 2083105;
	/// <summary>Conveyor1 未清空!</summary>
	/// <remarks></remarks>
		//Conveyor1 Is Not Empty
	public const  int Alarm_2083106 = 2083106;
	/// <summary>Conveyor1 設定速度異常!</summary>
	/// <remarks></remarks>
		//Conveyor1 Is Set Speed Error.
	public const  int Alarm_2083107 = 2083107;
	/// <summary>Conveyor1 真空逾時</summary>
	/// <remarks></remarks>
		//Conveyor1 Is Set Speed Error
	public const  int Alarm_2083108 = 2083108;
	/// <summary>A機阻擋缸上升逾時/前段阻擋缸上昇逾時/氣壓缸復歸逾時</summary>
	/// <remarks></remarks>
		//Machine A Stoper 'Up' Timeout Or Front Stoper 'Up' Timeout Or Electric Cylinder 'Home' Timeout.
	public const  int Alarm_2083109 = 2083109;
	/// <summary>A機上有產品</summary>
	/// <remarks></remarks>
		//There Are Products On Machine A
	public const  int Alarm_2083110 = 2083110;
	/// <summary>A機氣壓缸復歸逾時</summary>
	/// <remarks></remarks>
		//TMachine A Electric Cylinder 'Home' Timeout.
	public const  int Alarm_2083111 = 2083111;
	/// <summary>A機阻擋缸上升逾時</summary>
	/// <remarks></remarks>
		//Machine A Stoper 'Up' Timeout.
	public const  int Alarm_2083112 = 2083112;
	/// <summary>A機前段阻擋缸下降逾時</summary>
	/// <remarks></remarks>
		//Machine A Front Stoper 'Down' Timeout.
	public const  int Alarm_2083113 = 2083113;
	/// <summary>A機入口感測器逾時</summary>
	/// <remarks></remarks>
		//Machine A Entrance Sensor Timeout.
	public const  int Alarm_2083114 = 2083114;
	/// <summary>A機速度變換逾時</summary>
	/// <remarks></remarks>
		//Machine A Speed Change Timeout.
	public const  int Alarm_2083115 = 2083115;
	/// <summary>A機阻擋缸感測器逾時</summary>
	/// <remarks></remarks>
		//Machine A Stoper Sensor Timeout.
	public const  int Alarm_2083116 = 2083116;
	/// <summary>A機前段阻擋缸上降逾時</summary>
	/// <remarks></remarks>
		//Machine A Front Stoper 'Up' Timeout.
	public const  int Alarm_2083117 = 2083117;
	/// <summary>A機氣壓缸上升逾時</summary>
	/// <remarks></remarks>
		//Machine A Electric Cylinder 'Up' Timeout.
	public const  int Alarm_2083118 = 2083118;
	/// <summary>A機真空開啟逾時</summary>
	/// <remarks></remarks>
		//Machine A Vacuum ON Timeout.
	public const  int Alarm_2083119 = 2083119;
	/// <summary>A機真空關閉逾時</summary>
	/// <remarks></remarks>
		//Machine A Vacuum OFF Timeout.
	public const  int Alarm_2083120 = 2083120;
	/// <summary>A機離開感測器逾時</summary>
	/// <remarks></remarks>
		//Machine A Exit Sensor Timeout.
	public const  int Alarm_2083121 = 2083121;
	/// <summary>Conveyor1 安全狀態確認逾時</summary>
	/// <remarks></remarks>
		//Conveyor1 Check Safe State Timeout.
	public const  int Alarm_2083122 = 2083122;

	/// <summary>Conveyor1 到位感測器觸發逾時</summary>
	/// <remarks></remarks>
		//Conveyor1 Inposition Sensor Trigger Timeout.
	public const  int Alarm_2083123 = 2083123;
	/// <summary>Conveyor1 料盤逼緊缸夾關閉逾時</summary>
	/// <remarks></remarks>
		//Conveyor1 Tray Clamp Close Timeout.
	public const  int Alarm_2083124 = 2083124;
	/// <summary>Conveyor1 料盤逼緊缸夾開啟逾時</summary>
	/// <remarks></remarks>
		//Conveyor1 Tray Clamp Open Timeout.
	public const  int Alarm_2083125 = 2083125;
	/// <summary>Conveyor1 沒有料片在機台上</summary>
	/// <remarks></remarks>
		//Conveyor1 No Products On Top.
	public const  int Alarm_2083126 = 2083126;
	/// <summary>Conveyor1 阻擋缸下降逾時</summary>
	/// <remarks></remarks>
		//Conveyor1 Stoper  Down Timeout.
	public const  int Alarm_2083127 = 2083127;
	/// <summary>Conveyor1 定位資料錯誤</summary>
	/// <remarks></remarks>
		//Conveyor1Alignment Data Error!
	public const  int Alarm_2083128 = 2083128;
	/// <summary>Conveyor1 SkipMark資料不存在</summary>
	/// <remarks></remarks>
		//Conveyor1 SkipMark Data Not Exists!
	public const  int Alarm_2083129 = 2083129;
	/// <summary>A機上/下站異常</summary>
	/// <remarks></remarks>
		//Machine-A Conveyor Prev/Next Alarm!
	public const  int Alarm_2083130 = 2083130;
	/// <summary>Conveyor1料盤逼緊缸預備逾時</summary>
	/// <remarks></remarks>
		//Conveyor1 Tray Clamp OnReady Timeout.
	public const  int Alarm_2083131 = 2083131;
	/// <summary>Conveyor2狀態異常</summary>
	/// <remarks></remarks>
		//Conveyor2 Status Alarm!
	public const  int Alarm_2084000 = 2084000;
	/// <summary>Conveyor2自動生產動作異常</summary>
	/// <remarks></remarks>
		//Conveyor2 Auto Run Failed!
	public const  int Alarm_2084001 = 2084001;
	/// <summary>Conveyor2復歸動作異常</summary>
	/// <remarks></remarks>
		//Conveyor2 Home Failed!
	public const  int Alarm_2084002 = 2084002;
	/// <summary>Conveyor2 A機進料異常</summary>
	/// <remarks></remarks>
		//Conveyor2 Load to Machine-A Failed!
	public const  int Alarm_2084003 = 2084003;
	/// <summary>Conveyor2 A機退料異常</summary>
	/// <remarks></remarks>
		//Conveyor2 Unload from Machine-A Failed!
	public const  int Alarm_2084004 = 2084004;
	/// <summary>Conveyor2 B機進料異常</summary>
	/// <remarks></remarks>
		//Conveyor2 Load to Machine-B Failed!
	public const  int Alarm_2084005 = 2084005;
	/// <summary>Conveyor2 B機退料異常</summary>
	/// <remarks></remarks>
		//Conveyor2 Unload from Machine-B Failed!
	public const  int Alarm_2084006 = 20843006;
	/// <summary>Conveyor2 進料異常</summary>
	/// <remarks></remarks>
		//Conveyor2 Load Failed!
	public const  int Alarm_2084007 = 2084007;
	/// <summary>Conveyor2 退料異常</summary>
	/// <remarks></remarks>
		//Conveyor2 Unload Failed!
	public const  int Alarm_2084008 = 2084008;
	/// <summary>Conveyor2 EMS停機</summary>
	/// <remarks></remarks>
		//Conveyor2 EMS Stop!
	public const  int Alarm_2084100 = 2084100;
	/// <summary>Conveyor2 CDA異常</summary>
	/// <remarks></remarks>
		//Conveyor2 CDA Alarm!
	public const  int Alarm_2084101 = 2084101;
	/// <summary>Conveyor2 門鎖未關</summary>
	/// <remarks></remarks>
		//Conveyor2 Door ajar!
	public const  int Alarm_2084102 = 2084102;
	/// <summary>Conveyor2 馬達電力異常</summary>
	/// <remarks></remarks>
		//Conveyor2 Motor Power Alarm!
	public const  int Alarm_2084103 = 2084103;
	/// <summary>Conveyor2 溫控電力異常</summary>
	/// <remarks></remarks>
		//Conveyor2 Temp Power Alarm!
	public const  int Alarm_2084104 = 2084104;
	/// <summary>Conveyor2 馬達異常</summary>
	/// <remarks></remarks>
		//Conveyor2 Motor Alarm!
	public const  int Alarm_2084105 = 2084105;
	/// <summary>Conveyor2 未清空!</summary>
	/// <remarks></remarks>
		//Conveyor2 Is Not Empty
	public const  int Alarm_2084106 = 2084106;
	/// <summary>Conveyor2 設定速度異常!</summary>
	/// <remarks></remarks>
		//Conveyor2 Is Set Speed Error.
	public const  int Alarm_2084107 = 2084107;
	/// <summary>Conveyor2 真空逾時</summary>
	/// <remarks></remarks>
		//Conveyor2 Vacuum Is TimeOut.
	public const  int Alarm_2084108 = 2084108;
	/// <summary>B機阻擋缸上升逾時/前段阻擋缸上昇逾時/氣壓缸復歸逾時</summary>
	/// <remarks></remarks>
		//MAchine B Stoper 'Up' Timeout Or Electric Cylinder 'Home' Timeout.
	public const  int Alarm_2084109 = 2084109;
	/// <summary>B機上有產品</summary>
	/// <remarks></remarks>
		//There Are Products On Machine B
	public const  int Alarm_2084110 = 2084110;
	/// <summary>B機氣壓缸復歸逾時</summary>
	/// <remarks></remarks>
		//Machine B Electric Cylinder 'Home' Timeout.
	public const  int Alarm_2084111 = 2084111;
	/// <summary>B機阻擋缸上升逾時</summary>
	/// <remarks></remarks>
		//Machine B Stoper 'Up' Timeout.
	public const  int Alarm_2084112 = 2084112;
	/// <summary>B機阻擋缸下降逾時</summary>
	/// <remarks></remarks>
		//Machine B Stoper 'Down' Timeout.
	public const  int Alarm_2084113 = 2084113;
	/// <summary>B機入口感測器逾時</summary>
	/// <remarks></remarks>
		//Machine B Entrance Sensor Timeout.
	public const  int Alarm_2084114 = 2084114;
	/// <summary>B機滾輪速度變換逾時</summary>
	/// <remarks></remarks>
		//Machine B Roller Change Speed Timeout.
	public const  int Alarm_2084115 = 2084115;
	/// <summary>B機阻擋缸感測器逾時</summary>
	/// <remarks></remarks>
		//Machine B Stoper Sensor Timeout.
	public const  int Alarm_2084116 = 2084116;
	/// <summary>B機前段阻擋缸上降逾時</summary>
	/// <remarks></remarks>
		//Machine B Front Stoper 'Up' Timeout.
	public const  int Alarm_2084117 = 2084117;
	/// <summary>B機氣壓缸上升逾時</summary>
	/// <remarks></remarks>
		//Machine B Electric Cylinder 'Up' Timeout.
	public const  int Alarm_2084118 = 2084118;
	/// <summary>B機真空開啟逾時</summary>
	/// <remarks></remarks>
		//Machine B Vacuum ON Timeout.
	public const  int Alarm_2084119 = 2084119;
	/// <summary>B機真空關閉逾時</summary>
	/// <remarks></remarks>
		//Machine B Vacuum OFF Timeout.
	public const  int Alarm_2084120 = 2084120;
	/// <summary>B機離開感測器逾時</summary>
	/// <remarks></remarks>
		//Machine B Exit Sensor Timeout.
	public const  int Alarm_2084121 = 2084121;
	/// <summary>Conveyor2  安全狀態確認逾時</summary>
	/// <remarks></remarks>
		//Conveyor2 Check Safe State Timeout.
	public const  int Alarm_2084122 = 2084122;
	/// <summary>Conveyor2  到位感測器觸發逾時</summary>
	/// <remarks></remarks>
		//Conveyor2  Inposition Sensor Trigger Timeout.
	public const  int Alarm_2084123 = 2084123;
	/// <summary>Conveyor2  料盤逼緊缸夾關閉逾時</summary>
	/// <remarks></remarks>
		//Conveyor2  Tray Clamp Close Timeout.
	public const  int Alarm_2084124 = 2084124;
	/// <summary>Conveyor2  料盤逼緊缸夾開啟逾時</summary>
	/// <remarks></remarks>
		//Conveyor2  Tray Clamp Open Timeout.
	public const  int Alarm_2084125 = 2084125;
	/// <summary>Conveyor2  沒有料片在機台上</summary>
	/// <remarks></remarks>
		//Conveyor2  No Products On Top.
	public const  int Alarm_2084126 = 2084126;
	/// <summary>Conveyor2  阻擋缸下降逾時</summary>
	/// <remarks></remarks>
		//Conveyor2  Stoper  Down Timeout.
	public const  int Alarm_2084127 = 2084127;
	/// <summary>Conveyor2  定位資料錯誤</summary>
	/// <remarks></remarks>
		//Conveyor2 Alignment Data Error!
	public const  int Alarm_2084128 = 2084128;
	/// <summary>Conveyor2  SkipMark資料不存在</summary>
	/// <remarks></remarks>
		//Conveyor2  SkipMark Data Not Exists!
	public const  int Alarm_2084129 = 2084129;
	/// <summary>B機上/下站異常</summary>
	/// <remarks></remarks>
		//Machine-B Conveyor Prev/Next Alarm!
	public const  int Alarm_2084130 = 2084130;
	/// <summary>使用者登入失敗.</summary>
	/// <remarks></remarks>
		//User Login Failed!
	public const  int Warn_3000000 = 3000000;
	/// <summary>使用者登出失敗.</summary>
	/// <remarks></remarks>
		//User Logout Failed!
	public const  int Warn_3000001 = 3000001;
	/// <summary>"權限不足,不能調整高階使用者權限."</summary>
	/// <remarks></remarks>
		//Permission is NOT enough to adjust high-end user.
	public const  int Warn_3000002 = 3000002;
	/// <summary>設定權限高於目前使用者權限.</summary>
	/// <remarks></remarks>
		//Setting Permission is Higher than Current User.
	public const  int Warn_3000003 = 3000003;
	/// <summary>"使用者已存在,不能新增使用者"</summary>
	/// <remarks></remarks>
		//"User already exists, you can not add users."
	public const  int Warn_3000004 = 3000004;
	/// <summary>請先復歸.</summary>
	/// <remarks></remarks>
		//"Initialize First, Please."
	public const  int Warn_3000005 = 3000005;
	/// <summary>"復歸中,請稍後."</summary>
	/// <remarks></remarks>
		//System is Homing...Please Wait.
	public const  int Warn_3000006 = 3000006;
	/// <summary>"運行中,請稍後."</summary>
	/// <remarks></remarks>
		//"System is Running…,Please Wait."
	public const  int Warn_3000007 = 3000007;
	/// <summary>"運行中無法暫停,請稍後."</summary>
	/// <remarks></remarks>
		//System is Running…Can Not Pause. Please Wait.
	public const  int Warn_3000008 = 3000008;
	/// <summary>"運行中無法使用,請先復歸."</summary>
	/// <remarks></remarks>
		//System is Running…Initialize First.
	public const  int Warn_3000009 = 3000009;
	/// <summary>自動校正中</summary>
	/// <remarks></remarks>
		//System is Auto Calibrating…
	public const  int Warn_3000010 = 3000010;
	/// <summary>請先選取Recipe.</summary>
	/// <remarks></remarks>
		//"Select Recipe, Please."
	public const  int Warn_3000011 = 3000011;
	/// <summary>請先解除Recipe鎖定.</summary>
	/// <remarks></remarks>
		//"Unlock Recipe First, Please."
	public const  int Warn_3000012 = 3000012;
	/// <summary>請先選取Recipe Pattern.</summary>
	/// <remarks></remarks>
		//"Select Recipe Pattern First, Please."
	public const  int Warn_3000013 = 3000013;
	/// <summary>請先選取Pattern Step.</summary>
	/// <remarks></remarks>
		//"Select Pattern Step First, Please."
	public const  int Warn_3000014 = 3000014;
	/// <summary>請選擇場景.</summary>
	/// <remarks></remarks>
		//Please Select Sence !
	public const  int Warn_3000015 = 3000015;
	/// <summary>輸入資料錯誤.</summary>
	/// <remarks></remarks>
		//Input Data is Wrong!
	public const  int Warn_3000016 = 3000016;
	/// <summary>請確認開始與結束位置非重合.</summary>
	/// <remarks></remarks>
		//"Check Start and End Position, Please."
	public const  int Warn_3000017 = 3000017;
	/// <summary>請輸入角度(Deg)</summary>
	/// <remarks></remarks>
		//"Enter Angle(Deg), Please"
	public const  int Warn_3000018 = 3000018;
	/// <summary>請選擇點膠設定項目.</summary>
	/// <remarks></remarks>
		//"Select Dispense Item, Please."
	public const  int Warn_3000019 = 3000019;
	/// <summary>場景不存在.</summary>
	/// <remarks></remarks>
		//Scene does NOT Exist.
	public const  int Warn_3000020 = 3000020;
	/// <summary>請輸入ID</summary>
	/// <remarks></remarks>
		//Please key In User ID!
	public const  int Warn_3000021 = 3000021;
	/// <summary>"找不到特徵,校正失敗!"</summary>
	/// <remarks></remarks>
		//"Pattern No Found, Calibration Failed!"
	public const  int Warn_3000022 = 3000022;
	/// <summary>請建立場景</summary>
	/// <remarks></remarks>
		//Please create Scene!
	public const  int Warn_3000023 = 3000023;
	/// <summary>場景光源設定檔不存在</summary>
	/// <remarks></remarks>
		//Scene Light file NOT Exist!
	public const  int Warn_3000024 = 3000024;
	/// <summary>Node 不存在,請先選擇Node</summary>
	/// <remarks></remarks>
		//NodeID Not Exists, Select Node First.
	public const  int Warn_3000025 = 3000025;
	/// <summary>請先選擇天秤</summary>
	/// <remarks></remarks>
		//Select Balance First, Please!
	public const  int Warn_3000026 = 3000026;
	/// <summary>請先教導場景</summary>
	/// <remarks></remarks>
		//Please teach Scence!
	public const  int Warn_3000027 = 3000027;
	/// <summary>請先選擇天秤</summary>
	/// <remarks></remarks>
		//Select Scale First, Please!
	public const  int Warn_3000028 = 3000028;
	/// <summary>位置異常!軸向超出極限位置</summary>
	/// <remarks></remarks>
		//Pos. Error! AxisParameter is Out Of Range
	public const  int Warn_3000029 = 3000029;
	/// <summary>場景載入中，請稍後</summary>
	/// <remarks></remarks>
		//Scene is Loading, Please Wait.
	public const  int Warn_3000030 = 3000030;
	/// <summary>請先選擇溫控檔</summary>
	/// <remarks></remarks>
		//Select Temperature File First, Please!
	public const  int Warn_3000031 = 3000031;
	/// <summary>溫控檔不存在</summary>
	/// <remarks></remarks>
		//No Temperature File.
	public const  int Warn_3000032 = 3000032;
	/// <summary>檔案開啟失敗</summary>
	/// <remarks></remarks>
		//Failed to open file.
	public const  int Warn_3000033 = 3000033;
	/// <summary>物件不存在</summary>
	/// <remarks></remarks>
		//Object does NOT Exist.
	public const  int Warn_3000034 = 3000034;
	/// <summary>檔案儲存失敗</summary>
	/// <remarks></remarks>
		//Failed to save file.
	public const  int Warn_3000035 = 3000035;
	/// <summary>儲存完成</summary>
	/// <remarks></remarks>
		//Save OK!
	public const  int Warn_3000036 = 3000036;
	/// <summary>使用者登入</summary>
	/// <remarks></remarks>
		//User Login.
	public const  int Warn_3000037 = 3000037;
	/// <summary>場景載入失敗</summary>
	/// <remarks></remarks>
		//Scene Load Failed.
	public const  int Warn_3000038 = 3000038;
	/// <summary>資料不相符，無法繼續執行</summary>
	/// <remarks></remarks>
		//Data is Not Equal, Can't Continue!!
	public const  int Warn_3000039 = 3000039;
	/// <summary>請先選擇控制器</summary>
	/// <remarks></remarks>
		//Select Controller First, Please!
	public const  int Warn_3000040 = 3000040;
	/// <summary>參數檔案不存在</summary>
	/// <remarks></remarks>
		//Parameter Not Exists!
	public const  int Warn_3000041 = 3000041;
	/// <summary>Pattern使用中無法刪除</summary>
	/// <remarks></remarks>
		//Can't Delete this Pattern! It is in use!
	public const  int Warn_3000042 = 3000042;
	/// <summary>Pattern不存在</summary>
	/// <remarks></remarks>
		//Pattern Not Exists!
	public const  int Warn_3000043 = 3000043;
	/// <summary>請先選擇執行次數</summary>
	/// <remarks></remarks>
		//Select Round First, Please!
	public const  int Warn_3000044 = 3000044;
	/// <summary>Recipe錯誤,請重新建立檔案</summary>
	/// <remarks></remarks>
		//Recipe Error, Please Create New File.
	public const  int Warn_3000045 = 3000045;
	/// <summary>檔案名稱已存在</summary>
	/// <remarks></remarks>
		//File name Has Exist.
	public const  int Warn_3000046 = 3000046;
	/// <summary>請確認開始與中間位置非重合.</summary>
	/// <remarks></remarks>
		//Check Start and Middle Position, Please.
	public const  int Warn_3000047 = 3000047;
	/// <summary>請確認中間與結束位置非重合.</summary>
	/// <remarks></remarks>
		//Check Middle and End Position, Please.
	public const  int Warn_3000048 = 3000048;
	/// <summary>請確認三點共線問題</summary>
	/// <remarks></remarks>
		//Check Three Point Collinea issue, Please.
	public const  int Warn_3000049 = 3000049;
	/// <summary>請選擇測高模式</summary>
	/// <remarks></remarks>
		//Please select Measure Z Height Mode.
	public const  int Warn_3000050 = 3000050;
	/// <summary>儲存完成!請重啟程式</summary>
	/// <remarks></remarks>
		//Save OK!Please Restart Software.
	public const  int Warn_3000051 = 3000051;
	/// <summary>請先選擇CCD</summary>
	/// <remarks></remarks>
		//Select CCD First, Please!
	public const  int Warn_3000052 = 3000052;
	/// <summary>請先選擇CCD裝置</summary>
	/// <remarks></remarks>
		//Select CCD Device First, Please!
	public const  int Warn_3000053 = 3000053;
	/// <summary>請先選擇CCD SN</summary>
	/// <remarks></remarks>
		//Select CCD SN First, Please!
	public const  int Warn_3000054 = 3000054;
	/// <summary>請先選擇COM Port</summary>
	/// <remarks></remarks>
		//Select COM Port, Please.
	public const  int Warn_3000055 = 3000055;
	/// <summary>請先選擇 Laser Reader</summary>
	/// <remarks></remarks>
		//Select Laser Reader First, Please!
	public const  int Warn_3000056 = 3000056;
	/// <summary>請先選擇觸發板</summary>
	/// <remarks></remarks>
		//Select Trigger Board First, Please!
	public const  int Warn_3000057 = 3000057;
	/// <summary>檔案名稱錯誤</summary>
	/// <remarks></remarks>
		//File Name Error!
	public const  int Warn_3000058 = 3000058;
	/// <summary>資料格式錯誤</summary>
	/// <remarks></remarks>
		//Data format error!
	public const  int Warn_3000059 = 3000059;
	/// <summary>無相應站號</summary>
	/// <remarks></remarks>
		//Wrong Stage No.
	public const  int Warn_3000060 = 3000060;
	/// <summary>無相應節點</summary>
	/// <remarks></remarks>
		//Wrong Node ID.
	public const  int Warn_3000061 = 3000061;
	/// <summary>該層資料不存在</summary>
	/// <remarks></remarks>
		//Wrong Level No.
	public const  int Warn_3000062 = 3000062;
	/// <summary>請選擇項目</summary>
	/// <remarks></remarks>
		//Please Select Item!
	public const  int Warn_3000063 = 3000063;
	/// <summary>微量天平1 重量超出範圍</summary>
	/// <remarks></remarks>
		//Scale1 Weight out of Range!
	public const  int Warn_3000064 = 3000064;
	/// <summary>微量天平2 重量超出範圍</summary>
	/// <remarks></remarks>
		//Scale2 Weight out of Range!
	public const  int Warn_3000065 = 3000065;
	/// <summary>檔案不存在</summary>
	/// <remarks></remarks>
		//File Not Exists.
	public const  int Warn_3000066 = 3000066;
	/// <summary>場景ID已經存在!</summary>
	/// <remarks></remarks>
		//SceneID Already Exists!
	public const  int Warn_3000067 = 3000067;
	/// <summary>請先選擇電空閥!</summary>
	/// <remarks></remarks>
		//Select Electro Pneumatic Valve First, Please!
	public const  int Warn_3000068 = 3000068;
	/// <summary>膠材名稱不存在</summary>
	/// <remarks></remarks>
		//Glue Name Not Exists.
	public const  int Warn_3000069 = 3000069;
	/// <summary>請確認閥設定值!</summary>
	/// <remarks></remarks>
		//Plase Check Valve set !
	public const  int Warn_3000070 = 3000070;
	/// <summary>請先破真空</summary>
	/// <remarks></remarks>
		//Please break the vacuum first
	public const  int Warn_3000071 = 3000071;
	/// <summary>標準差為0，無法計算CPK</summary>
	/// <remarks></remarks>
		//Standard deviation is zero,CPK can not be calculated
	public const  int Warn_3000072 = 3000072;
	/// <summary>閥氣壓值為0</summary>
	/// <remarks></remarks>
		//Valve AP Value is Zero.
	public const  int Warn_3000073 = 3000073;
	/// <summary>Z 位置超出極限，請重試</summary>
	/// <remarks></remarks>
		//Z Pos out of limit.Please retry.
	public const  int Warn_3000074 = 3000074;
	/// <summary>CCD1定位不良.</summary>
	/// <remarks></remarks>
		//CCD1 Alignment NG.
	public const  int Warn_3012000 = 3012000;
	/// <summary>CCD1重複叫用Start Live.</summary>
	/// <remarks></remarks>
		//CCD1 Repeat Call Start Live.
	public const  int Warn_3012001 = 3012001;
	/// <summary>CCD1重複叫用End Live.</summary>
	/// <remarks></remarks>
		//CCD1 Repeat Call End Live.
	public const  int Warn_3012002 = 3012002;
	/// <summary>CCD1清潔校正片.</summary>
	/// <remarks></remarks>
		//CCD1 Clear Calibration Piece
	public const  int Warn_3012003 = 3012003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3012004 = 3012004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3012005 = 3012005;
	/// <summary>CCD1檢測不良.</summary>
	/// <remarks></remarks>
		//CCD1 Inspection NG.
	public const  int Warn_3012100 = 3012100;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3012101 = 3012101;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3012102 = 3012102;
	/// <summary></summary>
	/// <remarks></remarks>
		//  
	public const  int Warn_3012103 = 3012103;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3012104 = 3012104;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3012105 = 3012105;
	/// <summary>CCD2定位不良.</summary>
	/// <remarks></remarks>
		//CCD2 Alignment NG.
	public const  int Warn_3012200 = 3012200;
	/// <summary>CCD2重複叫用Start Live.</summary>
	/// <remarks></remarks>
		//CCD2 Repeat Call Start Live.
	public const  int Warn_3012201 = 3012201;
	/// <summary>CCD2重複叫用End Live.</summary>
	/// <remarks></remarks>
		//CCD2 Repeat Call End Live.
	public const  int Warn_3012202 = 3012202;
	/// <summary>CCD2清潔校正片.</summary>
	/// <remarks></remarks>
		//CCD2 Clear Calibration Piece
	public const  int Warn_3012203 = 3012203;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3012204 = 3012204;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3012205 = 3012205;
	/// <summary>CCD2檢測不良.</summary>
	/// <remarks></remarks>
		//CCD2 Inspection NG.
	public const  int Warn_3003100 = 3003100;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3003101 = 3003101;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3003102 = 3003102;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3003103 = 3003103;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3003104 = 3003104;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3003105 = 3003105;
	/// <summary>CCD3定位不良.</summary>
	/// <remarks></remarks>
		//CCD3 Alignment NG.
	public const  int Warn_3012300 = 3012300;
	/// <summary>CCD3重複叫用Start Live.</summary>
	/// <remarks></remarks>
		//CCD3 Repeat Call Start Live.
	public const  int Warn_3012301 = 3012301;
	/// <summary>CCD3重複叫用End Live.</summary>
	/// <remarks></remarks>
		//CCD3 Repeat Call End Live.
	public const  int Warn_3012302 = 3012302;
	/// <summary>CCD3清潔校正片.</summary>
	/// <remarks></remarks>
		//CCD3 Clear Calibration Piece
	public const  int Warn_3012303 = 3012303;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3012304 = 3012304;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3012305 = 3012305;
	/// <summary>CCD3檢測不良.</summary>
	/// <remarks></remarks>
		//CCD3 Inspection NG.
	public const  int Warn_3012400 = 3012400;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3012401 = 3012401;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3012402 = 3012402;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3012403 = 3012403;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3012404 = 3012404;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3012405 = 3012405;
	/// <summary>CCD4定位不良.</summary>
	/// <remarks></remarks>
		//CCD4 Alignment NG.
	public const  int Warn_3012500 = 3012500;
	/// <summary>CCD4重複叫用Start Live.</summary>
	/// <remarks></remarks>
		//CCD4 Repeat Call Start Live.
	public const  int Warn_3012501 = 3012501;
	/// <summary>CCD4重複叫用End Live.</summary>
	/// <remarks></remarks>
		//CCD4 Repeat Call End Live.
	public const  int Warn_3012502 = 3012502;
	/// <summary>CCD4清潔校正片.</summary>
	/// <remarks></remarks>
		//CCD4 Clear Calibration Piece
	public const  int Warn_3012503 = 3012503;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3012504 = 3012504;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3012505 = 3012505;
	/// <summary>CCD4檢測不良.</summary>
	/// <remarks></remarks>
		//CCD4 Inspection NG.
	public const  int Warn_3012600 = 3012600;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3012601 = 3012601;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3012602 = 3012602;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3012603 = 3012603;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3012604 = 3012604;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3012605 = 3012605;
	/// <summary>"閥1自動測高中,請稍後."</summary>
	/// <remarks></remarks>
		//Valve1 is Auto Detecting…Please Wait.
	public const  int Warn_3019000 = 3019000;
	/// <summary>"閥1自動校正中,請稍後."</summary>
	/// <remarks></remarks>
		//Valve1 Auto Calibration Running…Please Wait.
	public const  int Warn_3019001 = 3019001;
	/// <summary>閥1氣壓值設定失敗.</summary>
	/// <remarks></remarks>
		//Set Valve1 AP Value: N.A.
	public const  int Warn_3019002 = 3019002;
	/// <summary>閥1汽缸上抬逾時</summary>
	/// <remarks></remarks>
		//Valve1 Cylinder Up TimeOut.
	public const  int Warn_3019003 = 3019003;
	/// <summary>閥1汽缸下降逾時</summary>
	/// <remarks></remarks>
		//Valve1 Cylinder Down TimeOut.
	public const  int Warn_3019004 = 3019004;
	/// <summary>請更換閥1膠管</summary>
	/// <remarks></remarks>
		//"Exchange Valve1 Syringe, Please"
	public const  int Warn_3019005 = 3019005;
	/// <summary>閥1膠量不足</summary>
	/// <remarks></remarks>
		//Valve1 Low level
	public const  int Warn_3019006 = 3019006;
	/// <summary>閥1膠材壽命到期</summary>
	/// <remarks></remarks>
		//Valve1 LifeTime Expired
	public const  int Warn_3019007 = 3019007;
	/// <summary>閥1膠材計數到期</summary>
	/// <remarks></remarks>
		//Valve1 Life Count Expired
	public const  int Warn_3019008 = 3019008;
	/// <summary>請更換閥1清膠材料</summary>
	/// <remarks></remarks>
		//Exchange Valve1 Material!
	public const  int Warn_3019009 = 3019009;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3019010 = 3019010;
	/// <summary>"閥2自動測高中,請稍後."</summary>
	/// <remarks></remarks>
		//Valve2 is Auto Detecting…Please Wait.
	public const  int Warn_3019100 = 3019100;
	/// <summary>"閥2自動校正中,請稍後."</summary>
	/// <remarks></remarks>
		//Valve2 Auto Calibration Running…Please Wait.
	public const  int Warn_3019101 = 3019101;
	/// <summary>閥2氣壓值設定失敗.</summary>
	/// <remarks></remarks>
		//Set Valve2 AP Value: N.A.
	public const  int Warn_3019102 = 3019102;
	/// <summary>閥2汽缸上抬逾時</summary>
	/// <remarks></remarks>
		//Valve2 Cylinder Up TimeOut.
	public const  int Warn_3019103 = 3019103;
	/// <summary>閥2汽缸下降逾時</summary>
	/// <remarks></remarks>
		//Valve2 Cylinder Down TimeOut.
	public const  int Warn_3019104 = 3019104;
	/// <summary>請更換閥2膠管</summary>
	/// <remarks></remarks>
		//"Exchange Valve2 Syringe, Please"
	public const  int Warn_3019105 = 3019105;
	/// <summary>閥2膠量不足</summary>
	/// <remarks></remarks>
		//Valve2 Low level
	public const  int Warn_3019106 = 3019106;
	/// <summary>閥2膠材壽命到期</summary>
	/// <remarks></remarks>
		//Valve2 LifeTime Expired
	public const  int Warn_3019107 = 3019107;
	/// <summary>閥2膠材計數到期</summary>
	/// <remarks></remarks>
		//Valve2 Life Count Expired
	public const  int Warn_3019108 = 3019108;
	/// <summary>請更換閥2清膠材料</summary>
	/// <remarks></remarks>
		//Exchange Valve2 Material!
	public const  int Warn_3019109 = 3019109;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3019110 = 3019110;
	/// <summary>"閥3自動測高中,請稍後."</summary>
	/// <remarks></remarks>
		//Valve3 is Auto Detecting…Please Wait.
	public const  int Warn_3019200 = 3019200;
	/// <summary>"閥3自動校正中,請稍後."</summary>
	/// <remarks></remarks>
		//Valve3 Auto Calibration Running…Please Wait.
	public const  int Warn_3019201 = 3019201;
	/// <summary>閥3氣壓值設定失敗.</summary>
	/// <remarks></remarks>
		//Set Valve3 AP Value: N.A.
	public const  int Warn_3019202 = 3019202;
	/// <summary>閥3汽缸上抬逾時</summary>
	/// <remarks></remarks>
		//Valve3 Cylinder Up TimeOut.
	public const  int Warn_3019203 = 3019203;
	/// <summary>閥3汽缸下降逾時</summary>
	/// <remarks></remarks>
		//Valve3 Cylinder Down TimeOut.
	public const  int Warn_3019204 = 3019204;
	/// <summary>請更換閥3膠管</summary>
	/// <remarks></remarks>
		//"Exchange Valve3 Syringe, Please"
	public const  int Warn_3019205 = 3019205;
	/// <summary>閥3膠量不足</summary>
	/// <remarks></remarks>
		//Valve3 Low level
	public const  int Warn_3019206 = 3019206;
	/// <summary>閥3膠材壽命到期</summary>
	/// <remarks></remarks>
		//Valve3 LifeTime Expired
	public const  int Warn_3019207 = 3019207;
	/// <summary>閥3膠材計數到期</summary>
	/// <remarks></remarks>
		//Valve3 Life Count Expired
	public const  int Warn_3019208 = 3019208;
	/// <summary>請更換閥3清膠材料</summary>
	/// <remarks></remarks>
		//Exchange Valve3 Material!
	public const  int Warn_3019209 = 3019209;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3019210 = 3019210;
	/// <summary>"閥4自動測高中,請稍後."</summary>
	/// <remarks></remarks>
		//Valve4 is Auto Detecting…Please Wait.
	public const  int Warn_3019300 = 3019300;
	/// <summary>"閥4自動校正中,請稍後."</summary>
	/// <remarks></remarks>
		//Valve4 Auto Calibration Running…Please Wait.
	public const  int Warn_3019301 = 3019301;
	/// <summary>閥4氣壓值設定失敗.</summary>
	/// <remarks></remarks>
		//Set Valve4 AP Value: N.A.
	public const  int Warn_3019302 = 3019302;
	/// <summary>閥4汽缸上抬逾時</summary>
	/// <remarks></remarks>
		//Valve4 Cylinder Up TimeOut.
	public const  int Warn_3019303 = 3019303;
	/// <summary>閥4汽缸下降逾時</summary>
	/// <remarks></remarks>
		//Valve4 Cylinder Down TimeOut.
	public const  int Warn_3019304 = 3019304;
	/// <summary>請更換閥4膠管</summary>
	/// <remarks></remarks>
		//"Exchange Valve4 Syringe, Please"
	public const  int Warn_3019305 = 3019305;
	/// <summary>閥4膠量不足</summary>
	/// <remarks></remarks>
		//Valve4 Low level
	public const  int Warn_3019306 = 3019306;
	/// <summary>閥4膠材壽命到期</summary>
	/// <remarks></remarks>
		//Valve4 LifeTime Expired
	public const  int Warn_3019307 = 3019307;
	/// <summary>閥4膠材計數到期</summary>
	/// <remarks></remarks>
		//Valve4 Life Count Expired
	public const  int Warn_3019308 = 3019308;
	/// <summary>請更換閥4清膠材料</summary>
	/// <remarks></remarks>
		//Exchange Valve4 Material!
	public const  int Warn_3019309 = 3019309;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3019310 = 3019310;
	/// <summary>等待Conveyor阻擋缸下降.</summary>
	/// <remarks></remarks>
		//Wait Conveyor Stopper Down.
	public const  int Warn_3024000 = 3024000;
	/// <summary>沒有料片在Conveyor上</summary>
	/// <remarks></remarks>
		//No Products On Conveyor.
	public const  int Warn_3024001 = 3024001;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3024002 = 3024002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3024003 = 3024003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3024004 = 3024004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3024005 = 3024005;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3024006 = 3024006;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3024007 = 3024007;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3024008 = 3024008;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3024009 = 3024009;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int Warn_3024010 = 3024010;
	/// <summary>請等待X軸到位訊號(INP)</summary>
	/// <remarks></remarks>
		//Wait X-Axis Inposition(INP) Signal. Please Wait.
	public const  int Warn_3030000 = 3030000;
	/// <summary>請等待Y1軸到位訊號(INP)</summary>
	/// <remarks></remarks>
		//Wait Y1-Axis Inposition(INP) Signal. Please Wait.
	public const  int Warn_3031000 = 3031000;
	/// <summary>請等待Z軸到位訊號(INP)</summary>
	/// <remarks></remarks>
		//Wait Z-Axis Inposition(INP) Signal. Please Wait.
	public const  int Warn_3032000 = 3032000;
	/// <summary>請等待Y2軸到位訊號(INP)</summary>
	/// <remarks></remarks>
		//Wait Y2-Axis Inposition(INP) Signal. Please Wait.
	public const  int Warn_3033000 = 3033000;
	/// <summary>請等待B軸到位訊號(INP)</summary>
	/// <remarks></remarks>
		//Wait B-Axis Inposition(INP) Signal. Please Wait.
	public const  int Warn_3034000 = 3034000;
	/// <summary>請等待C軸到位訊號(INP)</summary>
	/// <remarks></remarks>
		//Wait C-Axis Inposition(INP) Signal. Please Wait.
	public const  int Warn_3035000 = 3035000;
	/// <summary>請等待運動群組1到位訊號(INP)</summary>
	/// <remarks></remarks>
		//Wait Group1 Inposition(INP) Signal. Please Wait.
	public const  int Warn_3036000 = 3036000;
	/// <summary>請等待Conveyor1軸到位訊號(INP)</summary>
	/// <remarks></remarks>
		//Wait Conveyor1-Axis Inposition(INP) Signal. Please Wait.
	public const  int Warn_3037000 = 3037000;
	/// <summary>請等待Conveyor2軸到位訊號(INP)</summary>
	/// <remarks></remarks>
		//Wait Conveyor2-Axis Inposition(INP) Signal. Please Wait.
	public const  int Warn_3038000 = 3038000;
	/// <summary>請等待S1軸到位訊號(INP)</summary>
	/// <remarks></remarks>
		//Wait S1-Axis Inposition(INP) Signal. Please Wait.
	public const  int Warn_3039000 = 3039000;
	/// <summary>請等待S2軸到位訊號(INP)</summary>
	/// <remarks></remarks>
		//Wait S2-Axis Inposition(INP) Signal. Please Wait.
	public const  int Warn_3040000 = 3040000;
	/// <summary>請等待S3軸到位訊號(INP)</summary>
	/// <remarks></remarks>
		//Wait S3-Axis Inposition(INP) Signal. Please Wait.
	public const  int Warn_3041000 = 3041000;
	/// <summary>請等待U軸到位訊號(INP)</summary>
	/// <remarks></remarks>
		//Wait U-Axis Inposition(INP) Signal. Please Wait.
	public const  int Warn_3042000 = 3042000;
	/// <summary>請等待V1軸到位訊號(INP)</summary>
	/// <remarks></remarks>
		//Wait V1-Axis Inposition(INP) Signal. Please Wait.
	public const  int Warn_3043000 = 3043000;
	/// <summary>請等待W軸到位訊號(INP)</summary>
	/// <remarks></remarks>
		//Wait W-Axis Inposition(INP) Signal. Please Wait.
	public const  int Warn_3044000 = 3044000;
	/// <summary>請等待V2軸到位訊號(INP)</summary>
	/// <remarks></remarks>
		//Wait V2-Axis Inposition(INP) Signal. Please Wait.
	public const  int Warn_3045000 = 3045000;
	/// <summary>請等待B2軸到位訊號(INP)</summary>
	/// <remarks></remarks>
		//Wait B2-Axis Inposition(INP) Signal. Please Wait.
	public const  int Warn_3046000 = 3046000;
	/// <summary>請等待C2軸到位訊號(INP)</summary>
	/// <remarks></remarks>
		//Wait C2-Axis Inposition(INP) Signal. Please Wait.
	public const  int Warn_3047000 = 3047000;
	/// <summary>請等待運動群組2到位訊號(INP)</summary>
	/// <remarks></remarks>
		//Wait Group2 Inposition(INP) Signal. Please Wait.
	public const  int Warn_3048000 = 3048000;
	/// <summary>系統啟動</summary>
	/// <remarks></remarks>
		//System Start
	public const  int INFO_6000000 = 6000000;
	/// <summary>系統關閉.</summary>
	/// <remarks></remarks>
		//System End
	public const  int INFO_6000001 = 6000001;
	/// <summary>機台型號:{0}</summary>
	/// <remarks></remarks>
		//Machine Type:{0}
	public const  int INFO_6000002 = 6000002;
	/// <summary>軟體版本:{0}</summary>
	/// <remarks></remarks>
		//Software Version:{0}
	public const  int INFO_6000003 = 6000003;
	/// <summary>觸發版本:{0}</summary>
	/// <remarks></remarks>
		//Trigger Version:{0}
	public const  int INFO_6000004 = 6000004;
	/// <summary>IO卡初始化完成</summary>
	/// <remarks></remarks>
		//Initialize IO Card OK.
	public const  int INFO_6000005 = 6000005;
	/// <summary>IO卡關卡成功</summary>
	/// <remarks></remarks>
		//Close IO Card OK.
	public const  int INFO_6000006 = 6000006;
	/// <summary>AI卡初始化完成</summary>
	/// <remarks></remarks>
		//Initialize AI Card OK.
	public const  int INFO_6000007 = 6000007;
	/// <summary>AI卡關卡成功</summary>
	/// <remarks></remarks>
		//Close AI Card OK.
	public const  int INFO_6000008 = 6000008;
	/// <summary>AO卡初始化完成</summary>
	/// <remarks></remarks>
		//Initialize AO Card OK.
	public const  int INFO_6000009 = 6000009;
	/// <summary>AO卡關卡成功</summary>
	/// <remarks></remarks>
		//Close AO Card OK.
	public const  int INFO_6000010 = 6000010;
	/// <summary>DI卡初始化完成</summary>
	/// <remarks></remarks>
		//Initialize DI Card OK.
	public const  int INFO_6000011 = 6000011;
	/// <summary>DI卡關卡成功</summary>
	/// <remarks></remarks>
		//Close DI Card OK.
	public const  int INFO_6000012 = 6000012;
	/// <summary>DO卡初始化完成</summary>
	/// <remarks></remarks>
		//Initialize DO Card OK.
	public const  int INFO_6000013 = 6000013;
	/// <summary>DO卡關卡成功</summary>
	/// <remarks></remarks>
		//Close DO Card OK.
	public const  int INFO_6000014 = 6000014;
	/// <summary>運動控制卡初始化完成</summary>
	/// <remarks></remarks>
		//Initialize Motion Card OK.
	public const  int INFO_6000015 = 6000015;
	/// <summary>運動控制卡關卡成功</summary>
	/// <remarks></remarks>
		//Close Motion Card OK.
	public const  int INFO_6000016 = 6000016;
	/// <summary>COM通訊埠開啟成功</summary>
	/// <remarks></remarks>
		//Open COM Port OK.
	public const  int INFO_6000017 = 6000017;
	/// <summary>COM通訊埠關閉成功.</summary>
	/// <remarks></remarks>
		//Close COM Port OK.
	public const  int INFO_6000018 = 6000018;
	/// <summary>網路連線成功</summary>
	/// <remarks></remarks>
		//EtherNET Connection OK.
	public const  int INFO_6000019 = 6000019;
	/// <summary>網路離線成功.</summary>
	/// <remarks></remarks>
		//Ethernet Diconnect OK.
	public const  int INFO_6000020 = 6000020;
	/// <summary>影像擷取卡1初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize Image Card1 OK.
	public const  int INFO_6000021 = 6000021;
	/// <summary>影像擷取卡1關閉成功.</summary>
	/// <remarks></remarks>
		//Close Image Card1 OK.
	public const  int INFO_6000022 = 6000022;
	/// <summary>雷射干涉儀1初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize Laser Interfeormeter1 OK.
	public const  int INFO_6000023 = 6000023;
	/// <summary>雷射干涉儀1關閉成功.</summary>
	/// <remarks></remarks>
		//Close Laser Interferometer1 OK.
	public const  int INFO_6000024 = 6000024;
	/// <summary>雷射干涉儀2初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize Laser Interfeormeter2 OK.
	public const  int INFO_6000025 = 6000025;
	/// <summary>雷射干涉儀2關閉成功.</summary>
	/// <remarks></remarks>
		//Close Laser Interferometer2 OK.
	public const  int INFO_6000026 = 6000026;
	/// <summary>雷射干涉儀3初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize Laser Interfeormeter3 OK.
	public const  int INFO_6000027 = 6000027;
	/// <summary>雷射干涉儀3關閉成功.</summary>
	/// <remarks></remarks>
		//Close Laser Interferometer3 OK.
	public const  int INFO_6000028 = 6000028;
	/// <summary>雷射干涉儀4初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize Laser Interfeormeter4 OK.
	public const  int INFO_6000029 = 6000029;
	/// <summary>雷射干涉儀4關閉成功.</summary>
	/// <remarks></remarks>
		//Close Laser Interferometer4 OK.
	public const  int INFO_6000030 = 6000030;
	/// <summary>微量天平1初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize Scale1 OK.
	public const  int INFO_6000031 = 6000031;
	/// <summary>微量天平1關閉成功.</summary>
	/// <remarks></remarks>
		//Close Scale1 OK.
	public const  int INFO_6000032 = 6000032;
	/// <summary>微量天平2初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize Scale2 OK.
	public const  int INFO_6000033 = 6000033;
	/// <summary>微量天平2關閉成功.</summary>
	/// <remarks></remarks>
		//Close Scale2 OK.
	public const  int INFO_6000034 = 6000034;
	/// <summary>FMCS1初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize FMCS1 OK.
	public const  int INFO_6000035 = 6000035;
	/// <summary>FMCS1關閉成功.</summary>
	/// <remarks></remarks>
		//Close FMCS1 OK.
	public const  int INFO_6000036 = 6000036;
	/// <summary>FMCS2初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize FMCS2 OK.
	public const  int INFO_6000037 = 6000037;
	/// <summary>FMCS2關閉成功.</summary>
	/// <remarks></remarks>
		//Close FMCS2 OK.
	public const  int INFO_6000038 = 6000038;
	/// <summary>FMCS3初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize FMCS3 OK.
	public const  int INFO_6000039 = 6000039;
	/// <summary>FMCS3關閉成功.</summary>
	/// <remarks></remarks>
		//Close FMCS3 OK.
	public const  int INFO_6000040 = 6000040;
	/// <summary>FMCS4初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize FMCS4 OK.
	public const  int INFO_6000041 = 6000041;
	/// <summary>FMCS4關閉成功.</summary>
	/// <remarks></remarks>
		//Close FMCS4 OK.
	public const  int INFO_6000042 = 6000042;
	/// <summary>使用者登入成功.</summary>
	/// <remarks></remarks>
		//User Login OK.
	public const  int INFO_6000043 = 6000043;
	/// <summary>使用者登出成功.</summary>
	/// <remarks></remarks>
		//User LogOut OK.
	public const  int INFO_6000044 = 6000044;
	/// <summary>CCD類型:{0}</summary>
	/// <remarks></remarks>
		//CCD Type:{0}
	public const  int INFO_6000045 = 6000045;
	/// <summary>CCD1初始化完成.</summary>
	/// <remarks></remarks>
		//Initialize CCD1 OK.
	public const  int INFO_6000046 = 6000046;
	/// <summary>CCD1關閉成功.</summary>
	/// <remarks></remarks>
		//Close CCD1 OK.
	public const  int INFO_6000047 = 6000047;
	/// <summary>CCD2初始化完成.</summary>
	/// <remarks></remarks>
		//Initialize CCD2 OK.
	public const  int INFO_6000048 = 6000048;
	/// <summary>CCD2關閉成功.</summary>
	/// <remarks></remarks>
		//Close CCD2 OK.
	public const  int INFO_6000049 = 6000049;
	/// <summary>CCD3初始化完成.</summary>
	/// <remarks></remarks>
		//Initialize CCD3 OK.
	public const  int INFO_6000050 = 6000050;
	/// <summary>CCD3關閉成功.</summary>
	/// <remarks></remarks>
		//Close CCD3 OK.
	public const  int INFO_6000051 = 6000051;
	/// <summary>CCD4初始化完成.</summary>
	/// <remarks></remarks>
		//Initialize CCD4 OK.
	public const  int INFO_6000052 = 6000052;
	/// <summary>CCD4關閉成功.</summary>
	/// <remarks></remarks>
		//Close CCD4 OK.
	public const  int INFO_6000053 = 6000053;
	/// <summary>儲存噴射閥Z高 閥1:{0} 測高感測器:{1}</summary>
	/// <remarks></remarks>
		//Save Jet Zpos Valve1: {0} Laser(Height Sensor): {1}
	public const  int INFO_6000054 = 6000054;
	/// <summary>介面設定噴射閥Z高 閥1:{0}</summary>
	/// <remarks></remarks>
		//UI Jet Zpos Valve1:{0}
	public const  int INFO_6000055 = 6000055;
	/// <summary>介面設定噴射閥Z高 測高感測器:{0}</summary>
	/// <remarks></remarks>
		//UI Jet Height Sensor Zpos:{0}
	public const  int INFO_6000056 = 6000056;
	/// <summary>儲存噴射閥Z高 閥2:{0} 測高感測器:{1}</summary>
	/// <remarks></remarks>
		//Save Jet Zpos Valve2: {0} Laser(Height Sensor): {1}
	public const  int INFO_6000057 = 6000057;
	/// <summary>介面設定噴射閥Z高 閥2:{0}</summary>
	/// <remarks></remarks>
		//UI Jet Zpos Valve2:{0}
	public const  int INFO_6000058 = 6000058;
	/// <summary>介面設定噴射閥Z高 測高感測器:{0}</summary>
	/// <remarks></remarks>
		//UI Jet Height Sensor Zpos:{0}
	public const  int INFO_6000059 = 6000059;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6000060 = 6000060;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6000061 = 6000061;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6000062 = 6000062;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6000063 = 6000063;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6000064 = 6000064;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6000065 = 6000065;
	/// <summary>傳送帶除能.</summary>
	/// <remarks></remarks>
		//Disable Conveyor.
	public const  int INFO_6001000 = 6001000;
	/// <summary>傳送帶致能.</summary>
	/// <remarks></remarks>
		//Enable Conveyor.
	public const  int INFO_6001001 = 6001001;
	/// <summary>CCD除能.</summary>
	/// <remarks></remarks>
		//Disable CCD.
	public const  int INFO_6001002 = 6001002;
	/// <summary>CCD致能.</summary>
	/// <remarks></remarks>
		//Enable CCD.
	public const  int INFO_6001003 = 6001003;
	/// <summary>膠量檢測1除能.</summary>
	/// <remarks></remarks>
		//Disable Glue Detector1
	public const  int INFO_6001004 = 6001004;
	/// <summary>膠量檢測1致能.</summary>
	/// <remarks></remarks>
		//Enable Glue Detector1
	public const  int INFO_6001005 = 6001005;
	/// <summary>膠量檢測2除能.</summary>
	/// <remarks></remarks>
		//Disable Glue Detector2
	public const  int INFO_6001006 = 6001006;
	/// <summary>膠量檢測2致能.</summary>
	/// <remarks></remarks>
		//Enable Glue Detector2
	public const  int INFO_6001007 = 6001007;
	/// <summary>真空On</summary>
	/// <remarks></remarks>
		//Pump On
	public const  int INFO_6001008 = 6001008;
	/// <summary>真空Off</summary>
	/// <remarks></remarks>
		//Pump Off
	public const  int INFO_6001009 = 6001009;
	/// <summary>設定閥1膠管氣壓值:{0}</summary>
	/// <remarks></remarks>
		//Set Valve1 AP Value: {0}
	public const  int INFO_6001010 = 6001010;
	/// <summary>設定閥2膠管氣壓值:{0}</summary>
	/// <remarks></remarks>
		//Set Valve2 AP Value: {0}
	public const  int INFO_6001011 = 6001011;
	/// <summary>設定閥3膠管氣壓值:{0}</summary>
	/// <remarks></remarks>
		//Set Valve3 AP Value: {0}
	public const  int INFO_6001012 = 6001012;
	/// <summary>設定閥4膠管氣壓值:{0}</summary>
	/// <remarks></remarks>
		//Set Valve4 AP Value: {0}
	public const  int INFO_6001013 = 6001013;
	/// <summary>閥1膠管氣壓啟用</summary>
	/// <remarks></remarks>
		//Valve1 Syringe Pressure On
	public const  int INFO_6001014 = 6001014;
	/// <summary>閥1膠管氣壓關閉</summary>
	/// <remarks></remarks>
		//Valve1 Syringe Pressure Off
	public const  int INFO_6001015 = 6001015;
	/// <summary>閥1順時針旋轉啟動</summary>
	/// <remarks></remarks>
		//Valve1 CW On
	public const  int INFO_6001016 = 6001016;
	/// <summary>閥1順時針旋轉關閉</summary>
	/// <remarks></remarks>
		//Valve1 CW Off
	public const  int INFO_6001017 = 6001017;
	/// <summary>閥1逆時針旋轉啟動</summary>
	/// <remarks></remarks>
		//Valve1 CCW On
	public const  int INFO_6001018 = 6001018;
	/// <summary>閥1逆時針旋轉關閉</summary>
	/// <remarks></remarks>
		//Valve1 CCW Off
	public const  int INFO_6001019 = 6001019;
	/// <summary>閥1汽缸上升</summary>
	/// <remarks></remarks>
		//Valve1 Cylinder Up
	public const  int INFO_6001020 = 6001020;
	/// <summary>閥1汽缸下降</summary>
	/// <remarks></remarks>
		//Valve1 Cylinder Down
	public const  int INFO_6001021 = 6001021;
	/// <summary>閥2膠管氣壓啟用</summary>
	/// <remarks></remarks>
		//Valve2 Syringe Pressure On
	public const  int INFO_6001022 = 6001022;
	/// <summary>閥2膠管氣壓關閉</summary>
	/// <remarks></remarks>
		//Valve2 Syringe Pressure Off
	public const  int INFO_6001023 = 6001023;
	/// <summary>閥2順時針旋轉啟動</summary>
	/// <remarks></remarks>
		//Valve2 CW On
	public const  int INFO_6001024 = 6001024;
	/// <summary>閥2順時針旋轉關閉</summary>
	/// <remarks></remarks>
		//Valve2 CW Off
	public const  int INFO_6001025 = 6001025;
	/// <summary>閥2逆時針旋轉啟動</summary>
	/// <remarks></remarks>
		//Valve2 CCW On
	public const  int INFO_6001026 = 6001026;
	/// <summary>閥2逆時針旋轉關閉</summary>
	/// <remarks></remarks>
		//Valve2 CCW Off
	public const  int INFO_6001027 = 6001027;
	/// <summary>閥2汽缸上升</summary>
	/// <remarks></remarks>
		//Valve2 Cylinder Up
	public const  int INFO_6001028 = 6001028;
	/// <summary>閥2汽缸下降</summary>
	/// <remarks></remarks>
		//Valve2 Cylinder Down
	public const  int INFO_6001029 = 6001029;
	/// <summary>擦膠閥夾爪閉合</summary>
	/// <remarks></remarks>
		//Clear Valve Clamp On
	public const  int INFO_6001030 = 6001030;
	/// <summary>擦膠閥夾爪張開</summary>
	/// <remarks></remarks>
		//Clear Valve Clamp Off
	public const  int INFO_6001031 = 6001031;
	/// <summary>擦膠馬達啟動</summary>
	/// <remarks></remarks>
		//Clear Valve Motor On
	public const  int INFO_6001032 = 6001032;
	/// <summary>擦膠馬達停止</summary>
	/// <remarks></remarks>
		//Clear Valve Motor Off
	public const  int INFO_6001033 = 6001033;
	/// <summary>擦膠馬達順時針旋轉啟動</summary>
	/// <remarks></remarks>
		//Clear Valve Motor CW On
	public const  int INFO_6001034 = 6001034;
	/// <summary>擦膠馬達順時針旋轉停止</summary>
	/// <remarks></remarks>
		//Clear Valve Motor CW Off
	public const  int INFO_6001035 = 6001035;
	/// <summary>擦膠馬達逆時針旋轉啟動</summary>
	/// <remarks></remarks>
		//Clear Valve Motor CCW On
	public const  int INFO_6001036 = 6001036;
	/// <summary>擦膠馬達逆時針旋轉停止</summary>
	/// <remarks></remarks>
		//Clear Valve Motor CCW Off
	public const  int INFO_6001037 = 6001037;
	/// <summary>絕對移動目標位置({0})</summary>
	/// <remarks></remarks>
		//AbsMove To Pos({0})
	public const  int INFO_6001038 = 6001038;
	/// <summary>"絕對移動目標位置({0},{1})"</summary>
	/// <remarks></remarks>
		//"AbsMove To Pos({0},{1})"
	public const  int INFO_6001039 = 6001039;
	/// <summary>"絕對移動目標位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"AbsMove To Pos({0},{1},{2})"
	public const  int INFO_6001040 = 6001040;
	/// <summary>"絕對移動目標位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"AbsMove To Pos({0},{1},{2},{3})"
	public const  int INFO_6001041 = 6001041;
	/// <summary>"絕對移動目標位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"AbsMove To Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6001042 = 6001042;
	/// <summary>"移動到第一定位點位置({0},{1})"</summary>
	/// <remarks></remarks>
		//"Goto Alignment Pos1({0},{1})"
	public const  int INFO_6001043 = 6001043;
	/// <summary>"移動到第一定位點位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Goto Alignment Pos1({0},{1},{2})"
	public const  int INFO_6001044 = 6001044;
	/// <summary>"移動到第一定位點位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Goto Alignment Pos1({0},{1},{2},{3})"
	public const  int INFO_6001045 = 6001045;
	/// <summary>"移動到第一定位點位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Goto Alignment Pos1({0},{1},{2},{3},{4})"
	public const  int INFO_6001046 = 6001046;
	/// <summary>"移動到第二定位點位置({0},{1})"</summary>
	/// <remarks></remarks>
		//"Goto Alignment Pos2({0},{1})"
	public const  int INFO_6001047 = 6001047;
	/// <summary>"移動到第二定位點位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Goto Alignment Pos2({0},{1},{2})"
	public const  int INFO_6001048 = 6001048;
	/// <summary>"移動到第二定位點位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Goto Alignment Pos2({0},{1},{2},{3})"
	public const  int INFO_6001049 = 6001049;
	/// <summary>"移動到第二定位點位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Goto Alignment Pos2({0},{1},{2},{3},{4})"
	public const  int INFO_6001050 = 6001050;
	/// <summary>"移動到CCD1位置({0},{1})"</summary>
	/// <remarks></remarks>
		//"Goto CCD1 Pos({0},{1})"
	public const  int INFO_6001051 = 6001051;
	/// <summary>"移動到CCD1位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Goto CCD1 Pos({0},{1},{2})"
	public const  int INFO_6001052 = 6001052;
	/// <summary>"移動到CCD1位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Goto CCD1 Pos({0},{1},{2},{3})"
	public const  int INFO_6001053 = 6001053;
	/// <summary>"移動到CCD1位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Goto CCD1 Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6001054 = 6001054;
	/// <summary>"移動到CCD2位置({0},{1})"</summary>
	/// <remarks></remarks>
		//"Goto CCD2 Pos({0},{1})"
	public const  int INFO_6001055 = 6001055;
	/// <summary>"移動到CCD2位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Goto CCD2 Pos({0},{1},{2})"
	public const  int INFO_6001056 = 6001056;
	/// <summary>"移動到CCD2位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Goto CCD2 Pos({0},{1},{2},{3})"
	public const  int INFO_6001057 = 6001057;
	/// <summary>"移動到CCD2位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Goto CCD2 Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6001058 = 6001058;
	/// <summary>"移動到CCD3位置({0},{1})"</summary>
	/// <remarks></remarks>
		//"Goto CCD3 Pos({0},{1})"
	public const  int INFO_6001059 = 6001059;
	/// <summary>"移動到CCD3位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Goto CCD3 Pos({0},{1},{2})"
	public const  int INFO_6001060 = 6001060;
	/// <summary>"移動到CCD3位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Goto CCD3 Pos({0},{1},{2},{3})"
	public const  int INFO_6001061 = 6001061;
	/// <summary>"移動到CCD3位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Goto CCD3 Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6001062 = 6001062;
	/// <summary>"移動到CCD4位置({0},{1})"</summary>
	/// <remarks></remarks>
		//"Goto CCD4 Pos({0},{1})"
	public const  int INFO_6001063 = 6001063;
	/// <summary>"移動到CCD4位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Goto CCD4 Pos({0},{1},{2})"
	public const  int INFO_6001064 = 6001064;
	/// <summary>"移動到CCD4位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Goto CCD4 Pos({0},{1},{2},{3})"
	public const  int INFO_6001065 = 6001065;
	/// <summary>"移動到CCD4位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Goto CCD4 Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6001066 = 6001066;
	/// <summary>馬達激磁.</summary>
	/// <remarks></remarks>
		//Servo On.
	public const  int INFO_6001067 = 6001067;
	/// <summary>馬達消磁.</summary>
	/// <remarks></remarks>
		//Servo Off.
	public const  int INFO_6001068 = 6001068;
	/// <summary>"移動到Height Sensor位置({0},{1})"</summary>
	/// <remarks></remarks>
		//"Goto Height Sensor Pos({0},{1})"
	public const  int INFO_6001069 = 6001069;
	/// <summary>"移動到Height Sensor位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Goto Height Sensor Pos({0},{1},{2})"
	public const  int INFO_6001070 = 6001070;
	/// <summary>"移動到Height Sensor位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Goto Height Sensor Pos({0},{1},{2},{3})"
	public const  int INFO_6001071 = 6001071;
	/// <summary>"移動到Height Sensor位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Goto Height Sensor Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6001072 = 6001072;
	/// <summary>蜂鳴器使用.</summary>
	/// <remarks></remarks>
		//Buzzer Use.
	public const  int INFO_6001073 = 6001073;
	/// <summary>蜂鳴器靜音.</summary>
	/// <remarks></remarks>
		//Buzzer Mute.
	public const  int INFO_6001074 = 6001074;
	/// <summary>閥加熱中</summary>
	/// <remarks></remarks>
		//Valve Heating.
	public const  int INFO_6001075 = 6001075;
	/// <summary>軸索引讀取成功.</summary>
	/// <remarks></remarks>
		//Axis Index Load OK.
	public const  int INFO_6002000 = 6002000;
	/// <summary>軸索引儲存成功.</summary>
	/// <remarks></remarks>
		//Axis Index Save OK.
	public const  int INFO_6002001 = 6002001;
	/// <summary>系統參數讀取成功.</summary>
	/// <remarks></remarks>
		//System Parameter Load OK.
	public const  int INFO_6002002 = 6002002;
	/// <summary>系統參數儲存成功.</summary>
	/// <remarks></remarks>
		//System Parameter Save OK.
	public const  int INFO_6002003 = 6002003;
	/// <summary>膠材參數讀取成功.</summary>
	/// <remarks></remarks>
		//Paste Parameter Load OK
	public const  int INFO_6002004 = 6002004;
	/// <summary>膠材參數儲存成功.</summary>
	/// <remarks></remarks>
		//Paste Parameter Save OK
	public const  int INFO_6002005 = 6002005;
	/// <summary>膠閥參數讀取成功.</summary>
	/// <remarks></remarks>
		//Valve Parameter Load OK.
	public const  int INFO_6002006 = 6002006;
	/// <summary>膠閥參數儲存成功.</summary>
	/// <remarks></remarks>
		//Valve Parameter Save OK.
	public const  int INFO_6002007 = 6002007;
	/// <summary>IO卡參數讀取成功.</summary>
	/// <remarks></remarks>
		//IO Card Load OK.
	public const  int INFO_6002008 = 6002008;
	/// <summary>IO卡參數儲存成功.</summary>
	/// <remarks></remarks>
		//IO Card Save OK.
	public const  int INFO_6002009 = 6002009;
	/// <summary>運動控制卡參數讀取成功.</summary>
	/// <remarks></remarks>
		//Motion Card Load OK.
	public const  int INFO_6002010 = 6002010;
	/// <summary>運動控制卡參數儲存成功.</summary>
	/// <remarks></remarks>
		//Motion Card Save OK.
	public const  int INFO_6002011 = 6002011;
	/// <summary>AI卡參數讀取成功.</summary>
	/// <remarks></remarks>
		//AI Card Load OK.
	public const  int INFO_6002012 = 6002012;
	/// <summary>AI卡參數儲存成功.</summary>
	/// <remarks></remarks>
		//AI Card Save OK.
	public const  int INFO_6002013 = 6002013;
	/// <summary>AO參數讀取成功.</summary>
	/// <remarks></remarks>
		//AO Card Load OK.
	public const  int INFO_6002014 = 6002014;
	/// <summary>AO參數儲存成功.</summary>
	/// <remarks></remarks>
		//AO Card Save OK.
	public const  int INFO_6002015 = 6002015;
	/// <summary>DI卡參數讀取成功.</summary>
	/// <remarks></remarks>
		//DI Card Load OK.
	public const  int INFO_6002016 = 6002016;
	/// <summary>DI卡參數儲存成功.</summary>
	/// <remarks></remarks>
		//DI Card Save OK.
	public const  int INFO_6002017 = 6002017;
	/// <summary>DO參數讀取成功.</summary>
	/// <remarks></remarks>
		//DO Card Load OK.
	public const  int INFO_6002018 = 6002018;
	/// <summary>DO參數儲存成功.</summary>
	/// <remarks></remarks>
		//DO Card Save OK.
	public const  int INFO_6002019 = 6002019;
	/// <summary>Recipe讀檔成功!</summary>
	/// <remarks></remarks>
		//Recipe Load OK.
	public const  int INFO_6002020 = 6002020;
	/// <summary>Recipe存檔成功!</summary>
	/// <remarks></remarks>
		//Recipe Save OK.
	public const  int INFO_6002021 = 6002021;
	/// <summary>單步參數讀取成功.</summary>
	/// <remarks></remarks>
		//Step Parameter Load OK.
	public const  int INFO_6002022 = 6002022;
	/// <summary>單步參數儲存成功.</summary>
	/// <remarks></remarks>
		//Step Parameter Save OK.
	public const  int INFO_6002023 = 6002023;
	/// <summary>"膠材參數已存在,是否覆蓋?"</summary>
	/// <remarks></remarks>
		//"Paste Parameter already exists, overwrite?"
	public const  int INFO_6002024 = 6002024;
	/// <summary>"噴射閥參數已存在,是否覆蓋?"</summary>
	/// <remarks></remarks>
		//"Jet Valve Parameter already exists, overwrite?"
	public const  int INFO_6002025 = 6002025;
	/// <summary>"螺桿閥參數已存在,是否覆蓋?"</summary>
	/// <remarks></remarks>
		//"Auger Valve Parameter already exists, overwrite?"
	public const  int INFO_6002026 = 6002026;
	/// <summary>"氣壓閥參數已存在,是否覆蓋?"</summary>
	/// <remarks></remarks>
		//"Air Valve Parameter already exists, overwrite?"
	public const  int INFO_6002027 = 6002027;
	/// <summary>Recipe鎖定.</summary>
	/// <remarks></remarks>
		//Recipe Locked.
	public const  int INFO_6002028 = 6002028;
	/// <summary>Recipe解除鎖定.</summary>
	/// <remarks></remarks>
		//Recipe Unlocked.
	public const  int INFO_6002029 = 6002029;
	/// <summary>原自動清膠頻率:{0}真空除膠頻率:{1}</summary>
	/// <remarks></remarks>
		//Old Auto Clear Glue:{0} Auto Purge:{1}
	public const  int INFO_6002030 = 6002030;
	/// <summary>新自動清膠頻率:{0}真空除膠頻率:{1}</summary>
	/// <remarks></remarks>
		//New Auto Clear Glue:{0} Auto Purge:{1}
	public const  int INFO_6002031 = 6002031;
	/// <summary>原Purge時出膠:{0}</summary>
	/// <remarks></remarks>
		//Old Purge Jet On:{0}
	public const  int INFO_6002032 = 6002032;
	/// <summary>新Purge時出膠:{0}</summary>
	/// <remarks></remarks>
		//New Purge Jet On:{0}
	public const  int INFO_6002033 = 6002033;
	/// <summary>原觸發路徑延伸長度:{0}</summary>
	/// <remarks></remarks>
		//Old Trigger Over Path:{0}
	public const  int INFO_6002034 = 6002034;
	/// <summary>新觸發路徑延伸長度:{0}</summary>
	/// <remarks></remarks>
		//New Trigger Over Path:{0}
	public const  int INFO_6002035 = 6002035;
	/// <summary>原平行線觸發模式:{0}</summary>
	/// <remarks></remarks>
		//Old Trigger Parallel Mode:{0}
	public const  int INFO_6002036 = 6002036;
	/// <summary>新平行線觸發模式:{0}</summary>
	/// <remarks></remarks>
		//New Trigger Parallel Mode:{0}
	public const  int INFO_6002037 = 6002037;
	/// <summary>原觸發補償模式:{0}</summary>
	/// <remarks></remarks>
		//Old Trigger Compensation Mode:{0}
	public const  int INFO_6002038 = 6002038;
	/// <summary>新觸發補償模式:{0}</summary>
	/// <remarks></remarks>
		//New Trigger Compensation Mode:{0}
	public const  int INFO_6002039 = 6002039;
	/// <summary>原膠量補償模式:{0}</summary>
	/// <remarks></remarks>
		//Old Glue Compensation Mode:
	public const  int INFO_6002040 = 6002040;
	/// <summary>新膠量補償模式:{0}</summary>
	/// <remarks></remarks>
		//New Glue Compensation Mode:
	public const  int INFO_6002041 = 6002041;
	/// <summary>原系統參數如下所示:</summary>
	/// <remarks></remarks>
		//Old System Parameter Show as below:
	public const  int INFO_6002042 = 6002042;
	/// <summary>新系統參數如下所示:</summary>
	/// <remarks></remarks>
		//New System Parameter Show as below:
	public const  int INFO_6002043 = 6002043;
	/// <summary>原馬達參數如下所示:</summary>
	/// <remarks></remarks>
		//Old Motor Parameter Show as below:
	public const  int INFO_6002044 = 6002044;
	/// <summary>新馬達參數如下所示:</summary>
	/// <remarks></remarks>
		//New Motor Parameter Show as below:
	public const  int INFO_6002045 = 6002045;
	/// <summary>原使用者權限設定如下所示:</summary>
	/// <remarks></remarks>
		//Old User Authority is Shown as below:
	public const  int INFO_6002046 = 6002046;
	/// <summary>新使用者權限設定如下所示:</summary>
	/// <remarks></remarks>
		//New User Authority is Shown as below:
	public const  int INFO_6002047 = 6002047;
	/// <summary>原螺桿閥過電流判定 CT1:{0} CT2:{1}</summary>
	/// <remarks></remarks>
		//Old Auger CT1: {0} CT2: {1}
	public const  int INFO_6002048 = 6002048;
	/// <summary>新螺桿閥過電流判定 CT1:{0} CT2:{1}</summary>
	/// <remarks></remarks>
		//New Auger CT1: {0} CT2: {1}
	public const  int INFO_6002049 = 6002049;
	/// <summary>原產能自動穩定:{0}</summary>
	/// <remarks></remarks>
		//Old Auto Cycle Tuning:{0}
	public const  int INFO_6002050 = 6002050;
	/// <summary>新產能自動穩定:{0}</summary>
	/// <remarks></remarks>
		//New Auto Cycle Tuning:{0}
	public const  int INFO_6002051 = 6002051;
	/// <summary>原校正重試次數:{0} 容許誤差:{1}</summary>
	/// <remarks></remarks>
		//Old Calibration Retry Limit:{0} Accept Toleranec:{1}
	public const  int INFO_6002052 = 6002052;
	/// <summary>新校正重試次數:{0} 容許誤差:{1}</summary>
	/// <remarks></remarks>
		//New Calibration Retry Limit:{0} Accept Toleranec:{1}
	public const  int INFO_6002053 = 6002053;
	/// <summary>"儲存閥1測高位置({0},{1},{2}) Z極限:{3}"</summary>
	/// <remarks></remarks>
		//"Save Valve1 Pin Pos({0},{1},{2}) Z Limit: {3}"
	public const  int INFO_6002066 = 6002066;
	/// <summary>"儲存閥1測高位置({0},{1},{2},{3}) Z極限:{3}"</summary>
	/// <remarks></remarks>
		//"Save Valve1 Pin Pos({0},{1},{2},{3}) Z Limit: {3}"
	public const  int INFO_6002067 = 6002067;
	/// <summary>"儲存閥1測高位置({0},{1},{2},{3},{4}) Z極限:{3}"</summary>
	/// <remarks></remarks>
		//"Save Valve1 Pin Pos({0},{1},{2},{3},{4}) Z Limit: {3}"
	public const  int INFO_6002068 = 6002068;
	/// <summary>"儲存閥2測高位置({0},{1},{2}) Z極限:{3}"</summary>
	/// <remarks></remarks>
		//"Save Valve2 Pin Pos({0},{1},{2}) Z Limit: {3}"
	public const  int INFO_6002069 = 6002069;
	/// <summary>"儲存閥2測高位置({0},{1},{2},{3}) Z極限:{3}"</summary>
	/// <remarks></remarks>
		//"Save Valve2 Pin Pos({0},{1},{2},{3}) Z Limit: {3}"
	public const  int INFO_6002070 = 6002070;
	/// <summary>"儲存閥2測高位置({0},{1},{2},{3},{4}) Z極限:{3}"</summary>
	/// <remarks></remarks>
		//"Save Valve2 Pin Pos({0},{1},{2},{3},{4}) Z Limit: {3}"
	public const  int INFO_6002071 = 6002071;
	/// <summary>原擦膠參數間距:{0}mm次數限制:{1} 偏移量{2}mm氣壓開啟時間{3}ms</summary>
	/// <remarks></remarks>
		//Old Clear Glue Parameter Pitch:{0}mm CountLimit:{1} Offset:{2}mmAP On Time:{3}ms
	public const  int INFO_6002072 = 6002072;
	/// <summary>新擦膠參數間距:{0}mm次數限制:{1} 偏移量{2}mm氣壓開啟時間{3}ms</summary>
	/// <remarks></remarks>
		//New Clear Glue Parameter Pitch:{0}mm CountLimit:{1} Offset:{2}mmAP On Time:{3}ms
	public const  int INFO_6002073 = 6002073;
	/// <summary>原閥1形式: {0}</summary>
	/// <remarks></remarks>
		//Old Valve1 Type: {0}
	public const  int INFO_6002074 = 6002074;
	/// <summary>新閥1形式: {0}</summary>
	/// <remarks></remarks>
		//New Valve1 Type: {0}
	public const  int INFO_6002075 = 6002075;
	/// <summary>原閥2形式: {0}</summary>
	/// <remarks></remarks>
		//Old Valve2 Type: {0}
	public const  int INFO_6002076 = 6002076;
	/// <summary>新閥2形式: {0}</summary>
	/// <remarks></remarks>
		//New Valve2 Type: {0}
	public const  int INFO_6002077 = 6002077;
	/// <summary>原閥3形式: {0}</summary>
	/// <remarks></remarks>
		//Old Valve3 Type: {0}
	public const  int INFO_6002078 = 6002078;
	/// <summary>新閥3形式: {0}</summary>
	/// <remarks></remarks>
		//New Valve3 Type: {0}
	public const  int INFO_6002079 = 6002079;
	/// <summary>原閥4形式: {0}</summary>
	/// <remarks></remarks>
		//Old Valve4 Type: {0}
	public const  int INFO_6002080 = 6002080;
	/// <summary>新閥4形式: {0}</summary>
	/// <remarks></remarks>
		//New Valve4 Type: {0}
	public const  int INFO_6002081 = 6002081;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6002082 = 6002082;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6002083 = 6002083;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6002084 = 6002084;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6002085 = 6002085;
	/// <summary>DIO卡1初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize DIO Card1 OK.
	public const  int INFO_6003000 = 6003000;
	/// <summary>DIO卡1關卡成功.</summary>
	/// <remarks></remarks>
		//Close DIO Card1 OK.
	public const  int INFO_6003001 = 6003001;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6003002 = 6003002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6003003 = 6003003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6003004 = 6003004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6003005 = 6003005;
	/// <summary>DI卡1初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize DI Card1 OK.
	public const  int INFO_6004000 = 6004000;
	/// <summary>DI卡1關卡成功.</summary>
	/// <remarks></remarks>
		//Close DI Card1 OK.
	public const  int INFO_6004001 = 6004001;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6004002 = 6004002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6004003 = 6004003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6004004 = 6004004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6004005 = 6004005;
	/// <summary>DO卡1初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize DO Card1 OK.
	public const  int INFO_6005000 = 6005000;
	/// <summary>DO卡1關卡成功.</summary>
	/// <remarks></remarks>
		//Close DO Card1 OK.
	public const  int INFO_6005001 = 6005001;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6005002 = 6005002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6005003 = 6005003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6005004 = 6005004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6005005 = 6005005;
	/// <summary>AIO卡1初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize AIO Card1 OK.
	public const  int INFO_6006000 = 6006000;
	/// <summary>AIO卡1關卡成功.</summary>
	/// <remarks></remarks>
		//Close AIO Card1 OK.
	public const  int INFO_6006001 = 6006001;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6006002 = 6006002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6006003 = 6006003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6006004 = 6006004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6006005 = 6006005;
	/// <summary>AI卡1初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize AI Card1 OK.
	public const  int INFO_6007000 = 6007000;
	/// <summary>AI卡1關卡成功.</summary>
	/// <remarks></remarks>
		//Close AI Card1 OK.
	public const  int INFO_6007001 = 6007001;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6007002 = 6007002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6007003 = 6007003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6007004 = 6007004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6007005 = 6007005;
	/// <summary>AO卡1初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize AO Card1 OK.
	public const  int INFO_6008000 = 6008000;
	/// <summary>AO卡1關卡成功.</summary>
	/// <remarks></remarks>
		//Close AO Card1 OK.
	public const  int INFO_6008001 = 6008001;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6008002 = 6008002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6008003 = 6008003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6008004 = 6008004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6008005 = 6008005;
	/// <summary>運動控制卡1初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize Motion Card1 OK.
	public const  int INFO_6009000 = 6009000;
	/// <summary>運動控制卡1關卡成功.</summary>
	/// <remarks></remarks>
		//Close Motion Card1 OK.
	public const  int INFO_6009001 = 6009001;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6009002 = 6009002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6009003 = 6009003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6009004 = 6009004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6009005 = 6009005;
	/// <summary>COM元件初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize COM Port OK.
	public const  int INFO_6010000 = 6010000;
	/// <summary>COM元件關閉成功.</summary>
	/// <remarks></remarks>
		//Close COM Port OK.
	public const  int INFO_6010001 = 6010001;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6010002 = 6010002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6010003 = 6010003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6010004 = 6010004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6010005 = 6010005;
	/// <summary>網路元件初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize Ethernet Port OK.
	public const  int INFO_6011000 = 6011000;
	/// <summary>網路元件關閉成功.</summary>
	/// <remarks></remarks>
		//Close Ethernet Port OK.
	public const  int INFO_6011001 = 6011001;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6011002 = 6011002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6011003 = 6011003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6011004 = 6011004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6011005 = 6011005;
	/// <summary>CCD1觸發</summary>
	/// <remarks></remarks>
		//Set CCD1 Trigger
	public const  int INFO_6012000 = 6012000;
	/// <summary>原CCD1自動校正取向高度:{0}</summary>
	/// <remarks></remarks>
		//Old Auto Calibration CCD1 Acquisition Pos Z:{0}
	public const  int INFO_6012001 = 6012001;
	/// <summary>新CCD1自動校正取向高度:{0}</summary>
	/// <remarks></remarks>
		//New Auto Calibration CCD1 Acquisition Pos Z:{0}
	public const  int INFO_6012002 = 6012002;
	/// <summary>CCD1轉換比例. A11:{0} A12:{1} A21:{2} A22:{3} B11:{4} B21:{5}</summary>
	/// <remarks></remarks>
		//CCD1 Scale Transorm. A11:{0} A12:{1} A21:{2} A22:{3} B11:{4} B21:{5}
	public const  int INFO_6012003 = 6012003;
	/// <summary>"影像偏移量({0},{1})Pixel"</summary>
	/// <remarks></remarks>
		//"Image Offset({0},{1}) Pixel"
	public const  int INFO_6012004 = 6012004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6012005 = 6012005;
	/// <summary>CCD2觸發</summary>
	/// <remarks></remarks>
		//Set CCD2 Trigger
	public const  int INFO_6012100 = 6012100;
	/// <summary>原CCD2自動校正取向高度:{0}</summary>
	/// <remarks></remarks>
		//Old Auto Calibration CCD2 Acquisition Pos Z:{0}
	public const  int INFO_6012101 = 6012101;
	/// <summary>新CCD2自動校正取向高度:{0}</summary>
	/// <remarks></remarks>
		//New Auto Calibration CCD2 Acquisition Pos Z:{0}
	public const  int INFO_6012102 = 6012102;
	/// <summary>CCD2轉換比例. A11:{0} A12:{1} A21:{2} A22:{3} B11:{4} B21:{5}</summary>
	/// <remarks></remarks>
		//CCD2 Scale Transorm. A11:{0} A12:{1} A21:{2} A22:{3} B11:{4} B21:{5}
	public const  int INFO_6012103 = 6012103;
	/// <summary>"影像偏移量({0},{1})Pixel"</summary>
	/// <remarks></remarks>
		//"Image Offset({0},{1}) Pixel"
	public const  int INFO_6012104 = 6012104;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6012105 = 6012105;
	/// <summary>CCD3觸發</summary>
	/// <remarks></remarks>
		//Set CCD3 Trigger
	public const  int INFO_6012200 = 6012200;
	/// <summary>原CCD3自動校正取向高度:{0}</summary>
	/// <remarks></remarks>
		//Old Auto Calibration CCD3 Acquisition Pos Z:{0}
	public const  int INFO_6012201 = 6012201;
	/// <summary>新CCD3自動校正取向高度:{0}</summary>
	/// <remarks></remarks>
		//New Auto Calibration CCD3 Acquisition Pos Z:{0}
	public const  int INFO_6012202 = 6012202;
	/// <summary>CCD3轉換比例. A11:{0} A12:{1} A21:{2} A22:{3} B11:{4} B21:{5}</summary>
	/// <remarks></remarks>
		//CCD3 Scale Transorm. A11:{0} A12:{1} A21:{2} A22:{3} B11:{4} B21:{5}
	public const  int INFO_6012203 = 6012203;
	/// <summary>"影像偏移量({0},{1})Pixel"</summary>
	/// <remarks></remarks>
		//"Image Offset({0},{1}) Pixel"
	public const  int INFO_6012204 = 6012204;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6012205 = 6012205;
	/// <summary>CCD4觸發</summary>
	/// <remarks></remarks>
		//Set CCD4 Trigger
	public const  int INFO_6012300 = 6012300;
	/// <summary>原CCD4自動校正取向高度:{0}</summary>
	/// <remarks></remarks>
		//Old Auto Calibration CCD4 Acquisition Pos Z:{0}
	public const  int INFO_6012301 = 6012301;
	/// <summary>新CCD4自動校正取向高度:{0}</summary>
	/// <remarks></remarks>
		//New Auto Calibration CCD4 Acquisition Pos Z:{0}
	public const  int INFO_6012302 = 6012302;
	/// <summary>CCD4轉換比例. A11:{0} A12:{1} A21:{2} A22:{3} B11:{4} B21:{5}</summary>
	/// <remarks></remarks>
		//CCD4 Scale Transorm. A11:{0} A12:{1} A21:{2} A22:{3} B11:{4} B21:{5}
	public const  int INFO_6012303 = 6012303;
	/// <summary>"影像偏移量({0},{1})Pixel"</summary>
	/// <remarks></remarks>
		//"Image Offset({0},{1}) Pixel"
	public const  int INFO_6012304 = 6012304;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6012305 = 6012305;
	/// <summary>原雷射模型監控:{0}</summary>
	/// <remarks></remarks>
		//Old Monitor Laser Model: {0}
	public const  int INFO_6014000 = 6014000;
	/// <summary>新雷射模型監控:{0}</summary>
	/// <remarks></remarks>
		//New Monitor Laser Model: {0}
	public const  int INFO_6014001 = 6014001;
	/// <summary>"原雷射測高模組1偏移量({0},{1})"</summary>
	/// <remarks></remarks>
		//"Old Laser Interferometer1 Offset({0},{1})"
	public const  int INFO_6014002 = 6014002;
	/// <summary>"新雷射測高模組1偏移量({0},{1})"</summary>
	/// <remarks></remarks>
		//"New Laser Interferometer1 Offset({0},{1})"
	public const  int INFO_6014003 = 6014003;
	/// <summary>"原測高參數 重測次數:{0},容許誤差:{1}"</summary>
	/// <remarks></remarks>
		//"Old Parameter Count Limit:{0}, Accept Tolerance:{1}"
	public const  int INFO_6014004 = 6014004;
	/// <summary>"新測高參數 重測次數:{0},容許誤差:{1}"</summary>
	/// <remarks></remarks>
		//"New Parameter Count Limit:{0}, Accept Tolerance:{1}"
	public const  int INFO_6014005 = 6014005;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6014006 = 6014006;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6014007 = 6014007;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6014008 = 6014008;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6014009 = 6014009;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6014010 = 6014010;
	/// <summary>微量天平1初始化完成.</summary>
	/// <remarks></remarks>
		//Initialize Scale1 OK.
	public const  int INFO_6015000 = 6015000;
	/// <summary>微量天平1關閉完成.</summary>
	/// <remarks></remarks>
		//Close Scale1 OK.
	public const  int INFO_6015001 = 6015001;
	/// <summary>微量天平2初始化完成.</summary>
	/// <remarks></remarks>
		//Initialize Scale2 OK.
	public const  int INFO_6015002 = 6015002;
	/// <summary>微量天平2關閉完成.</summary>
	/// <remarks></remarks>
		//Close Scale2 OK.
	public const  int INFO_6015003 = 6015003;
	/// <summary>原天秤1參數 X1:{0} X2:{1} Y1:{2} Y2:{3} StableTime:{4}Sec Mode:{5}</summary>
	/// <remarks></remarks>
		//Old Scale1 Parameter X1:{0} X2:{1} Y1:{2} Y2:{3} StableTime:{4}Sec Mode:{5}
	public const  int INFO_6015004 = 6015004;
	/// <summary>新天秤1參數 X1:{0} X2:{1} Y1:{2} Y2:{3} StableTime:{4}Sec Mode:{5}</summary>
	/// <remarks></remarks>
		//New Scale1 Parameter X1:{0} X2:{1} Y1:{2} Y2:{3} StableTime:{4}Sec Mode:{5}
	public const  int INFO_6015005 = 6015005;
	/// <summary>原天秤2參數 X1:{0} X2:{1} Y1:{2} Y2:{3} StableTime:{4}Sec Mode:{5}</summary>
	/// <remarks></remarks>
		//Old Scale2 Parameter X1:{0} X2:{1} Y1:{2} Y2:{3} StableTime:{4}Sec Mode:{5}
	public const  int INFO_6015006 = 6015006;
	/// <summary>新天秤2參數 X1:{0} X2:{1} Y1:{2} Y2:{3} StableTime:{4}Sec Mode:{5}</summary>
	/// <remarks></remarks>
		//New Scale2 Parameter X1:{0} X2:{1} Y1:{2} Y2:{3} StableTime:{4}Sec Mode:{5}
	public const  int INFO_6015007 = 6015007;
	/// <summary>天平1重量對氣壓控制: {0}</summary>
	/// <remarks></remarks>
		//Scale1 Weight-AP {0}
	public const  int INFO_6015008 = 6015008;
	/// <summary>天平1重量對點數控制: {0}</summary>
	/// <remarks></remarks>
		//Scale1 Weight-Pts {0}
	public const  int INFO_6015009 = 6015009;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6015010 = 6015010;
	/// <summary>FMCS1初始化完成!</summary>
	/// <remarks></remarks>
		//Initialize FMCS1 OK!
	public const  int INFO_6017000 = 6017000;
	/// <summary>FMCS1開始記錄.</summary>
	/// <remarks></remarks>
		//FMCS1 Record Start.
	public const  int INFO_6017001 = 6017001;
	/// <summary>FMCS1結束記錄.</summary>
	/// <remarks></remarks>
		//FMCS1 Record End.
	public const  int INFO_6017002 = 6017002;
	/// <summary>FMCS1取得資料</summary>
	/// <remarks></remarks>
		//FMCS1 Get Data
	public const  int INFO_6017003 = 6017003;
	/// <summary>FMCS1流量對氣壓控制: {0}</summary>
	/// <remarks></remarks>
		//FMCS1 Flow-AP: {0}
	public const  int INFO_6017004 = 6017004;
	/// <summary>FMCS1流量對點數控制: {0}</summary>
	/// <remarks></remarks>
		//FMCS1 Flow-Pts: {0}
	public const  int INFO_6017005 = 6017005;
	/// <summary>FMCS1體積對氣壓控制: {0}</summary>
	/// <remarks></remarks>
		//FMCS1 Volume-AP: {0}
	public const  int INFO_6017006 = 6017006;
	/// <summary>FMCS1體積對點數控制: {0}</summary>
	/// <remarks></remarks>
		//FMCS1 Volume-Pts: {0}
	public const  int INFO_6017007 = 6017007;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6017008 = 6017008;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6017009 = 6017009;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6017010 = 6017010;
	/// <summary>FMCS2初始化完成!</summary>
	/// <remarks></remarks>
		//Initialize FMCS2 OK!
	public const  int INFO_6017100 = 6017100;
	/// <summary>FMCS2開始記錄.</summary>
	/// <remarks></remarks>
		//FMCS2 Record Start.
	public const  int INFO_6017101 = 6017101;
	/// <summary>FMCS2結束記錄.</summary>
	/// <remarks></remarks>
		//FMCS2 Record End.
	public const  int INFO_6017102 = 6017102;
	/// <summary>FMCS2取得資料</summary>
	/// <remarks></remarks>
		//FMCS2 Get Data
	public const  int INFO_6017103 = 6017103;
	/// <summary>FMCS2流量對氣壓控制: {0}</summary>
	/// <remarks></remarks>
		//FMCS2 Flow-AP: {0}
	public const  int INFO_6017104 = 6017104;
	/// <summary>FMCS2流量對點數控制: {0}</summary>
	/// <remarks></remarks>
		//FMCS2 Flow-Pts: {0}
	public const  int INFO_6017105 = 6017105;
	/// <summary>FMCS2體積對氣壓控制: {0}</summary>
	/// <remarks></remarks>
		//FMCS2 Volume-AP: {0}
	public const  int INFO_6017106 = 6017106;
	/// <summary>FMCS2體積對點數控制: {0}</summary>
	/// <remarks></remarks>
		//FMCS2 Volume-Pts: {0}
	public const  int INFO_6017107 = 6017107;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6017108 = 6017108;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6017109 = 6017109;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6017110 = 6017110;
	/// <summary>FMCS3初始化完成!</summary>
	/// <remarks></remarks>
		//Initialize FMCS3 OK!
	public const  int INFO_6017200 = 6017200;
	/// <summary>FMCS3開始記錄.</summary>
	/// <remarks></remarks>
		//FMCS3 Record Start.
	public const  int INFO_6017201 = 6017201;
	/// <summary>FMCS3結束記錄.</summary>
	/// <remarks></remarks>
		//FMCS3 Record End.
	public const  int INFO_6017202 = 6017202;
	/// <summary>FMCS3取得資料</summary>
	/// <remarks></remarks>
		//FMCS3 Get Data
	public const  int INFO_6017203 = 6017203;
	/// <summary>FMCS3流量對氣壓控制: {0}</summary>
	/// <remarks></remarks>
		//FMCS3 Flow-AP: {0}
	public const  int INFO_6017204 = 6017204;
	/// <summary>FMCS3流量對點數控制: {0}</summary>
	/// <remarks></remarks>
		//FMCS3 Flow-Pts: {0}
	public const  int INFO_6017205 = 6017205;
	/// <summary>FMCS3體積對氣壓控制: {0}</summary>
	/// <remarks></remarks>
		//FMCS3 Volume-AP: {0}
	public const  int INFO_6017206 = 6017206;
	/// <summary>FMCS3體積對點數控制: {0}</summary>
	/// <remarks></remarks>
		//FMCS3 Volume-Pts: {0}
	public const  int INFO_6017207 = 6017207;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6017208 = 6017208;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6017209 = 6017209;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6017210 = 6017210;
	/// <summary>FMCS4初始化完成!</summary>
	/// <remarks></remarks>
		//Initialize FMCS4 OK!
	public const  int INFO_6017300 = 6017300;
	/// <summary>FMCS4開始記錄.</summary>
	/// <remarks></remarks>
		//FMCS4 Record Start.
	public const  int INFO_6017301 = 6017301;
	/// <summary>FMCS4結束記錄.</summary>
	/// <remarks></remarks>
		//FMCS4 Record End.
	public const  int INFO_6017302 = 6017302;
	/// <summary>FMCS4取得資料</summary>
	/// <remarks></remarks>
		//FMCS4 Get Data
	public const  int INFO_6017303 = 6017303;
	/// <summary>FMCS4流量對氣壓控制: {0}</summary>
	/// <remarks></remarks>
		//FMCS4 Flow-AP: {0}
	public const  int INFO_6017304 = 6017304;
	/// <summary>FMCS4流量對點數控制: {0}</summary>
	/// <remarks></remarks>
		//FMCS4 Flow-Pts: {0}
	public const  int INFO_6017305 = 6017305;
	/// <summary>FMCS4體積對氣壓控制: {0}</summary>
	/// <remarks></remarks>
		//FMCS4 Volume-AP: {0}
	public const  int INFO_6017306 = 6017306;
	/// <summary>FMCS4體積對點數控制: {0}</summary>
	/// <remarks></remarks>
		//FMCS4 Volume-Pts: {0}
	public const  int INFO_6017307 = 6017307;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6017308 = 6017308;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6017309 = 6017309;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6017310 = 6017310;
	/// <summary>原閥1加熱模式:{0}</summary>
	/// <remarks></remarks>
		//Old Valve1 Heat Mode:{0}
	public const  int INFO_6018000 = 6018000;
	/// <summary>新閥1加熱模式:{0}</summary>
	/// <remarks></remarks>
		//New Valve1 Heat Mode:{0}
	public const  int INFO_6018001 = 6018001;
	/// <summary>原閥2加熱模式:{0}</summary>
	/// <remarks></remarks>
		//Old Valve2 Heat Mode:{0}
	public const  int INFO_6018002 = 6018002;
	/// <summary>新閥2加熱模式:{0}</summary>
	/// <remarks></remarks>
		//New Valve2 Heat Mode:{0}
	public const  int INFO_6018003 = 6018003;
	/// <summary>原閥3加熱模式:{0}</summary>
	/// <remarks></remarks>
		//Old Valve3 Heat Mode:{0}
	public const  int INFO_6018004 = 6018004;
	/// <summary>新閥3加熱模式:{0}</summary>
	/// <remarks></remarks>
		//New Valve3 Heat Mode:{0}
	public const  int INFO_6018005 = 6018005;
	/// <summary>原閥4加熱模式:{0}</summary>
	/// <remarks></remarks>
		//Old Valve4 Heat Mode:{0}
	public const  int INFO_6018006 = 6018006;
	/// <summary>新閥4加熱模式:{0}</summary>
	/// <remarks></remarks>
		//New Valve4 Heat Mode:{0}
	public const  int INFO_6018007 = 6018007;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6018008 = 6018008;
	/// <summary>原閥1Purge時間:{0}ms Cycle Time:{1}ms</summary>
	/// <remarks></remarks>
		//Old Valve1 Purge Time:{0}ms Cycle Time:{1}ms
	public const  int INFO_6019000 = 6019000;
	/// <summary>新閥1Purge時間:{0}ms Cycle Time:{1}ms</summary>
	/// <remarks></remarks>
		//New Valve1 Purge Time:{0}ms Cycle Time:{1}ms
	public const  int INFO_6019001 = 6019001;
	/// <summary>"原閥1Purge位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve1 Purge Pos({0},{1},{2})"
	public const  int INFO_6019002 = 6019002;
	/// <summary>"新閥1Purge位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve1 Purge Pos({0},{1},{2})"
	public const  int INFO_6019003 = 6019003;
	/// <summary>"原閥1Purge位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve1 Purge Pos({0},{1},{2},{3})"
	public const  int INFO_6019004 = 6019004;
	/// <summary>"新閥1Purge位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve1 Purge Pos({0},{1},{2},{3})"
	public const  int INFO_6019005 = 6019005;
	/// <summary>"原閥1Purge位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve1 Purge Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019006 = 6019006;
	/// <summary>"新閥1Purge位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve1 Purge Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019007 = 6019007;
	/// <summary>"原閥1安全位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve1 SafePos({0},{1},{2})"
	public const  int INFO_6019008 = 6019008;
	/// <summary>"新閥1安全位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve1 SafePos({0},{1},{2})"
	public const  int INFO_6019009 = 6019009;
	/// <summary>"原閥1安全位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve1 SafePos({0},{1},{2},{3})"
	public const  int INFO_6019010 = 6019010;
	/// <summary>"新閥1安全位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve1 SafePos({0},{1},{2},{3})"
	public const  int INFO_6019011 = 6019011;
	/// <summary>"原閥1安全位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve1 SafePos({0},{1},{2},{3},{4})"
	public const  int INFO_6019012 = 6019012;
	/// <summary>"新閥1安全位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve1 SafePos({0},{1},{2},{3},{4})"
	public const  int INFO_6019013 = 6019013;
	/// <summary>"移動到閥1位置({0},{1})"</summary>
	/// <remarks></remarks>
		//"Goto Valve1 Pos({0},{1})"
	public const  int INFO_6019014 = 6019014;
	/// <summary>"移動到閥1位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Goto Valve1 Pos({0},{1},{2})"
	public const  int INFO_6019015 = 6019015;
	/// <summary>"移動到閥1位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Goto Valve1 Pos({0},{1},{2},{3})"
	public const  int INFO_6019016 = 6019016;
	/// <summary>"移動到閥1位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Goto Valve1 Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019017 = 6019017;
	/// <summary>原閥1測高位置({0}{1})</summary>
	/// <remarks></remarks>
		//"Old Valve1 Pin Pos({0},{1})"
	public const  int INFO_6019018 = 6019018;
	/// <summary>新閥1測高位置({0}{1})</summary>
	/// <remarks></remarks>
		//"New Valve1 Pin Pos({0},{1})"
	public const  int INFO_6019019 = 6019019;
	/// <summary>"原閥1測高位置({0}{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve1 Pin Pos({0},{1},{2})"
	public const  int INFO_6019020 = 6019020;
	/// <summary>"新閥1測高位置({0}{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve1 Pin Pos({0},{1},{2})"
	public const  int INFO_6019021 = 6019021;
	/// <summary>"原閥1測高位置({0}{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve1 Pin Pos({0},{1},{2},{3})"
	public const  int INFO_6019022 = 6019022;
	/// <summary>"新閥1測高位置({0}{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve1 Pin Pos({0},{1},{2},{3})"
	public const  int INFO_6019023 = 6019023;
	/// <summary>"原閥1測高位置({0}{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve1 Pin Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019024 = 6019024;
	/// <summary>"新閥1測高位置({0}{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve1 Pin Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019025 = 6019025;
	/// <summary>"原閥1校正位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve1 Calibration Pos({0},{1},{2})"
	public const  int INFO_6019026 = 6019026;
	/// <summary>"新閥1校正位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve1 Calibration Pos({0},{1},{2})"
	public const  int INFO_6019027 = 6019027;
	/// <summary>"原閥1校正位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve1 Calibration Pos({0},{1},{2},{3})"
	public const  int INFO_6019028 = 6019028;
	/// <summary>"新閥1校正位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve1 Calibration Pos({0},{1},{2},{3})"
	public const  int INFO_6019029 = 6019029;
	/// <summary>"原閥1校正位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve1 Calibration Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019030 = 6019030;
	/// <summary>"新閥1校正位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve1 Calibration Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019031 = 6019031;
	/// <summary>"原閥1換膠位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve1 Change Glue Pos({0},{1},{2})"
	public const  int INFO_6019032 = 6019032;
	/// <summary>"新閥1換膠位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve1 Change Glue Pos({0},{1},{2})"
	public const  int INFO_6019033 = 6019033;
	/// <summary>"原閥1換膠位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve1 Change Glue Pos({0},{1},{2},{3})"
	public const  int INFO_6019034 = 6019034;
	/// <summary>"新閥1換膠位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve1 Change Glue Pos({0},{1},{2},{3})"
	public const  int INFO_6019035 = 6019035;
	/// <summary>"原閥1換膠位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve1 Change Glue Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019036 = 6019036;
	/// <summary>"新閥1換膠位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve1 Change Glue Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019037 = 6019037;
	/// <summary>"原閥1清膠位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve1 Clear Glue Pos({0},{1},{2})"
	public const  int INFO_6019038 = 6019038;
	/// <summary>"新閥1清膠位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve1 Clear Glue Pos({0},{1},{2})"
	public const  int INFO_6019039 = 6019039;
	/// <summary>"原閥1清膠位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve1 Clear Glue Pos({0},{1},{2},{3})"
	public const  int INFO_6019040 = 6019040;
	/// <summary>"新閥1清膠位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve1 Clear Glue Pos({0},{1},{2},{3})"
	public const  int INFO_6019041 = 6019041;
	/// <summary>"原閥1清膠位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve1 Clear Glue Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019042 = 6019042;
	/// <summary>"新閥1清膠位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve1 Clear Glue Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019043 = 6019043;
	/// <summary>原閥1校正場景編號:{0}</summary>
	/// <remarks></remarks>
		//Old Valve1 Calibration SceneID:{0}
	public const  int INFO_6019044 = 6019044;
	/// <summary>新閥1校正場景編號:{0}</summary>
	/// <remarks></remarks>
		//New Valve1 Calibration SceneID:{0}
	public const  int INFO_6019045 = 6019045;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6019046 = 6019046;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6019047 = 6019047;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6019048 = 6019048;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6019049 = 6019049;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6019050 = 6019050;
	/// <summary>原閥2Purge時間:{0}ms Cycle Time:{1}ms</summary>
	/// <remarks></remarks>
		//Old Valve2 Purge Time:{0}ms Cycle Time:{1}ms
	public const  int INFO_6019100 = 6019100;
	/// <summary>新閥2Purge時間:{0}ms Cycle Time:{1}ms</summary>
	/// <remarks></remarks>
		//New Valve2 Purge Time:{0}ms Cycle Time:{1}ms
	public const  int INFO_6019101 = 6019101;
	/// <summary>"原閥2Purge位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve2 Purge Pos({0},{1},{2})"
	public const  int INFO_6019102 = 6019102;
	/// <summary>"新閥2Purge位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve2 Purge Pos({0},{1},{2})"
	public const  int INFO_6019103 = 6019103;
	/// <summary>"原閥2Purge位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve2 Purge Pos({0},{1},{2},{3})"
	public const  int INFO_6019104 = 6019104;
	/// <summary>"新閥2Purge位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve2 Purge Pos({0},{1},{2},{3})"
	public const  int INFO_6019105 = 6019105;
	/// <summary>"原閥2Purge位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve2 Purge Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019106 = 6019106;
	/// <summary>"新閥2Purge位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve2 Purge Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019107 = 6019107;
	/// <summary>"原閥2安全位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve2 SafePos({0},{1},{2})"
	public const  int INFO_6019108 = 6019108;
	/// <summary>"新閥2安全位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve2 SafePos({0},{1},{2})"
	public const  int INFO_6019109 = 6019109;
	/// <summary>"原閥2安全位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve2 SafePos({0},{1},{2},{3})"
	public const  int INFO_6019110 = 6019110;
	/// <summary>"新閥2安全位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve2 SafePos({0},{1},{2},{3})"
	public const  int INFO_6019111 = 6019111;
	/// <summary>"原閥2安全位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve2 SafePos({0},{1},{2},{3},{4})"
	public const  int INFO_6019112 = 6019112;
	/// <summary>"新閥2安全位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve2 SafePos({0},{1},{2},{3},{4})"
	public const  int INFO_6019113 = 6019113;
	/// <summary>"移動到閥2位置({0},{1})"</summary>
	/// <remarks></remarks>
		//"Goto Valve2 Pos({0},{1})"
	public const  int INFO_6019114 = 6019114;
	/// <summary>"移動到閥2位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Goto Valve2 Pos({0},{1},{2})"
	public const  int INFO_6019115 = 6019115;
	/// <summary>"移動到閥2位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Goto Valve2 Pos({0},{1},{2},{3})"
	public const  int INFO_6019116 = 6019116;
	/// <summary>"移動到閥2位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Goto Valve2 Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019117 = 6019117;
	/// <summary>原閥2測高位置({0}{1})</summary>
	/// <remarks></remarks>
		//"Old Valve2 Pin Pos({0},{1})"
	public const  int INFO_6019118 = 6019118;
	/// <summary>新閥2測高位置({0}{1})</summary>
	/// <remarks></remarks>
		//"New Valve2 Pin Pos({0},{1})"
	public const  int INFO_6019119 = 6019119;
	/// <summary>"原閥2測高位置({0}{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve2 Pin Pos({0},{1},{2})"
	public const  int INFO_6019120 = 6019120;
	/// <summary>"新閥2測高位置({0}{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve2 Pin Pos({0},{1},{2})"
	public const  int INFO_6019121 = 6019121;
	/// <summary>"原閥2測高位置({0}{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve2 Pin Pos({0},{1},{2},{3})"
	public const  int INFO_6019122 = 6019122;
	/// <summary>"新閥2測高位置({0}{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve2 Pin Pos({0},{1},{2},{3})"
	public const  int INFO_6019123 = 6019123;
	/// <summary>"原閥2測高位置({0}{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve2 Pin Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019124 = 6019124;
	/// <summary>"新閥2測高位置({0}{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve2 Pin Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019125 = 6019125;
	/// <summary>"原閥2校正位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve2 Calibration Pos({0},{1},{2})"
	public const  int INFO_6019126 = 6019126;
	/// <summary>"新閥2校正位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve2 Calibration Pos({0},{1},{2})"
	public const  int INFO_6019127 = 6019127;
	/// <summary>"原閥2校正位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve2 Calibration Pos({0},{1},{2},{3})"
	public const  int INFO_6019128 = 6019128;
	/// <summary>"新閥2校正位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve2 Calibration Pos({0},{1},{2},{3})"
	public const  int INFO_6019129 = 6019129;
	/// <summary>"原閥2校正位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve2 Calibration Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019130 = 6019130;
	/// <summary>"新閥2校正位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve2 Calibration Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019131 = 6019131;
	/// <summary>"原閥2換膠位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve2 Change Glue Pos({0},{1},{2})"
	public const  int INFO_6019132 = 6019132;
	/// <summary>"新閥2換膠位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve2 Change Glue Pos({0},{1},{2})"
	public const  int INFO_6019133 = 6019133;
	/// <summary>"原閥2換膠位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve2 Change Glue Pos({0},{1},{2},{3})"
	public const  int INFO_6019134 = 6019134;
	/// <summary>"新閥2換膠位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve2 Change Glue Pos({0},{1},{2},{3})"
	public const  int INFO_6019135 = 6019135;
	/// <summary>"原閥2換膠位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve2 Change Glue Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019136 = 6019136;
	/// <summary>"新閥2換膠位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve2 Change Glue Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019137 = 6019137;
	/// <summary>"原閥2清膠位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve2 Clear Glue Pos({0},{1},{2})"
	public const  int INFO_6019138 = 6019138;
	/// <summary>"新閥2清膠位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve2 Clear Glue Pos({0},{1},{2})"
	public const  int INFO_6019139 = 6019139;
	/// <summary>"原閥2清膠位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve2 Clear Glue Pos({0},{1},{2},{3})"
	public const  int INFO_6019140 = 6019140;
	/// <summary>"新閥2清膠位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve2 Clear Glue Pos({0},{1},{2},{3})"
	public const  int INFO_6019141 = 6019141;
	/// <summary>"原閥2清膠位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve2 Clear Glue Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019142 = 6019142;
	/// <summary>"新閥2清膠位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve2 Clear Glue Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019143 = 6019143;
	/// <summary>原閥2校正場景編號:{0}</summary>
	/// <remarks></remarks>
		//Old Valve2 Calibration SceneID:{0}
	public const  int INFO_6019144 = 6019144;
	/// <summary>新閥2校正場景編號:{0}</summary>
	/// <remarks></remarks>
		//New Valve2 Calibration SceneID:{0}
	public const  int INFO_6019145 = 6019145;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6019146 = 6019146;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6019147 = 6019147;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6019148 = 6019148;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6019149 = 6019149;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6019150 = 6019150;
	/// <summary>原閥3Purge時間:{0}ms Cycle Time:{1}ms</summary>
	/// <remarks></remarks>
		//Old Valve3 Purge Time:{0}ms Cycle Time:{1}ms
	public const  int INFO_6019200 = 6019200;
	/// <summary>新閥3Purge時間:{0}ms Cycle Time:{1}ms</summary>
	/// <remarks></remarks>
		//New Valve3 Purge Time:{0}ms Cycle Time:{1}ms
	public const  int INFO_6019201 = 6019201;
	/// <summary>"原閥3Purge位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve3 Purge Pos({0},{1},{2})"
	public const  int INFO_6019202 = 6019202;
	/// <summary>"新閥3Purge位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve3 Purge Pos({0},{1},{2})"
	public const  int INFO_6019203 = 6019203;
	/// <summary>"原閥3Purge位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve3 Purge Pos({0},{1},{2},{3})"
	public const  int INFO_6019204 = 6019204;
	/// <summary>"新閥3Purge位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve3 Purge Pos({0},{1},{2},{3})"
	public const  int INFO_6019205 = 6019205;
	/// <summary>"原閥3Purge位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve3 Purge Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019206 = 6019206;
	/// <summary>"新閥3Purge位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve3 Purge Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019207 = 6019207;
	/// <summary>"原閥3安全位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve3 SafePos({0},{1},{2})"
	public const  int INFO_6019208 = 6019208;
	/// <summary>"新閥3安全位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve3 SafePos({0},{1},{2})"
	public const  int INFO_6019209 = 6019209;
	/// <summary>"原閥3安全位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve3 SafePos({0},{1},{2},{3})"
	public const  int INFO_6019210 = 6019210;
	/// <summary>"新閥3安全位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve3 SafePos({0},{1},{2},{3})"
	public const  int INFO_6019211 = 6019211;
	/// <summary>"原閥3安全位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve3 SafePos({0},{1},{2},{3},{4})"
	public const  int INFO_6019212 = 6019212;
	/// <summary>"新閥3安全位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve3 SafePos({0},{1},{2},{3},{4})"
	public const  int INFO_6019213 = 6019213;
	/// <summary>"移動到閥3位置({0},{1})"</summary>
	/// <remarks></remarks>
		//"Goto Valve3 Pos({0},{1})"
	public const  int INFO_6019214 = 6019214;
	/// <summary>"移動到閥3位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Goto Valve3 Pos({0},{1},{2})"
	public const  int INFO_6019215 = 6019215;
	/// <summary>"移動到閥3位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Goto Valve3 Pos({0},{1},{2},{3})"
	public const  int INFO_6019216 = 6019216;
	/// <summary>"移動到閥3位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Goto Valve3 Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019217 = 6019217;
	/// <summary>原閥3測高位置({0}{1})</summary>
	/// <remarks></remarks>
		//"Old Valve3 Pin Pos({0},{1})"
	public const  int INFO_6019218 = 6019218;
	/// <summary>新閥3測高位置({0}{1})</summary>
	/// <remarks></remarks>
		//"New Valve3 Pin Pos({0},{1})"
	public const  int INFO_6019219 = 6019219;
	/// <summary>"原閥3測高位置({0}{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve3 Pin Pos({0},{1},{2})"
	public const  int INFO_6019220 = 6019220;
	/// <summary>"新閥3測高位置({0}{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve3 Pin Pos({0},{1},{2})"
	public const  int INFO_6019221 = 6019221;
	/// <summary>"原閥3測高位置({0}{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve3 Pin Pos({0},{1},{2},{3})"
	public const  int INFO_6019222 = 6019222;
	/// <summary>"新閥3測高位置({0}{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve3 Pin Pos({0},{1},{2},{3})"
	public const  int INFO_6019223 = 6019223;
	/// <summary>"原閥3測高位置({0}{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve3 Pin Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019224 = 6019224;
	/// <summary>"新閥3測高位置({0}{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve3 Pin Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019225 = 6019225;
	/// <summary>"原閥3校正位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve3 Calibration Pos({0},{1},{2})"
	public const  int INFO_6019226 = 6019226;
	/// <summary>"新閥3校正位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve3 Calibration Pos({0},{1},{2})"
	public const  int INFO_6019227 = 6019227;
	/// <summary>"原閥3校正位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve3 Calibration Pos({0},{1},{2},{3})"
	public const  int INFO_6019228 = 6019228;
	/// <summary>"新閥3校正位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve3 Calibration Pos({0},{1},{2},{3})"
	public const  int INFO_6019229 = 6019229;
	/// <summary>"原閥3校正位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve3 Calibration Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019230 = 6019230;
	/// <summary>"新閥3校正位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve3 Calibration Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019231 = 6019231;
	/// <summary>"原閥3換膠位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve3 Change Glue Pos({0},{1},{2})"
	public const  int INFO_6019232 = 6019232;
	/// <summary>"新閥3換膠位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve3 Change Glue Pos({0},{1},{2})"
	public const  int INFO_6019233 = 6019233;
	/// <summary>"原閥3換膠位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve3 Change Glue Pos({0},{1},{2},{3})"
	public const  int INFO_6019234 = 6019234;
	/// <summary>"新閥3換膠位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve3 Change Glue Pos({0},{1},{2},{3})"
	public const  int INFO_6019235 = 6019235;
	/// <summary>"原閥3換膠位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve3 Change Glue Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019236 = 6019236;
	/// <summary>"新閥3換膠位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve3 Change Glue Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019237 = 6019237;
	/// <summary>"原閥3清膠位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve3 Clear Glue Pos({0},{1},{2})"
	public const  int INFO_6019238 = 6019238;
	/// <summary>"新閥3清膠位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve3 Clear Glue Pos({0},{1},{2})"
	public const  int INFO_6019239 = 6019239;
	/// <summary>"原閥3清膠位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve3 Clear Glue Pos({0},{1},{2},{3})"
	public const  int INFO_6019240 = 6019240;
	/// <summary>"新閥3清膠位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve3 Clear Glue Pos({0},{1},{2},{3})"
	public const  int INFO_6019241 = 6019241;
	/// <summary>"原閥3清膠位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve3 Clear Glue Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019242 = 6019242;
	/// <summary>"新閥3清膠位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve3 Clear Glue Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019243 = 6019243;
	/// <summary>原閥3校正場景編號:{0}</summary>
	/// <remarks></remarks>
		//Old Valve3 Calibration SceneID:{0}
	public const  int INFO_6019244 = 6019244;
	/// <summary>新閥3校正場景編號:{0}</summary>
	/// <remarks></remarks>
		//New Valve3 Calibration SceneID:{0}
	public const  int INFO_6019245 = 6019245;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6019246 = 6019246;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6019247 = 6019247;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6019248 = 6019248;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6019249 = 6019249;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6019250 = 6019250;
	/// <summary>原閥4Purge時間:{0}ms Cycle Time:{1}ms</summary>
	/// <remarks></remarks>
		//Old Valve4 Purge Time:{0}ms Cycle Time:{1}ms
	public const  int INFO_6019300 = 6019300;
	/// <summary>新閥4Purge時間:{0}ms Cycle Time:{1}ms</summary>
	/// <remarks></remarks>
		//New Valve4 Purge Time:{0}ms Cycle Time:{1}ms
	public const  int INFO_6019301 = 6019301;
	/// <summary>"原閥4Purge位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve4 Purge Pos({0},{1},{2})"
	public const  int INFO_6019302 = 6019302;
	/// <summary>"新閥4Purge位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve4 Purge Pos({0},{1},{2})"
	public const  int INFO_6019303 = 6019303;
	/// <summary>"原閥4Purge位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve4 Purge Pos({0},{1},{2},{3})"
	public const  int INFO_6019304 = 6019304;
	/// <summary>"新閥4Purge位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve4 Purge Pos({0},{1},{2},{3})"
	public const  int INFO_6019305 = 6019305;
	/// <summary>"原閥4Purge位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve4 Purge Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019306 = 6019306;
	/// <summary>"新閥4Purge位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve4 Purge Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019307 = 6019307;
	/// <summary>"原閥4安全位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve4 SafePos({0},{1},{2})"
	public const  int INFO_6019308 = 6019308;
	/// <summary>"新閥4安全位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve4 SafePos({0},{1},{2})"
	public const  int INFO_6019309 = 6019309;
	/// <summary>"原閥4安全位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve4 SafePos({0},{1},{2},{3})"
	public const  int INFO_6019310 = 6019310;
	/// <summary>"新閥4安全位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve4 SafePos({0},{1},{2},{3})"
	public const  int INFO_6019311 = 6019311;
	/// <summary>"原閥4安全位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve4 SafePos({0},{1},{2},{3},{4})"
	public const  int INFO_6019312 = 6019312;
	/// <summary>"新閥4安全位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve4 SafePos({0},{1},{2},{3},{4})"
	public const  int INFO_6019313 = 6019313;
	/// <summary>"移動到閥4位置({0},{1})"</summary>
	/// <remarks></remarks>
		//"Goto Valve4 Pos({0},{1})"
	public const  int INFO_6019314 = 6019314;
	/// <summary>"移動到閥4位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Goto Valve4 Pos({0},{1},{2})"
	public const  int INFO_6019315 = 6019315;
	/// <summary>"移動到閥4位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Goto Valve4 Pos({0},{1},{2},{3})"
	public const  int INFO_6019316 = 6019316;
	/// <summary>"移動到閥4位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Goto Valve4 Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019317 = 6019317;
	/// <summary>原閥4測高位置({0}{1})</summary>
	/// <remarks></remarks>
		//"Old Valve4 Pin Pos({0},{1})"
	public const  int INFO_6019318 = 6019318;
	/// <summary>新閥4測高位置({0}{1})</summary>
	/// <remarks></remarks>
		//"New Valve4 Pin Pos({0},{1})"
	public const  int INFO_6019319 = 6019319;
	/// <summary>"原閥4測高位置({0}{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve4 Pin Pos({0},{1},{2})"
	public const  int INFO_6019320 = 6019320;
	/// <summary>"新閥4測高位置({0}{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve4 Pin Pos({0},{1},{2})"
	public const  int INFO_6019321 = 6019321;
	/// <summary>"原閥4測高位置({0}{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve4 Pin Pos({0},{1},{2},{3})"
	public const  int INFO_6019322 = 6019322;
	/// <summary>"新閥4測高位置({0}{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve4 Pin Pos({0},{1},{2},{3})"
	public const  int INFO_6019323 = 6019323;
	/// <summary>"原閥4測高位置({0}{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve4 Pin Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019324 = 6019324;
	/// <summary>"新閥4測高位置({0}{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve4 Pin Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019325 = 6019325;
	/// <summary>"原閥4校正位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve4 Calibration Pos({0},{1},{2})"
	public const  int INFO_6019326 = 6019326;
	/// <summary>"新閥4校正位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve4 Calibration Pos({0},{1},{2})"
	public const  int INFO_6019327 = 6019327;
	/// <summary>"原閥4校正位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve4 Calibration Pos({0},{1},{2},{3})"
	public const  int INFO_6019328 = 6019328;
	/// <summary>"新閥4校正位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve4 Calibration Pos({0},{1},{2},{3})"
	public const  int INFO_6019329 = 6019329;
	/// <summary>"原閥4校正位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve4 Calibration Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019330 = 6019330;
	/// <summary>"新閥4校正位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve4 Calibration Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019331 = 6019331;
	/// <summary>"原閥4換膠位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve4 Change Glue Pos({0},{1},{2})"
	public const  int INFO_6019332 = 6019332;
	/// <summary>"新閥4換膠位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve4 Change Glue Pos({0},{1},{2})"
	public const  int INFO_6019333 = 6019333;
	/// <summary>"原閥4換膠位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve4 Change Glue Pos({0},{1},{2},{3})"
	public const  int INFO_6019334 = 6019334;
	/// <summary>"新閥4換膠位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve4 Change Glue Pos({0},{1},{2},{3})"
	public const  int INFO_6019335 = 6019335;
	/// <summary>"原閥4換膠位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve4 Change Glue Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019336 = 6019336;
	/// <summary>"新閥4換膠位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve4 Change Glue Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019337 = 6019337;
	/// <summary>"原閥4清膠位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"Old Valve4 Clear Glue Pos({0},{1},{2})"
	public const  int INFO_6019338 = 6019338;
	/// <summary>"新閥4清膠位置({0},{1},{2})"</summary>
	/// <remarks></remarks>
		//"New Valve4 Clear Glue Pos({0},{1},{2})"
	public const  int INFO_6019339 = 6019339;
	/// <summary>"原閥4清膠位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"Old Valve4 Clear Glue Pos({0},{1},{2},{3})"
	public const  int INFO_6019340 = 6019340;
	/// <summary>"新閥4清膠位置({0},{1},{2},{3})"</summary>
	/// <remarks></remarks>
		//"New Valve4 Clear Glue Pos({0},{1},{2},{3})"
	public const  int INFO_6019341 = 6019341;
	/// <summary>"原閥4清膠位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"Old Valve4 Clear Glue Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019342 = 6019342;
	/// <summary>"新閥4清膠位置({0},{1},{2},{3},{4})"</summary>
	/// <remarks></remarks>
		//"New Valve4 Clear Glue Pos({0},{1},{2},{3},{4})"
	public const  int INFO_6019343 = 6019343;
	/// <summary>原閥4校正場景編號:{0}</summary>
	/// <remarks></remarks>
		//Old Valve4 Calibration SceneID:{0}
	public const  int INFO_6019344 = 6019344;
	/// <summary>新閥4校正場景編號:{0}</summary>
	/// <remarks></remarks>
		//New Valve4 Calibration SceneID:{0}
	public const  int INFO_6019345 = 6019345;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6019346 = 6019346;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6019347 = 6019347;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6019348 = 6019348;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6019349 = 6019349;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6019350 = 6019350;
	/// <summary>資料給傳送帶1:{0}</summary>
	/// <remarks></remarks>
		//Data To Conveyor1:{0}
	public const  int INFO_6020000 = 6020000;
	/// <summary>資料從傳送帶1:{0}</summary>
	/// <remarks></remarks>
		//Data From Conveyor1:{0}
	public const  int INFO_6020001 = 6020001;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6020002 = 6020002;
	/// <summary>程控光源1初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize Program Light1 OK.
	public const  int INFO_6022000 = 6022000;
	/// <summary>程控光源1關閉成功.</summary>
	/// <remarks></remarks>
		//Close Program Light1 OK.
	public const  int INFO_6022001 = 6022001;
	/// <summary>程控光源2初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize Program Light2 OK.
	public const  int INFO_6022002 = 6022002;
	/// <summary>程控光源2關閉成功.</summary>
	/// <remarks></remarks>
		//Close Program Light2 OK.
	public const  int INFO_6022003 = 6022003;
	/// <summary>程控光源3初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize Program Light3 OK.
	public const  int INFO_6022004 = 6022004;
	/// <summary>程控光源3關閉成功.</summary>
	/// <remarks></remarks>
		//Close Program Light3 OK.
	public const  int INFO_6022005 = 6022005;
	/// <summary>程控光源4初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize Program Light4 OK.
	public const  int INFO_6022006 = 6022006;
	/// <summary>程控光源4關閉成功.</summary>
	/// <remarks></remarks>
		//Close Program Light4 OK.
	public const  int INFO_6022007 = 6022007;
	/// <summary>程控光源5初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize Program Light5 OK.
	public const  int INFO_6022008 = 6022008;
	/// <summary>程控光源5關閉成功.</summary>
	/// <remarks></remarks>
		//Close Program Light5 OK.
	public const  int INFO_6022009 = 6022009;
	/// <summary>原設定CCD光源1亮度:{0}</summary>
	/// <remarks></remarks>
		//Old Set CCD Light1:{0}
	public const  int INFO_6022010 = 6022010;
	/// <summary>新設定CCD光源1亮度:{0}</summary>
	/// <remarks></remarks>
		//New Set CCD Light1:{0}
	public const  int INFO_6022011 = 6022011;
	/// <summary>原設定CCD光源2亮度:{0}</summary>
	/// <remarks></remarks>
		//Old Set CCD Light2:{0}
	public const  int INFO_6022012 = 6022012;
	/// <summary>新設定CCD光源2亮度:{0}</summary>
	/// <remarks></remarks>
		//New Set CCD Light2:{0}
	public const  int INFO_6022013 = 6022013;
	/// <summary>原設定CCD光源3亮度:{0}</summary>
	/// <remarks></remarks>
		//Old Set CCD Light3:{0}
	public const  int INFO_6022014 = 6022014;
	/// <summary>新設定CCD光源3亮度:{0}</summary>
	/// <remarks></remarks>
		//New Set CCD Light3:{0}
	public const  int INFO_6022015 = 6022015;
	/// <summary>原設定CCD光源4亮度:{0}</summary>
	/// <remarks></remarks>
		//Old Set CCD Light4:{0}
	public const  int INFO_6022016 = 6022016;
	/// <summary>新設定CCD光源4亮度:{0}</summary>
	/// <remarks></remarks>
		//New Set CCD Light4:{0}
	public const  int INFO_6022017 = 6022017;
	/// <summary>原設定CCD光源5亮度:{0}</summary>
	/// <remarks></remarks>
		//Old Set CCD Light5:{0}
	public const  int INFO_6022018 = 6022018;
	/// <summary>新設定CCD光源5亮度:{0}</summary>
	/// <remarks></remarks>
		//New Set CCD Light5:{0}
	public const  int INFO_6022019 = 6022019;
	/// <summary>原設定CCD光源6亮度:{0}</summary>
	/// <remarks></remarks>
		//Old Set CCD Light6:{0}
	public const  int INFO_6022020 = 6022020;
	/// <summary>新設定CCD光源6亮度:{0}</summary>
	/// <remarks></remarks>
		//New Set CCD Light6:{0}
	public const  int INFO_6022021 = 6022021;
	/// <summary>原設定CCD光源7亮度:{0}</summary>
	/// <remarks></remarks>
		//Old Set CCD Light7:{0}
	public const  int INFO_6022022 = 6022022;
	/// <summary>新設定CCD光源7亮度:{0}</summary>
	/// <remarks></remarks>
		//New Set CCD Light7:{0}
	public const  int INFO_6022023 = 6022023;
	/// <summary>原設定CCD光源8亮度:{0}</summary>
	/// <remarks></remarks>
		//Old Set CCD Light8:{0}
	public const  int INFO_6022024 = 6022024;
	/// <summary>新設定CCD光源8亮度:{0}</summary>
	/// <remarks></remarks>
		//New Set CCD Light8:{0}
	public const  int INFO_6022025 = 6022025;
	/// <summary>原設定CCD光源9亮度:{0}</summary>
	/// <remarks></remarks>
		//Old Set CCD Light9:{0}
	public const  int INFO_6022026 = 6022026;
	/// <summary>新設定CCD光源9亮度:{0}</summary>
	/// <remarks></remarks>
		//New Set CCD Light9:{0}
	public const  int INFO_6022027 = 6022027;
	/// <summary>原設定CCD光源10亮度:{0}</summary>
	/// <remarks></remarks>
		//Old Set CCD Light10:{0}
	public const  int INFO_6022028 = 6022028;
	/// <summary>新設定CCD光源10亮度:{0}</summary>
	/// <remarks></remarks>
		//New Set CCD Light10:{0}
	public const  int INFO_6022029 = 6022029;
	/// <summary>原設定CCD光源11亮度:{0}</summary>
	/// <remarks></remarks>
		//Old Set CCD Light11:{0}
	public const  int INFO_6022030 = 6022030;
	/// <summary>新設定CCD光源11亮度:{0}</summary>
	/// <remarks></remarks>
		//New Set CCD Light11:{0}
	public const  int INFO_6022031 = 6022031;
	/// <summary>原設定CCD光源12亮度:{0}</summary>
	/// <remarks></remarks>
		//Old Set CCD Light12:{0}
	public const  int INFO_6022032 = 6022032;
	/// <summary>新設定CCD光源12亮度:{0}</summary>
	/// <remarks></remarks>
		//New Set CCD Light12:{0}
	public const  int INFO_6022033 = 6022033;
	/// <summary>原設定CCD光源13亮度:{0}</summary>
	/// <remarks></remarks>
		//Old Set CCD Light13:{0}
	public const  int INFO_6022034 = 6022034;
	/// <summary>新設定CCD光源13亮度:{0}</summary>
	/// <remarks></remarks>
		//New Set CCD Light13:{0}
	public const  int INFO_6022035 = 6022035;
	/// <summary>原設定CCD光源14亮度:{0}</summary>
	/// <remarks></remarks>
		//Old Set CCD Light14:{0}
	public const  int INFO_6022036 = 6022036;
	/// <summary>新設定CCD光源14亮度:{0}</summary>
	/// <remarks></remarks>
		//New Set CCD Light14:{0}
	public const  int INFO_6022037 = 6022037;
	/// <summary>原設定CCD光源15亮度:{0}</summary>
	/// <remarks></remarks>
		//Old Set CCD Light15:{0}
	public const  int INFO_6022038 = 6022038;
	/// <summary>新設定CCD光源15亮度:{0}</summary>
	/// <remarks></remarks>
		//New Set CCD Light15:{0}
	public const  int INFO_6022039 = 6022039;
	/// <summary>原設定CCD光源16亮度:{0}</summary>
	/// <remarks></remarks>
		//Old Set CCD Light16:{0}
	public const  int INFO_6022040 = 6022040;
	/// <summary>新設定CCD光源16亮度:{0}</summary>
	/// <remarks></remarks>
		//New Set CCD Light16:{0}
	public const  int INFO_6022041 = 6022041;
	/// <summary>PLC1初始化成功.</summary>
	/// <remarks></remarks>
		//Initialize PLC1 OK.
	public const  int INFO_6023000 = 6023000;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6023001 = 6023001;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6023002 = 6023002;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6023003 = 6023003;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6023004 = 6023004;
	/// <summary></summary>
	/// <remarks></remarks>
		//
	public const  int INFO_6023005 = 6023005;
	/// <summary>UPH</summary>
	/// <remarks></remarks>
		//UPH
	public const  int STATUS_5000001 = 5000001;
	/// <summary>LOT ID</summary>
	/// <remarks></remarks>
		//LOT ID
	public const  int STATUS_5000002 = 5000002;
	/// <summary>RECIPE ID</summary>
	/// <remarks></remarks>
		//RECIPE ID
	public const  int STATUS_5000003 = 5000003;
	/// <summary>BARCODE ID</summary>
	/// <remarks></remarks>
		//BARCODE ID
	public const  int STATUS_5000004 = 5000004;
	/// <summary>VALVE1 AIR PRESSURE</summary>
	/// <remarks></remarks>
		//VALVE1 AIR PRESSURE
	public const  int STATUS_5001000 = 5001000;
	/// <summary>VALVE2 AIR PRESSURE</summary>
	/// <remarks></remarks>
		//VALVE2 AIR PRESSURE
	public const  int STATUS_5001001 = 5001001;
	/// <summary>VALVE3 AIR PRESSURE</summary>
	/// <remarks></remarks>
		//VALVE3 AIR PRESSURE
	public const  int STATUS_5001002 = 5001002;
	/// <summary>VALVE4 AIR PRESSURE</summary>
	/// <remarks></remarks>
		//VALVE4 AIR PRESSURE
	public const  int STATUS_5001003 = 5001003;
}

}
