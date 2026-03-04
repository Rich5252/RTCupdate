namespace RTCupdate
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            tbNTPserver = new TextBox();
            label2 = new Label();
            tbDefaultOffset = new TextBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            tbCurrentOffset = new TextBox();
            label6 = new Label();
            label7 = new Label();
            tbIncrement = new TextBox();
            btnIncrement = new Button();
            btnDecrement = new Button();
            btnNTPupdate = new Button();
            btnResetDefault = new Button();
            btnResetCurrent = new Button();
            label8 = new Label();
            tbNTPDifference = new TextBox();
            label9 = new Label();
            statusStrip1 = new StatusStrip();
            StatusNTP = new ToolStripStatusLabel();
            StatusError = new ToolStripStatusLabel();
            timer1 = new System.Windows.Forms.Timer(components);
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 6);
            label1.Name = "label1";
            label1.Size = new Size(63, 15);
            label1.TabIndex = 0;
            label1.Text = "NTP server";
            // 
            // tbNTPserver
            // 
            tbNTPserver.Location = new Point(98, 3);
            tbNTPserver.Name = "tbNTPserver";
            tbNTPserver.Size = new Size(127, 23);
            tbNTPserver.TabIndex = 1;
            tbNTPserver.Text = "time.google.com";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 33);
            label2.Name = "label2";
            label2.Size = new Size(76, 15);
            label2.TabIndex = 2;
            label2.Text = "Default delay";
            // 
            // tbDefaultOffset
            // 
            tbDefaultOffset.Location = new Point(99, 30);
            tbDefaultOffset.Name = "tbDefaultOffset";
            tbDefaultOffset.Size = new Size(45, 23);
            tbDefaultOffset.TabIndex = 3;
            tbDefaultOffset.Text = "150";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(150, 33);
            label3.Name = "label3";
            label3.Size = new Size(23, 15);
            label3.TabIndex = 4;
            label3.Text = "ms";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(10, 59);
            label4.Name = "label4";
            label4.Size = new Size(78, 15);
            label4.TabIndex = 5;
            label4.Text = "Current delay";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(150, 59);
            label5.Name = "label5";
            label5.Size = new Size(23, 15);
            label5.TabIndex = 7;
            label5.Text = "ms";
            // 
            // tbCurrentOffset
            // 
            tbCurrentOffset.Location = new Point(99, 56);
            tbCurrentOffset.Name = "tbCurrentOffset";
            tbCurrentOffset.Size = new Size(45, 23);
            tbCurrentOffset.TabIndex = 6;
            tbCurrentOffset.Text = "150";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(15, 85);
            label6.Name = "label6";
            label6.Size = new Size(61, 15);
            label6.TabIndex = 8;
            label6.Text = "Increment";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(150, 85);
            label7.Name = "label7";
            label7.Size = new Size(23, 15);
            label7.TabIndex = 10;
            label7.Text = "ms";
            // 
            // tbIncrement
            // 
            tbIncrement.Location = new Point(99, 82);
            tbIncrement.Name = "tbIncrement";
            tbIncrement.Size = new Size(45, 23);
            tbIncrement.TabIndex = 9;
            tbIncrement.Text = "50";
            // 
            // btnIncrement
            // 
            btnIncrement.Location = new Point(187, 81);
            btnIncrement.Name = "btnIncrement";
            btnIncrement.Size = new Size(38, 23);
            btnIncrement.TabIndex = 11;
            btnIncrement.Text = "Inc";
            btnIncrement.UseVisualStyleBackColor = true;
            btnIncrement.Click += btnIncrement_Click;
            // 
            // btnDecrement
            // 
            btnDecrement.Location = new Point(231, 81);
            btnDecrement.Name = "btnDecrement";
            btnDecrement.Size = new Size(38, 23);
            btnDecrement.TabIndex = 12;
            btnDecrement.Text = "Dec";
            btnDecrement.UseVisualStyleBackColor = true;
            btnDecrement.Click += btnDecrement_Click;
            // 
            // btnNTPupdate
            // 
            btnNTPupdate.Location = new Point(231, 3);
            btnNTPupdate.Name = "btnNTPupdate";
            btnNTPupdate.Size = new Size(38, 23);
            btnNTPupdate.TabIndex = 13;
            btnNTPupdate.Text = "Get";
            btnNTPupdate.UseVisualStyleBackColor = true;
            btnNTPupdate.Click += btnNTPupdate_Click;
            // 
            // btnResetDefault
            // 
            btnResetDefault.Location = new Point(187, 29);
            btnResetDefault.Name = "btnResetDefault";
            btnResetDefault.Size = new Size(38, 23);
            btnResetDefault.TabIndex = 14;
            btnResetDefault.Text = "Set";
            btnResetDefault.UseVisualStyleBackColor = true;
            btnResetDefault.Click += btnResetDefault_Click;
            // 
            // btnResetCurrent
            // 
            btnResetCurrent.Location = new Point(187, 55);
            btnResetCurrent.Name = "btnResetCurrent";
            btnResetCurrent.Size = new Size(38, 23);
            btnResetCurrent.TabIndex = 15;
            btnResetCurrent.Text = "Set";
            btnResetCurrent.UseVisualStyleBackColor = true;
            btnResetCurrent.Click += btnResetCurrent_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(150, 114);
            label8.Name = "label8";
            label8.Size = new Size(23, 15);
            label8.TabIndex = 18;
            label8.Text = "ms";
            label8.Visible = false;
            // 
            // tbNTPDifference
            // 
            tbNTPDifference.Location = new Point(99, 111);
            tbNTPDifference.Name = "tbNTPDifference";
            tbNTPDifference.ReadOnly = true;
            tbNTPDifference.Size = new Size(45, 23);
            tbNTPDifference.TabIndex = 17;
            tbNTPDifference.Text = "150";
            tbNTPDifference.Visible = false;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(15, 114);
            label9.Name = "label9";
            label9.Size = new Size(85, 15);
            label9.TabIndex = 16;
            label9.Text = "NTP difference";
            label9.Visible = false;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { StatusNTP, StatusError });
            statusStrip1.Location = new Point(0, 111);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(284, 22);
            statusStrip1.TabIndex = 19;
            statusStrip1.Text = "statusStrip1";
            // 
            // StatusNTP
            // 
            StatusNTP.Name = "StatusNTP";
            StatusNTP.Size = new Size(59, 17);
            StatusNTP.Text = "Last NTP: ";
            // 
            // StatusError
            // 
            StatusError.Name = "StatusError";
            StatusError.Size = new Size(0, 17);
            StatusError.TextAlign = ContentAlignment.MiddleRight;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 900000;
            timer1.Tick += btnResetCurrent_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 133);
            Controls.Add(statusStrip1);
            Controls.Add(label8);
            Controls.Add(tbNTPDifference);
            Controls.Add(label9);
            Controls.Add(btnResetCurrent);
            Controls.Add(btnResetDefault);
            Controls.Add(btnNTPupdate);
            Controls.Add(btnDecrement);
            Controls.Add(btnIncrement);
            Controls.Add(label7);
            Controls.Add(tbIncrement);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(tbCurrentOffset);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(tbDefaultOffset);
            Controls.Add(label2);
            Controls.Add(tbNTPserver);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "RTC Updater";
            FormClosing += Form1_FormClosing;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tbNTPserver;
        private Label label2;
        private TextBox tbDefaultOffset;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox tbCurrentOffset;
        private Label label6;
        private Label label7;
        private TextBox tbIncrement;
        private Button btnIncrement;
        private Button btnDecrement;
        private Button btnNTPupdate;
        private Button btnResetDefault;
        private Button btnResetCurrent;
        private Label label8;
        private TextBox tbNTPDifference;
        private Label label9;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel StatusNTP;
        private ToolStripStatusLabel StatusError;
        private System.Windows.Forms.Timer timer1;
    }
}
