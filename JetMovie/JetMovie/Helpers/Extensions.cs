using System;
using Microsoft.AspNetCore.Http;

namespace JetMovie.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("access-control-expose-headers", "Application-Error");
        }

        public static long ToUnixDate(this DateTime date)
        {
            return (long)date.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
