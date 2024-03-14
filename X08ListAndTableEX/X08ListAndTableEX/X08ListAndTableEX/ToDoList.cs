using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using Xamarin.Forms;

namespace X08ListAndTableEX
{
    public class ToDoList : ContentPage
    {
        public ToDoList()
        {
            StackLayout baseLayout = new StackLayout { VerticalOptions = LayoutOptions.StartAndExpand, Spacing = 10};

            /****** TODO LIST SETUP ******/
            StackLayout listLayout = new StackLayout { VerticalOptions = LayoutOptions.FillAndExpand };

            ObservableCollection<ToDo> list = new ObservableCollection<ToDo>
            {
                new ToDo { Title = "Pick up dry cleaning", Date = new DateTime(2024, 3, 15), Completed = false },
                new ToDo { Title = "Find missing child", Date = new DateTime(2024, 8, 19), Completed = false },
                new ToDo { Title = "Blaze with the boys", Date = new DateTime(2024, 4, 20), Completed = false },
            };

            ListView listView = new ListView()
            {
                ItemsSource = list,
                ItemTemplate = new DataTemplate(typeof(ToDoCell)),
                RowHeight = ToDoCell.RowHeight,
                HeightRequest = 2000
            };

      


            listLayout.Children.Add(listView);

            /****** ADD TO TODO LIST SETUP ******/

            StackLayout newLayout = new StackLayout { VerticalOptions = LayoutOptions.EndAndExpand };
            TableView table = new TableView { Intent = TableIntent.Form };

            EntryCell ecTitle = new EntryCell { Label = "Description" };
            SwitchCell scCompleted = new SwitchCell { Text = "Completed?" };
            //DatePicker datePicker = new DatePicker();
            ViewCell vcDate = new ViewCell { View = new DatePicker() };

            TableSection section = new TableSection("Add new To-Do item") { ecTitle, scCompleted, vcDate};

            table.Root = new TableRoot() { section };

            Button saveBtn = new Button { Text = "Save" };

            saveBtn.Clicked += (sender, e) =>
            {
                if (ecTitle.Text == "")
                {
                    ToDo td = new ToDo { Title = };
                }
            };

            newLayout.Children.Add(table);
            newLayout.Children.Add(saveBtn);

            listView.ItemTapped += (sender, e) =>
            {
                listView.SelectedItem = null;
                ecTitle.Text = (ToDo)e.Item;
            };

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
            Switch swCompleted = new Switch { IsToggled = false};

            lblTitle.SetBinding(Label.TextProperty, "Title");
            lblDate.SetBinding(Label.TextProperty, "Date");
            swCompleted.SetBinding(Switch.IsToggledProperty, "Completed");

            swLayout.Children.Add(lblCompleted);
            swLayout.Children.Add(swCompleted);


            View = new StackLayout
            {
                Spacing = 8,
                Padding = 5,
                Children = { lblTitle, lblDate, swLayout }
            };


            // long pressing on a cell will print up the delete option
            MenuItem mi = new MenuItem { Text = "Delete", IsDestructive = true };
            mi.Clicked += (sender, e) =>
            {
                ListView lv = (ListView)this.Parent;

                ((ObservableCollection<ToDo>)lv.ItemsSource).Remove(BindingContext as ToDo);
            };

            ContextActions.Add(mi);


        }
    }


}