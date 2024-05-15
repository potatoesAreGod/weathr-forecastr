using System.Net;
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