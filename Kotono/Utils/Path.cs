namespace Kotono.Utils
{
    public static class Path
    {
        // to replace by your path where your projects are located, this path is relative to the project's .dll folder
        private const string _path = "../../../../../";

        public const string Kotono = _path + "Kotono/Kotono/";

        // to replace by your path where your_project.csproj is located
        public const string Project = _path + "Kotono-TestApp/Kotono-TestApp/";
        public const string Assets = Project + "Assets/";
    }
}
