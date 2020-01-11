using System.Globalization;


namespace Alexhokl.Helpers
{
    public static class CultureHelper
    {
        /// <summary>
        /// Gets the name of neutral culture from the specified culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public static string GetNeutralCultureName(this CultureInfo culture)
        {
            if (culture.IsNeutralCulture)
                return culture.Name;

            // this may not support all the situations
            return culture.Name.Substring(0, 2);
        }
    }
}
