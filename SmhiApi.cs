namespace weathr_forecastr
{
    // From SMHI's forecast API, found at https://opendata.smhi.se/apidocs/metfcst/index.html
    public class Weather
    {
        public DateTime ApprovedTime { get; set; } // When the forecast was latest updated
        public DateTime ReferenceTime { get; set; } // When the forecast was approved by SMHI
        public Geometry Geometry { get; set; } // Where the forecast is valid
        public Timeseries[] timeSeries { get; set; } // Forecast hour by hour
        public string[] wsymb2 = [ // Definition of symbols
            "",
            "Clear sky",
            "Nearly clear sky",
            "Variable cloudiness",
            "Halfclear sky",
            "Cloudy sky",
            "Overcast",
            "Fog",
            "Light rain showers",
            "Moderate rain showers",
            "Heavy rain showers",
            "Thunderstorm",
            "Light sleet showers",
            "Moderate sleet showers",
            "Heavy sleet showers",
            "Light snow showers",
            "Moderate snow showers",
            "Heavy snow showers",
            "Light rain",
            "Moderate rain",
            "Heavy rain",
            "Thunder",
            "Light sleet",
            "Moderate sleet",
            "Heavy sleet",
            "Light snowfall",
            "Moderate snowfall",
            "Heavy snowfall"
        ];
    }

    public class Timeseries
    {
        public DateTime validTime { get; set; } // When the forecast is valid
        public Parameter[] parameters { get; set; } // Weather data
    }

    public class Geometry
    {
        public string Type { get; set; } // Single point or area
        public float[][] Coordinates { get; set; } // The coordinates
    }

    public class Parameter
    {
        public string name { get; set; } // Name of parameter
        public string levelType { get; set; } // The types are level above ground or level above sea
        public int level { get; set; } // The level in meters
        public string unit { get; set; } // Unit for values
        public float[] values { get; set; } // Actual value of the parameter
    }
}
