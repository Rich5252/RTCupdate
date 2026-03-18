using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using static NativeMethods;


public static class NativeMethods
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SystemTime
    {
        public ushort Year;
        public ushort Month;
        public ushort DayOfWeek;
        public ushort Day;
        public ushort Hour;
        public ushort Minute;
        public ushort Second;
        public ushort Milliseconds;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool SetSystemTime(ref SystemTime st);

    internal static class PrivilegeHelper
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool LookupPrivilegeValue(string lpSystemName, string lpName, out long lpLuid);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool AdjustTokenPrivileges(IntPtr TokenHandle, bool DisableAllPrivileges, ref TOKEN_PRIVILEGES NewState, int BufferLength, IntPtr PreviousState, IntPtr ReturnLength);

        [StructLayout(LayoutKind.Sequential)]
        private struct TOKEN_PRIVILEGES
        {
            public int PrivilegeCount;
            public long Luid;
            public int Attributes;
        }

        private const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        private const int TOKEN_QUERY = 0x00000008;
        private const int SE_PRIVILEGE_ENABLED = 0x00000002;
        private const string SE_SYSTEMTIME_NAME = "SeSystemtimePrivilege";

        public static bool EnableSystemTimePrivilege()
        {
            if (!OpenProcessToken(System.Diagnostics.Process.GetCurrentProcess().Handle, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out IntPtr token))
                return false;

            if (!LookupPrivilegeValue(null, SE_SYSTEMTIME_NAME, out long luid))
                return false;

            TOKEN_PRIVILEGES tp = new TOKEN_PRIVILEGES
            {
                PrivilegeCount = 1,
                Luid = luid,
                Attributes = SE_PRIVILEGE_ENABLED
            };

            return AdjustTokenPrivileges(token, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
        }
    }
}




public class SystemClock
{
    // Replaces FormatUtcTime
    public string GetFormattedUtcTime()
        => DateTime.UtcNow.ToString("HH:mm:ss.fff");

    public string GetUtcTimeMs()
        => DateTime.UtcNow.ToString("HH:mm:ss.fff");

    // Replaces SetSystemTimeWithOffset
    public bool UpdateClock(DateTime utcTime, int offsetMs)
    {
        // Try to enable the privilege first
        if (!PrivilegeHelper.EnableSystemTimePrivilege())
        {
            Console.WriteLine("Could not enable SystemTime privilege. Are you running as Admin?");
            return false;
        }

        DateTime adjustedTime = utcTime.AddMilliseconds(-offsetMs);

        var st = new NativeMethods.SystemTime
        {
            Year = (ushort)adjustedTime.Year,
            Month = (ushort)adjustedTime.Month,
            Day = (ushort)adjustedTime.Day,
            Hour = (ushort)adjustedTime.Hour,
            Minute = (ushort)adjustedTime.Minute,
            Second = (ushort)adjustedTime.Second,
            Milliseconds = (ushort)adjustedTime.Millisecond
        };

        if (!NativeMethods.SetSystemTime(ref st))
        {
            int error = Marshal.GetLastWin32Error();
            Console.WriteLine($"Failed to set clock. Error Code: {error}");
            return false;
        }
        return true;
    }
}

public class NtpClient
{
    private const int NtpDataLength = 48;

    public DateTime GetNetworkTime(string ntpServer = "time.google.com")
    {
        var ntpData = new byte[48];
        ntpData[0] = 0x23;

        var addresses = Dns.GetHostAddresses(ntpServer);

        // Find the first IPv4 address in the list to match AddressFamily.InterNetwork
        var ipAddress = Array.Find(addresses, a => a.AddressFamily == AddressFamily.InterNetwork);

        if (ipAddress == null) throw new Exception("No IPv4 address found for server.");

        var endPoint = new IPEndPoint(ipAddress, 123);

        using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
        {
            socket.Connect(endPoint);
            socket.ReceiveTimeout = 3000; // 3 second timeout
            socket.Send(ntpData);
            socket.Receive(ntpData);
        }       

        return GetDateTimeFromPacket(ntpData);
    }


    public DateTime GetDateTimeFromPacket(byte[] ntpData)
    {
        const int serverReplyTime = 40;

        // 1. Extract the 4-byte integer part (Seconds since 1900)
        int intPartRaw = BitConverter.ToInt32(ntpData, serverReplyTime);

        // 2. Convert from Network Byte Order (Big-Endian) to Host Order (Little-Endian)
        // We cast to uint after the swap to handle the large number of seconds correctly
        uint secondsSince1900 = (uint)IPAddress.NetworkToHostOrder(intPartRaw);

        // 3. Convert NTP Epoch (1900) to Unix Epoch (1970)
        // Difference: 2,208,988,800 seconds
        const uint ntpToUnixEpochSeconds = 2208988800U;
        long unixSeconds = secondsSince1900 - ntpToUnixEpochSeconds;

        // 4. Extract Fraction part (bytes 44-47) for millisecond precision
        int fractPartRaw = BitConverter.ToInt32(ntpData, serverReplyTime + 4);
        uint fractPart = (uint)IPAddress.NetworkToHostOrder(fractPartRaw);
        double milliseconds = (fractPart * 1000.0) / 0x100000000L;

        // 5. Build the final DateTime
        return DateTime.UnixEpoch.AddSeconds(unixSeconds).AddMilliseconds(milliseconds);
    }
}


public class TimeMonitor
{
    public double GetOffsetInMilliseconds(string ntpServer = "pool.ntp.org")
    {
        var ntpData = new byte[48];
        ntpData[0] = 0x23;

        var addresses = Dns.GetHostAddresses(ntpServer);

        // Find the first IPv4 address in the list to match AddressFamily.InterNetwork
        var ipAddress = Array.Find(addresses, a => a.AddressFamily == AddressFamily.InterNetwork);

        if (ipAddress == null) throw new Exception("No IPv4 address found for server.");

        var endPoint = new IPEndPoint(ipAddress, 123);

        using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
        {
            socket.Connect(endPoint);

            // t0: Local Transmit Time
            DateTime t0 = DateTime.UtcNow;
            socket.Send(ntpData);

            socket.ReceiveTimeout = 3000;
            socket.Receive(ntpData);

            // t3: Local Receive Time
            DateTime t3 = DateTime.UtcNow;

            // Extract t1: Server Receive Time (bytes 32-39)
            ulong serverArrivalTicks = GetServerTicks(ntpData, 32);
            DateTime t1 = ParseNtpTime(serverArrivalTicks);

            // Extract t2: Server Transmit Time (bytes 40-47)
            ulong serverTransmitTicks = GetServerTicks(ntpData, 40);
            DateTime t2 = ParseNtpTime(serverTransmitTicks);

            // 1. Calculate the total time the packet was "away"
            double totalTime = (t3 - t0).TotalMilliseconds;

            // 2. Calculate how long the server "held" the packet
            double serverProcessingTime = (t2 - t1).TotalMilliseconds;

            // 3. True Round Trip (Actual wire travel time)
            double trueRoundTrip = totalTime - serverProcessingTime;

            // 4. Final Offset
            // (Average of the time difference at arrival and departure)
            double offset = ((t1 - t0).TotalMilliseconds + (t2 - t3).TotalMilliseconds) / 2;

            return offset;
        }
    }

    private ulong GetServerTicks(byte[] data, int offset)
    {
        ulong intPart = BitConverter.ToUInt32(data, offset);
        ulong fractPart = BitConverter.ToUInt32(data, offset + 4);

        if (BitConverter.IsLittleEndian)
        {
            intPart = SwapEndianness(intPart);
            fractPart = SwapEndianness(fractPart);
        }

        return (intPart << 32) | fractPart;
    }

    private DateTime ParseNtpTime(ulong ntpTicks)
    {
        uint seconds = (uint)(ntpTicks >> 32);
        uint fraction = (uint)(ntpTicks & 0xffffffff);
        double milliseconds = (seconds * 1000.0) + (fraction * 1000.0 / 0x100000000L);
        return new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(milliseconds);
    }

    private uint SwapEndianness(ulong x)
        => (uint)(((x & 0x000000ff) << 24) | ((x & 0x0000ff00) << 8) |
                  ((x & 0x00ff0000) >> 8) | ((x & 0xff000000) >> 24));
}