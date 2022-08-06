using System.Net.Http;
using Newtonsoft.Json.Linq;

WeatherCLI weather = new WeatherCLI(args[0], args[1]);
dynamic data = weather.getWeather();

string displayText = $@"
Ubicación = {data.name}, {data.sys.country}
Descripción = {data.weather[0].description}
Temperatura = {data.main.temp} °C
Presión = {data.main.pressure} bar
Viento = {data.wind.speed} m/s
";

Console.WriteLine(displayText);
Console.ReadKey();
Console.Clear();

class WeatherCLI {

    private static readonly HttpClient client = new HttpClient();
    private string? city, country, apiKey;

    public WeatherCLI(string? city, string country){
        this.city = city;
        this.country = country;
        this.apiKey = "";
    }

    public object getWeather(){
        string apiURL = $"https://api.openweathermap.org/data/2.5/weather?q={city},{country}&appid={apiKey}&appid=${apiKey}&units=metric&lang=es";

        client.DefaultRequestHeaders.Clear();   

        var response = client.GetAsync(apiURL).Result;
        var data = response.Content.ReadAsStringAsync().Result;
        dynamic weatherJson = JObject.Parse(data);
        
        return weatherJson;
    }

    public void displayInfo(){

    }

}


