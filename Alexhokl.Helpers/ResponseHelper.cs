using System;
using System.Net.Http;
using System.Net.Http.Headers;


namespace Alexhokl.Helpers
{
    public static class ResponseHelper
    {
        /// <summary>
        /// Sets the cache control with the specified last modified date of the resource (data).
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="dataLastModifiedDate">The last modified date of the resource (data).</param>
        public static void SetCacheControl(this HttpResponseMessage response, DateTime dataLastModifiedDate)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            response.Content.Headers.LastModified = dataLastModifiedDate.ToUniversalTime();
            CacheControlHeaderValue cacheControl = new CacheControlHeaderValue();
            cacheControl.NoCache = false;
            cacheControl.MaxAge = new TimeSpan(0, 0, 1);
            response.Headers.CacheControl = cacheControl;
        }
    }
}
