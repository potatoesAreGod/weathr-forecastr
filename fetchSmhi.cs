using System.Text.Json;

namespace weathrForecastr
{
    public class SmhiForecast
    {
        public static async void FetchSmhi()
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

                // Read JSON
                string jsonData = await response.Content.ReadAsStringAsync();
                var jsonDataObject = JsonDocument.Parse(jsonData);
                Console.WriteLine("JSON data:");
                Console.WriteLine(jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                client.Dispose();
            }
        }
    }

}