using System.Reflection;


namespace Alexhokl.Helpers
{
    public static class VersionHelper
    {
        public static string ApplicationVersion
        {
            get
            {
                return
                    Assembly.GetEntryAssembly().GetName().Version.ToString();
            }
        }
    }
}
