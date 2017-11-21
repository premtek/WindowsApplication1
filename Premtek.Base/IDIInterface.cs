using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary>DI卡介面定義</summary>
    /// <remarks></remarks>
    public interface IDIInterface
    {
        /// <summary>關閉卡片</summary>
        /// <remarks></remarks>
        void Close();
        /// <summary>初始化</summary>
        /// <param name="deviceDescription"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        bool Initial(string deviceDescription);
        /// <summary>讀取資料</summary>
        /// <param name="port"></param>
        /// <param name="bytDiData"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        int Read(int port, ref byte bytDiData);
        /// <summary>埠數/卡</summary>
        /// <remarks></remarks>
        int PortPerCard { get; set; }
        /// <summary>點數/埠</summary>
        /// <remarks></remarks>
        int BitsPerPort { get; set; }
    }
}

