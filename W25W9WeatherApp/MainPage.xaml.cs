using System.Threading.Tasks;

namespace W25W9WeatherApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            if (DeviceInfo.Platform == DevicePlatform.Android)
                stack.Background = Brush.MediumPurple;
        }

        private async void OnGetWeatherBtnClicked(object sender, EventArgs e)
        {
            // read the device's location
            var location = await Geolocation.Default.GetLocationAsync();
            var lat = location.Latitude;
            var lon = location.Longitude;

            // request url
            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&appid=adee4d9d26685357054efd2f06359807";

            // make the api call
            var myWeather = await WeatherProxy.GetWeatherAsync(url);

            // display the result
            CityLbl.Text = myWeather.name;
            TemperatureLbl.Text = myWeather.main.temp.ToString("F0") + "\u00B0C";
            ConditionsLbl.Text = myWeather.weather[0].description;

            // display image
            string iconUrl = $"https://openweathermap.org/img/wn/{myWeather.weather[0].icon}@2x.png";
            WeatherImg.Source = ImageSource.FromUri(new Uri(iconUrl));
        }
    }
}
