using System.IO;
using System.Reflection;

namespace Alexhokl.Helpers
{
    public static class ResourceHelper
    {
        public static string GetManifestResourceText(string resourceName)
        {
            return
              GetManifestResourceText(
                Assembly.GetCallingAssembly(),
                resourceName);
        }

        public static string GetManifestResourceText(Assembly assembly, string resourceName)
        {
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
