Imports System
Imports ProjectCore
Imports ProjectIO


Public Class frmCCDTargerEdit
    Public sys As sSysParam


    Private Sub frmCCDTargerEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UcCCDTargerEdit1.sys = sys
        UcCCDTargerEdit1.Initial()

    End Sub

    Private Sub frmCCDTargerEdit_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

    End Sub
End Class