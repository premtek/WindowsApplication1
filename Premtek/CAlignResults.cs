using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>定位結果(包含多點)
    /// </summary>
    public class CAlignResults
    {
        public enmAlignType AlignType = enmAlignType.DevicePos1;
        /// <summary>第一定位點
        /// </summary>
        public int Ticket;
        /// <summary>第二定位點
        /// </summary>
        public int Ticket2;
        /// <summary>第三定位點
        /// </summary>
        public int Ticket3;
        /// <summary>第一定位點結果
        /// </summary>
        public CAlignResult Align1 = new CAlignResult();
        /// <summary>第二定位點結果
        /// </summary>
        public CAlignResult Align2 = new CAlignResult();
        /// <summary>第三定位點結果
        /// </summary>
        public CAlignResult Align3 = new CAlignResult();
        /// <summary>拍照中為Busy=True, 取得結果或未拍照為False.
        /// </summary>
        public bool IsBusy = false;
        /// <summary>拍照中為Busy=True, 取得結果或未拍照為False.
        /// </summary>
        public bool IsBusy2 = false;
        /// <summary>拍照中為Busy=True, 取得結果或未拍照為False.
        /// </summary>
        public bool IsBusy3 = false;
        /// <summary>預設忽略結果
        /// </summary>
        public bool ByPassResult = true;
    }
}
