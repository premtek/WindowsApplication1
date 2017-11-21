using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using ProjectCore;
namespace Premtek.Base
{
	public static class MCommonLaserReader
	{
		//' ''' <summary>[雷射干涉儀]</summary>
		//' ''' <remarks></remarks>

		public static CLaserReaderCollection gLaserReaderCollection = new CLaserReaderCollection();
		/// <summary>[通訊異常後 允取再從送幾次]</summary>
		/// <remarks></remarks>
		public const int gLaserReaderCmdMaxFailCounts = 3;

	}
}
