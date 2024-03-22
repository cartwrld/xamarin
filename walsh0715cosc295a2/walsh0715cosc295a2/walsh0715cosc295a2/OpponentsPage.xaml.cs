using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace walsh0715cosc295a2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpponentsPage : ContentPage
    {
        public OpponentsPage()
        {
            InitializeComponent();

            List<Opponent> oppList = App.OppDatabase.GetOpponents();

            Title = "Opponents";

            ListView lvOpps = new ListView
            {
                ItemsSource = oppList,
                ItemTemplate = new DataTemplate(typeof(OpponentCell)),
                RowHeight = 50
            };

            lvOpps.ItemTapped += (sender, e) =>
            {
                lvOpps.SelectedItem = null;
                Navigation.PushAsync(new MatchesPage((Opponent)e.Item));
            };

            StackLayout stklayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { lvOpps }
            };

            Content = stklayout;
        }
    }

    public class OpponentCell : ViewCell
    {
        public OpponentCell()
        { 
            Label lblFirst = new Label { FontSize = 20 };
            Label lblLast = new Label { FontSize = 20 };
            Label lblPhone = new Label { FontSize = 20, HorizontalOptions = LayoutOptions.End };

            lblFirst.SetBinding(Label.TextProperty, "fname");
            lblLast.SetBinding(Label.TextProperty, "lname");
            lblPhone.SetBinding(Label.TextProperty, "phone");

            StackLayout stkName = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { lblFirst, lblLast }
            };

            View = new StackLayout 
            { 
                Orientation = StackOrientation.Horizontal,
                Spacing = 50,
                Children = { stkName, lblPhone }, 
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center
            };

            MenuItem mi = new MenuItem { Text = "Delete", IsDestructive = true };
            mi.Clicked += (sender, e) =>
            {
                ListView lv = (ListView)this.Parent;

                ((ObservableCollection<Opponent>)lv.ItemsSource).Remove(BindingContext as Opponent);

            };
            ContextActions.Add(mi);
        }
    }
}