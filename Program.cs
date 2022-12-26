using System.Diagnostics;
using System.Runtime.InteropServices;

[DllImport("user32.dll")]
static extern IntPtr GetForegroundWindow();

[DllImport("user32.dll")]
static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

string GetForegroundProcessName()
{
    IntPtr hwnd = GetForegroundWindow();
    
    if (hwnd == IntPtr.Zero)
        return "Unknown";

    uint pid;
    GetWindowThreadProcessId(hwnd, out pid);

    foreach (Process p in Process.GetProcesses())
    {
        if (p.Id == pid)
            return p.ProcessName;
    }

    return "Unknown";
}

Console.WriteLine($"Focused process name: {GetForegroundProcessName()}");