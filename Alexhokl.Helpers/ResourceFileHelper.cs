using System;
using System.Collections;
using System.Globalization;
using System.Net;
using System.Resources;


namespace Alexhokl.Helpers
{
    /// <summary>
    /// This class wraps all the logics for translating .NET resource file from one culture to another.
    /// </summary>
    public static class ResourceFileHelper
    {
        public static void Translate(
            string inputFilename,
            CultureInfo inputCulture,
            CultureInfo outputCulture)
        {
            throw new NotImplementedException();
        }

        public static void Translate(
            string inputFilename,
            string outputFilename,
            CultureInfo inputCulture,
            CultureInfo outputCulture)
        {
            Translate(inputFilename, outputFilename, inputCulture, outputCulture, null);
        }

        public static void Translate(
            string inputFilename,
            string outputFilename,
            CultureInfo inputCulture,
            CultureInfo outputCulture,
            WebProxy proxy)
        {
            using (ResXResourceReader reader = new ResXResourceReader(inputFilename))
            {
                using (ResXResourceWriter writer = new ResXResourceWriter(outputFilename))
                {
                    foreach (DictionaryEntry entry in reader)
                        writer.AddResource(
                            entry.Key.ToString(),
                            GoogleTranslate.Translate(entry.Value.ToString(), inputCulture, outputCulture, proxy));

                    writer.Generate();
                }
            }
        }
    }
}
