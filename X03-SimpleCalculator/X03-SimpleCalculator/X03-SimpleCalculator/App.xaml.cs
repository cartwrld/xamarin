using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace X03_SimpleCalculator
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            StackLayout layout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            Label title = new Label { Text = "Welcome to Simple Calculator" };
            Entry num1 = new Entry();
            Entry num2 = new Entry();
            Picker picker = new Picker { Title = "Select an operation" };

            Button calc = new Button { Text = "Calculate!" };
            Label answer = new Label();

            calc.Clicked += (s, e) =>
            {
                var n1 = int.Parse(num1.Text);
                var n2 = int.Parse(num2.Text);
                var op = picker.SelectedItem;

                var ans = "";

                switch (op)
                {
                    case "Add": ans = (n1 + n2) + ""; break;
                    case "Subtract": ans = (n1 - n2) + ""; break;
                    case "Multiply": ans = (n1 * n2) + ""; break;
                    case "Divide": ans = (n1 / n2) + ""; break;
                }

                answer.Text = ans;
            };

            //MainPage = new MainPage();
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
