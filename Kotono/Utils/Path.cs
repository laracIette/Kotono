namespace Kotono.Utils
{
    internal static class Path
    {
        internal const string ASSETS = @"..\..\..\Assets\";

        internal const string SHADERS = @"..\..\..\Graphics\Shaders\";

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
