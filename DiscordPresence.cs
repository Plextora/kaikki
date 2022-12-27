namespace Kaikki;

using DiscordRPC;

public static class DiscordPresence
{
    public static DiscordRpcClient Client = null!;
    
    static void SetPresence(string name, string displayName, string largeimgtext)
    {
        Client.SetPresence(new RichPresence
        {
            Details = $"This user is using {displayName}",
            State = "Made by Plextora#0033",
            Assets = new Assets
            {
                LargeImageKey = name,
                LargeImageText = largeimgtext
            }
        });
    }

    public static string RunDiscordRpc(string procName)
    {
        switch (procName)
        {
            case "firefox":
                SetPresence("firefox", "Firefox", "Firefox logo");
                return "Set Firefox presence";
            case "Discord":
                SetPresence("discord", "Discord", "Discord logo");
                return "Set Discord presence";
            case "rider64":
                SetPresence("rider", "Rider IDE", "Rider IDE logo");
                return "Set Rider IDE presence";
            case "ShareX":
                SetPresence("sharex", "ShareX", "ShareX logo");
                return "Set ShareX presence";
            case "Kaikki":
                Client.ClearPresence();
                return "User is focused on the Kaikki window. Clearing presence...";
            default:
                SetPresence("kaikki_logo", procName, "Kaikki logo");
                return "Unsupported process";
        }
    }
    
    public static void StopDiscordRpc()
    {
        Client.ClearPresence();
        Client.Deinitialize();
        Client.Dispose();
    }
}