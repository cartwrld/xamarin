using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
            

namespace X08ListAndTableEX
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ToDoList : ContentPage
    {
        public ToDoList()
        {
            InitializeComponent();
            Title = "To-Do List";
            StackLayout baseLayout = new StackLayout { VerticalOptions = LayoutOptions.StartAndExpand, Spacing = 10 };

            /****** TODO LIST SETUP ******/
            StackLayout listLayout = new StackLayout { VerticalOptions = LayoutOptions.FillAndExpand };

            ObservableCollection<ToDo> list = new ObservableCollection<ToDo>
            {
                new ToDo { Description = "Pick up dry cleaning", Date = new DateTime(2024, 3, 15), Completed = false },
                new ToDo { Description = "Find missing child", Date = new DateTime(2024, 8, 19), Completed = false },
                new ToDo { Description = "Blaze with the boys", Date = new DateTime(2024, 4, 20), Completed = false },
            };

            ListView listView = new ListView()
            {
                ItemsSource = list,
                ItemTemplate = new DataTemplate(typeof(ToDoCell)),
                RowHeight = ToDoCell.RowHeight,
                HeightRequest = 2000
            };

            /****** ADD TO TODO LIST SETUP ******/

            StackLayout newLayout = new StackLayout { VerticalOptions = LayoutOptions.EndAndExpand };
            TableView table = new TableView { Intent = TableIntent.Form };

            EntryCell ecDesc = new EntryCell { Label = "Description" };
            SwitchCell scCompleted = new SwitchCell { Text = "Completed?" };
            DatePicker datePicker = new DatePicker { Format = "dddd, MMMM d, yyyy" };
            ViewCell vcDate = new ViewCell {View = datePicker };
            TableSection section = new TableSection("Add New Item") { ecDesc, scCompleted, vcDate };
            table.Root = new TableRoot() { section };
            Button saveBtn = new Button { Text = "Save" };
            bool updateItem = false;
            int updateIndex = -1;

            listView.ItemTapped += (sender, e) =>
            {
                listView.SelectedItem = null;
                ecDesc.Text = ((ToDo)e.Item).Description;
                datePicker.Date = ((ToDo)e.Item).Date;
                bool switchVal = ((ToDo)e.Item).Completed;
                scCompleted.On = switchVal;
                updateItem = true;
                updateIndex = e.ItemIndex;
            };

            listLayout.Children.Add(listView);

            saveBtn.Clicked += (sender, e) =>
            {
                string title = ecDesc.Text;
                bool completed = scCompleted.On;
                DateTime date = datePicker.Date;

                if (ecDesc.Text != "")
                {
                    if (updateItem)
                    {
                        ToDo td = list.ElementAt(updateIndex);
                        td.Description = title;
                        td.Completed = completed;
                        td.Date = date;
                        updateItem = false;
                    }
                    else
                    {
                        ToDo td = new ToDo { Description = title, Date = date, Completed = completed };
                        list.Add(td);
                    }

                    // reset controls
                    ecDesc.Text = "";
                    datePicker.Date = DateTime.Now;
                    scCompleted.On = false;
                }
                
            };

            newLayout.Children.Add(table);
            newLayout.Children.Add(saveBtn);

            baseLayout.Children.Add(listLayout);
            baseLayout.Children.Add(newLayout);

            Content = baseLayout;
        }
    }


    public class ToDoCell : ViewCell
    {
        public const int RowHeight = 90;

        public ToDoCell()
        {

            Label lblTitle = new Label { FontAttributes = FontAttributes.Bold };
            Label lblDate = new Label { FontAttributes = FontAttributes.Italic };
            StackLayout swLayout = new StackLayout { Orientation = StackOrientation.Horizontal, VerticalOptions = LayoutOptions.CenterAndExpand };
            Label lblCompleted = new Label { Text = "Completed?" };
            Switch swCompleted = new Switch { IsToggled = false, IsEnabled = false};

            lblTitle.SetBinding(Label.TextProperty, "Description");
            lblDate.SetBinding(Label.TextProperty, new Binding("Date", BindingMode.Default, new DateConverter()));
            swCompleted.SetBinding(Switch.IsToggledProperty, "Completed");

            swLayout.Children.Add(lblCompleted);
            swLayout.Children.Add(swCompleted);


            View = new StackLayout
            {
                Spacing = 8,
                Padding = 5,
                Children = { lblTitle, lblDate, swLayout }
            };

            /* !!!!!!!!!!!!! DO NOT DO THIS !!!!!!!!!!!!! */
            /* USE OnBindingContextChanged() TO DO STUFF WHEN BINDED TO-DO ITEM PROP IS CHANGED*/
            ToDo item = (ToDo)this.BindingContext; // the binding context of what this cell is holding (a ToDo instance)
            if (item != null)
            {
                item = null;
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            ToDo item = (ToDo)this.BindingContext; // the binding context of what this cell is holding (a ToDo instance)
            
            
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
}