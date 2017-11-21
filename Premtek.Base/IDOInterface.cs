using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>DO卡介面定義</summary>
    /// <remarks></remarks>
    public interface IDOInterface
    {

        /// <summary>關閉卡片</summary>
        /// <remarks></remarks>
        void Close();
        /// <summary> 初始化成功 </summary> 
        bool IsInitialOK { get; set; }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="deviceDescription"></param>
        /// <returns></returns>
        bool Initial(string deviceDescription);
        /// <summary>寫入資料</summary>
        /// <param name="port"></param>
        /// <param name="bytDoData"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        int Write(int port, ref byte bytDoData);
        /// <summary> 單點輸出 </summary>
        /// <param name="port"></param>
        /// <param name="bit"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool DOOutput(int port, int bit, bool value);
        /// <summary>埠數/卡</summary>
        /// <remarks></remarks>
        int PortPerCard { get; set; }
        /// <summary>點數/埠</summary>
        /// <remarks></remarks>
        int BitsPerPort { get; set; }
    }
}
