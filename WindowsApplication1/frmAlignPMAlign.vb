Imports System
Imports ProjectCore
Imports ProjectAOI

Imports Cognex.VisionPro
Imports Cognex.VisionPro.PMAlign

Public Class frmAlignPMAlign
    Dim ClassName As String = "frmAlignPMAlign"

    Public myPMAlignTool As Cognex.VisionPro.PMAlign.CogPMAlignTool
    Public myBlobTool As Cognex.VisionPro.Blob.CogBlobTool

    Public myROIType As eROIType

    Dim dragRect As New CogRectangle
    Dim mRect As New CogRectangle


    ''' <summary>
    ''' 暫存工具
    ''' </summary>
    ''' <remarks></remarks>
    Private mytmpPMAlignTool As Cognex.VisionPro.PMAlign.CogPMAlignTool
    Private mytmpBlobTool As Cognex.VisionPro.Blob.CogBlobTool

    Private Sub frmAlignPMAlign_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Select Case myROIType
            Case eROIType.PMAlign
                If myPMAlignTool IsNot Nothing Then
                    mytmpPMAlignTool = myPMAlignTool

                    If mytmpPMAlignTool.InputImage IsNot Nothing Then
                        CogDisplay1.Image = mytmpPMAlignTool.InputImage
                        CogDisplay1.Fit(True)
                    End If

                    If mytmpPMAlignTool.SearchRegion Is Nothing Then
                        '[Note] None-All Image
                        rdbSearchAreaALL.Checked = True
                    Else
                        rdbSearchArea.Checked = True
                    End If
                End If
            Case eROIType.Blob
                If myBlobTool IsNot Nothing Then
                    mytmpBlobTool = myBlobTool
                    If mytmpBlobTool.InputImage IsNot Nothing Then
                        CogDisplay1.Image = mytmpBlobTool.InputImage
                        CogDisplay1.Fit(True)
                    End If

                    If mytmpBlobTool.Region Is Nothing Then
                        '[Note] None-All Image
                        rdbSearchAreaALL.Checked = True
                    Else
                        rdbSearchArea.Checked = True
                    End If
                End If
        End Select
    End Sub

    Private Sub frmAlignPMAlign_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        CogDisplay1.Dispose()
        Me.Dispose(True)
    End Sub


    Private shiftIsDown As Boolean
    Private Sub cogDisplay1_KeyDown(ByVal sender As System.Object, ByVal e As KeyEventArgs)
        shiftIsDown = True 'e.Shift
        Debug.Print("KeyDown shiftIsDown:" & shiftIsDown)

    End Sub

    Private Sub cogDisplay1_KeyUp(ByVal sender As System.Object, ByVal e As KeyEventArgs)
        shiftIsDown = False 'e.Shift
        Debug.Print("KeyUp shiftIsDown:" & shiftIsDown)

    End Sub
    Private Sub mRect_Dragging(ByVal sender As System.Object, ByVal e As CogDraggingEventArgs)
        Debug.Print("mRect_Dragging:" & shiftIsDown)
        dragRect = CType(e.DragGraphic, CogRectangle)
        Select Case myROIType
            Case eROIType.PMAlign
                'If shiftIsDown Then
                If dragRect.Width > mytmpPMAlignTool.InputImage.Width Then
                    dragRect.Width = mytmpPMAlignTool.InputImage.Width
                End If
                If dragRect.Height > mytmpPMAlignTool.InputImage.Height Then
                    dragRect.Height = mytmpPMAlignTool.InputImage.Height
                End If

                dragRect.Width = Math.Ceiling(dragRect.Width)   '取整數
                dragRect.Height = Math.Ceiling(dragRect.Height) '取整數

                dragRect.X = (mytmpPMAlignTool.InputImage.Width / 2) - (dragRect.Width / 2)  '800
                dragRect.Y = (mytmpPMAlignTool.InputImage.Height / 2) - (dragRect.Height / 2) '600
                'End If
            Case eROIType.Blob
                'If shiftIsDown Then
                If dragRect.Width > mytmpBlobTool.InputImage.Width Then
                    dragRect.Width = mytmpBlobTool.InputImage.Width
                End If
                If dragRect.Height > mytmpBlobTool.InputImage.Height Then
                    dragRect.Height = mytmpBlobTool.InputImage.Height
                End If

                dragRect.Width = Math.Ceiling(dragRect.Width)   '取整數
                dragRect.Height = Math.Ceiling(dragRect.Height) '取整數

                dragRect.X = (mytmpBlobTool.InputImage.Width / 2) - (dragRect.Width / 2)  '800
                dragRect.Y = (mytmpBlobTool.InputImage.Height / 2) - (dragRect.Height / 2) '600
                'End If
        End Select




    End Sub

    Private Sub mRect_DraggingStopped(ByVal sender As System.Object, ByVal e As CogDraggingEventArgs)
        Debug.Print("mRect_DraggingStopped:" & shiftIsDown)
        mRect_Dragging(sender, e)

        '[Note]更新介面數值
        dragRect = CType(e.DragGraphic, CogRectangle)
        nmcSearchAreaW.Value = dragRect.Width
        nmcSearchAreaH.Value = dragRect.Height

    End Sub


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnCancel]" & vbTab & "Click")
        Me.Close()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If btnOK.Enabled = False Then
            Exit Sub
        End If
        gSyslog.Save("[" & ClassName & "]" & vbTab & "[btnOK]" & vbTab & "Click")
        btnOK.Enabled = False

        Select Case myROIType
            Case eROIType.PMAlign
                If rdbSearchArea.Checked Then
                    '[Note]有ROI
                    mytmpPMAlignTool.SearchRegion = mRect
                Else
                    mytmpPMAlignTool.SearchRegion = Nothing
                End If
                myPMAlignTool = mytmpPMAlignTool


            Case eROIType.Blob
                If rdbSearchArea.Checked Then
                    '[Note]有ROI
                    mytmpBlobTool.Region = mRect
                Else
                    mytmpBlobTool.Region = Nothing
                End If
                myBlobTool = mytmpBlobTool

        End Select

        '存檔成功 
        gSyslog.Save(gMsgHandler.GetMessage(Warn_3000036))
        MsgBox(gMsgHandler.GetMessage(Warn_3000036), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)

        btnOK.Enabled = True
    End Sub

    Private Sub rdbSearchAreaALL_CheckedChanged(sender As Object, e As EventArgs) Handles rdbSearchAreaALL.CheckedChanged
        If rdbSearchAreaALL.Checked Then
            rdbSearchArea.Checked = False
            nmcSearchAreaW.Enabled = False
            nmcSearchAreaH.Enabled = False

            Select Case myROIType
                Case eROIType.PMAlign
                    If mytmpPMAlignTool IsNot Nothing Then
                        mytmpPMAlignTool.SearchRegion = Nothing
                        CogDisplay1.InteractiveGraphics.Clear()
                    End If
                Case eROIType.Blob
                    If mytmpBlobTool IsNot Nothing Then
                        mytmpBlobTool.Region = Nothing
                        CogDisplay1.InteractiveGraphics.Clear()
                    End If

            End Select

        End If

    End Sub

    Private Sub rdbSearchArea_CheckedChanged(sender As Object, e As EventArgs) Handles rdbSearchArea.CheckedChanged
        If rdbSearchArea.Checked Then
            rdbSearchAreaALL.Checked = False
            nmcSearchAreaW.Enabled = True
            nmcSearchAreaH.Enabled = True

            Dim myRectWidth, myRectHeight As Double
            Dim myRectCenterX, myRectCenterY As Double


            Select Case myROIType
                Case eROIType.PMAlign
                    If mytmpPMAlignTool.SearchRegion Is Nothing Then
                        '[Note]沒有教導框，新增一個
                        'Dim tmp As CogRectangle = CType(mRect, CogRectangle)
                        myRectWidth = nmcSearchAreaW.Value
                        myRectHeight = nmcSearchAreaH.Value
                        myRectCenterX = (mytmpPMAlignTool.InputImage.Width / 2) '- (myRectWidth / 2)
                        myRectCenterY = (mytmpPMAlignTool.InputImage.Height / 2) '- (myRectHeight / 2)
                    Else
                        '[Note]調整教導框，
                        Dim typ = mytmpPMAlignTool.SearchRegion.GetType()
                        mRect = mytmpPMAlignTool.SearchRegion
                        Select Case typ.FullName
                            Case "Cognex.VisionPro.CogRectangle"
                                Dim tmp As CogRectangle = CType(mRect, CogRectangle)
                                myRectWidth = tmp.Width '教導Pattern大小
                                myRectHeight = tmp.Height '教導Pattern大小
                                myRectCenterX = tmp.CenterX
                                myRectCenterY = tmp.CenterY
                                'Case ""
                        End Select
                    End If
                Case eROIType.Blob
                    If mytmpBlobTool.Region Is Nothing Then
                        '[Note]沒有教導框，新增一個
                        'Dim tmp As CogRectangle = CType(mRect, CogRectangle)
                        myRectWidth = nmcSearchAreaW.Value
                        myRectHeight = nmcSearchAreaH.Value
                        myRectCenterX = (mytmpBlobTool.InputImage.Width / 2) '- (myRectWidth / 2)
                        myRectCenterY = (mytmpBlobTool.InputImage.Height / 2) '- (myRectHeight / 2)
                    Else
                        '[Note]調整教導框，
                        Dim typ = mytmpBlobTool.Region.GetType()
                        mRect = mytmpBlobTool.Region
                        Select Case typ.FullName
                            Case "Cognex.VisionPro.CogRectangle"
                                Dim tmp As CogRectangle = CType(mRect, CogRectangle)
                                myRectWidth = tmp.Width '教導Pattern大小
                                myRectHeight = tmp.Height '教導Pattern大小
                                myRectCenterX = tmp.CenterX
                                myRectCenterY = tmp.CenterY
                                'Case ""
                        End Select
                    End If

            End Select



            mRect.Interactive = True
            mRect.GraphicDOFEnable = CogRectangleDOFConstants.All
            mRect.Color = CogColorConstants.Green
            'If myPMTool.Pattern.TrainImage Is Nothing Then
            mRect.SetCenterWidthHeight(myRectCenterX, myRectCenterY, myRectWidth, myRectHeight)

            CogDisplay1.InteractiveGraphics.Clear()
            CogDisplay1.InteractiveGraphics.Add(mRect, "Rect", True)

            nmcSearchAreaW.Value = myRectWidth
            nmcSearchAreaH.Value = myRectHeight


            AddHandler mRect.Dragging, AddressOf mRect_Dragging
            AddHandler mRect.DraggingStopped, AddressOf mRect_DraggingStopped
        End If

    End Sub

    Private Sub nmcSearchAreaW_ValueChanged(sender As Object, e As EventArgs) Handles nmcSearchAreaW.ValueChanged
        Select Case myROIType
            Case eROIType.PMAlign
                If Not mytmpPMAlignTool Is Nothing Then
                    If Not mytmpPMAlignTool.InputImage Is Nothing Then
                        If nmcSearchAreaW.Value > mytmpPMAlignTool.InputImage.Width Then
                            nmcSearchAreaW.Value = mytmpPMAlignTool.InputImage.Width
                        End If
                        mRect.Width = nmcSearchAreaW.Value
                        mRect.X = (mytmpPMAlignTool.InputImage.Width / 2) - (mRect.Width / 2)
                    End If
                End If
            Case eROIType.Blob
                If Not mytmpBlobTool Is Nothing Then
                    If Not mytmpBlobTool.InputImage Is Nothing Then
                        If nmcSearchAreaW.Value > mytmpBlobTool.InputImage.Width Then
                            nmcSearchAreaW.Value = mytmpBlobTool.InputImage.Width
                        End If
                        mRect.Width = nmcSearchAreaW.Value
                        mRect.X = (mytmpBlobTool.InputImage.Width / 2) - (mRect.Width / 2)
                    End If
                End If
        End Select

    End Sub

    Private Sub nmcSearchAreaH_ValueChanged(sender As Object, e As EventArgs) Handles nmcSearchAreaH.ValueChanged
        Select Case myROIType
            Case eROIType.PMAlign
                If Not mytmpPMAlignTool Is Nothing Then
                    If Not mytmpPMAlignTool.InputImage Is Nothing Then
                        If nmcSearchAreaH.Value > mytmpPMAlignTool.InputImage.Height Then
                            nmcSearchAreaH.Value = mytmpPMAlignTool.InputImage.Height
                        End If
                        mRect.Height = nmcSearchAreaH.Value
                        mRect.Y = (mytmpPMAlignTool.InputImage.Height / 2) - (mRect.Height / 2)
                    End If
                End If
            Case eROIType.Blob
                If Not mytmpBlobTool Is Nothing Then
                    If Not mytmpBlobTool.InputImage Is Nothing Then
                        If nmcSearchAreaH.Value > mytmpBlobTool.InputImage.Height Then
                            nmcSearchAreaH.Value = mytmpBlobTool.InputImage.Height
                        End If
                        mRect.Height = nmcSearchAreaH.Value
                        mRect.Y = (mytmpBlobTool.InputImage.Height / 2) - (mRect.Height / 2)
                    End If
                End If
        End Select

    End Sub
End Class