Public Module MCommonDefineDI

    ''' <summary>
    ''' DI列表
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure enmDI
        ''' <summary>最大索引值</summary>
        ''' <remarks></remarks>
        Public Shared Max As Integer = -1
        Public Shared Reserved As Integer = -1
        ''' <summary>A機_EMS異常</summary>
        ''' <remarks></remarks>
        Public Shared EMS As Integer = -1
        ''' <summary>廠務氣壓偵測/A機_CDA</summary>
        ''' <remarks></remarks>
        Public Shared CDA As Integer = -1
        ''' <summary>緊急停止按紐/A機_EMO</summary>
        ''' <remarks></remarks>
        Public Shared EMO As Integer = -1
        ''' <summary>安全門插銷/A機_開門停機保護</summary>
        ''' <remarks></remarks>
        Public Shared DoorClose As Integer = -1
        ''' <summary>系統電力顯示/控制電源</summary>
        ''' <remarks></remarks>
        Public Shared MC1 As Integer = -1
        ''' <summary>馬達電力顯示/A機_MC1 馬達電力</summary>
        ''' <remarks></remarks>
        Public Shared MC2 As Integer = -1
        ''' <summary>加熱電力顯示/A機_MC2 溫控器</summary>
        ''' <remarks></remarks>
        Public Shared MC3 As Integer = -1
        ''' <summary>A機_開始生產按鍵</summary>
        ''' <remarks></remarks>
        Public Shared StartButton As Integer = -1
        ''' <summary>A機_暫停生產按鍵</summary>
        ''' <remarks></remarks>
        Public Shared PauseButton As Integer = -1
        ''' <summary>A機_硬體重置按鍵</summary>
        ''' <remarks></remarks>
        Public Shared ResetButton As Integer = -1
        ''' <summary>SMEMA 上站預備訊號偵測(有板可用)/A機_送板訊號</summary>
        ''' <remarks></remarks>
        Public Shared BoardAvailable As Integer = -1
        ''' <summary>SMEMA 下站要版訊號偵測(下站可收板)/A機_要板訊號</summary>
        ''' <remarks></remarks>
        Public Shared MachineReadyToRecieve As Integer = -1
        ''' <summary>A機_上站異常</summary>
        ''' <remarks></remarks>
        Public Shared PrevAlarm As Integer = -1
        ''' <summary>A機_下站異常</summary>
        ''' <remarks></remarks>
        Public Shared NextAlarm As Integer = -1
        ''' <summary>[位置1噴射閥控制器異常偵測/A機_左側閥控制器異常偵測]</summary>
        ''' <remarks></remarks>
        Public Shared ValveControllerAlarm1 As Integer = -1
        ''' <summary>[位置1膠槍膠量偵測/A機_左側閥膠量偵測]</summary>
        ''' <remarks></remarks>
        Public Shared DetectSyringeSensor1 As Integer = -1
        ''' <summary>[位置1觸發板異常偵測/A機_左側觸發板異常偵測]</summary>
        ''' <remarks></remarks>
        Public Shared TriggerBoardAlarm1 As Integer = -1
        ''' <summary>[位置1觸發板Ready偵測/A機_左側觸發板Ready偵測]</summary>
        ''' <remarks></remarks>
        Public Shared TriggerBoardReady1 As Integer = -1
        ''' <summary>[位置2噴射閥控制器異常偵測/A機_右側閥控制器異常偵測]</summary>
        ''' <remarks></remarks>
        Public Shared ValveControllerAlarm2 As Integer = -1
        ''' <summary>[位置2膠槍膠量偵測/A機_右側閥膠量偵測]</summary>
        ''' <remarks></remarks>
        Public Shared DetectSyringeSensor2 As Integer = -1
        ''' <summary>[位置2觸發板異常偵測/A機_右側觸發板異常偵測]</summary>
        ''' <remarks></remarks>
        Public Shared TriggerBoardAlarm2 As Integer = -1
        ''' <summary>[位置2觸發板Ready偵測/A機_右側觸發板Ready偵測]</summary>
        ''' <remarks></remarks>
        Public Shared TriggerBoardReady2 As Integer = -1
        ''' <summary>視覺Busy偵測/A機_左側CCD Busy(運算中為True運算完為False)</summary>
        ''' <remarks></remarks>
        Public Shared CcdBusy As Integer = -1
        ''' <summary>A機_右側CCD Busy</summary>
        ''' <remarks></remarks>
        Public Shared CCDBusy2 As Integer = -1
        ''' <summary>站1 阻擋氣缸上定位/A機_點膠作業區之料盤阻擋汽缸上昇定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station2StopperUpReady As Integer = -1
        ''' <summary>站1 阻擋氣缸下定位/A機_點膠作業區之料盤阻擋汽缸下降定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station2StopperDownReady As Integer = -1
        ''' <summary>站1 阻擋氣缸上定位/A機_點膠入料區之料盤阻擋汽缸上昇定位檢知(前)</summary>
        ''' <remarks></remarks>
        Public Shared Station1StopperUpReady As Integer = -1
        ''' <summary>站1 阻擋氣缸下定位/A機_點膠入料區之料盤阻擋汽缸下降定位檢知(前)</summary>
        ''' <remarks></remarks>
        Public Shared Station1StopperDownReady As Integer = -1
        ''' <summary>A機_入料馬達 異常</summary>
        ''' <remarks></remarks>
        Public Shared MoveInMotorAlarm As Integer = -1
        ''' <summary>站2 料盤感測器偵測/A機_點膠作業區之料盤檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station2TrayReady As Integer = -1
        ''' <summary>A機_點膠作業區之料盤流入檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station2TrayInSensor As Integer = -1
        ''' <summary>A機_點膠作業區之料盤流出檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station2TrayOutSensor As Integer = -1
        ''' <summary>A機_點膠作業區加熱區1汽缸上昇定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station2Heater1CylinderUpReady As Integer = -1
        ''' <summary>A機_點膠作業區加熱區1汽缸下降定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station2Heater1CylinderDownReady As Integer = -1
        ''' <summary>A機_點膠作業區加熱區2汽缸上昇定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station2Heater2CylinderUpReady As Integer = -1
        ''' <summary>A機_點膠作業區加熱區2汽缸下降定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station2Heater2CylinderDownReady As Integer = -1
        ''' <summary>A機_點膠作業區加熱區3汽缸上昇定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station2Heater3CylinderUpReady As Integer = -1
        ''' <summary>A機_點膠作業區加熱區3汽缸下降定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station2Heater3CylinderDownReady As Integer = -1
        ''' <summary>A機_點膠作業區加熱區4汽缸上昇定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station2Heater4CylinderUpReady As Integer = -1
        ''' <summary>A機_點膠作業區加熱區4汽缸下降定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station2Heater4CylinderDownReady As Integer = -1
        ''' <summary>A機_點膠作業區加熱區5汽缸上昇定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station2Heater5CylinderUpReady As Integer = -1
        ''' <summary>A機_點膠作業區加熱區5汽缸下降定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station2Heater5CylinderDownReady As Integer = -1
        ''' <summary>A機_點膠作業區加熱區6汽缸上昇定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station2Heater6CylinderUpReady As Integer = -1
        ''' <summary>A機_點膠作業區加熱區6汽缸下降定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station2Heater6CylinderDownReady As Integer = -1
        ''' <summary>真空除膠偵測/A機_左側Purge杯真空建立</summary>
        ''' <remarks></remarks>
        Public Shared PurgeVacuumReady As Integer = -1
        ''' <summary>A機_右側Purge杯真空建立</summary>
        ''' <remarks></remarks>
        Public Shared PurgeVacuumReady2 As Integer = -1
        ''' <summary>過溫開關/A機_點膠作業區1之溫度Interlock異常</summary>
        ''' <remarks></remarks>
        Public Shared OverTemperature As Integer = -1
        ''' <summary>A機_點膠作業區2之溫度Interlock異常</summary>
        ''' <remarks></remarks>
        Public Shared OverTemperature2 As Integer = -1
        ''' <summary>A機_點膠作業區3之溫度Interlock異常</summary>
        ''' <remarks></remarks>
        Public Shared OverTemperature3 As Integer = -1
        ''' <summary>A機_點膠作業區4之溫度Interlock異常</summary>
        ''' <remarks></remarks>
        Public Shared OverTemperature4 As Integer = -1
        ''' <summary>A機_點膠作業區5之溫度Interlock異常</summary>
        ''' <remarks></remarks>
        Public Shared OverTemperature5 As Integer = -1
        ''' <summary>A機_點膠作業區6之溫度Interlock異常</summary>
        ''' <remarks></remarks>
        Public Shared OverTemperature6 As Integer = -1
        ''' <summary>A機_點膠作業區1之加熱器異常</summary>
        ''' <remarks></remarks>
        Public Shared HeaterAlarm1 As Integer = -1
        ''' <summary>A機_點膠作業區2之加熱器異常</summary>
        ''' <remarks></remarks>
        Public Shared HeaterAlarm2 As Integer = -1
        ''' <summary>A機_點膠作業區3之加熱器異常</summary>
        ''' <remarks></remarks>
        Public Shared HeaterAlarm3 As Integer = -1
        ''' <summary>A機_點膠作業區4之加熱器異常</summary>
        ''' <remarks></remarks>
        Public Shared HeaterAlarm4 As Integer = -1
        ''' <summary>A機_點膠作業區5之加熱器異常</summary>
        ''' <remarks></remarks>
        Public Shared HeaterAlarm5 As Integer = -1
        ''' <summary>A機_點膠作業區6之加熱器異常</summary>
        ''' <remarks></remarks>
        Public Shared HeaterAlarm6 As Integer = -1
        ''' <summary>站2 夾具真空檢測/A機_Chuck1段真空建立(錶頭)</summary>
        ''' <remarks></remarks>
        Public Shared Station2ChuckVacuumReady As Integer = -1
        ''' <summary>A機_Chuck2段真空建立(錶頭)</summary>
        ''' <remarks></remarks>
        Public Shared Station2ChuckVacuumReady2 As Integer = -1
        ''' <summary>A機_Chuck3段真空建立(錶頭)</summary>
        ''' <remarks></remarks>
        Public Shared Station2ChuckVacuumReady3 As Integer = -1
        ''' <summary>A機_Chuck4段真空建立(錶頭)</summary>
        ''' <remarks></remarks>
        Public Shared Station2ChuckVacuumReady4 As Integer = -1
        ''' <summary>A機_Chuck5段真空建立(錶頭)</summary>
        ''' <remarks></remarks>
        Public Shared Station2ChuckVacuumReady5 As Integer = -1
        ''' <summary>A機_Chuck6段真空建立(錶頭)</summary>
        ''' <remarks></remarks>
        Public Shared Station2ChuckVacuumReady6 As Integer = -1
        ''' <summary>B機_點膠作業區1之溫度Interlock異常</summary>
        ''' <remarks></remarks>
        Public Shared OverTemperature7 As Integer = -1
        ''' <summary>B機_點膠作業區2之溫度Interlock異常</summary>
        ''' <remarks></remarks>
        Public Shared OverTemperature8 As Integer = -1
        ''' <summary>B機_點膠作業區3之溫度Interlock異常</summary>
        ''' <remarks></remarks>
        Public Shared OverTemperature9 As Integer = -1
        ''' <summary>B機_點膠作業區4之溫度Interlock異常</summary>
        ''' <remarks></remarks>
        Public Shared OverTemperature10 As Integer = -1
        ''' <summary>B機_點膠作業區5之溫度Interlock異常</summary>
        ''' <remarks></remarks>
        Public Shared OverTemperature11 As Integer = -1
        ''' <summary>B機_點膠作業區6之溫度Interlock異常</summary>
        ''' <remarks></remarks>
        Public Shared OverTemperature12 As Integer = -1
        ''' <summary>B機_點膠作業區1之加熱器異常</summary>
        ''' <remarks></remarks>
        Public Shared HeaterAlarm7 As Integer = -1
        ''' <summary>B機_點膠作業區2之加熱器異常</summary>
        ''' <remarks></remarks>
        Public Shared HeaterAlarm8 As Integer = -1
        ''' <summary>B機_點膠作業區3之加熱器異常</summary>
        ''' <remarks></remarks>
        Public Shared HeaterAlarm9 As Integer = -1
        ''' <summary>B機_點膠作業區4之加熱器異常</summary>
        ''' <remarks></remarks>
        Public Shared HeaterAlarm10 As Integer = -1
        ''' <summary>B機_點膠作業區5之加熱器異常</summary>
        ''' <remarks></remarks>
        Public Shared HeaterAlarm11 As Integer = -1
        ''' <summary>B機_點膠作業區6之加熱器異常</summary>
        ''' <remarks></remarks>
        Public Shared HeaterAlarm12 As Integer = -1
        ''' <summary>站3 夾具真空檢測/B機_Chuck1段真空建立(錶頭)</summary>
        ''' <remarks></remarks>
        Public Shared Station3ChuckVacuumReady As Integer = -1
        ''' <summary>B機_Chuck2段真空建立(錶頭)</summary>
        ''' <remarks></remarks>
        Public Shared Station3ChuckVacuumReady2 As Integer = -1
        ''' <summary>B機_Chuck3段真空建立(錶頭)</summary>
        ''' <remarks></remarks>
        Public Shared Station3ChuckVacuumReady3 As Integer = -1
        ''' <summary>B機_Chuck4段真空建立(錶頭)</summary>
        ''' <remarks></remarks>
        Public Shared Station3ChuckVacuumReady4 As Integer = -1
        ''' <summary>B機_Chuck5段真空建立(錶頭)</summary>
        ''' <remarks></remarks>
        Public Shared Station3ChuckVacuumReady5 As Integer = -1
        ''' <summary>B機_Chuck6段真空建立(錶頭)</summary>
        ''' <remarks></remarks>
        Public Shared Station3ChuckVacuumReady6 As Integer = -1
        ''' <summary>B機_EMS</summary>
        ''' <remarks></remarks>
        Public Shared EMS2 As Integer = -1
        ''' <summary>CDA2氣壓異常/B機_CDA</summary>
        ''' <remarks></remarks>
        Public Shared CDA2 As Integer = -1
        ''' <summary>B機_開門停機保護</summary>
        ''' <remarks></remarks>
        Public Shared DoorClose2 As Integer = -1
        ''' <summary>B機_MC1 馬達電力</summary>
        ''' <remarks></remarks>
        Public Shared MC_Motor2 As Integer = -1
        ''' <summary>B機_MC2 溫控器</summary>
        ''' <remarks></remarks>
        Public Shared MC_Heater2 As Integer = -1
        ''' <summary>B機_開始生產</summary>
        ''' <remarks></remarks>
        Public Shared StartButton2 As Integer = -1
        ''' <summary>B機_暫停生產</summary>
        ''' <remarks></remarks>
        Public Shared PauseButton2 As Integer = -1
        ''' <summary>B機_硬體重置</summary>
        ''' <remarks></remarks>
        Public Shared ResetButton2 As Integer = -1
        ''' <summary>B機_送板訊號</summary>
        ''' <remarks></remarks>
        Public Shared BoardAvailable2 As Integer = -1
        ''' <summary>B機_要板訊號</summary>
        ''' <remarks></remarks>
        Public Shared MachineReadyToRecieve2 As Integer = -1
        ''' <summary>B機_上站異常</summary>
        ''' <remarks></remarks>
        Public Shared PrevAlarm2 As Integer = -1
        ''' <summary>B機_下站異常</summary>
        ''' <remarks></remarks>
        Public Shared NextAlarm2 As Integer = -1
        ''' <summary>B機_左側Purge杯真空建立</summary>
        ''' <remarks></remarks>
        Public Shared PurgeVacuumReady3 As Integer = -1
        ''' <summary>[B機_左側閥控制器異常偵測]</summary>
        ''' <remarks></remarks>
        Public Shared ValveControllerAlarm3 As Integer = -1
        ''' <summary>[B機_左側閥膠量偵測]</summary>
        ''' <remarks></remarks>
        Public Shared DetectSyringeSensor3 As Integer = -1
        ''' <summary>[B機_左側觸發板異常偵測]</summary>
        ''' <remarks></remarks>
        Public Shared TriggerBoardAlarm3 As Integer = -1
        ''' <summary>[B機_左側觸發板Ready偵測]</summary>
        ''' <remarks></remarks>
        Public Shared TriggerBoardReady3 As Integer = -1
        ''' <summary>[B機_右側閥控制器異常偵測]</summary>
        ''' <remarks></remarks>
        Public Shared ValveControllerAlarm4 As Integer = -1
        ''' <summary>[B機_右側閥膠量偵測]</summary>
        ''' <remarks></remarks>
        Public Shared DetectSyringeSensor4 As Integer = -1
        ''' <summary>[B機_右側觸發板異常偵測]</summary>
        ''' <remarks></remarks>
        Public Shared TriggerBoardAlarm4 As Integer = -1
        ''' <summary>[B機_右側觸發板Ready偵測]</summary>
        ''' <remarks></remarks>
        Public Shared TriggerBoardReady4 As Integer = -1
        ''' <summary>B機_右側Purge杯真空建立</summary>
        ''' <remarks></remarks>
        Public Shared PurgeVacuumReady4 As Integer = -1
        ''' <summary>B機_左側CCD Busy</summary>
        ''' <remarks></remarks>
        Public Shared CCDBusy3 As Integer = -1
        ''' <summary>B機_右側CCD Busy</summary>
        ''' <remarks></remarks>
        Public Shared CCDBusy4 As Integer = -1
        ''' <summary>站1 阻擋氣缸上定位/B機_點膠作業區之料盤阻擋汽缸上昇定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station3StopperUpReady As Integer = -1
        ''' <summary>站1 阻擋氣缸下定位/B機_點膠作業區之料盤阻擋汽缸下降定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station3StopperDownReady As Integer = -1
        ''' <summary>B機_皮帶傳輸動力 Alarm</summary>
        ''' <remarks></remarks>
        Public Shared MoveInMotorAlarm2 As Integer = -1
        ''' <summary>站3 料盤感測器偵測/B機_點膠作業區之料盤定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station3TrayReady As Integer = -1
        ''' <summary>B機_點膠作業區之料盤流入檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station3TrayInSensor As Integer = -1
        ''' <summary>B機_點膠作業區之料盤流出檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station3TrayOutSensor As Integer = -1
        ''' <summary>B機_流道銜接區機料盤檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station23ConcateTraySensor As Integer = -1
        ''' <summary>B機_點膠作業區加熱區1汽缸上昇定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station3Heater1CylinderUpReady As Integer = -1
        ''' <summary>B機_點膠作業區加熱區1汽缸下降定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station3Heater1CylinderDownReady As Integer = -1
        ''' <summary>B機_點膠作業區加熱區2汽缸上昇定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station3Heater2CylinderUpReady As Integer = -1
        ''' <summary>B機_點膠作業區加熱區2汽缸下降定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station3Heater2CylinderDownReady As Integer = -1
        ''' <summary>B機_點膠作業區加熱區3汽缸上昇定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station3Heater3CylinderUpReady As Integer = -1
        ''' <summary>B機_點膠作業區加熱區3汽缸下降定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station3Heater3CylinderDownReady As Integer = -1
        ''' <summary>B機_點膠作業區加熱區4汽缸上昇定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station3Heater4CylinderUpReady As Integer = -1
        ''' <summary>B機_點膠作業區加熱區4汽缸下降定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station3Heater4CylinderDownReady As Integer = -1
        ''' <summary>B機_點膠作業區加熱區5汽缸上昇定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station3Heater5CylinderUpReady As Integer = -1
        ''' <summary>B機_點膠作業區加熱區5汽缸下降定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station3Heater5CylinderDownReady As Integer = -1
        ''' <summary>B機_點膠作業區加熱區6汽缸上昇定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station3Heater6CylinderUpReady As Integer = -1
        ''' <summary>B機_點膠作業區加熱區6汽缸下降定位檢知</summary>
        ''' <remarks></remarks>
        Public Shared Station3Heater6CylinderDownReady As Integer = -1
        ''' <summary>Loader Cassette Barcode 掃描完成</summary>
        ''' <remarks></remarks>
        Public Shared CassetteBarcodeReady1 As Integer = -1
        ''' <summary>Loader Cassette Mapping 完成</summary>
        ''' <remarks></remarks>
        Public Shared CassetteMappingReady1 As Integer = -1
        ''' <summary>Loader Recipe 變更完成</summary>
        ''' <remarks></remarks>
        Public Shared ExchangeRecipeFinish1 As Integer = -1
        ''' <summary>Loader 退 Cassette 完成</summary>
        ''' <remarks></remarks>
        Public Shared CassetteAbortFinish1 As Integer = -1
        ''' <summary>Unloader Cassette Barcode 掃描完成</summary>
        ''' <remarks></remarks>
        Public Shared CassetteBarcodeReady2 As Integer = -1
        ''' <summary>Unloader Cassette Mapping 完成</summary>
        ''' <remarks></remarks>
        Public Shared CassetteMappingReady2 As Integer = -1
        ''' <summary>Unloader Recipe 變更完成</summary>
        ''' <remarks></remarks>
        Public Shared ExchangeRecipeFinish2 As Integer = -1
        ''' <summary>Unloader 退 Cassette 完成</summary>
        ''' <remarks></remarks>
        Public Shared CassetteAbortFinish2 As Integer = -1
        ''' <summary>[StageNo1 ValveNo2升降汽缸 上升檢知]</summary>
        ''' <remarks></remarks>
        Public Shared ValveCylUpReady1 As Integer = -1
        ''' <summary>[StageNo1 ValveNo2升降汽缸 下降檢知]</summary>
        ''' <remarks></remarks>
        Public Shared ValveCylDownReady1 As Integer = -1
        ''' <summary>[StageNo2 ValveNo2升降汽缸 上升檢知]</summary>
        ''' <remarks></remarks>
        Public Shared ValveCylUpReady2 As Integer = -1
        ''' <summary>[StageNo2 ValveNo2升降汽缸 下降檢知]</summary>
        ''' <remarks></remarks>
        Public Shared ValveCylDownReady2 As Integer = -1
        ''' <summary>[StageNo3 ValveNo2升降汽缸 上升檢知]</summary>
        ''' <remarks></remarks>
        Public Shared ValveCylUpReady3 As Integer = -1
        ''' <summary>[StageNo3 ValveNo2升降汽缸 下降檢知]</summary>
        ''' <remarks></remarks>
        Public Shared ValveCylDownReady3 As Integer = -1
        ''' <summary>[StageNo4 ValveNo2升降汽缸 上升檢知]</summary>
        ''' <remarks></remarks>
        Public Shared ValveCylUpReady4 As Integer = -1
        ''' <summary>[StageNo4 ValveNo2升降汽缸 下降檢知]</summary>
        ''' <remarks></remarks>
        Public Shared ValveCylDownReady4 As Integer = -1



        '=== 以下Wetco未使用 ===

        ''' <summary>CV交握訊號</summary>
        ''' <remarks></remarks>
        Public Shared ConveyerReady As Integer = -1                          'DI Conveyer Ready
        ''' <summary>氣缸除膠夾定位</summary>
        ''' <remarks></remarks>
        Public Shared ClearGlueClampOnSensor As Integer = -1                 'DI-除膠氣缸夾定位
        ''' <summary>氣缸除膠放定位</summary>
        ''' <remarks></remarks>
        Public Shared ClearGlueClampOffSensor As Integer = -1                'DI-除膠氣缸放定位
        ''' <summary>視覺Gate偵測</summary>
        ''' <remarks></remarks>
        Public Shared CcdGate As Integer = -1                                'DI-CCD 判斷()IO訊號是否已經切換完成
        ''' <summary>視覺NG偵測</summary>
        ''' <remarks></remarks>
        Public Shared CcdOutputResult As Integer = -1                        'DI-CCD 判斷NG
        ''' <summary>視覺Ready偵測</summary>
        ''' <remarks></remarks>
        Public Shared CcdReady As Integer = -1                               'DI-CCD 判斷Ready(判斷是否完成取像，取像中為False取完閒置為True)
        ''' <summary>視覺Alarm偵測</summary>
        ''' <remarks></remarks>
        Public Shared CcdAlarm As Integer = -1                               'DI_CCD 判斷是否Alarm
        ''' <summary>視覺DO0偵測</summary>
        ''' <remarks></remarks>
        Public Shared CcdDO1 As Integer = -1                                 'DI_CCD DO_1 檢測
        ''' <summary>視覺DO1偵測</summary>
        ''' <remarks></remarks>
        Public Shared CcdDO2 As Integer = -1                                 'DI_CCD DO_2 檢測
        ''' <summary>試點平台偵測</summary>
        ''' <remarks></remarks>
        Public Shared PlateformVacuumReady As Integer = -1 '
        ''' <summary>站1 料盤感測器偵測</summary>
        ''' <remarks></remarks>
        Public Shared Station1TrayReady As Integer = -1
        ''' <summary>站1 夾具真空檢測</summary>
        ''' <remarks></remarks>
        Public Shared Station1ChuckVacuumReady As Integer = -1
        ''' <summary>料盤汽缸 夾定位</summary>
        ''' <remarks></remarks>
        Public Shared TrayClamperOnReady As Integer = -1
        ''' <summary>料盤汽缸 放定位</summary>
        ''' <remarks></remarks>
        Public Shared TrayClamperOffReady As Integer = -1
        ''' <summary>站1 頂升氣缸上定位</summary>
        ''' <remarks></remarks>
        Public Shared Station1TopLiftUpReady As Integer = -1
        ''' <summary>站1 頂升氣缸下定位</summary>
        ''' <remarks></remarks>
        Public Shared Station1TopLiftDownReady As Integer = -1
        ''' <summary>站2 頂升氣缸上定位</summary>
        ''' <remarks></remarks>
        Public Shared Station2TopLiftUpReady As Integer = -1
        ''' <summary>站2 頂升氣缸下定位</summary>
        ''' <remarks></remarks>
        Public Shared Station2TopLiftDownReady As Integer = -1
        ''' <summary>站3 頂升氣缸上定位</summary>
        ''' <remarks></remarks>
        Public Shared Station3TopLiftUpReady As Integer = -1
        ''' <summary>站3 頂升氣缸下定位</summary>
        ''' <remarks></remarks>
        Public Shared Station3TopLiftDownReady As Integer = -1


#Region "enmDI存取"

        ''' <summary>讀取DI索引記錄</summary>
        ''' <param name="strFileName"></param>
        ''' <remarks></remarks>
        Public Shared Sub LoadDIIndex(ByVal strFileName As String)
            Dim strSection As String
            strSection = "DIIndex"
            enmDI.BoardAvailable = CInt(ReadIniString(strSection, "BoardAvailable", strFileName, -1))
            enmDI.BoardAvailable2 = CInt(ReadIniString(strSection, "BoardAvailable2", strFileName, -1))
            enmDI.CcdAlarm = CInt(ReadIniString(strSection, "CcdAlarm", strFileName, -1))
            enmDI.CcdBusy = CInt(ReadIniString(strSection, "CcdBusy", strFileName, -1))
            enmDI.CCDBusy2 = CInt(ReadIniString(strSection, "CCDBusy2", strFileName, -1))
            enmDI.CCDBusy3 = CInt(ReadIniString(strSection, "CCDBusy3", strFileName, -1))
            enmDI.CCDBusy4 = CInt(ReadIniString(strSection, "CCDBusy4", strFileName, -1))
            enmDI.CcdDO1 = CInt(ReadIniString(strSection, "CcdDO1", strFileName, -1))
            enmDI.CcdDO2 = CInt(ReadIniString(strSection, "CcdDO2", strFileName, -1))
            enmDI.CcdGate = CInt(ReadIniString(strSection, "CcdGate", strFileName, -1))
            enmDI.CcdOutputResult = CInt(ReadIniString(strSection, "CcdOutputResult", strFileName, -1))
            enmDI.CcdReady = CInt(ReadIniString(strSection, ".", strFileName, -1))
            enmDI.CDA = CInt(ReadIniString(strSection, "CDA", strFileName, -1))
            enmDI.CDA2 = CInt(ReadIniString(strSection, "CDA2", strFileName, -1))
            enmDI.ClearGlueClampOffSensor = CInt(ReadIniString(strSection, "ClearGlueClampOffSensor", strFileName, -1))
            enmDI.ClearGlueClampOnSensor = CInt(ReadIniString(strSection, "ClearGlueClampOnSensor", strFileName, -1))
            enmDI.ConveyerReady = CInt(ReadIniString(strSection, "ConveyerReady", strFileName, -1))
            enmDI.DetectSyringeSensor1 = CInt(ReadIniString(strSection, "DetectSyringeSensor1", strFileName, -1))
            enmDI.DetectSyringeSensor2 = CInt(ReadIniString(strSection, "DetectSyringeSensor2", strFileName, -1))
            enmDI.DetectSyringeSensor3 = CInt(ReadIniString(strSection, "DetectSyringeSensor3", strFileName, -1))
            enmDI.DetectSyringeSensor4 = CInt(ReadIniString(strSection, "DetectSyringeSensor4", strFileName, -1))
            enmDI.DoorClose = CInt(ReadIniString(strSection, "DoorClose", strFileName, -1))
            enmDI.DoorClose2 = CInt(ReadIniString(strSection, "DoorClose2", strFileName, -1))
            enmDI.EMO = CInt(ReadIniString(strSection, "EMO", strFileName, -1))
            enmDI.EMS = CInt(ReadIniString(strSection, "EMS", strFileName, -1))
            enmDI.EMS2 = CInt(ReadIniString(strSection, "EMS2", strFileName, -1))
            enmDI.MC_Motor2 = CInt(ReadIniString(strSection, "MC_Motor2", strFileName, -1))
            enmDI.MC_Heater2 = CInt(ReadIniString(strSection, "MC_Heater2", strFileName, -1))
            enmDI.HeaterAlarm1 = CInt(ReadIniString(strSection, "HeaterAlarm1", strFileName, -1))
            enmDI.HeaterAlarm2 = CInt(ReadIniString(strSection, "HeaterAlarm2", strFileName, -1))
            enmDI.HeaterAlarm3 = CInt(ReadIniString(strSection, "HeaterAlarm3", strFileName, -1))
            enmDI.HeaterAlarm4 = CInt(ReadIniString(strSection, "HeaterAlarm4", strFileName, -1))
            enmDI.HeaterAlarm5 = CInt(ReadIniString(strSection, "HeaterAlarm5", strFileName, -1))
            enmDI.HeaterAlarm6 = CInt(ReadIniString(strSection, "HeaterAlarm6", strFileName, -1))
            enmDI.HeaterAlarm7 = CInt(ReadIniString(strSection, "HeaterAlarm7", strFileName, -1))
            enmDI.HeaterAlarm8 = CInt(ReadIniString(strSection, "HeaterAlarm8", strFileName, -1))
            enmDI.HeaterAlarm9 = CInt(ReadIniString(strSection, "HeaterAlarm9", strFileName, -1))
            enmDI.HeaterAlarm10 = CInt(ReadIniString(strSection, "HeaterAlarm10", strFileName, -1))
            enmDI.HeaterAlarm11 = CInt(ReadIniString(strSection, "HeaterAlarm11", strFileName, -1))
            enmDI.HeaterAlarm12 = CInt(ReadIniString(strSection, "HeaterAlarm12", strFileName, -1))
            enmDI.MachineReadyToRecieve = CInt(ReadIniString(strSection, "MachineReadyToRecieve", strFileName, -1))
            enmDI.MachineReadyToRecieve2 = CInt(ReadIniString(strSection, "MachineReadyToRecieve2", strFileName, -1))
            enmDI.Max = CInt(ReadIniString(strSection, "Max", strFileName, -1))
            enmDI.MC1 = CInt(ReadIniString(strSection, "MC1", strFileName, -1))
            enmDI.MC2 = CInt(ReadIniString(strSection, "MC2", strFileName, -1))
            enmDI.MC3 = CInt(ReadIniString(strSection, "MC3", strFileName, -1))
            enmDI.MoveInMotorAlarm = CInt(ReadIniString(strSection, "MoveInMotorAlarm", strFileName, -1))
            enmDI.MoveInMotorAlarm2 = CInt(ReadIniString(strSection, "MoveInMotorAlarm2", strFileName, -1))
            enmDI.PrevAlarm = CInt(ReadIniString(strSection, "PrevAlarm", strFileName, -1))
            enmDI.PrevAlarm2 = CInt(ReadIniString(strSection, "PrevAlarm2", strFileName, -1))
            enmDI.NextAlarm = CInt(ReadIniString(strSection, "NextAlarm", strFileName, -1))
            enmDI.NextAlarm2 = CInt(ReadIniString(strSection, "NextAlarm2", strFileName, -1))
            enmDI.OverTemperature = CInt(ReadIniString(strSection, "OverTemperature", strFileName, -1))
            enmDI.OverTemperature2 = CInt(ReadIniString(strSection, "OverTemperature2", strFileName, -1))
            enmDI.OverTemperature3 = CInt(ReadIniString(strSection, "OverTemperature3", strFileName, -1))
            enmDI.OverTemperature4 = CInt(ReadIniString(strSection, "OverTemperature4", strFileName, -1))
            enmDI.OverTemperature5 = CInt(ReadIniString(strSection, "OverTemperature5", strFileName, -1))
            enmDI.OverTemperature6 = CInt(ReadIniString(strSection, "OverTemperature6", strFileName, -1))
            enmDI.OverTemperature7 = CInt(ReadIniString(strSection, "OverTemperature7", strFileName, -1))
            enmDI.OverTemperature8 = CInt(ReadIniString(strSection, "OverTemperature8", strFileName, -1))
            enmDI.OverTemperature9 = CInt(ReadIniString(strSection, "OverTemperature9", strFileName, -1))
            enmDI.OverTemperature10 = CInt(ReadIniString(strSection, "OverTemperature10", strFileName, -1))
            enmDI.OverTemperature11 = CInt(ReadIniString(strSection, "OverTemperature11", strFileName, -1))
            enmDI.OverTemperature12 = CInt(ReadIniString(strSection, "OverTemperature12", strFileName, -1))
            enmDI.PlateformVacuumReady = CInt(ReadIniString(strSection, "PlateformVacuumReady", strFileName, -1))
            enmDI.PurgeVacuumReady = CInt(ReadIniString(strSection, "PurgeVacuumReady", strFileName, -1))
            enmDI.PurgeVacuumReady2 = CInt(ReadIniString(strSection, "PurgeVacuumReady2", strFileName, -1))
            enmDI.PurgeVacuumReady3 = CInt(ReadIniString(strSection, "PurgeVacuumReady3", strFileName, -1))
            enmDI.PurgeVacuumReady4 = CInt(ReadIniString(strSection, "PurgeVacuumReady4", strFileName, -1))
            enmDI.PauseButton = CInt(ReadIniString(strSection, "PauseButton", strFileName, -1))
            enmDI.PauseButton2 = CInt(ReadIniString(strSection, "PauseButton2", strFileName, -1))
            enmDI.ResetButton = CInt(ReadIniString(strSection, "ResetButton", strFileName, -1))
            enmDI.ResetButton2 = CInt(ReadIniString(strSection, "ResetButton2", strFileName, -1))
            enmDI.StartButton = CInt(ReadIniString(strSection, "StartButton", strFileName, -1))
            enmDI.StartButton2 = CInt(ReadIniString(strSection, "StartButton2", strFileName, -1))
            enmDI.Station1ChuckVacuumReady = CInt(ReadIniString(strSection, "Station1ChuckVacuumReady", strFileName, -1))
            enmDI.Station1StopperDownReady = CInt(ReadIniString(strSection, "Station1StopperDownReady", strFileName, -1))
            enmDI.Station1StopperUpReady = CInt(ReadIniString(strSection, "Station1StopperUpReady", strFileName, -1))
            enmDI.Station1TopLiftDownReady = CInt(ReadIniString(strSection, "Station1TopLiftDownReady", strFileName, -1))
            enmDI.Station1TopLiftUpReady = CInt(ReadIniString(strSection, "Station1TopLiftUpReady", strFileName, -1))
            enmDI.Station1TrayReady = CInt(ReadIniString(strSection, "Station1TrayReady", strFileName, -1))
            enmDI.Station2ChuckVacuumReady = CInt(ReadIniString(strSection, "Station2ChuckVacuumReady", strFileName, -1))
            enmDI.Station2StopperDownReady = CInt(ReadIniString(strSection, "Station2StopperDownReady", strFileName, -1))
            enmDI.Station2StopperUpReady = CInt(ReadIniString(strSection, "Station2StopperUpReady", strFileName, -1))
            enmDI.Station2TopLiftDownReady = CInt(ReadIniString(strSection, "Station2TopLiftDownReady", strFileName, -1))
            enmDI.Station2TopLiftUpReady = CInt(ReadIniString(strSection, "Station2TopLiftUpReady", strFileName, -1))
            enmDI.Station2TrayReady = CInt(ReadIniString(strSection, "Station2TrayReady", strFileName, -1))
            enmDI.Station2TrayInSensor = CInt(ReadIniString(strSection, "Station2TrayInSensor", strFileName, -1))
            enmDI.Station2TrayOutSensor = CInt(ReadIniString(strSection, "Station2TrayOutSensor", strFileName, -1))
            enmDI.Station2ChuckVacuumReady = CInt(ReadIniString(strSection, "Station2ChuckVacuumReady", strFileName, -1))
            enmDI.Station2ChuckVacuumReady2 = CInt(ReadIniString(strSection, "Station2ChuckVacuumReady2", strFileName, -1))
            enmDI.Station2ChuckVacuumReady3 = CInt(ReadIniString(strSection, "Station2ChuckVacuumReady3", strFileName, -1))
            enmDI.Station2ChuckVacuumReady4 = CInt(ReadIniString(strSection, "Station2ChuckVacuumReady4", strFileName, -1))
            enmDI.Station2ChuckVacuumReady5 = CInt(ReadIniString(strSection, "Station2ChuckVacuumReady5", strFileName, -1))
            enmDI.Station2ChuckVacuumReady6 = CInt(ReadIniString(strSection, "Station2ChuckVacuumReady6", strFileName, -1))
            enmDI.Station3ChuckVacuumReady = CInt(ReadIniString(strSection, "Station3ChuckVacuumReady", strFileName, -1))
            enmDI.Station3ChuckVacuumReady2 = CInt(ReadIniString(strSection, "Station3ChuckVacuumReady2", strFileName, -1))
            enmDI.Station3ChuckVacuumReady3 = CInt(ReadIniString(strSection, "Station3ChuckVacuumReady3", strFileName, -1))
            enmDI.Station3ChuckVacuumReady4 = CInt(ReadIniString(strSection, "Station3ChuckVacuumReady4", strFileName, -1))
            enmDI.Station3ChuckVacuumReady5 = CInt(ReadIniString(strSection, "Station3ChuckVacuumReady5", strFileName, -1))
            enmDI.Station3ChuckVacuumReady6 = CInt(ReadIniString(strSection, "Station3ChuckVacuumReady6", strFileName, -1))
            enmDI.Station3Heater1CylinderDownReady = CInt(ReadIniString(strSection, "Station3Heater1CylinderDownReady", strFileName, -1))
            enmDI.Station3Heater1CylinderUpReady = CInt(ReadIniString(strSection, "Station3Heater1CylinderUpReady", strFileName, -1))
            enmDI.Station3Heater2CylinderDownReady = CInt(ReadIniString(strSection, "Station3Heater2CylinderDownReady", strFileName, -1))
            enmDI.Station3Heater2CylinderUpReady = CInt(ReadIniString(strSection, "Station3Heater2CylinderUpReady", strFileName, -1))
            enmDI.Station3Heater3CylinderDownReady = CInt(ReadIniString(strSection, "Station3Heater3CylinderDownReady", strFileName, -1))
            enmDI.Station3Heater3CylinderUpReady = CInt(ReadIniString(strSection, "Station3Heater3CylinderUpReady", strFileName, -1))
            enmDI.Station3Heater4CylinderDownReady = CInt(ReadIniString(strSection, "Station3Heater4CylinderDownReady", strFileName, -1))
            enmDI.Station3Heater4CylinderUpReady = CInt(ReadIniString(strSection, "Station3Heater4CylinderUpReady", strFileName, -1))
            enmDI.Station3Heater5CylinderDownReady = CInt(ReadIniString(strSection, "Station3Heater5CylinderDownReady", strFileName, -1))
            enmDI.Station3Heater5CylinderUpReady = CInt(ReadIniString(strSection, "Station3Heater5CylinderUpReady", strFileName, -1))
            enmDI.Station3Heater6CylinderDownReady = CInt(ReadIniString(strSection, "Station3Heater6CylinderDownReady", strFileName, -1))
            enmDI.Station3Heater6CylinderUpReady = CInt(ReadIniString(strSection, "Station3Heater6CylinderUpReady", strFileName, -1))
            enmDI.Station3StopperDownReady = CInt(ReadIniString(strSection, "Station3StopperDownReady", strFileName, -1))
            enmDI.Station3StopperUpReady = CInt(ReadIniString(strSection, "Station3StopperUpReady", strFileName, -1))
            enmDI.Station3TopLiftDownReady = CInt(ReadIniString(strSection, "Station3TopLiftDownReady", strFileName, -1))
            enmDI.Station3TopLiftUpReady = CInt(ReadIniString(strSection, "Station3TopLiftUpReady", strFileName, -1))
            enmDI.Station3TrayInSensor = CInt(ReadIniString(strSection, "Station3TrayInSensor", strFileName, -1))
            enmDI.Station3TrayOutSensor = CInt(ReadIniString(strSection, "Station3TrayOutSensor", strFileName, -1))
            enmDI.Station3TrayReady = CInt(ReadIniString(strSection, "Station3TrayReady", strFileName, -1))
            enmDI.TrayClamperOffReady = CInt(ReadIniString(strSection, "Station2TrayClampOffReady", strFileName, -1))
            enmDI.TrayClamperOnReady = CInt(ReadIniString(strSection, "Station2TrayClampOnReady", strFileName, -1))
            enmDI.TrayClamperOffReady = CInt(ReadIniString(strSection, "TrayClamperOffReady", strFileName, -1))
            enmDI.TrayClamperOnReady = CInt(ReadIniString(strSection, "TrayClamperOnReady", strFileName, -1))
            enmDI.TriggerBoardAlarm1 = CInt(ReadIniString(strSection, "TriggerBoardAlarm1", strFileName, -1))
            enmDI.TriggerBoardAlarm2 = CInt(ReadIniString(strSection, "TriggerBoardAlarm2", strFileName, -1))
            enmDI.TriggerBoardAlarm3 = CInt(ReadIniString(strSection, "TriggerBoardAlarm3", strFileName, -1))
            enmDI.TriggerBoardAlarm4 = CInt(ReadIniString(strSection, "TriggerBoardAlarm4", strFileName, -1))
            enmDI.TriggerBoardReady1 = CInt(ReadIniString(strSection, "TriggerBoardReady1", strFileName, -1))
            enmDI.TriggerBoardReady2 = CInt(ReadIniString(strSection, "TriggerBoardReady2", strFileName, -1))
            enmDI.TriggerBoardReady3 = CInt(ReadIniString(strSection, "TriggerBoardReady3", strFileName, -1))
            enmDI.TriggerBoardReady4 = CInt(ReadIniString(strSection, "TriggerBoardReady4", strFileName, -1))
            enmDI.ValveControllerAlarm1 = CInt(ReadIniString(strSection, "ValveControllerAlarm1", strFileName, -1))
            enmDI.ValveControllerAlarm2 = CInt(ReadIniString(strSection, "ValveControllerAlarm2", strFileName, -1))
            enmDI.ValveControllerAlarm3 = CInt(ReadIniString(strSection, "ValveControllerAlarm3", strFileName, -1))
            enmDI.ValveControllerAlarm4 = CInt(ReadIniString(strSection, "ValveControllerAlarm4", strFileName, -1))
            enmDI.Station2Heater1CylinderUpReady = CInt(ReadIniString(strSection, "Station2Heater1CylinderUpReady", strFileName, -1))
            enmDI.Station2Heater1CylinderDownReady = CInt(ReadIniString(strSection, "Station2Heater1CylinderDownReady", strFileName, -1))
            enmDI.Station2Heater2CylinderUpReady = CInt(ReadIniString(strSection, "Station2Heater2CylinderUpReady", strFileName, -1))
            enmDI.Station2Heater2CylinderDownReady = CInt(ReadIniString(strSection, "Station2Heater2CylinderDownReady", strFileName, -1))
            enmDI.Station2Heater3CylinderUpReady = CInt(ReadIniString(strSection, "Station2Heater3CylinderUpReady", strFileName, -1))
            enmDI.Station2Heater3CylinderDownReady = CInt(ReadIniString(strSection, "Station2Heater3CylinderDownReady", strFileName, -1))
            enmDI.Station2Heater4CylinderUpReady = CInt(ReadIniString(strSection, "Station2Heater4CylinderUpReady", strFileName, -1))
            enmDI.Station2Heater4CylinderDownReady = CInt(ReadIniString(strSection, "Station2Heater4CylinderDownReady", strFileName, -1))
            enmDI.Station2Heater5CylinderUpReady = CInt(ReadIniString(strSection, "Station2Heater5CylinderUpReady", strFileName, -1))
            enmDI.Station2Heater5CylinderDownReady = CInt(ReadIniString(strSection, "Station2Heater5CylinderDownReady", strFileName, -1))
            enmDI.Station2Heater6CylinderUpReady = CInt(ReadIniString(strSection, "Station2Heater6CylinderUpReady", strFileName, -1))
            enmDI.Station2Heater6CylinderDownReady = CInt(ReadIniString(strSection, "Station2Heater6CylinderDownReady", strFileName, -1))
            enmDI.CassetteBarcodeReady1 = CInt(ReadIniString(strSection, "CassetteBarcodeReady1", strFileName, -1))
            enmDI.CassetteMappingReady1 = CInt(ReadIniString(strSection, "CassetteMappingReady1", strFileName, -1))
            enmDI.ExchangeRecipeFinish1 = CInt(ReadIniString(strSection, "ExchangeRecipeFinish1", strFileName, -1))
            enmDI.CassetteAbortFinish1 = CInt(ReadIniString(strSection, "CassetteAbortFinish1", strFileName, -1))
            enmDI.CassetteBarcodeReady2 = CInt(ReadIniString(strSection, "CassetteBarcodeReady2", strFileName, -1))
            enmDI.CassetteMappingReady2 = CInt(ReadIniString(strSection, "CassetteMappingReady2", strFileName, -1))
            enmDI.ExchangeRecipeFinish2 = CInt(ReadIniString(strSection, "ExchangeRecipeFinish2", strFileName, -1))
            enmDI.CassetteAbortFinish2 = CInt(ReadIniString(strSection, "CassetteAbortFinish2", strFileName, -1))
            enmDI.ValveCylUpReady1 = CInt(ReadIniString(strSection, "ValveCylUpReady1", strFileName, -1))
            enmDI.ValveCylUpReady2 = CInt(ReadIniString(strSection, "ValveCylUpReady2", strFileName, -1))
            enmDI.ValveCylUpReady3 = CInt(ReadIniString(strSection, "ValveCylUpReady3", strFileName, -1))
            enmDI.ValveCylUpReady4 = CInt(ReadIniString(strSection, "ValveCylUpReady4", strFileName, -1))
            enmDI.ValveCylDownReady1 = CInt(ReadIniString(strSection, "ValveCylDownReady1", strFileName, -1))
            enmDI.ValveCylDownReady2 = CInt(ReadIniString(strSection, "ValveCylDownReady2", strFileName, -1))
            enmDI.ValveCylDownReady3 = CInt(ReadIniString(strSection, "ValveCylDownReady3", strFileName, -1))
            enmDI.ValveCylDownReady4 = CInt(ReadIniString(strSection, "ValveCylDownReady4", strFileName, -1))
            enmDI.Max = CInt(ReadIniString(strSection, "Max", strFileName, -1))
        End Sub
        ''' <summary>儲存DI索引記錄</summary>
        ''' <param name="strFileName"></param>
        ''' <remarks></remarks>
        Public Shared Sub SaveDIIndex(ByVal strFileName As String)
            Dim strSection As String
            strSection = "DIIndex"
            Call SaveIniString(strSection, "BoardAvailable", CInt(enmDI.BoardAvailable), strFileName)
            Call SaveIniString(strSection, "BoardAvailable2", CInt(enmDI.BoardAvailable2), strFileName)
            Call SaveIniString(strSection, "CcdAlarm", CInt(enmDI.CcdAlarm), strFileName)
            Call SaveIniString(strSection, "CcdBusy", CInt(enmDI.CcdBusy), strFileName)
            Call SaveIniString(strSection, "CcdBusy2", CInt(enmDI.CcdBusy), strFileName)
            Call SaveIniString(strSection, "CcdBusy3", CInt(enmDI.CcdBusy), strFileName)
            Call SaveIniString(strSection, "CcdBusy4", CInt(enmDI.CcdBusy), strFileName)
            Call SaveIniString(strSection, "CcdDO1", CInt(enmDI.CcdDO1), strFileName)
            Call SaveIniString(strSection, "CcdDO2", CInt(enmDI.CcdDO2), strFileName)
            Call SaveIniString(strSection, "CcdGate", CInt(enmDI.CcdGate), strFileName)
            Call SaveIniString(strSection, "CcdOutputResult", CInt(enmDI.CcdOutputResult), strFileName)
            Call SaveIniString(strSection, "CcdReady", CInt(enmDI.CcdReady), strFileName)
            Call SaveIniString(strSection, "CDA", CInt(enmDI.CDA), strFileName)
            Call SaveIniString(strSection, "CDA2", CInt(enmDI.CDA2), strFileName)
            Call SaveIniString(strSection, "ClearGlueClampOffSensor", CInt(enmDI.ClearGlueClampOffSensor), strFileName)
            Call SaveIniString(strSection, "ClearGlueClampOnSensor", CInt(enmDI.ClearGlueClampOnSensor), strFileName)
            Call SaveIniString(strSection, "ConveyerReady", CInt(enmDI.ConveyerReady), strFileName)
            Call SaveIniString(strSection, "DetectSyringeSensor1", CInt(enmDI.DetectSyringeSensor1), strFileName)
            Call SaveIniString(strSection, "DetectSyringeSensor2", CInt(enmDI.DetectSyringeSensor2), strFileName)
            Call SaveIniString(strSection, "DetectSyringeSensor3", CInt(enmDI.DetectSyringeSensor3), strFileName)
            Call SaveIniString(strSection, "DetectSyringeSensor4", CInt(enmDI.DetectSyringeSensor4), strFileName)
            Call SaveIniString(strSection, "DoorClose", CInt(enmDI.DoorClose), strFileName)
            Call SaveIniString(strSection, "DoorClose2", CInt(enmDI.DoorClose2), strFileName)
            Call SaveIniString(strSection, "EMO", CInt(enmDI.EMO), strFileName)
            Call SaveIniString(strSection, "EMS", CInt(enmDI.EMS), strFileName)
            Call SaveIniString(strSection, "EMS2", CInt(enmDI.EMS2), strFileName)
            Call SaveIniString(strSection, "MC_Motor2", CInt(enmDI.MC_Motor2), strFileName)
            Call SaveIniString(strSection, "MC_Heater2", CInt(enmDI.MC_Heater2), strFileName)
            Call SaveIniString(strSection, "HeaterAlarm1", CInt(enmDI.HeaterAlarm1), strFileName)
            Call SaveIniString(strSection, "HeaterAlarm2", CInt(enmDI.HeaterAlarm2), strFileName)
            Call SaveIniString(strSection, "HeaterAlarm3", CInt(enmDI.HeaterAlarm3), strFileName)
            Call SaveIniString(strSection, "HeaterAlarm4", CInt(enmDI.HeaterAlarm4), strFileName)
            Call SaveIniString(strSection, "HeaterAlarm5", CInt(enmDI.HeaterAlarm5), strFileName)
            Call SaveIniString(strSection, "HeaterAlarm6", CInt(enmDI.HeaterAlarm6), strFileName)
            Call SaveIniString(strSection, "HeaterAlarm7", CInt(enmDI.HeaterAlarm7), strFileName)
            Call SaveIniString(strSection, "HeaterAlarm8", CInt(enmDI.HeaterAlarm8), strFileName)
            Call SaveIniString(strSection, "HeaterAlarm9", CInt(enmDI.HeaterAlarm9), strFileName)
            Call SaveIniString(strSection, "HeaterAlarm10", CInt(enmDI.HeaterAlarm10), strFileName)
            Call SaveIniString(strSection, "HeaterAlarm11", CInt(enmDI.HeaterAlarm11), strFileName)
            Call SaveIniString(strSection, "HeaterAlarm12", CInt(enmDI.HeaterAlarm12), strFileName)
            Call SaveIniString(strSection, "MachineReadyToRecieve", CInt(enmDI.MachineReadyToRecieve), strFileName)
            Call SaveIniString(strSection, "MachineReadyToRecieve2", CInt(enmDI.MachineReadyToRecieve2), strFileName)
            Call SaveIniString(strSection, "Max", CInt(enmDI.Max), strFileName)
            Call SaveIniString(strSection, "MC1", CInt(enmDI.MC1), strFileName)
            Call SaveIniString(strSection, "MC2", CInt(enmDI.MC2), strFileName)
            Call SaveIniString(strSection, "MC3", CInt(enmDI.MC3), strFileName)
            Call SaveIniString(strSection, "MoveInMotorAlarm", CInt(enmDI.MoveInMotorAlarm), strFileName)
            Call SaveIniString(strSection, "MoveInMotorAlarm2", CInt(enmDI.MoveInMotorAlarm2), strFileName)
            Call SaveIniString(strSection, "PrevAlarm", CInt(enmDI.PrevAlarm), strFileName)
            Call SaveIniString(strSection, "PrevAlarm2", CInt(enmDI.PrevAlarm2), strFileName)
            Call SaveIniString(strSection, "NextAlarm", CInt(enmDI.NextAlarm), strFileName)
            Call SaveIniString(strSection, "NextAlarm2", CInt(enmDI.NextAlarm2), strFileName)
            Call SaveIniString(strSection, "OverTemperature", CInt(enmDI.OverTemperature), strFileName)
            Call SaveIniString(strSection, "OverTemperature2", CInt(enmDI.OverTemperature2), strFileName)
            Call SaveIniString(strSection, "OverTemperature3", CInt(enmDI.OverTemperature3), strFileName)
            Call SaveIniString(strSection, "OverTemperature4", CInt(enmDI.OverTemperature4), strFileName)
            Call SaveIniString(strSection, "OverTemperature5", CInt(enmDI.OverTemperature5), strFileName)
            Call SaveIniString(strSection, "OverTemperature6", CInt(enmDI.OverTemperature6), strFileName)
            Call SaveIniString(strSection, "OverTemperature7", CInt(enmDI.OverTemperature7), strFileName)
            Call SaveIniString(strSection, "OverTemperature8", CInt(enmDI.OverTemperature8), strFileName)
            Call SaveIniString(strSection, "OverTemperature9", CInt(enmDI.OverTemperature9), strFileName)
            Call SaveIniString(strSection, "OverTemperature10", CInt(enmDI.OverTemperature10), strFileName)
            Call SaveIniString(strSection, "OverTemperature11", CInt(enmDI.OverTemperature11), strFileName)
            Call SaveIniString(strSection, "OverTemperature12", CInt(enmDI.OverTemperature12), strFileName)
            Call SaveIniString(strSection, "PlateformVacuumReady", CInt(enmDI.PlateformVacuumReady), strFileName)
            Call SaveIniString(strSection, "PurgeVacuumReady", CInt(enmDI.PurgeVacuumReady), strFileName)
            Call SaveIniString(strSection, "PurgeVacuumReady2", CInt(enmDI.PurgeVacuumReady2), strFileName)
            Call SaveIniString(strSection, "PurgeVacuumReady3", CInt(enmDI.PurgeVacuumReady3), strFileName)
            Call SaveIniString(strSection, "PurgeVacuumReady4", CInt(enmDI.PurgeVacuumReady4), strFileName)
            Call SaveIniString(strSection, "PauseButton", CInt(enmDI.PauseButton), strFileName)
            Call SaveIniString(strSection, "PauseButton2", CInt(enmDI.PauseButton2), strFileName)
            Call SaveIniString(strSection, "ResetButton", CInt(enmDI.ResetButton), strFileName)
            Call SaveIniString(strSection, "ResetButton2", CInt(enmDI.ResetButton2), strFileName)
            Call SaveIniString(strSection, "StartButton", CInt(enmDI.StartButton), strFileName)
            Call SaveIniString(strSection, "StartButton2", CInt(enmDI.StartButton2), strFileName)
            Call SaveIniString(strSection, "Station1ChuckVacuumReady", CInt(enmDI.Station1ChuckVacuumReady), strFileName)
            Call SaveIniString(strSection, "Station1StopperDownReady", CInt(enmDI.Station1StopperDownReady), strFileName)
            Call SaveIniString(strSection, "Station1StopperUpReady", CInt(enmDI.Station1StopperUpReady), strFileName)
            Call SaveIniString(strSection, "Station1TopLiftDownReady", CInt(enmDI.Station1TopLiftDownReady), strFileName)
            Call SaveIniString(strSection, "Station1TopLiftUpReady", CInt(enmDI.Station1TopLiftUpReady), strFileName)
            Call SaveIniString(strSection, "Station1TrayReady", CInt(enmDI.Station1TrayReady), strFileName)
            Call SaveIniString(strSection, "Station2ChuckVacuumReady", CInt(enmDI.Station2ChuckVacuumReady), strFileName)
            Call SaveIniString(strSection, "Station2StopperDownReady", CInt(enmDI.Station2StopperDownReady), strFileName)
            Call SaveIniString(strSection, "Station2StopperUpReady", CInt(enmDI.Station2StopperUpReady), strFileName)
            Call SaveIniString(strSection, "Station2TopLiftDownReady", CInt(enmDI.Station2TopLiftDownReady), strFileName)
            Call SaveIniString(strSection, "Station2TopLiftUpReady", CInt(enmDI.Station2TopLiftUpReady), strFileName)
            Call SaveIniString(strSection, "Station2TrayInSensor", CInt(enmDI.Station2TrayInSensor), strFileName)
            Call SaveIniString(strSection, "Station2TrayOutSensor", CInt(enmDI.Station2TrayOutSensor), strFileName)
            Call SaveIniString(strSection, "Station2TrayReady", CInt(enmDI.Station2TrayReady), strFileName)
            Call SaveIniString(strSection, "Station2ChuckVacuumReady", CInt(enmDI.Station2ChuckVacuumReady), strFileName)
            Call SaveIniString(strSection, "Station2ChuckVacuumReady2", CInt(enmDI.Station2ChuckVacuumReady2), strFileName)
            Call SaveIniString(strSection, "Station2ChuckVacuumReady3", CInt(enmDI.Station2ChuckVacuumReady3), strFileName)
            Call SaveIniString(strSection, "Station2ChuckVacuumReady4", CInt(enmDI.Station2ChuckVacuumReady4), strFileName)
            Call SaveIniString(strSection, "Station2ChuckVacuumReady5", CInt(enmDI.Station2ChuckVacuumReady5), strFileName)
            Call SaveIniString(strSection, "Station2ChuckVacuumReady6", CInt(enmDI.Station2ChuckVacuumReady6), strFileName)
            Call SaveIniString(strSection, "Station3ChuckVacuumReady", CInt(enmDI.Station3ChuckVacuumReady), strFileName)
            Call SaveIniString(strSection, "Station3ChuckVacuumReady2", CInt(enmDI.Station3ChuckVacuumReady2), strFileName)
            Call SaveIniString(strSection, "Station3ChuckVacuumReady3", CInt(enmDI.Station3ChuckVacuumReady3), strFileName)
            Call SaveIniString(strSection, "Station3ChuckVacuumReady4", CInt(enmDI.Station3ChuckVacuumReady4), strFileName)
            Call SaveIniString(strSection, "Station3ChuckVacuumReady5", CInt(enmDI.Station3ChuckVacuumReady5), strFileName)
            Call SaveIniString(strSection, "Station3ChuckVacuumReady6", CInt(enmDI.Station3ChuckVacuumReady6), strFileName)
            Call SaveIniString(strSection, "Station3Heater1CylinderDownReady", CInt(enmDI.Station3Heater1CylinderDownReady), strFileName)
            Call SaveIniString(strSection, "Station3Heater1CylinderUpReady", CInt(enmDI.Station3Heater1CylinderUpReady), strFileName)
            Call SaveIniString(strSection, "Station3Heater2CylinderDownReady", CInt(enmDI.Station3Heater2CylinderDownReady), strFileName)
            Call SaveIniString(strSection, "Station3Heater2CylinderUpReady", CInt(enmDI.Station3Heater2CylinderUpReady), strFileName)
            Call SaveIniString(strSection, "Station3Heater3CylinderDownReady", CInt(enmDI.Station3Heater3CylinderDownReady), strFileName)
            Call SaveIniString(strSection, "Station3Heater3CylinderUpReady", CInt(enmDI.Station3Heater3CylinderUpReady), strFileName)
            Call SaveIniString(strSection, "Station3Heater4CylinderDownReady", CInt(enmDI.Station3Heater4CylinderDownReady), strFileName)
            Call SaveIniString(strSection, "Station3Heater4CylinderUpReady", CInt(enmDI.Station3Heater4CylinderUpReady), strFileName)
            Call SaveIniString(strSection, "Station3Heater5CylinderDownReady", CInt(enmDI.Station3Heater5CylinderDownReady), strFileName)
            Call SaveIniString(strSection, "Station3Heater5CylinderUpReady", CInt(enmDI.Station3Heater5CylinderUpReady), strFileName)
            Call SaveIniString(strSection, "Station3Heater6CylinderDownReady", CInt(enmDI.Station3Heater6CylinderDownReady), strFileName)
            Call SaveIniString(strSection, "Station3Heater6CylinderUpReady", CInt(enmDI.Station3Heater6CylinderUpReady), strFileName)
            Call SaveIniString(strSection, "Station2TrayClampOffReady", CInt(enmDI.TrayClamperOffReady), strFileName)
            Call SaveIniString(strSection, "Station2TrayClampOnReady", CInt(enmDI.TrayClamperOnReady), strFileName)
            Call SaveIniString(strSection, "Station2TrayReady", CInt(enmDI.Station2TrayReady), strFileName)
            Call SaveIniString(strSection, "Station3ChuckVacuumReady", CInt(enmDI.Station3ChuckVacuumReady), strFileName)
            Call SaveIniString(strSection, "Station3StopperDownReady", CInt(enmDI.Station3StopperDownReady), strFileName)
            Call SaveIniString(strSection, "Station3StopperUpReady", CInt(enmDI.Station3StopperUpReady), strFileName)
            Call SaveIniString(strSection, "Station3TopLiftDownReady", CInt(enmDI.Station3TopLiftDownReady), strFileName)
            Call SaveIniString(strSection, "Station3TopLiftUpReady", CInt(enmDI.Station3TopLiftUpReady), strFileName)
            Call SaveIniString(strSection, "Station3TrayInSensor", CInt(enmDI.Station3TrayInSensor), strFileName)
            Call SaveIniString(strSection, "Station3TrayOutSensor", CInt(enmDI.Station3TrayOutSensor), strFileName)
            Call SaveIniString(strSection, "Station3TrayReady", CInt(enmDI.Station3TrayReady), strFileName)
            Call SaveIniString(strSection, "TriggerBoardAlarm1", CInt(enmDI.TriggerBoardAlarm1), strFileName)
            Call SaveIniString(strSection, "TriggerBoardAlarm2", CInt(enmDI.TriggerBoardAlarm2), strFileName)
            Call SaveIniString(strSection, "TriggerBoardAlarm3", CInt(enmDI.TriggerBoardAlarm3), strFileName)
            Call SaveIniString(strSection, "TriggerBoardAlarm4", CInt(enmDI.TriggerBoardAlarm4), strFileName)
            Call SaveIniString(strSection, "TriggerBoardReady1", CInt(enmDI.TriggerBoardReady1), strFileName)
            Call SaveIniString(strSection, "TriggerBoardReady2", CInt(enmDI.TriggerBoardReady2), strFileName)
            Call SaveIniString(strSection, "TriggerBoardReady3", CInt(enmDI.TriggerBoardReady3), strFileName)
            Call SaveIniString(strSection, "TriggerBoardReady4", CInt(enmDI.TriggerBoardReady4), strFileName)
            Call SaveIniString(strSection, "ValveControllerAlarm1", CInt(enmDI.ValveControllerAlarm1), strFileName)
            Call SaveIniString(strSection, "ValveControllerAlarm2", CInt(enmDI.ValveControllerAlarm2), strFileName)
            Call SaveIniString(strSection, "ValveControllerAlarm3", CInt(enmDI.ValveControllerAlarm3), strFileName)
            Call SaveIniString(strSection, "ValveControllerAlarm4", CInt(enmDI.ValveControllerAlarm4), strFileName)
            Call SaveIniString(strSection, "Station2Heater1CylinderUpReady", CInt(enmDI.Station2Heater1CylinderUpReady), strFileName)
            Call SaveIniString(strSection, "Station2Heater1CylinderDownReady", CInt(enmDI.Station2Heater1CylinderDownReady), strFileName)
            Call SaveIniString(strSection, "Station2Heater2CylinderUpReady", CInt(enmDI.Station2Heater2CylinderUpReady), strFileName)
            Call SaveIniString(strSection, "Station2Heater2CylinderDownReady", CInt(enmDI.Station2Heater2CylinderDownReady), strFileName)
            Call SaveIniString(strSection, "Station2Heater3CylinderUpReady", CInt(enmDI.Station2Heater3CylinderUpReady), strFileName)
            Call SaveIniString(strSection, "Station2Heater3CylinderDownReady", CInt(enmDI.Station2Heater3CylinderDownReady), strFileName)
            Call SaveIniString(strSection, "Station2Heater4CylinderUpReady", CInt(enmDI.Station2Heater4CylinderUpReady), strFileName)
            Call SaveIniString(strSection, "Station2Heater4CylinderDownReady", CInt(enmDI.Station2Heater4CylinderDownReady), strFileName)
            Call SaveIniString(strSection, "Station2Heater5CylinderUpReady", CInt(enmDI.Station2Heater5CylinderUpReady), strFileName)
            Call SaveIniString(strSection, "Station2Heater5CylinderDownReady", CInt(enmDI.Station2Heater5CylinderDownReady), strFileName)
            Call SaveIniString(strSection, "Station2Heater6CylinderUpReady", CInt(enmDI.Station2Heater6CylinderUpReady), strFileName)
            Call SaveIniString(strSection, "Station2Heater6CylinderDownReady", CInt(enmDI.Station2Heater6CylinderDownReady), strFileName)
            Call SaveIniString(strSection, "CassetteBarcodeReady1", CInt(enmDI.CassetteBarcodeReady1), strFileName)
            Call SaveIniString(strSection, "CassetteMappingReady1", CInt(enmDI.CassetteMappingReady1), strFileName)
            Call SaveIniString(strSection, "ExchangeRecipeFinish1", CInt(enmDI.ExchangeRecipeFinish1), strFileName)
            Call SaveIniString(strSection, "CassetteAbortFinish1", CInt(enmDI.CassetteAbortFinish1), strFileName)
            Call SaveIniString(strSection, "CassetteBarcodeReady2", CInt(enmDI.CassetteBarcodeReady2), strFileName)
            Call SaveIniString(strSection, "CassetteMappingReady2", CInt(enmDI.CassetteMappingReady2), strFileName)
            Call SaveIniString(strSection, "ExchangeRecipeFinish2", CInt(enmDI.ExchangeRecipeFinish2), strFileName)
            Call SaveIniString(strSection, "CassetteAbortFinish2", CInt(enmDI.CassetteAbortFinish2), strFileName)
            Call SaveIniString(strSection, "ValveCylUpReady1", CInt(enmDI.ValveCylUpReady1), strFileName)
            Call SaveIniString(strSection, "ValveCylUpReady2", CInt(enmDI.ValveCylUpReady2), strFileName)
            Call SaveIniString(strSection, "ValveCylUpReady3", CInt(enmDI.ValveCylUpReady3), strFileName)
            Call SaveIniString(strSection, "ValveCylUpReady4", CInt(enmDI.ValveCylUpReady4), strFileName)
            Call SaveIniString(strSection, "ValveCylDownReady1", CInt(enmDI.ValveCylDownReady1), strFileName)
            Call SaveIniString(strSection, "ValveCylDownReady2", CInt(enmDI.ValveCylDownReady2), strFileName)
            Call SaveIniString(strSection, "ValveCylDownReady3", CInt(enmDI.ValveCylDownReady3), strFileName)
            Call SaveIniString(strSection, "ValveCylDownReady4", CInt(enmDI.ValveCylDownReady4), strFileName)
            Call SaveIniString(strSection, "Max", CInt(enmDI.Max), strFileName)
        End Sub

#End Region
    End Structure


End Module
