'Eason 20170216 Ticket:100080 , Add Arc Type Parameter
Imports ProjectRecipe
Imports ProjectCore

''' <summary>
''' Arc Parameter Class
''' </summary>
''' <remarks></remarks>
Public Class ucArcTypeParameter

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

    Private FilePath As String = Application.StartupPath & "\Database\ArcValue\"

#End Region

#Region "Private Var"

    Private mlstArcValueSelectedItem As String = ""

    Private mOutLink_ArcValueDB As New Dictionary(Of String, CArcTypeParameter)

    Private mNowShowData As CArcTypeParameter = Nothing

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

    Private Function fnGetArcValueDbFileName(sName As String) As String
        Return Application.StartupPath & "\Database\ArcValue\" & sName & ".ldb"
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

        TipType.SetToolTip(textArcPreMoveDelayFactor, "Value : >= 0 ")
        TipType.SetToolTip(textArcPreDownSpeed, "Value : > 10 ")
        TipType.SetToolTip(textArcPreDownAcc, "Value : > 0 ")
        TipType.SetToolTip(textArcPreDispenseGap, "Value : + , - , 0 ")

        TipType.SetToolTip(textArcDuringSpeed, "Value : > 0 ")
        TipType.SetToolTip(textArcDuringShutOffDistance, "Value : > 0 ")

        TipType.SetToolTip(textArcPostSuckBack, "Value : >= 0 ")
        TipType.SetToolTip(textArcPostDwell, "Value : >= 0 ")
        TipType.SetToolTip(textArcPostBacktrackGap, "Value : >= 0 ")
        TipType.SetToolTip(textArcPostBacktrackLength, "Value : >= 0 ")
        TipType.SetToolTip(textArcPostBacktrackSpeed, "Value : > 0 ")
        TipType.SetToolTip(textArcPostRetractDistance, "Value : >= 0 ")
        TipType.SetToolTip(textArcPostRetractSpeed, "Value : >10 ")
        TipType.SetToolTip(textArcPostRetractAcc, "Value : > 0 ")

    End Sub
    Private Function fnCheckArcTextboxData() As Boolean

        Dim DataCheck As Boolean = True

        'Pre- Dispense -------------------------------------------------------------------
        'PreMoveDelayFactor : >= 0 textArcPreMoveDelayFactor
        If (IsNumeric(textArcPreMoveDelayFactor.Text) AndAlso Val(textArcPreMoveDelayFactor.Text) >= 0) Then
            fnBeginInvokeTextBackColor(textArcPreMoveDelayFactor, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textArcPreMoveDelayFactor, Color.Yellow)
            DataCheck = False
        End If
        'PreDownSpeed : > 10 textArcPreDownSpeed
        If (IsNumeric(textArcPreDownSpeed.Text) AndAlso Val(textArcPreDownSpeed.Text) > 10) Then
            fnBeginInvokeTextBackColor(textArcPreDownSpeed, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textArcPreDownSpeed, Color.Yellow)
            DataCheck = False
        End If
        'PreDownAcc : > 0 textArcPreDownAcc
        If (IsNumeric(textArcPreDownAcc.Text) AndAlso Val(textArcPreDownAcc.Text) > 0) Then
            fnBeginInvokeTextBackColor(textArcPreDownAcc, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textArcPreDownAcc, Color.Yellow)
            DataCheck = False
        End If
        'PreDispenseGap : +-  textArcPreDispenseGap
        If (IsNumeric(textArcPreDispenseGap.Text)) Then
            fnBeginInvokeTextBackColor(textArcPreDispenseGap, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textArcPreDispenseGap, Color.Yellow)
            DataCheck = False
        End If
        'During- Dispense -------------------------------------------------------------------

        'Speed : > 0 textArcDuringSpeed
        If (IsNumeric(textArcDuringSpeed.Text) AndAlso Val(textArcDuringSpeed.Text) > 0) Then
            fnBeginInvokeTextBackColor(textArcDuringSpeed, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textArcDuringSpeed, Color.Yellow)
            DataCheck = False
        End If
        'ShutOffDistance : >= 0 textArcDuringShutOffDistance
        If (IsNumeric(textArcDuringShutOffDistance.Text) AndAlso Val(textArcDuringShutOffDistance.Text) >= 0) Then
            fnBeginInvokeTextBackColor(textArcDuringShutOffDistance, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textArcDuringShutOffDistance, Color.Yellow)
            DataCheck = False
        End If
        'Post- Dispense -------------------------------------------------------------------

        'PostSuckBack >= 0 textArcPostSuckBack
        If (IsNumeric(textArcPostSuckBack.Text) AndAlso Val(textArcPostSuckBack.Text) >= 0) Then
            fnBeginInvokeTextBackColor(textArcPostSuckBack, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textArcPostSuckBack, Color.Yellow)
            DataCheck = False
        End If
        'PostDwell >= 0 textArcPostDwell
        If (IsNumeric(textArcPostDwell.Text) AndAlso Val(textArcPostDwell.Text) >= 0) Then
            fnBeginInvokeTextBackColor(textArcPostDwell, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textArcPostDwell, Color.Yellow)
            DataCheck = False
        End If
        'PostBacktrackGap >= 0 textArcPostBacktrackGap
        If (IsNumeric(textArcPostBacktrackGap.Text) AndAlso Val(textArcPostBacktrackGap.Text) >= 0) Then
            fnBeginInvokeTextBackColor(textArcPostBacktrackGap, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textArcPostBacktrackGap, Color.Yellow)
            DataCheck = False
        End If
        'PostBacktrackLength >= 0 textArcPostBacktrackLength
        If (IsNumeric(textArcPostBacktrackLength.Text) AndAlso Val(textArcPostBacktrackLength.Text) >= 0) Then
            fnBeginInvokeTextBackColor(textArcPostBacktrackLength, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textArcPostBacktrackLength, Color.Yellow)
            DataCheck = False
        End If
        'PostBacktrackSpeed > 0 textArcPostBacktrackSpeed
        If (IsNumeric(textArcPostBacktrackSpeed.Text) AndAlso Val(textArcPostBacktrackSpeed.Text) > 0) Then
            fnBeginInvokeTextBackColor(textArcPostBacktrackSpeed, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textArcPostBacktrackSpeed, Color.Yellow)
            DataCheck = False
        End If
        'PostRetractDistance >= 0 textArcPostRetractDistance
        If (IsNumeric(textArcPostRetractDistance.Text) AndAlso Val(textArcPostRetractDistance.Text) >= 0) Then
            fnBeginInvokeTextBackColor(textArcPostRetractDistance, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textArcPostRetractDistance, Color.Yellow)
            DataCheck = False
        End If
        'PostRetractSpeed > 10 textArcPostRetractSpeed
        If (IsNumeric(textArcPostRetractSpeed.Text) AndAlso Val(textArcPostRetractSpeed.Text) > 10) Then
            fnBeginInvokeTextBackColor(textArcPostRetractSpeed, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textArcPostRetractSpeed, Color.Yellow)
            DataCheck = False
        End If
        'PostRetractAcc > 0 textArcPostRetractAcc
        If (IsNumeric(textArcPostRetractAcc.Text) AndAlso Val(textArcPostRetractAcc.Text) > 0) Then
            fnBeginInvokeTextBackColor(textArcPostRetractAcc, System.Drawing.SystemColors.Window)
        Else
            fnBeginInvokeTextBackColor(textArcPostRetractAcc, Color.Yellow)
            DataCheck = False
        End If

        '==================================================================================
        ' Below old
        ''Pre- Dispense -------------------------------------------------------------------
        ''PreMoveDelayFactor : Can set 0
        'If (Not IsNumeric(textArcPreMoveDelayFactor.Text) Or Val(textArcPreMoveDelayFactor.Text) < 0) Then
        '    fnBeginInvokeTextBackColor(textArcPreMoveDelayFactor, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textArcPreMoveDelayFactor, System.Drawing.SystemColors.Window)
        'End If
        'If (Not IsNumeric(textArcPreDownSpeed.Text) Or Val(textArcPreDownSpeed.Text) <= 0) Then
        '    fnBeginInvokeTextBackColor(textArcPreDownSpeed, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textArcPreDownSpeed, System.Drawing.SystemColors.Window)
        'End If

        'If (Not IsNumeric(textArcPreDownAcc.Text) Or Val(textArcPreDownAcc.Text) <= 0) Then
        '    fnBeginInvokeTextBackColor(textArcPreDownAcc, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textArcPreDownAcc, System.Drawing.SystemColors.Window)
        'End If
        'If (Not IsNumeric(textArcPreDispenseGap.Text) Or Val(textArcPreDispenseGap.Text) < 0) Then
        '    fnBeginInvokeTextBackColor(textArcPreDispenseGap, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textArcPreDispenseGap, System.Drawing.SystemColors.Window)
        'End If
        ''During- Dispense -------------------------------------------------------------------
        'If (Not IsNumeric(textArcDuringSpeed.Text) Or Val(textArcDuringSpeed.Text) <= 0) Then
        '    fnBeginInvokeTextBackColor(textArcDuringSpeed, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textArcDuringSpeed, System.Drawing.SystemColors.Window)
        'End If
        'If (Not IsNumeric(textArcDuringShutOffDistance.Text) Or Val(textArcDuringShutOffDistance.Text) < 0) Then
        '    fnBeginInvokeTextBackColor(textArcDuringShutOffDistance, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textArcDuringShutOffDistance, System.Drawing.SystemColors.Window)
        'End If
        ''Post- Dispense -------------------------------------------------------------------
        'If (Not IsNumeric(textArcPostSuckBack.Text) Or Val(textArcPostSuckBack.Text) < 0) Then
        '    fnBeginInvokeTextBackColor(textArcPostSuckBack, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textArcPostSuckBack, System.Drawing.SystemColors.Window)
        'End If
        'If (Not IsNumeric(textArcPostDwell.Text) Or Val(textArcPostDwell.Text) < 0) Then
        '    fnBeginInvokeTextBackColor(textArcPostDwell, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textArcPostDwell, System.Drawing.SystemColors.Window)
        'End If
        'If (Not IsNumeric(textArcPostBacktrackGap.Text) Or Val(textArcPostBacktrackGap.Text) < 0) Then
        '    fnBeginInvokeTextBackColor(textArcPostBacktrackGap, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textArcPostBacktrackGap, System.Drawing.SystemColors.Window)
        'End If
        'If (Not IsNumeric(textArcPostBacktrackLength.Text) Or Val(textArcPostBacktrackLength.Text) < 0) Then
        '    fnBeginInvokeTextBackColor(textArcPostBacktrackLength, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textArcPostBacktrackLength, System.Drawing.SystemColors.Window)
        'End If
        'If (Not IsNumeric(textArcPostBacktrackSpeed.Text) Or Val(textArcPostBacktrackSpeed.Text) <= 0) Then
        '    fnBeginInvokeTextBackColor(textArcPostBacktrackSpeed, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textArcPostBacktrackSpeed, System.Drawing.SystemColors.Window)
        'End If
        'If (Not IsNumeric(textArcPostRetractDistance.Text) Or Val(textArcPostRetractDistance.Text) < 0) Then
        '    fnBeginInvokeTextBackColor(textArcPostRetractDistance, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textArcPostRetractDistance, System.Drawing.SystemColors.Window)
        'End If
        'If (Not IsNumeric(textArcPostRetractSpeed.Text) Or Val(textArcPostRetractSpeed.Text) <= 0) Then
        '    fnBeginInvokeTextBackColor(textArcPostRetractSpeed, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textArcPostRetractSpeed, System.Drawing.SystemColors.Window)
        'End If
        'If (Not IsNumeric(textArcPostRetractAcc.Text) Or Val(textArcPostRetractAcc.Text) <= 0) Then
        '    fnBeginInvokeTextBackColor(textArcPostRetractAcc, Color.Yellow)
        '    DataCheck = False
        'Else
        '    fnBeginInvokeTextBackColor(textArcPostRetractAcc, System.Drawing.SystemColors.Window)
        'End If
        '-----------------------------------------------------------------------------------------------
        Return DataCheck

    End Function

    Private Function GetArcValveDB(ByRef ArcValveParam As CArcTypeParameter) As Boolean

        If (fnCheckArcTextboxData() = True) Then

            ArcValveParam.Name = txtArcDB.Text

            ArcValveParam.PreMoveDelayFactor = Val(textArcPreMoveDelayFactor.Text)
            ArcValveParam.PreDownSpeed = Val(textArcPreDownSpeed.Text)
            ArcValveParam.PreDownAcc = Val(textArcPreDownAcc.Text)
            ArcValveParam.PreDispenseGap = Val(textArcPreDispenseGap.Text)

            ArcValveParam.DuringDispenseSpeed = Val(textArcDuringSpeed.Text)
            ArcValveParam.DuringShutOffDistance = Val(textArcDuringShutOffDistance.Text)

            ArcValveParam.PostSuckBack = Val(textArcPostSuckBack.Text)
            ArcValveParam.PostDwellTime = Val(textArcPostDwell.Text)
            ArcValveParam.PostBacktrackGap = Val(textArcPostBacktrackGap.Text)
            ArcValveParam.PostBacktrackLength = Val(textArcPostBacktrackLength.Text)
            ArcValveParam.PostBacktrackSpeed = Val(textArcPostBacktrackSpeed.Text)
            ArcValveParam.PostRetractDistance = Val(textArcPostRetractDistance.Text)
            ArcValveParam.PostRetractSpeed = Val(textArcPostRetractSpeed.Text)
            ArcValveParam.PostRetractAcc = Val(textArcPostRetractAcc.Text)
        Else
            Return False
        End If

        Return True

    End Function

    Private Sub ShowArcValue(ByVal ArcValveParam As CArcTypeParameter)
        '20170929 Toby_ Add 判斷
        If (Not IsNothing(Parent)) Then
            Me.BeginInvoke(Sub()
                               txtArcDB.Text = ArcValveParam.Name

                               textArcPreMoveDelayFactor.Text = ArcValveParam.PreMoveDelayFactor
                               textArcPreDownSpeed.Text = ArcValveParam.PreDownSpeed
                               textArcPreDownAcc.Text = ArcValveParam.PreDownAcc

                               textArcPreDispenseGap.Text = ArcValveParam.PreDispenseGap
                               textArcDuringSpeed.Text = ArcValveParam.DuringDispenseSpeed
                               textArcDuringShutOffDistance.Text = ArcValveParam.DuringShutOffDistance
                               textArcPostSuckBack.Text = ArcValveParam.PostSuckBack
                               textArcPostDwell.Text = ArcValveParam.PostDwellTime

                               textArcPostBacktrackGap.Text = ArcValveParam.PostBacktrackGap
                               textArcPostBacktrackLength.Text = ArcValveParam.PostBacktrackLength
                               textArcPostBacktrackSpeed.Text = ArcValveParam.PostBacktrackSpeed
                               textArcPostRetractDistance.Text = ArcValveParam.PostRetractDistance
                               textArcPostRetractSpeed.Text = ArcValveParam.PostRetractSpeed
                               textArcPostRetractAcc.Text = ArcValveParam.PostRetractAcc
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
            mlstArcValueSelectedItem = value

        End Set
        Get
            Return mlstArcValueSelectedItem
        End Get
    End Property

    ''' <summary>
    ''' Link Global Data
    ''' </summary>
    ''' <param name="OutLink_ArcValueDB"></param>
    ''' <remarks></remarks>
    Public Sub SetupDataLink(OutLink_ArcValueDB As Dictionary(Of String, CArcTypeParameter))
        mOutLink_ArcValueDB = OutLink_ArcValueDB
    End Sub

    Public ReadOnly Property NowData As CArcTypeParameter
        Get
            Return mNowShowData
        End Get
        'Set(value As CArcTypeParameter)
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

    Public Sub ArcValveDB_Update(Optional selectName As String = "")
        '20170929 Toby_ Add 判斷
        If (Not IsNothing(Parent)) Then
            Me.BeginInvoke(Sub()
                               lstArcDB.Items.Clear()
                               For i As Integer = 0 To mOutLink_ArcValueDB.Count - 1
                                   lstArcDB.Items.Add(mOutLink_ArcValueDB.Keys(i))
                               Next
                               If (selectName <> "") Then
                                   For Each item As Object In lstArcDB.Items
                                       If (item.ToString = selectName) Then
                                           lstArcDB.SelectedItem = item
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

    Private Sub fnAddArcValueEvent()

        AddHandler textArcPreMoveDelayFactor.TextChanged, AddressOf textArcValue_TextChanged
        AddHandler textArcPreDownSpeed.TextChanged, AddressOf textArcValue_TextChanged
        AddHandler textArcPreDownAcc.TextChanged, AddressOf textArcValue_TextChanged

        AddHandler textArcPreDispenseGap.TextChanged, AddressOf textArcValue_TextChanged
        AddHandler textArcDuringSpeed.TextChanged, AddressOf textArcValue_TextChanged
        AddHandler textArcDuringShutOffDistance.TextChanged, AddressOf textArcValue_TextChanged
        AddHandler textArcPostSuckBack.TextChanged, AddressOf textArcValue_TextChanged
        AddHandler textArcPostDwell.TextChanged, AddressOf textArcValue_TextChanged

        AddHandler textArcPostBacktrackGap.TextChanged, AddressOf textArcValue_TextChanged
        AddHandler textArcPostBacktrackLength.TextChanged, AddressOf textArcValue_TextChanged
        AddHandler textArcPostBacktrackSpeed.TextChanged, AddressOf textArcValue_TextChanged
        AddHandler textArcPostRetractDistance.TextChanged, AddressOf textArcValue_TextChanged
        AddHandler textArcPostRetractSpeed.TextChanged, AddressOf textArcValue_TextChanged
        AddHandler textArcPostRetractAcc.TextChanged, AddressOf textArcValue_TextChanged

    End Sub

    Private Sub btnArcDBAdd_Click(sender As Object, e As EventArgs) Handles btnArcDBAdd.Click
        Dim newArcName As String

        newArcName = txtArcDB.Text.Trim
        '沒有輸入任何資料
        If newArcName = "" Then
            txtArcDB.BackColor = Color.Yellow
            txtArcDB.Refresh() ''Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            txtArcDB.BackColor = System.Drawing.SystemColors.Window
            Exit Sub
        End If
        '非正確的命名規則
        If IsillegalFileName(newArcName) = True Then
            '檔案名稱錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000058))
            MsgBox(gMsgHandler.GetMessage(Warn_3000058), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        '名稱已經存在

        If mOutLink_ArcValueDB.ContainsKey(newArcName) Then
            If MsgBox("Arc Valve Data already Exists, Overwrite?", MsgBoxStyle.YesNo + MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground) = vbNo Then
                Exit Sub
            End If
        End If

        Dim ArcParaBuffer As New CArcTypeParameter(newArcName)

        If (GetArcValveDB(ArcParaBuffer) = True) Then
            ArcParaBuffer.Save(fnGetArcValueDbFileName(newArcName))
            If (mOutLink_ArcValueDB.ContainsKey(newArcName)) Then
                mOutLink_ArcValueDB(newArcName) = ArcParaBuffer
            Else
                mOutLink_ArcValueDB.Add(newArcName, ArcParaBuffer)
            End If
        Else
            '資錯格式錯誤
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000059))
            MsgBox(gMsgHandler.GetMessage(Warn_3000059), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        mlstArcValueSelectedItem = newArcName
        Call ArcValveDB_Update(newArcName)

    End Sub

    Private Sub btnArcDBUpdate_Click(sender As Object, e As EventArgs) Handles btnArcDBUpdate.Click

        Dim selectArcName As String
        Dim IsfindRecipe As Boolean

        If (txtArcDB.Text <> "") Then
            For Each item As Object In lstArcDB.Items
                If (item.ToString = txtArcDB.Text) Then
                    IsfindRecipe = True
                    lstArcDB.SelectedItem = item
                    Exit For
                End If
            Next
        End If

        If (IsfindRecipe = False) Then
            lstArcDB.BackColor = Color.Yellow
            lstArcDB.Refresh() ''Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstArcDB.BackColor = Color.White
            lstArcDB.Refresh() ''Soni / 2017.05.10
            Exit Sub
        End If

        selectArcName = lstArcDB.SelectedItem.ToString
        mlstArcValueSelectedItem = selectArcName
        If mOutLink_ArcValueDB.ContainsKey(selectArcName) = True Then

            Dim ArcParaBuffer As New CArcTypeParameter(selectArcName)
            If (GetArcValveDB(ArcParaBuffer) = True) Then
                ArcParaBuffer.Save(fnGetArcValueDbFileName(selectArcName))
                mOutLink_ArcValueDB(selectArcName) = ArcParaBuffer
            Else
                '資錯格式錯誤
                gSyslog.Save(gMsgHandler.GetMessage(Warn_3000059))
                MsgBox(gMsgHandler.GetMessage(Warn_3000059), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End If

        End If
    End Sub

    Private Sub btnArcDBDel_Click(sender As Object, e As EventArgs) Handles btnArcDBDel.Click
        Dim selectArcName As String
        Dim filename As String

        '沒有選取
        If lstArcDB.SelectedIndex < 0 Then
            lstArcDB.BackColor = Color.Yellow
            lstArcDB.Refresh() ''Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstArcDB.BackColor = Color.White
            lstArcDB.Refresh() ''Soni / 2017.05.10
            Exit Sub
        End If
        'jimmy 20170713
        selectArcName = lstArcDB.SelectedItem.ToString
        Dim mPatternName As String = ""
        filename = fnGetArcValueDbFileName(selectArcName)
        For mPatterNo = 0 To RecipeEdit.PatternCount - 1
            mPatternName = RecipeEdit.Pattern.Keys(mPatterNo)
            If RecipeEdit.Pattern(mPatternName).RoundCount > 0 Then
                For mRoundNo = 0 To RecipeEdit.Pattern(mPatternName).RoundCount - 1
                    For mStepNo = 0 To RecipeEdit.Pattern(mPatternName).Round(mRoundNo).StepCount - 1
                        If RecipeEdit.Pattern(mPatternName).Round(mRoundNo).CStep(mStepNo).Arc2D.ArcParameterName = selectArcName Or
                            RecipeEdit.Pattern(mPatternName).Round(mRoundNo).CStep(mStepNo).Arc3D.ArcParameterName = selectArcName Or
                            RecipeEdit.Pattern(mPatternName).Round(mRoundNo).CStep(mStepNo).Circle2D.ArcParameterName = selectArcName Or
                            RecipeEdit.Pattern(mPatternName).Round(mRoundNo).CStep(mStepNo).Circle3D.ArcParameterName = selectArcName Then
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

        If mOutLink_ArcValueDB.ContainsKey(lstArcDB.SelectedItem) = True Then
            mOutLink_ArcValueDB.Remove(lstArcDB.SelectedItem)
            Call ArcValveDB_Update()
            Call ShowArcValue(New CArcTypeParameter(""))
            mlstArcValueSelectedItem = ""
        End If
        If Not mOutLink_ArcValueDB.ContainsKey("Default") Then 'Soni + 2017.04.26 預設檔重建
            mOutLink_ArcValueDB.Add("Default", New CArcTypeParameter("Default"))
            mOutLink_ArcValueDB("Default").Save(fnGetArcValueDbFileName("Default"))
            Call ArcValveDB_Update()
        End If
    End Sub

    Private Sub lstArcDB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstArcDB.SelectedIndexChanged
        If lstArcDB.SelectedIndex < 0 Then
            lstArcDB.BackColor = Color.Yellow
            lstArcDB.Refresh() ''Soni / 2017.05.10
            System.Threading.Thread.Sleep(300)
            lstArcDB.BackColor = Color.White
            lstArcDB.Refresh() ''Soni / 2017.05.10
            Exit Sub
        End If
        txtArcDB.Text = lstArcDB.SelectedItem
        mlstArcValueSelectedItem = lstArcDB.SelectedItem
        Dim ArcParaBuffer As New CArcTypeParameter(txtArcDB.Text)
        ArcParaBuffer.Load(fnGetArcValueDbFileName(txtArcDB.Text))
        Call ShowArcValue(ArcParaBuffer)
    End Sub

    Private Sub textArcValue_TextChanged(sender As Object, e As EventArgs)

        If mlstArcValueSelectedItem = "" Then
            Exit Sub
        End If

        If mOutLink_ArcValueDB.ContainsKey(mlstArcValueSelectedItem) = True Then
            Dim ChangeColor As Color = Color.Red
            With mOutLink_ArcValueDB(mlstArcValueSelectedItem)

                If .PreMoveDelayFactor = Val(textArcPreMoveDelayFactor.Text) Then
                    textArcPreMoveDelayFactor.BackColor = System.Drawing.SystemColors.Window
                Else
                    textArcPreMoveDelayFactor.BackColor = ChangeColor
                End If

                If .PreDownSpeed = Val(textArcPreDownSpeed.Text) Then
                    textArcPreDownSpeed.BackColor = System.Drawing.SystemColors.Window
                Else
                    textArcPreDownSpeed.BackColor = ChangeColor
                End If

                If .PreDownAcc = Val(textArcPreDownAcc.Text) Then
                    textArcPreDownAcc.BackColor = System.Drawing.SystemColors.Window
                Else
                    textArcPreDownAcc.BackColor = ChangeColor
                End If

                If .PreDispenseGap = Val(textArcPreDispenseGap.Text) Then
                    textArcPreDispenseGap.BackColor = System.Drawing.SystemColors.Window
                Else
                    textArcPreDispenseGap.BackColor = ChangeColor
                End If

                If .DuringDispenseSpeed = Val(textArcDuringSpeed.Text) Then
                    textArcDuringSpeed.BackColor = System.Drawing.SystemColors.Window
                Else
                    textArcDuringSpeed.BackColor = ChangeColor
                End If

                If .DuringShutOffDistance = Val(textArcDuringShutOffDistance.Text) Then
                    textArcDuringShutOffDistance.BackColor = System.Drawing.SystemColors.Window
                Else
                    textArcDuringShutOffDistance.BackColor = ChangeColor
                End If

                If .PostSuckBack = Val(textArcPostSuckBack.Text) Then
                    textArcPostSuckBack.BackColor = System.Drawing.SystemColors.Window
                Else
                    textArcPostSuckBack.BackColor = ChangeColor
                End If

                If .PostDwellTime = Val(textArcPostDwell.Text) Then
                    textArcPostDwell.BackColor = System.Drawing.SystemColors.Window
                Else
                    textArcPostDwell.BackColor = ChangeColor
                End If

                If .PostBacktrackGap = Val(textArcPostBacktrackGap.Text) Then
                    textArcPostBacktrackGap.BackColor = System.Drawing.SystemColors.Window
                Else
                    textArcPostBacktrackGap.BackColor = ChangeColor
                End If

                If .PostBacktrackLength = Val(textArcPostBacktrackLength.Text) Then
                    textArcPostBacktrackLength.BackColor = System.Drawing.SystemColors.Window
                Else
                    textArcPostBacktrackLength.BackColor = ChangeColor
                End If

                If .PostBacktrackSpeed = Val(textArcPostBacktrackSpeed.Text) Then
                    textArcPostBacktrackSpeed.BackColor = System.Drawing.SystemColors.Window
                Else
                    textArcPostBacktrackSpeed.BackColor = ChangeColor
                End If

                If .PostRetractDistance = Val(textArcPostRetractDistance.Text) Then
                    textArcPostRetractDistance.BackColor = System.Drawing.SystemColors.Window
                Else
                    textArcPostRetractDistance.BackColor = ChangeColor
                End If

                If .PostRetractSpeed = Val(textArcPostRetractSpeed.Text) Then
                    textArcPostRetractSpeed.BackColor = System.Drawing.SystemColors.Window
                Else
                    textArcPostRetractSpeed.BackColor = ChangeColor
                End If
                If .PostRetractAcc = Val(textArcPostRetractAcc.Text) Then
                    textArcPostRetractAcc.BackColor = System.Drawing.SystemColors.Window
                Else
                    textArcPostRetractAcc.BackColor = ChangeColor
                End If
            End With
        End If
    End Sub

    Private Sub ucArcTypeParameter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fnAddArcValueEvent()
        fnSetupToolTip()
        If (Not SelectItemName = "") Then
            Dim ArcParaBuffer As New CArcTypeParameter(mlstArcValueSelectedItem)
            ArcParaBuffer.Load(fnGetArcValueDbFileName(mlstArcValueSelectedItem))
            ShowArcValue(ArcParaBuffer)
            mNowShowData = ArcParaBuffer
        End If
    End Sub



    Private Sub btSetDefaultValue_Click(sender As Object, e As EventArgs) Handles btSetDefaultValue.Click
        textArcPreMoveDelayFactor.Text = "0"
        textArcPreDownSpeed.Text = "100"
        textArcPreDownAcc.Text = "2940"
        textArcPreDispenseGap.Text = "2"
        textArcDuringSpeed.Text = "10"
        textArcDuringShutOffDistance.Text = "0"
        textArcPostSuckBack.Text = "0"
        textArcPostDwell.Text = "0"
        textArcPostBacktrackGap.Text = "0"
        textArcPostBacktrackLength.Text = "0"
        textArcPostBacktrackSpeed.Text = "100"
        textArcPostRetractDistance.Text = "0"
        textArcPostRetractSpeed.Text = "100"
        textArcPostRetractAcc.Text = "2940"
    End Sub
#End Region

End Class
