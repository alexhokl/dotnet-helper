using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace Alexhokl.Helpers
{
    public static class RequestHelper
    {
        /// <summary>
        /// Checks if the browser has the latest version in its cache
        /// by checking "If-Modified-Since" attribute in request header.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="resourceLastUpdatedDate">Last updated date of the resource in question.</param>
        /// <returns>
        ///   <c>true</c> if the browser has the latest version in its cache; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsBrowserHasCachedVersion(this HttpRequest request, DateTime resourceLastUpdatedDate)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return IsBrowserHasCachedVersion(request.GetIfModifiedSince(), resourceLastUpdatedDate);
        }

        /// <summary>
        /// Checks if the browser has the latest version in its cache
        /// by checking "If-Modified-Since" attribute in request header.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="resourceLastUpdatedDate">Last updated date of the resource in question.</param>
        /// <returns>
        ///   <c>true</c> if the browser has the latest version in its cache; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsBrowserHasCachedVersion(this HttpRequestMessage request, DateTime resourceLastUpdatedDate)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return IsBrowserHasCachedVersion(request.Headers.IfModifiedSince, resourceLastUpdatedDate);
        }

        /// <summary>
        /// Gets a clean path from context
        /// </summary>
        /// <returns></returns>
        public static string GetUrlWithSubdirectory(this IHttpContextAccessor accessor)
        {
            if (accessor == null)
                throw new ArgumentNullException(nameof(accessor));
            if (accessor.HttpContext == null)
                throw new ArgumentException("Context could not be null", nameof(accessor));

            var requestUrl = GetRequestUrl(accessor);
            var subDir = accessor.HttpContext == null ? "" : accessor.ToAbsolute("~/");
            return $"{requestUrl.Scheme}://{requestUrl.Authority}{subDir}".TrimEnd('/');
        }

        /// <summary>
        /// Gets the application path ending with a slash.
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationPath(this IHttpContextAccessor accessor)
        {
            if (accessor == null)
                throw new ArgumentNullException(nameof(accessor));
            if (accessor.HttpContext == null)
                throw new ArgumentException("Context could not be null", nameof(accessor));

            return
                accessor.HttpContext.Request.Path.Value.EndsWith('/') ?
                accessor.HttpContext.Request.Path.Value :
                $"{accessor.HttpContext.Request.Path.Value}/";
        }

        #region helper methods
        /// <summary>
        /// Checks if the browser has the latest version in its cache
        /// by checking "If-Modified-Since" attribute in request header.
        /// </summary>
        /// <param name="ifModifiedSince"><c>If-Modified-Since</c> in request header in UTC.</param>
        /// <param name="resourceLastUpdatedDate">Last updated date of the resource in question.</param>
        /// <returns>
        ///   <c>true</c> if the browser has the latest version in its cache; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsBrowserHasCachedVersion(DateTimeOffset? ifModifiedSince, DateTime resourceLastUpdatedDate)
        {
            if (!ifModifiedSince.HasValue)
                // not cached
                return false;

            double difference =
                ifModifiedSince.Value.Subtract(
                    resourceLastUpdatedDate.ToUniversalTime()).TotalSeconds;

            // browser does not count milliseconds
            if (difference >= 0 || Math.Abs(difference) < 1)
            {
                // the cached version is up-to-date
                return true;
            }

            // the cached version is out-of-date
            return false;
        }

        /// <summary>
        /// Gets the UTC of <c>If-Modified-Since</c> from request header.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private static DateTimeOffset? GetIfModifiedSince(this HttpRequest request)
        {
            string str = request.Headers["If-Modified-Since"];
            if (string.IsNullOrWhiteSpace(str))
                return null;

            DateTime date = DateTime.Parse(str);
            return date.ToUniversalTime();
        }

        private static Uri GetRequestUrl(this IHttpContextAccessor accessor)
        {
            if (accessor == null)
                throw new ArgumentNullException(nameof(accessor));
            if (accessor.HttpContext == null)
                throw new ArgumentException("Context could not be null", nameof(accessor));

            var request = accessor.HttpContext.Request;
            var builder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host
            };
            if (request.Host.Port.HasValue)
            {
                builder.Port = request.Host.Port.Value;
            }
            builder.Path = request.Path;
            builder.Query = request.QueryString.ToUriComponent();
            return builder.Uri;
        }

        private static string ToAbsolute(this IHttpContextAccessor httpContextAccessor, string contentPath)
        {
            var context = httpContextAccessor.HttpContext;
            return GenerateClientUrl(context.Request.PathBase, contentPath);
        }

        private static string GenerateClientUrl(PathString applicationPath, string path)
        {
            if (path.StartsWith("~/", StringComparison.Ordinal))
            {
                var segment = new PathString(path.Substring(1));
                return applicationPath.Add(segment).Value;
            }

            return path;
        }
        #endregion
    }
}
