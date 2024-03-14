using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace X02_SimpleLayout
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // comment out code after this line ad uncomment it to get MainPage.xaml example to show up
            // MainPage = new MainPage();

            StackLayout layout = new StackLayout { HorizontalOptions = LayoutOptions.CenterAndExpand,VerticalOptions = LayoutOptions.CenterAndExpand,};
            Label label = new Label { Text = "Click the button" };
            Button btnClick = new Button { Text = "Click Me" };
            layout.Children.Add(label);
            layout.Children.Add(btnClick);

            btnClick.Clicked += (s, e) =>
            {
                label.Text = "You clicked the button";
                MainPage.BackgroundColor = Color.MediumAquamarine;
            };

            MainPage = new ContentPage { Content = layout };
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
}
