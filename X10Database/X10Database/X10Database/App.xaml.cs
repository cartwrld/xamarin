using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
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

        static Picker picker;

        static EntryCell eID = new EntryCell { Label = "ID:" };
        static DatePicker datePicker = new DatePicker { Format = "dddd, MMMM dd, yyyy" };
        static ViewCell vcDate = new ViewCell { View = datePicker };
        static Slider slLitres = new Slider { Maximum = 100, Minimum = 1, ThumbColor = Color.DeepSkyBlue, MinimumTrackColor = Color.DeepPink, MaximumTrackColor = Color.Gray, WidthRequest = 300 };
        static Slider slCost = new Slider { Maximum = 250, Minimum = 0.01, ThumbColor = Color.DeepSkyBlue, MinimumTrackColor = Color.DeepPink, MaximumTrackColor = Color.Gray, WidthRequest = 300};

       

        static FuelPurchase currentFP;
        static Label lblDate = new Label { Text = "Date" };
        static Label txtDate = new Label { Text = (datePicker.Date).ToString(), FontSize = 20 };
        static Label lblLitres = new Label { Text = "Litres" };
        static Label txtLitres = new Label { Text = slLitres.Value.ToString(), FontSize = 20 };
        static Label lblCost = new Label { Text = "Cost" };
        static Label txtCost = new Label { Text = slCost.Value.ToString(), FontSize = 20 };

        static Button btnSearch;
        static Button btnNew;
        static Button btnDelete;
        static Button btnSave;

        public App()
        {
            InitializeComponent();

            List<FuelPurchase> list = App.Database.GetItems(); 

            List<int> ids = new List<int>();

            for (int i = 0; i < list.Count; i++)
            {
                ids.Add(Convert.ToInt32(list[i].ID));
            }

            picker = new Picker
            {
                Title = "Select a Fuel Purchase by ID",
                ItemsSource = ids,
                SelectedItem = null,
            };

            picker.SelectedIndexChanged += OnPickerSelectedIndexChanged;

            slLitres.ValueChanged += OnLitresSliderChanged;
            slCost.ValueChanged += OnCostSliderChanged;

            StackLayout stkLitres = SliderStack(1, slLitres, 100);
            StackLayout stkCost = SliderStack(0.01, slCost, 250);

            ViewCell vcLitres  = new ViewCell { View = stkLitres };
            ViewCell vcCost = new ViewCell { View = stkCost };
   
            StackLayout stkDateDisplay = HStack(lblDate, txtDate);
            StackLayout stkLitresDisplay = HStack(lblLitres, txtLitres);
            StackLayout stkCostDisplay = HStack(lblCost, txtCost);

            StackLayout display = new StackLayout 
            { 
                Orientation = StackOrientation.Vertical,
                Children = { stkDateDisplay, stkLitresDisplay, stkCostDisplay }
            };

            TableView tbl = new TableView { Intent = TableIntent.Form, Root = new TableRoot { new TableSection("Fuel Purchase") { eID, vcDate, vcLitres, vcCost} } };
            StackLayout tblStack = new StackLayout { VerticalOptions = LayoutOptions.Center, Spacing = 5, Padding = 5, Children = { picker, tbl, display, createButtons()} };
            ContentPage content = new ContentPage { Content = tblStack };

            MainPage = content;
        }

        StackLayout SliderStack(double min, Slider s, int max)
        {
            return new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children = { new Label { Text = min.ToString(), WidthRequest = 30 }, s, new Label { Text = max.ToString(), WidthRequest = 30 } }
            };
        }

        StackLayout createButtons()
        {
            btnSearch = new Button { Text = "Find" };
            btnNew = new Button { Text = "New" };
            btnDelete = new Button { Text = "Delete" };
            btnSave = new Button { Text = "Save" };

            btnSearch.Clicked += (s, e) =>  // find the matching purchase based on the typed in ID
            {
                FuelPurchase purchase = App.Database.GetItem(Convert.ToInt32(eID.Text));
                datePicker.Date = purchase.Date;
                slLitres.Value = purchase.Litres;
                slCost.Value = purchase.Cost;
            };

            btnNew.Clicked += (s, e) =>     // prepare for new entry
            {
                //picker.SelectedItem = null;
                eID.Text = "0";
                datePicker.Date = DateTime.Now;
                slLitres.Value = 0;
                slCost.Value = 0;
            };

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

            return new StackLayout { 
                Orientation = StackOrientation.Horizontal, 
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children = 
                { 
                    btnSearch,
                    btnNew,
                    btnDelete,
                    btnSave
                } 
            };
        }

        StackLayout HStack(Label l1, Label l2)
        {
            l1.WidthRequest = 100;
            l1.Padding = new Thickness(0, 5, 0, 0);
            l1.FontAttributes = FontAttributes.Bold;

            return new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                
                Children = { l1, l2 }
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
        
        void OnLitresSliderChanged(object sender, EventArgs e)
        {
            var slider = (Slider)sender;
            txtLitres.Text = (slider.Value).ToString();           
        }

        void OnCostSliderChanged(object sender, EventArgs e)
        {
            var slider = (Slider)sender;
            txtCost.Text = (slider.Value).ToString();
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
        }
    }

    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }

    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            return date.ToString("dddd, MMMM dd, yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}