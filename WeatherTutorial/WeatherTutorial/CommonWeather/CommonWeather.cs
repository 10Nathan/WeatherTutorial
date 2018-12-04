using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherTutorial.CommonWeather
{
    class CommonWeather
    {
        public static string API_LINK = "http://api.openweathermap.org/data/2.5/weather";
        public static string API_KEY = "5cde65f77a18f526f61d3f7e0dcddca2";

        public static string API_Request(string lat, string lon)
        {
            StringBuilder strBuilder = new StringBuilder(API_LINK);

            strBuilder.AppendFormat("?lat={0}&lon={1}&APPID={2}", lat, lon, API_KEY);

            return strBuilder.ToString();
        }

        public static DateTime ConvertUnixTimeToDateTime(double unix)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dt = dt.AddSeconds(unix).ToLocalTime();
            return dt;
        }
    }
}
