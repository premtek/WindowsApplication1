using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{

    /// <summary>虛擬DI卡</summary>
    /// <remarks></remarks>
    public class CDIVirtual : IDIInterface
    {
        public void Close()
        {
        }

        public bool Initial(string deviceDescription)
        {
            return true;
        }

        public int Read(int port, ref byte bytDiData)
        {
            return 0;
        }

        public int BitsPerPort { get; set; }

        public int PortPerCard { get; set; }
        public CDIVirtual()
        {
            BitsPerPort = 0;
            PortPerCard = 0;
        }
    }

}
