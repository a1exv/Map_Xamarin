using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MapApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPinView : ContentPage
    {
        public AddPinView()
        {
            InitializeComponent();
            BindingContext=new AddPinViewModel();
            ((AddPinViewModel) BindingContext).Navigation = this.Navigation;
        }
    }
}