
namespace AgOpenGPS.Forms.Pickers
{
    partial class FormRecordPicker
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
            this.lvLines = new System.Windows.Forms.ListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnDeleteField = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDeleteAB = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOpenExistingLv = new System.Windows.Forms.Button();
            this.btnTurnOffRecPath = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.bntKML = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.butRec = new System.Windows.Forms.Button();
            this.nudSpeed = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.ChkTool = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.nudSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // lvLines
            // 
            this.lvLines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvLines.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lvLines.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.Type});
            this.lvLines.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvLines.FullRowSelect = true;
            this.lvLines.GridLines = true;
            this.lvLines.HideSelection = false;
            this.lvLines.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.lvLines.Location = new System.Drawing.Point(5, 12);
            this.lvLines.MultiSelect = false;
            this.lvLines.Name = "lvLines";
            this.lvLines.Size = new System.Drawing.Size(967, 459);
            this.lvLines.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvLines.TabIndex = 87;
            this.lvLines.UseCompatibleStateImageBehavior = false;
            this.lvLines.View = System.Windows.Forms.View.Details;
            // 
            // chName
            // 
            this.chName.Text = "Record Name";
            this.chName.Width = 680;
            // 
            // btnDeleteField
            // 
            this.btnDeleteField.FlatAppearance.BorderSize = 0;
            this.btnDeleteField.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteField.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnDeleteField.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDeleteField.Image = global::AgOpenGPS.Properties.Resources.Trash;
            this.btnDeleteField.Location = new System.Drawing.Point(47, 503);
            this.btnDeleteField.Name = "btnDeleteField";
            this.btnDeleteField.Size = new System.Drawing.Size(71, 63);
            this.btnDeleteField.TabIndex = 95;
            this.btnDeleteField.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDeleteField.Click += new System.EventHandler(this.btnDeleteField_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(41, 489);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 96;
            this.label1.Text = "Delete Record";
            // 
            // btnDeleteAB
            // 
            this.btnDeleteAB.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDeleteAB.FlatAppearance.BorderSize = 0;
            this.btnDeleteAB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAB.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnDeleteAB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDeleteAB.Image = global::AgOpenGPS.Properties.Resources.Cancel64;
            this.btnDeleteAB.Location = new System.Drawing.Point(579, 503);
            this.btnDeleteAB.Name = "btnDeleteAB";
            this.btnDeleteAB.Size = new System.Drawing.Size(71, 63);
            this.btnDeleteAB.TabIndex = 97;
            this.btnDeleteAB.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(590, 489);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 16);
            this.label2.TabIndex = 98;
            this.label2.Text = "Cancel";
            // 
            // btnOpenExistingLv
            // 
            this.btnOpenExistingLv.BackColor = System.Drawing.Color.Transparent;
            this.btnOpenExistingLv.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnOpenExistingLv.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenExistingLv.Image = global::AgOpenGPS.Properties.Resources.FileOpen;
            this.btnOpenExistingLv.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpenExistingLv.Location = new System.Drawing.Point(697, 502);
            this.btnOpenExistingLv.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnOpenExistingLv.Name = "btnOpenExistingLv";
            this.btnOpenExistingLv.Size = new System.Drawing.Size(261, 63);
            this.btnOpenExistingLv.TabIndex = 99;
            this.btnOpenExistingLv.Text = "Use Selected";
            this.btnOpenExistingLv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOpenExistingLv.UseVisualStyleBackColor = false;
            this.btnOpenExistingLv.Click += new System.EventHandler(this.btnOpenExistingLv_Click);
            // 
            // btnTurnOffRecPath
            // 
            this.btnTurnOffRecPath.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnTurnOffRecPath.FlatAppearance.BorderSize = 0;
            this.btnTurnOffRecPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTurnOffRecPath.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnTurnOffRecPath.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTurnOffRecPath.Image = global::AgOpenGPS.Properties.Resources.SwitchOff;
            this.btnTurnOffRecPath.Location = new System.Drawing.Point(161, 502);
            this.btnTurnOffRecPath.Name = "btnTurnOffRecPath";
            this.btnTurnOffRecPath.Size = new System.Drawing.Size(71, 63);
            this.btnTurnOffRecPath.TabIndex = 100;
            this.btnTurnOffRecPath.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTurnOffRecPath.Click += new System.EventHandler(this.btnTurnOffRecPath_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(168, 489);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 101;
            this.label3.Text = "Path Off";
            // 
            // bntKML
            // 
            this.bntKML.FlatAppearance.BorderSize = 0;
            this.bntKML.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntKML.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.bntKML.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bntKML.Image = global::AgOpenGPS.Properties.Resources.BoundaryLoadFromGE;
            this.bntKML.Location = new System.Drawing.Point(333, 502);
            this.bntKML.Name = "bntKML";
            this.bntKML.Size = new System.Drawing.Size(71, 63);
            this.bntKML.TabIndex = 102;
            this.bntKML.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.bntKML.Click += new System.EventHandler(this.bntKML_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(352, 488);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 16);
            this.label4.TabIndex = 103;
            this.label4.Text = "KML";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(260, 489);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 16);
            this.label5.TabIndex = 104;
            this.label5.Text = "Rec";
            // 
            // butRec
            // 
            this.butRec.FlatAppearance.BorderSize = 0;
            this.butRec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butRec.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.butRec.ForeColor = System.Drawing.SystemColors.ControlText;
            this.butRec.Image = global::AgOpenGPS.Properties.Resources.RecPath;
            this.butRec.Location = new System.Drawing.Point(238, 502);
            this.butRec.Name = "butRec";
            this.butRec.Size = new System.Drawing.Size(71, 63);
            this.butRec.TabIndex = 105;
            this.butRec.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butRec.Click += new System.EventHandler(this.butRec_Click);
            // 
            // nudSpeed
            // 
            this.nudSpeed.DecimalPlaces = 1;
            this.nudSpeed.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudSpeed.Location = new System.Drawing.Point(413, 508);
            this.nudSpeed.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudSpeed.Name = "nudSpeed";
            this.nudSpeed.Size = new System.Drawing.Size(65, 30);
            this.nudSpeed.TabIndex = 106;
            this.nudSpeed.Value = new decimal(new int[] {
            11,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(410, 489);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 16);
            this.label6.TabIndex = 107;
            this.label6.Text = "Speed";
            // 
            // ChkTool
            // 
            this.ChkTool.Appearance = System.Windows.Forms.Appearance.Button;
            this.ChkTool.AutoSize = true;
            this.ChkTool.Checked = true;
            this.ChkTool.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkTool.Location = new System.Drawing.Point(493, 508);
            this.ChkTool.Name = "ChkTool";
            this.ChkTool.Size = new System.Drawing.Size(44, 33);
            this.ChkTool.TabIndex = 108;
            this.ChkTool.Text = "On";
            this.ChkTool.UseVisualStyleBackColor = true;
            this.ChkTool.CheckedChanged += new System.EventHandler(this.ChkTool_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(490, 488);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 16);
            this.label7.TabIndex = 109;
            this.label7.Text = "Tool On/Off";
            // 
            // Type
            // 
            this.Type.Text = "Type";
            this.Type.Width = 120;
            // 
            // FormRecordPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 578);
            this.ControlBox = false;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ChkTool);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nudSpeed);
            this.Controls.Add(this.butRec);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bntKML);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnTurnOffRecPath);
            this.Controls.Add(this.btnOpenExistingLv);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDeleteAB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDeleteField);
            this.Controls.Add(this.lvLines);
            this.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormRecordPicker";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormRecordPicker";
            this.Load += new System.EventHandler(this.FormRecordPicker_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvLines;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.Button btnDeleteField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDeleteAB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOpenExistingLv;
        private System.Windows.Forms.Button btnTurnOffRecPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bntKML;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button butRec;
        protected System.Windows.Forms.NumericUpDown nudSpeed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox ChkTool;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ColumnHeader Type;
    }
}