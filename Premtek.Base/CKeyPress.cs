using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace Premtek.Base
{    
    public class CKeyPress
    {
        /// <summary>KeyPress正十進位輸入檢查</summary>
        /// <param name="textbox1"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        public static void CheckUDecimal(TextBox textbox1, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                if (textbox1.SelectionStart > 0)
                {
                    string input = textbox1.Text.Substring(0, textbox1.SelectionStart - 1) + textbox1.Text.Substring(textbox1.SelectionStart);
                    //刪除字元後是否可辨識
                    decimal result = 0;
                    if (decimal.TryParse(input, out result) == false)
                    {
                        e.Handled = true;
                        textbox1.Text = "";
                        //不可辨識則清除
                    }
                    else
                    {
                        e.Handled = false;
                    }
                }
                else
                {
                    e.Handled = false;
                }

                return;
            }
            else
            {
                switch (Asc(e.KeyChar))
                {
                    case 46:
                        //.
                        //已經有
                        if (textbox1.Text.Contains("."))
                        {
                            e.Handled = true;
                            return;
                        }
                        else
                        {
                            string input = textbox1.Text.Substring(0, textbox1.SelectionStart) + e.KeyChar + textbox1.Text.Substring(textbox1.SelectionStart);
                            //增加字元後是否可辨識
                            decimal result = 0;
                            if (decimal.TryParse(input, out result) == false)
                            {
                                e.Handled = true;
                                textbox1.Text = "";
                                //不可辨識則清除
                            }
                            else
                            {
                                e.Handled = false;
                            }

                            return;
                        }
                       
                    default:
                        e.Handled = true;

                        break;
                }
            }
        }
        /// <summary>KeyPress十進位輸入檢查</summary>
        /// <param name="textbox1"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        public static void CheckDecimal(TextBox textbox1, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                if (textbox1.SelectionStart > 0)
                {
                    string input = textbox1.Text.Substring(0, textbox1.SelectionStart - 1) + textbox1.Text.Substring(textbox1.SelectionStart);
                    //刪除字元後是否可辨識
                    decimal result = 0;
                    if (decimal.TryParse(input, out result) == false)
                    {
                        e.Handled = true;
                        textbox1.Text = "";
                        //不可辨識則清除
                    }
                    else
                    {
                        e.Handled = false;
                    }
                }
                else
                {
                    e.Handled = false;
                }

                return;
            }
            else
            {
                switch (Asc(e.KeyChar))
                {
                    case 45:
                        //-
                        //已經有
                        if (textbox1.Text.Contains("-"))
                        {
                            e.Handled = true;
                            return;
                            //純負不可以
                        }
                        else if (textbox1.Text.Length <= 0)
                        {
                            e.Handled = true;
                        }
                        else if (textbox1.SelectionStart == 0)
                        {
                            e.Handled = false;
                            return;
                        }
                        else
                        {
                            e.Handled = true;
                            return;
                        }

                        break;
                    case 46:
                        //.
                        //已經有
                        if (textbox1.Text.Contains("."))
                        {
                            e.Handled = true;
                            return;
                        }
                        else
                        {
                            string input = textbox1.Text.Substring(0, textbox1.SelectionStart) + e.KeyChar + textbox1.Text.Substring(textbox1.SelectionStart);
                            //增加字元後是否可辨識
                            decimal result = 0;
                            if (decimal.TryParse(input, out result) == false)
                            {
                                e.Handled = true;
                                textbox1.Text = "";
                                //不可辨識則清除
                            }
                            else
                            {
                                e.Handled = false;
                            }

                            return;
                        }
                   
                    default:
                        e.Handled = true;

                        break;
                }
            }
        }
        /// <summary>KeyPress整數輸入檢查</summary>
        /// <param name="textbox1"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        public static void CheckInteger(TextBox textbox1, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) | char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                switch (Asc(e.KeyChar))
                {
                    case 45:
                        //-
                        //已經有
                        if (textbox1.Text.Contains("-"))
                        {
                            e.Handled = true;
                            return;
                        }
                        else if (textbox1.Text.Length <= 0)
                        {
                            e.Handled = true;
                        }
                        else if (textbox1.SelectionStart == 0)
                        {
                            e.Handled = false;
                            return;
                        }
                        else
                        {
                            e.Handled = true;
                        }
                        break;
                    default:
                        e.Handled = true;
                        break;
                }
                //轉換失敗
                int result = 0;
                if (int.TryParse(textbox1.Text,out result) == false)
                {
                    e.Handled = true;
                    return;
                }
            }
        }
        /// <summary>KeyPress正整數輸入檢查</summary>
        /// <param name="textbox1"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        public static void CheckUInteger(TextBox textbox1, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) | char.IsControl(e.KeyChar))
            {
                e.Handled = false;
                return;
            }
            else
            {
                e.Handled = true;
            }
        }

        private static int Asc(char character)
        {
            return (int)character;
        }

        private static string Chr(int asciiCode)
        {
            
            if (asciiCode >= 0 && asciiCode <= 255)
            {

                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();



                byte[] byteArray = new byte[] { (byte)asciiCode };

                string strCharacter = asciiEncoding.GetString(byteArray);



                return (strCharacter);

            }

            else
            {

                throw new ApplicationException("ASCII Code is not valid.");

            }

        }

    }

}
