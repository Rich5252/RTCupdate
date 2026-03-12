namespace RTCupdate
{
    public partial class Form1 : Form
    {
        public IniFile ini = new IniFile("settings.ini");
        private int AutoUpdateIntervalMs = 900000;              // 15 minute
        private NamedPipeClient _NamedPipeClient = new();       //coms with TXLink for UDP time offset monitoring

        public Form1()
        {
            InitializeComponent();
            this.TopMost = true;

            this.StartPosition = FormStartPosition.Manual;
            this.Top = int.Parse(ini.Read("Window", "Top", "100"));
            this.Left = int.Parse(ini.Read("Window", "Left", "100"));
            this.Width = int.Parse(ini.Read("Window", "Width", "300"));
            this.Height = int.Parse(ini.Read("Window", "Height", "172"));

            this.tbNTPserver.Text = ini.Read("Settings", "NTPServer", "time.google.com");
            this.tbDefaultOffset.Text = ini.Read("Settings", "DefaultOffset", "150");
            this.tbCurrentOffset.Text = ini.Read("Settings", "CurrentOffset", "150");
            this.tbIncrement.Text = ini.Read("Settings", "Increment", "50");
            AutoUpdateIntervalMs = int.Parse(ini.Read("Settings", "AutoUpdateIntervalMs", "900000"));

            RunSync(tbCurrentOffset.Text == "" ? 0 : int.Parse(tbCurrentOffset.Text), true);
        }

        public void RunSync(int offsetMs, bool RTCupdate = true)
        {
            var ntp = new NtpClient();
            var clock = new SystemClock();
            var monitor = new TimeMonitor();

            try
            {
                Console.WriteLine("Fetching NTP Time...");
                DateTime networkTime = ntp.GetNetworkTime(tbNTPserver.Text);



                if (RTCupdate)
                {
                    if (!clock.UpdateClock(networkTime, offsetMs))
                    {
                        StatusNTP.Text = "Failed to update clock";
                        return;
                    }
                }

                double diffMs = monitor.GetOffsetInMilliseconds(tbNTPserver.Text);

                string str = offsetMs.ToString();
                string strNPstatus = _NamedPipeClient.TryConnect() ? " +" : " -";
                _NamedPipeClient.SendCommand(str);      //send offset to TXLink for UDP time offset monitoring

                StatusNTP.Text = ($"Last NTP: {clock.GetFormattedUtcTime()}    RTCdiff: {diffMs:F0}ms {strNPstatus}");
            }
            catch (Exception ex)
            {
                StatusNTP.Text = ($"Error: {ex.Message}");
            }
        }

        private void btnNTPupdate_Click(object sender, EventArgs e)
        {
            RunSync(tbCurrentOffset.Text == "" ? 0 : int.Parse(tbCurrentOffset.Text), false);
        }

        private void btnResetDefault_Click(object sender, EventArgs e)
        {
            RunSync(tbCurrentOffset.Text == "" ? 0 : int.Parse(tbDefaultOffset.Text));
            tbCurrentOffset.Text = tbDefaultOffset.Text;
        }

        private void btnResetCurrent_Click(object sender, EventArgs e)
        {
            RunSync(tbCurrentOffset.Text == "" ? 0 : int.Parse(tbCurrentOffset.Text));
        }

        private void btnIncrement_Click(object sender, EventArgs e)
        {
            double cur = tbCurrentOffset.Text == "" ? 0 : int.Parse(tbCurrentOffset.Text);
            double inc = tbIncrement.Text == "" ? 0 : int.Parse(tbIncrement.Text);
            tbCurrentOffset.Text = (cur + inc).ToString();
            RunSync(tbCurrentOffset.Text == "" ? 0 : int.Parse(tbCurrentOffset.Text));
        }

        private void btnDecrement_Click(object sender, EventArgs e)
        {
            double cur = tbCurrentOffset.Text == "" ? 0 : int.Parse(tbCurrentOffset.Text);
            double inc = tbIncrement.Text == "" ? 0 : int.Parse(tbIncrement.Text);
            tbCurrentOffset.Text = (cur - inc).ToString();
            RunSync(tbCurrentOffset.Text == "" ? 0 : int.Parse(tbCurrentOffset.Text));
        }
        private void Form1_Activated(object sender, EventArgs e)
        {
            timer1.Interval = AutoUpdateIntervalMs;
            timer1.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                ini.Write("Window", "Top", this.Top.ToString());
                ini.Write("Window", "Left", this.Left.ToString());
                ini.Write("Window", "Width", this.Width.ToString());
                ini.Write("Window", "Height", this.Height.ToString());
            }

            ini.Write("Settings", "NTPServer", tbNTPserver.Text);
            ini.Write("Settings", "DefaultOffset", tbDefaultOffset.Text);
            ini.Write("Settings", "CurrentOffset", tbCurrentOffset.Text);
            ini.Write("Settings", "Increment", tbIncrement.Text);
        }


    }
}
