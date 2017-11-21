'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter
'Eason 20170117 Ticket:100010 , Change Variable

Imports ProjectCore

'Note: 註解先拿文件的參考，之後可以拿掉

Public Class CDotTypeParameter

#Region "Private Variable"

    Private _Name As String

    'Pre Dispense --------------------------------------------
    Private _PreSettlingTime As Decimal
    Private _PreDownSpeed As Decimal
    Private _PreDownAcc As Decimal
    Private _PreDispenseGap As Decimal
    'During Dispense --------------------------------------------
    Private _DuringValveOnTime As Decimal
    'Private _DuringDispenseGap As Decimal 'Eason 20170117 Ticket:100010 , Change Variable
    Private _DuringNumberOfShots As Integer
    Private _DuringMultiShotDelta As Decimal
    'Post Dispense --------------------------------------------
    Private _PostDwellTime As Decimal
    Private _PostRetractDistance As Decimal
    Private _PostRetractSpeed As Decimal
    Private _PostRetractAcc As Decimal
    Private _PostSuckBack As Decimal

#End Region

#Region "Public Property"

    ''' <summary>
    ''' [Dot Type Name]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(value As String)
            _Name = Value
        End Set
    End Property

    ''' <summary>
    ''' [穩定時間] , Unit => Sec
    ''' [Note]
    ''' This is the length of time that the Dispensing Head will park over the dispensing location before
    ''' beginning to dispense. The Settling Time is set to 0, except in special circumstances. For more
    ''' information, contact Asymtek Application Engineering.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PreSettlingTime() As Decimal
        Get
            Return _PreSettlingTime
        End Get
        Set(value As Decimal)
            _PreSettlingTime = Value
        End Set
    End Property
    ''' <summary>
    ''' [下降速度] , Unit => inch/Sec
    ''' [Note]
    ''' The Down Speed is how fast the Dispensing Head lowers to dispense. Low viscosity fluids tend to drip
    ''' and you may want to set this parameter to a high value. The higher the value, the faster the Dispensing
    ''' Head will move. Height sense accuracy is dependent on Down Speed and Down Acceleration. The units
    ''' are inches/second. Typically, 2 in/sec is the default setting. Refer to the manual for your particular
    ''' dispensing system for information on your Height Sensor.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PreDownSpeed() As Decimal
        Get
            Return _PreDownSpeed
        End Get
        Set(value As Decimal)
            _PreDownSpeed = Value
        End Set
    End Property
    ''' <summary>
    ''' [下降加速度] , Unit => inch/Sec^2
    ''' [Note]
    ''' This parameter controls how fast the Dispensing Head accelerates to the Down Speed. The higher the
    ''' value, the faster the Dispensing Head will move. The units are in inches/second squared. Typically, 300
    ''' in/sec2 is the default setting.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PreDownAcc() As Decimal
        Get
            Return _PreDownAcc
        End Get
        Set(value As Decimal)
            _PreDownAcc = Value
        End Set
    End Property

    ''' <summary>
    ''' [閥開啟時間] , Unit => Sec
    ''' [Note]
    ''' This parameter controls the time the valve is on while positioned over the dispensing location. The time
    ''' interval affects dot size. The longer the Valve-On Time, the larger the dot. The units are in seconds. The
    ''' typical value is 30 milliseconds, however, the Valve-On Time can be as long as 6 seconds, depending on
    ''' the desired dot size and fluid properties.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DuringValveOnTime() As Decimal
        Get
            Return _DuringValveOnTime
        End Get
        Set(value As Decimal)
            _DuringValveOnTime = Value
        End Set
    End Property
    ''' <summary>
    ''' [基底.尖針距離] , Unit => inch
    ''' [Note]
    ''' The Dispense Gap is the distance between the substrate you are dispensing on and the needle tip during
    ''' dispensing. This distance is one of the more common adjustments to optimize dispensing programs. The
    ''' units are in inches. Typically, the Dispense Gap is set to half the dot diameter.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PreDispenseGap() As Decimal 'Eason 20170117 Ticket:100010 , Change Variable
        Get
            Return _PreDispenseGap
        End Get
        Set(value As Decimal)
            _PreDispenseGap = value
        End Set
    End Property
    ''' <summary>
    ''' [同一位置多少次] , Unit => Count
    ''' [Note]
    ''' This parameter controls how many shots of fluid will be dispensed at a single location. When making
    ''' larger dots, it is desirable to move the needle up between shots at the same location. This parameter is set
    ''' greater than one (1) for more than one dot at the same location, and the Multi-shot Delta is the vertical
    ''' retract distance of the needle between each shot. The units are in shots. Typically, this is set to one (1)
    ''' unless you are dispensing as described above.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DuringNumberOfShots() As Decimal
        Get
            Return _DuringNumberOfShots
        End Get
        Set(value As Decimal)
            _DuringNumberOfShots = Value
        End Set
    End Property
    ''' <summary>
    ''' [同一位置每次回收高度] , Unit => inch
    ''' [Note]
    ''' This is the height that the Dispensing Head retracts between multiple shots at the same location. It is used
    ''' in conjunction with Number of Shots. The units are in inches. If using with Number of Shots, a typical
    ''' value is 0.010 inch.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DuringMultiShotDelta() As Decimal
        Get
            Return _DuringMultiShotDelta
        End Get
        Set(value As Decimal)
            _DuringMultiShotDelta = Value
        End Set
    End Property

    ''' <summary>
    ''' [點膠後穩定時間] , Unit => Sec
    ''' [Note]
    ''' Dwell Time is how long the Dispensing Head stays at the final position of a move before retracting. This
    ''' gives the fluid a chance to attach to the substrate. Increasing the Dwell can reduce “stringing” of material.
    ''' The units are seconds. For most solder pastes and adhesives, 30 ms is sufficient.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PostDwellTime() As Decimal
        Get
            Return _PostDwellTime
        End Get
        Set(value As Decimal)
            _PostDwellTime = Value
        End Set
    End Property
    ''' <summary>
    ''' [回復後高度] , Unit => inch
    ''' [Note]
    ''' This is a vertical distance that the Dispensing Head moves after dispensing. The Retract Distance must be
    ''' within the Safe Z Height defined in Initial Setup. The Retract Distance can be reduced to increase
    ''' dispensing speed. However, the dot quality will be affected in viscous fluids. The units are inches.
    ''' Typically, 100 mils (0.1 in) is sufficient.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PostRetractDistance() As Decimal
        Get
            Return _PostRetractDistance
        End Get
        Set(value As Decimal)
            _PostRetractDistance = Value
        End Set
    End Property
    ''' <summary>
    ''' [回復速度] , Unit => inch/Sec
    ''' [Note]
    ''' This is the speed of the Dispensing Head while moving the Retract Distance. This parameter affects when
    ''' the Dispensing Head moves to the next dot. The units are in inches/second. Typically, 2 in/sec
    ''' is sufficient.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PostRetractSpeed() As Decimal
        Get
            Return _PostRetractSpeed
        End Get
        Set(value As Decimal)
            _PostRetractSpeed = Value
        End Set
    End Property
    ''' <summary>
    ''' [回附加速度] , Unit => inch/Sec^2
    ''' [Note]
    ''' This is the parameter that controls how fast the Dispensing Head comes up to the Retract Speed. You may
    ''' need to set this high for small dispensing moves. The units are inches/second squared. Typically, 300
    ''' in/sec2 is sufficient.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PostRetractAcc() As Decimal
        Get
            Return _PostRetractAcc
        End Get
        Set(value As Decimal)
            _PostRetractAcc = Value
        End Set
    End Property
    ''' <summary>
    ''' [回吸時間] , Unit => Sec
    ''' [Note]
    ''' At the end of dispensing a line or a dot, the valve is reversed for the Suckback period to remove any
    ''' material from the tip of the needle. The Suckback value should be set according to fluid viscosity.
    ''' Typically, 10 ms (0.010 sec) is sufficient.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PostSuckBack() As Decimal
        Get
            Return _PostSuckBack
        End Get
        Set(value As Decimal)
            _PostSuckBack = Value
        End Set
    End Property

#End Region

#Region "Public Function"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sName">Dot Type Name</param>
    ''' <remarks></remarks>
    Public Sub New(sName As String)
        _Name = sName
        _PreSettlingTime = 0
        _PreDownSpeed = 100
        _PreDownAcc = 2940
        _PreDispenseGap = 2
        _DuringValveOnTime = 10 'ms
        _DuringNumberOfShots = 1
        _DuringMultiShotDelta = 0
        _PostDwellTime = 0
        _PostRetractDistance = 0
        _PostRetractSpeed = 100
        _PostRetractAcc = 2940
        _PostSuckBack = 0
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal fileName As String) As Boolean
        Try
            Dim strSection As String
            strSection = "DotValve"
            SaveIniString(strSection, "Name", _Name, fileName)
            'Pre
            SaveIniString(strSection, "PreSettlingTime", _PreSettlingTime, fileName)
            SaveIniString(strSection, "PreDownSpeed", _PreDownSpeed, fileName)
            SaveIniString(strSection, "PreDownAcc", _PreDownAcc, fileName)
            SaveIniString(strSection, "PreDispenseGap", _PreDispenseGap, fileName) 'Eason 20170117 Ticket:100010 , Change Variable
            'During
            SaveIniString(strSection, "DuringValveOnTime", _DuringValveOnTime, fileName)
            'SaveIniString(strSection, "DuringDispenseGap", _PreDispenseGap, fileName)'Eason 20170117 Ticket:100010 , Change Variable
            SaveIniString(strSection, "DuringNumberOfShots", _DuringNumberOfShots, fileName)
            SaveIniString(strSection, "DuringMultiShotDelta", _DuringMultiShotDelta, fileName)
            'Post
            SaveIniString(strSection, "PostDwellTime", _PostDwellTime, fileName)
            SaveIniString(strSection, "PostRetractDistance", _PostRetractDistance, fileName)
            SaveIniString(strSection, "PostRetractSpeed", _PostRetractSpeed, fileName)
            SaveIniString(strSection, "PostRetractAcc", _PostRetractAcc, fileName)
            SaveIniString(strSection, "PostSuckBack", _PostSuckBack, fileName)
            Return True

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002007), "Error_1002007", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002007) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try

    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Load(ByVal fileName As String) As Boolean
        Try

            Dim strSection As String
            strSection = "DotValve"
            _Name = ReadIniString(strSection, "Name", fileName)
            'Pre 
            _PreSettlingTime = Val(ReadIniString(strSection, "PreSettlingTime", fileName, 0.0))
            _PreDownSpeed = Val(ReadIniString(strSection, "PreDownSpeed", fileName, 0.0))
            _PreDownAcc = Val(ReadIniString(strSection, "PreDownAcc", fileName, 0.0))
            _PreDispenseGap = Val(ReadIniString(strSection, "PreDispenseGap", fileName, 0.0)) 'Eason 20170117 Ticket:100010 , Change Variable
            'During
            _DuringValveOnTime = Val(ReadIniString(strSection, "DuringValveOnTime", fileName, 0.0))
            '_PreDispenseGap = Val(ReadIniString(strSection, "DuringDispenseGap", fileName, 0.0)) 'Eason 20170117 Ticket:100010 , Change Variable
            _DuringNumberOfShots = Val(ReadIniString(strSection, "DuringNumberOfShots", fileName, 0))
            _DuringMultiShotDelta = Val(ReadIniString(strSection, "DuringMultiShotDelta", fileName, 0.0))
            'Post
            _PostDwellTime = Val(ReadIniString(strSection, "PostDwellTime", fileName, 0.0))
            _PostRetractDistance = Val(ReadIniString(strSection, "PostRetractDistance", fileName, 0.0))
            _PostRetractSpeed = Val(ReadIniString(strSection, "PostRetractSpeed", fileName, 0.0))
            _PostRetractAcc = Val(ReadIniString(strSection, "PostRetractAcc", fileName, 0.0))
            _PostSuckBack = Val(ReadIniString(strSection, "PostSuckBack", fileName, 0.0))
            Return True

        Catch ex As Exception
            gSyslog.Save(gMsgHandler.GetMessage(Error_1002006), "Error_1002006", eMessageLevel.Error)
            gSyslog.Save("Exception Message: " & ex.Message, , eMessageLevel.Error)
            MsgBox(gMsgHandler.GetMessage(Error_1002006) & ex.Message, MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Return False
        End Try

    End Function

#End Region

End Class
