using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace X04_GridLayout
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            StackLayout layout = new StackLayout { Margin=30, VerticalOptions = LayoutOptions.Center};
            StackLayout topRow = new StackLayout { Orientation = StackOrientation.Horizontal };
            topRow.Children.Add(new Label { Text="Output:" });
            Label lblOutput = new Label();
            topRow.Children.Add(lblOutput);
            layout.Children.Add(topRow);

            Grid grid = new Grid();

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
 
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Button btn1 = new Button { Text = "1" };
            Button btn2 = new Button { Text = "2" };
            Button btn3 = new Button { Text = "3" };
            Button btn4 = new Button { Text = "4" };
            Button btn5 = new Button { Text = "5" };
            Button btn6 = new Button { Text = "6" };
            Button btn7 = new Button { Text = "7" };
            Button btn8 = new Button { Text = "8" };
            Button btn9 = new Button { Text = "9" };
            Button btn0 = new Button { Text = "0" };
            Button btnP = new Button { Text = "+" };
            Button btnM = new Button { Text = "-" };
            Button btnE = new Button { Text = "=" };

          
            grid.Children.Add(btn1, 0, 0);
            grid.Children.Add(btn2, 0, 1);
            grid.Children.Add(btn3, 0, 2);
            grid.Children.Add(btn4, 1, 0);
            grid.Children.Add(btn5, 1, 1);
            grid.Children.Add(btn6, 1, 2);
            grid.Children.Add(btn7, 2, 0);
            grid.Children.Add(btn8, 2, 1);
            grid.Children.Add(btn9, 2, 2);
            grid.Children.Add(btn0, 3, 0);
            grid.Children.Add(btnP, 4, 0);
            grid.Children.Add(btnM, 4, 1);
            grid.Children.Add(btnE, 4, 2);

            layout.Children.Add(grid);



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
