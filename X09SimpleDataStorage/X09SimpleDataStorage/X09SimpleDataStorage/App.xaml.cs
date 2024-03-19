using System;
using System.Globalization;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace X09SimpleDataStorage
{
    public partial class App : Application
    {
        bool lightOn;
        Switch lightSwitch;

        async void isLightOn()
        {
            // find key light and put value in result - can be null
            string result = await SecureStorage.GetAsync("light");

            if (result != null)
            {
                // convert to bool
                lightOn = result.Equals("true") ? true : false;
            }
            else
            {
                await SecureStorage.SetAsync("light", "false"); // set to false if never saved before
            }

        }

        public App()
        {
            InitializeComponent();
            Label label = new Label { Text = "Light Switch" };
            isLightOn(); // figure out what the saved value is (t/f)
            lightSwitch = new Switch { IsToggled = lightOn };
            lightSwitch.Toggled += (s, e) =>
            {
                if (lightSwitch.IsToggled)  // save the setting
                {
                    SecureStorage.SetAsync("light", "true");
                }
                else
                {
                    SecureStorage.SetAsync("light", "false");
                }
            };
            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Children = { label, lightSwitch }
                }
            };

            MainPage.BindingContext = lightSwitch;  // make it so switch can effect properties of the page
            MainPage.SetBinding(VisualElement.BackgroundColorProperty, "IsToggled", converter: new ToggledToColorConverter());
            // the color of the page will be based on whether the switch is on or off
            // it will use our converter from below to determine how to do that

        }

        

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }

    public class ToggledToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Color.LightGray : Color.DarkGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
