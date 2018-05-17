using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapApp.CustomControls;
using MapApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MapApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = new MainPageViewModel();
            ((MainPageViewModel)BindingContext).Navigation = this.Navigation;
            InitializeComponent();
           
        }
        
    }
}
