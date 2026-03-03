namespace RTCupdate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.TopMost = true;

            RunSync(tbCurrentOffset.Text == "" ? 0 : int.Parse(tbCurrentOffset.Text), false);
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

                StatusNTP.Text = ($"Last NTP: {clock.GetFormattedUtcTime()}    RTCdiff: {diffMs:F0}ms");
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
    }
}
