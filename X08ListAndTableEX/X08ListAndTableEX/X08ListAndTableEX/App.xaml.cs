﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace X08ListAndTableEX
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ToDoList();
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
    }
}
