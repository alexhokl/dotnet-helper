using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace Alexhokl.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// Returns a delimited string from the specified list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        public static string ToDelimitedString<T>(this IEnumerable<T> list, string delimiter)
        {
            var query =
                from i in list
                select i.ToString();

            if (!query.Any())
                return string.Empty;

            return
                query.Aggregate((concat, i) =>
                    string.Concat(concat, delimiter, i));
        }

        /// <summary>
        /// Takes a camel case string and returns a delimited string.
        /// </summary>
        /// <param name="camelCaseString">The camel case string.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        public static string ToDelimitedString(this string camelCaseString, string delimiter)
        {
            Regex regex = new Regex("([A-Z])");
            var strings = regex.Split(camelCaseString);
            if (strings.Length == 0)
            {
                return string.Empty;
            }

            if (strings.Length == 1)
            {
                return strings.FirstOrDefault().ToLower();
            }

            return
                strings
                    .Where(s =>
                        !string.IsNullOrWhiteSpace(s))
                    .Aggregate((concat, i) =>
                        string.Format(
                            "{0}{1}{2}",
                            concat.ToLower(),
                            i.Length == 1 && i.ToUpper().Equals(i) ?
                                 delimiter :
                                string.Empty,
                            i.ToLower()));
        }

        /// <summary>
        /// To the number list. Assuming the list is comma separated.
        /// </summary>
        /// <param name="numberListString">The number list string.</param>
        /// <returns></returns>
        public static List<int> ToNumberList(this string numberListString)
        {
            string[] portalIdArray =
                numberListString.Split(
                    new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries);

            List<int> list = new List<int>();
            foreach (var i in portalIdArray)
            {
                int id = int.Parse(i);
                list.Add(id);
            }

            return list;
        }
    }
}
