using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Premtek
{
    public partial class ucRecipeStepParameter : UserControl
    {
        /// <summary>Recipe內單機數
        /// </summary>
        int MachineCount;
        /// <summary>Recipe內單機的平台數
        /// </summary>
        int MachineStageCount;
        /// <summary>Recipe內平台的閥數
        /// </summary>
        int StageValveCount;
        /// <summary>Recipe內軌道數
        /// </summary>
        int ConveyorCount;
        public ucRecipeStepParameter(int machineCount, int machineStageCount, int stageValveCount, int conveyorCount)
        {
            InitializeComponent();
            
            this.MachineCount = machineCount;
            this.MachineStageCount = machineStageCount;
            this.StageValveCount = stageValveCount;
            this.ConveyorCount = conveyorCount;
            
            switch (MachineCount)
            {
                case 2:
                    switch (machineStageCount)
                    {
                        case 2:
                            switch (stageValveCount)
                            {
                                case 2:
                                    btnStage1Valve1.Visible = true;
                                    btnStage1Valve2.Visible = true;
                                    btnStage2Valve1.Visible = true;
                                    btnStage2Valve2.Visible = true;
                                    btnStage3Valve1.Visible = true;
                                    btnStage3Valve2.Visible = true;
                                    btnStage4Valve1.Visible = true;
                                    btnStage4Valve2.Visible = true;
                                    break;
                                default:
                                    btnStage1Valve1.Visible = true;
                                    btnStage1Valve2.Visible = false;
                                    btnStage2Valve1.Visible = true;
                                    btnStage2Valve2.Visible = false;
                                    btnStage3Valve1.Visible = true;
                                    btnStage3Valve2.Visible = false;
                                    btnStage4Valve1.Visible = true;
                                    btnStage4Valve2.Visible = false;
                                    break;
                            }
                            break;
                        default:
                            //目前無此設計
                            System.Diagnostics.Debug.Assert(false);
                            break;
                    }
                    break;
                default:
                    switch (machineStageCount)
                    {
                        case 2:
                            switch (stageValveCount)
                            {
                                case 2:
                                    btnStage1Valve1.Visible = true;
                                    btnStage1Valve2.Visible = true;
                                    btnStage2Valve1.Visible = true;
                                    btnStage2Valve2.Visible = true;
                                    btnStage3Valve1.Visible = false;
                                    btnStage3Valve2.Visible = false;
                                    btnStage4Valve1.Visible = false;
                                    btnStage4Valve2.Visible = false;
                                    break;
                                default:
                                    btnStage1Valve1.Visible = true;
                                    btnStage1Valve2.Visible = false;
                                    btnStage2Valve1.Visible = true;
                                    btnStage2Valve2.Visible = false;
                                    btnStage3Valve1.Visible = false;
                                    btnStage3Valve2.Visible = false;
                                    btnStage4Valve1.Visible = false;
                                    btnStage4Valve2.Visible = false;
                                    break;
                            }
                            break;
                        default:
                           switch (stageValveCount)
                            {
                                case 2:
                                    btnStage1Valve1.Visible = true;
                                    btnStage1Valve2.Visible = true;
                                    btnStage2Valve1.Visible = false;
                                    btnStage2Valve2.Visible = false;
                                    btnStage3Valve1.Visible = false;
                                    btnStage3Valve2.Visible = false;
                                    btnStage4Valve1.Visible = false;
                                    btnStage4Valve2.Visible = false;
                                    break;
                                default:
                                    btnStage1Valve1.Visible = true;
                                    btnStage1Valve2.Visible = false;
                                    btnStage2Valve1.Visible = false;
                                    btnStage2Valve2.Visible = false;
                                    btnStage3Valve1.Visible = false;
                                    btnStage3Valve2.Visible = false;
                                    btnStage4Valve1.Visible = false;
                                    btnStage4Valve2.Visible = false;
                                    break;
                            }
                            break;
                    }
                    break;
            }
          
        }
        /// <summary>編輯用參數
        /// </summary>
        CRecipeParameterStepGroup _Temp;
        ///
        private Dictionary<string, CRecipeParameterStepGroup> _StepGroup;

        bool _IsCorrect;

        /// <summary>
        /// </summary>
        /// <param name="stepGroup"></param>
        public void SetValue(Dictionary<string, CRecipeParameterStepGroup> stepGroup)
        {
            this._StepGroup = stepGroup;
            lstStepGroup.Items.Clear();
            if (stepGroup != null)
            {
                foreach (string key in stepGroup.Keys)
                {
                    lstStepGroup.Items.Add(key);//顯示清單
                }
                if (stepGroup.Count > 0)//如有清單則預選顯示項目
                {
                    lstStepGroup.SelectedIndex = 0;
                }
            }
        }
        private void lstStepGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstStepGroup.SelectedIndex == -1)
            {
                palParam.Visible = false;
                return;
            }
            string _groupName = lstStepGroup.SelectedItem.ToString();
            if (!this._StepGroup.ContainsKey(_groupName)) return;//名稱不存在

            palParam.Visible = true;
            _Temp = this._StepGroup[_groupName].Clone();
            CRecipeParameterLine line = _Temp.Line;
            _IsCorrect = true;
            if (line != null)
            {
                if (ControlMisc.SetNumericValue(ref nmuLineAccDistance, line.AccDistance) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuLineDecDistance, line.DecDistance) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuLineMoveEndDelayTime, line.MoveEndDelayTime) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuLineSuckbackTime, line.SuckBackTime) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuLineValveOnDelayTime, line.ValveOnDelayTime) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuLineXYStableTime, line.XYStableTime) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuLineZDownAcc, line.ZDownAcc) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuLineZDownStableTime, line.ZStableTime) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuLineZDownVelocity, line.ZDownVelocity) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuLineBacktrackHeight, line.BacktrackHeight) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuLineBacktrackLength, line.BacktrackLength) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuLineBacktrackVelocity, line.BacktrackVelocity) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuLineBacktrackDelayTime, line.BacktrackDelayTime) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuLineRetractHeight, line.RetractHeight) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuLineRetractVelocity, line.RetractVelocity) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuLineRetractAcc, line.RetractAcc) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
            }
            CRecipeParameterDot dot = _Temp.Dot;
            if (dot != null)
            {

                if (ControlMisc.SetNumericValue(ref nmuDotSuckbackTime, dot.SuckBackTime) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuDotValveOnDelayTime, dot.ValveOnDelayTime) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuDotXYStableTime, dot.XYStableTime) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuDotZDownAcc, dot.ZDownAcc) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuDotZDownStableTime, dot.ZStableTime) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuDotZDownVelocity, dot.ZDownVelocity) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuDotBacktrackHeight, dot.BacktrackHeight) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuDotBacktrackLength, dot.BacktrackLength) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuDotBacktrackVelocity, dot.BacktrackVelocity) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuDotBacktrackDelayTime, dot.BacktrackDelayTime) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuDotRetractHeight, dot.RetractHeight) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuDotRetractVelocity, dot.RetractVelocity) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuDotRetractAcc, dot.RetractAcc) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
            }
            CRecipeParameterFindHeight FindHeight = _Temp.FindHeight;
            if (FindHeight != null)
            {
                if (ControlMisc.SetNumericValue(ref nmuFindHeightLSL, FindHeight.LowerTolerance) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
                if (ControlMisc.SetNumericValue(ref nmuFindHeightUSL, FindHeight.UpperTolerance) != ErrorCode.Success)
                {
                    _IsCorrect = false;
                }
            }
            CRecipeParameterValve Valve1 = _Temp.Valve1;
            if (Valve1 != null)
            {
                chkValve1Use.Checked = Valve1.UseValve;
            }
            CRecipeParameterValve Valve2 = _Temp.Valve2;
            if (Valve2 != null)
            {
                chkValve2Use.Checked = Valve2.UseValve;
            }
            chkValveControllerUse.Checked = _Temp.EnableValveCtrl;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        /// <summary>新增步驟參數集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string key = txtItem.Text;
            if (key == "")
            {
                txtItem.BackColor = Color.Yellow;
                System.Threading.Thread.CurrentThread.Join(300);
                txtItem.BackColor = SystemColors.Window;
                return;
            }
            if (key.Contains("*") || key.Contains("|") || key.Contains(@"\") || key.Contains(":") || key.Contains("\"") || key.Contains("<") || key.Contains(">") || key.Contains("?") || key.Contains("/"))
            {
                txtItem.BackColor = Color.Yellow;
                System.Threading.Thread.CurrentThread.Join(300);
                txtItem.BackColor = SystemColors.Window;
                txtItem.Text = key.Replace("*", "").Replace("|", "").Replace(@"\", "").Replace(":", "").Replace("\"", "").Replace("<", "").Replace(">", "").Replace("?", "").Replace("/", "");
                return;
            }
            if (_StepGroup.ContainsKey(key))
            {
                txtItem.BackColor = Color.Yellow;
                System.Threading.Thread.CurrentThread.Join(300);
                txtItem.BackColor = SystemColors.Window;
                txtItem.Text = "";
                return;
            }
            _StepGroup.Add(key, new CRecipeParameterStepGroup());
            lstStepGroup.Items.Add(key);
            lstStepGroup.SelectedItem = key;
        }

        /// <summary>刪除步驟參數集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstStepGroup.SelectedIndex < 0)
            {
                lstStepGroup.BackColor = Color.Yellow;
                System.Threading.Thread.CurrentThread.Join(300);
                lstStepGroup.BackColor = SystemColors.Window;
                return;
            }
            string key = lstStepGroup.SelectedItem.ToString();

            int index = lstStepGroup.SelectedIndex;
            if (_StepGroup.ContainsKey(key))
            {
                _StepGroup.Remove(key);
            }
            if (!_StepGroup.ContainsKey("Default"))
            {
                _StepGroup.Add("Default", new CRecipeParameterStepGroup());
            }
            lstStepGroup.Items.Remove(key);
            if (index < lstStepGroup.Items.Count)
            {
                lstStepGroup.SelectedIndex = index;
            }
            else
            {
                lstStepGroup.SelectedIndex = lstStepGroup.Items.Count - 1;
            }
        }

        /// <summary>複製
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (lstStepGroup.SelectedIndex < 0)
            {
                lstStepGroup.BackColor = Color.Yellow;
                System.Threading.Thread.CurrentThread.Join(300);
                lstStepGroup.BackColor = SystemColors.Window;
                return;
            }
            string key = lstStepGroup.SelectedItem.ToString();
            if (_StepGroup.ContainsKey(key))
            {
                CRecipeParameterStepGroup _Temp = _StepGroup[key].Clone();
                key = key + "_1";
                _StepGroup.Add(key, _Temp);
                lstStepGroup.Items.Add(key);
            }

        }

        /// <summary>儲存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.BackColor = Color.Yellow;
            string _groupName = lstStepGroup.SelectedItem.ToString();
            if (!this._StepGroup.ContainsKey(_groupName))
            {
                this._StepGroup.Add(_groupName, _Temp.Clone());
            }
            else
            {
                this._StepGroup[_groupName] = _Temp.Clone();
            }
            System.Threading.Thread.CurrentThread.Join(300);
            btnSave.BackColor = SystemColors.Control;
            btnSave.UseVisualStyleBackColor = true;
        }


        #region "線參數"
        private void chkExtend_CheckedChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Line == null) _Temp.Line = new CRecipeParameterLine();
            _Temp.Line.JetMode = chkExtend.Checked;
        }

        private void chkNonJetFly_CheckedChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Line == null) _Temp.Line = new CRecipeParameterLine();
            if (chkNonJetFly.Checked)
            {
                _Temp.Line.NonJetFly = 1;
            }
            else
            {
                _Temp.Line.NonJetFly = 0;
            }
        }

        private void nmuLineXYStableTime_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Line == null) _Temp.Line = new CRecipeParameterLine();
            _Temp.Line.XYStableTime = nmuLineXYStableTime.Value;
        }

        private void nmuLineZDownVelocity_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Line == null) _Temp.Line = new CRecipeParameterLine();
            _Temp.Line.ZDownVelocity = nmuLineZDownVelocity.Value;
        }

        private void nmuLineZDownAcc_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Line == null) _Temp.Line = new CRecipeParameterLine();
            _Temp.Line.ZDownAcc = nmuLineZDownAcc.Value;
        }

        private void nmuLineZDownStableTime_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Line == null) _Temp.Line = new CRecipeParameterLine();
            _Temp.Line.ZStableTime = nmuLineZDownStableTime.Value;
        }

        private void nmuLineAccDistance_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Line == null) _Temp.Line = new CRecipeParameterLine();
            _Temp.Line.AccDistance = nmuLineAccDistance.Value;
        }

        private void nmuLineValveOnDelayTime_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Line == null) _Temp.Line = new CRecipeParameterLine();
            _Temp.Line.ValveOnDelayTime = nmuLineValveOnDelayTime.Value;
        }

        private void nmuLineDecDistance_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Line == null) _Temp.Line = new CRecipeParameterLine();
            _Temp.Line.DecDistance = nmuLineDecDistance.Value;
        }

        private void nmuEndAheadDistance_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Line == null) _Temp.Line = new CRecipeParameterLine();
            _Temp.Line.EndAheadDistance = nmuEndAheadDistance.Value;
        }

        private void nmuLineSuckbackTime_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Line == null) _Temp.Line = new CRecipeParameterLine();
            _Temp.Line.SuckBackTime = nmuLineSuckbackTime.Value;
        }

        private void nmuLineMoveEndDelayTime_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Line == null) _Temp.Line = new CRecipeParameterLine();
            _Temp.Line.MoveEndDelayTime = nmuLineMoveEndDelayTime.Value;
        }

        private void nmuLineBacktrackHeight_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Line == null) _Temp.Line = new CRecipeParameterLine();
            _Temp.Line.BacktrackHeight = nmuLineBacktrackHeight.Value;
        }

        private void cmbLineBacktrackDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Line == null) _Temp.Line = new CRecipeParameterLine();
            _Temp.Line.BacktrackDirection = (BacktrackDirection)cmbLineBacktrackDirection.SelectedIndex;
        }

        private void nmuLineBacktrackLength_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Line == null) _Temp.Line = new CRecipeParameterLine();
            _Temp.Line.BacktrackLength = nmuLineBacktrackLength.Value;
        }

        private void nmuLineBacktrackVelocity_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Line == null) _Temp.Line = new CRecipeParameterLine();
            _Temp.Line.BacktrackVelocity = nmuLineBacktrackVelocity.Value;
        }

        private void nmuLineBacktrackDelayTime_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Line == null) _Temp.Line = new CRecipeParameterLine();
            _Temp.Line.BacktrackDelayTime = nmuLineBacktrackDelayTime.Value;
        }

        private void nmuLineRetractHeight_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Line == null) _Temp.Line = new CRecipeParameterLine();
            _Temp.Line.BacktrackHeight = nmuLineRetractHeight.Value;
        }

        private void nmuLineRetractVelocity_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Line == null) _Temp.Line = new CRecipeParameterLine();
            _Temp.Line.RetractVelocity = nmuLineRetractVelocity.Value;
        }

        private void nmuLineRetractAcc_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Line == null) _Temp.Line = new CRecipeParameterLine();
            _Temp.Line.RetractAcc = nmuLineRetractAcc.Value;
        }
        #endregion

        #region "點參數"

        private void nmuDotXYStableTime_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Dot == null) _Temp.Dot = new CRecipeParameterDot();
            _Temp.Dot.XYStableTime = nmuDotXYStableTime.Value;
        }

        private void nmuDotZDownVelocity_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Dot == null) _Temp.Dot = new CRecipeParameterDot();
            _Temp.Dot.ZDownVelocity = nmuDotZDownVelocity.Value;
        }

        private void nmuDotZDownAcc_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Dot == null) _Temp.Dot = new CRecipeParameterDot();
            _Temp.Dot.ZDownAcc = nmuDotZDownAcc.Value;
        }

        private void nmuDotZDownStableTime_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Dot == null) _Temp.Dot = new CRecipeParameterDot();
            _Temp.Dot.ZStableTime = nmuDotZDownStableTime.Value;
        }

        private void nmuDotValveOnDelayTime_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Dot == null) _Temp.Dot = new CRecipeParameterDot();
            _Temp.Dot.ValveOnDelayTime = nmuDotValveOnDelayTime.Value;
        }

        private void nmuDotSuckbackTime_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Dot == null) _Temp.Dot = new CRecipeParameterDot();
            _Temp.Dot.SuckBackTime = nmuDotSuckbackTime.Value;
        }

        private void nmuDotBacktrackHeight_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Dot == null) _Temp.Dot = new CRecipeParameterDot();
            _Temp.Dot.BacktrackHeight = nmuDotBacktrackHeight.Value;
        }

        private void cmbDotBacktrackDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Dot == null) _Temp.Dot = new CRecipeParameterDot();

        }

        private void nmuDotBacktrackLength_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Dot == null) _Temp.Dot = new CRecipeParameterDot();
            _Temp.Dot.BacktrackLength = nmuDotBacktrackLength.Value;
        }

        private void nmuDotBacktrackVelocity_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Dot == null) _Temp.Dot = new CRecipeParameterDot();
            _Temp.Dot.BacktrackVelocity = nmuDotBacktrackVelocity.Value;
        }

        private void nmuDotBacktrackDelayTime_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Dot == null) _Temp.Dot = new CRecipeParameterDot();
            _Temp.Dot.BacktrackDelayTime = nmuDotBacktrackDelayTime.Value;
        }

        private void nmuDotRetractHeight_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Dot == null) _Temp.Dot = new CRecipeParameterDot();
            _Temp.Dot.RetractHeight = nmuDotRetractHeight.Value;
        }

        private void nmuDotRetractVelocity_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Dot == null) _Temp.Dot = new CRecipeParameterDot();
            _Temp.Dot.RetractVelocity = nmuDotRetractVelocity.Value;
        }

        private void nmuDotRetractAcc_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Dot == null) _Temp.Dot = new CRecipeParameterDot();
            _Temp.Dot.RetractAcc = nmuDotRetractAcc.Value;
        }
        #endregion

        #region "測高"

        private void nmuFindHeightUSL_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.FindHeight == null) _Temp.FindHeight = new CRecipeParameterFindHeight();
            _Temp.FindHeight.UpperTolerance = nmuFindHeightUSL.Value;
        }

        private void nmuFindHeightLSL_ValueChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.FindHeight == null) _Temp.FindHeight = new CRecipeParameterFindHeight();
            _Temp.FindHeight.LowerTolerance = nmuFindHeightLSL.Value;
        }

        #endregion

        #region "閥控"
        private void chkValve1Use_CheckedChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Valve1 == null) _Temp.Valve1 = new CRecipeParameterValve();
            _Temp.Valve1.UseValve = chkValve1Use.Checked;
        }

        private void chkValve2Use_CheckedChanged(object sender, EventArgs e)
        {
            if (_Temp == null) return;
            if (_Temp.Valve2 == null) _Temp.Valve2 = new CRecipeParameterValve();
            _Temp.Valve2.UseValve = chkValve2Use.Checked;
        }

        private void chkValveControllerUse_CheckedChanged(object sender, EventArgs e)
        {
            _Temp.EnableValveCtrl = chkValveControllerUse.Checked;
        }
        #endregion

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

    }
}
