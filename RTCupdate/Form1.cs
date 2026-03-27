using Microsoft.VisualBasic.Devices;

namespace RTCupdate
{
    public partial class Form1 : Form
    {
        public IniFile ini = new IniFile("settings.ini");
        private int AutoUpdateIntervalMs = 900000;              // 15 minute
        private NamedPipeClient _NamedPipeClient = new();       //coms with TXLink for UDP time offset monitoring
        private int RTCinitCounter = 0;
        private int DefaultOffset = 300;                       //default offset
        private int Increment = 50;                              //default increment for buttons

        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Top = int.Parse(ini.Read("Window", "Top", "100"));
            this.Left = int.Parse(ini.Read("Window", "Left", "100"));
            this.Width = int.Parse(ini.Read("Window", "Width", "300"));
            this.Height = int.Parse(ini.Read("Window", "Height", "172"));

            this.tbNTPserver.Text = ini.Read("Settings", "NTPServer", "time.google.com");
            DefaultOffset = int.Parse(ini.Read("Settings", "DefaultOffset", "150"));
            this.tbCurrentOffset.Text = ini.Read("Settings", "CurrentOffset", "150");
            Increment = int.Parse(ini.Read("Settings", "Increment", "50"));
            this.tbTXdelay.Text = ini.Read("Settings", "TXdelay", "50");
            AutoUpdateIntervalMs = int.Parse(ini.Read("Settings", "AutoUpdateIntervalMs", "900000"));

            timer1.Interval = 30000;         //AutoUpdateIntervalMs;
            RTCinitCounter = 0;
            timer1.Start();

            this.WindowState = FormWindowState.Normal;
            this.TopMost = true;
        }

        private async void Form1_Shown(object sender, EventArgs e)
        {
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
                    if (!clock.UpdateClock(networkTime, offsetMs - 7))
                    {
                        StatusNTP.Text = "Failed to update clock";
                        return;
                    }
                }

                double diffMs = monitor.GetOffsetInMilliseconds(tbNTPserver.Text);

                // set required TXdelay from start of true UTC
                int TXdelay = offsetMs - (tbTXdelay.Text == "" ? 0 : int.Parse(tbTXdelay.Text));
                string str = TXdelay.ToString();
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
            RunSync(tbCurrentOffset.Text == "" ? 0 : int.Parse(tbCurrentOffset.Text), true);
        }


        private void btnIncrement_Click(object sender, EventArgs e)
        {
            double cur = tbCurrentOffset.Text == "" ? 0 : int.Parse(tbCurrentOffset.Text);
            double inc = Increment;
            tbCurrentOffset.Text = (cur + inc).ToString();
            RunSync(tbCurrentOffset.Text == "" ? 0 : int.Parse(tbCurrentOffset.Text), true);
        }

        private void btnDecrement_Click(object sender, EventArgs e)
        {
            double cur = tbCurrentOffset.Text == "" ? 0 : int.Parse(tbCurrentOffset.Text);
            double inc = Increment;
            tbCurrentOffset.Text = (cur - inc).ToString();
            RunSync(tbCurrentOffset.Text == "" ? 0 : int.Parse(tbCurrentOffset.Text), true);
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
            ini.Write("Settings", "DefaultOffset", DefaultOffset.ToString());
            ini.Write("Settings", "CurrentOffset", tbCurrentOffset.Text);
            ini.Write("Settings", "Increment", Increment.ToString());
            ini.Write("Settings", "TXdelay", tbTXdelay.Text);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RunSync(tbCurrentOffset.Text == "" ? 0 : int.Parse(tbCurrentOffset.Text), true);
            if (RTCinitCounter < 5)     //force RTC update a few times on startup to ensure it sticks
            {
                RunSync(tbCurrentOffset.Text == "" ? 0 : int.Parse(tbCurrentOffset.Text), true);
                RTCinitCounter++;
            }

            timer1.Interval = (timer1.Interval * 2 < AutoUpdateIntervalMs) ? timer1.Interval * 2 : AutoUpdateIntervalMs;
            timer1.Start();
        }

        private void btnIncTXdelay_Click(object sender, EventArgs e)
        {
            // send changed TXdelay to TXlink
            int Val = (tbTXdelay.Text == "" ? 0 : int.Parse(tbTXdelay.Text) + Increment);
            tbTXdelay.Text = Val.ToString();

            RunSync(tbCurrentOffset.Text == "" ? 0 : int.Parse(tbCurrentOffset.Text), false);
        }

        private void btnDecTXdelay_Click(object sender, EventArgs e)
        {
            // send changed TXdelay to TXlink
            int Val = (tbTXdelay.Text == "" ? 0 : int.Parse(tbTXdelay.Text) - Increment);
            Val = Val < 0 ? 0 : Val;
            tbTXdelay.Text = Val.ToString();

            RunSync(tbCurrentOffset.Text == "" ? 0 : int.Parse(tbCurrentOffset.Text), false);
        }

    }
}
