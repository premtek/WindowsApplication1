''' <summary>數學運算類別</summary>
''' <remarks></remarks>
Public Class CMath

    ''' <summary>[Sin]</summary>
    ''' <param name="Degree"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Sin(ByVal degree As Decimal) As Decimal

        Dim mA As Decimal
        Dim mSin As Decimal

        mA = degree Mod 90

        If mA = 0 Then
            Select Case degree
                Case 0
                    mSin = 0
                Case 90
                    mSin = 1
                Case 180
                    mSin = 0
                Case 270
                    mSin = -1
                Case 360
                    mSin = 0
                Case -90
                    mSin = -1
                Case -180
                    mSin = 0
                Case -270
                    mSin = 1
                Case -360
                    mSin = 0
            End Select
        Else
            mSin = CDec(Math.Sin(degree * Math.PI / 180))
        End If
        Return mSin
    End Function

    ''' <summary>[Cos]</summary>
    ''' <param name="Degree"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Cos(ByVal degree As Decimal) As Decimal

        Dim mA As Decimal
        Dim mCos As Decimal
        mA = degree Mod 90

        If mA = 0 Then
            Select Case degree
                Case 0
                    mCos = 1
                Case 90
                    mCos = 0
                Case 180
                    mCos = -1
                Case 270
                    mCos = 0
                Case 360
                    mCos = 1
                Case -90
                    mCos = 0
                Case -180
                    mCos = -1
                Case -270
                    mCos = 0
                Case -360
                    mCos = 1
            End Select
        Else
            mCos = CDec(Math.Cos(degree * Math.PI / 180))
        End If
        Return mCos

    End Function

    ''' <summary>純座標平移</summary>
    ''' <param name="inPosX"></param>
    ''' <param name="inposY"></param>
    ''' <param name="offsetX"></param>
    ''' <param name="offsetY"></param>
    ''' <param name="outPosX"></param>
    ''' <param name="outPosY"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Translation(ByVal inPosX As Decimal, ByVal inposY As Decimal, ByVal offsetX As Decimal, ByVal offsetY As Decimal, ByRef outPosX As Decimal, ByRef outPosY As Decimal) As Boolean
        outPosX = inPosX + offsetX
        outPosY = inposY + offsetY
        Return True
    End Function

    ''' <summary>純座標旋轉</summary>
    ''' <param name="inPosX"></param>
    ''' <param name="inposY"></param>
    ''' <param name="angle">度</param>
    ''' <param name="outPosX"></param>
    ''' <param name="outPosY"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Rotation(ByVal inPosX As Decimal, ByVal inposY As Decimal, ByVal angle As Decimal, ByRef outPosX As Decimal, ByRef outPosY As Decimal) As Boolean
        Dim mCos As Decimal
        Dim mSin As Decimal
        mCos = Cos(-1 * angle)
        mSin = Sin(-1 * angle)
        outPosX = Val(Format(inPosX * mCos - inposY * mSin, "0.000"))
        outPosY = Val(Format(inPosX * mSin + inposY * mCos, "0.000"))
        Return True
    End Function

    ''' <summary>[推算旋轉偏移後的座標]</summary>
    ''' <param name="IsRummyRun"></param>
    ''' <param name="Angle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function TransformRotation(ByVal isRummyRun As Boolean, ByVal angle As Decimal, ByVal cenPosX As Decimal, ByVal cenPosY As Decimal, ByVal inPosX As Decimal, ByVal inPosY As Decimal, ByRef outPosX As Decimal, ByRef outPosY As Decimal) As Boolean

        Dim mCos As Decimal
        Dim mSin As Decimal

        If isRummyRun = True Then
            angle = 0
        End If
        mCos = Cos(-1 * angle)
        mSin = Sin(-1 * angle)
        outPosX = CDec(Format((((inPosX - cenPosX) * mCos) - ((inPosY - cenPosY) * mSin)) + cenPosX, "0.000"))
        outPosY = CDec(Format((((inPosX - cenPosX) * mSin) + ((inPosY - cenPosY) * mCos)) + cenPosY, "0.000"))
        Return True

    End Function

    ''' <summary>取得2維向量長度</summary>
    ''' <param name="vectorX"></param>
    ''' <param name="vectorY"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Get2Norm(ByVal vectorX As Decimal, ByVal vectorY As Decimal, ByVal result As Decimal) As Boolean
        Try
            result = Math.Sqrt(vectorX * vectorX + vectorY * vectorY)
            Return True
        Catch ex As Exception
            gSyslog.Save(ex.Message & " " & ex.StackTrace, , eMessageLevel.Error)
            Return False
        End Try
    End Function
    ''' <summary>取得3維向量長度</summary>
    ''' <param name="vectorX"></param>
    ''' <param name="vectorY"></param>
    ''' <param name="vectorZ"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Get3Norm(ByVal vectorX As Decimal, ByVal vectorY As Decimal, ByVal vectorZ As Decimal, ByVal result As Decimal) As Boolean
        Try
            result = Math.Sqrt(vectorX * vectorX + vectorY * vectorY + vectorZ * vectorZ)
            Return True
        Catch ex As Exception
            gSyslog.Save(ex.Message & " " & ex.StackTrace, , eMessageLevel.Error)
            Return False
        End Try
    End Function

    ''' <summary>向量內積</summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DotProduct(ByVal aX As Decimal, ByVal aY As Decimal, ByVal bX As Decimal, ByVal bY As Decimal, ByVal result As Decimal) As Boolean
        Try
            result = aX * bX + aY * bY
            Return True
        Catch ex As Exception
            gSyslog.Save(ex.Message & " " & ex.StackTrace, , eMessageLevel.Error)
            Return False
        End Try
    End Function


    ''' <summary>
    ''' [二點定位計算式]
    ''' </summary>
    ''' <param name="Center1X">[第一點定位點X]</param>
    ''' <param name="Center1Y">[第一點定位點Y]</param>
    ''' <param name="Center2X">[第二點定位點X]</param>
    ''' <param name="Center2Y">[第二點定位點Y]</param>
    ''' <param name="Offset1X">[第一點定位點偏移量X]</param>
    ''' <param name="Offset1Y">[第一點定位點偏移量Y]</param>
    ''' <param name="Offset2X">[第二點定位點偏移量X]</param>
    ''' <param name="Offset2Y">[第二點定位點偏移量Y]</param>
    ''' <param name="OffsetX">[計算後的偏移量X]</param>
    ''' <param name="OffsetY">[計算後的偏移量Y]</param>
    ''' <param name="Angle">[計算後的旋轉量]</param>
    ''' <remarks></remarks>
    Public Shared Function TwoPointsCalibrationCalculate(ByVal Center1X As Decimal, ByVal Center1Y As Decimal, ByVal Center2X As Decimal, ByVal Center2Y As Decimal, ByVal Offset1X As Decimal, ByVal Offset1Y As Decimal, ByVal Offset2X As Decimal, ByVal Offset2Y As Decimal, ByRef OffsetX As Decimal, ByRef OffsetY As Decimal, ByRef Angle As Decimal) As Boolean
        Dim tempAngle As Decimal
        Dim crossData As Decimal
        Dim dotValve As Decimal
        Dim OldVectorX As Decimal
        Dim OldVectorY As Decimal
        Dim NewVectorX As Decimal
        Dim NewVectorY As Decimal
        Dim str As String

        Try

            OldVectorX = Center2X - Center1X
            OldVectorY = Center2Y - Center1Y
            NewVectorX = (Center2X - Offset2X) - (Center1X - Offset1X)
            NewVectorY = (Center2Y - Offset2Y) - (Center1Y - Offset1Y)
            crossData = OldVectorX * NewVectorY - OldVectorY * NewVectorX
            dotValve = OldVectorX * NewVectorX + OldVectorY * NewVectorY
            Dim Distance1 As Decimal = Math.Sqrt(OldVectorX * OldVectorX + OldVectorY * OldVectorY)
            Dim Distance2 As Decimal = Math.Sqrt(NewVectorX * NewVectorX + NewVectorY * NewVectorY)

            Dim value As Decimal = dotValve / Distance1 / Distance2
            If value > 1 Then value = 1 '小數點除數過範圍保護
            If value < -1 Then value = -1
            tempAngle = Math.Acos(value)
            If crossData >= 0 Then
                Angle = tempAngle * 180 / Math.PI
            Else
                Angle = -tempAngle * 180 / Math.PI
            End If
            OffsetX = Offset1X
            OffsetY = Offset1Y
            str = "1st X:" & Center1X.ToString("0.0000") & " Y:" & Center2X.ToString("0.0000") & " Offset X:" & Offset1X.ToString("0.0000") & " Y:" & Offset1Y.ToString("0.0000") &
                " 2rd X:" & Center2X.ToString("0.0000") & " Y:" & Center2Y.ToString("0.0000") & " Offset X:" & Offset2X.ToString("0.0000") & " Y:" & Offset2Y.ToString("0.0000") &
                " angle:" & Angle.ToString("0.0000")

            gSyslog.Save("TwoPointsCalibrationCalculate:" & str)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>取得向量夾角</summary>
    ''' <param name="v1P1X">第一組向量點1X</param>
    ''' <param name="v1P1Y">第一組向量點1Y</param>
    ''' <param name="v1P2X">第一組向量點2X</param>
    ''' <param name="v1P2Y">第一組向量點2Y</param>
    ''' <param name="v2P1X">第二組向量點1X</param>
    ''' <param name="v2P1Y">第二組向量點1Y</param>
    ''' <param name="v2P2X">第二組向量點2X</param>
    ''' <param name="v2P2Y">第二組向量點2Y</param>
    ''' <param name="angle">求得夾角</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetVectorAngle(ByVal v1P1X As Decimal, ByVal v1P1Y As Decimal, ByVal v1P2X As Decimal, ByVal v1P2Y As Decimal, ByVal v2P1X As Decimal, ByVal v2P1Y As Decimal, ByVal v2P2X As Decimal, ByVal v2P2Y As Decimal, ByRef angle As Decimal) As Boolean

        Dim mV1X As Decimal = v1P2X - v1P1X '第一組向量X
        Dim mV1Y As Decimal = v1P2Y - v1P1Y '第一組向量Y
        Dim mV2X As Decimal = v2P2X - v2P1X '第二組向量X
        Dim mV2Y As Decimal = v2P2Y - v2P1Y '第二組向量Y

        Dim mV1Distance As Decimal = Math.Sqrt(mV1X ^ 2 + mV1Y ^ 2) '第一組向量長度
        Dim mV2Distance As Decimal = Math.Sqrt(mV2X ^ 2 + mV2Y ^ 2) '第二組向量長度
        Dim DotValue As Decimal = mV1X * mV2X - mV1Y * mV2Y
        Dim CrossValue As Decimal = mV1X * mV2Y - mV1Y * mV2X
        Dim rad As Decimal = Math.Acos(DotValue / (mV1Distance * mV2Distance))
        If CrossValue > 0 Then
            angle = -rad * 180 / Math.PI
        Else
            angle = rad * 180 / Math.PI
        End If
        Return True
    End Function

    ''' <summary>
    ''' [單點旋轉計算式]
    ''' </summary>
    ''' <param name="CenPosX">[旋轉中心X]</param>
    ''' <param name="CenPosY">[旋轉中心Y]</param>
    ''' <param name="RotaPosX">[定位點X]</param>
    ''' <param name="RotaPosY">[定位點Y]</param>
    ''' <param name="Angle">[旋轉角]</param>
    ''' <param name="OutputX">[計算後定位點X]</param>
    ''' <param name="OutputY">[計算後定位點X]</param>
    ''' <remarks></remarks>
    Public Shared Function OnePointsCalibrationCalculate(ByVal CenPosX As Decimal, ByVal CenPosY As Decimal, ByVal RotaPosX As Decimal, ByVal RotaPosY As Decimal, ByVal Angle As Decimal, ByRef OutputX As Decimal, ByRef OutputY As Decimal) As Boolean
        Dim CosVal As Decimal
        Dim SinVal As Decimal

        CosVal = CDec(Cos(Angle / 180 * Math.PI))
        SinVal = CDec(Sin(Angle / 180 * Math.PI))

        OutputX = CDec(Format((RotaPosX - CenPosX) * CosVal - (RotaPosY - CenPosY) * SinVal, "0.000"))
        OutputY = CDec(Format((RotaPosX - CenPosX) * SinVal + (RotaPosY - CenPosY) * CosVal, "0.000"))
        Return True
    End Function

    ''' <summary>輸入三點對應座標,求解轉換矩陣</summary>
    ''' <param name="Xi1">輸入CCD座標(Pixel)</param>
    ''' <param name="Xi2">輸入CCD座標(Pixel)</param>
    ''' <param name="Xi3">輸入CCD座標(Pixel)</param>
    ''' <param name="Yi1">輸入CCD座標(Pixel)</param>
    ''' <param name="Yi2">輸入CCD座標(Pixel)</param>
    ''' <param name="Yi3">輸入CCD座標(Pixel)</param>
    ''' <param name="Xo1">輸出馬達座標(mm)</param>
    ''' <param name="Xo2">輸出馬達座標(mm)</param>
    ''' <param name="Xo3">輸出馬達座標(mm)</param>
    ''' <param name="Yo1">輸出馬達座標(mm)</param>
    ''' <param name="Yo2">輸出馬達座標(mm)</param>
    ''' <param name="Yo3">輸出馬達座標(mm)</param>
    ''' <param name="A11">轉換矩陣參數</param>
    ''' <param name="A12">轉換矩陣參數</param>
    ''' <param name="A21">轉換矩陣參數</param>
    ''' <param name="A22">轉換矩陣參數</param>
    ''' <param name="B11">轉換矩陣參數</param>
    ''' <param name="B21">轉換矩陣參數</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Point3CalcTranslation(ByVal Xi1 As Double, ByVal Xi2 As Double, ByVal Xi3 As Double, ByVal Yi1 As Double, ByVal Yi2 As Double, ByVal Yi3 As Double, _
                                          ByVal Xo1 As Double, ByVal Xo2 As Double, ByVal Xo3 As Double, ByVal Yo1 As Double, ByVal Yo2 As Double, ByVal Yo3 As Double, _
                                          ByRef A11 As Double, ByRef A12 As Double, ByRef A21 As Double, ByRef A22 As Double, ByRef B11 As Double, ByRef B21 As Double)
        '矩陣最小平方 求轉移函數
        'X = (A^T A)-1 A^T Y
        Dim ATA_a11 As Double = Xi1 ^ 2 + Xi2 ^ 2 + Xi3 ^ 2
        Dim ATA_a12 As Double = Xi1 * Yi1 + Xi2 * Yi2 + Xi3 * Yi3
        Dim ATA_a13 As Double = Xi1 + Xi2 + Xi3
        Dim ATA_a21 As Double = Xi1 * Yi1 + Xi2 * Yi2 + Xi3 * Yi3
        Dim ATA_a22 As Double = Yi1 ^ 2 + Yi2 ^ 2 + Yi3 ^ 2
        Dim ATA_a23 As Double = Yi1 + Yi2 + Yi3
        Dim ATA_a31 As Double = Xi1 + Xi2 + Xi3
        Dim ATA_a32 As Double = Yi1 + Yi2 + Yi3
        Dim ATA_a33 As Double = 3
        Dim detA As Double = ATA_a11 * ATA_a22 * ATA_a33 + ATA_a12 * ATA_a23 * ATA_a31 + ATA_a13 * ATA_a21 * ATA_a32 - ATA_a13 * ATA_a22 * ATA_a31 - ATA_a11 * ATA_a23 * ATA_a32 - ATA_a12 * ATA_a21 * ATA_a33

        If detA = 0 Then '無行列式值, 無法求解
            Return False
        End If

        '伴隨矩陣
        Dim Adj_a11 As Double = ATA_a22 * ATA_a33 - ATA_a23 * ATA_a32
        Dim Adj_a12 As Double = ATA_a13 * ATA_a32 - ATA_a12 * ATA_a33
        Dim Adj_a13 As Double = ATA_a12 * ATA_a23 - ATA_a13 * ATA_a22
        Dim Adj_a21 As Double = ATA_a23 * ATA_a31 - ATA_a21 * ATA_a33
        Dim Adj_a22 As Double = ATA_a11 * ATA_a33 - ATA_a13 * ATA_a31
        Dim Adj_a23 As Double = ATA_a13 * ATA_a21 - ATA_a11 * ATA_a23
        Dim Adj_a31 As Double = ATA_a21 * ATA_a32 - ATA_a22 * ATA_a31
        Dim Adj_a32 As Double = ATA_a12 * ATA_a31 - ATA_a11 * ATA_a32
        Dim Adj_a33 As Double = ATA_a11 * ATA_a22 - ATA_a12 * ATA_a21

        'ATA的反矩陣 (A^T A) -1
        Dim InvA_a11 As Double = Adj_a11 / detA
        Dim InvA_a12 As Double = Adj_a12 / detA
        Dim InvA_a13 As Double = Adj_a13 / detA
        Dim InvA_a21 As Double = Adj_a21 / detA
        Dim InvA_a22 As Double = Adj_a22 / detA
        Dim InvA_a23 As Double = Adj_a23 / detA
        Dim InvA_a31 As Double = Adj_a31 / detA
        Dim InvA_a32 As Double = Adj_a32 / detA
        Dim InvA_a33 As Double = Adj_a33 / detA

        'A^T Y
        Dim item1 As Double = Xi1 * Xo1 + Xi2 * Xo2 + Xi3 * Xo3
        Dim item2 As Double = Yi1 * Xo1 + Yi2 * Xo2 + Yi3 * Xo3
        Dim item3 As Double = Xo1 + Xo2 + Xo3
        A11 = InvA_a11 * item1 + InvA_a12 * item2 + InvA_a13 * item3
        A12 = InvA_a21 * item1 + InvA_a22 * item2 + InvA_a23 * item3
        B11 = InvA_a31 * item1 + InvA_a32 * item2 + InvA_a33 * item3

        A11 = Val(A11.ToString("#.00000"))
        A12 = Val(A12.ToString("#.00000"))
        B11 = Val(B11.ToString("#.00000"))

        'A^T Y
        Dim item4 As Double = Xi1 * Yo1 + Xi2 * Yo2 + Xi3 * Yo3
        Dim item5 As Double = Yi1 * Yo1 + Yi2 * Yo2 + Yi3 * Yo3
        Dim item6 As Double = Yo1 + Yo2 + Yo3
        A21 = InvA_a11 * item4 + InvA_a12 * item5 + InvA_a13 * item6
        A22 = InvA_a21 * item4 + InvA_a22 * item5 + InvA_a23 * item6
        B21 = InvA_a31 * item4 + InvA_a32 * item5 + InvA_a33 * item6

        A21 = Val(A21.ToString("#.00000"))
        A22 = Val(A22.ToString("#.00000"))
        B21 = Val(B21.ToString("#.00000"))

        Return True
    End Function

    ''' <summary>
    ''' 16進位轉10進位
    ''' </summary>
    ''' <param name="Hex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function HEX_to_DEC(ByVal Hex As String) As Integer
        Return Convert.ToInt32("0X" + Hex, 16)
    End Function

    Public Shared Function GetCircleby3Point(ByVal x As CPoint, ByVal y As CPoint, ByVal z As CPoint) As Circle
        Dim C As Circle
        Dim M As CPoint
        Dim a As Double
        M = GetCenterby3Point(x, y, z)
        a = GetAngleby3Point(M, x, z, GetClockwiseby3Point(x, y, z))
        C = New Circle(M.PointX, M.PointY, GetClockwiseby3Point(x, y, z), a)

        Return C
    End Function

    Public Shared Function GetAngleby3Point(ByVal center As CPoint, ByVal start As CPoint, ByVal final As CPoint, ByVal clockwise As Boolean) As Double
        Dim angle As Double
        Dim cos As Double
        Dim V1, V2 As CPoint 'V1= vectorAB  V2= vectorAC
        Dim AngleLargeThanPi As Integer


        V1 = New CPoint(start.PointX - center.PointX, start.PointY - center.PointY)
        V2 = New CPoint(final.PointX - center.PointX, final.PointY - center.PointY)

        'Console.WriteLine("V1.X: " & V1.PointX)
        'Console.WriteLine("V1.Y: " & V1.PointY)
        'Console.WriteLine("V2.X: " & V2.PointX)
        'Console.WriteLine("V2.Y: " & V2.PointY)
        cos = (V1.PointX * V2.PointX + V1.PointY * V2.PointY) / (Math.Sqrt((V1.PointX) ^ 2 + (V1.PointY) ^ 2) * Math.Sqrt((V2.PointX) ^ 2 + (V2.PointY) ^ 2))
        'Console.WriteLine("cos: " & cos & "內績:  " & (V1.PointX * V2.PointX + V1.PointY * V2.PointY))
        If cos < -1 Then
            cos = -1
        ElseIf cos > 1 Then
            cos = 1
        End If

        AngleLargeThanPi = V1.PointX * V2.PointY - V2.PointX * V1.PointY 'point1.getX() * point2.getY() - point2.getX()* point1.getY();
        'Console.WriteLine("AngleLargeThanPi: " & AngleLargeThanPi)
        If AngleLargeThanPi > 0 Then
            angle = 180.0 * Math.Acos(cos) / Math.PI
        Else
            angle = 360.0 - Math.Acos(cos) * 180.0 / Math.PI '360-acos(cos_theta)*180/Pi;
        End If

        If clockwise Then
            angle = 360.0 - angle
        End If

        'Console.WriteLine("angle: " & angle)



        Return angle
    End Function

    Public Shared Function GetCenterby3Point(ByVal x As CPoint, ByVal y As CPoint, ByVal z As CPoint) As CPoint
        Dim VerticalLine1, VerticalLine2 As Line
        Dim Center As New CPoint

        'Console.WriteLine("設一圓通過A(" & x.PointX & "," & x.PointY & "),B(" & y.PointX & "," & y.PointY & "),C(" & z.PointX & "," & z.PointY & ")三點，求圓心")
        VerticalLine1 = GetVerticalLineby2Point(x, y)
        'Console.WriteLine("GetVerticalLine1 by2Point:" & VerticalLine1.Line_M & "  " & VerticalLine1.Line_B)
        VerticalLine2 = GetVerticalLineby2Point(x, z)
        'Console.WriteLine("GetVerticalLine2  by2Point:" & VerticalLine2.Line_M & "  " & VerticalLine2.Line_B)

        If VerticalLine1.Line_M = VerticalLine2.Line_M Then
            'Console.WriteLine("平行線沒有交點")
        Else
            ' y= m1x+b1  y= m2x+b2 => (m1-m2)x= b2-b1 => x=(b2-b1)/(m1-m2

            If (VerticalLine1.Line_M = Double.MaxValue And VerticalLine2.Line_M = 0) Then
                Center.PointX = VerticalLine1.Line_B
                Center.PointY = VerticalLine2.Line_B
                'Console.WriteLine("中心1(" & Center.PointX & "," & Center.PointY & ")")
            ElseIf (VerticalLine1.Line_M = 0 And VerticalLine2.Line_M = Double.MaxValue) Then
                Center.PointX = VerticalLine2.Line_B
                Center.PointY = VerticalLine1.Line_B
                'Console.WriteLine("中心2(" & Center.PointX & "," & Center.PointY & ")")
            ElseIf (VerticalLine1.Line_M = 0) Then
                Center.PointY = VerticalLine1.Line_B
                Center.PointX = (Center.PointY - VerticalLine2.Line_B) / VerticalLine2.Line_M
                'Console.WriteLine("中心3(" & Center.PointX & "," & Center.PointY & ")")
            ElseIf (VerticalLine2.Line_M = 0) Then
                Center.PointY = VerticalLine2.Line_B
                Center.PointX = (Center.PointY - VerticalLine1.Line_B) / VerticalLine1.Line_M
                'Console.WriteLine("中心4(" & Center.PointX & "," & Center.PointY & ")")
            ElseIf (VerticalLine1.Line_M = Double.MaxValue) Then
                Center.PointX = VerticalLine1.Line_B
                Center.PointY = VerticalLine2.Line_M * Center.PointX + VerticalLine2.Line_B 'y=m1x+b1
                'Console.WriteLine("中心5(" & Center.PointX & "," & Center.PointY & ")")
            ElseIf (VerticalLine2.Line_M = Double.MaxValue) Then
                Center.PointX = VerticalLine2.Line_B
                Center.PointY = VerticalLine1.Line_M * Center.PointX + VerticalLine1.Line_B 'y=m1x+b1
                'Console.WriteLine("中心6(" & Center.PointX & "," & Center.PointY & ")")
            Else
                Center.PointX = (VerticalLine2.Line_B - VerticalLine1.Line_B) / (VerticalLine1.Line_M - VerticalLine2.Line_M)
                Center.PointY = VerticalLine1.Line_M * Center.PointX + VerticalLine1.Line_B 'y=m1x+b1
                'Console.WriteLine("中心7(" & Center.PointX & "," & Center.PointY & ")")

            End If

            'Console.WriteLine("圓的中心點為(" & Center.PointX & "," & Center.PointY & ")")
        End If

        Return Center
    End Function




    Public Shared Function GetVerticalLineby2Point(ByVal x As CPoint, ByVal y As CPoint)
        Dim VerticalLine As New Line
        Dim m, b As Double   'y=mx+b
        If (x.PointX - y.PointX = 0) Then
            m = Double.MaxValue '∞ 
            'Console.WriteLine("垂直")
        ElseIf (x.PointY - y.PointY = 0) Then
            m = 0
            'Console.WriteLine("水平")
        Else
            m = (x.PointY - y.PointY) / (x.PointX - y.PointX)
        End If
        b = x.PointY - m * x.PointX
        'Console.WriteLine("Y= " & m & " X+" & b)

        'VerticalLine
        Dim Vertical_m, Vertical_b As Double
        If m = 0 Then
            Vertical_m = Double.MaxValue '∞ 
        ElseIf m = Double.MaxValue Then
            Vertical_m = 0
        Else

            Vertical_m = -1 / m
        End If


        Dim MiddlePoint As New CPoint((x.PointX + y.PointX) / 2, (x.PointY + y.PointY) / 2)
        'Console.WriteLine("MiddlePoint: " & MiddlePoint.PointX & "  " & MiddlePoint.PointY)
        If Vertical_m = 0 Then
            Vertical_b = MiddlePoint.PointY
            'Console.WriteLine("垂直線是水平  中心X: " & Vertical_b)
        ElseIf Vertical_m = Double.MaxValue Then
            Vertical_b = MiddlePoint.PointX
            'Console.WriteLine("垂直線是垂直  中心Y: " & Vertical_b)
        Else
            Vertical_b = MiddlePoint.PointY - Vertical_m * MiddlePoint.PointX
        End If

        'Console.WriteLine("VerticalLine: Y=" & Vertical_m & "X + " & Vertical_b)


        VerticalLine.Line_M = Vertical_m
        VerticalLine.Line_B = Vertical_b

        Return VerticalLine

    End Function

    Public Shared Function GetClockwiseby3Point(ByVal x As CPoint, ByVal y As CPoint, ByVal z As CPoint) As Boolean
        Dim Clockwise As Boolean
        Dim Vector_a, Vector_b As CPoint
        Dim Cross, cos As Double
        Vector_a = New CPoint(y.PointX - x.PointX, y.PointY - x.PointY)
        Vector_b = New CPoint(z.PointX - x.PointX, z.PointY - x.PointY)
        'Console.WriteLine("Vector_a.X: " & Vector_a.PointX)
        'Console.WriteLine("Vector_a.Y: " & Vector_a.PointY)
        'Console.WriteLine("Vector_b.X: " & Vector_b.PointX)
        'Console.WriteLine("Vector_b.Y: " & Vector_b.PointY)
        If (Math.Sqrt((Vector_a.PointX) ^ 2 + (Vector_a.PointY) ^ 2) * Math.Sqrt((Vector_b.PointX) ^ 2 + (Vector_b.PointY) ^ 2)) > 0 Then
            Cross = ((Vector_a.PointX * Vector_b.PointY) - (Vector_b.PointX * Vector_a.PointY)) / (Math.Sqrt((Vector_a.PointX) ^ 2 + (Vector_a.PointY) ^ 2) * Math.Sqrt((Vector_b.PointX) ^ 2 + (Vector_b.PointY) ^ 2))
            cos = (Vector_a.PointX * Vector_b.PointX + Vector_a.PointY * Vector_b.PointY) / (Math.Sqrt((Vector_a.PointX) ^ 2 + (Vector_a.PointY) ^ 2) * Math.Sqrt((Vector_b.PointX) ^ 2 + (Vector_b.PointY) ^ 2))
        Else
            Cross = 0
            cos = -1
        End If

        'Console.WriteLine("Cross為" & Cross & "  cos: " & cos)
        If Cross > 0 Then
            Clockwise = False
        Else
            Clockwise = True 'Counterclockwise
        End If
        Return Clockwise
    End Function


End Class


Public Class Circle
    Public PointX As Double
    Public PointY As Double
    Public Angle As Double        '20160805
    Public clockwise As Boolean   'True = 順時針
    '20160526
    'Public Enum eArcDirection
    '    ''' <summary>[順時針]</summary>
    '    ''' <remarks></remarks>
    '    CW = 0
    '    ''' <summary>[逆時針]</summary>
    '    ''' <remarks></remarks>
    '    CCW = 1
    'End Enum

    Public Sub New()
        PointX = 0
        PointY = 0
        Angle = 0
        clockwise = 0
    End Sub
    '20160805
    Public Sub New(ByVal x As Double, ByVal y As Double, ByVal c As Boolean, ByVal a As Double)
        PointX = x
        PointY = y
        Angle = a
        clockwise = c

    End Sub
End Class


''' <summary>點斜式</summary>
''' <remarks></remarks>
Public Class Line ' y = mx +b
    ''' <summary>斜率</summary>
    ''' <remarks></remarks>
    Public Line_M As Double
    ''' <summary>截距</summary>
    ''' <remarks></remarks>
    Public Line_B As Double
    Public Sub New()
        Line_M = 0
        Line_B = 0
    End Sub
    Public Sub New(ByVal m As Double, ByVal b As Double)
        Line_M = m
        Line_B = b
    End Sub
End Class


Public Class CPoint
    Public PointX As Double
    Public PointY As Double
    Public Sub New()
        PointX = 0
        PointY = 0
    End Sub
    Public Sub New(ByVal x As Double, ByVal y As Double)
        PointX = x
        PointY = y
    End Sub
End Class

Public Class CPoint3D
    Public PointX As Decimal
    Public PointY As Decimal
    Public PointZ As Decimal
    Public Sub New()
        PointX = 0
        PointY = 0
        PointZ = 0
    End Sub
    Public Sub New(ByVal x As Decimal, ByVal y As Decimal, ByVal z As Decimal)
        PointX = x
        PointY = y
        PointZ = z
    End Sub
End Class