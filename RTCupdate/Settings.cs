using System.Runtime.InteropServices;
using System.Text;

public class IniFile
{
    private readonly string _path;

    // Import the Win32 API functions
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder retVal, int size, string filePath);

    public IniFile(string fileName)
    {
        // This ensures the INI is always in the same folder as your EXE
        // *** MAY REQUIRE ADMIN PRIVILAGES ***
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        _path = Path.Combine(baseDir, fileName);
    }

    public void Write(string section, string key, string value)
    {
        WritePrivateProfileString(section, key, value, _path);
    }

    public string Read(string section, string key, string defaultValue = "")
    {
        var buffer = new StringBuilder(255);
        GetPrivateProfileString(section, key, "", buffer, 255, _path);
        string str = buffer.ToString();

        if (str == "" && defaultValue != "")
        {
            // If the key doesn't exist, create it with the default value
            Write(section, key, defaultValue);
            str = defaultValue;
        }
        return str;
    }
}