using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UniversalWeather.Model;

namespace UniversalWeather.Helper
{
    public class Helper
    {
        public static async Task<OpenWeather> GetWeather(string lat, string lon)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(CommonWeather.CommonWeather.API_Request(lat, lon));
                var resultText = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    try
                    {
                        var users = JsonConvert.DeserializeObject<OpenWeather>(resultText);
                        return users;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(resultText);
                        return null;
                    }
                }
                return null;
            }
        }

    }
}
