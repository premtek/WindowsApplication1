using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    public static class CFileMisc
    {
        /// <summary>
        /// 是否不合法檔名
        /// </summary>
        /// <param name="sFileName">檔名</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool IsInvalidPath(string sFileName)
        {
            //'檔名限制定義
            string[] sError = { "@", "?", "*", "!", "/", "\\", "|" };
            char[] _InvalidPathChars = System.IO.Path.GetInvalidPathChars();
            char[] _InvalidFileNameChars = System.IO.Path.GetInvalidFileNameChars();
            try
            {
                for (int i = 0; i <= sError.Length - 1; i++)
                {
                    if (sFileName.Contains(sError[i]) == true)
                    {
                        return true;
                    }
                }
                for (int i = 0; i <= _InvalidPathChars.GetUpperBound(0); i++)
                {
                    if (sFileName.Contains(_InvalidPathChars[i]))
                    {
                        return true;
                    }
                }
                for (int i = 0; i <= _InvalidFileNameChars.GetUpperBound(0); i++)
                {
                    if (sFileName.Contains(_InvalidFileNameChars[i]))
                    {
                        return true;
                    }
                }
                return false;

            }
            catch
            {
                return false;
            }

        }

    }
}
