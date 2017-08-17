namespace Cake.Unity
{
    /// <summary>
    /// Target build platform.
    /// https://docs.unity3d.com/ScriptReference/BuildTarget.html
    /// </summary>
    public enum UnityBuildTarget
    {
        None = 0,
        StandaloneLinux,
        StandaloneLinux64,
        StandaloneOSXUniversal,
        StandaloneWindows,
        StandaloneWindows64,
        Android,
        iOS,
        WSAPlayer,
        WebGL,
        Tizen,
        PSP2,
        PS4,
        XboxOne,
        SamsungTV
    }
}
