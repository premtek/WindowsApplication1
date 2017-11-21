Public Class ucRecipeStatus
    Public Enum RecipeStatus
        ''' <summary>生產流程</summary>
        ''' <remarks></remarks>
        ProductProcess = 0
        ''' <summary>定位點</summary>
        ''' <remarks></remarks>
        Fiducial = 1
        ''' <summary>多層區塊</summary>
        ''' <remarks></remarks>
        MultiDevice = 2
        ''' <summary>基本構型</summary>
        ''' <remarks></remarks>
        BasicPattern = 3
        ''' <summary>產品圖樣</summary>
        ''' <remarks></remarks>
        Pattern = 4
        ''' <summary>打樣測試</summary>
        ''' <remarks></remarks>
        Test = 5
        ''' <summary>檢測</summary>
        ''' <remarks></remarks>
        Inspection = 6
        ''' <summary>製程控制</summary>
        ''' <remarks></remarks>
        ProcessControl = 7
    End Enum
    Dim mStatus As RecipeStatus


    Public Property Status As RecipeStatus
        Get
            Return mStatus
        End Get
        Set(value As RecipeStatus)
            mStatus = value
            UnselectAll()
            SelectOne(value)
        End Set
    End Property

    Sub UnselectAll()
        'Button1.BackColor = SystemColors.Control
        Button2.BackColor = SystemColors.Control
        'Button3.BackColor = SystemColors.Control
        'Button4.BackColor= SystemColors.Control
        Button5.BackColor = SystemColors.Control
        'Button6.BackColor = SystemColors.Control
        Button7.BackColor = SystemColors.Control
        'Button8.BackColor = SystemColors.Control
    End Sub

    Sub SelectOne(ByVal selectedStatus As RecipeStatus)
        Select Case selectedStatus
            'Case RecipeStatus.ProductProcess
            'Button1.BackColor = Color.Lime
            Case RecipeStatus.Fiducial
                Button2.BackColor = Color.Lime
            Case RecipeStatus.MultiDevice
                'Button3.BackColor = Color.Lime
                'Case RecipeStatus.BasicPattern
                'Button4.FlatAppearance.BorderColor = Color.Lime
            Case RecipeStatus.Pattern
                Button5.BackColor = Color.Lime
            Case RecipeStatus.Test
                'Button6.BackColor = Color.Lime
            Case RecipeStatus.Inspection
                Button7.BackColor = Color.Lime
                'Case RecipeStatus.ProcessControl
                '    Button8.BackColor = Color.Lime
        End Select
    End Sub

End Class
