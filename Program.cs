using System.Text.Json;

namespace weathr_forecastr
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Weathr Forecastr");

            string lon = "13.007812";
            string lat = "56.394448";
            Uri endpoint = new($"https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/lon/{lon}/lat/{lat}/data.json");
            HttpClient client = new();

            try
            {
                HttpResponseMessage result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                var weather = JsonSerializer.Deserialize<Weather>(json);

                foreach (var forecast in weather.timeSeries)
                {
                    if (forecast.validTime.Day == DateTime.UtcNow.Day && forecast.validTime.Hour == DateTime.UtcNow.Hour + 2)
                    {
                        var dayofweek = forecast.validTime.DayOfWeek;
                        TimeSpan time = forecast.validTime.TimeOfDay;
                        string unit = forecast.parameters[10].unit;
                        float degrees = forecast.parameters[10].values[0];
                        float wsybm2 = forecast.parameters[18].values[0];
                        string desc = "";

                        if (wsybm2 <= 2)
                            desc = "Sunny";
                        else if (wsybm2 >= 3 && wsybm2 <= 6)
                            desc = "Cloudy";
                        else if (wsybm2 == 7)
                            desc = "Foggy";
                        else if (wsybm2 >= 8 && wsybm2 <= 21)
                            desc = "Rainy";
                        else if (wsybm2 > 22)
                            desc = "Snowy";

                        Console.WriteLine("Current weather ({0} @ {1}): {2} and {3}{4}", dayofweek, time, desc, degrees, unit);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Dispose();
            }
            Console.ReadKey();
        }
    }
}
