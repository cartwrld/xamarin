using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace X03_SimpleCalculator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            btnCalc.Clicked += (sender, e) =>
            {
                var n1 = double.Parse(num1.Text);
                var n2 = double.Parse(num2.Text);
                var op = pickOp.SelectedItem;

                var answer = "";

                switch (op)
                {
                    case "Add": answer = (n1 + n2) + ""; break;
                    case "Subtract": answer = (n1 - n2) + ""; break;
                    case "Multiply": answer = (n1 * n2) + ""; break;
                    case "Divide": answer = (n1 / n2) + ""; break;
                }

                lblResult.Text = answer;

            };
        }
    }
}
