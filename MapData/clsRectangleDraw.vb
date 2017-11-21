Imports System.Windows.Forms
Imports System.Drawing

Public Class clsRectangleDraw
    Dim PicBox As New PictureBox
    Dim bAutoSize As Boolean = True
    Dim Column As Integer
    Dim Row As Integer
    Dim DieWidth, DieHeight As Integer
    Dim Color As Color = Drawing.Color.Black
    Dim myBitmap As Bitmap

    Sub New(ByVal pic As PictureBox)
        PicBox = pic
        AddHandler PicBox.Paint, AddressOf PicBox_Paint
    End Sub

    Public Sub SetDieSize(ByVal width As Integer, ByVal height As Integer)
        DieWidth = width
        DieHeight = height
    End Sub

    Public Sub SetLineColor(ByVal color As Color)
        Me.Color = color
    End Sub

    Public Sub Clean()

        'myBitmap = New Bitmap(CInt(PicBox.Width), CInt(PicBox.Height))
        'Using objGraphic = Graphics.FromImage(myBitmap)
        '    PicBox.Image = myBitmap
        'End Using
        PicBox.Image = Nothing
    End Sub

    Public Sub DrawRectangleArray(ByVal column As Integer, ByVal row As Integer, Optional ByVal autoSize As Boolean = False)
        Dim OriginX, OriginY As Integer
        Dim picWidth, picHeight As Integer

        myBitmap = PicBox.Image
        If myBitmap Is Nothing Then
            myBitmap = New Bitmap(CInt(PicBox.Width), CInt(PicBox.Height))
        End If

        Using objGraphic = Graphics.FromImage(myBitmap)
            If Not column = 0 AndAlso Not row = 0 Then
                OriginX = 30
                OriginY = 30
                If (autoSize) Then
                    picWidth = PicBox.Size.Width - 60
                    picHeight = PicBox.Size.Height - 60
                    DieWidth = picWidth / column
                    DieHeight = picHeight / row
                End If

                Dim pen1 As New Pen(Color, 2)
                objGraphic.DrawRectangle(pen1, OriginX, OriginY, picWidth, picHeight)

                For i As Integer = 0 To column
                    objGraphic.DrawLine(pen1, OriginX + (DieWidth * i), OriginY, OriginX + (DieWidth * i), OriginY + DieHeight * row)
                Next
                For i As Integer = 0 To row
                    objGraphic.DrawLine(pen1, OriginX, OriginY + (DieHeight * i), OriginX + DieWidth * column, OriginY + (DieHeight * i))
                Next
            End If

            PicBox.Image = myBitmap
        End Using
    End Sub

    Public Sub DrawRectangleArray(ByVal x As Integer, ByVal y As Integer, ByVal column As Integer, ByVal row As Integer, Optional ByVal autoSize As Boolean = False)
        Dim OriginX, OriginY As Integer
        Dim picWidth, picHeight As Integer

        myBitmap = PicBox.Image
        If myBitmap Is Nothing Then
            myBitmap = New Bitmap(CInt(PicBox.Width), CInt(PicBox.Height))
        End If
        Using objGraphic = Graphics.FromImage(myBitmap)
            If Not column = 0 AndAlso Not row = 0 Then
                If (autoSize) Then
                    picWidth = PicBox.Size.Width - 60
                    picHeight = PicBox.Size.Height - 60
                    DieWidth = picWidth / column
                    DieHeight = picHeight / row
                End If

                OriginX = 30 + x * DieWidth
                OriginY = 30 + y * DieHeight

                Dim pen1 As New Pen(Color, 2)
                objGraphic.DrawRectangle(pen1, OriginX, OriginY, picWidth, picHeight)

                For i As Integer = 0 To column
                    objGraphic.DrawLine(pen1, OriginX + (DieWidth * i), OriginY, OriginX + (DieWidth * i), OriginY + DieHeight * row)
                Next
                For i As Integer = 0 To row
                    objGraphic.DrawLine(pen1, OriginX, OriginY + (DieHeight * i), OriginX + DieWidth * column, OriginY + (DieHeight * i))
                Next
            End If

            PicBox.Image = myBitmap
        End Using
    End Sub

    Private Sub PicBox_Paint(sender As Object, e As Windows.Forms.PaintEventArgs)

    End Sub

End Class
