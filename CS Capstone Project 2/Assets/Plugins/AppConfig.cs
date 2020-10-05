/// <summary>
/// Contain configuration of a specific App.
/// </summary>
static class AppConfig
{
    public static string AppUrl              = "wss://localhost:6868";
    public static string AppName             = "UnityApp";
    
    /// <summary>
    /// Name of directory where contain tmp data and logs file.
    /// </summary>
    public static string TmpAppDataDir       = "UnityApp";
    public static string ClientId            = "ctjlxU1zFAtQMQMbX4jOKBtGUOsd6oAqDJRk0CYt";
    public static string ClientSecret        = "aV4DZW3bLm7KT8NzUhkBqxRVTESJJoLY45PLyFa6lnLaINCW7PwFeNo8skSNchEsN5pclNOCbdoyRZYspbApNIWDYykDWmIoOfLGFqUMz1aBFgEcw6PgMpsEy32uTyOH";
    public static string AppVersion          = "1.0.1 Dev";
    
    /// <summary>
    /// License Id is used for App
    /// In most cases, you don't need to specify the license id. Cortex will find the appropriate license based on the client id
    /// </summary>
    public static string AppLicenseId        = "";
}