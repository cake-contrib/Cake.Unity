namespace Cake.Unity.Arguments
{
    public enum ForceGLES
    {
        Auto = 0,
        v30 = 30,
        v31 = 31,
        v32 = 32
    }

    internal static class ForceGLESExtension
    {
        public static string Render(this ForceGLES version) =>
            version == ForceGLES.Auto ? "" : ((int)version).ToString();
    }
}
