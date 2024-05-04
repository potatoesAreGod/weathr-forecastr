using System.Text.Json;

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

            // Print headers
            var headers = response.Headers;
            Console.WriteLine("Headers:");
            foreach (var header in headers)
            {
                Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }

            // Read JSON
            string jsonData = await response.Content.ReadAsStringAsync();
            var jsonDataObject = JsonDocument.Parse(jsonData);
            Console.WriteLine("JSON data:");
            Console.WriteLine(jsonData);

            if (jsonDataObject.RootElement.TryGetProperty("t", out JsonElement element))
            {
                Console.WriteLine("T value: {0}", element);
            }
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