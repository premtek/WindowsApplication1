using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Premtek
{
    public static class CIni
    {
        [DllImport("Kernel32.DLL", EntryPoint = "WritePrivateProfileStringW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr WritePrivateProfileString(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);

        [DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern int GetPrivateProfileString(String section, String key, String defaultValue, StringBuilder retVal, int size, String filePath);

        /// <summary>字串讀取
        /// </summary>
        /// <param name="section">類別</param>
        /// <param name="keyName">名稱</param>
        /// <param name="fileName">檔名</param>
        /// <param name="defaultValue">預設值</param>
        /// <returns></returns>
        public static string ReadIniString(string section, string keyName, string fileName, string defaultValue = "")
        {
            try
            {
                const int loadLength = 1024;
                StringBuilder returnString = new StringBuilder(255);
                int length = GetPrivateProfileString(section, keyName, defaultValue, returnString, loadLength, fileName);
                string returnData = returnString.ToString();
                if (length >= returnData.Length)
                {
                    return returnData;
                }
                else
                {
                    return returnData.Remove(length);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return "";
            }
        }

        public static string ReadIniString(string section, string keyName, string fileName, int defaultValue = 0)
        {
            return ReadIniString(section, keyName, fileName, defaultValue.ToString());
        }
        public static string ReadIniString(string section, string keyName, string fileName, decimal defaultValue = decimal.Zero)
        {
            return ReadIniString(section, keyName, fileName, defaultValue.ToString());
        }
        /// <summary>字串儲存
        /// </summary>
        /// <param name="section"></param>
        /// <param name="keyName"></param>
        /// <param name="keyValue"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static long SaveIniString(string section, string keyName, string keyValue, string fileName)
        {
            try
            {
                IntPtr ret = WritePrivateProfileString(section, keyName, keyValue, fileName);
                return ret.ToInt32();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return 1;
            }

        }

        public static long SaveIniString(string section, string keyName, int keyValue, string fileName)
        {
            return SaveIniString(section, keyName, keyValue.ToString(), fileName);
        }
        public static long SaveIniString(string section, string keyName, decimal keyValue, string fileName)
        {
            return SaveIniString(section, keyName, keyValue.ToString(), fileName);
        }
        public static long SaveIniString(string section, string keyName, double keyValue, string fileName)
        {
            return SaveIniString(section, keyName, keyValue.ToString(), fileName);
        }
        public static long SaveIniString<T>(string section, string keyName, T keyValue, string fileName)
        {
            return SaveIniString(section, keyName, keyValue.ToString(), fileName);
        }
    }
}
