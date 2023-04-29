namespace EfWebApp.Models;

public class StartupInfo
{
    private StartupInfo(bool success)
    {
        Success = success;
    }

    private StartupInfo(bool success, string error) : this(success)
    {
        ErrorMessage = error;
    }

    public bool Success { get; private set; }

    public string? ErrorMessage { get; private set; }

    public static StartupInfo Ok()
    {
        return new StartupInfo(true);
    }

    public static StartupInfo Error(string error)
    {
        return new StartupInfo(false, error);
    }
}