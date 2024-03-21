using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Essentials;
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

        static EntryCell eID = new EntryCell { Label = "ID:" };
        static DatePicker datePicker = new DatePicker();
        static ViewCell vcDate = new ViewCell { View = datePicker };
        static Slider slLitres = new Slider { Maximum = 100, Minimum = 1, ThumbColor = Color.DeepSkyBlue, MinimumTrackColor = Color.DeepPink, MaximumTrackColor = Color.Gray, WidthRequest = 300 };
        static Slider slCost = new Slider { Maximum = 250, Minimum = 0.01, ThumbColor = Color.DeepSkyBlue, MinimumTrackColor = Color.DeepPink, MaximumTrackColor = Color.Gray, WidthRequest = 300 };

        static FuelPurchase currentFP;



        public App()
        {
            InitializeComponent();

            Debug.WriteLine("I%#U(@^*%&^@#*%^(@#*&%#");

            List<FuelPurchase> list = App.Database.GetItems(); 
            List<int> ids = new List<int>();

            for (int i = 0; i < list.Count; i++)
            {
                ids.Add(Convert.ToInt32(list[i].ID));
            }

            Picker picker = new Picker
            {
                Title = "Select a Fuel Purchase by ID",
                ItemsSource = ids,
            };

            picker.SelectedIndexChanged += OnPickerSelectedIndexChanged;

            //picker.SetBinding(Picker.SelectedItemProperty, "SelectedFP");
            

           
            //eID.SetBinding(EntryCell.TextProperty, "SelectedFP.ID");
            //datePicker.SetBinding(DatePicker.DateProperty, "SelectedFP.Date");
            //slLitres.SetBinding(Slider.ValueProperty, "SelectedFP.Litres");
            //slCost.SetBinding(Slider.ValueProperty, "SelectedFP.Cost");


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

            
            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    Spacing = 5,
                    Padding = 5,
                    Children =
                    {
                        picker,

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

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            FuelPurchase selectedItem = App.Database.GetItem((int)(picker.SelectedItem));

            if (selectedIndex != -1)
            {
                FuelPurchase currentFP = App.Database.GetItem(selectedIndex);
                //eID.Text = picker.ItemsSource[selectedIndex] + "";
                eID.Text = (selectedItem.ID).ToString();
                datePicker.Date = selectedItem.Date;
                slLitres.Value = selectedItem.Litres;
                slCost.Value = selectedItem.Cost;
            }

            Debug.WriteLine("\n===========================================================\n");
            Debug.WriteLine($"\nSelected: {selectedIndex}\n");
            Debug.WriteLine("\n===========================================================\n");

            //eID.Text = selectedItem;

        }
    }

    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
