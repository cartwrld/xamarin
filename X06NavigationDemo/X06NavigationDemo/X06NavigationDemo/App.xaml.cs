using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace X06NavigationDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            ToolbarItem tb = new ToolbarItem { Text="Root" };
            tb.Clicked += (sender, e) => { MainPage.Navigation.PopToRootAsync(); };
            MainPage.ToolbarItems.Add(tb);
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
