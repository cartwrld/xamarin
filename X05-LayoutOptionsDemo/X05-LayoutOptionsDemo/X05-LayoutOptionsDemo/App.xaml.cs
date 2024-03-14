using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace X05_LayoutOptionsDemo
{
    public partial class App : Application
    {
        static StackLayout stackLayout = new StackLayout 
        {
            BackgroundColor = Color.LightPink,
            Padding = 2,
            Spacing = 2
        }; 

        private static void AddElement(string text, LayoutOptions layoutOptions)
        {
            stackLayout.Children.Add(new Label
            {
                Text = text,
                HorizontalOptions = layoutOptions,
                VerticalOptions = layoutOptions,
                BackgroundColor = Color.White,
                HeightRequest = 30,
                WidthRequest = 160,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
            }) ;
            stackLayout.Children.Add(new BoxView
            {
                HeightRequest = 1,
                Color = Color.DodgerBlue,
            });
        }

        public App()
        {
            InitializeComponent();

            AddElement("Start", LayoutOptions.Start);
            AddElement("Center", LayoutOptions.Center);
            AddElement("End", LayoutOptions.End);
            AddElement("Fill", LayoutOptions.Fill);
            AddElement("StartAndExpand", LayoutOptions.StartAndExpand);
            AddElement("CenterAndExpand", LayoutOptions.CenterAndExpand);
            AddElement("EndAndExpand", LayoutOptions.EndAndExpand);
            AddElement("FillAndExpand", LayoutOptions.FillAndExpand);


            MainPage = new ContentPage { Content = stackLayout, Padding = 10};

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
