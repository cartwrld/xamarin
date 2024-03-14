using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace X07ListView
{
    public class FruitDetailPage : ContentPage
    {
        public FruitDetailPage(Fruit fruit)
        {
            Title = fruit.Name;
            
            // TableView to show a form for editing a Fruit instance

            StackLayout layout = new StackLayout { HorizontalOptions = LayoutOptions.Center };
            TableView table = new TableView { Intent = TableIntent.Form };
            EntryCell eName = new EntryCell { Label = "Name:", Text=fruit.Name };
            EntryCell eDesc = new EntryCell { Label = "Desc:",  Text=fruit.Desc };
            TableSection section = new TableSection(fruit.Name) { eName, eDesc };
            table.Root = new TableRoot() { section };
            layout.Children.Add(table);

            Button btnSave = new Button { Text = "Save Changes" };
            btnSave.Clicked += (sender, e) =>
            {
                fruit.Name = eName.Text;
                fruit.Desc = eDesc.Text;
                Navigation.PopAsync();
            };

            Button btnCancel = new Button { Text = "Cancel" };
            btnCancel.Clicked += (sender, e) => { Navigation.PopAsync(); };

            layout.Children.Add(btnSave);
            layout.Children.Add(btnCancel);

            Content = layout;

        }
    }
}