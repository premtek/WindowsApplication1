using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Premtek
{
    public static class CMath
    {
        /// <summary>垂足公式
        /// </summary>
        /// <param name="xo">直線上一點X</param>
        /// <param name="yo">直線上一點Y</param>
        /// <param name="slope">直線斜率</param>
        /// <param name="x">一點X</param>
        /// <param name="y">一點Y</param>
        /// <param name="xp">一點與直線的垂足座標X</param>
        /// <param name="yp">一點與直線的垂足座標Y</param>
        /// <returns></returns>
        public static bool GetFootPoint(decimal xo, decimal yo, decimal slope, decimal x, decimal y, out decimal xp, out decimal yp)
        {
            //起點與角度構成的線段 方程式為 X-mSlope*Y+mSlope*Ys-Xs=0

            decimal _A = 1;
            decimal _B = -slope;
            decimal _C = slope * yo - xo;
            xp = (_B * _B * x - _A * _B * y - _A * _C) / (_A * _A + _B * _B);
            yp = (-_A * _B * x + _A * _A * y - _B * _C) / (_A * _A + _B * _B);
            return true;
        }

        /// <summary>增加可整除保護的Sin轉換
        /// </summary>
        /// <param name="degree">度</param>
        /// <returns></returns>
        /// <remarks>主因載於避免應為整數的值呈現有小數造成數值不符</remarks>
        public static decimal Sin(decimal degree)
        {
            decimal _Remainder;//餘數
            int _Quotient;//商數
            decimal _Sin;

            _Remainder = degree % 90;
            _Quotient = (int)(degree / 90);
            if (_Remainder == 0)//餘數為0 90的倍數
            {
                switch (_Quotient / 4)
                {
                    case 0:
                        _Sin = 0;
                        break;
                    case 1:
                        _Sin = 1;
                        break;
                    case 2:
                        _Sin = 0;
                        break;
                    case 3:
                        _Sin = -1;
                        break;
                    case 4:
                        _Sin = 0;
                        break;
                    case -1:
                        _Sin = -1;
                        break;
                    case -2:
                        _Sin = 0;
                        break;
                    case -3:
                        _Sin = 1;
                        break;
                    case -4:
                        _Sin = 0;
                        break;
                    default://應該進不來
                        System.Diagnostics.Debug.Assert(false);
                        _Sin = 0;
                        break;
                }
            }
            else
            {
                _Sin = (decimal)Math.Sin((double)degree * Math.PI / 180);
            }
            if (_Sin > 1) _Sin = 1;//上下限保護
            if (_Sin < -1) _Sin = -1;//上下限保護
            return _Sin;
        }

        /// <summary>增加可整除保護的Cos轉換
        /// </summary>
        /// <param name="degree">度</param>
        /// <returns></returns>
        public static decimal Cos(decimal degree)
        {
            decimal _Remainder;//餘數
            int _Quotient;//商數
            decimal _Cos;

            _Remainder = degree % 90;
            _Quotient = (int)(degree / 90);
            if (_Remainder == 0)//餘數為0 90的倍數
            {
                switch (_Quotient / 4)
                {
                    case 0:
                        _Cos = 1;
                        break;
                    case 1:
                        _Cos = 0;
                        break;
                    case 2:
                        _Cos = -1;
                        break;
                    case 3:
                        _Cos = 0;
                        break;
                    case 4:
                        _Cos = 1;
                        break;
                    case -1:
                        _Cos = 0;
                        break;
                    case -2:
                        _Cos = -1;
                        break;
                    case -3:
                        _Cos = 0;
                        break;
                    case -4:
                        _Cos = 1;
                        break;
                    default://應該進不來
                        System.Diagnostics.Debug.Assert(false);
                        _Cos = 0;
                        break;
                }
            }
            else
            {
                _Cos = (decimal)Math.Cos((double)degree * Math.PI / 180);
            }
            if (_Cos > 1) _Cos = 1;//上下限保護
            if (_Cos < -1) _Cos = -1;//上下限保護
            return _Cos;
        }

        /// <summary>純座標旋轉(投影)
        /// </summary>
        /// <param name="Xi">輸入X向量(相對於旋轉中心)</param>
        /// <param name="Yi">輸入Y向量(相對於旋轉中心)</param>
        /// <param name="degree">角度</param>
        /// <param name="Xo">輸出X向量(相對於旋轉中心)</param>
        /// <param name="Yo">輸出Y向量(相對於旋轉中心)</param>
        /// <returns></returns>
        public static ErrorCode Rotation(decimal Xi, decimal Yi, decimal degree, out decimal Xo, out decimal Yo)
        {
            decimal _Cos = Cos(degree);
            decimal _Sin = Sin(degree);
            Xo = Xi * _Cos - Yi * _Sin;
            Yo = Xi * _Sin + Yi * _Cos;
            return ErrorCode.Success;
        }
        public static ErrorCode Rotation(decimal Xi, decimal Yi, decimal degreeX, decimal degreeY, out decimal Xo, out decimal Yo)
        {
            Xo = Xi * CMath.Cos(degreeX) - Yi * CMath.Sin(degreeY);
            Yo = Xi * CMath.Sin(degreeX) + Yi * CMath.Cos(degreeY);
            return ErrorCode.Success;
        }
        /// <summary>將輸入座標作平移,旋轉,平移,得輸出新座標XY
        /// </summary>
        /// <param name="degree">角度</param>
        /// <param name="Xc">旋轉中心</param>
        /// <param name="Yc">旋轉中心</param>
        /// <param name="Xi">輸入X座標</param>
        /// <param name="Yi">輸入Y座標</param>
        /// <param name="Xo">輸出X座標</param>
        /// <param name="Yo">輸出Y座標</param>
        /// <returns></returns>
        public static ErrorCode TranslationRotation(decimal degree, decimal Xc, decimal Yc, decimal Xi, decimal Yi, out decimal Xo, out decimal Yo)
        {
            decimal xic = (Xi - Xc);
            decimal yic = (Yi - Yc);
            decimal xoc, yoc;
            ErrorCode ret = Rotation(xic, yic, degree, out xoc, out yoc);
            Xo = xoc + Xc;
            Yo = yoc + Yc;
            return ret;
        }

        /// <summary>矩陣最小平方法(給定三組輸入輸出, 求轉換公式) ex. mm/Pixel
        /// </summary>
        /// <param name="Xi1">輸入CCD座標(Pixel)</param>
        /// <param name="Xi2">輸入CCD座標(Pixel)</param>
        /// <param name="Xi3">輸入CCD座標(Pixel)</param>
        /// <param name="Yi1">輸入CCD座標(Pixel)</param>
        /// <param name="Yi2">輸入CCD座標(Pixel)</param>
        /// <param name="Yi3">輸入CCD座標(Pixel)</param>
        /// <param name="Xo1">輸出馬達座標(mm)</param>
        /// <param name="Xo2">輸出馬達座標(mm)</param>
        /// <param name="Xo3">輸出馬達座標(mm)</param>
        /// <param name="Yo1">輸出馬達座標(mm)</param>
        /// <param name="Yo2">輸出馬達座標(mm)</param>
        /// <param name="Yo3">輸出馬達座標(mm)</param>
        /// <param name="A11">轉換矩陣參數</param>
        /// <param name="A12">轉換矩陣參數</param>
        /// <param name="A21">轉換矩陣參數</param>
        /// <param name="A22">轉換矩陣參數</param>
        /// <param name="B11">轉換矩陣參數</param>
        /// <param name="B21">轉換矩陣參數</param>
        /// <returns></returns>
        public static ErrorCode LeastSquareInMatrixForm(double Xi1, double Xi2, double Xi3, double Yi1, double Yi2, double Yi3, double Xo1, double Xo2, double Xo3, double Yo1, double Yo2, double Yo3, out double A11, out double A12, out double A21, out double A22, out double B11, out double B21)
        {
            //矩陣最小平方 求轉移函數
            //X = (A^T A)-1 A^T Y

            double ATA_a11 = Math.Pow(Xi1, 2) + Math.Pow(Xi2, 2) + Math.Pow(Xi3, 2);
            double ATA_a12 = Xi1 * Yi1 + Xi2 * Yi2 + Xi3 * Yi3;
            double ATA_a13 = Xi1 + Xi2 + Xi3;
            double ATA_a21 = Xi1 * Yi1 + Xi2 * Yi2 + Xi3 * Yi3;
            double ATA_a22 = Math.Pow(Yi1, 2) + Math.Pow(Yi2, 2) + Math.Pow(Yi3, 2);
            double ATA_a23 = Yi1 + Yi2 + Yi3;
            double ATA_a31 = Xi1 + Xi2 + Xi3;
            double ATA_a32 = Yi1 + Yi2 + Yi3;
            double ATA_a33 = 3;
            double detA = ATA_a11 * ATA_a22 * ATA_a33 + ATA_a12 * ATA_a23 * ATA_a31 + ATA_a13 * ATA_a21 * ATA_a32 - ATA_a13 * ATA_a22 * ATA_a31 - ATA_a11 * ATA_a23 * ATA_a32 - ATA_a12 * ATA_a21 * ATA_a33;

            //無行列式值, 無法求解
            if (detA == 0)
            {
                A11 = 1;
                A22 = 1;
                A12 = 0;
                A21 = 0;
                B11 = 0;
                B21 = 0;
                return ErrorCode.Failed;
            }

            //伴隨矩陣
            double Adj_a11 = ATA_a22 * ATA_a33 - ATA_a23 * ATA_a32;
            double Adj_a12 = ATA_a13 * ATA_a32 - ATA_a12 * ATA_a33;
            double Adj_a13 = ATA_a12 * ATA_a23 - ATA_a13 * ATA_a22;
            double Adj_a21 = ATA_a23 * ATA_a31 - ATA_a21 * ATA_a33;
            double Adj_a22 = ATA_a11 * ATA_a33 - ATA_a13 * ATA_a31;
            double Adj_a23 = ATA_a13 * ATA_a21 - ATA_a11 * ATA_a23;
            double Adj_a31 = ATA_a21 * ATA_a32 - ATA_a22 * ATA_a31;
            double Adj_a32 = ATA_a12 * ATA_a31 - ATA_a11 * ATA_a32;
            double Adj_a33 = ATA_a11 * ATA_a22 - ATA_a12 * ATA_a21;

            //ATA的反矩陣 (A^T A) -1
            double InvA_a11 = Adj_a11 / detA;
            double InvA_a12 = Adj_a12 / detA;
            double InvA_a13 = Adj_a13 / detA;
            double InvA_a21 = Adj_a21 / detA;
            double InvA_a22 = Adj_a22 / detA;
            double InvA_a23 = Adj_a23 / detA;
            double InvA_a31 = Adj_a31 / detA;
            double InvA_a32 = Adj_a32 / detA;
            double InvA_a33 = Adj_a33 / detA;

            //A^T Y
            double item1 = Xi1 * Xo1 + Xi2 * Xo2 + Xi3 * Xo3;
            double item2 = Yi1 * Xo1 + Yi2 * Xo2 + Yi3 * Xo3;
            double item3 = Xo1 + Xo2 + Xo3;
            A11 = InvA_a11 * item1 + InvA_a12 * item2 + InvA_a13 * item3;
            A12 = InvA_a21 * item1 + InvA_a22 * item2 + InvA_a23 * item3;
            B11 = InvA_a31 * item1 + InvA_a32 * item2 + InvA_a33 * item3;

            A11 = Math.Round(A11, 5);
            A12 = Math.Round(A12, 5);
            B11 = Math.Round(B11, 5);

            //A^T Y
            double item4 = Xi1 * Yo1 + Xi2 * Yo2 + Xi3 * Yo3;
            double item5 = Yi1 * Yo1 + Yi2 * Yo2 + Yi3 * Yo3;
            double item6 = Yo1 + Yo2 + Yo3;
            A21 = InvA_a11 * item4 + InvA_a12 * item5 + InvA_a13 * item6;
            A22 = InvA_a21 * item4 + InvA_a22 * item5 + InvA_a23 * item6;
            B21 = InvA_a31 * item4 + InvA_a32 * item5 + InvA_a33 * item6;

            A21 = Math.Round(A21, 5);
            A22 = Math.Round(A22, 5);
            B21 = Math.Round(B21, 5);

            return ErrorCode.Success;
        }

        /// <summary>取得兩點距離
        /// </summary>
        /// <param name="X1">第一點座標</param>
        /// <param name="Y1">第一點座標</param>
        /// <param name="X2">第二點座標</param>
        /// <param name="Y2">第二點座標</param>
        /// <returns>兩點距離</returns>
        public static decimal GetDistance(decimal X1, decimal Y1, decimal X2, decimal Y2)
        {
            double _X1 = (double)X1;
            double _X2 = (double)X2;
            double _Y1 = (double)Y1;
            double _Y2 = (double)Y2;
            double dis = Math.Sqrt((_X1 - _X2) * (_X1 - _X2) + (_Y1 - _Y2) * (_Y1 - _Y2));
            return (decimal)dis;
        }

        /// <summary>求三點求 外接圓圓心
        /// </summary>
        /// <param name="x1">第一點座標</param>
        /// <param name="y1">第一點座標</param>
        /// <param name="x2">第二點座標</param>
        /// <param name="y2">第二點座標</param>
        /// <param name="x3">第三點座標</param>
        /// <param name="y3">第三點座標</param>
        /// <param name="xc">圓心座標</param>
        /// <param name="yc">圓心座標</param>
        /// <returns>成功 Success, 失敗:Failed</returns>
        public static ErrorCode GetCircumCenter(decimal x1, decimal y1, decimal x2, decimal y2, decimal x3, decimal y3, out decimal xc, out decimal yc)
        {
            decimal dis1 = CMath.GetDistance(x1, y1, x2, y2);
            decimal dis2 = CMath.GetDistance(x1, y1, x3, y3);
            decimal dis3 = CMath.GetDistance(x2, y2, x3, y3);
            if ((dis1 == 0) || (dis2 == 0) || (dis3 == 0))//任兩點距離為0, 共點.
            {
                xc = 0;
                yc = 0;
                return ErrorCode.Failed;
            }
            if (CMath.Is3PointCoLinear(x1, y1, x2, y2, x3, y3))//三點共線 半徑無線大 沒有圓心
            {
                xc = 0;
                yc = 0;
                return ErrorCode.Failed;
            }
           
            //將三組資料待入圓心公式, 整理為AX+BY+C=0形式
            decimal _A1 = 2 * (x2 - x1);
            decimal _B1 = 2 * (y2 - y1);
            decimal _C1 = x1 * x1 + y1 * y1 - x2 * x2 - y2 * y2;
            decimal _A2 = 2 * (x3 - x1);
            decimal _B2 = 2 * (y3 - y1);
            decimal _C2 = x1 * x1 + y1 * y1 - x3 * x3 - y3 * y3;

            if ((_A1 * _B2) == (_A2 * _B1))//線上無解
            {
                xc = 0;
                yc = 0;
                return ErrorCode.Failed;
            }

            xc = (_B1 * _C2 - _B2 * _C1) / (_A1 * _B2 - _B1 * _A2);
            if (_B1 != 0)
            {
                yc = -(_A1 * xc + _C1) / _B1;
            }
            else if (_B2 != 0)
            {
                yc = -(_A2 * xc + _C2) / _B2;
            }
            else
            {
                yc = (_C1 * _A2 - _C2 * _A1) / (_A1 * _B2 - _B1 * _A2);
            }
            return ErrorCode.Success;
        }


        /// <summary>三點求弧方向
        /// </summary>
        /// <param name="Xc">弧心座標</param>
        /// <param name="Yc">弧心座標</param>
        /// <param name="Xs">起點座標</param>
        /// <param name="Ys">起點座標</param>
        /// <param name="Xe">終點座標</param>
        /// <param name="Ye">終點座標</param>
        /// <returns></returns>
        public static ArcCircleDirection GetArcDirection(decimal Xc, decimal Yc, decimal Xs, decimal Ys, decimal Xe, decimal Ye)
        {
            ArcCircleDirection _Clockwise = ArcCircleDirection.CounterClockWise;
            double _Xsc = (double)(Xs - Xc);
            double _Ysc = (double)(Ys - Yc);
            double _Xec = (double)(Xe - Xc);
            double _Yec = (double)(Ye - Yc);

            double _Cross = 0;

            double _Lsc = Math.Sqrt(_Xsc * _Xsc + _Ysc * _Ysc);
            double _Lec = Math.Sqrt(_Xec * _Xec + _Yec * _Yec);
            if ((_Lsc * _Lec) > 0)
            {
                _Cross = ((_Xsc * _Yec) - (_Xec * _Ysc)) / (_Lsc * _Lec);
            }
            else
            {
                _Cross = 0;
            }

            if (_Cross > 0)
            {
                _Clockwise = ArcCircleDirection.CounterClockWise;
            }
            else
            {
                _Clockwise = ArcCircleDirection.ClockWise;
                //Counterclockwise
            }
            return _Clockwise;
        }

        /// <summary>三點求圓心角
        /// </summary>
        /// <param name="center"></param>
        /// <param name="start"></param>
        /// <param name="final"></param>
        /// <param name="clockwise"></param>
        /// <returns></returns>
        public static double GetCentralAngle(decimal Xc, decimal Yc, decimal Xs, decimal Ys, decimal Xe, decimal Ye)
        {
            ArcCircleDirection _direction = GetArcDirection(Xc, Yc, Xs, Ys, Xe, Ye);

            double _Xsc = (double)(Xs - Xc);
            double _Ysc = (double)(Ys - Yc);
            double _Xec = (double)(Xe - Xc);
            double _Yec = (double)(Ye - Yc);

            double _Lsc = Math.Sqrt(_Xsc * _Xsc + _Ysc * _Ysc);//起點與圓心距離
            double _Lec = Math.Sqrt(_Xec * _Xec + _Yec * _Yec);//終點與圓心距離
            if (_Lsc == 0) return double.NaN;//不應出現的狀況
            if (_Lec == 0) return double.NaN;//不應出現的狀況

            double _angle = 0;//輸出角度
            double _cos = 0;//ArcCos的輸入值

            double _AngleLargeThanPi = 0;

            _cos = (_Xsc * _Xec + _Ysc * _Yec) / (_Lsc * _Lec);
            if (_cos < -1)
            {
                _cos = -1;
            }
            else if (_cos > 1)
            {
                _cos = 1;
            }

            _AngleLargeThanPi = _Xsc * _Yec - _Xec * _Ysc;

            if (_AngleLargeThanPi > 0)
            {
                _angle = 180.0 * Math.Acos(_cos) / Math.PI;
            }
            else
            {
                _angle = 360.0 - Math.Acos(_cos) * 180.0 / Math.PI;
            }

            if (_direction == ArcCircleDirection.ClockWise)
            {
                _angle = 360.0 - _angle;
            }

            return _angle;
        }

        /// <summary>三點共線判定
        /// </summary>
        /// <param name="x1">第一點座標</param>
        /// <param name="y1">第一點座標</param>
        /// <param name="x2">第二點座標</param>
        /// <param name="y2">第二點座標</param>
        /// <param name="x3">第三點座標</param>
        /// <param name="y3">第三點座標</param>
        /// <returns></returns>
        public static bool Is3PointCoLinear(decimal x1, decimal y1, decimal x2, decimal y2, decimal x3, decimal y3)
        {
            if ((x2 - x1) * (y3 - y1) == (x3 - x1) * (y2 - y1))//取兩向量斜率判定
                return true;

            if ((x1 - x2) * (y3 - y2) == (x3 - x2) * (y1 - y2))//取兩向量斜率判定
                return true;

            if ((x1 - x3) * (y2 - y3) == (x2 - x3) * (y1 - y3))//取兩向量斜率判定
                return true;
            return false;
        }
    }
}
