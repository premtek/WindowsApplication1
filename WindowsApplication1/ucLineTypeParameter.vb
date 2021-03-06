﻿'Eason 20170109 Ticket:100010 , Add Dot Line Recipe Parameter
Imports ProjectRecipe
Imports ProjectCore

''' <summary>
''' Line Parameter Class
''' </summary>
''' <remarks></remarks>
Public Class ucLineTypeParameter

#Region "Enum"

    ''' <summary>
    ''' Min : only Data show
    ''' Total : have IO Save/Update/Del/Select 
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ShowType As Integer
        Min
        Total
    End Enum
#End Region

#Region "Const"

    Private sizeCol0Width As Integer = 328

    Private sizeCol1Width As Integer = 76

    Private FileExtension As String = ".ldb"

    Private FilePath As String = Application.StartupPath & "\Database\LineValue\"

#End Region

#Region "Private Var"

    Private mlstLineValueSelectedItem As String = ""

    Private mOutLink_LineValueDB As New Dictionary(Of String, CLineTypeParameter)

    Private mNowShowData As CLineTypeParameter = Nothing

    Private mShowType As ShowType = ShowType.Total

    Dim TipType As New ToolTip()

#End Region

#Region "Private Function"

    Private Sub fnBeginInvokeTextBackColor(ByVal usContorl As Control, ByVal cColor As Color)
        '20170929 Toby_ Add 判斷
        If (Not IsNothing(Parent)) Then
            Me.BeginInvoke(Sub()
                               usContorl.BackColor = cColor
                           End Sub)
        End If
    End Sub

    Private Function fnGetLineValueDbFileName(sName As String) As String
        Return Application.StartupPath & "\Database\LineValue\" & sName & ".ldb"
    End Function

    Private Sub fnSetupToolTip()

        TipType = New ToolTip()
        TipType.Active = True  '取得或設定值，指出工具提示目前是否在作用中。
        TipType.AutomaticDelay = 200  '取得或設定工具提示的自動延遲。
        TipType.AutoPopDelay = 20000    '取得或設定當指標靜止於含指定工具提示文字的控制項上時，工具提示保持可見的時間。
        TipType.BackColor = Color.Black   '取得或設定工具提示的背景色彩。
        'TipType.CanRaiseEvents = True  '取得值，指出元件是否能引發事件。 (繼承自 Component)。
        'TipType.Container = AcceptButton   '取得包含 Component 的 IContainer。 (繼承自 Component)。
        'TipType.CreateParams()    '基礎架構。取得工具提示視窗的建立參數。
        'TipType.DesignMode()  '取得值，指出 Component 目前是否處於設計模式。 (繼承自 Component)。
        'TipType.Events()  '取得附加在這個 Component 上的事件處理常式清單。 (繼承自 Component)。
        TipType.ForeColor = Color.Lime   '取得或設定工具提示的前景色彩。
        TipType.InitialDelay = 1    '取得或設定在工具提示出現之前，所經過的時間。
        TipType.IsBalloon = True   '取得或設定值，指出工具提示是否應該使用汽球樣式的視窗。
        TipType.OwnerDraw = True   '取得或設定值，指出要由作業系統繪製工具提示或是由您提供的程式碼繪製。
        TipType.ReshowDelay = 1 '(取得或設定當指標從某個控制項移動到另一個控制項時, 在後續工具提示視窗出現之前, 必須經)
        TipType.ShowAlways = True  '取得或設定值，指出即使父控制項為非現用時，是否也會顯示工具提示視窗。
        'TipType.Site    '取得或設定 Component 的 ISite。 (繼承自 Component)。
        TipType.StripAmpersands = True '取得或設定值，以便判斷連字號 (&) 字元的處理方式。
        'TipType.Tag '取得或設定物件，其中含有與 ToolTip 關聯之程式設計人員提供的資料。
        TipType.ToolTipIcon = ToolTipIcon.Info '取得或設定值，以便定義要顯示在工具提示文字旁的圖示類型。
        TipType.ToolTipTitle = "Format"  '取得或設定工具提示視窗的標題。
        TipType.UseAnimation = True    '取得或設定值，以便判斷顯示工具提示時是否應該使用動畫效果。
        TipType.UseFading = True   '取得或設定值，以便判斷顯示工具提示時是否應該使用淡出效果。
        TipType.IsBalloon = True '取得或設定值，指出工具提示是否應該使用汽球樣式的視窗。

        TipType.SetToolTip(textLinePreMoveDelayFactor, "Value : >= 0 ")
        TipType.SetToolTip(textLinePreDownSpeed, "Value : > 10 ")
        TipType.SetToolTip(textLinePreDownAcc, "Value : > 0 ")
        TipType.SetToolTip(textLinePreDispenseGap, "Value : + , - , 0 ")

        TipType.SetToolTip(textLineDuringSpeed, "Value : > 0 ")
        TipType.SetToolTip(textLineDuringShutOffDistance, "Value : > 0 ")

        TipType.SetToolTip(textLinePostSuckBack, "Value : >= 0 ")
        TipType.SetToolTip(textLinePostDwell, "Value : >= 0 ")
        TipType.SetToolTip(textLinePostBacktrackGap, "Value : >= 0 ")
        TipType.SetToolTip(textLinePostBacktrackLength, "Value : >= 0 ")
        TipType.SetToolTip(textLinePostBacktrackSpeed, "Value : > 0 ")
        TipType.SetToolTip(textLinePostRetractDistance, "Value : >= 0 ")
        TipType.SetToolTip(textLinePostRetractSpeed, "Value : > 10 ")
        TipType.SetToolTip(textLinePostRetractAcc, "Value : > 0 ")

    End Sub
    Private Function fnCheckLineTextboxData() As Boolean

        Dim DataCheck As Boolean = True

        'Pre- Dispense -------------------------------------------------------------------
        'PreMoveDelayFactor : >= 0 textLinePreMoveDelayFactor
        If (IsNumeric(textLinePreMoveDelayFactor.Text) AndAlso Val(textLinePreMoveDelayFactor.Text) >= 0) Then
            fnBeginInvokeTextBackColor(textLinePreMoveDelayFactor, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textLinePreMoveDelayFactor, Color.Yellow)
            DataCheck = False
        End If
        'PreDownSpeed : > 10 textLinePreDownSpeed
        If (IsNumeric(textLinePreDownSpeed.Text) AndAlso Val(textLinePreDownSpeed.Text) > 10) Then
            fnBeginInvokeTextBackColor(textLinePreDownSpeed, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textLinePreDownSpeed, Color.Yellow)
            DataCheck = False
        End If
        'PreDownAcc : > 0 textLinePreDownAcc
        If (IsNumeric(textLinePreDownAcc.Text) AndAlso Val(textLinePreDownAcc.Text) > 0) Then
            fnBeginInvokeTextBackColor(textLinePreDownAcc, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textLinePreDownAcc, Color.Yellow)
            DataCheck = False
        End If
        'PreDispenseGap : +-  textLinePreDispenseGap
        If (IsNumeric(textLinePreDispenseGap.Text)) Then
            fnBeginInvokeTextBackColor(textLinePreDispenseGap, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textLinePreDispenseGap, Color.Yellow)
            DataCheck = False
        End If
        'During- Dispense -------------------------------------------------------------------

        'Speed : > 0 textLineDuringSpeed
        If (IsNumeric(textLineDuringSpeed.Text) AndAlso Val(textLineDuringSpeed.Text) > 0) Then
            fnBeginInvokeTextBackColor(textLineDuringSpeed, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textLineDuringSpeed, Color.Yellow)
            DataCheck = False
        End If
        'ShutOffDistance : >= 0 textLineDuringShutOffDistance
        If (IsNumeric(textLineDuringShutOffDistance.Text) AndAlso Val(textLineDuringShutOffDistance.Text) >= 0) Then
            fnBeginInvokeTextBackColor(textLineDuringShutOffDistance, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textLineDuringShutOffDistance, Color.Yellow)
            DataCheck = False
        End If
        'Post- Dispense -------------------------------------------------------------------

        'PostSuckBack >= 0 textLinePostSuckBack
        If (IsNumeric(textLinePostSuckBack.Text) AndAlso Val(textLinePostSuckBack.Text) >= 0) Then
            fnBeginInvokeTextBackColor(textLinePostSuckBack, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textLinePostSuckBack, Color.Yellow)
            DataCheck = False
        End If
        'PostDwell >= 0 textLinePostDwell
        If (IsNumeric(textLinePostDwell.Text) AndAlso Val(textLinePostDwell.Text) >= 0) Then
            fnBeginInvokeTextBackColor(textLinePostDwell, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textLinePostDwell, Color.Yellow)
            DataCheck = False
        End If
        'PostBacktrackGap >= 0 textLinePostBacktrackGap
        If (IsNumeric(textLinePostBacktrackGap.Text) AndAlso Val(textLinePostBacktrackGap.Text) >= 0) Then
            fnBeginInvokeTextBackColor(textLinePostBacktrackGap, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textLinePostBacktrackGap, Color.Yellow)
            DataCheck = False
        End If
        'PostBacktrackLength >= 0 textLinePostBacktrackLength
        If (IsNumeric(textLinePostBacktrackLength.Text) AndAlso Val(textLinePostBacktrackLength.Text) >= 0) Then
            fnBeginInvokeTextBackColor(textLinePostBacktrackLength, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textLinePostBacktrackLength, Color.Yellow)
            DataCheck = False
        End If
        'PostBacktrackSpeed > 0 textLinePostBacktrackSpeed
        If (IsNumeric(textLinePostBacktrackSpeed.Text) AndAlso Val(textLinePostBacktrackSpeed.Text) > 0) Then
            fnBeginInvokeTextBackColor(textLinePostBacktrackSpeed, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textLinePostBacktrackSpeed, Color.Yellow)
            DataCheck = False
        End If
        'PostRetractDistance >= 0 textLinePostRetractDistance
        If (IsNumeric(textLinePostRetractDistance.Text) AndAlso Val(textLinePostRetractDistance.Text) >= 0) Then
            fnBeginInvokeTextBackColor(textLinePostRetractDistance, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textLinePostRetractDistance, Color.Yellow)
            DataCheck = False
        End If
        'PostRetractSpeed > 10 textLinePostRetractSpeed
        If (IsNumeric(textLinePostRetractSpeed.Text) AndAlso Val(textLinePostRetractSpeed.Text) > 10) Then
            fnBeginInvokeTextBackColor(textLinePostRetractSpeed, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textLinePostRetractSpeed, Color.Yellow)
            DataCheck = False
        End If
        'PostRetractAcc > 0 textLinePostRetractAcc
        If (IsNumeric(textLinePostRetractAcc.Text) AndAlso Val(textLinePostRetractAcc.Text) > 0) Then
            fnBeginInvokeTextBackColor(textLinePostRetractAcc, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textLinePostRetractAcc, Color.Yellow)
            DataCheck = False
        End If

        '==================================================================================
        ' Below old
        ''Pre- Dispense -------------------------------------------------------------------
        ''PreMoveDelayFactor : Can set 0
        'If (Not IsNumeric(textLinePreMoveDelayFactor.Text) Or Val(textLinePreMoveDelayFactor.Text) < 0) Then
        '    fnBeginInvokeTextBackColor(textLinePreMoveDelayFactor, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textLinePreMoveDelayFactor, System.Drawing.SystemColors.Window)
        'End If
        'If (Not IsNumeric(textLinePreDownSpeed.Text) Or Val(textLinePreDownSpeed.Text) <= 0) Then
        '    fnBeginInvokeTextBackColor(textLinePreDownSpeed, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textLinePreDownSpeed, System.Drawing.SystemColors.Window)
        'End If

        'If (Not IsNumeric(textLinePreDownAcc.Text) Or Val(textLinePreDownAcc.Text) <= 0) Then
        '    fnBeginInvokeTextBackColor(textLinePreDownAcc, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textLinePreDownAcc, System.Drawing.SystemColors.Window)
        'End If
        'If (Not IsNumeric(textLinePreDispenseGap.Text) Or Val(textLinePreDispenseGap.Text) < 0) Then
        '    fnBeginInvokeTextBackColor(textLinePreDispenseGap, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textLinePreDispenseGap, System.Drawing.SystemColors.Window)
        'End If
        ''During- Dispense -------------------------------------------------------------------
        'If (Not IsNumeric(textLineDuringSpeed.Text) Or Val(textLineDuringSpeed.Text) <= 0) Then
        '    fnBeginInvokeTextBackColor(textLineDuringSpeed, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textLineDuringSpeed, System.Drawing.SystemColors.Window)
        'End If
        'If (Not IsNumeric(textLineDuringShutOffDistance.Text) Or Val(textLineDuringShutOffDistance.Text) < 0) Then
        '    fnBeginInvokeTextBackColor(textLineDuringShutOffDistance, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textLineDuringShutOffDistance, System.Drawing.SystemColors.Window)
        'End If
        ''Post- Dispense -------------------------------------------------------------------
        'If (Not IsNumeric(textLinePostSuckBack.Text) Or Val(textLinePostSuckBack.Text) < 0) Then
        '    fnBeginInvokeTextBackColor(textLinePostSuckBack, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textLinePostSuckBack, System.Drawing.SystemColors.Window)
        'End If
        'If (Not IsNumeric(textLinePostDwell.Text) Or Val(textLinePostDwell.Text) < 0) Then
        '    fnBeginInvokeTextBackColor(textLinePostDwell, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textLinePostDwell, System.Drawing.SystemColors.Window)
        'End If
        'If (Not IsNumeric(textLinePostBacktrackGap.Text) Or Val(textLinePostBacktrackGap.Text) < 0) Then
        '    fnBeginInvokeTextBackColor(textLinePostBacktrackGap, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textLinePostBacktrackGap, System.Drawing.SystemColors.Window)
        'End If
        'If (Not IsNumeric(textLinePostBacktrackLength.Text) Or Val(textLinePostBacktrackLength.Text) < 0) Then
        '    fnBeginInvokeTextBackColor(textLinePostBacktrackLength, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textLinePostBacktrackLength, System.Drawing.SystemColors.Window)
        'End If
        'If (Not IsNumeric(textLinePostBacktrackSpeed.Text) Or Val(textLinePostBacktrackSpeed.Text) <= 0) Then
        '    fnBeginInvokeTextBackColor(textLinePostBacktrackSpeed, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textLinePostBacktrackSpeed, System.Drawing.SystemColors.Window)
        'End If
        'If (Not IsNumeric(textLinePostRetractDistance.Text) Or Val(textLinePostRetractDistance.Text) < 0) Then
        '    fnBeginInvokeTextBackColor(textLinePostRetractDistance, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textLinePostRetractDistance, System.Drawing.SystemColors.Window)
        'End If
        'If (Not IsNumeric(textLinePostRetractSpeed.Text) Or Val(textLinePostRetractSpeed.Text) <= 0) Then
        '    fnBeginInvokeTextBackColor(textLinePostRetractSpeed, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textLinePostRetractSpeed, System.Drawing.SystemColors.Window)
        'End If
        'If (Not IsNumeric(textLinePostRetractAcc.Text) Or Val(textLinePostRetractAcc.Text) <= 0) Then
        '    fnBeginInvokeTextBackColor(textLinePostRetractAcc, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textLinePostRetractAcc, System.Drawing.SystemColors.Window)
        'End If
        '-----------------------------------------------------------------------------------------------
        Return DataCheck

    End Function

    Private Function GetLineValveDB(ByRef lineValveParam As CLineTypeParameter) As Boolean

        If (fnCheckLineTextboxData() = True) Then

            lineValveParam.Name = txtLineDB.Text

            lineValveParam.PreMoveDelayFactor = Val(textLinePreMoveDelayFactor.Text)
            lineValveParam.PreDownSpeed = Val(textLinePreDownSpeed.Text)
            lineValveParam.PreDownAcc = Val(textLinePreDownAcc.Text)
            lineValveParam.PreDispenseGap = Val(textLinePreDispenseGap.Text)

            lineValveParam.DuringDispenseSpeed = Val(textLineDuringSpeed.Text)
            lineValveParam.DuringShutOffDistance = Val(textLineDuringShutOffDistance.Text)

            lineValveParam.PostSuckBack = Val(textLinePostSuckBack.Text)
            lineValveParam.PostDwellTime = Val(textLinePostDwell.Text)
            lineValveParam.PostBacktrackGap = Val(textLinePostBacktrackGap.Text)
            lineValveParam.PostBacktrackLength = Val(textLinePostBacktrackLength.Text)
            lineValveParam.PostBacktrackSpeed = Val(textLinePostBacktrackSpeed.Text)
            lineValveParam.PostRetractDistance = Val(textLinePostRetractDistance.Text)
            lineValveParam.PostRetractSpeed = Val(textLinePostRetractSpeed.Text)
            lineValveParam.PostRetractAcc = Val(textLinePostRetractAcc.Text)
        Else
            Return False
        End If

        Return True

    End Function

    Private Sub ShowLineValue(ByVal lineValveParam As CLineTypeParameter)
        '20170929 Toby_ Add 判斷
        If (Not IsNothing(Parent)) Then
            Me.BeginInvoke(Sub()
                               txtLineDB.Text = lineValveParam.Name

                               textLinePreMoveDelayFactor.Text = lineValveParam.PreMoveDelayFactor
                               textLinePreDownSpeed.Text = lineValveParam.PreDownSpeed
                               textLinePreDownAcc.Text = lineValveParam.PreDownAcc

                               textLinePreDispenseGap.Text = lineValveParam.PreDispenseGap
                               textLineDuringSpeed.Text = lineValveParam.DuringDispenseSpeed
                               textLineDuringShutOffDistance.Text = lineValveParam.DuringShutOffDistance
                               textLinePostSuckBack.Text = lineValveParam.PostSuckBack
                               textLinePostDwell.Text = lineValveParam.PostDwellTime

                               textLinePostBacktrackGap.Text = lineValveParam.PostBacktrackGap
                               textLinePostBacktrackLength.Text = lineValveParam.PostBacktrackLength
                               textLinePostBacktrackSpeed.Text = lineValveParam.PostBacktrackSpeed
                               textLinePostRetractDistance.Text = lineValveParam.PostRetractDistance
                               textLinePostRetractSpeed.Text = lineValveParam.PostRetractSpeed
                               textLinePostRetractAcc.Text = lineValveParam.PostRetractAcc
                           End Sub)
        End If
    End Sub

#End Region

#Region "Public Function , Property"

    ''' <summary>
    ''' Select Recipe Name 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SelectItemName As String

        Set(value As String)
            mlstLineValueSelectedItem = value

        End Set
        Get
            Return mlstLineValueSelectedItem
        End Get
    End Property

    ''' <summary>
    ''' Link Global Data
    ''' </summary>
    ''' <param name="OutLink_LineValueDB"></param>
    ''' <remarks></remarks>
    Public Sub SetupDataLink(OutLink_LineValueDB As Dictionary(Of String, CLineTypeParameter))
        mOutLink_LineValueDB = OutLink_LineValueDB
    End Sub

    Public ReadOnly Property NowData As CLineTypeParameter
        Get
            Return mNowShowData
        End Get
        'Set(value As CLineTypeParameter)
        '    _NowShowData = value
        'End Set
    End Property

    ''' <summary>
    ''' Min : only Data show
    ''' Total : have IO Save/Update/Del/Select 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Type As ShowType

        Set(value As ShowType)
            mShowType = value
            Select Case mShowType

                Case ShowType.Min
                    TableLayoutPanel1.ColumnStyles(0).Width = 0
                    TableLayoutPanel1.ColumnStyles(1).Width = 0
                    TableLayoutPanel1.RowStyles(3).Height = 0
                Case ShowType.Total
                    TableLayoutPanel1.ColumnStyles(0).Width = sizeCol0Width
                    TableLayoutPanel1.ColumnStyles(1).Width = sizeCol1Width
                    TableLayoutPanel1.RowStyles(3).Height = 30
                Case Else
                    TableLayoutPanel1.ColumnStyles(0).Width = sizeCol0Width
                    TableLayoutPanel1.ColumnStyles(1).Width = sizeCol1Width
                    TableLayoutPanel1.RowStyles(3).Height = 30

            End Select


            Me.Refresh()
        End Set
        Get
            Return mShowType
        End Get

    End Property

    Public Sub LineValveDB_Update(Optional selectName As String = "")
        '20170929 Toby_ Add 判斷
        If (Not IsNothing(Parent)) Then
            Me.BeginInvoke(Sub()
                               lstLineDB.Items.Clear()
                               For i As Integer = 0 To mOutLink_LineValueDB.Count - 1
                                   lstLineDB.Items.Add(mOutLink_LineValueDB.Keys(i))
                               Next
                               If (selectName <> "") Then
                                   For Each item As Object In lstLineDB.Items
                                       If (item.ToString = selectName) Then
                                           lstLineDB.SelectedItem = item
                                           Exit For
                                       End If
                                   Next
                               End If
                           End Sub)
        End If
    End Sub

#End Region
    Public RecipeEdit As CRecipe
#Region "UserControl Event"

    Private Sub fnAddLineValueEvent()

        AddHandler textLinePreMoveDelayFactor.TextChanged, AddressOf textLineValue_TextChanged
        AddHandler textLinePreDownSpeed.TextChanged, AddressOf textLineValue_TextChanged
        AddHandler textLinePreDownAcc.TextChanged, AddressOf textLineValue_TextChanged

        AddHandler textLinePreDispenseGap.TextChanged, AddressOf textLineValue_TextChanged
        AddHandler textLineDuringSpeed.TextChanged, AddressOf textLineValue_TextChanged
        AddHandler textLineDuringShutOffDistance.TextChanged, AddressOf textLineValue_TextChanged
        AddHandler textLinePostSuckBack.TextChanged, AddressOf textLineValue_TextChanged
        AddHandler textLinePostDwell.TextChanged, AddressOf textLineValue_TextChanged

        AddHandler textLinePostBacktrackGap.TextChanged, AddressOf textLineValue_TextChanged
        AddHandler textLinePostBacktrackLength.TextChanged, AddressOf textLineValue_TextChanged
        AddHandler textLinePostBacktrackSpeed.TextChanged, AddressOf textLineValue_TextChanged
        AddHandler textLinePostRetractDistance.TextChanged, AddressOf textLineValue_TextChanged
        AddHandler textLinePostRetractSpeed.TextChanged, AddressOf textLineValue_TextChanged
        AddHandler textLinePostRetractAcc.TextChanged, AddressOf textLineValue_TextChanged

    End Sub

    Private Sub btnLineDBAdd_Click(sender As Object, e As EventArgs) Handles btnLineDBAdd.Click
        Dim newLineName As String

        newLineName = txtLineDB.Text.Trim
        '沒有輸入任何資料
        If newLineName = "" Then
            txtLineDB.BackColor = Color.Yellow
            txtLineDB.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            txtLineDB.BackColor = System.Drawing.SystemColors.Window
            txtLineDB.Refresh() 'Soni / 2017.05.10
            Exit Sub
        End If
        '非正確的命名規則
        If IsillegalFileName(newLineName) = True Then
            '檔案名稱錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000058))
            MsgBox(gMsgHandler.GetMessage(Warn_3000058), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        '名稱已經存在

        If mOutLink_LineValueDB.ContainsKey(newLineName) Then
            If MsgBox("Line Valve Data already Exists, Overwrite?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbNo Then
                Exit Sub
            End If
        End If

        Dim LineParaBuffer As New CLineTypeParameter(newLineName)

        If (GetLineValveDB(LineParaBuffer) = True) Then
            LineParaBuffer.Save(fnGetLineValueDbFileName(newLineName))
            If (mOutLink_LineValueDB.ContainsKey(newLineName)) Then
                mOutLink_LineValueDB(newLineName) = LineParaBuffer
            Else
                mOutLink_LineValueDB.Add(newLineName, LineParaBuffer)
            End If
        Else
            '資錯格式錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000059))
            MsgBox(gMsgHandler.GetMessage(Warn_3000059), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        mlstLineValueSelectedItem = newLineName
        Call LineValveDB_Update(newLineName)

    End Sub

    Private Sub btnLineDBUpdate_Click(sender As Object, e As EventArgs) Handles btnLineDBUpdate.Click

        Dim selectLineName As String
        Dim IsfindRecipe As Boolean

        If (txtLineDB.Text <> "") Then
            For Each item As Object In lstLineDB.Items
                If (item.ToString = txtLineDB.Text) Then
                    IsfindRecipe = True
                    lstLineDB.SelectedItem = item
                    Exit For
                End If
            Next
        End If

        If (IsfindRecipe = False) Then
            lstLineDB.BackColor = Color.Yellow
            lstLineDB.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstLineDB.BackColor = Color.White
            lstLineDB.Refresh() 'Soni / 2017.05.10
            Exit Sub
        End If


        selectLineName = lstLineDB.SelectedItem.ToString
        mlstLineValueSelectedItem = selectLineName
        If mOutLink_LineValueDB.ContainsKey(selectLineName) = True Then

            Dim LineParaBuffer As New CLineTypeParameter(selectLineName)
            If (GetLineValveDB(LineParaBuffer) = True) Then
                LineParaBuffer.Save(fnGetLineValueDbFileName(selectLineName))
                mOutLink_LineValueDB(selectLineName) = LineParaBuffer
            Else
                '資錯格式錯誤
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000059))
                MsgBox(gMsgHandler.GetMessage(Warn_3000059), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If

        End If
    End Sub

    Private Sub btnLineDBDel_Click(sender As Object, e As EventArgs) Handles btnLineDBDel.Click
        Dim selectLineName As String
        Dim filename As String

        '沒有選取
        If lstLineDB.SelectedIndex < 0 Then
            lstLineDB.BackColor = Color.Yellow
            lstLineDB.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstLineDB.BackColor = Color.White
            lstLineDB.Refresh() 'Soni / 2017.05.10
            Exit Sub
        End If

        'jimmy 20170713
        selectLineName = lstLineDB.SelectedItem.ToString
        Dim mPatternName As String = ""
        filename = fnGetLineValueDbFileName(selectLineName)
        For mPatterNo = 0 To RecipeEdit.PatternCount - 1
            mPatternName = RecipeEdit.Pattern.Keys(mPatterNo)
            If RecipeEdit.Pattern(mPatternName).RoundCount > 0 Then
                For mRoundNo = 0 To RecipeEdit.Pattern(mPatternName).RoundCount - 1
                    For mStepNo = 0 To RecipeEdit.Pattern(mPatternName).Round(mRoundNo).StepCount - 1
                        If RecipeEdit.Pattern(mPatternName).Round(mRoundNo).CStep(mStepNo).Line3D.LineParameterName = selectLineName Then

                            MessageBox.Show("Operation could not be completed")
                            Exit Sub
                        End If

                    Next
                Next
            End If
        Next

        If MsgBox("Are You Sure to Delete?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) <> MsgBoxResult.Yes Then 'Soni + 2017.04.26 刪除前確認
            Exit Sub
        End If

        Try
            If System.IO.File.Exists(filename) Then
                System.IO.File.Delete(filename)
            End If
        Catch ex As Exception
            MessageBox.Show("Exception Error :" & ex.ToString)
        Finally
        End Try

        If mOutLink_LineValueDB.ContainsKey(lstLineDB.SelectedItem) = True Then
            mOutLink_LineValueDB.Remove(lstLineDB.SelectedItem)
            Call LineValveDB_Update()
            Call ShowLineValue(New CLineTypeParameter(""))
            mlstLineValueSelectedItem = ""
        End If

        If Not mOutLink_LineValueDB.ContainsKey("Default") Then 'Soni + 2017.04.26 預設檔重建
            mOutLink_LineValueDB.Add("Default", New CLineTypeParameter("Default"))
            mOutLink_LineValueDB("Default").Save(fnGetLineValueDbFileName("Default"))
            Call LineValveDB_Update()
        End If
    End Sub

    Private Sub lstLineDB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstLineDB.SelectedIndexChanged
        If lstLineDB.SelectedIndex < 0 Then
            lstLineDB.BackColor = Color.Yellow
            lstLineDB.Refresh() 'Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstLineDB.BackColor = Color.White
            lstLineDB.Refresh() 'Soni / 2017.05.10
            Exit Sub
        End If
        txtLineDB.Text = lstLineDB.SelectedItem
        mlstLineValueSelectedItem = lstLineDB.SelectedItem
        Dim LineParaBuffer As New CLineTypeParameter(txtLineDB.Text)
        LineParaBuffer.Load(fnGetLineValueDbFileName(txtLineDB.Text))
        Call ShowLineValue(LineParaBuffer)
    End Sub

    Private Sub textLineValue_TextChanged(sender As Object, e As EventArgs)

        If mlstLineValueSelectedItem = "" Then
            Exit Sub
        End If

        If mOutLink_LineValueDB.ContainsKey(mlstLineValueSelectedItem) = True Then
            Dim ChangeColor As Color = Color.Red
            With mOutLink_LineValueDB(mlstLineValueSelectedItem)

                If .PreMoveDelayFactor = Val(textLinePreMoveDelayFactor.Text) Then
                    textLinePreMoveDelayFactor.BackColor = System.Drawing.SystemColors.Window
                Else
                    textLinePreMoveDelayFactor.BackColor = ChangeColor
                End If

                If .PreDownSpeed = Val(textLinePreDownSpeed.Text) Then
                    textLinePreDownSpeed.BackColor = System.Drawing.SystemColors.Window
                Else
                    textLinePreDownSpeed.BackColor = ChangeColor
                End If

                If .PreDownAcc = Val(textLinePreDownAcc.Text) Then
                    textLinePreDownAcc.BackColor = System.Drawing.SystemColors.Window
                Else
                    textLinePreDownAcc.BackColor = ChangeColor
                End If

                If .PreDispenseGap = Val(textLinePreDispenseGap.Text) Then
                    textLinePreDispenseGap.BackColor = System.Drawing.SystemColors.Window
                Else
                    textLinePreDispenseGap.BackColor = ChangeColor
                End If

                If .DuringDispenseSpeed = Val(textLineDuringSpeed.Text) Then
                    textLineDuringSpeed.BackColor = System.Drawing.SystemColors.Window
                Else
                    textLineDuringSpeed.BackColor = ChangeColor
                End If

                If .DuringShutOffDistance = Val(textLineDuringShutOffDistance.Text) Then
                    textLineDuringShutOffDistance.BackColor = System.Drawing.SystemColors.Window
                Else
                    textLineDuringShutOffDistance.BackColor = ChangeColor
                End If

                If .PostSuckBack = Val(textLinePostSuckBack.Text) Then
                    textLinePostSuckBack.BackColor = System.Drawing.SystemColors.Window
                Else
                    textLinePostSuckBack.BackColor = ChangeColor
                End If

                If .PostDwellTime = Val(textLinePostDwell.Text) Then
                    textLinePostDwell.BackColor = System.Drawing.SystemColors.Window
                Else
                    textLinePostDwell.BackColor = ChangeColor
                End If

                If .PostBacktrackGap = Val(textLinePostBacktrackGap.Text) Then
                    textLinePostBacktrackGap.BackColor = System.Drawing.SystemColors.Window
                Else
                    textLinePostBacktrackGap.BackColor = ChangeColor
                End If

                If .PostBacktrackLength = Val(textLinePostBacktrackLength.Text) Then
                    textLinePostBacktrackLength.BackColor = System.Drawing.SystemColors.Window
                Else
                    textLinePostBacktrackLength.BackColor = ChangeColor
                End If

                If .PostBacktrackSpeed = Val(textLinePostBacktrackSpeed.Text) Then
                    textLinePostBacktrackSpeed.BackColor = System.Drawing.SystemColors.Window
                Else
                    textLinePostBacktrackSpeed.BackColor = ChangeColor
                End If

                If .PostRetractDistance = Val(textLinePostRetractDistance.Text) Then
                    textLinePostRetractDistance.BackColor = System.Drawing.SystemColors.Window
                Else
                    textLinePostRetractDistance.BackColor = ChangeColor
                End If

                If .PostRetractSpeed = Val(textLinePostRetractSpeed.Text) Then
                    textLinePostRetractSpeed.BackColor = System.Drawing.SystemColors.Window
                Else
                    textLinePostRetractSpeed.BackColor = ChangeColor
                End If
                If .PostRetractAcc = Val(textLinePostRetractAcc.Text) Then
                    textLinePostRetractAcc.BackColor = System.Drawing.SystemColors.Window
                Else
                    textLinePostRetractAcc.BackColor = ChangeColor
                End If
            End With
        End If
    End Sub

    Private Sub ucLineTypeParameter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fnAddLineValueEvent()
        fnSetupToolTip()
        If (Not SelectItemName = "") Then
            Dim LineParaBuffer As New CLineTypeParameter(mlstLineValueSelectedItem)
            LineParaBuffer.Load(fnGetLineValueDbFileName(mlstLineValueSelectedItem))
            ShowLineValue(LineParaBuffer)
            mNowShowData = LineParaBuffer
        End If
    End Sub



    Private Sub btSetDefaultValue_Click(sender As Object, e As EventArgs) Handles btSetDefaultValue.Click
        textLinePreMoveDelayFactor.Text = "0"
        textLinePreDownSpeed.Text = "100"
        textLinePreDownAcc.Text = "2940"
        textLinePreDispenseGap.Text = "2"
        textLineDuringSpeed.Text = "10"
        textLineDuringShutOffDistance.Text = "0"
        textLinePostSuckBack.Text = "0"
        textLinePostDwell.Text = "0"
        textLinePostBacktrackGap.Text = "0"
        textLinePostBacktrackLength.Text = "0"
        textLinePostBacktrackSpeed.Text = "100"
        textLinePostRetractDistance.Text = "0"
        textLinePostRetractSpeed.Text = "100"
        textLinePostRetractAcc.Text = "2940"
    End Sub
#End Region
End Class
