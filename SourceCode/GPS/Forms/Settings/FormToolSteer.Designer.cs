namespace AgOpenGPS
{
    partial class FormToolSteer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormToolSteer));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblToolDistanceSet = new System.Windows.Forms.Label();
            this.lblToolDistanceActual = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.lblPWMDisplay = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnFreeDrive = new System.Windows.Forms.Button();
            this.btnToolDistanceDown = new ProXoft.WinForms.RepeatButton();
            this.btnToolDistanceUp = new ProXoft.WinForms.RepeatButton();
            this.btnFreeDriveZero = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSteer = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblWasOffset = new System.Windows.Forms.Label();
            this.hsbarWasOffset = new System.Windows.Forms.HScrollBar();
            this.hsbarCountsPerDegree = new System.Windows.Forms.HScrollBar();
            this.lblCountsPerDegree = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblIntegralXTE = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblMaxSteerAngle = new System.Windows.Forms.Label();
            this.hsbarIntegralXTE = new System.Windows.Forms.HScrollBar();
            this.label24 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pbarRight = new System.Windows.Forms.ProgressBar();
            this.hsbarWindupLimit = new System.Windows.Forms.HScrollBar();
            this.lblWindupLimit = new System.Windows.Forms.Label();
            this.hsbarMaxSteerAngle = new System.Windows.Forms.HScrollBar();
            this.pbarLeft = new System.Windows.Forms.ProgressBar();
            this.lblActualSteerAngleUpper = new System.Windows.Forms.Label();
            this.hsbarProportionalGainXTE = new System.Windows.Forms.HScrollBar();
            this.lblProportionalGainXTE = new System.Windows.Forms.Label();
            this.tabGain = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.hsbarProportionalGainHyd = new System.Windows.Forms.HScrollBar();
            this.lblProportionalGainHyd = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.hsbarMinPWM = new System.Windows.Forms.HScrollBar();
            this.lblHighSteerPWM = new System.Windows.Forms.Label();
            this.lblMinPWM = new System.Windows.Forms.Label();
            this.hsbarHighSteerPWM = new System.Windows.Forms.HScrollBar();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label70 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.cboxMotorDrive = new System.Windows.Forms.ComboBox();
            this.cboxConv = new System.Windows.Forms.ComboBox();
            this.label63 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.pboxSendSteer = new System.Windows.Forms.PictureBox();
            this.cboxDanfoss = new System.Windows.Forms.CheckBox();
            this.chkSteerInvertRelays = new System.Windows.Forms.CheckBox();
            this.chkInvertSteer = new System.Windows.Forms.CheckBox();
            this.chkInvertWAS = new System.Windows.Forms.CheckBox();
            this.btnSendSteerConfigPGN = new System.Windows.Forms.Button();
            this.label30 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblAntennaOffset = new System.Windows.Forms.Label();
            this.lblAntennaHeight = new System.Windows.Forms.Label();
            this.nudToolAntennaHeight = new System.Windows.Forms.NumericUpDown();
            this.nudToolAntennaOffset = new System.Windows.Forms.NumericUpDown();
            this.btnConvertToToolOnly = new System.Windows.Forms.Button();
            this.cboxToolOnlyGPS = new System.Windows.Forms.CheckBox();
            this.label28 = new System.Windows.Forms.Label();
            this.cboxSteerEnable = new System.Windows.Forms.ComboBox();
            this.label62 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabSteer.SuspendLayout();
            this.tabGain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxSendSteer)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudToolAntennaHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudToolAntennaOffset)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // lblToolDistanceSet
            // 
            this.lblToolDistanceSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblToolDistanceSet.BackColor = System.Drawing.Color.Transparent;
            this.lblToolDistanceSet.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToolDistanceSet.ForeColor = System.Drawing.Color.Black;
            this.lblToolDistanceSet.Location = new System.Drawing.Point(52, 5);
            this.lblToolDistanceSet.Name = "lblToolDistanceSet";
            this.lblToolDistanceSet.Size = new System.Drawing.Size(70, 23);
            this.lblToolDistanceSet.TabIndex = 306;
            this.lblToolDistanceSet.Text = "-55.5";
            this.lblToolDistanceSet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblToolDistanceSet.Click += new System.EventHandler(this.expandWindow_Click);
            // 
            // lblToolDistanceActual
            // 
            this.lblToolDistanceActual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblToolDistanceActual.BackColor = System.Drawing.Color.Transparent;
            this.lblToolDistanceActual.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToolDistanceActual.ForeColor = System.Drawing.Color.Black;
            this.lblToolDistanceActual.Location = new System.Drawing.Point(184, 5);
            this.lblToolDistanceActual.Name = "lblToolDistanceActual";
            this.lblToolDistanceActual.Size = new System.Drawing.Size(70, 23);
            this.lblToolDistanceActual.TabIndex = 311;
            this.lblToolDistanceActual.Text = "-55.5";
            this.lblToolDistanceActual.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblToolDistanceActual.Click += new System.EventHandler(this.expandWindow_Click);
            // 
            // lblError
            // 
            this.lblError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblError.BackColor = System.Drawing.Color.Transparent;
            this.lblError.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Black;
            this.lblError.Location = new System.Drawing.Point(308, 5);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(62, 23);
            this.lblError.TabIndex = 312;
            this.lblError.Text = "-30.0";
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblError.Click += new System.EventHandler(this.expandWindow_Click);
            // 
            // lblPWMDisplay
            // 
            this.lblPWMDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPWMDisplay.BackColor = System.Drawing.Color.Transparent;
            this.lblPWMDisplay.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPWMDisplay.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPWMDisplay.Location = new System.Drawing.Point(75, 89);
            this.lblPWMDisplay.Name = "lblPWMDisplay";
            this.lblPWMDisplay.Size = new System.Drawing.Size(64, 23);
            this.lblPWMDisplay.TabIndex = 316;
            this.lblPWMDisplay.Text = "255";
            this.lblPWMDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label9.Location = new System.Drawing.Point(17, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 23);
            this.label9.TabIndex = 318;
            this.label9.Text = "PWM:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(143, 5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 23);
            this.label11.TabIndex = 319;
            this.label11.Text = "Act:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label11.Click += new System.EventHandler(this.expandWindow_Click);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(7, 5);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 23);
            this.label12.TabIndex = 320;
            this.label12.Text = "Set:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label12.Click += new System.EventHandler(this.expandWindow_Click);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(268, 5);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 23);
            this.label13.TabIndex = 321;
            this.label13.Text = "Err:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label13.Click += new System.EventHandler(this.expandWindow_Click);
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label14.Location = new System.Drawing.Point(289, 84);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 16);
            this.label14.TabIndex = 322;
            this.label14.Text = "Or +5";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnFreeDrive
            // 
            this.btnFreeDrive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFreeDrive.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnFreeDrive.FlatAppearance.BorderSize = 0;
            this.btnFreeDrive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFreeDrive.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFreeDrive.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnFreeDrive.Image = global::AgOpenGPS.Properties.Resources.SteerDriveOff;
            this.btnFreeDrive.Location = new System.Drawing.Point(4, 24);
            this.btnFreeDrive.Name = "btnFreeDrive";
            this.btnFreeDrive.Size = new System.Drawing.Size(73, 56);
            this.btnFreeDrive.TabIndex = 228;
            this.btnFreeDrive.UseVisualStyleBackColor = false;
            this.btnFreeDrive.Click += new System.EventHandler(this.btnFreeDrive_Click);
            this.btnFreeDrive.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.btnFreeDrive_HelpRequested);
            // 
            // btnToolDistanceDown
            // 
            this.btnToolDistanceDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnToolDistanceDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnToolDistanceDown.FlatAppearance.BorderSize = 0;
            this.btnToolDistanceDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToolDistanceDown.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToolDistanceDown.Image = global::AgOpenGPS.Properties.Resources.ArrowLeft;
            this.btnToolDistanceDown.Location = new System.Drawing.Point(100, 24);
            this.btnToolDistanceDown.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnToolDistanceDown.Name = "btnToolDistanceDown";
            this.btnToolDistanceDown.Size = new System.Drawing.Size(73, 56);
            this.btnToolDistanceDown.TabIndex = 314;
            this.btnToolDistanceDown.UseVisualStyleBackColor = true;
            this.btnToolDistanceDown.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.btnSteerAngleDown_HelpRequested);
            this.btnToolDistanceDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnSteerAngleDown_MouseDown);
            // 
            // btnToolDistanceUp
            // 
            this.btnToolDistanceUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnToolDistanceUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnToolDistanceUp.FlatAppearance.BorderSize = 0;
            this.btnToolDistanceUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToolDistanceUp.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToolDistanceUp.Image = global::AgOpenGPS.Properties.Resources.ArrowRight;
            this.btnToolDistanceUp.Location = new System.Drawing.Point(196, 24);
            this.btnToolDistanceUp.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnToolDistanceUp.Name = "btnToolDistanceUp";
            this.btnToolDistanceUp.Size = new System.Drawing.Size(73, 56);
            this.btnToolDistanceUp.TabIndex = 315;
            this.btnToolDistanceUp.UseVisualStyleBackColor = true;
            this.btnToolDistanceUp.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.btnSteerAngleUp_HelpRequested);
            this.btnToolDistanceUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnSteerAngleUp_MouseDown);
            // 
            // btnFreeDriveZero
            // 
            this.btnFreeDriveZero.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFreeDriveZero.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnFreeDriveZero.FlatAppearance.BorderSize = 0;
            this.btnFreeDriveZero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFreeDriveZero.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFreeDriveZero.ForeColor = System.Drawing.Color.White;
            this.btnFreeDriveZero.Image = global::AgOpenGPS.Properties.Resources.SteerZeroSmall;
            this.btnFreeDriveZero.Location = new System.Drawing.Point(292, 24);
            this.btnFreeDriveZero.Name = "btnFreeDriveZero";
            this.btnFreeDriveZero.Size = new System.Drawing.Size(73, 56);
            this.btnFreeDriveZero.TabIndex = 313;
            this.btnFreeDriveZero.UseVisualStyleBackColor = true;
            this.btnFreeDriveZero.Click += new System.EventHandler(this.btnFreeDriveZero_Click);
            this.btnFreeDriveZero.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.btnFreeDriveZero_HelpRequested);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl1.Controls.Add(this.tabSteer);
            this.tabControl1.Controls.Add(this.tabGain);
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.ItemSize = new System.Drawing.Size(188, 48);
            this.tabControl1.Location = new System.Drawing.Point(3, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(384, 436);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 347;
            // 
            // tabSteer
            // 
            this.tabSteer.AutoScroll = true;
            this.tabSteer.BackColor = System.Drawing.Color.Gainsboro;
            this.tabSteer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tabSteer.Controls.Add(this.label7);
            this.tabSteer.Controls.Add(this.label10);
            this.tabSteer.Controls.Add(this.lblWasOffset);
            this.tabSteer.Controls.Add(this.hsbarWasOffset);
            this.tabSteer.Controls.Add(this.hsbarCountsPerDegree);
            this.tabSteer.Controls.Add(this.lblCountsPerDegree);
            this.tabSteer.Controls.Add(this.label23);
            this.tabSteer.Controls.Add(this.lblIntegralXTE);
            this.tabSteer.Controls.Add(this.label6);
            this.tabSteer.Controls.Add(this.lblMaxSteerAngle);
            this.tabSteer.Controls.Add(this.hsbarIntegralXTE);
            this.tabSteer.Controls.Add(this.label24);
            this.tabSteer.Controls.Add(this.label2);
            this.tabSteer.Controls.Add(this.pbarRight);
            this.tabSteer.Controls.Add(this.hsbarWindupLimit);
            this.tabSteer.Controls.Add(this.lblWindupLimit);
            this.tabSteer.Controls.Add(this.hsbarMaxSteerAngle);
            this.tabSteer.Controls.Add(this.pbarLeft);
            this.tabSteer.Controls.Add(this.lblActualSteerAngleUpper);
            this.tabSteer.Controls.Add(this.hsbarProportionalGainXTE);
            this.tabSteer.Controls.Add(this.lblProportionalGainXTE);
            this.tabSteer.ImageIndex = 0;
            this.tabSteer.Location = new System.Drawing.Point(4, 52);
            this.tabSteer.Name = "tabSteer";
            this.tabSteer.Size = new System.Drawing.Size(376, 380);
            this.tabSteer.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(79, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(277, 19);
            this.label7.TabIndex = 356;
            this.label7.Text = "Counts per Degree";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(79, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(277, 19);
            this.label10.TabIndex = 352;
            this.label10.Text = "WAS Zero";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWasOffset
            // 
            this.lblWasOffset.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWasOffset.ForeColor = System.Drawing.Color.Black;
            this.lblWasOffset.Location = new System.Drawing.Point(3, 46);
            this.lblWasOffset.Name = "lblWasOffset";
            this.lblWasOffset.Size = new System.Drawing.Size(61, 35);
            this.lblWasOffset.TabIndex = 353;
            this.lblWasOffset.Text = "888";
            this.lblWasOffset.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // hsbarWasOffset
            // 
            this.hsbarWasOffset.LargeChange = 1;
            this.hsbarWasOffset.Location = new System.Drawing.Point(71, 50);
            this.hsbarWasOffset.Maximum = 4000;
            this.hsbarWasOffset.Minimum = -4000;
            this.hsbarWasOffset.Name = "hsbarWasOffset";
            this.hsbarWasOffset.Size = new System.Drawing.Size(285, 30);
            this.hsbarWasOffset.TabIndex = 351;
            this.hsbarWasOffset.ValueChanged += new System.EventHandler(this.hsbarWasOffset_ValueChanged);
            // 
            // hsbarCountsPerDegree
            // 
            this.hsbarCountsPerDegree.LargeChange = 1;
            this.hsbarCountsPerDegree.Location = new System.Drawing.Point(71, 107);
            this.hsbarCountsPerDegree.Maximum = 255;
            this.hsbarCountsPerDegree.Minimum = 1;
            this.hsbarCountsPerDegree.Name = "hsbarCountsPerDegree";
            this.hsbarCountsPerDegree.Size = new System.Drawing.Size(285, 30);
            this.hsbarCountsPerDegree.TabIndex = 354;
            this.hsbarCountsPerDegree.Value = 20;
            // 
            // lblCountsPerDegree
            // 
            this.lblCountsPerDegree.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountsPerDegree.ForeColor = System.Drawing.Color.Black;
            this.lblCountsPerDegree.Location = new System.Drawing.Point(3, 107);
            this.lblCountsPerDegree.Name = "lblCountsPerDegree";
            this.lblCountsPerDegree.Size = new System.Drawing.Size(61, 35);
            this.lblCountsPerDegree.TabIndex = 355;
            this.lblCountsPerDegree.Text = "888";
            this.lblCountsPerDegree.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.Black;
            this.label23.Location = new System.Drawing.Point(79, 141);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(277, 19);
            this.label23.TabIndex = 341;
            this.label23.Text = "Max Steer Angle";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIntegralXTE
            // 
            this.lblIntegralXTE.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIntegralXTE.ForeColor = System.Drawing.Color.Black;
            this.lblIntegralXTE.Location = new System.Drawing.Point(3, 273);
            this.lblIntegralXTE.Name = "lblIntegralXTE";
            this.lblIntegralXTE.Size = new System.Drawing.Size(60, 35);
            this.lblIntegralXTE.TabIndex = 350;
            this.lblIntegralXTE.Text = "888";
            this.lblIntegralXTE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(79, 312);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(277, 19);
            this.label6.TabIndex = 337;
            this.label6.Text = "Windup Limit";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMaxSteerAngle
            // 
            this.lblMaxSteerAngle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxSteerAngle.ForeColor = System.Drawing.Color.Black;
            this.lblMaxSteerAngle.Location = new System.Drawing.Point(3, 159);
            this.lblMaxSteerAngle.Name = "lblMaxSteerAngle";
            this.lblMaxSteerAngle.Size = new System.Drawing.Size(60, 35);
            this.lblMaxSteerAngle.TabIndex = 303;
            this.lblMaxSteerAngle.Text = "888";
            this.lblMaxSteerAngle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // hsbarIntegralXTE
            // 
            this.hsbarIntegralXTE.LargeChange = 1;
            this.hsbarIntegralXTE.Location = new System.Drawing.Point(71, 278);
            this.hsbarIntegralXTE.Name = "hsbarIntegralXTE";
            this.hsbarIntegralXTE.Size = new System.Drawing.Size(285, 30);
            this.hsbarIntegralXTE.TabIndex = 349;
            this.hsbarIntegralXTE.Value = 5;
            this.hsbarIntegralXTE.ValueChanged += new System.EventHandler(this.hsbarIntegral_ValueChanged);
            this.hsbarIntegralXTE.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.hsbarIntegralPurePursuit_HelpRequested);
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(79, 255);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(277, 19);
            this.label24.TabIndex = 342;
            this.label24.Text = "Integral";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label24.UseCompatibleTextRendering = true;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(79, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(277, 19);
            this.label2.TabIndex = 335;
            this.label2.Text = "P";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbarRight
            // 
            this.pbarRight.Location = new System.Drawing.Point(189, 6);
            this.pbarRight.Maximum = 50;
            this.pbarRight.Name = "pbarRight";
            this.pbarRight.Size = new System.Drawing.Size(178, 10);
            this.pbarRight.TabIndex = 330;
            // 
            // hsbarWindupLimit
            // 
            this.hsbarWindupLimit.LargeChange = 1;
            this.hsbarWindupLimit.Location = new System.Drawing.Point(71, 335);
            this.hsbarWindupLimit.Maximum = 255;
            this.hsbarWindupLimit.Name = "hsbarWindupLimit";
            this.hsbarWindupLimit.Size = new System.Drawing.Size(285, 30);
            this.hsbarWindupLimit.TabIndex = 269;
            this.hsbarWindupLimit.Value = 1;
            this.hsbarWindupLimit.ValueChanged += new System.EventHandler(this.hsbarWindupLimit_ValueChanged);
            this.hsbarWindupLimit.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.hsbarLowSteerPWM_HelpRequested);
            // 
            // lblWindupLimit
            // 
            this.lblWindupLimit.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWindupLimit.ForeColor = System.Drawing.Color.Black;
            this.lblWindupLimit.Location = new System.Drawing.Point(3, 330);
            this.lblWindupLimit.Name = "lblWindupLimit";
            this.lblWindupLimit.Size = new System.Drawing.Size(61, 35);
            this.lblWindupLimit.TabIndex = 273;
            this.lblWindupLimit.Text = "888";
            this.lblWindupLimit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // hsbarMaxSteerAngle
            // 
            this.hsbarMaxSteerAngle.LargeChange = 1;
            this.hsbarMaxSteerAngle.Location = new System.Drawing.Point(71, 164);
            this.hsbarMaxSteerAngle.Maximum = 80;
            this.hsbarMaxSteerAngle.Minimum = 10;
            this.hsbarMaxSteerAngle.Name = "hsbarMaxSteerAngle";
            this.hsbarMaxSteerAngle.Size = new System.Drawing.Size(285, 30);
            this.hsbarMaxSteerAngle.TabIndex = 299;
            this.hsbarMaxSteerAngle.Value = 10;
            this.hsbarMaxSteerAngle.ValueChanged += new System.EventHandler(this.hsbarMaxSteerAngle_ValueChanged);
            this.hsbarMaxSteerAngle.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.hsbarMaxSteerAngle_HelpRequested);
            // 
            // pbarLeft
            // 
            this.pbarLeft.Location = new System.Drawing.Point(6, 6);
            this.pbarLeft.Maximum = 50;
            this.pbarLeft.Name = "pbarLeft";
            this.pbarLeft.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.pbarLeft.RightToLeftLayout = true;
            this.pbarLeft.Size = new System.Drawing.Size(178, 10);
            this.pbarLeft.TabIndex = 329;
            // 
            // lblActualSteerAngleUpper
            // 
            this.lblActualSteerAngleUpper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblActualSteerAngleUpper.AutoSize = true;
            this.lblActualSteerAngleUpper.BackColor = System.Drawing.Color.Transparent;
            this.lblActualSteerAngleUpper.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActualSteerAngleUpper.ForeColor = System.Drawing.Color.Black;
            this.lblActualSteerAngleUpper.Location = new System.Drawing.Point(37, 19);
            this.lblActualSteerAngleUpper.Name = "lblActualSteerAngleUpper";
            this.lblActualSteerAngleUpper.Size = new System.Drawing.Size(39, 19);
            this.lblActualSteerAngleUpper.TabIndex = 324;
            this.lblActualSteerAngleUpper.Text = "255";
            this.lblActualSteerAngleUpper.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // hsbarProportionalGainXTE
            // 
            this.hsbarProportionalGainXTE.LargeChange = 1;
            this.hsbarProportionalGainXTE.Location = new System.Drawing.Point(71, 221);
            this.hsbarProportionalGainXTE.Maximum = 200;
            this.hsbarProportionalGainXTE.Name = "hsbarProportionalGainXTE";
            this.hsbarProportionalGainXTE.Size = new System.Drawing.Size(285, 30);
            this.hsbarProportionalGainXTE.TabIndex = 254;
            this.hsbarProportionalGainXTE.Value = 4;
            this.hsbarProportionalGainXTE.ValueChanged += new System.EventHandler(this.hsbarProportionalGain_ValueChanged);
            this.hsbarProportionalGainXTE.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.hsbarProportionalGain_HelpRequested);
            // 
            // lblProportionalGainXTE
            // 
            this.lblProportionalGainXTE.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProportionalGainXTE.ForeColor = System.Drawing.Color.Black;
            this.lblProportionalGainXTE.Location = new System.Drawing.Point(3, 221);
            this.lblProportionalGainXTE.Name = "lblProportionalGainXTE";
            this.lblProportionalGainXTE.Size = new System.Drawing.Size(61, 35);
            this.lblProportionalGainXTE.TabIndex = 258;
            this.lblProportionalGainXTE.Text = "888";
            this.lblProportionalGainXTE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabGain
            // 
            this.tabGain.AutoScroll = true;
            this.tabGain.BackColor = System.Drawing.Color.Gainsboro;
            this.tabGain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tabGain.Controls.Add(this.label5);
            this.tabGain.Controls.Add(this.label1);
            this.tabGain.Controls.Add(this.hsbarProportionalGainHyd);
            this.tabGain.Controls.Add(this.lblProportionalGainHyd);
            this.tabGain.Controls.Add(this.label15);
            this.tabGain.Controls.Add(this.label4);
            this.tabGain.Controls.Add(this.hsbarMinPWM);
            this.tabGain.Controls.Add(this.lblHighSteerPWM);
            this.tabGain.Controls.Add(this.lblMinPWM);
            this.tabGain.Controls.Add(this.hsbarHighSteerPWM);
            this.tabGain.ImageIndex = 1;
            this.tabGain.Location = new System.Drawing.Point(4, 52);
            this.tabGain.Name = "tabGain";
            this.tabGain.Size = new System.Drawing.Size(376, 380);
            this.tabGain.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(57, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(277, 19);
            this.label5.TabIndex = 342;
            this.label5.Text = "Hydraulic Setup";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(87, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(277, 19);
            this.label1.TabIndex = 341;
            this.label1.Text = "P";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // hsbarProportionalGainHyd
            // 
            this.hsbarProportionalGainHyd.LargeChange = 1;
            this.hsbarProportionalGainHyd.Location = new System.Drawing.Point(83, 60);
            this.hsbarProportionalGainHyd.Maximum = 200;
            this.hsbarProportionalGainHyd.Name = "hsbarProportionalGainHyd";
            this.hsbarProportionalGainHyd.Size = new System.Drawing.Size(285, 30);
            this.hsbarProportionalGainHyd.TabIndex = 339;
            this.hsbarProportionalGainHyd.Value = 4;
            this.hsbarProportionalGainHyd.ValueChanged += new System.EventHandler(this.hsbarProportionalGainHyd_ValueChanged);
            // 
            // lblProportionalGainHyd
            // 
            this.lblProportionalGainHyd.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProportionalGainHyd.ForeColor = System.Drawing.Color.Black;
            this.lblProportionalGainHyd.Location = new System.Drawing.Point(6, 60);
            this.lblProportionalGainHyd.Name = "lblProportionalGainHyd";
            this.lblProportionalGainHyd.Size = new System.Drawing.Size(61, 35);
            this.lblProportionalGainHyd.TabIndex = 340;
            this.lblProportionalGainHyd.Text = "888";
            this.lblProportionalGainHyd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(87, 169);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(277, 19);
            this.label15.TabIndex = 338;
            this.label15.Text = "PWM Min";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(87, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(277, 19);
            this.label4.TabIndex = 336;
            this.label4.Text = "PWM Max";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // hsbarMinPWM
            // 
            this.hsbarMinPWM.LargeChange = 1;
            this.hsbarMinPWM.Location = new System.Drawing.Point(83, 198);
            this.hsbarMinPWM.Name = "hsbarMinPWM";
            this.hsbarMinPWM.Size = new System.Drawing.Size(285, 30);
            this.hsbarMinPWM.TabIndex = 284;
            this.hsbarMinPWM.Value = 10;
            this.hsbarMinPWM.ValueChanged += new System.EventHandler(this.hsbarMinPWM_ValueChanged);
            this.hsbarMinPWM.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.hsbarMinPWM_HelpRequested);
            // 
            // lblHighSteerPWM
            // 
            this.lblHighSteerPWM.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHighSteerPWM.ForeColor = System.Drawing.Color.Black;
            this.lblHighSteerPWM.Location = new System.Drawing.Point(6, 122);
            this.lblHighSteerPWM.Name = "lblHighSteerPWM";
            this.lblHighSteerPWM.Size = new System.Drawing.Size(61, 35);
            this.lblHighSteerPWM.TabIndex = 278;
            this.lblHighSteerPWM.Text = "888";
            this.lblHighSteerPWM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMinPWM
            // 
            this.lblMinPWM.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinPWM.ForeColor = System.Drawing.Color.Black;
            this.lblMinPWM.Location = new System.Drawing.Point(6, 193);
            this.lblMinPWM.Name = "lblMinPWM";
            this.lblMinPWM.Size = new System.Drawing.Size(61, 35);
            this.lblMinPWM.TabIndex = 288;
            this.lblMinPWM.Text = "888";
            this.lblMinPWM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // hsbarHighSteerPWM
            // 
            this.hsbarHighSteerPWM.LargeChange = 2;
            this.hsbarHighSteerPWM.Location = new System.Drawing.Point(83, 129);
            this.hsbarHighSteerPWM.Maximum = 255;
            this.hsbarHighSteerPWM.Name = "hsbarHighSteerPWM";
            this.hsbarHighSteerPWM.Size = new System.Drawing.Size(285, 30);
            this.hsbarHighSteerPWM.TabIndex = 274;
            this.hsbarHighSteerPWM.Value = 50;
            this.hsbarHighSteerPWM.ValueChanged += new System.EventHandler(this.hsbarHighSteerPWM_ValueChanged);
            this.hsbarHighSteerPWM.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.hsbarHighSteerPWM_HelpRequested);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ST_SteerTab.png");
            this.imageList1.Images.SetKeyName(1, "ST_GainTab.png");
            this.imageList1.Images.SetKeyName(2, "ST_StanleyTab.png");
            this.imageList1.Images.SetKeyName(3, "Sf_PPTab.png");
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.ForeColor = System.Drawing.Color.Black;
            this.label70.Location = new System.Drawing.Point(583, 138);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(84, 13);
            this.label70.TabIndex = 505;
            this.label70.Text = "Invert Motor Dir";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.ForeColor = System.Drawing.Color.Black;
            this.label29.Location = new System.Drawing.Point(428, 138);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(72, 13);
            this.label29.TabIndex = 504;
            this.label29.Text = "Invert Relays";
            // 
            // label68
            // 
            this.label68.BackColor = System.Drawing.Color.Transparent;
            this.label68.Font = new System.Drawing.Font("Tahoma", 15.75F);
            this.label68.ForeColor = System.Drawing.Color.Black;
            this.label68.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label68.Location = new System.Drawing.Point(689, 556);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(172, 30);
            this.label68.TabIndex = 502;
            this.label68.Text = "Send +  Save";
            this.label68.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboxMotorDrive
            // 
            this.cboxMotorDrive.BackColor = System.Drawing.Color.AliceBlue;
            this.cboxMotorDrive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxMotorDrive.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboxMotorDrive.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboxMotorDrive.FormattingEnabled = true;
            this.cboxMotorDrive.Items.AddRange(new object[] {
            "Cytron",
            "IBT2"});
            this.cboxMotorDrive.Location = new System.Drawing.Point(425, 294);
            this.cboxMotorDrive.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboxMotorDrive.Name = "cboxMotorDrive";
            this.cboxMotorDrive.Size = new System.Drawing.Size(175, 37);
            this.cboxMotorDrive.TabIndex = 495;
            this.cboxMotorDrive.Click += new System.EventHandler(this.EnableAlert_Click);
            this.cboxMotorDrive.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.cboxMotorDrive_HelpRequested);
            // 
            // cboxConv
            // 
            this.cboxConv.BackColor = System.Drawing.Color.AliceBlue;
            this.cboxConv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxConv.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboxConv.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboxConv.FormattingEnabled = true;
            this.cboxConv.Items.AddRange(new object[] {
            "Single",
            "Differential"});
            this.cboxConv.Location = new System.Drawing.Point(425, 399);
            this.cboxConv.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboxConv.Name = "cboxConv";
            this.cboxConv.Size = new System.Drawing.Size(175, 37);
            this.cboxConv.TabIndex = 500;
            this.cboxConv.Click += new System.EventHandler(this.EnableAlert_Click);
            this.cboxConv.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.cboxConv_HelpRequested);
            // 
            // label63
            // 
            this.label63.BackColor = System.Drawing.Color.Transparent;
            this.label63.Font = new System.Drawing.Font("Tahoma", 15.75F);
            this.label63.ForeColor = System.Drawing.Color.Black;
            this.label63.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label63.Location = new System.Drawing.Point(422, 264);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(181, 29);
            this.label63.TabIndex = 496;
            this.label63.Text = "Motor Driver";
            this.label63.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label64
            // 
            this.label64.BackColor = System.Drawing.Color.Transparent;
            this.label64.Font = new System.Drawing.Font("Tahoma", 15.75F);
            this.label64.ForeColor = System.Drawing.Color.Black;
            this.label64.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label64.Location = new System.Drawing.Point(422, 369);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(181, 29);
            this.label64.TabIndex = 497;
            this.label64.Text = "A2D Convertor";
            this.label64.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.BackColor = System.Drawing.Color.Transparent;
            this.label55.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.ForeColor = System.Drawing.Color.Black;
            this.label55.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label55.Location = new System.Drawing.Point(428, 4);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(52, 16);
            this.label55.TabIndex = 489;
            this.label55.Text = "Danfoss";
            this.label55.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pboxSendSteer
            // 
            this.pboxSendSteer.BackgroundImage = global::AgOpenGPS.Properties.Resources.ConSt_Mandatory1;
            this.pboxSendSteer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pboxSendSteer.Location = new System.Drawing.Point(742, 401);
            this.pboxSendSteer.Name = "pboxSendSteer";
            this.pboxSendSteer.Size = new System.Drawing.Size(93, 85);
            this.pboxSendSteer.TabIndex = 509;
            this.pboxSendSteer.TabStop = false;
            this.pboxSendSteer.Visible = false;
            this.pboxSendSteer.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.pboxSendSteer_HelpRequested);
            // 
            // cboxDanfoss
            // 
            this.cboxDanfoss.Appearance = System.Windows.Forms.Appearance.Button;
            this.cboxDanfoss.BackColor = System.Drawing.Color.AliceBlue;
            this.cboxDanfoss.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.cboxDanfoss.FlatAppearance.CheckedBackColor = System.Drawing.Color.MediumAquamarine;
            this.cboxDanfoss.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboxDanfoss.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboxDanfoss.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboxDanfoss.Image = global::AgOpenGPS.Properties.Resources.ConSt_Danfoss;
            this.cboxDanfoss.Location = new System.Drawing.Point(425, 23);
            this.cboxDanfoss.Name = "cboxDanfoss";
            this.cboxDanfoss.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cboxDanfoss.Size = new System.Drawing.Size(114, 78);
            this.cboxDanfoss.TabIndex = 507;
            this.cboxDanfoss.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cboxDanfoss.UseVisualStyleBackColor = false;
            this.cboxDanfoss.Click += new System.EventHandler(this.EnableAlert_Click);
            this.cboxDanfoss.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.cboxDanfoss_HelpRequested);
            // 
            // chkSteerInvertRelays
            // 
            this.chkSteerInvertRelays.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkSteerInvertRelays.BackColor = System.Drawing.Color.AliceBlue;
            this.chkSteerInvertRelays.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.chkSteerInvertRelays.FlatAppearance.CheckedBackColor = System.Drawing.Color.MediumAquamarine;
            this.chkSteerInvertRelays.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkSteerInvertRelays.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSteerInvertRelays.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkSteerInvertRelays.Image = global::AgOpenGPS.Properties.Resources.ConSt_InvertRelay;
            this.chkSteerInvertRelays.Location = new System.Drawing.Point(425, 154);
            this.chkSteerInvertRelays.Name = "chkSteerInvertRelays";
            this.chkSteerInvertRelays.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkSteerInvertRelays.Size = new System.Drawing.Size(109, 78);
            this.chkSteerInvertRelays.TabIndex = 503;
            this.chkSteerInvertRelays.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSteerInvertRelays.UseVisualStyleBackColor = false;
            this.chkSteerInvertRelays.Click += new System.EventHandler(this.EnableAlert_Click);
            this.chkSteerInvertRelays.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.chkSteerInvertRelays_HelpRequested);
            // 
            // chkInvertSteer
            // 
            this.chkInvertSteer.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkInvertSteer.BackColor = System.Drawing.Color.AliceBlue;
            this.chkInvertSteer.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.chkInvertSteer.FlatAppearance.CheckedBackColor = System.Drawing.Color.MediumAquamarine;
            this.chkInvertSteer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkInvertSteer.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInvertSteer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkInvertSteer.Image = global::AgOpenGPS.Properties.Resources.ConSt_InvertDirection;
            this.chkInvertSteer.Location = new System.Drawing.Point(580, 154);
            this.chkInvertSteer.Name = "chkInvertSteer";
            this.chkInvertSteer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkInvertSteer.Size = new System.Drawing.Size(109, 78);
            this.chkInvertSteer.TabIndex = 491;
            this.chkInvertSteer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkInvertSteer.UseVisualStyleBackColor = false;
            this.chkInvertSteer.Click += new System.EventHandler(this.EnableAlert_Click);
            this.chkInvertSteer.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.chkInvertSteer_HelpRequested);
            // 
            // chkInvertWAS
            // 
            this.chkInvertWAS.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkInvertWAS.BackColor = System.Drawing.Color.AliceBlue;
            this.chkInvertWAS.Checked = true;
            this.chkInvertWAS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInvertWAS.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.chkInvertWAS.FlatAppearance.CheckedBackColor = System.Drawing.Color.MediumAquamarine;
            this.chkInvertWAS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkInvertWAS.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInvertWAS.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkInvertWAS.Image = global::AgOpenGPS.Properties.Resources.ConSt_InvertWAS;
            this.chkInvertWAS.Location = new System.Drawing.Point(580, 21);
            this.chkInvertWAS.Name = "chkInvertWAS";
            this.chkInvertWAS.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkInvertWAS.Size = new System.Drawing.Size(109, 78);
            this.chkInvertWAS.TabIndex = 490;
            this.chkInvertWAS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkInvertWAS.UseVisualStyleBackColor = false;
            this.chkInvertWAS.Click += new System.EventHandler(this.EnableAlert_Click);
            this.chkInvertWAS.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.chkInvertWAS_HelpRequested);
            // 
            // btnSendSteerConfigPGN
            // 
            this.btnSendSteerConfigPGN.BackColor = System.Drawing.Color.Transparent;
            this.btnSendSteerConfigPGN.FlatAppearance.BorderSize = 0;
            this.btnSendSteerConfigPGN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendSteerConfigPGN.Image = global::AgOpenGPS.Properties.Resources.ToolAcceptChange;
            this.btnSendSteerConfigPGN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSendSteerConfigPGN.Location = new System.Drawing.Point(724, 482);
            this.btnSendSteerConfigPGN.Name = "btnSendSteerConfigPGN";
            this.btnSendSteerConfigPGN.Size = new System.Drawing.Size(133, 62);
            this.btnSendSteerConfigPGN.TabIndex = 501;
            this.btnSendSteerConfigPGN.UseVisualStyleBackColor = false;
            this.btnSendSteerConfigPGN.Click += new System.EventHandler(this.btnSendSteerConfigPGN_Click);
            this.btnSendSteerConfigPGN.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.pboxSendSteer_HelpRequested);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.ForeColor = System.Drawing.Color.Black;
            this.label30.Location = new System.Drawing.Point(583, 4);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(63, 13);
            this.label30.TabIndex = 515;
            this.label30.Text = "Invert WAS";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.lblError);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.lblToolDistanceSet);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.lblToolDistanceActual);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Location = new System.Drawing.Point(7, 443);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(376, 34);
            this.panel2.TabIndex = 324;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.btnFreeDriveZero);
            this.panel1.Controls.Add(this.btnToolDistanceUp);
            this.panel1.Controls.Add(this.btnFreeDrive);
            this.panel1.Controls.Add(this.btnToolDistanceDown);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.lblPWMDisplay);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Location = new System.Drawing.Point(7, 483);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(376, 122);
            this.panel1.TabIndex = 323;
            // 
            // lblAntennaOffset
            // 
            this.lblAntennaOffset.AutoSize = true;
            this.lblAntennaOffset.ForeColor = System.Drawing.Color.Black;
            this.lblAntennaOffset.Location = new System.Drawing.Point(751, 153);
            this.lblAntennaOffset.Name = "lblAntennaOffset";
            this.lblAntennaOffset.Size = new System.Drawing.Size(82, 13);
            this.lblAntennaOffset.TabIndex = 540;
            this.lblAntennaOffset.Text = "Antenna Offset";
            // 
            // lblAntennaHeight
            // 
            this.lblAntennaHeight.AutoSize = true;
            this.lblAntennaHeight.ForeColor = System.Drawing.Color.Black;
            this.lblAntennaHeight.Location = new System.Drawing.Point(751, 16);
            this.lblAntennaHeight.Name = "lblAntennaHeight";
            this.lblAntennaHeight.Size = new System.Drawing.Size(82, 13);
            this.lblAntennaHeight.TabIndex = 539;
            this.lblAntennaHeight.Text = "Antenna Height";
            // 
            // nudToolAntennaHeight
            // 
            this.nudToolAntennaHeight.BackColor = System.Drawing.Color.AliceBlue;
            this.nudToolAntennaHeight.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudToolAntennaHeight.InterceptArrowKeys = false;
            this.nudToolAntennaHeight.Location = new System.Drawing.Point(729, 36);
            this.nudToolAntennaHeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudToolAntennaHeight.Name = "nudToolAntennaHeight";
            this.nudToolAntennaHeight.ReadOnly = true;
            this.nudToolAntennaHeight.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.nudToolAntennaHeight.Size = new System.Drawing.Size(144, 52);
            this.nudToolAntennaHeight.TabIndex = 538;
            this.nudToolAntennaHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudToolAntennaHeight.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudToolAntennaHeight.Click += new System.EventHandler(this.nudToolAntennaHeight_Click);
            // 
            // nudToolAntennaOffset
            // 
            this.nudToolAntennaOffset.BackColor = System.Drawing.Color.AliceBlue;
            this.nudToolAntennaOffset.DecimalPlaces = 1;
            this.nudToolAntennaOffset.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudToolAntennaOffset.InterceptArrowKeys = false;
            this.nudToolAntennaOffset.Location = new System.Drawing.Point(729, 174);
            this.nudToolAntennaOffset.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudToolAntennaOffset.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.nudToolAntennaOffset.Name = "nudToolAntennaOffset";
            this.nudToolAntennaOffset.ReadOnly = true;
            this.nudToolAntennaOffset.Size = new System.Drawing.Size(144, 52);
            this.nudToolAntennaOffset.TabIndex = 537;
            this.nudToolAntennaOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudToolAntennaOffset.Click += new System.EventHandler(this.nudToolAntennaOffset_Click);
            // 
            // btnConvertToToolOnly
            // 
            this.btnConvertToToolOnly.BackColor = System.Drawing.Color.Transparent;
            this.btnConvertToToolOnly.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Turquoise;
            this.btnConvertToToolOnly.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConvertToToolOnly.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConvertToToolOnly.ForeColor = System.Drawing.Color.Black;
            this.btnConvertToToolOnly.Location = new System.Drawing.Point(742, 26);
            this.btnConvertToToolOnly.Name = "btnConvertToToolOnly";
            this.btnConvertToToolOnly.Size = new System.Drawing.Size(122, 64);
            this.btnConvertToToolOnly.TabIndex = 541;
            this.btnConvertToToolOnly.Text = "Set Vehicle To Tool";
            this.btnConvertToToolOnly.UseVisualStyleBackColor = false;
            this.btnConvertToToolOnly.Click += new System.EventHandler(this.btnConvertToToolOnly_Click);
            // 
            // cboxToolOnlyGPS
            // 
            this.cboxToolOnlyGPS.Appearance = System.Windows.Forms.Appearance.Button;
            this.cboxToolOnlyGPS.BackColor = System.Drawing.Color.AliceBlue;
            this.cboxToolOnlyGPS.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.cboxToolOnlyGPS.FlatAppearance.CheckedBackColor = System.Drawing.Color.MediumAquamarine;
            this.cboxToolOnlyGPS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboxToolOnlyGPS.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboxToolOnlyGPS.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboxToolOnlyGPS.Location = new System.Drawing.Point(448, 496);
            this.cboxToolOnlyGPS.Name = "cboxToolOnlyGPS";
            this.cboxToolOnlyGPS.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cboxToolOnlyGPS.Size = new System.Drawing.Size(114, 78);
            this.cboxToolOnlyGPS.TabIndex = 542;
            this.cboxToolOnlyGPS.Text = "GPS Tool Only";
            this.cboxToolOnlyGPS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cboxToolOnlyGPS.UseVisualStyleBackColor = false;
            this.cboxToolOnlyGPS.Click += new System.EventHandler(this.cboxToolOnlyGPS_Click);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.ForeColor = System.Drawing.Color.Black;
            this.label28.Location = new System.Drawing.Point(681, 341);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(147, 52);
            this.label28.TabIndex = 545;
            this.label28.Text = "Button - Push Release, On.   \r\n               Push Release, Off\r\nSwitch - While P" +
    "ushed is On. \r\n               Released is Off.";
            // 
            // cboxSteerEnable
            // 
            this.cboxSteerEnable.BackColor = System.Drawing.Color.AliceBlue;
            this.cboxSteerEnable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxSteerEnable.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboxSteerEnable.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboxSteerEnable.FormattingEnabled = true;
            this.cboxSteerEnable.Items.AddRange(new object[] {
            "None",
            "Switch",
            "Button"});
            this.cboxSteerEnable.Location = new System.Drawing.Point(666, 295);
            this.cboxSteerEnable.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboxSteerEnable.Name = "cboxSteerEnable";
            this.cboxSteerEnable.Size = new System.Drawing.Size(175, 37);
            this.cboxSteerEnable.TabIndex = 543;
            // 
            // label62
            // 
            this.label62.BackColor = System.Drawing.Color.Transparent;
            this.label62.Font = new System.Drawing.Font("Tahoma", 15.75F);
            this.label62.ForeColor = System.Drawing.Color.Black;
            this.label62.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label62.Location = new System.Drawing.Point(663, 264);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(181, 29);
            this.label62.TabIndex = 544;
            this.label62.Text = "Steer Enable";
            this.label62.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // FormToolSteer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(395, 611);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.cboxSteerEnable);
            this.Controls.Add(this.label62);
            this.Controls.Add(this.cboxToolOnlyGPS);
            this.Controls.Add(this.btnConvertToToolOnly);
            this.Controls.Add(this.lblAntennaOffset);
            this.Controls.Add(this.lblAntennaHeight);
            this.Controls.Add(this.nudToolAntennaHeight);
            this.Controls.Add(this.nudToolAntennaOffset);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label70);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label68);
            this.Controls.Add(this.cboxMotorDrive);
            this.Controls.Add(this.cboxConv);
            this.Controls.Add(this.label63);
            this.Controls.Add(this.label64);
            this.Controls.Add(this.label55);
            this.Controls.Add(this.pboxSendSteer);
            this.Controls.Add(this.cboxDanfoss);
            this.Controls.Add(this.chkSteerInvertRelays);
            this.Controls.Add(this.chkInvertSteer);
            this.Controls.Add(this.chkInvertWAS);
            this.Controls.Add(this.btnSendSteerConfigPGN);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpButton = true;
            this.MaximumSize = new System.Drawing.Size(908, 652);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(411, 521);
            this.Name = "FormToolSteer";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Auto Steer Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSteer_FormClosing);
            this.Load += new System.EventHandler(this.FormSteer_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabSteer.ResumeLayout(false);
            this.tabSteer.PerformLayout();
            this.tabGain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pboxSendSteer)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudToolAntennaHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudToolAntennaOffset)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnFreeDrive;
        private System.Windows.Forms.Label lblHighSteerPWM;
        private System.Windows.Forms.HScrollBar hsbarHighSteerPWM;
        private System.Windows.Forms.Label lblWindupLimit;
        private System.Windows.Forms.HScrollBar hsbarWindupLimit;
        private System.Windows.Forms.Label lblProportionalGainXTE;
        private System.Windows.Forms.HScrollBar hsbarProportionalGainXTE;
        private System.Windows.Forms.Label lblMinPWM;
        private System.Windows.Forms.HScrollBar hsbarMinPWM;
        private System.Windows.Forms.Label lblMaxSteerAngle;
        private System.Windows.Forms.HScrollBar hsbarMaxSteerAngle;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblToolDistanceSet;
        private System.Windows.Forms.Label lblToolDistanceActual;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnFreeDriveZero;
        private ProXoft.WinForms.RepeatButton btnToolDistanceDown;
        private ProXoft.WinForms.RepeatButton btnToolDistanceUp;
        private System.Windows.Forms.Label lblPWMDisplay;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabGain;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TabPage tabSteer;
        private System.Windows.Forms.ProgressBar pbarRight;
        private System.Windows.Forms.ProgressBar pbarLeft;
        private System.Windows.Forms.Label lblActualSteerAngleUpper;
        private System.Windows.Forms.HScrollBar hsbarIntegralXTE;
        private System.Windows.Forms.Label lblIntegralXTE;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.ComboBox cboxMotorDrive;
        private System.Windows.Forms.ComboBox cboxConv;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.PictureBox pboxSendSteer;
        private System.Windows.Forms.CheckBox cboxDanfoss;
        private System.Windows.Forms.CheckBox chkSteerInvertRelays;
        private System.Windows.Forms.CheckBox chkInvertSteer;
        private System.Windows.Forms.CheckBox chkInvertWAS;
        private System.Windows.Forms.Button btnSendSteerConfigPGN;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblAntennaOffset;
        private System.Windows.Forms.Label lblAntennaHeight;
        private System.Windows.Forms.NumericUpDown nudToolAntennaHeight;
        private System.Windows.Forms.NumericUpDown nudToolAntennaOffset;
        private System.Windows.Forms.Button btnConvertToToolOnly;
        private System.Windows.Forms.CheckBox cboxToolOnlyGPS;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblWasOffset;
        private System.Windows.Forms.HScrollBar hsbarWasOffset;
        private System.Windows.Forms.HScrollBar hsbarCountsPerDegree;
        private System.Windows.Forms.Label lblCountsPerDegree;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.HScrollBar hsbarProportionalGainHyd;
        private System.Windows.Forms.Label lblProportionalGainHyd;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox cboxSteerEnable;
        private System.Windows.Forms.Label label62;
    }
}