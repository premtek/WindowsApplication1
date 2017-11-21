Imports ProjectCore
'Imports ProjectIO
'Imports ProjectFeedback
'Imports ProjectRecipe
'Imports ProjectMotion



Imports System.Text.RegularExpressions
Imports System.Data.OleDb
'Imports System.Web.UI.DataVisualization.Charting       '//Chart Web 伺服器控制項的方法和屬性
Imports System.Drawing                                 '//繪圖功能的存取
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Random



Public Module MCommonFunction

    Public strShowMessage As String = "None"          '取得Message

    Private Delegate Sub ImInvokeLabelColor(ByVal ctrl As Windows.Forms.Label, ByVal color As Color)
    Public Sub InvokeLabelBackColor(ByVal ctrl As Windows.Forms.Label, ByVal color As Color)
        If ctrl.InvokeRequired Then
            Dim t As New ImInvokeLabelColor(AddressOf InvokeLabelBackColor)
            ctrl.Invoke(t, New Object() {ctrl, color})
        Else
            ctrl.BackColor = color
        End If
    End Sub

    ''' <summary>非同步更新Label控制項委派</summary>
    ''' <param name="ctrl"></param>
    ''' <param name="str"></param>
    ''' <remarks></remarks>
    Private Delegate Sub ImInvokeLabel(ByVal ctrl As Windows.Forms.Label, ByVal [str] As String)
    ''' <summary>非同步更新Label控制項方法</summary>
    ''' <param name="ctrl"></param>
    ''' <param name="str"></param>
    ''' <remarks></remarks>
    Public Sub InvokeLabel(ByVal ctrl As Windows.Forms.Label, _
   ByVal [str] As String)

        If ctrl.InvokeRequired Then
            Dim t As New ImInvokeLabel(AddressOf InvokeLabel)
            ctrl.Invoke(t, New Object() {ctrl, str})
        Else
            If str = "" Then
                ctrl.Text = ""
            Else
                ctrl.Text = str
            End If
        End If

    End Sub
    ''' <summary>
    ''' 非同步更新ComboBox控制項委派
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <param name="str"></param>
    ''' <remarks></remarks>
    Private Delegate Sub ImInvokeCombobox(ByVal ctrl As Windows.Forms.ComboBox, ByVal [str] As String)
    ''' <summary>
    ''' 非同步更新ComboBox控制項方法
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <param name="str"></param>
    ''' <remarks></remarks>
    Public Sub InvokeComboBox(ByVal ctrl As Windows.Forms.ComboBox, ByVal [str] As String)
        If ctrl.InvokeRequired Then
            Dim t As New ImInvokeCombobox(AddressOf InvokeComboBox)
            ctrl.Invoke(t, New Object() {ctrl, str})
        Else
            If str = "" Then
                ctrl.Text = ""
            Else
                ctrl.Text = str
            End If
        End If
    End Sub

    ''' <summary>非同步更新Button控制項委派</summary>
    ''' <param name="ctrl"></param>
    ''' <param name="str"></param>
    ''' <remarks></remarks>
    Private Delegate Sub ImInvokeButton(ByVal ctrl As Windows.Forms.Button, ByVal [str] As String)
    ''' <summary>非同步更新Button控制項方法</summary>
    ''' <param name="ctrl"></param>
    ''' <param name="str"></param>
    ''' <remarks></remarks>
    Public Sub InvokeButton(ByVal ctrl As Windows.Forms.Button, ByVal [str] As String)

        If ctrl.InvokeRequired Then
            Dim t As New ImInvokeButton(AddressOf InvokeButton)
            ctrl.Invoke(t, New Object() {ctrl, str})
        Else
            If str = "" Then
                ctrl.Text = ""
            Else
                ctrl.Text = str
            End If
        End If

    End Sub

    ''' <summary>非同步更新TextBox控制項委派</summary>
    ''' <param name="txtbox"></param>
    ''' <param name="str"></param>
    ''' <param name="append"></param>
    ''' <remarks></remarks>
    Private Delegate Sub ImInvokeTextBox(ByVal [txtbox] As Windows.Forms.TextBox, ByVal [str] As String, ByVal [append] As Boolean)
    ''' <summary>非同步更新TextBix控制項方法</summary>
    ''' <param name="txtbox"></param>
    ''' <param name="str"></param>
    ''' <param name="append"></param>
    ''' <remarks></remarks>
    Public Sub InvokeTextBox(ByVal [txtbox] As Windows.Forms.TextBox, ByVal [str] As String, Optional ByVal [append] As Boolean = False)

        If txtbox.InvokeRequired Then
            Dim t As New ImInvokeTextBox(AddressOf InvokeTextBox)
            txtbox.BeginInvoke(t, New Object() {txtbox, str, append})
        Else
            If str = "" Then
                txtbox.Text = ""
            Else
                If append Then
                    txtbox.AppendText(str)
                Else
                    txtbox.Text = str
                End If
            End If
        End If

    End Sub
    ''' <summary>
    ''' 非同步更新Button控制項委派
    ''' </summary>
    ''' <param name="button"></param>
    ''' <param name="enable"></param>
    ''' <remarks></remarks>
    Private Delegate Sub ImInvokeButtonEnable(ByVal [button] As Windows.Forms.Button, ByVal [enable] As Boolean)
    ''' <summary>非同步更新Button控制項方法</summary>
    ''' <param name="button"></param>
    ''' <param name="enable"></param>
    ''' <remarks></remarks>
    Public Sub InvokeButtonEnable(ByVal [button] As Windows.Forms.Button, ByVal [enable] As Boolean)

        If button.InvokeRequired Then
            Dim t As New ImInvokeButtonEnable(AddressOf InvokeButtonEnable)
            button.Invoke(t, New Object() {button, enable})
        Else
            button.Enabled = enable
        End If

    End Sub
    ''' <summary>非同步更新ListBox控制項委派</summary>
    ''' <param name="ctrl"></param>
    ''' <param name="str"></param>
    ''' <remarks></remarks>
    Private Delegate Sub ImInvokelistbox(ByVal ctrl As Windows.Forms.ListBox, ByVal [str] As String)
    ''' <summary>非同步更新ListBox控制項方法</summary>
    ''' <param name="ctrl"></param>
    ''' <param name="str"></param>
    ''' <remarks></remarks>
    Public Sub Invokelistbox(ByVal ctrl As Windows.Forms.ListBox, ByVal [str] As String)
        If ctrl.InvokeRequired Then
            Dim t As New ImInvokelistbox(AddressOf Invokelistbox)
            ctrl.Invoke(t, New Object() {ctrl, str})
        Else
            ctrl.Items.Add(str)
        End If
    End Sub


    ''' <summary>非同步更新Chart控制項委派(重量控制限定)</summary>
    ''' <param name="ctrl"></param>
    ''' <param name="Counter"></param>
    ''' <param name="Upper"></param>
    ''' <param name="Lower"></param>
    ''' <param name="Weight"></param>
    ''' <remarks></remarks>
    Private Delegate Sub ImInvokeChart(ByVal ctrl As Windows.Forms.DataVisualization.Charting.Chart, ByVal [Counter] As Double, ByVal [Upper] As Double, ByVal [Lower] As Double, ByVal [Weight] As Double)
    ''' <summary>非同步更新Chart控制項方法(重量控制限定)</summary>
    ''' <param name="ctrl"></param>
    ''' <param name="Counter"></param>
    ''' <param name="Upper"></param>
    ''' <param name="Lower"></param>
    ''' <param name="Weight"></param>
    ''' <remarks></remarks>
    Public Sub InvokeChartWeight(ByVal ctrl As Windows.Forms.DataVisualization.Charting.Chart, _
    ByVal [Counter] As Double, ByVal [Upper] As Double, ByVal [Lower] As Double, ByVal [Weight] As Double)
        If ctrl.InvokeRequired Then
            Dim t As New ImInvokeChart(AddressOf InvokeChartWeight)
            ctrl.Invoke(t, New Object() {ctrl, Counter, Upper, Lower, Weight})
        Else
            '20160909 Upper  Lower Weight

            ' If ctrl.Series.IsUniqueName("Upper") <> False Then '2016.06.27 保護  
            ctrl.Series("Upper").Points.AddXY(Counter, Upper)
            'End If
            'If ctrl.Series.IsUniqueName("重量") Then '2016.06.27 保護
            ctrl.Series("Weight").Points.AddXY(Counter, Math.Round(Weight, 3))
            'End If
            'If ctrl.Series.IsUniqueName("標準下限值") Then '2016.06.27 保護
            ctrl.Series("Lower").Points.AddXY(Counter, Lower)
            'End If
        End If
    End Sub

    ''' <summary>非同步更新Chart控制項委派(重量控制限定)</summary>
    ''' <param name="ctrl2"></param>
    ''' <param name="Counter"></param>
    ''' <param name="Upper"></param>
    ''' <param name="Lower"></param>
    ''' <param name="Weight"></param>
    ''' <remarks></remarks>
    Private Delegate Sub ImInvokeChartDots(ByVal ctrl2 As Windows.Forms.DataVisualization.Charting.Chart, ByVal [Counter] As Double, ByVal [Upper] As Double, ByVal [Lower] As Double, ByVal [Weight] As Double)
    ''' <summary>非同步更新Chart控制項方法(重量控制限定)</summary>
    ''' <param name="ctrl2"></param>
    ''' <param name="Counter"></param>
    ''' <param name="Upper"></param>
    ''' <param name="Lower"></param>
    ''' <param name="Weight"></param>
    ''' <remarks></remarks>
    Public Sub InvokeChartDotsWeight(ByVal ctrl2 As Windows.Forms.DataVisualization.Charting.Chart, ByVal [Counter] As Double, ByVal [Upper] As Double, ByVal [Lower] As Double, ByVal [Weight] As Double)
        If ctrl2.InvokeRequired Then
            Dim t As New ImInvokeChartDots(AddressOf InvokeChartDotsWeight)
            ctrl2.Invoke(t, New Object() {ctrl2, Counter, Upper, Lower, Weight})
        Else
            '20160909 Upper  Lower Weight
            '    ctrl.Series("標準上限值").Points.AddXY(Counter, Upper)
            ctrl2.Series("Dot_Max").Points.AddXY(Counter, Upper)
            '   If ctrl.Series.IsUniqueName("重量") Then '2016.06.27 保護
            ctrl2.Series("Dot_Weight").Points.AddXY(Counter, Math.Round(Weight, 3))
            'End If
            ctrl2.Series("Dot_Min").Points.AddXY(Counter, Lower)
        End If
    End Sub

    '20171114
    Private Delegate Sub ImInvokeClearMessage(ByVal cmb As Windows.Forms.ComboBox)
    Public Sub InvokeClearMessage(ByVal cmb As Windows.Forms.ComboBox)
        If cmb.InvokeRequired Then
            Dim t As New ImInvokeClearMessage(AddressOf InvokeClearMessage)
            cmb.Invoke(t, New Object() {cmb})
        Else
            cmb.Items.Clear()
            cmb.Text = ""
        End If
    End Sub

    'Jeffadd 20150302
    '==========================================================================================================================================================================
    Private Delegate Sub ImInvokeCreateChart1(ByVal ctrl As Windows.Forms.DataVisualization.Charting.Chart, ByVal [Upper] As Double, ByVal [Lower] As Double, ByVal [WeighingPointNumber] As Double, ByVal [WeightCounter] As Double)
    Public Sub InvokeCreateChart1(ByVal ctrl As Windows.Forms.DataVisualization.Charting.Chart, ByVal [Upper] As Double, ByVal [Lower] As Double, ByVal [WeighingPointNumber] As Double, ByVal [WeightCounter] As Double)

        If ctrl.InvokeRequired Then
            Dim t As New ImInvokeCreateChart1(AddressOf InvokeCreateChart1)
            ctrl.Invoke(t, New Object() {ctrl, Upper, Lower, WeighingPointNumber, WeightCounter})
        Else

            '[說明]:Counter 為零才清除螢幕
            If WeightCounter = 0 Then
                ctrl.Series.Clear()
                '[說明]:Update chartWeight
                ctrl.Titles.Clear()
                Dim newTitlesWeight1 As New Title("重量分布圖") '建立??

                newTitlesWeight1.Text = ""

                ctrl.Titles.Add(newTitlesWeight1)

                Dim newSeries1 As New Series("Upper") '新增?据集
                newSeries1.ChartType = SeriesChartType.Line '直?
                newSeries1.BorderWidth = 2
                newSeries1.Color = Color.Red
                '  newSeries1.XValueType = 30 'ChartValueType.Time
                newSeries1.IsValueShownAsLabel = False
                ctrl.Series.Add(newSeries1)

                Dim newSeries2 As New Series("Weight") '新增?据集
                newSeries2.ChartType = SeriesChartType.Line
                newSeries2.BorderWidth = 2
                newSeries2.Color = Color.Green
                '  newSeries2.XValueType = 30
                newSeries2.IsValueShownAsLabel = True
                newSeries2.MarkerStyle = MarkerStyle.Square
                ctrl.Series.Add(newSeries2)

                Dim newSeries3 As New Series("Lower") '新增?据集
                newSeries3.ChartType = SeriesChartType.Line
                newSeries3.BorderWidth = 2
                newSeries3.Color = Color.Red
                ' newSeries3.XValueType = 30 ' ChartValueType.Time
                newSeries3.IsValueShownAsLabel = False
                ctrl.Series.Add(newSeries3)

                '[說明]:計算秤重上下限 公式:重量 = 預設重量 + ((預設重量 * (比率))/2)
                ctrl.ChartAreas(0).AxisY.Minimum = Math.Round(((Upper + Lower) / 2) - (Upper - Lower) * 4, 3)
                ctrl.ChartAreas(0).AxisY.Maximum = Math.Round(((Upper + Lower) / 2) + (Upper - Lower) * 4, 3)

            End If
        End If
    End Sub

    Private Delegate Sub ImInvokeCreateChart2(ByVal ctr2 As Windows.Forms.DataVisualization.Charting.Chart, ByVal [Upper] As Double, ByVal [Lower] As Double, ByVal [WeighingPointNumber] As Double, ByVal [WeightCounter] As Double)
    Public Sub InvokeCreateChart2(ByVal ctr2 As Windows.Forms.DataVisualization.Charting.Chart, ByVal [Upper] As Double, ByVal [Lower] As Double, ByVal [WeighingPointNumber] As Double, ByVal [WeightCounter] As Double)

        If ctr2.InvokeRequired Then
            Dim t As New ImInvokeCreateChart2(AddressOf InvokeCreateChart2)
            ctr2.Invoke(t, New Object() {ctr2, Upper, Lower, WeighingPointNumber, WeightCounter})
        Else
            '[說明]:Counter 為零才清除螢幕
            If WeightCounter = 0 Then
                ctr2.Series.Clear()

                '[說明]:Update chartDotsWeight
                ctr2.Titles.Clear()
                Dim newTitles1 As New Title("重量分布圖") '建立??

                newTitles1.Text = ""

                ctr2.Titles.Add(newTitles1)

                Dim newSeries4 As New Series("Dot_Max") '新增?据集
                newSeries4.ChartType = SeriesChartType.Line '直?
                newSeries4.BorderWidth = 2
                newSeries4.Color = Color.Red
                ' newSeries4.XValueType = ChartValueType.Time
                newSeries4.IsValueShownAsLabel = False
                ctr2.Series.Add(newSeries4)

                Dim newSeries5 As New Series("Dot_Weight")
                newSeries5.ChartType = SeriesChartType.Column
                newSeries5.BorderWidth = 2
                newSeries5.Color = Color.Green
                ' newSeries5.XValueType = ChartValueType.Time
                newSeries5.IsValueShownAsLabel = True
                newSeries5.MarkerStyle = MarkerStyle.Square
                ctr2.Series.Add(newSeries5)

                Dim newSeries6 As New Series("Dot_Min")
                newSeries6.ChartType = SeriesChartType.Line
                newSeries6.BorderWidth = 2
                newSeries6.Color = Color.Red
                ' newSeries6.XValueType = ChartValueType.Time
                newSeries6.IsValueShownAsLabel = False
                ctr2.Series.Add(newSeries6)

                '[說明]:計算秤重上下限 
                ' ctr2.ChartAreas(0).AxisY.Minimum = (Math.Round(Lower / WeighingPointNumber, 4))
                '  ctr2.ChartAreas(0).AxisY.Maximum = (Math.Round(Upper / WeighingPointNumber, 4))

                ctr2.ChartAreas(0).AxisY.Minimum = Math.Round((Lower), 3)
                ctr2.ChartAreas(0).AxisY.Maximum = Math.Round((Upper), 3)
            End If
        End If

    End Sub
    '==========================================================================================================================================================================


    ''' <summary>按鍵唯讀行為(不可編輯的反應)</summary>
    ''' <remarks></remarks>
    Public Sub BtnReadOnlyBehavior(btn As Button)
        btn.BackColor = Color.Red
        btn.Refresh()
        Threading.Thread.Sleep(300)
        btn.BackColor = SystemColors.Control
        btn.Refresh()
    End Sub
    ''' <summary>按鍵行為(請先復歸反應)</summary>
    ''' <param name="btn"></param>
    ''' <remarks></remarks>
    Public Sub BtnHomeFirstBehavior(btn As Button)
        btn.BackColor = Color.Yellow
        btn.Refresh()
        Threading.Thread.Sleep(300)
        btn.BackColor = SystemColors.Control
        btn.Refresh()
    End Sub

    'Delay1 time  /使用在無迴圈裡面
    Public Sub Delay(ByVal Number As Double)      'Number單位為毫秒
        On Error GoTo ErrorHandler

        System.Threading.Thread.Sleep(Number)
ErrorHandler:
        If Err.Number <> 0 Then  ' No error. Do nothing.
            strShowMessage = Err.GetException.StackTrace
            'frmMessage.TopMost = True
            'frmMessage.Visible = False
            'frmMessage.Show()
            'frmMessage.BringToFront()
            Err.Clear()         '清除錯誤資訊
            Exit Sub            '離開Sub(跳出函式)
        End If
    End Sub

    ''' <summary>[Is TimeOut]</summary>
    ''' <param name="iStopWatch"></param>
    ''' <param name="iTime"></param>
    ''' <param name="iTimeOutTimer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsTimeOut(ByVal iStopWatch As Stopwatch, ByVal iTime As Double, ByVal iTimeOutTimer As Double) As Boolean
        If Math.Abs(iTime - iStopWatch.ElapsedMilliseconds) > iTimeOutTimer Then
            iStopWatch.Stop()
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>[Is TimeOut]</summary>
    ''' <param name="iStopWatch"></param>
    ''' <param name="iTimeOutTimer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsTimeOut(ByVal iStopWatch As Stopwatch, ByVal iTimeOutTimer As Double) As Boolean
        If Math.Abs(iStopWatch.ElapsedMilliseconds) > iTimeOutTimer Then
            iStopWatch.Stop()
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>自製轉Decimal</summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CDecimal(ByVal data As String) As Decimal
        Dim value As Decimal
        Decimal.TryParse(data, value)
        Return value
    End Function

    ''' <summary>數值方塊設定保護</summary>
    ''' <param name="numericUpdown"></param>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Sub SelectNumericUpDown(ByRef numericUpdown As NumericUpDown, ByVal value As Decimal)
        If value > numericUpdown.Maximum Then
            numericUpdown.Value = numericUpdown.Maximum
            numericUpdown.BackColor = Color.Red
            Exit Sub
        End If
        If value < numericUpdown.Minimum Then
            numericUpdown.Value = numericUpdown.Minimum
            numericUpdown.BackColor = Color.Red
            Exit Sub
        End If
        numericUpdown.Value = value
        numericUpdown.BackColor = Color.White
    End Sub

    ''' <summary>下拉式方塊控制項選取保護</summary>
    ''' <param name="cmb"></param>
    ''' <param name="index"></param>
    ''' <remarks></remarks>
    Sub SelectComboBox(ByRef cmb As ComboBox, ByVal index As Integer)
        If cmb.Items.Count = 0 Then
            cmb.BackColor = Color.Red
            Exit Sub
        End If
        If index >= cmb.Items.Count Then
            cmb.BackColor = Color.Red
            Exit Sub
        End If
        If index < 0 Then
            Exit Sub
        End If
        cmb.SelectedIndex = index
    End Sub

    ''' <summary>下拉式方塊控制項選取保護</summary>
    ''' <param name="cmb"></param>
    ''' <param name="item"></param>
    ''' <remarks></remarks>
    Sub SelectComboBox(ByRef cmb As ComboBox, ByVal item As String)
        If cmb.Items.Count = 0 Then
            cmb.BackColor = Color.Red
            Exit Sub
        End If
        If item Is Nothing Then
            cmb.BackColor = Color.Red
            Exit Sub
        End If
        If item = "" Then
            cmb.BackColor = Color.Red
            Exit Sub
        End If
        If Not cmb.Items.Contains(item) Then
            cmb.BackColor = Color.Red
            Exit Sub
        End If
       
        cmb.SelectedItem = item
    End Sub


End Module
