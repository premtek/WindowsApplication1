using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Premtek
{
    public partial class frmMapSplit : Form
    {
        /// <summary>編輯用資料
        /// </summary>
        List<CRecipeMap> _mapEdit;

        /// <summary>匯入/匯出用資料
        /// </summary>
        public List<CRecipeMap> MapList;
        /// <summary>建構時, 由外部傳入map.
        /// </summary>
        /// <param name="mapList"></param>
        public frmMapSplit()
        {
            InitializeComponent(); 
        }

        private void frmMapSplit_Load(object sender, EventArgs e)
        {
            _mapEdit = new List<CRecipeMap>();
            for (int i = 0; i < MapList.Count; i++)
            {
                _mapEdit.Add(MapList[i].Clone());
            }

            decimal totalValue;
            if (_mapEdit.Count > 0)
            {
                totalValue = _mapEdit[0].SplitedLeft + _mapEdit[0].SplitedRight;
                trackBar1.Maximum = Convert.ToInt32(totalValue);
                trackBar1.Value = Convert.ToInt32(_mapEdit[0].SplitedLeft);
                trackBar1.Minimum = 0;
                trackBar1.Visible = true;
                ShowTrack(ref trackBar1, ref lblMap1Split);
            }
            else
            {
                trackBar1.Visible = false;
                lblMap1Split.Visible = false;
                lblMap1.Visible = false;
            }
            if (_mapEdit.Count > 1)
            {
                totalValue = _mapEdit[1].SplitedLeft + _mapEdit[1].SplitedRight;
                trackBar2.Maximum = Convert.ToInt32(totalValue);
                trackBar2.Value = Convert.ToInt32(_mapEdit[1].SplitedLeft);
                trackBar2.Minimum = 0;
                trackBar2.Visible = true;
                ShowTrack(ref trackBar2, ref lblMap2Split);
            }
            else
            {
                trackBar2.Visible = false;
                lblMap2Split.Visible = false;
                lblMap2.Visible = false;
            }
            if (_mapEdit.Count > 2)
            {
                totalValue = _mapEdit[2].SplitedLeft + _mapEdit[2].SplitedRight;
                trackBar3.Maximum = Convert.ToInt32(totalValue);
                trackBar3.Value = Convert.ToInt32(_mapEdit[2].SplitedLeft);
                trackBar3.Minimum = 0;
                trackBar3.Visible = true;
                ShowTrack(ref trackBar3, ref lblMap3Split);
            }
            else
            {
                trackBar3.Visible = false;
                lblMap3Split.Visible = false;
                lblMap3.Visible = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            MapList.Clear();
            for(int i=0;i<_mapEdit.Count;i++)
            {
                MapList.Add(_mapEdit[i].Clone());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        void ShowTrack(ref TrackBar track, ref Label lbl)
        {
            decimal leftRatio = 10M * (decimal)track.Value / (decimal)track.Maximum;
            decimal rightRatio = 10M - leftRatio;
            lbl.Text = leftRatio.ToString("0.#") + ":" + rightRatio.ToString("0.#");
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            ShowTrack(ref trackBar1, ref lblMap1Split);
            _mapEdit[0].SplitedLeft = trackBar1.Value;
            _mapEdit[0].SplitedRight = trackBar1.Maximum - trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            ShowTrack(ref trackBar2, ref lblMap2Split);
          
            _mapEdit[1].SplitedLeft = trackBar2.Value;
            _mapEdit[1].SplitedRight = trackBar2.Maximum - trackBar2.Value;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            ShowTrack(ref trackBar3, ref lblMap3Split);
            _mapEdit[2].SplitedLeft = trackBar3.Value;
            _mapEdit[2].SplitedRight = trackBar3.Maximum - trackBar3.Value;
        }
    }
}
