namespace Kotono.Utils
{
    internal static class Path
    {
        internal const string ROOT = @"..\..\..\";

        internal const string DATA = @"..\..\..\Data\";

        internal const string ASSETS = @"..\..\..\Assets\";

        internal const string SHADERS = @"..\..\..\Graphics\Shaders\";

        internal static string FromRoot(string path)
        {
            return System.IO.Path.Combine(ROOT, path);
        }

        internal static string FromData(string path)
        {
            return System.IO.Path.Combine(DATA, path);
        }

        internal static string FromAssets(string path)
        {
            return System.IO.Path.Combine(ASSETS, path);
        }

        internal static string FromShaders(string path)
        {
            return System.IO.Path.Combine(SHADERS, path);
        }
    }
}
