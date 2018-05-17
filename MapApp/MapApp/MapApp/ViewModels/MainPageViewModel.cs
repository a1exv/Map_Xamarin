using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MapApp.CustomControls;
using MapApp.Views;
using Xamarin.Forms;

namespace MapApp.ViewModels
{
    public class MainPageViewModel:BaseViewModel
    {
        #region Fields
        private List<CustomPin> _pins;
        #endregion

        #region Properties

        public List<CustomPin> Pins
        {
            get
            {
                return _pins;
            }
            set
            {
               
                    _pins = value;
                    OnPropertyChanged();
               
            }
        }

        public ICommand AddPinCommand { get; set; }
        #endregion

        public MainPageViewModel()
        {
             GetPins();
            AddPinCommand=new Command(AddPin);
            MessagingCenter.Subscribe<AddPinViewModel>(this, "PinAdded", PinAdded);
        }

        #region Methods

        public void GetPins()
        {
            if (App.Current.Properties.ContainsKey("Pins"))
            {
                Pins = (List<CustomPin>)App.Current.Properties["Pins"];
            }
            
        }
        
        public void AddPin()
        {
            Navigation.PushAsync(new AddPinView());
        }

        private async void PinAdded(AddPinViewModel vm)
        {
            GetPins();
        }
        #endregion
    }
}
