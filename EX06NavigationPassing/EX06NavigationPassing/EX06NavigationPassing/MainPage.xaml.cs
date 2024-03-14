using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EX06NavigationPassing
{
    public partial class MainPage : ContentPage
    {
        List<Person> people = new List<Person>();
        
   
        
        List<Button> btns = new List<Button>();
        public MainPage()
        {
            InitializeComponent();
            
            /* PART 1 */

            //btnJoe.Clicked += (sender, e) => { Navigation.PushAsync(new exr1(btnJoe.Text)); };
            //btnJane.Clicked += (sender, e) => { Navigation.PushAsync(new exr1(btnJane.Text)); };
            //btnJenny.Clicked += (sender, e) => { Navigation.PushAsync(new exr1(btnJenny.Text)); };

            people.Add(new Person("Carter", "Walsh"));
            people.Add(new Person("Taylor", "Wagner"));
            people.Add(new Person("Brett", "Slobodzian"));
            people.Add(new Person("Mike", "Williams"));
            people.Add(new Person("Joe", "Grubber"));
            people.Add(new Person("Jane", "Chippinz"));
            people.Add(new Person("Jenny", "Froobles"));

            /* PART 2 */

            for (int i = 0; i < people.Count; i++)
            {

                Button btn = new Button { Text=(people[i]).firstName };

                btn.Clicked += (sender, e) =>
                {
                    Navigation.PushAsync(new exr1((people[i]).firstName));
                };

                stklyt.Children.Add(btn);

                btns.Add(btn);
            }

            Button readChanges = new Button { Text = "Read Changes" };

            //readChanges.Clicked += (sender, e) => {
            //    for (int i = 0; i<btns.Count; i++)
            //    {
            //        btns[i].Text = people[i].firstName + " " + people[i].lastName; 
            //    }
            //};
            stklyt.Children.Add(readChanges);


        }

        protected override void OnAppearing()
        {
            for (int i = 0; i < btns.Count; i++)
            {
                btns[i].Text = people[i].firstName + " " + people[i].lastName;
            }
            base.OnAppearing();

        }
    }
}
