Imports ProjectCore

Public Class ucProcessInfo
    ''' <summary>設定最大閥數</summary>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Public Sub SetMaxValve(ByVal value As enmValve)
        Select Case value
            Case enmValve.No1
                lblAirPressureValue1.Visible = True
                lblAirPressureValue2.Visible = False
                lblAirPressureValue3.Visible = False
                lblAirPressureValue4.Visible = False
                lblFlowWeightValue1.Visible = True
                lblFlowWeightValue2.Visible = False
                lblFlowWeightValue3.Visible = False
                lblFlowWeightValue4.Visible = False
                'lblNextPurgeTimeValue1.Visible = True
                'lblNextPurgeTimeValue2.Visible = False
                'lblNextPurgeTimeValue3.Visible = False
                'lblNextPurgeTimeValue4.Visible = False
                '2017/5/10_Toby
                lblCPKDisplay1.Visible = True
                lblCPKDisplay2.Visible = False
                lblCPKDisplay3.Visible = False
                lblCPKDisplay4.Visible = False

                'lblPatternDotValue1.Visible = True
                'lblPatternDotValue2.Visible = False
                'lblPatternDotValue3.Visible = False
                'lblPatternDotValue4.Visible = False
                'lblPatternWeightValue1.Visible = True
                'lblPatternWeightValue2.Visible = False
                'lblPatternWeightValue3.Visible = False
                'lblPatternWeightValue4.Visible = False
                lblPNozzleTempValue1.Visible = True
                lblPNozzleTempValue2.Visible = False
                lblPNozzleTempValue3.Visible = False
                lblPNozzleTempValue4.Visible = False
                lblPotLife1.Visible = True
                lblPotLife2.Visible = False
                lblPotLife3.Visible = False
                lblPotLife4.Visible = False
                lblPSyringeTempValue1.Visible = True
                lblPSyringeTempValue2.Visible = False
                lblPSyringeTempValue3.Visible = False
                lblPSyringeTempValue4.Visible = False
                lblValve1.Visible = True
                lblValve2.Visible = False
                lblValve3.Visible = False
                lblValve4.Visible = False
                lblValve1DispenseCount.Visible = True
                lblValve2DispenseCount.Visible = False
                lblValve3DispenseCount.Visible = False
                lblValve4DispenseCount.Visible = False
                lbGlueStartTime1.Visible = True
                lbGlueStartTime2.Visible = False
                lbGlueStartTime3.Visible = False
                lbGlueStartTime4.Visible = False

            Case enmValve.No2
                lblAirPressureValue1.Visible = True
                lblAirPressureValue2.Visible = True
                lblAirPressureValue3.Visible = False
                lblAirPressureValue4.Visible = False
                lblFlowWeightValue1.Visible = True
                lblFlowWeightValue2.Visible = True
                lblFlowWeightValue3.Visible = False
                lblFlowWeightValue4.Visible = False
                'lblNextPurgeTimeValue1.Visible = True
                'lblNextPurgeTimeValue2.Visible = True
                'lblNextPurgeTimeValue3.Visible = False
                'lblNextPurgeTimeValue4.Visible = False
                '2017/5/10_Toby
                lblCPKDisplay1.Visible = True
                lblCPKDisplay2.Visible = True
                lblCPKDisplay3.Visible = False
                lblCPKDisplay4.Visible = False

                'lblPatternDotValue1.Visible = True
                'lblPatternDotValue2.Visible = True
                'lblPatternDotValue3.Visible = False
                'lblPatternDotValue4.Visible = False
                'lblPatternWeightValue1.Visible = True
                'lblPatternWeightValue2.Visible = True
                'lblPatternWeightValue3.Visible = False
                'lblPatternWeightValue4.Visible = False
                lblPNozzleTempValue1.Visible = True
                lblPNozzleTempValue2.Visible = True
                lblPNozzleTempValue3.Visible = False
                lblPNozzleTempValue4.Visible = False
                lblPotLife1.Visible = True
                lblPotLife2.Visible = True
                lblPotLife3.Visible = False
                lblPotLife4.Visible = False
                lblPSyringeTempValue1.Visible = True
                lblPSyringeTempValue2.Visible = True
                lblPSyringeTempValue3.Visible = False
                lblPSyringeTempValue4.Visible = False
                lblValve1.Visible = True
                lblValve2.Visible = True
                lblValve3.Visible = False
                lblValve4.Visible = False
                lblValve1DispenseCount.Visible = True
                lblValve2DispenseCount.Visible = True
                lblValve3DispenseCount.Visible = False
                lblValve4DispenseCount.Visible = False
                lbGlueStartTime1.Visible = True
                lbGlueStartTime2.Visible = True
                lbGlueStartTime3.Visible = False
                lbGlueStartTime4.Visible = False

            Case enmValve.No3
                lblAirPressureValue1.Visible = True
                lblAirPressureValue2.Visible = True
                lblAirPressureValue3.Visible = True
                lblAirPressureValue4.Visible = False
                lblFlowWeightValue1.Visible = True
                lblFlowWeightValue2.Visible = True
                lblFlowWeightValue3.Visible = True
                lblFlowWeightValue4.Visible = False
                'lblNextPurgeTimeValue1.Visible = True
                'lblNextPurgeTimeValue2.Visible = True
                'lblNextPurgeTimeValue3.Visible = True
                'lblNextPurgeTimeValue4.Visible = False

                '2017/5/10_Toby
                lblCPKDisplay1.Visible = True
                lblCPKDisplay2.Visible = True
                lblCPKDisplay3.Visible = True
                lblCPKDisplay4.Visible = False

                'lblPatternDotValue1.Visible = True
                'lblPatternDotValue2.Visible = True
                'lblPatternDotValue3.Visible = True
                'lblPatternDotValue4.Visible = False
                'lblPatternWeightValue1.Visible = True
                'lblPatternWeightValue2.Visible = True
                'lblPatternWeightValue3.Visible = True
                'lblPatternWeightValue4.Visible = False
                lblPNozzleTempValue1.Visible = True
                lblPNozzleTempValue2.Visible = True
                lblPNozzleTempValue3.Visible = True
                lblPNozzleTempValue4.Visible = False
                lblPotLife1.Visible = True
                lblPotLife2.Visible = True
                lblPotLife3.Visible = True
                lblPotLife4.Visible = False
                lblPSyringeTempValue1.Visible = True
                lblPSyringeTempValue2.Visible = True
                lblPSyringeTempValue3.Visible = True
                lblPSyringeTempValue4.Visible = False
                lblValve1.Visible = True
                lblValve2.Visible = True
                lblValve3.Visible = True
                lblValve4.Visible = False
                lblValve1DispenseCount.Visible = True
                lblValve2DispenseCount.Visible = True
                lblValve3DispenseCount.Visible = True
                lblValve4DispenseCount.Visible = False
                lbGlueStartTime1.Visible = True
                lbGlueStartTime2.Visible = True
                lbGlueStartTime3.Visible = True
                lbGlueStartTime4.Visible = False

            Case enmValve.No4
                lblAirPressureValue1.Visible = True
                lblAirPressureValue2.Visible = True
                lblAirPressureValue3.Visible = True
                lblAirPressureValue4.Visible = True
                lblFlowWeightValue1.Visible = True
                lblFlowWeightValue2.Visible = True
                lblFlowWeightValue3.Visible = True
                lblFlowWeightValue4.Visible = True
                'lblNextPurgeTimeValue1.Visible = True
                'lblNextPurgeTimeValue2.Visible = True
                'lblNextPurgeTimeValue3.Visible = True
                'lblNextPurgeTimeValue4.Visible = True

                '2017/5/10_Toby
                lblCPKDisplay1.Visible = True
                lblCPKDisplay2.Visible = True
                lblCPKDisplay3.Visible = True
                lblCPKDisplay4.Visible = True

                'lblPatternDotValue1.Visible = True
                'lblPatternDotValue2.Visible = True
                'lblPatternDotValue3.Visible = True
                'lblPatternDotValue4.Visible = True
                'lblPatternWeightValue1.Visible = True
                'lblPatternWeightValue2.Visible = True
                'lblPatternWeightValue3.Visible = True
                'lblPatternWeightValue4.Visible = True
                lblPNozzleTempValue1.Visible = True
                lblPNozzleTempValue2.Visible = True
                lblPNozzleTempValue3.Visible = True
                lblPNozzleTempValue4.Visible = True
                lblPotLife1.Visible = True
                lblPotLife2.Visible = True
                lblPotLife3.Visible = True
                lblPotLife4.Visible = True
                lblPSyringeTempValue1.Visible = True
                lblPSyringeTempValue2.Visible = True
                lblPSyringeTempValue3.Visible = True
                lblPSyringeTempValue4.Visible = True
                lblValve1.Visible = True
                lblValve2.Visible = True
                lblValve3.Visible = True
                lblValve4.Visible = True
                lblValve1DispenseCount.Visible = True
                lblValve2DispenseCount.Visible = True
                lblValve3DispenseCount.Visible = True
                lblValve4DispenseCount.Visible = True
                lbGlueStartTime1.Visible = True
                lbGlueStartTime2.Visible = True
                lbGlueStartTime3.Visible = True
                lbGlueStartTime4.Visible = True
        End Select
    End Sub



End Class
