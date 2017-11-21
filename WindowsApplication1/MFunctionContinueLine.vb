Imports ProjectCore
Imports ProjectRecipe
Imports ProjectConveyor
Imports ProjectTriggerBoard

''' <summary>連續線模式</summary>
''' <remarks></remarks>
Public Module MFunctionContinueLine

    ''' <summary>求兩點間距</summary>
    ''' <param name="posX">第一點X</param>
    ''' <param name="posY">第一點Y</param>
    ''' <param name="posX2">第二點X</param>
    ''' <param name="posY2">第二點Y</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDistance(ByVal posX As Decimal, ByVal posY As Decimal, ByVal posX2 As Decimal, ByVal posY2 As Decimal) As Decimal
        Return Convert.ToDecimal(Math.Sqrt(Convert.ToDecimal(Math.Pow(posX - posX2, 2)) + Convert.ToDecimal(Math.Pow(posY - posY2, 2))))
    End Function
    ''' <summary>求兩點夾角</summary>
    ''' <param name="posX"></param>
    ''' <param name="posY"></param>
    ''' <param name="posX2"></param>
    ''' <param name="posY2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAngle(ByVal posX As Decimal, ByVal posY As Decimal, ByVal posX2 As Decimal, ByVal posY2 As Decimal) As Decimal
        If posY2 = posY Then
            If posX2 > posX Then '
                Return 0
            Else
                Return 180
            End If

        End If
        If posX2 = posX Then
            If posY2 > posY Then
                Return -90
            Else
                Return 90
            End If
        End If
        Return Convert.ToDecimal(Convert.ToDecimal(Math.Atan2(posY2 - posY, posX2 - posX)) * 180) / Math.PI
    End Function


    Public Function GetAngleJeffTest(ByVal posX As Decimal, ByVal posY As Decimal, ByVal posX2 As Decimal, ByVal posY2 As Decimal) As Decimal
        Return Convert.ToDecimal(Convert.ToDecimal(Math.Atan2(posY2 - posY, posX2 - posX)) * 180) / Math.PI
    End Function

#Region "Fenix 2015/02/13 Add Arc Calculate"""
    ''' <summary>
    ''' 計算圓弧或圓的圓心
    ''' </summary>
    ''' <param name="StartPos"></param>
    ''' <param name="EndPos"></param>
    ''' <param name="Degree"></param>
    ''' <param name="Center"></param>
    ''' <param name="dir"></param>
    ''' <remarks></remarks>
    Public Sub CenterCalculate(ByVal StartPos() As Decimal, ByVal EndPos() As Decimal, ByVal Degree As Decimal, ByRef Center() As Decimal, ByVal dir As Integer)
        Dim Length As Decimal
        Dim bterm As Decimal
        Dim Cterm As Decimal
        Dim vector1 As Decimal
        Dim vector2 As Decimal
        Dim CenterPos(2, 2) As Decimal
        Dim returnNum As Integer

        If Degree > 360 Then Degree = Degree Mod 360
        Dim DisPos(1) As Decimal
        DisPos(0) = StartPos(0) - EndPos(0)
        DisPos(1) = StartPos(1) - EndPos(1)
        Dim squareDis(1) As Decimal
        squareDis(0) = DisPos(0) * DisPos(0)
        squareDis(1) = DisPos(1) * DisPos(1)

        Length = Math.Sqrt(squareDis(0) + squareDis(1))

        bterm = -(StartPos(1) + EndPos(1))
        Dim originCValue As Decimal = squareDis(0) * squareDis(0) / 4 / Length / Length +
                squareDis(0) * (EndPos(1) * EndPos(1) + StartPos(1) * StartPos(1)) / 2 / Length / Length +
                (EndPos(1) * EndPos(1) - StartPos(1) * StartPos(1)) * (EndPos(1) * EndPos(1) - StartPos(1) * StartPos(1)) / 4 / Length / Length -
                squareDis(0) / 2 / (1 - Math.Cos(Degree / 180 * Math.PI))
        Cterm = Math.Round(originCValue, 3)
        Dim originCenterValue As Decimal = (-bterm + Math.Sqrt(Math.Round((bterm * bterm - 4 * Cterm), 2))) / 2
        CenterPos(0, 1) = Math.Round(originCenterValue, 3)

        Dim originCenterValue2 As Decimal = (-bterm - Math.Sqrt(Math.Round((bterm * bterm - 4 * Cterm), 2))) / 2
        CenterPos(1, 1) = Math.Round(originCenterValue2, 3)

        If (DisPos(0)) = 0 Then
            CenterPos(0, 0) = StartPos(0) + Math.Sqrt(Length * Length / 2 / (1 - Math.Cos(Degree / 180 * Math.PI)) - (CenterPos(0, 1) - StartPos(1)) * (CenterPos(0, 1) - StartPos(1)))
            CenterPos(1, 0) = StartPos(0) - Math.Sqrt(Length * Length / 2 / (1 - Math.Cos(Degree / 180 * Math.PI)) - (CenterPos(1, 1) - StartPos(1)) * (CenterPos(0, 1) - StartPos(1)))
        Else
            CenterPos(1, 0) = (StartPos(0) + EndPos(0)) / 2 + (EndPos(1) * EndPos(1) - StartPos(1) * StartPos(1)) / 2 / (EndPos(0) - StartPos(0)) - CenterPos(1, 1) * (EndPos(1) - StartPos(1)) / (EndPos(0) - StartPos(0))
            CenterPos(0, 0) = (StartPos(0) + EndPos(0)) / 2 + (EndPos(1) * EndPos(1) - StartPos(1) * StartPos(1)) / 2 / (EndPos(0) - StartPos(0)) - CenterPos(0, 1) * (EndPos(1) - StartPos(1)) / (EndPos(0) - StartPos(0))
        End If
        vector1 = (EndPos(0) - StartPos(0)) * (CenterPos(0, 1) - StartPos(1)) - ((EndPos(1) - StartPos(1)) * (CenterPos(0, 0) - StartPos(0)))
        vector2 = (EndPos(0) - StartPos(0)) * (CenterPos(1, 1) - StartPos(1)) - ((EndPos(1) - StartPos(1)) * (CenterPos(1, 0) - StartPos(0)))
        If Degree < 180 And dir = 0 Then
            If vector1 <= 0 Then
                returnNum = 0
            ElseIf vector2 < 0 Then
                returnNum = 1
            Else
                returnNum = 0
            End If
        ElseIf Degree < 180 And dir = 1 Then
            If vector1 >= 0 Then
                returnNum = 0
            ElseIf vector2 > 0 Then
                returnNum = 1
            Else
                returnNum = 0
            End If
        ElseIf Degree >= 180 And dir = 0 Then
            If vector1 >= 0 Then
                returnNum = 0
            ElseIf vector2 > 0 Then
                returnNum = 1
            Else
                returnNum = 0
            End If
        ElseIf Degree >= 180 And dir = 1 Then
            If vector1 <= 0 Then
                returnNum = 0
            ElseIf vector2 < 0 Then
                returnNum = 1
            Else
                returnNum = 0
            End If
        Else
            returnNum = 2
        End If
        Center(0) = CenterPos(returnNum, 0)
        Center(1) = CenterPos(returnNum, 1)
    End Sub
#End Region

End Module