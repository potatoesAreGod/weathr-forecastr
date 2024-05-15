<<<<<<< HEAD
﻿using System.Net;
using System.Text.Json;

namespace weathr_forecastr {
    class Program
    {
        static async Task FetchSmhi()
        {
            double lat = 58;
            double lon = 16;

            string apiUrl = $"https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/lon/{lon}/lat/{lat}/data.json";

            HttpClient client = new();

            try
            {
                // Make request
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();


            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                client.Dispose();
            }
        }

        static void Main()
        {
            Console.WriteLine("Weathr Forecastr");
            FetchSmhi();
            Console.ReadKey();
        }
    }
}
=======
﻿namespace weathrForecastr
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Weathr Forecastr");
            SmhiForecast.FetchSmhi();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
>>>>>>> d17d231d53605fcc53a86f16f46ca1ecd978a1cc
