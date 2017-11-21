using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>控制項其他功能
    /// </summary>
    public static class ControlMisc
    {
        /// <summary>受上下限限定保護的數值設定
        /// </summary>
        /// <param name="nmu"></param>
        /// <param name="value"></param>
        public static ErrorCode SetNumericValue(ref System.Windows.Forms.NumericUpDown nmu, decimal value)
        {
            if (nmu == null) return ErrorCode.Failed;
            if (value < nmu.Minimum)
            {
                nmu.Value = nmu.Minimum;
                return ErrorCode.Failed;
            }
            if (value > nmu.Maximum)
            {
                nmu.Value = nmu.Maximum;
                return ErrorCode.Failed;
            }
            nmu.Value = value;
            return ErrorCode.Success;
        }

        /// <summary>受選項存在保護的下拉列表框設定
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="item">目標選項</param>
        /// <param name="defaultValue">目標選項不存在時的預設選項</param>
        public static ErrorCode SetComboBox(ref System.Windows.Forms.ComboBox cmb, string item, string defaultValue)
        {
            if (cmb == null) return ErrorCode.Failed;
            if (!cmb.Items.Contains(item))
            {
                if (cmb.Items.Contains(defaultValue))
                {
                    cmb.SelectedItem = defaultValue;
                    return ErrorCode.Success;
                }
                else
                {
                    return ErrorCode.Failed;
                }
                
            }
            cmb.SelectedItem = item;
            return ErrorCode.Success;
        }

        /// <summary>受選項索引保護的下拉列表框設定
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="item"></param>
        public static ErrorCode SetComboBox(ref System.Windows.Forms.ComboBox cmb, int index, int defaultValue)
        {
            if (cmb == null) return ErrorCode.Failed;
            if (index < -1) return ErrorCode.Failed;
            if (index >= cmb.Items.Count)
            {
                if (defaultValue < cmb.Items.Count)
                {
                    cmb.SelectedIndex = defaultValue;
                    return ErrorCode.Success;
                }
                else
                {
                    return ErrorCode.Failed;
                }
            }
            cmb.SelectedIndex = index;
            return ErrorCode.Success;
        }
    }
}
