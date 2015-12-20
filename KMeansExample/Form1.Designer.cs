namespace KMeansExample
{
    partial class Form1
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
			this.btnRun = new System.Windows.Forms.Button();
			this.txtK = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtInputSize = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.lblSeqTime = new System.Windows.Forms.Label();
			this.lblParTime = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.lblSpeedup = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lblSpeedupD = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.lblParTimeD = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.lblSeqTimeD = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.txtInputSizeD = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.txtKD = new System.Windows.Forms.TextBox();
			this.btnRunD = new System.Windows.Forms.Button();
			this.label14 = new System.Windows.Forms.Label();
			this.txtDimD = new System.Windows.Forms.TextBox();
			this.pnlDisplay = new System.Windows.Forms.Panel();
			this.btnGen = new System.Windows.Forms.Button();
			this.btnStep = new System.Windows.Forms.Button();
			this.lblSpeedupDStatic = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.lblParTimeDStatic = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnRun
			// 
			this.btnRun.Enabled = false;
			this.btnRun.Location = new System.Drawing.Point(103, 57);
			this.btnRun.Name = "btnRun";
			this.btnRun.Size = new System.Drawing.Size(38, 23);
			this.btnRun.TabIndex = 6;
			this.btnRun.Text = "Run";
			this.btnRun.UseVisualStyleBackColor = true;
			this.btnRun.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtK
			// 
			this.txtK.Location = new System.Drawing.Point(163, 59);
			this.txtK.Name = "txtK";
			this.txtK.Size = new System.Drawing.Size(33, 20);
			this.txtK.TabIndex = 8;
			this.txtK.Text = "128";
			this.txtK.TextChanged += new System.EventHandler(this.txtK_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(144, 62);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(13, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "k";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(202, 62);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(27, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Size";
			// 
			// txtInputSize
			// 
			this.txtInputSize.Location = new System.Drawing.Point(235, 59);
			this.txtInputSize.Name = "txtInputSize";
			this.txtInputSize.Size = new System.Drawing.Size(57, 20);
			this.txtInputSize.TabIndex = 9;
			this.txtInputSize.Text = "16384";
			this.txtInputSize.TextChanged += new System.EventHandler(this.txtInputSize_TextChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(298, 62);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(62, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Seq time(s):";
			// 
			// lblSeqTime
			// 
			this.lblSeqTime.AutoSize = true;
			this.lblSeqTime.Location = new System.Drawing.Point(358, 62);
			this.lblSeqTime.Name = "lblSeqTime";
			this.lblSeqTime.Size = new System.Drawing.Size(16, 13);
			this.lblSeqTime.TabIndex = 7;
			this.lblSeqTime.Text = "---";
			// 
			// lblParTime
			// 
			this.lblParTime.AutoSize = true;
			this.lblParTime.Location = new System.Drawing.Point(465, 62);
			this.lblParTime.Name = "lblParTime";
			this.lblParTime.Size = new System.Drawing.Size(16, 13);
			this.lblParTime.TabIndex = 9;
			this.lblParTime.Text = "---";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(400, 62);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(59, 13);
			this.label6.TabIndex = 8;
			this.label6.Text = "Par time(s):";
			// 
			// lblSpeedup
			// 
			this.lblSpeedup.AutoSize = true;
			this.lblSpeedup.Location = new System.Drawing.Point(566, 62);
			this.lblSpeedup.Name = "lblSpeedup";
			this.lblSpeedup.Size = new System.Drawing.Size(16, 13);
			this.lblSpeedup.TabIndex = 11;
			this.lblSpeedup.Text = "---";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(510, 62);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(50, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "Speedup";
			// 
			// lblSpeedupD
			// 
			this.lblSpeedupD.AutoSize = true;
			this.lblSpeedupD.Location = new System.Drawing.Point(566, 17);
			this.lblSpeedupD.Name = "lblSpeedupD";
			this.lblSpeedupD.Size = new System.Drawing.Size(16, 13);
			this.lblSpeedupD.TabIndex = 22;
			this.lblSpeedupD.Text = "---";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(510, 17);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(50, 13);
			this.label7.TabIndex = 21;
			this.label7.Text = "Speedup";
			// 
			// lblParTimeD
			// 
			this.lblParTimeD.AutoSize = true;
			this.lblParTimeD.Location = new System.Drawing.Point(465, 17);
			this.lblParTimeD.Name = "lblParTimeD";
			this.lblParTimeD.Size = new System.Drawing.Size(16, 13);
			this.lblParTimeD.TabIndex = 20;
			this.lblParTimeD.Text = "---";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(400, 17);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(59, 13);
			this.label9.TabIndex = 19;
			this.label9.Text = "Par time(s):";
			// 
			// lblSeqTimeD
			// 
			this.lblSeqTimeD.AutoSize = true;
			this.lblSeqTimeD.Location = new System.Drawing.Point(358, 17);
			this.lblSeqTimeD.Name = "lblSeqTimeD";
			this.lblSeqTimeD.Size = new System.Drawing.Size(16, 13);
			this.lblSeqTimeD.TabIndex = 18;
			this.lblSeqTimeD.Text = "---";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(298, 17);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(62, 13);
			this.label11.TabIndex = 17;
			this.label11.Text = "Seq time(s):";
			// 
			// txtInputSizeD
			// 
			this.txtInputSizeD.Location = new System.Drawing.Point(235, 14);
			this.txtInputSizeD.Name = "txtInputSizeD";
			this.txtInputSizeD.Size = new System.Drawing.Size(57, 20);
			this.txtInputSizeD.TabIndex = 3;
			this.txtInputSizeD.Text = "16384";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(202, 17);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(27, 13);
			this.label12.TabIndex = 5;
			this.label12.Text = "Size";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(144, 17);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(13, 13);
			this.label13.TabIndex = 3;
			this.label13.Text = "k";
			// 
			// txtKD
			// 
			this.txtKD.Location = new System.Drawing.Point(162, 14);
			this.txtKD.Name = "txtKD";
			this.txtKD.Size = new System.Drawing.Size(33, 20);
			this.txtKD.TabIndex = 2;
			this.txtKD.Text = "16";
			// 
			// btnRunD
			// 
			this.btnRunD.Location = new System.Drawing.Point(15, 12);
			this.btnRunD.Name = "btnRunD";
			this.btnRunD.Size = new System.Drawing.Size(64, 23);
			this.btnRunD.TabIndex = 0;
			this.btnRunD.Text = "Run";
			this.btnRunD.UseVisualStyleBackColor = true;
			this.btnRunD.Click += new System.EventHandler(this.buttonD_Click);
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(85, 17);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(13, 13);
			this.label14.TabIndex = 1;
			this.label14.Text = "d";
			// 
			// txtDimD
			// 
			this.txtDimD.Location = new System.Drawing.Point(104, 14);
			this.txtDimD.Name = "txtDimD";
			this.txtDimD.Size = new System.Drawing.Size(33, 20);
			this.txtDimD.TabIndex = 1;
			this.txtDimD.Text = "15";
			// 
			// pnlDisplay
			// 
			this.pnlDisplay.BackColor = System.Drawing.SystemColors.Window;
			this.pnlDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlDisplay.Location = new System.Drawing.Point(12, 86);
			this.pnlDisplay.Name = "pnlDisplay";
			this.pnlDisplay.Size = new System.Drawing.Size(791, 536);
			this.pnlDisplay.TabIndex = 25;
			// 
			// btnGen
			// 
			this.btnGen.Location = new System.Drawing.Point(15, 57);
			this.btnGen.Name = "btnGen";
			this.btnGen.Size = new System.Drawing.Size(38, 23);
			this.btnGen.TabIndex = 4;
			this.btnGen.Text = "Gen";
			this.btnGen.UseVisualStyleBackColor = true;
			this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
			// 
			// btnStep
			// 
			this.btnStep.Enabled = false;
			this.btnStep.Location = new System.Drawing.Point(59, 57);
			this.btnStep.Name = "btnStep";
			this.btnStep.Size = new System.Drawing.Size(38, 23);
			this.btnStep.TabIndex = 5;
			this.btnStep.Text = "Step";
			this.btnStep.UseVisualStyleBackColor = true;
			this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
			// 
			// lblSpeedupDStatic
			// 
			this.lblSpeedupDStatic.AutoSize = true;
			this.lblSpeedupDStatic.Location = new System.Drawing.Point(772, 17);
			this.lblSpeedupDStatic.Name = "lblSpeedupDStatic";
			this.lblSpeedupDStatic.Size = new System.Drawing.Size(16, 13);
			this.lblSpeedupDStatic.TabIndex = 29;
			this.lblSpeedupDStatic.Text = "---";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(706, 17);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(65, 13);
			this.label8.TabIndex = 28;
			this.label8.Text = "[STC]Speed";
			// 
			// lblParTimeDStatic
			// 
			this.lblParTimeDStatic.AutoSize = true;
			this.lblParTimeDStatic.Location = new System.Drawing.Point(671, 17);
			this.lblParTimeDStatic.Name = "lblParTimeDStatic";
			this.lblParTimeDStatic.Size = new System.Drawing.Size(16, 13);
			this.lblParTimeDStatic.TabIndex = 27;
			this.lblParTimeDStatic.Text = "---";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(606, 17);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(64, 13);
			this.label15.TabIndex = 26;
			this.label15.Text = "[STC]Par(s):";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(674, 57);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(114, 23);
			this.button1.TabIndex = 30;
			this.button1.Text = "<debug>GenInput";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Visible = false;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(815, 634);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.lblSpeedupDStatic);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.lblParTimeDStatic);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.btnRun);
			this.Controls.Add(this.btnStep);
			this.Controls.Add(this.btnGen);
			this.Controls.Add(this.txtK);
			this.Controls.Add(this.pnlDisplay);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtDimD);
			this.Controls.Add(this.txtInputSize);
			this.Controls.Add(this.lblSpeedupD);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.lblSeqTime);
			this.Controls.Add(this.lblParTimeD);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.lblParTime);
			this.Controls.Add(this.lblSeqTimeD);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.lblSpeedup);
			this.Controls.Add(this.txtInputSizeD);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.txtKD);
			this.Controls.Add(this.btnRunD);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.Text = "k-Means Tester";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TextBox txtK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInputSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSeqTime;
        private System.Windows.Forms.Label lblParTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSpeedup;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblSpeedupD;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblParTimeD;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSeqTimeD;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtInputSizeD;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtKD;
        private System.Windows.Forms.Button btnRunD;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtDimD;
        private System.Windows.Forms.Panel pnlDisplay;
        private System.Windows.Forms.Button btnGen;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.Label lblSpeedupDStatic;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblParTimeDStatic;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button button1;
    }
}

