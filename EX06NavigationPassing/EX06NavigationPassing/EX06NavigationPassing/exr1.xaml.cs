using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EX06NavigationPassing
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class exr1 : ContentPage
    {
        public exr1(string name)
        {
            InitializeComponent();

            rootBtn.Clicked += (sender, e) => { Navigation.PopToRootAsync(); };

            personClicked.Text="You clicked on: " + name + "!";

        }

        //public exr1(Person person)
        //{
        //    InitializeComponent();

        //    person.setLn("Changed");

        //    rootBtn.Clicked += (sender, e) => { Navigation.PopToRootAsync(); };

        //    personClicked.Text = "You clicked on: " + person.firstName + "!";
        //}
    }
}