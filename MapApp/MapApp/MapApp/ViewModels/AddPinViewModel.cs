using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MapApp.CustomControls;
using MapApp.Managers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MapApp.ViewModels
{
    public class AddPinViewModel:BaseViewModel
    {
        #region Fields

        private string _name;
        private string _description;
        private int _rating;
        private double _latitude;
        private double _longitude;
        private bool _isWarningVisible;
        private bool _isSetCoordinate;
        #endregion


        #region Properties

        public GeolocationManager GeolocationManager => App.GeolocationManager;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get { return _description;}
            set {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged();
                } }
        }

        public int Rating
        {
            get { return _rating;}
            set
            {
                if (_rating != value)
                {
                    _rating = value;
                    OnPropertyChanged();
                }
            }
        }

        public double Latitude
        {
            get { return _latitude;}
            set {
                if (value != _latitude)
                {
                    _latitude = value;
                    OnPropertyChanged();
                } }
        }

        public double Longitude
        {
            get { return _longitude;}
            set {
                if (value != _longitude)
                {
                    _longitude = value;
                    OnPropertyChanged();
                } }
        }

        public bool IsWarningVisible
        {
            get { return _isWarningVisible;}
            set {
                if (_isWarningVisible != value)
                {
                    _isWarningVisible = value;
                    OnPropertyChanged();
                } }
        }

        public ICommand CreatePinCommand { get; set; }
        #endregion


        public AddPinViewModel()
        {
            CreatePinCommand = new Command(CreatePin);
            MessagingCenter.Instance.Subscribe<GeolocationManager>(this, "CoordinateChanged", SetCoordinate);
            IsWarningVisible = false;
            _isSetCoordinate = false;
            Rating = 5;
        }

        #region Methods

        private void CreatePin()
        {
            if (_isSetCoordinate && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Description))
            {
                List<CustomPin> pins;
                if (App.Current.Properties.ContainsKey("Pins"))
                {
                    pins = (List<CustomPin>) App.Current.Properties["Pins"];
                }
                else
                {
                    pins = new List<CustomPin>();
                    App.Current.Properties.Add("Pins", pins);
                }
                var pin = new CustomPin()
                {
                    Name = Name,
                    Description = Description,
                    Rating = Rating,
                    Position = new Position(Latitude, Longitude)
                };
                pins.Add(pin);
                App.Current.Properties["Pins"] = pins;
                Navigation.PopAsync();
                MessagingCenter.Send(this, "PinAdded");
                
            }
            else IsWarningVisible = true;
        }

        public async void SetCoordinate(GeolocationManager manager)
        {
            Latitude = manager.Latitude;
            Longitude = manager.Longitude;
            _isSetCoordinate = true;
        }

        #endregion
    }
}
