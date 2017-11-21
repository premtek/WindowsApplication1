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
    public partial class ucRecipePatternGroup : UserControl
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
        public ucRecipePatternGroup(int machineCount, int machineStageCount, int stageValveCount, int conveyorCount)
        {
            InitializeComponent();
            this.MachineCount = machineCount;
            this.MachineStageCount = machineStageCount;
            this.StageValveCount = stageValveCount;
            this.ConveyorCount = conveyorCount;
            cmbAlignFailed.Items.Clear();
            cmbAlignFailed.Items.Add("None");//不處理
            cmbAlignFailed.Items.Add("Caution");//提示警告
            cmbAlignFailed.Items.Add("Stop");//停止
            switch (ConveyorCount)
            {
                case 2:
                    switch (MachineStageCount)
                    {
                        case 2:
                            btnC1S1.Visible = true;
                            btnC1S2.Visible = true;
                            btnC2S1.Visible = true;
                            btnC2S2.Visible = true;
                            break;
                        default:
                            btnC1S1.Visible = true;
                            btnC1S2.Visible = true;
                            btnC2S1.Visible = true;
                            btnC2S2.Visible = true;
                            break;
                    }

                    break;

                default:
                    switch (MachineStageCount)
                    {
                        case 2:
                            btnC1S1.Visible = true;
                            btnC1S2.Visible = true;
                            btnC2S1.Visible = false;
                            btnC2S2.Visible = false;
                            break;
                        default:
                            btnC1S1.Visible = true;
                            btnC1S2.Visible = false;
                            btnC2S1.Visible = false;
                            btnC2S2.Visible = false;
                            break;
                    }
                    break;
            }
        }
        /// <summary>選擇的軌道編號
        /// </summary>
        int conveyorNo;
        /// <summary>選擇的Stage編號
        /// </summary>
        int stageNo;

        /// <summary>是否介面已載入
        /// </summary>
        /// <remarks>載入前不能引發ValueChanged等事件</remarks>
        private bool _IsLoaded = false;

        private CRecipe _Temp;
        /// <summary>外部傳入父表單
        /// </summary>
        private frmRecipe _parentForm;
        /// <summary>內部操作物件
        /// </summary>
        CRecipeAlignGroup _Align;
        /// <summary>設定數值
        /// </summary>
        /// <param name="AlignGroup">待顯示資料</param>
        /// <param name="selectedItem">選取的項目</param>
        /// <param name="parentForm">父表單</param>
        /// <returns></returns>
        public ErrorCode SetValue(CRecipe recipe, string selectedItem, frmRecipe parentForm)
        {
            this._IsLoaded = false;

            lstPatternList.Items.Clear();
            this._Temp = recipe;
            if ((recipe != null) && (recipe.Pattern.ContainsKey(selectedItem)) && (this._Temp.Pattern[selectedItem].Align != null))
            {
                foreach (string key in recipe.Pattern.Keys)
                {
                    lstPatternList.Items.Add(key);
                }
            }

            if (lstPatternList.Items.Contains(selectedItem))
            {
                lstPatternList.SelectedItem = selectedItem;
            }
            lstPatternList_SelectedIndexChanged(null, null);
            _parentForm = parentForm;
            this._IsLoaded = true;
            return ErrorCode.Success;
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        void UpdateUI()
        {
            switch (conveyorNo)
            {
                case 0:
                    switch (stageNo)
                    {
                        case 0:
                            btnC1S1.BackColor = Color.Blue;
                            btnC1S2.BackColor = SystemColors.Control;
                            btnC1S2.UseVisualStyleBackColor = true;
                            btnC2S1.BackColor = SystemColors.Control;
                            btnC2S1.UseVisualStyleBackColor = true;
                            btnC2S2.BackColor = SystemColors.Control;
                            btnC2S2.UseVisualStyleBackColor = true;
                            break;
                        case 1:
                            btnC1S1.BackColor = SystemColors.Control;
                            btnC1S1.UseVisualStyleBackColor = true;
                            btnC1S2.BackColor = Color.Blue;
                            btnC2S1.BackColor = SystemColors.Control;
                            btnC2S1.UseVisualStyleBackColor = true;
                            btnC2S2.BackColor = SystemColors.Control;
                            btnC2S2.UseVisualStyleBackColor = true;
                            break;
                    }
                    break;
                case 1:
                    switch (stageNo)
                    {
                        case 0:
                            btnC1S1.BackColor = SystemColors.Control;
                            btnC1S1.UseVisualStyleBackColor = true;
                            btnC1S2.UseVisualStyleBackColor = true;
                            btnC2S1.BackColor = Color.Blue;
                            btnC2S2.BackColor = SystemColors.Control;
                            btnC2S2.UseVisualStyleBackColor = true;
                            break;
                        case 1:
                            btnC1S1.BackColor = SystemColors.Control;
                            btnC1S1.UseVisualStyleBackColor = true;
                            btnC1S2.UseVisualStyleBackColor = true;
                            btnC2S1.BackColor = SystemColors.Control;
                            btnC2S1.UseVisualStyleBackColor = true;
                            btnC2S2.BackColor = Color.Blue;
                            break;
                    }
                    break;
            }

            txtName.Text = lstPatternList.SelectedItem.ToString();
            txtRemark.Text = _Align.Remark;
            switch (_Align.Type)
            {
                case enmAlignType.None:
                    rdbType1.Checked = true;
                    if (!this._IsLoaded) rdbType1_CheckedChanged(null, null);
                    break;
                case enmAlignType.DevicePos1:
                    rdbType2.Checked = true;
                    if (!this._IsLoaded) rdbType2_CheckedChanged(null, null);
                    break;
                case enmAlignType.DevicePos2:
                    rdbType3.Checked = true;
                    if (!this._IsLoaded) rdbType3_CheckedChanged(null, null);
                    break;
                case enmAlignType.DevicePos3:
                    if (!this._IsLoaded) rdbType4_CheckedChanged(null, null);
                    rdbType4.Checked = true;
                    break;
            }
            ControlMisc.SetNumericValue(ref nmuPosX, _Align.Align[conveyorNo][stageNo].Origin.X);
            ControlMisc.SetNumericValue(ref nmuPosY, _Align.Align[conveyorNo][stageNo].Origin.Y);
            ControlMisc.SetNumericValue(ref nmuPosZ, _Align.Align[conveyorNo][stageNo].Origin.Z);
            ControlMisc.SetNumericValue(ref nmuAlign1X, _Align.Align[conveyorNo][stageNo].Align1.Pos.X);
            ControlMisc.SetNumericValue(ref nmuAlign1Y, _Align.Align[conveyorNo][stageNo].Align1.Pos.Y);
            ControlMisc.SetNumericValue(ref nmuAlign1Z, _Align.Align[conveyorNo][stageNo].Align1.Pos.Z);
            ControlMisc.SetNumericValue(ref nmuAlign2X, _Align.Align[conveyorNo][stageNo].Align2.Pos.X);
            ControlMisc.SetNumericValue(ref nmuAlign2Y, _Align.Align[conveyorNo][stageNo].Align2.Pos.Y);
            ControlMisc.SetNumericValue(ref nmuAlign2Z, _Align.Align[conveyorNo][stageNo].Align2.Pos.Z);
            ControlMisc.SetNumericValue(ref nmuAlign3X, _Align.Align[conveyorNo][stageNo].Align3.Pos.X);
            ControlMisc.SetNumericValue(ref nmuAlign3Y, _Align.Align[conveyorNo][stageNo].Align3.Pos.Y);
            ControlMisc.SetNumericValue(ref nmuAlign3Z, _Align.Align[conveyorNo][stageNo].Align3.Pos.Z);
            ControlMisc.SetComboBox(ref cmbScene3, _Align.Align[conveyorNo][stageNo].Align3.Secne, "Default");
            ControlMisc.SetComboBox(ref cmbScene1, _Align.Align[conveyorNo][stageNo].Align1.Secne, "Default");
            ControlMisc.SetComboBox(ref cmbScene2, _Align.Align[conveyorNo][stageNo].Align2.Secne, "Default");
            ControlMisc.SetComboBox(ref cmbAlignFailed, (int)_Align.AlignFailed, 0);
            chkByPassResult.Checked = _Align.ByPassResult;
        }
        private void lstPatternList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstPatternList.SelectedItem == null)
            {
                palPattern.Enabled = false;
                return;//無此項目
            }
            if ((this._Temp == null) || (!this._Temp.Pattern.ContainsKey(lstPatternList.SelectedItem.ToString())))
            {
                palPattern.Enabled = false;
                return;//無此項目
            }
            palPattern.Enabled = true;
            _Align = _Temp.Pattern[lstPatternList.SelectedItem.ToString()].Align.Clone();
            UpdateUI();
        }

        private void rdbType1_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdbType1.Checked) return;
            grpAlign1.Enabled = false;
            grpAlign2.Enabled = false;
            grpAlign3.Enabled = false;
            _Align.Type = enmAlignType.None;
        }

        private void rdbType2_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdbType2.Checked) return;
            grpAlign1.Enabled = true;
            grpAlign2.Enabled = false;
            grpAlign3.Enabled = false;
            _Align.Type = enmAlignType.DevicePos1;
        }

        private void rdbType3_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdbType3.Checked) return;
            grpAlign1.Enabled = true;
            grpAlign2.Enabled = true;
            grpAlign3.Enabled = false;
            _Align.Type = enmAlignType.DevicePos2;
        }

        private void rdbType4_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdbType4.Checked) return;
            grpAlign1.Enabled = true;
            grpAlign2.Enabled = true;
            grpAlign3.Enabled = true;
            _Align.Type = enmAlignType.DevicePos3;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lstPatternList.SelectedIndex < 0)//未選
            {
                lstPatternList.BackColor = Color.Yellow;
                System.Threading.Thread.CurrentThread.Join(300);
                lstPatternList.BackColor = SystemColors.Window;
                return;
            }
            if (lstPatternList.SelectedItem == null)
            {
                lstPatternList.BackColor = Color.Yellow;
                System.Threading.Thread.CurrentThread.Join(300);
                lstPatternList.BackColor = SystemColors.Window;
                return;
            }
            string key = lstPatternList.SelectedItem.ToString();
            if (!_Temp.Pattern.ContainsKey(key))//項目不存在
            {
                lstPatternList.BackColor = Color.Red;
                System.Threading.Thread.CurrentThread.Join(300);
                lstPatternList.BackColor = SystemColors.Window;
                System.Diagnostics.Debug.Assert(false);//不應該發生
                return;
            }
            _Temp.Pattern[key].Align = _Align;//資料蓋回
        }

        private void btnGoStart_Click(object sender, EventArgs e)
        {

        }

        private void nmuPosX_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            if (_Align == null) return;
            this._Align.Align[conveyorNo][stageNo].Origin.X = nmuPosX.Value;

        }

        private void nmuPosY_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            if (_Align == null) return;
            this._Align.Align[conveyorNo][stageNo].Origin.Y = nmuPosY.Value;
        }

        private void nmuPosZ_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            if (_Align == null) return;
            this._Align.Align[conveyorNo][stageNo].Origin.Z = nmuPosZ.Value;
        }

        private void nmuAlign1X_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            if (_Align == null) return;
            this._Align.Align[conveyorNo][stageNo].Align1.Pos.X = nmuAlign1X.Value;
        }

        private void nmuAlign1Y_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            if (_Align == null) return;
            this._Align.Align[conveyorNo][stageNo].Align1.Pos.Y = nmuAlign1Y.Value;
        }

        private void nmuAlign1Z_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            if (_Align == null) return;
            this._Align.Align[conveyorNo][stageNo].Align1.Pos.Z = nmuAlign1Z.Value;
        }

        private void nmuAlign2X_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            if (_Align == null) return;
            this._Align.Align[conveyorNo][stageNo].Align2.Pos.X = nmuAlign2X.Value;
        }

        private void nmuAlign2Y_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            if (_Align == null) return;
            this._Align.Align[conveyorNo][stageNo].Align2.Pos.Y = nmuAlign2Y.Value;
        }

        private void nmuAlign2Z_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            if (_Align == null) return;
            this._Align.Align[conveyorNo][stageNo].Align2.Pos.Z = nmuAlign2Z.Value;
        }

        private void nmuSkipX_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            if (_Align == null) return;
            this._Align.Align[conveyorNo][stageNo].Align3.Pos.X = nmuAlign3X.Value;
        }

        private void nmuSkipY_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            if (_Align == null) return;
            this._Align.Align[conveyorNo][stageNo].Align3.Pos.Y = nmuAlign3Y.Value;
        }

        private void nmuSkipZ_ValueChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            if (_Align == null) return;
            this._Align.Align[conveyorNo][stageNo].Align3.Pos.Z = nmuAlign3Z.Value;
        }

        private void txtRemark_TextChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            if (_Align == null) return;
            this._Align.Remark = txtRemark.Text;
        }

        private void chkByPassResult_CheckedChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            if (_Align == null) return;
            this._Align.ByPassResult = chkByPassResult.Checked;
        }
        private void cmbAlignFailed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_IsLoaded) return;//載入前不能引發ValueChanged等事件
            if (_Align == null) return;

            Enum.TryParse<FailedReaction>(cmbAlignFailed.SelectedIndex.ToString(), out this._Align.AlignFailed);
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (txtPattern.Text == "")
            {
                txtPattern.BackColor = Color.Yellow;
                System.Threading.Thread.CurrentThread.Join(300);
                txtPattern.BackColor = SystemColors.Window;
                return;
            }
            if (CFileMisc.IsInvalidPath(txtPattern.Text))
            {
                txtPattern.BackColor = Color.Yellow;
                System.Threading.Thread.CurrentThread.Join(300);
                txtPattern.BackColor = SystemColors.Window;
                return;
            }
            string key = txtPattern.Text;
            if (_Temp.Pattern.ContainsKey(key))
            {
                lstPatternList.SelectedItem = key;
                ((Control)lstPatternList.Items[lstPatternList.SelectedIndex]).BackColor = Color.Yellow;
                System.Threading.Thread.CurrentThread.Join(300);
                ((Control)lstPatternList.Items[lstPatternList.SelectedIndex]).BackColor = SystemColors.Window;
                return;
            }
            _Temp.Pattern.Add(key, new CRecipePattern(_Temp));
            lstPatternList.Items.Add(key);
            _parentForm.cmbPattern.Items.Add(key);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstPatternList.SelectedIndex < 0)
            {
                lstPatternList.BackColor = Color.Yellow;
                System.Threading.Thread.CurrentThread.Join(300);
                lstPatternList.BackColor = SystemColors.Window;
                return;
            }
            string key = lstPatternList.SelectedItem.ToString();
            if (!_Temp.Pattern.ContainsKey(key))
            {
                lstPatternList.BackColor = Color.Red;
                System.Threading.Thread.CurrentThread.Join(300);
                lstPatternList.BackColor = SystemColors.Window;
                System.Diagnostics.Debug.Assert(false);//不應該發生
                return;
            }
            if (_Temp.Pattern.ContainsKey(key))
            {
                _Temp.Pattern.Remove(key);
            }
            if (lstPatternList.Items.Contains(key))
            {
                lstPatternList.Items.Remove(key);
                _parentForm.cmbPattern.Items.Remove(key);
            }

        }

        private void btnC1S1_Click(object sender, EventArgs e)
        {
            conveyorNo = 0;
            stageNo = 0;
            UpdateUI();
        }

        private void btnC2S1_Click(object sender, EventArgs e)
        {
            conveyorNo = 1;
            stageNo = 0;
            UpdateUI();
        }

        private void btnC1S2_Click(object sender, EventArgs e)
        {
            conveyorNo = 0;
            stageNo = 1;
            UpdateUI();
        }

        private void btnC2S2_Click(object sender, EventArgs e)
        {
            conveyorNo = 1;
            stageNo = 1;
            UpdateUI();
        }




    }
}
