'Eason 20170216 Ticket:100080 , Add Arc Type Parameter

Imports ProjectCore

'Note: 註解先拿文件的參考，之後可以拿掉
'Note: Parameter base copy from CLineTypeParameter
Public Class CArcTypeParameter

#Region "Private Variable"

    Private _Name As String

    'Pre Dispense --------------------------------------------
    Private _PreMoveDelayFactor As Decimal
    Private _PreDownSpeed As Decimal
    Private _PreDownAcc As Decimal
    Private _PreDispenseGap As Decimal
    'During Dispense --------------------------------------------
    Private _DuringDispenseSpeed As Decimal
    Private _DuringShutOffDistance As Decimal
    'Post Dispense --------------------------------------------
    Private _PostSuckBack As Decimal
    Private _PostDwellTime As Decimal
    Private _PostBacktrackGap As Decimal
    Private _PostBacktrackLength As Decimal
    Private _PostBacktrackSpeed As Decimal
    Private _PostRetractDistance As Decimal
    Private _PostRetractSpeed As Decimal
    Private _PostRetractAcc As Decimal

#End Region

#Region "Public Property"

    ''' <summary>
    ''' [Arc Type Name]
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(value As String)
            _Name = value
        End Set
    End Property
    ''' <summary>
    ''' [穩定時間] , Unit => Sec , 通常 0.05 sec 足夠的
    ''' [Note]
    ''' Some fluids have a high viscosity which means that they do not dispense as quickly as lower viscosity
    ''' fluids. The Pre-Move Delay Factor increases the time that the Dispensing Head is parked with the valve
    ''' ON, prior to a programmed move. The delay at the beginning position insures that a full line is dispensed.
    ''' The units are seconds. Typically, 50 ms (0.050 sec) is sufficient for most underfill fluids.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PreMoveDelayFactor() As Decimal
        Get
            Return _PreMoveDelayFactor
        End Get
        Set(value As Decimal)
            _PreMoveDelayFactor = value
        End Set
    End Property
    ''' <summary>
    ''' [下降速度] , Unit => inch/Sec
    ''' [Note]
    ''' The Down Speed is how fast the Dispensing Head lowers to dispense. Low viscosity fluids tend to drip
    ''' and you may want to set this parameter higher. The units are inches/second. Typically, 2 in/sec
    ''' is sufficient.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PreDownSpeed() As Decimal
        Get
            Return _PreDownSpeed
        End Get
        Set(value As Decimal)
            _PreDownSpeed = value
        End Set
    End Property
    ''' <summary>
    ''' [下降加速度] , Unit => inch/Sec^2
    ''' [Note]
    ''' This is the parameter that controls how fast the Dispensing Head accelerates to the Down Speed. The
    ''' units are inches/second2. Typically, 300 in/sec2 is sufficient.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PreDownAcc() As Decimal
        Get
            Return _PreDownAcc
        End Get
        Set(value As Decimal)
            _PreDownAcc = value
        End Set
    End Property

    ''' <summary>
    ''' [速度] , Unit => inch/Sec
    ''' [Note]
    ''' Speed refers to how fast the Dispensing Head moves during dispensing. This parameter is actually not
    ''' used for weight control lines, but basic lines where the speed of the Dispensing Head affects the amount
    ''' of fluid dispensed. The units are inches/second.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DuringDispenseSpeed() As Decimal
        Get
            Return _DuringDispenseSpeed
        End Get
        Set(value As Decimal)
            _DuringDispenseSpeed = value
        End Set
    End Property
    ''' <summary>
    ''' [基底.尖針距離] , Unit => inch
    ''' [Note]
    ''' The Dispense Gap is the distance between the needle tip and the substrate during dispensing operations.
    ''' This distance is one of the more common adjustments made to optimize dispensing programs. The units
    ''' are in inches. Typically 15-20 mils (0.015-0.020 in) is sufficient.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PreDispenseGap() As Decimal
        'Public Property DuringDispenseGap() As Decimal 'Eason 20170117 Ticket:100010 , Change Variable
        Get
            Return _PreDispenseGap
        End Get
        Set(value As Decimal)
            _PreDispenseGap = value
        End Set
    End Property
    ''' <summary>
    ''' [Note]
    ''' Shut-Off Distance controls where dispensing is discontinued at the end of a move. Low viscosity fluids
    ''' require that you stop dispensing prior to the end of the move so that the fluid does not exceed the pattern
    ''' limits. The Shut-Off Distance is the point where the valve stops dispensing before the end of the move.
    ''' The units are in inches. Typically, 30 mils (0.030 in) is sufficient.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DuringShutOffDistance() As Decimal
        Get
            Return _DuringShutOffDistance
        End Get
        Set(value As Decimal)
            _DuringShutOffDistance = value
        End Set
    End Property
    ''' <summary>
    ''' [Note]
    ''' At the end of dispensing a line or a dot, the valve is reversed for the Suckback period to remove any
    ''' material from the tip of the needle. The Suckback value should be set according to fluid viscosity.
    ''' Typically, 10 ms (0.010 seconds) is sufficient.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PostSuckBack() As Decimal
        'Public Property DuringSuckBack() As Decimal 'Eason 20170117 Ticket:100010 , Change Variable
        Get
            Return _PostSuckBack
        End Get
        Set(value As Decimal)
            _PostSuckBack = value
        End Set
    End Property
    ''' <summary>
    ''' [Note]
    ''' The Dwell is how long the Dispensing Head stays at the final position of a move before retracting. This
    ''' allows residual fluid to detach from the needle tip before the Dispensing Head moves to the next location.
    ''' The units are seconds. Typically, 0-20 ms (0.020 sec) is sufficient.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PostDwellTime() As Decimal
        'Public Property DuringDwellTime() As Decimal 'Eason 20170117 Ticket:100010 , Change Variable
        Get
            Return _PostDwellTime
        End Get
        Set(value As Decimal)
            _PostDwellTime = value
        End Set
    End Property

    ''' <summary>
    ''' [Note]
    ''' This is the vertical distance that the Dispensing Head moves up after completion of a move. At the end
    ''' of a move, the Dispensing Head raises the Backtrack Gap, and then moves the Backtrack Length back
    ''' over the line that was just dispensed. This encourages congealed fluids to detach and distributes any
    ''' excess fluid back across the line. The units are inches. Backtrack Gap should be at least double the
    ''' Dispense Gap.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PostBacktrackGap() As Decimal
        Get
            Return _PostBacktrackGap
        End Get
        Set(value As Decimal)
            _PostBacktrackGap = value
        End Set
    End Property
    ''' <summary>
    ''' [Note]
    ''' This is the horizontal distance that the Dispensing Head moves back over the dispensed line after moving
    ''' up the Backtrack Gap distance. This encourages congealed fluids to detach and distributes any excess
    ''' fluid back across the line. The units are inches. Typically, 30-40 mils (0.030-0.040 in) is sufficient.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PostBacktrackLength() As Decimal
        Get
            Return _PostBacktrackLength
        End Get
        Set(value As Decimal)
            _PostBacktrackLength = value
        End Set
    End Property
    ''' <summary>
    ''' [Note]
    ''' This is the speed that the Dispensing Head travels when moving along the Backtrack Length. The units
    ''' are inches/second. Typically, 2 in/sec is sufficient.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PostBacktrackSpeed() As Decimal
        Get
            Return _PostBacktrackSpeed
        End Get
        Set(value As Decimal)
            _PostBacktrackSpeed = value
        End Set
    End Property
    ''' <summary>
    ''' [Note]
    ''' This is a vertical distance that the Dispensing Head moves after the Backtrack Gap and Backtrack Length
    ''' dispensing commands have completed. Figure 6-13 shows the relationship between Retract Distance,
    ''' Backtrack Gap and Backtrack Length. The Retract Distance must be within the Safe-Z Height defined in
    ''' Initial Setup. The units are inches. Typically, 250 mils (0.250 inch) is sufficient.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PostRetractDistance() As Decimal
        Get
            Return _PostRetractDistance
        End Get
        Set(value As Decimal)
            _PostRetractDistance = value
        End Set
    End Property
    ''' <summary>
    ''' [Note]
    ''' This is the speed of the Dispensing Head while moving the Retract Distance. The units are in
    ''' inches/second. Typically, 2 in/sec is sufficient.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PostRetractSpeed() As Decimal
        Get
            Return _PostRetractSpeed
        End Get
        Set(value As Decimal)
            _PostRetractSpeed = value
        End Set
    End Property
    ''' <summary>
    ''' [Note]
    ''' This is the parameter that controls how fast the Dispensing Head comes up to the Retract Speed. You may
    ''' need to set this high for small dispensing moves. The units are inches/second2. Typically, 300 in/sec2 is
    ''' sufficient.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PostRetractAcc() As Decimal
        Get
            Return _PostRetractAcc
        End Get
        Set(value As Decimal)
            _PostRetractAcc = value
        End Set
    End Property
#End Region

#Region "Public Function"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sName">Arc Type Name</param>
    ''' <remarks></remarks>
    Public Sub New(sName As String)
        _Name = sName
        _PreMoveDelayFactor = 0
        _PreDownSpeed = 100
        _PreDownAcc = 2940
        _PreDispenseGap = 2
        _DuringDispenseSpeed = 10
        _DuringShutOffDistance = 0
        _PostSuckBack = 0
        _PostDwellTime = 0
        _PostBacktrackGap = 0
        _PostBacktrackLength = 0
        _PostBacktrackSpeed = 100
        _PostRetractDistance = 0
        _PostRetractSpeed = 100
        _PostRetractAcc = 2940
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
            strSection = "ArcValve"
            SaveIniString(strSection, "Name", _Name, fileName)
            'Pre
            SaveIniString(strSection, "PreMoveDelayFactor", _PreMoveDelayFactor, fileName)
            SaveIniString(strSection, "PreDownSpeed", _PreDownSpeed, fileName)
            SaveIniString(strSection, "PreDownAcc", _PreDownAcc, fileName)
            SaveIniString(strSection, "PreDispenseGap", _PreDispenseGap, fileName)
            'During
            SaveIniString(strSection, "DuringDispenseSpeed", _DuringDispenseSpeed, fileName)
            SaveIniString(strSection, "DuringShutOffDistance", _DuringShutOffDistance, fileName)
            'Post
            SaveIniString(strSection, "PostSuckBack", _PostSuckBack, fileName)
            SaveIniString(strSection, "PostDwellTime", _PostDwellTime, fileName)
            SaveIniString(strSection, "PostBacktrackGap", _PostBacktrackGap, fileName)
            SaveIniString(strSection, "PostBacktrackLength", _PostBacktrackLength, fileName)
            SaveIniString(strSection, "PostBacktrackSpeed", _PostBacktrackSpeed, fileName)
            SaveIniString(strSection, "PostRetractDistance", _PostRetractDistance, fileName)
            SaveIniString(strSection, "PostRetractSpeed", _PostRetractSpeed, fileName)
            SaveIniString(strSection, "PostRetractAcc", _PostRetractAcc, fileName)
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
            strSection = "ArcValve"
            _Name = ReadIniString(strSection, "Name", fileName)
            'Pre 
            _PreMoveDelayFactor = Val(ReadIniString(strSection, "PreMoveDelayFactor", fileName, 0.0))
            _PreDownSpeed = Val(ReadIniString(strSection, "PreDownSpeed", fileName, 0.0))
            _PreDownAcc = Val(ReadIniString(strSection, "PreDownAcc", fileName, 0.0))
            _PreDispenseGap = Val(ReadIniString(strSection, "PreDispenseGap", fileName, 0.0))
            'During
            _DuringDispenseSpeed = Val(ReadIniString(strSection, "DuringDispenseSpeed", fileName, 0.0))
            _DuringShutOffDistance = Val(ReadIniString(strSection, "DuringShutOffDistance", fileName, 0))
            'Post
            _PostSuckBack = Val(ReadIniString(strSection, "PostSuckBack", fileName, 0.0))
            _PostDwellTime = Val(ReadIniString(strSection, "PostDwellTime", fileName, 0.0))
            _PostBacktrackGap = Val(ReadIniString(strSection, "PostBacktrackGap", fileName, 0.0))
            _PostBacktrackLength = Val(ReadIniString(strSection, "PostBacktrackLength", fileName, 0.0))
            _PostBacktrackSpeed = Val(ReadIniString(strSection, "PostBacktrackSpeed", fileName, 0.0))
            _PostRetractDistance = Val(ReadIniString(strSection, "PostRetractDistance", fileName, 0.0))
            _PostRetractSpeed = Val(ReadIniString(strSection, "PostRetractSpeed", fileName, 0.0))
            _PostRetractAcc = Val(ReadIniString(strSection, "PostRetractAcc", fileName, 0.0))

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
