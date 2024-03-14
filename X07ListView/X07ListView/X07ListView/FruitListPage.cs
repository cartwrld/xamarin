using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace X07ListView
{
    public class FruitListPage : ContentPage
    {
        public FruitListPage()
        {
            ObservableCollection<Fruit> list = new ObservableCollection<Fruit>
            {
                new Fruit { Name = "Apple", Desc = "Red" },
                new Fruit { Name = "Banana", Desc = "Yellow" },
                new Fruit { Name = "Orange", Desc = "Orange" }
            };

            ListView listView = new ListView()
            {
                ItemsSource = list,
                // assign the cell template we created below as the template for each row in the list view
                ItemTemplate = new DataTemplate(typeof(FruitCell)),
                RowHeight = FruitCell.RowHeight,
            };

            // list view itself can determine what type of collection it is
            listView.ItemTapped += (sender, e) =>
            {
                listView.SelectedItem = null;   // deselect the item
                Navigation.PushAsync(new FruitDetailPage((Fruit)e.Item));  // e.Item =  underlying object that the item is showing
                
                // delete on click
                //((ObservableCollection<Fruit>)listView.ItemsSource).Remove(e.Item as Fruit); // in this case, list.Remove(...) could be used also
                
            };

            Title = "Fruit List";

            StackLayout layout = new StackLayout();
            layout.Children.Add(listView);
            Entry eName = new Entry();
            Entry eDesc = new Entry();
            Button btnNew = new Button {  Text = "Save New Fruit" };
            btnNew.Clicked += (sender, e) =>
            {
                Fruit f = new Fruit {  Name = eName.Text, Desc = eDesc.Text };
                list.Add(f);
                eName.Text = "";
                eDesc.Text = "";
            };

            layout.Children.Add(eName); 
            layout.Children.Add(eDesc); 
            layout.Children.Add(btnNew);

            // the following is an example of how to use pull-to-refresh
            ListView listview2 = new ListView
            {
                ItemsSource = GetTime(),    // method that gets our data
                IsPullToRefreshEnabled = true
            };

            listview2.Refreshing += (sender, e) =>
            {
                // handle what to do in an event of a refresh
                listview2.ItemsSource = GetTime();  // get the "new" data
                listview2.EndRefresh();
            };

            layout.Children.Add(listview2);

            Content = layout;
        }

        // nonesense method to get some "data" to show in list
        private List<string> GetTime()
        {
            List<string> list = new List<string>();
            list.Add(DateTime.Now.ToString());
            list.Add(DateTime.Now.ToString());
            list.Add(DateTime.Now.ToString());
            list.Add(DateTime.Now.ToString());
            return list;
        }

    }
    public class FruitCell : ViewCell
    {
        public const int RowHeight = 55;
        public FruitCell()
        {
            // typically two tasks to do here:
            // 1) Creating a layout for the view (specifying what it will look like) [view property]
            // 2) Bind any dynamic labels (etc) to properties that will display (from data class - Fruit)

            // since text is dynamic, we use SetBinding
            Label lblName = new Label { FontAttributes = FontAttributes.Bold };
            // target property (usually text) will show ""
            lblName.SetBinding(Label.TextProperty, "Name");

            Label lblDesc = new Label { TextColor = Color.Gray };
            lblDesc.SetBinding(Label.TextProperty, "Desc");

            View = new StackLayout  // same idea as setting Content = xyz in a content page
            {
                Spacing = 2,
                Padding = 5,
                Children = { lblName, lblDesc }
            };

            // Context action
            MenuItem mi = new MenuItem { Text = "Delete", IsDestructive=true };
            mi.Clicked += (sender, e) =>
            {
                // The listview is accessible through the Parent property
                ListView lv = (ListView)this.Parent;
                
                // in this case, list.Remove(...) could be used ifwere were somewhere with access to it
                ((ObservableCollection<Fruit>)lv.ItemsSource).Remove(BindingContext as Fruit); 
                
            };
            ContextActions.Add(mi);
        }
    }
}