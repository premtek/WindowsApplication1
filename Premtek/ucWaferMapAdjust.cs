using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapData;
using ProjectCore;
using Premtek.Base;

namespace Premtek
{
    public partial class ucWaferMapAdjust : UserControl
    {
        /// <summary>外部配接Sys
        /// </summary>
        public MSystemParameter.sSysParam Sys;
        /// <summary>外部配接軸卡
        /// </summary>
        public Premtek.Base.CMotionCollection Motion;
        /// <summary>位置移動開始
        /// </summary>
        public event EventHandler MoveStart;
        /// <summary>位置變更事件
        /// </summary>
        public event EventHandler PosChanged;

        /// <summary>參數數值變更
        /// </summary>
        public event EventHandler ValueChanged;

        /// <summary>NodeMap對應位置(做為繪圖起點)
        /// </summary>
        /// <remarks>注意從1開始, FirstDieIndex從0開始</remarks>
        public int NodeStaringX = 1;
        /// <summary>NodeMap對應位置(做為繪圖起點)
        /// </summary>
        /// <remarks>注意從1開始, FirstDieIndex從0開始</remarks>
        public int NodeStaringY = 1;

        public ucWaferMapAdjust()
        {
            InitializeComponent();
        }

        public bool IsUpdating = false;

        /// <summary>X方向數量
        /// </summary>
        public int CountX
        {
            get
            {
                return (int)nmuCountX.Value;
            }
            set
            {
                ControlMisc.SetNumericValue(ref nmuCountX, value);
            }
        }

        /// <summary>Y方向數量
        /// </summary>
        public int CountY
        {
            get
            {
                return (int)nmuCountY.Value;
            }
            set
            {
                ControlMisc.SetNumericValue(ref nmuCountY, value);
            }
        }

        /// <summary>Wafer圓中心
        /// </summary>
        public decimal WaferCenterX { get; set; }
        /// <summary>Wafer圓中心
        /// </summary>
        public decimal WaferCenterY { get; set; }

        /// <summary>Wafer有效半徑
        /// </summary>
        public decimal WaferEffectiveRadius { get; set; }
        /// <summary>間距X
        /// </summary>
        public decimal PitchX { get; set; }
        /// <summary>間距Y
        /// </summary>
        public decimal PitchY { get; set; }
        /// <summary>Die X方向尺寸
        /// </summary>
        public decimal DieSizeX { get; set; }
        /// <summary>Die Y方向尺寸
        /// </summary>
        public decimal DieSizeY { get; set; }
        /// <summary>X方向 Wafer傾斜角度(Deg)
        /// </summary>
        public decimal WaferAngle { get; set; }
        /// <summary>Y方向 Wafer傾斜角度(Deg)
        /// </summary>
        public decimal WaferAngleY { get; set; }
        /// <summary>首顆位置(定位點教導位置)
        /// </summary>
        public decimal FirstDiePosX { get; set; }
        /// <summary>首顆位置(定位點教導位置)
        /// </summary>
        public decimal FirstDiePosY { get; set; }
        /// <summary>首顆位置(定位點教導位置)
        /// </summary>
        public decimal FirstDiePosZ { get; set; }
        /// <summary>基準點教導位置
        /// </summary>
        public decimal OriginDiePosX { get; set; }
        /// <summary>基準點教導位置
        /// </summary>
        public decimal OriginDiePosY { get; set; }
        /// <summary>元件左下角位置X
        /// </summary>
        public decimal DieLeftDownCornerX { get; set; }
        /// <summary>元件左下角位置Y
        /// </summary>
        public decimal DieLeftDownCornerY { get; set; }
        /// <summary>首顆索引
        /// </summary>
        public int FirstDieIndexX { get; set; }
        /// <summary>首顆索引
        /// </summary>
        public int FirstDieIndexY { get; set; }
        bool _IsCorrect;
        /// <summary>資料正確
        /// </summary>
        public bool IsCorrect
        {
            get
            {
                return _IsCorrect;
            }
        }

        public void ShowProperty()
        {
            _IsCorrect = true;
            if (ControlMisc.SetNumericValue(ref nmuCountX, CountX) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuCountY, CountY) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuRadius, WaferEffectiveRadius) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuFirstDieIndexX, FirstDieIndexX + 1) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            if (ControlMisc.SetNumericValue(ref nmuFirstDieIndexY, FirstDieIndexY + 1) != ErrorCode.Success)
            {
                _IsCorrect = false;
            }
            lblFristDiePosX.Text = "X= " + FirstDiePosX.ToString("0.###") + " mm";
            lblFristDiePosY.Text = "Y= " + FirstDiePosY.ToString("0.###") + " mm";

        }

        public decimal TranslatedLeftUpperPosX
        {
            get
            {
                return _TranslatedLeftUpperPosX;
            }
        }
        public decimal TranslatedLeftUpperPosY
        {
            get
            {
                return _TranslatedLeftUpperPosY;
            }
        }
        /// <summary>(0,0)左上角位置(正)
        /// </summary>
        decimal _TranslatedLeftUpperPosX, _TranslatedLeftUpperPosY;
        /// <summary>依未旋轉前資料推算首顆位置(Align)
        /// </summary>
        /// <param name="originPosX"></param>
        /// <param name="originPosY"></param>
        /// <param name="idxX">從0開始</param>
        /// <param name="idxY">從0開始</param>
        /// <param name="firstDiePosX"></param>
        /// <param name="firstDiePosY"></param>
        /// <returns></returns>
        public bool GetFirstDiePos(decimal originPosX, decimal originPosY, int idxX, int idxY, ref decimal firstDiePosX, ref decimal firstDiePosY)
        {
            decimal posX = originPosX + PitchX * idxX;
            decimal posY = originPosY - PitchY * idxY;
            decimal _RotatedPosX = 0;
            decimal _RotatedPosY = 0;
            CMath.Rotation(posX - WaferCenterX, posY - WaferCenterY, WaferAngle,WaferAngleY, out _RotatedPosX, out _RotatedPosY);
            firstDiePosX = _RotatedPosX + WaferCenterX;
            firstDiePosY = _RotatedPosY + WaferCenterY;
            return true;
        }
        /// <summary>左下角元件位置參考點(正)
        /// </summary>
        decimal _RegularDieLeftDownPosX = 0;
        /// <summary>左下角元件位置參考點(正)
        /// </summary>
        decimal _RegularDieLeftDownPosY = 0;

        public void UpdateUI()
        {
            if (IsUpdating)
            {
                return;
            }
            IsUpdating = true;
            _IsCorrect = true;

            decimal WaferX_NEL;//Wafer有效範圍X軸負極限
            decimal WaferY_PEL;//Wafer有效範圍Y軸正極限

            WaferX_NEL = WaferCenterX - WaferEffectiveRadius;
            WaferY_PEL = WaferCenterY + WaferEffectiveRadius;

            
            CMath.Rotation(DieLeftDownCornerX - WaferCenterX, DieLeftDownCornerY - WaferCenterY, -WaferAngle,-WaferAngleY, out _RegularDieLeftDownPosX, out _RegularDieLeftDownPosY);//先把歪的位置轉正
            _RegularDieLeftDownPosX += WaferCenterX;
            _RegularDieLeftDownPosY += WaferCenterY;
            if (PitchX != 0)
            {
                decimal mOffsetX = (_RegularDieLeftDownPosX - WaferX_NEL) % PitchX;
                if (mOffsetX < 0) mOffsetX += PitchX;
                _TranslatedLeftUpperPosX = WaferX_NEL + mOffsetX;

            }
            else
            {
                _TranslatedLeftUpperPosX = _RegularDieLeftDownPosX;
            }
            if (PitchY != 0)
            {
                decimal mOffsetY = (WaferY_PEL - (_RegularDieLeftDownPosY + DieSizeY  )) % PitchY;
                if (mOffsetY < 0) mOffsetY += PitchY;
                _TranslatedLeftUpperPosY = WaferY_PEL - mOffsetY;
            }
            else
            {
                _TranslatedLeftUpperPosY = _RegularDieLeftDownPosY + DieSizeY;
            }


            decimal _RegularFirstDiePosX = 0;
            decimal _RegularFirstDiePosY = 0;
            CMath.Rotation(FirstDiePosX, FirstDiePosY, -WaferAngle, -WaferAngleY, out _RegularFirstDiePosX, out _RegularFirstDiePosY);//先把歪的位置轉正

            if (PitchX != 0)
            {
                
                FirstDieIndexX = Convert.ToInt32(Math.Floor((_RegularFirstDiePosX - _TranslatedLeftUpperPosX) / PitchX));
            }
            else
            {
                FirstDieIndexX = 0;
            }
            if (PitchY != 0)
            {
                FirstDieIndexY = Convert.ToInt32(Math.Floor((_TranslatedLeftUpperPosY - _RegularFirstDiePosY) / PitchY));
            }
            else
            {
                FirstDieIndexY = 0;
            }

            decimal _RotatedPosX, _RotatedPosY;

            CMath.Rotation(_TranslatedLeftUpperPosX - WaferCenterX, _TranslatedLeftUpperPosY - WaferCenterY, WaferAngle, WaferAngleY, out _RotatedPosX, out _RotatedPosY);
            OriginDiePosX = WaferCenterX + _RotatedPosX;
            OriginDiePosY = WaferCenterY + _RotatedPosY;

            //lblCCDPosX.Text = "X= " + OriginDiePosX.ToString("0.###") + " mm";
            //lblCCDPosY.Text = "Y= " + OriginDiePosY.ToString("0.###") + " mm";
            lblFristDiePosX.Text = "X= " + FirstDiePosX.ToString("0.###") + " mm";
            lblFristDiePosY.Text = "Y= " + FirstDiePosY.ToString("0.###") + " mm";

            ControlMisc.SetNumericValue(ref nmuFirstDieIndexX, FirstDieIndexX + 1);
            ControlMisc.SetNumericValue(ref nmuFirstDieIndexY, FirstDieIndexY + 1);

            if (FirstDieIndexX >= nmuCountX.Value)
            {
                nmuCountX.BackColor = Color.Red;
                _IsCorrect = false;
            }
            else
            {
                nmuCountX.BackColor = Color.White;
            }
            if (FirstDieIndexY >= nmuCountY.Value)
            {
                nmuCountY.BackColor = Color.Red;
                _IsCorrect = false;
            }
            else
            {
                nmuCountY.BackColor = Color.White;
            }

            mapData.Substrates = new Substrate[1];
            mapData.Substrates[0] = new Substrate();
            mapData.Substrates[0].DieArray = new Die[CountX, CountY];

            //此時可得(0,0)點座標 (mOriginDiePosX,mOriginDiePosY)

            for (int IdxX = 0; IdxX < CountX; IdxX++)
            {
                for (int IdxY = 0; IdxY < CountY; IdxY++)
                {

                    decimal _DiePosX = _TranslatedLeftUpperPosX + PitchX * IdxX;
                    decimal _DiePosY = _TranslatedLeftUpperPosY - PitchY * IdxY;
                    mapData.Substrates[0].DieArray[IdxX, IdxY] = new MapData.Die();
                    if (IsDieInRange2(_DiePosX, _DiePosY))
                    {
                        mapData.Substrates[0].DieArray[IdxX, IdxY].Bin = "1";
                    }
                    else
                    {
                        mapData.Substrates[0].DieArray[IdxX, IdxY].Bin = ".";
                    }
                }
            }
            RefreshMap();


            IsUpdating = false;


        }

        /// <summary>是否Die在有效範圍內
        /// </summary>
        /// <param name="dieLeftUpperPosX"></param>
        /// <param name="dieLeftUpperPosY"></param>
        /// <returns></returns>
        /// <remarks>輸入為旋轉前位置</remarks>
        bool IsDieInRange2(decimal dieLeftUpperPosX, decimal dieLeftUpperPosY)
        {
            if (WaferEffectiveRadius == 0) return true;//無半徑, 不判斷
            decimal _PosX1 = dieLeftUpperPosX;
            decimal _PosY1 = dieLeftUpperPosY;
            decimal _PosX2 = dieLeftUpperPosX + DieSizeX;
            decimal _PosY2 = dieLeftUpperPosY;
            decimal _PosX3 = dieLeftUpperPosX;
            decimal _PosY3 = dieLeftUpperPosY - DieSizeY;
            decimal _PosX4 = dieLeftUpperPosX + DieSizeX;
            decimal _PosY4 = dieLeftUpperPosY - DieSizeY;
            decimal _RotatedPosX1;
            decimal _RotatedPosY1;
            decimal _RotatedPosX2;
            decimal _RotatedPosY2;
            decimal _RotatedPosX3;
            decimal _RotatedPosY3;
            decimal _RotatedPosX4;
            decimal _RotatedPosY4;
            CMath.Rotation(_PosX1 - WaferCenterX, _PosY1 - WaferCenterY, WaferAngle, WaferAngleY, out _RotatedPosX1, out _RotatedPosY1);
            CMath.Rotation(_PosX2 - WaferCenterX, _PosY2 - WaferCenterY, WaferAngle, WaferAngleY, out _RotatedPosX2, out _RotatedPosY2);
            CMath.Rotation(_PosX3 - WaferCenterX, _PosY3 - WaferCenterY, WaferAngle, WaferAngleY, out _RotatedPosX3, out _RotatedPosY3);
            CMath.Rotation(_PosX4 - WaferCenterX, _PosY4 - WaferCenterY, WaferAngle, WaferAngleY, out _RotatedPosX4, out _RotatedPosY4);

            decimal mDistance1 = Premtek.CMath.GetDistance(_RotatedPosX1, _RotatedPosY1, 0, 0);
            decimal mDistance2 = Premtek.CMath.GetDistance(_RotatedPosX2, _RotatedPosY2, 0, 0);
            decimal mDistance3 = Premtek.CMath.GetDistance(_RotatedPosX3, _RotatedPosY3, 0, 0);
            decimal mDistance4 = Premtek.CMath.GetDistance(_RotatedPosX4, _RotatedPosY4, 0, 0);
            if (mDistance1 > WaferEffectiveRadius) return false;
            if (mDistance2 > WaferEffectiveRadius) return false;
            if (mDistance3 > WaferEffectiveRadius) return false;
            if (mDistance4 > WaferEffectiveRadius) return false;

            return true;
        }
       
        /// <summary>MAP資料存取物件
        /// </summary>
        clsMapData mapData = new clsMapData();

        private void btnReadMapFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Cursor Files|*.csv| Txt Files|*.txt";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openDialog.FileName;
                string[] arrStr = path.Split(new char[] { '\\' }, StringSplitOptions.None);
                string[] fileName = arrStr[arrStr.Length - 1].Split(new char[] { '.' }, StringSplitOptions.None);
                string savePath = "D:\\PIIData\\MappingData\\Source\\";
                string filePath = "D:\\PIIData\\MappingData\\Source\\" + fileName[0] + ".csv";

                int Data1Column = 0;
                int Data1Row = 0;

                if ((mapData.OpenFile(path)))
                {
                    Data1Column = mapData.Information.Column;
                    Data1Row = mapData.Information.Row;
                    CountX = Data1Column;
                    CountY = Data1Row;

                }
                else if ((mapData.WaferMapConvertToPIIMap(path, savePath)))
                {
                    if (mapData.OpenFile(filePath))
                    {
                        Data1Column = mapData.Information.Column;
                        Data1Row = mapData.Information.Row;
                        CountX = Data1Column;
                        CountY = Data1Row;

                    }
                }
                IsUpdating = true;
                ControlMisc.SetNumericValue(ref nmuCountX, CountX);
                ControlMisc.SetNumericValue(ref nmuCountY, CountY);
                IsUpdating = false;
                RefreshMap();
            }

        }

        private void btnSaveMapFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Cursor Files|*.csv| Txt Files|*.txt";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mapData.OutputAseMap(dialog.FileName);
            }
        }

        /// <summary>繪圖
        /// </summary>
        /// <param name="pic"></param>
        /// <param name="mapData"></param>
        void DrawMap(ref PictureBox pic, clsMapData mapData, int FirstDieIndexX, int FirstDieIndexY)
        {
            Bitmap _bmp = (Bitmap)pic.Image;
            if (_bmp == null)
            {
                _bmp = new Bitmap(pic.Width, pic.Height);
            }

            Graphics g = Graphics.FromImage(_bmp);
            g.FillRectangle(Brushes.Gray, 0, 0, pic.Width, pic.Height);
            
            if (mapData != null)
            {
                if (mapData.Substrates != null)
                {
                    if (mapData.Substrates[0] != null)
                    {
                        int CountX = mapData.Substrates[0].DieArray.GetUpperBound(0) + 1;
                        int CountY = mapData.Substrates[0].DieArray.GetUpperBound(1) + 1;
                        float drawWidth = (float)(picMap.Width - 1) / (float)CountX;
                        float drawHeight = (float)(picMap.Height - 1) / (float)CountY;
                        float drawPosX;
                        float drawPosY;
                        for (int xNo = NodeStaringX - 1; xNo < CountX; xNo++)
                        {
                            for (int yNo = NodeStaringX - 1; yNo < CountY; yNo++)
                            {
                                drawPosX = drawWidth * xNo;
                                drawPosY = drawHeight * yNo;

                                switch (mapData.Substrates[0].DieArray[xNo, yNo].Bin)
                                {
                                    case ".":
                                        g.FillRectangle(Brushes.White, drawPosX, drawPosY, drawWidth, drawHeight);
                                        break;
                                    default:
                                        g.FillRectangle(Brushes.Gray, drawPosX, drawPosY, drawWidth, drawHeight);
                                        break;
                                }
                                g.DrawRectangle(Pens.Black, drawPosX, drawPosY, drawWidth, drawHeight);
                            }
                        }
                        if ((FirstDieIndexX >= 0) && (FirstDieIndexY >= 0))
                        {
                            drawPosX = drawWidth * FirstDieIndexX;
                            drawPosY = drawHeight * FirstDieIndexY;
                            g.FillRectangle(Brushes.Yellow, drawPosX, drawPosY, drawWidth, drawHeight);
                            g.DrawRectangle(Pens.Black, drawPosX, drawPosY, drawWidth, drawHeight);
                        }
                    }
                }
            }
            pic.Image = _bmp;

        }

        private void nmuCountX_ValueChanged(object sender, EventArgs e)
        {
            if (IsUpdating) return;

            RefreshMap();
            if (ValueChanged != null)
            {
                ValueChanged(sender, e);
            }

        }

        private void nmuRadius_ValueChanged(object sender, EventArgs e)
        {
            if (IsUpdating) return;
            WaferEffectiveRadius = nmuRadius.Value;
            UpdateUI();
        }


        void ButtonLock(bool isEnabled)
        {
            btnGoPos1.Enabled = isEnabled;
            btnGoPos2.Enabled = isEnabled;
        }
        private void btnGoPos1_Click(object sender, EventArgs e)
        {
            decimal _FDposX = FirstDiePosX, _FDPosY = FirstDiePosY;
            _FDposX += DieSizeX * 0.5M;
            _FDPosY -= DieSizeY * 0.5M;
            //將基準點平移到元件中心
            GetFirstDiePos(_TranslatedLeftUpperPosX + DieSizeX * 0.5M, _TranslatedLeftUpperPosY - DieSizeY * 0.5M, (int)nmuFirstDieIndexX.Value - 1, (int)nmuFirstDieIndexY.Value - 1, ref _FDposX, ref _FDPosY);
            

            decimal[] _Pos = new decimal[] { _FDposX, _FDPosY, FirstDiePosZ, 0, 0, 0 };

            MSystemParameter.sSysParam _Sys = new MSystemParameter.sSysParam();
            _Sys.AxisX = Sys.AxisX;
            _Sys.AxisY = Sys.AxisY;
            _Sys.AxisZ = Sys.AxisZ;
            _Sys.AxisA = Sys.AxisA;
            _Sys.AxisB = Sys.AxisB;
            _Sys.AxisC = Sys.AxisC;

            _Sys.SysNum = 1000;
            _Sys.RunStatus = MSystemParameter.enmRunStatus.Running;
            if (MoveStart != null)
            {
                MoveStart(sender, e);
            }
            Task.Run(new Action(() =>
            {
                btnGoPos1.BeginInvoke(new Action(() =>
                {
                    btnGoPos1.BackColor = Color.Yellow;
                    ButtonLock(false);
                }));
                ErrorCode ret = ErrorCode.Running;
                do
                {
                    ret = CMotionMisc.Move3DPos(_Sys, _Pos);
                } while (ret == ErrorCode.Running);
                switch (_Sys.RunStatus)
                {
                    case MSystemParameter.enmRunStatus.Finish:
                        btnGoPos1.BeginInvoke(new Action(() =>
                        {
                            btnGoPos1.BackColor = SystemColors.Control;
                            btnGoPos1.UseVisualStyleBackColor = true;
                            ButtonLock(true);
                        }));
                        break;
                    case MSystemParameter.enmRunStatus.Alarm:
                        btnGoPos1.BeginInvoke(new Action(() =>
                        {
                            btnGoPos1.BackColor = Color.Red;
                            btnGoPos1.UseVisualStyleBackColor = true;
                            ButtonLock(true);
                        }));
                        break;
                    case MSystemParameter.enmRunStatus.Running:
                        if (ret == ErrorCode.Failed)
                        {
                            btnGoPos1.BeginInvoke(new Action(() =>
                            {
                                btnGoPos1.BackColor = Color.Red;
                                btnGoPos1.UseVisualStyleBackColor = true;
                                ButtonLock(true);
                            }));
                        }
                        else if (ret == ErrorCode.Running)
                        {
                            Microsoft.VisualBasic.Interaction.MsgBox("Unknown", Microsoft.VisualBasic.MsgBoxStyle.OkOnly | Microsoft.VisualBasic.MsgBoxStyle.SystemModal | Microsoft.VisualBasic.MsgBoxStyle.MsgBoxSetForeground);
                        }
                        break;
                }
                if (PosChanged != null)
                {
                    PosChanged(sender, e);
                }
            }));
        }

        private void btnGoPos2_Click(object sender, EventArgs e)
        {
            decimal _FDposX = FirstDiePosX, _FDPosY = FirstDiePosY;

            decimal[] _Pos = new decimal[] { _FDposX, _FDPosY, FirstDiePosZ, 0, 0, 0 };

            MSystemParameter.sSysParam _Sys = new MSystemParameter.sSysParam();
            _Sys.AxisX = Sys.AxisX;
            _Sys.AxisY = Sys.AxisY;
            _Sys.AxisZ = Sys.AxisZ;
            _Sys.AxisA = Sys.AxisA;
            _Sys.AxisB = Sys.AxisB;
            _Sys.AxisC = Sys.AxisC;

            _Sys.SysNum = 1000;
            _Sys.RunStatus = MSystemParameter.enmRunStatus.Running;
            if (MoveStart != null)
            {
                MoveStart(sender, e);
            }
            Task.Run(new Action(() =>
            {
                btnGoPos2.BeginInvoke(new Action(() =>
                {
                    btnGoPos2.BackColor = Color.Yellow;
                    ButtonLock(false);
                }));
                ErrorCode ret = ErrorCode.Running;
                do
                {
                    ret = CMotionMisc.Move3DPos(_Sys, _Pos);
                } while (ret == ErrorCode.Running);
                switch (_Sys.RunStatus)
                {
                    case MSystemParameter.enmRunStatus.Finish:
                        btnGoPos2.BeginInvoke(new Action(() =>
                        {
                            btnGoPos2.BackColor = SystemColors.Control;
                            btnGoPos2.UseVisualStyleBackColor = true;
                            ButtonLock(true);
                        }));
                        break;
                    case MSystemParameter.enmRunStatus.Alarm:
                        btnGoPos2.BeginInvoke(new Action(() =>
                        {
                            btnGoPos2.BackColor = Color.Red;
                            btnGoPos2.UseVisualStyleBackColor = true;
                            ButtonLock(true);
                        }));
                        break;
                }
                if (PosChanged != null)
                {
                    PosChanged(sender, e);
                }
            }));
        }

        private void nmuFirstDieIndexX_ValueChanged(object sender, EventArgs e)
        {

            FirstDieIndexX = (int)nmuFirstDieIndexX.Value - 1;
            if (IsUpdating) return;
            RefreshMap();
        }

        private void nmuFirstDieIndexY_ValueChanged(object sender, EventArgs e)
        {

            FirstDieIndexY = (int)nmuFirstDieIndexY.Value - 1;
            if (IsUpdating) return;
            RefreshMap();
        }

        private void picMap_MouseDown(object sender, MouseEventArgs e)
        {
            //計算區塊大小
            float drawWidth = (float)(picMap.Width - 1) / (float)CountX;
            float drawHeight = (float)(picMap.Height - 1) / (float)CountY;
            //推算索引
            int idxX = Convert.ToInt32((float)e.X / drawWidth + 0.5) + (NodeStaringX - 1);
            int idxY = Convert.ToInt32((float)e.Y / drawHeight + 0.5) + (NodeStaringY - 1);
            if (idxX < 1) idxX = 1;
            if (idxY < 1) idxY = 1;


            if (chkToggle.Checked)
            {
                idxX -= 1;//修正對應關係
                idxY -= 1;
                if (mapData.Substrates[0].DieArray[idxX, idxY].Bin == ".")
                {
                    mapData.Substrates[0].DieArray[idxX, idxY].Bin = "1";
                }
                else
                {
                    mapData.Substrates[0].DieArray[idxX, idxY].Bin = ".";
                }
                RefreshMap();
            }
            else
            {

                //選取介面選項
                ControlMisc.SetNumericValue(ref nmuFirstDieIndexX, idxX);
                ControlMisc.SetNumericValue(ref nmuFirstDieIndexY, idxY);
                //點擊移動
                btnGoPos1.PerformClick();
            }
        }


        void RefreshMap()
        {
            /// <summary>已知良品數
            /// </summary>
            int KnownGoodDie = 0;
            if (mapData.Substrates == null)
            {
                mapData.Substrates = new Substrate[1];
                mapData.Substrates[0] = new Substrate();
            }
            if (mapData.Substrates[0].DieArray == null)
            {
                mapData.Substrates[0].DieArray = new Die[CountX, CountY];
            }
            else if (((mapData.Substrates[0].DieArray.GetUpperBound(0) + 1) != CountX) || ((mapData.Substrates[0].DieArray.GetUpperBound(1) + 1) != CountY))
            {
                mapData.Substrates[0].DieArray = new Die[CountX, CountY];
            }
            for (int IdxX = 0; IdxX < CountX; IdxX++)
            {
                for (int IdxY = 0; IdxY < CountY; IdxY++)
                {
                    if (mapData.Substrates[0].DieArray[IdxX, IdxY] == null)
                    {
                        mapData.Substrates[0].DieArray[IdxX, IdxY] = new Die();
                    }
                    if (mapData.Substrates[0].DieArray[IdxX, IdxY].Bin == "1")
                    {
                        KnownGoodDie++;
                    }

                }
            }

            lblDieCount.Text = "DieCount:" + KnownGoodDie.ToString();
            DrawMap(ref picMap, mapData, FirstDieIndexX, FirstDieIndexY);

        }
        private void picMap_MouseMove(object sender, MouseEventArgs e)
        {
            //計算區塊大小
            float drawWidth = (float)(picMap.Width - 1) / (float)CountX;
            float drawHeight = (float)(picMap.Height - 1) / (float)CountY;
            //推算索引
            int idxX = Convert.ToInt32((float)e.X / drawWidth + 0.5) + (NodeStaringX - 1);
            int idxY = Convert.ToInt32((float)e.Y / drawHeight + 0.5) + (NodeStaringY - 1);
            if (idxX < 1) idxX = 1;
            if (idxY < 1) idxY = 1;
            lblIdxX.Text = "Xno = " + idxX.ToString();
            lblIdxY.Text = "Yno = " + idxY.ToString();
        }





    }
}
