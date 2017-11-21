using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectAOI;
using ProjectCore;
using ProjectIO;

namespace Premtek
{
    public partial class ucVision : UserControl
    {
        /// <summary>外部引入 場景名稱
        /// </summary>
        string _Scene = "";
        /// <summary>外部引入 CCD編號
        /// </summary>
        int CCDNo = 0;
        /// <summary>外部配接AOI物件
        /// </summary>
        public CAOICollection AOI { get; set; }
        public ucVision()
        {
            InitializeComponent();
        }
        public ErrorCode ShowUI()
        {
            nmuLight1.Maximum = 255;
            nmuLight2.Maximum = 255;
            nmuLight3.Maximum = 255;
            nmuLight4.Maximum = 255;

            if ((_Scene != null) && (_Scene != "") && AOI.SceneDictionary.ContainsKey(_Scene))
            {
                ControlMisc.SetNumericValue(ref nmuLight1, AOI.SceneDictionary[_Scene].LightValue[(int)MCommonDefine.enmValveLight.No1]);
                ControlMisc.SetNumericValue(ref nmuLight2, AOI.SceneDictionary[_Scene].LightValue[(int)MCommonDefine.enmValveLight.No2]);
                ControlMisc.SetNumericValue(ref nmuLight3, AOI.SceneDictionary[_Scene].LightValue[(int)MCommonDefine.enmValveLight.No3]);
                ControlMisc.SetNumericValue(ref nmuLight4, AOI.SceneDictionary[_Scene].LightValue[(int)MCommonDefine.enmValveLight.No4]);
                chkLight1.Checked = AOI.SceneDictionary[_Scene].LightEnable[(int)MCommonDefine.enmValveLight.No1];
                chkLight2.Checked = AOI.SceneDictionary[_Scene].LightEnable[(int)MCommonDefine.enmValveLight.No2];
                chkLight3.Checked = AOI.SceneDictionary[_Scene].LightEnable[(int)MCommonDefine.enmValveLight.No3];
                chkLight4.Checked = AOI.SceneDictionary[_Scene].LightEnable[(int)MCommonDefine.enmValveLight.No4];
            }
            chkLight1_CheckedChanged(null, null);
            chkLight2_CheckedChanged(null, null);
            chkLight3_CheckedChanged(null, null);
            chkLight4_CheckedChanged(null, null);

            MCommonDefine.enmLight light1 = CCDLightMapping(CCDNo, MCommonDefine.enmValveLight.No1);
            if (light1 == MCommonDefine.enmLight.None)
            {
                nmuLight1.Enabled = false;
                chkLight1.Enabled = false;
            }
            MCommonDefine.enmLight light2 = CCDLightMapping(CCDNo, MCommonDefine.enmValveLight.No2);
            if (light2 == MCommonDefine.enmLight.None)
            {
                nmuLight2.Enabled = false;
                chkLight2.Enabled = false;
            }
            MCommonDefine.enmLight light3 = CCDLightMapping(CCDNo, MCommonDefine.enmValveLight.No3);
            if (light3 == MCommonDefine.enmLight.None)
            {
                nmuLight3.Enabled = false;
                chkLight3.Enabled = false;
            }

            MCommonDefine.enmLight light4 = CCDLightMapping(CCDNo, MCommonDefine.enmValveLight.No4);
            if (light4 == MCommonDefine.enmLight.None)
            {
                nmuLight4.Enabled = false;
                chkLight4.Enabled = false;
            }

            return ErrorCode.Success;
            //   MCommonAOI.gLightCollection.SetCCDLight
        }

        void SetLightOnOff(MCommonDefine.enmLight light, bool value)
        {
            switch (light)
            {
                case MCommonDefine.enmLight.No1:
                    MCommonIO.gDOCollection.SetState((int)MCommonDefineDO.enmDO.CCDLight, value);
                    break;
                case MCommonDefine.enmLight.No2:
                    MCommonIO.gDOCollection.SetState((int)MCommonDefineDO.enmDO.CCDLight2, value);
                    break;
                case MCommonDefine.enmLight.No3:
                    MCommonIO.gDOCollection.SetState((int)MCommonDefineDO.enmDO.CCDLight3, value);
                    break;
                case MCommonDefine.enmLight.No4:
                    MCommonIO.gDOCollection.SetState((int)MCommonDefineDO.enmDO.CCDLight4, value);
                    break;
                case MCommonDefine.enmLight.No5:
                    MCommonIO.gDOCollection.SetState((int)MCommonDefineDO.enmDO.CCDLight5, value);
                    break;
                case MCommonDefine.enmLight.No6:
                    MCommonIO.gDOCollection.SetState((int)MCommonDefineDO.enmDO.CCDLight6, value);
                    break;
                case MCommonDefine.enmLight.No7:
                    MCommonIO.gDOCollection.SetState((int)MCommonDefineDO.enmDO.CCDLight7, value);
                    break;
                case MCommonDefine.enmLight.No8:
                    MCommonIO.gDOCollection.SetState((int)MCommonDefineDO.enmDO.CCDLight8, value);
                    break;
            }
        }

        /// <summary>CCD光源對應
        /// </summary>
        /// <param name="ccd"></param>
        /// <param name="light"></param>
        /// <returns></returns>
        public MCommonDefine.enmLight CCDLightMapping(int ccd, MCommonDefine.enmValveLight light)
        {
            switch (ProjectCore.MSystemParameter.gSSystemParameter.MachineType)
            {
                case MCommonDefine.enmMachineType.DCSW_800AQ:
                    switch (ccd)
                    {
                        case 0:
                            switch (light)
                            {
                                case MCommonDefine.enmValveLight.No1:
                                    return MCommonDefine.enmLight.No1;
                                case MCommonDefine.enmValveLight.No2:
                                    return MCommonDefine.enmLight.No2;
                                case MCommonDefine.enmValveLight.No3:
                                    return MCommonDefine.enmLight.None;
                                case MCommonDefine.enmValveLight.No4:
                                    return MCommonDefine.enmLight.None;
                            }
                            break;
                        case 1:
                            switch (light)
                            {
                                case MCommonDefine.enmValveLight.No1:
                                    return MCommonDefine.enmLight.No3;
                                case MCommonDefine.enmValveLight.No2:
                                    return MCommonDefine.enmLight.No4;
                                case MCommonDefine.enmValveLight.No3:
                                    return MCommonDefine.enmLight.None;
                                case MCommonDefine.enmValveLight.No4:
                                    return MCommonDefine.enmLight.None;
                            }
                            break;
                        case 2:
                            switch (light)
                            {
                                case MCommonDefine.enmValveLight.No1:
                                    return MCommonDefine.enmLight.No5;
                                case MCommonDefine.enmValveLight.No2:
                                    return MCommonDefine.enmLight.No6;
                                case MCommonDefine.enmValveLight.No3:
                                    return MCommonDefine.enmLight.None;
                                case MCommonDefine.enmValveLight.No4:
                                    return MCommonDefine.enmLight.None;
                            }
                            break;
                        case 3:
                            switch (light)
                            {
                                case MCommonDefine.enmValveLight.No1:
                                    return MCommonDefine.enmLight.No7;
                                case MCommonDefine.enmValveLight.No2:
                                    return MCommonDefine.enmLight.No8;
                                case MCommonDefine.enmValveLight.No3:
                                    return MCommonDefine.enmLight.None;
                                case MCommonDefine.enmValveLight.No4:
                                    return MCommonDefine.enmLight.None;
                            }
                            break;
                    }
                    break;
                case MCommonDefine.enmMachineType.eDTS_2S2V:
                    switch (ccd)
                    {
                        case 0:
                            switch (light)
                            {
                                case MCommonDefine.enmValveLight.No1:
                                    return MCommonDefine.enmLight.No1;
                                case MCommonDefine.enmValveLight.No2:
                                    return MCommonDefine.enmLight.No2;
                                case MCommonDefine.enmValveLight.No3:
                                    return MCommonDefine.enmLight.None;
                                case MCommonDefine.enmValveLight.No4:
                                    return MCommonDefine.enmLight.None;
                            }
                            break;
                        case 1:
                            switch (light)
                            {
                                case MCommonDefine.enmValveLight.No1:
                                    return MCommonDefine.enmLight.No3;
                                case MCommonDefine.enmValveLight.No2:
                                    return MCommonDefine.enmLight.No4;
                                case MCommonDefine.enmValveLight.No3:
                                    return MCommonDefine.enmLight.None;
                                case MCommonDefine.enmValveLight.No4:
                                    return MCommonDefine.enmLight.None;
                            }
                            break;

                    }
                    break;
                default:
                    switch (ccd)
                    {
                        case 0:
                            switch (light)
                            {
                                case MCommonDefine.enmValveLight.No1:
                                    return MCommonDefine.enmLight.No1;
                                case MCommonDefine.enmValveLight.No2:
                                    return MCommonDefine.enmLight.No2;
                                case MCommonDefine.enmValveLight.No3:
                                    return MCommonDefine.enmLight.No3;
                                case MCommonDefine.enmValveLight.No4:
                                    return MCommonDefine.enmLight.No4;
                            }
                            break;

                    }
                    break;
            }
            return MCommonDefine.enmLight.None;
        }

        private void chkLight1_CheckedChanged(object sender, EventArgs e)
        {
            MCommonDefine.enmLight light;
            if (_Scene == null)
            {
                light = CCDLightMapping(CCDNo, MCommonDefine.enmValveLight.No1);
                SetLightOnOff(light, chkLight1.Checked);
                return;
            }
            if (_Scene == "")
            {
                light = CCDLightMapping(CCDNo, MCommonDefine.enmValveLight.No1);
                SetLightOnOff(light, chkLight1.Checked);
                return;
            }
            if (AOI != null)
            {
                if (!AOI.SceneDictionary.ContainsKey(_Scene))
                {
                    light = CCDLightMapping(CCDNo, MCommonDefine.enmValveLight.No1);
                    SetLightOnOff(light, chkLight1.Checked);
                    return;
                }
                grpVision.ForeColor = SystemColors.ControlText;

                AOI.SceneDictionary[_Scene].LightEnable[(int)MCommonDefine.enmValveLight.No1] = chkLight1.Checked;
                nmuLight1.Enabled = AOI.SceneDictionary[_Scene].LightEnable[(int)MCommonDefine.enmValveLight.No1];
            }
            
            light = CCDLightMapping(CCDNo, MCommonDefine.enmValveLight.No1);
            SetLightOnOff(light, chkLight1.Checked);
        }

        private void chkLight2_CheckedChanged(object sender, EventArgs e)
        {
            MCommonDefine.enmLight light;
            if (_Scene == null)
            {
                light = CCDLightMapping(CCDNo, MCommonDefine.enmValveLight.No2);
                SetLightOnOff(light, chkLight2.Checked);
                return;
            }
            if (_Scene == "")
            {
                light = CCDLightMapping(CCDNo, MCommonDefine.enmValveLight.No2);
                SetLightOnOff(light, chkLight2.Checked);
                return;
            }
            if (AOI != null)
            {
                if (!AOI.SceneDictionary.ContainsKey(_Scene))
                {
                    light = CCDLightMapping(CCDNo, MCommonDefine.enmValveLight.No2);
                    SetLightOnOff(light, chkLight2.Checked);
                    return;
                }
                grpVision.ForeColor = SystemColors.ControlText;

                AOI.SceneDictionary[_Scene].LightEnable[(int)MCommonDefine.enmValveLight.No2] = chkLight2.Checked;
                nmuLight2.Enabled = AOI.SceneDictionary[_Scene].LightEnable[(int)MCommonDefine.enmValveLight.No2];
            }
           
            light = CCDLightMapping(CCDNo, MCommonDefine.enmValveLight.No2);
            SetLightOnOff(light, chkLight2.Checked);
        }

        private void chkLight3_CheckedChanged(object sender, EventArgs e)
        {
            MCommonDefine.enmLight light;
            if (_Scene == null)
            {
                light = CCDLightMapping(CCDNo, MCommonDefine.enmValveLight.No3);
                SetLightOnOff(light, chkLight3.Checked);
                return;
            }
            if (_Scene == "")
            {
                light = CCDLightMapping(CCDNo, MCommonDefine.enmValveLight.No3);
                SetLightOnOff(light, chkLight3.Checked);
                return;
            }
            if (AOI != null)
            {
                if (!AOI.SceneDictionary.ContainsKey(_Scene))
                {
                    light = CCDLightMapping(CCDNo, MCommonDefine.enmValveLight.No3);
                    SetLightOnOff(light, chkLight3.Checked);
                    return;
                }
                grpVision.ForeColor = SystemColors.ControlText;

                AOI.SceneDictionary[_Scene].LightEnable[(int)MCommonDefine.enmValveLight.No3] = chkLight3.Checked;
                nmuLight3.Enabled = AOI.SceneDictionary[_Scene].LightEnable[(int)MCommonDefine.enmValveLight.No3];
            }
            
            light = CCDLightMapping(CCDNo, MCommonDefine.enmValveLight.No3);
            SetLightOnOff(light, chkLight3.Checked);
        }

        private void chkLight4_CheckedChanged(object sender, EventArgs e)
        {
            MCommonDefine.enmLight light;
            if (_Scene == null)
            {
                light = CCDLightMapping(CCDNo, MCommonDefine.enmValveLight.No4);
                SetLightOnOff(light, chkLight4.Checked);
                return;
            }
            if (_Scene == "")
            {
                light = CCDLightMapping(CCDNo, MCommonDefine.enmValveLight.No4);
                SetLightOnOff(light, chkLight4.Checked);
                return;
            }
            if (AOI != null)
            {
                if (!AOI.SceneDictionary.ContainsKey(_Scene))
                {
                    light = CCDLightMapping(CCDNo, MCommonDefine.enmValveLight.No4);
                    SetLightOnOff(light, chkLight4.Checked);
                    return;
                }
                grpVision.ForeColor = SystemColors.ControlText;

                AOI.SceneDictionary[_Scene].LightEnable[(int)MCommonDefine.enmValveLight.No4] = chkLight4.Checked;
                nmuLight4.Enabled = AOI.SceneDictionary[_Scene].LightEnable[(int)MCommonDefine.enmValveLight.No4];
            }
            
            light = CCDLightMapping(CCDNo, MCommonDefine.enmValveLight.No4);
            SetLightOnOff(light, chkLight4.Checked);
        }
    }
}
