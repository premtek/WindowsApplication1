using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    /// <summary> 虛擬DI卡 </summary>
    public class CDOVirtual : IDOInterface
    {

        public void Close()
        {
        }
        public bool IsInitialOK { get; set; }

        public bool Initial(string deviceDescription)
        {
            return true;
        }

        public int Write(int port, ref byte bytDiData)
        {
            return 0;
        }

        public bool DOOutput(int port, int bit, bool value)
        {
            return true;
        }

        public int PortPerCard { get; set; }

        public int BitsPerPort { get; set; }

        public CDOVirtual()
        {
            BitsPerPort = 8;
            PortPerCard = 4;
        }
    }
}
