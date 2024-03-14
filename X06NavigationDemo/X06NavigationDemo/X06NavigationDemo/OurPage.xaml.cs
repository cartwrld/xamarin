using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace X06NavigationDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OurPage : ContentPage
    {
        public OurPage()
        {
            InitializeComponent();

            btnNew.Clicked += (sender, e) => { Navigation.PushAsync(new OurPage()); };
            btnNewModal.Clicked += (sender, e) => { Navigation.PushModalAsync(new OurPage());};
            btnPop.Clicked += (sender, e) => { Navigation.PopAsync();};
            btnPopModal.Clicked += (sender, e) => { Navigation.PopModalAsync();};
            btnPopToRoot.Clicked += (sender, e) => { Navigation.PopToRootAsync();};

            Title = "Our Page";

        }
    }
}