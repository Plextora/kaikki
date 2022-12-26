using System.Diagnostics;
using System.Runtime.InteropServices;
using DiscordRPC;

DiscordRpcClient client;
client = new DiscordRpcClient("932775385014353970");
client.Initialize();

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

void RunDiscordRpc()
{
    client.SetPresence(new RichPresence
    {
        Details = $"This user is using {GetForegroundProcessName()}",
        State = "Made by Plextora#0033",
        Assets = new Assets
        {
            LargeImageKey = "kaikki_logo",
            LargeImageText = "Kaikki Logo"
        }
    });
}

void StopDiscordRpc()
{
    client.ClearPresence();
    client.Deinitialize();
    client.Dispose();
}

Console.WriteLine($"Focused process name: {GetForegroundProcessName()}");

RunDiscordRpc();

Console.ReadLine();

StopDiscordRpc();