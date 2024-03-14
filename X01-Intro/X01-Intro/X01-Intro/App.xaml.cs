using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace X01_Intro
{
    public partial class App : Application
    {
        public static Size ScreenSize;
         public static Label label;

        const int padding = 10;
        const int spacing = 5;
        const int count = 5;

        public App()
        {
            // never delete this if you see it anywhere, don't put code before it
            InitializeComponent();

            /* content expects a view, or container/stack of views */
            label = new Label { Text = "Size?" };
            // MainPage = new ContentPage { Content = label };

            var boxSize = (ScreenSize.Width - 2 * padding + spacing) / count - spacing;
            MainPage = new ContentPage {
                Padding = padding,
                Content = new ColorGrid(boxSize, count, count)
                {
                    RowSpacing = spacing,
                    ColumnSpacing = spacing,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                }
            };
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


    public class ColorGrid : Grid
    {
        public ColorGrid(double boxSize, int rows, int cols)
        {
            // just a loop within a loop to create the grid
            for (var row  = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    var box = new BoxView
                    {
                        Color = Color.FromRgb(row * 256 / rows, col * 256 / cols, 127),
                        WidthRequest = boxSize,
                        HeightRequest = boxSize
                    };

                    Children.Add(box, row, col);
                }
            }
        }
    }
}
