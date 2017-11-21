Public Module MCommonDefineDO

    ''' <summary>
    ''' DO列表
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure enmDO
        ''' <summary>索引最大值</summary>
        ''' <remarks></remarks>
        Public Shared Max As Integer = 100
        Public Shared Reserved As Integer = -1
        ''' <summary>塔燈-紅色指示燈/A機_四色燈-紅燈(異常)</summary>
        ''' <remarks></remarks>
        Public Shared RedIndicator As Integer = -1
        ''' <summary>塔燈-黃色指示燈/A機_四色燈-黃燈(警告)</summary>
        ''' <remarks></remarks>
        Public Shared YellowIndicator As Integer = -1
        ''' <summary>塔燈-綠色指示燈/A機_四色燈-綠燈(生產)</summary>
        ''' <remarks></remarks>
        Public Shared GreenIndicator As Integer = -1
        ''' <summary>塔燈-藍色指示燈)</summary>
        ''' <remarks></remarks>
        Public Shared BlueIndicator As Integer = -1
        ''' <summary>塔燈-蜂鳴器動作/A機_蜂鳴器</summary>
        ''' <remarks></remarks>
        Public Shared Buzzer As Integer = -1
        ''' <summary>安全門扣動作/A機_門鎖</summary>
        ''' <remarks></remarks>
        Public Shared DoorLock As Integer = -1
        ''' <summary>A機_開始生產燈</summary>
        ''' <remarks></remarks>
        Public Shared StartButtonLight As Integer = -1
        ''' <summary>A機_暫停生產燈</summary>
        ''' <remarks></remarks>
        Public Shared PauseButtonLight As Integer = -1
        ''' <summary>本站可接受物料(上站要板訊號)/A機_向上站要板訊號</summary>
        ''' <remarks></remarks>
        Public Shared MachineReadyToRecieve As Integer = -1
        ''' <summary>本站生產完成(下站預備訊號)/A機_向下站供板完成</summary>
        ''' <remarks></remarks>
        Public Shared BoardAvailable As Integer = -1
        ''' <summary>DO-System Alarm/A機_本站異常</summary>
        ''' <remarks></remarks>
        Public Shared SystemAlarm As Integer = -1
        ''' <summary>DO-測高Sensor Reset/雷射測高重置/A機_左側重啟測高感測器</summary>
        ''' <remarks></remarks>
        Public Shared LaserReaderReset1 As Integer = -1
        ''' <summary>[位置1觸發板重置/A機_左側重啟觸發板]</summary>
        ''' <remarks></remarks>
        Public Shared TriggerBoardReset1 As Integer = -1
        ''' <summary>[位置1噴射閥控制器重置/A機_左側重啟閥控制器]</summary>
        ''' <remarks></remarks>
        Public Shared ValveControllerReset1 As Integer = -1
        ''' <summary>[A機_左側閥觸發出膠]</summary>
        ''' <remarks></remarks>
        Public Shared DispensingTrigger1 As Integer = -1
        ''' <summary>A機_左側閥轉向</summary>
        ''' <remarks></remarks>
        Public Shared ValveAugerDir1 As Integer = -1
        ''' <summary>A機_右側重啟測高感測器</summary>
        ''' <remarks></remarks>
        Public Shared LaserReaderReset2 As Integer = -1
        ''' <summary>[A機_右側重啟觸發板]</summary>
        ''' <remarks></remarks>
        Public Shared TriggerBoardReset2 As Integer = -1
        ''' <summary>[A機_右側重啟閥控制器]</summary>
        ''' <remarks></remarks>
        Public Shared ValveControllerReset2 As Integer = -1
        ''' <summary>[A機_右側閥觸發出膠]</summary>
        ''' <remarks></remarks>
        Public Shared DispensingTrigger2 As Integer = -1
        ''' <summary>A機_右側閥轉向</summary>
        ''' <remarks></remarks>
        Public Shared ValveAugerDir2 As Integer = -1
        ''' <summary>DO-CCD 觸發拍照/視覺觸發拍照/A機_左側CCD觸發</summary>
        ''' <remarks></remarks>
        Public Shared CcdImageTrigger As Integer = -1
        ''' <summary>A機_右側CCD觸發</summary>
        ''' <remarks></remarks>
        Public Shared CcdImageTrigger2 As Integer = -1
        ''' <summary>站2 阻擋氣缸動作/A機_點膠作業區之料盤阻擋汽缸上昇</summary>
        ''' <remarks></remarks>
        Public Shared Station2StopperUp As Integer = -1
        ''' <summary>A機_點膠作業區之料盤阻擋汽缸下降</summary>
        ''' <remarks></remarks>
        Public Shared Station2StopperDown As Integer = -1
        ''' <summary>站1 阻擋氣缸動作/A機_點膠作業區之料盤阻擋汽缸上昇(前)</summary>
        ''' <remarks></remarks>
        Public Shared Station1StopperUpDown As Integer = -1
        ''' <summary>A機_點膠作業區之料盤阻擋汽缸下降(前)</summary>
        ''' <remarks></remarks>
        Public Shared Station1StopperDown As Integer = -1
        ''' <summary>A機_前皮帶傳輸動力 CW</summary>
        ''' <remarks></remarks>
        Public Shared MoveInMotorCW As Integer = -1
        ''' <summary>A機_前皮帶傳輸動力 CCW</summary>
        ''' <remarks></remarks>
        Public Shared MoveInMotorCCW As Integer = -1
        ''' <summary>A機_前皮帶傳輸動力 Slow</summary>
        ''' <remarks></remarks>
        Public Shared MoveInMotorSlow As Integer = -1
        ''' <summary>A機_前皮帶傳輸動力 Reset</summary>
        ''' <remarks></remarks>
        Public Shared MoveInMotorReset As Integer = -1
        ''' <summary>A機_點膠加熱區1汽缸上昇</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderUp1 As Integer = -1
        ''' <summary>A機_點膠加熱區1汽缸下降</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderDown1 As Integer = -1
        ''' <summary>A機_點膠加熱區2汽缸上昇</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderUp2 As Integer = -1
        ''' <summary>A機_點膠加熱區2汽缸下降</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderDown2 As Integer = -1
        ''' <summary>A機_點膠加熱區3汽缸上昇</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderUp3 As Integer = -1
        ''' <summary>A機_點膠加熱區3汽缸下降</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderDown3 As Integer = -1
        ''' <summary>A機_點膠加熱區4汽缸上昇</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderUp4 As Integer = -1
        ''' <summary>A機_點膠加熱區4汽缸下降</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderDown4 As Integer = -1
        ''' <summary>A機_點膠加熱區5汽缸上昇</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderUp5 As Integer = -1
        ''' <summary>A機_點膠加熱區5汽缸下降</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderDown5 As Integer = -1
        ''' <summary>A機_點膠加熱區6汽缸上昇</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderUp6 As Integer = -1
        ''' <summary>A機_點膠加熱區6汽缸下降</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderDown6 As Integer = -1
        ''' <summary>DO-Purge 清膠/真空除膠動作/A機_左側Purge杯真空</summary>
        ''' <remarks></remarks>
        Public Shared Purge As Integer = -1
        ''' <summary>A機_左側Purge杯破真空</summary>
        ''' <remarks></remarks>
        Public Shared PurgeVacuumBreaker As Integer = -1
        ''' <summary>A機_右側Purge杯真空</summary>
        ''' <remarks></remarks>
        Public Shared PurgeVacuum2 As Integer = -1
        ''' <summary>A機_右側Purge杯破真空</summary>
        ''' <remarks></remarks>
        Public Shared PurgeVacuumBreaker2 As Integer = -1
        ''' <summary>位置1膠桶正壓動作/A機_左側閥膠管正壓</summary>
        ''' <remarks></remarks>
        Public Shared SyringePressure1 As Integer = -1
        ''' <summary>位置2膠桶正壓動作/A機_右側閥膠管正壓</summary>
        ''' <remarks></remarks>
        Public Shared SyringePressure2 As Integer = -1
        ''' <summary>A機_點膠加熱區加熱控制開1</summary>
        ''' <remarks></remarks>
        Public Shared HeaterOn1 As Integer = -1
        ''' <summary>A機_點膠加熱區加熱控制開2</summary>
        ''' <remarks></remarks>
        Public Shared HeaterOn2 As Integer = -1
        ''' <summary>A機_點膠加熱區加熱控制開3</summary>
        ''' <remarks></remarks>
        Public Shared HeaterOn3 As Integer = -1
        ''' <summary>A機_點膠加熱區加熱控制開4</summary>
        ''' <remarks></remarks>
        Public Shared HeaterOn4 As Integer = -1
        ''' <summary>A機_點膠加熱區加熱控制開5</summary>
        ''' <remarks></remarks>
        Public Shared HeaterOn5 As Integer = -1
        ''' <summary>A機_點膠加熱區加熱控制開6</summary>
        ''' <remarks></remarks>
        Public Shared HeaterOn6 As Integer = -1
        ''' <summary>A機_Chuck1前段破真空</summary>
        ''' <remarks></remarks>
        Public Shared Station2ChuckVacuumBreak1 As Integer = -1
        ''' <summary>A機_Chuck2前段破真空</summary>
        ''' <remarks></remarks>
        Public Shared Station2ChuckVacuumBreak2 As Integer = -1
        ''' <summary>A機_Chuck3前段破真空</summary>
        ''' <remarks></remarks>
        Public Shared Station2ChuckVacuumBreak3 As Integer = -1
        ''' <summary>A機_Chuck4前段破真空</summary>
        ''' <remarks></remarks>
        Public Shared Station2ChuckVacuumBreak4 As Integer = -1
        ''' <summary>A機_Chuck5前段破真空</summary>
        ''' <remarks></remarks>
        Public Shared Station2ChuckVacuumBreak5 As Integer = -1
        ''' <summary>A機_Chuck6前段破真空</summary>
        ''' <remarks></remarks>
        Public Shared Station2ChuckVacuumBreak6 As Integer = -1
        ''' <summary>站2 料盤真空動作/A機_Chuck1前段真空</summary>
        ''' <remarks></remarks>
        Public Shared Station2ChuckVacuum As Integer = -1
        ''' <summary>A機_Chuck2前段真空</summary>
        ''' <remarks></remarks>
        Public Shared Station2ChuckVacuum2 As Integer = -1
        ''' <summary>A機_Chuck3前段真空</summary>
        ''' <remarks></remarks>
        Public Shared Station2ChuckVacuum3 As Integer = -1
        ''' <summary>A機_Chuck4前段真空</summary>
        ''' <remarks></remarks>
        Public Shared Station2ChuckVacuum4 As Integer = -1
        ''' <summary>A機_Chuck5前段真空</summary>
        ''' <remarks></remarks>
        Public Shared Station2ChuckVacuum5 As Integer = -1
        ''' <summary>A機_Chuck6前段真空</summary>
        ''' <remarks></remarks>
        Public Shared Station2ChuckVacuum6 As Integer = -1
        ''' <summary>B機_點膠作業區加熱控制開1</summary>
        ''' <remarks></remarks>
        Public Shared HeaterOn7 As Integer = -1
        ''' <summary>B機_點膠作業區加熱控制開2</summary>
        ''' <remarks></remarks>
        Public Shared HeaterOn8 As Integer = -1
        ''' <summary>B機_點膠作業區加熱控制開3</summary>
        ''' <remarks></remarks>
        Public Shared HeaterOn9 As Integer = -1
        ''' <summary>B機_點膠作業區加熱控制開4</summary>
        ''' <remarks></remarks>
        Public Shared HeaterOn10 As Integer = -1
        ''' <summary>B機_點膠作業區加熱控制開5</summary>
        ''' <remarks></remarks>
        Public Shared HeaterOn11 As Integer = -1
        ''' <summary>B機_點膠作業區加熱控制開6</summary>
        ''' <remarks></remarks>
        Public Shared HeaterOn12 As Integer = -1
        ''' <summary>B機_Chuck1前段破真空</summary>
        ''' <remarks></remarks>
        Public Shared Station3ChuckVacuumBreak1 As Integer = -1
        ''' <summary>B機_Chuck2前段破真空</summary>
        ''' <remarks></remarks>
        Public Shared Station3ChuckVacuumBreak2 As Integer = -1
        ''' <summary>B機_Chuck3前段破真空</summary>
        ''' <remarks></remarks>
        Public Shared Station3ChuckVacuumBreak3 As Integer = -1
        ''' <summary>B機_Chuck4前段破真空</summary>
        ''' <remarks></remarks>
        Public Shared Station3ChuckVacuumBreak4 As Integer = -1
        ''' <summary>B機_Chuck5前段破真空</summary>
        ''' <remarks></remarks>
        Public Shared Station3ChuckVacuumBreak5 As Integer = -1
        ''' <summary>B機_Chuck6前段破真空</summary>
        ''' <remarks></remarks>
        Public Shared Station3ChuckVacuumBreak6 As Integer = -1
        ''' <summary>站3 料盤真空動作/B機_Chuck1前段真空</summary>
        ''' <remarks></remarks>
        Public Shared Station3ChuckVacuum As Integer = -1
        ''' <summary>B機_Chuck2前段真空</summary>
        ''' <remarks></remarks>
        Public Shared Station3ChuckVacuum2 As Integer = -1
        ''' <summary>B機_Chuck3前段真空</summary>
        ''' <remarks></remarks>
        Public Shared Station3ChuckVacuum3 As Integer = -1
        ''' <summary>B機_Chuck4前段真空</summary>
        ''' <remarks></remarks>
        Public Shared Station3ChuckVacuum4 As Integer = -1
        ''' <summary>B機_Chuck5前段真空</summary>
        ''' <remarks></remarks>
        Public Shared Station3ChuckVacuum5 As Integer = -1
        ''' <summary>B機_Chuck6前段真空</summary>
        ''' <remarks></remarks>
        Public Shared Station3ChuckVacuum6 As Integer = -1
        ''' <summary>安全門扣動作/B機_門鎖</summary>
        ''' <remarks></remarks>
        Public Shared DoorLock2 As Integer = -1
        ''' <summary>B機_開始生產燈</summary>
        ''' <remarks></remarks>
        Public Shared StartButtonLight2 As Integer = -1
        ''' <summary>B機_暫停生產燈</summary>
        ''' <remarks></remarks>
        Public Shared PauseButtonLight2 As Integer = -1
        ''' <summary>本站可接受物料(上站要板訊號)/B機_向上站要板訊號</summary>
        ''' <remarks></remarks>
        Public Shared MachineReadyToRecieve2 As Integer = -1
        ''' <summary>本站生產完成(下站預備訊號)/B機_向下站供板完成</summary>
        ''' <remarks></remarks>
        Public Shared BoardAvailable2 As Integer = -1
        ''' <summary>DO-System Alarm/B機_本站異常</summary>
        ''' <remarks></remarks>
        Public Shared SystemAlarm2 As Integer = -1
        ''' <summary>B機_左側重啟測高感測器</summary>
        ''' <remarks></remarks>
        Public Shared LaserReaderReset3 As Integer = -1
        ''' <summary>[B機_左側重啟觸發板]</summary>
        ''' <remarks></remarks>
        Public Shared TriggerBoardReset3 As Integer = -1
        ''' <summary>[B機_左側重啟閥控制器]</summary>
        ''' <remarks></remarks>
        Public Shared ValveControllerReset3 As Integer = -1
        ''' <summary>[B機_左側閥觸發出膠]</summary>
        ''' <remarks></remarks>
        Public Shared DispensingTrigger3 As Integer = -1
        ''' <summary>B機_左側閥轉向</summary>
        ''' <remarks></remarks>
        Public Shared ValveAugerDir3 As Integer = -1
        ''' <summary>B機_右側重啟測高感測器</summary>
        ''' <remarks></remarks>
        Public Shared ResetLaserReader4 As Integer = -1
        ''' <summary>[B機_右側重啟觸發板]</summary>
        ''' <remarks></remarks>
        Public Shared TriggerBoardReset4 As Integer = -1
        ''' <summary>[B機_右側重啟閥控制器]</summary>
        ''' <remarks></remarks>
        Public Shared ValveControllerReset4 As Integer = -1
        ''' <summary>[B機_右側閥觸發出膠]</summary>
        ''' <remarks></remarks>
        Public Shared DispensingTrigger4 As Integer = -1
        ''' <summary>B機_右側閥轉向</summary>
        ''' <remarks></remarks>
        Public Shared ValveAugerDir4 As Integer = -1
        ''' <summary>B機_左側CCD觸發</summary>
        ''' <remarks></remarks>
        Public Shared CcdImageTrigger3 As Integer = -1
        ''' <summary>B機_右側CCD觸發</summary>
        ''' <remarks></remarks>
        Public Shared CcdImageTrigger4 As Integer = -1
        ''' <summary>站3 阻擋氣缸動作/B機_點膠作業區之料盤阻擋汽缸上昇</summary>
        ''' <remarks></remarks>
        Public Shared Station3StopperUp As Integer = -1
        ''' <summary>B機_點膠作業區之料盤阻擋汽缸下降</summary>
        ''' <remarks></remarks>
        Public Shared Station3StopperDown As Integer = -1
        ''' <summary>B機_前皮帶傳輸動力 CW</summary>
        ''' <remarks></remarks>
        Public Shared MoveInMotorCW2 As Integer = -1
        ''' <summary>B機_前皮帶傳輸動力 CCW</summary>
        ''' <remarks></remarks>
        Public Shared MoveInMotorCCW2 As Integer = -1
        ''' <summary>B機_前皮帶傳輸動力 Slow</summary>
        ''' <remarks></remarks>
        Public Shared MoveInMotorSlow2 As Integer = -1
        ''' <summary>B機_前皮帶傳輸動力 Reset</summary>
        ''' <remarks></remarks>
        Public Shared MoveInMotorReset2 As Integer = -1
        ''' <summary>B機_點膠加熱區1汽缸上昇</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderUp7 As Integer = -1
        ''' <summary>B機_點膠加熱區1汽缸下降</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderDown7 As Integer = -1
        ''' <summary>B機_點膠加熱區2汽缸上昇</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderUp8 As Integer = -1
        ''' <summary>B機_點膠加熱區2汽缸下降</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderDown8 As Integer = -1
        ''' <summary>B機_點膠加熱區3汽缸上昇</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderUp9 As Integer = -1
        ''' <summary>B機_點膠加熱區3汽缸下降</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderDown9 As Integer = -1
        ''' <summary>B機_點膠加熱區4汽缸上昇</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderUp10 As Integer = -1
        ''' <summary>B機_點膠加熱區4汽缸下降</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderDown10 As Integer = -1
        ''' <summary>B機_點膠加熱區5汽缸上昇</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderUp11 As Integer = -1
        ''' <summary>B機_點膠加熱區5汽缸下降</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderDown11 As Integer = -1
        ''' <summary>B機_點膠加熱區6汽缸上昇</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderUp12 As Integer = -1
        ''' <summary>B機_點膠加熱區6汽缸下降</summary>
        ''' <remarks></remarks>
        Public Shared HeaterCylinderDown12 As Integer = -1
        ''' <summary>B機_左側Purge杯真空</summary>
        ''' <remarks></remarks>
        Public Shared PurgeVacuum3 As Integer = -1
        ''' <summary>B機_左側Purge杯破真空</summary>
        ''' <remarks></remarks>
        Public Shared PurgeVacuumBreaker3 As Integer = -1
        ''' <summary>B機_右側Purge杯真空</summary>
        ''' <remarks></remarks>
        Public Shared PurgeVacuum4 As Integer = -1
        ''' <summary>B機_右側Purge杯破真空</summary>
        ''' <remarks></remarks>
        Public Shared PurgeVacuumBreaker4 As Integer = -1
        ''' <summary>B機_左側閥膠管正壓</summary>
        ''' <remarks></remarks>
        Public Shared SyringePressure3 As Integer = -1
        ''' <summary>B機_右側閥膠管正壓</summary>
        ''' <remarks></remarks>
        Public Shared SyringePressure4 As Integer = -1
        ''' <summary>光源開關1</summary>
        ''' <remarks></remarks>
        Public Shared CCDLight As Integer = -1
        ''' <summary>光源開關2</summary>
        ''' <remarks></remarks>
        Public Shared CCDLight2 As Integer = -1
        ''' <summary>光源開關3</summary>
        ''' <remarks></remarks>
        Public Shared CCDLight3 As Integer = -1
        ''' <summary>光源開關4</summary>
        ''' <remarks></remarks>
        Public Shared CCDLight4 As Integer = -1
        ''' <summary>光源開關5</summary>
        ''' <remarks></remarks>
        Public Shared CCDLight5 As Integer = -1
        ''' <summary>光源開關6</summary>
        ''' <remarks></remarks>
        Public Shared CCDLight6 As Integer = -1
        ''' <summary>光源開關7</summary>
        ''' <remarks></remarks>
        Public Shared CCDLight7 As Integer = -1
        ''' <summary>光源開關8</summary>
        ''' <remarks></remarks>
        Public Shared CCDLight8 As Integer = -1
        ''' <summary>Loader Cassette Barcode 資料接收完成</summary>
        ''' <remarks></remarks>
        Public Shared CassetteBarcodeReceiveFinish1 As Integer = -1
        ''' <summary>Loader Cassette Mapping 資料接收完成</summary>
        ''' <remarks></remarks>
        Public Shared MappingDataReceiveFinish1 As Integer = -1
        ''' <summary>Loader Recipe 更換</summary>
        ''' <remarks></remarks>
        Public Shared ExchangeRecipe1 As Integer = -1
        ''' <summary>Loader 退 Cassette</summary>
        ''' <remarks></remarks>
        Public Shared CassetteAbort1 As Integer = -1
        ''' <summary>Unloader Cassette Barcode 資料接收完成</summary>
        ''' <remarks></remarks>
        Public Shared CassetteBarcodeReceiveFinish2 As Integer = -1
        ''' <summary>Unloader Cassette Mapping 資料接收完成</summary>
        ''' <remarks></remarks>
        Public Shared MappingDataReceiveFinish2 As Integer = -1
        ''' <summary>Unloader Recipe 更換</summary>
        ''' <remarks></remarks>
        Public Shared ExchangeRecipe2 As Integer = -1
        ''' <summary>Unloader 退 Cassette</summary>
        ''' <remarks></remarks>
        Public Shared CassetteAbort2 As Integer = -1
        ''' <summary>料盤氣缸 夾持</summary>
        ''' <remarks></remarks>
        Public Shared TrayClamper As Integer = -1
        ''' <summary>料盤汽缸 夾</summary>
        ''' <remarks></remarks>
        Public Shared TrayClamperOn As Integer = -1
        ''' <summary> 料盤汽缸 放</summary>
        ''' <remarks></remarks>
        Public Shared TrayClamperOff As Integer = -1
        ''' <summary>氣缸除膠 夾</summary>
        ''' <remarks></remarks>
        Public Shared ClearGlueClampOn As Integer = -1
        ''' <summary>氣缸除膠 放</summary>
        ''' <remarks></remarks>
        Public Shared ClearGlueClampOff As Integer = -1
        ''' <summary>散熱模組1</summary>
        ''' <remarks></remarks>
        Public Shared CoolDown1 As Integer = -1
        ''' <summary>散熱模組1</summary>
        ''' <remarks></remarks>
        Public Shared CoolDown2 As Integer = -1
        ''' <summary>散熱模組1</summary>
        ''' <remarks></remarks>
        Public Shared CoolDown3 As Integer = -1
        ''' <summary>散熱模組1</summary>
        ''' <remarks></remarks>
        Public Shared CoolDown4 As Integer = -1
        ''' <summary>[StageNo1 ValveNo2升降汽缸 上升動作]</summary>
        ''' <remarks></remarks>
        Public Shared ValveCylUp1 As Integer = -1
        ''' <summary>[StageNo1 ValveNo2升降汽缸 下降動作]</summary>
        ''' <remarks></remarks>
        Public Shared ValveCylDown1 As Integer = -1
        ''' <summary>[StageNo2 ValveNo2升降汽缸 上升動作]</summary>
        ''' <remarks></remarks>
        Public Shared ValveCylUp2 As Integer = -1
        ''' <summary>[StageNo2 ValveNo2升降汽缸 下降動作]</summary>
        ''' <remarks></remarks>
        Public Shared ValveCylDown2 As Integer = -1
        ''' <summary>[StageNo3 ValveNo2升降汽缸 上升動作]</summary>
        ''' <remarks></remarks>
        Public Shared ValveCylUp3 As Integer = -1
        ''' <summary>[StageNo3 ValveNo2升降汽缸 下降動作]</summary>
        ''' <remarks></remarks>
        Public Shared ValveCylDown3 As Integer = -1
        ''' <summary>[StageNo4 ValveNo2升降汽缸 上升動作]</summary>
        ''' <remarks></remarks>
        Public Shared ValveCylUp4 As Integer = -1
        ''' <summary>[StageNo4 ValveNo2升降汽缸 下降動作]</summary>
        ''' <remarks></remarks>
        Public Shared ValveCylDown4 As Integer = -1

        '=== 以下Wetco未使用 ==
        ''' <summary>硬體重置燈</summary>
        ''' <remarks></remarks>
        Public Shared ResetButtonLight As Integer = -1
        ''' <summary>
        ''' 除膠馬達動力 ON/OFF
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared ClearGlueMotorPowerOn As Integer = -1
        ''' <summary>
        ''' 除膠馬達轉向 CW/CCW
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared ClearGlueMotorCW As Integer = -1
        ''' <summary>
        ''' 閥1加熱開啓
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Valve1HeaterOn As Integer = -1
        ''' <summary>閥2加熱開啓</summary>
        ''' <remarks></remarks>
        Public Shared Valve2HeaterOn As Integer = -1
        ''' <summary>閥3加熱開啓</summary>
        ''' <remarks></remarks>
        Public Shared Valve3HeaterOn As Integer = -1
        ''' <summary>閥4加熱開啓</summary>
        ''' <remarks></remarks>
        Public Shared Valve4HeaterOn As Integer = -1
        ''' <summary>Stage2 Motor 煞車解除</summary>
        ''' <remarks></remarks>
        Public Shared Station2Unlock As Integer = -1
        ''' <summary>Z軸剎車解除</summary>
        ''' <remarks></remarks>
        Public Shared UnlockZAxis As Integer = -1                         'DO-Z軸煞車解除
        ''' <summary>位置1螺桿閥控制器重置</summary>
        ''' <remarks></remarks>
        Public Shared Screw1ControllerReset As Integer = -1               'DO-1號螺桿閥控制器reset
        ''' <summary>位置2螺桿閥控制器重置</summary>
        ''' <remarks></remarks>
        Public Shared Screw2ControllerReset As Integer = -1              'DO-2號螺桿閥控制器reset
        ''' <summary>加熱控制器重置</summary>
        ''' <remarks></remarks>
        Public Shared HeaterControllerReset As Integer = -1
        ''' <summary>加熱器電源</summary>
        ''' <remarks></remarks>
        Public Shared HeaterPower As Integer = -1
        ''' <summary>試點平台真空吸</summary>
        ''' <remarks></remarks>
        Public Shared DummyVacuum As Integer = -1 '試點平台真空
        ''' <summary>位置1 螺桿閥反轉</summary>
        ''' <remarks></remarks>
        Public Shared DispenserNo1ScrewValveCCW As Integer = -1          'DO-1號膠槍回吸(逆轉)
        ''' <summary>位置2 螺桿閥反轉</summary>
        ''' <remarks></remarks>
        Public Shared DispenserNo2ScrewValveCCW As Integer = -1          'DO-2號膠槍回吸(逆轉)
        ''' <summary>軸卡模組重置</summary>
        ''' <remarks></remarks>
        Public Shared MotionCardReset As Integer = -1                    'DO-軸卡Reset

        ''' <summary>視覺Bit0動作</summary>
        ''' <remarks></remarks>
        Public Shared CcdBit0 As Integer = -1                            'DO-CCD Bit0(0:為定位判斷,1:出膠判斷)
        ''' <summary>視覺Bit1動作</summary>
        ''' <remarks></remarks>
        Public Shared CcdBit1 As Integer = -1                            'DO-CCD Bit1
        ''' <summary>視覺重置</summary>
        ''' <remarks></remarks>
        Public Shared CcdReset As Integer = -1                          'DO-CCD Reset

        '20170215
        '接觸式測高
        ''' <summary>No.1/(2)_重啟測高感測器</summary>
        ''' <remarks></remarks>
        Public Shared ContactHeightReZero As Integer = -1
        ''' <summary>No.1/(2)_接觸式測高電磁閥</summary>
        ''' <remarks></remarks>
        Public Shared ContactHeightSolenoidValve As Integer = -1
        ''' <summary>閥1_閥體正壓 電磁閥</summary>
        ''' <remarks></remarks>
        Public Shared ValvePressure1 As Integer = -1
        ''' <summary>閥2_閥體正壓 電磁閥</summary>
        ''' <remarks></remarks>
        Public Shared ValvePressure2 As Integer = -1
        ''' <summary>閥3_閥體正壓 電磁閥</summary>
        ''' <remarks></remarks>
        Public Shared ValvePressure3 As Integer = -1
        ''' <summary>閥4_閥體正壓 電磁閥</summary>
        ''' <remarks></remarks>
        Public Shared ValvePressure4 As Integer = -1
        ''' <summary>站1 料盤頂升動作</summary>
        ''' <remarks></remarks>
        Public Shared Station1TopLiftUpDown As Integer = -1
        ''' <summary>站2 料盤頂升動作</summary>
        ''' <remarks></remarks>
        Public Shared Station2TopLiftUpDown As Integer = -1
        ''' <summary>站3 料盤頂升動作</summary>
        ''' <remarks></remarks>
        Public Shared Station3TopLiftUpDown As Integer = -1
        ''' <summary>站1 料盤真空動作</summary>
        ''' <remarks></remarks>
        Public Shared Station1ChuckVacuum As Integer = -1
        ''' <summary>軌道箝制器 放</summary>
        ''' <remarks></remarks>
        Public Shared HoldBack As Integer = -1
        ''' <summary>步進馬達電源</summary>
        ''' <remarks></remarks>
        Public Shared SteppingMotor As Integer = -1
        ''' <summary>System On</summary>
        ''' <remarks></remarks>
        Public Shared SystemOn As Integer = -1
        ''' <summary>真空Pump</summary>
        ''' <remarks></remarks>
        Public Shared Pump As Integer = -1
        ''' <summary>站1 加熱控制器動作</summary>
        ''' <remarks></remarks>
        Public Shared Station1Heating As Integer = -1
        ''' <summary>站2 加熱控制器動作</summary>
        ''' <remarks></remarks>
        Public Shared Station2Heating As Integer = -1
        ''' <summary>站3 加熱控制器動作</summary>
        ''' <remarks></remarks>
        Public Shared Station3Heating As Integer = -1

#Region "enmDO存取"

        ''' <summary>讀取DO索引記錄</summary>
        ''' <param name="strFileName"></param>
        ''' <remarks></remarks>
        Public Shared Sub LoadDOIndex(ByVal strFileName As String)
            Dim strSection As String
            strSection = "DOIndex"

            enmDO.BlueIndicator = CInt(ReadIniString(strSection, "BlueIndicator", strFileName, -1))
            enmDO.BoardAvailable = CInt(ReadIniString(strSection, "BoardAvailable", strFileName, -1))
            enmDO.BoardAvailable2 = CInt(ReadIniString(strSection, "BoardAvailable2", strFileName, -1))
            enmDO.Buzzer = CInt(ReadIniString(strSection, "Buzzer", strFileName, -1))
            enmDO.CcdBit0 = CInt(ReadIniString(strSection, "CcdBit0", strFileName, -1))
            enmDO.CcdBit1 = CInt(ReadIniString(strSection, "CcdBit1", strFileName, -1))
            enmDO.CcdImageTrigger = CInt(ReadIniString(strSection, "CcdImageTrigger", strFileName, -1))
            enmDO.CcdImageTrigger2 = CInt(ReadIniString(strSection, "CcdImageTrigger2", strFileName, -1))
            enmDO.CcdImageTrigger3 = CInt(ReadIniString(strSection, "CcdImageTrigger3", strFileName, -1))
            enmDO.CcdImageTrigger4 = CInt(ReadIniString(strSection, "CcdImageTrigger4", strFileName, -1))
            enmDO.CCDLight = CInt(ReadIniString(strSection, "CCDLight", strFileName, -1))
            enmDO.CCDLight2 = CInt(ReadIniString(strSection, "CCDLight2", strFileName, -1))
            enmDO.CCDLight3 = CInt(ReadIniString(strSection, "CCDLight3", strFileName, -1))
            enmDO.CCDLight4 = CInt(ReadIniString(strSection, "CCDLight4", strFileName, -1))
            enmDO.CCDLight5 = CInt(ReadIniString(strSection, "CCDLight5", strFileName, -1))
            enmDO.CCDLight6 = CInt(ReadIniString(strSection, "CCDLight6", strFileName, -1))
            enmDO.CCDLight7 = CInt(ReadIniString(strSection, "CCDLight7", strFileName, -1))
            enmDO.CCDLight8 = CInt(ReadIniString(strSection, "CCDLight8", strFileName, -1))
            enmDO.CcdReset = CInt(ReadIniString(strSection, "CcdReset", strFileName, -1))
            enmDO.ClearGlueClampOff = CInt(ReadIniString(strSection, "ClearGlueClampOff", strFileName, -1))
            enmDO.ClearGlueClampOn = CInt(ReadIniString(strSection, "ClearGlueClampOn", strFileName, -1))
            enmDO.DispenserNo1ScrewValveCCW = CInt(ReadIniString(strSection, "DispenserNo1ScrewValveCCW", strFileName, -1))
            enmDO.ValveCylUp1 = CInt(ReadIniString(strSection, "ValveCylUp1", strFileName, -1))
            enmDO.ValveCylDown1 = CInt(ReadIniString(strSection, "ValveCylDown1", strFileName, -1))
            enmDO.DispenserNo2ScrewValveCCW = CInt(ReadIniString(strSection, "DispenserNo2ScrewValveCCW", strFileName, -1))
            enmDO.DoorLock = CInt(ReadIniString(strSection, "DoorLock", strFileName, -1))
            enmDO.DoorLock2 = CInt(ReadIniString(strSection, "DoorLock2", strFileName, -1))
            enmDO.DummyVacuum = CInt(ReadIniString(strSection, "DummyVacuum", strFileName, -1))
            enmDO.SyringePressure1 = CInt(ReadIniString(strSection, "SyringePressure1", strFileName, -1))
            enmDO.SyringePressure2 = CInt(ReadIniString(strSection, "SyringePressure2", strFileName, -1))
            enmDO.GreenIndicator = CInt(ReadIniString(strSection, "GreenIndicator", strFileName, -1))
            enmDO.HeaterControllerReset = CInt(ReadIniString(strSection, "HeaterControllerReset", strFileName, -1))
            enmDO.HeaterCylinderDown1 = CInt(ReadIniString(strSection, "HeaterCylinderDown1", strFileName, -1))
            enmDO.HeaterCylinderDown2 = CInt(ReadIniString(strSection, "HeaterCylinderDown2", strFileName, -1))
            enmDO.HeaterCylinderDown3 = CInt(ReadIniString(strSection, "HeaterCylinderDown3", strFileName, -1))
            enmDO.HeaterCylinderDown4 = CInt(ReadIniString(strSection, "HeaterCylinderDown4", strFileName, -1))
            enmDO.HeaterCylinderDown5 = CInt(ReadIniString(strSection, "HeaterCylinderDown5", strFileName, -1))
            enmDO.HeaterCylinderDown6 = CInt(ReadIniString(strSection, "HeaterCylinderDown6", strFileName, -1))
            enmDO.HeaterCylinderDown7 = CInt(ReadIniString(strSection, "HeaterCylinderDown7", strFileName, -1))
            enmDO.HeaterCylinderDown8 = CInt(ReadIniString(strSection, "HeaterCylinderDown8", strFileName, -1))
            enmDO.HeaterCylinderDown9 = CInt(ReadIniString(strSection, "HeaterCylinderDown9", strFileName, -1))
            enmDO.HeaterCylinderDown10 = CInt(ReadIniString(strSection, "HeaterCylinderDown10", strFileName, -1))
            enmDO.HeaterCylinderDown11 = CInt(ReadIniString(strSection, "HeaterCylinderDown11", strFileName, -1))
            enmDO.HeaterCylinderDown12 = CInt(ReadIniString(strSection, "HeaterCylinderDown12", strFileName, -1))
            enmDO.HeaterCylinderUp1 = CInt(ReadIniString(strSection, "HeaterCylinderUp1", strFileName, -1))
            enmDO.HeaterCylinderUp2 = CInt(ReadIniString(strSection, "HeaterCylinderUp2", strFileName, -1))
            enmDO.HeaterCylinderUp3 = CInt(ReadIniString(strSection, "HeaterCylinderUp3", strFileName, -1))
            enmDO.HeaterCylinderUp4 = CInt(ReadIniString(strSection, "HeaterCylinderUp4", strFileName, -1))
            enmDO.HeaterCylinderUp5 = CInt(ReadIniString(strSection, "HeaterCylinderUp5", strFileName, -1))
            enmDO.HeaterCylinderUp6 = CInt(ReadIniString(strSection, "HeaterCylinderUp6", strFileName, -1))
            enmDO.HeaterCylinderUp7 = CInt(ReadIniString(strSection, "HeaterCylinderUp7", strFileName, -1))
            enmDO.HeaterCylinderUp8 = CInt(ReadIniString(strSection, "HeaterCylinderUp8", strFileName, -1))
            enmDO.HeaterCylinderUp9 = CInt(ReadIniString(strSection, "HeaterCylinderUp9", strFileName, -1))
            enmDO.HeaterCylinderUp10 = CInt(ReadIniString(strSection, "HeaterCylinderUp10", strFileName, -1))
            enmDO.HeaterCylinderUp11 = CInt(ReadIniString(strSection, "HeaterCylinderUp11", strFileName, -1))
            enmDO.HeaterCylinderUp12 = CInt(ReadIniString(strSection, "HeaterCylinderUp12", strFileName, -1))
            enmDO.HeaterOn1 = CInt(ReadIniString(strSection, "HeaterOn1", strFileName, -1))
            enmDO.HeaterOn2 = CInt(ReadIniString(strSection, "HeaterOn2", strFileName, -1))
            enmDO.HeaterOn3 = CInt(ReadIniString(strSection, "HeaterOn3", strFileName, -1))
            enmDO.HeaterOn4 = CInt(ReadIniString(strSection, "HeaterOn4", strFileName, -1))
            enmDO.HeaterOn5 = CInt(ReadIniString(strSection, "HeaterOn5", strFileName, -1))
            enmDO.HeaterOn6 = CInt(ReadIniString(strSection, "HeaterOn6", strFileName, -1))
            enmDO.HeaterOn7 = CInt(ReadIniString(strSection, "HeaterOn7", strFileName, -1))
            enmDO.HeaterOn8 = CInt(ReadIniString(strSection, "HeaterOn8", strFileName, -1))
            enmDO.HeaterOn9 = CInt(ReadIniString(strSection, "HeaterOn9", strFileName, -1))
            enmDO.HeaterOn10 = CInt(ReadIniString(strSection, "HeaterOn10", strFileName, -1))
            enmDO.HeaterOn11 = CInt(ReadIniString(strSection, "HeaterOn11", strFileName, -1))
            enmDO.HeaterOn12 = CInt(ReadIniString(strSection, "HeaterOn12", strFileName, -1))
            enmDO.HeaterPower = CInt(ReadIniString(strSection, "HeaterPower", strFileName, -1))
            enmDO.HoldBack = CInt(ReadIniString(strSection, "HoldBack", strFileName, -1))
            enmDO.MachineReadyToRecieve = CInt(ReadIniString(strSection, "MachineReadyToRecieve", strFileName, -1))
            enmDO.MachineReadyToRecieve2 = CInt(ReadIniString(strSection, "MachineReadyToRecieve2", strFileName, -1))
            enmDO.MotionCardReset = CInt(ReadIniString(strSection, "MotionCardReset", strFileName, -1))
            enmDO.MoveInMotorCCW = CInt(ReadIniString(strSection, "MoveInMotorCCW", strFileName, -1))
            enmDO.MoveInMotorCCW2 = CInt(ReadIniString(strSection, "MoveInMotorCCW2", strFileName, -1))
            enmDO.MoveInMotorCW = CInt(ReadIniString(strSection, "MoveInMotorCW", strFileName, -1))
            enmDO.MoveInMotorCW2 = CInt(ReadIniString(strSection, "MoveInMotorCW2", strFileName, -1))
            enmDO.MoveInMotorReset = CInt(ReadIniString(strSection, "MoveInMotorReset", strFileName, -1))
            enmDO.MoveInMotorReset2 = CInt(ReadIniString(strSection, "MoveInMotorReset2", strFileName, -1))
            enmDO.MoveInMotorSlow = CInt(ReadIniString(strSection, "MoveInMotorSlow", strFileName, -1))
            enmDO.MoveInMotorSlow2 = CInt(ReadIniString(strSection, "MoveInMotorSlow2", strFileName, -1))
            enmDO.PauseButtonLight = CInt(ReadIniString(strSection, "PauseButtonLight", strFileName, -1))
            enmDO.PauseButtonLight2 = CInt(ReadIniString(strSection, "PauseButtonLight2", strFileName, -1))
            enmDO.Pump = CInt(ReadIniString(strSection, "Pump", strFileName, -1))
            enmDO.Purge = CInt(ReadIniString(strSection, "Purge", strFileName, -1))
            enmDO.PurgeVacuum2 = CInt(ReadIniString(strSection, "PurgeVacuum2", strFileName, -1))
            enmDO.PurgeVacuum3 = CInt(ReadIniString(strSection, "PurgeVacuum3", strFileName, -1))
            enmDO.PurgeVacuum4 = CInt(ReadIniString(strSection, "PurgeVacuum4", strFileName, -1))
            enmDO.PurgeVacuumBreaker = CInt(ReadIniString(strSection, "PurgeVacuumBreaker", strFileName, -1))
            enmDO.PurgeVacuumBreaker2 = CInt(ReadIniString(strSection, "PurgeVacuumBreaker2", strFileName, -1))
            enmDO.PurgeVacuumBreaker3 = CInt(ReadIniString(strSection, "PurgeVacuumBreaker3", strFileName, -1))
            enmDO.PurgeVacuumBreaker4 = CInt(ReadIniString(strSection, "PurgeVacuumBreaker4", strFileName, -1))
            enmDO.RedIndicator = CInt(ReadIniString(strSection, "RedIndicator", strFileName, -1))
            enmDO.Screw1ControllerReset = CInt(ReadIniString(strSection, "Screw1ControllerReset", strFileName, -1))
            enmDO.Screw2ControllerReset = CInt(ReadIniString(strSection, "Screw2ControllerReset", strFileName, -1))
            enmDO.StartButtonLight = CInt(ReadIniString(strSection, "StartButtonLight", strFileName, -1))
            enmDO.StartButtonLight2 = CInt(ReadIniString(strSection, "StartButtonLight2", strFileName, -1))
            enmDO.Station1ChuckVacuum = CInt(ReadIniString(strSection, "Station1VacuumOn", strFileName, -1))
            enmDO.Station1Heating = CInt(ReadIniString(strSection, "Station1Heating", strFileName, -1))
            enmDO.Station1StopperDown = CInt(ReadIniString(strSection, "Station1StopperDown", strFileName, -1))
            enmDO.Station1StopperUpDown = CInt(ReadIniString(strSection, "Station1StopperUp", strFileName, -1))
            enmDO.Station1TopLiftUpDown = CInt(ReadIniString(strSection, "Station1Up", strFileName, -1))
            enmDO.Station2ChuckVacuum = CInt(ReadIniString(strSection, "Station2ChuckVacuum", strFileName, -1))
            enmDO.Station2ChuckVacuum2 = CInt(ReadIniString(strSection, "Station2ChuckVacuum2", strFileName, -1))
            enmDO.Station2ChuckVacuum3 = CInt(ReadIniString(strSection, "Station2ChuckVacuum3", strFileName, -1))
            enmDO.Station2ChuckVacuum4 = CInt(ReadIniString(strSection, "Station2ChuckVacuum4", strFileName, -1))
            enmDO.Station2ChuckVacuum5 = CInt(ReadIniString(strSection, "Station2ChuckVacuum5", strFileName, -1))
            enmDO.Station2ChuckVacuum6 = CInt(ReadIniString(strSection, "Station2ChuckVacuum6", strFileName, -1))
            enmDO.Station2ChuckVacuumBreak1 = CInt(ReadIniString(strSection, "Station2ChuckVacuumBreak1", strFileName, -1))
            enmDO.Station2ChuckVacuumBreak2 = CInt(ReadIniString(strSection, "Station2ChuckVacuumBreak2", strFileName, -1))
            enmDO.Station2ChuckVacuumBreak3 = CInt(ReadIniString(strSection, "Station2ChuckVacuumBreak3", strFileName, -1))
            enmDO.Station2ChuckVacuumBreak4 = CInt(ReadIniString(strSection, "Station2ChuckVacuumBreak4", strFileName, -1))
            enmDO.Station2ChuckVacuumBreak5 = CInt(ReadIniString(strSection, "Station2ChuckVacuumBreak5", strFileName, -1))
            enmDO.Station2ChuckVacuumBreak6 = CInt(ReadIniString(strSection, "Station2ChuckVacuumBreak6", strFileName, -1))
            enmDO.Station2Heating = CInt(ReadIniString(strSection, "Station2Heating", strFileName, -1))
            enmDO.Station2StopperDown = CInt(ReadIniString(strSection, "Station2StopperDown", strFileName, -1))
            enmDO.Station2StopperUp = CInt(ReadIniString(strSection, "Station2StopperUp", strFileName, -1))
            enmDO.Station2TopLiftUpDown = CInt(ReadIniString(strSection, "Station2Up", strFileName, -1))
            enmDO.Station2Unlock = CInt(ReadIniString(strSection, "Station2Unlock", strFileName, -1))
            enmDO.Station3ChuckVacuum = CInt(ReadIniString(strSection, "Station3ChuckVacuum", strFileName, -1))
            enmDO.Station3ChuckVacuum2 = CInt(ReadIniString(strSection, "Station3ChuckVacuum2", strFileName, -1))
            enmDO.Station3ChuckVacuum3 = CInt(ReadIniString(strSection, "Station3ChuckVacuum3", strFileName, -1))
            enmDO.Station3ChuckVacuum4 = CInt(ReadIniString(strSection, "Station3ChuckVacuum4", strFileName, -1))
            enmDO.Station3ChuckVacuum5 = CInt(ReadIniString(strSection, "Station3ChuckVacuum5", strFileName, -1))
            enmDO.Station3ChuckVacuum6 = CInt(ReadIniString(strSection, "Station3ChuckVacuum6", strFileName, -1))
            enmDO.Station3ChuckVacuumBreak1 = CInt(ReadIniString(strSection, "Station3ChuckVacuumBreak1", strFileName, -1))
            enmDO.Station3ChuckVacuumBreak2 = CInt(ReadIniString(strSection, "Station3ChuckVacuumBreak2", strFileName, -1))
            enmDO.Station3ChuckVacuumBreak3 = CInt(ReadIniString(strSection, "Station3ChuckVacuumBreak3", strFileName, -1))
            enmDO.Station3ChuckVacuumBreak4 = CInt(ReadIniString(strSection, "Station3ChuckVacuumBreak4", strFileName, -1))
            enmDO.Station3ChuckVacuumBreak5 = CInt(ReadIniString(strSection, "Station3ChuckVacuumBreak5", strFileName, -1))
            enmDO.Station3ChuckVacuumBreak6 = CInt(ReadIniString(strSection, "Station3ChuckVacuumBreak6", strFileName, -1))
            enmDO.Station3Heating = CInt(ReadIniString(strSection, "Station3Heating", strFileName, -1))
            enmDO.Station3StopperDown = CInt(ReadIniString(strSection, "Station3StopperDown", strFileName, -1))
            enmDO.Station3StopperUp = CInt(ReadIniString(strSection, "Station3StopperUp", strFileName, -1))
            enmDO.Station3TopLiftUpDown = CInt(ReadIniString(strSection, "Station3Up", strFileName, -1))
            enmDO.SteppingMotor = CInt(ReadIniString(strSection, "SteppingMotor", strFileName, -1))
            enmDO.SyringePressure3 = CInt(ReadIniString(strSection, "SyringePressure3", strFileName, -1))
            enmDO.SyringePressure4 = CInt(ReadIniString(strSection, "SyringePressure4", strFileName, -1))
            enmDO.SystemAlarm = CInt(ReadIniString(strSection, "SystemAlarm", strFileName, -1))
            enmDO.SystemAlarm2 = CInt(ReadIniString(strSection, "SystemAlarm2", strFileName, -1))
            enmDO.SystemOn = CInt(ReadIniString(strSection, "SystemOn", strFileName, -1))
            enmDO.TrayClamper = CInt(ReadIniString(strSection, "TrayClamper", strFileName, -1))
            enmDO.UnlockZAxis = CInt(ReadIniString(strSection, "UnlockZAxis", strFileName, -1))
            enmDO.ValveAugerDir1 = CInt(ReadIniString(strSection, "ValveAugerDir1", strFileName, -1))
            enmDO.ValveAugerDir2 = CInt(ReadIniString(strSection, "ValveAugerDir2", strFileName, -1))
            enmDO.ValveAugerDir3 = CInt(ReadIniString(strSection, "ValveAugerDir3", strFileName, -1))
            enmDO.ValveAugerDir4 = CInt(ReadIniString(strSection, "ValveAugerDir4", strFileName, -1))
            enmDO.ValvePressure1 = CInt(ReadIniString(strSection, "ValvePressure1", strFileName, -1))
            enmDO.ValvePressure2 = CInt(ReadIniString(strSection, "ValvePressure2", strFileName, -1))
            enmDO.ValvePressure3 = CInt(ReadIniString(strSection, "ValvePressure3", strFileName, -1))
            enmDO.ValvePressure4 = CInt(ReadIniString(strSection, "ValvePressure4", strFileName, -1))

            enmDO.YellowIndicator = CInt(ReadIniString(strSection, "YellowIndicator", strFileName, -1))
            enmDO.ValveControllerReset1 = CInt(ReadIniString(strSection, "ValveControllerReset1", strFileName, -1))
            enmDO.ValveControllerReset2 = CInt(ReadIniString(strSection, "ValveControllerReset2", strFileName, -1))
            enmDO.ValveControllerReset3 = CInt(ReadIniString(strSection, "ValveControllerReset3", strFileName, -1))
            enmDO.ValveControllerReset4 = CInt(ReadIniString(strSection, "ValveControllerReset4", strFileName, -1))
            enmDO.DispensingTrigger1 = CInt(ReadIniString(strSection, "DispensingTrigger1", strFileName, -1))
            enmDO.DispensingTrigger2 = CInt(ReadIniString(strSection, "DispensingTrigger2", strFileName, -1))
            enmDO.DispensingTrigger3 = CInt(ReadIniString(strSection, "DispensingTrigger3", strFileName, -1))
            enmDO.DispensingTrigger4 = CInt(ReadIniString(strSection, "DispensingTrigger4", strFileName, -1))
            enmDO.TriggerBoardReset1 = CInt(ReadIniString(strSection, "TriggerBoardReset1", strFileName, -1))
            enmDO.TriggerBoardReset2 = CInt(ReadIniString(strSection, "TriggerBoardReset2", strFileName, -1))
            enmDO.TriggerBoardReset3 = CInt(ReadIniString(strSection, "TriggerBoardReset3", strFileName, -1))
            enmDO.TriggerBoardReset4 = CInt(ReadIniString(strSection, "TriggerBoardReset4", strFileName, -1))
            enmDO.LaserReaderReset1 = CInt(ReadIniString(strSection, "LaserReaderReset1", strFileName, -1))
            enmDO.LaserReaderReset2 = CInt(ReadIniString(strSection, "LaserReaderReset2", strFileName, -1))
            enmDO.LaserReaderReset3 = CInt(ReadIniString(strSection, "LaserReaderReset3", strFileName, -1))
            enmDO.ResetLaserReader4 = CInt(ReadIniString(strSection, "ResetLaserReader4", strFileName, -1))
            enmDO.ClearGlueMotorPowerOn = CInt(ReadIniString(strSection, "ClearGlueMotorPowerOn", strFileName, -1))
            enmDO.ClearGlueMotorCW = CInt(ReadIniString(strSection, "ClearGlueMotorCW", strFileName, -1))
            enmDO.Station2ChuckVacuumBreak1 = CInt(ReadIniString(strSection, "Station2ChuckVacuumBreak1", strFileName, -1))
            enmDO.Station2ChuckVacuum = CInt(ReadIniString(strSection, "Station2ChuckVacuum", strFileName, -1))
            enmDO.Valve1HeaterOn = CInt(ReadIniString(strSection, "Valve1HeaterOn", strFileName, -1))
            enmDO.Valve2HeaterOn = CInt(ReadIniString(strSection, "Valve2HeaterOn", strFileName, -1))
            enmDO.Valve3HeaterOn = CInt(ReadIniString(strSection, "Valve3HeaterOn", strFileName, -1))
            enmDO.Valve4HeaterOn = CInt(ReadIniString(strSection, "Valve4HeaterOn", strFileName, -1))
            enmDO.TrayClamperOn = CInt(ReadIniString(strSection, "TrayClamperOn", strFileName, -1))
            enmDO.TrayClamperOff = CInt(ReadIniString(strSection, "TrayClamperOff", strFileName, -1))
            enmDO.CassetteBarcodeReceiveFinish1 = CInt(ReadIniString(strSection, "CassetteBarcodeReceiveFinish1", strFileName, -1))
            enmDO.MappingDataReceiveFinish1 = CInt(ReadIniString(strSection, "MappingDataReceiveFinish1", strFileName, -1))
            enmDO.ExchangeRecipe1 = CInt(ReadIniString(strSection, "ExchangeRecipe1", strFileName, -1))
            enmDO.CassetteAbort1 = CInt(ReadIniString(strSection, "CassetteAbort1", strFileName, -1))
            enmDO.CassetteBarcodeReceiveFinish2 = CInt(ReadIniString(strSection, "CassetteBarcodeReceiveFinish2", strFileName, -1))
            enmDO.MappingDataReceiveFinish2 = CInt(ReadIniString(strSection, "MappingDataReceiveFinish2", strFileName, -1))
            enmDO.ExchangeRecipe2 = CInt(ReadIniString(strSection, "ExchangeRecipe2", strFileName, -1))
            enmDO.CassetteAbort2 = CInt(ReadIniString(strSection, "CassetteAbort2", strFileName, -1))
            enmDO.CoolDown1 = CInt(ReadIniString(strSection, "CoolDown1", strFileName, -1)) 'Soni + 2016.09.16 散熱模組接點
            enmDO.CoolDown2 = CInt(ReadIniString(strSection, "CoolDown2", strFileName, -1)) 'Soni + 2016.09.16 散熱模組接點
            enmDO.CoolDown3 = CInt(ReadIniString(strSection, "CoolDown3", strFileName, -1)) 'Soni + 2016.09.16 散熱模組接點
            enmDO.CoolDown4 = CInt(ReadIniString(strSection, "CoolDown4", strFileName, -1)) 'Soni + 2016.09.16 散熱模組接點
            enmDO.ValveCylUp1 = CInt(ReadIniString(strSection, "ValveCylUp1", strFileName, -1))
            enmDO.ValveCylUp2 = CInt(ReadIniString(strSection, "ValveCylUp2", strFileName, -1))
            enmDO.ValveCylUp3 = CInt(ReadIniString(strSection, "ValveCylUp3", strFileName, -1))
            enmDO.ValveCylUp4 = CInt(ReadIniString(strSection, "ValveCylUp4", strFileName, -1))
            enmDO.ValveCylDown1 = CInt(ReadIniString(strSection, "ValveCylDown1", strFileName, -1))
            enmDO.ValveCylDown2 = CInt(ReadIniString(strSection, "ValveCylDown2", strFileName, -1))
            enmDO.ValveCylDown3 = CInt(ReadIniString(strSection, "ValveCylDown3", strFileName, -1))
            enmDO.ValveCylDown4 = CInt(ReadIniString(strSection, "ValveCylDown4", strFileName, -1))
            enmDO.ContactHeightReZero = CInt(ReadIniString(strSection, "ContactHeightReZero", strFileName, -1))
            enmDO.ContactHeightSolenoidValve = CInt(ReadIniString(strSection, "ContactHeightSolenoidValve", strFileName, -1))
            enmDO.Max = CInt(ReadIniString(strSection, "Max", strFileName, -1))
        End Sub
        ''' <summary>儲存DO索引記錄</summary>
        ''' <param name="strFileName"></param>
        ''' <remarks></remarks>
        Public Shared Sub SaveDOIndex(ByVal strFileName As String)
            Dim strSection As String
            strSection = "DOIndex"
            Call SaveIniString(strSection, "BlueIndicator", CInt(enmDO.BlueIndicator), strFileName)
            Call SaveIniString(strSection, "BoardAvailable", CInt(enmDO.BoardAvailable), strFileName)
            Call SaveIniString(strSection, "BoardAvailable2", CInt(enmDO.BoardAvailable2), strFileName)
            Call SaveIniString(strSection, "Buzzer", CInt(enmDO.Buzzer), strFileName)
            Call SaveIniString(strSection, "CcdBit0", CInt(enmDO.CcdBit0), strFileName)
            Call SaveIniString(strSection, "CcdBit1", CInt(enmDO.CcdBit1), strFileName)
            Call SaveIniString(strSection, "CcdImageTrigger", CInt(enmDO.CcdImageTrigger), strFileName)
            Call SaveIniString(strSection, "CcdImageTrigger2", CInt(enmDO.CcdImageTrigger2), strFileName)
            Call SaveIniString(strSection, "CcdImageTrigger3", CInt(enmDO.CcdImageTrigger3), strFileName)
            Call SaveIniString(strSection, "CcdImageTrigger4", CInt(enmDO.CcdImageTrigger4), strFileName)
            Call SaveIniString(strSection, "CCDLight", CInt(enmDO.CCDLight), strFileName)
            Call SaveIniString(strSection, "CCDLight2", CInt(enmDO.CCDLight2), strFileName)
            Call SaveIniString(strSection, "CCDLight3", CInt(enmDO.CCDLight3), strFileName)
            Call SaveIniString(strSection, "CCDLight4", CInt(enmDO.CCDLight4), strFileName)
            Call SaveIniString(strSection, "CCDLight5", CInt(enmDO.CCDLight5), strFileName)
            Call SaveIniString(strSection, "CCDLight6", CInt(enmDO.CCDLight6), strFileName)
            Call SaveIniString(strSection, "CCDLight7", CInt(enmDO.CCDLight7), strFileName)
            Call SaveIniString(strSection, "CCDLight8", CInt(enmDO.CCDLight8), strFileName)
            Call SaveIniString(strSection, "CcdReset", CInt(enmDO.CcdReset), strFileName)
            Call SaveIniString(strSection, "ClearGlueClampOff", CInt(enmDO.ClearGlueClampOff), strFileName)
            Call SaveIniString(strSection, "ClearGlueClampOn", CInt(enmDO.ClearGlueClampOn), strFileName)
            Call SaveIniString(strSection, "DispenserNo1ScrewValveCCW", CInt(enmDO.DispenserNo1ScrewValveCCW), strFileName)
            Call SaveIniString(strSection, "ValveCylUp1", CInt(enmDO.ValveCylUp1), strFileName)
            Call SaveIniString(strSection, "ValveCylDown1", CInt(enmDO.ValveCylDown1), strFileName)
            Call SaveIniString(strSection, "DispenserNo2ScrewValveCCW", CInt(enmDO.DispenserNo2ScrewValveCCW), strFileName)
            Call SaveIniString(strSection, "DoorLock", CInt(enmDO.DoorLock), strFileName)
            Call SaveIniString(strSection, "DoorLock2", CInt(enmDO.DoorLock2), strFileName)
            Call SaveIniString(strSection, "DummyVacuum", CInt(enmDO.DummyVacuum), strFileName)
            Call SaveIniString(strSection, "SyringePressure1", CInt(enmDO.SyringePressure1), strFileName)
            Call SaveIniString(strSection, "SyringePressure2", CInt(enmDO.SyringePressure2), strFileName)
            Call SaveIniString(strSection, "GreenIndicator", CInt(enmDO.GreenIndicator), strFileName)
            Call SaveIniString(strSection, "HeaterControllerReset", CInt(enmDO.HeaterControllerReset), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderDown1", CInt(enmDO.HeaterCylinderDown1), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderDown2", CInt(enmDO.HeaterCylinderDown2), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderDown3", CInt(enmDO.HeaterCylinderDown3), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderDown4", CInt(enmDO.HeaterCylinderDown4), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderDown5", CInt(enmDO.HeaterCylinderDown5), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderDown6", CInt(enmDO.HeaterCylinderDown6), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderDown7", CInt(enmDO.HeaterCylinderDown7), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderDown8", CInt(enmDO.HeaterCylinderDown8), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderDown9", CInt(enmDO.HeaterCylinderDown9), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderDown10", CInt(enmDO.HeaterCylinderDown10), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderDown11", CInt(enmDO.HeaterCylinderDown11), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderDown12", CInt(enmDO.HeaterCylinderDown12), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderUp1", CInt(enmDO.HeaterCylinderUp1), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderUp2", CInt(enmDO.HeaterCylinderUp2), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderUp3", CInt(enmDO.HeaterCylinderUp3), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderUp4", CInt(enmDO.HeaterCylinderUp4), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderUp5", CInt(enmDO.HeaterCylinderUp5), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderUp6", CInt(enmDO.HeaterCylinderUp6), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderUp7", CInt(enmDO.HeaterCylinderUp7), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderUp8", CInt(enmDO.HeaterCylinderUp8), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderUp9", CInt(enmDO.HeaterCylinderUp9), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderUp10", CInt(enmDO.HeaterCylinderUp10), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderUp11", CInt(enmDO.HeaterCylinderUp11), strFileName)
            Call SaveIniString(strSection, "HeaterCylinderUp12", CInt(enmDO.HeaterCylinderUp12), strFileName)
            Call SaveIniString(strSection, "HeaterOn1", CInt(enmDO.HeaterOn1), strFileName)
            Call SaveIniString(strSection, "HeaterOn2", CInt(enmDO.HeaterOn2), strFileName)
            Call SaveIniString(strSection, "HeaterOn3", CInt(enmDO.HeaterOn3), strFileName)
            Call SaveIniString(strSection, "HeaterOn4", CInt(enmDO.HeaterOn4), strFileName)
            Call SaveIniString(strSection, "HeaterOn5", CInt(enmDO.HeaterOn5), strFileName)
            Call SaveIniString(strSection, "HeaterOn6", CInt(enmDO.HeaterOn6), strFileName)
            Call SaveIniString(strSection, "HeaterOn7", CInt(enmDO.HeaterOn7), strFileName)
            Call SaveIniString(strSection, "HeaterOn8", CInt(enmDO.HeaterOn8), strFileName)
            Call SaveIniString(strSection, "HeaterOn9", CInt(enmDO.HeaterOn9), strFileName)
            Call SaveIniString(strSection, "HeaterOn10", CInt(enmDO.HeaterOn10), strFileName)
            Call SaveIniString(strSection, "HeaterOn11", CInt(enmDO.HeaterOn11), strFileName)
            Call SaveIniString(strSection, "HeaterOn12", CInt(enmDO.HeaterOn12), strFileName)
            Call SaveIniString(strSection, "HeaterPower", CInt(enmDO.HeaterPower), strFileName)
            Call SaveIniString(strSection, "HoldBack", CInt(enmDO.HoldBack), strFileName)
            Call SaveIniString(strSection, "MachineReadyToRecieve", CInt(enmDO.MachineReadyToRecieve), strFileName)
            Call SaveIniString(strSection, "MachineReadyToRecieve2", CInt(enmDO.MachineReadyToRecieve2), strFileName)
            Call SaveIniString(strSection, "MotionCardReset", CInt(enmDO.MotionCardReset), strFileName)
            Call SaveIniString(strSection, "MoveInMotorCCW", CInt(enmDO.MoveInMotorCCW), strFileName)
            Call SaveIniString(strSection, "MoveInMotorCCW2", CInt(enmDO.MoveInMotorCCW2), strFileName)
            Call SaveIniString(strSection, "MoveInMotorCW", CInt(enmDO.MoveInMotorCW), strFileName)
            Call SaveIniString(strSection, "MoveInMotorCW2", CInt(enmDO.MoveInMotorCW2), strFileName)
            Call SaveIniString(strSection, "MoveInMotorReset", CInt(enmDO.MoveInMotorReset), strFileName)
            Call SaveIniString(strSection, "MoveInMotorReset2", CInt(enmDO.MoveInMotorReset2), strFileName)
            Call SaveIniString(strSection, "MoveInMotorSlow", CInt(enmDO.MoveInMotorSlow), strFileName)
            Call SaveIniString(strSection, "MoveInMotorSlow2", CInt(enmDO.MoveInMotorSlow2), strFileName)
            Call SaveIniString(strSection, "PauseButtonLight", CInt(enmDO.PauseButtonLight), strFileName)
            Call SaveIniString(strSection, "PauseButtonLight2", CInt(enmDO.PauseButtonLight2), strFileName)
            Call SaveIniString(strSection, "Pump", CInt(enmDO.Pump), strFileName)
            Call SaveIniString(strSection, "Purge", CInt(enmDO.Purge), strFileName)
            Call SaveIniString(strSection, "PurgeVacuum2", CInt(enmDO.PurgeVacuum2), strFileName)
            Call SaveIniString(strSection, "PurgeVacuum3", CInt(enmDO.PurgeVacuum3), strFileName)
            Call SaveIniString(strSection, "PurgeVacuum4", CInt(enmDO.PurgeVacuum4), strFileName)
            Call SaveIniString(strSection, "PurgeVacuumBreaker", CInt(enmDO.PurgeVacuumBreaker), strFileName)
            Call SaveIniString(strSection, "PurgeVacuumBreaker2", CInt(enmDO.PurgeVacuumBreaker2), strFileName)
            Call SaveIniString(strSection, "PurgeVacuumBreaker3", CInt(enmDO.PurgeVacuumBreaker3), strFileName)
            Call SaveIniString(strSection, "PurgeVacuumBreaker4", CInt(enmDO.PurgeVacuumBreaker4), strFileName)
            Call SaveIniString(strSection, "RedIndicator", CInt(enmDO.RedIndicator), strFileName)
            Call SaveIniString(strSection, "Screw1ControllerReset", CInt(enmDO.Screw1ControllerReset), strFileName)
            Call SaveIniString(strSection, "Screw2ControllerReset", CInt(enmDO.Screw2ControllerReset), strFileName)
            Call SaveIniString(strSection, "StartButtonLight", CInt(enmDO.StartButtonLight), strFileName)
            Call SaveIniString(strSection, "StartButtonLight2", CInt(enmDO.StartButtonLight2), strFileName)
            Call SaveIniString(strSection, "Station1VacuumOn", CInt(enmDO.Station1ChuckVacuum), strFileName)
            Call SaveIniString(strSection, "Station1Heating", CInt(enmDO.Station1Heating), strFileName)
            Call SaveIniString(strSection, "Station1StopperDown", CInt(enmDO.Station1StopperDown), strFileName)
            Call SaveIniString(strSection, "Station1BlockUp", CInt(enmDO.Station1StopperUpDown), strFileName)
            Call SaveIniString(strSection, "Station1Up", CInt(enmDO.Station1TopLiftUpDown), strFileName)
            Call SaveIniString(strSection, "Station2ChuckVacuum", CInt(enmDO.Station2ChuckVacuum), strFileName)
            Call SaveIniString(strSection, "Station2ChuckVacuum2", CInt(enmDO.Station2ChuckVacuum2), strFileName)
            Call SaveIniString(strSection, "Station2ChuckVacuum3", CInt(enmDO.Station2ChuckVacuum3), strFileName)
            Call SaveIniString(strSection, "Station2ChuckVacuum4", CInt(enmDO.Station2ChuckVacuum4), strFileName)
            Call SaveIniString(strSection, "Station2ChuckVacuum5", CInt(enmDO.Station2ChuckVacuum5), strFileName)
            Call SaveIniString(strSection, "Station2ChuckVacuum6", CInt(enmDO.Station2ChuckVacuum6), strFileName)
            Call SaveIniString(strSection, "Station2ChuckVacuumBreak1", CInt(enmDO.Station2ChuckVacuumBreak1), strFileName)
            Call SaveIniString(strSection, "Station2ChuckVacuumBreak2", CInt(enmDO.Station2ChuckVacuumBreak2), strFileName)
            Call SaveIniString(strSection, "Station2ChuckVacuumBreak3", CInt(enmDO.Station2ChuckVacuumBreak3), strFileName)
            Call SaveIniString(strSection, "Station2ChuckVacuumBreak4", CInt(enmDO.Station2ChuckVacuumBreak4), strFileName)
            Call SaveIniString(strSection, "Station2ChuckVacuumBreak5", CInt(enmDO.Station2ChuckVacuumBreak5), strFileName)
            Call SaveIniString(strSection, "Station2ChuckVacuumBreak6", CInt(enmDO.Station2ChuckVacuumBreak6), strFileName)
            Call SaveIniString(strSection, "Station2Heating", CInt(enmDO.Station2Heating), strFileName)
            Call SaveIniString(strSection, "Station2StopperDown", CInt(enmDO.Station2StopperDown), strFileName)
            Call SaveIniString(strSection, "Station2BlockUp", CInt(enmDO.Station2StopperUp), strFileName)
            Call SaveIniString(strSection, "Station2Up", CInt(enmDO.Station2TopLiftUpDown), strFileName)
            Call SaveIniString(strSection, "Station2Unlock", CInt(enmDO.Station2Unlock), strFileName)
            Call SaveIniString(strSection, "Station3ChuckVacuum", CInt(enmDO.Station3ChuckVacuum), strFileName)
            Call SaveIniString(strSection, "Station3ChuckVacuum2", CInt(enmDO.Station3ChuckVacuum2), strFileName)
            Call SaveIniString(strSection, "Station3ChuckVacuum3", CInt(enmDO.Station3ChuckVacuum3), strFileName)
            Call SaveIniString(strSection, "Station3ChuckVacuum4", CInt(enmDO.Station3ChuckVacuum4), strFileName)
            Call SaveIniString(strSection, "Station3ChuckVacuum5", CInt(enmDO.Station3ChuckVacuum5), strFileName)
            Call SaveIniString(strSection, "Station3ChuckVacuum6", CInt(enmDO.Station3ChuckVacuum6), strFileName)
            Call SaveIniString(strSection, "Station3ChuckVacuumBreak1", CInt(enmDO.Station3ChuckVacuumBreak1), strFileName)
            Call SaveIniString(strSection, "Station3ChuckVacuumBreak2", CInt(enmDO.Station3ChuckVacuumBreak2), strFileName)
            Call SaveIniString(strSection, "Station3ChuckVacuumBreak3", CInt(enmDO.Station3ChuckVacuumBreak3), strFileName)
            Call SaveIniString(strSection, "Station3ChuckVacuumBreak4", CInt(enmDO.Station3ChuckVacuumBreak4), strFileName)
            Call SaveIniString(strSection, "Station3ChuckVacuumBreak5", CInt(enmDO.Station3ChuckVacuumBreak5), strFileName)
            Call SaveIniString(strSection, "Station3ChuckVacuumBreak6", CInt(enmDO.Station3ChuckVacuumBreak6), strFileName)
            Call SaveIniString(strSection, "Station3Heating", CInt(enmDO.Station3Heating), strFileName)
            Call SaveIniString(strSection, "Station3StopperDown", CInt(enmDO.Station3StopperDown), strFileName)
            Call SaveIniString(strSection, "Station3StopperUpDown", CInt(enmDO.Station3StopperUp), strFileName)
            Call SaveIniString(strSection, "Station3Up", CInt(enmDO.Station3TopLiftUpDown), strFileName)
            Call SaveIniString(strSection, "SteppingMotor", CInt(enmDO.SteppingMotor), strFileName)
            Call SaveIniString(strSection, "SyringePressure3", CInt(enmDO.SyringePressure3), strFileName)
            Call SaveIniString(strSection, "SyringePressure4", CInt(enmDO.SyringePressure4), strFileName)
            Call SaveIniString(strSection, "SystemAlarm", CInt(enmDO.SystemAlarm), strFileName)
            Call SaveIniString(strSection, "SystemAlarm2", CInt(enmDO.SystemAlarm2), strFileName)
            Call SaveIniString(strSection, "SystemOn", CInt(enmDO.SystemOn), strFileName)
            Call SaveIniString(strSection, "TrayClamper", CInt(enmDO.TrayClamper), strFileName)
            Call SaveIniString(strSection, "UnlockZAxis", CInt(enmDO.UnlockZAxis), strFileName)
            Call SaveIniString(strSection, "ValveAugerDir1", CInt(enmDO.ValveAugerDir1), strFileName)
            Call SaveIniString(strSection, "ValveAugerDir2", CInt(enmDO.ValveAugerDir2), strFileName)
            Call SaveIniString(strSection, "ValveAugerDir3", CInt(enmDO.ValveAugerDir3), strFileName)
            Call SaveIniString(strSection, "ValveAugerDir4", CInt(enmDO.ValveAugerDir4), strFileName)
            Call SaveIniString(strSection, "ValvePressure1", CInt(enmDO.ValvePressure1), strFileName)
            Call SaveIniString(strSection, "ValvePressure2", CInt(enmDO.ValvePressure2), strFileName)
            Call SaveIniString(strSection, "ValvePressure3", CInt(enmDO.ValvePressure3), strFileName)
            Call SaveIniString(strSection, "ValvePressure4", CInt(enmDO.ValvePressure4), strFileName)
            Call SaveIniString(strSection, "YellowIndicator", CInt(enmDO.YellowIndicator), strFileName)
            Call SaveIniString(strSection, "ValveControllerReset1", CInt(enmDO.ValveControllerReset1), strFileName)
            Call SaveIniString(strSection, "ValveControllerReset2", CInt(enmDO.ValveControllerReset2), strFileName)
            Call SaveIniString(strSection, "ValveControllerReset3", CInt(enmDO.ValveControllerReset3), strFileName)
            Call SaveIniString(strSection, "ValveControllerReset4", CInt(enmDO.ValveControllerReset4), strFileName)
            Call SaveIniString(strSection, "DispensingTrigger1", CInt(enmDO.DispensingTrigger1), strFileName)
            Call SaveIniString(strSection, "DispensingTrigger2", CInt(enmDO.DispensingTrigger2), strFileName)
            Call SaveIniString(strSection, "DispensingTrigger3", CInt(enmDO.DispensingTrigger3), strFileName)
            Call SaveIniString(strSection, "DispensingTrigger4", CInt(enmDO.DispensingTrigger4), strFileName)
            Call SaveIniString(strSection, "LaserReaderReset1", CInt(enmDO.LaserReaderReset1), strFileName)
            Call SaveIniString(strSection, "LaserReaderReset2", CInt(enmDO.LaserReaderReset2), strFileName)
            Call SaveIniString(strSection, "LaserReaderReset3", CInt(enmDO.LaserReaderReset3), strFileName)
            Call SaveIniString(strSection, "ResetLaserReader4", CInt(enmDO.ResetLaserReader4), strFileName)
            Call SaveIniString(strSection, "TriggerBoardReset1", CInt(enmDO.TriggerBoardReset1), strFileName)
            Call SaveIniString(strSection, "TriggerBoardReset2", CInt(enmDO.TriggerBoardReset2), strFileName)
            Call SaveIniString(strSection, "TriggerBoardReset3", CInt(enmDO.TriggerBoardReset3), strFileName)
            Call SaveIniString(strSection, "TriggerBoardReset4", CInt(enmDO.TriggerBoardReset4), strFileName)
            Call SaveIniString(strSection, "Valve1HeaterOn", CInt(enmDO.Valve1HeaterOn), strFileName)
            Call SaveIniString(strSection, "Valve2HeaterOn", CInt(enmDO.Valve2HeaterOn), strFileName)
            Call SaveIniString(strSection, "Valve3HeaterOn", CInt(enmDO.Valve3HeaterOn), strFileName)
            Call SaveIniString(strSection, "Valve4HeaterOn", CInt(enmDO.Valve4HeaterOn), strFileName)
            Call SaveIniString(strSection, "TrayClamperOn", CInt(enmDO.TrayClamperOn), strFileName)
            Call SaveIniString(strSection, "TrayClamperOff", CInt(enmDO.TrayClamperOff), strFileName)
            Call SaveIniString(strSection, "CassetteBarcodeReceiveFinish1", CInt(enmDO.CassetteBarcodeReceiveFinish1), strFileName)
            Call SaveIniString(strSection, "MappingDataReceiveFinish1", CInt(enmDO.MappingDataReceiveFinish1), strFileName)
            Call SaveIniString(strSection, "ExchangeRecipe1", CInt(enmDO.ExchangeRecipe1), strFileName)
            Call SaveIniString(strSection, "CassetteAbort1", CInt(enmDO.CassetteAbort1), strFileName)
            Call SaveIniString(strSection, "CassetteBarcodeReceiveFinish2", CInt(enmDO.CassetteBarcodeReceiveFinish2), strFileName)
            Call SaveIniString(strSection, "MappingDataReceiveFinish2", CInt(enmDO.MappingDataReceiveFinish2), strFileName)
            Call SaveIniString(strSection, "ExchangeRecipe2", CInt(enmDO.ExchangeRecipe2), strFileName)
            Call SaveIniString(strSection, "CassetteAbort2", CInt(enmDO.CassetteAbort2), strFileName)
            Call SaveIniString(strSection, "CoolDown1", CInt(enmDO.CoolDown1), strFileName)  'Soni + 2016.09.16 散熱模組接點
            Call SaveIniString(strSection, "CoolDown2", CInt(enmDO.CoolDown2), strFileName)  'Soni + 2016.09.16 散熱模組接點
            Call SaveIniString(strSection, "CoolDown3", CInt(enmDO.CoolDown3), strFileName)  'Soni + 2016.09.16 散熱模組接點
            Call SaveIniString(strSection, "CoolDown4", CInt(enmDO.CoolDown4), strFileName)  'Soni + 2016.09.16 散熱模組接點
            Call SaveIniString(strSection, "ValveCylUp1", CInt(enmDO.ValveCylUp1), strFileName)
            Call SaveIniString(strSection, "ValveCylUp2", CInt(enmDO.ValveCylUp2), strFileName)
            Call SaveIniString(strSection, "ValveCylUp3", CInt(enmDO.ValveCylUp3), strFileName)
            Call SaveIniString(strSection, "ValveCylUp4", CInt(enmDO.ValveCylUp4), strFileName)
            Call SaveIniString(strSection, "ValveCylDown1", CInt(enmDO.ValveCylDown1), strFileName)
            Call SaveIniString(strSection, "ValveCylDown2", CInt(enmDO.ValveCylDown2), strFileName)
            Call SaveIniString(strSection, "ValveCylDown3", CInt(enmDO.ValveCylDown3), strFileName)
            Call SaveIniString(strSection, "ValveCylDown4", CInt(enmDO.ValveCylDown4), strFileName)
            Call SaveIniString(strSection, "ContactHeightReZero", CInt(enmDO.ContactHeightReZero), strFileName)
            Call SaveIniString(strSection, "ContactHeightSolenoidValve", CInt(enmDO.ContactHeightSolenoidValve), strFileName)
            Call SaveIniString(strSection, "Max", CInt(enmDO.Max), strFileName)
        End Sub

#End Region
    End Structure


End Module
