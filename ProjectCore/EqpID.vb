Public Module EqpID
    ''Alarm code 定義規則
    ' 若不確定加什麼Alarm訊息，麻煩先用 "Error_1000002" 

    '項目           		Range

    '參數存取       		2000	2999
    'DIO卡	       		3000	3999
    'DI卡	       		4000	4999
    'DO卡	       		5000	5999
    'AIO卡			6000	6999
    'AI卡			7000	7999
    'AO卡			8000	8999
    '動控制卡		9000	9999
    'COM元件			10000	10999
    '網路元件		11000	11999
    'CCD			12000	12999
    '影像擷取卡		13000	13999
    '雷射干涉儀		14000	14999
    '微量天平		15000	15999
    '觸發板			16000	16999
    'FMCS			17000	17999
    '加熱器			18000	18999
    '噴射閥			19000	19999
    '螺桿閥			20000	20999
    '氣壓閥			21000	21999
    '程控光源		22000	22999
    'PLC			23000	23999
    '閥控制器		24000	24999
    'MAP			25000	25999
    '第一組平台X軸		30000	30999
    '第一組平台Y軸		31000	31999
    '第一組平台Z軸		32000	32999
    '第一組平台X2軸		33000	33999
    '第一組平台B軸(Tilt)	34000	34999
    '第一組平台C軸		35000	35999
    '第一組平台運動群組1	36000	36999
    '第一組傳送帶Conveyor1軸	37000	37999
    '第一組傳送帶Conveyor2軸	38000	38999
    '第一組傳送帶S1軸	39000	39999
    '第一組傳送帶S2軸	40000	40999
    '第一組傳送帶S3軸	41000	41999
    '第二組平台U軸(X軸)	42000	42999
    '第二組平台V軸(Y軸)	43000	43999
    '第二組平台W軸(Z軸)	44000	44999
    '第二組平台U2軸(X2軸)	45000	45999
    '第二組平台(B軸)(Tilt)	46000	46999
    '第二組平台F軸(C軸)	47000	47999
    '第二組平台運動群組2	48000	48999
    '第二組傳送帶Conveyor1軸	49000	49999
    '第二組傳送帶Conveyor2軸	50000	50999
    '第二組傳送帶S1軸	51000	51999
    '第二組傳送帶S2軸	52000	52999
    '第二組傳送帶S3軸	53000	53999
    '第三組平台R軸(X軸)	60000	60999
    '第三組平台S軸(Y軸)	61000	61999
    '第三組平台T軸(Z軸)	62000	62999
    '第三組平台R2軸(X2軸)	63000	63999
    '第三組平台(B軸)(Tilt)	64000	64999
    '第三組平台I軸(C軸)	65000	65999
    '第三組平台運動群組1	66000	66999
    '第四組平台O軸(X軸)	67000	67999
    '第四組平台P軸(Y軸)	68000	68999
    '第四組平台Q軸(Z軸)	69000	69999
    '第四組平台O2軸(X2軸)	70000	70999
    '第四組平台(B軸)(Tilt)	71000	71999
    '第四組平台L軸(C軸)	72000	72999
    '第四組平台運動群組1	73000	73999
    '整機			80000	80999
    'A機			81000	81999
    'B機			82000	82999
    'Conveyor1		83000	83999
    'Conveyor2		84000	84999







    ''' <summary>Program Start</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4000001 = 4000001    'Program Start
    ''' <summary>Program End</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4000002 = 4000002    'Program End
    ''' <summary>User Login</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4000003 = 4000003    'User Login
    ''' <summary>User Logout</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4000004 = 4000004    'User Logout
    ''' <summary>AutoRun Start</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4000005 = 4000005    'AutoRun Start
    ''' <summary>AutoRun End</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4000006 = 4000006    'AutoRun End
    ''' <summary>Calibration Start</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4000007 = 4000007    'Calibration Start
    ''' <summary>Calibration End</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4000008 = 4000008    'Calibration End
    ''' <summary>Recipe Edit Start</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4000009 = 4000009    'Recipe Edit Start
    ''' <summary>Recipe Edit End</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4000010 = 4000010    'Recipe Edit End
    ''' <summary>Conveyor Load Start</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4049000 = 4049000    'Conveyor Load Start
    ''' <summary>Conveyor Load End</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4049001 = 4049001    'Conveyor Load End
    ''' <summary>Conveyor Unload Start</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4049002 = 4049002    'Conveyor Unload Start
    ''' <summary>Conveyor Unload End</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4049003 = 4049003    'Conveyor Unload End
    ''' <summary>Station1 Start</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4049004 = 4049004    'Station1 Start
    ''' <summary>Station1 End</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4049005 = 4049005    'Station1 End
    ''' <summary>Station2 Start</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4049006 = 4049006    'Station2 Start
    ''' <summary>Station2 End</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4049007 = 4049007    'Station2 End
    ''' <summary>Station3 Start</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4049008 = 4049008    'Station3 Start
    ''' <summary>Station3 End</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4049009 = 4049009    'Station3 End
    ''' <summary>Loader Start</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4054000 = 4054000    'Loader Start
    ''' <summary>Loader End</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4054001 = 4054001    'Loader End
    ''' <summary>Unloader Start</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4054002 = 4054002    'Unloader Start
    ''' <summary>Unloader End</summary>
    ''' <remarks></remarks>
    Public Const EQP_EVENT_4054003 = 4054003    'Unloader End
    ''' <summary>請確認以系統管理員執行.</summary>
    ''' <remarks></remarks>
    Public Const Error_1000000 = 1000000    '"Please, ensure System Run by Administrator."
    ''' <summary>"軸索引設定值錯誤,系統即將關閉!!!"</summary>
    ''' <remarks></remarks>
    Public Const Error_1000001 = 1000001    '"Axis Index is Error, System will Shutdown!!!"
    ''' <summary>施工中…未完待續.</summary>
    ''' <remarks></remarks>
    Public Const Error_1000002 = 1000002    'Working…To Be Continued.
    ''' <summary>該功能不支援!</summary>
    ''' <remarks></remarks>
    Public Const Error_1000003 = 1000003    'Function is NOT Supported.
    ''' <summary>龍門同動失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1000004 = 1000004    'Gantry Failed!
    ''' <summary>頁面開啟失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1000005 = 1000005    'Form Open Failed!
    ''' <summary>使用者登入發生嚴重錯誤!</summary>
    ''' <remarks></remarks>
    Public Const Error_1000006 = 1000006    'User Login fatal Error!
    ''' <summary>不明例外.</summary>
    ''' <remarks></remarks>
    Public Const Error_1000007 = 1000007    'Unknown Exception.
    ''' <summary>影像計算逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1000008 = 1000008    'Image Calculation Timeout!
    ''' <summary>Recipe Pattern繪圖失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1000009 = 1000009    'Draw Recipe Pattern Failed!
    ''' <summary>Recipe Map繪圖失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1000010 = 1000010    'Draw Recipe Map Failed!
    ''' <summary>CCD 硬體異常，請確認!</summary>
    ''' <remarks></remarks>
    Public Const Error_1000011 = 1000011    'CCD Hardware Error, Please Check!
    ''' <summary>串接路徑異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1000012 = 1000012    'Create Working Path Failed!
    ''' <summary>系統失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1000013 = 1000013    'System Failed!
    ''' <summary>系統平台錯誤!</summary>
    ''' <remarks></remarks>
    Public Const Error_1000014 = 1000014    'System Stage Error!
    ''' <summary>平台數量錯誤!</summary>
    ''' <remarks></remarks>
    Public Const Error_1000015 = 1000015    'Stage Count Error!
    ''' <summary>軸索引讀取失敗!請確認參數正確!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002000 = 1002000    'Axis Index Load Failed!
    ''' <summary>軸索引儲存失敗!請確認檔案未被佔用!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002001 = 1002001    'Axis Index Save Failed!
    ''' <summary>系統參數讀取失敗!請確認參數正確!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002002 = 1002002    'System Parameter Load Failed!
    ''' <summary>系統參數儲存失敗!請確認檔案未被佔用!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002003 = 1002003    'System Parameter Save Failed!
    ''' <summary>膠材參數讀取失敗!請確認參數正確!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002004 = 1002004    'Paste Parameter Load Failed
    ''' <summary>膠材參數儲存失敗!請確認檔案未被佔用!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002005 = 1002005    'Paste Parameter Save Failed
    ''' <summary>膠閥參數讀取失敗!請確認參數正確!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002006 = 1002006    'Valve Parameter Load Failed!
    ''' <summary>膠閥參數儲存失敗!請確認檔案未被佔用!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002007 = 1002007    'Valve Parameter Save Failed!
    ''' <summary>IO卡參數讀取失敗!請確認參數正確!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002008 = 1002008    'IO Card Load Failed!
    ''' <summary>IO卡參數儲存失敗!請確認檔案未被佔用!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002009 = 1002009    'IO Card Save Failed!
    ''' <summary>運動控制卡參數讀取失敗!請確認參數正確!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002010 = 1002010    'Motion Card Load Failed!
    ''' <summary>運動控制卡參數儲存失敗!請確認檔案未被佔用!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002011 = 1002011    'Motion Card Save Failed!
    ''' <summary>AI卡參數讀取失敗!請確認參數正確!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002012 = 1002012    'AI Card Load Failed!
    ''' <summary>AI卡參數儲存失敗!請確認檔案未被佔用!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002013 = 1002013    'AI Card Save Failed!
    ''' <summary>AO參數讀取失敗!請確認參數正確!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002014 = 1002014    'AO Card Load Failed!
    ''' <summary>AO參數儲存失敗!請確認檔案未被佔用!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002015 = 1002015    'AO Card Save Failed!
    ''' <summary>DI卡參數讀取失敗!請確認參數正確!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002016 = 1002016    'DI Card Load Failed!
    ''' <summary>DI卡參數儲存失敗!請確認檔案未被佔用!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002017 = 1002017    'DI Card Save Failed!
    ''' <summary>DO參數讀取失敗!請確認參數正確!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002018 = 1002018    'DO Card Load Failed!
    ''' <summary>DO參數儲存失敗!請確認檔案未被佔用!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002019 = 1002019    'DO Card Save Failed!
    ''' <summary>Recipe讀檔失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002020 = 1002020    'Recipe Load Failed!
    ''' <summary>Recipe存檔失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002021 = 1002021    'Recipe Save Failed!
    ''' <summary>單步參數讀取失敗!請確認參數正確!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002022 = 1002022    'Step Parameter Load Failed!
    ''' <summary>單步參數儲存失敗!請確認檔案未被佔用!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002023 = 1002023    'Step Parameter Save Failed!
    ''' <summary>設備訊息行為讀檔失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002024 = 1002024    'Message Behavior Load Failed!
    ''' <summary>設備訊息行為存檔失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002025 = 1002025    'Message Behavior Save Failed!
    ''' <summary>天平參數讀檔失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002026 = 1002026    'Scale Parameter Load Failed!
    ''' <summary>天平參數存檔失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002027 = 1002027    'Scale Parameter Save Failed!
    ''' <summary>閥1校正檔讀檔失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002028 = 1002028    'Valve1 Calibration Load Failed!
    ''' <summary>閥1校正檔存檔失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002029 = 1002029    'Valve1 Calibration Save Failed!
    ''' <summary>閥2校正檔讀檔失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002030 = 1002030    'Valve2 Calibration Load Failed!
    ''' <summary>閥2校正檔存檔失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002031 = 1002031    'Valve2 Calibration Save Failed!
    ''' <summary>閥3校正檔讀檔失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002032 = 1002032    'Valve3 Calibration Load Failed!
    ''' <summary>閥3校正檔存檔失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002033 = 1002033    'Valve3 Calibration Save Failed!
    ''' <summary>閥4校正檔讀檔失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002034 = 1002034    'Valve4 Calibration Load Failed!
    ''' <summary>閥4校正檔存檔失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002035 = 1002035    'Valve4 Calibration Save Failed!
    ''' <summary>閥高校正檔讀檔失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002036 = 1002036    'Valve Height Calibration Load Failed!
    ''' <summary>閥高校正檔存檔失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1002037 = 1002037    'Valve Height Calibration Save Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1002038 = 1002038    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1002039 = 1002039    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1002040 = 1002040    '
    ''' <summary>IO卡初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1003000 = 1003000    'Initialize IO Card Failed!
    ''' <summary>IO卡關卡失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1003001 = 1003001    'Close IO Card Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1003002 = 1003002    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1003003 = 1003003    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1003004 = 1003004    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1003005 = 1003005    '
    ''' <summary>DI卡初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1004000 = 1004000    'Initialize DI Card Failed!
    ''' <summary>DI卡關卡失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1004001 = 1004001    'Close DI Card Failed!
    ''' <summary>DI卡取得輸入值失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1004002 = 1004002    'DI Card Get State Failed!
    ''' <summary>DI卡更新輸入值失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1004003 = 1004003    'DI Card Refresh State Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1004004 = 1004004    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1004005 = 1004005    '
    ''' <summary>DO卡初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1005000 = 1005000    'Initialize DO Card Failed!
    ''' <summary>DO卡關卡失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1005001 = 1005001    'Close DO Card Failed!
    ''' <summary>DO卡取得輸出值失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1005002 = 1005002    'DO Card Get State Failed!
    ''' <summary>DO卡輸出值失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1005003 = 1005003    'DO Card Output Failed!
    ''' <summary>DO卡反向輸出失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1005004 = 1005004    'DO Card Toggle Output Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1005005 = 1005005    '
    ''' <summary>AIO卡初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1006000 = 1006000    'Initialize AIO Card Failed!
    ''' <summary>AIO卡關卡失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1006001 = 1006001    'Close AIO Card Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1006002 = 1006002    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1006003 = 1006003    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1006004 = 1006004    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1006005 = 1006005    '
    ''' <summary>AI卡初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1007000 = 1007000    'Initialize AI Card Failed!
    ''' <summary>AI卡關卡失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1007001 = 1007001    'Close AI Card Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1007002 = 1007002    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1007003 = 1007003    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1007004 = 1007004    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1007005 = 1007005    '
    ''' <summary>AO卡初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1008000 = 1008000    'Initialize AO Card Failed!
    ''' <summary>AO卡關卡失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1008001 = 1008001    'Close AO Card Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1008002 = 1008002    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1008003 = 1008003    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1008004 = 1008004    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1008005 = 1008005    '
    ''' <summary>運動控制卡1初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1009000 = 1009000    'Initialize Motion Card1 Failed!
    ''' <summary>運動控制卡1關卡失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1009001 = 1009001    'Close Motion Card1 Failed!
    ''' <summary>"運動控制卡1初始化失敗,無法啟用卡!"</summary>
    ''' <remarks></remarks>
    Public Const Error_1009002 = 1009002    'Initialize Motion Card1 Failed! Can Not Open Device.
    ''' <summary>運動控制卡1初始化失敗!取得屬性失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1009003 = 1009003    'Initialize Motion Card1 Failed! Get Property Error.
    ''' <summary>運動控制卡1初始化失敗!無法啟用軸!</summary>
    ''' <remarks></remarks>
    Public Const Error_1009004 = 1009004    'Initialize Motion Card1 Failed! Open Axis Failed!
    ''' <summary>"運動控制卡1初始化失敗,無法取得可用卡片!"</summary>
    ''' <remarks></remarks>
    Public Const Error_1009005 = 1009005    'Motion Card1 Can Not Get Available Device
    ''' <summary>運動控制卡1命令發送失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1009006 = 1009006    'Motion Card1 Send Command Failed!
    ''' <summary>運動控制卡1IO狀態為警報!</summary>
    ''' <remarks></remarks>
    Public Const Error_1009007 = 1009007    'Motion Card1 Motion IO Status is Alarm!
    ''' <summary>{0}移動到位逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1009008 = 1009008    '{0} Move Timeout!
    ''' <summary>運動控制卡1復歸第一段速設定失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1009009 = 1009009    'Motion Card1 Set Home Velocity Low Failed!
    ''' <summary>運動控制卡1復歸第二段速設定失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1009010 = 1009010    'Motion Card1 Set Home Velocity High Failed!
    ''' <summary>運動控制卡1復歸加速度設定失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1009011 = 1009011    'Motion Card1 Set Home Acceleration Failed!
    ''' <summary>運動控制卡1復歸減速度設定失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1009012 = 1009012    'Motion Card1 Set Home Deceleration Failed!
    ''' <summary>運動控制卡1初速度設定失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1009013 = 1009013    'Motion Card1 Set Velocity Low Failed!
    ''' <summary>運動控制卡1最大速度設定失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1009014 = 1009014    'Motion Card1 Set Velocity High Failed!
    ''' <summary>運動控制卡1加速度設定失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1009015 = 1009015    'Motion Card1 Set Acceleration Failed!
    ''' <summary>運動控制卡1減速度設定失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1009016 = 1009016    'Motion Card1 Set Deceleration Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1009017 = 1009017    '
    ''' <summary>COM通訊埠開啟失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1010000 = 1010000    'Open COM Port Failed!
    ''' <summary>COM通訊埠關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1010001 = 1010001    'COM Port Close Failed!
    ''' <summary>COM通訊埠已開啟!</summary>
    ''' <remarks></remarks>
    Public Const Error_1010002 = 1010002    'COM Port Is Opened!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1010003 = 1010003    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1010004 = 1010004    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1010005 = 1010005    '
    ''' <summary>"網路連線逾時!請確認IP,Port未被佔用."</summary>
    ''' <remarks></remarks>
    Public Const Error_1011000 = 1011000    '"EtherNET Connection Time Out! Please confirm IP, Port Unoccupied."
    ''' <summary>網路連線斷開失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1011001 = 1011001    'EtherNET Close Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1011002 = 1011002    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1011003 = 1011003    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1011004 = 1011004    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1011005 = 1011005    '
    ''' <summary>CCD1初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1012000 = 1012000    'Initialize CCD1 Failed!
    ''' <summary>CCD1關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1012001 = 1012001    'Close CCD1 Failed!
    ''' <summary>CCD1通訊逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1012002 = 1012002    'CCD1 Communication TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1012003 = 1012003    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1012004 = 1012004    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1012005 = 1012005    '
    ''' <summary>CCD2初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1012100 = 1012100    'Initialize CCD2 Failed!
    ''' <summary>CCD2關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1012101 = 1012101    'Close CCD2 Failed!
    ''' <summary>CCD2通訊逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1012102 = 1012102    'CCD2 Communication TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1012103 = 1012103    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1012104 = 1012104    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1012105 = 1012105    '
    ''' <summary>CCD3初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1012200 = 1012200    'Initialize CCD3 Failed!
    ''' <summary>CCD3關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1012201 = 1012201    'Close CCD3 Failed!
    ''' <summary>CCD3通訊逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1012202 = 1012202    'CCD3 Communication TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1012203 = 1012203    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1012204 = 1012204    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1012205 = 1012205    '
    ''' <summary>CCD4初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1012300 = 1012300    'Initialize CCD4 Failed!
    ''' <summary>CCD4關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1012301 = 1012301    'Close CCD4 Failed!
    ''' <summary>CCD4通訊逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1012302 = 1012302    'CCD4 Communication TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1012303 = 1012303    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1012304 = 1012304    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1012305 = 1012305    '
    ''' <summary>CCD5初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1012400 = 1012400    'Initialize CCD5 Failed!
    ''' <summary>CCD5關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1012401 = 1012401    'Close CCD5 Failed!
    ''' <summary>CCD5通訊逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1012402 = 1012402    'CCD5 Communication TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1012403 = 1012403    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1012404 = 1012404    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1012405 = 1012405    '
    ''' <summary>影像擷取卡初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1013000 = 1013000    'Initialize Image Card Failed!
    ''' <summary>影像擷取卡關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1013001 = 1013001    'Close Image Card Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1013002 = 1013002    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1013003 = 1013003    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1013004 = 1013004    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1013005 = 1013005    '
    ''' <summary>測高儀1初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1014000 = 1014000    'Initialize Altimeter 1 Failed!
    ''' <summary>測高儀1關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1014001 = 1014001    'Close Altimeter 1 Failed!
    ''' <summary>測高儀1選取Recipe失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1014002 = 1014002    'Altimeter 1 Select Recipe Failed!
    ''' <summary>測高儀1 IP位置設定錯誤!</summary>
    ''' <remarks></remarks>
    Public Const Error_1014003 = 1014003    'Altimeter 1 IP-Address Error!
    ''' <summary>測高儀1讀值失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1014004 = 1014004    'Altimeter 1 Read Value Error!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1014005 = 1014005    '
    ''' <summary>測高儀2初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1014100 = 1014100    'Initialize Altimeter 2 Failed!
    ''' <summary>測高儀2關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1014101 = 1014101    'Close Altimeter 2 Failed!
    ''' <summary>測高儀2選取Recipe失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1014102 = 1014102    'Altimeter 2 Select Recipe Failed!
    ''' <summary>測高儀2 IP位置設定錯誤!</summary>
    ''' <remarks></remarks>
    Public Const Error_1014103 = 1014103    'Altimeter 2 IP-Address Error!
    ''' <summary>測高儀2讀值失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1014104 = 1014104    'Altimeter 2 Read Value Error!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1014105 = 1014105    '
    ''' <summary>測高儀3初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1014200 = 1014200    'Initialize Altimeter 3 Failed!
    ''' <summary>測高儀3關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1014201 = 1014201    'Close Altimeter 3 Failed!
    ''' <summary>雷射干涉儀3選取Recipe失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1014202 = 1014202    'Altimeter 3 Select Recipe Failed!
    ''' <summary>測高儀3 IP位置設定錯誤!</summary>
    ''' <remarks></remarks>
    Public Const Error_1014203 = 1014203    'Altimeter 3 IP-Address Error!
    ''' <summary>測高儀3讀值失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1014204 = 1014204    'Altimeter 3 Read Value Error!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1014205 = 1014205    '
    ''' <summary>測高儀4初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1014300 = 1014300    'Initialize Altimeter 4 Failed!
    ''' <summary>測高儀4關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1014301 = 1014301    'Close Altimeter 4 Failed!
    ''' <summary>測高儀4選取Recipe失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1014302 = 1014302    'Altimeter 4 Select Recipe Failed!
    ''' <summary>測高儀4 IP位置設定錯誤!</summary>
    ''' <remarks></remarks>
    Public Const Error_1014303 = 1014303    'Altimeter 4 IP-Address Error!
    ''' <summary>測高儀4讀值失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1014304 = 1014304    'Altimeter 4 Read Value Error!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1014305 = 1014305    '
    ''' <summary>微量天平1初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1015000 = 1015000    'Initialize Scale1 Failed!
    ''' <summary>微量天平1關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1015001 = 1015001    'Close Scale1 Failed!
    ''' <summary>微量天平1通訊逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1015002 = 1015002    'Scale1 Communication Timeout!
    ''' <summary>微量天平1命令發送失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1015003 = 1015003    'Scale1 Send Command Error!
    ''' <summary>微量天平1接收資料失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1015004 = 1015004    'Scale1 Get Data Error!
    ''' <summary>微量天平1重量校正失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1015005 = 1015005    'Scale1 Calibration Failed!
    ''' <summary>微量天平1重量不穩定!</summary>
    ''' <remarks></remarks>
    Public Const Error_1015006 = 1015006    'Scale1  is instability.
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1015007 = 1015007    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1015008 = 1015008    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1015009 = 1015009    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1015010 = 1015010    '
    ''' <summary>微量天平2初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1015100 = 1015100    'Initialize Scale2 Failed!
    ''' <summary>微量天平2關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1015101 = 1015101    'Close Scale2 Failed!
    ''' <summary>微量天平2通訊逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1015102 = 1015102    'Scale2 Communication Timeout!
    ''' <summary>微量天平2命令發送失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1015103 = 1015103    'Scale2 Send Command Error!
    ''' <summary>微量天平2接收資料失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1015104 = 1015104    'Scale2 Get Data Error!
    ''' <summary>微量天平2重量校正失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1015105 = 1015105    'Scale2 Calibration Failed!
    ''' <summary>微量天平2重量不穩定!</summary>
    ''' <remarks></remarks>
    Public Const Error_1015106 = 1015106    'Scale2  is instability.
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1015107 = 1015107    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1015108 = 1015108    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1015109 = 1015109    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1015110 = 1015110    '
    ''' <summary>觸發板1初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016000 = 1016000    'Initialize Trigger Board1 Failed!
    ''' <summary>觸發板1關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016001 = 1016001    'Close Trigger Board1 Failed!
    ''' <summary>觸發板1通訊逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016002 = 1016002    'Trigger Board1 Communication Timeout!
    ''' <summary>觸發板1命令發送錯誤!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016003 = 1016003    'Trigger Board1 Send Command Error!
    ''' <summary>觸發板1資料接收錯誤!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016004 = 1016004    'Trigger Board1Recieved Data Error!
    ''' <summary>觸發板1讀取版本錯誤!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016005 = 1016005    'Trigger Board1 Get Version Failed!
    ''' <summary>觸發板2初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016100 = 1016100    'Initialize Trigger Board2 Failed!
    ''' <summary>觸發板2關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016101 = 1016101    'Close Trigger Board2 Failed!
    ''' <summary>觸發板2通訊逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016102 = 1016102    'Trigger Board2 Communication Timeout!
    ''' <summary>觸發板2命令發送錯誤!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016103 = 1016103    'Trigger Board2 Send Command Error!
    ''' <summary>觸發板2資料接收錯誤!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016104 = 1016104    'Trigger Board2 Recieved Data Error!
    ''' <summary>觸發板2讀取版本錯誤!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016105 = 1016105    'Trigger Board2 Get Version Failed!
    ''' <summary>觸發板3初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016200 = 1016200    'Initialize Trigger Board3 Failed!
    ''' <summary>觸發板3關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016201 = 1016201    'Close Trigger Board3 Failed!
    ''' <summary>觸發板3通訊逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016202 = 1016202    'Trigger Board3 Communication Timeout!
    ''' <summary>觸發板3命令發送錯誤!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016203 = 1016203    'Trigger Board3 Send Command Error!
    ''' <summary>觸發板3資料接收錯誤!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016204 = 1016204    'Trigger Board3 Recieved Data Error!
    ''' <summary>觸發板3讀取版本錯誤!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016205 = 1016205    'Trigger Board3 Get Version Failed!
    ''' <summary>觸發板4初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016300 = 1016300    'Initialize Trigger Board4 Failed!
    ''' <summary>觸發板4關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016301 = 1016301    'Close Trigger Board4 Failed!
    ''' <summary>觸發板4通訊逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016302 = 1016302    'Trigger Board4 Communication Timeout!
    ''' <summary>觸發板4命令發送錯誤!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016303 = 1016303    'Trigger Board4 Send Command Error!
    ''' <summary>觸發板4資料接收錯誤!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016304 = 1016304    'Trigger Board4 Recieved Data Error!
    ''' <summary>觸發板4讀取版本錯誤!</summary>
    ''' <remarks></remarks>
    Public Const Error_1016305 = 1016305    'Trigger Board4 Get Version Failed!
    ''' <summary>FMCS1初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1017000 = 1017000    'Initialize FMCS1 Failed!
    ''' <summary>FMCS1關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1017001 = 1017001    'Close FMCS1 Failed
    ''' <summary>FMCS1通訊逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1017002 = 1017002    'FMCS1 Communication TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1017003 = 1017003    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1017004 = 1017004    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1017005 = 1017005    '
    ''' <summary>FMCS2初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1017100 = 1017100    'Initialize FMCS2 Failed!
    ''' <summary>FMCS2關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1017101 = 1017101    'Close FMCS2 Failed
    ''' <summary>FMCS2通訊逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1017102 = 1017102    'FMCS2 Communication TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1017103 = 1017103    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1017104 = 1017104    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1017105 = 1017105    '
    ''' <summary>FMCS3初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1017200 = 1017200    'Initialize FMCS3 Failed!
    ''' <summary>FMCS3關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1017201 = 1017201    'Close FMCS3 Failed
    ''' <summary>FMCS3通訊逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1017202 = 1017202    'FMCS3 Communication TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1017203 = 1017203    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1017204 = 1017204    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1017205 = 1017205    '
    ''' <summary>FMCS4初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1017300 = 1017300    'Initialize FMCS4 Failed!
    ''' <summary>FMCS4關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1017301 = 1017301    'Close FMCS4 Failed
    ''' <summary>FMCS4通訊逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1017302 = 1017302    'FMCS4 Communication TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1017303 = 1017303    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1017304 = 1017304    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1017305 = 1017305    '
    ''' <summary>加熱器1初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1018000 = 1018000    'Initialize Heater1 Failed!
    ''' <summary>加熱器1關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1018001 = 1018001    'Close Heater1 Failed!
    ''' <summary>加熱器1通訊逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1018002 = 1018002    'Heater1 Communication TimeOut!
    ''' <summary>加熱器1資料錯誤</summary>
    ''' <remarks></remarks>
    Public Const Error_1018003 = 1018003    'Heater1 Temp Date Fail
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1018004 = 1018004    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1018005 = 1018005    '
    ''' <summary>加熱器2初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1018100 = 1018100    'Initialize Heater2 Failed!
    ''' <summary>加熱器2關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1018101 = 1018101    'Close Heater2 Failed!
    ''' <summary>加熱器2通訊逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1018102 = 1018102    'Heater2 Communication TimeOut!
    ''' <summary>加熱器2資料錯誤</summary>
    ''' <remarks></remarks>
    Public Const Error_1018103 = 1018103    'Heater2 Temp Date Fail
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1018104 = 1018104    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1018105 = 1018105    '
    ''' <summary>加熱器3初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1018200 = 1018200    'Initialize Heater3 Failed!
    ''' <summary>加熱器3關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1018201 = 1018201    'Close Heater3 Failed!
    ''' <summary>加熱器3通訊逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1018202 = 1018202    'Heater3 Communication TimeOut!
    ''' <summary>加熱器3資料錯誤</summary>
    ''' <remarks></remarks>
    Public Const Error_1018203 = 1018203    'Heater3 Temp Date Fail
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1018204 = 1018204    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1018205 = 1018205    '
    ''' <summary>加熱器4初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1018300 = 1018300    'Initialize Heater4 Failed!
    ''' <summary>加熱器4關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1018301 = 1018301    'Close Heater4 Failed!
    ''' <summary>加熱器4通訊逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1018302 = 1018302    'Heater4 Communication TimeOut!
    ''' <summary>加熱器4資料錯誤</summary>
    ''' <remarks></remarks>
    Public Const Error_1018303 = 1018303    'Heater4 Temp Date Fail
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1018304 = 1018304    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1018305 = 1018305    '
    ''' <summary>噴射閥1初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1019000 = 1019000    'Initialize Jet Valve1 Failed!
    ''' <summary>噴射閥1關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1019001 = 1019001    'Close Jet Valve1 Failed!
    ''' <summary>噴射閥1控制器異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1019002 = 1019002    'Jet Valve1 Controller Alarm!
    ''' <summary>噴射閥資料異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1019003 = 1019003    'Jet Valve Database Alarm!
    ''' <summary>"噴射閥控制器讀/寫資料失敗!"</summary>
    ''' <remarks></remarks>
    Public Const Error_1019004 = 1019004    'Jet Valve Controller Read/Write Failed!
    ''' <summary>請選擇噴射閥1</summary>
    ''' <remarks></remarks>
    Public Const Error_1019005 = 1019005    'Select Jet Valve1, Please!
    ''' <summary>請選擇噴射閥2</summary>
    ''' <remarks></remarks>
    Public Const Error_1019006 = 1019006    'Select Jet Valve2, Please!
    ''' <summary>請選擇噴射閥3</summary>
    ''' <remarks></remarks>
    Public Const Error_1019007 = 1019007    'Select Jet Valve3, Please!
    ''' <summary>請選擇噴射閥4</summary>
    ''' <remarks></remarks>
    Public Const Error_1019008 = 1019008    'Select Jet Valve4, Please!
    ''' <summary>螺桿閥1初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1020000 = 1020000    'Initialize Auger Valve1 Failed!
    ''' <summary>螺桿閥1關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1020001 = 1020001    'Close Auger Valve1 Failed!
    ''' <summary>螺桿閥1異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1020002 = 1020002    'Auger Valve1 Alarm!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1020003 = 1020003    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1020004 = 1020004    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1020005 = 1020005    '
    ''' <summary>氣壓閥1初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1021000 = 1021000    'Initialize Time-pressure Valve1 Failed!
    ''' <summary>氣壓閥1關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1021001 = 1021001    'Close Time-pressure Valve1 Failed!
    ''' <summary>氣壓閥1異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1021002 = 1021002    'Time-pressure Valve1 Alarm!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1021003 = 1021003    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1021004 = 1021004    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1021005 = 1021005    '
    ''' <summary>程控光源初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1022000 = 1022000    'Initialize Program Light Failed!
    ''' <summary>程控光源關閉失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1022001 = 1022001    'Close Program Light Failed!
    ''' <summary>程控光源通訊逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1022002 = 1022002    'Program Light Communication Timeout!
    ''' <summary>程控光源通訊失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1022003 = 1022003    'Program Light Communication Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1022004 = 1022004    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1022005 = 1022005    '
    ''' <summary>PLC1初始化失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1023000 = 1023000    'Initialize PLC1 Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1023001 = 1023001    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1023002 = 1023002    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1023003 = 1023003    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1023004 = 1023004    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1023005 = 1023005    '
    ''' <summary>Map檔開啟失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1025000 = 1025000    'Mapping Data Open Failed!
    ''' <summary>Map檔切割失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1025001 = 1025001    'Mapping Data Split Failed!
    ''' <summary>Map檔填入失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1025002 = 1025002    'Mapping Data Fill Failed!
    ''' <summary>節點資料連結失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1025003 = 1025003    'Node Map Connect Failed!
    ''' <summary>無法載入Map檔,請手動載入Map檔</summary>
    ''' <remarks></remarks>
    Public Const Error_1025004 = 1025004    'Can't load Map, Please click Load Map Manual
    ''' <summary>A_L_DP_X軸移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1030000 = 1030000    'A_L_DP_X Axis  Move Error!
    ''' <summary>A_L_DP_X軸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1030001 = 1030001    'A_L_DP_X Axis  wait Home Timeout!
    ''' <summary>A_L_DP_X軸馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1030002 = 1030002    'A_L_DP_X Axis  is Alarm!
    ''' <summary>A_L_DP_X軸馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1030003 = 1030003    'A_L_DP_X Axis  Get Motor Status Failed!
    ''' <summary>A_L_DP_X軸等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1030004 = 1030004    'A_L_DP_X Axis  wait INP Timeout!
    ''' <summary>A_L_DP_X軸移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1030005 = 1030005    'A_L_DP_X Axis  Command is Out of SPEL
    ''' <summary>A_L_DP_X軸移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1030006 = 1030006    'A_L_DP_X Axis  Command is Out of SNEL
    ''' <summary>A_L_DP_X軸接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1030007 = 1030007    'A_L_DP_X Axis  Touch HPEL
    ''' <summary>A_L_DP_X軸接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1030008 = 1030008    'A_L_DP_X Axis  Touch HNEL
    ''' <summary>A_L_DP_X軸錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1030009 = 1030009    'A_L_DP_X Axis  Error Stop
    ''' <summary>A_L_DP_X軸參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1030010 = 1030010    'A_L_DP_X Axis  Invalid Parameter
    ''' <summary>A_L_DP_X軸加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1030011 = 1030011    'A_L_DP_X Axis  Invalid Acc
    ''' <summary>A_L_DP_X軸減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1030012 = 1030012    'A_L_DP_X Axis  Invalid Dec
    ''' <summary>A_L_DP_X軸最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1030013 = 1030013    'A_L_DP_X Axis  Invalid VelHigh
    ''' <summary>A_L_DP_X軸初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1030014 = 1030014    'A_L_DP_X Axis  Invalid VelLow
    ''' <summary>A_L_DP_X軸觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1030015 = 1030015    'A_L_DP_X Axis  Cmp Table Error.
    ''' <summary>A_L_DP_X軸復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1030016 = 1030016    'A_L_DP_X Axis  Command Home Error!
    ''' <summary>A_L_DP_X軸設定速度失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1030017 = 1030017    'A_L_DP_X Axis SetSpeed Error!
    ''' <summary>A_L_DP_X軸命令執行失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1030018 = 1030018    'A_L_DP_X Axis Command Error!
    ''' <summary>A_L_DP_X軸命令執行逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1030019 = 1030019    'A_L_DP_X Axis X Command is TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1030020 = 1030020    '
    ''' <summary>A_L_DP_Y軸 移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1031000 = 1031000    'A_L_DP_Y Axis  Move Error!
    ''' <summary>A_L_DP_Y軸 復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1031001 = 1031001    'A_L_DP_Y Axis  wait Home Timeout!
    ''' <summary>A_L_DP_Y軸 馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1031002 = 1031002    'A_L_DP_Y Axis  is Alarm!
    ''' <summary>A_L_DP_Y軸 馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1031003 = 1031003    'A_L_DP_Y Axis  Get Motor Status Failed!
    ''' <summary>A_L_DP_Y軸 等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1031004 = 1031004    'A_L_DP_Y Axis  wait INP Timeout!
    ''' <summary>A_L_DP_Y軸 移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1031005 = 1031005    'A_L_DP_Y Axis  Command is Out of SPEL
    ''' <summary>A_L_DP_Y軸 移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1031006 = 1031006    'A_L_DP_Y Axis  Command is Out of SNEL
    ''' <summary>A_L_DP_Y軸 接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1031007 = 1031007    'A_L_DP_Y Axis  Touch HPEL
    ''' <summary>A_L_DP_Y軸 接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1031008 = 1031008    'A_L_DP_Y Axis  Touch HNEL
    ''' <summary>A_L_DP_Y軸 錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1031009 = 1031009    'A_L_DP_Y Axis  Error Stop
    ''' <summary>A_L_DP_Y軸 參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1031010 = 1031010    'A_L_DP_Y Axis  Invalid Parameter
    ''' <summary>A_L_DP_Y軸 加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1031011 = 1031011    'A_L_DP_Y Axis  Invalid Acc
    ''' <summary>A_L_DP_Y軸 減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1031012 = 1031012    'A_L_DP_Y Axis  Invalid Dec
    ''' <summary>A_L_DP_Y軸 最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1031013 = 1031013    'A_L_DP_Y Axis  Invalid VelHigh
    ''' <summary>A_L_DP_Y軸 初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1031014 = 1031014    'A_L_DP_Y Axis  Invalid VelLow
    ''' <summary>A_L_DP_Y軸 觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1031015 = 1031015    'A_L_DP_Y Axis  Cmp Table Error.
    ''' <summary>A_L_DP_Y軸 復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1031016 = 1031016    'A_L_DP_Y Axis  Command Home Error!
    ''' <summary>A_L_DP_Y軸設定速度失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1031017 = 1031017    'A_L_DP_Y Axis SetSpeed Error!
    ''' <summary>A_L_DP_Y軸命令執行失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1031018 = 1031018    'A_L_DP_Y Axis Command Error!
    ''' <summary>A_L_DP_Y軸命令執行逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1031019 = 1031019    'A_L_DP_Y Axis X Command is TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1031020 = 1031020    '
    ''' <summary>A_L_DP_Z軸 移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1032000 = 1032000    'A_L_DP_Z Axis Move Error!
    ''' <summary>A_L_DP_Z軸 復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1032001 = 1032001    'A_L_DP_Z Axis wait Home Timeout!
    ''' <summary>A_L_DP_Z軸 馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1032002 = 1032002    'A_L_DP_Z Axis is Alarm!
    ''' <summary>A_L_DP_Z軸 馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1032003 = 1032003    'A_L_DP_Z Axis Get Motor Status Failed!
    ''' <summary>A_L_DP_Z軸 等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1032004 = 1032004    'A_L_DP_Z Axis wait INP Timeout!
    ''' <summary>A_L_DP_Z軸 移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1032005 = 1032005    'A_L_DP_Z Axis Command is Out of SPEL
    ''' <summary>A_L_DP_Z軸 移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1032006 = 1032006    'A_L_DP_Z Axis Command is Out of SNEL
    ''' <summary>A_L_DP_Z軸 接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1032007 = 1032007    'A_L_DP_Z Axis Touch HPEL
    ''' <summary>A_L_DP_Z軸 接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1032008 = 1032008    'A_L_DP_Z Axis Touch HNEL
    ''' <summary>A_L_DP_Z軸 錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1032009 = 1032009    'A_L_DP_Z Axis Error Stop
    ''' <summary>A_L_DP_Z軸 參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1032010 = 1032010    'A_L_DP_Z Axis Invalid Parameter
    ''' <summary>A_L_DP_Z軸 加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1032011 = 1032011    'A_L_DP_Z Axis Invalid Acc
    ''' <summary>A_L_DP_Z軸 減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1032012 = 1032012    'A_L_DP_Z Axis Invalid Dec
    ''' <summary>A_L_DP_Z軸 最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1032013 = 1032013    'A_L_DP_Z Axis Invalid VelHigh
    ''' <summary>A_L_DP_Z軸 初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1032014 = 1032014    'A_L_DP_Z Axis Invalid VelLow
    ''' <summary>A_L_DP_Z軸 觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1032015 = 1032015    'A_L_DP_Z Axis Cmp Table Error.
    ''' <summary>A_L_DP_Z軸 復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1032016 = 1032016    'A_L_DP_Z Axis Command Home Error!
    ''' <summary>A_L_DP_Z軸設定速度失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1032017 = 1032017    'A_L_DP_Z Axis SetSpeed Error!
    ''' <summary>A_L_DP_Z軸命令執行失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1032018 = 1032018    'A_L_DP_Z Axis Command Error!
    ''' <summary>A_L_DP_Z軸命令執行逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1032019 = 1032019    'A_L_DP_Z Axis X Command is TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1032020 = 1032020    '
    ''' <summary>Y2軸命令執行失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1033000 = 1033000    'Y2 Axis Command Error!
    ''' <summary>Y2軸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1033001 = 1033001    'Y2 Axis wait Home Timeout!
    ''' <summary>Y2軸馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1033002 = 1033002    'Y2 Axis is Alarm!
    ''' <summary>Y2軸馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1033003 = 1033003    'Y2 Axis Get Motor Status Failed!
    ''' <summary>Y2軸等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1033004 = 1033004    'Y2 Axis wait INP Timeout!
    ''' <summary>Y2軸移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1033005 = 1033005    'Y2 Axis Command is Out of SPEL
    ''' <summary>Y2軸移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1033006 = 1033006    'Y2 Axis Command is Out of SNEL
    ''' <summary>Y2軸接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1033007 = 1033007    'Y2 Axis Touch HPEL
    ''' <summary>Y2軸接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1033008 = 1033008    'Y2 Axis Touch HNEL
    ''' <summary>Y2軸錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1033009 = 1033009    'Y2 Axis Error Stop
    ''' <summary>Y2軸參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1033010 = 1033010    'Y2 Axis Invalid Parameter
    ''' <summary>Y2軸加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1033011 = 1033011    'Y2 Axis Invalid Acc
    ''' <summary>Y2軸減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1033012 = 1033012    'Y2 Axis Invalid Dec
    ''' <summary>Y2軸最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1033013 = 1033013    'Y2 Axis Invalid VelHigh
    ''' <summary>Y2軸初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1033014 = 1033014    'Y2 Axis Invalid VelLow
    ''' <summary>Y2軸觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1033015 = 1033015    'Y2 Axis Cmp Table Error.
    ''' <summary>Y2軸復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1033016 = 1033016    'Y2 Axis Command Home Error!
    ''' <summary>Y2軸安全位置等待逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1033017 = 1033017    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1033018 = 1033018    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1033019 = 1033019    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1033020 = 1033020    '
    ''' <summary>A_L_DP_Tilt移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1034000 = 1034000    'A_L_DP_Tilt  Move Error!
    ''' <summary>A_L_DP_Tilt復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1034001 = 1034001    'A_L_DP_Tilt wait Home Timeout!
    ''' <summary>A_L_DP_Tilt馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1034002 = 1034002    'A_L_DP_Tilt is Alarm!
    ''' <summary>A_L_DP_Tilt馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1034003 = 1034003    'A_L_DP_Tilt Get Motor Status Failed!
    ''' <summary>A_L_DP_Tilt等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1034004 = 1034004    'A_L_DP_Tilt wait INP Timeout!
    ''' <summary>A_L_DP_Tilt移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1034005 = 1034005    'A_L_DP_Tilt Command is Out of SPEL
    ''' <summary>A_L_DP_Tilt移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1034006 = 1034006    'A_L_DP_Tilt Command is Out of SNEL
    ''' <summary>A_L_DP_Tilt接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1034007 = 1034007    'A_L_DP_Tilt Touch HPEL
    ''' <summary>A_L_DP_Tilt接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1034008 = 1034008    'A_L_DP_Tilt Touch HNEL
    ''' <summary>A_L_DP_Tilt錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1034009 = 1034009    'A_L_DP_Tilt Error Stop
    ''' <summary>A_L_DP_Tilt參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1034010 = 1034010    'A_L_DP_Tilt Invalid Parameter
    ''' <summary>A_L_DP_Tilt加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1034011 = 1034011    'A_L_DP_Tilt Invalid Acc
    ''' <summary>A_L_DP_Tilt減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1034012 = 1034012    'A_L_DP_Tilt Invalid Dec
    ''' <summary>A_L_DP_Tilt最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1034013 = 1034013    'A_L_DP_Tilt Invalid VelHigh
    ''' <summary>A_L_DP_Tilt初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1034014 = 1034014    'A_L_DP_Tilt Invalid VelLow
    ''' <summary>A_L_DP_Tilt觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1034015 = 1034015    'A_L_DP_Tilt Cmp Table Error.
    ''' <summary>A_L_DP_Tilt復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1034016 = 1034016    'A_L_DP_Tilt Command Home Error!
    ''' <summary>A_L_DP_Tilt軸設定速度失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1034017 = 1034017    'A_L_DP_Tilt Axis SetSpeed Error!
    ''' <summary>A_L_DP_Tilt命令執行失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1034018 = 1034018    'A_L_DP_Tilt  Command Error!
    ''' <summary>A_L_DP_Tilt命令執行逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1034019 = 1034019    'A_L_DP_Tilt Command is TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1034020 = 1034020    '
    ''' <summary>運動群組1移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1036000 = 1036000    'Group1 Move Error!
    ''' <summary>運動群組1復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1036001 = 1036001    'Group1 wait Home Timeout!
    ''' <summary>運動群組1馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1036002 = 1036002    'Group1 is Alarm!
    ''' <summary>運動群組1馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1036003 = 1036003    'Group1 Get Motor Status Failed!
    ''' <summary>運動群組1等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1036004 = 1036004    'Group1 wait INP Timeout!
    ''' <summary>運動群組1移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1036005 = 1036005    'Group1 Command is Out of SPEL
    ''' <summary>運動群組1移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1036006 = 1036006    'Group1 Command is Out of SNEL
    ''' <summary>運動群組1接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1036007 = 1036007    'Group1 Touch HPEL
    ''' <summary>運動群組1接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1036008 = 1036008    'Group1 Touch HNEL
    ''' <summary>運動群組1錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1036009 = 1036009    'Group1 Error Stop
    ''' <summary>運動群組1參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1036010 = 1036010    'Group1 Invalid Parameter
    ''' <summary>運動群組1加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1036011 = 1036011    'Group1 Invalid Acc
    ''' <summary>運動群組1減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1036012 = 1036012    'Group1 Invalid Dec
    ''' <summary>運動群組1最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1036013 = 1036013    'Group1 Invalid VelHigh
    ''' <summary>運動群組1初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1036014 = 1036014    'Group1 Invalid VelLow
    ''' <summary>運動群組1觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1036015 = 1036015    'Group1 Cmp Table Error.
    ''' <summary>運動群組1復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1036016 = 1036016    'Group1 Command Home Error!
    ''' <summary>運動群組1命令執行失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1036017 = 1036017    'Group1 Command Error!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1036018 = 1036018    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1036019 = 1036019    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1036020 = 1036020    '
    ''' <summary>Conveyor1軸命令執行失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1037000 = 1037000    'Conveyor1 Axis Command Error!
    ''' <summary>Conveyor1軸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1037001 = 1037001    'Conveyor1Axis wait Home Timeout!
    ''' <summary>Conveyor1軸馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1037002 = 1037002    'Conveyor1Axis is Alarm!
    ''' <summary>Conveyor1軸馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1037003 = 1037003    'Conveyor1Axis Get Motor Status Failed!
    ''' <summary>Conveyor1軸等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1037004 = 1037004    'Conveyor1Axis wait INP Timeout!
    ''' <summary>Conveyor1軸移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1037005 = 1037005    'Conveyor1Axis Command is Out of SPEL
    ''' <summary>Conveyor1軸移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1037006 = 1037006    'Conveyor1Axis Command is Out of SNEL
    ''' <summary>Conveyor1軸接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1037007 = 1037007    'Conveyor1Axis Touch HPEL
    ''' <summary>Conveyor1軸接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1037008 = 1037008    'Conveyor1Axis Touch HNEL
    ''' <summary>Conveyor1軸錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1037009 = 1037009    'Conveyor1Axis Error Stop
    ''' <summary>Conveyor1軸參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1037010 = 1037010    'Conveyor1Axis Invalid Parameter
    ''' <summary>Conveyor1軸加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1037011 = 1037011    'Conveyor1Axis Invalid Acc
    ''' <summary>Conveyor1軸減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1037012 = 1037012    'Conveyor1Axis Invalid Dec
    ''' <summary>Conveyor1軸最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1037013 = 1037013    'Conveyor1Axis Invalid VelHigh
    ''' <summary>Conveyor1軸初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1037014 = 1037014    'Conveyor1Axis Invalid VelLow
    ''' <summary>Conveyor1觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1037015 = 1037015    'Conveyor1 Cmp Table Error.
    ''' <summary>Conveyor1復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1037016 = 1037016    'Conveyor1 Command Home Error!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1037017 = 1037017    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1037018 = 1037018    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1037019 = 1037019    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1037020 = 1037020    '
    ''' <summary>Conveyor2軸命令執行失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1038000 = 1038000    'Conveyor2 Axis Command Error!
    ''' <summary>Conveyor2軸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1038001 = 1038001    'Conveyor2 Axis wait Home Timeout!
    ''' <summary>Conveyor2軸馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1038002 = 1038002    'Conveyor2 Axis is Alarm!
    ''' <summary>Conveyor2軸馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1038003 = 1038003    'Conveyor2 Axis Get Motor Status Failed!
    ''' <summary>Conveyor2軸等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1038004 = 1038004    'Conveyor2 Axis wait INP Timeout!
    ''' <summary>Conveyor2軸移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1038005 = 1038005    'Conveyor2 Axis Command is Out of SPEL
    ''' <summary>Conveyor2軸移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1038006 = 1038006    'Conveyor2 Axis Command is Out of SNEL
    ''' <summary>Conveyor2軸接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1038007 = 1038007    'Conveyor2 Axis Touch HPEL
    ''' <summary>Conveyor2軸接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1038008 = 1038008    'Conveyor2 Axis Touch HNEL
    ''' <summary>Conveyor2軸錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1038009 = 1038009    'Conveyor2 Axis Error Stop
    ''' <summary>Conveyor2軸參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1038010 = 1038010    'Conveyor2 Axis Invalid Parameter
    ''' <summary>Conveyor2軸加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1038011 = 1038011    'Conveyor2 Axis Invalid Acc
    ''' <summary>Conveyor2軸減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1038012 = 1038012    'Conveyor2 Axis Invalid Dec
    ''' <summary>Conveyor2軸最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1038013 = 1038013    'Conveyor2 Axis Invalid VelHigh
    ''' <summary>Conveyor2軸初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1038014 = 1038014    'Conveyor2 Axis Invalid VelLow
    ''' <summary>Conveyor2觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1038015 = 1038015    'Conveyor2 Cmp Table Error.
    ''' <summary>Conveyor2復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1038016 = 1038016    'Conveyor2 Command Home Error!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1038017 = 1038017    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1038018 = 1038018    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1038019 = 1038019    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1038020 = 1038020    '
    ''' <summary>S1軸命令執行失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1039000 = 1039000    'S1 Axis Command Error!
    ''' <summary>S1軸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1039001 = 1039001    'S1 Axis wait Home Timeout!
    ''' <summary>S1軸馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1039002 = 1039002    'S1 Axis is Alarm!
    ''' <summary>S1軸馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1039003 = 1039003    'S1 Axis Get Motor Status Failed!
    ''' <summary>S1軸等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1039004 = 1039004    'S1 Axis wait INP Timeout!
    ''' <summary>S1軸移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1039005 = 1039005    'S1 Axis Command is Out of SPEL
    ''' <summary>S1軸移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1039006 = 1039006    'S1 Axis Command is Out of SNEL
    ''' <summary>S1軸接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1039007 = 1039007    'S1 Axis Touch HPEL
    ''' <summary>S1軸接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1039008 = 1039008    'S1 Axis Touch HNEL
    ''' <summary>S1軸錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1039009 = 1039009    'S1 Axis Error Stop
    ''' <summary>S1軸參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1039010 = 1039010    'S1 Axis Invalid Parameter
    ''' <summary>S1軸加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1039011 = 1039011    'S1 Axis Invalid Acc
    ''' <summary>S1軸減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1039012 = 1039012    'S1 Axis Invalid Dec
    ''' <summary>S1軸最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1039013 = 1039013    'S1 Axis Invalid VelHigh
    ''' <summary>S1軸初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1039014 = 1039014    'S1 Axis Invalid VelLow
    ''' <summary>S1軸觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1039015 = 1039015    'S1 Axis Cmp Table Error.
    ''' <summary>S1軸復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1039016 = 1039016    'S1 Axis Command Home Error!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1039017 = 1039017    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1039018 = 1039018    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1039019 = 1039019    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1039020 = 1039020    '
    ''' <summary>S2軸命令執行失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1040000 = 1040000    'S2 Axis Command Error!
    ''' <summary>S2軸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1040001 = 1040001    'S2 Axis wait Home Timeout!
    ''' <summary>S2軸馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1040002 = 1040002    'S2 Axis is Alarm!
    ''' <summary>S2軸馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1040003 = 1040003    'S2 Axis Get Motor Status Failed!
    ''' <summary>S2軸等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1040004 = 1040004    'S2 Axis wait INP Timeout!
    ''' <summary>S2軸移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1040005 = 1040005    'S2 Axis Command is Out of SPEL
    ''' <summary>S2軸移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1040006 = 1040006    'S2 Axis Command is Out of SNEL
    ''' <summary>S2軸接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1040007 = 1040007    'S2 Axis Touch HPEL
    ''' <summary>S2軸接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1040008 = 1040008    'S2 Axis Touch HNEL
    ''' <summary>S2軸錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1040009 = 1040009    'S2 Axis Error Stop
    ''' <summary>S2軸參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1040010 = 1040010    'S2 Axis Invalid Parameter
    ''' <summary>S2軸加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1040011 = 1040011    'S2 Axis Invalid Acc
    ''' <summary>S2軸減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1040012 = 1040012    'S2 Axis Invalid Dec
    ''' <summary>S2軸最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1040013 = 1040013    'S2 Axis Invalid VelHigh
    ''' <summary>S2軸初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1040014 = 1040014    'S2 Axis Invalid VelLow
    ''' <summary>S2軸觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1040015 = 1040015    'S2 Axis Cmp Table Error.
    ''' <summary>S2軸復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1040016 = 1040016    'S2 Axis Command Home Error!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1040017 = 1040017    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1040018 = 1040018    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1040019 = 1040019    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1040020 = 1040020    '
    ''' <summary>S3軸命令執行失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1041000 = 1041000    'S3 Axis Command Error!
    ''' <summary>S3軸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1041001 = 1041001    'S3 Axis wait Home Timeout!
    ''' <summary>S3軸馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1041002 = 1041002    'S3 Axis is Alarm!
    ''' <summary>S3軸馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1041003 = 1041003    'S3 Axis Get Motor Status Failed!
    ''' <summary>S3軸等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1041004 = 1041004    'S3 Axis wait INP Timeout!
    ''' <summary>S3軸移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1041005 = 1041005    'S3 Axis Command is Out of SPEL
    ''' <summary>S3軸移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1041006 = 1041006    'S3 Axis Command is Out of SNEL
    ''' <summary>S3軸接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1041007 = 1041007    'S3 Axis Touch HPEL
    ''' <summary>S3軸接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1041008 = 1041008    'S3 Axis Touch HNEL
    ''' <summary>S3軸錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1041009 = 1041009    'S3 Axis Error Stop
    ''' <summary>S3軸參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1041010 = 1041010    'S3 Axis Invalid Parameter
    ''' <summary>S3軸加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1041011 = 1041011    'S3 Axis Invalid Acc
    ''' <summary>S3軸減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1041012 = 1041012    'S3 Axis Invalid Dec
    ''' <summary>S3軸最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1041013 = 1041013    'S3 Axis Invalid VelHigh
    ''' <summary>S3軸初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1041014 = 1041014    'S3 Axis Invalid VelLow
    ''' <summary>S3軸觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1041015 = 1041015    'S3 Axis Cmp Table Error.
    ''' <summary>S3軸復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1041016 = 1041016    'S3 Axis Command Home Error!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1041017 = 1041017    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1041018 = 1041018    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1041019 = 1041019    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1041020 = 1041020    '
    ''' <summary>A_R_DP_X軸移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1042000 = 1042000    'A_R_DP_X Axis Move Error!
    ''' <summary>A_R_DP_X 軸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1042001 = 1042001    'A_R_DP_X Axis wait Home Timeout!
    ''' <summary>A_R_DP_X 軸馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1042002 = 1042002    'A_R_DP_X Axis is Alarm!
    ''' <summary>A_R_DP_X 軸馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1042003 = 1042003    'A_R_DP_X Axis Get Motor Status Failed!
    ''' <summary>A_R_DP_X 軸等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1042004 = 1042004    'A_R_DP_X Axis wait INP Timeout!
    ''' <summary>A_R_DP_X 軸移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1042005 = 1042005    'A_R_DP_X Axis Command is Out of SPEL
    ''' <summary>A_R_DP_X 軸移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1042006 = 1042006    'A_R_DP_X Axis Command is Out of SNEL
    ''' <summary>A_R_DP_X 軸接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1042007 = 1042007    'A_R_DP_X Axis Touch HPEL
    ''' <summary>A_R_DP_X 軸接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1042008 = 1042008    'A_R_DP_X Axis Touch HNEL
    ''' <summary>A_R_DP_X 軸錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1042009 = 1042009    'A_R_DP_X Axis Error Stop
    ''' <summary>A_R_DP_X 軸參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1042010 = 1042010    'A_R_DP_X Axis Invalid Parameter
    ''' <summary>A_R_DP_X 軸加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1042011 = 1042011    'A_R_DP_X Axis Invalid Acc
    ''' <summary>A_R_DP_X 軸減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1042012 = 1042012    'A_R_DP_X Axis Invalid Dec
    ''' <summary>A_R_DP_X 軸最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1042013 = 1042013    'A_R_DP_X Axis Invalid VelHigh
    ''' <summary>A_R_DP_X 軸初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1042014 = 1042014    'A_R_DP_X Axis Invalid VelLow
    ''' <summary>A_R_DP_X 軸觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1042015 = 1042015    'A_R_DP_X Axis Cmp Table Error.
    ''' <summary>A_R_DP_X 軸復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1042016 = 1042016    'A_R_DP_X Axis Command Home Error!
    ''' <summary>A_R_DP_X軸設定速度失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1042017 = 1042017    'A_R_DP_X Axis SetSpeed Error!
    ''' <summary>A_R_DP_X軸命令執行失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1042018 = 1042018    'A_R_DP_X Axis Command Error!
    ''' <summary>A_L_DP_X軸命令執行逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1042019 = 1042019    'A_R_DP_X Axis Command is TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1042020 = 1042020    '
    ''' <summary>A_R_DP_Y軸移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1043000 = 1043000    'A_R_DP_Y Axis Move Error!
    ''' <summary>A_R_DP_Y軸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1043001 = 1043001    'A_R_DP_Y Axis wait Home Timeout!
    ''' <summary>A_R_DP_Y軸馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1043002 = 1043002    'A_R_DP_Y Axis is Alarm!
    ''' <summary>A_R_DP_Y軸馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1043003 = 1043003    'A_R_DP_Y Axis Get Motor Status Failed!
    ''' <summary>A_R_DP_Y軸等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1043004 = 1043004    'A_R_DP_Y Axis wait INP Timeout!
    ''' <summary>A_R_DP_Y軸移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1043005 = 1043005    'A_R_DP_Y Axis Command is Out of SPEL
    ''' <summary>A_R_DP_Y軸移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1043006 = 1043006    'A_R_DP_Y Axis Command is Out of SNEL
    ''' <summary>A_R_DP_Y軸接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1043007 = 1043007    'A_R_DP_Y Axis Touch HPEL
    ''' <summary>A_R_DP_Y軸接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1043008 = 1043008    'A_R_DP_Y Axis Touch HNEL
    ''' <summary>A_R_DP_Y軸錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1043009 = 1043009    'A_R_DP_Y Axis Error Stop
    ''' <summary>A_R_DP_Y軸參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1043010 = 1043010    'A_R_DP_Y Axis Invalid Parameter
    ''' <summary>A_R_DP_Y軸加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1043011 = 1043011    'A_R_DP_Y Axis Invalid Acc
    ''' <summary>A_R_DP_Y軸減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1043012 = 1043012    'A_R_DP_Y Axis Invalid Dec
    ''' <summary>A_R_DP_Y軸最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1043013 = 1043013    'A_R_DP_Y Axis Invalid VelHigh
    ''' <summary>A_R_DP_Y軸初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1043014 = 1043014    'A_R_DP_Y Axis Invalid VelLow
    ''' <summary>A_R_DP_Y軸觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1043015 = 1043015    'A_R_DP_Y Axis Cmp Table Error.
    ''' <summary>A_R_DP_Y軸復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1043016 = 1043016    'A_R_DP_Y Axis Command Home Error!
    ''' <summary>A_R_DP_Y軸設定速度失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1043017 = 1043017    'A_R_DP_Y Axis SetSpeed Error!
    ''' <summary>A_R_DP_Y軸命令執行失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1043018 = 1043018    'A_R_DP_Y Axis Command Error!
    ''' <summary>A_L_DP_Y軸命令執行逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1043019 = 1043019    'A_R_DP_Y Axis Command is TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1043020 = 1043020    '
    ''' <summary>A_R_DP_Z軸移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1044000 = 1044000    'A_R_DP_Z Axis Move Error!
    ''' <summary>A_R_DP_Z 軸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1044001 = 1044001    'A_R_DP_Z Axis wait Home Timeout!
    ''' <summary>A_R_DP_Z 軸馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1044002 = 1044002    'A_R_DP_Z Axis is Alarm!
    ''' <summary>A_R_DP_Z 軸馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1044003 = 1044003    'A_R_DP_Z Axis Get Motor Status Failed!
    ''' <summary>A_R_DP_Z 軸等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1044004 = 1044004    'A_R_DP_Z Axis wait INP Timeout!
    ''' <summary>A_R_DP_Z 軸移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1044005 = 1044005    'A_R_DP_Z Axis Command is Out of SPEL
    ''' <summary>A_R_DP_Z 軸移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1044006 = 1044006    'A_R_DP_Z Axis Command is Out of SNEL
    ''' <summary>A_R_DP_Z 軸接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1044007 = 1044007    'A_R_DP_Z Axis Touch HPEL
    ''' <summary>A_R_DP_Z 軸接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1044008 = 1044008    'A_R_DP_Z Axis Touch HNEL
    ''' <summary>A_R_DP_Z 軸錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1044009 = 1044009    'A_R_DP_Z Axis Error Stop
    ''' <summary>A_R_DP_Z 軸參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1044010 = 1044010    'A_R_DP_Z Axis Invalid Parameter
    ''' <summary>A_R_DP_Z 軸加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1044011 = 1044011    'A_R_DP_Z Axis Invalid Acc
    ''' <summary>A_R_DP_Z 軸減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1044012 = 1044012    'A_R_DP_Z Axis Invalid Dec
    ''' <summary>A_R_DP_Z 軸最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1044013 = 1044013    'A_R_DP_Z Axis Invalid VelHigh
    ''' <summary>A_R_DP_Z 軸初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1044014 = 1044014    'A_R_DP_Z Axis Invalid VelLow
    ''' <summary>A_R_DP_Z 軸觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1044015 = 1044015    'A_R_DP_Z Axis Cmp Table Error.
    ''' <summary>A_R_DP_Z 軸復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1044016 = 1044016    'A_R_DP_Z Axis Command Home Error!
    ''' <summary>A_R_DP_Z軸設定速度失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1044017 = 1044017    'A_R_DP_Z Axis SetSpeed Error!
    ''' <summary>A_R_DP_Z軸命令執行失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1044018 = 1044018    'A_R_DP_Z Axis Command Error!
    ''' <summary>A_L_DP_Z軸命令執行逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1044019 = 1044019    'A_R_DP_Z Axis Command is TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1044020 = 1044020    '
    ''' <summary>V2軸命令執行失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1045000 = 1045000    'V2 Axis Command Error!
    ''' <summary>V2軸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1045001 = 1045001    'V2 Axis wait Home Timeout!
    ''' <summary>V2軸馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1045002 = 1045002    'V2 Axis is Alarm!
    ''' <summary>V2軸馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1045003 = 1045003    'V2 Axis Get Motor Status Failed!
    ''' <summary>V2軸等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1045004 = 1045004    'V2 Axis wait INP Timeout!
    ''' <summary>V2軸移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1045005 = 1045005    'V2 Axis Command is Out of SPEL
    ''' <summary>V2軸移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1045006 = 1045006    'V2 Axis Command is Out of SNEL
    ''' <summary>V2軸接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1045007 = 1045007    'V2 Axis Touch HPEL
    ''' <summary>V2軸接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1045008 = 1045008    'V2 Axis Touch HNEL
    ''' <summary>V2軸錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1045009 = 1045009    'V2 Axis Error Stop
    ''' <summary>V2軸參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1045010 = 1045010    'V2 Axis Invalid Parameter
    ''' <summary>V2軸加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1045011 = 1045011    'V2 Axis Invalid Acc
    ''' <summary>V2軸減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1045012 = 1045012    'V2 Axis Invalid Dec
    ''' <summary>V2軸最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1045013 = 1045013    'V2 Axis Invalid VelHigh
    ''' <summary>V2軸初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1045014 = 1045014    'V2 Axis Invalid VelLow
    ''' <summary>V2軸觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1045015 = 1045015    'V2 Axis Cmp Table Error.
    ''' <summary>V2軸復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1045016 = 1045016    'V2 Axis Command Home Error!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1045017 = 1045017    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1045018 = 1045018    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1045019 = 1045019    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1045020 = 1045020    '
    ''' <summary>A_R_DP_Tilt移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1046000 = 1046000    'A_R_DP_Tilt  Move Error!
    ''' <summary>A_R_DP_Tilt復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1046001 = 1046001    'A_R_DP_Tilt wait Home Timeout!
    ''' <summary>A_R_DP_Tilt馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1046002 = 1046002    'A_R_DP_Tilt is Alarm!
    ''' <summary>A_R_DP_Tilt馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1046003 = 1046003    'A_R_DP_Tilt Get Motor Status Failed!
    ''' <summary>A_R_DP_Tilt等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1046004 = 1046004    'A_R_DP_Tilt wait INP Timeout!
    ''' <summary>A_R_DP_Tilt移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1046005 = 1046005    'A_R_DP_Tilt Command is Out of SPEL
    ''' <summary>A_R_DP_Tilt移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1046006 = 1046006    'A_R_DP_Tilt Command is Out of SNEL
    ''' <summary>A_R_DP_Tilt接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1046007 = 1046007    'A_R_DP_Tilt Touch HPEL
    ''' <summary>A_R_DP_Tilt接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1046008 = 1046008    'A_R_DP_Tilt Touch HNEL
    ''' <summary>A_R_DP_Tilt錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1046009 = 1046009    'A_R_DP_Tilt Error Stop
    ''' <summary>A_R_DP_Tilt參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1046010 = 1046010    'A_R_DP_Tilt Invalid Parameter
    ''' <summary>A_R_DP_Tilt加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1046011 = 1046011    'A_R_DP_Tilt Invalid Acc
    ''' <summary>A_R_DP_Tilt減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1046012 = 1046012    'A_R_DP_Tilt Invalid Dec
    ''' <summary>A_R_DP_Tilt最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1046013 = 1046013    'A_R_DP_Tilt Invalid VelHigh
    ''' <summary>A_R_DP_Tilt初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1046014 = 1046014    'A_R_DP_Tilt Invalid VelLow
    ''' <summary>A_R_DP_Tilt觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1046015 = 1046015    'A_R_DP_Tilt Cmp Table Error.
    ''' <summary>A_R_DP_Tilt復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1046016 = 1046016    'A_R_DP_Tilt Command Home Error!
    ''' <summary>A_R_DP_Tilt軸設定速度失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1046017 = 1046017    'A_R_DP_Tilt Axis SetSpeed Error!
    ''' <summary>A_R_DP_Tilt命令執行失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1046018 = 1046018    'A_R_DP_Tilt  Command Error!
    ''' <summary>A_L_DP_Tilt命令執行逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1046019 = 1046019    'A_R_DP_Tilt Command is TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1046020 = 1046020    '
    ''' <summary>運動群組2移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1048000 = 1048000    'Group2 Move Error!
    ''' <summary>運動群組2復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1048001 = 1048001    'Group2 wait Home Timeout!
    ''' <summary>運動群組2馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1048002 = 1048002    'Group2 is Alarm!
    ''' <summary>運動群組2馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1048003 = 1048003    'Group2 Get Motor Status Failed!
    ''' <summary>運動群組2等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1048004 = 1048004    'Group2 wait INP Timeout!
    ''' <summary>運動群組2移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1048005 = 1048005    'Group2 Command is Out of SPEL
    ''' <summary>運動群組2移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1048006 = 1048006    'Group2 Command is Out of SNEL
    ''' <summary>運動群組2接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1048007 = 1048007    'Group2 Touch HPEL
    ''' <summary>運動群組2接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1048008 = 1048008    'Group2 Touch HNEL
    ''' <summary>運動群組2錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1048009 = 1048009    'Group2 Error Stop
    ''' <summary>運動群組2參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1048010 = 1048010    'Group2 Invalid Parameter
    ''' <summary>運動群組2加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1048011 = 1048011    'Group2 Invalid Acc
    ''' <summary>運動群組2減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1048012 = 1048012    'Group2 Invalid Dec
    ''' <summary>運動群組2最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1048013 = 1048013    'Group2 Invalid VelHigh
    ''' <summary>運動群組2初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1048014 = 1048014    'Group2 Invalid VelLow
    ''' <summary>運動群組2觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1048015 = 1048015    'Group2 Cmp Table Error.
    ''' <summary>運動群組2復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1048016 = 1048016    'Group2 Command Home Error!
    ''' <summary>運動群組2命令執行失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1048017 = 1048017    'Group2 Command Error!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1048018 = 1048018    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1048019 = 1048019    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1048020 = 1048020    '
    ''' <summary>Conveyor1軸命令執行失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1049000 = 1049000    'Conveyor1 Axis Command Error!
    ''' <summary>Conveyor1軸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1049001 = 1049001    'Conveyor1Axis wait Home Timeout!
    ''' <summary>Conveyor1軸馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1049002 = 1049002    'Conveyor1Axis is Alarm!
    ''' <summary>Conveyor1軸馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1049003 = 1049003    'Conveyor1Axis Get Motor Status Failed!
    ''' <summary>Conveyor1軸等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1049004 = 1049004    'Conveyor1Axis wait INP Timeout!
    ''' <summary>Conveyor1軸移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1049005 = 1049005    'Conveyor1Axis Command is Out of SPEL
    ''' <summary>Conveyor1軸移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1049006 = 1049006    'Conveyor1Axis Command is Out of SNEL
    ''' <summary>Conveyor1軸接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1049007 = 1049007    'Conveyor1Axis Touch HPEL
    ''' <summary>Conveyor1軸接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1049008 = 1049008    'Conveyor1Axis Touch HNEL
    ''' <summary>Conveyor1軸錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1049009 = 1049009    'Conveyor1Axis Error Stop
    ''' <summary>Conveyor1軸參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1049010 = 1049010    'Conveyor1Axis Invalid Parameter
    ''' <summary>Conveyor1軸加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1049011 = 1049011    'Conveyor1Axis Invalid Acc
    ''' <summary>Conveyor1軸減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1049012 = 1049012    'Conveyor1Axis Invalid Dec
    ''' <summary>Conveyor1軸最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1049013 = 1049013    'Conveyor1Axis Invalid VelHigh
    ''' <summary>Conveyor1軸初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1049014 = 1049014    'Conveyor1Axis Invalid VelLow
    ''' <summary>Conveyor1觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1049015 = 1049015    'Conveyor1 Cmp Table Error.
    ''' <summary>Conveyor1復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1049016 = 1049016    'Conveyor1 Command Home Error!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1049017 = 1049017    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1049018 = 1049018    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1049019 = 1049019    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1049020 = 1049020    '
    ''' <summary>Conveyor2軸命令執行失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1050000 = 1050000    'Conveyor2 Axis Command Error!
    ''' <summary>Conveyor2軸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1050001 = 1050001    'Conveyor2 Axis wait Home Timeout!
    ''' <summary>Conveyor2軸馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1050002 = 1050002    'Conveyor2 Axis is Alarm!
    ''' <summary>Conveyor2軸馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1050003 = 1050003    'Conveyor2 Axis Get Motor Status Failed!
    ''' <summary>Conveyor2軸等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1050004 = 1050004    'Conveyor2 Axis wait INP Timeout!
    ''' <summary>Conveyor2軸移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1050005 = 1050005    'Conveyor2 Axis Command is Out of SPEL
    ''' <summary>Conveyor2軸移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1050006 = 1050006    'Conveyor2 Axis Command is Out of SNEL
    ''' <summary>Conveyor2軸接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1050007 = 1050007    'Conveyor2 Axis Touch HPEL
    ''' <summary>Conveyor2軸接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1050008 = 1050008    'Conveyor2 Axis Touch HNEL
    ''' <summary>Conveyor2軸錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1050009 = 1050009    'Conveyor2 Axis Error Stop
    ''' <summary>Conveyor2軸參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1050010 = 1050010    'Conveyor2 Axis Invalid Parameter
    ''' <summary>Conveyor2軸加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1050011 = 1050011    'Conveyor2 Axis Invalid Acc
    ''' <summary>Conveyor2軸減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1050012 = 1050012    'Conveyor2 Axis Invalid Dec
    ''' <summary>Conveyor2軸最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1050013 = 1050013    'Conveyor2 Axis Invalid VelHigh
    ''' <summary>Conveyor2軸初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1050014 = 1050014    'Conveyor2 Axis Invalid VelLow
    ''' <summary>Conveyor2觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1050015 = 1050015    'Conveyor2 Cmp Table Error.
    ''' <summary>Conveyor2復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1050016 = 1050016    'Conveyor2 Command Home Error!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1050017 = 1050017    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1050018 = 1050018    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1050019 = 1050019    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1050020 = 1050020    '
    ''' <summary>S1軸命令執行失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1051000 = 1051000    'S1 Axis Command Error!
    ''' <summary>S1軸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1051001 = 1051001    'S1 Axis wait Home Timeout!
    ''' <summary>S1軸馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1051002 = 1051002    'S1 Axis is Alarm!
    ''' <summary>S1軸馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1051003 = 1051003    'S1 Axis Get Motor Status Failed!
    ''' <summary>S1軸等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1051004 = 1051004    'S1 Axis wait INP Timeout!
    ''' <summary>S1軸移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1051005 = 1051005    'S1 Axis Command is Out of SPEL
    ''' <summary>S1軸移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1051006 = 1051006    'S1 Axis Command is Out of SNEL
    ''' <summary>S1軸接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1051007 = 1051007    'S1 Axis Touch HPEL
    ''' <summary>S1軸接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1051008 = 1051008    'S1 Axis Touch HNEL
    ''' <summary>S1軸錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1051009 = 1051009    'S1 Axis Error Stop
    ''' <summary>S1軸參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1051010 = 1051010    'S1 Axis Invalid Parameter
    ''' <summary>S1軸加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1051011 = 1051011    'S1 Axis Invalid Acc
    ''' <summary>S1軸減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1051012 = 1051012    'S1 Axis Invalid Dec
    ''' <summary>S1軸最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1051013 = 1051013    'S1 Axis Invalid VelHigh
    ''' <summary>S1軸初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1051014 = 1051014    'S1 Axis Invalid VelLow
    ''' <summary>S1軸觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1051015 = 1051015    'S1 Axis Cmp Table Error.
    ''' <summary>S1軸復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1051016 = 1051016    'S1 Axis Command Home Error!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1051017 = 1051017    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1051018 = 1051018    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1051019 = 1051019    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1051020 = 1051020    '
    ''' <summary>A_Chuck_Z1軸命令執行失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1052000 = 1052000    'A_Chuck_Z1 Axis Command Error!
    ''' <summary>A_Chuck_Z1軸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1052001 = 1052001    'A_Chuck_Z1 Axis wait Home Timeout!
    ''' <summary>A_Chuck_Z1軸馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1052002 = 1052002    'A_Chuck_Z1 Axis is Alarm!
    ''' <summary>A_Chuck_Z1軸馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1052003 = 1052003    'A_Chuck_Z1 Axis Get Motor Status Failed!
    ''' <summary>A_Chuck_Z1軸等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1052004 = 1052004    'A_Chuck_Z1 Axis wait INP Timeout!
    ''' <summary>A_Chuck_Z1軸移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1052005 = 1052005    'A_Chuck_Z1 Axis Command is Out of SPEL
    ''' <summary>A_Chuck_Z1軸移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1052006 = 1052006    'A_Chuck_Z1 Axis Command is Out of SNEL
    ''' <summary>A_Chuck_Z1軸接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1052007 = 1052007    'A_Chuck_Z1 Axis Touch HPEL
    ''' <summary>A_Chuck_Z1軸接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1052008 = 1052008    'A_Chuck_Z1 Axis Touch HNEL
    ''' <summary>A_Chuck_Z1軸錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1052009 = 1052009    'A_Chuck_Z1 Axis Error Stop
    ''' <summary>A_Chuck_Z1軸參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1052010 = 1052010    'A_Chuck_Z1 Axis Invalid Parameter
    ''' <summary>A_Chuck_Z1軸加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1052011 = 1052011    'A_Chuck_Z1 Axis Invalid Acc
    ''' <summary>A_Chuck_Z1軸減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1052012 = 1052012    'A_Chuck_Z1 Axis Invalid Dec
    ''' <summary>A_Chuck_Z1軸最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1052013 = 1052013    'A_Chuck_Z1 Axis Invalid VelHigh
    ''' <summary>A_Chuck_Z1軸初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1052014 = 1052014    'A_Chuck_Z1 Axis Invalid VelLow
    ''' <summary>A_Chuck_Z1軸觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1052015 = 1052015    'A_Chuck_Z1 Axis Cmp Table Error.
    ''' <summary>A_Chuck_Z1軸復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1052016 = 1052016    'A_Chuck_Z1 Axis Command Home Error!
    ''' <summary>A_Chuck_Z1設定速度異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1052017 = 1052017    'A_Chuck_Z1 Set Speed Error!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1052018 = 1052018    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1052019 = 1052019    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1052020 = 1052020    '
    ''' <summary>B_Chuck_Z1軸命令執行失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1053000 = 1053000    'B_Chuck_Z1 Axis Command Error!
    ''' <summary>B_Chuck_Z1軸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1053001 = 1053001    'B_Chuck_Z1 Axis wait Home Timeout!
    ''' <summary>B_Chuck_Z1軸馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1053002 = 1053002    'B_Chuck_Z1 Axis is Alarm!
    ''' <summary>B_Chuck_Z1軸馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1053003 = 1053003    'B_Chuck_Z1 Axis Get Motor Status Failed!
    ''' <summary>B_Chuck_Z1軸等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1053004 = 1053004    'B_Chuck_Z1 Axis wait INP Timeout!
    ''' <summary>B_Chuck_Z1軸移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1053005 = 1053005    'B_Chuck_Z1 Axis Command is Out of SPEL
    ''' <summary>B_Chuck_Z1軸移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1053006 = 1053006    'B_Chuck_Z1 Axis Command is Out of SNEL
    ''' <summary>B_Chuck_Z1軸接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1053007 = 1053007    'B_Chuck_Z1 Axis Touch HPEL
    ''' <summary>B_Chuck_Z1軸接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1053008 = 1053008    'B_Chuck_Z1 Axis Touch HNEL
    ''' <summary>B_Chuck_Z1軸錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1053009 = 1053009    'B_Chuck_Z1 Axis Error Stop
    ''' <summary>B_Chuck_Z1軸參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1053010 = 1053010    'B_Chuck_Z1 Axis Invalid Parameter
    ''' <summary>B_Chuck_Z1軸加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1053011 = 1053011    'B_Chuck_Z1 Axis Invalid Acc
    ''' <summary>B_Chuck_Z1軸減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1053012 = 1053012    'B_Chuck_Z1 Axis Invalid Dec
    ''' <summary>B_Chuck_Z1軸最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1053013 = 1053013    'B_Chuck_Z1 Axis Invalid VelHigh
    ''' <summary>B_Chuck_Z1軸初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1053014 = 1053014    'B_Chuck_Z1 Axis Invalid VelLow
    ''' <summary>B_Chuck_Z1軸觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1053015 = 1053015    'B_Chuck_Z1 Axis Cmp Table Error.
    ''' <summary>B_Chuck_Z1軸復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1053016 = 1053016    'B_Chuck_Z1 Axis Command Home Error!
    ''' <summary>B_Chuck_Z1設定速度異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1053017 = 1053017    'B_Chuck_Z1 Set Speed Error!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1053018 = 1053018    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1053019 = 1053019    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1053020 = 1053020    '
    ''' <summary>B_L_DP_X軸移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1060000 = 1060000    'B_L_DP_X Axis Move Error!
    ''' <summary>B_L_DP_X 軸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1060001 = 1060001    'B_L_DP_X Axis wait Home Timeout!
    ''' <summary>B_L_DP_X 軸馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1060002 = 1060002    'B_L_DP_X Axis is Alarm!
    ''' <summary>B_L_DP_X 軸馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1060003 = 1060003    'B_L_DP_X Axis Get Motor Status Failed!
    ''' <summary>B_L_DP_X 軸等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1060004 = 1060004    'B_L_DP_X Axis wait INP Timeout!
    ''' <summary>B_L_DP_X 軸移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1060005 = 1060005    'B_L_DP_X Axis Command is Out of SPEL
    ''' <summary>B_L_DP_X 軸移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1060006 = 1060006    'B_L_DP_X Axis Command is Out of SNEL
    ''' <summary>B_L_DP_X 軸接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1060007 = 1060007    'B_L_DP_X Axis Touch HPEL
    ''' <summary>B_L_DP_X 軸接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1060008 = 1060008    'B_L_DP_X Axis Touch HNEL
    ''' <summary>B_L_DP_X 軸錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1060009 = 1060009    'B_L_DP_X Axis Error Stop
    ''' <summary>B_L_DP_X 軸參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1060010 = 1060010    'B_L_DP_X Axis Invalid Parameter
    ''' <summary>B_L_DP_X 軸加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1060011 = 1060011    'B_L_DP_X Axis Invalid Acc
    ''' <summary>B_L_DP_X 軸減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1060012 = 1060012    'B_L_DP_X Axis Invalid Dec
    ''' <summary>B_L_DP_X 軸最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1060013 = 1060013    'B_L_DP_X Axis Invalid VelHigh
    ''' <summary>B_L_DP_X 軸初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1060014 = 1060014    'B_L_DP_X Axis Invalid VelLow
    ''' <summary>B_L_DP_X 軸觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1060015 = 1060015    'B_L_DP_X Axis Cmp Table Error.
    ''' <summary>B_L_DP_X 軸復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1060016 = 1060016    'B_L_DP_X Axis Command Home Error!
    ''' <summary>B_L_DP_X軸設定速度失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1060017 = 1060017    'B_L_DP_X Axis SetSpeed Error!
    ''' <summary>B_L_DP_X軸命令執行失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1060018 = 1060018    'B_L_DP_X Axis Command Error!
    ''' <summary>B_L_DP_X軸命令執行逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1060019 = 1060019    'B_L_DP_X Axis Command is TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1060020 = 1060020    '
    ''' <summary>B_L_DP_Y軸移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1061000 = 1061000    'B_L_DP_Y Axis Move Error!
    ''' <summary>B_L_DP_Y軸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1061001 = 1061001    'B_L_DP_Y Axis wait Home Timeout!
    ''' <summary>B_L_DP_Y軸馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1061002 = 1061002    'B_L_DP_Y Axis is Alarm!
    ''' <summary>B_L_DP_Y軸馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1061003 = 1061003    'B_L_DP_Y Axis Get Motor Status Failed!
    ''' <summary>B_L_DP_Y軸等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1061004 = 1061004    'B_L_DP_Y Axis wait INP Timeout!
    ''' <summary>B_L_DP_Y軸移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1061005 = 1061005    'B_L_DP_Y Axis Command is Out of SPEL
    ''' <summary>B_L_DP_Y軸移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1061006 = 1061006    'B_L_DP_Y Axis Command is Out of SNEL
    ''' <summary>B_L_DP_Y軸接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1061007 = 1061007    'B_L_DP_Y Axis Touch HPEL
    ''' <summary>B_L_DP_Y軸接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1061008 = 1061008    'B_L_DP_Y Axis Touch HNEL
    ''' <summary>B_L_DP_Y軸錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1061009 = 1061009    'B_L_DP_Y Axis Error Stop
    ''' <summary>B_L_DP_Y軸參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1061010 = 1061010    'B_L_DP_Y Axis Invalid Parameter
    ''' <summary>B_L_DP_Y軸加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1061011 = 1061011    'B_L_DP_Y Axis Invalid Acc
    ''' <summary>B_L_DP_Y軸減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1061012 = 1061012    'B_L_DP_Y Axis Invalid Dec
    ''' <summary>B_L_DP_Y軸最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1061013 = 1061013    'B_L_DP_Y Axis Invalid VelHigh
    ''' <summary>B_L_DP_Y軸初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1061014 = 1061014    'B_L_DP_Y Axis Invalid VelLow
    ''' <summary>B_L_DP_Y軸觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1061015 = 1061015    'B_L_DP_Y Axis Cmp Table Error.
    ''' <summary>B_L_DP_Y軸復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1061016 = 1061016    'B_L_DP_Y Axis Command Home Error!
    ''' <summary>B_L_DP_Y軸設定速度失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1061017 = 1061017    'B_L_DP_Y Axis SetSpeed Error!
    ''' <summary>B_L_DP_Y軸命令執行失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1061018 = 1061018    'B_L_DP_Y Axis Command Error!
    ''' <summary>B_L_DP_Y軸命令執行逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1061019 = 1061019    'B_L_DP_Y Axis Command is TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1061020 = 1061020    '
    ''' <summary>B_L_DP_Z軸移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1062000 = 1062000    'B_L_DP_Z Axis Move Error!
    ''' <summary>B_L_DP_Z 軸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1062001 = 1062001    'B_L_DP_Z Axis wait Home Timeout!
    ''' <summary>B_L_DP_Z 軸馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1062002 = 1062002    'B_L_DP_Z Axis is Alarm!
    ''' <summary>B_L_DP_Z 軸馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1062003 = 1062003    'B_L_DP_Z Axis Get Motor Status Failed!
    ''' <summary>B_L_DP_Z 軸等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1062004 = 1062004    'B_L_DP_Z Axis wait INP Timeout!
    ''' <summary>B_L_DP_Z 軸移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1062005 = 1062005    'B_L_DP_Z Axis Command is Out of SPEL
    ''' <summary>B_L_DP_Z 軸移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1062006 = 1062006    'B_L_DP_Z Axis Command is Out of SNEL
    ''' <summary>B_L_DP_Z 軸接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1062007 = 1062007    'B_L_DP_Z Axis Touch HPEL
    ''' <summary>B_L_DP_Z 軸接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1062008 = 1062008    'B_L_DP_Z Axis Touch HNEL
    ''' <summary>B_L_DP_Z 軸錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1062009 = 1062009    'B_L_DP_Z Axis Error Stop
    ''' <summary>B_L_DP_Z 軸參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1062010 = 1062010    'B_L_DP_Z Axis Invalid Parameter
    ''' <summary>B_L_DP_Z 軸加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1062011 = 1062011    'B_L_DP_Z Axis Invalid Acc
    ''' <summary>B_L_DP_Z 軸減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1062012 = 1062012    'B_L_DP_Z Axis Invalid Dec
    ''' <summary>B_L_DP_Z 軸最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1062013 = 1062013    'B_L_DP_Z Axis Invalid VelHigh
    ''' <summary>B_L_DP_Z 軸初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1062014 = 1062014    'B_L_DP_Z Axis Invalid VelLow
    ''' <summary>B_L_DP_Z 軸觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1062015 = 1062015    'B_L_DP_Z Axis Cmp Table Error.
    ''' <summary>B_L_DP_Z 軸復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1062016 = 1062016    'B_L_DP_Z Axis Command Home Error!
    ''' <summary>B_L_DP_Z軸設定速度失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1062017 = 1062017    'B_L_DP_Z Axis SetSpeed Error!
    ''' <summary>B_L_DP_Z軸命令執行失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1062018 = 1062018    'B_L_DP_Z Axis Command Error!
    ''' <summary>B_L_DP_Z軸命令執行逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1062019 = 1062019    'B_L_DP_Z Axis Command is TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1062020 = 1062020    '
    ''' <summary>B_L_DP_Tilt移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1064000 = 1064000    'B_L_DP_Tilt Move Error!
    ''' <summary>B_L_DP_Tilt復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1064001 = 1064001    'B_L_DP_Tilt wait Home Timeout!
    ''' <summary>B_L_DP_Tilt馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1064002 = 1064002    'B_L_DP_Tilt is Alarm!
    ''' <summary>B_L_DP_Tilt馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1064003 = 1064003    'B_L_DP_Tilt Get Motor Status Failed!
    ''' <summary>B_L_DP_Tilt等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1064004 = 1064004    'B_L_DP_Tilt wait INP Timeout!
    ''' <summary>B_L_DP_Tilt移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1064005 = 1064005    'B_L_DP_Tilt Command is Out of SPEL
    ''' <summary>B_L_DP_Tilt移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1064006 = 1064006    'B_L_DP_Tilt Command is Out of SNEL
    ''' <summary>B_L_DP_Tilt接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1064007 = 1064007    'B_L_DP_Tilt Touch HPEL
    ''' <summary>B_L_DP_Tilt接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1064008 = 1064008    'B_L_DP_Tilt Touch HNEL
    ''' <summary>B_L_DP_Tilt錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1064009 = 1064009    'B_L_DP_Tilt Error Stop
    ''' <summary>B_L_DP_Tilt參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1064010 = 1064010    'B_L_DP_Tilt Invalid Parameter
    ''' <summary>B_L_DP_Tilt加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1064011 = 1064011    'B_L_DP_Tilt Invalid Acc
    ''' <summary>B_L_DP_Tilt減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1064012 = 1064012    'B_L_DP_Tilt Invalid Dec
    ''' <summary>B_L_DP_Tilt最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1064013 = 1064013    'B_L_DP_Tilt Invalid VelHigh
    ''' <summary>B_L_DP_Tilt初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1064014 = 1064014    'B_L_DP_Tilt Invalid VelLow
    ''' <summary>B_L_DP_Tilt觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1064015 = 1064015    'B_L_DP_Tilt Cmp Table Error.
    ''' <summary>B_L_DP_Tilt復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1064016 = 1064016    'B_L_DP_Tilt Command Home Error!
    ''' <summary>B_L_DP_Tilt軸設定速度失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1064017 = 1064017    'B_L_DP_Tilt Axis SetSpeed Error!
    ''' <summary>B_L_DP_Tilt命令執行失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1064018 = 1064018    'B_L_DP_Tilt Command Error!
    ''' <summary>B_L_DP_Tilt命令執行逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1064019 = 1064019    'B_L_DP_Tilt Command is TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1064020 = 1064020    '
    ''' <summary>運動群組3移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1066000 = 1066000    'Group3 Move Error!
    ''' <summary>運動群組3復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1066001 = 1066001    'Group3 wait Home Timeout!
    ''' <summary>運動群組3馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1066002 = 1066002    'Group3 is Alarm!
    ''' <summary>運動群組3馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1066003 = 1066003    'Group3 Get Motor Status Failed!
    ''' <summary>運動群組3等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1066004 = 1066004    'Group3 wait INP Timeout!
    ''' <summary>運動群組3移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1066005 = 1066005    'Group3 Command is Out of SPEL
    ''' <summary>運動群組3移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1066006 = 1066006    'Group3 Command is Out of SNEL
    ''' <summary>運動群組3接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1066007 = 1066007    'Group3 Touch HPEL
    ''' <summary>運動群組3接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1066008 = 1066008    'Group3 Touch HNEL
    ''' <summary>運動群組3錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1066009 = 1066009    'Group3 Error Stop
    ''' <summary>運動群組3參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1066010 = 1066010    'Group3 Invalid Parameter
    ''' <summary>運動群組3加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1066011 = 1066011    'Group3 Invalid Acc
    ''' <summary>運動群組3減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1066012 = 1066012    'Group3 Invalid Dec
    ''' <summary>運動群組3最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1066013 = 1066013    'Group3 Invalid VelHigh
    ''' <summary>運動群組3初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1066014 = 1066014    'Group3 Invalid VelLow
    ''' <summary>運動群組3觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1066015 = 1066015    'Group3 Cmp Table Error.
    ''' <summary>運動群組3復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1066016 = 1066016    'Group3 Command Home Error!
    ''' <summary>運動群組3命令執行失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1066017 = 1066017    'Group3 Command Error!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1066018 = 1066018    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1066019 = 1066019    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1066020 = 1066020    '
    ''' <summary>B_R_DP_X軸移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1067000 = 1067000    'B_R_DP_X Axis Move Error!
    ''' <summary>B_R_DP_X 軸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1067001 = 1067001    'B_R_DP_X Axis wait Home Timeout!
    ''' <summary>B_R_DP_X 軸馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1067002 = 1067002    'B_R_DP_X Axis is Alarm!
    ''' <summary>B_R_DP_X 軸馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1067003 = 1067003    'B_R_DP_X Axis Get Motor Status Failed!
    ''' <summary>B_R_DP_X 軸等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1067004 = 1067004    'B_R_DP_X Axis wait INP Timeout!
    ''' <summary>B_R_DP_X 軸移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1067005 = 1067005    'B_R_DP_X Axis Command is Out of SPEL
    ''' <summary>B_R_DP_X 軸移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1067006 = 1067006    'B_R_DP_X Axis Command is Out of SNEL
    ''' <summary>B_R_DP_X 軸接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1067007 = 1067007    'B_R_DP_X Axis Touch HPEL
    ''' <summary>B_R_DP_X 軸接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1067008 = 1067008    'B_R_DP_X Axis Touch HNEL
    ''' <summary>B_R_DP_X 軸錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1067009 = 1067009    'B_R_DP_X Axis Error Stop
    ''' <summary>B_R_DP_X 軸參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1067010 = 1067010    'B_R_DP_X Axis Invalid Parameter
    ''' <summary>B_R_DP_X 軸加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1067011 = 1067011    'B_R_DP_X Axis Invalid Acc
    ''' <summary>B_R_DP_X 軸減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1067012 = 1067012    'B_R_DP_X Axis Invalid Dec
    ''' <summary>B_R_DP_X 軸最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1067013 = 1067013    'B_R_DP_X Axis Invalid VelHigh
    ''' <summary>B_R_DP_X 軸初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1067014 = 1067014    'B_R_DP_X Axis Invalid VelLow
    ''' <summary>B_R_DP_X 軸觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1067015 = 1067015    'B_R_DP_X Axis Cmp Table Error.
    ''' <summary>B_R_DP_X 軸復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1067016 = 1067016    'B_R_DP_X Axis Command Home Error!
    ''' <summary>B_R_DP_X軸設定速度失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1067017 = 1067017    'B_R_DP_X Axis SetSpeed Error!
    ''' <summary>B_R_DP_X軸命令執行失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1067018 = 1067018    'B_R_DP_X Axis Command Error!
    ''' <summary>B_R_DP_X軸命令執行逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1067019 = 1067019    'B_R_DP_X Axis Command is TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1067020 = 1067020    '
    ''' <summary>B_R_DP_Y軸移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1068000 = 1068000    'B_R_DP_Y Axis Move Error!
    ''' <summary>B_R_DP_Y軸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1068001 = 1068001    'B_R_DP_Y Axis wait Home Timeout!
    ''' <summary>B_R_DP_Y軸馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1068002 = 1068002    'B_R_DP_Y Axis is Alarm!
    ''' <summary>B_R_DP_Y軸馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1068003 = 1068003    'B_R_DP_Y Axis Get Motor Status Failed!
    ''' <summary>B_R_DP_Y軸等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1068004 = 1068004    'B_R_DP_Y Axis wait INP Timeout!
    ''' <summary>B_R_DP_Y軸移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1068005 = 1068005    'B_R_DP_Y Axis Command is Out of SPEL
    ''' <summary>B_R_DP_Y軸移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1068006 = 1068006    'B_R_DP_Y Axis Command is Out of SNEL
    ''' <summary>B_R_DP_Y軸接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1068007 = 1068007    'B_R_DP_Y Axis Touch HPEL
    ''' <summary>B_R_DP_Y軸接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1068008 = 1068008    'B_R_DP_Y Axis Touch HNEL
    ''' <summary>B_R_DP_Y軸錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1068009 = 1068009    'B_R_DP_Y Axis Error Stop
    ''' <summary>B_R_DP_Y軸參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1068010 = 1068010    'B_R_DP_Y Axis Invalid Parameter
    ''' <summary>B_R_DP_Y軸加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1068011 = 1068011    'B_R_DP_Y Axis Invalid Acc
    ''' <summary>B_R_DP_Y軸減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1068012 = 1068012    'B_R_DP_Y Axis Invalid Dec
    ''' <summary>B_R_DP_Y軸最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1068013 = 1068013    'B_R_DP_Y Axis Invalid VelHigh
    ''' <summary>B_R_DP_Y軸初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1068014 = 1068014    'B_R_DP_Y Axis Invalid VelLow
    ''' <summary>B_R_DP_Y軸觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1068015 = 1068015    'B_R_DP_Y Axis Cmp Table Error.
    ''' <summary>B_R_DP_Y軸復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1068016 = 1068016    'B_R_DP_Y Axis Command Home Error!
    ''' <summary>B_R_DP_Y軸設定速度失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1068017 = 1068017    'B_R_DP_Y Axis SetSpeed Error!
    ''' <summary>B_R_DP_Y軸命令執行失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1068018 = 1068018    'B_R_DP_Y Axis Command Error!
    ''' <summary>B_R_DP_Y軸命令執行逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1068019 = 1068019    'B_R_DP_Y Axis Command is TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1068020 = 1068020    '
    ''' <summary>B_R_DP_Z 軸移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1069000 = 1069000    'B_R_DP_Z Axis Move Error!
    ''' <summary>B_R_DP_Z 軸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1069001 = 1069001    'B_R_DP_Z Axis wait Home Timeout!
    ''' <summary>B_R_DP_Z 軸馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1069002 = 1069002    'B_R_DP_Z Axis is Alarm!
    ''' <summary>B_R_DP_Z 軸馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1069003 = 1069003    'B_R_DP_Z Axis Get Motor Status Failed!
    ''' <summary>B_R_DP_Z 軸等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1069004 = 1069004    'B_R_DP_Z Axis wait INP Timeout!
    ''' <summary>B_R_DP_Z 軸移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1069005 = 1069005    'B_R_DP_Z Axis Command is Out of SPEL
    ''' <summary>B_R_DP_Z 軸移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1069006 = 1069006    'B_R_DP_Z Axis Command is Out of SNEL
    ''' <summary>B_R_DP_Z 軸接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1069007 = 1069007    'B_R_DP_Z Axis Touch HPEL
    ''' <summary>B_R_DP_Z 軸接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1069008 = 1069008    'B_R_DP_Z Axis Touch HNEL
    ''' <summary>B_R_DP_Z 軸錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1069009 = 1069009    'B_R_DP_Z Axis Error Stop
    ''' <summary>B_R_DP_Z 軸參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1069010 = 1069010    'B_R_DP_Z Axis Invalid Parameter
    ''' <summary>B_R_DP_Z 軸加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1069011 = 1069011    'B_R_DP_Z Axis Invalid Acc
    ''' <summary>B_R_DP_Z 軸減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1069012 = 1069012    'B_R_DP_Z Axis Invalid Dec
    ''' <summary>B_R_DP_Z 軸最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1069013 = 1069013    'B_R_DP_Z Axis Invalid VelHigh
    ''' <summary>B_R_DP_Z 軸初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1069014 = 1069014    'B_R_DP_Z Axis Invalid VelLow
    ''' <summary>B_R_DP_Z 軸觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1069015 = 1069015    'B_R_DP_Z Axis Cmp Table Error.
    ''' <summary>B_R_DP_Z 軸復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1069016 = 1069016    'B_R_DP_Z Axis Command Home Error!
    ''' <summary>B_R_DP_Z軸設定速度失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1069017 = 1069017    'B_R_DP_Z Axis SetSpeed Error!
    ''' <summary>B_R_DP_Z軸命令執行失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1069018 = 1069018    'B_R_DP_Z Axis Command Error!
    ''' <summary>B_R_DP_Z軸命令執行逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1069019 = 1069019    'B_R_DP_Z Axis Command is TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1069020 = 1069020    '
    ''' <summary>B_R_DP_Tilt移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1071000 = 1071000    'B_R_DP_Tilt  Move Error!
    ''' <summary>B_R_DP_Tilt復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1071001 = 1071001    'B_R_DP_Tilt wait Home Timeout!
    ''' <summary>B_R_DP_Tilt馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1071002 = 1071002    'B_R_DP_Tilt is Alarm!
    ''' <summary>B_R_DP_Tilt馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1071003 = 1071003    'B_R_DP_Tilt Get Motor Status Failed!
    ''' <summary>B_R_DP_Tilt等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1071004 = 1071004    'B_R_DP_Tilt wait INP Timeout!
    ''' <summary>B_R_DP_Tilt移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1071005 = 1071005    'B_R_DP_Tilt Command is Out of SPEL
    ''' <summary>B_R_DP_Tilt移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1071006 = 1071006    'B_R_DP_Tilt Command is Out of SNEL
    ''' <summary>B_R_DP_Tilt接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1071007 = 1071007    'B_R_DP_Tilt Touch HPEL
    ''' <summary>B_R_DP_Tilt接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1071008 = 1071008    'B_R_DP_Tilt Touch HNEL
    ''' <summary>B_R_DP_Tilt錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1071009 = 1071009    'B_R_DP_Tilt Error Stop
    ''' <summary>B_R_DP_Tilt參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1071010 = 1071010    'B_R_DP_Tilt Invalid Parameter
    ''' <summary>B_R_DP_Tilt加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1071011 = 1071011    'B_R_DP_Tilt Invalid Acc
    ''' <summary>B_R_DP_Tilt減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1071012 = 1071012    'B_R_DP_Tilt Invalid Dec
    ''' <summary>B_R_DP_Tilt最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1071013 = 1071013    'B_R_DP_Tilt Invalid VelHigh
    ''' <summary>B_R_DP_Tilt初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1071014 = 1071014    'B_R_DP_Tilt Invalid VelLow
    ''' <summary>B_R_DP_Tilt觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1071015 = 1071015    'B_R_DP_Tilt Cmp Table Error.
    ''' <summary>B_R_DP_Tilt復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1071016 = 1071016    'B_R_DP_Tilt Command Home Error!
    ''' <summary>B_R_DP_Tilt軸設定速度失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1071017 = 1071017    'B_R_DP_Tilt Axis SetSpeed Error!
    ''' <summary>B_R_DP_Tilt命令執行失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1071018 = 1071018    'B_R_DP_Tilt  Command Error!
    ''' <summary>B_R_DP_Tilt命令執行逾時!</summary>
    ''' <remarks></remarks>
    Public Const Error_1071019 = 1071019    'B_R_DP_Tilt Command is TimeOut!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1071020 = 1071020    '
    ''' <summary>運動群組4移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1073000 = 1073000    'Group4 Move Error!
    ''' <summary>運動群組4復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1073001 = 1073001    'Group4 wait Home Timeout!
    ''' <summary>運動群組4馬達Alarm</summary>
    ''' <remarks></remarks>
    Public Const Error_1073002 = 1073002    'Group4 is Alarm!
    ''' <summary>運動群組4馬達狀態取得失敗</summary>
    ''' <remarks></remarks>
    Public Const Error_1073003 = 1073003    'Group4 Get Motor Status Failed!
    ''' <summary>運動群組4等待到位逾時</summary>
    ''' <remarks></remarks>
    Public Const Error_1073004 = 1073004    'Group4 wait INP Timeout!
    ''' <summary>運動群組4移動命令超出軟體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1073005 = 1073005    'Group4 Command is Out of SPEL
    ''' <summary>運動群組4移動命令超出軟體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1073006 = 1073006    'Group4 Command is Out of SNEL
    ''' <summary>運動群組4接觸硬體正極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1073007 = 1073007    'Group4 Touch HPEL
    ''' <summary>運動群組4接觸硬體負極限</summary>
    ''' <remarks></remarks>
    Public Const Error_1073008 = 1073008    'Group4 Touch HNEL
    ''' <summary>運動群組4錯誤停止</summary>
    ''' <remarks></remarks>
    Public Const Error_1073009 = 1073009    'Group4 Error Stop
    ''' <summary>運動群組4參數無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1073010 = 1073010    'Group4 Invalid Parameter
    ''' <summary>運動群組4加速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1073011 = 1073011    'Group4 Invalid Acc
    ''' <summary>運動群組4減速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1073012 = 1073012    'Group4 Invalid Dec
    ''' <summary>運動群組4最大速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1073013 = 1073013    'Group4 Invalid VelHigh
    ''' <summary>運動群組4初速度無效</summary>
    ''' <remarks></remarks>
    Public Const Error_1073014 = 1073014    'Group4 Invalid VelLow
    ''' <summary>運動群組4觸發比較表錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Error_1073015 = 1073015    'Group4 Cmp Table Error.
    ''' <summary>運動群組4復歸命令異常!</summary>
    ''' <remarks></remarks>
    Public Const Error_1073016 = 1073016    'Group4 Command Home Error!
    ''' <summary>運動群組4命令執行失敗!</summary>
    ''' <remarks></remarks>
    Public Const Error_1073017 = 1073017    'Group4 Command Error!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1073018 = 1073018    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1073019 = 1073019    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Error_1073020 = 1073020    '
    ''' <summary>"廠務氣壓不足,無法復歸!"</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000000 = 2000000    'CDA Alarm. Can't Home.
    ''' <summary>"緊急停止中, 無法復歸!"</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000001 = 2000001    '"EMO Alarm, Can't Home."
    ''' <summary>"PLC異常,無法復歸!"</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000002 = 2000002    '"PLC Alarm, Can't Home."
    ''' <summary>"互鎖保護,無法復歸!"</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000003 = 2000003    '"Interlock Alarm, Can't Home."
    ''' <summary>CCD 場景毀損!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000004 = 2000004    '"CCD Sence destroyed"
    ''' <summary>"伺服系統未激磁,無法復歸!"</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000005 = 2000005    '"Servo Off, Can't Home."
    ''' <summary>取像工具不存在! 請先設定!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000006 = 2000006    'Acquisition Tool Does Not Exist.
    ''' <summary>定位工具不存在! 請先設定!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000007 = 2000007    'Alignment Tool Does Not Exist.
    ''' <summary>檢測工具不存在! 請先設定!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000008 = 2000008    'Inspection Tool Does Not Exist.
    ''' <summary>請先進行影像教導!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000009 = 2000009    '"Train Image, First."
    ''' <summary>場景切換失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000010 = 2000010    'Select Scene Failed!
    ''' <summary>第一輪第一基準點定位失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000011 = 2000011    'Round1 Alignment Point1 Failed!
    ''' <summary>第一輪第二基準點定位失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000012 = 2000012    'Round1 Alignment Point2 Failed!
    ''' <summary>第一輪第三基準點定位失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000013 = 2000013    'Round1 Alignment Point3 Failed!
    ''' <summary>第一輪第四基準點定位失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000014 = 2000014    'Round1 Alignment Point4 Failed!
    ''' <summary>第二輪第一基準點定位失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000015 = 2000015    'Round2 Alignment Point1 Failed!
    ''' <summary>第二輪第二基準點定位失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000016 = 2000016    'Round2 Alignment Point2 Failed!
    ''' <summary>第二輪第三基準點定位失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000017 = 2000017    'Round2 Alignment Point3 Failed!
    ''' <summary>場景 {0} 影像教導失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000018 = 2000018    'Scene {0} Image Train Failed!
    ''' <summary>場景 {0} 特徵識別失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000019 = 2000019    'Scene {0} Pattern Recognition Failed!
    ''' <summary>顯示物件不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000020 = 2000020    'CogDisplay is Dispose!
    ''' <summary>秤重資料庫檔案不存在</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000021 = 2000021    'FlowRateDB Name does not exist!
    ''' <summary>定位資料不存在</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000022 = 2000022    'Alignment Data Not Exists!
    ''' <summary>工具不存在</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000023 = 2000023    'Tool Subject Not Exists.
    ''' <summary>工具輸入不存在</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000024 = 2000024    'Tool Subject Input Not Exists.
    ''' <summary>工具輸入影像不存在</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000025 = 2000025    'Tool Subject Input 'InputImage' Not Exists.
    ''' <summary>CCD讀取影像格式錯誤</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000026 = 2000026    'CCD Set VideoFormatType Error!
    ''' <summary>CCD序號不匹配</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000027 = 2000027    'CCD SerialNumber Not Matched!
    ''' <summary>軸向異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000028 = 2000028    'Axis Alarm.
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000029 = 2000029    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2000030 = 2000030    '
    ''' <summary>汽缸上逾時!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2004000 = 2004000    'Cylinder Up Time Out!
    ''' <summary>汽缸下逾時!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2004001 = 2004001    'Cylinder Down Time Out!
    ''' <summary>夾爪閉逾時!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2004002 = 2004002    'Clamp On Time Out!
    ''' <summary>夾爪開逾時!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2004003 = 2004003    'Clamp Off Time Out!
    ''' <summary>CCD1取像工具不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012000 = 2012000    'CCD1 Acquisition Tool Not Exists.
    ''' <summary>CCD1取像輸出數量為0!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012001 = 2012001    'CCD1 Acquisition Output List Count is 0.
    ''' <summary>CCD1取像輸出不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012002 = 2012002    'CCD1 Acquisition Output Not Exists.
    ''' <summary>CCD1 取像TimeOut!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012003 = 2012003    'CCD1 Acquisition TimeOut!
    ''' <summary>CCD1 影像運算 TimeOut!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012004 = 2012004    'CCD1 Calculation TimeOut!
    ''' <summary>CCD1 通訊異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012005 = 2012005    'CCD1 Communication Error!
    ''' <summary>CCD1定位工具異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012100 = 2012100    'CCD1 Alignment Tool Failed!
    ''' <summary>CCD1定位工具未教導!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012101 = 2012101    'CCD1 Alignment Tool Untrained!
    ''' <summary>CCD1定位有多個符合特徵!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012102 = 2012102    'CCD1 Alignment Multi-Match Pattern!
    ''' <summary>CCD1符合特徵未找到!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012103 = 2012103    'CCD1 Match Pattern Not Found
    ''' <summary>CCD1輸入影像不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012104 = 2012104    'CCD1 Input Image Not Exists
    ''' <summary>CCD1定位結果顯示失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012105 = 2012105    'CCD1 Show Alignment Result Failed!
    ''' <summary>CCD1定位結果不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012106 = 2012106    'CCD1 Alignment Result Not Exists.
    ''' <summary>CCD1定位場景不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012107 = 2012107    'CCD1 Alignment Scene Not Exists.
    ''' <summary>CCD1定位工具不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012108 = 2012108    'CCD1 Alignment Tool Not Exists.
    ''' <summary>CCD1定位工具輸入物件不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012109 = 2012109    'CCD1 Alignment Input Not Exists.
    ''' <summary>CCD1定位工具輸入數量為0!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012110 = 2012110    'CCD1 Alignment Input List Count is 0.
    ''' <summary>CCD1定位結果誤差過大!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012111 = 2012111    'CCD1 Alignment Out of Range!
    ''' <summary>CCD1定位超過角度限制!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012112 = 2012112    'CCD1 Alignment Angle Out of Range!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012113 = 2012113    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012114 = 2012114    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012115 = 2012115    '
    ''' <summary>CCD1檢測工具異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012200 = 2012200    'CCD1 Inspection Tool Failed!
    ''' <summary>CCD1檢測工具未教導!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012201 = 2012201    'CCD1 Inspection Tool Untrained!
    ''' <summary>CCD1檢測工具警報!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012202 = 2012202    'CCD1 Inspection Alarm!
    ''' <summary>CCD1輸入影像不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012203 = 2012203    'CCD1 Input Image Not Exists
    ''' <summary>CCD1檢測結果顯示失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012204 = 2012204    'CCD1 Show Inspection Result Failed!
    ''' <summary>CCD1檢測結果不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012205 = 2012205    'CCD1 Inspection Result Not Exists.
    ''' <summary>CCD1檢測場景不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012206 = 2012206    'CCD1 Inspection Scene Not Exists.
    ''' <summary>CCD1檢測工具不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012207 = 2012207    'CCD1 Inspection Tool Not Exists.
    ''' <summary>CCD1檢測工具輸入物件不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012208 = 2012208    'CCD1 Inspection Input Not Exists.
    ''' <summary>CCD1檢測工具輸入數量為0!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012209 = 2012209    'CCD1 Inspection Input List Count is 0.
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012210 = 2012210    '
    ''' <summary>CCD2取像工具不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012300 = 2012300    'CCD2 Acquisition Tool Not Exists.
    ''' <summary>CCD2取像輸出數量為0!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012301 = 2012301    'CCD2 Acquisition Output List Count is 0.
    ''' <summary>CCD2取像輸出不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012302 = 2012302    'CCD2 Acquisition Output Not Exists.
    ''' <summary>CCD2 取像TimeOut!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012303 = 2012303    'CCD2 Acquisition TimeOut!
    ''' <summary>CCD2 影像運算 TimeOut!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012304 = 2012304    'CCD2 Calculation TimeOut!
    ''' <summary>CCD2 通訊異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012305 = 2012305    'CCD2 Communication Error!
    ''' <summary>CCD2定位工具異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012400 = 2012400    'CCD2 Alignment Tool Failed!
    ''' <summary>CCD2定位工具未教導!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012401 = 2012401    'CCD2 Alignment Tool Untrained!
    ''' <summary>CCD2定位有多個符合特徵!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012402 = 2012402    'CCD2 Alignment Multi-Match Pattern!
    ''' <summary>CCD2符合特徵未找到!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012403 = 2012403    'CCD2 Match Pattern Not Found
    ''' <summary>CCD2輸入影像不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012404 = 2012404    'CCD2 Input Image Not Exists
    ''' <summary>CCD2定位結果顯示失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012405 = 2012405    'CCD2 Show Alignment Result Failed!
    ''' <summary>CCD2定位結果不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012406 = 2012406    'CCD2 Alignment Result Not Exists.
    ''' <summary>CCD2定位場景不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012407 = 2012407    'CCD2 Alignment Scene Not Exists.
    ''' <summary>CCD2定位工具不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012408 = 2012408    'CCD2 Alignment Tool Not Exists.
    ''' <summary>CCD2定位工具輸入物件不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012409 = 2012409    'CCD2 Alignment Input Not Exists.
    ''' <summary>CCD2定位工具輸入數量為0!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012410 = 2012410    'CCD2 Alignment Input List Count is 0.
    ''' <summary>CCD2定位結果誤差過大!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012411 = 2012411    'CCD2 Alignment Out of Range!
    ''' <summary>CCD2定位超過角度限制!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012412 = 2012412    'CCD2 Alignment Angle Out of Range!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012413 = 2012413    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012414 = 2012414    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012415 = 2012415    '
    ''' <summary>CCD2檢測工具異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012500 = 2012500    'CCD2 Inspection Tool Failed!
    ''' <summary>CCD2檢測工具未教導!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012501 = 2012501    'CCD2 Inspection Tool Untrained!
    ''' <summary>CCD2檢測工具警報!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012502 = 2012502    'CCD2 Inspection Alarm!
    ''' <summary>CCD2輸入影像不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012503 = 2012503    'CCD2 Input Image Not Exists
    ''' <summary>CCD2檢測結果顯示失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012504 = 2012504    'CCD2 Show Inspection Result Failed!
    ''' <summary>CCD2檢測結果不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012505 = 2012505    'CCD2 Inspection Result Not Exists.
    ''' <summary>CCD2檢測場景不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012506 = 2012506    'CCD2 Inspection Scene Not Exists.
    ''' <summary>CCD2檢測工具不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012507 = 2012507    'CCD2 Inspection Tool Not Exists.
    ''' <summary>CCD2檢測工具輸入物件不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012508 = 2012508    'CCD2 Inspection Input Not Exists.
    ''' <summary>CCD2檢測工具輸入數量為0!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012509 = 2012509    'CCD2 Inspection Input List Count is 0.
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012510 = 2012510    '
    ''' <summary>CCD3取像工具不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012600 = 2012600    'CCD3 Acquisition Tool Not Exists.
    ''' <summary>CCD3取像輸出數量為0!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012601 = 2012601    'CCD3 Acquisition Output List Count is 0.
    ''' <summary>CCD3取像輸出不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012602 = 2012602    'CCD3 Acquisition Output Not Exists.
    ''' <summary>CCD3 取像TimeOut!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012603 = 2012603    'CCD3 Acquisition TimeOut!
    ''' <summary>CCD3 影像運算 TimeOut!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012604 = 2012604    'CCD3 Calculation TimeOut!
    ''' <summary>CCD3 通訊異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012605 = 2012605    'CCD3 Communication Error!
    ''' <summary>CCD3定位工具異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012700 = 2012700    'CCD3 Alignment Tool Failed!
    ''' <summary>CCD3定位工具未教導!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012701 = 2012701    'CCD3 Alignment Tool Untrained!
    ''' <summary>CCD3定位有多個符合特徵!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012702 = 2012702    'CCD3 Alignment Multi-Match Pattern!
    ''' <summary>CCD3符合特徵未找到!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012703 = 2012703    'CCD3 Match Pattern Not Found
    ''' <summary>CCD3輸入影像不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012704 = 2012704    'CCD3 Input Image Not Exists
    ''' <summary>CCD3定位結果顯示失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012705 = 2012705    'CCD3 Show Alignment Result Failed!
    ''' <summary>CCD3定位結果不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012706 = 2012706    'CCD3 Alignment Result Not Exists.
    ''' <summary>CCD3定位場景不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012707 = 2012707    'CCD3 Alignment Scene Not Exists.
    ''' <summary>CCD3定位工具不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012708 = 2012708    'CCD3 Alignment Tool Not Exists.
    ''' <summary>CCD3定位工具輸入物件不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012709 = 2012709    'CCD3 Alignment Input Not Exists.
    ''' <summary>CCD3定位工具輸入數量為0!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012710 = 2012710    'CCD3 Alignment Input List Count is 0.
    ''' <summary>CCD3定位結果誤差過大!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012711 = 2012711    'CCD3 Alignment Out of Range!
    ''' <summary>CCD3定位超過角度限制!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012712 = 2012712    'CCD3 Alignment Angle Out of Range!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012713 = 2012713    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012714 = 2012714    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012715 = 2012715    '
    ''' <summary>CCD3檢測工具異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012800 = 2012800    'CCD3 Inspection Tool Failed!
    ''' <summary>CCD3檢測工具未教導!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012801 = 2012801    'CCD3 Inspection Tool Untrained!
    ''' <summary>CCD3檢測工具警報!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012802 = 2012802    'CCD3 Inspection Alarm!
    ''' <summary>CCD3輸入影像不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012803 = 2012803    'CCD3 Input Image Not Exists
    ''' <summary>CCD3檢測結果顯示失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012804 = 2012804    'CCD3 Show Inspection Result Failed!
    ''' <summary>CCD3檢測結果不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012805 = 2012805    'CCD3 Inspection Result Not Exists.
    ''' <summary>CCD3檢測場景不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012806 = 2012806    'CCD3 Inspection Scene Not Exists.
    ''' <summary>CCD3檢測工具不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012807 = 2012807    'CCD3 Inspection Tool Not Exists.
    ''' <summary>CCD3檢測工具輸入物件不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012808 = 2012808    'CCD3 Inspection Input Not Exists.
    ''' <summary>CCD3檢測工具輸入數量為0!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012809 = 2012809    'CCD3 Inspection Input List Count is 0.
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012810 = 2012810    '
    ''' <summary>CCD4取像工具不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012900 = 2012900    'CCD4 Acquisition Tool Not Exists.
    ''' <summary>CCD4取像輸出數量為0!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012901 = 2012901    'CCD4 Acquisition Output List Count is 0.
    ''' <summary>CCD4取像輸出不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012902 = 2012902    'CCD4 Acquisition Output Not Exists.
    ''' <summary>CCD4 取像TimeOut!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012903 = 2012903    'CCD4 Acquisition TimeOut!
    ''' <summary>CCD4 影像運算 TimeOut!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012904 = 2012904    'CCD4 Calculation TimeOut!
    ''' <summary>CCD4 通訊異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2012905 = 2012905    'CCD4 Communication Error!
    ''' <summary>CCD4定位工具異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013000 = 2013000    'CCD4 Alignment Tool Failed!
    ''' <summary>CCD4定位工具未教導!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013001 = 2013001    'CCD4 Alignment Tool Untrained!
    ''' <summary>CCD4定位有多個符合特徵!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013002 = 2013002    'CCD4 Alignment Multi-Match Pattern!
    ''' <summary>CCD4符合特徵未找到!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013003 = 2013003    'CCD4 Match Pattern Not Found
    ''' <summary>CCD4輸入影像不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013004 = 2013004    'CCD4 Input Image Not Exists
    ''' <summary>CCD4定位結果顯示失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013005 = 2013005    'CCD4 Show Alignment Result Failed!
    ''' <summary>CCD4定位結果不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013006 = 2013006    'CCD4 Alignment Result Not Exists.
    ''' <summary>CCD4定位場景不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013007 = 2013007    'CCD4 Alignment Scene Not Exists.
    ''' <summary>CCD4定位工具不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013008 = 2013008    'CCD4 Alignment Tool Not Exists.
    ''' <summary>CCD4定位工具輸入物件不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013009 = 2013009    'CCD4 Alignment Input Not Exists.
    ''' <summary>CCD4定位工具輸入數量為0!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013010 = 2013010    'CCD4 Alignment Input List Count is 0.
    ''' <summary>CCD4定位結果誤差過大!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013011 = 2013011    'CCD4 Alignment Out of Range!
    ''' <summary>CCD4定位超過角度限制!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013012 = 2013012    'CCD4 Alignment Angle Out of Range!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013013 = 2013013    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013014 = 2013014    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013015 = 2013015    '
    ''' <summary>CCD4檢測工具異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013100 = 2013100    'CCD4 Inspection Tool Failed!
    ''' <summary>CCD4檢測工具未教導!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013101 = 2013101    'CCD4 Inspection Tool Untrained!
    ''' <summary>CCD4檢測工具警報!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013102 = 2013102    'CCD4 Inspection Alarm!
    ''' <summary>CCD4輸入影像不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013103 = 2013103    'CCD4 Input Image Not Exists
    ''' <summary>CCD4檢測結果顯示失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013104 = 2013104    'CCD4 Show Inspection Result Failed!
    ''' <summary>CCD4檢測結果不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013105 = 2013105    'CCD4 Inspection Result Not Exists.
    ''' <summary>CCD4檢測場景不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013106 = 2013106    'CCD4 Inspection Scene Not Exists.
    ''' <summary>CCD4檢測工具不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013107 = 2013107    'CCD4 Inspection Tool Not Exists.
    ''' <summary>CCD4檢測工具輸入物件不存在!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013108 = 2013108    'CCD4 Inspection Input Not Exists.
    ''' <summary>CCD4檢測工具輸入數量為0!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013109 = 2013109    'CCD4 Inspection Input List Count is 0.
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2013110 = 2013110    '
    ''' <summary>測高儀1輸入值異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014000 = 2014000    'Altimeter 1 Value is Out of Range!
    ''' <summary>測高儀1重試失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014001 = 2014001    'Altimeter 1 Retry Failed!
    ''' <summary>測高儀1自動校正失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014002 = 2014002    'Altimeter 1 Auto Calibration Failed!
    ''' <summary>測高儀1自動測高失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014003 = 2014003    'Altimeter 1 Auto Find Height Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014004 = 2014004    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014005 = 2014005    '
    ''' <summary>測高儀2輸入值異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014100 = 2014100    'Altimeter 2 Value is Out of Range!
    ''' <summary>測高儀2重試失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014101 = 2014101    'Altimeter 2 Retry Failed!
    ''' <summary>測高儀2自動校正失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014102 = 2014102    'Altimeter 2 Auto Calibration Failed!
    ''' <summary>測高儀2自動測高失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014103 = 2014103    'Altimeter 2 Auto Find Height Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014104 = 2014104    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014105 = 2014105    '
    ''' <summary>測高儀3輸入值異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014200 = 2014200    'Altimeter 3 Value is Out of Range!
    ''' <summary>測高儀3重試失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014201 = 2014201    'Altimeter 3 Retry Failed!
    ''' <summary>測高儀3自動校正失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014202 = 2014202    'Altimeter3 Auto Calibration Failed!
    ''' <summary>測高儀3自動測高失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014203 = 2014203    'Altimeter 3 Auto Find Height Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014204 = 2014204    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014205 = 2014205    '
    ''' <summary>測高儀4輸入值異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014300 = 2014300    'Altimeter 4 Value is Out of Range!
    ''' <summary>測高儀4重試失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014301 = 2014301    'Altimeter 4 Retry Failed!
    ''' <summary>測高儀4自動校正失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014302 = 2014302    'Altimeter 4 Auto Calibration Failed!
    ''' <summary>測高儀4自動測高失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014303 = 2014303    'Altimeter 4 Auto Find Height Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014304 = 2014304    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2014305 = 2014305    '
    ''' <summary>微量天平1初始化失敗! 通訊埠被佔用!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2015000 = 2015000    'Initialize Scale1 Failed! Port is Occupied!
    ''' <summary>微量天平1命令發送失敗!通訊埠未開啟!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2015001 = 2015001    'Scale1 Send Command Error! Port is Not Opened!
    ''' <summary>"微量天平1 重量補正失敗!"</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2015002 = 2015002    'Scale1 Weight Correction failed!
    ''' <summary>"微量天平1無回應!"</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2015003 = 2015003    'Scale1 Not Response!
    ''' <summary>重量1超出設定範圍</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2015004 = 2015004    'Weight1 Out of Range
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2015005 = 2015005    '
    ''' <summary>微量天平2初始化失敗! 通訊埠被佔用!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2015006 = 2015006    'Initialize Scale2 Failed! Port is Occupied!
    ''' <summary>微量天平2命令發送失敗!通訊埠未開啟!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2015007 = 2015007    'Scale2 Send Command Error! Port is Not Opened!
    ''' <summary>"微量天平2 重量補正失敗!"</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2015008 = 2015008    'Scale2 Weight Correction failed!
    ''' <summary>"微量天平2無回應!"</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2015009 = 2015009    'Scale2 Not Response!
    ''' <summary>重量2超出設定範圍</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2015010 = 2015010   'Weight2 Out of Range
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2015011 = 2015011    '
    ''' <summary>觸發卡1等待觸發逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016000 = 2016000    'Trigger Board1 TimeOut!
    ''' <summary>觸發卡1重置錯誤</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016001 = 2016001    'Trigger Board1 Reset Error!
    ''' <summary>Trigger Board1 Too Much or Incorrect Recipe Data, J Mode</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016002 = 2016002    'Trigger Board1 Too Much or Incorrect Recipe Data, J Mode
    ''' <summary>Trigger Board1 Too Much or Incorrect Recipe Data, T or P Mode</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016003 = 2016003    'Trigger Board1 Too Much or Incorrect Recipe Data, T or P Mode
    ''' <summary>Trigger Board1 Data Incorrect, X or D Command</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016004 = 2016004    'Trigger Board1 Data Incorrect, X or D Command
    ''' <summary>Trigger Board1 Data Incorrect, ParaTmp[]</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016005 = 2016005    'Trigger Board1 Data Incorrect, ParaTmp[]
    ''' <summary>Trigger Board1 Not Enough Memory For Target Points</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016006 = 2016006    'Trigger Board1 Not Enough Memory For Target Points
    ''' <summary>Trigger Board1 Data Incorrect, Cycle Time or Dots Number</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016007 = 2016007    'Trigger Board1 Data Incorrect, Cycle Time or Dots Number
    ''' <summary>Trigger Board1 Data Incorrect, Pitch or Dots Number</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016008 = 2016008    'Trigger Board1 Data Incorrect, Pitch or Dots Number
    ''' <summary>Trigger Board1 Fail To Generate Fire Table</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016009 = 2016009    'Trigger Board1 Fail To Generate Fire Table
    ''' <summary>Trigger Board1 Not Enough Memory For History Data</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016010 = 2016010    'Trigger Board1 Not Enough Memory For History Data
    ''' <summary>Trigger Board1 Dummy Run Do Not End Normally</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016011 = 2016011    'Trigger Board1 Dummy Run Do Not End Normally
    ''' <summary>Trigger Board1 Fail To Generate Trigger Advanced Distance</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016012 = 2016012    'Trigger Board1 Fail To Generate Trigger Advanced Distance
    ''' <summary>Trigger Board1 UART Receive Data Timeout</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016013 = 2016013    'Trigger Board1 UART Receive Data Timeout
    ''' <summary>Trigger Board1 Alarm Is Not Cleared</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016014 = 2016014    'Trigger Board1 Alarm Is Not Cleared
    ''' <summary>Trigger Board1 SPI:Too Many Fails To Talk With Slave</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016015 = 2016015    'Trigger Board1 SPI:Too Many Fails To Talk With Slave
    ''' <summary>Trigger Board1 Communication Error With Remote Display</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016016 = 2016016    'Trigger Board1 Communication Error With Remote Display
    ''' <summary>Trigger Board1 Selected PulseTime/Falling/Stroke Is Not Available</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016017 = 2016017    'Trigger Board1 Selected PulseTime/Falling/Stroke Is Not Available
    ''' <summary>Trigger Board1 Just Reboot</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016018 = 2016018    'Trigger Board1 Just Reboot
    ''' <summary>Trigger Board1 Fail To Set Parameters To Jetting Driver Board</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016019 = 2016019    'Trigger Board1 Fail To Set Parameters To Jetting Driver Board
    ''' <summary>Trigger Board1 Too Much or Incorrect Recipe Data, G or F Mode</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016020 = 2016020    'Trigger Board1 Too Much or Incorrect Recipe Data, G or F Mode
    ''' <summary>Trigger Board1 Failed to Trim The Recipe File Of F Mode</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016021 = 2016021    'Trigger Board1 Failed to Trim The Recipe File Of F Mode
    ''' <summary>Trigger Board1 Recipe: Point Type Should Not Connected With Line Type</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016022 = 2016022    'Trigger Board1 Recipe: Point Type Should Not Connected With Line Type
    ''' <summary>Trigger Board1 There Are Untriggered Dots</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016023 = 2016023    'Trigger Board1 There Are Untriggered Dots
    ''' <summary>Trigger Board1 Hit Too Late</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016024 = 2016024    'Trigger Board1 Hit Too Late
    ''' <summary>Trigger Board1 Away From Target, Will Never Be Hit</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016025 = 2016025    'Trigger Board1 Away From Target, Will Never Be Hit
    ''' <summary>Trigger Board1 Fire Table Content Has Zero</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016026 = 2016026    'Trigger Board1 Fire Table Content Has Zero
    ''' <summary>Trigger Board1 Not Enough Memory For Mapping Data</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016027 = 2016027    'Trigger Board1 Not Enough Memory For Mapping Data
    ''' <summary>Trigger Board1 Dummy Run: Error In Direction Check Process</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016028 = 2016028    'Trigger Board1 Dummy Run: Error In Direction Check Process
    ''' <summary>Trigger Board1 Fire: Error In Direction Check Process</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016029 = 2016029    'Trigger Board1 Fire: Error In Direction Check Process
    ''' <summary>Trigger Board1 HMI Communication Timeout</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016030 = 2016030    'Trigger Board1 HMI Communication Timeout
    ''' <summary>Trigger Board1 Driver Over Temperature</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016031 = 2016031    'Trigger Board1 Driver Over Temperature
    ''' <summary>Trigger Board1 Driver has no High Voltage</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016032 = 2016032    'Trigger Board1 Driver has no High Voltage
    ''' <summary>Trigger Board1 Communication fail with PICO valve</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016033 = 2016033    'Trigger Board1 Communication fail with PICO valve
    ''' <summary>Trigger Board1 Piezo Over Temperature</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016034 = 2016034    'Trigger Board1 Piezo Over Temperature
    ''' <summary>Trigger Board1 Driver Board reboot</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016035 = 2016035    'Trigger Board1 Driver Board reboot
    ''' <summary>觸發卡2等待觸發逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016100 = 2016100    'Trigger Board2 TimeOut!
    ''' <summary>觸發卡2重置錯誤</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016101 = 2016101    'Trigger Board2 Reset Error!
    ''' <summary>Trigger Board2 Too Much or Incorrect Recipe Data, J Mode</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016102 = 2016102    'Trigger Board2 Too Much or Incorrect Recipe Data, J Mode
    ''' <summary>Trigger Board2 Too Much or Incorrect Recipe Data, T or P Mode</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016103 = 2016103    'Trigger Board2 Too Much or Incorrect Recipe Data, T or P Mode
    ''' <summary>Trigger Board2 Data Incorrect, X or D Command</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016104 = 2016104    'Trigger Board2 Data Incorrect, X or D Command
    ''' <summary>Trigger Board2 Data Incorrect, ParaTmp[]</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016105 = 2016105    'Trigger Board2 Data Incorrect, ParaTmp[]
    ''' <summary>Trigger Board2 Not Enough Memory For Target Points</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016106 = 2016106    'Trigger Board2 Not Enough Memory For Target Points
    ''' <summary>Trigger Board2 Data Incorrect, Cycle Time or Dots Number</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016107 = 2016107    'Trigger Board2 Data Incorrect, Cycle Time or Dots Number
    ''' <summary>Trigger Board2 Data Incorrect, Pitch or Dots Number</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016108 = 2016108    'Trigger Board2 Data Incorrect, Pitch or Dots Number
    ''' <summary>Trigger Board2 Fail To Generate Fire Table</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016109 = 2016109    'Trigger Board2 Fail To Generate Fire Table
    ''' <summary>Trigger Board2 Not Enough Memory For History Data</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016110 = 2016110    'Trigger Board2 Not Enough Memory For History Data
    ''' <summary>Trigger Board2 Dummy Run Do Not End Normally</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016111 = 2016111    'Trigger Board2 Dummy Run Do Not End Normally
    ''' <summary>Trigger Board2 Fail To Generate Trigger Advanced Distance</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016112 = 2016112    'Trigger Board2 Fail To Generate Trigger Advanced Distance
    ''' <summary>Trigger Board2 UART Receive Data Timeout</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016113 = 2016113    'Trigger Board2 UART Receive Data Timeout
    ''' <summary>Trigger Board2 Alarm Is Not Cleared</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016114 = 2016114    'Trigger Board2 Alarm Is Not Cleared
    ''' <summary>Trigger Board2 SPI:Too Many Fails To Talk With Slave</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016115 = 2016115    'Trigger Board2 SPI:Too Many Fails To Talk With Slave
    ''' <summary>Trigger Board2 Communication Error With Remote Display</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016116 = 2016116    'Trigger Board2 Communication Error With Remote Display
    ''' <summary>Trigger Board2 Selected PulseTime/Falling/Stroke Is Not Available</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016117 = 2016117    'Trigger Board2 Selected PulseTime/Falling/Stroke Is Not Available
    ''' <summary>Trigger Board2 Just Reboot</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016118 = 2016118    'Trigger Board2 Just Reboot
    ''' <summary>Trigger Board2 Fail To Set Parameters To Jetting Driver Board</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016119 = 2016119    'Trigger Board2 Fail To Set Parameters To Jetting Driver Board
    ''' <summary>Trigger Board2 Too Much or Incorrect Recipe Data, G or F Mode</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016120 = 2016120    'Trigger Board2 Too Much or Incorrect Recipe Data, G or F Mode
    ''' <summary>Trigger Board2 Failed to Trim The Recipe File Of F Mode</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016121 = 2016121    'Trigger Board2 Failed to Trim The Recipe File Of F Mode
    ''' <summary>Trigger Board2 Recipe: Point Type Should Not Connected With Line Type</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016122 = 2016122    'Trigger Board2 Recipe: Point Type Should Not Connected With Line Type
    ''' <summary>Trigger Board2 There Are Untriggered Dots</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016123 = 2016123    'Trigger Board2 There Are Untriggered Dots
    ''' <summary>Trigger Board2 Hit Too Late</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016124 = 2016124    'Trigger Board2 Hit Too Late
    ''' <summary>Trigger Board2 Away From Target, Will Never Be Hit</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016125 = 2016125    'Trigger Board2 Away From Target, Will Never Be Hit
    ''' <summary>Trigger Board2 Fire Table Content Has Zero</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016126 = 2016126    'Trigger Board2 Fire Table Content Has Zero
    ''' <summary>Trigger Board2 Not Enough Memory For Mapping Data</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016127 = 2016127    'Trigger Board2 Not Enough Memory For Mapping Data
    ''' <summary>Trigger Board2 Dummy Run: Error In Direction Check Process</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016128 = 2016128    'Trigger Board2 Dummy Run: Error In Direction Check Process
    ''' <summary>Trigger Board2 Fire: Error In Direction Check Process</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016129 = 2016129    'Trigger Board2 Fire: Error In Direction Check Process
    ''' <summary>Trigger Board2 HMI Communication Timeout</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016130 = 2016130    'Trigger Board2 HMI Communication Timeout
    ''' <summary>Trigger Board2 Driver Over Temperature</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016131 = 2016131    'Trigger Board2 Driver Over Temperature
    ''' <summary>Trigger Board2 Driver has no High Voltage</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016132 = 2016132    'Trigger Board2 Driver has no High Voltage
    ''' <summary>Trigger Board2 Communication fail with PICO valve</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016133 = 2016133    'Trigger Board2 Communication fail with PICO valve
    ''' <summary>Trigger Board2 Piezo Over Temperature</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016134 = 2016134    'Trigger Board1 Piezo Over Temperature
    ''' <summary>Trigger Board2 Driver Board reboot</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016135 = 2016135    'Trigger Board2 Driver Board reboot
    ''' <summary>"觸發卡3等待觸發逾時"</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016200 = 2016200    'Trigger Board3 TimeOut!
    ''' <summary>觸發卡3重置錯誤</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016201 = 2016201    'Trigger Board3 Reset Error!
    ''' <summary>Trigger Board3 Too Much or Incorrect Recipe Data, J Mode</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016202 = 2016202    'Trigger Board3 Too Much or Incorrect Recipe Data, J Mode
    ''' <summary>Trigger Board3 Too Much or Incorrect Recipe Data, T or P Mode</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016203 = 2016203    'Trigger Board3 Too Much or Incorrect Recipe Data, T or P Mode
    ''' <summary>Trigger Board3 Data Incorrect, X or D Command</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016204 = 2016204    'Trigger Board3 Data Incorrect, X or D Command
    ''' <summary>Trigger Board3 Data Incorrect, ParaTmp[]</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016205 = 2016205    'Trigger Board3 Data Incorrect, ParaTmp[]
    ''' <summary>Trigger Board3 Not Enough Memory For Target Points</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016206 = 2016206    'Trigger Board3 Not Enough Memory For Target Points
    ''' <summary>Trigger Board3 Data Incorrect, Cycle Time or Dots Number</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016207 = 2016207    'Trigger Board3 Data Incorrect, Cycle Time or Dots Number
    ''' <summary>Trigger Board3 Data Incorrect, Pitch or Dots Number</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016208 = 2016208    'Trigger Board3 Data Incorrect, Pitch or Dots Number
    ''' <summary>Trigger Board3 Fail To Generate Fire Table</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016209 = 2016209    'Trigger Board3 Fail To Generate Fire Table
    ''' <summary>Trigger Board3 Not Enough Memory For History Data</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016210 = 2016210    'Trigger Board3 Not Enough Memory For History Data
    ''' <summary>Trigger Board3 Dummy Run Do Not End Normally</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016211 = 2016211    'Trigger Board3 Dummy Run Do Not End Normally
    ''' <summary>Trigger Board3 Fail To Generate Trigger Advanced Distance</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016212 = 2016212    'Trigger Board3 Fail To Generate Trigger Advanced Distance
    ''' <summary>Trigger Board3 UART Receive Data Timeout</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016213 = 2016213    'Trigger Board3 UART Receive Data Timeout
    ''' <summary>Trigger Board3 Alarm Is Not Cleared</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016214 = 2016214    'Trigger Board3 Alarm Is Not Cleared
    ''' <summary>Trigger Board3 SPI:Too Many Fails To Talk With Slave</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016215 = 2016215    'Trigger Board3 SPI:Too Many Fails To Talk With Slave
    ''' <summary>Trigger Board3 Communication Error With Remote Display</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016216 = 2016216    'Trigger Board3 Communication Error With Remote Display
    ''' <summary>Trigger Board3 Selected PulseTime/Falling/Stroke Is Not Available</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016217 = 2016217    'Trigger Board3 Selected PulseTime/Falling/Stroke Is Not Available
    ''' <summary>Trigger Board3 Just Reboot</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016218 = 2016218    'Trigger Board3 Just Reboot
    ''' <summary>Trigger Board3 Fail To Set Parameters To Jetting Driver Board</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016219 = 2016219    'Trigger Board3 Fail To Set Parameters To Jetting Driver Board
    ''' <summary>Trigger Board3 Too Much or Incorrect Recipe Data, G or F Mode</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016220 = 2016220    'Trigger Board3 Too Much or Incorrect Recipe Data, G or F Mode
    ''' <summary>Trigger Board3 Failed to Trim The Recipe File Of F Mode</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016221 = 2016221    'Trigger Board3 Failed to Trim The Recipe File Of F Mode
    ''' <summary>Trigger Board3 Recipe: Point Type Should Not Connected With Line Type</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016222 = 2016222    'Trigger Board3 Recipe: Point Type Should Not Connected With Line Type
    ''' <summary>Trigger Board3 There Are Untriggered Dots</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016223 = 2016223    'Trigger Board3 There Are Untriggered Dots
    ''' <summary>Trigger Board3 Hit Too Late</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016224 = 2016224    'Trigger Board3 Hit Too Late
    ''' <summary>Trigger Board3 Away From Target, Will Never Be Hit</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016225 = 2016225    'Trigger Board3 Away From Target, Will Never Be Hit
    ''' <summary>Trigger Board3 Fire Table Content Has Zero</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016226 = 2016226    'Trigger Board3 Fire Table Content Has Zero
    ''' <summary>Trigger Board3 Not Enough Memory For Mapping Data</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016227 = 2016227    'Trigger Board3 Not Enough Memory For Mapping Data
    ''' <summary>Trigger Board3 Dummy Run: Error In Direction Check Process</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016228 = 2016228    'Trigger Board3 Dummy Run: Error In Direction Check Process
    ''' <summary>Trigger Board3 Fire: Error In Direction Check Process</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016229 = 2016229    'Trigger Board3 Fire: Error In Direction Check Process
    ''' <summary>Trigger Board3 HMI Communication Timeout</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016230 = 2016230    'Trigger Board3 HMI Communication Timeout
    ''' <summary>Trigger Board3 Driver Over Temperature</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016231 = 2016231    'Trigger Board3 Driver Over Temperature
    ''' <summary>Trigger Board3 Driver has no High Voltage</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016232 = 2016232    'Trigger Board3 Driver has no High Voltage
    ''' <summary>Trigger Board3 Communication fail with PICO valve</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016233 = 2016233    'Trigger Board3 Communication fail with PICO valve
    ''' <summary>Trigger Board3 Piezo Over Temperature</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016234 = 2016234    'Trigger Board3 Piezo Over Temperature
    ''' <summary>Trigger Board3 Driver Board reboot</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016235 = 2016235    'Trigger Board3 Driver Board reboot
    ''' <summary>"觸發卡4等待觸發逾時"</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016300 = 2016300    'Trigger Board4 TimeOut!
    ''' <summary>觸發卡4重置錯誤</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016301 = 2016301    'Trigger Board4 Reset Error!
    ''' <summary>Trigger Board4 Too Much or Incorrect Recipe Data, J Mode</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016302 = 2016302    'Trigger Board4 Too Much or Incorrect Recipe Data, J Mode
    ''' <summary>Trigger Board4 Too Much or Incorrect Recipe Data, T or P Mode</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016303 = 2016303    'Trigger Board4 Too Much or Incorrect Recipe Data, T or P Mode
    ''' <summary>Trigger Board4 Data Incorrect, X or D Command</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016304 = 2016304    'Trigger Board4 Data Incorrect, X or D Command
    ''' <summary>Trigger Board4 Data Incorrect, ParaTmp[]</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016305 = 2016305    'Trigger Board4 Data Incorrect, ParaTmp[]
    ''' <summary>Trigger Board4 Not Enough Memory For Target Points</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016306 = 2016306    'Trigger Board4 Not Enough Memory For Target Points
    ''' <summary>Trigger Board4 Data Incorrect, Cycle Time or Dots Number</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016307 = 2016307    'Trigger Board4 Data Incorrect, Cycle Time or Dots Number
    ''' <summary>Trigger Board4 Data Incorrect, Pitch or Dots Number</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016308 = 2016308    'Trigger Board4 Data Incorrect, Pitch or Dots Number
    ''' <summary>Trigger Board4 Fail To Generate Fire Table</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016309 = 2016309    'Trigger Board4 Fail To Generate Fire Table
    ''' <summary>Trigger Board4 Not Enough Memory For History Data</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016310 = 2016310    'Trigger Board4 Not Enough Memory For History Data
    ''' <summary>Trigger Board4 Dummy Run Do Not End Normally</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016311 = 2016311    'Trigger Board4 Dummy Run Do Not End Normally
    ''' <summary>Trigger Board4 Fail To Generate Trigger Advanced Distance</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016312 = 2016312    'Trigger Board4 Fail To Generate Trigger Advanced Distance
    ''' <summary>Trigger Board4 UART Receive Data Timeout</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016313 = 2016313    'Trigger Board4 UART Receive Data Timeout
    ''' <summary>Trigger Board4 Alarm Is Not Cleared</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016314 = 2016314    'Trigger Board4 Alarm Is Not Cleared
    ''' <summary>Trigger Board4 SPI:Too Many Fails To Talk With Slave</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016315 = 2016315    'Trigger Board4 SPI:Too Many Fails To Talk With Slave
    ''' <summary>Trigger Board4 Communication Error With Remote Display</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016316 = 2016316    'Trigger Board4 Communication Error With Remote Display
    ''' <summary>Trigger Board4 Selected PulseTime/Falling/Stroke Is Not Available</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016317 = 2016317    'Trigger Board4 Selected PulseTime/Falling/Stroke Is Not Available
    ''' <summary>Trigger Board4 Just Reboot</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016318 = 2016318    'Trigger Board4 Just Reboot
    ''' <summary>Trigger Board4 Fail To Set Parameters To Jetting Driver Board</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016319 = 2016319    'Trigger Board4 Fail To Set Parameters To Jetting Driver Board
    ''' <summary>Trigger Board4 Too Much or Incorrect Recipe Data, G or F Mode</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016320 = 2016320    'Trigger Board4 Too Much or Incorrect Recipe Data, G or F Mode
    ''' <summary>Trigger Board4 Failed to Trim The Recipe File Of F Mode</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016321 = 2016321    'Trigger Board4 Failed to Trim The Recipe File Of F Mode
    ''' <summary>Trigger Board4 Recipe: Point Type Should Not Connected With Line Type</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016322 = 2016322    'Trigger Board4 Recipe: Point Type Should Not Connected With Line Type
    ''' <summary>Trigger Board4 There Are Untriggered Dots</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016323 = 2016323    'Trigger Board4 There Are Untriggered Dots
    ''' <summary>Trigger Board4 Hit Too Late</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016324 = 2016324    'Trigger Board4 Hit Too Late
    ''' <summary>Trigger Board4 Away From Target, Will Never Be Hit</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016325 = 2016325    'Trigger Board4 Away From Target, Will Never Be Hit
    ''' <summary>Trigger Board4 Fire Table Content Has Zero</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016326 = 2016326    'Trigger Board4 Fire Table Content Has Zero
    ''' <summary>Trigger Board4 Not Enough Memory For Mapping Data</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016327 = 2016327    'Trigger Board4 Not Enough Memory For Mapping Data
    ''' <summary>Trigger Board4 Dummy Run: Error In Direction Check Process</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016328 = 2016328    'Trigger Board4 Dummy Run: Error In Direction Check Process
    ''' <summary>Trigger Board4 Fire: Error In Direction Check Process</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016329 = 2016329    'Trigger Board4 Fire: Error In Direction Check Process
    ''' <summary>Trigger Board4 HMI Communication Timeout</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016330 = 2016330    'Trigger Board4 HMI Communication Timeout
    ''' <summary>Trigger Board4 Driver Over Temperature</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016331 = 2016331    'Trigger Board4 Driver Over Temperature
    ''' <summary>Trigger Board4 Driver has no High Voltage</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016332 = 2016332    'Trigger Board4 Driver has no High Voltage
    ''' <summary>Trigger Board4 Communication fail with PICO valve</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016333 = 2016333    'Trigger Board4 Communication fail with PICO valve
    ''' <summary>Trigger Board4 Piezo Over Temperature</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016334 = 2016334    'Trigger Board4 Piezo Over Temperature
    ''' <summary>Trigger Board4 Driver Board reboot</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2016335 = 2016335    'Trigger Board4 Driver Board reboot
    ''' <summary>FMCS1初始化失敗! 通訊埠被佔用!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017000 = 2017000    'Initialize FMCS1 Failed! Port is Occupied!
    ''' <summary>FMCS1資料超過規格上界!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017001 = 2017001    'FMCS1 Data is Out of USL
    ''' <summary>FMCS1資料低於規格下界!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017002 = 2017002    'FMCS1 Data is Out of LSL
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017003 = 2017003    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017004 = 2017004    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017005 = 2017005    '
    ''' <summary>FMCS2初始化失敗! 通訊埠被佔用!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017100 = 2017100    'Initialize FMCS2 Failed! Port is Occupied!
    ''' <summary>FMCS2資料超過規格上界!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017101 = 2017101    'FMCS2 Data is Out of USL
    ''' <summary>FMCS2資料低於規格下界!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017102 = 2017102    'FMCS2 Data is Out of LSL
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017103 = 2017103    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017104 = 2017104    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017105 = 2017105    '
    ''' <summary>FMCS3初始化失敗! 通訊埠被佔用!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017200 = 2017200    'Initialize FMCS3 Failed! Port is Occupied!
    ''' <summary>FMCS3資料超過規格上界!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017201 = 2017201    'FMCS3 Data is Out of USL
    ''' <summary>FMCS3資料低於規格下界!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017202 = 2017202    'FMCS3 Data is Out of LSL
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017203 = 2017203    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017204 = 2017204    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017205 = 2017205    '
    ''' <summary>FMCS4初始化失敗! 通訊埠被佔用!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017300 = 2017300    'Initialize FMCS4 Failed! Port is Occupied!
    ''' <summary>FMCS4資料超過規格上界!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017301 = 2017301    'FMCS4 Data is Out of USL
    ''' <summary>FMCS4資料低於規格下界!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017302 = 2017302    'FMCS4 Data is Out of LSL
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017303 = 2017303    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017304 = 2017304    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2017305 = 2017305    '
    ''' <summary>閥1膠管氣壓建立異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019000 = 2019000    'Valve1 Syringe Air Pressure Alarm!
    ''' <summary>閥1Purge真空建立失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019001 = 2019001    'Valve1 Purge Vacuum Alarm
    ''' <summary>閥1控制器異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019002 = 2019002    'Valve1 Controller Alarm!
    ''' <summary>閥1膠重判定異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019003 = 2019003    'Valve1 Weight is Out of Range!
    ''' <summary>閥1膠量判定異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019004 = 2019004    'Valve1 Volume is Out of Range!
    ''' <summary>閥1自動校正失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019005 = 2019005    'Valve1 Auto Calibration Failed!
    ''' <summary>閥1自動測高失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019006 = 2019006    'Valve1 Auto Find Height Failed!
    ''' <summary>閥1膠閥清潔失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019007 = 2019007    'Valve1 Clear Failed!
    ''' <summary>閥1閥座溫度判定異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019008 = 2019008    'Valve1 ValveSet Temperature is Out of Range
    ''' <summary>閥1閥體溫度判定異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019009 = 2019009    'Valve1 ValveBody Temperature is Out of Range
    ''' <summary>閥1膠管溫度判定異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019010 = 2019010    'Valve1 Syringe Temperature is Out of Range
    ''' <summary>閥1換膠動作異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019011 = 2019011    'Valve1 Change Action Failed!
    ''' <summary>閥1Purge動作異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019012 = 2019012    'Valve1 Purge Action Failed!
    ''' <summary>閥1秤重動作異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019013 = 2019013    'Valve1 Weighing Action Failed!
    ''' <summary>閥1Purge參數讀取失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019014 = 2019014    'Valve1 Purge Parameter Reading Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019015 = 2019015    '
    ''' <summary>閥2膠管氣壓建立異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019100 = 2019100    'Valve2 Syringe Air Pressure Alarm!
    ''' <summary>閥2Purge真空建立失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019101 = 2019101    'Valve2 Purge Vacuum Alarm
    ''' <summary>閥2控制器異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019102 = 2019102    'Valve2 Controller Alarm!
    ''' <summary>閥2膠重判定異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019103 = 2019103    'Valve2 Weight is Out of Range!
    ''' <summary>閥2膠量判定異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019104 = 2019104    'Valve2 Volume is Out of Range!
    ''' <summary>閥2自動校正失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019105 = 2019105    'Valve2 Auto Calibration Failed!
    ''' <summary>閥2自動測高失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019106 = 2019106    'Valve2 Auto Find Height Failed!
    ''' <summary>閥2膠閥清潔失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019107 = 2019107    'Valve2 Clear Failed!
    ''' <summary>閥2閥座溫度判定異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019108 = 2019108    'Valve2 ValveSet Temperature is Out of Range
    ''' <summary>閥2閥體溫度判定異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019109 = 2019109    'Valve2 ValveBody Temperature is Out of Range
    ''' <summary>閥2膠管溫度判定異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019110 = 2019110    'Valve2 Syringe Temperature is Out of Range
    ''' <summary>閥2換膠動作異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019111 = 2019111    'Valve2 Change Action Failed!
    ''' <summary>閥2Purge動作異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019112 = 2019112    'Valve2 Purge Action Failed!
    ''' <summary>閥2秤重動作異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019113 = 2019113    'Valve2 Weighing Action Failed!
    ''' <summary>閥2Purge參數讀取失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019114 = 2019114    'Valve2 Purge Parameter Reading Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019115 = 2019115    '
    ''' <summary>閥3膠管氣壓建立異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019200 = 2019200    'Valve3 Syringe Air Pressure Alarm!
    ''' <summary>閥3Purge真空建立失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019201 = 2019201    'Valve3 Purge Vacuum Alarm
    ''' <summary>閥3控制器異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019202 = 2019202    'Valve3 Controller Alarm!
    ''' <summary>閥3膠重判定異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019203 = 2019203    'Valve3 Weight is Out of Range!
    ''' <summary>閥3膠量判定異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019204 = 2019204    'Valve3 Volume is Out of Range!
    ''' <summary>閥3自動校正失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019205 = 2019205    'Valve3 Auto Calibration Failed!
    ''' <summary>閥3自動測高失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019206 = 2019206    'Valve3 Auto Find Height Failed!
    ''' <summary>閥3膠閥清潔失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019207 = 2019207    'Valve3 Clear Failed!
    ''' <summary>閥3閥座溫度判定異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019208 = 2019208    'Valve3 ValveSet Temperature is Out of Range
    ''' <summary>閥3閥體溫度判定異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019209 = 2019209    'Valve3 ValveBody Temperature is Out of Range
    ''' <summary>閥3膠管溫度判定異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019210 = 2019210    'Valve3 Syringe Temperature is Out of Range
    ''' <summary>閥3換膠動作異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019211 = 2019211    'Valve3 Change Action Failed!
    ''' <summary>閥3Purge動作異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019212 = 2019212    'Valve3 Purge Action Failed!
    ''' <summary>閥3秤重動作異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019213 = 2019213    'Valve3 Weighing Action Failed!
    ''' <summary>閥3Purge參數讀取失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019214 = 2019214    'Valve3 Purge Parameter Reading Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019215 = 2019215    '
    ''' <summary>閥4膠管氣壓建立異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019300 = 2019300    'Valve4 Syringe Air Pressure Alarm!
    ''' <summary>閥4Purge真空建立失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019301 = 2019301    'Valve4 Purge Vacuum Alarm
    ''' <summary>閥4控制器異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019302 = 2019302    'Valve4 Controller Alarm!
    ''' <summary>閥4膠重判定異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019303 = 2019303    'Valve4 Weight is Out of Range!
    ''' <summary>閥4膠量判定異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019304 = 2019304    'Valve4 Volume is Out of Range!
    ''' <summary>閥4自動校正失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019305 = 2019305    'Valve4 Auto Calibration Failed!
    ''' <summary>閥4自動測高失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019306 = 2019306    'Valve4 Auto Find Height Failed!
    ''' <summary>閥4膠閥清潔失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019307 = 2019307    'Valve4 Clear Failed!
    ''' <summary>閥4閥座溫度判定異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019308 = 2019308    'Valve4 ValveSet Temperature is Out of Range
    ''' <summary>閥4閥體溫度判定異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019309 = 2019309    'Valve4 ValveBody Temperature is Out of Range
    ''' <summary>閥4膠管溫度判定異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019310 = 2019310    'Valve4 Syringe Temperature is Out of Range
    ''' <summary>閥4換膠動作異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019311 = 2019311    'Valve4 Change Action Failed!
    ''' <summary>閥4Purge動作異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019312 = 2019312    'Valve4 Purge Action Failed!
    ''' <summary>閥4秤重動作異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019313 = 2019313    'Valve4 Weighing Action Failed!
    ''' <summary>閥4Purge參數讀取失敗!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019314 = 2019314    'Valve4 Purge Parameter Reading Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2019315 = 2019315    '
    ''' <summary>螺桿閥1過電流警報!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020000 = 2020000    'Auger Valve1 CT Alarm!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020001 = 2020001    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020002 = 2020002    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020003 = 2020003    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020004 = 2020004    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020005 = 2020005    '
    ''' <summary>螺桿閥2過電流警報!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020100 = 2020100    'Auger Valve2 CT Alarm!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020101 = 2020101    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020102 = 2020102    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020103 = 2020103    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020104 = 2020104    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020105 = 2020105    '
    ''' <summary>螺桿閥3過電流警報!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020200 = 2020200    'Auger Valve3 CT Alarm!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020201 = 2020201    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020202 = 2020202    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020203 = 2020203    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020204 = 2020204    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020205 = 2020205    '
    ''' <summary>螺桿閥4過電流警報!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020300 = 2020300    'Auger Valve4 CT Alarm!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020301 = 2020301    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020302 = 2020302    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020303 = 2020303    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020304 = 2020304    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2020305 = 2020305    '
    ''' <summary>閥控制器讀取資料失敗</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2024000 = 2024000    'Valve Controller1 Read Data Failed!
    ''' <summary>"閥控制器寫資料失敗"</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2024001 = 2024001    'Valve Controller1 Write Data Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2024002 = 2024002    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2024003 = 2024003    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2024004 = 2024004    '
    ''' <summary>平台1CCD定位動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2036000 = 2036000    'Stage1 CCD Alignment Failed!
    ''' <summary>平台1雷射測高動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2036001 = 2036001    'Stage1 Z-Measurement Failed!
    ''' <summary>平台1CCD檢測動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2036002 = 2036002    'Stage1 CCD Inspection Failed!
    ''' <summary>平台1同步換膠動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2036003 = 2036003    'Stage1 Synchronized Exchange Failed!
    ''' <summary>平台1同步清膠動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2036004 = 2036004    'Stage1 Synchronized Clean Failed!
    ''' <summary>平台1同步Purge動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2036005 = 2036005    'Stage1 Synchronized Purge Failed!
    ''' <summary>平台1順序秤重動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2036006 = 2036006    'Stage1 Sequential Weighing Failed!
    ''' <summary>平台1同步自動測高異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2036007 = 2036007    'Stage1 Synchronized Auto Z-Measurement Failed!
    ''' <summary>平台1同步自動校正異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2036008 = 2036008    'Stage1 Synchronized Auto Calibration Failed!
    ''' <summary>平台1復歸逾時!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2036009 = 2036009    'Stage1 Home Timeout!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2036010 = 2036010    '
    ''' <summary>(Station 1)阻擋氣缸未下降，請檢查!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037000 = 2037000    'Station1 Stopper Down Failed!
    ''' <summary>(Station 1)阻擋氣缸未上升，請檢查!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037001 = 2037001    'Station1 Stopper Up Failed!
    ''' <summary>(Station 1)頂升氣缸未下降，請檢查!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037002 = 2037002    'Station1 Chuck Down Failed!
    ''' <summary>(Station 1)頂升氣缸未上升，請檢查!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037003 = 2037003    'Station1 Chuck Up Failed!
    ''' <summary>(Station 1)料盤感測器偵測到Tray盤，請拿走!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037004 = 2037004    'Station1 Tray on Conveyor! Remove It!
    ''' <summary>(Station 1)料盤感測器偵測Tray盤超時，請檢查!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037005 = 2037005    'Station1 Tray Sensor Timeout!
    ''' <summary>(Station 1)夾具真空未建立，請檢查!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037006 = 2037006    'Station1 Chuck Vacuum build Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037007 = 2037007    '
    ''' <summary>(Station 2)阻擋氣缸未下降，請檢查!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037100 = 2037100    'Station2 Stopper Down Failed!
    ''' <summary>(Station 2)阻擋氣缸未上升，請檢查!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037101 = 2037101    'Station2 Stopper Up Failed!
    ''' <summary>(Station 2)頂升氣缸未下降，請檢查!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037102 = 2037102    'Station2 Chuck Down Failed!
    ''' <summary>(Station 2)頂升氣缸未上升，請檢查!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037103 = 2037103    'Station2 Chuck Up Failed!
    ''' <summary>(Station 2)料盤感測器偵測到Tray盤，請拿走!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037104 = 2037104    'Station2 Tray on Conveyor! Remove It!
    ''' <summary>(Station 2)料盤感測器偵測Tray盤超時，請檢查!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037105 = 2037105    'Station2 Tray Sensor Timeout!
    ''' <summary>(Station 2)夾具真空未建立，請檢查!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037106 = 2037106    'Station2 Chuck Vacuum build Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037107 = 2037107    '
    ''' <summary>(Station 2)料盤氣缸夾持未關閉，請檢查!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037108 = 2037108    'Station2 Tray Clamp Close Failed!
    ''' <summary>(Station 2)料盤氣缸夾持未打開，請檢查!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037109 = 2037109    'Station2 Tray Clamp Open Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037110 = 2037110    '
    ''' <summary>(Station 3)阻擋氣缸未下降，請檢查!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037200 = 2037200    'Station3 Stopper Down Failed!
    ''' <summary>(Station 3)阻擋氣缸未上升，請檢查!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037201 = 2037201    'Station3 Stopper Up Failed!
    ''' <summary>(Station 3)頂升氣缸未下降，請檢查!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037202 = 2037202    'Station3 Chuck Down Failed!
    ''' <summary>(Station 3)頂升氣缸未上升，請檢查!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037203 = 2037203    'Station3 Chuck Up Failed!
    ''' <summary>(Station 3)料盤感測器偵測到Tray盤，請拿走!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037204 = 2037204    'Station3 Tray on Conveyor! Remove It!
    ''' <summary>(Station 3)料盤感測器偵測Tray盤超時，請檢查!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037205 = 2037205    'Station3 Tray Sensor Timeout!
    ''' <summary>(Station 3)夾具真空未建立，請檢查!!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037206 = 2037206    'Station3 Chuck Vacuum build Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037207 = 2037207    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037300 = 2037300    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037301 = 2037301    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037302 = 2037302    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037303 = 2037303    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037304 = 2037304    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2037305 = 2037305    '
    ''' <summary>平台2CCD定位動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2048000 = 2048000    'Stage2 CCD Alignment Failed!
    ''' <summary>平台2雷射測高動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2048001 = 2048001    'Stage2 Z-Measurement Failed!
    ''' <summary>平台2CCD檢測動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2048002 = 2048002    'Stage2 CCD Inspection Failed!
    ''' <summary>平台2同步換膠動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2048003 = 2048003    'Stage2 Synchronized Exchange Failed!
    ''' <summary>平台2同步清膠動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2048004 = 2048004    'Stage2 Synchronized Clean Failed!
    ''' <summary>平台2同步Purge動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2048005 = 2048005    'Stage2 Synchronized Purge Failed!
    ''' <summary>平台2順序秤重動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2048006 = 2048006    'Stage2 Sequential Weighing Failed!
    ''' <summary>平台2同步自動測高異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2048007 = 2048007    'Stage2 Synchronized Auto Z-Measurement Failed!
    ''' <summary>平台2同步自動校正異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2048008 = 2048008    'Stage2 Synchronized Auto Calibration Failed!
    ''' <summary>平台2復歸逾時!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2048009 = 2048009    'Stage2 Home Timeout!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2048010 = 2048010    '
    ''' <summary>進料端通訊逾時!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054000 = 2054000    'Loader Communication Timeout!
    ''' <summary>退料端通訊逾時!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054001 = 2054001    'Unloader Communication Timeout!
    ''' <summary>進料端溫控設定失敗</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054002 = 2054002    'The Loader Temperature is setting failed!!!
    ''' <summary>退料端溫控設定失敗</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054003 = 2054003    'The Unloader Temperature is setting failed!!!
    ''' <summary>進料端溫控設定為0</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054004 = 2054004    'The Loader Temperature is setting 0!!!
    ''' <summary>退料端溫控設定為0</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054005 = 2054005    'The Unloader Temperature is setting 0!!!
    ''' <summary>Loader PLC Timeout : Get Machine Status.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054006 = 2054006    'Loader PLC Timeout : Get Machine Status. 
    ''' <summary>Loader PLC Timeout : Get Pass Model.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054007 = 2054007    ' Loader PLC Timeout : Get Pass Model.
    ''' <summary>Loader PLC Timeout : Set Pass Model.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054008 = 2054008    ' Loader PLC Timeout : Set Pass Model.
    ''' <summary>Loader PLC Timeout : Get Alarm Code.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054009 = 2054009    ' Loader PLC Timeout : Get Alarm Code.
    ''' <summary>Loader PLC Timeout : Set Product Type.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054010 = 2054010    ' Loader PLC Timeout : Set Product Type.
    ''' <summary>Loader PLC Timeout : Get Product Type.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054011 = 2054011    ' Loader PLC Timeout : Get Product Type.
    ''' <summary>Loader PLC Timeout : Get Hot Plate Temperatures.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054012 = 2054012    ' Loader PLC Timeout : Get Hot Plate Temperatures.
    ''' <summary>Loader PLC Timeout : Get Target Temperature.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054013 = 2054013    ' Loader PLC Timeout : Get Target Temperature.
    ''' <summary>Loader PLC Timeout : Set Target Temperature.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054014 = 2054014    ' Loader PLC Timeout : Set Target Temperature.
    ''' <summary>Loader PLC Timeout : Get Product Number.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054015 = 2054015    ' Loader PLC Timeout : Get Product Number.
    ''' <summary>Loader PLC Timeout : Set Product Number.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054016 = 2054016    ' Loader PLC Timeout : Set Product Number.
    ''' <summary>Loader PLC Timeout : Get Cassette Data.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054017 = 2054017    ' Loader PLC Timeout : Get Cassette Data.
    ''' <summary>Loader PLC Timeout : Get Cassette Barcode.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054018 = 2054018    ' Loader PLC Timeout : Get Cassette Barcode.
    ''' <summary>Loader PLC Timeout : Set Cassette Barcode.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054019 = 2054019    ' Loader PLC Timeout : Set Cassette Barcode.
    ''' <summary>Loader PLC Timeout : Cassette Abort.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054020 = 2054020    ' Loader PLC Timeout : Cassette Abort.
    ''' <summary>Loader PLC ExMessage</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054021 = 2054021    ' Loader PLC ExMessage
    ''' <summary>Unloader PLC Timeout : Get Machine Status.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054022 = 2054022    ' Unloader PLC Timeout : Get Machine Status.
    ''' <summary>Unloader PLC Timeout : Get Pass Model.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054023 = 2054023    ' Unloader PLC Timeout : Get Pass Model.
    ''' <summary>Unloader PLC Timeout : Set Pass Model.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054024 = 2054024    ' Unloader PLC Timeout : Set Pass Model.
    ''' <summary>Unloader PLC Timeout : Get Alarm Code.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054025 = 2054025    ' Unloader PLC Timeout : Get Alarm Code.
    ''' <summary>Unloader PLC Timeout : Set Product Type.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054026 = 2054026    ' Unloader PLC Timeout : Set Product Type.
    ''' <summary>Unloader PLC Timeout : Get Product Type.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054027 = 2054027    ' Unloader PLC Timeout : Get Product Type.
    ''' <summary>Unloader PLC Timeout : Get Hot Plate Temperatures.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054028 = 2054028    ' Unloader PLC Timeout : Get Hot Plate Temperatures.
    ''' <summary>Unloader PLC Timeout : Set Target Temperature.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054029 = 2054029    ' Unloader PLC Timeout : Set Target Temperature.
    ''' <summary>Unloader PLC Timeout : Get Target Temperature.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054030 = 2054030    ' Unloader PLC Timeout : Get Target Temperature.
    ''' <summary>Unloader PLC Timeout : Get Product Number.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054031 = 2054031    'Unloader PLC Timeout : Get Product Number.
    ''' <summary>Unloader PLC Timeout : Set Product Number.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054032 = 2054032    ' Unloader PLC Timeout : Set Product Number.
    ''' <summary>Unloader PLC Timeout : Get Cassette Data.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054033 = 2054033    'Unloader PLC Timeout : Get Cassette Data.
    ''' <summary>Unloader PLC Timeout : Cassette Abort.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054034 = 2054034    ' Unloader PLC Timeout : Cassette Abort.
    ''' <summary>Unloader PLC Timeout : Set Last Product Number.</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054035 = 2054035    ' Unloader PLC Timeout : Set Last Product Number.
    ''' <summary>Unloader PLC ExMessage</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054036 = 2054036    ' Unloader PLC ExMessage
    ''' <summary>Loader PLC連結失敗</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054037 = 2054037    ' Loader PLC connect fail.
    ''' <summary>Unloader PLC連結失敗</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2054038 = 2054038    ' Unloader PLC connect fail.
    ''' <summary>平台3CCD定位動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2066000 = 2066000    'Stage3 CCD Alignment Failed!
    ''' <summary>平台3雷射測高動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2066001 = 2066001    'Stage3 Z-Measurement Failed!
    ''' <summary>平台3CCD檢測動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2066002 = 2066002    'Stage3 CCD Inspection Failed!
    ''' <summary>平台3同步換膠動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2066003 = 2066003    'Stage3 Synchronized Exchange Failed!
    ''' <summary>平台3同步清膠動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2066004 = 2066004    'Stage3 Synchronized Clean Failed!
    ''' <summary>平台3同步Purge動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2066005 = 2066005    'Stage3 Synchronized Purge Failed!
    ''' <summary>平台3順序秤重動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2066006 = 2066006    'Stage3 Sequential Weighing Failed!
    ''' <summary>平台3同步自動測高異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2066007 = 2066007    'Stage3 Synchronized Auto Z-Measurement Failed!
    ''' <summary>平台3同步自動校正異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2066008 = 2066008    'Stage3 Synchronized Auto Calibration Failed!
    ''' <summary>平台3復歸逾時!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2066009 = 2066009    'Stage3 Home Timeout!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2066010 = 2066010    '
    ''' <summary>平台4CCD定位動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2073000 = 2073000    'Stage4 CCD Alignment Failed!
    ''' <summary>平台4雷射測高動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2073001 = 2073001    'Stage4 Z-Measurement Failed!
    ''' <summary>平台4CCD檢測動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2073002 = 2073002    'Stage4 CCD Inspection Failed!
    ''' <summary>平台4同步換膠動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2073003 = 2073003    'Stage4 Synchronized Exchange Failed!
    ''' <summary>平台4同步清膠動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2073004 = 2073004    'Stage4 Synchronized Clean Failed!
    ''' <summary>平台4同步Purge動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2073005 = 2073005    'Stage4 Synchronized Purge Failed!
    ''' <summary>平台4順序秤重動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2073006 = 2073006    'Stage4 Sequential Weighing Failed!
    ''' <summary>平台4同步自動測高異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2073007 = 2073007    'Stage4 Synchronized Auto Z-Measurement Failed!
    ''' <summary>平台4同步自動校正異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2073008 = 2073008    'Stage4 Synchronized Auto Calibration Failed!
    ''' <summary>平台4復歸逾時!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2073009 = 2073009    'Stage4 Home Timeout!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2073010 = 2073010    '
    ''' <summary>整機狀態異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080000 = 2080000    'Over-all Status Alarm!
    ''' <summary>整機自動生產動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080001 = 2080001    'Over-all Auto Run Failed!
    ''' <summary>整機復歸動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080002 = 2080002    'Over-all Home Failed!
    ''' <summary>整機EMO停機</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080003 = 2080003    'Over-all EMO Stop!
    ''' <summary>Recipe與MappingData種類不同</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080004 = 2080004    'Recipe and MappingData Type is Not Matched!
    ''' <summary>Recipe與MappingData大小不同</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080005 = 2080005    'Recipe and MappingData Size is Not Matched!
    ''' <summary>X軸移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080006 = 2080006    'Axis X Move Error
    ''' <summary>Y軸移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080007 = 2080007    ' Axis Y Move Error
    ''' <summary>Z軸移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080008 = 2080008    'Axis Z Move Error
    ''' <summary>Tilt軸移動失敗</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080009 = 2080009    'Axis Tilt Move Error
    ''' <summary>X.Y軸移動逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080010 = 2080010    'Axis X,Y Wait Inposition Time Out!!!
    ''' <summary>SMEMA 載入準備逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080011 = 2080011    'SMEMA Loader Ready Timeout.
    ''' <summary>SMEMA 載入預備關閉逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080012 = 2080012    'SMEMA Loader Ready OFF Timeout.
    ''' <summary>SMEMA 載出預備逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080013 = 2080013    'SMEMA Unloader Ready Timeout.
    ''' <summary>A/B機氣壓缸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080014 = 2080014    'Machine A/B Electric Cylinder 'Home' Timeout.
    ''' <summary>A/B機阻擋板下降逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080015 = 2080015    'Machine A/B Stoper 'Down' Timeout.
    ''' <summary>SMEMA 載入預備逾時或感測器不在安全位置</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080016 = 2080016    'SMEMA Loader Ready Timeout Or Sensor Not Safe.
    ''' <summary>A/B機氣壓缸復歸逾時或A機阻擋板下降逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080017 = 2080017    'Machine A/B Electric Cylinder 'Home' Timeout or Machine A Stoper 'Down' Timeout.
    ''' <summary>SMEMA 載出預備逾時或感測器不在安全位置</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080018 = 2080018    'SMEMA Unloader Ready Timeout Or Sensor Not Safe.
    ''' <summary>SMEMA 載出預備關閉逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080019 = 2080019    'SMEMA Unloader Ready OFF Timeout.
    ''' <summary>工作區確認狀態錯誤</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080020 = 2080020    'Work-Station Check State is False!!!
    ''' <summary>工作區確認狀態正確</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080021 = 2080021    'Work-Station CheckState is True!!!
    ''' <summary>沒有Tilt軸</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080022 = 2080022    'None Tilt Axis
    ''' <summary>請先停止 Auto Run</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080023 = 2080023    'Stop Auto Run, Please.
    ''' <summary>Tilt 位置錯誤!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2080024 = 2080024    'Tilt Pos Error!
    ''' <summary>A機狀態異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2081000 = 2081000    'Machine-A Status Alarm!
    ''' <summary>A機自動生產動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2081001 = 2081001    'Machine-A Auto Run Failed!
    ''' <summary>A機復歸動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2081002 = 2081002    'Machine-A Home Failed!
    ''' <summary>A機進料異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2081003 = 2081003    'Machine-A Load Failed!
    ''' <summary>A機退料異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2081004 = 2081004    'Machine-A Unload Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2081005 = 2081005    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2081006 = 2081006    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2081007 = 2081007    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2081008 = 2081008    '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2081009 = 2081009    '
    ''' <summary>A機EMS停機</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2081100 = 2081100    'Machine-A EMS Stop!
    ''' <summary>A機CDA異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2081101 = 2081101    'Machine-A CDA Alarm!
    ''' <summary>A機門鎖未關</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2081102 = 2081102    'Machine-A Door ajar!
    ''' <summary>A機馬達電力異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2081103 = 2081103    'Machine-A Motor Power Alarm!
    ''' <summary>A機溫控電力異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2081104 = 2081104    'Machine-A Temp Power Alarm!
    ''' <summary>A機門鎖未鎖</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2081105 = 2081105    'Machine-A Door Interlock!
    ''' <summary>ERROR A : DataOutputAseMap</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2081106 = 2081106    'ERROR A : DataOutputAseMap
    ''' <summary>ERROR A : CoverMapData</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2081107 = 2081107    'ERROR A : CoverMapData
    ''' <summary>B機狀態異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2082000 = 2082000    'Machine-B Status Alarm!
    ''' <summary>B機自動生產動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2082001 = 2082001    'Machine-B Auto Run Failed!
    ''' <summary>B機復歸動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2082002 = 2082002    'Machine-B Home Failed!
    ''' <summary>B機進料異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2082003 = 2082003    'Machine-B Load Failed!
    ''' <summary>B機退料異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2082004 = 2082004    'Machine-B Unload Failed!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Alarm_2082005 = 2082005    '
    ''' <summary>B機EMS停機</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2082100 = 2082100    'Machine-B EMS Stop!
    ''' <summary>B機CDA異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2082101 = 2082101    'Machine-B CDA Alarm!
    ''' <summary>B機門鎖未關</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2082102 = 2082102    'Machine-B Door ajar!
    ''' <summary>B機馬達電力異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2082103 = 2082103    'Machine-B Motor Power Alarm!
    ''' <summary>B機溫控電力異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2082104 = 2082104    'Machine-B Temp Power Alarm!
    ''' <summary>B機門鎖未鎖</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2082105 = 2082105    'Machine-B Door Interlock!
    ''' <summary>ERROR B : DataOutputAseMap</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2082106 = 2082106    'ERROR B : DataOutputAseMap
    ''' <summary>ERROR B : CoverMapData</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2082107 = 2082107    'ERROR B : CoverMapData
    ''' <summary>Conveyor1狀態異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083000 = 2083000    'Conveyor1 Status Alarm!
    ''' <summary>Conveyor1自動生產動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083001 = 2083001    'Conveyor1 Auto Run Failed!
    ''' <summary>Conveyor1復歸動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083002 = 2083002    'Conveyor1 Home Failed!
    ''' <summary>Conveyor1 A機進料異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083003 = 2083003    'Conveyor1 Load to Machine-A Failed!
    ''' <summary>Conveyor1 A機退料異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083004 = 2083004    'Conveyor1 Unload from Machine-A Failed!
    ''' <summary>Conveyor1 B機進料異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083005 = 2083005    'Conveyor1 Load to Machine-B Failed!
    ''' <summary>Conveyor1 B機退料異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083006 = 2083006    'Conveyor1 Unload from Machine-B Failed!
    ''' <summary>Conveyor1 進料異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083007 = 2083007    'Conveyor1 Load Failed!
    ''' <summary>Conveyor1 退料異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083008 = 2083008    'Conveyor1 Unload Failed!
    ''' <summary>Conveyor1 EMS停機</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083100 = 2083100    'Conveyor1 EMS Stop!
    ''' <summary>Conveyor1 CDA異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083101 = 2083101    'Conveyor1 CDA Alarm!
    ''' <summary>Conveyor1 門鎖未關</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083102 = 2083102    'Conveyor1 Door ajar!
    ''' <summary>Conveyor1 馬達電力異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083103 = 2083103    'Conveyor1 Motor Power Alarm!
    ''' <summary>Conveyor1 溫控電力異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083104 = 2083104    'Conveyor1 Temp Power Alarm!
    ''' <summary>Conveyor1 馬達異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083105 = 2083105    'Conveyor1 Motor Alarm!
    ''' <summary>Conveyor1 未清空!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083106 = 2083106    'Conveyor1 Is Not Empty
    ''' <summary>Conveyor1 設定速度異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083107 = 2083107    'Conveyor1 Is Set Speed Error.
    ''' <summary>Conveyor1 真空逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083108 = 2083108    'Conveyor1 Is Set Speed Error
    ''' <summary>A機阻擋缸上升逾時/前段阻擋缸上昇逾時/氣壓缸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083109 = 2083109    'Machine A Stoper 'Up' Timeout Or Front Stoper 'Up' Timeout Or Electric Cylinder 'Home' Timeout.
    ''' <summary>A機上有產品</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083110 = 2083110    'There Are Products On Machine A
    ''' <summary>A機氣壓缸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083111 = 2083111    'TMachine A Electric Cylinder 'Home' Timeout.
    ''' <summary>A機阻擋缸上升逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083112 = 2083112    'Machine A Stoper 'Up' Timeout.
    ''' <summary>A機前段阻擋缸下降逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083113 = 2083113    'Machine A Front Stoper 'Down' Timeout.
    ''' <summary>A機入口感測器逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083114 = 2083114    'Machine A Entrance Sensor Timeout.
    ''' <summary>A機速度變換逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083115 = 2083115    'Machine A Speed Change Timeout.
    ''' <summary>A機阻擋缸感測器逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083116 = 2083116    'Machine A Stoper Sensor Timeout.
    ''' <summary>A機前段阻擋缸上降逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083117 = 2083117    'Machine A Front Stoper 'Up' Timeout.
    ''' <summary>A機氣壓缸上升逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083118 = 2083118    'Machine A Electric Cylinder 'Up' Timeout.
    ''' <summary>A機真空開啟逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083119 = 2083119    'Machine A Vacuum ON Timeout.
    ''' <summary>A機真空關閉逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083120 = 2083120    'Machine A Vacuum OFF Timeout.
    ''' <summary>A機離開感測器逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083121 = 2083121    'Machine A Exit Sensor Timeout.
    ''' <summary>Conveyor1 安全狀態確認逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083122 = 2083122    'Conveyor1 Check Safe State Timeout.

    ''' <summary>Conveyor1 到位感測器觸發逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083123 = 2083123    'Conveyor1 Inposition Sensor Trigger Timeout.
    ''' <summary>Conveyor1 料盤逼緊缸夾關閉逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083124 = 2083124    'Conveyor1 Tray Clamp Close Timeout.
    ''' <summary>Conveyor1 料盤逼緊缸夾開啟逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083125 = 2083125    'Conveyor1 Tray Clamp Open Timeout.
    ''' <summary>Conveyor1 沒有料片在機台上</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083126 = 2083126    'Conveyor1 No Products On Top.
    ''' <summary>Conveyor1 阻擋缸下降逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083127 = 2083127    'Conveyor1 Stoper  Down Timeout.
    ''' <summary>Conveyor1 定位資料錯誤</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083128 = 2083128    'Conveyor1Alignment Data Error!
    ''' <summary>Conveyor1 SkipMark資料不存在</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083129 = 2083129    'Conveyor1 SkipMark Data Not Exists!
    ''' <summary>A機上/下站異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083130 = 2083130    'Machine-A Conveyor Prev/Next Alarm!
    ''' <summary>Conveyor1料盤逼緊缸預備逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2083131 = 2083131    'Conveyor1 Tray Clamp OnReady Timeout.
    ''' <summary>Conveyor2狀態異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084000 = 2084000    'Conveyor2 Status Alarm!
    ''' <summary>Conveyor2自動生產動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084001 = 2084001    'Conveyor2 Auto Run Failed!
    ''' <summary>Conveyor2復歸動作異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084002 = 2084002    'Conveyor2 Home Failed!
    ''' <summary>Conveyor2 A機進料異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084003 = 2084003    'Conveyor2 Load to Machine-A Failed!
    ''' <summary>Conveyor2 A機退料異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084004 = 2084004    'Conveyor2 Unload from Machine-A Failed!
    ''' <summary>Conveyor2 B機進料異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084005 = 2084005    'Conveyor2 Load to Machine-B Failed!
    ''' <summary>Conveyor2 B機退料異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084006 = 20843006    'Conveyor2 Unload from Machine-B Failed!
    ''' <summary>Conveyor2 進料異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084007 = 2084007    'Conveyor2 Load Failed!
    ''' <summary>Conveyor2 退料異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084008 = 2084008    'Conveyor2 Unload Failed!
    ''' <summary>Conveyor2 EMS停機</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084100 = 2084100    'Conveyor2 EMS Stop!
    ''' <summary>Conveyor2 CDA異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084101 = 2084101    'Conveyor2 CDA Alarm!
    ''' <summary>Conveyor2 門鎖未關</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084102 = 2084102    'Conveyor2 Door ajar!
    ''' <summary>Conveyor2 馬達電力異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084103 = 2084103    'Conveyor2 Motor Power Alarm!
    ''' <summary>Conveyor2 溫控電力異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084104 = 2084104    'Conveyor2 Temp Power Alarm!
    ''' <summary>Conveyor2 馬達異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084105 = 2084105    'Conveyor2 Motor Alarm!
    ''' <summary>Conveyor2 未清空!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084106 = 2084106    'Conveyor2 Is Not Empty
    ''' <summary>Conveyor2 設定速度異常!</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084107 = 2084107    'Conveyor2 Is Set Speed Error.
    ''' <summary>Conveyor2 真空逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084108 = 2084108    'Conveyor2 Vacuum Is TimeOut.
    ''' <summary>B機阻擋缸上升逾時/前段阻擋缸上昇逾時/氣壓缸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084109 = 2084109    'MAchine B Stoper 'Up' Timeout Or Electric Cylinder 'Home' Timeout.
    ''' <summary>B機上有產品</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084110 = 2084110    'There Are Products On Machine B
    ''' <summary>B機氣壓缸復歸逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084111 = 2084111    'Machine B Electric Cylinder 'Home' Timeout.
    ''' <summary>B機阻擋缸上升逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084112 = 2084112    'Machine B Stoper 'Up' Timeout.
    ''' <summary>B機阻擋缸下降逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084113 = 2084113    'Machine B Stoper 'Down' Timeout.
    ''' <summary>B機入口感測器逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084114 = 2084114    'Machine B Entrance Sensor Timeout.
    ''' <summary>B機滾輪速度變換逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084115 = 2084115    'Machine B Roller Change Speed Timeout.
    ''' <summary>B機阻擋缸感測器逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084116 = 2084116    'Machine B Stoper Sensor Timeout.
    ''' <summary>B機前段阻擋缸上降逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084117 = 2084117    'Machine B Front Stoper 'Up' Timeout.
    ''' <summary>B機氣壓缸上升逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084118 = 2084118    'Machine B Electric Cylinder 'Up' Timeout.
    ''' <summary>B機真空開啟逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084119 = 2084119    'Machine B Vacuum ON Timeout.
    ''' <summary>B機真空關閉逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084120 = 2084120    'Machine B Vacuum OFF Timeout.
    ''' <summary>B機離開感測器逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084121 = 2084121    'Machine B Exit Sensor Timeout.
    ''' <summary>Conveyor2  安全狀態確認逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084122 = 2084122    'Conveyor2 Check Safe State Timeout.
    ''' <summary>Conveyor2  到位感測器觸發逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084123 = 2084123    'Conveyor2  Inposition Sensor Trigger Timeout.
    ''' <summary>Conveyor2  料盤逼緊缸夾關閉逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084124 = 2084124    'Conveyor2  Tray Clamp Close Timeout.
    ''' <summary>Conveyor2  料盤逼緊缸夾開啟逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084125 = 2084125    'Conveyor2  Tray Clamp Open Timeout.
    ''' <summary>Conveyor2  沒有料片在機台上</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084126 = 2084126    'Conveyor2  No Products On Top.
    ''' <summary>Conveyor2  阻擋缸下降逾時</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084127 = 2084127    'Conveyor2  Stoper  Down Timeout.
    ''' <summary>Conveyor2  定位資料錯誤</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084128 = 2084128    'Conveyor2 Alignment Data Error!
    ''' <summary>Conveyor2  SkipMark資料不存在</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084129 = 2084129    'Conveyor2  SkipMark Data Not Exists!
    ''' <summary>B機上/下站異常</summary>
    ''' <remarks></remarks>
    Public Const Alarm_2084130 = 2084130    'Machine-B Conveyor Prev/Next Alarm!
    ''' <summary>使用者登入失敗.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000000 = 3000000 'User Login Failed!
    ''' <summary>使用者登出失敗.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000001 = 3000001 'User Logout Failed!
    ''' <summary>"權限不足,不能調整高階使用者權限."</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000002 = 3000002 'Permission is NOT enough to adjust high-end user.
    ''' <summary>設定權限高於目前使用者權限.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000003 = 3000003 'Setting Permission is Higher than Current User.
    ''' <summary>"使用者已存在,不能新增使用者"</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000004 = 3000004 '"User already exists, you can not add users."
    ''' <summary>請先復歸.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000005 = 3000005 '"Initialize First, Please."
    ''' <summary>"復歸中,請稍後."</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000006 = 3000006 'System is Homing...Please Wait.
    ''' <summary>"運行中,請稍後."</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000007 = 3000007 '"System is Running…,Please Wait."
    ''' <summary>"運行中無法暫停,請稍後."</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000008 = 3000008 'System is Running…Can Not Pause. Please Wait.
    ''' <summary>"運行中無法使用,請先復歸."</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000009 = 3000009 'System is Running…Initialize First.
    ''' <summary>自動校正中</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000010 = 3000010 'System is Auto Calibrating…
    ''' <summary>請先選取Recipe.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000011 = 3000011 '"Select Recipe, Please."
    ''' <summary>請先解除Recipe鎖定.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000012 = 3000012 '"Unlock Recipe First, Please."
    ''' <summary>請先選取Recipe Pattern.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000013 = 3000013 '"Select Recipe Pattern First, Please."
    ''' <summary>請先選取Pattern Step.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000014 = 3000014 '"Select Pattern Step First, Please."
    ''' <summary>請選擇場景.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000015 = 3000015 'Please Select Sence !
    ''' <summary>輸入資料錯誤.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000016 = 3000016 'Input Data is Wrong!
    ''' <summary>請確認開始與結束位置非重合.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000017 = 3000017 '"Check Start and End Position, Please."
    ''' <summary>請輸入角度(Deg)</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000018 = 3000018 '"Enter Angle(Deg), Please"
    ''' <summary>請選擇點膠設定項目.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000019 = 3000019 '"Select Dispense Item, Please."
    ''' <summary>場景不存在.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000020 = 3000020 'Scene does NOT Exist.
    ''' <summary>請輸入ID</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000021 = 3000021 'Please key In User ID!
    ''' <summary>"找不到特徵,校正失敗!"</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000022 = 3000022 '"Pattern No Found, Calibration Failed!"
    ''' <summary>請建立場景</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000023 = 3000023 'Please create Scene!
    ''' <summary>場景光源設定檔不存在</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000024 = 3000024 'Scene Light file NOT Exist!
    ''' <summary>Node 不存在,請先選擇Node</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000025 = 3000025 'NodeID Not Exists, Select Node First.
    ''' <summary>請先選擇天秤</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000026 = 3000026 'Select Balance First, Please!
    ''' <summary>請先教導場景</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000027 = 3000027 'Please teach Scence!
    ''' <summary>請先選擇天秤</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000028 = 3000028 'Select Scale First, Please!
    ''' <summary>位置異常!軸向超出極限位置</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000029 = 3000029 'Pos. Error! AxisParameter is Out Of Range
    ''' <summary>場景載入中，請稍後</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000030 = 3000030 'Scene is Loading, Please Wait.
    ''' <summary>請先選擇溫控檔</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000031 = 3000031 'Select Temperature File First, Please!
    ''' <summary>溫控檔不存在</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000032 = 3000032 'No Temperature File.
    ''' <summary>檔案開啟失敗</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000033 = 3000033 'Failed to open file.
    ''' <summary>物件不存在</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000034 = 3000034 'Object does NOT Exist.
    ''' <summary>檔案儲存失敗</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000035 = 3000035 'Failed to save file.
    ''' <summary>儲存完成</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000036 = 3000036 'Save OK!
    ''' <summary>使用者登入</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000037 = 3000037 'User Login.
    ''' <summary>場景載入失敗</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000038 = 3000038 'Scene Load Failed.
    ''' <summary>資料不相符，無法繼續執行</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000039 = 3000039 'Data is Not Equal, Can't Continue!!
    ''' <summary>請先選擇控制器</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000040 = 3000040 'Select Controller First, Please!
    ''' <summary>參數檔案不存在</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000041 = 3000041 'Parameter Not Exists!
    ''' <summary>Pattern使用中無法刪除</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000042 = 3000042 'Can't Delete this Pattern! It is in use!
    ''' <summary>Pattern不存在</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000043 = 3000043 'Pattern Not Exists!
    ''' <summary>請先選擇執行次數</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000044 = 3000044 'Select Round First, Please!
    ''' <summary>Recipe錯誤,請重新建立檔案</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000045 = 3000045 'Recipe Error, Please Create New File.
    ''' <summary>檔案名稱已存在</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000046 = 3000046 'File name Has Exist.
    ''' <summary>請確認開始與中間位置非重合.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000047 = 3000047 'Check Start and Middle Position, Please.
    ''' <summary>請確認中間與結束位置非重合.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000048 = 3000048 'Check Middle and End Position, Please.
    ''' <summary>請確認三點共線問題</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000049 = 3000049 'Check Three Point Collinea issue, Please.
    ''' <summary>請選擇測高模式</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000050 = 3000050 'Please select Measure Z Height Mode.
    ''' <summary>儲存完成!請重啟程式</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000051 = 3000051 'Save OK!Please Restart Software.
    ''' <summary>請先選擇CCD</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000052 = 3000052 'Select CCD First, Please!
    ''' <summary>請先選擇CCD裝置</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000053 = 3000053 'Select CCD Device First, Please!
    ''' <summary>請先選擇CCD SN</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000054 = 3000054 'Select CCD SN First, Please!
    ''' <summary>請先選擇COM Port</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000055 = 3000055 'Select COM Port, Please.
    ''' <summary>請先選擇 Laser Reader</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000056 = 3000056 'Select Laser Reader First, Please!
    ''' <summary>請先選擇觸發板</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000057 = 3000057 'Select Trigger Board First, Please!
    ''' <summary>檔案名稱錯誤</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000058 = 3000058 'File Name Error!
    ''' <summary>資料格式錯誤</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000059 = 3000059 'Data format error!
    ''' <summary>無相應站號</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000060 = 3000060 'Wrong Stage No.
    ''' <summary>無相應節點</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000061 = 3000061 'Wrong Node ID.
    ''' <summary>該層資料不存在</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000062 = 3000062 'Wrong Level No.
    ''' <summary>請選擇項目</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000063 = 3000063 'Please Select Item!
    ''' <summary>微量天平1 重量超出範圍</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000064 = 3000064 'Scale1 Weight out of Range!
    ''' <summary>微量天平2 重量超出範圍</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000065 = 3000065 'Scale2 Weight out of Range!
    ''' <summary>檔案不存在</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000066 = 3000066 'File Not Exists.
    ''' <summary>場景ID已經存在!</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000067 = 3000067 'SceneID Already Exists!
    ''' <summary>請先選擇電空閥!</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000068 = 3000068 'Select Electro Pneumatic Valve First, Please!
    ''' <summary>膠材名稱不存在</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000069 = 3000069 'Glue Name Not Exists.
    ''' <summary>請確認閥設定值!</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000070 = 3000070 'Plase Check Valve set !
    ''' <summary>請先破真空</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000071 = 3000071 'Please break the vacuum first
    ''' <summary>標準差為0，無法計算CPK</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000072 = 3000072 'Standard deviation is zero,CPK can not be calculated
    ''' <summary>閥氣壓值為0</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000073 = 3000073 'Valve AP Value is Zero.
    ''' <summary>Z 位置超出極限，請重試</summary>
    ''' <remarks></remarks>
    Public Const Warn_3000074 = 3000074 'Z Pos out of limit.Please retry.
    ''' <summary>CCD1定位不良.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3012000 = 3012000 'CCD1 Alignment NG.
    ''' <summary>CCD1重複叫用Start Live.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3012001 = 3012001 'CCD1 Repeat Call Start Live.
    ''' <summary>CCD1重複叫用End Live.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3012002 = 3012002 'CCD1 Repeat Call End Live.
    ''' <summary>CCD1清潔校正片.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3012003 = 3012003 'CCD1 Clear Calibration Piece
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012004 = 3012004 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012005 = 3012005 '
    ''' <summary>CCD1檢測不良.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3012100 = 3012100 'CCD1 Inspection NG.
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012101 = 3012101 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012102 = 3012102 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012103 = 3012103 '  
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012104 = 3012104 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012105 = 3012105 '
    ''' <summary>CCD2定位不良.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3012200 = 3012200 'CCD2 Alignment NG.
    ''' <summary>CCD2重複叫用Start Live.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3012201 = 3012201 'CCD2 Repeat Call Start Live.
    ''' <summary>CCD2重複叫用End Live.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3012202 = 3012202 'CCD2 Repeat Call End Live.
    ''' <summary>CCD2清潔校正片.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3012203 = 3012203 'CCD2 Clear Calibration Piece
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012204 = 3012204 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012205 = 3012205 '
    ''' <summary>CCD2檢測不良.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3003100 = 3003100 'CCD2 Inspection NG.
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3003101 = 3003101 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3003102 = 3003102 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3003103 = 3003103 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3003104 = 3003104 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3003105 = 3003105 '
    ''' <summary>CCD3定位不良.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3012300 = 3012300 'CCD3 Alignment NG.
    ''' <summary>CCD3重複叫用Start Live.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3012301 = 3012301 'CCD3 Repeat Call Start Live.
    ''' <summary>CCD3重複叫用End Live.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3012302 = 3012302 'CCD3 Repeat Call End Live.
    ''' <summary>CCD3清潔校正片.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3012303 = 3012303 'CCD3 Clear Calibration Piece
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012304 = 3012304 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012305 = 3012305 '
    ''' <summary>CCD3檢測不良.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3012400 = 3012400 'CCD3 Inspection NG.
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012401 = 3012401 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012402 = 3012402 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012403 = 3012403 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012404 = 3012404 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012405 = 3012405 '
    ''' <summary>CCD4定位不良.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3012500 = 3012500 'CCD4 Alignment NG.
    ''' <summary>CCD4重複叫用Start Live.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3012501 = 3012501 'CCD4 Repeat Call Start Live.
    ''' <summary>CCD4重複叫用End Live.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3012502 = 3012502 'CCD4 Repeat Call End Live.
    ''' <summary>CCD4清潔校正片.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3012503 = 3012503 'CCD4 Clear Calibration Piece
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012504 = 3012504 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012505 = 3012505 '
    ''' <summary>CCD4檢測不良.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3012600 = 3012600 'CCD4 Inspection NG.
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012601 = 3012601 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012602 = 3012602 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012603 = 3012603 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012604 = 3012604 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3012605 = 3012605 '
    ''' <summary>"閥1自動測高中,請稍後."</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019000 = 3019000 'Valve1 is Auto Detecting…Please Wait.
    ''' <summary>"閥1自動校正中,請稍後."</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019001 = 3019001 'Valve1 Auto Calibration Running…Please Wait.
    ''' <summary>閥1氣壓值設定失敗.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019002 = 3019002 'Set Valve1 AP Value: N.A.
    ''' <summary>閥1汽缸上抬逾時</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019003 = 3019003 'Valve1 Cylinder Up TimeOut.
    ''' <summary>閥1汽缸下降逾時</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019004 = 3019004 'Valve1 Cylinder Down TimeOut.
    ''' <summary>請更換閥1膠管</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019005 = 3019005 '"Exchange Valve1 Syringe, Please"
    ''' <summary>閥1膠量不足</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019006 = 3019006 'Valve1 Low level
    ''' <summary>閥1膠材壽命到期</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019007 = 3019007 'Valve1 LifeTime Expired
    ''' <summary>閥1膠材計數到期</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019008 = 3019008 'Valve1 Life Count Expired
    ''' <summary>請更換閥1清膠材料</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019009 = 3019009 'Exchange Valve1 Material!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3019010 = 3019010 '
    ''' <summary>"閥2自動測高中,請稍後."</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019100 = 3019100 'Valve2 is Auto Detecting…Please Wait.
    ''' <summary>"閥2自動校正中,請稍後."</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019101 = 3019101 'Valve2 Auto Calibration Running…Please Wait.
    ''' <summary>閥2氣壓值設定失敗.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019102 = 3019102 'Set Valve2 AP Value: N.A.
    ''' <summary>閥2汽缸上抬逾時</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019103 = 3019103 'Valve2 Cylinder Up TimeOut.
    ''' <summary>閥2汽缸下降逾時</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019104 = 3019104 'Valve2 Cylinder Down TimeOut.
    ''' <summary>請更換閥2膠管</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019105 = 3019105 '"Exchange Valve2 Syringe, Please"
    ''' <summary>閥2膠量不足</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019106 = 3019106 'Valve2 Low level
    ''' <summary>閥2膠材壽命到期</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019107 = 3019107 'Valve2 LifeTime Expired
    ''' <summary>閥2膠材計數到期</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019108 = 3019108 'Valve2 Life Count Expired
    ''' <summary>請更換閥2清膠材料</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019109 = 3019109 'Exchange Valve2 Material!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3019110 = 3019110 '
    ''' <summary>"閥3自動測高中,請稍後."</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019200 = 3019200 'Valve3 is Auto Detecting…Please Wait.
    ''' <summary>"閥3自動校正中,請稍後."</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019201 = 3019201 'Valve3 Auto Calibration Running…Please Wait.
    ''' <summary>閥3氣壓值設定失敗.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019202 = 3019202 'Set Valve3 AP Value: N.A.
    ''' <summary>閥3汽缸上抬逾時</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019203 = 3019203 'Valve3 Cylinder Up TimeOut.
    ''' <summary>閥3汽缸下降逾時</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019204 = 3019204 'Valve3 Cylinder Down TimeOut.
    ''' <summary>請更換閥3膠管</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019205 = 3019205 '"Exchange Valve3 Syringe, Please"
    ''' <summary>閥3膠量不足</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019206 = 3019206 'Valve3 Low level
    ''' <summary>閥3膠材壽命到期</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019207 = 3019207 'Valve3 LifeTime Expired
    ''' <summary>閥3膠材計數到期</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019208 = 3019208 'Valve3 Life Count Expired
    ''' <summary>請更換閥3清膠材料</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019209 = 3019209 'Exchange Valve3 Material!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3019210 = 3019210 '
    ''' <summary>"閥4自動測高中,請稍後."</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019300 = 3019300 'Valve4 is Auto Detecting…Please Wait.
    ''' <summary>"閥4自動校正中,請稍後."</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019301 = 3019301 'Valve4 Auto Calibration Running…Please Wait.
    ''' <summary>閥4氣壓值設定失敗.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019302 = 3019302 'Set Valve4 AP Value: N.A.
    ''' <summary>閥4汽缸上抬逾時</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019303 = 3019303 'Valve4 Cylinder Up TimeOut.
    ''' <summary>閥4汽缸下降逾時</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019304 = 3019304 'Valve4 Cylinder Down TimeOut.
    ''' <summary>請更換閥4膠管</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019305 = 3019305 '"Exchange Valve4 Syringe, Please"
    ''' <summary>閥4膠量不足</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019306 = 3019306 'Valve4 Low level
    ''' <summary>閥4膠材壽命到期</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019307 = 3019307 'Valve4 LifeTime Expired
    ''' <summary>閥4膠材計數到期</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019308 = 3019308 'Valve4 Life Count Expired
    ''' <summary>請更換閥4清膠材料</summary>
    ''' <remarks></remarks>
    Public Const Warn_3019309 = 3019309 'Exchange Valve4 Material!
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3019310 = 3019310 '
    ''' <summary>等待Conveyor阻擋缸下降.</summary>
    ''' <remarks></remarks>
    Public Const Warn_3024000 = 3024000 'Wait Conveyor Stopper Down.
    ''' <summary>沒有料片在Conveyor上</summary>
    ''' <remarks></remarks>
    Public Const Warn_3024001 = 3024001 'No Products On Conveyor.
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3024002 = 3024002 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3024003 = 3024003 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3024004 = 3024004 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3024005 = 3024005 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3024006 = 3024006 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3024007 = 3024007 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3024008 = 3024008 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3024009 = 3024009 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const Warn_3024010 = 3024010 '
    ''' <summary>請等待X軸到位訊號(INP)</summary>
    ''' <remarks></remarks>
    Public Const Warn_3030000 = 3030000 'Wait X-Axis Inposition(INP) Signal. Please Wait.
    ''' <summary>請等待Y1軸到位訊號(INP)</summary>
    ''' <remarks></remarks>
    Public Const Warn_3031000 = 3031000 'Wait Y1-Axis Inposition(INP) Signal. Please Wait.
    ''' <summary>請等待Z軸到位訊號(INP)</summary>
    ''' <remarks></remarks>
    Public Const Warn_3032000 = 3032000 'Wait Z-Axis Inposition(INP) Signal. Please Wait.
    ''' <summary>請等待Y2軸到位訊號(INP)</summary>
    ''' <remarks></remarks>
    Public Const Warn_3033000 = 3033000 'Wait Y2-Axis Inposition(INP) Signal. Please Wait.
    ''' <summary>請等待B軸到位訊號(INP)</summary>
    ''' <remarks></remarks>
    Public Const Warn_3034000 = 3034000 'Wait B-Axis Inposition(INP) Signal. Please Wait.
    ''' <summary>請等待C軸到位訊號(INP)</summary>
    ''' <remarks></remarks>
    Public Const Warn_3035000 = 3035000 'Wait C-Axis Inposition(INP) Signal. Please Wait.
    ''' <summary>請等待運動群組1到位訊號(INP)</summary>
    ''' <remarks></remarks>
    Public Const Warn_3036000 = 3036000 'Wait Group1 Inposition(INP) Signal. Please Wait.
    ''' <summary>請等待Conveyor1軸到位訊號(INP)</summary>
    ''' <remarks></remarks>
    Public Const Warn_3037000 = 3037000 'Wait Conveyor1-Axis Inposition(INP) Signal. Please Wait.
    ''' <summary>請等待Conveyor2軸到位訊號(INP)</summary>
    ''' <remarks></remarks>
    Public Const Warn_3038000 = 3038000 'Wait Conveyor2-Axis Inposition(INP) Signal. Please Wait.
    ''' <summary>請等待S1軸到位訊號(INP)</summary>
    ''' <remarks></remarks>
    Public Const Warn_3039000 = 3039000 'Wait S1-Axis Inposition(INP) Signal. Please Wait.
    ''' <summary>請等待S2軸到位訊號(INP)</summary>
    ''' <remarks></remarks>
    Public Const Warn_3040000 = 3040000 'Wait S2-Axis Inposition(INP) Signal. Please Wait.
    ''' <summary>請等待S3軸到位訊號(INP)</summary>
    ''' <remarks></remarks>
    Public Const Warn_3041000 = 3041000 'Wait S3-Axis Inposition(INP) Signal. Please Wait.
    ''' <summary>請等待U軸到位訊號(INP)</summary>
    ''' <remarks></remarks>
    Public Const Warn_3042000 = 3042000 'Wait U-Axis Inposition(INP) Signal. Please Wait.
    ''' <summary>請等待V1軸到位訊號(INP)</summary>
    ''' <remarks></remarks>
    Public Const Warn_3043000 = 3043000 'Wait V1-Axis Inposition(INP) Signal. Please Wait.
    ''' <summary>請等待W軸到位訊號(INP)</summary>
    ''' <remarks></remarks>
    Public Const Warn_3044000 = 3044000 'Wait W-Axis Inposition(INP) Signal. Please Wait.
    ''' <summary>請等待V2軸到位訊號(INP)</summary>
    ''' <remarks></remarks>
    Public Const Warn_3045000 = 3045000 'Wait V2-Axis Inposition(INP) Signal. Please Wait.
    ''' <summary>請等待B2軸到位訊號(INP)</summary>
    ''' <remarks></remarks>
    Public Const Warn_3046000 = 3046000 'Wait B2-Axis Inposition(INP) Signal. Please Wait.
    ''' <summary>請等待C2軸到位訊號(INP)</summary>
    ''' <remarks></remarks>
    Public Const Warn_3047000 = 3047000 'Wait C2-Axis Inposition(INP) Signal. Please Wait.
    ''' <summary>請等待運動群組2到位訊號(INP)</summary>
    ''' <remarks></remarks>
    Public Const Warn_3048000 = 3048000 'Wait Group2 Inposition(INP) Signal. Please Wait.
    ''' <summary>系統啟動</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000000 = 6000000 'System Start
    ''' <summary>系統關閉.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000001 = 6000001 'System End
    ''' <summary>機台型號:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000002 = 6000002 'Machine Type:{0}
    ''' <summary>軟體版本:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000003 = 6000003 'Software Version:{0}
    ''' <summary>觸發版本:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000004 = 6000004 'Trigger Version:{0}
    ''' <summary>IO卡初始化完成</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000005 = 6000005 'Initialize IO Card OK.
    ''' <summary>IO卡關卡成功</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000006 = 6000006 'Close IO Card OK.
    ''' <summary>AI卡初始化完成</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000007 = 6000007 'Initialize AI Card OK.
    ''' <summary>AI卡關卡成功</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000008 = 6000008 'Close AI Card OK.
    ''' <summary>AO卡初始化完成</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000009 = 6000009 'Initialize AO Card OK.
    ''' <summary>AO卡關卡成功</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000010 = 6000010 'Close AO Card OK.
    ''' <summary>DI卡初始化完成</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000011 = 6000011 'Initialize DI Card OK.
    ''' <summary>DI卡關卡成功</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000012 = 6000012 'Close DI Card OK.
    ''' <summary>DO卡初始化完成</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000013 = 6000013 'Initialize DO Card OK.
    ''' <summary>DO卡關卡成功</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000014 = 6000014 'Close DO Card OK.
    ''' <summary>運動控制卡初始化完成</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000015 = 6000015 'Initialize Motion Card OK.
    ''' <summary>運動控制卡關卡成功</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000016 = 6000016 'Close Motion Card OK.
    ''' <summary>COM通訊埠開啟成功</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000017 = 6000017 'Open COM Port OK.
    ''' <summary>COM通訊埠關閉成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000018 = 6000018 'Close COM Port OK.
    ''' <summary>網路連線成功</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000019 = 6000019 'EtherNET Connection OK.
    ''' <summary>網路離線成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000020 = 6000020 'Ethernet Diconnect OK.
    ''' <summary>影像擷取卡1初始化成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000021 = 6000021 'Initialize Image Card1 OK.
    ''' <summary>影像擷取卡1關閉成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000022 = 6000022 'Close Image Card1 OK.
    ''' <summary>雷射干涉儀1初始化成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000023 = 6000023 'Initialize Laser Interfeormeter1 OK.
    ''' <summary>雷射干涉儀1關閉成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000024 = 6000024 'Close Laser Interferometer1 OK.
    ''' <summary>雷射干涉儀2初始化成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000025 = 6000025 'Initialize Laser Interfeormeter2 OK.
    ''' <summary>雷射干涉儀2關閉成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000026 = 6000026 'Close Laser Interferometer2 OK.
    ''' <summary>雷射干涉儀3初始化成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000027 = 6000027 'Initialize Laser Interfeormeter3 OK.
    ''' <summary>雷射干涉儀3關閉成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000028 = 6000028 'Close Laser Interferometer3 OK.
    ''' <summary>雷射干涉儀4初始化成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000029 = 6000029 'Initialize Laser Interfeormeter4 OK.
    ''' <summary>雷射干涉儀4關閉成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000030 = 6000030 'Close Laser Interferometer4 OK.
    ''' <summary>微量天平1初始化成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000031 = 6000031 'Initialize Scale1 OK.
    ''' <summary>微量天平1關閉成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000032 = 6000032 'Close Scale1 OK.
    ''' <summary>微量天平2初始化成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000033 = 6000033 'Initialize Scale2 OK.
    ''' <summary>微量天平2關閉成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000034 = 6000034 'Close Scale2 OK.
    ''' <summary>FMCS1初始化成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000035 = 6000035 'Initialize FMCS1 OK.
    ''' <summary>FMCS1關閉成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000036 = 6000036 'Close FMCS1 OK.
    ''' <summary>FMCS2初始化成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000037 = 6000037 'Initialize FMCS2 OK.
    ''' <summary>FMCS2關閉成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000038 = 6000038 'Close FMCS2 OK.
    ''' <summary>FMCS3初始化成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000039 = 6000039 'Initialize FMCS3 OK.
    ''' <summary>FMCS3關閉成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000040 = 6000040 'Close FMCS3 OK.
    ''' <summary>FMCS4初始化成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000041 = 6000041 'Initialize FMCS4 OK.
    ''' <summary>FMCS4關閉成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000042 = 6000042 'Close FMCS4 OK.
    ''' <summary>使用者登入成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000043 = 6000043 'User Login OK.
    ''' <summary>使用者登出成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000044 = 6000044 'User LogOut OK.
    ''' <summary>CCD類型:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000045 = 6000045 'CCD Type:{0}
    ''' <summary>CCD1初始化完成.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000046 = 6000046 'Initialize CCD1 OK.
    ''' <summary>CCD1關閉成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000047 = 6000047 'Close CCD1 OK.
    ''' <summary>CCD2初始化完成.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000048 = 6000048 'Initialize CCD2 OK.
    ''' <summary>CCD2關閉成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000049 = 6000049 'Close CCD2 OK.
    ''' <summary>CCD3初始化完成.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000050 = 6000050 'Initialize CCD3 OK.
    ''' <summary>CCD3關閉成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000051 = 6000051 'Close CCD3 OK.
    ''' <summary>CCD4初始化完成.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000052 = 6000052 'Initialize CCD4 OK.
    ''' <summary>CCD4關閉成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000053 = 6000053 'Close CCD4 OK.
    ''' <summary>儲存噴射閥Z高 閥1:{0} 測高感測器:{1}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000054 = 6000054 'Save Jet Zpos Valve1: {0} Laser(Height Sensor): {1}
    ''' <summary>介面設定噴射閥Z高 閥1:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000055 = 6000055 'UI Jet Zpos Valve1:{0}
    ''' <summary>介面設定噴射閥Z高 測高感測器:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000056 = 6000056 'UI Jet Height Sensor Zpos:{0}
    ''' <summary>儲存噴射閥Z高 閥2:{0} 測高感測器:{1}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000057 = 6000057 'Save Jet Zpos Valve2: {0} Laser(Height Sensor): {1}
    ''' <summary>介面設定噴射閥Z高 閥2:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000058 = 6000058 'UI Jet Zpos Valve2:{0}
    ''' <summary>介面設定噴射閥Z高 測高感測器:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6000059 = 6000059 'UI Jet Height Sensor Zpos:{0}
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6000060 = 6000060 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6000061 = 6000061 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6000062 = 6000062 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6000063 = 6000063 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6000064 = 6000064 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6000065 = 6000065 '
    ''' <summary>傳送帶除能.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001000 = 6001000 'Disable Conveyor.
    ''' <summary>傳送帶致能.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001001 = 6001001 'Enable Conveyor.
    ''' <summary>CCD除能.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001002 = 6001002 'Disable CCD.
    ''' <summary>CCD致能.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001003 = 6001003 'Enable CCD.
    ''' <summary>膠量檢測1除能.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001004 = 6001004 'Disable Glue Detector1
    ''' <summary>膠量檢測1致能.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001005 = 6001005 'Enable Glue Detector1
    ''' <summary>膠量檢測2除能.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001006 = 6001006 'Disable Glue Detector2
    ''' <summary>膠量檢測2致能.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001007 = 6001007 'Enable Glue Detector2
    ''' <summary>真空On</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001008 = 6001008 'Pump On
    ''' <summary>真空Off</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001009 = 6001009 'Pump Off
    ''' <summary>設定閥1膠管氣壓值:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001010 = 6001010 'Set Valve1 AP Value: {0}
    ''' <summary>設定閥2膠管氣壓值:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001011 = 6001011 'Set Valve2 AP Value: {0}
    ''' <summary>設定閥3膠管氣壓值:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001012 = 6001012 'Set Valve3 AP Value: {0}
    ''' <summary>設定閥4膠管氣壓值:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001013 = 6001013 'Set Valve4 AP Value: {0}
    ''' <summary>閥1膠管氣壓啟用</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001014 = 6001014 'Valve1 Syringe Pressure On
    ''' <summary>閥1膠管氣壓關閉</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001015 = 6001015 'Valve1 Syringe Pressure Off
    ''' <summary>閥1順時針旋轉啟動</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001016 = 6001016 'Valve1 CW On
    ''' <summary>閥1順時針旋轉關閉</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001017 = 6001017 'Valve1 CW Off
    ''' <summary>閥1逆時針旋轉啟動</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001018 = 6001018 'Valve1 CCW On
    ''' <summary>閥1逆時針旋轉關閉</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001019 = 6001019 'Valve1 CCW Off
    ''' <summary>閥1汽缸上升</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001020 = 6001020 'Valve1 Cylinder Up
    ''' <summary>閥1汽缸下降</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001021 = 6001021 'Valve1 Cylinder Down
    ''' <summary>閥2膠管氣壓啟用</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001022 = 6001022 'Valve2 Syringe Pressure On
    ''' <summary>閥2膠管氣壓關閉</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001023 = 6001023 'Valve2 Syringe Pressure Off
    ''' <summary>閥2順時針旋轉啟動</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001024 = 6001024 'Valve2 CW On
    ''' <summary>閥2順時針旋轉關閉</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001025 = 6001025 'Valve2 CW Off
    ''' <summary>閥2逆時針旋轉啟動</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001026 = 6001026 'Valve2 CCW On
    ''' <summary>閥2逆時針旋轉關閉</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001027 = 6001027 'Valve2 CCW Off
    ''' <summary>閥2汽缸上升</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001028 = 6001028 'Valve2 Cylinder Up
    ''' <summary>閥2汽缸下降</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001029 = 6001029 'Valve2 Cylinder Down
    ''' <summary>擦膠閥夾爪閉合</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001030 = 6001030 'Clear Valve Clamp On
    ''' <summary>擦膠閥夾爪張開</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001031 = 6001031 'Clear Valve Clamp Off
    ''' <summary>擦膠馬達啟動</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001032 = 6001032 'Clear Valve Motor On
    ''' <summary>擦膠馬達停止</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001033 = 6001033 'Clear Valve Motor Off
    ''' <summary>擦膠馬達順時針旋轉啟動</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001034 = 6001034 'Clear Valve Motor CW On
    ''' <summary>擦膠馬達順時針旋轉停止</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001035 = 6001035 'Clear Valve Motor CW Off
    ''' <summary>擦膠馬達逆時針旋轉啟動</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001036 = 6001036 'Clear Valve Motor CCW On
    ''' <summary>擦膠馬達逆時針旋轉停止</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001037 = 6001037 'Clear Valve Motor CCW Off
    ''' <summary>絕對移動目標位置({0})</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001038 = 6001038 'AbsMove To Pos({0})
    ''' <summary>"絕對移動目標位置({0},{1})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001039 = 6001039 '"AbsMove To Pos({0},{1})"
    ''' <summary>"絕對移動目標位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001040 = 6001040 '"AbsMove To Pos({0},{1},{2})"
    ''' <summary>"絕對移動目標位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001041 = 6001041 '"AbsMove To Pos({0},{1},{2},{3})"
    ''' <summary>"絕對移動目標位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001042 = 6001042 '"AbsMove To Pos({0},{1},{2},{3},{4})"
    ''' <summary>"移動到第一定位點位置({0},{1})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001043 = 6001043 '"Goto Alignment Pos1({0},{1})"
    ''' <summary>"移動到第一定位點位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001044 = 6001044 '"Goto Alignment Pos1({0},{1},{2})"
    ''' <summary>"移動到第一定位點位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001045 = 6001045 '"Goto Alignment Pos1({0},{1},{2},{3})"
    ''' <summary>"移動到第一定位點位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001046 = 6001046 '"Goto Alignment Pos1({0},{1},{2},{3},{4})"
    ''' <summary>"移動到第二定位點位置({0},{1})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001047 = 6001047 '"Goto Alignment Pos2({0},{1})"
    ''' <summary>"移動到第二定位點位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001048 = 6001048 '"Goto Alignment Pos2({0},{1},{2})"
    ''' <summary>"移動到第二定位點位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001049 = 6001049 '"Goto Alignment Pos2({0},{1},{2},{3})"
    ''' <summary>"移動到第二定位點位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001050 = 6001050 '"Goto Alignment Pos2({0},{1},{2},{3},{4})"
    ''' <summary>"移動到CCD1位置({0},{1})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001051 = 6001051 '"Goto CCD1 Pos({0},{1})"
    ''' <summary>"移動到CCD1位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001052 = 6001052 '"Goto CCD1 Pos({0},{1},{2})"
    ''' <summary>"移動到CCD1位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001053 = 6001053 '"Goto CCD1 Pos({0},{1},{2},{3})"
    ''' <summary>"移動到CCD1位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001054 = 6001054 '"Goto CCD1 Pos({0},{1},{2},{3},{4})"
    ''' <summary>"移動到CCD2位置({0},{1})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001055 = 6001055 '"Goto CCD2 Pos({0},{1})"
    ''' <summary>"移動到CCD2位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001056 = 6001056 '"Goto CCD2 Pos({0},{1},{2})"
    ''' <summary>"移動到CCD2位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001057 = 6001057 '"Goto CCD2 Pos({0},{1},{2},{3})"
    ''' <summary>"移動到CCD2位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001058 = 6001058 '"Goto CCD2 Pos({0},{1},{2},{3},{4})"
    ''' <summary>"移動到CCD3位置({0},{1})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001059 = 6001059 '"Goto CCD3 Pos({0},{1})"
    ''' <summary>"移動到CCD3位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001060 = 6001060 '"Goto CCD3 Pos({0},{1},{2})"
    ''' <summary>"移動到CCD3位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001061 = 6001061 '"Goto CCD3 Pos({0},{1},{2},{3})"
    ''' <summary>"移動到CCD3位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001062 = 6001062 '"Goto CCD3 Pos({0},{1},{2},{3},{4})"
    ''' <summary>"移動到CCD4位置({0},{1})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001063 = 6001063 '"Goto CCD4 Pos({0},{1})"
    ''' <summary>"移動到CCD4位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001064 = 6001064 '"Goto CCD4 Pos({0},{1},{2})"
    ''' <summary>"移動到CCD4位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001065 = 6001065 '"Goto CCD4 Pos({0},{1},{2},{3})"
    ''' <summary>"移動到CCD4位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001066 = 6001066 '"Goto CCD4 Pos({0},{1},{2},{3},{4})"
    ''' <summary>馬達激磁.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001067 = 6001067 'Servo On.
    ''' <summary>馬達消磁.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001068 = 6001068 'Servo Off.
    ''' <summary>"移動到Height Sensor位置({0},{1})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001069 = 6001069 '"Goto Height Sensor Pos({0},{1})"
    ''' <summary>"移動到Height Sensor位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001070 = 6001070 '"Goto Height Sensor Pos({0},{1},{2})"
    ''' <summary>"移動到Height Sensor位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001071 = 6001071 '"Goto Height Sensor Pos({0},{1},{2},{3})"
    ''' <summary>"移動到Height Sensor位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001072 = 6001072 '"Goto Height Sensor Pos({0},{1},{2},{3},{4})"
    ''' <summary>蜂鳴器使用.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001073 = 6001073 'Buzzer Use.
    ''' <summary>蜂鳴器靜音.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001074 = 6001074 'Buzzer Mute.
    ''' <summary>閥加熱中</summary>
    ''' <remarks></remarks>
    Public Const INFO_6001075 = 6001075 'Valve Heating.
    ''' <summary>軸索引讀取成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002000 = 6002000 'Axis Index Load OK.
    ''' <summary>軸索引儲存成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002001 = 6002001 'Axis Index Save OK.
    ''' <summary>系統參數讀取成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002002 = 6002002 'System Parameter Load OK.
    ''' <summary>系統參數儲存成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002003 = 6002003 'System Parameter Save OK.
    ''' <summary>膠材參數讀取成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002004 = 6002004 'Paste Parameter Load OK
    ''' <summary>膠材參數儲存成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002005 = 6002005 'Paste Parameter Save OK
    ''' <summary>膠閥參數讀取成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002006 = 6002006 'Valve Parameter Load OK.
    ''' <summary>膠閥參數儲存成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002007 = 6002007 'Valve Parameter Save OK.
    ''' <summary>IO卡參數讀取成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002008 = 6002008 'IO Card Load OK.
    ''' <summary>IO卡參數儲存成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002009 = 6002009 'IO Card Save OK.
    ''' <summary>運動控制卡參數讀取成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002010 = 6002010 'Motion Card Load OK.
    ''' <summary>運動控制卡參數儲存成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002011 = 6002011 'Motion Card Save OK.
    ''' <summary>AI卡參數讀取成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002012 = 6002012 'AI Card Load OK.
    ''' <summary>AI卡參數儲存成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002013 = 6002013 'AI Card Save OK.
    ''' <summary>AO參數讀取成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002014 = 6002014 'AO Card Load OK.
    ''' <summary>AO參數儲存成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002015 = 6002015 'AO Card Save OK.
    ''' <summary>DI卡參數讀取成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002016 = 6002016 'DI Card Load OK.
    ''' <summary>DI卡參數儲存成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002017 = 6002017 'DI Card Save OK.
    ''' <summary>DO參數讀取成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002018 = 6002018 'DO Card Load OK.
    ''' <summary>DO參數儲存成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002019 = 6002019 'DO Card Save OK.
    ''' <summary>Recipe讀檔成功!</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002020 = 6002020 'Recipe Load OK.
    ''' <summary>Recipe存檔成功!</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002021 = 6002021 'Recipe Save OK.
    ''' <summary>單步參數讀取成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002022 = 6002022 'Step Parameter Load OK.
    ''' <summary>單步參數儲存成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002023 = 6002023 'Step Parameter Save OK.
    ''' <summary>"膠材參數已存在,是否覆蓋?"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002024 = 6002024 '"Paste Parameter already exists, overwrite?"
    ''' <summary>"噴射閥參數已存在,是否覆蓋?"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002025 = 6002025 '"Jet Valve Parameter already exists, overwrite?"
    ''' <summary>"螺桿閥參數已存在,是否覆蓋?"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002026 = 6002026 '"Auger Valve Parameter already exists, overwrite?"
    ''' <summary>"氣壓閥參數已存在,是否覆蓋?"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002027 = 6002027 '"Air Valve Parameter already exists, overwrite?"
    ''' <summary>Recipe鎖定.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002028 = 6002028 'Recipe Locked.
    ''' <summary>Recipe解除鎖定.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002029 = 6002029 'Recipe Unlocked.
    ''' <summary>原自動清膠頻率:{0}真空除膠頻率:{1}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002030 = 6002030 'Old Auto Clear Glue:{0} Auto Purge:{1}
    ''' <summary>新自動清膠頻率:{0}真空除膠頻率:{1}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002031 = 6002031 'New Auto Clear Glue:{0} Auto Purge:{1}
    ''' <summary>原Purge時出膠:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002032 = 6002032 'Old Purge Jet On:{0}
    ''' <summary>新Purge時出膠:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002033 = 6002033 'New Purge Jet On:{0}
    ''' <summary>原觸發路徑延伸長度:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002034 = 6002034 'Old Trigger Over Path:{0}
    ''' <summary>新觸發路徑延伸長度:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002035 = 6002035 'New Trigger Over Path:{0}
    ''' <summary>原平行線觸發模式:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002036 = 6002036 'Old Trigger Parallel Mode:{0}
    ''' <summary>新平行線觸發模式:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002037 = 6002037 'New Trigger Parallel Mode:{0}
    ''' <summary>原觸發補償模式:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002038 = 6002038 'Old Trigger Compensation Mode:{0}
    ''' <summary>新觸發補償模式:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002039 = 6002039 'New Trigger Compensation Mode:{0}
    ''' <summary>原膠量補償模式:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002040 = 6002040 'Old Glue Compensation Mode:
    ''' <summary>新膠量補償模式:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002041 = 6002041 'New Glue Compensation Mode:
    ''' <summary>原系統參數如下所示:</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002042 = 6002042 'Old System Parameter Show as below:
    ''' <summary>新系統參數如下所示:</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002043 = 6002043 'New System Parameter Show as below:
    ''' <summary>原馬達參數如下所示:</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002044 = 6002044 'Old Motor Parameter Show as below:
    ''' <summary>新馬達參數如下所示:</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002045 = 6002045 'New Motor Parameter Show as below:
    ''' <summary>原使用者權限設定如下所示:</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002046 = 6002046 'Old User Authority is Shown as below:
    ''' <summary>新使用者權限設定如下所示:</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002047 = 6002047 'New User Authority is Shown as below:
    ''' <summary>原螺桿閥過電流判定 CT1:{0} CT2:{1}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002048 = 6002048 'Old Auger CT1: {0} CT2: {1}
    ''' <summary>新螺桿閥過電流判定 CT1:{0} CT2:{1}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002049 = 6002049 'New Auger CT1: {0} CT2: {1}
    ''' <summary>原產能自動穩定:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002050 = 6002050 'Old Auto Cycle Tuning:{0}
    ''' <summary>新產能自動穩定:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002051 = 6002051 'New Auto Cycle Tuning:{0}
    ''' <summary>原校正重試次數:{0} 容許誤差:{1}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002052 = 6002052 'Old Calibration Retry Limit:{0} Accept Toleranec:{1}
    ''' <summary>新校正重試次數:{0} 容許誤差:{1}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002053 = 6002053 'New Calibration Retry Limit:{0} Accept Toleranec:{1}
    ''' <summary>"儲存閥1測高位置({0},{1},{2}) Z極限:{3}"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002066 = 6002066 '"Save Valve1 Pin Pos({0},{1},{2}) Z Limit: {3}"
    ''' <summary>"儲存閥1測高位置({0},{1},{2},{3}) Z極限:{3}"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002067 = 6002067 '"Save Valve1 Pin Pos({0},{1},{2},{3}) Z Limit: {3}"
    ''' <summary>"儲存閥1測高位置({0},{1},{2},{3},{4}) Z極限:{3}"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002068 = 6002068 '"Save Valve1 Pin Pos({0},{1},{2},{3},{4}) Z Limit: {3}"
    ''' <summary>"儲存閥2測高位置({0},{1},{2}) Z極限:{3}"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002069 = 6002069 '"Save Valve2 Pin Pos({0},{1},{2}) Z Limit: {3}"
    ''' <summary>"儲存閥2測高位置({0},{1},{2},{3}) Z極限:{3}"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002070 = 6002070 '"Save Valve2 Pin Pos({0},{1},{2},{3}) Z Limit: {3}"
    ''' <summary>"儲存閥2測高位置({0},{1},{2},{3},{4}) Z極限:{3}"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002071 = 6002071 '"Save Valve2 Pin Pos({0},{1},{2},{3},{4}) Z Limit: {3}"
    ''' <summary>原擦膠參數間距:{0}mm次數限制:{1} 偏移量{2}mm氣壓開啟時間{3}ms</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002072 = 6002072 'Old Clear Glue Parameter Pitch:{0}mm CountLimit:{1} Offset:{2}mmAP On Time:{3}ms
    ''' <summary>新擦膠參數間距:{0}mm次數限制:{1} 偏移量{2}mm氣壓開啟時間{3}ms</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002073 = 6002073 'New Clear Glue Parameter Pitch:{0}mm CountLimit:{1} Offset:{2}mmAP On Time:{3}ms
    ''' <summary>原閥1形式: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002074 = 6002074 'Old Valve1 Type: {0}
    ''' <summary>新閥1形式: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002075 = 6002075 'New Valve1 Type: {0}
    ''' <summary>原閥2形式: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002076 = 6002076 'Old Valve2 Type: {0}
    ''' <summary>新閥2形式: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002077 = 6002077 'New Valve2 Type: {0}
    ''' <summary>原閥3形式: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002078 = 6002078 'Old Valve3 Type: {0}
    ''' <summary>新閥3形式: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002079 = 6002079 'New Valve3 Type: {0}
    ''' <summary>原閥4形式: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002080 = 6002080 'Old Valve4 Type: {0}
    ''' <summary>新閥4形式: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6002081 = 6002081 'New Valve4 Type: {0}
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6002082 = 6002082 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6002083 = 6002083 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6002084 = 6002084 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6002085 = 6002085 '
    ''' <summary>DIO卡1初始化成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6003000 = 6003000 'Initialize DIO Card1 OK.
    ''' <summary>DIO卡1關卡成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6003001 = 6003001 'Close DIO Card1 OK.
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6003002 = 6003002 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6003003 = 6003003 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6003004 = 6003004 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6003005 = 6003005 '
    ''' <summary>DI卡1初始化成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6004000 = 6004000 'Initialize DI Card1 OK.
    ''' <summary>DI卡1關卡成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6004001 = 6004001 'Close DI Card1 OK.
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6004002 = 6004002 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6004003 = 6004003 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6004004 = 6004004 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6004005 = 6004005 '
    ''' <summary>DO卡1初始化成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6005000 = 6005000 'Initialize DO Card1 OK.
    ''' <summary>DO卡1關卡成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6005001 = 6005001 'Close DO Card1 OK.
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6005002 = 6005002 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6005003 = 6005003 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6005004 = 6005004 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6005005 = 6005005 '
    ''' <summary>AIO卡1初始化成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6006000 = 6006000 'Initialize AIO Card1 OK.
    ''' <summary>AIO卡1關卡成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6006001 = 6006001 'Close AIO Card1 OK.
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6006002 = 6006002 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6006003 = 6006003 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6006004 = 6006004 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6006005 = 6006005 '
    ''' <summary>AI卡1初始化成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6007000 = 6007000 'Initialize AI Card1 OK.
    ''' <summary>AI卡1關卡成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6007001 = 6007001 'Close AI Card1 OK.
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6007002 = 6007002 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6007003 = 6007003 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6007004 = 6007004 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6007005 = 6007005 '
    ''' <summary>AO卡1初始化成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6008000 = 6008000 'Initialize AO Card1 OK.
    ''' <summary>AO卡1關卡成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6008001 = 6008001 'Close AO Card1 OK.
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6008002 = 6008002 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6008003 = 6008003 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6008004 = 6008004 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6008005 = 6008005 '
    ''' <summary>運動控制卡1初始化成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6009000 = 6009000 'Initialize Motion Card1 OK.
    ''' <summary>運動控制卡1關卡成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6009001 = 6009001 'Close Motion Card1 OK.
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6009002 = 6009002 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6009003 = 6009003 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6009004 = 6009004 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6009005 = 6009005 '
    ''' <summary>COM元件初始化成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6010000 = 6010000 'Initialize COM Port OK.
    ''' <summary>COM元件關閉成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6010001 = 6010001 'Close COM Port OK.
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6010002 = 6010002 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6010003 = 6010003 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6010004 = 6010004 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6010005 = 6010005 '
    ''' <summary>網路元件初始化成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6011000 = 6011000 'Initialize Ethernet Port OK.
    ''' <summary>網路元件關閉成功.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6011001 = 6011001 'Close Ethernet Port OK.
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6011002 = 6011002 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6011003 = 6011003 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6011004 = 6011004 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6011005 = 6011005 '
    ''' <summary>CCD1觸發</summary>
    ''' <remarks></remarks>
    Public Const INFO_6012000 = 6012000 'Set CCD1 Trigger
    ''' <summary>原CCD1自動校正取向高度:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6012001 = 6012001 'Old Auto Calibration CCD1 Acquisition Pos Z:{0}
    ''' <summary>新CCD1自動校正取向高度:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6012002 = 6012002 'New Auto Calibration CCD1 Acquisition Pos Z:{0}
    ''' <summary>CCD1轉換比例. A11:{0} A12:{1} A21:{2} A22:{3} B11:{4} B21:{5}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6012003 = 6012003 'CCD1 Scale Transorm. A11:{0} A12:{1} A21:{2} A22:{3} B11:{4} B21:{5}
    ''' <summary>"影像偏移量({0},{1})Pixel"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6012004 = 6012004 '"Image Offset({0},{1}) Pixel"
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6012005 = 6012005 '
    ''' <summary>CCD2觸發</summary>
    ''' <remarks></remarks>
    Public Const INFO_6012100 = 6012100 'Set CCD2 Trigger
    ''' <summary>原CCD2自動校正取向高度:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6012101 = 6012101 'Old Auto Calibration CCD2 Acquisition Pos Z:{0}
    ''' <summary>新CCD2自動校正取向高度:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6012102 = 6012102 'New Auto Calibration CCD2 Acquisition Pos Z:{0}
    ''' <summary>CCD2轉換比例. A11:{0} A12:{1} A21:{2} A22:{3} B11:{4} B21:{5}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6012103 = 6012103 'CCD2 Scale Transorm. A11:{0} A12:{1} A21:{2} A22:{3} B11:{4} B21:{5}
    ''' <summary>"影像偏移量({0},{1})Pixel"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6012104 = 6012104 '"Image Offset({0},{1}) Pixel"
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6012105 = 6012105 '
    ''' <summary>CCD3觸發</summary>
    ''' <remarks></remarks>
    Public Const INFO_6012200 = 6012200 'Set CCD3 Trigger
    ''' <summary>原CCD3自動校正取向高度:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6012201 = 6012201 'Old Auto Calibration CCD3 Acquisition Pos Z:{0}
    ''' <summary>新CCD3自動校正取向高度:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6012202 = 6012202 'New Auto Calibration CCD3 Acquisition Pos Z:{0}
    ''' <summary>CCD3轉換比例. A11:{0} A12:{1} A21:{2} A22:{3} B11:{4} B21:{5}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6012203 = 6012203 'CCD3 Scale Transorm. A11:{0} A12:{1} A21:{2} A22:{3} B11:{4} B21:{5}
    ''' <summary>"影像偏移量({0},{1})Pixel"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6012204 = 6012204 '"Image Offset({0},{1}) Pixel"
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6012205 = 6012205 '
    ''' <summary>CCD4觸發</summary>
    ''' <remarks></remarks>
    Public Const INFO_6012300 = 6012300 'Set CCD4 Trigger
    ''' <summary>原CCD4自動校正取向高度:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6012301 = 6012301 'Old Auto Calibration CCD4 Acquisition Pos Z:{0}
    ''' <summary>新CCD4自動校正取向高度:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6012302 = 6012302 'New Auto Calibration CCD4 Acquisition Pos Z:{0}
    ''' <summary>CCD4轉換比例. A11:{0} A12:{1} A21:{2} A22:{3} B11:{4} B21:{5}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6012303 = 6012303 'CCD4 Scale Transorm. A11:{0} A12:{1} A21:{2} A22:{3} B11:{4} B21:{5}
    ''' <summary>"影像偏移量({0},{1})Pixel"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6012304 = 6012304 '"Image Offset({0},{1}) Pixel"
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6012305 = 6012305 '
    ''' <summary>原雷射模型監控:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6014000 = 6014000 'Old Monitor Laser Model: {0}
    ''' <summary>新雷射模型監控:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6014001 = 6014001 'New Monitor Laser Model: {0}
    ''' <summary>"原雷射測高模組1偏移量({0},{1})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6014002 = 6014002 '"Old Laser Interferometer1 Offset({0},{1})"
    ''' <summary>"新雷射測高模組1偏移量({0},{1})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6014003 = 6014003 '"New Laser Interferometer1 Offset({0},{1})"
    ''' <summary>"原測高參數 重測次數:{0},容許誤差:{1}"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6014004 = 6014004 '"Old Parameter Count Limit:{0}, Accept Tolerance:{1}"
    ''' <summary>"新測高參數 重測次數:{0},容許誤差:{1}"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6014005 = 6014005 '"New Parameter Count Limit:{0}, Accept Tolerance:{1}"
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6014006 = 6014006 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6014007 = 6014007 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6014008 = 6014008 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6014009 = 6014009 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6014010 = 6014010 '
    ''' <summary>微量天平1初始化完成.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6015000 = 6015000 'Initialize Scale1 OK.
    ''' <summary>微量天平1關閉完成.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6015001 = 6015001 'Close Scale1 OK.
    ''' <summary>微量天平2初始化完成.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6015002 = 6015002 'Initialize Scale2 OK.
    ''' <summary>微量天平2關閉完成.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6015003 = 6015003 'Close Scale2 OK.
    ''' <summary>原天秤1參數 X1:{0} X2:{1} Y1:{2} Y2:{3} StableTime:{4}Sec Mode:{5}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6015004 = 6015004 'Old Scale1 Parameter X1:{0} X2:{1} Y1:{2} Y2:{3} StableTime:{4}Sec Mode:{5}
    ''' <summary>新天秤1參數 X1:{0} X2:{1} Y1:{2} Y2:{3} StableTime:{4}Sec Mode:{5}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6015005 = 6015005 'New Scale1 Parameter X1:{0} X2:{1} Y1:{2} Y2:{3} StableTime:{4}Sec Mode:{5}
    ''' <summary>原天秤2參數 X1:{0} X2:{1} Y1:{2} Y2:{3} StableTime:{4}Sec Mode:{5}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6015006 = 6015006 'Old Scale2 Parameter X1:{0} X2:{1} Y1:{2} Y2:{3} StableTime:{4}Sec Mode:{5}
    ''' <summary>新天秤2參數 X1:{0} X2:{1} Y1:{2} Y2:{3} StableTime:{4}Sec Mode:{5}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6015007 = 6015007 'New Scale2 Parameter X1:{0} X2:{1} Y1:{2} Y2:{3} StableTime:{4}Sec Mode:{5}
    ''' <summary>天平1重量對氣壓控制: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6015008 = 6015008 'Scale1 Weight-AP {0}
    ''' <summary>天平1重量對點數控制: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6015009 = 6015009 'Scale1 Weight-Pts {0}
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6015010 = 6015010 '
    ''' <summary>FMCS1初始化完成!</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017000 = 6017000 'Initialize FMCS1 OK!
    ''' <summary>FMCS1開始記錄.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017001 = 6017001 'FMCS1 Record Start.
    ''' <summary>FMCS1結束記錄.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017002 = 6017002 'FMCS1 Record End.
    ''' <summary>FMCS1取得資料</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017003 = 6017003 'FMCS1 Get Data
    ''' <summary>FMCS1流量對氣壓控制: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017004 = 6017004 'FMCS1 Flow-AP: {0}
    ''' <summary>FMCS1流量對點數控制: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017005 = 6017005 'FMCS1 Flow-Pts: {0}
    ''' <summary>FMCS1體積對氣壓控制: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017006 = 6017006 'FMCS1 Volume-AP: {0}
    ''' <summary>FMCS1體積對點數控制: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017007 = 6017007 'FMCS1 Volume-Pts: {0}
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6017008 = 6017008 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6017009 = 6017009 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6017010 = 6017010 '
    ''' <summary>FMCS2初始化完成!</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017100 = 6017100 'Initialize FMCS2 OK!
    ''' <summary>FMCS2開始記錄.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017101 = 6017101 'FMCS2 Record Start.
    ''' <summary>FMCS2結束記錄.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017102 = 6017102 'FMCS2 Record End.
    ''' <summary>FMCS2取得資料</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017103 = 6017103 'FMCS2 Get Data
    ''' <summary>FMCS2流量對氣壓控制: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017104 = 6017104 'FMCS2 Flow-AP: {0}
    ''' <summary>FMCS2流量對點數控制: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017105 = 6017105 'FMCS2 Flow-Pts: {0}
    ''' <summary>FMCS2體積對氣壓控制: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017106 = 6017106 'FMCS2 Volume-AP: {0}
    ''' <summary>FMCS2體積對點數控制: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017107 = 6017107 'FMCS2 Volume-Pts: {0}
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6017108 = 6017108 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6017109 = 6017109 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6017110 = 6017110 '
    ''' <summary>FMCS3初始化完成!</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017200 = 6017200 'Initialize FMCS3 OK!
    ''' <summary>FMCS3開始記錄.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017201 = 6017201 'FMCS3 Record Start.
    ''' <summary>FMCS3結束記錄.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017202 = 6017202 'FMCS3 Record End.
    ''' <summary>FMCS3取得資料</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017203 = 6017203 'FMCS3 Get Data
    ''' <summary>FMCS3流量對氣壓控制: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017204 = 6017204 'FMCS3 Flow-AP: {0}
    ''' <summary>FMCS3流量對點數控制: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017205 = 6017205 'FMCS3 Flow-Pts: {0}
    ''' <summary>FMCS3體積對氣壓控制: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017206 = 6017206 'FMCS3 Volume-AP: {0}
    ''' <summary>FMCS3體積對點數控制: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017207 = 6017207 'FMCS3 Volume-Pts: {0}
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6017208 = 6017208 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6017209 = 6017209 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6017210 = 6017210 '
    ''' <summary>FMCS4初始化完成!</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017300 = 6017300 'Initialize FMCS4 OK!
    ''' <summary>FMCS4開始記錄.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017301 = 6017301 'FMCS4 Record Start.
    ''' <summary>FMCS4結束記錄.</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017302 = 6017302 'FMCS4 Record End.
    ''' <summary>FMCS4取得資料</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017303 = 6017303 'FMCS4 Get Data
    ''' <summary>FMCS4流量對氣壓控制: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017304 = 6017304 'FMCS4 Flow-AP: {0}
    ''' <summary>FMCS4流量對點數控制: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017305 = 6017305 'FMCS4 Flow-Pts: {0}
    ''' <summary>FMCS4體積對氣壓控制: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017306 = 6017306 'FMCS4 Volume-AP: {0}
    ''' <summary>FMCS4體積對點數控制: {0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6017307 = 6017307 'FMCS4 Volume-Pts: {0}
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6017308 = 6017308 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6017309 = 6017309 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6017310 = 6017310 '
    ''' <summary>原閥1加熱模式:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6018000 = 6018000 'Old Valve1 Heat Mode:{0}
    ''' <summary>新閥1加熱模式:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6018001 = 6018001 'New Valve1 Heat Mode:{0}
    ''' <summary>原閥2加熱模式:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6018002 = 6018002 'Old Valve2 Heat Mode:{0}
    ''' <summary>新閥2加熱模式:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6018003 = 6018003 'New Valve2 Heat Mode:{0}
    ''' <summary>原閥3加熱模式:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6018004 = 6018004 'Old Valve3 Heat Mode:{0}
    ''' <summary>新閥3加熱模式:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6018005 = 6018005 'New Valve3 Heat Mode:{0}
    ''' <summary>原閥4加熱模式:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6018006 = 6018006 'Old Valve4 Heat Mode:{0}
    ''' <summary>新閥4加熱模式:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6018007 = 6018007 'New Valve4 Heat Mode:{0}
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6018008 = 6018008 '
    ''' <summary>原閥1Purge時間:{0}ms Cycle Time:{1}ms</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019000 = 6019000 'Old Valve1 Purge Time:{0}ms Cycle Time:{1}ms
    ''' <summary>新閥1Purge時間:{0}ms Cycle Time:{1}ms</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019001 = 6019001 'New Valve1 Purge Time:{0}ms Cycle Time:{1}ms
    ''' <summary>"原閥1Purge位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019002 = 6019002 '"Old Valve1 Purge Pos({0},{1},{2})"
    ''' <summary>"新閥1Purge位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019003 = 6019003 '"New Valve1 Purge Pos({0},{1},{2})"
    ''' <summary>"原閥1Purge位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019004 = 6019004 '"Old Valve1 Purge Pos({0},{1},{2},{3})"
    ''' <summary>"新閥1Purge位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019005 = 6019005 '"New Valve1 Purge Pos({0},{1},{2},{3})"
    ''' <summary>"原閥1Purge位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019006 = 6019006 '"Old Valve1 Purge Pos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥1Purge位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019007 = 6019007 '"New Valve1 Purge Pos({0},{1},{2},{3},{4})"
    ''' <summary>"原閥1安全位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019008 = 6019008 '"Old Valve1 SafePos({0},{1},{2})"
    ''' <summary>"新閥1安全位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019009 = 6019009 '"New Valve1 SafePos({0},{1},{2})"
    ''' <summary>"原閥1安全位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019010 = 6019010 '"Old Valve1 SafePos({0},{1},{2},{3})"
    ''' <summary>"新閥1安全位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019011 = 6019011 '"New Valve1 SafePos({0},{1},{2},{3})"
    ''' <summary>"原閥1安全位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019012 = 6019012 '"Old Valve1 SafePos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥1安全位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019013 = 6019013 '"New Valve1 SafePos({0},{1},{2},{3},{4})"
    ''' <summary>"移動到閥1位置({0},{1})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019014 = 6019014 '"Goto Valve1 Pos({0},{1})"
    ''' <summary>"移動到閥1位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019015 = 6019015 '"Goto Valve1 Pos({0},{1},{2})"
    ''' <summary>"移動到閥1位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019016 = 6019016 '"Goto Valve1 Pos({0},{1},{2},{3})"
    ''' <summary>"移動到閥1位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019017 = 6019017 '"Goto Valve1 Pos({0},{1},{2},{3},{4})"
    ''' <summary>原閥1測高位置({0}{1})</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019018 = 6019018 '"Old Valve1 Pin Pos({0},{1})"
    ''' <summary>新閥1測高位置({0}{1})</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019019 = 6019019 '"New Valve1 Pin Pos({0},{1})"
    ''' <summary>"原閥1測高位置({0}{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019020 = 6019020 '"Old Valve1 Pin Pos({0},{1},{2})"
    ''' <summary>"新閥1測高位置({0}{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019021 = 6019021 '"New Valve1 Pin Pos({0},{1},{2})"
    ''' <summary>"原閥1測高位置({0}{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019022 = 6019022 '"Old Valve1 Pin Pos({0},{1},{2},{3})"
    ''' <summary>"新閥1測高位置({0}{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019023 = 6019023 '"New Valve1 Pin Pos({0},{1},{2},{3})"
    ''' <summary>"原閥1測高位置({0}{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019024 = 6019024 '"Old Valve1 Pin Pos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥1測高位置({0}{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019025 = 6019025 '"New Valve1 Pin Pos({0},{1},{2},{3},{4})"
    ''' <summary>"原閥1校正位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019026 = 6019026 '"Old Valve1 Calibration Pos({0},{1},{2})"
    ''' <summary>"新閥1校正位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019027 = 6019027 '"New Valve1 Calibration Pos({0},{1},{2})"
    ''' <summary>"原閥1校正位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019028 = 6019028 '"Old Valve1 Calibration Pos({0},{1},{2},{3})"
    ''' <summary>"新閥1校正位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019029 = 6019029 '"New Valve1 Calibration Pos({0},{1},{2},{3})"
    ''' <summary>"原閥1校正位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019030 = 6019030 '"Old Valve1 Calibration Pos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥1校正位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019031 = 6019031 '"New Valve1 Calibration Pos({0},{1},{2},{3},{4})"
    ''' <summary>"原閥1換膠位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019032 = 6019032 '"Old Valve1 Change Glue Pos({0},{1},{2})"
    ''' <summary>"新閥1換膠位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019033 = 6019033 '"New Valve1 Change Glue Pos({0},{1},{2})"
    ''' <summary>"原閥1換膠位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019034 = 6019034 '"Old Valve1 Change Glue Pos({0},{1},{2},{3})"
    ''' <summary>"新閥1換膠位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019035 = 6019035 '"New Valve1 Change Glue Pos({0},{1},{2},{3})"
    ''' <summary>"原閥1換膠位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019036 = 6019036 '"Old Valve1 Change Glue Pos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥1換膠位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019037 = 6019037 '"New Valve1 Change Glue Pos({0},{1},{2},{3},{4})"
    ''' <summary>"原閥1清膠位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019038 = 6019038 '"Old Valve1 Clear Glue Pos({0},{1},{2})"
    ''' <summary>"新閥1清膠位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019039 = 6019039 '"New Valve1 Clear Glue Pos({0},{1},{2})"
    ''' <summary>"原閥1清膠位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019040 = 6019040 '"Old Valve1 Clear Glue Pos({0},{1},{2},{3})"
    ''' <summary>"新閥1清膠位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019041 = 6019041 '"New Valve1 Clear Glue Pos({0},{1},{2},{3})"
    ''' <summary>"原閥1清膠位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019042 = 6019042 '"Old Valve1 Clear Glue Pos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥1清膠位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019043 = 6019043 '"New Valve1 Clear Glue Pos({0},{1},{2},{3},{4})"
    ''' <summary>原閥1校正場景編號:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019044 = 6019044 'Old Valve1 Calibration SceneID:{0}
    ''' <summary>新閥1校正場景編號:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019045 = 6019045 'New Valve1 Calibration SceneID:{0}
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6019046 = 6019046 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6019047 = 6019047 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6019048 = 6019048 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6019049 = 6019049 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6019050 = 6019050 '
    ''' <summary>原閥2Purge時間:{0}ms Cycle Time:{1}ms</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019100 = 6019100 'Old Valve2 Purge Time:{0}ms Cycle Time:{1}ms
    ''' <summary>新閥2Purge時間:{0}ms Cycle Time:{1}ms</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019101 = 6019101 'New Valve2 Purge Time:{0}ms Cycle Time:{1}ms
    ''' <summary>"原閥2Purge位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019102 = 6019102 '"Old Valve2 Purge Pos({0},{1},{2})"
    ''' <summary>"新閥2Purge位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019103 = 6019103 '"New Valve2 Purge Pos({0},{1},{2})"
    ''' <summary>"原閥2Purge位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019104 = 6019104 '"Old Valve2 Purge Pos({0},{1},{2},{3})"
    ''' <summary>"新閥2Purge位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019105 = 6019105 '"New Valve2 Purge Pos({0},{1},{2},{3})"
    ''' <summary>"原閥2Purge位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019106 = 6019106 '"Old Valve2 Purge Pos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥2Purge位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019107 = 6019107 '"New Valve2 Purge Pos({0},{1},{2},{3},{4})"
    ''' <summary>"原閥2安全位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019108 = 6019108 '"Old Valve2 SafePos({0},{1},{2})"
    ''' <summary>"新閥2安全位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019109 = 6019109 '"New Valve2 SafePos({0},{1},{2})"
    ''' <summary>"原閥2安全位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019110 = 6019110 '"Old Valve2 SafePos({0},{1},{2},{3})"
    ''' <summary>"新閥2安全位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019111 = 6019111 '"New Valve2 SafePos({0},{1},{2},{3})"
    ''' <summary>"原閥2安全位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019112 = 6019112 '"Old Valve2 SafePos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥2安全位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019113 = 6019113 '"New Valve2 SafePos({0},{1},{2},{3},{4})"
    ''' <summary>"移動到閥2位置({0},{1})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019114 = 6019114 '"Goto Valve2 Pos({0},{1})"
    ''' <summary>"移動到閥2位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019115 = 6019115 '"Goto Valve2 Pos({0},{1},{2})"
    ''' <summary>"移動到閥2位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019116 = 6019116 '"Goto Valve2 Pos({0},{1},{2},{3})"
    ''' <summary>"移動到閥2位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019117 = 6019117 '"Goto Valve2 Pos({0},{1},{2},{3},{4})"
    ''' <summary>原閥2測高位置({0}{1})</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019118 = 6019118 '"Old Valve2 Pin Pos({0},{1})"
    ''' <summary>新閥2測高位置({0}{1})</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019119 = 6019119 '"New Valve2 Pin Pos({0},{1})"
    ''' <summary>"原閥2測高位置({0}{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019120 = 6019120 '"Old Valve2 Pin Pos({0},{1},{2})"
    ''' <summary>"新閥2測高位置({0}{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019121 = 6019121 '"New Valve2 Pin Pos({0},{1},{2})"
    ''' <summary>"原閥2測高位置({0}{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019122 = 6019122 '"Old Valve2 Pin Pos({0},{1},{2},{3})"
    ''' <summary>"新閥2測高位置({0}{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019123 = 6019123 '"New Valve2 Pin Pos({0},{1},{2},{3})"
    ''' <summary>"原閥2測高位置({0}{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019124 = 6019124 '"Old Valve2 Pin Pos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥2測高位置({0}{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019125 = 6019125 '"New Valve2 Pin Pos({0},{1},{2},{3},{4})"
    ''' <summary>"原閥2校正位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019126 = 6019126 '"Old Valve2 Calibration Pos({0},{1},{2})"
    ''' <summary>"新閥2校正位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019127 = 6019127 '"New Valve2 Calibration Pos({0},{1},{2})"
    ''' <summary>"原閥2校正位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019128 = 6019128 '"Old Valve2 Calibration Pos({0},{1},{2},{3})"
    ''' <summary>"新閥2校正位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019129 = 6019129 '"New Valve2 Calibration Pos({0},{1},{2},{3})"
    ''' <summary>"原閥2校正位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019130 = 6019130 '"Old Valve2 Calibration Pos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥2校正位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019131 = 6019131 '"New Valve2 Calibration Pos({0},{1},{2},{3},{4})"
    ''' <summary>"原閥2換膠位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019132 = 6019132 '"Old Valve2 Change Glue Pos({0},{1},{2})"
    ''' <summary>"新閥2換膠位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019133 = 6019133 '"New Valve2 Change Glue Pos({0},{1},{2})"
    ''' <summary>"原閥2換膠位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019134 = 6019134 '"Old Valve2 Change Glue Pos({0},{1},{2},{3})"
    ''' <summary>"新閥2換膠位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019135 = 6019135 '"New Valve2 Change Glue Pos({0},{1},{2},{3})"
    ''' <summary>"原閥2換膠位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019136 = 6019136 '"Old Valve2 Change Glue Pos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥2換膠位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019137 = 6019137 '"New Valve2 Change Glue Pos({0},{1},{2},{3},{4})"
    ''' <summary>"原閥2清膠位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019138 = 6019138 '"Old Valve2 Clear Glue Pos({0},{1},{2})"
    ''' <summary>"新閥2清膠位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019139 = 6019139 '"New Valve2 Clear Glue Pos({0},{1},{2})"
    ''' <summary>"原閥2清膠位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019140 = 6019140 '"Old Valve2 Clear Glue Pos({0},{1},{2},{3})"
    ''' <summary>"新閥2清膠位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019141 = 6019141 '"New Valve2 Clear Glue Pos({0},{1},{2},{3})"
    ''' <summary>"原閥2清膠位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019142 = 6019142 '"Old Valve2 Clear Glue Pos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥2清膠位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019143 = 6019143 '"New Valve2 Clear Glue Pos({0},{1},{2},{3},{4})"
    ''' <summary>原閥2校正場景編號:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019144 = 6019144 'Old Valve2 Calibration SceneID:{0}
    ''' <summary>新閥2校正場景編號:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019145 = 6019145 'New Valve2 Calibration SceneID:{0}
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6019146 = 6019146 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6019147 = 6019147 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6019148 = 6019148 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6019149 = 6019149 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6019150 = 6019150 '
    ''' <summary>原閥3Purge時間:{0}ms Cycle Time:{1}ms</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019200 = 6019200 'Old Valve3 Purge Time:{0}ms Cycle Time:{1}ms
    ''' <summary>新閥3Purge時間:{0}ms Cycle Time:{1}ms</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019201 = 6019201 'New Valve3 Purge Time:{0}ms Cycle Time:{1}ms
    ''' <summary>"原閥3Purge位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019202 = 6019202 '"Old Valve3 Purge Pos({0},{1},{2})"
    ''' <summary>"新閥3Purge位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019203 = 6019203 '"New Valve3 Purge Pos({0},{1},{2})"
    ''' <summary>"原閥3Purge位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019204 = 6019204 '"Old Valve3 Purge Pos({0},{1},{2},{3})"
    ''' <summary>"新閥3Purge位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019205 = 6019205 '"New Valve3 Purge Pos({0},{1},{2},{3})"
    ''' <summary>"原閥3Purge位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019206 = 6019206 '"Old Valve3 Purge Pos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥3Purge位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019207 = 6019207 '"New Valve3 Purge Pos({0},{1},{2},{3},{4})"
    ''' <summary>"原閥3安全位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019208 = 6019208 '"Old Valve3 SafePos({0},{1},{2})"
    ''' <summary>"新閥3安全位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019209 = 6019209 '"New Valve3 SafePos({0},{1},{2})"
    ''' <summary>"原閥3安全位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019210 = 6019210 '"Old Valve3 SafePos({0},{1},{2},{3})"
    ''' <summary>"新閥3安全位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019211 = 6019211 '"New Valve3 SafePos({0},{1},{2},{3})"
    ''' <summary>"原閥3安全位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019212 = 6019212 '"Old Valve3 SafePos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥3安全位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019213 = 6019213 '"New Valve3 SafePos({0},{1},{2},{3},{4})"
    ''' <summary>"移動到閥3位置({0},{1})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019214 = 6019214 '"Goto Valve3 Pos({0},{1})"
    ''' <summary>"移動到閥3位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019215 = 6019215 '"Goto Valve3 Pos({0},{1},{2})"
    ''' <summary>"移動到閥3位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019216 = 6019216 '"Goto Valve3 Pos({0},{1},{2},{3})"
    ''' <summary>"移動到閥3位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019217 = 6019217 '"Goto Valve3 Pos({0},{1},{2},{3},{4})"
    ''' <summary>原閥3測高位置({0}{1})</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019218 = 6019218 '"Old Valve3 Pin Pos({0},{1})"
    ''' <summary>新閥3測高位置({0}{1})</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019219 = 6019219 '"New Valve3 Pin Pos({0},{1})"
    ''' <summary>"原閥3測高位置({0}{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019220 = 6019220 '"Old Valve3 Pin Pos({0},{1},{2})"
    ''' <summary>"新閥3測高位置({0}{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019221 = 6019221 '"New Valve3 Pin Pos({0},{1},{2})"
    ''' <summary>"原閥3測高位置({0}{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019222 = 6019222 '"Old Valve3 Pin Pos({0},{1},{2},{3})"
    ''' <summary>"新閥3測高位置({0}{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019223 = 6019223 '"New Valve3 Pin Pos({0},{1},{2},{3})"
    ''' <summary>"原閥3測高位置({0}{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019224 = 6019224 '"Old Valve3 Pin Pos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥3測高位置({0}{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019225 = 6019225 '"New Valve3 Pin Pos({0},{1},{2},{3},{4})"
    ''' <summary>"原閥3校正位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019226 = 6019226 '"Old Valve3 Calibration Pos({0},{1},{2})"
    ''' <summary>"新閥3校正位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019227 = 6019227 '"New Valve3 Calibration Pos({0},{1},{2})"
    ''' <summary>"原閥3校正位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019228 = 6019228 '"Old Valve3 Calibration Pos({0},{1},{2},{3})"
    ''' <summary>"新閥3校正位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019229 = 6019229 '"New Valve3 Calibration Pos({0},{1},{2},{3})"
    ''' <summary>"原閥3校正位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019230 = 6019230 '"Old Valve3 Calibration Pos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥3校正位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019231 = 6019231 '"New Valve3 Calibration Pos({0},{1},{2},{3},{4})"
    ''' <summary>"原閥3換膠位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019232 = 6019232 '"Old Valve3 Change Glue Pos({0},{1},{2})"
    ''' <summary>"新閥3換膠位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019233 = 6019233 '"New Valve3 Change Glue Pos({0},{1},{2})"
    ''' <summary>"原閥3換膠位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019234 = 6019234 '"Old Valve3 Change Glue Pos({0},{1},{2},{3})"
    ''' <summary>"新閥3換膠位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019235 = 6019235 '"New Valve3 Change Glue Pos({0},{1},{2},{3})"
    ''' <summary>"原閥3換膠位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019236 = 6019236 '"Old Valve3 Change Glue Pos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥3換膠位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019237 = 6019237 '"New Valve3 Change Glue Pos({0},{1},{2},{3},{4})"
    ''' <summary>"原閥3清膠位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019238 = 6019238 '"Old Valve3 Clear Glue Pos({0},{1},{2})"
    ''' <summary>"新閥3清膠位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019239 = 6019239 '"New Valve3 Clear Glue Pos({0},{1},{2})"
    ''' <summary>"原閥3清膠位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019240 = 6019240 '"Old Valve3 Clear Glue Pos({0},{1},{2},{3})"
    ''' <summary>"新閥3清膠位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019241 = 6019241 '"New Valve3 Clear Glue Pos({0},{1},{2},{3})"
    ''' <summary>"原閥3清膠位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019242 = 6019242 '"Old Valve3 Clear Glue Pos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥3清膠位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019243 = 6019243 '"New Valve3 Clear Glue Pos({0},{1},{2},{3},{4})"
    ''' <summary>原閥3校正場景編號:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019244 = 6019244 'Old Valve3 Calibration SceneID:{0}
    ''' <summary>新閥3校正場景編號:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019245 = 6019245 'New Valve3 Calibration SceneID:{0}
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6019246 = 6019246 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6019247 = 6019247 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6019248 = 6019248 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6019249 = 6019249 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6019250 = 6019250 '
    ''' <summary>原閥4Purge時間:{0}ms Cycle Time:{1}ms</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019300 = 6019300 'Old Valve4 Purge Time:{0}ms Cycle Time:{1}ms
    ''' <summary>新閥4Purge時間:{0}ms Cycle Time:{1}ms</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019301 = 6019301 'New Valve4 Purge Time:{0}ms Cycle Time:{1}ms
    ''' <summary>"原閥4Purge位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019302 = 6019302 '"Old Valve4 Purge Pos({0},{1},{2})"
    ''' <summary>"新閥4Purge位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019303 = 6019303 '"New Valve4 Purge Pos({0},{1},{2})"
    ''' <summary>"原閥4Purge位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019304 = 6019304 '"Old Valve4 Purge Pos({0},{1},{2},{3})"
    ''' <summary>"新閥4Purge位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019305 = 6019305 '"New Valve4 Purge Pos({0},{1},{2},{3})"
    ''' <summary>"原閥4Purge位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019306 = 6019306 '"Old Valve4 Purge Pos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥4Purge位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019307 = 6019307 '"New Valve4 Purge Pos({0},{1},{2},{3},{4})"
    ''' <summary>"原閥4安全位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019308 = 6019308 '"Old Valve4 SafePos({0},{1},{2})"
    ''' <summary>"新閥4安全位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019309 = 6019309 '"New Valve4 SafePos({0},{1},{2})"
    ''' <summary>"原閥4安全位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019310 = 6019310 '"Old Valve4 SafePos({0},{1},{2},{3})"
    ''' <summary>"新閥4安全位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019311 = 6019311 '"New Valve4 SafePos({0},{1},{2},{3})"
    ''' <summary>"原閥4安全位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019312 = 6019312 '"Old Valve4 SafePos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥4安全位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019313 = 6019313 '"New Valve4 SafePos({0},{1},{2},{3},{4})"
    ''' <summary>"移動到閥4位置({0},{1})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019314 = 6019314 '"Goto Valve4 Pos({0},{1})"
    ''' <summary>"移動到閥4位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019315 = 6019315 '"Goto Valve4 Pos({0},{1},{2})"
    ''' <summary>"移動到閥4位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019316 = 6019316 '"Goto Valve4 Pos({0},{1},{2},{3})"
    ''' <summary>"移動到閥4位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019317 = 6019317 '"Goto Valve4 Pos({0},{1},{2},{3},{4})"
    ''' <summary>原閥4測高位置({0}{1})</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019318 = 6019318 '"Old Valve4 Pin Pos({0},{1})"
    ''' <summary>新閥4測高位置({0}{1})</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019319 = 6019319 '"New Valve4 Pin Pos({0},{1})"
    ''' <summary>"原閥4測高位置({0}{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019320 = 6019320 '"Old Valve4 Pin Pos({0},{1},{2})"
    ''' <summary>"新閥4測高位置({0}{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019321 = 6019321 '"New Valve4 Pin Pos({0},{1},{2})"
    ''' <summary>"原閥4測高位置({0}{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019322 = 6019322 '"Old Valve4 Pin Pos({0},{1},{2},{3})"
    ''' <summary>"新閥4測高位置({0}{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019323 = 6019323 '"New Valve4 Pin Pos({0},{1},{2},{3})"
    ''' <summary>"原閥4測高位置({0}{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019324 = 6019324 '"Old Valve4 Pin Pos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥4測高位置({0}{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019325 = 6019325 '"New Valve4 Pin Pos({0},{1},{2},{3},{4})"
    ''' <summary>"原閥4校正位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019326 = 6019326 '"Old Valve4 Calibration Pos({0},{1},{2})"
    ''' <summary>"新閥4校正位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019327 = 6019327 '"New Valve4 Calibration Pos({0},{1},{2})"
    ''' <summary>"原閥4校正位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019328 = 6019328 '"Old Valve4 Calibration Pos({0},{1},{2},{3})"
    ''' <summary>"新閥4校正位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019329 = 6019329 '"New Valve4 Calibration Pos({0},{1},{2},{3})"
    ''' <summary>"原閥4校正位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019330 = 6019330 '"Old Valve4 Calibration Pos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥4校正位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019331 = 6019331 '"New Valve4 Calibration Pos({0},{1},{2},{3},{4})"
    ''' <summary>"原閥4換膠位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019332 = 6019332 '"Old Valve4 Change Glue Pos({0},{1},{2})"
    ''' <summary>"新閥4換膠位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019333 = 6019333 '"New Valve4 Change Glue Pos({0},{1},{2})"
    ''' <summary>"原閥4換膠位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019334 = 6019334 '"Old Valve4 Change Glue Pos({0},{1},{2},{3})"
    ''' <summary>"新閥4換膠位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019335 = 6019335 '"New Valve4 Change Glue Pos({0},{1},{2},{3})"
    ''' <summary>"原閥4換膠位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019336 = 6019336 '"Old Valve4 Change Glue Pos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥4換膠位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019337 = 6019337 '"New Valve4 Change Glue Pos({0},{1},{2},{3},{4})"
    ''' <summary>"原閥4清膠位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019338 = 6019338 '"Old Valve4 Clear Glue Pos({0},{1},{2})"
    ''' <summary>"新閥4清膠位置({0},{1},{2})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019339 = 6019339 '"New Valve4 Clear Glue Pos({0},{1},{2})"
    ''' <summary>"原閥4清膠位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019340 = 6019340 '"Old Valve4 Clear Glue Pos({0},{1},{2},{3})"
    ''' <summary>"新閥4清膠位置({0},{1},{2},{3})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019341 = 6019341 '"New Valve4 Clear Glue Pos({0},{1},{2},{3})"
    ''' <summary>"原閥4清膠位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019342 = 6019342 '"Old Valve4 Clear Glue Pos({0},{1},{2},{3},{4})"
    ''' <summary>"新閥4清膠位置({0},{1},{2},{3},{4})"</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019343 = 6019343 '"New Valve4 Clear Glue Pos({0},{1},{2},{3},{4})"
    ''' <summary>原閥4校正場景編號:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019344 = 6019344 'Old Valve4 Calibration SceneID:{0}
    ''' <summary>新閥4校正場景編號:{0}</summary>
    ''' <remarks></remarks>
    Public Const INFO_6019345 = 6019345 'New Valve4 Calibration SceneID:{0}
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6019346 = 6019346 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6019347 = 6019347 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6019348 = 6019348 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6019349 = 6019349 '
    ''' <summary></summary>
    ''' <remarks></remarks>
    Public Const INFO_6019350 = 6019350 '
''' <summary>資料給傳送帶1:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6020000 = 6020000	'Data To Conveyor1:{0}
''' <summary>資料從傳送帶1:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6020001 = 6020001	'Data From Conveyor1:{0}
''' <summary></summary>
''' <remarks></remarks>
	Public Const INFO_6020002 = 6020002	'
''' <summary>程控光源1初始化成功.</summary>
''' <remarks></remarks>
	Public Const INFO_6022000 = 6022000	'Initialize Program Light1 OK.
''' <summary>程控光源1關閉成功.</summary>
''' <remarks></remarks>
	Public Const INFO_6022001 = 6022001	'Close Program Light1 OK.
''' <summary>程控光源2初始化成功.</summary>
''' <remarks></remarks>
	Public Const INFO_6022002 = 6022002	'Initialize Program Light2 OK.
''' <summary>程控光源2關閉成功.</summary>
''' <remarks></remarks>
	Public Const INFO_6022003 = 6022003	'Close Program Light2 OK.
''' <summary>程控光源3初始化成功.</summary>
''' <remarks></remarks>
	Public Const INFO_6022004 = 6022004	'Initialize Program Light3 OK.
''' <summary>程控光源3關閉成功.</summary>
''' <remarks></remarks>
	Public Const INFO_6022005 = 6022005	'Close Program Light3 OK.
''' <summary>程控光源4初始化成功.</summary>
''' <remarks></remarks>
	Public Const INFO_6022006 = 6022006	'Initialize Program Light4 OK.
''' <summary>程控光源4關閉成功.</summary>
''' <remarks></remarks>
	Public Const INFO_6022007 = 6022007	'Close Program Light4 OK.
''' <summary>程控光源5初始化成功.</summary>
''' <remarks></remarks>
	Public Const INFO_6022008 = 6022008	'Initialize Program Light5 OK.
''' <summary>程控光源5關閉成功.</summary>
''' <remarks></remarks>
	Public Const INFO_6022009 = 6022009	'Close Program Light5 OK.
''' <summary>原設定CCD光源1亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022010 = 6022010	'Old Set CCD Light1:{0}
''' <summary>新設定CCD光源1亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022011 = 6022011	'New Set CCD Light1:{0}
''' <summary>原設定CCD光源2亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022012 = 6022012	'Old Set CCD Light2:{0}
''' <summary>新設定CCD光源2亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022013 = 6022013	'New Set CCD Light2:{0}
''' <summary>原設定CCD光源3亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022014 = 6022014	'Old Set CCD Light3:{0}
''' <summary>新設定CCD光源3亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022015 = 6022015	'New Set CCD Light3:{0}
''' <summary>原設定CCD光源4亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022016 = 6022016	'Old Set CCD Light4:{0}
''' <summary>新設定CCD光源4亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022017 = 6022017	'New Set CCD Light4:{0}
''' <summary>原設定CCD光源5亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022018 = 6022018	'Old Set CCD Light5:{0}
''' <summary>新設定CCD光源5亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022019 = 6022019	'New Set CCD Light5:{0}
''' <summary>原設定CCD光源6亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022020 = 6022020	'Old Set CCD Light6:{0}
''' <summary>新設定CCD光源6亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022021 = 6022021	'New Set CCD Light6:{0}
''' <summary>原設定CCD光源7亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022022 = 6022022	'Old Set CCD Light7:{0}
''' <summary>新設定CCD光源7亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022023 = 6022023	'New Set CCD Light7:{0}
''' <summary>原設定CCD光源8亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022024 = 6022024	'Old Set CCD Light8:{0}
''' <summary>新設定CCD光源8亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022025 = 6022025	'New Set CCD Light8:{0}
''' <summary>原設定CCD光源9亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022026 = 6022026	'Old Set CCD Light9:{0}
''' <summary>新設定CCD光源9亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022027 = 6022027	'New Set CCD Light9:{0}
''' <summary>原設定CCD光源10亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022028 = 6022028	'Old Set CCD Light10:{0}
''' <summary>新設定CCD光源10亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022029 = 6022029	'New Set CCD Light10:{0}
''' <summary>原設定CCD光源11亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022030 = 6022030	'Old Set CCD Light11:{0}
''' <summary>新設定CCD光源11亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022031 = 6022031	'New Set CCD Light11:{0}
''' <summary>原設定CCD光源12亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022032 = 6022032	'Old Set CCD Light12:{0}
''' <summary>新設定CCD光源12亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022033 = 6022033	'New Set CCD Light12:{0}
''' <summary>原設定CCD光源13亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022034 = 6022034	'Old Set CCD Light13:{0}
''' <summary>新設定CCD光源13亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022035 = 6022035	'New Set CCD Light13:{0}
''' <summary>原設定CCD光源14亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022036 = 6022036	'Old Set CCD Light14:{0}
''' <summary>新設定CCD光源14亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022037 = 6022037	'New Set CCD Light14:{0}
''' <summary>原設定CCD光源15亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022038 = 6022038	'Old Set CCD Light15:{0}
''' <summary>新設定CCD光源15亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022039 = 6022039	'New Set CCD Light15:{0}
''' <summary>原設定CCD光源16亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022040 = 6022040	'Old Set CCD Light16:{0}
''' <summary>新設定CCD光源16亮度:{0}</summary>
''' <remarks></remarks>
	Public Const INFO_6022041 = 6022041	'New Set CCD Light16:{0}
''' <summary>PLC1初始化成功.</summary>
''' <remarks></remarks>
	Public Const INFO_6023000 = 6023000	'Initialize PLC1 OK.
''' <summary></summary>
''' <remarks></remarks>
	Public Const INFO_6023001 = 6023001	'
''' <summary></summary>
''' <remarks></remarks>
	Public Const INFO_6023002 = 6023002	'
''' <summary></summary>
''' <remarks></remarks>
	Public Const INFO_6023003 = 6023003	'
''' <summary></summary>
''' <remarks></remarks>
	Public Const INFO_6023004 = 6023004	'
''' <summary></summary>
''' <remarks></remarks>
	Public Const INFO_6023005 = 6023005	'
''' <summary>UPH</summary>
''' <remarks></remarks>
	Public Const STATUS_5000001 = 5000001	'UPH
''' <summary>LOT ID</summary>
''' <remarks></remarks>
	Public Const STATUS_5000002 = 5000002	'LOT ID
''' <summary>RECIPE ID</summary>
''' <remarks></remarks>
	Public Const STATUS_5000003 = 5000003	'RECIPE ID
''' <summary>BARCODE ID</summary>
''' <remarks></remarks>
	Public Const STATUS_5000004 = 5000004	'BARCODE ID
''' <summary>VALVE1 AIR PRESSURE</summary>
''' <remarks></remarks>
	Public Const STATUS_5001000 = 5001000	'VALVE1 AIR PRESSURE
''' <summary>VALVE2 AIR PRESSURE</summary>
''' <remarks></remarks>
	Public Const STATUS_5001001 = 5001001	'VALVE2 AIR PRESSURE
''' <summary>VALVE3 AIR PRESSURE</summary>
''' <remarks></remarks>
	Public Const STATUS_5001002 = 5001002	'VALVE3 AIR PRESSURE
''' <summary>VALVE4 AIR PRESSURE</summary>
''' <remarks></remarks>
	Public Const STATUS_5001003 = 5001003	'VALVE4 AIR PRESSURE
End Module
