namespace Cake.Unity.Arguments
{
    public enum ForceGLCore
    {
        Auto = 0,
        v32 = 32,
        v33 = 33,
        v40 = 40,
        v41 = 41,
        v42 = 42,
        v43 = 43,
        v44 = 44,
        v45 = 45
    }

    internal static class ForceGLCoreExtension
    {
        public static string Render(this ForceGLCore version) =>
            version == ForceGLCore.Auto ? "" : ((int) version).ToString();
    }
}
