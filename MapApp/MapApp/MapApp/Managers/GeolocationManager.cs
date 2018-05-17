using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MapApp.Managers
{
    public class GeolocationManager
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public void SetCoordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
            MessagingCenter.Instance.Send(this, "CoordinateChanged");
        }
    }
}
