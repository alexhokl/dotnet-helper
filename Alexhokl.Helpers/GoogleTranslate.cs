using System.Globalization;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Alexhokl.Helpers
{
    public static class GoogleTranslate
    {
        /// <summary>
        /// Translates the specified input.
        /// </summary>
        /// <param name="input">Text to be translated</param>
        /// <param name="inputCulture">Culture information of input string</param>
        /// <param name="outputCulture">Culture information of output string</param>
        /// <returns></returns>
        public static string Translate(string input, CultureInfo inputCulture, CultureInfo outputCulture)
        {
            return Translate(input, inputCulture, outputCulture, null);
        }

        /// <summary>
        /// Translates the specified input.
        /// </summary>
        /// <param name="input">Text to be translated</param>
        /// <param name="inputCulture">Culture information of input string</param>
        /// <param name="outputCulture">Culture information of output string</param>
        /// <param name="proxy">web proxy</param>
        /// <returns></returns>
        public static string Translate(string input, CultureInfo inputCulture, CultureInfo outputCulture, WebProxy proxy)
        {
            // Todo: Please update to use the new URL instead
            // see https://developers.google.com/translate/v2/getting_started

            string url =
                string.Format(
                    "http://ajax.googleapis.com/ajax/services/language/translate?v=1.0&langpair={0}|{1}&q={2}",
                    inputCulture.Name, outputCulture.Name, input);
            string jsonString = string.Empty;
            using (WebClient webClient = new WebClient())
            {
                if (proxy != null)
                    webClient.Proxy = proxy;
                webClient.Encoding = Encoding.UTF8;
                jsonString = webClient.DownloadString(url);
            }
            var json = JsonConvert.DeserializeObject(jsonString) as JObject;
            return json.Property("translatedText").Value.ToString();
        }

        /// <summary>
        /// Gets the culture information of the specified input string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="proxy">The proxy.</param>
        /// <returns></returns>
        public static CultureInfo Detect(string input, WebProxy proxy)
        {
            string url = string.Format("http://ajax.googleapis.com/ajax/services/language/detect?v=1.0&q={0}", input);
            string jsonString = string.Empty;
            using (WebClient webClient = new WebClient())
            {
                if (proxy != null)
                    webClient.Proxy = proxy;
                webClient.Encoding = Encoding.UTF8;
                jsonString = webClient.DownloadString(url);
            }
            var json = JsonConvert.DeserializeObject(jsonString) as JObject;
            var languageCode = json.Property("language").Value.ToString();
            return CultureInfo.GetCultureInfo(languageCode);
        }
    }
}
