using System.Diagnostics;
using System.Runtime.InteropServices;
using DiscordRPC;
using Kaikki;

DiscordPresence.Client = new DiscordRpcClient("932775385014353970");
DiscordPresence.Client.Initialize();

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

Console.WriteLine("Press ESC to stop");
do {
    while (!Console.KeyAvailable) {
        Thread.Sleep(1000);
    
        Console.WriteLine($"Focused process name: {GetForegroundProcessName()}");

        if (GetForegroundProcessName() != "Unknown")
        {
            string returnValue = DiscordPresence.RunDiscordRpc(GetForegroundProcessName());
            
            Console.WriteLine(returnValue == "Unsupported process"
                ? "Set default Discord presence"
                : returnValue);
        }
        else
        {
            DiscordPresence.Client.ClearPresence();
        }
    }       
} while (Console.ReadKey(true).Key != ConsoleKey.Escape);

DiscordPresence.StopDiscordRpc();