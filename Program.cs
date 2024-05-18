using System.Text.Json;

namespace weathr_forecastr
{
    class Program
    {
        private static void GetWeather()
        {
            // Variables for the api
            string lon = "12.964";
            string lat = "56.508";
            Uri endpoint = new($"https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/lon/{lon}/lat/{lat}/data.json");
            HttpClient client = new();

            try
            {
                // Try to get data from smhi
                HttpResponseMessage result = client.GetAsync(endpoint).Result;
                result.EnsureSuccessStatusCode();

                // Deserialize the recieved json
                var json = result.Content.ReadAsStringAsync().Result;
                var weather = JsonSerializer.Deserialize<Weather>(json);

                // Loop through all recieved data
                foreach (var forecast in weather.timeSeries)
                {
                    // Ensure we're recieving the relevant forecast
                    if (forecast.validTime.ToString("yyyy-MM-dd HH") == DateTime.Now.ToString("yyyy-MM-dd HH"))
                    {
                        // Extract relevant info
                        // Date and time for the forecast
                        var dayofweek = forecast.validTime.DayOfWeek;
                        TimeSpan time = forecast.validTime.TimeOfDay;

                        // Current state of weather
                        float symbol = forecast.parameters[18].values[0];
                        string desc = weather.wsymb2[(int)symbol];

                        // Current temperature
                        string unit = "";
                        float degrees = 0f;
                        bool temperatureFound = false;

                        // For some reason the temperature parameter change location,
                        // therefore we have to loop through all parameters until we find it
                        for (int i = 0; i < forecast.parameters.Length; i++)
                        {
                            if (forecast.parameters[i].name == "t")
                            {
                                degrees = forecast.parameters[i].values[0];
                                unit = forecast.parameters[i].unit;
                                temperatureFound = true;
                                break;
                            }
                        }

                        // Write forecast to user
                        if (temperatureFound)
                            Console.WriteLine("Current weather in Halmstad ({0} @ {1}): {2} and {3}{4}", dayofweek, time, desc, degrees, unit);
                        else
                            Console.WriteLine("Current weather in Halmstad ({0} @ {1}): {2}. Temperature data not available.", dayofweek, time, desc);

                        break;
                    }
                }
            }
            // Catch exceptions
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Close connection and free up resources
                client.Dispose();
            }
        }

        static void Main()
        {
            Console.WriteLine("Weathr Forecastr");
            GetWeather();
        }
    }
}
