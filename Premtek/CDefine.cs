using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
/// <summary>系統命令</summary>
    /// <remarks></remarks>
    public enum eSysCommand
    {
        /// <summary>無命令</summary>
        /// <remarks></remarks>
        None = 0,
        /// <summary>復歸</summary>
        /// <remarks></remarks>
        Home = 1,
        /// <summary>自動生產</summary>
        /// <remarks></remarks>
        AutoRun = 2,
        /// <summary>擦膠</summary>
        /// <remarks></remarks>
        ClearGlue = 3,
        /// <summary>排膠</summary>
        /// <remarks></remarks>
        Purge = 4,
        /// <summary>CCD閥自動校正</summary>
        /// <remarks></remarks>
        CCDValveAutoCalibrationXY = 5,
        /// <summary>單邊微量天平秤重</summary>
        /// <remarks></remarks>
        WeightUnit = 6,
        /// <summary>單邊手動微量天平秤重</summary>
        /// <remarks></remarks>
        ManuallyWeightUnit = 7,
        /// <summary>單邊換膠</summary>
        /// <remarks></remarks>
        ChangeGlue = 8,
        /// <summary>單邊閥自動測高</summary>
        /// <remarks></remarks>
        DispenserAutoSearch = 9,
        /// <summary>單邊雷射測高</summary>
        /// <remarks></remarks>
        LaserReader = 10,
        /// <summary>單邊CCD定位</summary>
        /// <remarks></remarks>
        CCDFix = 11,
        /// <summary>單邊CCD後檢</summary>
        /// <remarks></remarks>
        CCDSCanGlue = 12,
        /// <summary>單邊點膠</summary>
        /// <remarks></remarks>
        Dispensing = 13,
        /// <summary>產品資料載入</summary>
        /// <remarks></remarks>
        ProductLoading = 14,
        /// <summary>退料</summary>
        /// <remarks></remarks>
        ProductUnload = 15,
        /// <summary>進料</summary>
        /// <remarks></remarks>
        Loading = 16,
        /// <summary>A機進料</summary>
        /// <remarks></remarks>
        LoadA = 17,
        /// <summary>B機進料</summary>
        /// <remarks></remarks>
        LoadB = 18,
        /// <summary>B機退料 </summary>
        /// <remarks></remarks>
        UnloadB = 19,
        /// <summary>A機退料</summary>
        /// <remarks></remarks>
        UnloadA = 20,
        /// <summary>點膠前動作(秤重、Purge)</summary>
        /// <remarks></remarks>
        PrevDispense = 21,
        /// <summary>CCD-閥全自動校正(XYZ)</summary>
        /// <remarks></remarks>
        CCDValveFullAutoCalibration = 22,
        /// <summary>手動生產模式</summary>
        /// <remarks></remarks>
        ManualProduceMode = 23,
        // ''' <summary>單動模式[可單獨執行特定動作，例如：測重、清膠等等]</summary>
        // ''' <remarks></remarks>
        //SingleAction = 24
        /// <summary>[接續流程(接續之前沒做完的生產流程)]</summary>
        /// <remarks></remarks>
        ContinueLastRun = 25,
        /// <summary>[強制退料]</summary>
        /// <remarks></remarks>
        AbnormalUnload = 26,
        /// <summary>A機復歸</summary>
        /// <remarks></remarks>
        HomeA = 27,
        /// <summary>B機復歸</summary>
        /// <remarks></remarks>
        HomeB = 28,
        /// <summary>安全</summary>
        /// <remarks></remarks>
        Safe = 29,
        /// <summary>[監控]</summary>
        /// <remarks></remarks>
        Monitor = 30

    }

   
    public class CDefine
    {

    }
}
