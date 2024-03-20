using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace X10Database
{
    public partial class App : Application
    {
        static FuelDatabase database;
        public static FuelDatabase Database     // this is helpful in other pages, as App.Database is now available in other pages.
        {
            get
            {
                if (database == null)   // check if database is already connected
                {
                    database = new FuelDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("FuelSQLite.db3"));
                }
                return database;
            }
        }

      


        public App()
        {
            InitializeComponent();

            List<FuelPurchase> list = App.Database.GetItems();

            EntryCell eID = new EntryCell { Label = "ID:" };
            //EntryCell eDate = new EntryCell { Label = "Date:" };
            DatePicker datePicker = new DatePicker();
            ViewCell vcDate = new ViewCell{ View = datePicker };
            Slider slLitres = new Slider { Maximum = 100, Minimum = 1, ThumbColor = Color.DeepSkyBlue, MinimumTrackColor = Color.DeepPink, MaximumTrackColor = Color.Gray, WidthRequest = 300 };
            Slider slCost = new Slider { Maximum = 250, Minimum = 0.01, ThumbColor = Color.DeepSkyBlue, MinimumTrackColor = Color.DeepPink, MaximumTrackColor = Color.Gray, WidthRequest = 300 };
            StackLayout stkLitres = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children = { new Label { Text = "1", WidthRequest = 30 }, slLitres, new Label { Text = "100", WidthRequest = 30 } }
            };
            StackLayout stkCost = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children = { new Label { Text = "0.01", WidthRequest = 30 }, slCost, new Label { Text = "250", WidthRequest = 30 } }
            };

            ViewCell vcLitres  = new ViewCell { View = stkLitres };
            ViewCell vcCost = new ViewCell { View = stkCost };
            //EntryCell eLitres = new EntryCell { Label = "Litres:" };
            //EntryCell eCost = new EntryCell { Label = "Cost:" };

            Button btnSearch = new Button { Text = "Find" };

            btnSearch.Clicked += (s, e) =>  // find the matching purchase based on the typed in ID
            {
                FuelPurchase purchase = App.Database.GetItem(Convert.ToInt32(eID.Text));
                datePicker.Date = purchase.Date;
                slLitres.Value = purchase.Litres;
                slCost.Value = purchase.Cost;
            };

            Button btnNew = new Button { Text = "New" };
            btnNew.Clicked += (s, e) =>     // prepare for new entry
            {
                eID.Text = "0";
                datePicker.Date = DateTime.Now;
                slLitres.Value = 0;
                slCost.Value = 0;
            };

            Button btnDelete = new Button { Text = "Delete" };
            btnDelete.Clicked += (s, e) =>
            {
                FuelPurchase item = new FuelPurchase
                {
                    ID = Convert.ToInt32(eID.Text),
                    Date = Convert.ToDateTime(datePicker.Date),
                    Litres = Convert.ToDouble(slLitres.Value),
                    Cost = Convert.ToDouble(slCost.Value),
                };
                App.Database.DeleteItem(item);
            };

            Button btnSave = new Button { Text = "Save" };
            btnSave.Clicked += (s, e) =>
            {
                FuelPurchase item = new FuelPurchase
                {
                    ID = Convert.ToInt32(eID.Text),
                    Date = Convert.ToDateTime(datePicker.Date),
                    Litres = Convert.ToDouble(slLitres.Value),
                    Cost = Convert.ToDouble(slCost.Value),
                };
                App.Database.SaveItem(item);
            };

            List<int> ids = new List<int>();

            for (int i = 0; i < list.Count; i++)
            {
                ids.Add(Convert.ToInt32(list[i].ID));
            }


            Picker picker = new Picker
            {
                ItemsSource = ids,
            };
            
            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    Spacing = 5,
                    Padding = 5,
                    Children =
                    {
                        

                        new TableView
                        {
                            Intent = TableIntent.Form,
                            Root = new TableRoot
                            {
                                new TableSection("Fuel Purchase")
                                {
                                    eID, vcDate, vcLitres, vcCost,
                                }
                            }
                        },
                        new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Children = { btnSearch, btnSave, btnNew, btnDelete }
                        }
                    }
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

    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
