Imports ProjectCore

Public Class ucDigitKeyboard

    Const MaxValueLength As Integer = 11
    Public Event OnEnterClicked(sender As Object, e As EventArgs)

    Private Sub Btn1_Click(sender As Object, e As EventArgs) Handles btn1.Click
        gSyslog.Save("[ucDigitKeyboard]" & vbTab & "[btn1]" & vbTab & "Click")
        If Val(lblValue.Text) = 0 Then
            lblValue.Text = "1"
        ElseIf lblValue.Text.Length >= MaxValueLength Then '上限輸入保護
        Else
            lblValue.Text += "1"
        End If

    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        gSyslog.Save("[ucDigitKeyboard]" & vbTab & "[BtnClear]" & vbTab & "Click")
        lblValue.Text = "0"
    End Sub

    Private Sub Btn2_Click(sender As Object, e As EventArgs) Handles btn2.Click
        gSyslog.Save("[ucDigitKeyboard]" & vbTab & "[btn2]" & vbTab & "Click")
        If Val(lblValue.Text) = 0 Then
            lblValue.Text = "2"
        ElseIf lblValue.Text.Length >= MaxValueLength Then '上限輸入保護
        Else
            lblValue.Text += "2"
        End If
    End Sub

    Private Sub Btn3_Click(sender As Object, e As EventArgs) Handles btn3.Click
        gSyslog.Save("[ucDigitKeyboard]" & vbTab & "[btn3]" & vbTab & "Click")
        If Val(lblValue.Text) = 0 Then
            lblValue.Text = "3"
        ElseIf lblValue.Text.Length >= MaxValueLength Then '上限輸入保護
        Else
            lblValue.Text += "3"
        End If
    End Sub

    Private Sub Btn4_Click(sender As Object, e As EventArgs) Handles btn4.Click
        gSyslog.Save("[ucDigitKeyboard]" & vbTab & "[btn4]" & vbTab & "Click")
        If Val(lblValue.Text) = 0 Then
            lblValue.Text = "4"
        ElseIf lblValue.Text.Length >= MaxValueLength Then '上限輸入保護
        Else
            lblValue.Text += "4"
        End If
    End Sub

    Private Sub Btn5_Click(sender As Object, e As EventArgs) Handles btn5.Click
        gSyslog.Save("[ucDigitKeyboard]" & vbTab & "[btn5]" & vbTab & "Click")
        If Val(lblValue.Text) = 0 Then
            lblValue.Text = "5"
        ElseIf lblValue.Text.Length >= MaxValueLength Then '上限輸入保護
        Else
            lblValue.Text += "5"
        End If
    End Sub

    Private Sub Btn6_Click(sender As Object, e As EventArgs) Handles btn6.Click
        gSyslog.Save("[ucDigitKeyboard]" & vbTab & "[btn6]" & vbTab & "Click")
        If Val(lblValue.Text) = 0 Then
            lblValue.Text = "6"
        ElseIf lblValue.Text.Length >= MaxValueLength Then '上限輸入保護
        Else
            lblValue.Text += "6"
        End If
    End Sub

    Private Sub Btn7_Click(sender As Object, e As EventArgs) Handles btn7.Click
        gSyslog.Save("[ucDigitKeyboard]" & vbTab & "[btn7]" & vbTab & "Click")
        If Val(lblValue.Text) = 0 Then
            lblValue.Text = "7"
        ElseIf lblValue.Text.Length >= MaxValueLength Then '上限輸入保護
        Else
            lblValue.Text += "7"
        End If
    End Sub

    Private Sub Btn8_Click(sender As Object, e As EventArgs) Handles btn8.Click
        gSyslog.Save("[ucDigitKeyboard]" & vbTab & "[btn8]" & vbTab & "Click")
        If Val(lblValue.Text) = 0 Then
            lblValue.Text = "8"
        ElseIf lblValue.Text.Length >= MaxValueLength Then '上限輸入保護
        Else
            lblValue.Text += "8"
        End If
    End Sub

    Private Sub Btn9_Click(sender As Object, e As EventArgs) Handles btn9.Click
        gSyslog.Save("[ucDigitKeyboard]" & vbTab & "[btn9]" & vbTab & "Click")
        If Val(lblValue.Text) = 0 Then
            lblValue.Text = "9"
        ElseIf lblValue.Text.Length >= MaxValueLength Then '上限輸入保護
        Else
            lblValue.Text += "9"
        End If
    End Sub

    Private Sub BtnBackSpace_Click(sender As Object, e As EventArgs) Handles BtnBackSpace.Click
        gSyslog.Save("[ucDigitKeyboard]" & vbTab & "[BtnBackSpace]" & vbTab & "Click")
        If Val(lblValue.Text) = 0 Then
            lblValue.Text = "0"
        Else
            lblValue.Text = lblValue.Text.Substring(0, lblValue.Text.Length - 1)
        End If
    End Sub

    Private Sub BtnDot_Click(sender As Object, e As EventArgs) Handles btnDot.Click
        gSyslog.Save("[ucDigitKeyboard]" & vbTab & "[btnDot]" & vbTab & "Click")
        If Val(lblValue.Text) = 0 Then
            lblValue.Text = "0"
        ElseIf lblValue.Text.EndsWith(".") Then
            '已輸入
        Else
            lblValue.Text += "."
        End If
    End Sub

    Private Sub BtnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click
        gSyslog.Save("[ucDigitKeyboard]" & vbTab & "[btnEnter]" & vbTab & "Click")
        Value = Convert.ToDouble(lblValue.Text)
        RaiseEvent OnEnterClicked(Me, e)
    End Sub
    Public Property Value As Double

    Private Sub Btn0_Click(sender As Object, e As EventArgs) Handles btn0.Click
        gSyslog.Save("[ucDigitKeyboard]" & vbTab & "[btn0]" & vbTab & "Click")
        If Val(lblValue.Text) = 0 Then
            lblValue.Text = "0"
        ElseIf lblValue.Text.Length >= MaxValueLength Then '上限輸入保護
        Else
            lblValue.Text += "0"
        End If
    End Sub

    Private Sub Btn00_Click(sender As Object, e As EventArgs) Handles btn00.Click
        gSyslog.Save("[ucDigitKeyboard]" & vbTab & "[btn00]" & vbTab & "Click")
        If Val(lblValue.Text) = 0 Then
            lblValue.Text = "0"
        ElseIf lblValue.Text.Length >= MaxValueLength Then '上限輸入保護
        Else
            lblValue.Text += "00"
        End If
    End Sub
End Class
