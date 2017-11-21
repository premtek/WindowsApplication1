Imports ProjectRecipe
Imports ProjectMotion
Imports ProjectCore

Public Class ucLevelNonArray

    ''' <summary>
    ''' 非陣列清單
    ''' </summary>
    ''' <remarks></remarks>
    Public NonArray As New List(Of NonArray)

    ''' <summary>
    ''' 未修改前清單
    ''' </summary>
    Public NonArrayTemp() As List(Of NonArray)

    ''' <summary>外部引入 設定基準點X</summary>
    ''' <remarks></remarks>
    Public BasicPosX As Decimal
    ''' <summary>外部引入 設定基準點Y</summary>
    ''' <remarks></remarks>
    Public BasicPosY As Decimal
    ''' <summary>外部引入 設定基準點Z</summary>
    ''' <remarks></remarks>
    Public BasicPosZ As Decimal
    ''' <summary>配接用參數</summary>
    ''' <remarks></remarks>
    Public NodeID As String


    ''' <summary>表單已載入</summary>
    ''' <remarks></remarks>
    Public IsFormLoaded As Boolean

    Dim mLevelNo As Integer
    ''' <summary>設定陣列所在層數</summary>
    ''' <remarks></remarks>
    Public Property LevelNo As Integer
        Get
            Return mLevelNo
        End Get
        Set(value As Integer)
            mLevelNo = value
            If value < 0 Then '層數不對時, 不能設定與儲存
                grpNonArray.Enabled = False
            Else
                grpNonArray.Enabled = True
            End If
        End Set
    End Property

    'jimmy 20170725
    Dim mSelected As Integer


    Public sys As sSysParam
    ''' <summary>已移動</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event OnMoved(sender As Object, e As EventArgs)
    ''' <summary>已變更值</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ValueChanged(sender As Object, e As EventArgs)

    ''' <summary>介面保護更新</summary>
    ''' <remarks></remarks>
    Public Sub RefreshUI()
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then '未復歸不能按
            btnMovePos.Enabled = False
            btnSetPos.Enabled = False

        Else
            btnMovePos.Enabled = True
            btnSetPos.Enabled = True

        End If
        RefreshLstNonArray()
    End Sub

    ''' <summary>新增</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If lstNonArray.SelectedIndex = -1 Then '沒選擇加在最末端
            NonArray.Add(New NonArray)
            UpdateNonArray(sender, e)
            lstNonArray.SelectedIndex = NonArray.Count - 1
        Else '選擇則加在選擇項目
            'Dim mSelected As Integer = lstNonArray.SelectedIndex
            'mSelected = lstNonArray.SelectedIndex
            If NonArray.Count <> 0 Then
                mSelected += 1
                NonArray.Insert(mSelected, New NonArray)
                UpdateNonArray(sender, e)
                If mSelected < lstNonArray.Items.Count Then
                    lstNonArray.SelectedIndex = mSelected
                End If
            Else
                NonArray.Add(New NonArray)
                UpdateNonArray(sender, e)
                lstNonArray.SelectedIndex = 0
            End If

        End If
        LockNumericUpDown()
        btnDelete.Enabled = True
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        btnDelete.Enabled = False
        If lstNonArray.SelectedIndex < 0 Then
            '請選擇項目
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000063))
            MsgBox(gMsgHandler.GetMessage(Warn_3000063), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            'MsgBox("Select Item.")
            btnDelete.Enabled = True
            Exit Sub
        End If
        Dim mSelected As Integer = lstNonArray.SelectedIndex
        'jimmy 20170804
        If mSelected = 0 Then
            Select Case gSSystemParameter.LanguageType
                Case enmLanguageType.eEnglish
                    MsgBox("Data couldn't Delete.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eSimplifiedChinese
                    MsgBox("资料无法删除", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eTraditionalChinese
                    MsgBox("資料無法刪除", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            'jimmy 20170804
            btnDelete.Enabled = True
            Exit Sub
        End If
        'Toby add
        If NonArray.Count < 2 Then
            Select Case gSSystemParameter.LanguageType
                Case enmLanguageType.eEnglish
                    MsgBox("Data couldn't Delete.", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eSimplifiedChinese
                    MsgBox("资料无法删除", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
                Case enmLanguageType.eTraditionalChinese
                    MsgBox("資料無法刪除", MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            End Select
            'jimmy 20170804
            btnDelete.Enabled = True
            Exit Sub
        End If


        NonArray.RemoveAt(lstNonArray.SelectedIndex)
        UpdateNonArray(sender, e)
        If mSelected >= lstNonArray.Items.Count Then '最末端 選最後一筆
            lstNonArray.SelectedIndex = lstNonArray.Items.Count - 1
        Else
            lstNonArray.SelectedIndex = mSelected
        End If
        If NonArray.Count = 0 Then '刪到底, 封印
            btnSetPos.Enabled = False
            btnMovePos.Enabled = False
            nmcPosX.Enabled = False
            nmcPosY.Enabled = False
            btnDelete.Enabled = False
        Else
            btnDelete.Enabled = True
        End If

    End Sub

    Sub RefreshLstNonArray()
        With lstNonArray.Items
            .Clear()
            For mItem As Integer = 0 To NonArray.Count - 1
                .Add(mItem.ToString("000"))
            Next
        End With
    End Sub
    Private Sub ucLevelNonArray_Load(sender As Object, e As EventArgs) Handles Me.Load
        If NonArray.Count = 0 Then '增加預設 (防舊的rcp)
            NonArray.Add(New NonArray)
        End If
        RefreshLstNonArray()

        '初始NonArray 預設 ("000" X:0 Y:0)_Toby
        If NonArray.Count < 1 Then
            Dim DefaultArray As NonArray
            DefaultArray.RelPosX = 0
            DefaultArray.RelPosY = 0
            NonArray.Add(DefaultArray)
            UpdateNonArray(sender, e)
            lstNonArray.SelectedIndex = NonArray.Count - 1
        End If

    End Sub

    Private Sub lstNonArray_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstNonArray.SelectedIndexChanged
        If lstNonArray.SelectedIndex < 0 Then '沒選不能Set
            btnSetPos.Enabled = False
            btnMovePos.Enabled = False
            nmcPosX.Enabled = False
            nmcPosY.Enabled = False
            Exit Sub
        End If
        If lstNonArray.SelectedIndex > NonArray.Count - 1 Then '選項超過上界
            btnSetPos.Enabled = False
            btnMovePos.Enabled = False
            nmcPosX.Enabled = False
            nmcPosY.Enabled = False
            Exit Sub
        End If
        Me.IsFormLoaded = False
        nmcPosX.Value = NonArray(lstNonArray.SelectedIndex).RelPosX
        nmcPosY.Value = NonArray(lstNonArray.SelectedIndex).RelPosY
        Me.IsFormLoaded = True
        btnSetPos.Enabled = True
        btnMovePos.Enabled = True
        nmcPosX.Enabled = True
        nmcPosY.Enabled = True
        mSelected = lstNonArray.SelectedIndex
        LockNumericUpDown()
    End Sub

    ''' <summary>更新單層非陣列設定值</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Sub UpdateNonArray(sender As Object, e As EventArgs)
        Dim StageNo As Integer = sys.StageNo
        If IsFormLoaded = False Then
            Exit Sub
        End If
        '--- 可編輯才儲存 ---
        If Not gCRecipe.Editable Then
            Exit Sub
        End If

        If StageNo >= gCRecipe.Node.Count Then
            '無相應站號
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000060))
            MsgBox(gMsgHandler.GetMessage(Warn_3000060), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If Not gCRecipe.Node(StageNo).ContainsKey(NodeID) Then
            '無相應節點
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000061))
            MsgBox(gMsgHandler.GetMessage(Warn_3000061), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        If LevelNo >= gCRecipe.Node(StageNo)(NodeID).Array.Count Then
            '該層資料不存在
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000062))
            MsgBox(gMsgHandler.GetMessage(Warn_3000062), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If

        gCRecipe.Node(StageNo)(NodeID).Array(LevelNo).NonArray = NonArray
        gCRecipe.Node(StageNo)(NodeID).Array(LevelNo).LevelType = eLevelType.NoneArray

        RaiseEvent ValueChanged(sender, e)
    End Sub

    Private Sub btnSetPos_Click(sender As Object, e As EventArgs) Handles btnSetPos.Click
        'Dim mSelected As Integer = lstNonArray.SelectedIndex
        ' mSelected = lstNonArray.SelectedIndex
        If mSelected < 0 Then
            '請選擇項目
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000063))
            MsgBox(gMsgHandler.GetMessage(Warn_3000063), MsgBoxStyle.SystemModal + MsgBoxStyle.MsgBoxSetForeground)
            Exit Sub
        End If
        Dim mItem As New NonArray

        'NonArray第一點應鎖定為(0,0), 以確保frmRecipe01移動時不會移錯
        'jimmy 20170717
        If mSelected = 0 Then
            nmcPosX.Value = 0
            nmcPosY.Value = 0
            mItem.RelPosX = 0
            mItem.RelPosY = 0
            NonArray(mSelected) = mItem
            UpdateNonArray(sender, e)
            Exit Sub
        End If

        'mItem.RelPosX = nmcPosX.Value
        'mItem.RelPosY = nmcPosY.Value
        '與第一個拍照定位點
        mItem.RelPosX = gCMotion.GetPositionValue(sys.AxisX) - BasicPosX 'Soni / 2017.07.03
        mItem.RelPosY = gCMotion.GetPositionValue(sys.AxisY) - BasicPosY 'Soni / 2017.07.03
        nmcPosX.Value = mItem.RelPosX 'Soni / 2017.07.03 
        nmcPosY.Value = mItem.RelPosY 'Soni / 2017.07.03
        NonArray(mSelected) = mItem
        UpdateNonArray(sender, e)
    End Sub

    Private Sub btnMovePos_Click(sender As Object, e As EventArgs) Handles btnMovePos.Click
        gSyslog.Save("[frmRecipe01]" & vbTab & "[btnGoStartPos]" & vbTab & "Click")
        btnMovePos.Enabled = False
        If gSYS(eSys.OverAll).Act(eAct.Home).RunStatus <> enmRunStatus.Finish Then
            BtnHomeFirstBehavior(sender)
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
            btnMovePos.Enabled = True
            Exit Sub
        End If
        If gCRecipe Is Nothing Then
            gSyslog.Save(gMsgHandler.GetMessage(Warn_3000012), "Warn_3000012", eMessageLevel.Warning)
            btnMovePos.Enabled = True
            Exit Sub
        End If

        '[說明]:X、Y、Z軸
        gCMotion.SetVelAccDec(sys.AxisX)
        gCMotion.SetVelAccDec(sys.AxisY)
        gCMotion.SetVelAccDec(sys.AxisZ)
        gCMotion.SetVelAccDec(sys.AxisB)
        gCMotion.SetVelAccDec(sys.AxisC)


        Dim AxisNo(4) As Integer
        Dim TargetPos(4) As Decimal
        AxisNo(0) = sys.AxisX
        AxisNo(1) = sys.AxisY
        AxisNo(2) = sys.AxisZ
        AxisNo(3) = sys.AxisB
        AxisNo(4) = sys.AxisC
        TargetPos(0) = nmcPosX.Value + BasicPosX
        TargetPos(1) = nmcPosY.Value + BasicPosY
        TargetPos(2) = BasicPosZ
        TargetPos(3) = 0
        TargetPos(4) = 0
        ButtonSafeMovePos(sender, AxisNo, TargetPos, sys)
        RaiseEvent OnMoved(sender, e)
        btnMovePos.Enabled = True
    End Sub


    Private Sub nmcPosX_ValueChanged(sender As Object, e As EventArgs) Handles nmcPosX.ValueChanged, nmcPosY.ValueChanged
        If Me.IsFormLoaded = False Then
            Exit Sub
        End If
        ' Dim mSelected As Integer = lstNonArray.SelectedIndex
        '  mSelected = lstNonArray.SelectedIndex
        Dim mItem As New NonArray

        'NonArray第一點應鎖定為(0,0), 以確保frmRecipe01移動時不會移錯
        'jimmy 20170717
        If mSelected = 0 Then
            'nmcPosX.Value = 0
            ' nmcPosY.Value = 0
            mItem.RelPosX = 0
            mItem.RelPosY = 0
        Else
            mItem.RelPosX = nmcPosX.Value
            mItem.RelPosY = nmcPosY.Value
        End If



        If NonArray.Count = 0 Then
            Exit Sub
        End If
        If mSelected < 0 Then
            Exit Sub
        End If
        If mSelected >= NonArray.Count Then
            Exit Sub
        End If
        NonArray(mSelected) = mItem

        RaiseEvent ValueChanged(sender, e)
    End Sub

    Public Sub LockNumericUpDown()
        If mSelected = 0 Then
            nmcPosX.Enabled = False
            nmcPosY.Enabled = False
        Else
            nmcPosX.Enabled = True
            nmcPosY.Enabled = True
        End If
    End Sub
End Class
