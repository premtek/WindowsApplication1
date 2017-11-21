Imports ProjectRecipe
Imports ProjectCore

Module MDispenseRelationship
    ''' <summary>
    ''' [Node內點膠關係的清單]
    ''' </summary>
    ''' <remarks></remarks>
    Public DispenseRelationshipList(enmStage.Max) As List(Of SNodePatterm)

    Public Structure SNodePatterm
        ''' <summary>[Node名稱]</summary>
        ''' <remarks></remarks>
        Public NodeName As String
        ''' <summary>[Pattern名稱]</summary>
        ''' <remarks></remarks>
        Public PatternName As String
        ''' <summary>[哪一層的Node]</summary>
        ''' <remarks></remarks>
        Public NodeLevel As Integer
    End Structure

    ''' <summary>[將Node與Patterm串接起來]</summary>
    ''' <param name="Recipe"></param>
    ''' <param name="StageNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DispenseRecipeNodeSort(ByVal Recipe As CRecipe, ByVal StageNo As enmStage) As Boolean

        Dim mI As Integer
        Dim mNodeName As String
        Dim mPatternName As String
        Dim mNodePatterm As SNodePatterm
        Dim mNodeLevel As Integer
        Dim mJ As Integer
        Dim mK As Integer
        Dim mIsNeedDispensing As Boolean

        mNodeLevel = 1
        DispenseRelationshipList(StageNo) = New List(Of SNodePatterm)
        For mI = 0 To Recipe.DispenseTraversal(StageNo).Count - 1
            '[說明]:找出對應的Node & Patterm
            mNodeName = Recipe.DispenseTraversal(StageNo)(mI)
            mPatternName = Recipe.Node(StageNo)(mNodeName).PatternName
            Call GetNodeLevel(mNodeName, mNodeLevel)

            mIsNeedDispensing = False
            With mNodePatterm
                .NodeName = mNodeName
                .PatternName = mPatternName
                .NodeLevel = mNodeLevel
                '[Note]:檢查此Pattern是否有點膠動作，若有才加入
                '                                      沒有表示PassDispensing
                If Recipe.Pattern(.PatternName).Round.Count > 0 Then
                    For mJ = 0 To Recipe.Pattern(.PatternName).Round.Count - 1
                        If Recipe.Pattern(.PatternName).Round(mJ).StepCount > 0 Then
                            For mK = 0 To Recipe.Pattern(.PatternName).Round(mJ).StepCount - 1
                                Select Case Recipe.Pattern(.PatternName).Round(mJ).CStep(mK).StepType
                                    Case eStepFunctionType.Arc2D, eStepFunctionType.Arc3D, eStepFunctionType.Circle2D, eStepFunctionType.Circle3D, eStepFunctionType.Dots3D, eStepFunctionType.Line3D
                                        mIsNeedDispensing = True
                                        Exit For
                                End Select
                            Next
                        End If
                        If mIsNeedDispensing = True Then
                            Exit For
                        End If
                    Next
                    If mIsNeedDispensing = True Then
                        DispenseRelationshipList(StageNo).Add(mNodePatterm)
                        'Debug.Print("NodeName,PatternName,NodeLevel: " & mNodePatterm.NodeName & " , " & mNodePatterm.PatternName & " , " & mNodePatterm.NodeLevel)
                    End If
                End If
            End With
        Next

        Return True

    End Function



    ''' <summary>[將最大的Round Count取出(StageNo1~StageMax)]</summary>
    ''' <param name="Recipe"></param>
    ''' <param name="maxRound"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMaxRoundCount(ByVal Recipe As CRecipe, ByRef maxRound As Integer) As Boolean

        Dim mI As Integer
        Dim mNodeName As String
        Dim mPatternName As String
        Dim mStageNo As Integer
        Dim mMaxRound As Integer

        mMaxRound = -1
        For mStageNo = enmStage.No1 To gSSystemParameter.StageCount - 1
            For mI = 0 To Recipe.DispenseTraversal(mStageNo).Count - 1
                '[說明]:找出對應的Node & Patterm
                mNodeName = Recipe.DispenseTraversal(mStageNo)(mI)
                mPatternName = Recipe.Node(mStageNo)(mNodeName).PatternName
                If Recipe.Pattern(mPatternName).Round.Count > mMaxRound Then
                    mMaxRound = Recipe.Pattern(mPatternName).Round.Count
                End If
            Next
        Next
        maxRound = mMaxRound
        Return True

    End Function


    ''' <summary>[使用哪幾個閥]</summary>
    ''' <param name="Recipe"></param>
    ''' <param name="StageNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function WhichValveIsUsed(ByVal Recipe As CRecipe, ByVal StageNo As enmStage, ByRef isUseValveNo1 As Boolean, ByRef isUseValveNo2 As Boolean) As Boolean

        Dim mI As Integer
        Dim mJ As Integer
        Dim mK As Integer
        Dim mNodeName As String
        Dim mPatternName As String
        Dim mUseValve1 As Boolean
        Dim mUseValve2 As Boolean

        For mI = 0 To Recipe.DispenseTraversal(StageNo).Count - 1
            '[說明]:找出對應的Node & Patterm
            mNodeName = Recipe.DispenseTraversal(StageNo)(mI)
            mPatternName = Recipe.Node(StageNo)(mNodeName).PatternName
            '[Note]:檢查此Pattern是否有點膠動作
            If Recipe.Pattern(mPatternName).Round.Count > 0 Then
                For mJ = 0 To Recipe.Pattern(mPatternName).Round.Count - 1
                    If Recipe.Pattern(mPatternName).Round(mJ).StepCount > 0 Then
                        For mK = 0 To Recipe.Pattern(mPatternName).Round(mJ).StepCount - 1
                            Select Case Recipe.Pattern(mPatternName).Round(mJ).CStep(mK).StepType
                                Case eStepFunctionType.SelectValve
                                    Select Case Recipe.Pattern(mPatternName).Round(mJ).CStep(mK).SelectValve.ValveNo
                                        Case eValveWorkMode.Valve1
                                            mUseValve1 = True
                                            If gSSystemParameter.MultiDispenseEnable = True Then
                                                mUseValve2 = True
                                            End If

                                        Case eValveWorkMode.Valve2
                                            mUseValve2 = True

                                    End Select
                            End Select
                        Next
                    End If
                Next
            End If
        Next
        isUseValveNo1 = mUseValve1
        isUseValveNo2 = mUseValve2
        Return True
    End Function


End Module



