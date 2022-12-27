using DiscordRPC;

namespace Kaikki;

public static class DiscordPresence
{
    public static DiscordRpcClient Client = null!;

    private static readonly Dictionary<string, PresenceDetails> SupportedPresenceDict = new()
    {
        {
            "firefox",
            new PresenceDetails(
                "firefox",
                "Firefox",
                "Firefox logo",
                "Set Firefox presence")
        },
        {
            "Discord",
            new PresenceDetails(
                "discord",
                "Discord",
                "Discord logo",
                "Set Discord presence")
        },
        {
            "rider64",
            new PresenceDetails(
                "rider",
                "Rider IDE",
                "Rider IDE logo",
                "Set Rider IDE presence")
        },
        {
            "ShareX",
            new PresenceDetails(
                "sharex",
                "ShareX",
                "ShareX logo",
                "Set ShareX presence")
        },
        {
            "ShellExperienceHost",
            new PresenceDetails(
                "windows",
                "a Windows component",
                "Windows logo",
                "Set ShellExperienceHost presence")
        },
        {
            "StartMenuExperienceHost",
            new PresenceDetails(
                "windows",
                "Windows Start Menu",
                "Windows logo",
                "Set Start Menu presence")
        },
        {
            "explorer",
            new PresenceDetails(
                "file_explorer",
                "File Explorer/Task bar",
                "File explorer logo",
                "Set explorer presence")
        },
        {
            "SearchApp",
            new PresenceDetails(
                "search_app",
                "Windows search",
                "Windows Search logo",
                "Set SearchApp (Windows search) presence")
        },
        {
            "Taskmgr",
            new PresenceDetails(
                "task_manager",
                "Task Manager",
                "Task Manager logo",
                "Set Task Manager presence")
        },
    };

    private static void SetPresence(string name, string displayName, string largeimgtext)
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
        if (SupportedPresenceDict.TryGetValue(procName, out var presenceDetails))
        {
            SetPresence(presenceDetails.Name, presenceDetails.DisplayName, presenceDetails.Imgtext);
            return presenceDetails.ReturnMessage;
        }

        if (procName == "Kaikki")
        {
            Client.ClearPresence();
            return "User is focused on the Kaikki window. Clearing presence...";
        }

        SetPresence("kaikki_logo", procName, "Kaikki logo");
        return "Unsupported process";
    }

    public static void StopDiscordRpc()
    {
        Client.ClearPresence();
        Client.Deinitialize();
        Client.Dispose();
    }

    private record PresenceDetails(
        string Name,
        string DisplayName,
        string Imgtext,
        string ReturnMessage
    );
}