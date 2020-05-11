using System;
using Quartz;


namespace Alexhokl.Helpers.Quartz
{
    public static class Helper
    {
        public static string GetParameter(this IJobExecutionContext context, string key)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            return context.MergedJobDataMap[key] as string;
        }

        public static int? GetIntegerParameter(this IJobExecutionContext context, string key)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            string value = context.MergedJobDataMap[key] as string;
            if (value == null)
                return null;
            return int.Parse(value);
        }

        public static bool? GetBooleanParameter(this IJobExecutionContext context, string key)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            string value = context.MergedJobDataMap[key] as string;
            if (value == null)
                return null;
            return bool.Parse(value);
        }
    }
}
