using System;
using System.IO;
using System.Net;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Alexhokl.Helpers
{
    public static class GoogleUrlShortener
    {
        /// <summary>
        /// Returns a shortened URL from the specified <c>url</c> using Google Shortener <c>shortenerUrl</c>.
        /// </summary>
        /// <see cref="https://developers.google.com/url-shortener/v1/getting_started"/>
        /// <param name="url">The URL to be shortened.</param>
        /// <param name="shortenerUrl">URL to Google Shortener.</param>
        public static string GetShortenedUrl(string url, string shortenerUrl)
        {
            HttpWebRequest request =
                HttpWebRequest.Create(shortenerUrl)
                as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";

            using (Stream stream = request.GetRequestStream())
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(
                        JsonConvert.SerializeObject(
                            new { longUrl = url }));
                }
            }

            try
            {
                using (HttpWebResponse webResponse = request.GetResponse() as HttpWebResponse)
                {
                    using (Stream stream = webResponse.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            string response = reader.ReadToEnd();
                            var json = JsonConvert.DeserializeObject(response) as JObject;
                            return json.Property("id").Value.ToString();
                        }
                    }
                }
            }
            catch (WebException webEx)
            {
                throw new ApplicationException(
                    string.Format(
                        "Unable to retrieve shortened URL from google. [ShortenerUrl:{0}], [UrlToBeShorten:{1}], [Status:{2}]",
                        request.RequestUri, url, webEx.Status.ToString()),
                    webEx);
            }
        }
    }
}
