using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Premtek
{
    /// <summary>定位點位置與場景
    /// </summary>
    public class CRecipeAlignPos:IDisposable
    {
        /// <summary>定位點位置
        /// </summary>
        public CPosPoint Pos;
        /// <summary>場景
        /// </summary>
        public string Secne;
        /// <summary>忙碌/計算中
        /// </summary>
        public bool IsBusy;
        /// <summary>複製</summary>
        /// <returns></returns>
        public CRecipeAlignPos Clone()
        {
            CRecipeAlignPos mTemp = new CRecipeAlignPos();
            mTemp.Pos = this.Pos.Clone();
            mTemp.Secne = this.Secne;
            return mTemp;
        }

        #region "IDisposable"

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).

                }
                else
                {
                    Pos = null;
                    //Debug.Print("CRecipeAlignPos Dispose(False)");
                }
                disposed = true;
            }
        }

        /// <summary>資源釋放
        /// </summary>
         ~CRecipeAlignPos()
        {
            Dispose(false);
        }
        #endregion

         public CRecipeAlignPos()
         {
             Pos = new CPosPoint();
             Secne = "";
         }
    }

}
