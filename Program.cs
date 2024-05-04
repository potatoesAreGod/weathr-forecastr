namespace weathrForecastr
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
