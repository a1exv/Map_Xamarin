using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MapApp.CustomControls
{
    public class CustomMap:Map
    {
        public List<CustomPin> CustomPins
        {
            get { return (List<CustomPin>) GetValue(CustomPinsProperty); }
            set
            {
                SetValue(CustomPinsProperty, value);
            }
        }


        public static readonly BindableProperty CustomPinsProperty =BindableProperty.Create(nameof(CustomPins), typeof(List<CustomPin>), typeof(CustomMap), null,
            BindingMode.TwoWay);
        public void SetLocation(double latitude, double longitude)
        {
            App.GeolocationManager.SetCoordinate(latitude, longitude);
        }

        public CustomMap() : base()
        {
            MoveToRegion(MapSpan.FromCenterAndRadius(new Position(53.9, 27.5), Distance.FromKilometers(20)));
        }
    }
}
