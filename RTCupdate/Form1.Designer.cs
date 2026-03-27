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
            label4 = new Label();
            label5 = new Label();
            tbCurrentOffset = new TextBox();
            btnIncRTCdelay = new Button();
            btnDecRTCdelay = new Button();
            btnNTPupdate = new Button();
            label8 = new Label();
            tbTXdelay = new TextBox();
            label9 = new Label();
            statusStrip1 = new StatusStrip();
            StatusNTP = new ToolStripStatusLabel();
            StatusError = new ToolStripStatusLabel();
            timer1 = new System.Windows.Forms.Timer(components);
            btnIncTXdelay = new Button();
            btnDecTXdelay = new Button();
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
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(9, 36);
            label4.Name = "label4";
            label4.Size = new Size(57, 15);
            label4.TabIndex = 5;
            label4.Text = "RTC delay";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(150, 36);
            label5.Name = "label5";
            label5.Size = new Size(23, 15);
            label5.TabIndex = 7;
            label5.Text = "ms";
            // 
            // tbCurrentOffset
            // 
            tbCurrentOffset.Location = new Point(99, 33);
            tbCurrentOffset.Name = "tbCurrentOffset";
            tbCurrentOffset.Size = new Size(45, 23);
            tbCurrentOffset.TabIndex = 6;
            tbCurrentOffset.Text = "150";
            // 
            // btnIncRTCdelay
            // 
            btnIncRTCdelay.Location = new Point(187, 32);
            btnIncRTCdelay.Name = "btnIncRTCdelay";
            btnIncRTCdelay.Size = new Size(38, 23);
            btnIncRTCdelay.TabIndex = 11;
            btnIncRTCdelay.Text = "Inc";
            btnIncRTCdelay.UseVisualStyleBackColor = true;
            btnIncRTCdelay.Click += btnIncrement_Click;
            // 
            // btnDecRTCdelay
            // 
            btnDecRTCdelay.Location = new Point(231, 32);
            btnDecRTCdelay.Name = "btnDecRTCdelay";
            btnDecRTCdelay.Size = new Size(38, 23);
            btnDecRTCdelay.TabIndex = 12;
            btnDecRTCdelay.Text = "Dec";
            btnDecRTCdelay.UseVisualStyleBackColor = true;
            btnDecRTCdelay.Click += btnDecrement_Click;
            // 
            // btnNTPupdate
            // 
            btnNTPupdate.Location = new Point(231, 3);
            btnNTPupdate.Name = "btnNTPupdate";
            btnNTPupdate.Size = new Size(38, 23);
            btnNTPupdate.TabIndex = 13;
            btnNTPupdate.Text = "Set";
            btnNTPupdate.UseVisualStyleBackColor = true;
            btnNTPupdate.Click += btnNTPupdate_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(150, 65);
            label8.Name = "label8";
            label8.Size = new Size(23, 15);
            label8.TabIndex = 18;
            label8.Text = "ms";
            // 
            // tbTXdelay
            // 
            tbTXdelay.Location = new Point(99, 62);
            tbTXdelay.Name = "tbTXdelay";
            tbTXdelay.Size = new Size(45, 23);
            tbTXdelay.TabIndex = 17;
            tbTXdelay.Text = "50";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(9, 65);
            label9.Name = "label9";
            label9.Size = new Size(75, 15);
            label9.TabIndex = 16;
            label9.Text = "TX UTC delay";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { StatusNTP, StatusError });
            statusStrip1.Location = new Point(0, 94);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(276, 22);
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
            timer1.Interval = 60000;
            timer1.Tick += timer1_Tick;
            // 
            // btnIncTXdelay
            // 
            btnIncTXdelay.Location = new Point(187, 61);
            btnIncTXdelay.Name = "btnIncTXdelay";
            btnIncTXdelay.Size = new Size(38, 23);
            btnIncTXdelay.TabIndex = 20;
            btnIncTXdelay.Text = "Inc";
            btnIncTXdelay.UseVisualStyleBackColor = true;
            btnIncTXdelay.Click += btnIncTXdelay_Click;
            // 
            // btnDecTXdelay
            // 
            btnDecTXdelay.Location = new Point(231, 61);
            btnDecTXdelay.Name = "btnDecTXdelay";
            btnDecTXdelay.Size = new Size(38, 23);
            btnDecTXdelay.TabIndex = 21;
            btnDecTXdelay.Text = "Dec";
            btnDecTXdelay.UseVisualStyleBackColor = true;
            btnDecTXdelay.Click += btnDecTXdelay_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(276, 116);
            Controls.Add(btnDecTXdelay);
            Controls.Add(btnIncTXdelay);
            Controls.Add(statusStrip1);
            Controls.Add(label8);
            Controls.Add(tbTXdelay);
            Controls.Add(label9);
            Controls.Add(btnNTPupdate);
            Controls.Add(btnIncRTCdelay);
            Controls.Add(btnDecRTCdelay);
            Controls.Add(label5);
            Controls.Add(tbCurrentOffset);
            Controls.Add(label4);
            Controls.Add(tbNTPserver);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "RTC Updater";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            Shown += Form1_Shown;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tbNTPserver;
        private Label label4;
        private Label label5;
        private TextBox tbCurrentOffset;
        private Button btnIncRTCdelay;
        private Button btnDecRTCdelay;
        private Button btnNTPupdate;
        private Label label8;
        private TextBox tbTXdelay;
        private Label label9;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel StatusNTP;
        private ToolStripStatusLabel StatusError;
        private System.Windows.Forms.Timer timer1;
        private Button btnIncTXdelay;
        private Button btnDecTXdelay;
    }
}
