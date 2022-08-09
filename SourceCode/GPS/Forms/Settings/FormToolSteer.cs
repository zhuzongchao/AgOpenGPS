using System;
using System.Drawing;
using System.Windows.Forms; 

namespace AgOpenGPS
{
    public partial class FormToolSteer : Form
    {

        private readonly FormGPS mf = null;

        private bool toSend = false;
        private int windowSizeState = 0;
        private int counter=0;

        //Form stuff
        public FormToolSteer(Form callingForm)
        {
            mf = callingForm as FormGPS;
            InitializeComponent();
            nudToolAntennaHeight.Controls[0].Enabled = false;
            nudToolAntennaOffset.Controls[0].Enabled = false;

            this.Text = gStr.gsAutoSteerConfiguration;
        }

        private void FormSteer_Load(object sender, EventArgs e)
        {
            nudToolAntennaHeight.Value = (decimal)(Properties.Vehicle.Default.setTool_antennaHeight * mf.m2InchOrCm);
            nudToolAntennaOffset.Value = (decimal)(Properties.Vehicle.Default.setTool_antennaOffset * mf.m2InchOrCm);

            cboxToolOnlyGPS.Checked = Properties.Settings.Default.setGPS_isGPSToolOnly;

            if (cboxToolOnlyGPS.Checked)
            {
                nudToolAntennaHeight.Visible = false;
                nudToolAntennaOffset.Visible = false;
                lblAntennaHeight.Visible = false;
                lblAntennaOffset.Visible = false;
                btnConvertToToolOnly.Visible = true;
            }
            else
            {
                nudToolAntennaHeight.Visible = true;
                nudToolAntennaOffset.Visible = true;
                lblAntennaHeight.Visible = true;
                lblAntennaOffset.Visible = true;
                btnConvertToToolOnly.Visible = false;
            }

            //WAS Zero, CPD
            hsbarWasOffset.ValueChanged -= hsbarWasOffset_ValueChanged;
            hsbarCountsPerDegree.ValueChanged -= hsbarCountsPerDegree_ValueChanged;

            hsbarWasOffset.Value = (int)Properties.Vehicle.Default.setTool_wasOffset; ;
            hsbarCountsPerDegree.Value = Properties.Vehicle.Default.setTool_wasCounts;

            lblCountsPerDegree.Text = hsbarCountsPerDegree.Value.ToString();

            hsbarWasOffset.ValueChanged += hsbarWasOffset_ValueChanged;
            hsbarCountsPerDegree.ValueChanged += hsbarCountsPerDegree_ValueChanged;

            hsbarWasOffset.LargeChange = (int)(hsbarCountsPerDegree.Value * 0.1);
            lblWasOffset.Text = (hsbarWasOffset.Value / (double)(hsbarCountsPerDegree.Value)).ToString("N1");

            //min pwm, kP
            hsbarMinPWM.ValueChanged -= hsbarMinPWM_ValueChanged;
            hsbarProportionalGainXTE.ValueChanged -= hsbarProportionalGain_ValueChanged;

            hsbarMinPWM.Value = Properties.Vehicle.Default.setTool_MinPWM; ;
            lblMinPWM.Text = hsbarMinPWM.Value.ToString();

            hsbarProportionalGainXTE.Value = Properties.Vehicle.Default.setTool_PXTE; 
            lblProportionalGainXTE.Text = hsbarProportionalGainXTE.Value.ToString();

            hsbarMinPWM.ValueChanged += hsbarMinPWM_ValueChanged;
            hsbarProportionalGainXTE.ValueChanged += hsbarProportionalGain_ValueChanged;

            //Hydraulic Prop Gain
            hsbarProportionalGainHyd.ValueChanged -= hsbarProportionalGainHyd_ValueChanged;
            hsbarProportionalGainHyd.Value = Properties.Vehicle.Default.setTool_PHyd;
            lblProportionalGainHyd.Text = hsbarProportionalGainHyd.Value.ToString();
            hsbarProportionalGainHyd.ValueChanged += hsbarProportionalGainHyd_ValueChanged;

            //low steer, high steer
            hsbarWindupLimit.ValueChanged -= hsbarWindupLimit_ValueChanged;
            hsbarHighSteerPWM.ValueChanged -= hsbarHighSteerPWM_ValueChanged;

            hsbarWindupLimit.Value = Properties.Vehicle.Default.setTool_windupLimit;
            lblWindupLimit.Text = hsbarWindupLimit.Value.ToString();

            hsbarHighSteerPWM.Value = Properties.Vehicle.Default.setTool_HighPWM;
            lblHighSteerPWM.Text = hsbarHighSteerPWM.Value.ToString();

            hsbarWindupLimit.ValueChanged += hsbarWindupLimit_ValueChanged;
            hsbarHighSteerPWM.ValueChanged += hsbarHighSteerPWM_ValueChanged;

            //max steer, sidehill comp, integral
            hsbarMaxSteerAngle.ValueChanged -= hsbarMaxSteerAngle_ValueChanged;
            //hsbarSideHillComp.ValueChanged -= hsbarSideHillComp_ValueChanged;
            hsbarIntegralXTE.ValueChanged -= hsbarIntegral_ValueChanged;

            hsbarMaxSteerAngle.Value = (Int16)Properties.Vehicle.Default.setTool_maxSteerAngle;
            lblMaxSteerAngle.Text = (hsbarMaxSteerAngle.Value * 0.1).ToString("N1");

            //hsbarSideHillComp.Value = (int)(Properties.Vehicle.Default.setTool_sideHillComp);
            //lblSideHillComp.Text = hsbarSideHillComp.Value.ToString();

            hsbarIntegralXTE.Value = (int)(Properties.Vehicle.Default.setTool_IXTE);
            lblIntegralXTE.Text = hsbarIntegralXTE.Value.ToString();

            hsbarMaxSteerAngle.ValueChanged += hsbarMaxSteerAngle_ValueChanged;
            //hsbarSideHillComp.ValueChanged += hsbarSideHillComp_ValueChanged;
            hsbarIntegralXTE.ValueChanged += hsbarIntegral_ValueChanged;

            //make sure free drive is off
            btnFreeDrive.Image = Properties.Resources.SteerDriveOff;
            mf.vehicle.ast.isInFreeDriveMode = false;
            btnToolDistanceDown.Enabled = false;
            btnToolDistanceUp.Enabled = false;
            //hSBarFreeDrive.Value = 0;
            mf.vehicle.ast.driveFreeToolDistance = 0;

            int sett = Properties.Vehicle.Default.setArdToolSteer_setting0;

            if ((sett & 1) == 0) chkInvertWAS.Checked = false;
            else chkInvertWAS.Checked = true;

            if ((sett & 2) == 0) chkSteerInvertRelays.Checked = false;
            else chkSteerInvertRelays.Checked = true;

            if ((sett & 4) == 0) chkInvertSteer.Checked = false;
            else chkInvertSteer.Checked = true;

            if ((sett & 8) == 0) cboxConv.Text = "Differential";
            else cboxConv.Text = "Single";

            if ((sett & 16) == 0) cboxMotorDrive.Text = "IBT2";
            else cboxMotorDrive.Text = "Cytron";

            if ((sett & 32) == 32) cboxSteerEnable.Text = "Switch";
            else if ((sett & 64) == 64) cboxSteerEnable.Text = "Button";
            else cboxSteerEnable.Text = "None";

            if ((sett & 128) == 128) cboxDanfoss.Checked = true;
            else cboxDanfoss.Checked = false;

            

            toSend = true; //should send when opening, so the right values are in the module
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

            double actAng = mf.mc.actualSteerAngleDegrees * 5;  //Fixx
            if (actAng > 0)
            {
                if (actAng > 49) actAng = 49;
                CExtensionMethods.SetProgressNoAnimation(pbarRight, (int)actAng);
                pbarLeft.Value = 0;
            }
            else
            {
                if (actAng < -49) actAng = -49;
                pbarRight.Value = 0;
                CExtensionMethods.SetProgressNoAnimation(pbarLeft, (int)-actAng);
            }

            lblToolDistanceSet.Text = mf.guidanceLineDistanceOffTool.ToString();
            lblToolDistanceActual.Text = mf.mc.toolActualDistance.ToString("N1");
            lblActualSteerAngleUpper.Text = lblToolDistanceActual.Text;
            lblError.Text = mf.mc.toolError.ToString("N1");
            if (mf.mc.toolError > 0) lblError.ForeColor = Color.Red;
            else lblError.ForeColor = Color.DarkGreen;

            lblPWMDisplay.Text = mf.mc.toolPWM.ToString();
            counter++;
            if (toSend && counter > 4)
            {
                //Fixx
                Properties.Vehicle.Default.setTool_maxSteerAngle = mf.p_232.pgn[mf.p_232.maxSteer] = unchecked((byte)hsbarMaxSteerAngle.Value);
                Properties.Vehicle.Default.setTool_wasCounts = mf.p_232.pgn[mf.p_232.wasCounts] = unchecked((byte)hsbarCountsPerDegree.Value);
                Properties.Vehicle.Default.setTool_wasOffset = mf.p_232.pgn[mf.p_232.wasOffsetHi] = unchecked((byte)(hsbarWasOffset.Value >> 8));
                Properties.Vehicle.Default.setTool_wasOffset = mf.p_232.pgn[mf.p_232.wasOffsetLo] = unchecked((byte)hsbarWasOffset.Value);
                Properties.Vehicle.Default.setTool_HighPWM = mf.p_232.pgn[mf.p_232.highPWM] = unchecked((byte)hsbarHighSteerPWM.Value);
                Properties.Vehicle.Default.setTool_windupLimit = mf.p_232.pgn[mf.p_232.windup] = unchecked((byte)hsbarWindupLimit.Value);
                Properties.Vehicle.Default.setTool_PXTE = mf.p_232.pgn[mf.p_232.PXTE] = unchecked((byte)hsbarProportionalGainXTE.Value);
                Properties.Vehicle.Default.setTool_IXTE = mf.p_232.pgn[mf.p_232.IXTE] = unchecked((byte)hsbarIntegralXTE.Value);
                Properties.Vehicle.Default.setTool_MinPWM = mf.p_232.pgn[mf.p_232.minPWM] = unchecked((byte)hsbarMinPWM.Value);
                Properties.Vehicle.Default.setTool_PHyd = mf.p_232.pgn[mf.p_232.PHyd] = unchecked((byte)hsbarProportionalGainHyd.Value);


                mf.SendPgnToLoop(mf.p_232.pgn);
                toSend = false;
                counter = 0;
            }

            if (hsbarMinPWM.Value > hsbarWindupLimit.Value) lblMinPWM.ForeColor = Color.OrangeRed;
            else lblMinPWM.ForeColor = SystemColors.ControlText;

        }

        private void FormSteer_FormClosing(object sender, FormClosingEventArgs e)
        {
            mf.vehicle.ast.isInFreeDriveMode = false;

            Properties.Settings.Default.Save();
            Properties.Vehicle.Default.Save();

            //save current vehicle
            SettingsIO.ExportAll(mf.vehiclesDirectory + mf.vehicleFileName + ".XML");
        }

        private void nudToolAntennaHeight_Click(object sender, EventArgs e)
        {
            if (mf.KeypadToNUD((NumericUpDown)sender, this))
            {
                Properties.Vehicle.Default.setTool_antennaHeight = (double)nudToolAntennaHeight.Value * mf.inchOrCm2m;
                mf.pn.toolAntennaHeight = Properties.Vehicle.Default.setTool_antennaHeight;
            }
        }

        private void nudToolAntennaOffset_Click(object sender, EventArgs e)
        {
            if (mf.KeypadToNUD((NumericUpDown)sender, this))
            {
                Properties.Vehicle.Default.setTool_antennaOffset = (double)nudToolAntennaOffset.Value * mf.inchOrCm2m;
                mf.pn.toolAntennaOffset = Properties.Vehicle.Default.setTool_antennaOffset;
            }
        }

        #region Gain
        private void hsbarMinPWM_ValueChanged(object sender, EventArgs e)
        {
            lblMinPWM.Text = unchecked((byte)hsbarMinPWM.Value).ToString();
            toSend = true;
            counter = 0;
        }

        private void hsbarProportionalGain_ValueChanged(object sender, EventArgs e)
        {
            lblProportionalGainXTE.Text = unchecked((byte)hsbarProportionalGainXTE.Value).ToString();
            toSend = true;
            counter = 0;
        }

        private void hsbarWindupLimit_ValueChanged(object sender, EventArgs e)
        {
            lblWindupLimit.Text = unchecked((byte)hsbarWindupLimit.Value).ToString();
            toSend = true;
            counter = 0;
        }

        private void hsbarHighSteerPWM_ValueChanged(object sender, EventArgs e)
        {
            if (hsbarWindupLimit.Value > hsbarHighSteerPWM.Value) hsbarWindupLimit.Value = hsbarHighSteerPWM.Value;
            lblHighSteerPWM.Text = unchecked((byte)hsbarHighSteerPWM.Value).ToString();
            toSend = true;
            counter = 0;
        }
        #endregion

        #region Steer
        private void hsbarMaxSteerAngle_ValueChanged(object sender, EventArgs e)
        {
            lblMaxSteerAngle.Text = (hsbarMaxSteerAngle.Value * 0.1).ToString("N1");
            toSend = true;
            counter = 0;
        }

        private void hsbarCountsPerDegree_ValueChanged(object sender, EventArgs e)
        {
            lblCountsPerDegree.Text = unchecked((byte)hsbarCountsPerDegree.Value).ToString();
            hsbarWasOffset.LargeChange = (int)(hsbarCountsPerDegree.Value * 0.1);
            toSend = true;
            counter = 0;
        }

        private void hsbarIntegral_ValueChanged(object sender, EventArgs e)
        {
            lblIntegralXTE.Text = hsbarIntegralXTE.Value.ToString();
            toSend = true;
            counter = 0;
        }

        //private void hsbarSideHillComp_ValueChanged(object sender, EventArgs e)
        //{
        //    lblSideHillComp.Text = hsbarSideHillComp.Value.ToString();
        //    toSend = true;
        //    counter = 0;
        //}

        #endregion

        private void btnConvertToToolOnly_Click(object sender, EventArgs e)
        {
            DialogResult result3 = MessageBox.Show("Are you sure you want to change settings to Tool Only?",
                "Tool Only Settings",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (result3 == DialogResult.Yes)
            {
                //make it see thru
                Properties.Settings.Default.setDisplay_vehicleOpacity = 23;
                mf.vehicleOpacity = 0.23;
                mf.vehicleOpacityByte = 60;

                //antenna slightly ahead of pivot at minimum distance
                mf.vehicle.antennaPivot = Properties.Vehicle.Default.setVehicle_antennaPivot = 0.1;

                //image to triangle
                mf.isVehicleImage = false;
                Properties.Settings.Default.setDisplay_isVehicleImage = false;

                //front hitch
                mf.tool.isToolRearFixed = Properties.Vehicle.Default.setTool_isToolRearFixed = false;
                mf.tool.isToolTrailing = Properties.Vehicle.Default.setTool_isToolTrailing = false;
                mf.tool.isToolTBT = Properties.Vehicle.Default.setTool_isToolTBT = false;
                mf.tool.isToolFrontFixed = Properties.Vehicle.Default.setTool_isToolFront = true;

                //hitch slightly forward to min length
                Properties.Vehicle.Default.setVehicle_hitchLength = mf.tool.hitchLength = 0.1;
            }
        }

        private void expandWindow_Click(object sender, EventArgs e)
        {
            if (windowSizeState++ > 1) windowSizeState = 0;
            if (windowSizeState == 1) this.Size = new System.Drawing.Size(411, 652);
            else if (windowSizeState == 2) this.Size = new System.Drawing.Size(908, 652);
            else if (windowSizeState == 0) this.Size = new System.Drawing.Size(411, 521);

        }

        private void EnableAlert_Click(object sender, EventArgs e)
        {
            pboxSendSteer.Visible = true;
        }

        private void btnSendSteerConfigPGN_Click(object sender, EventArgs e)
        {
            SaveSettings();
            mf.SendPgnToLoop(mf.p_231.pgn);
            pboxSendSteer.Visible = false;

            mf.TimedMessageBox(1000, gStr.gsMachinePort, "Settings Sent To Tool Steer Module");
        }

        private void SaveSettings()
        {

            int set = 1;
            int reset = 2046;
            int sett = 0;

            if (chkInvertWAS.Checked) sett |= set;
            else sett &= reset;

            set <<= 1;
            reset <<= 1;
            reset += 1;
            if (chkSteerInvertRelays.Checked) sett |= set;
            else sett &= reset;

            set <<= 1;
            reset <<= 1;
            reset += 1;
            if (chkInvertSteer.Checked) sett |= set;
            else sett &= reset;

            set <<= 1;
            reset <<= 1;
            reset += 1;
            if (cboxConv.Text == "Single") sett |= set;
            else sett &= reset;

            set <<= 1;
            reset <<= 1;
            reset += 1;
            if (cboxMotorDrive.Text == "Cytron") sett |= set;
            else sett &= reset;

            set <<= 1;
            reset <<= 1;
            reset += 1;
            if (cboxSteerEnable.Text == "Switch") sett |= set;
            else sett &= reset;

            set <<= 1;
            reset <<= 1;
            reset += 1;
            if (cboxSteerEnable.Text == "Button") sett |= set;
            else sett &= reset;

            set <<= 1;
            reset <<= 1;
            reset += 1;
            if (cboxDanfoss.Checked) sett |= set;
            else sett &= reset;

            //set = (set << 1);
            //reset = (reset << 1);
            //reset = (reset + 1);
            //if ( ) sett |= set;
            //else sett &= reset;

            Properties.Vehicle.Default.setArdToolSteer_setting0 = (byte)sett;

            mf.p_231.pgn[mf.p_231.set0] = Properties.Vehicle.Default.setArdToolSteer_setting0;

            Properties.Settings.Default.Save();
            Properties.Vehicle.Default.Save();

            pboxSendSteer.Visible = false;
        }


        #region Free Drive
        private void btnFreeDrive_Click(object sender, EventArgs e)
        {
            if (mf.vehicle.ast.isInFreeToolDriveMode)
            {
                //turn OFF free drive mode
                btnFreeDrive.Image = Properties.Resources.SteerDriveOff;
                btnFreeDrive.BackColor = Color.FromArgb(50, 50, 70);
                mf.vehicle.ast.isInFreeToolDriveMode = false;
                btnToolDistanceDown.Enabled = false;
                btnToolDistanceUp.Enabled = false;
                //hSBarFreeDrive.Value = 0;
                mf.vehicle.ast.driveFreeToolDistance = 0;
            }
            else
            {
                //turn ON free drive mode
                btnFreeDrive.Image = Properties.Resources.SteerDriveOn;
                btnFreeDrive.BackColor = Color.LightGreen;
                mf.vehicle.ast.isInFreeToolDriveMode = true;
                btnToolDistanceDown.Enabled = true;
                btnToolDistanceUp.Enabled = true;
                //hSBarFreeDrive.Value = 0;
                mf.vehicle.ast.driveFreeToolDistance = 0;
                lblToolDistanceSet.Text = "0";
            }
        }

        private void btnFreeDriveZero_Click(object sender, EventArgs e)
        {
            if (mf.vehicle.ast.driveFreeToolDistance == 0)
                mf.vehicle.ast.driveFreeToolDistance = 50;
            else mf.vehicle.ast.driveFreeToolDistance = 0;
        }


        private void btnSteerAngleUp_MouseDown(object sender, MouseEventArgs e)
        {
            mf.vehicle.ast.driveFreeToolDistance++;
            if (mf.vehicle.ast.driveFreeToolDistance > 100) mf.vehicle.ast.driveFreeToolDistance = 100;
        }

        private void btnSteerAngleDown_MouseDown(object sender, MouseEventArgs e)
        {
            mf.vehicle.ast.driveFreeToolDistance--;
            if (mf.vehicle.ast.driveFreeToolDistance < -100) mf.vehicle.ast.driveFreeToolDistance = -100;
        }
        #endregion

        private void cboxToolOnlyGPS_Click(object sender, EventArgs e)
        {
            mf.pn.isGPSToolOnly = cboxToolOnlyGPS.Checked;
            Properties.Settings.Default.setGPS_isGPSToolOnly = mf.pn.isGPSToolOnly;
            Properties.Settings.Default.Save();

            if (cboxToolOnlyGPS.Checked)
            {
                nudToolAntennaHeight.Visible = false;
                nudToolAntennaOffset.Visible = false;
                lblAntennaHeight.Visible = false;
                lblAntennaOffset.Visible = false;
                btnConvertToToolOnly.Visible = true;
            }
            else
            {
                nudToolAntennaHeight.Visible = true;
                nudToolAntennaOffset.Visible = true;
                lblAntennaHeight.Visible = true;
                lblAntennaOffset.Visible = true;
                btnConvertToToolOnly.Visible = false;
            }
        }

        #region Help

        private void btnZeroWAS_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.h_btnZeroWAS, gStr.gsHelp);
        }

        private void hsbarWasOffset_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.h_hsbarWasOffset, gStr.gsHelp);
        }

        private void hsbarCountsPerDegree_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.h_hsbarCountsPerDegree, gStr.gsHelp);

        }

        private void hsbarAckerman_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.h_hsbarAckerman, gStr.gsHelp);
        }

        private void hsbarMaxSteerAngle_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.h_hsbarMaxSteerAngle, gStr.gsHelp);
        }

        private void hsbarProportionalGain_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.h_hsbarProportionalGain, gStr.gsHelp);
        }

        private void hsbarHighSteerPWM_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.h_hsbarHighSteerPWM, gStr.gsHelp);
        }

        private void hsbarLowSteerPWM_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.h_hsbarLowSteerPWM, gStr.gsHelp);
        }

        private void hsbarMinPWM_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.h_hsbarMinPWM, gStr.gsHelp);
        }

        private void hsbarStanleyGain_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.h_hsbarStanleyGain, gStr.gsHelp);
        }

        private void hsbarHeadingErrorGain_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.h_hsbarHeadingErrorGain, gStr.gsHelp);
        }

        private void hsbarIntegral_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.h_hsbarIntegral, gStr.gsHelp);
        }

        private void hsbarLookAhead_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.h_hsbarLookAhead, gStr.gsHelp);
        }

        private void hsbarLookAheadMult_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.h_hsbarLookAheadMult, gStr.gsHelp);
        }

        private void hsbarSideHillComp_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.h_hsbarSideHillComp, gStr.gsHelp);
        }

        private void hsbarIntegralPurePursuit_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.h_hsbarIntegralPurePursuit, gStr.gsHelp);
        }

        private void btnFreeDrive_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.h_btnFreeDrive, gStr.gsHelp);
        }

        private void btnSteerAngleDown_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.h_btnSteerAngleDown, gStr.gsHelp);
        }

        private void btnSteerAngleUp_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.h_btnSteerAngleUp, gStr.gsHelp);
        }

        private void btnFreeDriveZero_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.h_btnFreeDriveZero, gStr.gsHelp);
        }

        private void btnStartSA_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.h_btnStartSA, gStr.gsHelp);
        }

        private void chkInvertWAS_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.hc_chkInvertWAS, gStr.gsHelp);
        }

        private void chkInvertSteer_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.hc_chkInvertSteer, gStr.gsHelp);
        }

        private void chkSteerInvertRelays_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.hc_chkSteerInvertRelays, gStr.gsHelp);
        }

        private void cboxDanfoss_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.hc_cboxDanfoss, gStr.gsHelp);
        }

        private void cboxEncoder_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.hc_cboxEncoder, gStr.gsHelp);
        }

        private void cboxPressureSensor_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.hc_cboxPressureSensor, gStr.gsHelp);
        }

        private void cboxCurrentSensor_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.hc_cboxCurrentSensor, gStr.gsHelp);
        }

        private void nudMaxCounts_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.hc_nudMaxCounts, gStr.gsHelp);
        }

        private void cboxMotorDrive_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.hc_cboxMotorDrive, gStr.gsHelp);
        }

        private void cboxConv_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.hc_cboxConv, gStr.gsHelp);
        }

        private void cboxSteerEnable_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.hc_cboxSteerEnable, gStr.gsHelp);
        }

        private void pboxSendSteer_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.hc_pboxSendSteer, gStr.gsHelp);
        }

        private void btnSendSteerConfigPGN_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(gStr.hc_btnSendSteerConfigPGN, gStr.gsHelp);
        }

        #endregion

        private void hsbarWasOffset_ValueChanged(object sender, EventArgs e)
        {
            lblWasOffset.Text = (hsbarWasOffset.Value / (double)(hsbarCountsPerDegree.Value)).ToString("N1");
            toSend = true;
            counter = 0;
        }

        private void hsbarProportionalGainHyd_ValueChanged(object sender, EventArgs e)
        {
            lblProportionalGainHyd.Text = hsbarProportionalGainHyd.Value.ToString();
            toSend = true;
            counter = 0;
        }
    }
}
