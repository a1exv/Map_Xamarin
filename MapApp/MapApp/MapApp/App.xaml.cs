using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapApp.Managers;
using Xamarin.Forms;

namespace MapApp
{
    public partial class App : Application
    {
        public static GeolocationManager GeolocationManager { get; set; }
        public App()
        {
            InitializeComponent();
            GeolocationManager=new GeolocationManager();
            MainPage = new NavigationPage(new MapApp.Views.MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
