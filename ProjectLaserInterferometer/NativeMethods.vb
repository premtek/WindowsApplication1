'----------------------------------------------------------------------------- 
' <copyright file="NativeMethods.cs.cs" company="KEYENCE">
'	 Copyright (c) 2013 KEYENCE CORPORATION.  All rights reserved.
' </copyright>
'----------------------------------------------------------------------------- 
Imports System.Runtime.InteropServices

#Region "Enum"

''' <summary>
''' Return value definition
''' </summary>
Public Enum Rc
	''' <summary>Normal termination</summary>
	Ok = &H0
	''' <summary>Failed to open the device</summary>
	ErrOpenDevice = &H1000
	''' <summary>Device not open</summary>
	ErrNoDevice
	''' <summary>Command send error</summary>
	ErrSend
	''' <summary>Response reception error</summary>
	ErrReceive
	''' <summary>Timeout</summary>
	ErrTimeout
	''' <summary>No free space</summary>
	ErrNomemory
	''' <summary>Parameter error</summary>
	ErrParameter
	''' <summary>Received header format error</summary>
	ErrRecvFmt

	''' <summary>Not open error (for high-speed communication)</summary>
	ErrHispeedNoDevice = &H1009
	''' <summary>Already open error (for high-speed communication)</summary>
	ErrHispeedOpenYet
	''' <summary>Already performing high-speed communication error (for high-speed communication)</summary>
	ErrHispeedRecvYet
	''' <summary>Insufficient buffer size</summary>
	ErrBufferShort
End Enum

''' <summary>
''' Definition that indicates the validity of a measurement value
''' </summary>
Public Enum LJV7IF_MEASURE_DATA_INFO
	LJV7IF_MEASURE_DATA_INFO_VALID = &H0
	' Valid
	LJV7IF_MEASURE_DATA_INFO_ALARM = &H1
	' Alarm value
	LJV7IF_MEASURE_DATA_INFO_WAIT = &H2
	' Judgment wait value
End Enum

''' <summary>
''' Definition that indicates the tolerance judgment result of the measurement
''' </summary>
Public Enum LJV7IF_JUDGE_RESULT
	LJV7IF_JUDGE_RESULT_HI = &H1
	' HI
	LJV7IF_JUDGE_RESULT_GO = &H2
	' GO
	LJV7IF_JUDGE_RESULT_LO = &H4
	' LO
End Enum

''' Get batch profile position specification method designation
Public Enum BatchPos As Byte
	''' <summary>From current</summary>
	Current = &H0
	''' <summary>Specify position</summary>
	Spec = &H2
	''' <summary>From current after commitment</summary>
	Commited = &H3
	''' <summary>Current only</summary>
	CurrentOnly = &H4
End Enum

''' Setting value storage level designation
Public Enum SettingDepth As Byte
	''' <summary>Settings write area</summary>
	Write = &H0
	''' <summary>Active measurement area</summary>
	Running = &H1
	''' <summary>Save area</summary>
	Save = &H2
End Enum

''' Definition that indicates the "setting type" in LJV7IF_TARGET_SETTING structure.
Public Enum SettingType As Byte
	''' <summary>Environment setting</summary>
	Environment = &H1
	''' <summary>Common measurement setting</summary>
	Common = &H2
	''' <summary>Measurement Program setting</summary>
	Program00 = &H10
	Program01
	Program02
	Program03
	Program04
	Program05
	Program06
	Program07
	Program08
	Program09
	Program10
	Program11
	Program12
	Program13
	Program14
	Program15
End Enum

''' Get profile target buffer designation
Public Enum ProfileBank As Byte
	''' <summary>Active surface</summary>
	Active = &H0
	''' <summary>Inactive surface</summary>	
	Inactive = &H1
End Enum

''' Get profile position specification method designation
Public Enum ProfilePos As Byte
	''' <summary>From current</summary>
	Current = &H0
	''' <summary>From oldest</summary>
	Oldest = &H1
	''' <summary>Specify position</summary>
	Spec = &H2
End Enum

#End Region

#Region "Structure"
''' <summary>
''' Ethernet settings structure
''' </summary>
<StructLayout(LayoutKind.Sequential)> _
Public Structure LJV7IF_ETHERNET_CONFIG
	<MarshalAs(UnmanagedType.ByValArray, SizeConst := 4)> _
	Public abyIpAddress As Byte()
	Public wPortNo As UShort
	<MarshalAs(UnmanagedType.ByValArray, SizeConst := 2)> _
	Public reserve As Byte()

End Structure

''' <summary>
''' Date and time structure
''' </summary>
<StructLayout(LayoutKind.Sequential)> _
Public Structure LJV7IF_TIME
	Public byYear As Byte
	Public byMonth As Byte
	Public byDay As Byte
	Public byHour As Byte
	Public byMinute As Byte
	Public bySecond As Byte
	<MarshalAs(UnmanagedType.ByValArray, SizeConst := 2)> _
	Public reserve As Byte()
End Structure

''' <summary>
''' Setting item designation structure
''' </summary>
<StructLayout(LayoutKind.Sequential)> _
Public Structure LJV7IF_TARGET_SETTING
	Public byType As Byte
	Public byCategory As Byte
	Public byItem As Byte
	Public reserve As Byte
	Public byTarget1 As Byte
	Public byTarget2 As Byte
	Public byTarget3 As Byte
	Public byTarget4 As Byte
End Structure

''' <summary>
''' Measurement results structure
''' </summary>
<StructLayout(LayoutKind.Sequential)> _
Public Structure LJV7IF_MEASURE_DATA
	Public byDataInfo As Byte
	Public byJudge As Byte
	<MarshalAs(UnmanagedType.ByValArray, SizeConst := 2)> _
	Public reserve As Byte()
	Public fValue As Single
End Structure

''' <summary>
''' Profile information structure
''' </summary>
<StructLayout(LayoutKind.Sequential)> _
Public Structure LJV7IF_PROFILE_INFO
	Public byProfileCnt As Byte
	Public byEnvelope As Byte
	<MarshalAs(UnmanagedType.ByValArray, SizeConst := 2)> _
	Public reserve As Byte()
	Public wProfDataCnt As Short
	<MarshalAs(UnmanagedType.ByValArray, SizeConst := 2)> _
	Public reserve2 As Byte()
	Public lXStart As Integer
	Public lXPitch As Integer
End Structure

''' <summary>
''' Profile header information structure
''' </summary>
<StructLayout(LayoutKind.Sequential)> _
Public Structure LJV7IF_PROFILE_HEADER
	Public reserve As UInteger
	Public dwTriggerCnt As UInteger
	Public dwEncoderCnt As UInteger
	<MarshalAs(UnmanagedType.ByValArray, SizeConst := 3)> _
	Public reserve2 As UInteger()
End Structure

''' <summary>
''' Profile footer information structure
''' </summary>
<StructLayout(LayoutKind.Sequential)> _
Public Structure LJV7IF_PROFILE_FOOTER
	Public reserve As UInteger
End Structure

''' <summary>
''' High-speed mode get profile request structure (batch measurement: off)
''' </summary>
<StructLayout(LayoutKind.Sequential)> _
Public Structure LJV7IF_GET_PROFILE_REQ
	Public byTargetBank As Byte
	Public byPosMode As Byte
	<MarshalAs(UnmanagedType.ByValArray, SizeConst := 2)> _
	Public reserve As Byte()
	Public dwGetProfNo As UInteger
	Public byGetProfCnt As Byte
	Public byErase As Byte
	<MarshalAs(UnmanagedType.ByValArray, SizeConst := 2)> _
	Public reserve2 As Byte()
End Structure

''' <summary>
''' High-speed mode get profile request structure (batch measurement: on)
''' </summary>
<StructLayout(LayoutKind.Sequential)> _
Public Structure LJV7IF_GET_BATCH_PROFILE_REQ
	Public byTargetBank As Byte
	Public byPosMode As Byte
	<MarshalAs(UnmanagedType.ByValArray, SizeConst := 2)> _
	Public reserve As Byte()
	Public dwGetBatchNo As UInteger
	Public dwGetProfNo As UInteger
	Public byGetProfCnt As Byte
	Public byErase As Byte
	<MarshalAs(UnmanagedType.ByValArray, SizeConst := 2)> _
	Public reserve2 As Byte()
End Structure

''' <summary>
''' Advanced mode get profile request structure (batch measurement: on)
''' </summary>
<StructLayout(LayoutKind.Sequential)> _
Public Structure LJV7IF_GET_BATCH_PROFILE_ADVANCE_REQ
	Public byPosMode As Byte
	<MarshalAs(UnmanagedType.ByValArray, SizeConst := 3)> _
	Public reserve As Byte()
	Public dwGetBatchNo As UInteger
	Public dwGetProfNo As UInteger
	Public byGetProfCnt As Byte
	<MarshalAs(UnmanagedType.ByValArray, SizeConst := 3)> _
	Public reserve2 As Byte()
End Structure

''' <summary>
''' High-speed mode get profile response structure (batch measurement: off)
''' </summary>
<StructLayout(LayoutKind.Sequential)> _
Public Structure LJV7IF_GET_PROFILE_RSP
	Public dwCurrentProfNo As UInteger
	Public dwOldestProfNo As UInteger
	Public dwGetTopProfNo As UInteger
	Public byGetProfCnt As Byte
	<MarshalAs(UnmanagedType.ByValArray, SizeConst := 3)> _
	Public reserve As Byte()
End Structure

''' <summary>
''' High-speed mode get profile response structure (batch measurement: on)
''' </summary>
<StructLayout(LayoutKind.Sequential)> _
Public Structure LJV7IF_GET_BATCH_PROFILE_RSP
	Public dwCurrentBatchNo As UInteger
	Public dwCurrentBatchProfCnt As UInteger
	Public dwOldestBatchNo As UInteger
	Public dwOldestBatchProfCnt As UInteger
	Public dwGetBatchNo As UInteger
	Public dwGetBatchProfCnt As UInteger
	Public dwGetBatchTopProfNo As UInteger
	Public byGetProfCnt As Byte
	Public byCurrentBatchCommited As Byte
	<MarshalAs(UnmanagedType.ByValArray, SizeConst := 2)> _
	Public reserve As Byte()
End Structure

''' <summary>
''' Advanced mode get profile response structure (batch measurement: on)
''' </summary>
<StructLayout(LayoutKind.Sequential)> _
Public Structure LJV7IF_GET_BATCH_PROFILE_ADVANCE_RSP
	Public dwGetBatchNo As UInteger
	Public dwGetBatchProfCnt As UInteger
	Public dwGetBatchTopProfNo As UInteger
	Public byGetProfCnt As Byte
	<MarshalAs(UnmanagedType.ByValArray, SizeConst := 3)> _
	Public reserve As Byte()
End Structure

''' <summary>
''' Storage status request structure
''' </summary>
<StructLayout(LayoutKind.Sequential)> _
Public Structure LJV7IF_GET_STRAGE_STATUS_REQ
	Public dwRdArea As UInteger
	' Target surface to read
End Structure

''' <summary>
''' Storage status response structure
''' </summary>
<StructLayout(LayoutKind.Sequential)> _
Public Structure LJV7IF_GET_STRAGE_STATUS_RSP
	Public dwSurfaceCnt As UInteger
	' Storage surface number
	Public dwActiveSurface As UInteger
	' Active storage surface
End Structure

''' <summary>
''' Storage information structure
''' </summary>
<StructLayout(LayoutKind.Sequential)> _
Public Structure LJV7IF_STORAGE_INFO
	Public byStatus As Byte
	Public byProgramNo As Byte
	Public byTarget As Byte
	<MarshalAs(UnmanagedType.ByValArray, SizeConst := 5)> _
	Public reserve As Byte()
	Public dwStorageCnt As UInteger
End Structure

''' <summary>
''' Get storage data request structure
''' </summary>
<StructLayout(LayoutKind.Sequential)> _
Public Structure LJV7IF_GET_STORAGE_REQ
	<MarshalAs(UnmanagedType.ByValArray, SizeConst := 4)> _
	Public reserve As Byte()
	Public dwSurface As UInteger
	Public dwStartNo As UInteger
	Public dwDataCnt As UInteger
End Structure

''' <summary>
''' Get batch profile storage request structure
''' </summary>
<StructLayout(LayoutKind.Sequential)> _
Public Structure LJV7IF_GET_BATCH_PROFILE_STORAGE_REQ
	<MarshalAs(UnmanagedType.ByValArray, SizeConst := 4)> _
	Public reserve As Byte()
	Public dwSurface As UInteger
	Public dwGetBatchNo As UInteger
	Public dwGetBatchTopProfNo As UInteger
	Public byGetProfCnt As Byte
	<MarshalAs(UnmanagedType.ByValArray, SizeConst := 3)> _
	Public reserved As Byte()
End Structure

''' <summary>
''' Get storage data response structure
''' </summary>
<StructLayout(LayoutKind.Sequential)> _
Public Structure LJV7IF_GET_STORAGE_RSP
	Public dwStartNo As UInteger
	Public dwDataCnt As UInteger
	Public stBaseTime As LJV7IF_TIME
End Structure
''' <summary>
''' Get batch profile storage response structure
''' </summary>
<StructLayout(LayoutKind.Sequential)> _
Public Structure LJV7IF_GET_BATCH_PROFILE_STORAGE_RSP
	Public dwGetBatchNo As UInteger
	Public dwGetBatchProfCnt As UInteger
	Public dwGetBatchTopProfNo As UInteger
	Public byGetProfCnt As Byte
	<MarshalAs(UnmanagedType.ByValArray, SizeConst := 3)> _
	Public reserve As Byte()
	Public stBaseTime As LJV7IF_TIME
End Structure

''' <summary>
''' High-speed communication start preparation request structure
''' </summary>
<StructLayout(LayoutKind.Sequential)> _
Public Structure LJV7IF_HIGH_SPEED_PRE_START_REQ
	Public bySendPos As Byte
	' Send start position
	<MarshalAs(UnmanagedType.ByValArray, SizeConst := 3)> _
	Public reserve As Byte()
	' Reservation 
End Structure

#End Region

#Region "Method"
''' <summary>
''' Callback function for high-speed communication
''' </summary>
''' <param name="buffer">Received profile data pointer</param>
''' <param name="size">Size in units of bytes of one profile</param>
''' <param name="count">Number of profiles</param>
''' <param name="notify">Finalization condition</param>
''' <param name="user">Thread ID</param>
<UnmanagedFunctionPointer(CallingConvention.Cdecl)> _
Public Delegate Sub HighSpeedDataCallBack(buffer As IntPtr, size As UInteger, count As UInteger, notify As UInteger, user As UInteger)

''' <summary>
''' Function definitions
''' </summary>
Friend Class NativeMethods
	''' <summary>
	''' Get measurement results (the data of all 16 OUTs, including those that are not being measured, is stored).
	''' </summary>
	Friend Shared ReadOnly Property MeasurementDataCount() As Integer
		Get
			Return 16
		End Get
	End Property

	''' <summary>
	''' Number of connectable devices
	''' </summary>
	Friend Shared ReadOnly Property DeviceCount() As Integer
		Get
			Return 6
		End Get
	End Property

	''' <summary>
	''' Fixed value for the bytes of environment settings data 
	''' </summary>
	Friend Shared ReadOnly Property EnvironmentSettingSize() As UInt32
		Get
			Return 60
		End Get
	End Property

	''' <summary>
	''' Fixed value for the bytes of common measurement settings data 
	''' </summary>
	Friend Shared ReadOnly Property CommonSettingSize() As UInt32
		Get
			Return 12
		End Get
	End Property

	''' <summary>
	''' Fixed value for the bytes of program settings data 
	''' </summary>
	Friend Shared ReadOnly Property ProgramSettingSize() As UInt32
		Get
			Return 10932
		End Get
	End Property

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_Initialize() As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_Finalize() As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_GetVersion() As UInteger
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_UsbOpen(lDeviceId As Integer) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_EthernetOpen(lDeviceId As Integer, ByRef ethernetConfig As LJV7IF_ETHERNET_CONFIG) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_CommClose(lDeviceId As Integer) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_RebootController(lDeviceId As Integer) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_RetrunToFactorySetting(lDeviceId As Integer) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_GetError(lDeviceId As Integer, byRcvMax As Byte, ByRef pbyErrCnt As Byte, pwErrCode As IntPtr) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_ClearError(lDeviceId As Integer, wErrCode As Short) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_Trigger(lDeviceId As Integer) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_StartMeasure(lDeviceId As Integer) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_StopMeasure(lDeviceId As Integer) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_AutoZero(lDeviceId As Integer, byOnOff As Byte, dwOut As UInteger) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_Timing(lDeviceId As Integer, byOnOff As Byte, dwOut As UInteger) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_Reset(lDeviceId As Integer, dwOut As UInteger) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_ClearMemory(lDeviceId As Integer) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_SetSetting(lDeviceId As Integer, byDepth As Byte, TargetSetting As LJV7IF_TARGET_SETTING, pData As IntPtr, dwDataSize As UInteger, ByRef pdwError As UInteger) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_GetSetting(lDeviceId As Integer, byDepth As Byte, TargetSetting As LJV7IF_TARGET_SETTING, pData As IntPtr, dwDataSize As UInteger) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_InitializeSetting(lDeviceId As Integer, byDepth As Byte, byTarget As Byte) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_ReflectSetting(lDeviceId As Integer, byDepth As Byte, ByRef pdwError As UInteger) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_RewriteTemporarySetting(lDeviceId As Integer, byDepth As Byte) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_CheckMemoryAccess(lDeviceId As Integer, ByRef pbyBusy As Byte) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_SetTime(lDeviceId As Integer, ByRef time As LJV7IF_TIME) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_GetTime(lDeviceId As Integer, ByRef time As LJV7IF_TIME) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_ChangeActiveProgram(lDeviceId As Integer, byProgNo As Byte) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_GetActiveProgram(lDeviceId As Integer, ByRef pbyProgNo As Byte) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_GetMeasurementValue(lDeviceId As Integer, <Out> pMeasureData As LJV7IF_MEASURE_DATA()) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_GetProfile(lDeviceId As Integer, ByRef pReq As LJV7IF_GET_PROFILE_REQ, ByRef pRsp As LJV7IF_GET_PROFILE_RSP, ByRef pProfileInfo As LJV7IF_PROFILE_INFO, pdwProfileData As IntPtr, dwDataSize As UInteger) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_GetBatchProfile(lDeviceId As Integer, ByRef pReq As LJV7IF_GET_BATCH_PROFILE_REQ, ByRef pRsp As LJV7IF_GET_BATCH_PROFILE_RSP, ByRef pProfileInfo As LJV7IF_PROFILE_INFO, pdwBatchData As IntPtr, dwDataSize As UInteger) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_GetProfileAdvance(lDeviceId As Integer, ByRef pProfileInfo As LJV7IF_PROFILE_INFO, pdwProfileData As IntPtr, dwDataSize As UInteger, <Out> pMeasureData As LJV7IF_MEASURE_DATA()) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_GetBatchProfileAdvance(lDeviceId As Integer, ByRef pReq As LJV7IF_GET_BATCH_PROFILE_ADVANCE_REQ, ByRef pRsp As LJV7IF_GET_BATCH_PROFILE_ADVANCE_RSP, ByRef pProfileInfo As LJV7IF_PROFILE_INFO, pdwBatchData As IntPtr, dwDataSize As UInteger, _
		<Out> pBatchMeasureData As LJV7IF_MEASURE_DATA(), <Out> pMeasureData As LJV7IF_MEASURE_DATA()) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_StartStorage(lDeviceId As Integer) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_StopStorage(lDeviceId As Integer) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_GetStorageStatus(lDeviceId As Integer, ByRef pReq As LJV7IF_GET_STRAGE_STATUS_REQ, ByRef pRsp As LJV7IF_GET_STRAGE_STATUS_RSP, ByRef pStorageInfo As LJV7IF_STORAGE_INFO) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_GetStorageData(lDeviceId As Integer, ByRef pReq As LJV7IF_GET_STORAGE_REQ, ByRef pStorageInfo As LJV7IF_STORAGE_INFO, ByRef pRsp As LJV7IF_GET_STORAGE_RSP, pdwData As IntPtr, dwDataSize As UInteger) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_GetStorageProfile(lDeviceId As Integer, ByRef pReq As LJV7IF_GET_STORAGE_REQ, ByRef pStorageInfo As LJV7IF_STORAGE_INFO, ByRef pRes As LJV7IF_GET_STORAGE_RSP, ByRef pProfileInfo As LJV7IF_PROFILE_INFO, pdwData As IntPtr, _
		dwDataSize As UInteger) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_GetStorageBatchProfile(lDeviceId As Integer, ByRef pReq As LJV7IF_GET_BATCH_PROFILE_STORAGE_REQ, ByRef pStorageInfo As LJV7IF_STORAGE_INFO, ByRef pRes As LJV7IF_GET_BATCH_PROFILE_STORAGE_RSP, ByRef pProfileInfo As LJV7IF_PROFILE_INFO, pdwData As IntPtr, _
		dwDataSize As UInteger, ByRef pTimeOffset As UInteger, <Out> pMeasureData As LJV7IF_MEASURE_DATA()) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_HighSpeedDataUsbCommunicationInitalize(lDeviceId As Integer, pCallBack As HighSpeedDataCallBack, dwProfileCnt As UInteger, dwThreadId As UInteger) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_HighSpeedDataEthernetCommunicationInitalize(lDeviceId As Integer, ByRef pEthernetConfig As LJV7IF_ETHERNET_CONFIG, wHighSpeedPortNo As UShort, pCallBack As HighSpeedDataCallBack, dwProfileCnt As UInteger, dwThreadId As UInteger) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_PreStartHighSpeedDataCommunication(lDeviceId As Integer, ByRef pReq As LJV7IF_HIGH_SPEED_PRE_START_REQ, ByRef pProfileInfo As LJV7IF_PROFILE_INFO) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_StartHighSpeedDataCommunication(lDeviceId As Integer) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_StopHighSpeedDataCommunication(lDeviceId As Integer) As Integer
	End Function

	<DllImport("LJV7_IF.dll")> _
	Friend Shared Function LJV7IF_HighSpeedDataCommunicationFinalize(lDeviceId As Integer) As Integer
	End Function
End Class
#End Region

