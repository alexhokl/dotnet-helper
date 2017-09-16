using System;
using System.Globalization;
using System.Text;

namespace Alexhokl.Helpers
{
	public static class DateHelper
	{
		/// <summary>
		/// Returns a string specifying the given date. (e.g. 23rd August, 2007)
		/// </summary>
		/// <param name="date">Date</param>
		/// <returns></returns>
		public static string GetOldEnglishStyleDate(this DateTime date)
		{
			StringBuilder builder = new StringBuilder();

			builder.Append(GetOldEnglishStyleDay(date.Day));
			builder.Append(date.ToString("MMMM, yyyy", new DateTimeFormatInfo()));

			return builder.ToString();
		}

		/// <summary>
		/// Returns a string specifying the given date. (e.g. 23rd August, 2007)
		/// </summary>
		/// <param name="date">Date</param>
		/// <returns></returns>
		public static string GetOldEnglishStyleDate(this DateTime date, Formats format)
		{
			StringBuilder builder = new StringBuilder();
			builder.Append(GetOldEnglishStyleDay(date.Day));
			switch (format)
			{
				case Formats.DM:
					builder.Append(" " + date.ToString("MMMM", new DateTimeFormatInfo()));
					break;
				case Formats.DMY:
					builder.Append(" " + date.ToString("MMMM, yyyy", new DateTimeFormatInfo()));
					break;
			}
			return builder.ToString();
		}

		/// <summary>
		/// Returns a string specifying the given day. (e.g. 23rd)
		/// </summary>
		/// <param name="day">Day of month</param>
		/// <returns></returns>
		public static string GetOldEnglishStyleDay(int day)
		{
			StringBuilder builder = new StringBuilder(day.ToString());
			switch (day)
			{
				case 1:
				case 21:
				case 31:
					builder.Append("st");
					break;
				case 2:
				case 22:
					builder.Append("nd");
					break;
				case 3:
					builder.Append("rd");
					break;
				default:
					builder.Append("th");
					break;
			}
			return builder.ToString();
		}

	}

	public enum Formats
	{
		DMY,
		DM,
		D
	}
}