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
    public static string ClientId            = "ySecV2p1HXHl8xaKkS4mu65NpSCCFUwyFNFCDE3n";
    public static string ClientSecret        = "gcXNGAprYLxvMBYKiPceeAysfqigEJSHWV0ofLEcnEqMFS9rxfbwidx8lK1PhlTVXpmxXP2OhqP543UwPMwmHJycgKtyU1jhdjVhgcPHcadQ8yGB9iDlDeperBqsI5i3";
    public static string AppVersion          = "1.0.1 Dev";
    
    /// <summary>
    /// License Id is used for App
    /// In most cases, you don't need to specify the license id. Cortex will find the appropriate license based on the client id
    /// </summary>
    public static string AppLicenseId        = "";
}